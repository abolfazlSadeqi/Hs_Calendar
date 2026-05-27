using Hs_Calender;
using HsCalendar;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Xml;

namespace Hs_Calendar;

public  sealed   class TimeInfo
{
    #region ================= CORE =================

    private readonly TimeSpan _ts;
    private readonly HsDate _convert;

    
    public TimeInfo(TimeSpan? timeSpan)
    {
        _ts = timeSpan is null? DateTime.Now.TimeOfDay: timeSpan.Value;
        _convert = new HsDate();
    }

    public TimeSpan Value => _ts;

    #endregion

    #region ================= RAW =================

    public TimeSpan Raw => _ts;

    public long Ticks => _ts.Ticks;

    public int Days => _ts.Days;
    public int Hours => _ts.Hours;
    public int Minutes => _ts.Minutes;
    public int Seconds => _ts.Seconds;
    public int Milliseconds => _ts.Milliseconds;

    public long TotalTicks => _ts.Ticks;

    public double TotalDays => _ts.TotalDays;
    public double TotalHours => _ts.TotalHours;
    public double TotalMinutes => _ts.TotalMinutes;
    public double TotalSeconds => _ts.TotalSeconds;
    public double TotalMilliseconds => _ts.TotalMilliseconds;

    // ================= Extra useful helpers =================

    public bool IsZero => _ts == TimeSpan.Zero;
    public bool IsPositive => _ts > TimeSpan.Zero;
    public bool IsNegative => _ts < TimeSpan.Zero;

    public TimeSpan Abs => _ts.Duration();
    public TimeSpan Negate => -_ts;

    public int Sign => _ts == TimeSpan.Zero ? 0 : (_ts.Ticks > 0 ? 1 : -1);

    // Safe components (normalized absolute parts)
    public int AbsDays => Math.Abs(_ts.Days);
    public int AbsHours => Math.Abs(_ts.Hours);
    public int AbsMinutes => Math.Abs(_ts.Minutes);
    public int AbsSeconds => Math.Abs(_ts.Seconds);
    public int AbsMilliseconds => Math.Abs(_ts.Milliseconds);

    // Breakdown from absolute value (normalized)
    public int NormalizedDays => _ts.Duration().Days;
    public int NormalizedHours => _ts.Duration().Hours;
    public int NormalizedMinutes => _ts.Duration().Minutes;
    public int NormalizedSeconds => _ts.Duration().Seconds;
    public int NormalizedMilliseconds => _ts.Duration().Milliseconds;

    // Formatting helpers
    public string ToInvariantString() => _ts.ToString("c");
    public string ToShortString() => $"{_ts.Days}.{_ts.Hours:00}:{_ts.Minutes:00}:{_ts.Seconds:00}";
    public string ToCompactString() => $"{_ts:hh\\:mm\\:ss}";

    // ISO 8601 duration (basic)
    public string ToIso8601()
    {
        var ts = _ts.Duration();
        return $"P{ts.Days}DT{ts.Hours}H{ts.Minutes}M{ts.Seconds}S";
    }

    #endregion

    #region ================= NORMALIZATION =================

    public TimeSpan NormalizeMilliseconds =>
        TimeSpan.FromMilliseconds(SafeRound(TotalMilliseconds));

    public TimeSpan NormalizeSeconds =>
        TimeSpan.FromSeconds(SafeRound(TotalSeconds));

    public TimeSpan NormalizeMinutes =>
        TimeSpan.FromMinutes(SafeRound(TotalMinutes));

    public TimeSpan NormalizeHours =>
        TimeSpan.FromHours(SafeRound(TotalHours));

    public TimeSpan NormalizeDays =>
        TimeSpan.FromDays(SafeRound(TotalDays));

    #endregion


    #region ================= HIGH PRECISION NORMALIZATION =================

    public TimeSpan NormalizeSecondsPrecise =>
        TimeSpan.FromTicks((long)Math.Round(TotalSeconds * TimeSpan.TicksPerSecond));

    public TimeSpan NormalizeMinutesPrecise =>
        TimeSpan.FromTicks((long)Math.Round(TotalMinutes * TimeSpan.TicksPerMinute));

    public TimeSpan NormalizeHoursPrecise =>
        TimeSpan.FromTicks((long)Math.Round(TotalHours * TimeSpan.TicksPerHour));

    #endregion

    #region ================= SAFE CORE =================

    private  double SafeRound(double value)
    {
        if (double.IsNaN(value) || double.IsInfinity(value))
            return 0;

        if (value > double.MaxValue / 2) return double.MaxValue / 2;
        if (value < double.MinValue / 2) return double.MinValue / 2;

        return Math.Round(value);
    }

    #endregion

    #region ================= PERSIAN NUMBERS =================

    private string ToPersian(int value)
    {
        return _convert?.ToPersianDigits(value.ToString()) ?? value.ToString();
    }

    private string ToPersian(long value)
    {
        return _convert?.ToPersianDigits(value.ToString()) ?? value.ToString();
    }

    private string ToPersian(double value)
    {
        return _convert?.ToPersianDigits(value.ToString()) ?? value.ToString();
    }

   private string Pd<T>(T value)
    {
        if (value == null)
            return string.Empty;

        return _convert?.ToPersianDigits(value.ToString()) ?? value.ToString();
    }

    #region ---- Time Components (Days / Hours / Minutes / Seconds) ----

    public string PersianDays => ToPersian(Days);
    public string PersianHours => ToPersian(Hours);
    public string PersianMinutes => ToPersian(Minutes);
    public string PersianSeconds => ToPersian(Seconds);

    #endregion

    #region ---- Total Time Values ----

    public string PersianTotalHours => ToPersian((long)TotalHours);
    public string PersianTotalMinutes => ToPersian((long)TotalMinutes);
    public string PersianTotalSeconds => ToPersian((long)TotalSeconds);

    #endregion

    #region ---- Optional: Formatted TimeSpan (Very Useful) ----

    public string PersianTimeFormatted =>
        $"{PersianDays}:{PersianHours}:{PersianMinutes}:{PersianSeconds}";

    #endregion

    #region ---- Optional: Raw numeric access in Persian ----

    public string PersianDaysRaw => ToPersian(Days);
    public string PersianHoursRaw => ToPersian(Hours);
    public string PersianMinutesRaw => ToPersian(Minutes);
    public string PersianSecondsRaw => ToPersian(Seconds);

    #endregion

    #endregion
    #region ================= WORD CONVERT =================

    private string ToPersianWords(int value)
        => NumberToWordsConverter.NumberToPersianWords(value);

    private string ToEnglishWords(int value)
        => NumberToWordsConverter.NumberToEnglishWords(value);

    public enum Language
    {
        Persian,
        English
    }

    private string ToWords(int value, Language lang)
    {
        return lang switch
        {
            Language.Persian => NumberToWordsConverter.NumberToPersianWords(value),
            Language.English => NumberToWordsConverter.NumberToEnglishWords(value),
            _ => value.ToString()
        };
    }

    // ================= DAYS =================
    public string DaysText(Language lang = Language.Persian)
        => ToWords(Days, lang);

    public string DaysPersianText => DaysText(Language.Persian);
    public string DaysEnglishText => DaysText(Language.English);

    // ================= HOURS =================
    public string HoursText(Language lang = Language.Persian)
        => ToWords(Hours, lang);

    public string HoursPersianText => HoursText(Language.Persian);
    public string HoursEnglishText => HoursText(Language.English);

    #endregion

    #region ================= FORMATS =================

    #region ================= BASIC FORMATS =================

    public string HHmmss => $"{Hours:00}:{Minutes:00}:{Seconds:00}";
    public string HHmm => $"{Hours:00}:{Minutes:00}";
    public string HHmmssfff => $"{Hours:00}:{Minutes:00}:{Seconds:00}.{Milliseconds:000}";

    public string Compact => $"{Hours:00}{Minutes:00}{Seconds:00}";
    public string CompactWithMs => $"{Hours:00}{Minutes:00}{Seconds:00}{Milliseconds:000}";

    #endregion


    #region ================= HUMAN READABLE =================

    public string HumanReadable
    {
        get
        {
            var parts = new List<string>();

            if (Days > 0) parts.Add($"{Days} day(s)");
            if (Hours > 0) parts.Add($"{Hours} hour(s)");
            if (Minutes > 0) parts.Add($"{Minutes} minute(s)");
            if (Seconds > 0) parts.Add($"{Seconds} second(s)");

            return parts.Count > 0 ? string.Join(", ", parts) : "0 seconds";
        }
    }

    public string PersianReadable =>
        $"{Days} روز، {Hours} ساعت، {Minutes} دقیقه، {Seconds} ثانیه";

    #endregion


    #region ================= CLOCK FORMATS =================

    public string Clock24 => DateTime.Today.Add(_ts).ToString("HH:mm:ss");
    public string Clock24WithMs => DateTime.Today.Add(_ts).ToString("HH:mm:ss.fff");

    public string Clock12 => DateTime.Today.Add(_ts).ToString("hh:mm:ss tt");
    public string Clock12Short => DateTime.Today.Add(_ts).ToString("hh:mm tt");

    #endregion


    #region ================= FILE / SERIALIZATION =================

    public string FileFormat => $"{Days}_{Hours}_{Minutes}_{Seconds}";
    public string FileFormatFull => $"{Days}_{Hours}_{Minutes}_{Seconds}_{Milliseconds}";

    public string LogFormat => $"[{Days:00}:{Hours:00}:{Minutes:00}:{Seconds:00}]";

    public string IsoLike => $"{Days:D2}.{Hours:D2}:{Minutes:D2}:{Seconds:D2}";

    #endregion


    #region ================= URL / SAFE STRING =================

    public string UrlFormat => $"{Days}-d-{Hours}-h-{Minutes}-m";
    public string Slug => $"{Days}d-{Hours}h-{Minutes}m-{Seconds}s";
    public string QueryString => $"d={Days}&h={Hours}&m={Minutes}&s={Seconds}";

    #endregion


    #region ================= STOPWATCH / PERFORMANCE =================

    public string Stopwatch => HHmmssfff;
    public string StopwatchLong => HHmmssfff + $" ({Milliseconds} ms)";

    #endregion


    #region ================= COMPARISON HELPERS =================

    public bool IsLessThanMinute => _ts.TotalMinutes < 1;
    public bool IsLessThanHour => _ts.TotalHours < 1;

    #endregion


    #region ================= CUSTOM FORMATS =================

    public string SmartFormat =>
        _ts.TotalDays >= 1 ? $"{Days}d {Hours}h {Minutes}m" :
        _ts.TotalHours >= 1 ? $"{Hours}h {Minutes}m {Seconds}s" :
        _ts.TotalMinutes >= 1 ? $"{Minutes}m {Seconds}s" :
        $"{Seconds}s";

    public string DigitalClock => $"{Hours:00}:{Minutes:00}:{Seconds:00}";

    #endregion
    #endregion

    #region ================= HUMANIZE =================

    public string HumanizeFa
    {
        get
        {
            var p = new List<string>();

            if (Days > 0) p.Add($"{Pd(Days)} روز");
            if (Hours > 0) p.Add($"{Pd(Hours)} ساعت");
            if (Minutes > 0) p.Add($"{Pd(Minutes)} دقیقه");
            if (Seconds > 0) p.Add($"{Pd(Seconds)} ثانیه");

            return p.Count == 0 ? "۰ ثانیه" : string.Join(" و ", p);
        }
    }

    public string HumanizeEn
    {
        get
        {
            var p = new List<string>();

            if (Days > 0) p.Add($"{Days} day");
            if (Hours > 0) p.Add($"{Hours} hour");
            if (Minutes > 0) p.Add($"{Minutes} minute");
            if (Seconds > 0) p.Add($"{Seconds} second");

            return p.Count == 0 ? "0 second" : string.Join(", ", p);
        }
    }

    #endregion

    #region ================= RELATIVE =================
    private bool IsJustNow => TotalSeconds < 5;
    private bool IsSeconds => TotalMinutes < 1;
    private bool IsMinutes => TotalHours < 1;
    private bool IsHours => TotalDays < 1;
    private bool IsDays => TotalDays < 2;
    private bool IsShortDays => TotalDays < 7;

    public string AgoFa => $"{HumanizeFa} پیش";
    public string LaterFa => $"{HumanizeFa} بعد";

    public string AgoEn => $"{HumanizeEn} ago";
    public string LaterEn => $"in {HumanizeEn}";

    public bool IsSlaBreached(TimeSpan limit) => _ts > limit;
    public bool IsNearSla(TimeSpan limit) => _ts > limit * 0.9;

    public string GetFriendlyLabel()
    {
        if (IsMicro) return "Micro";
        if (IsShort) return "Short";
        if (IsMedium) return "Medium";
        if (IsLong) return "Long";
        return "Very Long";
    }

    public string GetBucket()
    {
        return TotalMinutes switch
        {
            < 1 => "0-1m",
            < 5 => "1-5m",
            < 30 => "5-30m",
            < 60 => "30-60m",
            < 240 => "1-4h",
            < 1440 => "1d",
            _ => "1d+"
        };
    }
    #endregion

    #region ================= SMART RANGE =================

    public string Range =>
        TotalSeconds < 60 ? "seconds" :
        TotalMinutes < 60 ? "minutes" :
        TotalHours < 24 ? "hours" :
        TotalDays < 7 ? "days" :
        TotalDays < 30 ? "weeks" :
        TotalDays < 365 ? "months" : "years";

    public string RangeFa =>
        TotalSeconds < 60 ? "ثانیه" :
        TotalMinutes < 60 ? "دقیقه" :
        TotalHours < 24 ? "ساعت" :
        TotalDays < 7 ? "روز" :
        TotalDays < 30 ? "هفته" :
        TotalDays < 365 ? "ماه" : "سال";

    #endregion

    #region ================= BUCKETS =================

    public string Bucket =>
        TotalSeconds < 5 ? "instant" :
        TotalSeconds < 60 ? "very_short" :
        TotalMinutes < 10 ? "short" :
        TotalMinutes < 60 ? "medium" :
        TotalHours < 24 ? "long" :
        TotalDays < 7 ? "very_long" : "extreme";

    #endregion

    #region ================= CLASSIFICATION =================

    // --- Duration based ---
    public bool IsMicro => TotalSeconds < 10;
    public bool IsVeryShort => TotalMinutes < 1;
    public bool IsShort => TotalMinutes < 5;
    public bool IsMedium => TotalMinutes >= 5 && TotalHours < 6;
    public bool IsLong => TotalDays >= 1;
    public bool IsVeryLong => TotalDays >= 7;
    public bool IsExtreme => TotalDays >= 30;
    public bool IsHuge => TotalDays > 365;

    // --- Work / business patterns ---
    public bool IsBusinessHour => TotalHours >= 8 && TotalHours < 18;
    public bool IsAfterWork => TotalHours >= 18 && TotalHours < 23;
    public bool IsNightShift => TotalHours >= 0 && TotalHours < 6;

    // --- Time of day (more granular) ---
    public bool IsMorning => TotalHours >= 5 && TotalHours < 12;
    public bool IsAfternoon => TotalHours >= 12 && TotalHours < 17;
    public bool IsEvening => TotalHours >= 17 && TotalHours < 21;
    public bool IsNight => TotalHours >= 21 || TotalHours < 5;
    public bool IsSleepTime => TotalHours >= 23 || TotalHours < 6;

    // --- Contextual (modern systems) ---
    public bool IsPeakHour => (TotalHours >= 7 && TotalHours <= 9) || (TotalHours >= 17 && TotalHours <= 20);
    public bool IsOffPeak => !IsPeakHour;
    public bool IsLunchTime => TotalHours >= 12 && TotalHours < 14;

    #endregion
    #region ================= BREAKDOWN =================

    public int Weeks => Days / 7;
    public int RemainingDays => Days % 7;

    public int MonthsApprox => Days / 30;
    public int YearsApprox => Days / 365;

    public int QuarterApprox => MonthsApprox / 3;

    #endregion

    #region ================= BREAKDOWN =================

    // basic
    public int TotalSecondsInt => (int)TotalSeconds;
    public int TotalMinutesInt => (int)TotalMinutes;
    public int TotalHoursInt => (int)TotalHours;
    public int TotalDaysInt => (int)TotalDays;


    // improved precision (new)
    public int RemainingHours => (int)(TotalHours % 24);
    public int RemainingMinutes => (int)(TotalMinutes % 60);
    public int RemainingSeconds => (int)(TotalSeconds % 60);

    // business breakdown
    public int WorkDaysApprox => Days - (Days / 7 * 2); // rough weekend removal
    public int BusinessWeeks => Days / 5;


    #endregion

    #region ================= MATH =================

    public TimeInfo Add(TimeSpan v) => new(_ts + v);
    public TimeInfo Subtract(TimeSpan v) => new(_ts - v);

    public TimeInfo Multiply(double factor) => new(TimeSpan.FromTicks((long)(_ts.Ticks * factor)));
    public TimeInfo Divide(double divisor) => new(TimeSpan.FromTicks((long)(_ts.Ticks / divisor)));


    // normalization helpers
    public TimeInfo Clamp(TimeSpan min, TimeSpan max)
    {
        var clamped = _ts < min ? min : (_ts > max ? max : _ts);
        return new TimeInfo(clamped);
    }

    #endregion
    #region ================= ROUND =================

    // basic rounding
    public TimeSpan RoundMinutes() => TimeSpan.FromMinutes(Math.Round(TotalMinutes));
    public TimeSpan RoundHours() => TimeSpan.FromHours(Math.Round(TotalHours));
    public TimeSpan RoundDays() => TimeSpan.FromDays(Math.Round(TotalDays));

    // advanced rounding
    public TimeSpan FloorMinutes() => TimeSpan.FromMinutes(Math.Floor(TotalMinutes));
    public TimeSpan CeilingMinutes() => TimeSpan.FromMinutes(Math.Ceiling(TotalMinutes));

    public TimeSpan RoundToNearest(TimeSpan unit)
    {
        long ticks = (long)Math.Round((double)_ts.Ticks / unit.Ticks) * unit.Ticks;
        return new TimeSpan(ticks);
    }

    // common shortcuts
    public TimeSpan RoundTo5Minutes() => RoundToNearest(TimeSpan.FromMinutes(5));
    public TimeSpan RoundTo15Minutes() => RoundToNearest(TimeSpan.FromMinutes(15));
    public TimeSpan RoundToHour() => RoundToNearest(TimeSpan.FromHours(1));

    #endregion
    #region ================= ANALYTICS =================

    public double PercentOf(TimeSpan total)
        => total.Ticks == 0 ? 0 : (TotalMilliseconds / total.TotalMilliseconds) * 100;

    public bool IsOutlier(TimeSpan avg)
        => Math.Abs(TotalSeconds - avg.TotalSeconds) > avg.TotalSeconds * 2;

    #endregion

    #region ================= CONVERT =================


    public DateTime AddTo(DateTime d) => d.Add(_ts);
    public DateTime SubtractFrom(DateTime d) => d.Subtract(_ts);

    public DateTimeOffset AddTo(DateTimeOffset d) => d.Add(_ts);
    public DateTimeOffset SubtractFrom(DateTimeOffset d) => d.Subtract(_ts);

    public TimeSpan AddTo(TimeSpan t) => t + _ts;
    public TimeSpan SubtractFrom(TimeSpan t) => t - _ts;


    public DateTime AddToUtc(DateTime utc)
        => DateTime.SpecifyKind(utc, DateTimeKind.Utc).Add(_ts);

    public DateTime SubtractFromUtc(DateTime utc)
        => DateTime.SpecifyKind(utc, DateTimeKind.Utc).Subtract(_ts);

    public DateTimeOffset AddToUtc(DateTimeOffset utc)
        => utc.ToUniversalTime().Add(_ts);

    public DateTimeOffset SubtractFromUtc(DateTimeOffset utc)
        => utc.ToUniversalTime().Subtract(_ts);


    public DateTime NowPlus => DateTime.Now.Add(_ts);
    public DateTime NowMinus => DateTime.Now.Subtract(_ts);

    public DateTime UtcNowPlus => DateTime.UtcNow.Add(_ts);
    public DateTime UtcNowMinus => DateTime.UtcNow.Subtract(_ts);

    public DateTimeOffset OffsetNowPlus => DateTimeOffset.Now.Add(_ts);
    public DateTimeOffset OffsetNowMinus => DateTimeOffset.Now.Subtract(_ts);

    public DateTime SafeAdd(DateTime d)
    {
        try { return d.Add(_ts); }
        catch (ArgumentOutOfRangeException)
        {
            return _ts >= TimeSpan.Zero ? DateTime.MaxValue : DateTime.MinValue;
        }
    }

    public DateTimeOffset SafeAdd(DateTimeOffset d)
    {
        try { return d.Add(_ts); }
        catch (ArgumentOutOfRangeException)
        {
            return _ts >= TimeSpan.Zero ? DateTimeOffset.MaxValue : DateTimeOffset.MinValue;
        }
    }


    public DateTime AddToClamped(DateTime d, DateTime min, DateTime max)
    {
        var result = d.Add(_ts);
        if (result < min) return min;
        if (result > max) return max;
        return result;
    }

    public DateTimeOffset AddToClamped(DateTimeOffset d, DateTimeOffset min, DateTimeOffset max)
    {
        var result = d.Add(_ts);
        if (result < min) return min;
        if (result > max) return max;
        return result;
    }


    public DateTime AddToRoundedMinutes(DateTime d)
    {
        var ts = TimeSpan.FromMinutes(Math.Round(_ts.TotalMinutes));
        return d.Add(ts);
    }

    public DateTime AddToRoundedHours(DateTime d)
    {
        var ts = TimeSpan.FromHours(Math.Round(_ts.TotalHours));
        return d.Add(ts);
    }

    public DateTimeOffset AddToRoundedDays(DateTimeOffset d)
    {
        var ts = TimeSpan.FromDays(Math.Round(_ts.TotalDays));
        return d.Add(ts);
    }



    public DateTime AddToUnixSeconds(long unixSeconds)
    {
        var baseTime = DateTimeOffset.FromUnixTimeSeconds(unixSeconds);
        return baseTime.Add(_ts).UtcDateTime;
    }

    public long NowPlusAsUnixSeconds()
        => DateTimeOffset.UtcNow.Add(_ts).ToUnixTimeSeconds();

    public long AddToUnixMilliseconds(long unixMs)
    {
        var baseTime = DateTimeOffset.FromUnixTimeMilliseconds(unixMs);
        return baseTime.Add(_ts).ToUnixTimeMilliseconds();
    }


    public DateTime ConvertToTimeZone(DateTime utc, string timeZoneId)
    {
        var tz = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        var utcDate = DateTime.SpecifyKind(utc, DateTimeKind.Utc);
        return TimeZoneInfo.ConvertTimeFromUtc(utcDate, tz);
    }

    public DateTimeOffset ConvertToTimeZone(DateTimeOffset dto, string timeZoneId)
    {
        var tz = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        return TimeZoneInfo.ConvertTime(dto, tz);
    }



    public DateTime Transform(DateTime d, Func<TimeSpan, TimeSpan>? modifier = null)
    {
        var ts = modifier?.Invoke(_ts) ?? _ts;
        return d.Add(ts);
    }

    public DateTimeOffset Transform(DateTimeOffset d, Func<TimeSpan, TimeSpan>? modifier = null)
    {
        var ts = modifier?.Invoke(_ts) ?? _ts;
        return d.Add(ts);
    }


    public DateTime Apply(DateTime d, bool reverse = false)
        => reverse ? d.Subtract(_ts) : d.Add(_ts);

    public DateTimeOffset Apply(DateTimeOffset d, bool reverse = false)
        => reverse ? d.Subtract(_ts) : d.Add(_ts);

    #endregion

    #region ================= TIME CONVERSION ENGINE =================

    public DateTime ToUtc(DateTime dt)
    {
        return dt.Kind switch
        {
            DateTimeKind.Utc => dt,
            DateTimeKind.Local => dt.ToUniversalTime(),
            DateTimeKind.Unspecified => DateTime.SpecifyKind(dt, DateTimeKind.Utc),
            _ => dt
        };
    }

    public DateTime ToLocal(DateTime dt)
    {
        return dt.Kind switch
        {
            DateTimeKind.Local => dt,
            DateTimeKind.Utc => dt.ToLocalTime(),
            DateTimeKind.Unspecified => DateTime.SpecifyKind(dt, DateTimeKind.Local),
            _ => dt
        };
    }

    public DateTime ToUnspecified(DateTime dt)
    {
        return DateTime.SpecifyKind(dt, DateTimeKind.Unspecified);
    }



    public DateTimeOffset ToOffset(DateTime dt)
    {
        return dt.Kind switch
        {
            DateTimeKind.Utc => new DateTimeOffset(dt, TimeSpan.Zero),
            DateTimeKind.Local => new DateTimeOffset(dt),
            DateTimeKind.Unspecified => new DateTimeOffset(DateTime.SpecifyKind(dt, DateTimeKind.Local)),
            _ => new DateTimeOffset(dt)
        };
    }

    public DateTime FromOffset(DateTimeOffset dto)
    {
        return dto.UtcDateTime;
    }

    public DateTime UtcToLocal(DateTime utc)
    {
        return DateTime.SpecifyKind(utc, DateTimeKind.Utc).ToLocalTime();
    }

    public DateTime LocalToUtc(DateTime local)
    {
        return DateTime.SpecifyKind(local, DateTimeKind.Local).ToUniversalTime();
    }


    public DateTime FromUnixSeconds(long seconds)
    {
        return DateTimeOffset.FromUnixTimeSeconds(seconds).UtcDateTime;
    }

    public DateTime FromUnixMilliseconds(long ms)
    {
        return DateTimeOffset.FromUnixTimeMilliseconds(ms).UtcDateTime;
    }
    public long ToUnixSeconds(DateTime dt)
    {
        var utc = DateTime.SpecifyKind(ToUtc(dt), DateTimeKind.Utc);
        return new DateTimeOffset(utc).ToUnixTimeSeconds();
    }

    public long ToUnixMilliseconds(DateTime dt)
    {
        var utc = DateTime.SpecifyKind(ToUtc(dt), DateTimeKind.Utc);
        return new DateTimeOffset(utc).ToUnixTimeMilliseconds();
    }



    public DateTime NowLocal => DateTime.Now;
    public DateTime NowUtc => DateTime.UtcNow;
    public DateTimeOffset NowOffset => DateTimeOffset.Now;


    public DateTime Convert(DateTime dt, TimeZoneKind from, TimeZoneKind to)
    {
        var utc = from switch
        {
            TimeZoneKind.Utc => dt,
            TimeZoneKind.Local => LocalToUtc(dt),
            TimeZoneKind.Unspecified => ToUtc(dt),
            _ => dt
        };

        return to switch
        {
            TimeZoneKind.Utc => utc,
            TimeZoneKind.Local => UtcToLocal(utc),
            TimeZoneKind.Unspecified => ToUnspecified(utc),
            _ => utc
        };
    }
    public enum TimeZoneKind
    {
        Utc,
        Local,
        Unspecified
    }
    #endregion


    #region ================= TIMEZONE CONVERSION =================

    public DateTime ConvertTime(DateTime dt, string fromTimeZoneId, string toTimeZoneId)
    {
        var fromZone = TimeZoneInfo.FindSystemTimeZoneById(fromTimeZoneId);
        var toZone = TimeZoneInfo.FindSystemTimeZoneById(toTimeZoneId);

        var utc = TimeZoneInfo.ConvertTimeToUtc(dt, fromZone);

        return TimeZoneInfo.ConvertTimeFromUtc(utc, toZone);
    }



    public DateTime ConvertTime(DateTime dt, TimeZoneInfo fromZone, TimeZoneInfo toZone)
    {
        var utc = TimeZoneInfo.ConvertTimeToUtc(dt, fromZone);
        return TimeZoneInfo.ConvertTimeFromUtc(utc, toZone);
    }


    public DateTime UtcToTimeZone(DateTime utc, string timeZoneId)
    {
        var zone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        return TimeZoneInfo.ConvertTimeFromUtc(utc, zone);
    }

    public DateTime TimeZoneToUtc(DateTime dt, string timeZoneId)
    {
        var zone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        return TimeZoneInfo.ConvertTimeToUtc(dt, zone);
    }


    public DateTime NowInUtc => DateTime.UtcNow;

    public DateTime NowInLocal => DateTime.Now;

    public DateTime NowInTimeZone(string timeZoneId)
    {
        var zone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        return TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.Utc, zone);
    }


    public bool TryConvertTime(DateTime dt, string from, string to, out DateTime result)
    {
        try
        {
            result = ConvertTime(dt, from, to);
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }

    public DateTime NowIn(string timeZoneId)
    {
        return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow,
            TimeZoneInfo.FindSystemTimeZoneById(timeZoneId));
    }

    #endregion

    #region ================= SERIALIZE =================

    public string ToJson() => JsonSerializer.Serialize(new
    {
        Days,
        Hours,
        Minutes,
        Seconds,
        TotalDays,
        TotalHours,
        TotalMinutes,
        TotalSeconds,
        Range,
        Bucket
    });

    public string ToIso() => XmlConvert.ToString(_ts);

    public Dictionary<string, object> ToDictionary() => new()
    {
        ["Days"] = Days,
        ["Hours"] = Hours,
        ["Minutes"] = Minutes,
        ["Seconds"] = Seconds,
        ["Total"] = TotalSeconds,
        ["HumanFa"] = HumanizeFa,
        ["HumanEn"] = HumanizeEn,
        ["Range"] = Range,
        ["Bucket"] = Bucket
    };

    #endregion

    #region ================= OPERATORS =================

    // basic math
    public static TimeInfo operator +(TimeInfo a, TimeInfo b)
        => new(a._ts + b._ts);

    public static TimeInfo operator -(TimeInfo a, TimeInfo b)
        => new(a._ts - b._ts);

    public static TimeInfo operator *(TimeInfo a, double factor)
        => new(TimeSpan.FromTicks((long)(a._ts.Ticks * factor)));

    public static TimeInfo operator /(TimeInfo a, double divisor)
        => new(TimeSpan.FromTicks((long)(a._ts.Ticks / divisor)));

    // comparisons
    public static bool operator >(TimeInfo a, TimeInfo b)
        => a._ts > b._ts;

    public static bool operator <(TimeInfo a, TimeInfo b)
        => a._ts < b._ts;

    public static bool operator >=(TimeInfo a, TimeInfo b)
        => a._ts >= b._ts;

    public static bool operator <=(TimeInfo a, TimeInfo b)
        => a._ts <= b._ts;

    public static bool operator ==(TimeInfo a, TimeInfo b)
        => a._ts == b._ts;

    public static bool operator !=(TimeInfo a, TimeInfo b)
        => a._ts != b._ts;

    #endregion

    public override bool Equals(object? obj)
    {
        return obj is TimeInfo other && _ts.Equals(other._ts);
    }

    public override int GetHashCode()
    {
        return _ts.GetHashCode();
    }

    public override string ToString()
    => $"{Days:00}.{Hours:00}:{Minutes:00}:{Seconds:00}";

    public string ToDetailedString()
        => $"{TotalDays:0.##}d ({TotalHours:0.##}h) ({TotalMinutes:0.##}m)";

    public  bool TryParse(string input, out TimeInfo result)
    {
        result = default;

        if (TimeSpan.TryParse(input, out var ts))
        {
            result = new TimeInfo(ts);
            return true;
        }

        return false;
    }

    public  TimeInfo Parse(string input)
    {
        return new TimeInfo(TimeSpan.Parse(input));
    }



    public  DateTime Now(string timeZoneId)
    {
        var tz = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz);
    }
    // ================= GLOBAL =================
    public  DateTime UtcNow() => DateTime.UtcNow;
    public  DateTime LocalNow() => DateTime.Now;

    // ================= MIDDLE EAST =================
    public  DateTime IranNow() => Now("Iran Standard Time");
    public  DateTime AzerbaijanNow() => Now("Azerbaijan Standard Time");
    public  DateTime TurkeyNow() => Now("Turkey Standard Time");
    public  DateTime UaeNow() => Now("Arabian Standard Time");
    public  DateTime SaudiArabiaNow() => Now("Arabian Standard Time");
    public  DateTime QatarNow() => Now("Arabian Standard Time");
    public  DateTime KuwaitNow() => Now("Arabian Standard Time");
    public  DateTime IsraelNow() => Now("Israel Standard Time");

    // ================= EUROPE =================
    public  DateTime UkNow() => Now("GMT Standard Time");
    public  DateTime IrelandNow() => Now("GMT Standard Time");
    public  DateTime FranceNow() => Now("Romance Standard Time");
    public  DateTime GermanyNow() => Now("W. Europe Standard Time");
    public  DateTime ItalyNow() => Now("W. Europe Standard Time");
    public  DateTime SpainNow() => Now("Romance Standard Time");
    public  DateTime NetherlandsNow() => Now("W. Europe Standard Time");
    public  DateTime BelgiumNow() => Now("Romance Standard Time");
    public  DateTime SwitzerlandNow() => Now("W. Europe Standard Time");
    public  DateTime AustriaNow() => Now("W. Europe Standard Time");
    public  DateTime SwedenNow() => Now("W. Europe Standard Time");
    public  DateTime NorwayNow() => Now("W. Europe Standard Time");
    public  DateTime DenmarkNow() => Now("W. Europe Standard Time");
    public  DateTime FinlandNow() => Now("FLE Standard Time");
    public  DateTime PolandNow() => Now("Central Europe Standard Time");
    public  DateTime UkraineNow() => Now("FLE Standard Time");
    public  DateTime RussiaMoscowNow() => Now("Russian Standard Time");

    // ================= ASIA =================
    public  DateTime JapanNow() => Now("Tokyo Standard Time");
    public  DateTime ChinaNow() => Now("China Standard Time");
    public  DateTime IndiaNow() => Now("India Standard Time");
    public  DateTime PakistanNow() => Now("Pakistan Standard Time");
    public  DateTime BangladeshNow() => Now("Bangladesh Standard Time");
    public  DateTime IndonesiaNow() => Now("SE Asia Standard Time");
    public  DateTime SingaporeNow() => Now("Singapore Standard Time");
    public  DateTime MalaysiaNow() => Now("Singapore Standard Time");
    public  DateTime SouthKoreaNow() => Now("Korea Standard Time");
    public  DateTime NorthKoreaNow() => Now("Korea Standard Time");
    public  DateTime ThailandNow() => Now("SE Asia Standard Time");
    public  DateTime VietnamNow() => Now("SE Asia Standard Time");
    public  DateTime PhilippinesNow() => Now("Singapore Standard Time");
    public  DateTime SriLankaNow() => Now("Sri Lanka Standard Time");
    public  DateTime NepalNow() => Now("Nepal Standard Time");

    // ================= AMERICA =================
    public  DateTime UsaEastNow() => Now("Eastern Standard Time");
    public  DateTime UsaCentralNow() => Now("Central Standard Time");
    public  DateTime UsaWestNow() => Now("Pacific Standard Time");
    public  DateTime CanadaTorontoNow() => Now("Eastern Standard Time");
    public  DateTime CanadaVancouverNow() => Now("Pacific Standard Time");
    public  DateTime MexicoNow() => Now("Central Standard Time");
    public  DateTime BrazilNow() => Now("E. South America Standard Time");
    public  DateTime ArgentinaNow() => Now("Argentina Standard Time");
    public  DateTime ChileNow() => Now("Pacific SA Standard Time");
    public  DateTime ColombiaNow() => Now("SA Pacific Standard Time");
    public  DateTime PeruNow() => Now("SA Pacific Standard Time");

    // ================= AFRICA =================
    public  DateTime EgyptNow() => Now("Egypt Standard Time");
    public  DateTime SouthAfricaNow() => Now("South Africa Standard Time");
    public  DateTime MoroccoNow() => Now("Morocco Standard Time");
    public  DateTime AlgeriaNow() => Now("W. Central Africa Standard Time");
    public  DateTime NigeriaNow() => Now("W. Central Africa Standard Time");
    public  DateTime KenyaNow() => Now("E. Africa Standard Time");

    // ================= OCEANIA =================
    public  DateTime AustraliaSydneyNow() => Now("AUS Eastern Standard Time");
    public  DateTime AustraliaPerthNow() => Now("W. Australia Standard Time");
    public  DateTime AustraliaBrisbaneNow() => Now("E. Australia Standard Time");
    public  DateTime NewZealandNow() => Now("New Zealand Standard Time");
    public  DateTime FijiNow() => Now("Fiji Standard Time");



}


