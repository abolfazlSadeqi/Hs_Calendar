using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using Hs_Calender;
using HsCalendar;

namespace Hs_Calendar;

public sealed class DateInfo : System.Globalization.PersianCalendar
{
    private readonly DateTime _date;
    private readonly HsDate _hs;



    public DateInfo(DateTime? date = null)
    {
        _date = date ?? DateTime.Now;
        _hs = new HsDate();
    }
    #region ================= RAW DATE =================


    public DateTime Raw => _date;

    public DateOnly RawDate => DateOnly.FromDateTime(_date);

    public string RawFa => _hs.Format(_date, "yyyy/MM/dd");

    public string RawEn => _date.ToString("yyyy-MM-dd");

    public string RawIso => _hs.IsoDate(_date);

    public long Unix => _hs.ToUnix(_date);

    // ================= BASIC FLAGS =================

    public bool IsToday => _date.Date == DateTime.Today;

    public bool IsYesterday =>
        _date.Date == DateTime.Today.AddDays(-1);

    public bool IsTomorrow =>
        _date.Date == DateTime.Today.AddDays(1);

    public bool IsLeapYearFa =>
    _hs.IsLeapYear(_hs.PersianYear(_date));

    public bool IsLeapYearEn =>
        DateTime.IsLeapYear(_date.Year);

    // ================= DAY INFO =================

    public int DayOfYear =>
        _date.DayOfYear;

    public int DayOfWeekNumber =>
        (int)_date.DayOfWeek;

    public string DayOfWeekEn =>
        _date.DayOfWeek.ToString();

    public string DayOfWeekFa =>
        _hs.PersianWeekName(_date);

    // ================= KEYS =================

    public int PersianDateKey =>
        _hs.PersianDateKey(_date);

    public int GregorianDateKey =>
        _hs.GregorianDateKey(_date);

    public int PersianYearMonthKey =>
        _hs.PersianYearMonthKey(_date);

    public int PersianYearQuarterKey =>
        _hs.PersianYearQuarterKey(_date);

    public int PersianYearWeekKey =>
        _hs.PersianYearWeekKey(_date);

    #endregion

    #region ================= PERSIAN (FA) =================

    // ================= NUMERIC =================

    public int YearFa =>
        _hs.PersianYear(_date);

    public int MonthFa =>
        _hs.PersianMonth(_date);

    public int DayFa =>
        _hs.PersianDay(_date);

    public int WeekFa =>
        _hs.PersianWeekOfYear(_date);

    public int QuarterFa =>
        _hs.PersianQuarter(_date);

    public int HalfYearFa =>
        _hs.PersianHalfYear(_date);

    // ================= NAMES =================

    public string MonthNameFa =>
        _hs.PersianMonthName(_date);

    public string WeekNameFa =>
        _hs.PersianWeekName(_date);

    public string SeasonFa =>
        _hs.PersianSeason(_date);

    // ================= TEXT =================

    public string YearFaText =>
        ToFaWords(YearFa);

    public string MonthFaText =>
        MonthNameFa;
    public string WeekDayFa => _hs.PersianWeekName(_date);

    public string DayFaText =>
        ToFaWords(DayFa);

    public string WeekFaText =>
        ToFaWords(WeekFa);

    public string QuarterFaText =>
        $"{_hs.PersianSeason(_date)}";


    // ================= FORMATS =================

    public string PersianDate =>
        _hs.Format(_date, "yyyy/MM/dd");

    public string PersianDateShort =>
        _hs.Format(_date, "yy/MM/dd");

    public string PersianDateLong =>
        _hs.Format(_date, "ddd dd MMMM yyyy");

    public string PersianMonthYear =>
        $"{MonthNameFa} {YearFa}";

    public string PersianFullText =>
        $"{WeekNameFa} {DayFa} {MonthNameFa} {YearFa}";

    // ================= SEASONS =================

    public bool IsSpringFa =>
        QuarterFa == 1;

    public bool IsSummerFa =>
        QuarterFa == 2;

    public bool IsAutumnFa =>
        QuarterFa == 3;

    public bool IsWinterFa =>
        QuarterFa == 4;

    // ================= MONTH FLAGS =================

    public bool IsFirstMonthFa =>
        MonthFa == 1;

    public bool IsLastMonthFa =>
        MonthFa == 12;

    public bool IsFirstDayFa =>
        DayFa == 1;

    public bool IsLastDayFa =>
        DayFa ==
        DateTime.DaysInMonth(
            _date.Year,
            _date.Month);

    // ================= SMART =================

    public string SmartFa =>
        _hs.SmartDate(_date);

    #endregion

    #region ================= GREGORIAN (EN) =================

    // ================= NUMERIC =================

    public int YearEn =>
        _date.Year;

    public int MonthEn =>
        _date.Month;

    public int DayEn =>
        _date.Day;

    public int WeekEn =>
        _hs.GregorianWeekOfYear(_date);

    public int QuarterEn =>
        _hs.GregorianQuarter(_date);

    public int HalfYearEn =>
        _hs.GregorianHalfYear(_date);

    // ================= NAMES =================

    public string MonthNameEn =>
        _hs.EnglishMonthName(_date);

    public string WeekNameEn =>
        _hs.EnglishWeekName(_date);

    public string SeasonEn =>
        _hs.EnglishSeason(_date);

    // ================= TEXT =================

    public string YearEnText =>
        ToEnWords(YearEn);

    public string MonthEnText =>
        MonthNameEn;
    public string WeekDayEn => _hs.EnglishWeekName(_date);

    public string DayEnText =>
        ToEnWords(DayEn);

    public string WeekEnText =>
        ToEnWords(WeekEn);

    public string QuarterEnText =>
        $"Quarter {_hs.EnglishSeason}";


    // ================= FORMATS =================

    public string GregorianDate =>
        _date.ToString("yyyy/MM/dd");

    public string GregorianDateShort =>
        _date.ToString("yy/MM/dd");

    public string GregorianDateLong =>
        _date.ToString("dddd dd MMMM yyyy");

    public string GregorianMonthYear =>
        $"{MonthNameEn} {YearEn}";

    public string GregorianFullText =>
        $"{WeekNameEn} {DayEn} {MonthNameEn} {YearEn}";

    // ================= SEASONS =================

    public bool IsSpringEn =>
        QuarterEn == 1;

    public bool IsSummerEn =>
        QuarterEn == 2;

    public bool IsAutumnEn =>
        QuarterEn == 3;

    public bool IsWinterEn =>
        QuarterEn == 4;

    // ================= MONTH FLAGS =================

    public bool IsFirstMonthEn =>
        MonthEn == 1;

    public bool IsLastMonthEn =>
        MonthEn == 12;

    public bool IsFirstDayEn =>
        DayEn == 1;

    public bool IsLastDayEn =>
        DayEn ==
        DateTime.DaysInMonth(
            YearEn,
            MonthEn);

    // ================= SMART =================

    public string SmartEn
    {
        get
        {
            if (_date.Date == DateTime.Today)
                return "Today";

            if (_date.Date == DateTime.Today.AddDays(-1))
                return "Yesterday";

            if (_date.Date == DateTime.Today.AddDays(1))
                return "Tomorrow";

            return _date.ToString("dddd dd MMMM yyyy");
        }
    }

    #endregion
    #region ================= START / END (FULL FA + EN + SEASON + QUARTER + HALFYEAR) =================

    // DAY
    public DateTime StartOfDayFa
        => _hs.StartOfDay(_date);
    public DateInfo AtStartOfDayFa()
    => new DateInfo(StartOfDayFa);

    public DateTime EndOfDayFa
        => _hs.EndOfDay(_date);

    public DateInfo AtEndOfDayFa()
      => new DateInfo(EndOfDayFa);
    // WEEK

    public DateTime StartOfWeekFa
    {
        get
        {
            int diff =
                ((int)_date.DayOfWeek + 1) % 7;

            return _date.Date.AddDays(-diff);
        }
    }
    public DateInfo AtStartOfWeekFa()
      => new DateInfo(StartOfWeekFa);

    public DateTime EndOfWeekFa
    {
        get
        {
            return StartOfWeekFa
                .AddDays(6)
                .Date
                .AddDays(1)
                .AddTicks(-1);
        }
    }
    public DateInfo AtEndOfWeekFa()
  => new DateInfo(EndOfWeekFa);
    // MONTH
    public DateTime StartOfMonthFa
        => _hs.StartOfPersianMonth(_date);

    public DateInfo AtStartOfMonthFa()
=> new DateInfo(StartOfMonthFa);

    public DateTime EndOfMonthFa
        => _hs.EndOfPersianMonth(_date)
            .Date
            .AddHours(23)
            .AddMinutes(59)
            .AddSeconds(59);

    public DateInfo AtEndOfMonthFa()
=> new DateInfo(EndOfMonthFa);

    // QUARTER
    public DateTime StartOfQuarterFa
    {
        get
        {
            int quarter = _hs.PersianQuarter(_date);

            int month = quarter switch
            {
                1 => 1,
                2 => 4,
                3 => 7,
                _ => 10
            };

            return _hs.ToGregorian(YearFa, month, 1);
        }
    }

    public DateInfo AtStartOfQuarterFa()
=> new DateInfo(StartOfQuarterFa);

    
    public DateTime EndOfQuarterFa
    {
        get
        {
            int quarter = _hs.PersianQuarter(_date);

            int endMonth = quarter switch
            {
                1 => 3,
                2 => 6,
                3 => 9,
                4 => 12,
                _ => 12
            };

            int year = YearFa;

            int day = GetDaysInMonth(year, endMonth);

            return _hs.ToGregorian(year, endMonth, day, 23, 59, 59);
        }
    }
    public DateInfo AtEndOfQuarterFa() => new DateInfo(EndOfQuarterFa);
    // HALF YEAR
    public DateTime StartOfHalfYearFa
    {
        get
        {
            int month = MonthFa <= 6 ? 1 : 7;

            return _hs.ToGregorian(YearFa, month, 1);
        }
    }

    public DateInfo AtStartOfHalfYearFa() => new DateInfo(StartOfHalfYearFa);
    public DateTime EndOfHalfYearFa
    {
        get
        {
            int endMonth = MonthFa <= 6 ? 6 : 12;

            int year = YearFa;

            int day = GetDaysInMonth(year, endMonth);

            return _hs.ToGregorian(year, endMonth, day, 23, 59, 59);
        }
    }
    public DateInfo AtEndOfHalfYearFa() => new DateInfo(EndOfHalfYearFa);

    // SEASON
    public DateTime StartOfSeasonFa
    {
        get
        {
            int month = SeasonFa switch
            {
                "بهار" => 1,
                "تابستان" => 4,
                "پاییز" => 7,
                _ => 10
            };

            return _hs.ToGregorian(YearFa, month, 1);
        }
    }
    public DateInfo AtStartOfSeasonFa() => new DateInfo(StartOfSeasonFa);

    public DateTime EndOfSeasonFa
    {
        get
        {
            int season = _hs.PersianQuarter(_date);

            int endMonth = season switch
            {
                1 => 3,
                2 => 6,
                3 => 9,
                4 => 12,
                _ => 12
            };

            int year = YearFa;

            int day = GetDaysInMonth(year, endMonth);

            return _hs.ToGregorian(year, endMonth, day, 23, 59, 59);
        }
    }
    public DateInfo AtEndOfSeasonFa() => new DateInfo(EndOfSeasonFa);

    // YEAR
    public DateTime StartOfYearFa
        => _hs.ToGregorian(YearFa, 1, 1);
    public DateInfo AtStartOfYearFa() => new DateInfo(StartOfYearFa);

    public DateTime EndOfYearFa
    {
        get
        {
            int lastDay = IsLeapYearFa ? 30 : 29;

            return _hs.ToGregorian(
                    YearFa,
                    12,
                    lastDay)
                .Date
                .AddHours(23)
                .AddMinutes(59)
                .AddSeconds(59);
        }
    }

    public DateInfo AtEndOfYearFa() => new DateInfo(EndOfYearFa);

    // DAY
    public DateTime StartOfDayEn
        => _hs.StartOfDay(_date);


    public DateTime EndOfDayEn
        => _hs.EndOfDay(_date);

    // WEEK
    public DateTime StartOfWeekEn
    {
        get
        {
            int diff =
                ((int)_date.DayOfWeek + 6) % 7;

            return _date.Date.AddDays(-diff);
        }
    }


    public DateInfo AtStartOfWeekEn() => new DateInfo(StartOfWeekEn);
    public DateTime EndOfWeekEn
        => StartOfWeekEn.AddDays(6)
            .Date
            .AddHours(23)
            .AddMinutes(59)
            .AddSeconds(59);

    // MONTH
    public DateTime StartOfMonthEn
        => _hs.StartOfMonth(_date);
    public DateTime EndOfMonthEn
        => _hs.EndOfMonth(_date);
    public DateInfo AtStartOfMonthEn() => new DateInfo(StartOfMonthEn);
    public DateInfo AtEndOfMonthEn() => new DateInfo(EndOfMonthEn);
    // QUARTER
    public DateTime StartOfQuarterEn
    {
        get
        {
            int quarter = _hs.GregorianQuarter(_date);

            int month = quarter switch
            {
                1 => 1,
                2 => 4,
                3 => 7,
                _ => 10
            };

            return new DateTime(YearEn, month, 1);
        }
    }

    public DateTime EndOfQuarterEn
    {
        get
        {
            return StartOfQuarterEn
                .AddMonths(3)
                .AddTicks(-1);
        }
    }
    public DateInfo AtStartOfQuarterEn()
=> new DateInfo(StartOfQuarterEn);
    public DateInfo AtEndOfQuarterEn()
=> new DateInfo(EndOfQuarterEn);
    // HALF YEAR
    public DateTime StartOfHalfYearEn
    {
        get
        {
            int month = MonthEn <= 6 ? 1 : 7;

            return new DateTime(YearEn, month, 1);
        }
    }

    public DateTime EndOfHalfYearEn
    {
        get
        {
            return StartOfHalfYearEn
                .AddMonths(6)
                .AddTicks(-1);
        }
    }

    // SEASON
    public DateTime StartOfSeasonEn
    {
        get
        {
            int month = SeasonEn switch
            {
                "Spring" => 1,
                "Summer" => 4,
                "Autumn" => 7,
                _ => 10
            };

            return new DateTime(YearEn, month, 1);
        }
    }

    public DateTime EndOfSeasonEn
    {
        get
        {
            return StartOfSeasonEn
                .AddMonths(3)
                .AddTicks(-1);
        }
    }

    // YEAR
    public DateTime StartOfYearEn
        => _hs.StartOfYear(_date);

    public DateTime EndOfYearEn
        => _hs.EndOfYear(_date);

    public DateInfo AtStartOfYearEn() => new DateInfo(StartOfYearEn);
    public DateInfo AtEndOfYearEn() => new DateInfo(EndOfYearEn);
    #endregion



    #region ================= WEEK OF MONTH =================

    // =====================================================
    // PERSIAN
    // =====================================================

    public int WeekOfMonthFa
    {
        get
        {
            DateTime firstDayOfMonth =
                ToDateTime(
                    YearFa,
                    MonthFa,
                    1,
                    0,
                    0,
                    0,
                    0);

            int firstDayOffset =
                ((int)firstDayOfMonth.DayOfWeek + 1) % 7;

            return ((DayFa + firstDayOffset - 1) / 7) + 1;
        }
    }

    // =====================================================
    // GREGORIAN
    // =====================================================

    public int WeekOfMonthEn
    {
        get
        {
            DateTime firstDayOfMonth =
                new(
                    YearEn,
                    MonthEn,
                    1);

            int firstDayOffset =
                ((int)firstDayOfMonth.DayOfWeek + 6) % 7;

            return ((DayEn + firstDayOffset - 1) / 7) + 1;
        }
    }

    #endregion

    #region ================= WEEK OF YEAR =================

    // =====================================================
    // PERSIAN
    // =====================================================

    public int WeekOfYearFa
    {
        get
        {
            DateTime firstDayOfYear =
                ToDateTime(
                    YearFa,
                    1,
                    1,
                    0,
                    0,
                    0,
                    0);

            int offset =
                ((int)firstDayOfYear.DayOfWeek + 1) % 7;

            int days =
                (_date.Date - firstDayOfYear.Date).Days;

            return ((days + offset) / 7) + 1;
        }
    }

    // =====================================================
    // GREGORIAN
    // =====================================================

    public int WeekOfYearEn =>
        CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(
            _date,
            CalendarWeekRule.FirstFourDayWeek,
            DayOfWeek.Monday);

    #endregion

    #region ================= QUARTER INFO =================


    public int WeekOfQuarterFa
    {
        get
        {
            int days =
                (_date.Date - StartOfQuarterFa.Date).Days;

            return (days / 7) + 1;
        }
    }

    #endregion

    #region ================= GREGORIAN QUARTER =================


    public int WeekOfQuarterEn
    {
        get
        {
            int days =
                (_date.Date - StartOfQuarterEn.Date).Days;

            return (days / 7) + 1;
        }
    }

    #endregion

    #region ================= WEEK FLAGS =================

    public bool IsFirstWeekOfMonthFa =>
        WeekOfMonthFa == 1;

    public bool IsLastWeekOfMonthFa
    {
        get
        {
            int totalWeeks =
                ((_hs.PersianMonth(_date) - 1) / 7) + 1;

            return WeekOfMonthFa == totalWeeks;
        }
    }

    public bool IsFirstWeekOfQuarterFa =>
        WeekOfQuarterFa == 1;

    public bool IsLastWeekOfQuarterFa
    {
        get
        {
            int totalWeeks =
                ((EndOfQuarterFa.Date - StartOfQuarterFa.Date).Days / 7) + 1;

            return WeekOfQuarterFa == totalWeeks;
        }
    }

    public bool IsFirstWeekOfYearFa =>
        WeekOfYearFa == 1;

    public bool IsLastWeekOfYearFa
    {
        get
        {
            int totalWeeks =
                ((EndOfYearFa.Date - StartOfYearFa.Date).Days / 7) + 1;

            return WeekOfYearFa == totalWeeks;
        }
    }

    public bool IsFirstWeekOfMonthEn =>
        WeekOfMonthEn == 1;

    public bool IsLastWeekOfMonthEn
    {
        get
        {
            int totalWeeks =
                ((_date.Month - 1) / 7) + 1;

            return WeekOfMonthEn == totalWeeks;
        }
    }

    public bool IsFirstWeekOfQuarterEn =>
        WeekOfQuarterEn == 1;

    public bool IsLastWeekOfQuarterEn
    {
        get
        {
            int totalWeeks =
                ((EndOfQuarterEn.Date - StartOfQuarterEn.Date).Days / 7) + 1;

            return WeekOfQuarterEn == totalWeeks;
        }
    }

    public bool IsFirstWeekOfYearEn =>
        WeekOfYearEn == 1;

    public bool IsLastWeekOfYearEn
    {
        get
        {
            int totalWeeks =
                CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(
                    new DateTime(YearEn, 12, 31),
                    CalendarWeekRule.FirstFourDayWeek,
                    DayOfWeek.Monday);

            return WeekOfYearEn == totalWeeks;
        }
    }

    #endregion

    #region ================= SAFE HELPERS =================

    // =====================================================
    // PERSIAN HELPERS
    // =====================================================

    private DateTime AddDaysFa(int days)
    {
        return _date.AddDays(days);
    }

    private DateTime AddMonthsFa(int months)
    {
        int year = YearFa;
        int month = MonthFa + months;
        int day = DayFa;

        while (month > 12)
        {
            year++;
            month -= 12;
        }

        while (month < 1)
        {
            year--;
            month += 12;
        }

        int maxDay =
            GetDaysInMonth(year, month);

        if (day > maxDay)
            day = maxDay;

        return ToDateTime(
            year,
            month,
            day,
            0,
            0,
            0,
            0);
    }

    private DateTime AddYearsFa(int years)
    {
        int year =
            YearFa + years;

        int month =
            MonthFa;

        int day =
            DayFa;

        int maxDay =
            GetDaysInMonth(
                year,
                month);

        if (day > maxDay)
            day = maxDay;

        return ToDateTime(
            year,
            month,
            day,
            0,
            0,
            0,
            0);
    }

    private DateTime AddDaysEn(int days)
    {
        return _date.AddDays(days);
    }

    private DateTime AddMonthsEn(int months)
    {
        return _date.AddMonths(months);
    }

    private DateTime AddYearsEn(int years)
    {
        return _date.AddYears(years);
    }

    #endregion

    #region ================= NAVIGATION =================


    public DateTime YesterdayFa =>
        AddDaysFa(-1);
    public DateInfo AtYesterdayFa() => new DateInfo(YesterdayFa);

    public DateTime TomorrowFa =>
        AddDaysFa(1);
    public DateInfo AtTomorrowFa() => new DateInfo(TomorrowFa);

    // 🇬🇧 Gregorian

    public DateTime YesterdayEn =>
        AddDaysEn(-1);

    public DateTime TomorrowEn =>
        AddDaysEn(1);


    // 🇮🇷 Persian

    public DateTime PreviousWeekFa =>
        AddDaysFa(-7);

    public DateInfo AtPreviousWeekFa() => new DateInfo(PreviousWeekFa);
    public DateInfo AtPreviousWeekFa_FirstDay() => AtPreviousWeekFa().AtStartOfWeekFa();

    public DateTime NextWeekFa =>
        AddDaysFa(7);
    public DateInfo AtNextWeekFa() => new DateInfo(NextWeekFa);
    public DateInfo AtNextWeekFa_FirstDay() => AtNextWeekFa().AtStartOfWeekFa();

    // 🇬🇧 Gregorian

    public DateTime PreviousWeekEn =>
        AddDaysEn(-7);
    public DateInfo AtPreviousWeekEn() => new DateInfo(PreviousWeekEn);
    public DateInfo PreviousWeekEn_FirstDay() => AtPreviousWeekEn().AtStartOfWeekEn();
    public DateTime NextWeekEn =>
        AddDaysEn(7);

    public DateInfo AtNextWeekEn() => new DateInfo(NextWeekEn);
    public DateInfo AtNextWeekEn_FirstDay() => AtNextWeekEn().AtStartOfWeekEn();
    // 🇮🇷 Persian

    public DateTime PreviousMonthFa =>
        AddMonthsFa(-1);

    public DateTime NextMonthFa =>
        AddMonthsFa(1);

    public DateInfo AtPreviousMonthFa() =>
       new DateInfo(PreviousMonthFa);
    public DateInfo AtAtPreviousMonthFa_FirstDay() => AtPreviousMonthFa().AtStartOfMonthFa();

    public DateInfo AtNextMonthFa() =>
        new DateInfo(NextMonthFa);

    public DateInfo AtAtNextMonthFa_FirstDay() => AtNextMonthFa().AtStartOfMonthFa();

    // 🇬🇧 Gregorian

    public DateTime PreviousMonthEn =>
        AddMonthsEn(-1);

    public DateTime NextMonthEn =>
        AddMonthsEn(1);

    public DateInfo AtPreviousMonthEn() => new DateInfo(PreviousMonthEn);
    public DateInfo AtPreviousMonthEn_FirstDay() => AtPreviousMonthEn().AtStartOfMonthFa();

    public DateInfo AtNextMonthEn() => new DateInfo(NextMonthEn);
    public DateInfo AtNextMonthEn_FirstDay() => AtNextMonthEn().AtStartOfMonthFa();

    // 🇮🇷 Persian

    public DateTime PreviousQuarterFa =>
        AddMonthsFa(-3);

    public DateTime NextQuarterFa =>
        AddMonthsFa(3);
    public DateInfo AtPreviousQuarterFa() =>
   new DateInfo(PreviousQuarterFa);

    public DateInfo AtNextQuarterFa() =>
        new DateInfo(NextQuarterFa);

    public DateInfo AtPreviousQuarterFa_FirstDay() => AtPreviousQuarterFa().AtStartOfQuarterFa();

    public DateInfo AtNextQuarterFa_FirstDay() => AtNextQuarterFa().AtStartOfQuarterFa();
    // 🇬🇧 Gregorian

    public DateTime PreviousQuarterEn =>
        AddMonthsEn(-3);

    public DateTime NextQuarterEn =>
        AddMonthsEn(3);


    public DateInfo AtPreviousQuarterEn() => new DateInfo(PreviousQuarterEn);
    public DateInfo AtPreviousQuarterEn_FirstDay() => AtPreviousQuarterEn().AtStartOfQuarterEn();

    public DateInfo AtNextQuarterEn() => new DateInfo(NextQuarterEn);
    public DateInfo AtNextQuarterEn_FirstDay() => AtNextQuarterEn().AtStartOfQuarterEn();
    // 🇮🇷 Persian

    public DateTime PreviousHalfYearFa =>
        AddMonthsFa(-6);

    public DateTime NextHalfYearFa =>
        AddMonthsFa(6);

    public DateInfo AtPreviousHalfYearFa() => new DateInfo(PreviousHalfYearFa);
    public DateInfo AtPreviousHalfYearFa_FirstDay() => AtPreviousHalfYearFa().AtStartOfYearFa();
    public DateInfo AtNextHalfYearFa() => new DateInfo(NextHalfYearFa);
    public DateInfo AtNextHalfYearFa_FirstDay() => AtNextHalfYearFa().AtStartOfYearFa();
   
    // 🇬🇧 Gregorian

    public DateTime PreviousHalfYearEn =>
        AddMonthsEn(-6);

    public DateTime NextHalfYearEn =>
        AddMonthsEn(6);


    public DateInfo AtPreviousHalfYearEn() => new DateInfo(PreviousHalfYearEn);
    public DateInfo AtPreviousHalfYearEn_FirstDay() => AtPreviousHalfYearEn().AtStartOfYearEn();


    public DateInfo AtNextHalfYearEn() => new DateInfo(NextHalfYearEn);
    public DateInfo AtNextHalfYearEn_FirstDay() => AtNextHalfYearEn().AtStartOfYearEn();

    // 🇮🇷 Persian

    public DateTime PreviousYearFa =>
        AddYearsFa(-1);

    public DateTime NextYearFa =>
        AddYearsFa(1);
    public DateInfo AtPreviousYearFa() =>
     new DateInfo(PreviousYearFa);

    public DateInfo AtNextYearFa() =>
         new DateInfo(NextYearFa);


    public DateInfo AtAtPreviousYearFa_FirstDay() => AtPreviousYearFa().AtStartOfYearFa();

    public DateInfo AtAtNextYearFa_FirstDay() => AtNextYearFa().AtStartOfYearFa();
    // 🇬🇧 Gregorian

    public DateTime PreviousYearEn =>
        AddYearsEn(-1);

    public DateTime NextYearEn =>
        AddYearsEn(1);

    public DateInfo AtPreviousYearEn() => new DateInfo(PreviousYearEn);
    public DateInfo AtPreviousYearEn_FirstDay() => AtPreviousYearEn().AtStartOfYearEn();

    public DateInfo AtNextYearEn() => new DateInfo(NextYearEn);
    public DateInfo AtNextYearEn_FirstDay() => AtNextYearEn().AtStartOfYearEn();
    #endregion

    #region ================= HALF YEAR =================


    public bool IsFirstHalfFa =>
        MonthFa is >= 1 and <= 6;

    public bool IsSecondHalfFa =>
        MonthFa is >= 7 and <= 12;

    public int HalfYearFaNumber =>
        IsFirstHalfFa ? 1 : 2;

    public string HalfYearFaText =>
        IsFirstHalfFa
            ? "نیمه اول سال"
            : "نیمه دوم سال";


    public bool IsFirstHalfEn =>
        MonthEn is >= 1 and <= 6;

    public bool IsSecondHalfEn =>
        MonthEn is >= 7 and <= 12;

    public int HalfYearEnNumber =>
        IsFirstHalfEn ? 1 : 2;

    public string HalfYearEnText =>
        IsFirstHalfEn
            ? "First Half"
            : "Second Half";

    #endregion

    #region ================= WEEK HELPERS =================


    public int TotalWeeksFa
    {
        get
        {
            int days =
                (EndOfYearFa.Date - StartOfYearFa.Date).Days + 1;

            return (int)Math.Ceiling(days / 7d);
        }
    }

    public int TotalWeeksEn
    {
        get
        {
            int days =
                (EndOfYearEn.Date - StartOfYearEn.Date).Days + 1;

            return (int)Math.Ceiling(days / 7d);
        }
    }


    #endregion

    #region ================= FLAGS =================


    // 🇮🇷 Persian

    public bool IsFirstDayOfMonthFa =>
        DayFa == 1;

    public bool IsLastDayOfMonthFa =>
        DayFa ==
        GetDaysInMonth(
            YearFa,
            MonthFa);

    // 🇬🇧 Gregorian

    public bool IsFirstDayOfMonthEn =>
        DayEn == 1;

    public bool IsLastDayOfMonthEn =>
        DayEn ==
        DateTime.DaysInMonth(
            YearEn,
            MonthEn);


    // 🇮🇷 Persian

    public bool IsFirstMonthOfYearFa =>
        MonthFa == 1;

    public bool IsLastMonthOfYearFa =>
        MonthFa == 12;

    // 🇬🇧 Gregorian

    public bool IsFirstMonthOfYearEn =>
        MonthEn == 1;

    public bool IsLastMonthOfYearEn =>
        MonthEn == 12;

    // 🇮🇷 Persian

    public bool IsFirstWeekFa =>
        WeekFa == 1;

    public bool IsLastWeekFa =>
        WeekFa == TotalWeeksFa;

    // 🇬🇧 Gregorian

    public bool IsFirstWeekEn =>
        WeekEn == 1;

    public bool IsLastWeekEn =>
        WeekEn == TotalWeeksEn;

    #endregion

    #region ================= LEAP YEAR =================


    public string LeapYearFaText =>
      IsLeapYearFa
          ? "سال کبیسه"
          : "سال عادی";


    public string LeapYearEnText =>
        IsLeapYearEn
            ? "Leap Year"
            : "Normal Year";

    #endregion
    #region ================= FORMATS (ALL REQUIRED PATTERNS) =================


    public string FormatFa =>
        $"{YearFa}/{MonthFa:00}/{DayFa:00}";

    public string FormatFaCompact =>
        $"{YearFa}{MonthFa:00}{DayFa:00}";

    public string FormatFaShort =>
        $"{YearFa % 100:00}/{MonthFa:00}/{DayFa:00}";

    public string FormatFaNoPad =>
        $"{YearFa}/{MonthFa}/{DayFa}";

    public string FormatFaLong =>
        $"{WeekDayFa} {DayFa} {MonthNameFa} {YearFa}";


    public string FormatEn =>
        _date.ToString("yyyy/MM/dd");

    public string FormatEnDash =>
        _date.ToString("yyyy-MM-dd");

    public string FormatEnCompact =>
        _date.ToString("yyyyMMdd");

    public string FormatEnShort =>
        _date.ToString("yy/MM/dd");

    public string FormatEnLong =>
        _date.ToString("dddd dd MMMM yyyy");

    public string FormatEnWithTime =>
        _date.ToString("yyyy/MM/dd HH:mm:ss");

    public string FormatEnWithTimeShort =>
        _date.ToString("yyyy/MM/dd HH:mm");

    public string FormatEnClock =>
        _date.ToString("HH:mm:ss");

    public string FormatEnClockShort =>
        _date.ToString("HH:mm");

    #endregion

    #region ================= DATE ONLY =================

    public string DateOnlyFa =>
        $"{YearFa}/{MonthFa:00}/{DayFa:00}";

    public string DateOnlyEn =>
        _date.ToString("yyyy-MM-dd");

    #endregion

    #region ================= RAW API / DATABASE PATTERNS =================

    public string Pattern_YYYY_MM =>
        _date.ToString("yyyy-MM");

    public string Pattern_YYYY_MM_DD =>
        _date.ToString("yyyy-MM-dd");

    public string Pattern_YYYY_MM_DD_HH =>
        _date.ToString("yyyy-MM-dd HH");

    public string Pattern_YYYY_MM_DD_HH_MM =>
        _date.ToString("yyyy-MM-dd HH:mm");

    public string Pattern_YYYY_MM_DD_HH_MM_SS =>
        _date.ToString("yyyy-MM-dd HH:mm:ss");

    public string Pattern_YYYYMMDD =>
        _date.ToString("yyyyMMdd");

    public string Pattern_YYYYMM =>
        _date.ToString("yyyyMM");

    public string Pattern_Fa_YYYY_MM =>
        $"{YearFa}-{MonthFa:00}";

    public string Pattern_Fa_YYYY_MM_DD =>
        $"{YearFa}-{MonthFa:00}-{DayFa:00}";

    public string Pattern_Fa_YYYY_MM_SLASH =>
        $"{YearFa}/{MonthFa:00}";

    public string Pattern_Fa_YYYY_MM_DD_SLASH =>
        $"{YearFa}/{MonthFa:00}/{DayFa:00}";

    public string Pattern_Fa_YYYYMMDD =>
        $"{YearFa}{MonthFa:00}{DayFa:00}";

    #endregion

    #region ================= SEASON PATTERNS =================

    public string Pattern_Season =>
        $"{YearFa}-{SeasonFa}";

    public string Pattern_YYYY_Season =>
        $"{YearFa}/{SeasonFa}";

    public string Pattern_Season_Compact =>
        $"{YearFa}{SeasonFa}";

    public string Pattern_Season_Label =>
        $"{_hs.PersianSeason}-{YearFa}";

    #endregion

    #region ================= WEEK PATTERNS =================

    public string Pattern_Fa_YYYY_MM_WW =>
        $"{YearFa}-{MonthFa:00}-{WeekOfYearFa:00}";

    public string Pattern_Fa_YYYY_MM_WW_SLASH =>
        $"{YearFa}/{MonthFa:00}/{WeekOfYearFa:00}";

    public string Pattern_Fa_YYYY_WW =>
        $"{YearFa}-{WeekOfYearFa:00}";

    public string Pattern_Fa_WW =>
        $"{WeekOfYearFa:00}";

    public string Pattern_Fa_YYYY_Quarter_Week =>
        $"{YearFa}-Q{QuarterFa}-W{WeekOfQuarterFa}";


    public string Pattern_En_YYYY_MM_WW =>
        $"{YearEn}-{MonthEn:00}-{WeekOfYearEn:00}";

    public string Pattern_En_YYYY_WW =>
        $"{YearEn}-{WeekOfYearEn:00}";

    public string Pattern_En_WW =>
        $"{WeekOfYearEn:00}";

    public string Pattern_En_YYYY_Quarter_Week =>
        $"{YearEn}-Q{QuarterEn}-W{WeekOfQuarterEn}";

    #endregion

    #region ================= UI / HUMAN READABLE =================

    public string FormatFaUI =>
        $"{WeekDayFa}، {DayFa} {MonthNameFa} {YearFa}";

    public string FormatEnUI =>
        $"{_date:dddd}, {_date:dd MMMM yyyy}";

    public string FormatEnFullUI =>
        $"{_date:dddd dd MMMM yyyy HH:mm}";

    #endregion

    #region ================= LOGGING FORMATS =================

    public string LogFormatEn =>
        _date.ToString("yyyy-MM-dd HH:mm:ss.fff");

    #endregion

    #region ================= SORTABLE KEYS =================

    public string SortKeyEn =>
        _date.ToString("yyyyMMddHHmmss");



    #endregion

    #region ================= WEEKEND / WORKDAY =================

    public bool IsWeekendFa =>
        _date.DayOfWeek == DayOfWeek.Friday;

    public bool IsWorkDayFa =>
        !IsWeekendFa;
    public bool IsWeekendEn =>
        _date.DayOfWeek == DayOfWeek.Saturday
        || _date.DayOfWeek == DayOfWeek.Sunday;

    public bool IsWorkDayEn =>
        !IsWeekendEn;

    #endregion


    #region ================= CONVERT =================

    public DateTime ToGregorian(int y, int m, int d)
        => _hs.ToGregorian(y, m, d);

    public DateTime ToGregorianFull(int y, int m, int d, int h, int min, int s)
        => _hs.ToGregorian(y, m, d, h, min, s);

    #endregion

    #region ================= WORDS (FA + EN) =================

    public string ToFaWords(int value)
        => NumberToWordsConverter.NumberToPersianWords(value);

    public string ToEnWords(int value)
        => NumberToWordsConverter.NumberToEnglishWords(value);

    #endregion

    #region ================= DIFFERENCE =================

    public int DiffDays(DateTime other) => _hs.DiffDays(other, _date);

    public int DiffMonths(DateTime other)
        => _hs.DiffMonths(other, _date);

    public int DiffYears(DateTime other)
 => _hs.DiffYears(other, _date);


    #endregion

    #region ================= REMAINDER (FA + EN) =================

    public string RemainderDaysFa(DateTime other)
    {
        int d = Math.Abs(DiffDays(other));
        return $"{ToFaWords(d)} روز";
    }

    public string RemainderDaysEn(DateTime other)
    {
        int d = Math.Abs(DiffDays(other));
        return $"{ToEnWords(d)} days";
    }

    public string RemainderFa(DateTime other)
    {
        var (y, m, d) = DiffYMD(other);
        return $"{ToFaWords(y)} سال و {ToFaWords(m)} ماه و {ToFaWords(d)} روز";
    }

    public string RemainderEn(DateTime other)
    {
        var (y, m, d) = DiffYMD(other);
        return $"{ToEnWords(y)} years, {ToEnWords(m)} months, {ToEnWords(d)} days";
    }
    public (int Years, int Months, int Days) DiffYMD(DateTime other)
    {
        return _hs.DiffYMD(other, _date);
    }


    #endregion


    #region ================= SERIALIZE =================

    public string ToJson()
    {
        return JsonSerializer.Serialize(new
        {
            Fa = new
            {
                YearFa,
                MonthFa,
                DayFa,
                MonthNameFa,
                WeekDayFa,
                SeasonFa
            },
            En = new
            {
                YearEn,
                MonthEn,
                DayEn,
                MonthNameEn,
                WeekDayEn
            },
            Raw = _date
        });
    }

    #endregion

    #region ================= TIMEZONE =================

    public DateTime InTimeZone(string id)
    {
        var tz = TimeZoneInfo.FindSystemTimeZoneById(id);
        return TimeZoneInfo.ConvertTime(_date, tz);
    }

    #endregion

    public override string ToString()
        => FormatFa;


    public long ToUnix => _hs.ToUnix(_date);

    public DateTime FromUnix(long unix) => _hs.FromUnix(unix);

    public DateTime ToUtc => _hs.ToUtc(_date);

    public DateTime ToLocal => _hs.ToLocal(_date);


    public int BusinessDays(DateTime other) => _hs.BusinessDays(_date, other);

    public List<DateTime> DateRange(DateTime other) => _hs.DateRange(_date, other);


    public bool IsValidPersianDate(int y, int m, int d)
    => _hs.IsValidPersianDate(y, m, d);

    public bool IsLeapYear(int year)
        => _hs.IsLeapYear(year);

    public DateTime ParsePersian(string value)
    => _hs.ParsePersian(value);

    public bool TryParsePersian(string value, out DateTime result)
        => _hs.TryParsePersian(value, out result);


    public DateTime AddYears(int v) => _hs.AddYears(_date, v);

    public DateTime AddMonths(int v) => _hs.AddMonths(_date, v);

    public DateTime AddDays(int v) => _hs.AddDays(_date, v);

    public DateTime AddHours(int v) => _hs.AddHours(_date, v);

    public DateTime AddMinutes(int v) => _hs.AddMinutes(_date, v);

    public DateTime AddSeconds(int v) => _hs.AddSeconds(_date, v);

    public DateTime ToGregorian(int y, int m, int d, int h, int min, int s)
        => _hs.ToGregorian(y, m, d, h, min, s);

    public string TimeAgo => _hs.TimeAgo(_date);

    public string TimeLeft => _hs.TimeLeft(_date);

    public string SmartDate => _hs.SmartDate(_date);

    public string ToPersianDigits(string v)
    => _hs.ToPersianDigits(v);

    public string ToEnglishDigits(string v)
        => _hs.ToEnglishDigits(v);

    public int Age(DateTime birthDate)
    => _hs.Age(birthDate);

    public DateTime Min(params DateTime[] d)
    => _hs.Min(d);

    public DateTime Max(params DateTime[] d)
        => _hs.Max(d);

    public DateOnly ToDateOnly()
    => _hs.ToDateOnly(_date);

    public TimeOnly ToTimeOnly()
        => _hs.ToTimeOnly(_date);

    public DateTimeOffset ToOffset()
        => _hs.ToOffset(_date);

    public DateTime Min(DateTime other)
    => _hs.Min(_date, other);

    public DateTime Max(DateTime other)
        => _hs.Max(_date, other);


    public int DiffHours(DateTime other)
    => _hs.DiffHours(_date, other);

    public int DiffMinutes(DateTime other)
        => _hs.DiffMinutes(_date, other);

    public int DiffSeconds(DateTime other)
        => _hs.DiffSeconds(_date, other);


    public int PersianQuarter => _hs.PersianQuarter(_date);
    public int GregorianQuarter => _hs.GregorianQuarter(_date);

    public int PersianHalfYear => _hs.PersianHalfYear(_date);
    public int GregorianHalfYear => _hs.GregorianHalfYear(_date);

    public int PersianWeekOfYear => _hs.PersianWeekOfYear(_date);
    public int GregorianWeekOfYear => _hs.GregorianWeekOfYear(_date);



    public string ShortTime(DateTime date)
        => _hs.ShortTime(date);

    public string FullTime(DateTime date)
       => _hs.FullTime(date);

    public string IsoDate(DateTime date)
       => _hs.IsoDate(date);

    public string IsoDateTime(DateTime date)
       => _hs.IsoDateTime(date);

}