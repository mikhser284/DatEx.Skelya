using System;
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
}
