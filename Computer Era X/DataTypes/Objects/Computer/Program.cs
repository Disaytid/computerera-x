using Computer_Era_X.Models;
using System;

namespace Computer_Era_X.DataTypes.Objects.Computer
{
    public class ProgramProperties
    {
        public string IconName { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string ControlName { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }

    public class Program : Item<ProgramProperties>
    {
        public Program(int uid, string name, string type, double price, DateTime man_date, ProgramProperties properties) : base(uid, name, type, price, man_date, properties) { }
        public Program(Item item) : base(item) { }

        public override string Info()
        {
            return Properties.Description;
        }
    }
}
