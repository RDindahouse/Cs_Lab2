using System;

public class Date
{
    // Private fields to store day, month, and year
    private int day;
    private int month;
    private int year;

    // Constructor to initialize the date
    public Date(int day, int month, int year)
    {
        if (!IsValidDate(day, month, year))
        {
            throw new ArgumentException("Invalid date.");
        }

        this.day = day;
        this.month = month;
        this.year = year;
    }

    // Method to check if a date is valid
    private bool IsValidDate(int day, int month, int year)
    {
        if (year < 1 || month < 1 || month > 12)
        {
            return false;
        }

        int[] daysInMonth = { 31, IsLeapYear(year) ? 29 : 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        return day >= 1 && day <= daysInMonth[month - 1];
    }

    // Method to check if a year is a leap year
    private bool IsLeapYear(int year)
    {
        return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
    }

    public static Date operator +(Date date, int days)
    {
        DateTime dateTime = new DateTime(date.year, date.month, date.day).AddDays(days);
        return new Date(dateTime.Day, dateTime.Month, dateTime.Year);
    }

    public static Date operator -(Date date, int days)
    {
        DateTime dateTime = new DateTime(date.year, date.month, date.day).AddDays(-days);
        return new Date(dateTime.Day, dateTime.Month, dateTime.Year);
    }

    public static bool operator ==(Date date1, Date date2)
    {
        return date1.year == date2.year && date1.month == date2.month && date1.day == date2.day;
    }

    public static bool operator !=(Date date1, Date date2)
    {
        return !(date1 == date2);
    }

    // Method to display the date in dd/mm/yyyy format
    public string ToShortDateString()
    {
        return $"{day:D2}/{month:D2}/{year:D4}";
    }

    // Method to display the date in "Month day, year" format
    public string ToLongDateString()
    {
        string monthName = new DateTime(year, month, day).ToString("MMMM");
        return $"{monthName} {day}, {year}";
    }

    // Method to display the date in "DayOfWeek, Month day, year" format
    public string ToFullDateString()
    {
        string dayOfWeek = new DateTime(year, month, day).ToString("dddd");
        string monthName = new DateTime(year, month, day).ToString("MMMM");
        return $"{dayOfWeek}, {monthName} {day}, {year}";
    }

    // Method to override ToString() method and display the date in dd/mm/yyyy format
    public override string ToString()
    {
        return ToShortDateString();
    }

    // Override Equals() method to compare dates
    public override bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] object obj)
{
    if (obj is Date otherDate)
    {
        return this == otherDate;
    }
    return false;
}

    // Override GetHashCode() method
    public override int GetHashCode()
    {
        return year.GetHashCode() ^ month.GetHashCode() ^ day.GetHashCode();
    }
}

class Program
{
    static void Main()
    {
        // Create a date object
        Date date1 = new Date(25, 6, 2024);

        // Add 10 days to the date
        Date date2 = date1 + 10;

        // Subtract 5 days from the date
        Date date3 = date1 - 5;

        // Compare dates for equality
        Console.WriteLine($"Date 1: {date1.ToShortDateString()}");
        Console.WriteLine($"Date 2: {date2.ToShortDateString()}");
        Console.WriteLine($"Date 3: {date3.ToShortDateString()}");
        Console.WriteLine($"Date 1 and Date 2 are {(date1 == date2 ? "equal" : "not equal")}");
        Console.WriteLine($"Date 1 and Date 3 are {(date1 == date3 ? "equal" : "not equal")}");
    }
}