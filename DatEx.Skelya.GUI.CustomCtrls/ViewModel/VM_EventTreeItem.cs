using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace DatEx.Skelya.GUI.CustomCtrls.ViewModel
{
    public class VM_EventTreeItem : INotifyPropertyChanged
    {
        private String _name;
        public String Name { get => _name; set { _name = value; OnPropertyChanged(nameof(Name)); }}

        private ObservableCollection<VM_EventTreeItem> _descendants;

        public ObservableCollection<VM_EventTreeItem> Descendants 
        { 
            get => _descendants; 
            set { _descendants = value; OnPropertyChanged(nameof(Descendants)); } 
        }

        private VM_EventTreeItem _ancestor;
        public VM_EventTreeItem Ancestor 
        { 
            get => _ancestor; 
            set { _ancestor = value; OnPropertyChanged(nameof(Ancestor)); } 
        }

        private Int32 _allEventsCount;
        public Int32 AllEventsCount 
        { 
            get => _allEventsCount; 
            private set { _allEventsCount = value; OnPropertyChanged(nameof(AllEventsCount)); } 
        }

        private Int32 _dangerEventsCount;
        public Int32 DangerEventsCount 
        { 
            get => _dangerEventsCount; 
            set
            { 
                _dangerEventsCount = value;
                OnPropertyChanged(nameof(DangerEventsCount));
                UpdateAllEventsCounter();
            }
        }

        private Int32 _warningEventsCount;
        public Int32 WarningEventsCount 
        {
            get => _warningEventsCount;
            set
            {
                _warningEventsCount = value;
                OnPropertyChanged(nameof(WarningEventsCount));
                UpdateAllEventsCounter();
            } 
        }

        private Int32 _infoEvents;
        public Int32 InfoEventsCount
        { 
            get => _infoEvents; 
            set
            {
                
                _infoEvents = value;
                OnPropertyChanged(nameof(InfoEventsCount));
                UpdateAllEventsCounter();
            } 
        }

        

        private void UpdateAllEventsCounter()
        {
            AllEventsCount = DangerEventsCount + WarningEventsCount + InfoEventsCount;
            UpdateAncestor();
        }

        private void UpdateAncestor()
        {
            if (Ancestor == null) return;
            
            Ancestor._dangerEventsCount = 0;
            Ancestor._warningEventsCount = 0;
            Ancestor._infoEvents = 0;
            foreach (var descendant in Ancestor.Descendants)
            {
                Ancestor._dangerEventsCount += descendant.DangerEventsCount;
                Ancestor._warningEventsCount += descendant.WarningEventsCount;
                Ancestor._infoEvents += descendant.InfoEventsCount;
            }

            Ancestor.OnPropertyChanged(nameof(DangerEventsCount));
            Ancestor.OnPropertyChanged(nameof(WarningEventsCount));
            Ancestor.OnPropertyChanged(nameof(InfoEventsCount));
        }

        public VM_EventTreeItem(String name = null, Int32 danger = 0, Int32 warning = 0, Int32 info = 0)
        {
            Descendants = new ObservableCollection<VM_EventTreeItem>();
            Name = name;
            DangerEventsCount = danger;
            WarningEventsCount = warning;
            InfoEventsCount = info;            
        }

        public void AddDescendant(VM_EventTreeItem descendant)
        {
            Descendants.Add(descendant);
            DangerEventsCount += descendant.DangerEventsCount;
            WarningEventsCount += descendant.WarningEventsCount;
            InfoEventsCount += descendant.WarningEventsCount;
            descendant.Ancestor = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));


        public static VM_EventTreeItem GetTestItems()
        {
            var allItems = new VM_EventTreeItem("All items");

            var company1 = new VM_EventTreeItem("СП Росоша");
            company1.AddDescendant(new VM_EventTreeItem("dev 01A", 0, 1, 5));
            company1.AddDescendant(new VM_EventTreeItem("dev 01B", 1, 0, 3));
            var company2 = new VM_EventTreeItem("СП Жмеринка", 1, 0, 3);
            var company3 = new VM_EventTreeItem("СП Козятин", 2, 1, 1);
            var company4 = new VM_EventTreeItem("ООО АФТ", 4, 0, 2);
            var company5 = new VM_EventTreeItem("ООО Лещинское", 0, 5, 15);

            allItems.AddDescendant(company1);
            allItems.AddDescendant(company2);
            allItems.AddDescendant(company3);
            allItems.AddDescendant(company4);
            allItems.AddDescendant(company5);

            return allItems;
        }

        public override string ToString()
        {
            return $"{Name} ({Descendants.Count}) A: {AllEventsCount} D: {DangerEventsCount} W {WarningEventsCount} I: {InfoEventsCount}";
        }
    }
}
