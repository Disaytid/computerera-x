using System.Collections.Generic;
using Computer_Era_X.DataTypes.Enums;

namespace Computer_Era_X.DataTypes.Dictionaries
{
    public static class DItems
    {
        public static readonly Dictionary<ItemTypes, string> ItemIcon = new Dictionary<ItemTypes, string>
        {
            { ItemTypes.Case, "pack://application:,,,/Resources/coffin.png" },
            { ItemTypes.Motherboard, "pack://application:,,,/Resources/circuitry.png" },
            { ItemTypes.PSU, "pack://application:,,,/Resources/plug.png" },
            { ItemTypes.RAM, "pack://application:,,,/Resources/brain.png" },
            { ItemTypes.CPU, "pack://application:,,,/Resources/processor.png" },
            { ItemTypes.CPUCooler, "pack://application:,,,/Resources/computer-fan.png" },
            { ItemTypes.HDD, "pack://application:,,,/Resources/stone-tablet.png" },
            { ItemTypes.VideoCard, "pack://application:,,,/Resources/cyber-eye.png" },
            { ItemTypes.Monitor, "pack://application:,,,/Resources/tv.png" },
            { ItemTypes.OpticalDrive, "pack://application:,,,/Resources/compact-disc.png" },
            { ItemTypes.Mouse, "pack://application:,,,/Resources/mouse.png" },
            { ItemTypes.Keyboard, "pack://application:,,,/Resources/keyboard.png" },
            { ItemTypes.OpticalDisc, "pack://application:,,,/Resources/compact-disc.png" },
            { ItemTypes.OS, "pack://application:,,,/Resources/compact-disc.png" },
            { ItemTypes.Program, "pack://application:,,,/Resources/compact-disc.png" },
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
