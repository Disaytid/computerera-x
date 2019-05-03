using System;
using System.Collections.ObjectModel;
using Computer_Era_X.DataTypes.Enums;
using Computer_Era_X.Models;
using Computer_Era_X.Properties;
using Computer_Era_X.Properties;

namespace Computer_Era_X.DataTypes.Objects.Computer
{
    public class CaseProperties
    {
        public CaseTypes CaseType; 
        public Collection<MotherboardTypes> FormFactor = new Collection<MotherboardTypes>();
        public Collection<PSUTypes> FormFactorPSU = new Collection<PSUTypes>();
        public int CoolerHeight;    //Maximum height of the cooler on the processor to the lid in millimeters
        public int VideocardLength; //Maximum length of the video card to the cover in millimeters
        // ReSharper disable once InconsistentNaming
        public int Sections3_5;
        // ReSharper disable once InconsistentNaming
        public int Sections2_5;
        public int ExpansionSlots;
        public int BuiltinFans;    //The number of built-in fans
        public int PlacesFans;      //Free places for installation of fans
        public bool LiquidCooling;
        // ReSharper disable once InconsistentNaming
        public int USB2_0;
        // ReSharper disable once InconsistentNaming
        public int USB3_0;
        public bool HeadphoneJack;
        public bool MicrophoneJack; //Presence of microphone jack
    }
    public class Case : Item<CaseProperties>
    {
        public Case(int uid, string name, string type, int price, DateTime manDate, CaseProperties properties) : base(uid, name, type, price, manDate, properties) { }
        public Case(Item item) : base(item) { }
        public override string Info()
        {
            string info = Resources.Name + " " + Name + Environment.NewLine;
            info += Resources.TypeСase + ": " + Properties.CaseType + Environment.NewLine;
            info += Resources.FormFactor + ": ";
            for (var i = 0; Properties.FormFactor.Count > i; i++)
            { if (i == 0) { info += Properties.FormFactor[i]; } else { info += ",  " + Properties.FormFactor[i]; } }
            info += Environment.NewLine;
            info += Resources.Sections + " 3.5: " + Properties.Sections3_5 + Environment.NewLine;
            info += Resources.Sections + " 2.5: " + Properties.Sections2_5 + Environment.NewLine;
            info += Resources.BuiltInFans + ": " + Properties.BuiltinFans + Environment.NewLine;
            info += Resources.PlacesForFans + ": " + Properties.PlacesFans + Environment.NewLine;
            info += Resources.LiquidCoolingSupport + ": " + (Properties.LiquidCooling ? Resources.Yes : Resources.No) + Environment.NewLine;
            info += "USB "+ Resources.Nests.ToLower() + " 2.0: " + Properties.USB2_0 + Environment.NewLine;
            info += "USB " + Resources.Nests.ToLower() + " 3.0: " + Properties.USB3_0 + Environment.NewLine;
            info += Resources.HeadphoneJack + ": " + (Properties.HeadphoneJack ? Resources.Yes : Resources.No) + Environment.NewLine;
            info += Resources.MicrophoneJack + ": " + (Properties.MicrophoneJack ? Resources.Yes : Resources.No);
            return info;
        }

        public int GetCountCompatiblePlaces(HDDFormFactor formFactor)
        {
            switch (formFactor)
            {
                case HDDFormFactor.ThreeFive:
                    return Properties.Sections3_5;
                case HDDFormFactor.TwoFive:
                    return Properties.Sections2_5;
                default:
                    return 0;
            }
        }
    }
}
