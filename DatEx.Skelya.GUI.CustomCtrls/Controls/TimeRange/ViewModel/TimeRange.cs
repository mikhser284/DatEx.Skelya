using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DatEx.Skelya.GUI.CustomCtrls.ViewModel
{
    public class VM_TimeRange : INotifyPropertyChanged
    {
        private DateTime? _start;
        public DateTime? Start
        {
            get => _start;
            set { _start = value; OnPropChanged(nameof(Start)); }
        }

        private DateTime? _end;
        public DateTime? End
        {
            get => _end;
            set { _end = value; OnPropChanged(nameof(End)); }
        }

        private TimeSpan _length;
        public TimeSpan Length
        {
            get => _length;
            set { _length = value; OnPropChanged(nameof(Length)); }
        }

        private Boolean _isFixed;
        public Boolean IsFixed
        {
            get => _isFixed;
            set { _isFixed = value; OnPropChanged(nameof(IsFixed)); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        public static VM_TimeRange UpdateFrom(VM_TimeRange source, VM_TimeRange destination)
        {
            VM_TimeRange dest = destination;
            if(source is null)
            {
                dest = source;
                return dest;
            }
            if(destination == null) dest = new VM_TimeRange();                
            dest.Start = source.Start;
            dest.End = source.End;
            dest.Length = source.Length;
            dest.IsFixed = source.IsFixed;
            return dest;
        }
    }
}
