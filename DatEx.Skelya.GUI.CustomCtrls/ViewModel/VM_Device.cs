using DatEx.Skelya.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatEx.Skelya.GUI.CustomCtrls.ViewModel
{
    public class VM_Device
    {
        public Boolean? CheckMark { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public Int32? TypeId { get; set; }

        public VM_DeviceType Type { get; set; }

        public Int32? DataSectorId { get; set; }

        public VM_DataSector DataSector { get; set; }

        public VM_Device(Device dev)
        {
            CheckMark = true;
            Id = dev.Id;
            Name = dev.Name;
            TypeId = dev.Type?.Id;
            Type = dev.Type is null ? null : new VM_DeviceType(dev.Type);
            DataSectorId = dev.DataSector?.Id;
            DataSector = dev.DataSector is null ? null : new VM_DataSector(dev.DataSector);
        }

        public override System.String ToString() => Name;
    }
}
