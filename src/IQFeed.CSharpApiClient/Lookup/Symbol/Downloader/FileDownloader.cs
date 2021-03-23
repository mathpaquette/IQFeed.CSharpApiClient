using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;

namespace IQFeed.CSharpApiClient.Lookup.Symbol.Downloader
{
    public class FileDownloader
    {
        private readonly ILocalCacheStrategy _localCacheStrategy;

        public FileDownloader(ILocalCacheStrategy localCacheStrategy)
        {
            _localCacheStrategy = localCacheStrategy;
        }

        public string GetFile(string url, bool useCache, TimeSpan expiration)
        {
            return GetFile(Path.GetTempPath(), url, useCache, expiration);
        }

        public string GetFile(string downloadFolder, string url, bool useCache, TimeSpan expiration)
        {
            var fileName = GetFileNameFromUrl(url);
            var downloadPath = Path.Combine(downloadFolder, fileName);
            return useCache ? GetFileWithCache(downloadPath, url, expiration) : GetFileWithoutCache(downloadPath, url);
        }

        private string GetFileWithCache(string archivePath, string url, TimeSpan expiration)
        {
            return _localCacheStrategy.HasExpired(archivePath, expiration) ?
                GetFileWithoutCache(archivePath, url) :
                GetArchivedFile(archivePath, false);
        }

        private string GetFileWithoutCache(string archivePath, string url)
        {
            using (var myWebClient = new WebClient())
            {
                myWebClient.DownloadFile(url, archivePath);
            }

            return GetArchivedFile(archivePath, true);
        }

        private string GetArchivedFile(string archivePath, bool extractFile)
        {
            var archiveDirectoryName = new FileInfo(archivePath).DirectoryName;

            using (var archive = ZipFile.OpenRead(archivePath))
            {
                var archiveFile = archive.Entries.Single();
                var fileName = Path.Combine(archiveDirectoryName, archiveFile.FullName);
                
                if (!File.Exists(fileName) || extractFile)
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