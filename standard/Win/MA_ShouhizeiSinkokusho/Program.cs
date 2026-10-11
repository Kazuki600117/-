using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Obic7fx.Win.M0000.Print.MA_ShouhizeiSinkokusho
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
                    using (MA_ShouhizeiSinkokusho ShouhizeiSinkokusho = new MA_ShouhizeiSinkokusho(Obic7fx.Com.A0000.Tool.Logic.CurrentUser.JobID))
                    {
                        if (ShouhizeiSinkokusho.CheckExecute())
                        {
                            Application.Run(ShouhizeiSinkokusho);

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