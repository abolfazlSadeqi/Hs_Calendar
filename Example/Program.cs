

using Hs_Calendar;

TimeInfo timeStringSpan = new TimeInfo(DateTime.Now.TimeOfDay);
var dt = new DateInfo();

Console.WriteLine(" QuarterFaText: " + dt.QuarterFaText);
Console.WriteLine(" MonthFaText: " + dt.MonthFaText);
Console.WriteLine(" NextHalfYearFa: " + dt.NextHalfYearFa);
Console.WriteLine("NextWeekFa : " + dt.NextWeekFa);
Console.WriteLine("QuarterEnText : " + dt.QuarterEnText);
Console.WriteLine(" WeekFaText: " + dt.WeekFaText);
Console.WriteLine(" WeekOfMonthFa: " + dt.WeekOfMonthFa);
Console.WriteLine(" WeekOfQuarterFa: " + dt.WeekOfQuarterFa);
Console.WriteLine(" AtStartOfDayFa: " + dt.AtStartOfDayFa());
Console.WriteLine(" AtStartOfMonthFa: " + dt.AtStartOfMonthFa());
Console.WriteLine(" AtStartOfSeasonFa: " + dt.AtStartOfSeasonFa());
Console.WriteLine(" AtStartOfWeekFa: " + dt.AtStartOfWeekFa());
Console.WriteLine(" AtStartOfYearFa: " + dt.AtStartOfYearFa());

Console.WriteLine(" AtEndOfDayFa: " + dt.AtEndOfDayFa());
Console.WriteLine(" AtEndOfMonthFa: " + dt.AtEndOfMonthFa());
Console.WriteLine(" AtEndOfSeasonFa: " + dt.AtEndOfSeasonFa());
Console.WriteLine(" AtEndOfWeekFa: " + dt.AtEndOfWeekFa());
Console.WriteLine("AtEndOfYearFa : " + dt.AtEndOfYearFa());
Console.WriteLine("AtEndOfHalfYearFa : " + dt.AtEndOfHalfYearFa());
Console.WriteLine(" AtEndOfWeekFa: " + dt.AtEndOfWeekFa());

Console.WriteLine(" IsLeapYearFa: " + dt.IsLeapYearFa);
Console.WriteLine(" IsYesterday: " + dt.IsYesterday);
Console.WriteLine("AtNextHalfYearFa : " + dt.AtNextHalfYearFa());
Console.WriteLine("AtTomorrowFa : " + dt.AtTomorrowFa());
Console.WriteLine("AtYesterdayFa : " + dt.AtYesterdayFa());
Console.WriteLine("----------------" );

Console.WriteLine(" AtNextHalfYearFa: " + dt.AtNextHalfYearFa());
Console.WriteLine(" AtNextMonthFa: " + dt.AtNextMonthFa().AtStartOfMonthFa());
Console.WriteLine(" AtNextQuarterFa: " + dt.AtNextQuarterFa().AtStartOfQuarterFa());
Console.WriteLine("AtNextWeekFa : " + dt.AtNextWeekFa().AtStartOfWeekFa());
Console.WriteLine(" AtNextYearFa: " + dt.AtNextYearFa().AtStartOfYearFa());

Console.WriteLine("----------------");

Console.WriteLine(" AtNextHalfYearFa: " + dt.AtNextHalfYearFa());
Console.WriteLine("AtNextMonthFa : " + dt.AtNextMonthFa());
Console.WriteLine(" AtNextQuarterFa: " + dt.AtNextQuarterFa());
Console.WriteLine(" AtNextWeekFa: " + dt.AtNextWeekFa());
Console.WriteLine(" AtNextYearFa: " + dt.AtNextYearFa());
Console.WriteLine("----------------");
Console.WriteLine(" AtPreviousMonthFa: " + dt.AtPreviousMonthFa());
Console.WriteLine("AtPreviousQuarterFa : " + dt.AtPreviousQuarterFa());
Console.WriteLine(" AtPreviousWeekFa: " + dt.AtPreviousWeekFa());
Console.WriteLine("AtPreviousYearFa : " + dt.AtPreviousYearFa());

Console.WriteLine("----------------");
Console.WriteLine("AtAtNextMonthFa_FirstDay : " + dt.AtAtNextMonthFa_FirstDay());
Console.WriteLine("AtAtNextYearFa_FirstDay : " + dt.AtAtNextYearFa_FirstDay());
Console.WriteLine(" AtAtPreviousMonthFa_FirstDay: " + dt.AtAtPreviousMonthFa_FirstDay());
Console.WriteLine(" AtAtPreviousYearFa_FirstDay: " + dt.AtAtPreviousYearFa_FirstDay());
Console.WriteLine("AtNextHalfYearEn_FirstDay : " + dt.AtNextHalfYearEn_FirstDay());
Console.WriteLine(" AtNextHalfYearFa_FirstDay: " + dt.AtNextHalfYearFa_FirstDay());
Console.WriteLine(" AtNextMonthEn_FirstDay: " + dt.AtNextMonthEn_FirstDay());
Console.WriteLine(" AtNextQuarterEn_FirstDay: " + dt.AtNextQuarterEn_FirstDay());
Console.WriteLine(" AtNextQuarterFa_FirstDay: " + dt.AtNextQuarterFa_FirstDay());
Console.WriteLine("AtNextWeekEn_FirstDay : " + dt.AtNextWeekEn_FirstDay());
Console.WriteLine(" AtNextWeekFa_FirstDay: " + dt.AtNextWeekFa_FirstDay());
Console.WriteLine("AtNextYearEn_FirstDay : " + dt.AtNextYearEn_FirstDay());
Console.WriteLine("RemainderDaysFa : " + dt.RemainderDaysFa(DateTime.Now.AddDays(10)));
Console.WriteLine("RemainderFa : " + dt.RemainderFa(DateTime.Now.AddDays(10)));

Console.WriteLine("----------------");
Console.WriteLine(" AgoFa: " + timeStringSpan.AgoFa);
Console.WriteLine(" AgoEn: " + timeStringSpan.AgoEn);
Console.WriteLine(" Clock24WithMs: " + timeStringSpan.Clock24WithMs);
Console.WriteLine(" DigitalClock: " + timeStringSpan.DigitalClock);
Console.WriteLine("SmartFormat : " + timeStringSpan.SmartFormat);
Console.WriteLine(" UrlFormat: " + timeStringSpan.UrlFormat);





