using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hs_Calender;

using System;
using System.Text;

public static class NumberToWordsConverter
{
    // ================= ENGLISH =================
    public static string NumberToEnglishWords(int number)
    {
        if (number == 0) return "zero";
        if (number < 0) return "minus " + NumberToEnglishWords(Math.Abs(number));

        string[] units =
        {
        "", "one", "two", "three", "four", "five", "six", "seven",
        "eight", "nine", "ten", "eleven", "twelve", "thirteen",
        "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen"
    };

        string[] tens =
        {
        "", "", "twenty", "thirty", "forty", "fifty",
        "sixty", "seventy", "eighty", "ninety"
    };

        var sb = new StringBuilder();

        if (number >= 1000000000)
        {
            sb.Append(NumberToEnglishWords(number / 1000000000) + " billion ");
            number %= 1000000000;
        }

        if (number >= 1000000)
        {
            sb.Append(NumberToEnglishWords(number / 1000000) + " million ");
            number %= 1000000;
        }

        if (number >= 1000)
        {
            sb.Append(NumberToEnglishWords(number / 1000) + " thousand ");
            number %= 1000;
        }

        if (number >= 100)
        {
            sb.Append(NumberToEnglishWords(number / 100) + " hundred ");
            number %= 100;
        }

        if (number > 0)
        {
            if (sb.Length > 0) sb.Append("and ");

            if (number < 20)
                sb.Append(units[number]);
            else
            {
                sb.Append(tens[number / 10]);
                if ((number % 10) > 0)
                    sb.Append("-" + units[number % 10]);
            }
        }

        return sb.ToString().Trim();
    }

    // ================= PERSIAN =================
    public static string NumberToPersianWords(int number)
    {
        if (number == 0) return "صفر";
        if (number < 0) return "منفی " + NumberToPersianWords(Math.Abs(number));

        string[] units =
        {
        "", "یک", "دو", "سه", "چهار", "پنج", "شش", "هفت",
        "هشت", "نه", "ده", "یازده", "دوازده", "سیزده",
        "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده"
    };

        string[] tens =
        {
        "", "", "بیست", "سی", "چهل", "پنجاه",
        "شصت", "هفتاد", "هشتاد", "نود"
    };

        StringBuilder sb = new StringBuilder();

        if (number >= 1000000000)
        {
            sb.Append(NumberToPersianWords(number / 1000000000) + " میلیارد ");
            number %= 1000000000;
        }

        if (number >= 1000000)
        {
            sb.Append(NumberToPersianWords(number / 1000000) + " میلیون ");
            number %= 1000000;
        }

        if (number >= 1000)
        {
            sb.Append(NumberToPersianWords(number / 1000) + " هزار ");
            number %= 1000;
        }

        if (number >= 100)
        {
            sb.Append(NumberToPersianWords(number / 100) + " صد ");
            number %= 100;
        }

        if (number > 0)
        {
            if (sb.Length > 0) sb.Append(" و ");

            if (number < 20)
                sb.Append(units[number]);
            else
            {
                sb.Append(tens[number / 10]);
                if ((number % 10) > 0)
                    sb.Append(" و " + units[number % 10]);
            }
        }

        return sb.ToString().Trim();
    }
}
