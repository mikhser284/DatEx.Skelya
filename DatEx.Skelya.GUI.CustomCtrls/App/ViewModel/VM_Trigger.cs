using System;
using System.Collections.Generic;
using System.Text;
using DatEx.Skelya.DataModel;

namespace DatEx.Skelya.GUI.CustomCtrls.ViewModel
{
    public class VM_Trigger
    {
        public Int32 Id { get; set; }
        public String EventId { get; set; }
        public String Name { get; set; }
        public Int64 Value { get; set; }

        public VM_Trigger(Trigger trigger)
        {
            Id = trigger.Id;
            EventId = trigger.EventId;
            Name = trigger.Name;
            Value = trigger.Value;
        }
    }
}
