using DatEx.Skelya.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatEx.Skelya.GUI.CustomCtrls.ViewModel
{
    public class VM_DeviceType
    {
        public Boolean? CheckMark { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Classifier { get; set; }

        public VM_DeviceType(DeviceType x)
        {
            CheckMark = true;
            Id = x.Id;
            Name = x.Name;
            Classifier = x.Classifier;
        }
        public override String ToString() => $"{Name} ({Classifier})";
    }
}
