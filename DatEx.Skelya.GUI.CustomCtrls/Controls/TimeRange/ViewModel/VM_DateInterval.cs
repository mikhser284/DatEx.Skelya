using System;
using System.ComponentModel;

namespace DatEx.Skelya.GUI.CustomCtrls.ViewModel
{
    public class TimeRange : INotifyPropertyChanged
    {
        private DateTime? _timeStart;
        public DateTime? TimeStart
        {
            get => _timeStart;
            set { _timeStart = value; OnPropChanged(nameof(_timeStart)); }
        }

        private DateTime? _timeEnd;
        public DateTime? TimeEnd
        {
            get => _timeEnd;
            set { _timeEnd = value; OnPropChanged(nameof(TimeEnd)); }
        }

        private TimeSpan? _timePeriod;
        public TimeSpan? TimePeriod
        {
            get => _timePeriod;
            set { _timePeriod = value; OnPropChanged(nameof(TimePeriod)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropChanged(string propName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        // TimeSpan
        // SpanOfTime
        // TimeInterval
    }
}
