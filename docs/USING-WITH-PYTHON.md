# IQFeed.CSharpApiClient with Python
The purpose of the document is to show How-To use IQFeed.CSharpApiClient with Python. Feel free to improve this doc by creating a PR.  
You'll find more [examples](#additional-examples) down below.

## How-To
This is pretty straightforward to use a compiled DLL through Python code with certain conditions. For more [info](https://github.com/pythonnet/pythonnet/wiki).

##### Step 1 - Install Python for .NET
`pip install pythonnet`

##### Step 2 - Download and extract the DLL
- Go to [Nuget.org](https://www.nuget.org/packages/IQFeed.CSharpApiClient/) and click 'Download package' to fetch the latest .nupkg file.  
- Then use your favorite zip utility, i.e. 7-Zip, WinRAR, etc. to extract files.  
- Copy DLL file from `lib\net461\IQFeed.CSharpApiClient.dll` to one folder.

##### Step 3 - Execute the python code 
- Set `<folder>` where you extracted the DLL file
- Set your `<login> <password>` and `<product_id>`
- Run the code below

```python
# Dynamically add IQFeed.CSharpApiClient DLL
assembly_path = r"C:\<folder>"

import sys
sys.path.append(assembly_path)

# Reference IQFeed.CSharpApiClient DLL
import clr
clr.AddReference("IQFeed.CSharpApiClient")

from IQFeed.CSharpApiClient import IQFeedLauncher

# Step 1 - Run IQConnect launcher
IQFeedLauncher.Start("<login>", "<password>", "<product_id>")

# Step 2 - Use the appropriate factory to create the client
from IQFeed.CSharpApiClient.Lookup import LookupClientFactory
lookupClient = LookupClientFactory.CreateNew()

# Step 3 - Connect it
lookupClient.Connect()

# Step 4 - Make any requests you need or want! 
ticks = lookupClient.Historical.GetHistoryTickDatapoints("AAPL", 100)

for tick in ticks:
    print(tick)

print('Completed!')
```


## Additional examples

#### Historical (for large request)
```python
assembly_path = r"C:\<folder>"
import sys
sys.path.append(assembly_path)
import clr
clr.AddReference("IQFeed.CSharpApiClient")
from IQFeed.CSharpApiClient import IQFeedLauncher
IQFeedLauncher.Start("<login>", "<password>", "<product_id>")

from IQFeed.CSharpApiClient.Lookup import LookupClientFactory
from IQFeed.CSharpApiClient.Lookup.Historical.Messages import TickMessage
import os

# Create Lookup client
lookupClient = LookupClientFactory.CreateNew()

# Connect
lookupClient.Connect()

# Save ticks to disk
ticksFilename = lookupClient.Historical.File.GetHistoryTickDatapoints("AAPL", 100)

# Move tmp filename
dstTicksFilename = "ticks.csv"
os.replace(ticksFilename, dstTicksFilename)

# Read ticks from disk
ticksFromFile = TickMessage.ParseFromFile(dstTicksFilename)
for tick in ticksFromFile:
    print(tick)
```


#### Streaming Level 1
```python
assembly_path = r"C:\<folder>"
import sys
sys.path.append(assembly_path)
import clr
clr.AddReference("IQFeed.CSharpApiClient")
from IQFeed.CSharpApiClient import IQFeedLauncher
IQFeedLauncher.Start("<login>", "<password>", "<product_id>")

from IQFeed.CSharpApiClient.Streaming.Level1 import Level1ClientFactory
import time

# Create Level1 client
level1Client = Level1ClientFactory.CreateNew()

# Level 1 handler function
def level1UpdateSummaryHandler(msg):
    print(msg)

# Subscribe to Summary/Update events
level1Client.Summary += level1UpdateSummaryHandler
level1Client.Update += level1UpdateSummaryHandler

# Connect
level1Client.Connect()

# Request streaming
level1Client.ReqWatch("AAPL")

# Wait 30 seconds
time.sleep(30)
```

#### Streaming Level 2
```python
assembly_path = r"C:\<folder>"
import sys
sys.path.append(assembly_path)
import clr
clr.AddReference("IQFeed.CSharpApiClient")

from IQFeed.CSharpApiClient import IQFeedLauncher
IQFeedLauncher.Start()

from IQFeed.CSharpApiClient.Streaming.Level2 import Level2ClientFactory
import time

# Create Level2 client
level2Client = Level2ClientFactory.CreateNew()

# Level 2 handler function
def level2UpdateSummaryHandler(msg):
    print(msg)

# Subscribe to Summary/Update events
level2Client.System += level2UpdateSummaryHandler
level2Client.Error += level2UpdateSummaryHandler
level2Client.Summary += level2UpdateSummaryHandler
level2Client.Update += level2UpdateSummaryHandler

# Connect
level2Client.Connect()

# Request streaming
level2Client.ReqWatch("AAPL")

# Wait 30 seconds
time.sleep(30)
```