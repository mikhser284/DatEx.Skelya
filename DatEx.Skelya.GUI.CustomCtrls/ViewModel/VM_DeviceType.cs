using DatEx.Skelya.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatEx.Skelya.GUI.CustomCtrls.ViewModel
{
    public class VM_DeviceType
    {
        public Boolean? CheckMark { get; set; }

        public Int32 Id { get; set; }

        public String Name { get; set; }

        public String Classifier { get; set; }

        public VM_DeviceType(DeviceType devType)
        {
            CheckMark = true;
            Id = devType.Id;
            Name = devType.Name;
            Classifier = devType.Classifier;
        }
        public override String ToString() => $"{Name} ({Classifier})";
    }
}
