using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using DatEx.Skelya.DataModel;

namespace DatEx.Skelya.GUI.CustomCtrls.ViewModel
{
    public class VM_Comment : INotifyPropertyChanged
    {
        private Int32 _id;
        public Int32 Id
        {
            get => _id;
            set { _id = value; OnPropChanged(nameof(Id)); }
        }

        private String _description;
        public string Description
        {
            get => _description;
            set { _description = value; OnPropChanged(nameof(Description)); }
        }

        private String _authorName;
        public string AuthorName
        {
            get => _authorName;
            set { _authorName = value; OnPropChanged(nameof(AuthorName)); }
        }

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set { _date = value; OnPropChanged(nameof(Date)); }
        }

        public VM_Comment(Comment comment)
        {
            Id = comment.Id;
            Description = comment.Description;
            AuthorName = comment.AuthorName;
            Date = comment.Date;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }        
}
