using Computer_Era_X.DataTypes.Enums;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Computer_Era_X.Views
{
    public partial class MessageBox
    {
        public MessageBox()
        {
            InitializeComponent();
        }

        static MessageBox _messageBox;
        static MessageBoxResult _result = MessageBoxResult.No;
        public static MessageBoxResult Show
       (string caption, string msg, MessageBoxType type)
        {
            switch (type)
            {
                case MessageBoxType.ConfirmationWithYesNo:
                    return Show(caption, msg, MessageBoxButton.YesNo,
                    DataTypes.Enums.MessageBoxImage.Question);
                case MessageBoxType.Information:
                    return Show(caption, msg, MessageBoxButton.OK,
                    DataTypes.Enums.MessageBoxImage.Information);
                case MessageBoxType.Error:
                    return Show(caption, msg, MessageBoxButton.OK,
                    DataTypes.Enums.MessageBoxImage.Error);
                case MessageBoxType.Warning:
                    return Show(caption, msg, MessageBoxButton.OK,
                    DataTypes.Enums.MessageBoxImage.Warning);
                default:
                    return MessageBoxResult.No;
            }
        }

        public static MessageBoxResult Show(string msg, MessageBoxType type)
        {
            return Show(string.Empty, msg, type);
        }
        public static MessageBoxResult Show(string msg)
        {
            return Show(string.Empty, msg,
            MessageBoxButton.OK, DataTypes.Enums.MessageBoxImage.None);
        }
        public static MessageBoxResult Show
        (string caption, string text)
        {
            return Show(caption, text,
            MessageBoxButton.OK, DataTypes.Enums.MessageBoxImage.None);
        }
        public static MessageBoxResult Show
        (string caption, string text, MessageBoxButton button)
        {
            return Show(caption, text, button,
            DataTypes.Enums.MessageBoxImage.None);
        }
        public static MessageBoxResult Show
        (string caption, string text,
        MessageBoxButton button, DataTypes.Enums.MessageBoxImage image)
        {
            _messageBox = new MessageBox
            { Text = { Text = text }, Title = { Content = caption } };
            SetVisibilityOfButtons(button);
            SetImageOfMessageBox(image);
            _messageBox.ShowDialog();
            return _result;
        }

        private static void SetVisibilityOfButtons(MessageBoxButton button)
        {
            switch (button)
            {
                case MessageBoxButton.OK:
                    _messageBox.ButtonNo.Visibility = Visibility.Collapsed;
                    _messageBox.ButtonYes.Visibility = Visibility.Collapsed;
                    _messageBox.ButtonOk.Focus();
                    break;
                case MessageBoxButton.YesNo:
                    _messageBox.ButtonOk.Visibility = Visibility.Collapsed;
                    _messageBox.ButtonNo.Focus();
                    break;
                case MessageBoxButton.OKCancel:
                    _messageBox.ButtonNo.Visibility = Visibility.Collapsed;
                    _messageBox.ButtonYes.Visibility = Visibility.Collapsed;
                    _messageBox.TextInput.Visibility = Visibility.Visible;
                    _messageBox.ButtonOk.Focus();
                    break;
            }
        }

        private static void SetImageOfMessageBox(DataTypes.Enums.MessageBoxImage image)
        {
            switch (image)
            {
                case DataTypes.Enums.MessageBoxImage.Warning:
                    _messageBox.SetImage("warning.png");
                    break;
                case DataTypes.Enums.MessageBoxImage.Question:
                    _messageBox.SetImage("question.png");
                    break;
                case DataTypes.Enums.MessageBoxImage.Information:
                    _messageBox.SetImage("information.png");
                    break;
                case DataTypes.Enums.MessageBoxImage.Error:
                    _messageBox.SetImage("error.png");
                    break;
                default:
                    _messageBox.Icon.Visibility = Visibility.Collapsed;
                    break;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Equals(sender, ButtonOk))
                _result = MessageBoxResult.OK;
            else if (Equals(sender, ButtonYes))
                _result = MessageBoxResult.Yes;
            else if (Equals(sender, ButtonNo))
                _result = MessageBoxResult.No;
            else
                _result = MessageBoxResult.None;
            _messageBox.Close();
            _messageBox = null;
        }
        private void SetImage(string imageName)
        {
            string uri = $"/Assets/Icons/{imageName}";
            var uriSource = new Uri(uri, UriKind.Relative);
            Icon.Source = new BitmapImage(uriSource);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
