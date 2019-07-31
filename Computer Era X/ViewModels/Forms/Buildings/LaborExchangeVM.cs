using Computer_Era_X.DataTypes.Objects;
using Computer_Era_X.Models;
using Computer_Era_X.Properties;
using Computer_Era_X.Views;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MessageBox = Computer_Era_X.Views.MessageBox;

namespace Computer_Era_X.ViewModels
{
    partial class GameVM
    {
        private LaborExchange model = new LaborExchange(); 

        partial void LaborExchangeInit()
        {
            WillQuit = new DelegateCommand(LayOff);
        }

        private void LaborExchangeStartGame()
        {
            CreateJobCards();
            BoardWithVacancies = new ContentControl();
            BoardWithVacancies.SizeChanged += ResizeBoard;
        }

        private void CreateJobCards()
        {
            Collection<Profession> professions = GameEnvironment.Professions;
            Collection<Company> companies = GameEnvironment.GameValues.Companies;
            DateTime game_date = GameEnvironment.Events.Timer.DateTime;

            List<Company> current_companies = new List<Company>();
            current_companies = companies.ToList();
            current_companies.RemoveAll(e => DateTime.Compare(e.OpeningYear, game_date) > 0);

            for (int i = 0; i < professions.Count; i++)
            {
                JobCards.Add(new JobCard(professions[i], current_companies, i, GameEnvironment.Random));
            }
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

        private void CreateJobsGrid()
        {
            BoardWithVacancies.Content = null;
            Grid _jobsGrid = new Grid();
            _jobsGrid.ShowGridLines = true; //Debug

            double cell_size = 250;
            double size = Math.Floor(BoardWeight / cell_size);
            double len = BoardWeight / size;

            for (int i = 0; i < size; i++)
            {
                ColumnDefinition col = new ColumnDefinition
                {
                    Width = new GridLength(len, GridUnitType.Star)
                };
                _jobsGrid.ColumnDefinitions.Insert(i, col);
            }

            int index = 0;
            for (int i = 0; i < Convert.ToInt32(size); i++)
            {
                StackPanel columnPanel = new StackPanel();

                if (JobCards.Count <= size)
                {
                    for (int j = 0; j < JobCards.Count; j++)
                    {
                        StackPanel panel;
                        panel = AddCard(j);
                        panel.SetValue(Grid.ColumnProperty, j);
                        _jobsGrid.Children.Add(panel);
                    }
                    break;
                }
                else
                {
                    double count = Math.Ceiling(JobCards.Count / size);
                    //double sc = (i + 1) * size;
                    //double stCount = count;

                    for (int j = 0; j < count; j++)
                    {
                        if (i + 1 > size - (size * count - JobCards.Count) & j == count - 1) { break; }
                        columnPanel.Children.Add(AddCard(index));
                        index += 1;
                    }

                    columnPanel.SetValue(Grid.ColumnProperty, i);
                    _jobsGrid.Children.Add(columnPanel);
                }

            }

            BoardWithVacancies.Content = _jobsGrid;
        }

        private StackPanel AddCard(int index)
        {
            //MessageBox.Show(index.ToString());

            //string text = JobCards[index].Name;
            //text = char.ToUpper(text[0]) + text.Substring(1);

            TextBlock professionName = new TextBlock
            {
                Text = string.Format(Resources.RequiresXInTheCompanyX, JobCards[index].Name, JobCards[index].CompanyName),
                TextWrapping = TextWrapping.Wrap,
                FontSize = 18,
                Foreground = new SolidColorBrush(Colors.Blue),
                Margin = new Thickness(10, 20, 10, 5)
            };

            TextBlock professionSalary = new TextBlock
            {
                Text = Resources.Salary + ": " + JobCards[index].Salary * GameEnvironment.Player.Money[0].Course + " " + GameEnvironment.Player.Money[0].Abbreviation,
                FontSize = 18,
                Foreground = new SolidColorBrush(Colors.Blue),
                Margin = new Thickness(10, 5, 10, 5)
            };

            TextBlock professionTime = new TextBlock
            {
                Text = Resources.WorkingHours + ": " + JobCards[index].FromTime.ToString("HH:mm") + " - " + JobCards[index].ToTime.ToString("HH:mm"),
                FontSize = 18,
                Foreground = new SolidColorBrush(Colors.Blue),
                Margin = new Thickness(10, 5, 10, 5)
            };

            StackPanel stackPanel = new StackPanel
            {
                Tag = JobCards[index].Id.ToString(),
                Margin = new Thickness(10, 10, 10, 10),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 250,
                Width = 250,
                Background = JobCards[index].StickerColor,
                Cursor = Cursors.Hand,
            };

            //stackPanel.MouseDown += new MouseButtonEventHandler(StackPanel_MouseDown); ВЕРНУТЬ!!!!!!!!!!!!!!

            stackPanel.Children.Add(professionName);
            stackPanel.Children.Add(professionSalary);
            stackPanel.Children.Add(professionTime);

            return stackPanel;
        }

        private void ResizeBoard(object sender, System.EventArgs e)
        {
            BoardWeight = (sender as ContentControl).ActualWidth;
            CreateJobsGrid();
        }

        private ContentControl _boardWithVacancies;
        private double BoardWeight;
        readonly Collection<JobCard> JobCards = new Collection<JobCard>();
        public bool RetirementOpportunity => GameEnvironment.Player.Job != null ? true : false;
        public ContentControl BoardWithVacancies
        {
            get => _boardWithVacancies;
            set => SetProperty(ref _boardWithVacancies, value);
        }

        public DelegateCommand WillQuit { get; set; }
    }
}
