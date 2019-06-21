using System.ComponentModel.DataAnnotations;

namespace Computer_Era_X.DataTypes.Objects
{
    public class Profession
    {
        public int ID { get; set; }
        public string SystemName { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public double Complexity { get; set; }
        public int WorkingHours { get; set; }
        public int DayPeriod { get; set; }
    }
}
