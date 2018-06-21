namespace Registration_v2.UI
{
    using System.Collections.Generic;
    using System.Windows.Controls;
    using System.Windows.Media.Media3D;
    using HelixToolkit.Wpf;
    using log4net;
    using Registration_v2.IO;

    /// <summary>
    /// Interakční logika pro HelixViewer.xaml
    /// </summary>
    public partial class HelixViewer : UserControl
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 3D viewport.
        /// </summary>
        private HelixViewport3D helixViewport;

        private List<string> fileNames;

        /// <summary>
        /// Initializes a new instance of the <see cref="HelixViewer"/> class.
        /// </summary>
        public HelixViewer()
        {
            this.InitializeComponent();
            this.Create3DViewport();
            this.fileNames = new List<string>();
        }

        /// <summary>
        /// Adds a new point cloud to the <see cref="helixViewport"/>.
        /// </summary>
        /// <param name="model">Point cloud that will be added.</param>
        public void AddModel(string model)
        {
            Log.Info("Adding a model to a viewer");
            FileReader reader = new FileReader();
            ModelVisual3D visual3D = new ModelVisual3D
            {
                Content = reader.ReadModel(model).Model
            };

            this.fileNames.Add(model);
            this.helixViewport.Children.Add(visual3D);
            this.helixViewport.ZoomExtents();
        }

        public void Remove(string model)
        {
            Log.Info("Removing a model from a viewer");
            this.helixViewport.Children.RemoveAt(this.fileNames.IndexOf(model) + 2);
            this.fileNames.Remove(model);
        }

        /// <summary>
        /// Creates viewport and sets lighting and camera.
        /// </summary>
        private void Create3DViewport()
        {
            Log.Info("Creating a viewer");
            this.helixViewport = new HelixViewport3D
            {
                ShowFrameRate = true,
                ShowCoordinateSystem = true,
                ModelUpDirection = new Vector3D(0, 1, 0)
            };

            DefaultLights lights = new DefaultLights();
            PerspectiveCamera camera = new PerspectiveCamera()
            {
                Position = new Point3D(0, 0, 0),
                LookDirection = new Vector3D(0, 0, -1)
            };

            this.helixViewport.Children.Add(lights);
            this.helixViewport.Camera = camera;
            this.AddChild(this.helixViewport);
        }
    }
}
