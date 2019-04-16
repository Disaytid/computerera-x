using Computer_Era_X.DataTypes.Enums;
using Computer_Era_X.Models;
using System;
using System.Collections.Generic;

namespace Computer_Era_X.DataTypes.Interfaces
{
    public interface IScenario
    {
        string Name { get; set; }
        List<Setting> Settings { get; set; }
        void Start(GameEnvironment gameEnvironment);
        void GameOver(string cause);
    }

    public class Setting
    {
        public string Name { get; set; }
        public TypeSettingsData Type { get; set; }
        public string Value { get; set; }
        public string[] Values { get; set; }

        public Setting(string name, TypeSettingsData type, string value)
        {
            if (type == TypeSettingsData.List)
            { throw new Exception("Scenario set setting: Invalid type (" + type.ToString() + ")" + " for this constructor"); }

            if (type == TypeSettingsData.Bool && (Convert.ToInt32(Value) != 0 & Convert.ToInt32(Value) != 1))
            { throw new Exception("Scenario set setting: Value not boolean"); }

            if (type == TypeSettingsData.Integer && !int.TryParse(value, out _))
            { throw new Exception("Scenario set setting: Value not number"); }

            if (type == TypeSettingsData.Double && !double.TryParse(value, out _))
            { throw new Exception("Scenario set setting: Value not double"); }

            Name = name;
            Type = type;
            Value = value;
        }
        public Setting(string name, TypeSettingsData type, string[] values)
        {
            if (type != TypeSettingsData.List) { throw new Exception("Scenario set setting: Invalid type (" + type.ToString() + ")" + " for this constructor"); }

            Name = name;
            Type = type;
            Values = values;
        }
    }
}
