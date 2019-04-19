using System;
using System.Windows.Threading;
using Prism.Mvvm;

namespace Computer_Era_X.Models
{
    public class Timer : BindableBase
    {
        private DateTime _dateTime = new DateTime(1990, 1, 1, 7, 0, 0);

        public readonly TimeSpan TimeSpanPlay = new TimeSpan(0, 0, 0, 0, 20);
        public readonly TimeSpan TimeSpanFastPlay = new TimeSpan(0, 0, 0, 0, 10);
        public readonly TimeSpan TimeSpanVeryFastPlay = new TimeSpan(0, 0, 0, 0, 5);
        public DateTime DateTime
        {
            get => _dateTime;
            private set
            {
                SetProperty(ref _dateTime, value);
                RaisePropertyChanged("GameTime");
            }
        }       
        public DispatcherTimer DTimer = new DispatcherTimer();
        private DateTime _oldDateTime;
        public delegate void MethodContainer();

        public event MethodContainer Minute;
        public event MethodContainer Hour;
        public event MethodContainer Day;
        public event MethodContainer Week;
        public event MethodContainer Month;
        public event MethodContainer Year;

        public Timer()
        {
            DTimer.Tick += DTimerTick;
            DTimer.Interval = TimeSpanPlay;
            _oldDateTime = DateTime;
        }
        /// <summary>
        /// Stops the timer and simulates until the specified date.
        /// </summary>
        public void FastSimulation(DateTime dateTime)
        {
            DTimer.Stop();
            while (dateTime > DateTime)
            {
                Tick();
            }
            DTimer.Start();
        }
        private void Tick()
        {
            DateTime = DateTime.AddMinutes(1);
            Minutes();
            Hours();
            Days();
            Months();
            Years();
        }

        private void DTimerTick(object sender, EventArgs args)
        {
            Tick();
        }
        private void Minutes()
        {
            if (DateTime.Minute > _oldDateTime.Minute & DateTime.Hour == _oldDateTime.Hour
                || DateTime.Minute < _oldDateTime.Minute & (DateTime.Hour > _oldDateTime.Hour
                || DateTime.Hour < _oldDateTime.Hour & (DateTime.Day > _oldDateTime.Day
                || DateTime.Day < _oldDateTime.Day & (DateTime.Month > _oldDateTime.Month
                || DateTime.Month < _oldDateTime.Month & DateTime.Year > _oldDateTime.Year))))
            {
                _oldDateTime = new DateTime(_oldDateTime.Year,
                                            _oldDateTime.Month,
                                            _oldDateTime.Day,
                                            _oldDateTime.Hour,
                                            DateTime.Minute,
                                            _oldDateTime.Second);
                Minute?.Invoke();
            }
        }
        private void Hours()
        {
            if (DateTime.Hour > _oldDateTime.Hour & DateTime.Day == _oldDateTime.Day
                || DateTime.Hour < _oldDateTime.Hour & (DateTime.Day > _oldDateTime.Day
                || DateTime.Day < _oldDateTime.Day & (DateTime.Month > _oldDateTime.Month
                || DateTime.Month < _oldDateTime.Month & DateTime.Year > _oldDateTime.Year)))
            {
                _oldDateTime = new DateTime(_oldDateTime.Year,
                                            _oldDateTime.Month,
                                            _oldDateTime.Day,
                                            DateTime.Hour,
                                            _oldDateTime.Minute,
                                            _oldDateTime.Second);
                Hour?.Invoke();
            }
        }
        private void Days()
        {
            if (DateTime.Day > _oldDateTime.Day & DateTime.Month == _oldDateTime.Month
                || DateTime.Day < _oldDateTime.Day & (DateTime.Month > _oldDateTime.Month
                || DateTime.Month < _oldDateTime.Month & DateTime.Year > _oldDateTime.Year))
            {
                _oldDateTime = new DateTime(_oldDateTime.Year,
                                            _oldDateTime.Month,
                                            DateTime.Day,
                                            _oldDateTime.Hour,
                                            _oldDateTime.Minute,
                                            _oldDateTime.Second);
                Day?.Invoke();
                if (DateTime.DayOfWeek == DayOfWeek.Sunday)
                {
                    Week?.Invoke();
                }
            }
        }
        private void Months()
        {
            if (DateTime.Month > _oldDateTime.Month & DateTime.Year == _oldDateTime.Year
                || DateTime.Month < _oldDateTime.Month & DateTime.Year > _oldDateTime.Year)
            {
                _oldDateTime = new DateTime(_oldDateTime.Year,
                                            DateTime.Month,
                                            _oldDateTime.Day,
                                            _oldDateTime.Hour,
                                            _oldDateTime.Minute,
                                            _oldDateTime.Second);
                Month?.Invoke();
            }
        }
        private void Years()
        {
            if (DateTime.Year > _oldDateTime.Year)
            {
                _oldDateTime = new DateTime(DateTime.Year,
                                            _oldDateTime.Month,
                                            _oldDateTime.Day,
                                            _oldDateTime.Hour,
                                            _oldDateTime.Minute,
                                            _oldDateTime.Second);
                Year?.Invoke();
            }
        }
    }
} 
