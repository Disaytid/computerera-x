using System.Collections.Generic;
using Computer_Era_X.DataTypes.Enums;

namespace Computer_Era_X.DataTypes.Dictionaries
{
    public static class DItems
    {
        public static readonly Dictionary<ItemTypes, string> ItemIcon = new Dictionary<ItemTypes, string>
        {
            { ItemTypes.Case, "pack://application:,,,/Assets/Icons/coffin.png" },
            { ItemTypes.Motherboard, "pack://application:,,,/Assets/Icons/circuitry.png" },
            { ItemTypes.PSU, "pack://application:,,,/Assets/Icons/plug.png" },
            { ItemTypes.RAM, "pack://application:,,,/Assets/Icons/brain.png" },
            { ItemTypes.CPU, "pack://application:,,,/Assets/Icons/rocessor.png" },
            { ItemTypes.CPUCooler, "pack://application:,,,/Assets/Icons/computer-fan.png" },
            { ItemTypes.HDD, "pack://application:,,,/Assets/Icons/stone-tablet.png" },
            { ItemTypes.VideoCard, "pack://application:,,,/Assets/Icons/cyber-eye.png" },
            { ItemTypes.Monitor, "pack://application:,,,/Assets/Icons/tv.png" },
            { ItemTypes.OpticalDrive, "pack://application:,,,/Assets/Icons/compact-disc.png" },
            { ItemTypes.Mouse, "pack://application:,,,/Assets/Icons/mouse.png" },
            { ItemTypes.Keyboard, "pack://application:,,,/Assets/Icons/keyboard.png" },
            { ItemTypes.OpticalDisc, "pack://application:,,,/Assets/Icons/compact-disc.png" },
            { ItemTypes.OS, "pack://application:,,,/Assets/Icons/compact-disc.png" },
            { ItemTypes.Program, "pack://application:,,,/Assets/Icons/compact-disc.png" },
        };

        public static readonly Dictionary<ItemTypes, string> LocalizedItemTypes = new Dictionary<ItemTypes, string>
        {
            { ItemTypes.Case, Properties.Resources.Case },
            { ItemTypes.Motherboard, Properties.Resources.Motherboard },
            { ItemTypes.RAM, Properties.Resources.RAM },
            { ItemTypes.PSU, Properties.Resources.PSU },
            { ItemTypes.CPU, Properties.Resources.CPU },
            { ItemTypes.CPUCooler, Properties.Resources.CPUCooler },
            { ItemTypes.HDD, Properties.Resources.HDD },
            { ItemTypes.Monitor, Properties.Resources.Monitor },
            { ItemTypes.VideoCard, Properties.Resources.VideoCard },
            { ItemTypes.OpticalDrive, Properties.Resources.OpticalDrive },
            { ItemTypes.Mouse, Properties.Resources.Mouse },
            { ItemTypes.Keyboard, Properties.Resources.Keyboard },
            { ItemTypes.OpticalDisc, Properties.Resources.OpticalDisc },
            { ItemTypes.Program, Properties.Resources.Program },
        };
    }
}
