using System;
using System.Windows;
using System.Windows.Controls;
using Computer_Era_X.Models;

namespace Computer_Era_X.Views
{
    public partial class Map
    {
        public Map()
        {
            InitializeComponent();
            var mapReader = new MapReader(this);
            Browser.ObjectForScripting = mapReader;
        }
        public void TransitionProcessing(string obj)
        {
            Browser.Tag = obj;
        }
    }
    public class WebBrowserHelper
    {
        public static readonly DependencyProperty BodyProperty =
            DependencyProperty.RegisterAttached("Body", typeof(string), typeof(WebBrowserHelper), new PropertyMetadata(OnBodyChanged));

        public static string GetBody(DependencyObject dependencyObject)
        {
            return (string)dependencyObject.GetValue(BodyProperty);
        }

        public static void SetBody(DependencyObject dependencyObject, string body)
        {
            dependencyObject.SetValue(BodyProperty, body);
        }

        private static void OnBodyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var webBrowser = (WebBrowser)d;
            webBrowser.Navigate((string)e.NewValue);
        }
    }
}
