using System.Windows.Controls;
using System.Windows;

namespace Computer_Era_X.Views.Forms
{
    public partial class Form : UserControl
    {
        private string _title;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                FormTitle.Text = Title;
            }
        }
        public Form()
        {
            InitializeComponent();
        }
    }
}
