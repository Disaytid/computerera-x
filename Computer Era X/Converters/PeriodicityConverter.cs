using System;
using Computer_Era_X.DataTypes.Enums;
using Computer_Era_X.Properties;

namespace Computer_Era_X.Converters
{
    public static class PeriodicityConverter
    {
        public static string ToLocalizedString(Periodicity periodicity)
        {
            switch (periodicity)
            {
                case Periodicity.Year:
                    return Resources.Year;
                case Periodicity.Month:
                    return Resources.Month;
                case Periodicity.Week:
                    return Resources.Week;
                case Periodicity.Day:
                    return Resources.Day;
                case Periodicity.Hour:
                    return Resources.Hour;
                case Periodicity.Minute:
                    return Resources.Minute;
                default:
                    return periodicity.ToString();
            }
        }

        public static DateTime GetDateTimeFromPeriodicity(DateTime dateTime, Periodicity periodicity, int periodicityValue)
        {
            DateTime newDateTime;
            switch (periodicity)
            {
                case Periodicity.Minute:
                    newDateTime = dateTime.AddMinutes(periodicityValue);
                    break;
                case Periodicity.Hour:
                    newDateTime = dateTime.AddHours(periodicityValue);
                    break;
                case Periodicity.Day:
                    newDateTime = dateTime.AddDays(periodicityValue);
                    break;
                case Periodicity.Week:
                    newDateTime = dateTime.AddDays(periodicityValue * 7);
                    break;
                case Periodicity.Month:
                    newDateTime = dateTime.AddMonths(periodicityValue);
                    break;
                case Periodicity.Year:
                    newDateTime = dateTime.AddYears(periodicityValue);
                    break;
                default:
                    newDateTime = dateTime;
                    break;
            }
            return newDateTime;
        }

        public static DateTime GetDateTimeFromPeriodicity(DateTime dateTime, int periodicity, int periodicityValue)
        {
            Periodicity _periodicity = (Periodicity)Enum.GetValues(typeof(Periodicity)).GetValue(periodicity);
            return GetDateTimeFromPeriodicity(dateTime, _periodicity, periodicityValue);
        }

        public static DateTime GetDateByPeriod(DateTime dateTime, Periodicity periodicity, int value)
        {
            if (periodicity == Periodicity.Year) { return dateTime.AddYears(value); }
            else if (periodicity == Periodicity.Month) { return dateTime.AddMonths(value); }
            else if (periodicity == Periodicity.Week) { return dateTime.AddDays(7 * value); }
            else if (periodicity == Periodicity.Day) { return dateTime.AddDays(value); }
            else if (periodicity == Periodicity.Hour) { return dateTime.AddHours(value); }
            else if (periodicity == Periodicity.Minute) { return dateTime.AddMinutes(value); }
            else { return dateTime; }
        }

        public static DateTime GetDateByPeriod(DateTime dateTime, int periodicity, int value)
        {
            Periodicity _periodicity = (Periodicity)Enum.GetValues(typeof(Periodicity)).GetValue(periodicity);
            return GetDateByPeriod(dateTime, _periodicity, value);
        }

        public static int GetNumberOfPeriods(Periodicity periodicity, int periodicity_value, DateTime startDateTime, DateTime endDateTime)
        {
            if (periodicity == Periodicity.Year) { return (endDateTime.Year - startDateTime.Year) / periodicity_value; }
            else if (periodicity == Periodicity.Month) { return ((endDateTime.Month - startDateTime.Month) + 12 * (endDateTime.Year - startDateTime.Year)) / periodicity_value; }
            else if (periodicity == Periodicity.Week) { return (Convert.ToInt32((endDateTime - startDateTime).TotalDays) / 7) / periodicity_value; }
            else if (periodicity == Periodicity.Day) { return Convert.ToInt32((endDateTime - startDateTime).TotalDays) / periodicity_value; }
            else if (periodicity == Periodicity.Hour) { return Convert.ToInt32((endDateTime - startDateTime).TotalHours) / periodicity_value; }
            else if (periodicity == Periodicity.Minute) { return Convert.ToInt32((endDateTime - startDateTime).TotalMinutes) / periodicity_value; }
            else { return 0; }
        }

        public static int GetNumberOfPeriods(int periodicity, int periodicity_value, DateTime startDateTime, DateTime endDateTime)
        {
            Periodicity _periodicity = (Periodicity)Enum.GetValues(typeof(Periodicity)).GetValue(periodicity);
            return GetNumberOfPeriods(_periodicity, periodicity_value, startDateTime, endDateTime);
        }
    }
}
