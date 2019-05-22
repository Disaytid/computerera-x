using Computer_Era_X.DataTypes.Enums;
using Computer_Era_X.DataTypes.Objects.Computer;

namespace Computer_Era_X.DataTypes.Objects
{
    public class Size // In millimeters
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Depth { get; set; }


        /// <param name="width">Width in millimeters</param>
        /// <param name="height">Height in millimeters</param>
        /// <param name="depth">Depth in millimeters</param>
        public Size(int width, int height, int depth)
        {
            Width = width;
            Height = height;
            Depth = depth;
        }
    }
    public class Resolution
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Resolution(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
    public class Partition
    {
        public string Name { get; set; }
        public int PartitionNumber { get; set; }
        public string Letter { get; set; }
        public int Volume { get; set; } // In kilobytes
        public FileSystem FileSystem { get; set; }
        public OperatingSystem OperatingSystem { get; set; }
    }
}
