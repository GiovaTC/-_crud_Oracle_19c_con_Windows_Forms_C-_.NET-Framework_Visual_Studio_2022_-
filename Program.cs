using System;
using System.Windows.Forms;

namespace crud_oracle_19c
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();

            Application.SetCompatibleTextRenderingDefault(false);
                
            Application.Run(new Form1());
        }
    }
}