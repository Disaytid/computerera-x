namespace Computer_Era_X.Views
{
    public partial class Form
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
