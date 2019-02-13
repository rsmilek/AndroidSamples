using System;

namespace MqttNetApp
{
    public class TimeMeas
    {
        public DateTime StartStamp { get; private set; }
        public DateTime StopStamp { get; private set; }
        public TimeSpan Diff => StopStamp - StartStamp;
        public int DiffMilliseconds => Convert.ToInt32(Diff.TotalMilliseconds);
        public double DiffSeconds => Diff.TotalMilliseconds / 1000;
        public string DiffSecondsStr => string.Format("{0:0.000 s}", DiffSeconds);

        public void Start() => StartStamp = DateTime.UtcNow;

        public TimeSpan Stop()
        {
            StopStamp = DateTime.UtcNow;
            return Diff;
        }
    }
}