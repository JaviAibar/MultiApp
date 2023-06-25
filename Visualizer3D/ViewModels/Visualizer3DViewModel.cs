using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visualizer3D.ViewModels
{
    public class Visualizer3DViewModel : BindableBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public Visualizer3DViewModel()
        {
            Message = "Visualizer3DView from Visualizer3DModule";
        }
    }
}
