using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace DatEx.Skelya.GUI.ViewModel
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        public static ObservableCollection<VM_Comment> GetTestCollection()
        {
            ObservableCollection<VM_Comment> testCollection = new ObservableCollection<VM_Comment>
            {
                new VM_Comment { Id = 1, Date = DateTime.Now, AuthorName = "Пупкин В.И.", Description = "Комментарий 01" },
                new VM_Comment { Id = 2, Date = DateTime.Now, AuthorName = "Пупкин В.И.", Description = "Комментарий 02\nМногострочный комментарий \nМногострочный комментарий" },
                new VM_Comment { Id = 3, Date = DateTime.Now, AuthorName = "Пупкин В.И.", Description = "Комментарий 03" },
                new VM_Comment { Id = 4, Date = DateTime.Now, AuthorName = "Пупкин В.И.", Description = "Комментарий 04\nМногострочный комментарий" },
                new VM_Comment { Id = 5, Date = DateTime.Now, AuthorName = "Пупкин В.И.", Description = "Комментарий 05" },
            };

            return testCollection;
        }
    }
}
