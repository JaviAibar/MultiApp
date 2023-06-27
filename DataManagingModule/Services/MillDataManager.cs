using DataManagingModule.Interfaces;
using DataManagingModule.Model;
using MultiApp.Core;
using MultiApp.Core.Extensions;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataManagingModule.Services
{
    internal class MillDataManager : BindableBase, IDataManager<Mill>
    {
        // Fields
        public Action unsavedChanged;

        // Services

        // Properties
        private string Header { set; get; }
        public ObservableCollection<Mill> Data { get; private set; }
        public string Path
        {
            get => _path; set
            {
                _path = value;
                LoadData();
            }
        }
        

        // Properties backing field
        private string _path;
        private bool _unsavedChanges;

        // Observable Properties

        public bool UnsavedChanges
        {
            get => _unsavedChanges; private set
            {
                _unsavedChanges = value;
                unsavedChanged?.Invoke();
            }
        }

        // Constructors
        public MillDataManager() { }
        public MillDataManager(string path)
        {
            Path = path;
        }

        // Methods
        public void AddRow(Mill row)
        {
            Data.Add(row);
            UnsavedChanges = true;
        }

        public ObservableCollection<Mill> LoadData()
        {
            Data = new ObservableCollection<Mill>(ReadCSV());
            UnsavedChanges = false;
            return Data;
        }

        public void RemoveRow(Mill row)
        {
            Data.Remove(row);
            UnsavedChanges = true;
        }

        public void SaveChanges()
        {
            SaveCSV();
            UnsavedChanges = false;
        }
        private void SaveCSV()
        {
            File.WriteAllText(Path, Header);
            File.AppendAllText(Path, string.Join("\n", Data.AsEnumerable()));
        }

        private IEnumerable<Mill> ReadCSV()
        {
            string[] lines = File.ReadAllLines(System.IO.Path.ChangeExtension(Path, ".csv"));
            string[] dataArray = TakeHeaderFromData(lines);

            return dataArray.Select(line =>
            {
                string[] data = line.Split(Constants.CsvDelimiter);

                return new Mill(
                    data[0],
                    data[1].ParseOr0(),
                    data[2].ParseOr0(),
                    data[3].ParseOr0(),
                    data[4].ParseOr0(),
                    data[5].ParseOr0(),
                    data[6].ParseOr0(),
                    data[7].ParseOr0(),
                    data[8].ParseOr0(),
                    data[9].ParseOr0(),
                    data[10].ParseOr0(),
                    data[11].ParseOr0(),
                    data[12].ParseOr0());
            });
        }

        private string[] TakeHeaderFromData(string[] originalArray)
        {
            Header = originalArray[0] + "\n";
            string[] subarray = new string[originalArray.Length - 1];
            Array.Copy(originalArray, 1, subarray, 0, originalArray.Length - 1);
            return subarray;
        }

        public int UpdateRow(Mill oldRow, Mill newRow)
        {
            bool found = false;
            int index = -1;
            ObservableCollection<Mill> data = Data;
            for (int i = 0; i < Data.Count; i++)
            {
                if (data[i].CaseName == oldRow.CaseName)
                {
                    data[i] = newRow;
                    index = i;
                    found = true;
                    break;
                }
            }
            if (found) return index;
            MessageBox.Show("Sorry, data could not be updated. Please, try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return -1;
        }
    }
}
