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
}
