using System;
using System.Windows.Forms;

namespace TestProject
{
    /// <summary>
    /// 프로그램
    /// </summary>
    static class Program
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////// Method
        ////////////////////////////////////////////////////////////////////////////////////////// Static
        //////////////////////////////////////////////////////////////////////////////// Private

        #region 프로그램 시작하기 - Main()

        /// <summary>
        /// 프로그램 시작하기
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        #endregion
    }
}