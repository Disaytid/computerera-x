using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Computer_Era_X.Converters;
using EIcon = Computer_Era_X.DataTypes.Enums.Icon;

namespace Computer_Era_X.Models.Systems
{
    public class Message
    {
        public ImageSource Icon { get; }
        public string Title { get; }
        public string Text { get; }

        public Message(string title, string text, EIcon icon)
        {
            Icon = new BitmapImage(new Uri("pack://application:,,,/Assets/Icons/" + IconsConverter.GetIconPath(icon)));
            Title = title;
            Text = text;
        }
    }
}
