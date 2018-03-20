namespace _3DGraphicsCompression
{
    using System;
    using System.Windows.Forms;
    using log4net;

    /// <summary>
    /// Main entry point of the application.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Main entry point of the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Log.Info("Start of the application");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CompressionForm());
        }
    }
}
