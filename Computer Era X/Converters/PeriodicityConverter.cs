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
    }
}
