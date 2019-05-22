using System;
using System.Collections.ObjectModel;
using Computer_Era_X.DataTypes.Enums;
using Computer_Era_X.Models;
using Computer_Era_X.Properties;

namespace Computer_Era_X.DataTypes.Objects.Computer
{
    public class MonitorProperties
    {
        public double Size { get; set; }
        public Resolution Resolution { get; set; }
        public int MaxFrameRefreshRate { get; set; }    // Maximum frame refresh rate (Hz)
        public Collection<VideoInterface> VideoInterfaces { get; set; } = new Collection<VideoInterface>();
    }

    public class Monitor : Item<MonitorProperties>
    {
        public Monitor(int uid, string name, string type, double price, DateTime man_date, MonitorProperties properties) : base(uid, name, type, price, man_date, properties) { }
        public Monitor(Item item) : base(item) { }

        public override string Info()
        {
            string info = Resources.Name + ": " + Name + Environment.NewLine;
            info += Resources.Size + ": " + Properties.Size + "\"" + Environment.NewLine;
            info += Resources.Resolution + ": " + Properties.Resolution.Width + "x" + Properties.Resolution.Height + Environment.NewLine;
            info += Resources.MaximumFrameRefreshRate + ": " + Properties.MaxFrameRefreshRate + " " + Resources.Hz + Environment.NewLine;
            info += Resources.ConnectionInterface + ": "; foreach (VideoInterface vInterface in Properties.VideoInterfaces) { info += vInterface + ", "; }
            info = info.Remove(info.Length - 2, 2);
            return info;
        }

        public bool IsCompatibility(Collection<VideoInterface> vInterfaces)
        {
            foreach (VideoInterface vInterface in vInterfaces)
            {
                foreach (VideoInterface mvInterface in Properties.VideoInterfaces)
                {
                    if (vInterface == mvInterface) { return true; }
                }
            }

            return false;
        }

        public Collection<VideoInterface> Compatibility(Collection<VideoInterface> vInterfaces)
        {
            Collection<VideoInterface> videoInterfaces = new Collection<VideoInterface>();

            foreach (VideoInterface vInterface in vInterfaces)
            {
                foreach (VideoInterface mvInterface in Properties.VideoInterfaces)
                {
                    if (vInterface == mvInterface) { videoInterfaces.Add(mvInterface); }
                }
            }

            return videoInterfaces;
        }
    }
}
