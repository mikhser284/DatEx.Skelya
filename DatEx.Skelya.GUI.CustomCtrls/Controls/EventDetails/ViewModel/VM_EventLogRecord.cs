using DatEx.Skelya.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace DatEx.Skelya.GUI.CustomCtrls.ViewModel
{
    public class VM_Snapshot
    {
        public int Id { get; set; } //■

        public Int32 EventLogRecordId { get; set; }

        public string MimeType { get; set; } //■

        public string Name { get; set; } //■

        public DateTime Time { get; set; } //■

        public Size? SizePx { get; set; } //■

        public string File { get; set; } //■
    }



    public class VM_EventLogRecord : INotifyPropertyChanged
    {
        private Boolean? _eventHasSnapshot;        
        public Boolean? EventHasSnapshot
        {
            get => _eventHasSnapshot;
            set { _eventHasSnapshot = value; OnPropChanged(nameof(EventHasSnapshot)); }
        }

        private Int32? _snapshotId;
        public Int32? SnapshotId //■
        {
            get => _snapshotId;
            set { _snapshotId = value; OnPropChanged(nameof(SnapshotId)); }
        }

        private VM_Snapshot _snapshot;
        public VM_Snapshot Snapshot //■
        {
            get => _snapshot;
            set { _snapshot = value; OnPropChanged(nameof(Snapshot)); }
        }

        public int EventId { get; set; } //■

        public Boolean EventHasComments { get => Comments != null && Comments.Count > 0;  }

        public DateTime EventTime { get; set; } //■

        public string EventDescription { get; set; } //■

        public String EventTypeId { get; set; } //■

        public string EventTypeName { get; set; } //■

        public String EventCriticality { get; set; } //■

        public ObservableCollection<DataModel.Trigger> EventTypeTriggers { get; set; }

        public Int32? DataSectorId { get; set; }

        public String DataSectorName { get; set; } //■

        public String DeviceId { get; set; } //■

        public String DeviceName { get; set; } //■

        public Int32? DeviceTypeId { get; set; } //■

        public String DeviceTypeName { get; set; } //■

        public String DeviceTypeClassifier { get; set; } //■


        public ObservableCollection<VM_Device> AllDevices { get; set; }

        public ObservableCollection<Comment> Comments { get; set; }

        public VM_EventRecordData Data { get; set; }

        public VM_EventLogRecord(EventLogRecord x)
        {
            EventId = x.Id;
            EventHasSnapshot = null;
            EventTime = x.DateTime;
            EventDescription = x.Description;
            EventTypeId = x.Event.Id;
            EventTypeName = x.Event.Name;
            EventCriticality = x.Event.Type;
            EventTypeTriggers = new ObservableCollection<DataModel.Trigger>();
            x.Event.Triggers?.ForEach(e => EventTypeTriggers.Add(e));

            Device dev = x.Devices.FirstOrDefault();            

            DataSectorId = dev?.DataSector?.Id ?? null;
            DataSectorName = dev?.DataSector?.Name ?? null;
            DeviceId = dev?.Id;
            DeviceName = dev?.Name;
            DeviceTypeId = dev?.Type?.Id ?? null;
            DeviceTypeName = dev?.Type?.Name;
            DeviceTypeClassifier = dev?.Type?.Classifier;

            AllDevices = new ObservableCollection<VM_Device>();
            x.Devices?.ForEach(e => AllDevices.Add(new VM_Device(e)));
            Comments = new ObservableCollection<Comment>();
            x.Comments?.ForEach(e => Comments.Add(e));
            Data = x.Data is null ? null : new VM_EventRecordData(x.Data);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropChanged(string propName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }

    public class VM_EventRecordData
    {
        public VM_EventRecordData(EventLogRecordData d)
        {
            Weight = d.Weight;
            PrevWeight = d.PrevWeight;
            PrevTransaction = d.PrevTransaction;
            Transaction = d.Transaction;
            TransactionDataCount = d.TransactionDataCount;
            Stable = d.Stable;
            Duration = d.Duration;
            TransactionMaxWeight = d.TransactionMaxWeight;
            Perimetr = d.Perimetr;
            DriverInCar = d.DriverInCar;
            Status = d.Status;
            Reader = d.Reader;
            Truck1 = d.Truck1;
            Truck2 = d.Truck2;
            ItemName = d.ItemName;
            OperationName = d.OperationName;
            OperationType = d.OperationType;
            On = d.On;
            Waybill = d.Waybill;
            TransactionWaybill = d.TransactionWaybill;
            State = d.State;
            Weight1 = d.Weight1;
            Weight2 = d.Weight2;
            WeightCenter = d.WeightCenter;
            Command = d.Command;
            User = d.User;
            Id = d.Id;
            EPC = d.EPC;
            Accesslevel = Accesslevel;
            AmbietTemperature = AmbietTemperature;
            AnalysisCounter = d.AnalysisCounter;
            Subsample_id = d.Subsample_id;
            SubsampleNrOFSubSamples = d.SubsampleNrOFSubSamples;
            SampleHash = d.SampleHash;
            ProtDM = d.ProtDM;
            Moisture = d.Moisture;
            OilDM = d.OilDM;
            StarchDM = d.StarchDM;
        }
    
        public Double? Weight { get; set; } //■
        public Double? PrevWeight { get; set; } //■
        public Int64? PrevTransaction { get; set; } //■
        public Int64? Transaction { get; set; } //■
        public Int32? TransactionDataCount { get; set; } //■
        public Boolean? Stable { get; set; } //■
        public Int32? Duration { get; set; } //■
        public Double? TransactionMaxWeight { get; set; } //■
        public Boolean? Perimetr { get; set; } //■
        public Boolean? DriverInCar { get; set; } //■
        public Boolean? Status { get; set; } //■
        public String Reader { get; set; } //■
        public object Truck1 { get; set; } //■
        public String Truck2 { get; set; } //■
        public String ItemName { get; set; } //■
        public String OperationName { get; set; } //■
        public String OperationType { get; set; } //■
        public Boolean? On { get; set; } //■
        public String Waybill { get; set; } //■
        public String TransactionWaybill { get; set; } //■
        public Boolean? State { get; set; } //■
        public Double? Weight1 { get; set; } //■
        public Double? Weight2 { get; set; } //■
        public Double? WeightCenter { get; set; } //■
        public String Command { get; set; } //■
        public String User { get; set; } //■
        public String Id { get; set; } //■
        public String EPC { get; set; } //■
        public String Accesslevel { get; set; } //■
        public Object AmbietTemperature { get; set; } //■
        public Int32? AnalysisCounter { get; set; } //■
        public String Subsample_id { get; set; } //■
        public Object SubsampleNrOFSubSamples { get; set; } //■
        public String SampleHash { get; set; } //■
        public Double? ProtDM { get; set; } //■
        public Double? Moisture { get; set; } //■
        public Double? OilDM { get; set; } //■
        public Double? StarchDM { get; set; } //■
    }
}
