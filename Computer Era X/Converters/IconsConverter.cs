using Computer_Era_X.DataTypes.Enums;

namespace Computer_Era_X.Converters
{
    public static class IconsConverter
    {
        public static string GetIconPath(Icon icon)
        {
            switch (icon)
            {
                case Icon.Money:
                    return "coin.png";
                default:
                    return "info.png";
            }
        }
    }
}
