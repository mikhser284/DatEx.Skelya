using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using DatEx.Skelya.DataModel;

namespace DatEx.Skelya.GUI.ViewModel
{
    public class VM_DataSector
    {
        public Int32 Id { get; set; }

        public String Name { get; set; }

        public Boolean? CheckMark { get; set; }

        public ObservableCollection<VM_Device> Devices { get; set; }

        public VM_DataSector(DataSector x)
        {
            Id = x.Id;
            Name = x.Name;
            CheckMark = true;
            Devices = new ObservableCollection<VM_Device>();
        }

        public override String ToString() => Name;
    }
}
