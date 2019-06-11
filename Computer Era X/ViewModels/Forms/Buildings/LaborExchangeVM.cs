
using Computer_Era_X.Models;
using Computer_Era_X.Properties;
using Computer_Era_X.Views;
using Prism.Commands;
using System.Windows.Controls;

namespace Computer_Era_X.ViewModels
{
    partial class GameVM
    {
        private LaborExchange model = new LaborExchange(); 

        partial void LaborExchangeInit()
        {
            WillQuit = new DelegateCommand(LayOff);
        }

        private void LayOff()
        {
            GameEvent @event = GameEnvironment.Events.Events.Find(e => e.Name == "job");
            GameEnvironment.Events.Events.Remove(@event);

            double amount = 0;
            if (GameEnvironment.Player.Job.DateEmployment < GameEnvironment.Events.Timer.DateTime)
            {
                if (GameEnvironment.Player.Job.DateEmployment.Month == GameEnvironment.Events.Timer.DateTime.Month & GameEnvironment.Player.Job.DateEmployment.Year == GameEnvironment.Events.Timer.DateTime.Year)
                {
                    amount = GameEnvironment.Events.Timer.DateTime.Day - GameEnvironment.Player.Job.DateEmployment.Day;
                } else {
                    amount = GameEnvironment.Events.Timer.DateTime.Day;
                }
                amount *= (GameEnvironment.Player.Job.Salary * GameEnvironment.Player.Money[0].Course);
            }

            string Company = GameEnvironment.Player.Job.CompanyName;
            GameEnvironment.Player.Job = null;

            GameEnvironment.Player.Money[0].TopUp(Resources.Dismissal, string.Format(Resources.CompanyX, Company), GameEnvironment.Events.Timer.DateTime, amount);
            MessageBox.Show(string.Format(Resources.GameMessage16, amount, GameEnvironment.Player.Money[0].Abbreviation));
        }

        private ContentControl _boardWithVacancies;
        public bool RetirementOpportunity => GameEnvironment.Player.Job != null ? true : false;

        public ContentControl BoardWithVacancies
        {
            get => _boardWithVacancies;
            set => SetProperty(ref _boardWithVacancies, value);
        }

        public DelegateCommand WillQuit { get; set; }
    }
}
