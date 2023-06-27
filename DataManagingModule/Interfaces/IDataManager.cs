using DataManagingModule.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagingModule.Interfaces
{
    public interface IDataManager<T>
    {
        public bool UnsavedChanges { get; }
        public ObservableCollection<Mill> Data { get; }

        public ObservableCollection<T> LoadData();

        public void RemoveRow(T row);

        public void AddRow(T row);

        public void SaveChanges();

        public int UpdateRow(T oldRow, T newRow);
    }
}
