using Computer_Era_X.Converters;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EIcon = Computer_Era_X.DataTypes.Enums.Icon;

namespace Computer_Era_X.Models
{
    public class Message
    {
        public ImageSource Icon { get; private set; }
        public string Title { get; private set; }
        public string Text { get; private set; }

        public Message(string title, string text, EIcon icon)
        {
            Icon = new BitmapImage(new Uri("pack://application:,,,/Assets/Icons/" + IconsConverter.GetIconPath(icon)));
            Title = title;
            Text = text;
        }
    }
}
