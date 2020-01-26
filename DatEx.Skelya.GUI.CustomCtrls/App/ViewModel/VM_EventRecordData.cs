using DatEx.Skelya.DataModel;
using System;

namespace DatEx.Skelya.GUI.CustomCtrls.ViewModel
{
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
        public String Reader { get; set; }
        public object Truck1 { get; set; }
        public String Truck2 { get; set; }
        public String ItemName { get; set; }
        public String OperationName { get; set; }
        public String OperationType { get; set; }
        public Boolean? On { get; set; }
        public String Waybill { get; set; }
        public String TransactionWaybill { get; set; }
        public Boolean? State { get; set; }
        public Double? Weight1 { get; set; }
        public Double? Weight2 { get; set; }
        public Double? WeightCenter { get; set; }
        public String Command { get; set; }
        public String User { get; set; }
        public String Id { get; set; }
        public String EPC { get; set; }
        public String Accesslevel { get; set; }
        public Object AmbietTemperature { get; set; }
        public Int32? AnalysisCounter { get; set; }
        public String Subsample_id { get; set; }
        public Object SubsampleNrOFSubSamples { get; set; }
        public String SampleHash { get; set; }
        public Double? ProtDM { get; set; }
        public Double? Moisture { get; set; }
        public Double? OilDM { get; set; }
        public Double? StarchDM { get; set; }
    }
}
