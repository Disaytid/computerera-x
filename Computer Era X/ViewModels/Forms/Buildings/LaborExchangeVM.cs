
using Computer_Era_X.Views;
using System.Windows.Controls;

namespace Computer_Era_X.ViewModels
{
    partial class GameVM
    {
        private LaborExchange model = new LaborExchange(); 

        partial void LaborExchangeInit()
        {

        }

        private ContentControl _boardWithVacancies;

        public ContentControl BoardWithVacancies
        {
            get => _boardWithVacancies;
            set => SetProperty(ref _boardWithVacancies, value);
        }
    }
}
