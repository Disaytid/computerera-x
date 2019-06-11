using Computer_Era_X.DataTypes.Objects;
using Computer_Era_X.Properties;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace Computer_Era_X.Models
{
    public class LaborExchangeModel : BindableBase
    {
    }

    public class JobCard
    {
        public int Id { get; }
        public string Name { get; }
        public string CompanyName { get; }
        public double Salary { get; }
        public double Complexity { get; } //Limit from 0 to 1
        public DateTime FromTime { get; }
        public DateTime ToTime { get; }
        public DateTime DateEmployment { get; set; }
        public SolidColorBrush StickerColor { get; }

        public JobCard(Profession profession, List<Company> company, int id, Random rnd)
        {
            Id = id;
            Name = profession.Name;

            //Company name
            List<Company> current_company = company;
            if (current_company.Count > 0)
            {
                int companyId = rnd.Next(0, current_company.Count);
                CompanyName = current_company[companyId].Name;
            } else { CompanyName = Resources.AtTheDevilOnTheHorns; }

            //Salary
            int floatSalary = Convert.ToInt32(profession.Salary * 10 / 100);
            Salary = profession.Salary + rnd.Next(-floatSalary, floatSalary);
            Complexity = profession.Complexity;

            //Work period
            switch (profession.DayPeriod)
            {
                case 1:
                    FromTime = new DateTime(1000, 1, 1, 8, 0, 0);
                    ToTime = FromTime.AddHours(profession.WorkingHours);
                    break;
                case 2:
                    FromTime = new DateTime(1000, 1, 1, 14, 0, 0);
                    ToTime = FromTime.AddHours(profession.WorkingHours);
                    break;
                case 3:
                    FromTime = new DateTime(1000, 1, 1, 20, 0, 0);
                    ToTime = FromTime.AddHours(profession.WorkingHours);
                    break;
                case 4:
                    FromTime = new DateTime(1000, 1, 1, 2, 0, 0);
                    ToTime = FromTime.AddHours(profession.WorkingHours);
                    break;
                default:
                    int case_id = rnd.Next(1, 4);
                    if (case_id == 1) { goto case 1; }
                    else if (case_id == 2) { goto case 2; }
                    else if (case_id == 3) { goto case 3; }
                    else { goto case 4; }
            }

            //Sticker color
            switch (rnd.Next(1, 5))
            {
                case 1:
                    StickerColor = new SolidColorBrush(Color.FromRgb(255, 224, 175));
                    break;
                case 2:
                    StickerColor = new SolidColorBrush(Color.FromRgb(252, 136, 252));
                    break;
                case 3:
                    StickerColor = new SolidColorBrush(Color.FromRgb(184, 233, 134));
                    break;
                case 4:
                    StickerColor = new SolidColorBrush(Color.FromRgb(72, 186, 255));
                    break;
                default:
                    StickerColor = new SolidColorBrush(Colors.White);
                    break;
            }
        }
    }
}
