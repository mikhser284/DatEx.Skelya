using DatEx.Skelya.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DatEx.Skelya.GUI.CustomCtrls.ViewModel
{
    public class VM_EventLogRecord : INotifyPropertyChanged
    {
        public int EventId { get; set; } //■

        public DateTime EventTime { get; set; } //■

        public String EventCriticality { get; set; } //■

        public ObservableCollection<VM_Trigger> EventTypeTriggers { get; set; } //■


        private Boolean? _eventHasSnapshot;        
        public Boolean? EventHasSnapshot //■
        {
            get => _eventHasSnapshot;
            set { _eventHasSnapshot = value; OnPropChanged(nameof(EventHasSnapshot)); }
        }        

        private Int32? _snapshotId;
        public Int32? SnapshotId
        {
            get => _snapshotId;
            set { _snapshotId = value; OnPropChanged(nameof(SnapshotId)); }
        }

        private VM_Snapshot _snapshot;
        public VM_Snapshot Snapshot
        {
            get => _snapshot;
            set { _snapshot = value; OnPropChanged(nameof(Snapshot)); }
        }

        
        public Boolean EventHasComments { get => Comments != null && Comments.Count > 0;  }



        public string EventDescription { get; set; } //■

        public String EventTypeId { get; set; } //■

        public string EventTypeName { get; set; } //■


        public Int32? DataSectorId { get; set; } //■

        public String DataSectorName { get; set; } //■

        public String DeviceId { get; set; } //■

        public String DeviceName { get; set; } //■

        public Int32? DeviceTypeId { get; set; } //■

        public String DeviceTypeName { get; set; } //■

        public String DeviceTypeClassifier { get; set; } //■


        public ObservableCollection<VM_Device> AllDevices { get; set; }

        public ObservableCollection<VM_Comment> Comments { get; set; }

        public VM_EventRecordData Data { get; set; }



        public VM_EventLogRecord(EventLogRecord e)
        {
            EventId = e.Id;
            EventHasSnapshot = null;
            EventTime = e.DateTime;
            EventDescription = e.Description;
            EventTypeId = e.Event.Id;
            EventTypeName = e.Event.Name;
            EventCriticality = e.Event.Type;
            EventTypeTriggers = new ObservableCollection<VM_Trigger>();            
            e.Event.Triggers?.ForEach(trigger => { trigger.EventId = e.Event.Id; EventTypeTriggers.Add(new VM_Trigger(trigger)); });

            Device dev = e.Devices.FirstOrDefault();            

            DataSectorId = dev?.DataSector?.Id ?? null;
            DataSectorName = dev?.DataSector?.Name ?? null;
            DeviceId = dev?.Id;
            DeviceName = dev?.Name;
            DeviceTypeId = dev?.Type?.Id ?? null;
            DeviceTypeName = dev?.Type?.Name;
            DeviceTypeClassifier = dev?.Type?.Classifier;

            AllDevices = new ObservableCollection<VM_Device>();
            e.Devices?.ForEach(e => AllDevices.Add(new VM_Device(e)));
            Comments = new ObservableCollection<VM_Comment>();
            e.Comments?.ForEach(e => Comments.Add(new VM_Comment(e)));
            Data = e.Data is null ? null : new VM_EventRecordData(e.Data);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropChanged(string propName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
