# Hs_Calender

A powerful, dual-calendar C# library for seamless Date and Time manipulation. It provides a fluent, object-oriented wrapper around `DateTime` and `TimeSpan`, offering extensive support for both **Gregorian** and **Persian** calendars, along with smart formatting, time zone conversion, and arithmetic operations.

## ✨ Features

*   **Dual Calendar Support:** Seamlessly switch between Persian (Jalali) and Gregorian calendars using a single unified API (`DateInfo`).
*   **Time Manipulation:** Enhanced `TimeSpan` handling with rich formatting, mathematical operators, and string representations (`TimeInfo`).
*   **Smart Localization:** Built-in support for converting numbers to words (Persian/English) and digit conversion.
*   **Time Zone Helpers:** Easily convert time between UTC, Local, and specific time zones (e.g., Iran Standard Time).
*   **Date Periods:** Built-in methods to get Start/End of Weeks, Months, Quarters, and Years in both calendars.
*   **Fluent Interface:** Easy-to-use chainable methods for date navigation and transformation.

## 📦 Installation

Install the package via the .NET CLI:

```bash
dotnet add package Hs_Calender
```

Or via the NuGet Package Manager Console:

```powershell
Install-Package Hs_Calender
```

## 🚀 Quick Start

### DateInfo

The `DateInfo` class allows you to access date properties in both Persian and Gregorian systems instantly.

```csharp
using Hs_Calender;

// Initialize with current date, or specific DateTime
var date = new DateInfo(); 

// Access Persian Properties
Console.WriteLine(date.PersianDate);        // Output: 1402/10/15
Console.WriteLine(date.MonthNameFa);        // Output: دی
Console.WriteLine(date.DayOfWeekFa);        // Output: پنج‌شنبه

// Access Gregorian Properties
Console.WriteLine(date.GregorianDate);      // Output: 2024/01/05
Console.WriteLine(date.DayOfWeekEn);        // Output: Thursday

// Start and End of Persian Month
var startOfPersianMonth = date.AtStartOfMonthFa();
var endOfPersianMonth   = date.AtEndOfMonthFa();

// Smart Text Output
Console.WriteLine(date.PersianFullText);    // Output: پنج‌شنبه 15 دی 1402
```

### TimeInfo (Enhanced TimeSpan)

The `TimeInfo` class wraps `TimeSpan` to provide readable formats and easier calculations.

```csharp
using Hs_Calender;

var ts = new TimeSpan(2, 30, 45); // 2 hours, 30 mins, 45 secs
var time = new TimeInfo(ts);

// Various Formats
Console.WriteLine(time.HHmmss);           // Output: 02:30:45
Console.WriteLine(time.Clock24WithMs);    // Output: 02:30:45.000
Console.WriteLine(time.PersianReadable);  // Output: 0 روز، 2 ساعت، 30 دقیقه، 45 ثانیه

// Safe Components
Console.WriteLine(time.AbsHours);         // Output: 2

// Math Operations
var time2 = new TimeInfo(TimeSpan.FromHours(1));
var totalTime = time + time2;             // Operator overloading supported
Console.WriteLine(totalTime.HHmmss);      // Output: 03:30:45
```

## 📖 Usage Examples

### Time Zone Conversion

Convert times easily between different zones:

```csharp
var time = new TimeInfo();

// Get current time in Iran
var iranTime = time.NowInTimeZone("Iran Standard Time");
Console.WriteLine(iranTime.ToString("HH:mm"));

// Convert UTC to specific zone
var converted = time.UtcToTimeZone(DateTime.UtcNow, "Azerbaijan Standard Time");
```

### Date Navigation

Navigate through dates using the fluent `At...` methods:

```csharp
var date = new DateInfo();

// Navigate to start of Persian Year
var noruz = date.AtStartOfYearFa(); 

// Navigate to end of Gregorian Quarter
var endOfQ = date.AtEndOfQuarterEn();

// Get previous/next months
var nextMonth = date.AtNextMonthFa();
```

### Number to Words

Convert date components to words for reports or invoices:

```csharp
var date = new DateInfo(new DateTime(2024, 1, 1));

Console.WriteLine(date.YearFaText);   // Output: هزار و چهارصد و چهار
Console.WriteLine(date.DayFaText);    // Output: یازده
```

### Utility Methods

Calculate age, business days, or differences:

```csharp
var birthDate = new DateInfo(new DateTime(1990, 5, 20));
var age = birthDate.Age(DateTime.Now); // Returns age in years

var diff = date.DiffDays(birthDate.Raw);
var businessDays = date.BusinessDays(DateTime.Now.AddDays(10));
```


## 🤝 Contributing

Contributions are welcome! If you find a bug or have a feature request, please open an issue on GitHub.

1. Fork the repository
2. Create your feature branch
3. Commit your changes
4. Push to the branch 
5. Open a Pull Request
