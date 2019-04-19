using System;
using System.Collections.Generic;
using System.Linq;
using Computer_Era_X.Converters;
using Computer_Era_X.DataTypes.Enums;

namespace Computer_Era_X.Models
{
    public delegate void MethodContainer(GameEvent @event);
    public class GameEvent
    {
        public string Name;
        public DateTime ResponseTime;
        public Periodicity InitialPeriodicity;
        public Periodicity Periodicity;
        public int PeriodicityValue;
        public MethodContainer Method;
        public bool Restart;

        public GameEvent(string name, DateTime responseTime, Periodicity periodicity, int periodicityValue,
            MethodContainer method, bool restart = false)
        {
            Name = name;
            ResponseTime = responseTime;
            InitialPeriodicity = periodicity;
            Periodicity = periodicity;
            PeriodicityValue = periodicityValue;
            Method = method;
            Restart = restart;
        }
    }

    public class GameEvents
    {
        public List<GameEvent> Events = new List<GameEvent>();
        public Timer Timer = new Timer();

        public GameEvents()
        {
            Timer.Minute += Minute;
            Timer.Hour += Hour;
            Timer.Day += Day;
            Timer.Week += Week;
            Timer.Month += Month;
            Timer.Year += Year;
        }
        private void EventRun(GameEvent gameEvent)
        {
            if (DateTime.Compare(gameEvent.ResponseTime, Timer.DateTime) <= 0)
            {
                gameEvent.Method(gameEvent);

                if (gameEvent.Restart)
                {
                    gameEvent.ResponseTime = PeriodicityConverter.GetDateTimeFromPeriodicity(gameEvent.ResponseTime, gameEvent.InitialPeriodicity, gameEvent.PeriodicityValue);
                    gameEvent.Periodicity = gameEvent.InitialPeriodicity;
                }
                else { Events.Remove(gameEvent); }
            }
            else
            {
                if (gameEvent.ResponseTime.Year == Timer.DateTime.Year)
                {
                    if (gameEvent.Periodicity == Periodicity.Year) { gameEvent.Periodicity = Periodicity.Month; }
                }
                else { return; }

                if (gameEvent.ResponseTime.Month == Timer.DateTime.Month)
                {
                    if (gameEvent.Periodicity == Periodicity.Month) { gameEvent.Periodicity = Periodicity.Week; }
                }
                else { return; }

                if ((gameEvent.ResponseTime - Timer.DateTime).Days <= 7 & (gameEvent.ResponseTime - Timer.DateTime).Days >= 0)
                {
                    if (gameEvent.Periodicity == Periodicity.Week) { gameEvent.Periodicity = Periodicity.Day; }
                }
                else { return; }

                if (gameEvent.ResponseTime.Day == Timer.DateTime.Day)
                {
                    if (gameEvent.Periodicity == Periodicity.Day) { gameEvent.Periodicity = Periodicity.Hour; }
                }
                else { return; }

                if (gameEvent.ResponseTime.Hour != Timer.DateTime.Hour) return;
                if (gameEvent.Periodicity == Periodicity.Hour) { gameEvent.Periodicity = Periodicity.Minute; }
            }
        }
        private void RunEvents(Periodicity periodicity)
        {
            var events = Events.Where(e => e.Periodicity == periodicity).ToList();
            foreach (var t in events)
            {
                EventRun(t);
            }
        }
        private void Minute() => RunEvents(Periodicity.Minute);
        private void Hour() => RunEvents(Periodicity.Hour);
        private void Day() => RunEvents(Periodicity.Day);
        private void Week() => RunEvents(Periodicity.Week);
        private void Month() => RunEvents(Periodicity.Month);
        private void Year() => RunEvents(Periodicity.Year);
    }
}
