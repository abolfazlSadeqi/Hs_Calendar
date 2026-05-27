
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace HsCalendar;

public sealed class HsDate
{
    private static readonly PersianCalendar Persian = new();

    #region Months

    private static readonly string[] PersianMonths =
    {
        "",
        "فروردین",
        "اردیبهشت",
        "خرداد",
        "تیر",
        "مرداد",
        "شهریور",
        "مهر",
        "آبان",
        "آذر",
        "دی",
        "بهمن",
        "اسفند"
    };

    private static readonly string[] EnglishMonths =
    {
        "",
        "January",
        "February",
        "March",
        "April",
        "May",
        "June",
        "July",
        "August",
        "September",
        "October",
        "November",
        "December"
    };

    #endregion

    #region Weeks

    private static readonly string[] PersianWeekDays =
    {
        "یکشنبه",
        "دوشنبه",
        "سه شنبه",
        "چهارشنبه",
        "پنجشنبه",
        "جمعه",
        "شنبه"
    };

    private static readonly string[] EnglishWeekDays =
    {
        "Sunday",
        "Monday",
        "Tuesday",
        "Wednesday",
        "Thursday",
        "Friday",
        "Saturday"
    };

    #endregion

    #region Digits

    private static readonly char[] PersianDigits =
    {
        '۰','۱','۲','۳','۴',
        '۵','۶','۷','۸','۹'
    };

    #endregion

    #region Gregorian To Persian

    public int PersianYear(DateTime date)
        => Persian.GetYear(date);

    public int PersianMonth(DateTime date)
        => Persian.GetMonth(date);

    public int PersianDay(DateTime date)
        => Persian.GetDayOfMonth(date);

    #endregion

    #region Persian To Gregorian

    public DateTime ToGregorian(
        int year,
        int month,
        int day)
    {
        return Persian.ToDateTime(
            year,
            month,
            day,
            0,
            0,
            0,
            0);
    }

    public DateTime ToGregorian(
        int year,
        int month,
        int day,
        int hour,
        int minute,
        int second)
    {
        return Persian.ToDateTime(
            year,
            month,
            day,
            hour,
            minute,
            second,
            0);
    }

    #endregion

    #region Month Names

    public string PersianMonthName(DateTime date)
        => PersianMonths[PersianMonth(date)];

    public string EnglishMonthName(DateTime date)
        => EnglishMonths[date.Month];

    #endregion

    #region Week Names

    public string PersianWeekName(DateTime date)
        => PersianWeekDays[(int)date.DayOfWeek];

    public string EnglishWeekName(DateTime date)
        => EnglishWeekDays[(int)date.DayOfWeek];

    #endregion

    #region Quarter

    public int PersianQuarter(DateTime date)
        => ((PersianMonth(date) - 1) / 3) + 1;

    public int GregorianQuarter(DateTime date)
        => ((date.Month - 1) / 3) + 1;

    #endregion

    #region Half Year

    public int PersianHalfYear(DateTime date)
        => PersianMonth(date) <= 6 ? 1 : 2;

    public int GregorianHalfYear(DateTime date)
        => date.Month <= 6 ? 1 : 2;

    #endregion

    #region Seasons

    public string PersianSeason(DateTime date)
    {
        return PersianQuarter(date) switch
        {
            1 => "بهار",
            2 => "تابستان",
            3 => "پاییز",
            4 => "زمستان",
            _ => ""
        };
    }

    public string EnglishSeason(DateTime date)
    {
        return GregorianQuarter(date) switch
        {
            1 => "Spring",
            2 => "Summer",
            3 => "Autumn",
            4 => "Winter",
            _ => ""
        };
    }

    #endregion

    #region Week Of Year

    public int PersianWeekOfYear(DateTime date)
    {
        return Persian.GetWeekOfYear(
            date,
            CalendarWeekRule.FirstDay,
            DayOfWeek.Saturday);
    }

    public int GregorianWeekOfYear(DateTime date)
    {
        return CultureInfo.InvariantCulture.Calendar
            .GetWeekOfYear(
                date,
                CalendarWeekRule.FirstDay,
                DayOfWeek.Monday);
    }

    #endregion

    #region Date Keys

    public int PersianDateKey(DateTime date)
    {
        return int.Parse(
            $"{PersianYear(date)}" +
            $"{PersianMonth(date):00}" +
            $"{PersianDay(date):00}");
    }

    public int GregorianDateKey(DateTime date)
    {
        return int.Parse(
            $"{date.Year}" +
            $"{date.Month:00}" +
            $"{date.Day:00}");
    }

    public int PersianYearMonthKey(DateTime date)
    {
        return int.Parse(
            $"{PersianYear(date)}" +
            $"{PersianMonth(date):00}");
    }

    public int PersianYearQuarterKey(DateTime date)
    {
        return int.Parse(
            $"{PersianYear(date)}" +
            $"{PersianQuarter(date):00}");
    }

    public int PersianYearWeekKey(DateTime date)
    {
        return int.Parse(
            $"{PersianYear(date)}" +
            $"{PersianWeekOfYear(date):00}");
    }

    #endregion

    #region Formatter

    public string Format(
        DateTime date,
        string format = "yyyy/MM/dd",
        bool persianDigits = true)
    {
        var result = format;

        result = result.Replace(
            "yyyy",
            PersianYear(date).ToString("0000"));

        result = result.Replace(
            "yy",
            (PersianYear(date) % 100).ToString("00"));

        result = result.Replace(
            "MMMM",
            PersianMonthName(date));

        result = result.Replace(
            "MM",
            PersianMonth(date).ToString("00"));

        result = result.Replace(
            "dd",
            PersianDay(date).ToString("00"));

        result = result.Replace(
            "HH",
            date.Hour.ToString("00"));

        result = result.Replace(
            "mm",
            date.Minute.ToString("00"));

        result = result.Replace(
            "ss",
            date.Second.ToString("00"));

        result = result.Replace(
            "fff",
            date.Millisecond.ToString("000"));

        result = result.Replace(
            "ddd",
            PersianWeekName(date));

        result = result.Replace(
            "WW",
            PersianWeekOfYear(date).ToString());

        result = result.Replace(
            "Q",
            PersianQuarter(date).ToString());

        result = result.Replace(
            "HY",
            PersianHalfYear(date).ToString());

        result = result.Replace(
            "Season",
            PersianSeason(date));

        if (persianDigits)
            result = ToPersianDigits(result);

        return result;
    }

    #endregion

    #region Common Formats

    public string ShortDate(DateTime date)
        => Format(date, "yyyy/MM/dd");

    public string LongDate(DateTime date)
        => Format(date, "ddd dd MMMM yyyy");

    public string FullDate(DateTime date)
        => Format(date,
            "ddd dd MMMM yyyy HH:mm:ss");

    public string ShortTime(DateTime date)
        => Format(date, "HH:mm");

    public string FullTime(DateTime date)
        => Format(date, "HH:mm:ss");

    public string IsoDate(DateTime date)
        => date.ToString("yyyy-MM-dd");

    public string IsoDateTime(DateTime date)
        => date.ToString("yyyy-MM-ddTHH:mm:ss");

    #endregion

    #region Humanize

    public string TimeAgo(DateTime date)
    {
        var span = DateTime.Now - date;

        if (span.TotalSeconds < 60)
            return $"{(int)span.TotalSeconds} ثانیه پیش";

        if (span.TotalMinutes < 60)
            return $"{(int)span.TotalMinutes} دقیقه پیش";

        if (span.TotalHours < 24)
            return $"{(int)span.TotalHours} ساعت پیش";

        if (span.TotalDays < 30)
            return $"{(int)span.TotalDays} روز پیش";

        if (span.TotalDays < 365)
            return $"{(int)(span.TotalDays / 30)} ماه پیش";

        return $"{(int)(span.TotalDays / 365)} سال پیش";
    }

    public string TimeLeft(DateTime date)
    {
        var span = date - DateTime.Now;

        if (span.TotalSeconds < 60)
            return $"{(int)span.TotalSeconds} ثانیه مانده";

        if (span.TotalMinutes < 60)
            return $"{(int)span.TotalMinutes} دقیقه مانده";

        if (span.TotalHours < 24)
            return $"{(int)span.TotalHours} ساعت مانده";

        if (span.TotalDays < 30)
            return $"{(int)span.TotalDays} روز مانده";

        if (span.TotalDays < 365)
            return $"{(int)(span.TotalDays / 30)} ماه مانده";

        return $"{(int)(span.TotalDays / 365)} سال مانده";
    }

    #endregion

    #region Smart Date

    public string SmartDate(DateTime date)
    {
        if (date.Date == DateTime.Today)
            return "امروز";

        if (date.Date == DateTime.Today.AddDays(-1))
            return "دیروز";

        if (date.Date == DateTime.Today.AddDays(1))
            return "فردا";

        return ShortDate(date);
    }

    #endregion

    #region Difference

    public TimeSpan Difference(
        DateTime start,
        DateTime end)
        => end - start;

    public int DiffDays(
        DateTime start,
        DateTime end)
        => Math.Abs((end - start).Days);

    public int DiffMonths(
        DateTime start,
        DateTime end)
    {
        return Math.Abs(
            ((end.Year - start.Year) * 12)
            + end.Month - start.Month);
    }

    public int DiffYears(
        DateTime start,
        DateTime end)
        => Math.Abs(end.Year - start.Year);

    public int DiffHours(
        DateTime start,
        DateTime end)
        => (int)Math.Abs(
            (end - start).TotalHours);

    public int DiffMinutes(
        DateTime start,
        DateTime end)
        => (int)Math.Abs(
            (end - start).TotalMinutes);

    public int DiffSeconds(
        DateTime start,
        DateTime end)
        => (int)Math.Abs(
            (end - start).TotalSeconds);

    #endregion

    #region Business Days

    public bool IsWeekend(DateTime date)
        => date.DayOfWeek == DayOfWeek.Friday;

    public bool IsWorkDay(DateTime date)
        => !IsWeekend(date);

    public int BusinessDays(
        DateTime start,
        DateTime end)
    {
        int count = 0;

        for (var dt = start.Date;
             dt <= end.Date;
             dt = dt.AddDays(1))
        {
            if (!IsWeekend(dt))
                count++;
        }

        return count;
    }

    #endregion

    #region Start End

    public DateTime StartOfDay(DateTime date)
        => date.Date;

    public DateTime EndOfDay(DateTime date)
        => date.Date
            .AddDays(1)
            .AddTicks(-1);

    public DateTime StartOfMonth(DateTime date)
        => new(date.Year, date.Month, 1);

    public DateTime EndOfMonth(DateTime date)
        => StartOfMonth(date)
            .AddMonths(1)
            .AddTicks(-1);

    public DateTime StartOfYear(DateTime date)
        => new(date.Year, 1, 1);

    public DateTime EndOfYear(DateTime date)
        => new(date.Year, 12, 31, 23, 59, 59);

    #endregion

    #region Persian Start End

    public DateTime StartOfPersianMonth(DateTime date)
    {
        return ToGregorian(
            PersianYear(date),
            PersianMonth(date),
            1);
    }

    public DateTime EndOfPersianMonth(DateTime date)
    {
        var year = PersianYear(date);
        var month = PersianMonth(date);

        var days =
            Persian.GetDaysInMonth(year, month);

        return ToGregorian(year, month, days);
    }

    #endregion

    #region Unix

    public long ToUnix(DateTime date)
        => new DateTimeOffset(date)
            .ToUnixTimeSeconds();

    public DateTime FromUnix(long unix)
        => DateTimeOffset
            .FromUnixTimeSeconds(unix)
            .DateTime;

    #endregion

    #region UTC

    public DateTime ToUtc(DateTime date)
        => date.ToUniversalTime();

    public DateTime ToLocal(DateTime date)
        => date.ToLocalTime();

    #endregion

    #region Validation

    public bool IsValidPersianDate(
        int year,
        int month,
        int day)
    {
        try
        {
            Persian.ToDateTime(
                year,
                month,
                day,
                0,
                0,
                0,
                0);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool IsLeapYear(int year)
        => Persian.IsLeapYear(year);

    #endregion

    #region Parse

    public DateTime ParsePersian(string value)
    {
        value = ToEnglishDigits(value);

        value = value.Replace("-", "/");

        var parts = value.Split('/');

        if (parts.Length != 3)
            throw new Exception(
                "Invalid Persian Date");

        return ToGregorian(
            int.Parse(parts[0]),
            int.Parse(parts[1]),
            int.Parse(parts[2]));
    }

    public bool TryParsePersian(
        string value,
        out DateTime result)
    {
        try
        {
            result = ParsePersian(value);
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }

    #endregion

    #region Digits

    public string ToPersianDigits(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return string.Empty;

        var builder = new StringBuilder();

        foreach (var ch in value)
        {
            if (char.IsDigit(ch))
                builder.Append(
                    PersianDigits[ch - '0']);
            else
                builder.Append(ch);
        }

        return builder.ToString();
    }

    public string ToEnglishDigits(string value)
    {
        return value
            .Replace('۰', '0')
            .Replace('۱', '1')
            .Replace('۲', '2')
            .Replace('۳', '3')
            .Replace('۴', '4')
            .Replace('۵', '5')
            .Replace('۶', '6')
            .Replace('۷', '7')
            .Replace('۸', '8')
            .Replace('۹', '9');
    }

    #endregion

    #region Age

    public int Age(DateTime birthDate)
    {
        var today = DateTime.Today;

        var age = today.Year - birthDate.Year;

        if (birthDate > today.AddYears(-age))
            age--;

        return age;
    }

    #endregion

    #region Nullable

    public string FormatNullable(
        DateTime? date,
        string format = "yyyy/MM/dd")
    {
        if (date == null)
            return string.Empty;

        return Format(date.Value, format);
    }

    #endregion

    #region DateRange

    public List<DateTime> DateRange(
        DateTime start,
        DateTime end)
    {
        var result = new List<DateTime>();

        for (var dt = start.Date;
             dt <= end.Date;
             dt = dt.AddDays(1))
        {
            result.Add(dt);
        }

        return result;
    }

    #endregion

    #region Min Max

    public DateTime Min(
        params DateTime[] dates)
        => dates.Min();

    public DateTime Max(
        params DateTime[] dates)
        => dates.Max();

    #endregion

    #region Add

    public DateTime AddYears(
        DateTime date,
        int years)
        => date.AddYears(years);

    public DateTime AddMonths(
        DateTime date,
        int months)
        => date.AddMonths(months);

    public DateTime AddDays(
        DateTime date,
        int days)
        => date.AddDays(days);

    public DateTime AddHours(
        DateTime date,
        int hours)
        => date.AddHours(hours);

    public DateTime AddMinutes(
        DateTime date,
        int minutes)
        => date.AddMinutes(minutes);

    public DateTime AddSeconds(
        DateTime date,
        int seconds)
        => date.AddSeconds(seconds);

    #endregion

    #region DateOnly TimeOnly

    public DateOnly ToDateOnly(DateTime date)
        => DateOnly.FromDateTime(date);

    public TimeOnly ToTimeOnly(DateTime date)
        => TimeOnly.FromDateTime(date);

    #endregion

    #region Offset

    public DateTimeOffset ToOffset(DateTime date)
        => new(date);

    public (int Years, int Months, int Days) DiffYMD(DateTime date1, DateTime date2)
    {
        // همیشه date1 <= date2
        if (date1 > date2)
        {
            var temp = date1;
            date1 = date2;
            date2 = temp;
        }

        int years = date2.Year - date1.Year;
        int months = date2.Month - date1.Month;
        int days = date2.Day - date1.Day;

        if (days < 0)
        {
            months--;
            var prevMonth = date2.AddMonths(-1);
            days += DateTime.DaysInMonth(prevMonth.Year, prevMonth.Month);
        }

        if (months < 0)
        {
            years--;
            months += 12;
        }

        return (years, months, days);
    }
    #endregion
}

public static class HsDateExtensions
{
    private static readonly HsDate Hs = new();

    public static string ToPersianDate(
        this DateTime date,
        string format = "yyyy/MM/dd")
    {
        return Hs.Format(date, format);
    }

    public static string ToPersianDateTime(
        this DateTime date)
    {
        return Hs.FullDate(date);
    }

    public static string ToShortPersianDate(
        this DateTime date)
    {
        return Hs.ShortDate(date);
    }

    public static string ToLongPersianDate(
        this DateTime date)
    {
        return Hs.LongDate(date);
    }

    public static string ToTimeAgo(
        this DateTime date)
    {
        return Hs.TimeAgo(date);
    }

    public static string ToTimeLeft(
        this DateTime date)
    {
        return Hs.TimeLeft(date);
    }

    public static bool IsWeekend(
        this DateTime date)
    {
        return Hs.IsWeekend(date);
    }

    public static bool IsWorkDay(
        this DateTime date)
    {
        return Hs.IsWorkDay(date);
    }

    public static DateTime StartOfMonth(
        this DateTime date)
    {
        return Hs.StartOfMonth(date);
    }

    public static DateTime EndOfMonth(
        this DateTime date)
    {
        return Hs.EndOfMonth(date);
    }

    public static DateTime StartOfDay(
        this DateTime date)
    {
        return Hs.StartOfDay(date);
    }

    public static DateTime EndOfDay(
        this DateTime date)
    {
        return Hs.EndOfDay(date);
    }

    public static int ToPersianDateKey(
        this DateTime date)
    {
        return Hs.PersianDateKey(date);
    }

    public static int ToGregorianDateKey(
        this DateTime date)
    {
        return Hs.GregorianDateKey(date);
    }

    public static int Age(
        this DateTime birthDate)
    {
        return Hs.Age(birthDate);
    }

    public static long ToUnix(
        this DateTime date)
    {
        return Hs.ToUnix(date);
    }

    public static DateTime FromUnix(
        this long unix)
    {
        return Hs.FromUnix(unix);
    }

    public static string ToPersianDigits(
        this string value)
    {
        return Hs.ToPersianDigits(value);
    }

    public static string ToEnglishDigits(
        this string value)
    {
        return Hs.ToEnglishDigits(value);
    }

    public static int PersianYear(
        this DateTime date)
    {
        return Hs.PersianYear(date);
    }

    public static int PersianMonth(
        this DateTime date)
    {
        return Hs.PersianMonth(date);
    }

    public static int PersianDay(
        this DateTime date)
    {
        return Hs.PersianDay(date);
    }

    public static string PersianMonthName(
        this DateTime date)
    {
        return Hs.PersianMonthName(date);
    }

    public static string PersianWeekName(
        this DateTime date)
    {
        return Hs.PersianWeekName(date);
    }

    public static string PersianSeason(
        this DateTime date)
    {
        return Hs.PersianSeason(date);
    }

    public static int PersianQuarter(
        this DateTime date)
    {
        return Hs.PersianQuarter(date);
    }

    public static int PersianWeekOfYear(
        this DateTime date)
    {
        return Hs.PersianWeekOfYear(date);
    }
}

