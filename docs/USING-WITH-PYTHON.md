# IQFeed.CSharpApiClient with Python
The purpose of the document is to show How-To use IQFeed.CSharpApiClient with python. Feel free to improve this doc by creating a PR.

## How-To
This is pretty straightforward to use a compiled DLL through Python code with certain conditions. For more [info](https://github.com/pythonnet/pythonnet/wiki).

##### Step 1 - Install Python for .NET
`pip install pythonnet`

##### Step 2 - Download and extract the DLL
- Go to [Nuget.org](https://www.nuget.org/packages/IQFeed.CSharpApiClient/) and click 'Download package' to fetch the latest .nupkg file.  
- Then use your favorite zip utility, i.e. 7-Zip, WinRAR, etc. to extract files.  
- Copy DLL file from `lib\net45\IQFeed.CSharpApiClient.dll` to one folder.

##### Step 3 - Execute the python code 
- Set `<folder>` where you extracted the DLL file
- Set your `<login> <password>` and `<product_id>`
- Run the code below

```python
## Dynamically add IQFeed.CSharpApiClient DLL
assembly_path = r"C:\<folder>"

import sys
sys.path.append(assembly_path)

## Reference IQFeed.CSharpApiClient DLL
import clr
clr.AddReference("IQFeed.CSharpApiClient")

from IQFeed.CSharpApiClient import IQFeedLauncher
from IQFeed.CSharpApiClient.Lookup import LookupClientFactory

## Step 1 - Run IQConnect launcher
IQFeedLauncher.Start("<login>", "<password>", "<product_id>")

## Step 2 - Use the appropriate factory to create the client
lookupClient = LookupClientFactory.CreateNew()

## Step 3 - Connect it
lookupClient.Connect()

# Step 4 - Make any requests you need or want! 
tickMessages = lookupClient.Historical.GetHistoryTickDatapoints("AAPL", 100)

for tick in tickMessages:
    print(tick)

print('Completed!')
```