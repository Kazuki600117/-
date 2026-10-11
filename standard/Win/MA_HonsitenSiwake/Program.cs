using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Obic7fx.Win.M0000.Input.MA_HonsitenSiwake
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                if (Obic7fx.Win.A0000.Tool.Logic.UI.CheckFunction.MainCheck() == true)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    using (MA_HonsitenSiwake honsitensiwake = new MA_HonsitenSiwake(Obic7fx.Com.A0000.Tool.Logic.CurrentUser.JobID))
                    {
                        if (honsitensiwake.CheckExecute())
                        {
                            Application.Run(honsitensiwake);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(ex);
                Application.Exit();
            }
        }
    }

}