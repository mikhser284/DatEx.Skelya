using DatEx.Skelya.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatEx.Skelya.GUI.ViewModel
{
    public class VM_Device
    {
        public Boolean? CheckMark { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public DeviceType Type { get; set; }

        public Int32? DataSectorId { get; set; }

        public VM_Device(Device x)
        {
            CheckMark = true;
            Id = x.Id;
            Name = x.Name;
            Type = x.Type;
            DataSectorId = x.DataSector.Id;
        }

        public override System.String ToString() => Name;
    }
}
