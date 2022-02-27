public class Date
{
    int year;
    int month;
    int quarter;

    public Date(int y, int m, int q)
    {
        if (y <= 0 || m <= 0 || q <= 0)
        {
            return;
        }
        year = y;
        month = m;
        quarter = q;
        ClearingDate();
    }

    void ClearingDate()
    {
        while (quarter > 4)
        {
            quarter -= 4;
            month++;
        }
        while (month > 12)
        {
            month -= 12;
            year++;
        }
        while (quarter <= 0)
        {
            month--;
            quarter += 4;
        }
        while (month <= 0)
        {
            year--;
            month = 12;
        }
    }

    public static Date operator ++(Date date)
    {
        date.quarter++;
        date.ClearingDate();
        return date;
    }
    public static Date operator --(Date date)
    {
        date.quarter--;
        date.ClearingDate();
        return date;
    }

    public static Date operator +(Date date, int i)
    {
        date.quarter += i;
        date.ClearingDate();
        return date;
    }

    public static Date operator -(Date date, int i)
    {
        date.quarter -= i;
        date.ClearingDate();
        return date;
    }

    public static bool operator ==(Date date, Date dDay)
    {
        date.ClearingDate();
        dDay.ClearingDate();
        if(date.year == dDay.year && date.month == dDay.month && date.quarter == dDay.quarter)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool operator !=(Date date, Date dDay)
    {
        date.ClearingDate();
        dDay.ClearingDate();
        if (date.year == dDay.year && date.month == dDay.month && date.quarter == dDay.quarter)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public override int GetHashCode()
    {
        return (year - 1) * 48 + (month - 1) * 4 + quarter;
    }

    public override bool Equals(object obj)
    {
        if (obj is Date date)
        {
            return this == date;
        }
        else
        {
            return false;
        }
    }

    public override string ToString()
    {
        return year + "�� " + month + "�� " + quarter + "�б�";
    }
}
