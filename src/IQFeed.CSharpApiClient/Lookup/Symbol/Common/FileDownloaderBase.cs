using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Security.Cryptography;

namespace IQFeed.CSharpApiClient.Lookup.Symbol.Common
{
    public abstract class FileDownloaderBase
    {
        private readonly IFileModificationStrategy _fileModificationStrategy;

        protected FileDownloaderBase(IFileModificationStrategy fileModificationStrategy)
        {
            _fileModificationStrategy = fileModificationStrategy;

            // https://stackoverflow.com/questions/2859790/the-request-was-aborted-could-not-create-ssl-tls-secure-channel
#if NET45
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
#endif
        }

        public string GetFile(string url, string downloadPath = null, bool useCache = true)
        {
            var archiveName = GetFileNameFromUrl(url);
            var archivePath = downloadPath ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, archiveName);

            return useCache ? GetFileWithCache(archivePath, url) : GetFileWithoutCache(archivePath, url);
        }

        private string GetFileWithCache(string archivePath, string url)
        {
            FileCacheInfo fileCacheInfo;
            string archivedFilePath;
            var lastModificationTimestamp = _fileModificationStrategy.GetLastModificationTimestamp(url);
            var cachePath = $"{archivePath}.cache";

            // check for cache
            if (File.Exists(archivePath) && File.Exists(cachePath))
            {
                fileCacheInfo = FileCacheInfo.Load(cachePath);
                archivedFilePath = GetArchivedFile(archivePath);
                var archiveMd5 = GetFileMD5(archivePath);

                if (fileCacheInfo.LastModificationTimestamp == lastModificationTimestamp && fileCacheInfo.Checksum == archiveMd5)
                    return File.Exists(archivedFilePath) ? archivedFilePath : GetArchivedFile(archivePath, true);
            }

            // download the file
            using (var webClient = new WebClient())
            {
                webClient.DownloadFile(url, archivePath);
            }

            // write cache info
            archivedFilePath = GetArchivedFile(archivePath, true);
            fileCacheInfo = new FileCacheInfo(lastModificationTimestamp, GetFileMD5(archivePath));
            fileCacheInfo.Save(cachePath);

            return archivedFilePath;
        }

        private string GetFileWithoutCache(string archivePath, string url)
        {
            // download the file
            using (var myWebClient = new WebClient())
            {
                myWebClient.DownloadFile(url, archivePath);
            }

            return GetArchivedFile(archivePath);
        }

        private string GetFileMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        private string GetArchivedFile(string archivePath, bool extractFile = false)
        {
            var archiveDirectoryName = new FileInfo(archivePath).DirectoryName;

            using (var archive = ZipFile.OpenRead(archivePath))
            {
                var archiveFile = archive.Entries.Single();
                var fileName = Path.Combine(archiveDirectoryName, archiveFile.FullName);
                if (extractFile)
                    archiveFile.ExtractToFile(fileName, true);
                return fileName;
            }
        }

        private string GetFileNameFromUrl(string url)
        {
            var uri = new Uri(url);
            return Path.GetFileName(uri.LocalPath);
        }
    }
}