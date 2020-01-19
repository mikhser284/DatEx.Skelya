using DatEx.Skelya.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DatEx.Skelya.GUI.CustomCtrls.ViewModel
{
    public class VM_EventLogRecord : INotifyPropertyChanged
    {
        public Boolean? EventHasShapshot { get; set; }

        public Int32? IdOfSnapshot { get; set; }

        public DateTime? ShapshotDate { get; set; }

        public String SnapshotFileMimeType { get; set; }

        public string SnapshotName { get; set; }


        public int EventId { get; set; }

        private Int32? _eventSnapshotId;
        public Int32? EventSnapshotId
        {
            get => _eventSnapshotId; 
            set { _eventSnapshotId = value; OnPropertyChanged(nameof(EventSnapshotId)); }
        }

        public Boolean EventHasComments { get => Comments != null && Comments.Count > 0;  }

        public DateTime EventTime { get; set; }

        public string EventDescription { get; set; }

        public String EventTypeId { get; set; }

        public string EventTypeName { get; set; }

        public String EventType { get; set; }

        public List<Trigger> EventTypeTriggers { get; set; }

        public Int32? DataSectorId { get; set; }

        public String DataSectorName { get; set; }

        public String DeviceId { get; set; }

        public String DeviceName { get; set; }

        public Int32? DeviceTypeId { get; set; }

        public String DeviceTypeName { get; set; }

        public String DeviceTypeClassifier { get; set; }


        public List<Device> AllDevices { get; set; }

        public List<Comment> Comments { get; set; }

        public EventLogRecordData Data { get; set; }

        public VM_EventLogRecord(EventLogRecord x)
        {
            EventId = x.Id;
            EventSnapshotId = null;
            EventTime = x.DateTime;
            EventDescription = x.Description;
            EventTypeId = x.Event.Id;
            EventTypeName = x.Event.Name;
            EventType = x.Event.Type;
            EventTypeTriggers = new List<Trigger>();
            x.Event.Triggers?.ForEach(e => EventTypeTriggers.Add(e));

            Device dev = x.Devices.FirstOrDefault();            

            DataSectorId = dev?.DataSector?.Id ?? null;
            DataSectorName = dev?.DataSector?.Name ?? null;
            DeviceId = dev?.Id;
            DeviceName = dev?.Name;
            DeviceTypeId = dev?.Type?.Id ?? null;
            DeviceTypeName = dev?.Type?.Name;
            DeviceTypeClassifier = dev?.Type?.Classifier;

            AllDevices = new List<Device>();
            x.Devices?.ForEach(e => AllDevices.Add(e));
            Comments = new List<Comment>();
            x.Comments?.ForEach(e => Comments.Add(e));
            Data = x.Data;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
