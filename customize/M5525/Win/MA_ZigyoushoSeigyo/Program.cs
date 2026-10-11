using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Obic7fx.Win.M5525.MasterM.MA_ZigyoushoSeigyo
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
                    using (MA_ZigyoushoSeigyoMasterMaintenance zigyoushoSeigyoMasterMaintenance = new MA_ZigyoushoSeigyoMasterMaintenance(Obic7fx.Com.A0000.Tool.Logic.CurrentUser.JobID))
                    {
                        if (zigyoushoSeigyoMasterMaintenance.CheckExecute())
                        {
                            Application.Run(zigyoushoSeigyoMasterMaintenance);
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