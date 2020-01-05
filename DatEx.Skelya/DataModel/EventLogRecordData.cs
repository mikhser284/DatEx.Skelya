using Newtonsoft.Json;
using System;

namespace DatEx.Skelya.DataModel
{
    public class EventLogRecordData
    {
        [JsonProperty("weight")]
        public Double? Weight { get; set; }

        [JsonProperty("prevWeight")]
        public Double? PrevWeight { get; set; }

        [JsonProperty("prevTransaction")]
        public Int64? PrevTransaction { get; set; }

        [JsonProperty("transaction")]
        public Int64? Transaction { get; set; }

        [JsonProperty("transactionDataCount")]
        public Int32? TransactionDataCount { get; set; }

        [JsonProperty("stable")]
        public Boolean? Stable { get; set; }

        [JsonProperty("duration")]
        public Int32? Duration { get; set; }

        [JsonProperty("transactionMaxWeight")]
        public Double? TransactionMaxWeight { get; set; }

        [JsonProperty("perimetr")]
        public Boolean? Perimetr { get; set; }

        [JsonProperty("driverInCar")]
        public Boolean? DriverInCar { get; set; }

        [JsonProperty("status")]
        public Boolean? Status { get; set; }

        [JsonProperty("reader")]
        public String Reader { get; set; }

        [JsonProperty("truck1")]
        public object Truck1 { get; set; }

        [JsonProperty("truck2")]
        public String Truck2 { get; set; }

        [JsonProperty("item_name")]
        public String ItemName { get; set; }

        [JsonProperty("operation_name")]
        public String OperationName { get; set; }

        [JsonProperty("operation_type")]
        public String OperationType { get; set; }

        [JsonProperty("on")]
        public Boolean? On { get; set; }

        [JsonProperty("waybill")]
        public String Waybill { get; set; }

        [JsonProperty("transactionWaybill")]
        public String TransactionWaybill { get; set; }

        [JsonProperty("state")]
        public Boolean? State { get; set; }

        [JsonProperty("weight1")]
        public Double? Weight1 { get; set; }

        [JsonProperty("weight2")]
        public Double? Weight2 { get; set; }

        [JsonProperty("weight_center")]
        public Double? WeightCenter { get; set; }

        [JsonProperty("command")]
        public String Command { get; set; }

        [JsonProperty("user")]
        public String User { get; set; }

        [JsonProperty("id")]
        public String Id { get; set; }

        [JsonProperty("EPC")]
        public String EPC { get; set; }

        [JsonProperty("accesslevel")]
        public String Accesslevel { get; set; }

        [JsonProperty("ambietTemperature")]
        public Object AmbietTemperature { get; set; }

        [JsonProperty("analysisCounter")]
        public Int32? AnalysisCounter { get; set; }

        [JsonProperty("subsample_id")]
        public String Subsample_id { get; set; }

        [JsonProperty("subsample_NrOFSubSamples")]
        public Object SubsampleNrOFSubSamples { get; set; }

        [JsonProperty("sampleHash")]
        public String SampleHash { get; set; }

        [JsonProperty("ProtDM")]
        public Double? ProtDM { get; set; }

        [JsonProperty("Moisture")]
        public Double? Moisture { get; set; }

        [JsonProperty("OilDM")]
        public Double? OilDM { get; set; }

        [JsonProperty("StarchDM")]
        public Double? StarchDM { get; set; }
    }
}
