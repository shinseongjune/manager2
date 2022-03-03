public class Date
{
    int year;
    int month;
    int quarter;

    public int Year => year;
    public int Month => month;
    public int Quarter => quarter;

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

    public Date(Date date)
    {
        year = date.year;
        month = date.month;
        quarter = date.quarter;
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
        if (year < 0) throw new MinusYearException();
    }

    public static Date operator ++(Date date)
    {
        Date d = new(date);
        d.quarter++;
        d.ClearingDate();
        return d;
    }
    public static Date operator --(Date date)
    {
        Date d = new(date);
        d.quarter--;
        d.ClearingDate();
        return d;
    }

    public static Date operator +(Date date, int i)
    {
        Date d = new(date);
        d.quarter += i;
        d.ClearingDate();
        return d;
    }

    public static Date operator -(Date date, int i)
    {
        Date d = new(date);
        d.quarter -= i;
        d.ClearingDate();
        return d;
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

    public static bool operator <(Date date1, Date date2)
    {
        date1.ClearingDate();
        date2.ClearingDate();
        return date1.GetHashCode() < date2.GetHashCode();
    }

    public static bool operator >(Date date1, Date date2)
    {
        date1.ClearingDate();
        date2.ClearingDate();
        return date1.GetHashCode() > date2.GetHashCode();
    }

    public static bool operator <=(Date date1, Date date2)
    {
        date1.ClearingDate();
        date2.ClearingDate();
        return date1.GetHashCode() <= date2.GetHashCode();
    }

    public static bool operator >=(Date date1, Date date2)
    {
        date1.ClearingDate();
        date2.ClearingDate();
        return date1.GetHashCode() >= date2.GetHashCode();
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
        return year + "년" + " " + month + "월" + " " + quarter + "분기";
    }
}
