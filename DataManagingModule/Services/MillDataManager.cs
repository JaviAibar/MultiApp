using DataManagingModule.Interfaces;
using DataManagingModule.Model;
using MultiApp.Core;
using MultiApp.Core.Extensions;
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

namespace DataManagingModule.Services
{
    internal class MillDataManager : BindableBase, IDataManager<Mill>
    {
        private string _path;
        public Action unsavedChanged;
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

        private bool _unsavedChanges;
        /* public bool UnsavedChanges
         {
             get { return _unsavedChanges; }
             set { SetProperty(ref _unsavedChanges, value); }
         }*/
        public bool UnsavedChanges
        {
            get => _unsavedChanges; private set
            {
                _unsavedChanges = value;
                unsavedChanged?.Invoke();
            }
        }

        public MillDataManager() { }
        public MillDataManager(string path)
        {
            Path = path;
        }
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
            System.Diagnostics.Debug.WriteLine(string.Join(Constants.CsvDelimiter, Data.AsEnumerable()));
            File.WriteAllText(Path, Header);
            File.AppendAllText(Path, string.Join("\n", Data.AsEnumerable()));
        }

        private IEnumerable<Mill> ReadCSV()
        {
            /* METHOD WITH URI
            StreamResourceInfo info = Application.GetResourceStream(uri);
            // We change file extension here to make sure it's a .csv file.
            // TODO: Error checking.
            StreamReader sr = new StreamReader(info.Stream);
            if (sr.EndOfStream)
            {
                MessageBox.Show($"File {uri.AbsolutePath} it's empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            List<Mill> values = null;
            do
            {
                string[] data = sr.ReadLine().Split(';');
                // We return a person with the data in order.
                values.Add(
                    new Mill(data[0], double.Parse(data[1]), double.Parse(data[2]), double.Parse(data[3]), double.Parse(data[4]),
                    double.Parse(data[5]), double.Parse(data[6]), double.Parse(data[7]), double.Parse(data[8]), double.Parse(data[9]),
                    double.Parse(data[10]), double.Parse(data[11]), double.Parse(data[12]))
                );
            } while (!sr.EndOfStream);
            return values;*/

            string[] lines = File.ReadAllLines(System.IO.Path.ChangeExtension(Path, ".csv"));
            Header = lines[0] + "\n";
            string[] subarray = new string[lines.Length - 1];
            Array.Copy(lines, 1, subarray, 0, lines.Length - 1);
            // lines.Select allows me to project each line as a Person. 
            // This will give me an IEnumerable<Person> back.
            return subarray.Select(line =>
            {
                string[] data = line.Split(Constants.CsvDelimiter);
                // We return a person with the data in order.
                //return new Mill(data[0],
                //    double.Parse(string.IsNullOrEmpty(data[1]) ? "0" : data[1]),
                //    double.Parse(string.IsNullOrEmpty(data[2]) ? "0" : data[2]),
                //    double.Parse(string.IsNullOrEmpty(data[3]) ? "0" : data[3]),
                //    double.Parse(string.IsNullOrEmpty(data[4]) ? "0" : data[4]),
                //    double.Parse(string.IsNullOrEmpty(data[5]) ? "0" : data[5]),
                //    double.Parse(string.IsNullOrEmpty(data[6]) ? "0" : data[6]),
                //    double.Parse(string.IsNullOrEmpty(data[7]) ? "0" : data[7]),
                //    double.Parse(string.IsNullOrEmpty(data[8]) ? "0" : data[8]),
                //    double.Parse(string.IsNullOrEmpty(data[9]) ? "0" : data[9]),
                //    double.Parse(string.IsNullOrEmpty(data[10]) ? "0" : data[10]),
                //    double.Parse(string.IsNullOrEmpty(data[11]) ? "0" : data[11]),
                //    double.Parse(string.IsNullOrEmpty(data[12]) ? "0" : data[12]));
                return new Mill(data[0],
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
