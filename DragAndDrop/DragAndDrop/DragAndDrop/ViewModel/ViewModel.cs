using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace DragAndDrop
{
    public class ViewModel : INotifyPropertyChanged
    {
        #region Fields

        private ObservableCollection<ToDoItem> toDoList;

        private Command<object> markDoneCommand;
        private bool isVisible;
        private string footerText;

        public string FooterText
        {
            get { return footerText; }
            set
            {
                footerText = value;
                OnPropertyChanged("FooterText");
            }
        }

        #endregion

        #region Constructor

        public ViewModel()
        {
            this.GenerateSource();
            MarkDoneCommand = new Command<object>(MarkItemAsDone);
        }

        private void MarkItemAsDone(object obj)
        {
            var item = obj as ToDoItem;
            item.IsDone = !item.IsDone;
        }

        #endregion

        #region Property

        public Command<object> MarkDoneCommand { get
            {
                return markDoneCommand;
            }
             set
            {
                if(markDoneCommand != value)
                {
                    markDoneCommand = value;
                    OnPropertyChanged("MarkDoneCommand");
                }
            }
        }

        public ObservableCollection<ToDoItem> ToDoList
        {
            get
            {
                return toDoList;
            }
            set
            {
                this.toDoList = value;
            }
        }

        public bool IsVisible
        {
            get
            {
                return isVisible;
            }
            set
            {
                isVisible = value;
                OnPropertyChanged("IsVisible");
            }
        }

        #endregion

        #region Method

        public void GenerateSource()
        {
            ToDoListRepository todoRepository = new ToDoListRepository();
            toDoList = todoRepository.GetToDoList();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
