//
//  2008/12/01  新規作成    S.Mino
//  [変更履歴]
//  2009/08/07  変更        K.Tajima    画面の[前年中に減少、取得したもののみを表示]を[前年中に減少、取得したもののみを印字]に変更
//  2012/11/05  変更        E.Sato      (N00970)eLTax対応
//  2014/02/21  削除        T.Sato      (7532)From-To対応
//  2017/12/27  修正        Y.Kunihiro  (86607)申告資産が０件となった自治体の課税台帳ファイルの「種類別明細書ファイル名」にファイル名がセットされる件の修正
//  2026/10/11  変更                    (5525)事業所別償却資産申告対応
//                                      N0000 標準画面を複製。ファイル出力で出力区分＝事業所別の場合、事業所ごとにファイルを分割して出力する
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//  2017/12/27  修正  Y.Kunihiro    (86607)申告資産が０件となった自治体の課税台帳ファイルの「種類別明細書ファイル名」にファイル名がセットされる件の修正 開始
using System.Linq;
//  2017/12/27  修正  Y.Kunihiro    (86607)申告資産が０件となった自治体の課税台帳ファイルの「種類別明細書ファイル名」にファイル名がセットされる件の修正 終了

using ComTool = Obic7fx.Com.A0000.Tool;
using ComToolLogic = Obic7fx.Com.A0000.Tool.Logic;
using WinTool = Obic7fx.Win.A0000.Tool;
using WinToolLogic = Obic7fx.Win.A0000.Tool.Logic;
using WinToolUI = Obic7fx.Win.A0000.Tool.UI;
using WinToolLogicUI = Obic7fx.Win.A0000.Tool.Logic.UI;
using WinToolM = Obic7fx.Win.M0000.Tool;
using WinToolSearchM = Obic7fx.Win.M0000.Tool.Search;
using WinToolSearchComM = Obic7fx.Win.M0000.Tool.SearchCom;
using WinToolSearchN = Obic7fx.Win.N0000.Tool.Search;
using WinToolSearchComN = Obic7fx.Win.N0000.Tool.SearchCom;

namespace Obic7fx.Win.N5525.Print.NA_ShoukyakuSisanSinkokusho
{
    /// <summary>
    /// 償却資産申告書
    /// </summary>
    public partial class NA_ShoukyakuSisanSinkokusho : Obic7fx.Win.M0000.Tool.UI.Obic7MBussinessFormBase
    {
        #region 変数

        //DB接続用クラス（ステートフル）
        private WinToolLogic.DBAccess businessDB;

        //オペレーション日付
        private DateTime operationDate;

        //前回保存用ジョブID
        private string zenkaiHozonJobID;

        //初期表示基準日
        private DateTime defaultHyouziKijunDate;

        //初期表示申告年度
        private DateTime defaultHyouziSinkokuNendo;

        //< 取得マスタ情報退避用変数 >
        //* 会計共通セキュリティ設定マスタの情報を退避 *
        //事業所セキュリティパターンコード
        private string zigyoushoSecurityPatternCD;
        //事業所セキュリティ状態区分
        private int zigyoushoSecurityJoutaiKBN;
        #endregion

        #region 定数

        //システム分類
        private const string SYSTEM_CATEGORY_CD = "N";
        //カスタマイズSEQ
        private const string CUSTOMAIZE_SEQ = "0000";
        //メッセージCD1
        private const string MESSAGECD1 = "000";

        //ストアド名
        //2012/11/05 E.Sato ST (N00970)eLTax対応
        //private const string PROCEDURE_KOTEISISANITIRAN_PRINT = "N0000PR_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Print";
        //  2026/10/11  変更  (5525)事業所別償却資産申告対応 開始
        //private const string PROCEDURE_SHOUKYAKUSISANSINKOKUSHO_PRINT = "N0000PR_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Print";
        //private const string PROCEDURE_SHURUIBETUMEISAISHO_PRINT = "N0000PR_Obic7fx_Win_N0000_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_Print";
        private const string PROCEDURE_SHOUKYAKUSISANSINKOKUSHO_PRINT = "N5525PR_Obic7fx_Win_N5525_Print_NA_ShoukyakuSisanSinkokusho_Print";
        private const string PROCEDURE_SHURUIBETUMEISAISHO_PRINT = "N5525PR_Obic7fx_Win_N5525_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_Print";

        /// <summary>
        /// ファイル分割用の事業所コード列名(ストアドが末尾に出力する。ファイルには出力しない)
        /// </summary>
        private const string COLUMN_BUNKATU_ZIGYOUSHOCD = "N5525_事業所コード";

        /// <summary>
        /// 出力区分＝全社の場合の事業所コード
        /// </summary>
        private const string ZIGYOUSHOCD_ZENSHA = "*";
        //  2026/10/11  変更  (5525)事業所別償却資産申告対応 終了
        //2012/11/05 E.Sato ED

        //ログ出力用ジョブID
        string LOG_JOBID = "Print.NA_ShoukyakuSisanSinkokusho";

        #endregion

        #region メソッド

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="jobID">ジョブID</param>
        public NA_ShoukyakuSisanSinkokusho(string jobID)
            : base(jobID)
        {
            //コンポーネント初期化
            InitializeComponent();
            //メインフォームフラグ
            base.isMainForm = true;
            //フォーム初期化
            InitializeForm();
        }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected NA_ShoukyakuSisanSinkokusho()
            : base()
        {
            InitializeComponent();
        }

        #endregion

        #region フォーム初期化

        /// <summary>
        /// フォーム各項目の初期化
        /// </summary>
        private void InitializeForm()
        {
            //DB接続クラスをインスタンス化
            businessDB = new WinToolLogic.DBAccess(SYSTEM_CATEGORY_CD, CUSTOMAIZE_SEQ, ComToolLogic.DBAccess.Obic7DBAccessDBCategory.Business);
            //履歴更新トリガを無効にする
            businessDB.IsNotUseTrigger = true;

            //ファンクションボタンセット
            SetFunctionButton();

            //プライベート変数を初期化
            SetVariableDefault();

            //画面のクリア
            ClearDisplay();
        }
        
        /// <summary>
        /// ファンクションボタン初期化
        /// </summary>
        private void SetFunctionButton()
        { 
            //F1
            base.obic7ToolStripButtonWithLogicF1.Text = F1.BLANK;
            base.obic7ToolStripButtonWithLogicF1.Obic7JobSecurity = ComTool.Obic7JobSecurities.NoSecurity;
            //F2
            base.obic7ToolStripButtonWithLogicF2.Text = F2.BLANK;
            base.obic7ToolStripButtonWithLogicF2.Obic7JobSecurity = ComTool.Obic7JobSecurities.NoSecurity;
            //F3
            base.obic7ToolStripButtonWithLogicF3.Text = F3.BLANK;
            base.obic7ToolStripButtonWithLogicF3.Obic7JobSecurity = ComTool.Obic7JobSecurities.NoSecurity;
            //F4 検索
            base.obic7ToolStripButtonWithLogicF4.Text = F4.SEARCH;
            base.obic7ToolStripButtonWithLogicF4.Obic7JobSecurity = ComTool.Obic7JobSecurities.NoSecurity;
            base.obic7ToolStripButtonWithLogicF4.Click += new EventHandler(obic7ToolStripButtonWithLogicF4_Click);
            //F5 条件解除
            base.obic7ToolStripButtonWithLogicF5.Text = F5.CANCEL_CONDITION;
            base.obic7ToolStripButtonWithLogicF5.Obic7JobSecurity = ComTool.Obic7JobSecurities.NoSecurity;
            base.obic7ToolStripButtonWithLogicF5.Click += new EventHandler(obic7ToolStripButtonWithLogicF5_Click);
            //F6 前回呼出
            base.obic7ToolStripButtonWithLogicF6.Text = "F6 前回呼出";
            base.obic7ToolStripButtonWithLogicF6.Obic7JobSecurity = ComTool.Obic7JobSecurities.NoSecurity;
            base.obic7ToolStripButtonWithLogicF6.Click += new EventHandler(obic7ToolStripButtonWithLogicF6_Click);
            //F7 印刷
            base.obic7ToolStripButtonWithLogicF7.Text = F7.PRINT;
            base.obic7ToolStripButtonWithLogicF7.Obic7JobSecurity = ComTool.Obic7JobSecurities.Print;
            base.obic7ToolStripButtonWithLogicF7.Click += new EventHandler(obic7ToolStripButtonWithLogicF7_Click);
            //F8
            base.obic7ToolStripButtonWithLogicF8.Text = F8.BLANK;
            base.obic7ToolStripButtonWithLogicF8.Obic7JobSecurity = ComTool.Obic7JobSecurities.NoSecurity;
            //F9 出力
            base.obic7ToolStripButtonWithLogicF9.Text = F9.BLANK;
            base.obic7ToolStripButtonWithLogicF9.Obic7JobSecurity = ComTool.Obic7JobSecurities.NoSecurity;
            //2012/11/05 E.Sato ST (N00970)eLTax対応
            ////F10
            //base.obic7ToolStripButtonWithLogicF10.Text = F10.BLANK;
            //base.obic7ToolStripButtonWithLogicF10.Obic7JobSecurity = ComTool.Obic7JobSecurities.NoSecurity;
            //F10
            base.obic7ToolStripButtonWithLogicF10.Text = F10.EXECUTE;
            base.obic7ToolStripButtonWithLogicF10.Obic7JobSecurity = ComTool.Obic7JobSecurities.FileOutput;
            base.obic7ToolStripButtonWithLogicF10.Click += new EventHandler(obic7ToolStripButtonWithLogicF10_Click);
            //2012/11/05 E.Sato ED
            //F11
            base.obic7ToolStripButtonWithLogicF11.Text = F11.BLANK;
            base.obic7ToolStripButtonWithLogicF11.Obic7JobSecurity = ComTool.Obic7JobSecurities.NoSecurity;
            //F12 閉じる
            base.obic7ToolStripButtonWithLogicF12.Text = F12.CLOSE;
            base.obic7ToolStripButtonWithLogicF12.Obic7JobSecurity = ComTool.Obic7JobSecurities.NoSecurity;

            //使用可否の設定
            base.SetFunctionButton(false, false, false, false, true, true, true, false, false, false, false, true);
        }

        /// <summary>
        /// プライベート変数の初期化
        /// </summary>
        private void SetVariableDefault()
        {
            //前回保存用ジョブID
            zenkaiHozonJobID = ComToolLogic.CurrentUser.JobID;
            //初期表示基準日
            defaultHyouziKijunDate = new DateTime();
        }

        #endregion

        #region 画面のクリア

        /// <summary>
        /// 画面のクリア
        /// </summary>
        private void ClearDisplay()
        {
            //項目をクリア

            //基準日
            this.obic7TextDatetimeKijunDate.Value = null;
            //資産区分
            this.obic7RadioButtonSisanKBN_HonSisanOnly.Checked = true;
            //表紙(作表条件)出力
            this.obic7CheckBoxSakuhyouJoukenSitei.Checked = false;
            //2012/11/05 E.Sato ST (N00970)eLTax対応
            //出力指定
            this.obic7RadioButtonShuturyokuSitei_Print.Checked = true;
            //2012/11/05 E.Sato ED
            //申告種類
            this.obic7RadioButtonSinkokuShurui_Densan.Checked = true;
            //評価額を印字
            this.obic7CheckBoxInzi_Hyoukagaku.Checked = true;
            //前年中に減少、取得したもののみを表示
            this.obic7CheckBoxInzi_Zennenchu.Checked = true;
            //用途
            this.obic7RadioButtonYouto_Teishutu.Checked = true;
            //出力区分
            this.obic7RadioButtonZigyoushoZensha.Checked = true;

            //提出年月日
            this.obic7TextDatetimeTeishutuDate.Text = "";
            //申告年度
            this.obic7TextDatetimeSinkokuNendo.Text = "";

            //申告自治体_開始
            this.obic7TextStringZititaiCDStart.Text = "";
            //申告自治体_開始_名
            this.obic7LabelStringZititaiNameStart.Text = "";
            //申告自治体_終了
            this.obic7TextStringZititaiCDEnd.Text = "";
            //申告自治体_終了_名
            this.obic7LabelStringZititaiNameEnd.Text = "";
            //事業所_開始
            this.obic7TextStringZigyoushoCDStart.Text = "";
            //事業所_開始_名
            this.obic7LabelStringZigyoushoNameStart.Text = "";
            //事業所_終了
            this.obic7TextStringZigyoushoCDEnd.Text = "";
            //事業所_終了_名
            this.obic7LabelStringZigyoushoNameEnd.Text = "";

            //2012/11/05 E.Sato ST (N00970)eLTax対応
            //格納先
            this.obic7TextStringOutPath.Text = "";
            //2012/11/05 E.Sato ED
        }

        #endregion

        #region 初期値

        #region コントロールマスタ情報取得

        /// <summary>
        /// コントロールマスタ情報取得
        /// </summary>
        private void GetControlInfo()
        {
            //固定資産コントロールマスタの情報を取得
            using (DataTable koteiSisanControlData = WinToolSearchComN.N0000MS_NA_KoteiSisanControl.SelectByKey(0, ""))
            {
                //初期表示基準日の取得
                if (ComTool.Function.IsValidDataTable(koteiSisanControlData) == true)
                {
                    if (koteiSisanControlData.Rows[0]["KijunDate"] == DBNull.Value)
                    {
                        //基準日がNULLの場合、システム日付をセット
                        defaultHyouziKijunDate = (DateTime)businessDB.ExecuteScalar("SELECT GETDATE()");
                    }
                    else
                    {
                        //データがある場合、コントロールマスタの基準日をセット
                        defaultHyouziKijunDate = (DateTime)koteiSisanControlData.Rows[0]["KijunDate"];
                    }
                }
                else
                {
                    //データがない場合、、メッセージを表示し終了する
                    WinToolLogicUI.Obic7MessageBox.Show(SYSTEM_CATEGORY_CD, CUSTOMAIZE_SEQ, MESSAGECD1, "00160", null, null, this.GetType()
                                   , WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                   , WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                   );
                    this.Close();
                }
            }
        }

        #endregion

        #region 地方税コントロールマスタ情報取得

        /// <summary>
        /// 地方税コントロールマスタ情報取得
        /// </summary>
        private void GetTihouzeiControlInfo()
        {
            //固定資産コントロールマスタの情報を取得
            using (DataTable tihouzeiControl = WinToolSearchComN.N0000MS_NA_TihouzeiControl.SelectByKey(0, ""))
            {
                //初期表示基準日の取得
                if (ComTool.Function.IsValidDataTable(tihouzeiControl) == true)
                {
                    //コントロールマスタの基準日をセット
                    defaultHyouziSinkokuNendo = (DateTime)tihouzeiControl.Rows[0]["SinkokuNendo"];
                }
            }
        }

        #endregion

        #region セキュリティ情報取得

        /// <summary>
        /// セキュリティ情報取得
        /// </summary>
        private void GetSecurityInfo()
        {
            //事業所セキュリティを取得
            using (DataTable zigyoushoSecurityData = WinToolSearchComM.M0000MS_MA_Security.GetZigyoushoSecurity(ComToolLogic.CurrentUser.UserID))
            {
                //事業所セキュリティのパターンコードを変数にセット
                zigyoushoSecurityPatternCD = ComTool.Function.AnyToString(zigyoushoSecurityData.Rows[0]["SecurityPatternCD"]);

                //事業所セキュリティの状態区分を変数にセット
                zigyoushoSecurityJoutaiKBN = ComTool.Function.AnyToInt32(zigyoushoSecurityData.Rows[0]["SecurityJoutaiKBN"]);
            }
        }

        #endregion

        #region 画面初期値設定

        /// <summary>
        /// 画面の各項目に初期値をセット
        /// </summary>
        private void SetDisplayDefault()
        {
            //基準日セット
            this.obic7TextDatetimeKijunDate.Value = defaultHyouziKijunDate;

            //資産区分
            //[本資産のみ]を ON
            this.obic7RadioButtonSisanKBN_HonSisanOnly.Checked = true;

            //申告種類
            //[電算申告]を ON
            this.obic7RadioButtonSinkokuShurui_Densan.Checked = true;

            //印字
            //評価額を印字
            this.obic7CheckBoxInzi_Hyoukagaku.Checked = true;
            //前年中に減少、取得したもののみを表示
            this.obic7CheckBoxInzi_Zennenchu.Checked = true;

            //用途
            //[提出用]を ON
            this.obic7RadioButtonYouto_Teishutu.Checked = true;

            //出力区分
            //[全社]を ON
            this.obic7RadioButtonZigyoushoZensha.Checked = true;

            //提出年月日の初期セット
            this.obic7TextDatetimeTeishutuDate.Value = (DateTime)businessDB.ExecuteScalar("SELECT GETDATE()");
            
            //申告年度セット
            this.obic7TextDatetimeSinkokuNendo.Value = defaultHyouziSinkokuNendo;

            //評価額を印字
            ControlLock(this.obic7CheckBoxInzi_Hyoukagaku, false, 1);
            //前年中に減少、取得したもののみを表示
            ControlLock(this.obic7CheckBoxInzi_Zennenchu, false, 1);
            //事業所_開始
            ControlLock(this.obic7TextStringZigyoushoCDStart, false, 1);
            ControlLock(this.obic7ButtonWithLogicSearchZigyoushoCDStart, false);
            //事業所_終了
            ControlLock(this.obic7TextStringZigyoushoCDEnd, false, 1);
            ControlLock(this.obic7ButtonWithLogicSearchZigyoushoCDEnd, false);
            
        }

        #endregion
        
        #endregion
        
        #region ファンクション

        #region F4（検索）の実行処理

        /// <summary>
        /// F4（検索）の実行処理
        /// </summary>
        private void F4Action()
        {
            //コントロール判定用-テキストボックス
            WinToolUI.Obic7TextAbstract textAbstract = null;

            //直前コントロール情報取得
            textAbstract = this.PrevControl as WinToolUI.Obic7TextAbstract;

            if (textAbstract != null)
            {
                //各項目ごとに検索処理
                if (SearchExecute((WinToolUI.Obic7TextAbstract)textAbstract))
                {
                    //フォーカスを次のコントロールに移動
                    this.SelectNextInputControl(true);

                    return;
                }
                else
                {
                    // キャンセルされた場合は直前コントロールにフォーカスセット
                    this.PrevControl.Focus();
                }
            }
        }

        #endregion

        #region F5（条件解除）の実処理

        /// <summary>
        /// F5（条件解除）の実処理
        /// </summary>
        private void F5Action()
        {
            //コントロール判定用
            WinToolUI.Obic7TextAbstract textAbstract = null;

            if (this.PrevControl is WinToolUI.Obic7TextAbstract)
            {
                //直前コントロールに対して検証イベントを実行したくないので、
                //CausesValidationプロパティを一時的にfalseにする
                textAbstract = this.PrevControl as WinToolUI.Obic7TextAbstract;
                textAbstract.CausesValidation = false;

                //フォーカスをダミー項目に逃がす
                this.obic7TextStringDummy.Focus();

                //直前コントロールのCausesValidationプロパティを元に戻す
                textAbstract.CausesValidation = true;
            }

            //画面のクリア
            ClearDisplay();

            //画面初期値設定
            SetDisplayDefault();

            //エラープロバイダの解除
            ClearErrorProvider();

            //初期フォーカスをセット
            SetDefaultFocus();
        }

        #endregion

        #region F6（前回呼出）の実処理

        /// <summary>
        /// F6（前回呼出）の実処理
        /// </summary>
        private void F6Action()
        {
            //コントロール判定用
            WinToolUI.Obic7TextAbstract textAbstract = null;

            if (this.PrevFocus is WinToolUI.Obic7TextAbstract)
            {
                //直前コントロールに対して検証イベントを実行したくないので、
                //CausesValidationプロパティを一時的にfalseにする
                textAbstract = this.PrevFocus as WinToolUI.Obic7TextAbstract;
                textAbstract.CausesValidation = false;

                //フォーカスをダミー項目に逃がす
                this.obic7TextStringDummy.Focus();

                //直前コントロールのCausesValidationプロパティを元に戻す
                textAbstract.CausesValidation = true;
            }

            //前回呼出
            ZenkaiYobidasi();

            if (textAbstract != null)
            {
                //ダミー項目から元の直前コントロールにフォーカスを戻す
                textAbstract.Focus();

                if (textAbstract.Focused == false)
                {
                    //フォーカスを戻せなかった場合

                    //初期フォーカスをセット
                    SetDefaultFocus();
                }
            }
        }

        #endregion

        #region F7（印刷）の実処理

        /// <summary>
        /// F7（印刷）の実処理
        /// </summary>
        private void F7Action()
        {
            //最終フォーカス項目の検証イベントを強制的に発生
            if (this.Validate())
            {
                //入力チェック
                //2012/11/05 E.Sato ST (N00970)eLTax対応
                //if (InputCheck())
                if (InputCheck(0))
                //2012/11/05 E.Sato ED
                {
                    //ログ出力を行う
                    OutPutReferenceLog();

                    //前回保存
                    ZenkaiHozon();

                    //ActiveReport出力
                    if (OutPutActiveReport() == false)
                    {
                        //終了処理
                        return;
                    }
                }
            }

            //終了処理
            return;
        }

        #endregion

        //2012/11/05 E.Sato ST (N00970)eLTax対応
        #region F10（実行）の実処理

        /// <summary>
        /// F10（実行）の実処理
        /// </summary>
        private void F10Action()
        {
            //最終フォーカス項目の検証イベントを強制的に発生
            if (this.Validate())
            {
                //入力チェック
                if (InputCheck(1))
                {
                    //確認メッセージ
                    if (WinToolLogicUI.Obic7MessageBox.Show(SYSTEM_CATEGORY_CD, CUSTOMAIZE_SEQ, MESSAGECD1, "00168", null, null, GetType(),
                                                         WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxButtons.YesNo,
                                                         WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1) == DialogResult.No)
                    {
                        //NOの場合
                        return;
                    };

                    //ログ出力を行う
                    OutPutReferenceLog();

                    //前回保存
                    ZenkaiHozon();

                    //ファイル出力
                    if (OutPutFile() == false)
                    {
                        //終了処理
                        return;
                    }

                    //完了メッセージの表示
                    WinToolLogicUI.Obic7MessageBox.Show(SYSTEM_CATEGORY_CD, CUSTOMAIZE_SEQ, MESSAGECD1, "00164", null, null, GetType(),
                                                         WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxButtons.OK,
                                                         WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1);
                }
            }

            //終了処理
            return;
        }

        #endregion
        //2012/11/05 E.Sato ED

        #endregion

        #region 出力メソッド

        #region ActiveReport出力

        /// <summary>
        /// ActiveReport出力
        /// </summary>
        /// <returns>true:エラーなし false:エラーあり</returns>
        private bool OutPutActiveReport()
        {
            int errorKBN = 0;
            SqlParameter errorCD1 = null;
            SqlParameter errorCD2 = null;
            SqlParameter errorProcName = null;
            SqlParameter errorSummary = null;
            SqlParameter errorDetail = null;
            SqlParameter errorBtnKBN = null;
            SqlParameter errorBtnDefault = null;
            string[] errorMsgDetail = new string[3];

            //件数
            int? kensuuCount = 0;

            //ActiveReportクラス
            Obic7fx.Win.N0000.Report.NA_ShoukyakuSisanSinkokusho.NA_ShoukyakuSisanSinkokusho reportShoukyakuSisanSinkokusho;

            //印刷用クラス
            Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print printClass;


            //ActiveReport印刷用のクラスのインスタンスを作成する
            using (reportShoukyakuSisanSinkokusho = new Obic7fx.Win.N0000.Report.NA_ShoukyakuSisanSinkokusho.NA_ShoukyakuSisanSinkokusho())
            {
                //レポートクラスのデータ抽出
                kensuuCount = reportShoukyakuSisanSinkokusho.DataRead(
                                                           ref errorKBN
                                                         , ref errorCD1
                                                         , ref errorCD2
                                                         , ref errorProcName
                                                         , ref errorSummary
                                                         , ref errorDetail
                                                         , ref errorBtnKBN
                                                         , ref errorBtnDefault
                                                         , ref errorMsgDetail
                                                         , 1 //処理区分
                                                         , this.obic7TextDatetimeKijunDate.Value //基準日
                                                         , GetSisanKBNSiteiNo() //資産区分
                                                         , GetSisanKBNSiteiName() //資産区分名
                                                         , GetSakuhyouJoukenSiteiNo() //表紙(作表条件)出力
                                                         , GetSinkokuShuruiSiteiNo() //申告種類
                                                         , GetSinkokuShuruiSiteiName() //申告種類名
                                                         , GetHyoukagakuSiteiNo() //[評価額を印字]
                                                         , GetZennenchuSiteiNo() //[前年中に減少、取得したもののみを表示]
                                                         , GetYoutoSiteiNo() //用途
                                                         , GetYoutoSiteiName() //用途名
                                                         , GetZigyoushoSiteiNo() //出力区分
                                                         , GetZigyoushoSiteiName() //出力区分名
                                                         , this.obic7TextDatetimeTeishutuDate.Value //提出年月日
                                                         , this.obic7TextDatetimeSinkokuNendo.Value //申告年度
                                                         , this.obic7TextDatetimeSinkokuNendo.Text //申告年度(表紙用)
                                                         , this.obic7TextStringZititaiCDStart.Text //申告自治体_開始
                                                         , this.obic7TextStringZititaiCDEnd.Text //申告自治体_ 終了
                                                         , this.obic7LabelStringZititaiNameStart.Text //申告自治体名_開始
                                                         , this.obic7LabelStringZititaiNameEnd.Text //申告自治体名_終了
                                                         , this.obic7TextStringZigyoushoCDStart.Text //事業所_開始
                                                         , this.obic7TextStringZigyoushoCDEnd.Text //事業所_終了
                                                         , this.obic7LabelStringZigyoushoNameStart.Text //事業所名_開始
                                                         , this.obic7LabelStringZigyoushoNameEnd.Text //事業所名_終了
                                                         , zigyoushoSecurityPatternCD //事業所セキュリティパターンコード
                                                        );

                if (kensuuCount == null)
                {
                    //【ストアドにて業務ロジック以外のエラー（システムエラー）発生時は、ここで例外を処理する。】

                    //メッセージボックス表示
                    WinToolLogicUI.Obic7MessageBox.Show(SYSTEM_CATEGORY_CD, CUSTOMAIZE_SEQ, MESSAGECD1, "00119", null, errorMsgDetail, GetType(),
                                                         WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxButtons.OK,
                                                         WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1);

                    return false;
                }

                if (kensuuCount == 0)
                {
                    //システムエラー発生時
                    //（留意事項）エラー発生時、ストアドの「ERR_SHORI」ブロックで処理された結果のパラメータは、C＃側で参照することが出来ません。
                    //            よってこの部分が処理されることはありませんが、明示的に共通部品の例外を発生させる例として記載しておきます。
                    if (errorKBN == 1)
                    {
                        //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                        //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                        throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = "
                                                           + errorProcName.Value.ToString() + " エラー内容 = " + errorSummary.Value.ToString());
                    }

                    //個別エラー発生時
                    if (errorKBN == 2)
                    {
                        //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                        ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);

                        return false;
                    }

                    //データがなければエラー処理
                    WinToolLogicUI.Obic7MessageBox.Show(SYSTEM_CATEGORY_CD, CUSTOMAIZE_SEQ, MESSAGECD1, "00107", null, null, this.GetType()
                                                        , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                        , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                        );
                    return false;
                }

                if (kensuuCount >= 1)
                {
                    //検索結果が存在すれば印刷処理を行う
                    printClass = new WinToolLogic.Print.Obic7Print();
                    //印刷用クラスInitialize
                    printClass.PrintInitialize(reportShoukyakuSisanSinkokusho);
                    //印刷ダイアログ表示
                    printClass.ShowPrintMenu();
                }
            }

            return true;
        }

        #endregion

        //2012/11/05 E.Sato ST (N00970)eLTax対応
        #region ファイル出力

        /// <summary>
        /// ファイル出力
        /// </summary>
        /// <returns>true:エラーなし false:エラーあり</returns>
        private bool OutPutFile()
        {
            int errorKBN = 0;
            SqlParameter errorCD1 = null;
            SqlParameter errorCD2 = null;
            SqlParameter errorProcName = null;
            SqlParameter errorSummary = null;
            SqlParameter errorDetail = null;
            SqlParameter errorBtnKBN = null;
            SqlParameter errorBtnDefault = null;
            string[] errorMsgDetail = new string[3];

            DataTable dtHead = new DataTable();
            DataTable dtMeisai = new DataTable();
            List<string> fileList = new List<string>();

            //帳票出力用データを作成する。(課税台帳)
            dtHead = this.DataReadHead(
                   ref errorKBN
                 , ref errorCD1
                 , ref errorCD2
                 , ref errorProcName
                 , ref errorSummary
                 , ref errorDetail
                 , ref errorBtnKBN
                 , ref errorBtnDefault
                 , ref errorMsgDetail
                 , 1 //処理区分
                 , this.obic7TextDatetimeKijunDate.Value //基準日
                 , GetSisanKBNSiteiNo() //資産区分
                 , GetSisanKBNSiteiName() //資産区分名
                 , GetSakuhyouJoukenSiteiNo() //表紙(作表条件)出力
                 , GetSinkokuShuruiSiteiNo() //申告種類
                 , GetSinkokuShuruiSiteiName() //申告種類名
                 , GetHyoukagakuSiteiNo() //[評価額を印字]
                 , GetZennenchuSiteiNo() //[前年中に減少、取得したもののみを表示]
                 , GetYoutoSiteiNo() //用途
                 , GetYoutoSiteiName() //用途名
                 , GetZigyoushoSiteiNo() //出力区分
                 , GetZigyoushoSiteiName() //出力区分名
                 , this.obic7TextDatetimeTeishutuDate.Value //提出年月日
                 , this.obic7TextDatetimeSinkokuNendo.Value //申告年度
                 , this.obic7TextDatetimeSinkokuNendo.Text //申告年度(表紙用)
                 , this.obic7TextStringZititaiCDStart.Text //申告自治体_開始
                 , this.obic7TextStringZititaiCDEnd.Text //申告自治体_ 終了
                 , this.obic7LabelStringZititaiNameStart.Text //申告自治体名_開始
                 , this.obic7LabelStringZititaiNameEnd.Text //申告自治体名_終了
                 , this.obic7TextStringZigyoushoCDStart.Text //事業所_開始
                 , this.obic7TextStringZigyoushoCDEnd.Text //事業所_終了
                 , this.obic7LabelStringZigyoushoNameStart.Text //事業所名_開始
                 , this.obic7LabelStringZigyoushoNameEnd.Text //事業所名_終了
                 , zigyoushoSecurityPatternCD //事業所セキュリティパターンコード
            );
            if (dtHead == null)
            {
                //個別エラー発生時
                if (errorKBN == 2)
                {
                    //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                    ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);

                    return false;
                }
                return false;
            }


            //帳票出力用データを作成する。(種類別明細)
            //レポートクラスのデータ抽出
            dtMeisai = this.DataReadMeisai(
                   ref errorKBN
                 , ref errorCD1
                 , ref errorCD2
                 , ref errorProcName
                 , ref errorSummary
                 , ref errorDetail
                 , ref errorBtnKBN
                 , ref errorBtnDefault
                 , ref errorMsgDetail
                 , 1 //処理区分
                 , this.obic7TextDatetimeKijunDate.Value //基準日
                 , GetSisanKBNSiteiNo() //資産区分
                 , GetSisanKBNSiteiName() //資産区分名
                 , GetSakuhyouJoukenSiteiNo() //表紙(作表条件)出力
                 , 0//GetSinkokuKBNSiteiNo() //申告区分
                 , ""//GetSinkokuKBNSiteiName() //申告区分名
                 , GetYoutoSiteiNo() //用途
                 , GetYoutoSiteiName() //用途名
                 , 1//GetSisanCDSiteiNo() //[資産コードを印字]
                 , 1//GetkagakuSiteiNo() // [価額を印字]
                 , 1//GetKazeiHyoujunGakuSiteiNo() //[課税標準額を印字]
                 , GetZigyoushoSiteiNo() //出力区分
                 , GetZigyoushoSiteiName() //出力区分名
                 , this.obic7TextDatetimeSinkokuNendo.Value //申告年度
                 , this.obic7TextDatetimeSinkokuNendo.Text //申告年度(表紙用)
                 , this.obic7TextStringZititaiCDStart.Text //申告自治体_開始
                 , this.obic7TextStringZititaiCDEnd.Text //申告自治体_ 終了
                 , this.obic7LabelStringZititaiNameStart.Text //申告自治体名_開始
                 , this.obic7LabelStringZititaiNameEnd.Text //申告自治体名_終了
                 , this.obic7TextStringZigyoushoCDStart.Text //事業所_開始
                 , this.obic7TextStringZigyoushoCDEnd.Text //事業所_終了
                 , this.obic7LabelStringZigyoushoNameStart.Text //事業所名_開始
                 , this.obic7LabelStringZigyoushoNameEnd.Text //事業所名_終了
                 , zigyoushoSecurityPatternCD //事業所セキュリティパターンコード
                 );
            if (dtMeisai == null)
            {
                //個別エラー発生時
                if (errorKBN == 2)
                {
                    //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                    ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);

                    return false;
                }
                return false;
            }

            //該当データが無い場合
            //  2017/12/27  修正  Y.Kunihiro    (86607)申告資産が０件となった自治体の課税台帳ファイルの「種類別明細書ファイル名」にファイル名がセットされる件の修正 開始
            //if (!ComTool.Function.IsValidDataTable(dtHead)
            //    || !ComTool.Function.IsValidDataTable(dtMeisai))
            if (!ComTool.Function.IsValidDataTable(dtHead))
            //  2017/12/27  修正  Y.Kunihiro    (86607)申告資産が０件となった自治体の課税台帳ファイルの「種類別明細書ファイル名」にファイル名がセットされる件の修正 開始
            {
                //データがなければエラー処理
                WinToolLogicUI.Obic7MessageBox.Show(SYSTEM_CATEGORY_CD, CUSTOMAIZE_SEQ, MESSAGECD1, "00107", null, null, this.GetType()
                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                    );
                return false;
            }

            //  2026/10/11  変更  (5525)事業所別償却資産申告対応 開始
            //出力単位(事業所コード)の一覧を作成する。全社の場合は"*"の1件のみ
            List<string> zigyoushoCDList = dtHead.AsEnumerable()
                                                 .Select(row => row[COLUMN_BUNKATU_ZIGYOUSHOCD].ToString())
                                                 .Distinct()
                                                 .OrderBy(cd => cd, StringComparer.Ordinal)
                                                 .ToList();

            //ファイルチェックを行う。(出力するファイル先に同一ファイル名が存在するかチェック)
            {
                //今回出力するファイル名をリスト化する。
                StringBuilder existsFile = new StringBuilder();

                foreach (string zigyoushoCD in zigyoushoCDList)
                {
                    //課税台帳(データが存在する出力単位のみ)
                    fileList.Add(GetKazeiDaichouFileName(zigyoushoCD));

                    //種類別明細書(データが存在する出力単位のみ)
                    if (ExistsZigyoushoData(dtMeisai, zigyoushoCD))
                    {
                        fileList.Add(GetMeisaishoFileName(zigyoushoCD));
                    }
                }

                //存在チェック
                for (int i = 0; i < fileList.Count; i++)
                {
                    if (System.IO.File.Exists(this.obic7TextStringOutPath.Value + fileList[i]))
                    {
                        existsFile.AppendLine(fileList[i]);
                    }
                }

                //既に存在するファイルがある場合、警告メッセージ
                if (!String.IsNullOrEmpty(existsFile.ToString()))
                {
                    object[] obj = new object[1];
                    obj[0] = existsFile.ToString();
                    if (WinToolLogicUI.Obic7MessageBox.Show(SYSTEM_CATEGORY_CD, CUSTOMAIZE_SEQ, MESSAGECD1, "03176", null, obj, GetType(),
                                     WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxButtons.YesNo,
                                     WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1) == DialogResult.No)
                    {
                        //NOの場合
                        return false;
                    };
                }
            }

            //ファイル出力(出力単位ごと)
            {
                // FileIOクラス
                Obic7fx.Win.A0000.Tool.Logic.UI.FileIO csvFileIO;
                csvFileIO = new Obic7fx.Win.A0000.Tool.Logic.UI.FileIO();

                foreach (string zigyoushoCD in zigyoushoCDList)
                {
                    DataTable dtHeadZigyousho = ExtractZigyoushoData(dtHead, zigyoushoCD);
                    DataTable dtMeisaiZigyousho = ExtractZigyoushoData(dtMeisai, zigyoushoCD);
                    string kazeiDaichouFileName = GetKazeiDaichouFileName(zigyoushoCD);
                    string meisaishoFileName = GetMeisaishoFileName(zigyoushoCD);

                    //データテーブル(種類別明細)の「提出先市町村コード」を自治体コードとして取得する。
                    var dtZititaiCD = dtMeisaiZigyousho.AsEnumerable().Select(row => row["提出先市町村コード"].ToString()).Distinct().ToList();

                    //課税台帳ファイルの出力
                    //明細書ファイル名をセット(種類別明細にデータが存在する自治体のみ)
                    for (int i = 0; i < dtHeadZigyousho.Rows.Count; i++)
                    {
                        if (dtZititaiCD.Contains(dtHeadZigyousho.Rows[i]["提出先市町村コード"].ToString()))
                        {
                            dtHeadZigyousho.Rows[i]["種類別明細書ファイル名"] = meisaishoFileName;
                        }
                    }
                    dtHeadZigyousho.Columns.Remove(COLUMN_BUNKATU_ZIGYOUSHOCD);
                    dtHeadZigyousho.AcceptChanges();
                    csvFileIO.FileData = dtHeadZigyousho;
                    if (csvFileIO.WriteText(this.obic7TextStringOutPath.Value + kazeiDaichouFileName
                                        , ','
                                        , WinToolLogicUI.FileIO.Obic7FileIOCodePage.ShiftJis
                                        , true
                                        )
                        == false)
                    {
                        return false;
                    }

                    //明細書ファイルの出力
                    if (ComTool.Function.IsValidDataTable(dtMeisaiZigyousho))
                    {
                        dtMeisaiZigyousho.Columns.Remove(COLUMN_BUNKATU_ZIGYOUSHOCD);
                        dtMeisaiZigyousho.AcceptChanges();
                        csvFileIO.FileData = dtMeisaiZigyousho;
                        if (csvFileIO.WriteText(this.obic7TextStringOutPath.Value + meisaishoFileName
                                            , ','
                                            , WinToolLogicUI.FileIO.Obic7FileIOCodePage.ShiftJis
                                            , true
                                            )
                            == false)
                        {
                            return false;
                        }
                    }
                }
            }
            //  2026/10/11  変更  (5525)事業所別償却資産申告対応 終了

            return true;
        }

        #endregion
        //2012/11/05 E.Sato ED

        //  2026/10/11  変更  (5525)事業所別償却資産申告対応 開始
        #region ファイル分割

        /// <summary>
        /// 課税台帳ファイル名を取得
        /// </summary>
        /// <param name="zigyoushoCD">事業所コード(全社の場合は"*")</param>
        /// <returns>課税台帳ファイル名</returns>
        private string GetKazeiDaichouFileName(string zigyoushoCD)
        {
            return GetFileNamePrefix(zigyoushoCD) + "_課税台帳.inf";
        }

        /// <summary>
        /// 種類別明細書ファイル名を取得
        /// </summary>
        /// <param name="zigyoushoCD">事業所コード(全社の場合は"*")</param>
        /// <returns>種類別明細書ファイル名</returns>
        private string GetMeisaishoFileName(string zigyoushoCD)
        {
            return GetFileNamePrefix(zigyoushoCD) + "_明細書.csv";
        }

        /// <summary>
        /// 出力ファイル名の共通部分を取得
        /// 全社　：[申告年度]年度_[会社NO]
        /// 事業所：[申告年度]年度_[会社NO]_[事業所コード]
        /// </summary>
        /// <param name="zigyoushoCD">事業所コード(全社の場合は"*")</param>
        /// <returns>出力ファイル名の共通部分</returns>
        private string GetFileNamePrefix(string zigyoushoCD)
        {
            string prefix = this.obic7TextDatetimeSinkokuNendo.Text + "年度_" + ComToolLogic.CurrentUser.CompanyNO.ToString();

            if (zigyoushoCD != ZIGYOUSHOCD_ZENSHA)
            {
                prefix = prefix + "_" + zigyoushoCD;
            }

            return prefix;
        }

        /// <summary>
        /// 指定した事業所のデータが存在するか
        /// </summary>
        /// <param name="data">ストアドの出力結果</param>
        /// <param name="zigyoushoCD">事業所コード(全社の場合は"*")</param>
        /// <returns>true:存在する、false:存在しない</returns>
        private bool ExistsZigyoushoData(DataTable data, string zigyoushoCD)
        {
            if (!ComTool.Function.IsValidDataTable(data))
            {
                return false;
            }

            return data.AsEnumerable().Any(row => row[COLUMN_BUNKATU_ZIGYOUSHOCD].ToString() == zigyoushoCD);
        }

        /// <summary>
        /// 指定した事業所のデータを抽出する(列構成は元のデータテーブルと同じ)
        /// </summary>
        /// <param name="data">ストアドの出力結果</param>
        /// <param name="zigyoushoCD">事業所コード(全社の場合は"*")</param>
        /// <returns>指定した事業所のデータ</returns>
        private DataTable ExtractZigyoushoData(DataTable data, string zigyoushoCD)
        {
            DataTable result = data.Clone();

            foreach (DataRow row in data.Rows)
            {
                if (row[COLUMN_BUNKATU_ZIGYOUSHOCD].ToString() == zigyoushoCD)
                {
                    result.ImportRow(row);
                }
            }

            return result;
        }

        #endregion
        //  2026/10/11  変更  (5525)事業所別償却資産申告対応 終了

        #region ログを出力

        /// <summary>
        /// ログを出力
        /// </summary>
        private void OutPutReferenceLog()
        {
            //2012/11/05 E.Sato ST (N00970)eLTax対応
            ////メッセージ詳細のパラメータ用
            //object[] detailParams = new object[14];
            //メッセージ詳細のパラメータ用
            object[] detailParams = new object[16];
            //2012/11/05 E.Sato ED

            //基準日
            detailParams[0] = this.obic7TextDatetimeKijunDate.Value == null ? "" : ((DateTime)this.obic7TextDatetimeKijunDate.Value).ToString("yyyy/MM/dd");

            //資産区分
            detailParams[1] = GetSisanKBNSiteiName();
            //表紙(作表条件)出力
            detailParams[2] = this.obic7CheckBoxSakuhyouJoukenSitei.Checked.ToString();
            //申告種類
            detailParams[3] = GetSinkokuShuruiSiteiName();
            //評価額を印字
            detailParams[4] = this.obic7CheckBoxInzi_Hyoukagaku.Checked.ToString(); 
            //前年中に減少、取得したもののみを表示
            detailParams[5] = this.obic7CheckBoxInzi_Zennenchu.Checked.ToString();
            //用途
            detailParams[6] = GetYoutoSiteiName();
            //出力区分
            detailParams[7] = GetZigyoushoSiteiName();
            //提出年月日
            detailParams[8] = this.obic7TextDatetimeTeishutuDate.Text;
            //申告年度
            detailParams[9] = this.obic7TextDatetimeSinkokuNendo.Text;
            //申告自治体_開始
            detailParams[10] = this.obic7TextStringZititaiCDStart.Text;
            //申告自治体_終了
            detailParams[11] = this.obic7TextStringZititaiCDEnd.Text;
            //事業所_開始
            detailParams[12] = this.obic7RadioButtonZigyousho.Checked == true ? this.obic7TextStringZigyoushoCDStart.Text : "";
            //事業所_終了
            detailParams[13] = this.obic7RadioButtonZigyousho.Checked == true ? this.obic7TextStringZigyoushoCDEnd.Text : "";
            //2012/11/05 E.Sato ST (N00970)eLTax対応
            //出力指定
            detailParams[14] = this.obic7RadioButtonShuturyokuSitei_Print.Checked == true ? "印刷" : "ファイル出力";
            //格納先
            detailParams[15] = this.obic7RadioButtonShuturyokuSitei_Print.Checked == true ? "" : this.obic7TextStringOutPath.Value;
            //2012/11/05 E.Sato ED

            //FixedLogを出力する
            ComToolLogic.Log.FixedOutPut(SYSTEM_CATEGORY_CD, CUSTOMAIZE_SEQ, LOG_JOBID, "DataReference", null, detailParams, this.GetType());
        }

        #endregion

        #endregion

        #region DBアクセス系

        #region ワーク削除

        /// <summary>
        /// ワーク削除
        /// </summary>
        /// <param name="workName">削除対象のワークテーブル</param>
        private bool DeleteWorkTable(string workName)
        {
            //チェック結果
            bool result = true;

            //SQL編集用
            StringBuilder sb = new StringBuilder();

            sb.Append(" DELETE FROM " + workName + " ");
            sb.Append(" WHERE ");
            sb.Append("     OperationComputerName = " + ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.ComputerName));
            sb.Append(" AND OperationUserID       = " + ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.UserID));
            sb.Append(" AND OperationJobID        = " + ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.JobID));

            //SQL実行
            result = SQLExecuteNonQuery(ComTool.Function.AnyToString(sb), false);

            return result;
        }

        #endregion

        #endregion

        #region 入力チェック

        #region 入力チェック(メイン)

        //2012/11/05 E.Sato ST (N00970)eLTax対応
        ///// <summary>
        ///// 入力チェック(メイン)
        ///// </summary>
        ///// <returns>true:エラーなし false:エラーあり</returns>
        //private bool InputCheck()
        /// <summary>
        /// 入力チェック(メイン)
        /// </summary>
        /// <param name="shoriKBN">0:印刷 1:実行</param>
        /// <returns>true:エラーなし false:エラーあり</returns>
        private bool InputCheck(int shoriKBN)
        //2012/11/05 E.Sato ED
        {
            //チェック結果
            bool result = true;

            //フォーカス移動 要・不要フラグ
            bool isNeedFocusSet = true;

            //エラープロバイダの初期化
            ClearErrorProvider();

            //未入力チェック
            //2012/11/05 E.Sato ST (N00970)eLTax対応
            //if (!MinyuryokuCheck(isNeedFocusSet))
            if (!MinyuryokuCheck(isNeedFocusSet, shoriKBN))
            //2012/11/05 E.Sato ED
            {
                WinToolLogicUI.Obic7MessageBox.Show(SYSTEM_CATEGORY_CD, CUSTOMAIZE_SEQ, MESSAGECD1, "00103", null, null, this.GetType()
                                                   , WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                   , WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                   );
                return false;
            }

            //大小チェック
            if (!DaishouCheck(isNeedFocusSet))
            {
                return false;
            }

            //マスタチェック
            if (!MasterCheck())
            {
                return false;
            }

            return result;
        }

        #endregion

        #region 未入力チェック

        //2012/11/05 E.Sato ST (N00970)eLTax対応
        ///// <summary>
        ///// 明細部未入力チェック
        ///// </summary>
        ///// <param name="isNeedFocusSet">フォーカスセット要・不要フラグ</param>
        ///// <returns>true:エラーなし false:エラーあり</returns>
        //private bool MinyuryokuCheck(bool isNeedFocusSet)
        /// <summary>
        /// 明細部未入力チェック
        /// </summary>
        /// <param name="isNeedFocusSet">フォーカスセット要・不要フラグ</param>
        /// <param name="shoriKBN">0:印刷 1:実行</param>
        /// <returns>true:エラーなし false:エラーあり</returns>
        private bool MinyuryokuCheck(bool isNeedFocusSet, int shoriKBN)
        //2012/11/05 E.Sato ED
        {
            //チェック結果
            bool result = true;

            //基準日
            if (!InputItemMinyuryokuCheck(this.obic7TextDatetimeKijunDate, isNeedFocusSet, 0))
            {
                isNeedFocusSet = false;
                result = false;
            }

            //申告年度
            if (!InputItemMinyuryokuCheck(this.obic7TextDatetimeSinkokuNendo, isNeedFocusSet, 0))
            {
                isNeedFocusSet = false;
                result = false;
            }

            //2012/11/05 E.Sato ST (N00970)eLTax対応
            //実行時のみ
            if (shoriKBN == 1)
            {
                //格納先
                if (!InputItemMinyuryokuCheck(this.obic7TextStringOutPath, isNeedFocusSet, 0))
                {
                    isNeedFocusSet = false;
                    result = false;
                }
            }
            //2012/11/05 E.Sato ED

            return result;
        }

        #endregion

        #region 大小チェック

        /// <summary>
        /// 明細部大小チェック
        /// </summary>
        /// <param name="isNeedFocusSet">フォーカスセット要・不要フラグ</param>
        /// <returns>true:エラーなし false:エラーあり</returns>
        private bool DaishouCheck(bool isNeedFocusSet)
        {
            //チェック結果
            bool result = true;

            //基本抽出条件

            //自治体
            if (!InputItemDaishouCheck(this.obic7TextStringZititaiCDStart, this.obic7TextStringZititaiCDEnd, isNeedFocusSet, 0))
            {
                isNeedFocusSet = false;
                result = false;
            }

            //出力区分出力別を選択時
            if (this.obic7RadioButtonZigyousho.Checked)
            {
                //事業所
                if (!InputItemDaishouCheck(this.obic7TextStringZigyoushoCDStart, this.obic7TextStringZigyoushoCDEnd, isNeedFocusSet, 0))
                {
                    isNeedFocusSet = false;
                    result = false;
                }
            }

            return result;
        }

        #endregion

        #region マスタチェック

        /// <summary>
        /// 明細部マスタチェック
        /// </summary>
        /// <returns>true:エラーなし false:エラーあり</returns>
        private bool MasterCheck()
        {
            return true;
        }

        #endregion

        #endregion

        #region 項目別入力チェック

        #region 事業所のチェック

        /// <summary>
        /// 事業所のチェック
        /// </summary>
        /// <param name="zigyoushoCD">事業所コード</param>
        /// <param name="zigyoushoName">事業所名</param>
        /// <param name="isNeedErrorShori">true:エラー処理あり false:エラー処理なし</param>
        /// <returns></returns>
        private bool CheckZigyousho(WinToolUI.Obic7TextString zigyoushoCD, WinToolUI.Obic7LabelString zigyoushoName, bool isNeedErrorShori)
        {
            //名称のクリア
            zigyoushoName.Text = "";

            if (!string.IsNullOrEmpty(zigyoushoCD.Text))
            {
                //未入力でない場合

                //基準日が未入力の場合、処理終了
                if (string.IsNullOrEmpty(ComTool.Function.AnyToString(this.obic7TextDatetimeKijunDate.Text)))
                {
                    return true;
                }
                //基準日が日付に変換不可能な場合、処理終了
                DateTime dtwork = new DateTime();
                if (!DateTime.TryParse(this.obic7TextDatetimeKijunDate.Text, out dtwork))
                {
                    return true;
                }

                //事業所マスタの情報を取得
                using (DataTable zigyoushoData = WinToolSearchComM.M0000MS_MA_Zigyousho.SelectByKaiteiDate(
                                                                                          zigyoushoCD.Text
                                                                                        , (DateTime)this.obic7TextDatetimeKijunDate.Value
                                                                                        , zigyoushoSecurityPatternCD
                                                                                        , 0
                                                                                        , 0
                                                                                        , 1
                                                                                        , ""
                                                                                        ))
                {
                    if (!ComTool.Function.IsValidDataTable(zigyoushoData)
                        || (zigyoushoData.Rows.Count <= 0))
                    {
                        //該当データなしの場合、処理なし
                    }
                    else
                    {
                        //名称をセット
                        zigyoushoName.Value = ComTool.Function.AnyToString(zigyoushoData.Rows[0]["ZigyoushoNameRyaku"]);
                    }
                }
            }

            return true;
        }

        #endregion

        #region 申告自治体のチェック

        /// <summary>
        /// 申告自治体のチェック
        /// </summary>
        /// <param name="zititaiCD">申告自治体コード</param>
        /// <param name="zititaiName">申告自治体名</param>
        /// <param name="isNeedErrorShori">true:エラー処理あり false:エラー処理なし</param>
        /// <returns></returns>
        private bool CheckZititai(WinToolUI.Obic7TextString zititaiCD, WinToolUI.Obic7LabelString zititaiName, bool isNeedErrorShori)
        {
            //名称のクリア
            zititaiName.Text = "";

            if (!string.IsNullOrEmpty(zititaiCD.Text))
            {
                //未入力でない場合

                //基準日が未入力の場合、処理終了
                if (string.IsNullOrEmpty(ComTool.Function.AnyToString(this.obic7TextDatetimeKijunDate.Text)))
                {
                    return true;
                }
                //基準日が日付に変換不可能な場合、処理終了
                DateTime dtwork = new DateTime();
                if (!DateTime.TryParse(this.obic7TextDatetimeKijunDate.Text, out dtwork))
                {
                    return true;
                }

                //自治体マスタの情報を取得
                using (DataTable sinkokuZititaData = WinToolSearchComN.N0000MS_NA_Zititai.SelectByKey(zititaiCD.Text, ""))
                {
                    if (!ComTool.Function.IsValidDataTable(sinkokuZititaData)
                        || (sinkokuZititaData.Rows.Count <= 0))
                    {
                        //該当データなしの場合、処理なし
                    }
                    else
                    {
                        //名称をセット
                        zititaiName.Value = ComTool.Function.AnyToString(sinkokuZititaData.Rows[0]["ZititaiName"]);
                    }
                }
            }

            return true;
        }

        #endregion

        #region 申告年度のチェック

        /// <summary>
        /// 申告年度のチェック
        /// </summary>
        /// <param name="sinkokuNendo">申告年度</param>
        /// <param name="isNeedErrorShori">true:エラー処理あり false:エラー処理なし</param>
        /// <returns></returns>
        private bool CheckSinkokuNendo(WinToolUI.Obic7TextDatetime sinkokuNendo, bool isNeedErrorShori)
        {
            if (!string.IsNullOrEmpty(sinkokuNendo.Text))
            {
                //未入力でない場合

            }

            return true;
        }

        #endregion

        #endregion

        #region 検索

        /// <summary>
        /// 検索処理
        /// </summary>
        private bool SearchExecute(WinToolUI.Obic7TextAbstract textAbstract)
        {
            //事業所_開始・終了
            if ((textAbstract == this.obic7TextStringZigyoushoCDStart)
                || (textAbstract == this.obic7TextStringZigyoushoCDEnd))
            {
                //事業所コード検索
                using (WinToolSearchM.MA_ZigyoushoCD zigyoushoCDSearch = new WinToolSearchM.MA_ZigyoushoCD())
                {
                    //パブリックプロパティに値をセット
                    zigyoushoCDSearch.BasicDate = (DateTime)this.obic7TextDatetimeKijunDate.Value;
                    zigyoushoCDSearch.SecurityPatternCD = zigyoushoSecurityPatternCD;
                    zigyoushoCDSearch.AllSumKubun = 0;
                    zigyoushoCDSearch.AllZigyoushoKubun = 0;
                    zigyoushoCDSearch.NameNullKubun = 1;

                    //検索ダイアログの関連付け
                    textAbstract.AttachedSearchDialog = zigyoushoCDSearch;

                    //検索画面の呼出
                    if (textAbstract.ShowDialog() == DialogResult.OK)
                    {
                        return true;
                    }
                }
            }
            //自治体_開始・終了
            if ((textAbstract == this.obic7TextStringZititaiCDStart)
                || (textAbstract == this.obic7TextStringZititaiCDEnd))
            {
                //自治体コード検索
                using (WinToolSearchN.NA_ZititaiCD zititaiCDSearch = new WinToolSearchN.NA_ZititaiCD())
                {
                    //パブリックプロパティに値をセット
                    //検索ダイアログの関連付け
                    textAbstract.AttachedSearchDialog = zititaiCDSearch;

                    //検索画面の呼出
                    if (textAbstract.ShowDialog() == DialogResult.OK)
                    {
                        return true;
                    }
                }
            }
            //★
            //格納先
            if (textAbstract == this.obic7TextStringOutPath)
            {
                //フォルダ選択ダイアログをインスタンス化
                WinToolLogic.UI.Obic7FolderBrowserDialog dialog = new WinToolLogic.UI.Obic7FolderBrowserDialog();

                {
                    //プロパティに値をセット
                    dialog.SelectedPath = this.obic7TextStringOutPath.Text;

                    //ダイアログ表示
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        //出力パスをセットする
                        this.obic7TextStringOutPath.Text = ComTool.Function.AddDirSep(dialog.SelectedPath);

                        return true;
                    }
                }
            }
            //★

            //検索でOKが押されなかった場合
            return false;
        }

        #endregion

        #region ワークデータ削除

        /// <summary>
        /// ワークデータ削除
        /// </summary>
        private void WorkDataDelete()
        {
            //該当ワークテーブルなし
        }

        #endregion

        #region 改定日管理項目の名称再取得

        /// <summary>
        /// 改定日管理項目の名称再取得
        /// </summary>
        private void RegetKoumokuName()
        {
            //事業所_開始のチェック
            CheckZigyousho(this.obic7TextStringZigyoushoCDStart, this.obic7LabelStringZigyoushoNameStart, false);
            //事業所_終了のチェック
            CheckZigyousho(this.obic7TextStringZigyoushoCDEnd, this.obic7LabelStringZigyoushoNameEnd, false);
        }

        #endregion

        #region 指定値の取得

        #region 出力区分の値を取得

        /// <summary>
        /// 出力区分の値を取得(数値)
        /// </summary>
        /// <returns></returns>
        private byte GetZigyoushoSiteiNo()
        {
            byte siteiNo = 0;

            //出力区分
            if (this.obic7RadioButtonZigyoushoZensha.Checked == true)
            {
                siteiNo = 0;
            }
            else if (this.obic7RadioButtonZigyousho.Checked == true)
            {
                siteiNo = 1;
            }

            return siteiNo;
        }

        /// <summary>
        /// 出力区分の値を取得(名称)
        /// </summary>
        /// <returns></returns>
        private string GetZigyoushoSiteiName()
        {
            string siteiName = "";

            //出力区分
            if (this.obic7RadioButtonZigyoushoZensha.Checked == true)
            {
                siteiName = "全社";
            }
            else if (this.obic7RadioButtonZigyousho.Checked == true)
            {
                siteiName = "事業所別";
            }

            return siteiName;
        }

        #endregion

        #region 資産区分の値を取得

        /// <summary>
        /// 資産区分の値を取得(数値)
        /// </summary>
        /// <returns></returns>
        private byte GetSisanKBNSiteiNo()
        {
            byte siteiNo = 0;

            //資産区分
            if (this.obic7RadioButtonSisanKBN_HonSisanOnly.Checked == true)
            {
                siteiNo = 0;
            }
            else if (this.obic7RadioButtonSisanKBN_KariSisanOnly.Checked == true)
            {
                siteiNo = 1;
            }
            else if (this.obic7RadioButtonSisanKBN_All.Checked == true)
            {
                siteiNo = 2;
            }

            return siteiNo;
        }

        /// <summary>
        /// 資産区分の値を取得(名称)
        /// </summary>
        /// <returns></returns>
        private string GetSisanKBNSiteiName()
        {
            string siteiName = "";

            //資産区分
            if (this.obic7RadioButtonSisanKBN_HonSisanOnly.Checked == true)
            {
                siteiName = "本資産のみ";
            }
            else if (this.obic7RadioButtonSisanKBN_KariSisanOnly.Checked == true)
            {
                siteiName = "仮資産のみ";
            }
            else if (this.obic7RadioButtonSisanKBN_All.Checked == true)
            {
                siteiName = "全て含む";
            }

            return siteiName;
        }

        #endregion

        #region [表紙(作成条件)出力]の値を取得

        /// <summary>
        /// [表紙(作成条件)出力]の値を取得(数値)
        /// </summary>
        /// <returns></returns>
        private byte GetSakuhyouJoukenSiteiNo()
        {
            byte siteiNo = 0;

            //[表紙(作成条件)出力]
            if (this.obic7CheckBoxSakuhyouJoukenSitei.Checked == true)
            {
                siteiNo = 1;
            }
            else
            {
                siteiNo = 0;
            }

            return siteiNo;
        }

        /// <summary>
        /// [表紙(作成条件)出力]の値を取得(True or Flase)
        /// </summary>
        /// <returns></returns>
        private string GetSakuhyouJoukenSiteiName()
        {
            string siteiName = "";

            //[表紙(作成条件)出力]
            if (this.obic7CheckBoxSakuhyouJoukenSitei.Checked == true)
            {
                siteiName = "True";
            }
            else
            {
                siteiName = "False";
            }

            return siteiName;
        }

        #endregion

        #region [評価額を印字]の値を取得

        /// <summary>
        /// [評価額を印字]の値を取得(数値)
        /// </summary>
        /// <returns></returns>
        private byte GetHyoukagakuSiteiNo()
        {
            byte siteiNo = 0;

            //[評価額を印字]
            if (this.obic7CheckBoxInzi_Hyoukagaku.Checked == true && this.obic7RadioButtonSinkokuShurui_Zougen.Checked == true)
            {
                siteiNo = 1;
            }
            else
            {
                siteiNo = 0;
            }

            return siteiNo;
        }

        /// <summary>
        /// [評価額を印字]の値を取得(True or Flase)
        /// </summary>
        /// <returns></returns>
        private string GetHyoukagakuSiteiName()
        {
            string siteiName = "";

            //[評価額を印字]
            if (this.obic7CheckBoxInzi_Hyoukagaku.Checked == true && this.obic7RadioButtonSinkokuShurui_Zougen.Checked == true)
            {
                siteiName = "True";
            }
            else
            {
                siteiName = "False";
            }

            return siteiName;
        }

        #endregion

        #region [前年中に減少、取得したもののみを表示]の値を取得

        /// <summary>
        /// [前年中に減少、取得したもののみを表示]の値を取得(数値)
        /// </summary>
        /// <returns></returns>
        private byte GetZennenchuSiteiNo()
        {
            byte siteiNo = 0;

            //[前年中に減少、取得したもののみを表示]
            if (this.obic7CheckBoxInzi_Zennenchu.Checked == true && this.obic7RadioButtonSinkokuShurui_Zougen.Checked == true)
            {
                siteiNo = 1;
            }
            else
            {
                siteiNo = 0;
            }

            return siteiNo;
        }

        /// <summary>
        /// [前年中に減少、取得したもののみを表示]の値を取得(True or Flase)
        /// </summary>
        /// <returns></returns>
        private string GetZennenchuSiteiName()
        {
            string siteiName = "";

            //[前年中に減少、取得したもののみを表示]
            if (this.obic7CheckBoxInzi_Zennenchu.Checked == true && this.obic7RadioButtonSinkokuShurui_Zougen.Checked == true)
            {
                siteiName = "True";
            }
            else
            {
                siteiName = "False";
            }

            return siteiName;
        }

        #endregion

        #region 申告種類の値を取得

        /// <summary>
        /// 申告種類の値を取得(数値)
        /// </summary>
        /// <returns></returns>
        private byte GetSinkokuShuruiSiteiNo()
        {
            byte siteiNo = 0;

            //申告種類
            if (this.obic7RadioButtonSinkokuShurui_Densan.Checked == true)
            {
                siteiNo = 0;
            }
            else if (this.obic7RadioButtonSinkokuShurui_Zougen.Checked == true)
            {
                siteiNo = 1;
            }

            return siteiNo;
        }

        /// <summary>
        /// 申告種類の値を取得(名称)
        /// </summary>
        /// <returns></returns>
        private string GetSinkokuShuruiSiteiName()
        {
            string siteiName = "";

            //申告種類
            if (this.obic7RadioButtonSinkokuShurui_Densan.Checked == true)
            {
                siteiName = "電算申告";
            }
            else if (this.obic7RadioButtonSinkokuShurui_Zougen.Checked == true)
            {
                siteiName = "増減申告";
            }

            return siteiName;
        }

        #endregion

        #region 用途の値を取得

        /// <summary>
        /// 用途の値を取得(数値)
        /// </summary>
        /// <returns></returns>
        private byte GetYoutoSiteiNo()
        {
            byte siteiNo = 0;

            //用途
            if (this.obic7RadioButtonYouto_Teishutu.Checked == true)
            {
                siteiNo = 0;
            }
            else if (this.obic7RadioButtonYouto_Hikae.Checked == true)
            {
                siteiNo = 1;
            }

            return siteiNo;
        }

        /// <summary>
        /// 用途の値を取得(名称)
        /// </summary>
        /// <returns></returns>
        private string GetYoutoSiteiName()
        {
            string siteiName = "";

            //用途
            if (this.obic7RadioButtonYouto_Teishutu.Checked == true)
            {
                siteiName = "提出用";
            }
            else if (this.obic7RadioButtonYouto_Hikae.Checked == true)
            {
                siteiName = "控用";
            }

            return siteiName;
        }

        #endregion

        #endregion

        #region 初期フォーカスセット

        /// <summary>
        /// 初期フォーカスセット
        /// </summary>
        /// <returns></returns>
        private void SetDefaultFocus()
        {
            //基準日へ
            this.obic7TextDatetimeKijunDate.Focus();
        }

        #endregion

        #region エラープロバイダクリア

        /// <summary>
        /// エラープロバイダの解除
        /// </summary>
        private void ClearErrorProvider()
        {
            //エラープロバイダの初期化

            //基準日
            WinToolUI.Obic7ErrorProvider.Clear(this.obic7TextDatetimeKijunDate);
            //提出年月日
            WinToolUI.Obic7ErrorProvider.Clear(this.obic7TextDatetimeTeishutuDate);
            //申告年度
            WinToolUI.Obic7ErrorProvider.Clear(this.obic7TextDatetimeSinkokuNendo);
            //申告自治体_開始
            WinToolUI.Obic7ErrorProvider.Clear(this.obic7TextStringZititaiCDStart);
            //申告自治体_終了
            WinToolUI.Obic7ErrorProvider.Clear(this.obic7TextStringZititaiCDEnd);
            //事業所_開始
            WinToolUI.Obic7ErrorProvider.Clear(this.obic7TextStringZigyoushoCDStart);
            //事業所_終了
            WinToolUI.Obic7ErrorProvider.Clear(this.obic7TextStringZigyoushoCDEnd);
        }

        #endregion
        
        #region 共通メソッド

        #region エラープロバイダ表示

        /// <summary>
        /// エラープロバイダを表示する
        /// </summary>
        /// <param name="errorControl">表示対象コントロール</param>
        /// <param name="errorCD2">メッセージCD2</param>
        /// <param name="errorParam">メッセージパラメータ用</param>
        private void DispErrorProvider(Control errorControl, string errorCD2, params object[] errorParam)
        {
            //エラーパラメータ
            object[] param ={ errorParam };
            //メッセージクラス
            ComToolLogic.Message message = new ComToolLogic.Message();
            //メッセージを取得
            message.GetMessage(SYSTEM_CATEGORY_CD, CUSTOMAIZE_SEQ, MESSAGECD1, errorCD2, param, null);
            //エラープロバイダを表示する
            WinToolUI.Obic7ErrorProvider.SetError(errorControl, message.MessageSummary);
        }

        #endregion

        #region 入力項目大小チェック

        /// <summary>
        /// 入力項目大小チェック_文字列用
        /// </summary>
        /// <param name="textFrom">条件（開始）入力テキストボックス</param>
        /// <param name="textTo">条件（終了）入力テキストボックス</param>
        /// <param name="isNeedFocusSet">フォーカスセット要・不要フラグ</param>
        /// <param name="dispJoukenKBN">1:基本抽出条件を表示、2:拡張抽出条件を表示</param>
        /// <returns>true:エラーなし false:エラーあり</returns>
        private bool InputItemDaishouCheck(WinToolUI.Obic7TextString textFrom, WinToolUI.Obic7TextString textTo, bool isNeedFocusSet, byte dispJoukenKBN)
        {
            //チェック結果
            bool result = true;

            //エラープロバイダを解除
            WinToolUI.Obic7ErrorProvider.Clear(textFrom);
            WinToolUI.Obic7ErrorProvider.Clear(textTo);

            if ((!string.IsNullOrEmpty(textFrom.Value)) && (!string.IsNullOrEmpty(textTo.Value)))
            {
                //両方が入力されていた場合は大小比較を行う
                if (textFrom.Text.CompareTo(textTo.Value) > 0)
                {
                    //開始＞終了の場合

                    //フォーカスの設定
                    if (isNeedFocusSet == true)
                    {
                        //フォーカスセット要の場合、フォーカスを移動する
                        textFrom.Focus();
                    }
                    //エラープロバイダに範囲指定エラーを表示
                    DispErrorProvider(textFrom, "00105", null);
                    DispErrorProvider(textTo, "00105", null);
                    //エラーあり
                    result = false;
                }
            }
            return result;
        }

        /// <summary>
        /// 入力項目大小チェック_数値用
        /// </summary>        
        /// <param name="textFrom">条件（開始）入力テキストボックス</param>
        /// <param name="textTo">条件（終了）入力テキストボックス</param>
        /// <param name="isNeedFocusSet">フォーカスセット要・不要フラグ</param>
        /// <param name="dispJoukenKBN">1:基本抽出条件を表示、2:拡張抽出条件を表示</param>
        /// <returns>true:エラーなし false:エラーあり</returns>
        private bool InputItemDaishouCheck(WinToolUI.Obic7TextNumber textFrom, WinToolUI.Obic7TextNumber textTo, bool isNeedFocusSet, byte dispJoukenKBN)
        {
            //チェック結果
            bool result = true;

            //エラープロバイダを解除
            WinToolUI.Obic7ErrorProvider.Clear(textFrom);
            WinToolUI.Obic7ErrorProvider.Clear(textTo);

            if ((textFrom.Value != null) && (textTo.Value != null))
            {
                //両方が入力されていた場合は大小比較を行う
                if (textFrom.Value > textTo.Value)
                {
                    //開始＞終了の場合

                    //フォーカスの設定
                    if (isNeedFocusSet == true)
                    {
                        //フォーカスセット要の場合、フォーカスを移動する
                        textFrom.Focus();
                    }
                    //エラープロバイダに範囲指定エラーを表示
                    DispErrorProvider(textFrom, "00105", null);
                    DispErrorProvider(textTo, "00105", null);
                    //エラーあり
                    result = false;
                }
            }
            return result;
        }

        /// <summary>
        /// 入力項目大小チェック_日付用
        /// </summary>
        /// <param name="textFrom">条件（開始）入力テキストボックス</param>
        /// <param name="textTo">条件（終了）入力テキストボックス</param>
        /// <param name="isNeedFocusSet">フォーカスセット要・不要フラグ</param>
        /// <param name="dispJoukenKBN">1:基本抽出条件を表示、2:拡張抽出条件を表示</param>
        /// <returns>true:エラーなし false:エラーあり</returns>
        private bool InputItemDaishouCheck(WinToolUI.Obic7TextDatetime textFrom, WinToolUI.Obic7TextDatetime textTo, bool isNeedFocusSet, byte dispJoukenKBN)
        {
            //チェック結果
            bool result = true;

            //エラープロバイダを解除
            WinToolUI.Obic7ErrorProvider.Clear(textFrom);
            WinToolUI.Obic7ErrorProvider.Clear(textTo);

            if ((textFrom.Value != null) && (textTo.Value != null))
            {
                //両方が入力されていた場合は大小比較を行う
                if (DateTime.Compare((DateTime)textFrom.Value, (DateTime)textTo.Value) > 0)
                {
                    //開始＞終了の場合

                    //フォーカスの設定
                    if (isNeedFocusSet == true)
                    {
                        //フォーカスセット要の場合、フォーカスを移動する
                        textFrom.Focus();
                    }
                    //エラープロバイダに範囲指定エラーを表示
                    DispErrorProvider(textFrom, "00105", null);
                    DispErrorProvider(textTo, "00105", null);
                    //エラーあり
                    result = false;
                }
            }
            return result;
        }

        #endregion

        #region 入力項目未入力チェック

        /// <summary>
        /// 未入力チェック
        /// </summary>
        /// <param name="textAbstract">入力テキストボックス</param>
        /// <param name="isNeedFocusSet">フォーカスセット要・不要フラグ</param>
        /// <param name="dispJoukenKBN">1:基本抽出条件を表示、2:拡張抽出条件を表示</param>
        /// <returns>true:エラーなし false:エラーあり</returns>
        private bool InputItemMinyuryokuCheck(WinToolUI.Obic7TextAbstract textAbstract, bool isNeedFocusSet, byte dispJoukenKBN)
        {
            //チェック結果
            bool result = true;

            //エラープロバイダを解除
            WinToolUI.Obic7ErrorProvider.Clear(textAbstract);

            if (String.IsNullOrEmpty(textAbstract.Text))
            {
                //未入力の場合
                if (isNeedFocusSet == true)
                {
                    //フォーカスセット要の場合、フォーカスを移動する
                    textAbstract.Focus();
                }
                //エラープロバイダに未入力エラーを表示
                DispErrorProvider(textAbstract, "00104", null);
                //エラーあり
                result = false;
            }

            return result;
        }

        #endregion

        #region 項目ロック制御

        /// <summary>
        /// ロック制御
        /// </summary>
        /// <param name="control">対象コントロール</param>
        /// <param name="enable">true:使用可能 false:使用不可</param>
        private void ControlLock(object control, bool enable)
        {
            ControlLock(control, enable, 0);
        }

        /// <summary>
        /// ロック制御
        /// </summary>
        /// <param name="control">対象コントロール</param>
        /// <param name="enable">true:使用可能 false:使用不可</param>
        /// <param name="kbn">0:テキストボックスをReadOnlyで制御 1:テキストボックスをEnableで制御</param>
        private void ControlLock(object control, bool enable, byte kbn)
        {
            //パネルならばパネルに配置されている全てのコントロールをロック制御する
            if (control is Obic7fx.Win.A0000.Tool.UI.Obic7Panel)
            {
                foreach (object obj in ((Obic7fx.Win.A0000.Tool.UI.Obic7Panel)control).Controls)
                {
                    ControlLock(obj, enable, kbn);
                }
            }
            //グループボックスならばグループボックスに配置されている全てのコントロールをロック制御する
            else if (control is Obic7fx.Win.A0000.Tool.UI.Obic7GroupBox)
            {
                foreach (object obj in ((Obic7fx.Win.A0000.Tool.UI.Obic7GroupBox)control).Controls)
                {
                    ControlLock(obj, enable, kbn);
                }
            }
            //Obic7TextAbstractを継承したコントロールをロック制御する
            else if (control is Obic7fx.Win.A0000.Tool.UI.Obic7TextAbstract)
            {
                if (control != this.obic7TextStringDummy)
                {
                    if (kbn == 0)
                    {
                        ((Obic7fx.Win.A0000.Tool.UI.Obic7TextAbstract)control).ReadOnly = !enable;
                        ((Obic7fx.Win.A0000.Tool.UI.Obic7TextAbstract)control).TabStop = enable;
                    }
                    else
                    {
                        ((Obic7fx.Win.A0000.Tool.UI.Obic7TextAbstract)control).Enabled = enable;
                    }
                }
            }
            //ボタンをロック制御する
            else if (control is Obic7fx.Win.A0000.Tool.UI.Obic7Button)
            {
                ((Obic7fx.Win.A0000.Tool.UI.Obic7Button)control).Enabled = enable;
            }
            //チェックボックスをロック制御する
            else if (control is Obic7fx.Win.A0000.Tool.UI.Obic7CheckBox)
            {
                ((Obic7fx.Win.A0000.Tool.UI.Obic7CheckBox)control).Enabled = enable;
            }
            //ラジオボタンをロック制御する
            else if (control is Obic7fx.Win.A0000.Tool.UI.Obic7RadioButton)
            {
                ((Obic7fx.Win.A0000.Tool.UI.Obic7RadioButton)control).Enabled = enable;
            }
        }

        #endregion

        #region ストアドエラー処理

        /// <summary>
        /// ストアドのエラーメッセージ用
        /// </summary>
        /// <param name="errorCD1"></param>
        /// <param name="errorCD2"></param>
        /// <param name="errorProcName"></param>
        /// <param name="errorSummary"></param>
        /// <param name="errorDetail"></param>
        /// <param name="errorBtnKBN"></param>
        /// <param name="errorBtnDefault"></param>
        private void ProcError(SqlParameter errorCD1, SqlParameter errorCD2, SqlParameter errorProcName, SqlParameter errorSummary, SqlParameter errorDetail, SqlParameter errorBtnKBN, SqlParameter errorBtnDefault)
        {
            //メッセージ概要・メッセージ詳細がパラメータ形式の文言の場合、パラメータにセットする値を配列形式で指定。
            //ストアドからの戻り値を、垂直タブ文字をデリミタとして分割し、配列に格納する。
            string[] objErrSummary = null;
            string[] objErrDetail = null;

            if (!(string.IsNullOrEmpty(ComTool.Function.AnyToString(errorSummary.Value))))
            {
                objErrSummary = errorSummary.Value.ToString().Split('\v');
            }

            if (!(string.IsNullOrEmpty(ComTool.Function.AnyToString(errorDetail.Value))))
            {
                objErrDetail = errorDetail.Value.ToString().Split('\v');
            }

            //ストアドからの戻り値より、メッセージボックスに表示するボタン種類とデフォルトボタンを設定する。
            WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxButtons btnType;
            WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxDefaultButton btnDefault;

            switch (ComTool.Function.AnyToInt16(ComTool.Function.AnyToString(errorBtnKBN.Value)))
            {
                case 0:
                    btnType = WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxButtons.OK;
                    break;

                case 1:
                    btnType = WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxButtons.YesNo;
                    break;

                case 2:
                    btnType = WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxButtons.YesNoCancel;
                    break;

                default:
                    btnType = WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxButtons.OK;
                    break;
            }

            switch (ComTool.Function.AnyToInt16(ComTool.Function.AnyToString(errorBtnDefault)))
            {
                case 0:
                    btnDefault = WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1;
                    break;

                case 1:
                    btnDefault = WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button2;
                    break;

                case 2:
                    btnDefault = WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button3;
                    break;

                default:
                    btnDefault = WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1;
                    break;
            }

            WinToolLogicUI.Obic7MessageBox.Show(SYSTEM_CATEGORY_CD
                                                , CUSTOMAIZE_SEQ
                                                , ComTool.Function.AnyToString(errorCD1.Value)
                                                , ComTool.Function.AnyToString(errorCD2.Value)
                                                , objErrSummary
                                                , objErrDetail
                                                , GetType()
                                                , btnType
                                                , btnDefault
                                                );


        }

        #endregion

        #region ストアド引数セット

        /// <summary>
        /// ストアドプロシージャへの引数引渡しの際に使用
        /// 未入力項目は""ではなくDBNull.Valueをセットするようにする
        /// </summary>
        /// <param name="inputText">ストアドに渡す値</param>
        /// <returns></returns>
        private object ConvertParamData(string inputText)
        {
            if (!(string.IsNullOrEmpty(inputText)))
            {
                return inputText;
            }
            else
            {
                return DBNull.Value;
            }
        }

        /// <summary>
        /// ストアドプロシージャへの引数引渡しの際に使用
        /// 未入力項目は""ではなくDBNull.Valueをセットするようにする
        /// </summary>
        /// <param name="inputText">ストアドに渡す値</param>
        /// <returns></returns>
        private object ConvertParamData(decimal? inputText)
        {
            if (inputText != null)
            {
                return inputText;
            }
            else
            {
                return DBNull.Value;
            }
        }

        /// <summary>
        /// ストアドプロシージャへの引数引渡しの際に使用
        /// 未入力項目はNULLではなくDBNull.Valueをセットするようにする
        /// </summary>
        /// <param name="inputText">ストアドに渡す値</param>
        /// <returns></returns>
        private object ConvertParamData(DateTime? inputText)
        {
            if (ComTool.Function.IsDateTime(inputText))
            {
                return (DateTime)inputText;
            }
            else
            {
                return DBNull.Value;
            }
        }

        #endregion

        #region SQL文直接発行用メソッド

        /// <summary>
        /// ExecuteNonQuery(SQL文直接発行用)
        /// </summary>
        /// <param name="strSQL"></param>
        /// <param name="isNeedTransaction">true:トランザクションあり false:トランザクションなし</param>
        private bool SQLExecuteNonQuery(string strSQL, bool isNeedTransaction)
        {
            //SQL実行
            try
            {
                businessDB.ExecuteNonQuery(strSQL);
            }
            catch (SqlException ex)
            {
                //【ストアドにて業務ロジック以外のエラー（システムエラー）発生時は、ここで例外を処理する。】
                //メッセージ分類は「InternalError」、メッセージアイコンは「Error」とする。
                //ロールバック、メッセージボックス表示処理は業務ロジックで行い、処理の中断のみ行う。
                //アプリケーションは異常終了させない。
                //注：この処理を行うのは、デッドロックの場合のみとする。

                bool bDeadLockFlg = true;

                //エラーがデッドロック以外の場合、フラグをfalseに設定する。	
                //ex.エラーコード = 1205 → デッドロック	
                //                = 2627 → キー重複	
                //                = 8134 → ゼロ除算	
                if (ex.Number != 1205)
                {
                    bDeadLockFlg = false;
                }

                if (bDeadLockFlg == true)
                {
                    //デッドロックの場合は、業務にてエラー処理を行う。

                    if (isNeedTransaction == true)
                    {
                        //ロールバック
                        businessDB.RollBackTransaction();
                    }

                    //メッセージボックス表示
                    WinToolLogicUI.Obic7MessageBox.Show(SYSTEM_CATEGORY_CD, CUSTOMAIZE_SEQ, MESSAGECD1, "00119", null, null, GetType(),
                                                         WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxButtons.OK,
                                                         WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1);
                    return false;
                }
                else
                {
                    //デッドロック以外のエラーはthrowして、共通クラスのエラー処理に委ねる。
                    throw;
                }
            }

            return true;
        }

        /// <summary>
        /// ExecuteNonQuery(SQL文直接発行用)
        /// </summary>
        /// <param name="strSQL"></param>
        /// <param name="count"></param>影響を受けた行数
        /// <param name="isNeedTransaction">true:トランザクションあり false:トランザクションなし</param>
        private bool SQLExecuteNonQuery(string strSQL, ref int count, bool isNeedTransaction)
        {
            //戻り値の初期化
            count = 0;

            //SQL実行
            try
            {
                count = businessDB.ExecuteNonQuery(strSQL);
            }
            catch (SqlException ex)
            {
                //【ストアドにて業務ロジック以外のエラー（システムエラー）発生時は、ここで例外を処理する。】
                //メッセージ分類は「InternalError」、メッセージアイコンは「Error」とする。
                //ロールバック、メッセージボックス表示処理は業務ロジックで行い、処理の中断のみ行う。
                //アプリケーションは異常終了させない。
                //注：この処理を行うのは、デッドロックの場合のみとする。

                bool bDeadLockFlg = true;

                //エラーがデッドロック以外の場合、フラグをfalseに設定する。	
                //ex.エラーコード = 1205 → デッドロック	
                //                = 2627 → キー重複	
                //                = 8134 → ゼロ除算	
                if (ex.Number != 1205)
                {
                    bDeadLockFlg = false;
                }

                if (bDeadLockFlg == true)
                {
                    //デッドロックの場合は、業務にてエラー処理を行う。

                    if (isNeedTransaction == true)
                    {
                        //ロールバック
                        businessDB.RollBackTransaction();
                    }

                    //メッセージボックス表示
                    WinToolLogicUI.Obic7MessageBox.Show(SYSTEM_CATEGORY_CD, CUSTOMAIZE_SEQ, MESSAGECD1, "00119", null, null, GetType(),
                                                         WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxButtons.OK,
                                                         WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1);
                    return false;
                }
                else
                {
                    //デッドロック以外のエラーはthrowして、共通クラスのエラー処理に委ねる。
                    throw;
                }
            }

            return true;
        }

        /// <summary>
        /// ExecuteScalar(SQL文直接発行用)
        /// </summary>
        /// <param name="strSQL"></param>
        /// <param name="resultObject"></param>
        /// <param name="isNeedTransaction">true:トランザクションあり false:トランザクションなし</param>
        private bool SQLExecuteScalar(string strSQL, ref object resultObject, bool isNeedTransaction)
        {
            //戻り値の初期化
            resultObject = null;

            //SQL実行
            try
            {
                resultObject = businessDB.ExecuteScalar(strSQL);
            }
            catch (SqlException ex)
            {
                //【ストアドにて業務ロジック以外のエラー（システムエラー）発生時は、ここで例外を処理する。】
                //メッセージ分類は「InternalError」、メッセージアイコンは「Error」とする。
                //ロールバック、メッセージボックス表示処理は業務ロジックで行い、処理の中断のみ行う。
                //アプリケーションは異常終了させない。
                //注：この処理を行うのは、デッドロックの場合のみとする。

                bool bDeadLockFlg = true;

                //エラーがデッドロック以外の場合、フラグをfalseに設定する。	
                //ex.エラーコード = 1205 → デッドロック	
                //                = 2627 → キー重複	
                //                = 8134 → ゼロ除算	
                if (ex.Number != 1205)
                {
                    bDeadLockFlg = false;
                }

                if (bDeadLockFlg == true)
                {
                    //デッドロックの場合は、業務にてエラー処理を行う。

                    if (isNeedTransaction == true)
                    {
                        //ロールバック
                        businessDB.RollBackTransaction();
                    }

                    //メッセージボックス表示
                    WinToolLogicUI.Obic7MessageBox.Show(SYSTEM_CATEGORY_CD, CUSTOMAIZE_SEQ, MESSAGECD1, "00119", null, null, GetType(),
                                                         WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxButtons.OK,
                                                         WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1);
                    return false;
                }
                else
                {
                    //デッドロック以外のエラーはthrowして、共通クラスのエラー処理に委ねる。
                    throw;
                }
            }

            return true;
        }

        /// <summary>
        /// GetDataTable(SQL文直接発行用)
        /// </summary>
        /// <param name="strSQL"></param>
        /// <param name="resultData"></param>
        /// <param name="isNeedTransaction">true:トランザクションあり false:トランザクションなし</param>
        private bool SQLGetDataTable(string strSQL, ref DataTable resultData, bool isNeedTransaction)
        {
            //戻り値の初期化
            resultData = null;

            //SQL実行
            try
            {
                resultData = businessDB.GetDataTable(strSQL);
            }
            catch (SqlException ex)
            {
                //【ストアドにて業務ロジック以外のエラー（システムエラー）発生時は、ここで例外を処理する。】
                //メッセージ分類は「InternalError」、メッセージアイコンは「Error」とする。
                //ロールバック、メッセージボックス表示処理は業務ロジックで行い、処理の中断のみ行う。
                //アプリケーションは異常終了させない。
                //注：この処理を行うのは、デッドロックの場合のみとする。
                //ex.エラーコード = 1205 → デッドロック

                bool bDeadLockFlg = true;

                //エラーがデッドロック以外の場合、フラグをfalseに設定する。	
                //ex.エラーコード = 1205 → デッドロック	
                //                = 2627 → キー重複	
                //                = 8134 → ゼロ除算	
                if (ex.Number != 1205)
                {
                    bDeadLockFlg = false;
                }

                if (bDeadLockFlg == true)
                {
                    //デッドロックの場合は、業務にてエラー処理を行う。

                    if (isNeedTransaction == true)
                    {
                        //ロールバック
                        businessDB.RollBackTransaction();
                    }

                    //メッセージボックス表示
                    WinToolLogicUI.Obic7MessageBox.Show(SYSTEM_CATEGORY_CD, CUSTOMAIZE_SEQ, MESSAGECD1, "00119", null, null, GetType(),
                                                         WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxButtons.OK,
                                                         WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1);
                    return false;
                }
                else
                {
                    //デッドロック以外のエラーはthrowして、共通クラスのエラー処理に委ねる。
                    throw;
                }
            }

            return true;
        }

        #endregion

        #endregion

        #region 前回保存機能

        #region 前回保存

        /// <summary>
        /// 前回保存
        /// </summary>
        private bool ZenkaiHozon()
        {
            //xml定義
            System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();
            System.Xml.XmlNode xmlnode = xmldoc.CreateNode(System.Xml.XmlNodeType.Element, zenkaiHozonJobID, "");

            //コントロール数分xmlを作成
            foreach (object obj in this.Controls)
            {
                MakeXml(obj, xmldoc, xmlnode);
            }

            //xmlを閉じる
            xmldoc.AppendChild(xmlnode);

            //xmlを保存する
            SaveXml(xmldoc);

            return true;
        }

        #endregion

        #region 前回呼出

        /// <summary>
        /// 前回呼出
        /// </summary>
        private bool ZenkaiYobidasi()
        {
            //XMLデータを取得する
            System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();
            if (!this.GetXml(out xmldoc))
            {
                return false;
            }

            //xmlファイルのelement数分処理を行う
            foreach (System.Xml.XmlElement xmlelement in xmldoc[zenkaiHozonJobID])
            {
                //該当elementと一致するコントロールに値をセット
                foreach (object obj in this.Controls)
                {
                    this.ReadXml(obj, xmlelement);
                }
            }

            //xml値にて取得したコードから名称を取得する
            SetValueFromXml();

            return true;
        }

        #endregion

        #region MakeXml - 画面入力値をxmlファイルに保存する

        /// <summary>
        /// 画面入力値をxmlファイルに保存する
        /// </summary>
        /// <param name="control">対象コントロール</param>
        /// <param name="xmldoc">保存対象xml</param>
        /// <param name="xmlnode">保存対象node</param>
        private void MakeXml(object control, System.Xml.XmlDocument xmldoc, System.Xml.XmlNode xmlnode)
        {
            //パネルならばパネルに配置されているコントロールをxmlファイルに保存する
            if (control is Obic7fx.Win.A0000.Tool.UI.Obic7Panel)
            {
                foreach (object obj in ((Obic7fx.Win.A0000.Tool.UI.Obic7Panel)control).Controls)
                {
                    MakeXml(obj, xmldoc, xmlnode);
                }
            }
            //グループボックスならばグループボックスに配置されているコントロールをxmlファイルに保存する
            else if (control is Obic7fx.Win.A0000.Tool.UI.Obic7GroupBox)
            {
                foreach (object obj in ((Obic7fx.Win.A0000.Tool.UI.Obic7GroupBox)control).Controls)
                {
                    MakeXml(obj, xmldoc, xmlnode);
                }
            }
            //Obic7TextAbstractを継承したコントロールならば値を保存する
            else if (control is Obic7fx.Win.A0000.Tool.UI.Obic7TextAbstract)
            {
                if (((Obic7fx.Win.A0000.Tool.UI.Obic7TextAbstract)control).Equals(this.obic7TextDatetimeKijunDate) == false)
                {
                    //基準日以外の場合

                    System.Xml.XmlElement xmlelement = xmldoc.CreateElement(((Obic7fx.Win.A0000.Tool.UI.Obic7TextAbstract)control).Name);
                    xmlelement.SetAttribute("value", ((Obic7fx.Win.A0000.Tool.UI.Obic7TextAbstract)control).Text);
                    xmlnode.AppendChild(xmlelement);
                }
            }
            //チェックボックスならば値を保存する
            else if (control is Obic7fx.Win.A0000.Tool.UI.Obic7CheckBox)
            {
                System.Xml.XmlElement xmlelement = xmldoc.CreateElement(((Obic7fx.Win.A0000.Tool.UI.Obic7CheckBox)control).Name);
                xmlelement.SetAttribute("value", ((Obic7fx.Win.A0000.Tool.UI.Obic7CheckBox)control).Checked.ToString());
                xmlnode.AppendChild(xmlelement);
            }
            //ラジオボタンならば値を保存する
            else if (control is Obic7fx.Win.A0000.Tool.UI.Obic7RadioButton)
            {
                System.Xml.XmlElement xmlelement = xmldoc.CreateElement(((Obic7fx.Win.A0000.Tool.UI.Obic7RadioButton)control).Name);
                xmlelement.SetAttribute("value", ((Obic7fx.Win.A0000.Tool.UI.Obic7RadioButton)control).Checked.ToString());
                xmlnode.AppendChild(xmlelement);
            }
            //ラベルならば任意検索対象かどうかの判定をおこなう
            else if (control is Obic7fx.Win.A0000.Tool.UI.Obic7LabelString)
            {
                //任意検索の場合、選択されているFULLテキストにて値を保存する

                //該当項目なし
            }
        }

        #endregion

        #region SaveXml - 画面入力値を保持したxmlファイルを保存する

        /// <summary>
        /// 画面入力値を保持したxmlファイルを保存する
        /// </summary>
        /// <param name="xmldoc">前回内容保存ＸＭＬ形式ドキュメント</param>
        public bool SaveXml(System.Xml.XmlDocument xmldoc)
        {
            return Obic7fx.Win.A0000.Tool.Logic.Xml.SaveXml(ComToolLogic.CurrentUser.UserID, zenkaiHozonJobID, xmldoc);
        }

        #endregion

        #region GetXml - 前回値が保存されているxmlファイルを取得する

        /// <summary>
        /// 前回値が保存されているxmlファイルを取得する
        /// </summary>
        /// <returns>True:成功 / False:失敗</returns>
        private bool GetXml(out System.Xml.XmlDocument xmldoc)
        {
            return Obic7fx.Win.A0000.Tool.Logic.Xml.ReadXml(ComToolLogic.CurrentUser.UserID, zenkaiHozonJobID, out xmldoc);
        }

        #endregion

        #region ReadXml - xmlファイルを解析しコントロールに値をセットする。

        /// <summary>
        /// xmlファイルを解析しコントロールに値をセットする。
        /// </summary>
        /// <param name="control">画面コントロール</param>
        /// <param name="xmlelement">XMLエレメント</param>
        private void ReadXml(object control, System.Xml.XmlElement xmlelement)
        {
            DateTime dtwork = new DateTime();

            //パネルならばパネルに割り当てられているコントロールに対して解析を行う
            if (control is Obic7fx.Win.A0000.Tool.UI.Obic7Panel)
            {
                foreach (object obj in ((Obic7fx.Win.A0000.Tool.UI.Obic7Panel)control).Controls)
                {
                    this.ReadXml(obj, xmlelement);
                }
            }
            //グループボックスならばパネルに割り当てられているコントロールに対して解析を行う
            else if (control is Obic7fx.Win.A0000.Tool.UI.Obic7GroupBox)
            {
                foreach (object obj in ((Obic7fx.Win.A0000.Tool.UI.Obic7GroupBox)control).Controls)
                {
                    this.ReadXml(obj, xmlelement);
                }
            }
            //Obic7TextDatetimeコントロールならば同一コントロールに値をセット
            else if (control is Obic7fx.Win.A0000.Tool.UI.Obic7TextDatetime)
            {
                if (((Obic7fx.Win.A0000.Tool.UI.Obic7TextDatetime)control).Name.Equals(xmlelement.Name) && ((Obic7fx.Win.A0000.Tool.UI.Obic7TextDatetime)control).Enabled)
                {
                    if (((Obic7fx.Win.A0000.Tool.UI.Obic7TextDatetime)control).Equals(this.obic7TextDatetimeKijunDate) == false)
                    {
                        //基準日以外の場合

                        if (((Obic7fx.Win.A0000.Tool.UI.Obic7TextDatetime)control).Equals(this.obic7TextDatetimeSinkokuNendo) == false)
                        {
                            //申告年度以外の場合
                            //日付として正しいならば値をセット。日付として正しくない場合は上書きしない
                            if (DateTime.TryParse(xmlelement.Attributes.GetNamedItem("value").Value, out dtwork))
                            {
                                ((Obic7fx.Win.A0000.Tool.UI.Obic7TextDatetime)control).Text = xmlelement.Attributes.GetNamedItem("value").Value;
                            }
                            else
                            {
                                //コントロールに上書きしない
                            }
                        }
                        else
                        {
                            ((Obic7fx.Win.A0000.Tool.UI.Obic7TextDatetime)control).Text = xmlelement.Attributes.GetNamedItem("value").Value;
                        }
                    }
                }
            }
            //Obic7TextAbstractを継承しているコントロールならば同一コントロールに値をセット
            else if (control is Obic7fx.Win.A0000.Tool.UI.Obic7TextAbstract)
            {
                if (((Obic7fx.Win.A0000.Tool.UI.Obic7TextAbstract)control).Name.Equals(xmlelement.Name) && ((Obic7fx.Win.A0000.Tool.UI.Obic7TextAbstract)control).Enabled)
                {
                    ((Obic7fx.Win.A0000.Tool.UI.Obic7TextAbstract)control).Text = xmlelement.Attributes.GetNamedItem("value").Value;
                }
            }
            //チェックボックスならば同一コントロールに値をセット
            else if (control is Obic7fx.Win.A0000.Tool.UI.Obic7CheckBox)
            {
                if (((Obic7fx.Win.A0000.Tool.UI.Obic7CheckBox)control).Name.Equals(xmlelement.Name))
                {
                    if(xmlelement.Attributes.GetNamedItem("value").Value.Equals("True"))
                    {
                        ((Obic7fx.Win.A0000.Tool.UI.Obic7CheckBox)control).Checked = true;
                    }
                    else
                    {
                        ((Obic7fx.Win.A0000.Tool.UI.Obic7CheckBox)control).Checked = false;
                    }
                }
            }
            //ラジオボタンならば同一コントロールに値をセット
            else if (control is Obic7fx.Win.A0000.Tool.UI.Obic7RadioButton)
            {
                if (((Obic7fx.Win.A0000.Tool.UI.Obic7RadioButton)control).Name.Equals(xmlelement.Name) && ((Obic7fx.Win.A0000.Tool.UI.Obic7RadioButton)control).Enabled)
                {
                    if (xmlelement.Attributes.GetNamedItem("value").Value.Equals("True"))
                    {
                        ((Obic7fx.Win.A0000.Tool.UI.Obic7RadioButton)control).Checked = true;
                    }
                }
            }
            //ラベルならば同一コントロールに値をセット
            else if (control is Obic7fx.Win.A0000.Tool.UI.Obic7LabelString)
            {
                //（ラベル項目については任意検索コントロールのみXMLに保存されている想定）
                if (((Obic7fx.Win.A0000.Tool.UI.Obic7LabelString)control).Name.Equals(xmlelement.Name))
                {
                    //該当項目なし
                }
            }
        }

        #endregion

        #region SetValueFromXml - xmlから取得した値に応じた名称を取得する

        /// <summary>
        /// xmlから取得した値に応じた名称を取得する
        /// </summary>
        private void SetValueFromXml()
        {
            DateTime dtwork = new DateTime();

            //申告自治体_開始
            if (CheckZititai(this.obic7TextStringZititaiCDStart, this.obic7LabelStringZititaiNameStart, false) == false)
            {
                //エラーの場合
                //処理なし
            }
            //申告自治体_終了
            if (CheckZititai(this.obic7TextStringZititaiCDEnd, this.obic7LabelStringZititaiNameEnd, false) == false)
            {
                //エラーの場合
                //処理なし
            }

            if (DateTime.TryParse(this.obic7TextDatetimeKijunDate.Text, out dtwork))
            {
                //基準日が日付変換可能な場合

                //事業所_開始
                if (CheckZigyousho(this.obic7TextStringZigyoushoCDStart, this.obic7LabelStringZigyoushoNameStart, false) == false)
                {
                    //エラーの場合
                    //処理なし
                }
                //事業所_終了
                if (CheckZigyousho(this.obic7TextStringZigyoushoCDEnd, this.obic7LabelStringZigyoushoNameEnd, false) == false)
                {
                    //エラーの場合
                    //処理なし
                }
            }

        }

        #endregion

        #endregion

        //2012/11/05 E.Sato ST (N00970)eLTax対応

        #region DBアクセス系

        /// <summary>
        /// データ抽出処理
        /// </summary>
        /// <param name="errorKBN">エラー区分</param>
        /// <param name="errorCD1">エラーCD1</param>
        /// <param name="errorCD2">エラーCD2</param>
        /// <param name="errorProcName">エラーストアド名</param>
        /// <param name="errorSummary">エラーメッセージ概要</param>
        /// <param name="errorDetail">エラーメッセージ詳細</param>
        /// <param name="errorBtnKBN">ボタン区分</param>
        /// <param name="errorBtnDefault">デフォルトボタン区分</param>
        /// <param name="errorMsgDetail">エラーメッセージ詳細（システムエラー用）</param>
        /// <param name="shoriKBN">処理区分</param>
        /// <param name="kijunDate">基準日</param>
        /// <param name="sisanKBN">資産区分</param>
        /// <param name="sisanKBNName">資産区分名</param>
        /// <param name="sakuhyouJoukenSitei">表紙(作表条件)出力</param>
        /// <param name="sinkokuShurui">申告種類</param>
        /// <param name="sinkokuShuruiName">申告種類名</param>
        /// <param name="hyoukagakuSitei">[評価額を印字]</param>
        /// <param name="zennenchuSitei">[前年中に減少、取得したもののみを表示]</param>
        /// <param name="youtoSitei">用途</param>
        /// <param name="youtoName">用途名</param>
        /// <param name="shuturyokuKBN">出力区分</param>
        /// <param name="shuturyokuKBNName">出力区分名</param>
        /// <param name="teishutuDate">提出年月日</param>
        /// <param name="sinkokuNendo">申告年度</param>
        /// <param name="hyousiSinkokuNendo">申告年度(表紙用)</param>
        /// <param name="zititaiCDBegin">申告自治体_開始</param>
        /// <param name="zititaiCDEnd">申告自治体_終了</param>
        /// <param name="zititaiNameBegin">申告自治体名_開始</param>
        /// <param name="zititaiNameEnd">申告自治体名_終了</param>
        /// <param name="zigyoushoCDBegin">事業所_開始</param>
        /// <param name="zigyoushoCDEnd">事業所_終了</param>
        /// <param name="zigyoushoNameBegin">事業所名_開始</param>
        /// <param name="zigyoushoNameEnd">事業所名_終了</param>
        /// <param name="zigyoushoSecurityPatternCD">事業所セキュリティパターンコード</param>
        /// <returns>該当件数</returns>
        private DataTable DataReadHead(
                                  ref int errorKBN
                                , ref SqlParameter errorCD1
                                , ref SqlParameter errorCD2
                                , ref SqlParameter errorProcName
                                , ref SqlParameter errorSummary
                                , ref SqlParameter errorDetail
                                , ref SqlParameter errorBtnKBN
                                , ref SqlParameter errorBtnDefault
                                , ref string[] errorMsgDetail
                                , byte shoriKBN
                                , DateTime? kijunDate
                                , byte sisanKBN
                                , string sisanKBNName
                                , byte sakuhyouJoukenSitei
                                , byte sinkokuShurui
                                , string sinkokuShuruiName
                                , byte hyoukagakuSitei
                                , byte zennenchuSitei
                                , byte youtoSitei
                                , string youtoName
                                , byte shuturyokuKBN
                                , string shuturyokuKBNName
                                , DateTime? teishutuDate
                                , DateTime? sinkokuNendo
                                , string hyousiSinkokuNendo
                                , string zititaiCDBegin
                                , string zititaiCDEnd
                                , string zititaiNameBegin
                                , string zititaiNameEnd
                                , string zigyoushoCDBegin
                                , string zigyoushoCDEnd
                                , string zigyoushoNameBegin
                                , string zigyoushoNameEnd
                                , string zigyoushoSecurityPatternCD
                                )
        {
            //取得データ格納用
            DataTable selectMainData = new DataTable();

            //プロシージャー呼び出し
            using (SqlCommand cm = businessDB.NewSqlCommand())
            {

                cm.CommandType = CommandType.StoredProcedure;
                cm.CommandText = PROCEDURE_SHOUKYAKUSISANSINKOKUSHO_PRINT;

                //エラー区分
                SqlParameter outErrorKBN;
                outErrorKBN = cm.Parameters.Add("@po_ErrorKBN", SqlDbType.Int);
                outErrorKBN.Direction = ParameterDirection.Output;
                //エラーCD1
                SqlParameter outErrorCD1;
                outErrorCD1 = cm.Parameters.Add("@po_ErrorCD1", SqlDbType.VarChar, 128);
                outErrorCD1.Direction = ParameterDirection.Output;
                //エラーCD2
                SqlParameter outErrorCD2;
                outErrorCD2 = cm.Parameters.Add("@po_ErrorCD2", SqlDbType.VarChar, 128);
                outErrorCD2.Direction = ParameterDirection.Output;
                //エラーストアド名
                SqlParameter paramErrorProcName;
                paramErrorProcName = cm.Parameters.Add("@po_ErrorProcName", SqlDbType.VarChar, 128);
                paramErrorProcName.Direction = ParameterDirection.Output;
                //エラーメッセージ概要
                SqlParameter paramErrorSummary;
                paramErrorSummary = cm.Parameters.Add("@po_ErrorSummary", SqlDbType.VarChar, 256);
                paramErrorSummary.Direction = ParameterDirection.Output;
                //エラーメッセージ詳細
                SqlParameter paramErrorDetail;
                paramErrorDetail = cm.Parameters.Add("@po_ErrorDetail", SqlDbType.VarChar, 2560);
                paramErrorDetail.Direction = ParameterDirection.Output;
                //ボタン区分
                SqlParameter paramErrorBtnKBN;
                paramErrorBtnKBN = cm.Parameters.Add("@po_ErrorBtnKBN", SqlDbType.TinyInt);
                paramErrorBtnKBN.Direction = ParameterDirection.Output;
                //デフォルトボタン区分
                SqlParameter paramErrorBtnDefault;
                paramErrorBtnDefault = cm.Parameters.Add("@po_ErrorBtnDefault", SqlDbType.TinyInt);
                paramErrorBtnDefault.Direction = ParameterDirection.Output;

                //基準日
                SqlParameter paramKijunDate;
                paramKijunDate = cm.Parameters.Add("@p_KijunDate", SqlDbType.DateTime);
                paramKijunDate.Direction = ParameterDirection.Input;
                paramKijunDate.Value = ConvertParamData(kijunDate);
                //資産区分
                SqlParameter paramSisanKBN;
                paramSisanKBN = cm.Parameters.Add("@p_SisanKBN", SqlDbType.TinyInt);
                paramSisanKBN.Direction = ParameterDirection.Input;
                paramSisanKBN.Value = ConvertParamData(sisanKBN);
                //申告種類
                SqlParameter paramSinkokushurui;
                paramSinkokushurui = cm.Parameters.Add("@p_Sinkokushurui", SqlDbType.TinyInt);
                paramSinkokushurui.Direction = ParameterDirection.Input;
                paramSinkokushurui.Value = ConvertParamData(sinkokuShurui);
                //評価額印字
                SqlParameter paramHyoukagakuPrintKBN;
                paramHyoukagakuPrintKBN = cm.Parameters.Add("@p_HyoukagakuPrintKBN", SqlDbType.TinyInt);
                paramHyoukagakuPrintKBN.Direction = ParameterDirection.Input;
                paramHyoukagakuPrintKBN.Value = ConvertParamData(hyoukagakuSitei);
                //前年中印字
                SqlParameter paramZennenPrintKBN;
                paramZennenPrintKBN = cm.Parameters.Add("@p_ZennenPrintKBN", SqlDbType.TinyInt);
                paramZennenPrintKBN.Direction = ParameterDirection.Input;
                paramZennenPrintKBN.Value = ConvertParamData(zennenchuSitei);
                //用途
                SqlParameter paramYoutoKBN;
                paramYoutoKBN = cm.Parameters.Add("@p_YoutoKBN", SqlDbType.TinyInt);
                paramYoutoKBN.Direction = ParameterDirection.Input;
                paramYoutoKBN.Value = ConvertParamData(youtoSitei);
                //出力区分
                SqlParameter paramShuturyokuKBN;
                paramShuturyokuKBN = cm.Parameters.Add("@p_ShuturyokuKBN", SqlDbType.TinyInt);
                paramShuturyokuKBN.Direction = ParameterDirection.Input;
                paramShuturyokuKBN.Value = ConvertParamData(shuturyokuKBN);
                //提出日
                SqlParameter paramTeishutuDate;
                paramTeishutuDate = cm.Parameters.Add("@p_TeishutuDate", SqlDbType.DateTime);
                paramTeishutuDate.Direction = ParameterDirection.Input;
                paramTeishutuDate.Value = ConvertParamData(teishutuDate);
                //申告年度
                SqlParameter paramSinkokuNendo;
                paramSinkokuNendo = cm.Parameters.Add("@p_SinkokuNendo", SqlDbType.DateTime);
                paramSinkokuNendo.Direction = ParameterDirection.Input;
                paramSinkokuNendo.Value = ConvertParamData(sinkokuNendo);
                //自治体コード開始
                SqlParameter paramZititaiCDBegin;
                paramZititaiCDBegin = cm.Parameters.Add("@p_ZititaiCDBegin", SqlDbType.VarChar, 6);
                paramZititaiCDBegin.Direction = ParameterDirection.Input;
                paramZititaiCDBegin.Value = ConvertParamData(zititaiCDBegin);
                //自治体コード終了
                SqlParameter paramZititaiCDEnd;
                paramZititaiCDEnd = cm.Parameters.Add("@p_ZititaiCDEnd", SqlDbType.VarChar, 6);
                paramZititaiCDEnd.Direction = ParameterDirection.Input;
                paramZititaiCDEnd.Value = ConvertParamData(zititaiCDEnd);
                //事業所コード開始
                SqlParameter paramZigyoushoCDBegin;
                paramZigyoushoCDBegin = cm.Parameters.Add("@p_ZigyoushoCDBegin", SqlDbType.VarChar, 6);
                paramZigyoushoCDBegin.Direction = ParameterDirection.Input;
                paramZigyoushoCDBegin.Value = ConvertParamData(zigyoushoCDBegin);
                //事業所コード終了
                SqlParameter paramZigyoushoCDEnd;
                paramZigyoushoCDEnd = cm.Parameters.Add("@p_ZigyoushoCDEnd", SqlDbType.VarChar, 6);
                paramZigyoushoCDEnd.Direction = ParameterDirection.Input;
                paramZigyoushoCDEnd.Value = ConvertParamData(zigyoushoCDEnd);
                //事業所セキュリティパターンコード
                SqlParameter paramZigSecPtnCD;
                paramZigSecPtnCD = cm.Parameters.Add("@p_ZigSecPtnCD", SqlDbType.VarChar, 4);
                paramZigSecPtnCD.Direction = ParameterDirection.Input;
                paramZigSecPtnCD.Value = ConvertParamData(zigyoushoSecurityPatternCD);
                //会社名
                SqlParameter paramCompanyName;
                paramCompanyName = cm.Parameters.Add("@p_CompanyName", SqlDbType.VarChar, 50);
                paramCompanyName.Direction = ParameterDirection.Input;
                paramCompanyName.Value = ConvertParamData(ComToolLogic.CurrentUser.CompanyName);
                //出力指定
                SqlParameter paramShuturyokuSitei;
                paramShuturyokuSitei = cm.Parameters.Add("@p_ShuturyokuSiteiKBN", SqlDbType.TinyInt);
                paramShuturyokuSitei.Direction = ParameterDirection.Input;
                paramShuturyokuSitei.Value = 1;

                //SQL実行
                try
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cm))
                    {
                        da.Fill(selectMainData);
                    }
                }
                catch (SqlException ex)
                {
                    //【ストアドにて業務ロジック以外のエラー（システムエラー）発生時は、ここで例外を処理する。】
                    //メッセージ分類は「InternalError」、メッセージアイコンは「Error」とする。
                    //ロールバック、メッセージボックス表示処理は業務ロジックで行い、処理の中断のみ行う。
                    //アプリケーションは異常終了させない。
                    //注：この処理を行うのは、デッドロックの場合のみとする。

                    bool bDeadLockFlg = true;

                    //メッセージ詳細の組み立て
                    errorMsgDetail = new string[3];

                    //ストアド名
                    errorMsgDetail[0] = ex.Procedure.ToString();
                    //エラーコード
                    errorMsgDetail[1] = ex.Number.ToString();
                    //エラー内容
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMsgDetail[2] += ex.Errors[i].ToString();
                    }

                    //エラーがデッドロック以外の場合、フラグをfalseに設定する。	
                    //ex.エラーコード = 1205 → デッドロック	
                    //                = 2627 → キー重複	
                    //                = 8134 → ゼロ除算	
                    if (ex.Number != 1205)
                    {
                        bDeadLockFlg = false;
                    }

                    if (bDeadLockFlg == true)
                    {
                        //デッドロックの場合は、業務にてエラー処理を行う。
                        return null;
                    }
                    else
                    {
                        //デッドロック以外のエラーはthrowして、共通クラスのエラー処理に委ねる。
                        throw ex;
                    }
                }

                //個別エラー発生時
                if (ComTool.Function.AnyToInt16(outErrorKBN.Value) != 0)
                {
                    errorKBN = ComTool.Function.AnyToInt32(outErrorKBN.Value);
                    errorCD1 = outErrorCD1;
                    errorCD2 = outErrorCD2;
                    errorProcName = paramErrorProcName;
                    errorSummary = paramErrorSummary;
                    errorDetail = paramErrorDetail;
                    errorBtnKBN = paramErrorBtnKBN;
                    errorBtnDefault = paramErrorBtnDefault;

                    return null;
                }
            }

            return selectMainData;
        }

        /// <summary>
        /// データ抽出処理
        /// </summary>
        /// <param name="errorKBN">エラー区分</param>
        /// <param name="errorCD1">エラーCD1</param>
        /// <param name="errorCD2">エラーCD2</param>
        /// <param name="errorProcName">エラーストアド名</param>
        /// <param name="errorSummary">エラーメッセージ概要</param>
        /// <param name="errorDetail">エラーメッセージ詳細</param>
        /// <param name="errorBtnKBN">ボタン区分</param>
        /// <param name="errorBtnDefault">デフォルトボタン区分</param>
        /// <param name="errorMsgDetail">エラーメッセージ詳細（システムエラー用）</param>
        /// <param name="shoriKBN">処理区分</param>
        /// <param name="kijunDate">基準日</param>
        /// <param name="sisanKBN">資産区分</param>
        /// <param name="sisanKBNName">資産区分名</param>
        /// <param name="sakuhyouJoukenSitei">表紙(作表条件)出力</param>
        /// <param name="sinkokuKBN">申告区分</param>
        /// <param name="sinkokuKBNName">申告区分名</param>
        /// <param name="youtoSitei">用途</param>
        /// <param name="youtoName">用途名</param>
        /// <param name="inziSisanCD">[資産コードを印字]</param>
        /// <param name="inziKagaku">[価額を印字]</param>
        /// <param name="inziKazeiHyoujunGaku">[課税標準額を印字]</param>
        /// <param name="shuturyokuKBN">出力区分</param>
        /// <param name="shuturyokuKBNName">出力区分名</param>
        /// <param name="sinkokuNendo">申告年度</param>
        /// <param name="hyousiSinkokuNendo">申告年度(表紙用)</param>
        /// <param name="zititaiCDBegin">申告自治体_開始</param>
        /// <param name="zititaiCDEnd">申告自治体_終了</param>
        /// <param name="zititaiNameBegin">申告自治体名_開始</param>
        /// <param name="zititaiNameEnd">申告自治体名_終了</param>
        /// <param name="zigyoushoCDBegin">事業所_開始</param>
        /// <param name="zigyoushoCDEnd">事業所_終了</param>
        /// <param name="zigyoushoNameBegin">事業所名_開始</param>
        /// <param name="zigyoushoNameEnd">事業所名_終了</param>
        /// <param name="zigyoushoSecurityPatternCD">事業所セキュリティパターン</param>
        /// <returns>該当件数</returns>
        public DataTable DataReadMeisai(
                                  ref int errorKBN
                                , ref SqlParameter errorCD1
                                , ref SqlParameter errorCD2
                                , ref SqlParameter errorProcName
                                , ref SqlParameter errorSummary
                                , ref SqlParameter errorDetail
                                , ref SqlParameter errorBtnKBN
                                , ref SqlParameter errorBtnDefault
                                , ref string[] errorMsgDetail
                                , byte shoriKBN
                                , DateTime? kijunDate
                                , byte sisanKBN
                                , string sisanKBNName
                                , byte sakuhyouJoukenSitei
                                , byte sinkokuKBN
                                , string sinkokuKBNName
                                , byte youtoSitei
                                , string youtoName
                                , byte inziSisanCD
                                , byte inziKagaku
                                , byte inziKazeiHyoujunGaku
                                , byte shuturyokuKBN
                                , string shuturyokuKBNName
                                , DateTime? sinkokuNendo
                                , string hyousiSinkokuNendo
                                , string zititaiCDBegin
                                , string zititaiCDEnd
                                , string zititaiNameBegin
                                , string zititaiNameEnd
                                , string zigyoushoCDBegin
                                , string zigyoushoCDEnd
                                , string zigyoushoNameBegin
                                , string zigyoushoNameEnd
                                , string zigyoushoSecurityPatternCD
                                )
        {
            //取得データ格納用
            DataTable selectMainData = new DataTable();

            //プロシージャー呼び出し
            using (SqlCommand cm = businessDB.NewSqlCommand())
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.CommandText = PROCEDURE_SHURUIBETUMEISAISHO_PRINT;

                //エラー区分
                SqlParameter outErrorKBN;
                outErrorKBN = cm.Parameters.Add("@po_ErrorKBN", SqlDbType.Int);
                outErrorKBN.Direction = ParameterDirection.Output;
                //エラーCD1
                SqlParameter outErrorCD1;
                outErrorCD1 = cm.Parameters.Add("@po_ErrorCD1", SqlDbType.VarChar, 128);
                outErrorCD1.Direction = ParameterDirection.Output;
                //エラーCD2
                SqlParameter outErrorCD2;
                outErrorCD2 = cm.Parameters.Add("@po_ErrorCD2", SqlDbType.VarChar, 128);
                outErrorCD2.Direction = ParameterDirection.Output;
                //エラーストアド名
                SqlParameter paramErrorProcName;
                paramErrorProcName = cm.Parameters.Add("@po_ErrorProcName", SqlDbType.VarChar, 128);
                paramErrorProcName.Direction = ParameterDirection.Output;
                //エラーメッセージ概要
                SqlParameter paramErrorSummary;
                paramErrorSummary = cm.Parameters.Add("@po_ErrorSummary", SqlDbType.VarChar, 256);
                paramErrorSummary.Direction = ParameterDirection.Output;
                //エラーメッセージ詳細
                SqlParameter paramErrorDetail;
                paramErrorDetail = cm.Parameters.Add("@po_ErrorDetail", SqlDbType.VarChar, 2560);
                paramErrorDetail.Direction = ParameterDirection.Output;
                //ボタン区分
                SqlParameter paramErrorBtnKBN;
                paramErrorBtnKBN = cm.Parameters.Add("@po_ErrorBtnKBN", SqlDbType.TinyInt);
                paramErrorBtnKBN.Direction = ParameterDirection.Output;
                //デフォルトボタン区分
                SqlParameter paramErrorBtnDefault;
                paramErrorBtnDefault = cm.Parameters.Add("@po_ErrorBtnDefault", SqlDbType.TinyInt);
                paramErrorBtnDefault.Direction = ParameterDirection.Output;

                //基準日
                SqlParameter paramKijunDate;
                paramKijunDate = cm.Parameters.Add("@p_KijunDate", SqlDbType.DateTime);
                paramKijunDate.Direction = ParameterDirection.Input;
                paramKijunDate.Value = ConvertParamData(kijunDate);
                //資産区分
                SqlParameter paramSisanKBN;
                paramSisanKBN = cm.Parameters.Add("@p_SisanKBN", SqlDbType.TinyInt);
                paramSisanKBN.Direction = ParameterDirection.Input;
                paramSisanKBN.Value = ConvertParamData(sisanKBN);
                //申告種類
                SqlParameter paramSinkokuKBN;
                paramSinkokuKBN = cm.Parameters.Add("@p_SinkokuKBN", SqlDbType.TinyInt);
                paramSinkokuKBN.Direction = ParameterDirection.Input;
                paramSinkokuKBN.Value = ConvertParamData(sinkokuKBN);
                //資産コード印字
                SqlParameter paramInziSisanCD;
                paramInziSisanCD = cm.Parameters.Add("@p_SisanCDPrintKBN", SqlDbType.TinyInt);
                paramInziSisanCD.Direction = ParameterDirection.Input;
                paramInziSisanCD.Value = ConvertParamData(inziSisanCD);
                //価額印字
                SqlParameter paramInziKagaku;
                paramInziKagaku = cm.Parameters.Add("@p_KagakuPrintKBN", SqlDbType.TinyInt);
                paramInziKagaku.Direction = ParameterDirection.Input;
                paramInziKagaku.Value = ConvertParamData(inziKagaku);
                //課税標準額印字
                SqlParameter paramInziKazeiHyoujunGaku;
                paramInziKazeiHyoujunGaku = cm.Parameters.Add("@p_KazeiHyoujunGakuPrintKBN", SqlDbType.TinyInt);
                paramInziKazeiHyoujunGaku.Direction = ParameterDirection.Input;
                paramInziKazeiHyoujunGaku.Value = ConvertParamData(inziKazeiHyoujunGaku);
                //出力区分
                SqlParameter paramShuturyokuKBN;
                paramShuturyokuKBN = cm.Parameters.Add("@p_ShuturyokuKBN", SqlDbType.TinyInt);
                paramShuturyokuKBN.Direction = ParameterDirection.Input;
                paramShuturyokuKBN.Value = ConvertParamData(shuturyokuKBN);
                //申告年度
                SqlParameter paramSinkokuNendo;
                paramSinkokuNendo = cm.Parameters.Add("@p_SinkokuNendo", SqlDbType.DateTime);
                paramSinkokuNendo.Direction = ParameterDirection.Input;
                paramSinkokuNendo.Value = ConvertParamData(sinkokuNendo);
                //自治体コード開始
                SqlParameter paramZititaiCDBegin;
                paramZititaiCDBegin = cm.Parameters.Add("@p_ZititaiCDBegin", SqlDbType.VarChar, 6);
                paramZititaiCDBegin.Direction = ParameterDirection.Input;
                paramZititaiCDBegin.Value = ConvertParamData(zititaiCDBegin);
                //自治体コード終了
                SqlParameter paramZititaiCDEnd;
                paramZititaiCDEnd = cm.Parameters.Add("@p_ZititaiCDEnd", SqlDbType.VarChar, 6);
                paramZititaiCDEnd.Direction = ParameterDirection.Input;
                paramZititaiCDEnd.Value = ConvertParamData(zititaiCDEnd);
                //事業所コード開始
                SqlParameter paramZigyoushoCDBegin;
                paramZigyoushoCDBegin = cm.Parameters.Add("@p_ZigyoushoCDBegin", SqlDbType.VarChar, 6);
                paramZigyoushoCDBegin.Direction = ParameterDirection.Input;
                paramZigyoushoCDBegin.Value = ConvertParamData(zigyoushoCDBegin);
                //事業所コード終了
                SqlParameter paramZigyoushoCDEnd;
                paramZigyoushoCDEnd = cm.Parameters.Add("@p_ZigyoushoCDEnd", SqlDbType.VarChar, 6);
                paramZigyoushoCDEnd.Direction = ParameterDirection.Input;
                paramZigyoushoCDEnd.Value = ConvertParamData(zigyoushoCDEnd);
                //事業所セキュリティパターンコード
                SqlParameter paramZigSecPtnCD;
                paramZigSecPtnCD = cm.Parameters.Add("@p_ZigSecPtnCD", SqlDbType.VarChar, 4);
                paramZigSecPtnCD.Direction = ParameterDirection.Input;
                paramZigSecPtnCD.Value = ConvertParamData(zigyoushoSecurityPatternCD);
                //会社名
                SqlParameter paramCompanyName;
                paramCompanyName = cm.Parameters.Add("@p_CompanyName", SqlDbType.VarChar, 50);
                paramCompanyName.Direction = ParameterDirection.Input;
                paramCompanyName.Value = ConvertParamData(ComToolLogic.CurrentUser.CompanyName);
                //出力指定
                SqlParameter paramShuturyokuSitei;
                paramShuturyokuSitei = cm.Parameters.Add("@p_ShuturyokuSiteiKBN", SqlDbType.TinyInt);
                paramShuturyokuSitei.Direction = ParameterDirection.Input;
                paramShuturyokuSitei.Value = 1;

                //SQL実行
                try
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cm))
                    {
                        da.Fill(selectMainData);
                    }
                }
                catch (SqlException ex)
                {
                    //【ストアドにて業務ロジック以外のエラー（システムエラー）発生時は、ここで例外を処理する。】
                    //メッセージ分類は「InternalError」、メッセージアイコンは「Error」とする。
                    //ロールバック、メッセージボックス表示処理は業務ロジックで行い、処理の中断のみ行う。
                    //アプリケーションは異常終了させない。
                    //注：この処理を行うのは、デッドロックの場合のみとする。

                    bool bDeadLockFlg = true;

                    //メッセージ詳細の組み立て
                    errorMsgDetail = new string[3];

                    //ストアド名
                    errorMsgDetail[0] = ex.Procedure.ToString();
                    //エラーコード
                    errorMsgDetail[1] = ex.Number.ToString();
                    //エラー内容
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMsgDetail[2] += ex.Errors[i].ToString();
                    }

                    //エラーがデッドロック以外の場合、フラグをfalseに設定する。	
                    //ex.エラーコード = 1205 → デッドロック	
                    //                = 2627 → キー重複	
                    //                = 8134 → ゼロ除算	
                    if (ex.Number != 1205)
                    {
                        bDeadLockFlg = false;
                    }

                    if (bDeadLockFlg == true)
                    {
                        //デッドロックの場合は、業務にてエラー処理を行う。
                        return null;
                    }
                    else
                    {
                        //デッドロック以外のエラーはthrowして、共通クラスのエラー処理に委ねる。
                        throw ex;
                    }
                }

                //個別エラー発生時
                if (ComTool.Function.AnyToInt16(outErrorKBN.Value) != 0)
                {
                    errorKBN = ComTool.Function.AnyToInt32(outErrorKBN.Value);
                    errorCD1 = outErrorCD1;
                    errorCD2 = outErrorCD2;
                    errorProcName = paramErrorProcName;
                    errorSummary = paramErrorSummary;
                    errorDetail = paramErrorDetail;
                    errorBtnKBN = paramErrorBtnKBN;
                    errorBtnDefault = paramErrorBtnDefault;

                    return null;
                }
            }

            return selectMainData;
        }

        #endregion

        //2012/11/05 E.Sato ED

        #endregion

        #region イベント

        #region フォーム

        /// <summary>
        /// フォームロード時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NA_ShoukyakuSisanSinkokusho_Load(object sender, EventArgs e)
        {
            //ワークテーブルのレコードを削除
            WorkDataDelete();

            //ワークのKEY用の日付を取得
            operationDate = (DateTime)businessDB.ExecuteScalar("SELECT GETDATE()");

            //コントロールマスタ情報の取得
            GetControlInfo();
            
            //地方税コントロールマスタ情報の取得
            GetTihouzeiControlInfo();

            //セキュリティ情報の取得
            GetSecurityInfo();

            //画面初期値設定
            SetDisplayDefault();
        }

        /// <summary>
        /// フォーム初期表示後の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NA_ShoukyakuSisanSinkokusho_Shown(object sender, EventArgs e)
        {
            //初期フォーカスをセット
            SetDefaultFocus();
        }

        /// <summary>
        /// フォーム上でのKeyDownイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NA_ShoukyakuSisanSinkokusho_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Alt == false) && (e.Control == false) && (e.Shift == false) && (e.KeyCode == Keys.Enter))
            {
                //Shift ALT Controlを押さない状態でエンターキーを押下した場合
                //次項目にフォーカスを移動
                base.SelectNextInputControl(true);
            }
        }

        /// <summary>
        /// フォームを閉じる際の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NA_ShoukyakuSisanSinkokusho_FormClosed(object sender, FormClosedEventArgs e)
        {
            //ワークテーブルのレコードを削除
            WorkDataDelete();
            
        }

        #endregion
        
        #region ヘッダー部のコントロール

        #region 基準日　Validating

        /// <summary>
        /// 基準日 検証時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7TextDatetimeKijunDate_Validating(object sender, CancelEventArgs e)
        {
            //コントロール判定用
            WinToolUI.Obic7TextDatetime textAbstract = null;

            //直前コントロール情報取得
            textAbstract = this.PrevControl as WinToolUI.Obic7TextDatetime;

            //コントロールが使用不可の場合、処理終了
            if (textAbstract.ReadOnly == true)
            {
                return;
            }

            //入力値が変更された場合のみ処理を行う
            if (textAbstract.IsChangedText == true)
            {
                if (!String.IsNullOrEmpty(textAbstract.Text))
                {
                    //未入力でない場合

                    //改定日管理項目の名称再取得
                    RegetKoumokuName();
                }
            }

            //必須チェック
            if (String.IsNullOrEmpty(ComTool.Function.AnyToString(textAbstract.Text)) == true)
            {
                //未入力の場合

                //処理をキャンセル
                e.Cancel = true;
                //エラープロバイダの表示
                DispErrorProvider(textAbstract, "00104", null);

                return;
            }
        }

        #endregion

        #region 出力指定　CheckedChanged
        /// <summary>
        /// 出力指定 選択変更時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7RadioButtonShuturyokuSitei_CheckedChanged(object sender, EventArgs e)
        {
            //ロック制御
            if (this.obic7RadioButtonShuturyokuSitei_Print.Checked)
            {
                //ロックを解除する

                //申告種類
                this.obic7RadioButtonSinkokuShurui_Densan.Enabled = true;
                this.obic7RadioButtonSinkokuShurui_Zougen.Enabled = true;

                //用途
                this.obic7RadioButtonYouto_Teishutu.Enabled = true;
                this.obic7RadioButtonYouto_Hikae.Enabled = true;

                //出力区分
                this.obic7RadioButtonZigyoushoZensha.Enabled = true;
                this.obic7RadioButtonZigyousho.Enabled = true;

                //ファンクションボタン
                this.obic7ToolStripButtonWithLogicF7.Enabled = true;
                this.obic7ToolStripButtonWithLogicF10.Enabled = false;
            }
            else 
            {
                //ロックする

                //申告種類
                this.obic7RadioButtonSinkokuShurui_Densan.Checked = true;
                this.obic7RadioButtonSinkokuShurui_Densan.Enabled = false;
                this.obic7RadioButtonSinkokuShurui_Zougen.Enabled = false;

                //用途
                this.obic7RadioButtonYouto_Teishutu.Checked = true;
                this.obic7RadioButtonYouto_Teishutu.Enabled = false;
                this.obic7RadioButtonYouto_Hikae.Enabled = false;

                //出力区分
                this.obic7RadioButtonZigyoushoZensha.Checked = true;
                this.obic7RadioButtonZigyoushoZensha.Enabled = false;
                this.obic7RadioButtonZigyousho.Enabled = false;

                //ファンクションボタン
                this.obic7ToolStripButtonWithLogicF7.Enabled = false;
                this.obic7ToolStripButtonWithLogicF10.Enabled = true;
            }
        }
        #endregion 

        #endregion

        #region 明細部のコントロール

        #region 事業所　Validating

        /// <summary>
        /// 事業所 検証時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7TextStringZigyousho_Validating(object sender, CancelEventArgs e)
        {
            //コントロール判定用
            WinToolUI.Obic7TextString textAbstract = null;
            //コントロール判定用 - 関連する項目
            WinToolUI.Obic7LabelString labelAbstractName = null;

            //直前コントロール情報取得
            textAbstract = this.PrevControl as WinToolUI.Obic7TextString;

            //コントロールが使用不可の場合、処理終了
            if (textAbstract.ReadOnly == true)
            {
                return;
            }
            if (textAbstract == this.obic7TextStringZigyoushoCDStart)
            {
                //事業所_開始の場合
                labelAbstractName = this.obic7LabelStringZigyoushoNameStart;
            }
            else if (textAbstract == this.obic7TextStringZigyoushoCDEnd)
            {
                //事業所_終了の場合
                labelAbstractName = this.obic7LabelStringZigyoushoNameEnd;
            }

            //  2014/02/21  削除        T.Sato      (7532)From-To対応
            ////入力値が変更された場合のみ処理を行う
            //if (textAbstract.IsChangedText == true)
            //{
            //  2014/02/21  削除        T.Sato      (7532)From-To対応
                if ((textAbstract == this.obic7TextStringZigyoushoCDStart)
                    || (textAbstract == this.obic7TextStringZigyoushoCDEnd))
                {
                    //事業所_開始・終了の場合
                    
                    if (!String.IsNullOrEmpty(textAbstract.Text))
                    {
                        //未入力でない場合

                        //[事業所_範囲指定]を ON
                        this.obic7RadioButtonZigyousho.Checked = true;
                    }

                    //入力チェック
                    if (CheckZigyousho(textAbstract, labelAbstractName, true) == false)
                    {
                        //処理をキャンセル
                        e.Cancel = true;
                    }
                }
            //  2014/02/21  削除        T.Sato      (7532)From-To対応
            //}
            //  2014/02/21  削除        T.Sato      (7532)From-To対応
        }

        #endregion

        #region 申告自治体　Validating

        /// <summary>
        /// 申告自治体 検証時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7TextStringZititaiCD_Validating(object sender, CancelEventArgs e)
        {
            //コントロール判定用
            WinToolUI.Obic7TextString textAbstract = null;
            //コントロール判定用 - 関連する項目
            WinToolUI.Obic7LabelString labelAbstractName = null;

            //直前コントロール情報取得
            textAbstract = this.PrevControl as WinToolUI.Obic7TextString;

            //コントロールが使用不可の場合、処理終了
            if (textAbstract.ReadOnly == true)
            {
                return;
            }

            //関連する項目をセット
            if (textAbstract == this.obic7TextStringZititaiCDStart)
            {
                //申告自治体_開始の場合
                labelAbstractName = this.obic7LabelStringZititaiNameStart;
            }
            else if (textAbstract == this.obic7TextStringZititaiCDEnd)
            {
                //申告自治体_終了の場合
                labelAbstractName = this.obic7LabelStringZititaiNameEnd;
            }

            //  2014/02/21  削除        T.Sato      (7532)From-To対応
            ////入力値が変更された場合のみ処理を行う
            //if (textAbstract.IsChangedText == true)
            //{
            //  2014/02/21  削除        T.Sato      (7532)From-To対応
                //入力チェック
                if (CheckZititai(textAbstract, labelAbstractName, true) == false)
                {
                    //処理をキャンセル
                    e.Cancel = true;
                }
            //  2014/02/21  削除        T.Sato      (7532)From-To対応
            //}
            //  2014/02/21  削除        T.Sato      (7532)From-To対応
        }

        #endregion

        #region 申告種類 CheckedChanged

        /// <summary>
        /// 申告種類 CheckedChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7RadioButtonSinkokuShurui_CheckedChanged(object sender, EventArgs e)
        {
            //フォーカス制御
            if (this.obic7RadioButtonSinkokuShurui_Densan.Checked)
            {
                //評価額を印字
                ControlLock(this.obic7CheckBoxInzi_Hyoukagaku, false, 1);
                //前年中に減少、取得したもののみを表示
                ControlLock(this.obic7CheckBoxInzi_Zennenchu, false, 1);
            }
            else if (this.obic7RadioButtonSinkokuShurui_Zougen.Checked)
            {
                //評価額を印字
                ControlLock(this.obic7CheckBoxInzi_Hyoukagaku, true, 1);
                //前年中に減少、取得したもののみを表示
                ControlLock(this.obic7CheckBoxInzi_Zennenchu, true, 1);
            }

        }

        #endregion

        #region 出力区分  CheckedChanged

        /// <summary>
        /// 出力区分  CheckedChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7RadioButtonZigyousho_CheckedChanged(object sender, EventArgs e)
        {
            //フォーカス制御
            if (this.obic7RadioButtonZigyoushoZensha.Checked)
            {
                //事業所_開始
                ControlLock(this.obic7TextStringZigyoushoCDStart, false, 1);
                ControlLock(this.obic7ButtonWithLogicSearchZigyoushoCDStart, false);
                //事業所_終了
                ControlLock(this.obic7TextStringZigyoushoCDEnd, false, 1);
                ControlLock(this.obic7ButtonWithLogicSearchZigyoushoCDEnd, false);
            }
            else if (this.obic7RadioButtonZigyousho.Checked)
            {
                //事業所_開始
                ControlLock(this.obic7TextStringZigyoushoCDStart, true, 1);
                ControlLock(this.obic7ButtonWithLogicSearchZigyoushoCDStart, true);
                //事業所_終了
                ControlLock(this.obic7TextStringZigyoushoCDEnd, true, 1);
                ControlLock(this.obic7ButtonWithLogicSearchZigyoushoCDEnd, true);
            }

        }

        #endregion

        #region 申告年度　Validating

        /// <summary>
        /// 申告年度 検証時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7TextDatetimeSinkokuNendo_Validating(object sender, CancelEventArgs e)
        {
            //コントロール判定用
            WinToolUI.Obic7TextDatetime textAbstract = null;

            //直前コントロール情報取得
            textAbstract = this.PrevControl as WinToolUI.Obic7TextDatetime;

            //コントロールが使用不可の場合、処理終了
            if (textAbstract.ReadOnly == true)
            {
                return;
            }


            //入力値が変更された場合のみ処理を行う
            if (textAbstract.IsChangedText == true)
            {
                //入力チェック
                if (CheckSinkokuNendo(textAbstract, true) == false)
                {
                    //処理をキャンセル
                    e.Cancel = true;
                }
            }

            //必須チェック
            if (String.IsNullOrEmpty(ComTool.Function.AnyToString(textAbstract.Text)) == true)
            {
                //未入力の場合

                //処理をキャンセル
                e.Cancel = true;
                //エラープロバイダの表示
                DispErrorProvider(textAbstract, "00104", null);

                return;
            }
        }

        #endregion
    
        #endregion

        #region コントロール　Enter

        /// <summary>
        /// 入力項目フォーカス取得時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputControl_Enter(object sender, EventArgs e)
        {
            //F4ボタンの使用可否を設定
            switch (((Control)sender).Name)
            {
                //事業所_開始
                case "obic7TextStringZigyoushoCDStart":
                //事業所_終了
                case "obic7TextStringZigyoushoCDEnd":
                //申告自治体_開始
                case "obic7TextStringZititaiCDStart":
                //申告自治体_終了
                case "obic7TextStringZititaiCDEnd":
                    if (((Obic7fx.Win.A0000.Tool.UI.Obic7TextAbstract)sender).ReadOnly == true)
                    {
                        //コントロールが使用不可の場合
                        this.obic7ToolStripButtonWithLogicF4.Enabled = false;
                    }
                    else
                    {
                        //コントロールが使用可の場合
                        this.obic7ToolStripButtonWithLogicF4.Enabled = true;
                    }
                    break;
                //2012/11/05 E.Sato ST (N00970)eLTax対応
                //格納先
                case "obic7TextStringOutPath":
                    //コントロールが使用可の場合
                    this.obic7ToolStripButtonWithLogicF4.Enabled = true;
                    break;
                //2012/11/05 E.Sato ED
                //上記以外のコントロール
                default:
                    this.obic7ToolStripButtonWithLogicF4.Enabled = false;
                    break;
            }

            //2012/11/05 E.Sato ST (N00970)eLTax対応
            //F7、F10の制御
            if (this.obic7RadioButtonShuturyokuSitei_Print.Checked == true)
            {
                this.obic7ToolStripButtonWithLogicF7.Enabled = true;
                this.obic7ToolStripButtonWithLogicF10.Enabled = false;
            }
            else
            {
                this.obic7ToolStripButtonWithLogicF7.Enabled = false;
                this.obic7ToolStripButtonWithLogicF10.Enabled = true;
            }
            //2012/11/05 E.Sato ED
        }

        #endregion

        #region 検索ボタン　Click

        /// <summary>
        /// 検索ボタン押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7ButtonWithLogicSearch_Click(object sender, EventArgs e)
        {
            //フォーカスをコード入力エリアに移動
            switch (((Control)sender).Name)
            {
                //事業所_開始
                case "obic7ButtonWithLogicSearchZigyoushoCDStart":
                    this.obic7TextStringZigyoushoCDStart.Focus();
                    break;
                //事業所_終了
                case "obic7ButtonWithLogicSearchZigyoushoCDEnd":
                    this.obic7TextStringZigyoushoCDEnd.Focus();
                    break;
                //申告自治体_開始
                case "obic7ButtonWithLogicSearchZititaiCDStart":
                    this.obic7TextStringZititaiCDStart.Focus();
                    break;
                //申告自治体_終了
                case "obic7ButtonWithLogicSearchZititaiCDEnd":
                    this.obic7TextStringZititaiCDEnd.Focus();
                    break;
            }
            //検索実行
            base.obic7ToolStripButtonWithLogicF4.PerformClick();
        }

        //2012/11/05 E.Sato ST (N00970)eLTax対応
        /// <summary>
        /// ファイル出力先検索クリック時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7ButtonWithLogicSearchOutPath_Click(object sender, EventArgs e)
        {
            this.obic7TextStringOutPath.Focus();
            //検索実行
            base.obic7ToolStripButtonWithLogicF4.PerformClick();
        }
        //2012/11/05 E.Sato ED

        #endregion

        #region ファンクション　Click

        /// <summary>
        /// F4キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7ToolStripButtonWithLogicF4_Click(object sender, EventArgs e)
        {
            F4Action();
        }

        /// <summary>
        /// F5キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7ToolStripButtonWithLogicF5_Click(object sender, EventArgs e)
        {
            F5Action();
        }

        /// <summary>
        /// F6キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7ToolStripButtonWithLogicF6_Click(object sender, EventArgs e)
        {
            F6Action();
        }

        /// <summary>
        /// F7キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7ToolStripButtonWithLogicF7_Click(object sender, EventArgs e)
        {
            F7Action();
        }

        //2012/11/05 E.Sato ST (N00970)eLTax対応
        /// <summary>
        /// F10キー押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7ToolStripButtonWithLogicF10_Click(object sender, EventArgs e)
        {
            F10Action();
        }
        //2012/11/05 E.Sato ED
        #endregion

        #endregion

    }
}