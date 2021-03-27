using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IQFeed.CSharpApiClient.Common {
    internal struct FrequencyStopwatch {
        private readonly long _frequency;
        private long _elapsed;
        private long _startTimeStamp;
        private bool _isRunning;

        public DateTime StartTime => new DateTime(_elapsed);

        public FrequencyStopwatch(long frequency) {
            _frequency = frequency;
            _elapsed = 0;
            _startTimeStamp = 0;
            _isRunning = false;
        }

        public FrequencyStopwatch(long frequency, bool start) {
            _frequency = frequency;
            _elapsed = 0;
            _isRunning = true;
            _startTimeStamp = start ? GetTimestamp() : 0;
        }

        /// <summary>Starts, or resumes, measuring elapsed time for an interval.</summary>
        public void Start() {
            if (_isRunning)
                return;
            _startTimeStamp = GetTimestamp();
            _isRunning = true;
        }

        /// <summary>Initializes a new <see cref="T:System.Diagnostics.StopwatchStruct" /> instance, sets the elapsed time property to zero, and starts measuring elapsed time.</summary>
        /// <returns>A <see cref="T:System.Diagnostics.StopwatchStruct" /> that has just begun measuring elapsed time.</returns>
        public static FrequencyStopwatch StartNew(long frequency) {
            return new FrequencyStopwatch(frequency, true);
        }

        /// <summary>Stops measuring elapsed time for an interval.</summary>
        public void Stop() {
            if (!_isRunning)
                return;
            _elapsed += GetTimestamp() - _startTimeStamp;
            _isRunning = false;
            if (_elapsed >= 0L)
                return;
            _elapsed = 0L;
        }

        /// <summary>Stops time interval measurement and resets the elapsed time to zero.</summary>
        public void Reset() {
            _elapsed = 0;
            _startTimeStamp = 0;
            _isRunning = false;
        }

        /// <summary>Stops time interval measurement, resets the elapsed time to zero, and starts measuring elapsed time.</summary>
        public void Restart() {
            _elapsed = 0L;
            _startTimeStamp = GetTimestamp();
            _isRunning = true;
        }

        /// <summary>If the time elapsed so far surpassed <paramref name="stepFrequency"/> then it'll return ignore any excess time that do not fit into a step that we'll taken into account on next checkpoint.</summary>
        /// <remarks>Has no isRunning check</remarks>
        /// <returns>Ticks</returns>
        public long Checkpoint() {
            long elapsedFreq = GetTimestamp() - _startTimeStamp;
            if (elapsedFreq >= _frequency) {
                elapsedFreq -= elapsedFreq % _frequency; //trim
                _startTimeStamp += elapsedFreq;
                return elapsedFreq;
            } else
                return 0;
        }

        /// <summary>If the time elapsed so far surpassed <paramref name="stepFrequency"/> then it'll return ignore any excess time that do not fit into a step that we'll taken into account on next checkpoint.</summary>
        /// <remarks>Has no isRunning check</remarks>
        /// <returns>Ticks</returns>
        public long Excess => (GetTimestamp() - _startTimeStamp) % _frequency;

        /// <summary>If the time elapsed so far surpassed <paramref name="stepFrequency"/> then it'll return ignore any excess time that do not fit into a step that we'll taken into account on next checkpoint.</summary>
        /// <remarks>Has no isRunning check</remarks>
        /// <returns>Milliseconds</returns>
        public int ExcessMilliseconds => (int) new TimeSpan((GetTimestamp() - _startTimeStamp) % _frequency).TotalMilliseconds;

        /// <summary>Gets a value indicating whether the <see cref="T:System.Diagnostics.StopwatchStruct" /> timer is running.</summary>
        /// <returns>
        /// <see langword="true" /> if the <see cref="T:System.Diagnostics.StopwatchStruct" /> instance is currently running and measuring elapsed time for an interval; otherwise, <see langword="false" />.</returns>

        public bool IsRunning => _isRunning;

        /// <summary>Gets the total elapsed time measured by the current instance.</summary>
        /// <returns>A read-only <see cref="T:System.TimeSpan" /> representing the total elapsed time measured by the current instance.</returns>

        public TimeSpan Elapsed => new TimeSpan(GetElapsedDateTimeTicks());

        /// <summary>Gets the total elapsed time measured by the current instance, in milliseconds.</summary>
        /// <returns>A read-only long integer representing the total number of milliseconds measured by the current instance.</returns>

        public long ElapsedMilliseconds => GetElapsedDateTimeTicks() / 10000L;

        /// <summary>Gets the total elapsed time measured by the current instance, in timer ticks.</summary>
        /// <returns>A read-only long integer representing the total number of timer ticks measured by the current instance.</returns>

        public long ElapsedTicks => GetRawElapsedTicks();


        /// <summary>Gets the current number of ticks in the timer mechanism.</summary>
        /// <returns>A long integer representing the tick counter value of the underlying timer mechanism.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long GetTimestamp() => DateTime.UtcNow.Ticks;


        private long GetRawElapsedTicks() {
            long elapsed = this._elapsed;
            if (!_isRunning)
                return elapsed;
            long num = GetTimestamp() - _startTimeStamp;
            elapsed += num;

            return elapsed;
        }

        private long GetElapsedDateTimeTicks() {
            return GetRawElapsedTicks();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long FromRaw(long rawCounter) {
            return rawCounter;
        }
    }
}