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