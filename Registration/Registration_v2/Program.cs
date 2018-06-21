namespace Registration_v2
{
    using System;
    using System.Windows.Forms;
    using Registration_v2.Data;
    using Registration_v2.UI;

    /// <summary>
    /// Entry point of the application.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Entry point of the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Model3DBean model3DBean = new Model3DBean();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(model3DBean));
        }
    }
}
