using HelixToolkit.Wpf;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Resources;

namespace Visualizer3D.ViewModels
{
    public class Visualizer3DViewModel : BindableBase
    {
        private OpenFileDialog _fileDialog;
        private ObjReader _helixObjReader;
        private ModelImporter _modelImporter;

        // Backing fields
        private Model3D _selectedModel;

        // Properties
        public Model3D SelectedModel
        {
            get { return _selectedModel; }
            set { SetProperty(ref _selectedModel, value); }
        }

        // Delegate commands
        private DelegateCommand _loadDefaultModel;
        private DelegateCommand _openExplorer;

        private DelegateCommand _generateModel;
        public DelegateCommand GenerateModel =>
            _generateModel ?? (_generateModel = new DelegateCommand(ExecuteGenerateModel));


        // Delegate properties
        public DelegateCommand LoadDefaultModel =>
            _loadDefaultModel ?? (_loadDefaultModel = new DelegateCommand(ExecuteLoadDefaultModel));
        public DelegateCommand OpenExplorer =>
            _openExplorer ?? (_openExplorer = new DelegateCommand(ExecuteOpenExplorer));

        void ExecuteLoadDefaultModel()
        {
            Uri uriModel = new Uri(@"pack://application:,,,/Visualizer3D;component/Models3D/montserra.obj");
            StreamResourceInfo info = Application.GetResourceStream(uriModel);
            SelectedModel = null;
            SelectedModel = _helixObjReader.Read(info.Stream);
            // _helixObjReader.Read(uriModel.);
            //_modelImporter.Load(info.Stream.);
        }

        void ExecuteOpenExplorer()
        {
            if (_fileDialog.ShowDialog() == false) return;
            SelectedModel = null;
            //SelectedModel = _helixObjReader.Read(_fileDialog.FileName);
            SelectedModel = _modelImporter.Load(_fileDialog.FileName);

        }
        void ExecuteGenerateModel()
        {
            SelectedModel = GenerateDefaultModel();
        }


        public Visualizer3DViewModel()
        {
            _modelImporter = new ModelImporter();
            _helixObjReader = new ObjReader();
            _fileDialog = new OpenFileDialog();
            _fileDialog.Filter = "3D Models (*.obj)|*.obj|All files (*.*)|*.*";
        }

        public Model3D GenerateDefaultModel()
        {
            // Create a model group
            var modelGroup = new Model3DGroup();

            // Create a mesh builder and add a box to it
            var meshBuilder = new MeshBuilder(false, false);
            meshBuilder.AddBox(new Point3D(0, 0, 1), 1, 2, 0.5);
            meshBuilder.AddBox(new Rect3D(0, 0, 1.2, 0.5, 1, 0.4));

            // Create a mesh from the builder (and freeze it)
            var mesh = meshBuilder.ToMesh(true);

            // Create some materials
            var greenMaterial = MaterialHelper.CreateMaterial(Colors.Green);
            var redMaterial = MaterialHelper.CreateMaterial(Colors.Red);
            var blueMaterial = MaterialHelper.CreateMaterial(Colors.Blue);
            var insideMaterial = MaterialHelper.CreateMaterial(Colors.Yellow);

            // Add 3 models to the group (using the same mesh, that's why we had to freeze it)
            modelGroup.Children.Add(new GeometryModel3D { Geometry = mesh, Material = greenMaterial, BackMaterial = insideMaterial });
            modelGroup.Children.Add(new GeometryModel3D { Geometry = mesh, Transform = new TranslateTransform3D(-2, 0, 0), Material = redMaterial, BackMaterial = insideMaterial });
            modelGroup.Children.Add(new GeometryModel3D { Geometry = mesh, Transform = new TranslateTransform3D(2, 0, 0), Material = blueMaterial, BackMaterial = insideMaterial });

            // Set the property, which will be bound to the Content property of the ModelVisual3D (see MainWindow.xaml)
            return modelGroup;
        }

    }
}
