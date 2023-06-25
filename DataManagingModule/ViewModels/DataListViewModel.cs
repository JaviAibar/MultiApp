using DataManagingModule.Model;
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
using System.Windows.Resources;
using System.Windows.Shapes;

namespace DataManagingModule.ViewModels
{
    public class DataListViewModel : BindableBase
    {


        private ObservableCollection<Mill> _millData;
        public ObservableCollection<Mill> MillData
        {
            get { return _millData; }
            set { SetProperty(ref _millData, value); }
        }
        public DataListViewModel()
        {
        //Uri uriData = new Uri(@"pack://application:,,,/DataManagingModule;component/Database/mill.csv");
            Uri uriData = new Uri(@"pack://application:,,,/Database/mill.csv");
            // Se usa el reader que corresponda al tipo de archivo que deseemos cargar
            ReadCSV(uriData);
        }
        public List<Mill> ReadCSV(Uri uri)
        {
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
            return values;
        }
    }
}
