using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace DatEx.Skelya.GUI.CustomCtrls.ViewModel
{
    public class VM_FilterInfo : INotifyPropertyChanged
    {
        public Boolean CanBeUpdatedByUser { get; private set; }

        private String _filterName;
        public String FilterName
        {
            get => _filterName;
            set { _filterName = value; OnPropChanged(nameof(FilterName)); }
        }

        private DateTime? _timeFrom;
        public DateTime? TimeFrom
        {
            get => _timeFrom;
            set { _timeFrom = value; OnPropChanged(nameof(TimeFrom)); }
        }

        private DateTime? _timeTill;
        public DateTime? TimeTill
        {
            get => _timeTill;
            set { _timeTill = value; OnPropChanged(nameof(TimeTill)); }
        }

        public SimpleCheckableItem ContainsSnapshot { get; set; }

        public SimpleCheckableItem ContainsComments { get; set; }

        public SetOfCheckableItemsWithId<Int32> EventsCtiticity { get; set; }

        public SetOfCheckableItemsWithId<Int32> EventsTypes { get; set; }

        public SetOfCheckableItemsWithId<Int32> DataSectors { get; set; }

        public SetOfCheckableItemsWithId<Int32> DeviceTypes { get; set; }

        public SetOfCheckableItemsWithId<Int32> Devices { get; set; }

        
        public VM_FilterInfo()
        {
            ContainsSnapshot = new SimpleCheckableItem("Содержит изображения");
            ContainsComments = new SimpleCheckableItem("Содержит комментарии");
            EventsCtiticity = new SetOfCheckableItemsWithId<Int32>("Критичность событий");
            EventsTypes = new SetOfCheckableItemsWithId<Int32>("Типы событий");
            DataSectors = new SetOfCheckableItemsWithId<Int32>("Расположение");
            DeviceTypes = new SetOfCheckableItemsWithId<Int32>("Типы устройств");
            Devices = new SetOfCheckableItemsWithId<Int32>("Устройства");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }



    public class SimpleCheckableItem: INotifyPropertyChanged
    {
        private Boolean? _checkMark;
        public Boolean? CheckMark
        {
            get => _checkMark;
            set { _checkMark = value; OnPropChanged(nameof(CheckMark)); }
        }

        private String _name;
        public String Name
        {
            get => Name;
            set { _name = value; OnPropChanged(nameof(Name)); }
        }
        
        public SimpleCheckableItem(String name)
        {
            CheckMark = null;
            Name = name;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }

    public class SetOfCheckableItemsWithId<T> : INotifyPropertyChanged
    {
        private String _name;
        public String Name
        {
            get => _name;
            set { _name = value; OnPropChanged(nameof(Name)); }
        }

        public ObservableCollection<CheckableItemWithId<T>> Items { get; set; }

        public SetOfCheckableItemsWithId(String name)
        {
            Name = name;
            Items = new ObservableCollection<CheckableItemWithId<T>>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }


    public class CheckableItemWithId<T> : INotifyPropertyChanged
    {
        private Boolean? _checkMark;
        public Boolean? CheckMark
        {
            get => _checkMark;
            set { _checkMark = value; OnPropChanged(nameof(CheckMark)); }
        }

        private String _name;
        public String Name
        {
            get => Name;
            set { _name = value; OnPropChanged(nameof(Name)); }
        }

        private T _Id;
        public T Id
        {
            get => _Id;
            set { _Id = value; OnPropChanged(nameof(Id)); }
        }

        public CheckableItemWithId(String name, T id)
        {
            CheckMark = false;
            Name = name;
            Id = id;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
