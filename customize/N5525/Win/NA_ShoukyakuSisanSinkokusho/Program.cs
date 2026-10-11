using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Obic7fx.Win.N5525.Print.NA_ShoukyakuSisanSinkokusho
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
                    using (NA_ShoukyakuSisanSinkokusho shoukyakuSisanSinkokusho = new NA_ShoukyakuSisanSinkokusho(Obic7fx.Com.A0000.Tool.Logic.CurrentUser.JobID))
                    {
                        if (shoukyakuSisanSinkokusho.CheckExecute())
                        {
                            Application.Run(shoukyakuSisanSinkokusho);
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