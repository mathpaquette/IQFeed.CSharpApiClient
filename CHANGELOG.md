### IQFeed.CSharpApiClient 2.6.2 - April 03, 2021

##### Issues Resolved
* Fixed issue with LookupRateLimiter when > 8 clients on burst of requests
* Fixed issue with TickMessages resampler with 'O'
* Fixed issue with Dynamic Fields and Type field


### IQFeed.CSharpApiClient 2.6.1 - March 06, 2021
* Added TryParse on Lookup data to detect Invalid data coming from IQFeed


### IQFeed.CSharpApiClient 2.6.0 - December 17, 2020
* Added Lookup requests rate limiter required by [IQFeed](http://forums.iqfeed.net/index.cfm?page=topic&topicID=5832)


### IQFeed.CSharpApiClient 2.5.1 - August 1, 2020
* Replaced Market Symbols and Expired Options cache strategies


### IQFeed.CSharpApiClient 2.5.0 - June 17, 2020
* Added DynamicFields support for Level1
* Added News support for Historical
* Added ToCsv on FundamentalMessage

##### Issues Resolved
* Adjusted RegEx for parsing IntervalBarMessage

##### Breaking Changes
 * Removed support for generic Messages
 * Added IUpdateSummaryMessage for exposing Summary and Update events from Level1
 * Dropped support for NET45

### IQFeed.CSharpApiClient 2.1.0 - April 18, 2020
* Added synchronized methods to support Python .NET


### IQFeed.CSharpApiClient 2.0.2 - April 10, 2020
* Added request sent to IQFeed when throwing IQFeed Exception
##### Issues Resolved
 * Fixed IsPortOpen hang network issue with .NET Core 3


### IQFeed.CSharpApiClient 2.0.1 - January 30, 2020
* Added generics on most messages for better flexibility and reuse
* Added message handlers on most clients to support `<float>`, `<double>` and `<decimal>` messages.
* Added extension methods for message conversion
* Removed explicit framework target on net461
##### Breaking Changes
 * Migrated from float to double for most messages
 * Type needs to be specified for client and message instantiation
 * Renamed all Async methods with Get prefix


### IQFeed.CSharpApiClient 1.5.0 - October 27, 2019
* Added Level 2 data support
* Replaced DateTime for time in L1 and L2 UpdateSummaryMessage with TimeSpan for better readability


### IQFeed.CSharpApiClient 1.4.6 - October 10, 2019
* Added Symbol and Market Info Lookup support
* Added more information when throwing IQFeed exception

##### Issues Resolved
 * Added better error detection to avoid true negative with request Id and some edge cases.


### IQFeed.CSharpApiClient 1.4.4 - July 31, 2019
##### Issues Resolved
 * Fixed ReqBarWatch request format from derivative data
 * Removed deprecated license warning in VS2017+


### IQFeed.CSharpApiClient 1.4.1 - August 12, 2018
##### Issues Resolved
 * Reversed Open and Close position in IntervalMessage and DailyWeeklyMonthlyMessage
 * Historical requests supporting RequestId parameter
 * IntervalMessage can support larger TotalVolume


### IQFeed.CSharpApiClient 1.4.0 - August 5, 2018
This release contains a significant amount of improvements. More importantly, we added support
for derivative data and now supporting the latest protocol version 6.0. All messages parsing are
 now culture invariant and in the same way, we increased a lot our unit testing.

* Added Derivative data support
* Added support for protocol version 6.0
* Added BaseFacade, BaseMessageHandler and customs IQFeedException
* ChainsFacade now returning chains instances
* Added NumberOfTrades in IntervalMessage
* Added DataDirection enum parameter for all historical request

##### Issues Resolved
 * Messages parsing are now culture independent
 * LookupClient BufferSize can be customized
 * DailyWeeklyMonthlyMessage can handle large value
 * Workaround for test coverage