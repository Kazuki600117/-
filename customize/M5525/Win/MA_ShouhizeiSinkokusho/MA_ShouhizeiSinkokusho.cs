//
//  2007/11/18  新規作成    NOSE
//  2008/06/13  変更        N.Sasaki         事業所略名がNULLの場合にエラーとしないよう変更
//  2008/06/17  変更        N.Sasaki         月度期間マスタチェック→決算期マスタチェックに変更
//  2008/07/16  変更        G.Ukita          前回入力情報保存 を、F12:閉じる→帳票出力用データ作成の前に変更
//  2008/07/30  変更　　　　G.Ukita          MessageCD1にセットするJOBIDについて"Obic7fx.Win.M0000."をカットする。
//  2008/08/25  変更　　　　T.Nakano         デザインで使用されている括弧を全角から半角に修正
//　2009/02/06　変更　　　　T.Nakano       　(M04156)事業所名を正式名称を表示出来るようし、それに伴いレイアウトも修正
//  2011/01/06  変更   OBIC T.Sugawa         (M04472-01)決算期を跨いで出力出来るよう変更(2010/08/30 OBIC H.Sudo 変更分マージ)
//  2012/01/25  修正   OBIC T.Shiraishi      (M04737)平成23年度税制改正対応 
//  2013/07/09  変更   OBIC Y.Katsumata    GX対応(M00005:帳票範囲指定に「前月セット」追加)
//  2013/10/01  修正   Y.Katsumata            GX対応(M00005:帳票範囲指定に「前月セット」追加) 戻り対応
//  2013/10/10  修正        T.Shiraishi      (M00060)平成26年消費税改正対応(申告書の対応) 
//  2015/07/02  変更        R.Aoki           (25192-25347-25385)リバースチャージ対応
//  2018/05/11  変更        T.Kimura         (99108-99004)【消費税率10%対応】
//  2019/09/02  変更        M.Wasguzu        (139334-139359)【電子申告対応】
//  2021/02/15  修正        K.Matsumoto     (184646, 184647)      R3年度消費税制度改正対応
//  2023/07/24  修正        Y.Nakajima       (221366)【消費税申告管理】R5年インボイス対応(消費税申告の達人連携対応分)
//  2023/07/25  修正        Y.Matsuzawa      (221366)【消費税申告管理】R5年インボイス対応
//  2026/10/11  変更                         (5525)事業所別消費税計算方式対応
//                                           M0000 標準画面を複製。出力区分＝事業所の場合、消費税計算方式(売上・仕入 × R5.9.30以前・R5.10.1以後)を
//                                           事業所別制御マスタ(M5525MS_MA_ZigyoushoSeigyo)の値で計算する(未設定の区分は財務コントロールマスタの値)
//
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
// 2019/09/02  変更 M.Washizu (139334-139359)【電子申告対応】開始
using System.Xml;
using System.Xml.Linq;
using System.IO;
// 2019/09/02  変更 M.Washizu (139334-139359)【電子申告対応】終了
using Obic7fx.Win.A0000.Tool.Logic;
using Obic7fx.Win.A0000.Tool.Logic.UI;
using Obic7fx.Win.A0000.Tool.UI;
using Obic7fx.Win.M0000.Tool.UI;
using Obic7fx.Com.A0000.Tool;
using ComTool = Obic7fx.Com.A0000.Tool;
using ComToolLogic = Obic7fx.Com.A0000.Tool.Logic;
using WinTool = Obic7fx.Win.A0000.Tool;
using Obic7fx.Win.M0000.Tool.Search;
using WinToolLogic = Obic7fx.Win.A0000.Tool.Logic;
using SerchCommonCodeCom = Obic7fx.Win.M0000.Tool.Search.MA_CodeDialog;
using WinMToolSearchCom = Obic7fx.Win.M0000.Tool.SearchCom;

namespace Obic7fx.Win.M5525.Print.MA_ShouhizeiSinkokusho
{
    /// <summary>
    /// 消費税申告書
    /// </summary>
    public partial class MA_ShouhizeiSinkokusho : Obic7fx.Win.M0000.Tool.UI.Obic7MBussinessFormBase
    {
        #region 定数
        //メッセージコード1
        private const string MESSAGECD1 = "000";

        //システム分類
        private const string SYSTEMCATEGORYCD = "M";

        //カスタマイズSEQ
        private const string CUSTOMIZESEQ = "0000";

        //メッセージCD1
        private const string MESSAGE_CD1 = "000";

        // 2013/07/09 修正 Y.Katsumata 開始↓ GX対応(M00005:帳票範囲指定に「前月セット」追加)
        // 前月データ取得ストアド
        private const string ZENGETUINFO_SELECT = "M0000PR_MA_GetZengetuInfo";
        // 2013/07/09 修正 Y.Katsumata 終了↑ GX対応(M00005:帳票範囲指定に「前月セット」追加)

        // 2019/09/02  変更 M.Washizu (139334-139359)【電子申告対応】開始
        //ストアド 電子申告用のデータを作成
        private const string DENSISINKOKU_SELECT = "M0000PR_Obic7fx_Win_M0000_Print_MA_ShouhizeiSinkokusho_DensiSinkoku_Print";
        // 2019/09/02  変更 M.Washizu (139334-139359)【電子申告対応】終了

        #endregion

        #region 変数

        //DB接続用クラス（ステートフル）
        WinToolLogic.DBAccess businessDB;

        //事業所セキュリティパターンコード
        private string zigSecurityPatternCD;

        //事業所セキュリティ状態区分
        private int zigSecurityJoutaiKbn;

        //システム日付
        private DateTime systemDate;

        //基準日
        private DateTime kijunDate;

        //初期表示事業所コード
        private string defaultZigyoushoCD;

        //2023/07/25    Y.Matsuzawa      修正      (221366)【消費税申告管理】R5年インボイス対応 ↓
        ////売上消費税積上方式区分
        //R5.9.30以前売上消費税計算方式区分
        //2023/07/25    Y.Matsuzawa      修正      (221366)【消費税申告管理】R5年インボイス対応 ↑
        private int uriageZeiTumiageKBN;

        //2023/07/25    Y.Matsuzawa      修正      (221366)【消費税申告管理】R5年インボイス対応 ↓
        ////仕入消費税積上方式区分
        //R5.9.30以前仕入消費税積上方式区分
        //2023/07/25    Y.Matsuzawa      修正      (221366)【消費税申告管理】R5年インボイス対応 ↑
        private int siireZeiTumiageKBN;

        //消費税控除方式区分
        private int zeiKoujoKBN;

        //2009/02/06　変更　T.Nakano  開始　(M04156)事業所名を正式名称を表示出来るようし、それに伴いレイアウトも修正
        //事業所正式名称格納変数
        string zigyoushoName = "";
        //2009/02/06　変更　T.Nakano  終了

        //2012/01/25 修正 OBIC T.Shiraishi (M04737) 開始
        //事業年度制御区分
        int zigyounendoSeigyoKBN = 0;
        //2012/01/25 修正 OBIC T.Shiraishi (M04737) 終了

        // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 開始
        //会社運用形態区分
        private int kaishaUnyouKeitaiKBN;
        //法人番号
        private string houzinNO;
        // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 終了

        // 2019/09/02  変更 M.Washizu (139334-139359)【電子申告対応】開始
        //経理方式区分
        private int keiriHousikiKBN;
        //出力区分(0:F9押下 、1:F10押下、 2:その他)
        private int shuturyokuKBN;
        // 2019/09/02  変更 M.Washizu (139334-139359)【電子申告対応】終了

        //2023/07/25    Y.Matsuzawa      修正      (221366)【消費税申告管理】R5年インボイス対応 ↓
        //R5.10.1以後売上消費税計算方式区分
        private int _r5NenOctIgoUriageZeiTumiageKBN;

        //R5.10.1以後仕入消費税計算方式区分
        private int _r5NenOctIgoSiireZeiTumiageKBN;
        //2023/07/25    Y.Matsuzawa      修正      (221366)【消費税申告管理】R5年インボイス対応 ↑

        #endregion

        #region メソッド

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="jobID"></param>
        public MA_ShouhizeiSinkokusho(string jobID)
            : base(jobID)
        {
            InitializeComponent();
            //メインフォーム
            base.isMainForm = true;
            //フォーム初期化
            InitializeForm();
        }

        //引数なしコンストラクタを追加
        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected MA_ShouhizeiSinkokusho()
            : base()
        {
            InitializeComponent();
        }
        #endregion

        #region 項目初期化
        /// <summary>
        /// 項目初期化
        /// </summary>
        private void InitializeForm()
        {
            //DB接続クラスをインスタンス化
            businessDB = new WinToolLogic.DBAccess("M", "0000", ComToolLogic.DBAccess.Obic7DBAccessDBCategory.Business);

            //ファンクションボタンセット
            SetFunctionButton();
        }
        #endregion

        #region 入力項目初期化
        /// <summary>
        /// 入力項目初期化
        /// </summary>
        private void ClearInput()
        {
            //基準日
            obic7TextDatetimeKijunBi.Text = "";
            //対象期間_開始
            obic7TextDatetimeKikanST.Text = "";
            //対象期間_終了
            obic7TextDatetimeKikanED.Text = "";
            //事業所コード
            obic7TextStringZigyoushoCD.Text = "";
            //事業所名
            obic7LabelStringZigyoushoName.Text = "";
            // 2019/09/02  変更 M.Washizu (139334-139359)【電子申告対応】開始
            //格納先
            obic7TextStringOutPath.Text = "";
            // 2019/09/02  変更 M.Washizu (139334-139359)【電子申告対応】終了

            //セキュリティ状態区分がALL以外の場合は事業所全社選択不可
            // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 開始
            //if (zigSecurityJoutaiKbn != 0)
            if (zigSecurityJoutaiKbn != 0 || kaishaUnyouKeitaiKBN == 1)
            // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 終了
            {
                obic7RadioButtonZensha.Checked = false;
                obic7RadioButtonZigyousho.Checked = true;
            }
            else
            {
                obic7RadioButtonZensha.Checked = true;
                obic7RadioButtonZigyousho.Checked = false;
            }
            // 2018/05/11 T.Kimura 変更 (99108-99004)【消費税率10%対応】↓
            if (zigyounendoSeigyoKBN == 0
                || zigyounendoSeigyoKBN == 1
                || zigyounendoSeigyoKBN == 2
                || zigyounendoSeigyoKBN == 3)
            {
                // 2018/05/11 T.Kimura 変更 (99108-99004)【消費税率10%対応】↑
                obic7RadioButtonKijunJouhou.Checked = true;
                obic7RadioButtonSinkokusho.Checked = false;
                // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 開始
                obic7RadioButtonUtiwakesho.Checked = false;
                // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 終了
                obic7RadioButtonHuhyou1.Checked = false;
                obic7RadioButtonHuhyou2.Checked = false;
                obic7RadioButtonHuhyou3.Checked = false;
                // 2018/05/11 T.Kimura 変更 (99108-99004)【消費税率10%対応】↓
            }
            else
            {
                obic7RadioButton_H31_KisoJouhou.Checked = true;
                obic7RadioButton_H31_KisoJouhouKyuu.Checked = false;
                obic7RadioButton_H31_Sinkokusho.Checked = false;
                obic7RadioButton_H31_Utiwakesho.Checked = false;
                obic7RadioButton_H31_Huhyou11.Checked = false;
                obic7RadioButton_H31_Huhyou12.Checked = false;
                obic7RadioButton_H31_Huhyou21.Checked = false;
                obic7RadioButton_H31_Huhyou22.Checked = false;
                //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↓
                obic7RadioButton_H31_Huhyou13.Checked = false;
                obic7RadioButton_H31_Huhyou23.Checked = false;
                //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↑
            }
            // 2018/05/11 T.Kimura 変更 (99108-99004)【消費税率10%対応】↑

            //2012/01/25 修正 OBIC T.Shiraishi (M04737) 開始
            if (zigyounendoSeigyoKBN == 1)
            {
                this.obic7RadioButtonBubunKoujo.Checked = true;
            }
            //2012/01/25 修正 OBIC T.Shiraishi (M04737) 終了

        }
        #endregion

        // 2013/07/09 修正 Y.Katsumata 開始↓ GX対応(M00005:帳票範囲指定に「前月セット」追加)
        #region 前月データ抽出＆画面セット処理
        /// <summary>
        /// 前月データ抽出＆画面セット処理
        /// </summary>
        /// <remarks>データ取得無しは何もしない</remarks>
        private void GetZengetuInfo()
        {
            using (SqlCommand sc = this.businessDB.NewSqlCommand())
            {
                sc.CommandType = CommandType.StoredProcedure;
                sc.CommandText = ZENGETUINFO_SELECT;

                #region 共通パラメータ設定

                SqlParameter outparam1;
                outparam1 = sc.Parameters.Add("@po_ErrorKBN", SqlDbType.Int);
                outparam1.Direction = ParameterDirection.Output;

                SqlParameter outparam2;
                outparam2 = sc.Parameters.Add("@po_ErrorCD1", SqlDbType.VarChar, 128);
                outparam2.Direction = ParameterDirection.Output;

                SqlParameter outparam3;
                outparam3 = sc.Parameters.Add("@po_ErrorCD2", SqlDbType.VarChar, 128);
                outparam3.Direction = ParameterDirection.Output;

                // エラー発生ストアドプロシージャ名
                SqlParameter outparam4;
                outparam4 = sc.Parameters.Add("@po_ErrorProcName", SqlDbType.VarChar, 128);
                outparam4.Direction = ParameterDirection.Output;

                // エラーメッセージ概要
                SqlParameter outparam5;
                outparam5 = sc.Parameters.Add("@po_ErrorSummary", SqlDbType.VarChar, 128);
                outparam5.Direction = ParameterDirection.Output;

                // エラーメッセージ詳細
                SqlParameter outparam6;
                outparam6 = sc.Parameters.Add("@po_ErrorDetail", SqlDbType.VarChar, 2560);
                outparam6.Direction = ParameterDirection.Output;

                // エラーメッセージボックスボタン
                SqlParameter outparam7;
                outparam7 = sc.Parameters.Add("@po_ErrorBtnKBN", SqlDbType.TinyInt);
                outparam7.Direction = ParameterDirection.Output;

                // エラーメッセージボックスデフォルトボタン
                SqlParameter outparam8;
                outparam8 = sc.Parameters.Add("@po_ErrorBtnDefault", SqlDbType.TinyInt);
                outparam8.Direction = ParameterDirection.Output;

                // 期首年月日
                SqlParameter outparamKishuDate;
                outparamKishuDate = sc.Parameters.Add("@po_KishuDate", SqlDbType.DateTime);
                outparamKishuDate.Direction = ParameterDirection.Output;

                // 月度開始日
                SqlParameter outparamGetudoKaishiDate;
                outparamGetudoKaishiDate = sc.Parameters.Add("@po_GetudoKaisiDate", SqlDbType.DateTime);
                outparamGetudoKaishiDate.Direction = ParameterDirection.Output;

                // 月度終了日
                SqlParameter outparamGetudoShuuryouDate;
                outparamGetudoShuuryouDate = sc.Parameters.Add("@po_GetudoShuuryouDate", SqlDbType.DateTime);
                outparamGetudoShuuryouDate.Direction = ParameterDirection.Output;

                // 決算期
                SqlParameter outparamKessanki;
                outparamKessanki = sc.Parameters.Add("@po_Kessanki", SqlDbType.SmallInt);
                outparamKessanki.Direction = ParameterDirection.Output;

                // 表示用決算期
                SqlParameter outparamKessankiForDisp;
                outparamKessankiForDisp = sc.Parameters.Add("@po_KessankiForDisp", SqlDbType.VarChar, 5);
                outparamKessankiForDisp.Direction = ParameterDirection.Output;

                // 期首_月度呼称
                SqlParameter outparamKishuGetudoKoshou;
                outparamKishuGetudoKoshou = sc.Parameters.Add("@po_KishuGetudoKoshou", SqlDbType.VarChar, 10);
                outparamKishuGetudoKoshou.Direction = ParameterDirection.Output;

                // 月度呼称
                SqlParameter outparamGetudoKoshou;
                outparamGetudoKoshou = sc.Parameters.Add("@po_GetudoKoshou", SqlDbType.VarChar, 10);
                outparamGetudoKoshou.Direction = ParameterDirection.Output;

                // 基準日(現在日付)
                SqlParameter param1;
                param1 = sc.Parameters.Add("@p_KijunDate", SqlDbType.DateTime);
                param1.Direction = ParameterDirection.Input;
                param1.Value = this.obic7TextDatetimeKijunBi.Value;

                // リターン
                SqlParameter param2;
                param2 = sc.Parameters.Add("RetVal", SqlDbType.Int);
                param2.Direction = ParameterDirection.ReturnValue;
                #endregion

                try
                {
                    // ストアド実行
                    sc.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }

                //正常終了時
                if (ComTool.Function.AnyToInt16(outparam1.Value) == 0)
                {
                    //[戻り値].[月度開始日]を[対象期間_開始]にセットする。
                    this.obic7TextDatetimeKikanST.Value = ComTool.Function.AnyToDateTime(outparamGetudoKaishiDate.Value);
                    //[戻り値].[月度終了日]を[対象期間_終了]にセットする。
                    this.obic7TextDatetimeKikanED.Value = ComTool.Function.AnyToDateTime(outparamGetudoShuuryouDate.Value);
                }
            }
        }

        #endregion
        // 2013/07/09 修正 Y.Katsumata 終了↑ GX対応(M00005:帳票範囲指定に「前月セット」追加)

        #region ファンクションボタンセット
        /// <summary>
        /// ファンクションボタンセット
        /// </summary>
        private void SetFunctionButton()
        {
            //F1
            base.obic7ToolStripButtonWithLogicF1.Text = F1.BLANK;
            base.obic7ToolStripButtonWithLogicF1.Obic7JobSecurity = ComTool.Obic7JobSecurities.NoSecurity;

            //F2 補足入力
            base.obic7ToolStripButtonWithLogicF2.Text = "F2 補足入力";
            base.obic7ToolStripButtonWithLogicF2.Obic7JobSecurity = ComTool.Obic7JobSecurities.NoSecurity;
            base.obic7ToolStripButtonWithLogicF2.Click += new EventHandler(obic7ToolStripButtonWithLogicF2_Click);

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

            //F6 前回保存
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

            //F9
            // 2019/09/02  変更 M.Washizu (139334-139359)【電子申告対応】開始
            //base.obic7ToolStripButtonWithLogicF9.Text = F9.BLANK;
            //base.obic7ToolStripButtonWithLogicF9.Obic7JobSecurity = ComTool.Obic7JobSecurities.NoSecurity;
            base.obic7ToolStripButtonWithLogicF9.Text = "F9 XML確認";
            base.obic7ToolStripButtonWithLogicF9.Obic7JobSecurity = ComTool.Obic7JobSecurities.FileOutput;
            base.obic7ToolStripButtonWithLogicF9.Click += new EventHandler(obic7ToolStripButtonWithLogicF9_Click);
            // 2019/09/02  変更 M.Washizu (139334-139359)【電子申告対応】終了

            //F10
            // 2019/09/02  変更 M.Washizu (139334-139359)【電子申告対応】開始
            //base.obic7ToolStripButtonWithLogicF10.Text = F10.BLANK;
            //base.obic7ToolStripButtonWithLogicF10.Obic7JobSecurity = ComTool.Obic7JobSecurities.NoSecurity;
            base.obic7ToolStripButtonWithLogicF10.Text = "F10 XML出力";
            base.obic7ToolStripButtonWithLogicF10.Obic7JobSecurity = ComTool.Obic7JobSecurities.FileOutput;
            base.obic7ToolStripButtonWithLogicF10.Click += new EventHandler(obic7ToolStripButtonWithLogicF10_Click);
            // 2019/09/02  変更 M.Washizu (139334-139359)【電子申告対応】終了


            //F11
            base.obic7ToolStripButtonWithLogicF11.Text = F11.BLANK;
            base.obic7ToolStripButtonWithLogicF11.Obic7JobSecurity = ComTool.Obic7JobSecurities.NoSecurity;

            //F12 閉じる
            base.obic7ToolStripButtonWithLogicF12.Text = F12.CLOSE;
            base.obic7ToolStripButtonWithLogicF12.Obic7JobSecurity = ComTool.Obic7JobSecurities.NoSecurity;
        }
        #endregion

        #region F2ボタン押下時の処理
        /// <summary>
        /// F2ボタン押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7ToolStripButtonWithLogicF2_Click(object sender, EventArgs e)
        {
            F2Action();
        }

        /// <summary>
        /// 補足入力呼出
        /// </summary>
        private void F2Action()
        {
            //事業所の未入力チェック
            if (obic7RadioButtonZigyousho.Checked == true)
            {
                if (String.IsNullOrEmpty(obic7TextStringZigyoushoCD.Text))
                {
                    //エラープロバイダを表示
                    DispErrorProvider(obic7TextStringZigyoushoCD, "00104", null);

                    obic7LabelStringZigyoushoName.Text = "";

                    obic7TextStringZigyoushoCD.Focus();

                    // データがなければエラー処理
                    WinTool.Logic.UI.Obic7MessageBox.Show("M"
                                                        , "0000"
                                                        , "000"
                                                        , "00103"
                                                        , null
                                                        , null
                                                        , this.GetType()
                                                        , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                        , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                        );

                    return;
                }
            }

            // コントロール判定用-テキストボックス
            Obic7TextAbstract textAbstract = null;

            // 直前コントロール情報取得
            textAbstract = PrevControl as Obic7TextAbstract;

            //申告書補足入力
            using (Obic7fx.Win.M0000.MasterM.MA_ShouhizeiSinkokuHosoku.MA_ShouhizeiSinkokuHosokuMasterMaintenance ssh = new Obic7fx.Win.M0000.MasterM.MA_ShouhizeiSinkokuHosoku.MA_ShouhizeiSinkokuHosokuMasterMaintenance(ComTool.Logic.CurrentUser.JobID))
            {
                if (obic7RadioButtonZigyousho.Checked == true)
                {
                    //事業所の場合チェックする
                    if (textAbstract == obic7TextStringZigyoushoCD)
                    {
                        if (Validate() == false)
                        {
                            return;
                        }
                    }

                    //事業所CD
                    ssh.ZigyoushoCD = obic7TextStringZigyoushoCD.Text;

                    //事業所名
                    ssh.ZigyoushoName = obic7LabelStringZigyoushoName.Value;
                }
                else
                {
                    //事業所CD
                    ssh.ZigyoushoCD = "*";

                    //事業所名
                    ssh.ZigyoushoName = "全社";
                }

                if (ssh.CheckExecute())
                {
                    ssh.ShowDialog();
                }
            }
        }
        #endregion

        #region F4ボタン押下時の処理
        /// <summary>
        /// F4ボタン押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7ToolStripButtonWithLogicF4_Click(object sender, EventArgs e)
        {
            F4Action();
        }

        /// <summary>
        /// 検索
        /// </summary>
        private void F4Action()
        {
            // コントロール判定用-テキストボックス
            Obic7TextAbstract textAbstract = null;

            // 直前コントロール情報取得
            textAbstract = PrevControl as Obic7TextAbstract;

            if (textAbstract != null)
            {
                // 各項目ごとに検索処理
                if (ShowSearchDailog((Obic7TextString)textAbstract))
                {
                    //フォーカスを次のコントロールに移動
                    SelectNextInputControl(true);
                    return;
                }
                else
                {
                    //フォーカスを直前コントロールに移動
                    PrevControl.Focus();
                    return;
                }
            }

            // 検索でOKが押された場合、次のコントロールにフォーカスセット
            this.PrevControl.Focus();
        }
        #endregion

        #region F5ボタン押下時の処理
        /// <summary>
        /// F5ボタン押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7ToolStripButtonWithLogicF5_Click(object sender, EventArgs e)
        {
            F5Action();
        }

        /// <summary>
        /// 条件解除
        /// </summary>
        private void F5Action()
        {
            WinTool.UI.Obic7TextAbstract textAbstract = null;

            if (this.PrevControl is WinTool.UI.Obic7TextAbstract)
            {
                //直前コントロールに対して検証イベントを実行したくないので、
                //CausesValidationプロパティを一時的にfalseにする
                textAbstract = this.PrevControl as WinTool.UI.Obic7TextAbstract;
                textAbstract.CausesValidation = false;

                //フォーカスをダミー項目に逃がす
                this.obic7TextStringDummy.Focus();

                //直前コントロールのCausesValidationプロパティを元に戻す
                textAbstract.CausesValidation = true;
            }

            //入力項目初期化
            ClearInput();

            //セキュリティ情報取得
            GetSecurity();

            //2012/01/25 修正 OBIC T.Shiraishi (M04737) 開始
            GetZigyounendoSeigyoKBN();
            //2012/01/25 修正 OBIC T.Shiraishi (M04737) 終了

            //初期情報取得
            GetDefaultValue();

            //初期値セット
            SetDefaultValue();

            // フォーカスセット
            obic7TextDatetimeKikanST.Focus();
        }
        #endregion

        #region F6ボタン押下時の処理
        /// <summary>
        /// F6ボタン押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7ToolStripButtonWithLogicF6_Click(object sender, EventArgs e)
        {
            F6Action();
        }

        /// <summary>
        /// 前回呼出
        /// </summary>
        private void F6Action()
        {
            WinTool.UI.Obic7TextAbstract textAbstract = null;

            if (this.PrevFocus is WinTool.UI.Obic7TextAbstract)
            {
                //直前コントロールに対して検証イベントを実行したくないので、
                //CausesValidationプロパティを一時的にfalseにする
                textAbstract = this.PrevFocus as WinTool.UI.Obic7TextAbstract;
                textAbstract.CausesValidation = false;

                //フォーカスをダミー項目に逃がす
                this.obic7TextStringDummy.Focus();

                //直前コントロールのCausesValidationプロパティを元に戻す
                textAbstract.CausesValidation = true;
            }

            // エラープロバイダの初期化
            ClearErrorProvider();

            //XMLデータを取得する
            System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();
            if (!this.GetXml(out xmldoc))
            {
                return;
            }

            //xmlファイルのelement数分処理を行う
            foreach (System.Xml.XmlElement xmlelement in xmldoc[ComToolLogic.CurrentUser.JobID])
            {
                //該当elementと一致するコントロールに値をセット
                foreach (object obj in this.Controls)
                {
                    this.ReadXml(obj, xmlelement);
                }
            }

            //xml値にて取得したコードから名称を取得する
            SetValueFromXml();

            if (textAbstract != null)
            {
                //ダミー項目から元の直前コントロールにフォーカスを戻す
                textAbstract.Focus();
            }
        }
        #endregion

        #region F7ボタン押下時の処理
        /// <summary>
        /// F7ボタン押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7ToolStripButtonWithLogicF7_Click(object sender, EventArgs e)
        {
            F7Action();
        }

        /// <summary>
        /// 印刷
        /// </summary>
        private void F7Action()
        {
            // コントロール判定用-テキストボックス
            WinTool.UI.Obic7TextAbstract textAbstract = null;
            // 2019/09/02  変更 M.Washizu (139334-139359)【電子申告対応】開始
            //出力区分(0:F9押下 、1:F10押下、 2:その他)2の場合は格納先未入力チェックしない
            shuturyokuKBN = 2;
            // 2019/09/02  変更 M.Washizu (139334-139359)【電子申告対応】終了
            if (this.PrevControl is WinTool.UI.Obic7TextAbstract)
            {
                // 直前コントロール情報取得
                textAbstract = this.PrevControl as WinTool.UI.Obic7TextAbstract;

                textAbstract.CheckOnClose = true;
            }

            try
            {
                // 最終フォーカス項目の検証イベントを強制的に発生
                if (this.Validate())
                {
                    // 入力チェック
                    if (InputCheck())
                    {
                        // 検索処理を行う直前にログ出力を行う
                        OutPutReferenceLog();

                        //  2008/07/16  変更 G.Ukita 開始    前回入力情報保存 を、F12:閉じる→帳票出力用データ作成の前に変更
                        //xml定義
                        System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();
                        System.Xml.XmlNode xmlnode = xmldoc.CreateNode(System.Xml.XmlNodeType.Element, ComToolLogic.CurrentUser.JobID, "");

                        //コントロール数分xmlを作成
                        foreach (object obj in this.Controls)
                        {
                            MakeXml(obj, xmldoc, xmlnode);
                        }

                        //xmlを閉じる
                        xmldoc.AppendChild(xmlnode);

                        //xmlを保存する
                        SaveXml(xmldoc);
                        //  2008/07/16  変更 G.Ukita 終了

                        // ActiveReport出力
                        if (OutPutActiveReport() == false)
                        {
                            // 終了処理
                            return;
                        }

                    }

                }
            }
            finally
            {
                if (textAbstract != null)
                {
                    textAbstract.CheckOnClose = false;
                }
            }

            // 終了処理
            return;
        }
        #endregion

        // 2019/09/02  変更 M.Washizu (139334-139359)【電子申告対応】開始
        #region F9ボタン押下時の処理
        /// <summary>
        /// F9ボタン押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7ToolStripButtonWithLogicF9_Click(object sender, EventArgs e)
        {
            F9Action();
        }

        /// <summary>
        /// xml出力内容確認用ファイル出力(Excel)
        /// </summary>
        private void F9Action()
        {
            // コントロール判定用-テキストボックス
            WinTool.UI.Obic7TextAbstract textAbstract = null;
            //出力区分(0:F9押下 、1:F10押下、 2:その他) 2の場合は格納先未入力チェックしない
            shuturyokuKBN = 0;

            if (this.PrevControl is WinTool.UI.Obic7TextAbstract)
            {
                // 直前コントロール情報取得
                textAbstract = this.PrevControl as WinTool.UI.Obic7TextAbstract;

                textAbstract.CheckOnClose = true;
            }

            try
            {
                // 最終フォーカス項目の検証イベントを強制的に発生
                if (this.Validate())
                {
                    // 入力チェック
                    if (InputCheck())
                    {

                        // ログ出力を行う
                        OutPutReferenceLog();

                        //xml定義
                        System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();
                        System.Xml.XmlNode xmlnode = xmldoc.CreateNode(System.Xml.XmlNodeType.Element, ComToolLogic.CurrentUser.JobID, "");

                        //コントロール数分xmlを作成
                        foreach (object obj in this.Controls)
                        {
                            MakeXml(obj, xmldoc, xmlnode);
                        }

                        //xmlを閉じる
                        xmldoc.AppendChild(xmlnode);

                        //xmlを保存する
                        SaveXml(xmldoc);

                        // ファイル出力
                        if (OutPutFile() == false)
                        {
                            // 終了処理
                            return;
                        }

                        // 完了メッセージ
                        Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show("M"
                                                            , "0000"
                                                            , "000"
                                                            , "00278"
                                                            , null
                                                            , null
                                                            , this.GetType()
                                                            , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                            , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                            );
                    }
                }
            }
            finally
            {
                if (textAbstract != null)
                {
                    textAbstract.CheckOnClose = false;
                }
            }
            // 終了処理
            return;
        }
        #endregion

        #region F10ボタン押下時の処理
        /// <summary>
        /// F10ボタン押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7ToolStripButtonWithLogicF10_Click(object sender, EventArgs e)
        {
            F10Action();
        }

        /// <summary>
        /// 電子申告用Xmlファイル出力
        /// </summary>
        private void F10Action()
        {
            // コントロール判定用-テキストボックス
            WinTool.UI.Obic7TextAbstract textAbstract = null;
            //出力区分(0:F9押下 、1:F10押下、 2:その他) 2の場合は格納先未入力チェックしない
            shuturyokuKBN = 1;

            if (this.PrevControl is WinTool.UI.Obic7TextAbstract)
            {
                // 直前コントロール情報取得
                textAbstract = this.PrevControl as WinTool.UI.Obic7TextAbstract;

                textAbstract.CheckOnClose = true;
            }

            try
            {
                // 最終フォーカス項目の検証イベントを強制的に発生
                if (this.Validate())
                {
                    // 入力チェック
                    if (InputCheck())
                    {

                        // ログ出力を行う
                        OutPutReferenceLog();

                        //xml定義
                        System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();
                        System.Xml.XmlNode xmlnode = xmldoc.CreateNode(System.Xml.XmlNodeType.Element, ComToolLogic.CurrentUser.JobID, "");

                        //コントロール数分xmlを作成
                        foreach (object obj in this.Controls)
                        {
                            MakeXml(obj, xmldoc, xmlnode);
                        }

                        //xmlを閉じる
                        xmldoc.AppendChild(xmlnode);

                        //xmlを保存する
                        SaveXml(xmldoc);

                        // Xml出力
                        if (OutPutXmlFile() == false)
                        {
                            // 終了処理
                            return;
                        }

                        //完了メッセージの表示
                        Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD, CUSTOMIZESEQ, MESSAGE_CD1, "00278", null, null, GetType(),
                                                             Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK,
                                                             Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1);
                    }
                }
            }
            finally
            {
                //this.Validate()で検証をかけるために変えたプロパティを元に戻す
                //注）確実に戻さないと「閉じる」処理が正常に行えなくなるため
                //try～finallyで確実に通すようにする
                if (textAbstract != null)
                {
                    textAbstract.CheckOnClose = false;
                }
            }
            // 終了処理
            return;
        }
        #endregion
        // 2019/09/02  変更 M.Washizu (139334-139359)【電子申告対応】終了

        #region ログ出力
        /// <summary>
        /// ログ出力
        /// </summary>
        /// <returns></returns>
        private void OutPutReferenceLog()
        {
            // メッセージ詳細のパラメータ用

            //2012/01/25 修正 OBIC T.Shiraishi (M04737) 開始
            //object[] detailParams = new object[6];
            object[] detailParams = new object[7];
            //2012/01/25 修正 OBIC T.Shiraishi (M04737) 終了

            // 変数宣言
            string zigyoushoSitei = "";
            string chouhyoKBNSitei = "";
            string zigyoushoCD = "";

            // 事業所指定
            if (obic7RadioButtonZensha.Checked == true)
            {
                zigyoushoSitei = "全社";
                zigyoushoCD = "";
            }
            else
            {
                zigyoushoSitei = "事業所";
                zigyoushoCD = obic7TextStringZigyoushoCD.Text;
            }

            // 2018/05/11 T.Kimura 変更 (99108-99004)【消費税率10%対応】↓
            if (zigyounendoSeigyoKBN == 0
                || zigyounendoSeigyoKBN == 1
                || zigyounendoSeigyoKBN == 2
                || zigyounendoSeigyoKBN == 3)
            {
                // 2018/05/11 T.Kimura 変更 (99108-99004)【消費税率10%対応】↑
                // 出力帳票指定
                if (obic7RadioButtonKijunJouhou.Checked == true)
                {
                    chouhyoKBNSitei = "算出基礎情報";
                }
                else if (obic7RadioButtonSinkokusho.Checked == true)
                {
                    chouhyoKBNSitei = "申告書";
                }
                // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 開始
                else if (obic7RadioButtonUtiwakesho.Checked == true)
                {
                    chouhyoKBNSitei = "内訳書";
                }
                // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 終了
                else if (obic7RadioButtonHuhyou1.Checked == true)
                {
                    chouhyoKBNSitei = "付表1";

                }
                else if (obic7RadioButtonHuhyou2.Checked == true)
                {
                    chouhyoKBNSitei = "付表2";

                }
                else if (obic7RadioButtonHuhyou3.Checked == true)
                {
                    chouhyoKBNSitei = "付表2-(2)";

                }
                // 2018/05/11 T.Kimura 変更 (99108-99004)【消費税率10%対応】↓
            }
            //平成31年度以降の場合
            else
            {
                // 出力帳票指定
                if (obic7RadioButton_H31_KisoJouhou.Checked == true)
                {
                    chouhyoKBNSitei = "算出基礎情報";
                }
                else if (obic7RadioButton_H31_KisoJouhouKyuu.Checked == true)
                {
                    chouhyoKBNSitei = "算出基礎情報(旧税率)";
                }
                else if (obic7RadioButton_H31_Sinkokusho.Checked == true)
                {
                    chouhyoKBNSitei = "申告書";
                }
                else if (obic7RadioButton_H31_Utiwakesho.Checked == true)
                {
                    chouhyoKBNSitei = "内訳書";
                }
                else if (obic7RadioButton_H31_Huhyou11.Checked == true)
                {
                    chouhyoKBNSitei = "付表1-1";
                }
                else if (obic7RadioButton_H31_Huhyou12.Checked == true)
                {
                    chouhyoKBNSitei = "付表1-2";
                }
                else if (obic7RadioButton_H31_Huhyou21.Checked == true)
                {
                    chouhyoKBNSitei = "付表2-1";
                }
                else if (obic7RadioButton_H31_Huhyou22.Checked == true)
                {
                    chouhyoKBNSitei = "付表2-2";
                }
                //  2021/02/15  修正        K.Matsumoto     (184646, 184647)      R3年度消費税制度改正対応       ↓
                else if (obic7RadioButton_H31_Huhyou23.Checked == true)
                {
                    chouhyoKBNSitei = "付表2-3";
                }
                else if (obic7RadioButton_H31_Huhyou13.Checked == true)
                {
                    chouhyoKBNSitei = "付表1-3";
                }
                //  2021/02/15  修正        K.Matsumoto     (184646, 184647)      R3年度消費税制度改正対応       ↑
            }
            // 2018/05/11 T.Kimura 変更 (99108-99004)【消費税率10%対応】↑
            //2012/01/25 修正 OBIC T.Shiraishi (M04737) 開始
            string koujoSitei = "";
            if (this.obic7RadioButtonBubunKoujo.Checked)
            {
                koujoSitei = "課税売上に対応する部分のみを控除";
            }
            else
            {
                koujoSitei = "全額を控除(課税売上高:年額5億円未満)";
            }
            //2012/01/25 修正 OBIC T.Shiraishi (M04737) 終了


            // 基準日
            detailParams[0] = obic7TextDatetimeKijunBi.Value == null ? "" : ((DateTime)obic7TextDatetimeKijunBi.Value).ToString("yyyy/MM/dd");
            // 対象期   
            detailParams[1] = obic7TextDatetimeKikanST.Value == null ? "" : ((DateTime)obic7TextDatetimeKikanST.Value).ToString("yyyy/MM/dd");
            // 対象期間_終了
            detailParams[2] = obic7TextDatetimeKikanED.Value == null ? "" : ((DateTime)obic7TextDatetimeKikanED.Value).ToString("yyyy/MM/dd");
            // 事業所指定
            detailParams[3] = zigyoushoSitei;
            // 事業所コード
            detailParams[4] = zigyoushoCD;
            // 出力帳票区分
            detailParams[5] = chouhyoKBNSitei;
            //2012/01/25 修正 OBIC T.Shiraishi (M04737) 開始
            // 控除指定
            detailParams[6] = koujoSitei;
            //2012/01/25 修正 OBIC T.Shiraishi (M04737) 終了

            // FixedLogを出力する
            //  2008/07/30  変更　G.Ukita 開始    MessageCD1にセットするJOBIDについて"Obic7fx.Win.M0000."をカットする。
            //ComToolLogic.Log.FixedOutPut(SYSTEMCATEGORYCD, CUSTOMIZESEQ, ComTool.Logic.CurrentUser.JobID, "DataReference", null, detailParams, this.GetType());
            ComToolLogic.Log.FixedOutPut(SYSTEMCATEGORYCD, CUSTOMIZESEQ, "Print.MA_ShouhizeiSinkokusho", "DataReference", null, detailParams, this.GetType());
            //  2008/07/30  変更　G.Ukita 終了
        }
        #endregion

        #region 帳票出力
        /// <summary>
        /// 帳票出力
        /// </summary>
        /// <returns></returns>
        private bool OutPutActiveReport()
        {
            // ActiveReportクラス
            Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho1 reportMA_ShouhizeiSinkokusho1;
            Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho2 reportMA_ShouhizeiSinkokusho2;
            Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho3 reportMA_ShouhizeiSinkokusho3;
            Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho4 reportMA_ShouhizeiSinkokusho4;
            Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho5 reportMA_ShouhizeiSinkokusho5;
            //2012/01/25 修正 OBIC T.Shiraishi (M04737) 開始
            Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho6 reportMA_ShouhizeiSinkokusho6;
            Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho7 reportMA_ShouhizeiSinkokusho7;
            //2012/01/25 修正 OBIC T.Shiraishi (M04737) 終了
            //  2013/10/10  修正        T.Shiraishi      (M00060)平成26年消費税改正対応(申告書の対応)  開始↓
            //改正後:付表2
            Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho8 reportMA_ShouhizeiSinkokusho8;
            //改正後:付表2-(2)
            Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho9 reportMA_ShouhizeiSinkokusho9;
            //改正後:転記資料
            Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho10 reportMA_ShouhizeiSinkokusho10;
            //改正後:付表1
            Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho11 reportMA_ShouhizeiSinkokusho11;
            //  2013/10/10  修正        T.Shiraishi      (M00060)平成26年消費税改正対応(申告書の対応)  終了↑
            // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 開始
            //H27年度改正後:付表2
            Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho12 reportMA_ShouhizeiSinkokusho12;
            //H27年度改正後:付表2-(2)
            Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho13 reportMA_ShouhizeiSinkokusho13;
            //H27年度改正後:申告書
            Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho14 reportMA_ShouhizeiSinkokusho14;
            //H27年度改正後:付表1
            Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho15 reportMA_ShouhizeiSinkokusho15;
            //H27年度改正後:内訳書
            Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho16 reportMA_ShouhizeiSinkokusho16;
            // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 終了
            // 2018/05/11 T.Kimura 変更 (99108-99004)【消費税率10%対応】↓
            //H31年度改正後:算出基礎情報
            Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_H31_SanshutuKisoSin reportMA_ShouhizeiSinkokusho_H31_SanshutuKisoSin;
            //H31年度改正後:算出基礎情報(旧税率)
            Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_H31_SanshutuKisoKyuu reportMA_ShouhizeiSinkokusho_H31_SanshutuKisoKyuu;
            //H31年度改正後:申告書
            Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_H31_Sinkoku reportMA_ShouhizeiSinkokusho_H31_Sinkoku;
            //H31年度改正後:内訳書
            Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_H31_Utiwake reportMA_ShouhizeiSinkokusho_H31_Utiwake;
            //H31年度改正後:付表1-1
            Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_H31_Huhyou11 reportMA_ShouhizeiSinkokusho_H31_Huhyou11;
            //H31年度改正後:付表1-2
            Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_H31_Huhyou12 reportMA_ShouhizeiSinkokusho_H31_Huhyou12;
            //H31年度改正後:付表2-1
            Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_H31_Huhyou21 reportMA_ShouhizeiSinkokusho_H31_Huhyou21;
            //H31年度改正後:付表2-2
            Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_H31_Huhyou22 reportMA_ShouhizeiSinkokusho_H31_Huhyou22;
            // 2018/05/11 T.Kimura 変更 (99108-99004)【消費税率10%対応】↑
            //2021/02/15    K.Matsumoto     修正      (184646, 184647)        R3年度消費税制度改正対応       ↓
            //H31年度改正後:付表2-3
            Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_H31_Huhyou23 reportMA_ShouhizeiSinkokusho_H31_Huhyou23;
            //H31年度改正後:付表1-3
            Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_H31_Huhyou13 reportMA_ShouhizeiSinkokusho_H31_Huhyou13;
            //2021/02/15    K.Matsumoto     修正      (184646, 184647)        R3年度消費税制度改正対応       ↑
            //2023/07/25    Y.Matsuzawa      修正      (221366)【消費税申告管理】R5年インボイス対応 ↓
            //R5年度改正後:算出基礎情報
            Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_R5_SanshutuKisoSin reportMA_ShouhizeiSinkokusho_R5_SanshutuKisoSin;
            //R5年度改正後:算出基礎情報(旧税率)
            Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_R5_SanshutuKisoKyuu reportMA_ShouhizeiSinkokusho_R5_SanshutuKisoKyuu;
            //R5年度改正後:付表1-1
            Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_R5_Huhyou11 reportMA_ShouhizeiSinkokusho_R5_Huhyou11;
            //R5年度改正後:付表1-2
            Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_R5_Huhyou12 reportMA_ShouhizeiSinkokusho_R5_Huhyou12;
            //R5年度改正後:付表1-3
            Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_R5_Huhyou13 reportMA_ShouhizeiSinkokusho_R5_Huhyou13;
            //R5年度改正後:付表2-1
            Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_R5_Huhyou21 reportMA_ShouhizeiSinkokusho_R5_Huhyou21;
            //R5年度改正後:付表2-2
            Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_R5_Huhyou22 reportMA_ShouhizeiSinkokusho_R5_Huhyou22;
            //R5年度改正後:付表2-3
            Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_R5_Huhyou23 reportMA_ShouhizeiSinkokusho_R5_Huhyou23;
            //2023/07/25    Y.Matsuzawa      修正      (221366)【消費税申告管理】R5年インボイス対応 ↑
            // 印刷用クラス
            Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print printClass;

            // 変数宣言
            int zigyoushoSitei = 0;
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

            // 事業所指定
            if (obic7RadioButtonZensha.Checked == true)
            {
                zigyoushoSitei = 1;

            }
            else
            {
                zigyoushoSitei = 2;
            }

            //2012/01/25 修正 OBIC T.Shiraishi (M04737) 開始
            int koujoSitei = 0;
            //仕入控除税額の計算方法
            if (this.obic7RadioButtonBubunKoujo.Checked == true)
            {
                koujoSitei = 1;
            }
            else
            {
                koujoSitei = 2;
            }
            //2012/01/25 修正 OBIC T.Shiraishi (M04737) 終了

            //  2015/07/02  変更        R.Aoki           (25192-25347-25385)リバースチャージ対応  開始
            //法人番号の取得
            GetHouzinNO();
            //  2015/07/02  変更        R.Aoki           (25192-25347-25385)リバースチャージ対応  終了
            // 2018/05/11 T.Kimura 変更 (99108-99004)【消費税率10%対応】↓
            if (zigyounendoSeigyoKBN == 0
                || zigyounendoSeigyoKBN == 1
                || zigyounendoSeigyoKBN == 2
                || zigyounendoSeigyoKBN == 3)
            {
                // 2018/05/11 T.Kimura 変更 (99108-99004)【消費税率10%対応】↑
                // ActiveReport印刷用のクラスのインスタンスを作成する
                if (obic7RadioButtonKijunJouhou.Checked == true)
                {
                    using (reportMA_ShouhizeiSinkokusho1 = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho1())
                    {

                        // レポートクラスのデータ抽出
                        kensuuCount = reportMA_ShouhizeiSinkokusho1.DataRead(
                                                      ref errorKBN
                                                    , ref errorCD1
                                                    , ref errorCD2
                                                    , ref errorProcName
                                                    , ref errorSummary
                                                    , ref errorDetail
                                                    , ref errorBtnKBN
                                                    , ref errorBtnDefault
                                                    , ref errorMsgDetail
                                                    , obic7TextDatetimeKikanST.Text
                                                    , obic7TextDatetimeKikanED.Text
                                                    , zigyoushoSitei
                                                    , obic7TextStringZigyoushoCD.Value
                                                    , obic7LabelStringZigyoushoName.Value
                                                    , 1
                                                    , uriageZeiTumiageKBN
                                                    , siireZeiTumiageKBN
                            //2012/01/25 修正 OBIC T.Shiraishi (M04737) 開始
                            //, zeiKoujoKBN);
                                                    , zeiKoujoKBN
                                                    , koujoSitei);
                        //2012/01/25 修正 OBIC T.Shiraishi (M04737) 終了
                        if (kensuuCount == 0)
                        {
                            //システムエラー発生時
                            if (errorKBN == 1)
                            {
                                //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                            }

                            //個別エラー発生時
                            if (errorKBN == 2)
                            //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                            {
                                ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                return false;
                            }

                            // データがなければエラー処理
                            Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                , CUSTOMIZESEQ
                                                                , MESSAGE_CD1
                                                                , "00107"
                                                                , null
                                                                , null
                                                                , this.GetType()
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                );
                            return false;
                        }
                        else
                        {
                            // 検索結果が存在すれば印刷処理を行う
                            printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                            // 印刷用クラスInitialize
                            printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho1);
                            // 印刷ダイアログ表示
                            printClass.ShowPrintMenu();
                        }
                    }
                }
                else if (obic7RadioButtonSinkokusho.Checked == true)
                {
                    //  2013/10/10  修正        T.Shiraishi      (M00060)平成26年消費税改正対応(申告書の対応)  開始↓
                    if (zigyounendoSeigyoKBN == 2)
                    {
                        //改正後転記資料
                        using (reportMA_ShouhizeiSinkokusho10 = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho10())
                        {

                            // レポートクラスのデータ抽出
                            kensuuCount = reportMA_ShouhizeiSinkokusho10.DataRead(
                                                          ref errorKBN
                                                        , ref errorCD1
                                                        , ref errorCD2
                                                        , ref errorProcName
                                                        , ref errorSummary
                                                        , ref errorDetail
                                                        , ref errorBtnKBN
                                                        , ref errorBtnDefault
                                                        , ref errorMsgDetail
                                                        , obic7TextDatetimeKikanST.Text
                                                        , obic7TextDatetimeKikanED.Text
                                                        , zigyoushoSitei
                                                        , obic7TextStringZigyoushoCD.Value
                                                        , obic7LabelStringZigyoushoName.Value
                                                        , 2
                                                        , uriageZeiTumiageKBN
                                                        , siireZeiTumiageKBN
                                                        , zeiKoujoKBN
                                                        , koujoSitei);
                            if (kensuuCount == 0)
                            {
                                //システムエラー発生時
                                if (errorKBN == 1)
                                {
                                    //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                    //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                    throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                                }


                                //個別エラー発生時
                                if (errorKBN == 2)
                                //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                                {
                                    ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                    return false;
                                }

                                // データがなければエラー処理
                                Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                    , CUSTOMIZESEQ
                                                                    , MESSAGE_CD1
                                                                    , "00107"
                                                                    , null
                                                                    , null
                                                                    , this.GetType()
                                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                    );
                                return false;
                            }
                            else
                            {
                                // 検索結果が存在すれば印刷処理を行う
                                printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                                // 印刷用クラスInitialize
                                printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho10);
                                // 印刷ダイアログ表示
                                printClass.ShowPrintMenu();
                            }
                        }
                    }
                    // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 開始
                    else if (zigyounendoSeigyoKBN == 3)
                    {
                        //H27年度改正後:申告書
                        using (reportMA_ShouhizeiSinkokusho14 = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho14())
                        {

                            // レポートクラスのデータ抽出
                            kensuuCount = reportMA_ShouhizeiSinkokusho14.DataRead(
                                                          ref errorKBN
                                                        , ref errorCD1
                                                        , ref errorCD2
                                                        , ref errorProcName
                                                        , ref errorSummary
                                                        , ref errorDetail
                                                        , ref errorBtnKBN
                                                        , ref errorBtnDefault
                                                        , ref errorMsgDetail
                                                        , obic7TextDatetimeKikanST.Text
                                                        , obic7TextDatetimeKikanED.Text
                                                        , zigyoushoSitei
                                                        , obic7TextStringZigyoushoCD.Value
                                                        , obic7LabelStringZigyoushoName.Value
                                                        , 2
                                                        , uriageZeiTumiageKBN
                                                        , siireZeiTumiageKBN
                                                        , zeiKoujoKBN
                                                        , koujoSitei
                                                        , houzinNO);
                            if (kensuuCount == 0)
                            {
                                //システムエラー発生時
                                if (errorKBN == 1)
                                {
                                    //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                    //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                    throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                                }


                                //個別エラー発生時
                                if (errorKBN == 2)
                                //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                                {
                                    ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                    return false;
                                }

                                // データがなければエラー処理
                                Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                    , CUSTOMIZESEQ
                                                                    , MESSAGE_CD1
                                                                    , "00107"
                                                                    , null
                                                                    , null
                                                                    , this.GetType()
                                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                    );
                                return false;
                            }
                            else
                            {
                                // 検索結果が存在すれば印刷処理を行う
                                printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                                // 印刷用クラスInitialize
                                printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho14);
                                // 印刷ダイアログ表示
                                printClass.ShowPrintMenu();
                            }
                        }
                    }
                    // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 終了
                    else
                    {
                        //  2013/10/10  修正        T.Shiraishi      (M00060)平成26年消費税改正対応(申告書の対応)  終了↑


                        using (reportMA_ShouhizeiSinkokusho2 = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho2())
                        {

                            // レポートクラスのデータ抽出
                            kensuuCount = reportMA_ShouhizeiSinkokusho2.DataRead(
                                                          ref errorKBN
                                                        , ref errorCD1
                                                        , ref errorCD2
                                                        , ref errorProcName
                                                        , ref errorSummary
                                                        , ref errorDetail
                                                        , ref errorBtnKBN
                                                        , ref errorBtnDefault
                                                        , ref errorMsgDetail
                                                        , obic7TextDatetimeKikanST.Text
                                                        , obic7TextDatetimeKikanED.Text
                                                        , zigyoushoSitei
                                                        , obic7TextStringZigyoushoCD.Value
                                                        , obic7LabelStringZigyoushoName.Value
                                                        , 2
                                                        , uriageZeiTumiageKBN
                                                        , siireZeiTumiageKBN
                                //2012/01/25 修正 OBIC T.Shiraishi (M04737) 開始
                                //, zeiKoujoKBN);
                                                        , zeiKoujoKBN
                                                        , koujoSitei);
                            //2012/01/25 修正 OBIC T.Shiraishi (M04737) 終了

                            if (kensuuCount == 0)
                            {
                                //システムエラー発生時
                                if (errorKBN == 1)
                                {
                                    //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                    //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                    throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                                }


                                //個別エラー発生時
                                if (errorKBN == 2)
                                //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                                {
                                    ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                    return false;
                                }

                                // データがなければエラー処理
                                Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                    , CUSTOMIZESEQ
                                                                    , MESSAGE_CD1
                                                                    , "00107"
                                                                    , null
                                                                    , null
                                                                    , this.GetType()
                                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                    );
                                return false;
                            }
                            else
                            {
                                // 検索結果が存在すれば印刷処理を行う
                                printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                                // 印刷用クラスInitialize
                                printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho2);
                                // 印刷ダイアログ表示
                                printClass.ShowPrintMenu();
                            }
                        }
                        //  2013/10/10  修正        T.Shiraishi      (M00060)平成26年消費税改正対応(申告書の対応)  開始↓
                    }
                    //  2013/10/10  修正        T.Shiraishi      (M00060)平成26年消費税改正対応(申告書の対応)  終了↑
                }
                // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 開始
                else if (obic7RadioButtonUtiwakesho.Checked == true)
                {
                    if (zigyounendoSeigyoKBN == 3)
                    {
                        //H27年度改正後:内訳書
                        using (reportMA_ShouhizeiSinkokusho16 = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho16())
                        {

                            // レポートクラスのデータ抽出
                            kensuuCount = reportMA_ShouhizeiSinkokusho16.DataRead(
                                                          ref errorKBN
                                                        , ref errorCD1
                                                        , ref errorCD2
                                                        , ref errorProcName
                                                        , ref errorSummary
                                                        , ref errorDetail
                                                        , ref errorBtnKBN
                                                        , ref errorBtnDefault
                                                        , ref errorMsgDetail
                                                        , obic7TextDatetimeKikanST.Text
                                                        , obic7TextDatetimeKikanED.Text
                                                        , zigyoushoSitei
                                                        , obic7TextStringZigyoushoCD.Value
                                                        , obic7LabelStringZigyoushoName.Value
                                                        , 6
                                                        , uriageZeiTumiageKBN
                                                        , siireZeiTumiageKBN
                                                        , zeiKoujoKBN
                                                        , koujoSitei
                                                        , houzinNO);
                            if (kensuuCount == 0)
                            {
                                //システムエラー発生時
                                if (errorKBN == 1)
                                {
                                    //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                    //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                    throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                                }


                                //個別エラー発生時
                                if (errorKBN == 2)
                                //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                                {
                                    ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                    return false;
                                }

                                // データがなければエラー処理
                                Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                    , CUSTOMIZESEQ
                                                                    , MESSAGE_CD1
                                                                    , "00107"
                                                                    , null
                                                                    , null
                                                                    , this.GetType()
                                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                    );
                                return false;
                            }
                            else
                            {
                                // 検索結果が存在すれば印刷処理を行う
                                printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                                // 印刷用クラスInitialize
                                printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho16);
                                // 印刷ダイアログ表示
                                printClass.ShowPrintMenu();
                            }
                        }
                    }
                    else
                    {

                    }
                }
                // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 終了
                else if (obic7RadioButtonHuhyou1.Checked == true)
                {
                    //  2013/10/10  修正        T.Shiraishi      (M00060)平成26年消費税改正対応(申告書の対応)  開始↓
                    if (zigyounendoSeigyoKBN == 2)
                    {
                        //改正後付表1
                        using (reportMA_ShouhizeiSinkokusho11 = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho11())
                        {

                            // レポートクラスのデータ抽出
                            kensuuCount = reportMA_ShouhizeiSinkokusho11.DataRead(
                                                          ref errorKBN
                                                        , ref errorCD1
                                                        , ref errorCD2
                                                        , ref errorProcName
                                                        , ref errorSummary
                                                        , ref errorDetail
                                                        , ref errorBtnKBN
                                                        , ref errorBtnDefault
                                                        , ref errorMsgDetail
                                                        , obic7TextDatetimeKikanST.Text
                                                        , obic7TextDatetimeKikanED.Text
                                                        , zigyoushoSitei
                                                        , obic7TextStringZigyoushoCD.Value
                                                        , zigyoushoName
                                                        , 3
                                                        , uriageZeiTumiageKBN
                                                        , siireZeiTumiageKBN
                                                        , zeiKoujoKBN
                                                        , koujoSitei);
                            if (kensuuCount == 0)
                            {
                                //システムエラー発生時
                                if (errorKBN == 1)
                                {
                                    //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                    //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                    throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                                }


                                //個別エラー発生時
                                if (errorKBN == 2)
                                //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                                {
                                    ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                    return false;
                                }

                                // データがなければエラー処理
                                Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                    , CUSTOMIZESEQ
                                                                    , MESSAGE_CD1
                                                                    , "00107"
                                                                    , null
                                                                    , null
                                                                    , this.GetType()
                                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                    );
                                return false;
                            }
                            else
                            {
                                // 検索結果が存在すれば印刷処理を行う
                                printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                                // 印刷用クラスInitialize
                                printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho11);
                                // 印刷ダイアログ表示
                                printClass.ShowPrintMenu();
                            }
                        }
                    }
                    // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 開始
                    else if (zigyounendoSeigyoKBN == 3)
                    {
                        //H27年度改正後:付表1
                        using (reportMA_ShouhizeiSinkokusho15 = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho15())
                        {

                            // レポートクラスのデータ抽出
                            kensuuCount = reportMA_ShouhizeiSinkokusho15.DataRead(
                                                          ref errorKBN
                                                        , ref errorCD1
                                                        , ref errorCD2
                                                        , ref errorProcName
                                                        , ref errorSummary
                                                        , ref errorDetail
                                                        , ref errorBtnKBN
                                                        , ref errorBtnDefault
                                                        , ref errorMsgDetail
                                                        , obic7TextDatetimeKikanST.Text
                                                        , obic7TextDatetimeKikanED.Text
                                                        , zigyoushoSitei
                                                        , obic7TextStringZigyoushoCD.Value
                                                        , zigyoushoName
                                                        , 3
                                                        , uriageZeiTumiageKBN
                                                        , siireZeiTumiageKBN
                                                        , zeiKoujoKBN
                                                        , koujoSitei);
                            if (kensuuCount == 0)
                            {
                                //システムエラー発生時
                                if (errorKBN == 1)
                                {
                                    //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                    //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                    throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                                }


                                //個別エラー発生時
                                if (errorKBN == 2)
                                //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                                {
                                    ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                    return false;
                                }

                                // データがなければエラー処理
                                Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                    , CUSTOMIZESEQ
                                                                    , MESSAGE_CD1
                                                                    , "00107"
                                                                    , null
                                                                    , null
                                                                    , this.GetType()
                                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                    );
                                return false;
                            }
                            else
                            {
                                // 検索結果が存在すれば印刷処理を行う
                                printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                                // 印刷用クラスInitialize
                                printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho15);
                                // 印刷ダイアログ表示
                                printClass.ShowPrintMenu();
                            }
                        }
                    }
                    // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 終了
                    else
                    {
                        //  2013/10/10  修正        T.Shiraishi      (M00060)平成26年消費税改正対応(申告書の対応)  終了↑

                        using (reportMA_ShouhizeiSinkokusho3 = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho3())
                        {

                            // レポートクラスのデータ抽出
                            kensuuCount = reportMA_ShouhizeiSinkokusho3.DataRead(
                                                          ref errorKBN
                                                        , ref errorCD1
                                                        , ref errorCD2
                                                        , ref errorProcName
                                                        , ref errorSummary
                                                        , ref errorDetail
                                                        , ref errorBtnKBN
                                                        , ref errorBtnDefault
                                                        , ref errorMsgDetail
                                                        , obic7TextDatetimeKikanST.Text
                                                        , obic7TextDatetimeKikanED.Text
                                                        , zigyoushoSitei
                                                        , obic7TextStringZigyoushoCD.Value
                                //2009/02/06　変更　T.Nakano  開始　(M04156)事業所名を正式名称を表示出来るようし、それに伴いレイアウトも修正
                                //, obic7LabelStringZigyoushoName.Value
                                                        , zigyoushoName
                                //2009/02/06　変更　T.Nakano  終了
                                                        , 3
                                                        , uriageZeiTumiageKBN
                                                        , siireZeiTumiageKBN
                                //2012/01/25 修正 OBIC T.Shiraishi (M04737) 開始
                                //, zeiKoujoKBN);
                                                        , zeiKoujoKBN
                                                        , koujoSitei);
                            //2012/01/25 修正 OBIC T.Shiraishi (M04737) 終了
                            if (kensuuCount == 0)
                            {
                                //システムエラー発生時
                                if (errorKBN == 1)
                                {
                                    //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                    //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                    throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                                }


                                //個別エラー発生時
                                if (errorKBN == 2)
                                //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                                {
                                    ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                    return false;
                                }

                                // データがなければエラー処理
                                Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                    , CUSTOMIZESEQ
                                                                    , MESSAGE_CD1
                                                                    , "00107"
                                                                    , null
                                                                    , null
                                                                    , this.GetType()
                                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                    );
                                return false;
                            }
                            else
                            {
                                // 検索結果が存在すれば印刷処理を行う
                                printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                                // 印刷用クラスInitialize
                                printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho3);
                                // 印刷ダイアログ表示
                                printClass.ShowPrintMenu();
                            }
                        }
                        //  2013/10/10  修正        T.Shiraishi      (M00060)平成26年消費税改正対応(申告書の対応)  開始↓
                    }
                    //  2013/10/10  修正        T.Shiraishi      (M00060)平成26年消費税改正対応(申告書の対応)  終了↑
                }
                else if (obic7RadioButtonHuhyou2.Checked == true)
                {
                    //2012/01/25 修正 OBIC T.Shiraishi (M04737) 開始
                    if (zigyounendoSeigyoKBN == 0)
                    {
                        //2012/01/25 修正 OBIC T.Shiraishi (M04737) 終了

                        using (reportMA_ShouhizeiSinkokusho4 = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho4())
                        {

                            // レポートクラスのデータ抽出
                            kensuuCount = reportMA_ShouhizeiSinkokusho4.DataRead(
                                                          ref errorKBN
                                                        , ref errorCD1
                                                        , ref errorCD2
                                                        , ref errorProcName
                                                        , ref errorSummary
                                                        , ref errorDetail
                                                        , ref errorBtnKBN
                                                        , ref errorBtnDefault
                                                        , ref errorMsgDetail
                                                        , obic7TextDatetimeKikanST.Text
                                                        , obic7TextDatetimeKikanED.Text
                                                        , zigyoushoSitei
                                                        , obic7TextStringZigyoushoCD.Value
                                //2009/02/06　変更　T.Nakano  開始　(M04156)事業所名を正式名称を表示出来るようし、それに伴いレイアウトも修正
                                //, obic7LabelStringZigyoushoName.Value
                                                        , zigyoushoName
                                //2009/02/06　変更　T.Nakano  終了
                                                        , 4
                                                        , uriageZeiTumiageKBN
                                                        , siireZeiTumiageKBN
                                //2012/01/25 修正 OBIC T.Shiraishi (M04737) 開始
                                //, zeiKoujoKBN);
                                                        , zeiKoujoKBN
                                                        , koujoSitei);
                            //2012/01/25 修正 OBIC T.Shiraishi (M04737) 終了
                            if (kensuuCount == 0)
                            {
                                //システムエラー発生時
                                if (errorKBN == 1)
                                {
                                    //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                    //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                    throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                                }

                                //個別エラー発生時
                                if (errorKBN == 2)
                                //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                                {
                                    ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                    return false;
                                }

                                // データがなければエラー処理
                                Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                    , CUSTOMIZESEQ
                                                                    , MESSAGE_CD1
                                                                    , "00107"
                                                                    , null
                                                                    , null
                                                                    , this.GetType()
                                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                    );
                                return false;
                            }
                            else
                            {
                                // 検索結果が存在すれば印刷処理を行う
                                printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                                // 印刷用クラスInitialize
                                printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho4);
                                // 印刷ダイアログ表示
                                printClass.ShowPrintMenu();
                            }
                        }
                        //2012/01/25 修正 OBIC T.Shiraishi (M04737) 開始
                    }
                    // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 開始
                    else if (zigyounendoSeigyoKBN == 3)
                    {
                        //H27改正後：付表2
                        using (reportMA_ShouhizeiSinkokusho12 = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho12())
                        {

                            // レポートクラスのデータ抽出
                            kensuuCount = reportMA_ShouhizeiSinkokusho12.DataRead(
                                                          ref errorKBN
                                                        , ref errorCD1
                                                        , ref errorCD2
                                                        , ref errorProcName
                                                        , ref errorSummary
                                                        , ref errorDetail
                                                        , ref errorBtnKBN
                                                        , ref errorBtnDefault
                                                        , ref errorMsgDetail
                                                        , obic7TextDatetimeKikanST.Text
                                                        , obic7TextDatetimeKikanED.Text
                                                        , zigyoushoSitei
                                                        , obic7TextStringZigyoushoCD.Value
                                                        , zigyoushoName
                                                        , 4
                                                        , uriageZeiTumiageKBN
                                                        , siireZeiTumiageKBN
                                                        , zeiKoujoKBN
                                                        , koujoSitei);
                            if (kensuuCount == 0)
                            {
                                //システムエラー発生時
                                if (errorKBN == 1)
                                {
                                    //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                    //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                    throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                                }

                                //個別エラー発生時
                                if (errorKBN == 2)
                                //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                                {
                                    ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                    return false;
                                }

                                // データがなければエラー処理
                                Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                    , CUSTOMIZESEQ
                                                                    , MESSAGE_CD1
                                                                    , "00107"
                                                                    , null
                                                                    , null
                                                                    , this.GetType()
                                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                    );
                                return false;
                            }
                            else
                            {
                                // 検索結果が存在すれば印刷処理を行う
                                printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                                // 印刷用クラスInitialize
                                printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho12);
                                // 印刷ダイアログ表示
                                printClass.ShowPrintMenu();
                            }
                        }
                    }
                    // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 終了
                    //  2013/10/10  修正        T.Shiraishi      (M00060)平成26年消費税改正対応(申告書の対応)  開始↓
                    else if (zigyounendoSeigyoKBN == 2)
                    {
                        using (reportMA_ShouhizeiSinkokusho8 = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho8())
                        {

                            // レポートクラスのデータ抽出
                            kensuuCount = reportMA_ShouhizeiSinkokusho8.DataRead(
                                                          ref errorKBN
                                                        , ref errorCD1
                                                        , ref errorCD2
                                                        , ref errorProcName
                                                        , ref errorSummary
                                                        , ref errorDetail
                                                        , ref errorBtnKBN
                                                        , ref errorBtnDefault
                                                        , ref errorMsgDetail
                                                        , obic7TextDatetimeKikanST.Text
                                                        , obic7TextDatetimeKikanED.Text
                                                        , zigyoushoSitei
                                                        , obic7TextStringZigyoushoCD.Value
                                                        , zigyoushoName
                                                        , 4
                                                        , uriageZeiTumiageKBN
                                                        , siireZeiTumiageKBN
                                                        , zeiKoujoKBN
                                                        , koujoSitei);
                            if (kensuuCount == 0)
                            {
                                //システムエラー発生時
                                if (errorKBN == 1)
                                {
                                    //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                    //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                    throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                                }

                                //個別エラー発生時
                                if (errorKBN == 2)
                                //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                                {
                                    ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                    return false;
                                }

                                // データがなければエラー処理
                                Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                    , CUSTOMIZESEQ
                                                                    , MESSAGE_CD1
                                                                    , "00107"
                                                                    , null
                                                                    , null
                                                                    , this.GetType()
                                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                    );
                                return false;
                            }
                            else
                            {
                                // 検索結果が存在すれば印刷処理を行う
                                printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                                // 印刷用クラスInitialize
                                printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho8);
                                // 印刷ダイアログ表示
                                printClass.ShowPrintMenu();
                            }
                        }
                    }
                    //  2013/10/10  修正        T.Shiraishi      (M00060)平成26年消費税改正対応(申告書の対応)  終了↑
                    else
                    {

                        using (reportMA_ShouhizeiSinkokusho6 = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho6())
                        {

                            // レポートクラスのデータ抽出
                            kensuuCount = reportMA_ShouhizeiSinkokusho6.DataRead(
                                                          ref errorKBN
                                                        , ref errorCD1
                                                        , ref errorCD2
                                                        , ref errorProcName
                                                        , ref errorSummary
                                                        , ref errorDetail
                                                        , ref errorBtnKBN
                                                        , ref errorBtnDefault
                                                        , ref errorMsgDetail
                                                        , obic7TextDatetimeKikanST.Text
                                                        , obic7TextDatetimeKikanED.Text
                                                        , zigyoushoSitei
                                                        , obic7TextStringZigyoushoCD.Value
                                                        , zigyoushoName
                                                        , 4
                                                        , uriageZeiTumiageKBN
                                                        , siireZeiTumiageKBN
                                                        , zeiKoujoKBN
                                                        , koujoSitei);
                            if (kensuuCount == 0)
                            {
                                //システムエラー発生時
                                if (errorKBN == 1)
                                {
                                    //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                    //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                    throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                                }

                                //個別エラー発生時
                                if (errorKBN == 2)
                                //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                                {
                                    ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                    return false;
                                }

                                // データがなければエラー処理
                                Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                    , CUSTOMIZESEQ
                                                                    , MESSAGE_CD1
                                                                    , "00107"
                                                                    , null
                                                                    , null
                                                                    , this.GetType()
                                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                    );
                                return false;
                            }
                            else
                            {
                                // 検索結果が存在すれば印刷処理を行う
                                printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                                // 印刷用クラスInitialize
                                printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho6);
                                // 印刷ダイアログ表示
                                printClass.ShowPrintMenu();
                            }
                        }
                    }
                    //2012/01/25 修正 OBIC T.Shiraishi (M04737) 終了
                }
                else if (obic7RadioButtonHuhyou3.Checked == true)
                {
                    //2012/01/25 修正 OBIC T.Shiraishi (M04737) 開始
                    if (zigyounendoSeigyoKBN == 0)
                    {
                        //2012/01/25 修正 OBIC T.Shiraishi (M04737) 終了

                        using (reportMA_ShouhizeiSinkokusho5 = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho5())
                        {

                            // レポートクラスのデータ抽出
                            kensuuCount = reportMA_ShouhizeiSinkokusho5.DataRead(
                                                          ref errorKBN
                                                        , ref errorCD1
                                                        , ref errorCD2
                                                        , ref errorProcName
                                                        , ref errorSummary
                                                        , ref errorDetail
                                                        , ref errorBtnKBN
                                                        , ref errorBtnDefault
                                                        , ref errorMsgDetail
                                                        , obic7TextDatetimeKikanST.Text
                                                        , obic7TextDatetimeKikanED.Text
                                                        , zigyoushoSitei
                                                        , obic7TextStringZigyoushoCD.Value
                                //2009/02/06　変更　T.Nakano  開始　(M04156)事業所名を正式名称を表示出来るようし、それに伴いレイアウトも修正
                                //, obic7LabelStringZigyoushoName.Value
                                                        , zigyoushoName
                                //2009/02/06　変更　T.Nakano  終了
                                                        , 5
                                                        , uriageZeiTumiageKBN
                                                        , siireZeiTumiageKBN
                                //2012/01/25 修正 OBIC T.Shiraishi (M04737) 開始
                                //, zeiKoujoKBN);
                                                        , zeiKoujoKBN
                                                        , koujoSitei);
                            //2012/01/25 修正 OBIC T.Shiraishi (M04737) 終了
                            if (kensuuCount == 0)
                            {

                                //システムエラー発生時
                                if (errorKBN == 1)
                                {
                                    //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                    //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                    throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                                }


                                //個別エラー発生時
                                if (errorKBN == 2)
                                //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                                {
                                    ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                    return false;
                                }

                                // データがなければエラー処理
                                Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                    , CUSTOMIZESEQ
                                                                    , MESSAGE_CD1
                                                                    , "00107"
                                                                    , null
                                                                    , null
                                                                    , this.GetType()
                                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                    );
                                return false;
                            }
                            else
                            {
                                // 検索結果が存在すれば印刷処理を行う
                                printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                                // 印刷用クラスInitialize
                                printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho5);
                                // 印刷ダイアログ表示
                                printClass.ShowPrintMenu();
                            }
                        }
                        //2012/01/25 修正 OBIC T.Shiraishi (M04737) 開始
                    }
                    // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 開始
                    else if (zigyounendoSeigyoKBN == 3)
                    {
                        //H27年度改正後:付表2-(2)
                        using (reportMA_ShouhizeiSinkokusho13 = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho13())
                        {

                            // レポートクラスのデータ抽出
                            kensuuCount = reportMA_ShouhizeiSinkokusho13.DataRead(
                                                          ref errorKBN
                                                        , ref errorCD1
                                                        , ref errorCD2
                                                        , ref errorProcName
                                                        , ref errorSummary
                                                        , ref errorDetail
                                                        , ref errorBtnKBN
                                                        , ref errorBtnDefault
                                                        , ref errorMsgDetail
                                                        , obic7TextDatetimeKikanST.Text
                                                        , obic7TextDatetimeKikanED.Text
                                                        , zigyoushoSitei
                                                        , obic7TextStringZigyoushoCD.Value
                                                        , zigyoushoName
                                                        , 5
                                                        , uriageZeiTumiageKBN
                                                        , siireZeiTumiageKBN
                                                        , zeiKoujoKBN
                                                        , koujoSitei);
                            if (kensuuCount == 0)
                            {

                                //システムエラー発生時
                                if (errorKBN == 1)
                                {
                                    //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                    //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                    throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                                }


                                //個別エラー発生時
                                if (errorKBN == 2)
                                //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                                {
                                    ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                    return false;
                                }

                                // データがなければエラー処理
                                Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                    , CUSTOMIZESEQ
                                                                    , MESSAGE_CD1
                                                                    , "00107"
                                                                    , null
                                                                    , null
                                                                    , this.GetType()
                                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                    );
                                return false;
                            }
                            else
                            {
                                // 検索結果が存在すれば印刷処理を行う
                                printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                                // 印刷用クラスInitialize
                                printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho13);
                                // 印刷ダイアログ表示
                                printClass.ShowPrintMenu();
                            }
                        }
                    }
                    // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 終了
                    //  2013/10/10  修正        T.Shiraishi      (M00060)平成26年消費税改正対応(申告書の対応)  開始↓
                    else if (zigyounendoSeigyoKBN == 2)
                    {
                        using (reportMA_ShouhizeiSinkokusho9 = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho9())
                        {

                            // レポートクラスのデータ抽出
                            kensuuCount = reportMA_ShouhizeiSinkokusho9.DataRead(
                                                          ref errorKBN
                                                        , ref errorCD1
                                                        , ref errorCD2
                                                        , ref errorProcName
                                                        , ref errorSummary
                                                        , ref errorDetail
                                                        , ref errorBtnKBN
                                                        , ref errorBtnDefault
                                                        , ref errorMsgDetail
                                                        , obic7TextDatetimeKikanST.Text
                                                        , obic7TextDatetimeKikanED.Text
                                                        , zigyoushoSitei
                                                        , obic7TextStringZigyoushoCD.Value
                                                        , zigyoushoName
                                                        , 5
                                                        , uriageZeiTumiageKBN
                                                        , siireZeiTumiageKBN
                                                        , zeiKoujoKBN
                                                        , koujoSitei);
                            if (kensuuCount == 0)
                            {

                                //システムエラー発生時
                                if (errorKBN == 1)
                                {
                                    //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                    //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                    throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                                }


                                //個別エラー発生時
                                if (errorKBN == 2)
                                //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                                {
                                    ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                    return false;
                                }

                                // データがなければエラー処理
                                Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                    , CUSTOMIZESEQ
                                                                    , MESSAGE_CD1
                                                                    , "00107"
                                                                    , null
                                                                    , null
                                                                    , this.GetType()
                                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                    );
                                return false;
                            }
                            else
                            {
                                // 検索結果が存在すれば印刷処理を行う
                                printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                                // 印刷用クラスInitialize
                                printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho9);
                                // 印刷ダイアログ表示
                                printClass.ShowPrintMenu();
                            }
                        }
                    }
                    //  2013/10/10  修正        T.Shiraishi      (M00060)平成26年消費税改正対応(申告書の対応)  終了↑
                    else
                    {
                        using (reportMA_ShouhizeiSinkokusho7 = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho7())
                        {

                            // レポートクラスのデータ抽出
                            kensuuCount = reportMA_ShouhizeiSinkokusho7.DataRead(
                                                          ref errorKBN
                                                        , ref errorCD1
                                                        , ref errorCD2
                                                        , ref errorProcName
                                                        , ref errorSummary
                                                        , ref errorDetail
                                                        , ref errorBtnKBN
                                                        , ref errorBtnDefault
                                                        , ref errorMsgDetail
                                                        , obic7TextDatetimeKikanST.Text
                                                        , obic7TextDatetimeKikanED.Text
                                                        , zigyoushoSitei
                                                        , obic7TextStringZigyoushoCD.Value
                                                        , zigyoushoName
                                                        , 5
                                                        , uriageZeiTumiageKBN
                                                        , siireZeiTumiageKBN
                                                        , zeiKoujoKBN
                                                        , koujoSitei);
                            if (kensuuCount == 0)
                            {

                                //システムエラー発生時
                                if (errorKBN == 1)
                                {
                                    //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                    //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                    throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                                }


                                //個別エラー発生時
                                if (errorKBN == 2)
                                //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                                {
                                    ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                    return false;
                                }

                                // データがなければエラー処理
                                Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                    , CUSTOMIZESEQ
                                                                    , MESSAGE_CD1
                                                                    , "00107"
                                                                    , null
                                                                    , null
                                                                    , this.GetType()
                                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                    );
                                return false;
                            }
                            else
                            {
                                // 検索結果が存在すれば印刷処理を行う
                                printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                                // 印刷用クラスInitialize
                                printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho7);
                                // 印刷ダイアログ表示
                                printClass.ShowPrintMenu();
                            }
                        }
                    }
                    //2012/01/25 修正 OBIC T.Shiraishi (M04737) 終了
                }
                // 2018/05/11 T.Kimura 変更 (99108-99004)【消費税率10%対応】↓
            }
            // 平成31年度以降
            //2023/07/25    Y.Matsuzawa      修正      (221366)【消費税申告管理】R5年インボイス対応 ↓
            //else
            else if (zigyounendoSeigyoKBN == 4)
            //2023/07/25    Y.Matsuzawa      修正      (221366)【消費税申告管理】R5年インボイス対応 ↑
            {
                // ActiveReport印刷用のクラスのインスタンスを作成する
                if (obic7RadioButton_H31_KisoJouhou.Checked == true)
                {
                    using (reportMA_ShouhizeiSinkokusho_H31_SanshutuKisoSin = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_H31_SanshutuKisoSin())
                    {

                        // レポートクラスのデータ抽出
                        kensuuCount = reportMA_ShouhizeiSinkokusho_H31_SanshutuKisoSin.DataRead(
                                                      ref errorKBN
                                                    , ref errorCD1
                                                    , ref errorCD2
                                                    , ref errorProcName
                                                    , ref errorSummary
                                                    , ref errorDetail
                                                    , ref errorBtnKBN
                                                    , ref errorBtnDefault
                                                    , ref errorMsgDetail
                                                    , obic7TextDatetimeKikanST.Text
                                                    , obic7TextDatetimeKikanED.Text
                                                    , zigyoushoSitei
                                                    , obic7TextStringZigyoushoCD.Value
                                                    , obic7LabelStringZigyoushoName.Value
                                                    , 1
                                                    , uriageZeiTumiageKBN
                                                    , siireZeiTumiageKBN
                                                    , zeiKoujoKBN
                                                    , koujoSitei);
                        if (kensuuCount == 0)
                        {
                            //システムエラー発生時
                            if (errorKBN == 1)
                            {
                                //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                            }

                            //個別エラー発生時
                            if (errorKBN == 2)
                            //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                            {
                                ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                return false;
                            }

                            // データがなければエラー処理
                            Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                , CUSTOMIZESEQ
                                                                , MESSAGE_CD1
                                                                , "00107"
                                                                , null
                                                                , null
                                                                , this.GetType()
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                );
                            return false;
                        }
                        else
                        {
                            // 検索結果が存在すれば印刷処理を行う
                            printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                            // 印刷用クラスInitialize
                            printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho_H31_SanshutuKisoSin);
                            // 印刷ダイアログ表示
                            printClass.ShowPrintMenu();
                        }
                    }
                }
                else if (obic7RadioButton_H31_KisoJouhouKyuu.Checked == true)
                {
                    using (reportMA_ShouhizeiSinkokusho_H31_SanshutuKisoKyuu = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_H31_SanshutuKisoKyuu())
                    {

                        // レポートクラスのデータ抽出
                        kensuuCount = reportMA_ShouhizeiSinkokusho_H31_SanshutuKisoKyuu.DataRead(
                                                      ref errorKBN
                                                    , ref errorCD1
                                                    , ref errorCD2
                                                    , ref errorProcName
                                                    , ref errorSummary
                                                    , ref errorDetail
                                                    , ref errorBtnKBN
                                                    , ref errorBtnDefault
                                                    , ref errorMsgDetail
                                                    , obic7TextDatetimeKikanST.Text
                                                    , obic7TextDatetimeKikanED.Text
                                                    , zigyoushoSitei
                                                    , obic7TextStringZigyoushoCD.Value
                                                    , obic7LabelStringZigyoushoName.Value
                                                    , 7
                                                    , uriageZeiTumiageKBN
                                                    , siireZeiTumiageKBN
                                                    , zeiKoujoKBN
                                                    , koujoSitei);
                        if (kensuuCount == 0)
                        {
                            //システムエラー発生時
                            if (errorKBN == 1)
                            {
                                //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                            }

                            //個別エラー発生時
                            if (errorKBN == 2)
                            //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                            {
                                ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                return false;
                            }

                            // データがなければエラー処理
                            Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                , CUSTOMIZESEQ
                                                                , MESSAGE_CD1
                                                                , "00107"
                                                                , null
                                                                , null
                                                                , this.GetType()
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                );
                            return false;
                        }
                        else
                        {
                            // 検索結果が存在すれば印刷処理を行う
                            printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                            // 印刷用クラスInitialize
                            printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho_H31_SanshutuKisoKyuu);
                            // 印刷ダイアログ表示
                            printClass.ShowPrintMenu();
                        }
                    }

                }
                else if (obic7RadioButton_H31_Sinkokusho.Checked == true)
                {
                    //H31年度改正後:申告書
                    using (reportMA_ShouhizeiSinkokusho_H31_Sinkoku = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_H31_Sinkoku())
                    {

                        // レポートクラスのデータ抽出
                        kensuuCount = reportMA_ShouhizeiSinkokusho_H31_Sinkoku.DataRead(
                                                      ref errorKBN
                                                    , ref errorCD1
                                                    , ref errorCD2
                                                    , ref errorProcName
                                                    , ref errorSummary
                                                    , ref errorDetail
                                                    , ref errorBtnKBN
                                                    , ref errorBtnDefault
                                                    , ref errorMsgDetail
                                                    , obic7TextDatetimeKikanST.Text
                                                    , obic7TextDatetimeKikanED.Text
                                                    , zigyoushoSitei
                                                    , obic7TextStringZigyoushoCD.Value
                                                    , obic7LabelStringZigyoushoName.Value
                                                    , 2
                                                    , uriageZeiTumiageKBN
                                                    , siireZeiTumiageKBN
                                                    , _r5NenOctIgoUriageZeiTumiageKBN
                                                    , _r5NenOctIgoSiireZeiTumiageKBN
                                                    , zeiKoujoKBN
                                                    , koujoSitei
                                                    , houzinNO);
                        if (kensuuCount == 0)
                        {
                            //システムエラー発生時
                            if (errorKBN == 1)
                            {
                                //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                            }

                            //個別エラー発生時
                            if (errorKBN == 2)
                            //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                            {
                                ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                return false;
                            }

                            // データがなければエラー処理
                            Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                , CUSTOMIZESEQ
                                                                , MESSAGE_CD1
                                                                , "00107"
                                                                , null
                                                                , null
                                                                , this.GetType()
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                );
                            return false;
                        }
                        else
                        {
                            // 検索結果が存在すれば印刷処理を行う
                            printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                            // 印刷用クラスInitialize
                            printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho_H31_Sinkoku);
                            // 印刷ダイアログ表示
                            printClass.ShowPrintMenu();
                        }
                    }
                }
                else if (obic7RadioButton_H31_Utiwakesho.Checked == true)
                {
                    //H31年度改正後:内訳書
                    using (reportMA_ShouhizeiSinkokusho_H31_Utiwake = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_H31_Utiwake())
                    {
                        // レポートクラスのデータ抽出
                        kensuuCount = reportMA_ShouhizeiSinkokusho_H31_Utiwake.DataRead(
                                                      ref errorKBN
                                                    , ref errorCD1
                                                    , ref errorCD2
                                                    , ref errorProcName
                                                    , ref errorSummary
                                                    , ref errorDetail
                                                    , ref errorBtnKBN
                                                    , ref errorBtnDefault
                                                    , ref errorMsgDetail
                                                    , obic7TextDatetimeKikanST.Text
                                                    , obic7TextDatetimeKikanED.Text
                                                    , zigyoushoSitei
                                                    , obic7TextStringZigyoushoCD.Value
                                                    , obic7LabelStringZigyoushoName.Value
                                                    , 6
                                                    , uriageZeiTumiageKBN
                                                    , siireZeiTumiageKBN
                                                    , _r5NenOctIgoUriageZeiTumiageKBN
                                                    , _r5NenOctIgoSiireZeiTumiageKBN
                                                    , zeiKoujoKBN
                                                    , koujoSitei
                                                    , houzinNO);
                        if (kensuuCount == 0)
                        {
                            //システムエラー発生時
                            if (errorKBN == 1)
                            {
                                //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                            }

                            //個別エラー発生時
                            if (errorKBN == 2)
                            //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                            {
                                ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                return false;
                            }

                            // データがなければエラー処理
                            Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                , CUSTOMIZESEQ
                                                                , MESSAGE_CD1
                                                                , "00107"
                                                                , null
                                                                , null
                                                                , this.GetType()
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                );
                            return false;
                        }
                        else
                        {
                            // 検索結果が存在すれば印刷処理を行う
                            printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                            // 印刷用クラスInitialize
                            printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho_H31_Utiwake);
                            // 印刷ダイアログ表示
                            printClass.ShowPrintMenu();
                        }
                    }
                }
                else if (obic7RadioButton_H31_Huhyou11.Checked == true)
                {
                    //H31年度改正後:付表1-1
                    using (reportMA_ShouhizeiSinkokusho_H31_Huhyou11 = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_H31_Huhyou11())
                    {
                        // レポートクラスのデータ抽出
                        kensuuCount = reportMA_ShouhizeiSinkokusho_H31_Huhyou11.DataRead(
                                                      ref errorKBN
                                                    , ref errorCD1
                                                    , ref errorCD2
                                                    , ref errorProcName
                                                    , ref errorSummary
                                                    , ref errorDetail
                                                    , ref errorBtnKBN
                                                    , ref errorBtnDefault
                                                    , ref errorMsgDetail
                                                    , obic7TextDatetimeKikanST.Text
                                                    , obic7TextDatetimeKikanED.Text
                                                    , zigyoushoSitei
                                                    , obic7TextStringZigyoushoCD.Value
                                                    , zigyoushoName
                                                    , 8
                                                    , uriageZeiTumiageKBN
                                                    , siireZeiTumiageKBN
                                                    , zeiKoujoKBN
                                                    , koujoSitei);
                        if (kensuuCount == 0)
                        {
                            //システムエラー発生時
                            if (errorKBN == 1)
                            {
                                //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                            }

                            //個別エラー発生時
                            if (errorKBN == 2)
                            //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                            {
                                ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                return false;
                            }

                            // データがなければエラー処理
                            Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                , CUSTOMIZESEQ
                                                                , MESSAGE_CD1
                                                                , "00107"
                                                                , null
                                                                , null
                                                                , this.GetType()
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                );
                            return false;
                        }
                        else
                        {
                            // 検索結果が存在すれば印刷処理を行う
                            printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                            // 印刷用クラスInitialize
                            printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho_H31_Huhyou11);
                            // 印刷ダイアログ表示
                            printClass.ShowPrintMenu();
                        }
                    }
                }
                else if (obic7RadioButton_H31_Huhyou12.Checked == true)
                {
                    //H31年度改正後:付表1-2
                    using (reportMA_ShouhizeiSinkokusho_H31_Huhyou12 = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_H31_Huhyou12())
                    {
                        // レポートクラスのデータ抽出
                        kensuuCount = reportMA_ShouhizeiSinkokusho_H31_Huhyou12.DataRead(
                                                      ref errorKBN
                                                    , ref errorCD1
                                                    , ref errorCD2
                                                    , ref errorProcName
                                                    , ref errorSummary
                                                    , ref errorDetail
                                                    , ref errorBtnKBN
                                                    , ref errorBtnDefault
                                                    , ref errorMsgDetail
                                                    , obic7TextDatetimeKikanST.Text
                                                    , obic7TextDatetimeKikanED.Text
                                                    , zigyoushoSitei
                                                    , obic7TextStringZigyoushoCD.Value
                                                    , zigyoushoName
                                                    , 8
                                                    , uriageZeiTumiageKBN
                                                    , siireZeiTumiageKBN
                                                    , zeiKoujoKBN
                                                    , koujoSitei);
                        if (kensuuCount == 0)
                        {
                            //システムエラー発生時
                            if (errorKBN == 1)
                            {
                                //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                            }

                            //個別エラー発生時
                            if (errorKBN == 2)
                            //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                            {
                                ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                return false;
                            }

                            // データがなければエラー処理
                            Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                , CUSTOMIZESEQ
                                                                , MESSAGE_CD1
                                                                , "00107"
                                                                , null
                                                                , null
                                                                , this.GetType()
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                );
                            return false;
                        }
                        else
                        {
                            // 検索結果が存在すれば印刷処理を行う
                            printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                            // 印刷用クラスInitialize
                            printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho_H31_Huhyou12);
                            // 印刷ダイアログ表示
                            printClass.ShowPrintMenu();
                        }
                    }
                }
                else if (obic7RadioButton_H31_Huhyou21.Checked == true)
                {
                    //H31年度改正後:付表2-1
                    using (reportMA_ShouhizeiSinkokusho_H31_Huhyou21 = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_H31_Huhyou21())
                    {
                        // レポートクラスのデータ抽出
                        kensuuCount = reportMA_ShouhizeiSinkokusho_H31_Huhyou21.DataRead(
                                                      ref errorKBN
                                                    , ref errorCD1
                                                    , ref errorCD2
                                                    , ref errorProcName
                                                    , ref errorSummary
                                                    , ref errorDetail
                                                    , ref errorBtnKBN
                                                    , ref errorBtnDefault
                                                    , ref errorMsgDetail
                                                    , obic7TextDatetimeKikanST.Text
                                                    , obic7TextDatetimeKikanED.Text
                                                    , zigyoushoSitei
                                                    , obic7TextStringZigyoushoCD.Value
                                                    , zigyoushoName
                                                    , 8
                                                    , uriageZeiTumiageKBN
                                                    , siireZeiTumiageKBN
                                                    , zeiKoujoKBN
                                                    , koujoSitei);
                        if (kensuuCount == 0)
                        {
                            //システムエラー発生時
                            if (errorKBN == 1)
                            {
                                //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                            }

                            //個別エラー発生時
                            if (errorKBN == 2)
                            //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                            {
                                ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                return false;
                            }

                            // データがなければエラー処理
                            Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                , CUSTOMIZESEQ
                                                                , MESSAGE_CD1
                                                                , "00107"
                                                                , null
                                                                , null
                                                                , this.GetType()
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                );
                            return false;
                        }
                        else
                        {
                            // 検索結果が存在すれば印刷処理を行う
                            printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                            // 印刷用クラスInitialize
                            printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho_H31_Huhyou21);
                            // 印刷ダイアログ表示
                            printClass.ShowPrintMenu();
                        }
                    }
                }
                else if (obic7RadioButton_H31_Huhyou22.Checked == true)
                {
                    //H31年度改正後:付表2-2
                    using (reportMA_ShouhizeiSinkokusho_H31_Huhyou22 = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_H31_Huhyou22())
                    {
                        // レポートクラスのデータ抽出
                        kensuuCount = reportMA_ShouhizeiSinkokusho_H31_Huhyou22.DataRead(
                                                      ref errorKBN
                                                    , ref errorCD1
                                                    , ref errorCD2
                                                    , ref errorProcName
                                                    , ref errorSummary
                                                    , ref errorDetail
                                                    , ref errorBtnKBN
                                                    , ref errorBtnDefault
                                                    , ref errorMsgDetail
                                                    , obic7TextDatetimeKikanST.Text
                                                    , obic7TextDatetimeKikanED.Text
                                                    , zigyoushoSitei
                                                    , obic7TextStringZigyoushoCD.Value
                                                    , zigyoushoName
                                                    , 8
                                                    , uriageZeiTumiageKBN
                                                    , siireZeiTumiageKBN
                                                    , zeiKoujoKBN
                                                    , koujoSitei);
                        if (kensuuCount == 0)
                        {
                            //システムエラー発生時
                            if (errorKBN == 1)
                            {
                                //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                            }

                            //個別エラー発生時
                            if (errorKBN == 2)
                            //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                            {
                                ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                return false;
                            }

                            // データがなければエラー処理
                            Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                , CUSTOMIZESEQ
                                                                , MESSAGE_CD1
                                                                , "00107"
                                                                , null
                                                                , null
                                                                , this.GetType()
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                );
                            return false;
                        }
                        else
                        {
                            // 検索結果が存在すれば印刷処理を行う
                            printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                            // 印刷用クラスInitialize
                            printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho_H31_Huhyou22);
                            // 印刷ダイアログ表示
                            printClass.ShowPrintMenu();
                        }
                    }
                }
                //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↓
                else if (obic7RadioButton_H31_Huhyou23.Checked == true)
                {
                    //H31年度改正後:付表2-1
                    using (reportMA_ShouhizeiSinkokusho_H31_Huhyou23 = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_H31_Huhyou23())
                    {
                        // レポートクラスのデータ抽出
                        kensuuCount = reportMA_ShouhizeiSinkokusho_H31_Huhyou23.DataRead(
                                                      ref errorKBN
                                                    , ref errorCD1
                                                    , ref errorCD2
                                                    , ref errorProcName
                                                    , ref errorSummary
                                                    , ref errorDetail
                                                    , ref errorBtnKBN
                                                    , ref errorBtnDefault
                                                    , ref errorMsgDetail
                                                    , obic7TextDatetimeKikanST.Text
                                                    , obic7TextDatetimeKikanED.Text
                                                    , zigyoushoSitei
                                                    , obic7TextStringZigyoushoCD.Value
                                                    , zigyoushoName
                                                    , 8
                                                    , uriageZeiTumiageKBN
                                                    , siireZeiTumiageKBN
                                                    , zeiKoujoKBN
                                                    , koujoSitei);
                        if (kensuuCount == 0)
                        {
                            //システムエラー発生時
                            if (errorKBN == 1)
                            {
                                //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                            }

                            //個別エラー発生時
                            if (errorKBN == 2)
                            //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                            {
                                ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                return false;
                            }

                            // データがなければエラー処理
                            Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                , CUSTOMIZESEQ
                                                                , MESSAGE_CD1
                                                                , "00107"
                                                                , null
                                                                , null
                                                                , this.GetType()
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                );
                            return false;
                        }
                        else
                        {
                            // 検索結果が存在すれば印刷処理を行う
                            printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                            // 印刷用クラスInitialize
                            printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho_H31_Huhyou23);
                            // 印刷ダイアログ表示
                            printClass.ShowPrintMenu();
                        }
                    }
                    //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↑
                    //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↓
                }
                else if (obic7RadioButton_H31_Huhyou13.Checked == true)
                {
                    //H31年度改正後:付表2-1
                    using (reportMA_ShouhizeiSinkokusho_H31_Huhyou13 = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_H31_Huhyou13())
                    {
                        // レポートクラスのデータ抽出
                        kensuuCount = reportMA_ShouhizeiSinkokusho_H31_Huhyou13.DataRead(
                                                      ref errorKBN
                                                    , ref errorCD1
                                                    , ref errorCD2
                                                    , ref errorProcName
                                                    , ref errorSummary
                                                    , ref errorDetail
                                                    , ref errorBtnKBN
                                                    , ref errorBtnDefault
                                                    , ref errorMsgDetail
                                                    , obic7TextDatetimeKikanST.Text
                                                    , obic7TextDatetimeKikanED.Text
                                                    , zigyoushoSitei
                                                    , obic7TextStringZigyoushoCD.Value
                                                    , zigyoushoName
                                                    , 8
                                                    , uriageZeiTumiageKBN
                                                    , siireZeiTumiageKBN
                                                    , zeiKoujoKBN
                                                    , koujoSitei);
                        if (kensuuCount == 0)
                        {
                            //システムエラー発生時
                            if (errorKBN == 1)
                            {
                                //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                            }

                            //個別エラー発生時
                            if (errorKBN == 2)
                            //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                            {
                                ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                return false;
                            }

                            // データがなければエラー処理
                            Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                , CUSTOMIZESEQ
                                                                , MESSAGE_CD1
                                                                , "00107"
                                                                , null
                                                                , null
                                                                , this.GetType()
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                );
                            return false;
                        }
                        else
                        {
                            // 検索結果が存在すれば印刷処理を行う
                            printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                            // 印刷用クラスInitialize
                            printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho_H31_Huhyou13);
                            // 印刷ダイアログ表示
                            printClass.ShowPrintMenu();
                        }
                    }
                }
                //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↑
            }
            // 2018/05/11 T.Kimura 変更 (99108-99004)【消費税率10%対応】↑
            //2023/07/25    Y.Matsuzawa      修正      (221366)【消費税申告管理】R5年インボイス対応 ↓
            // 令和5年10月インボイス対応以降
            else if (zigyounendoSeigyoKBN == 5)
            {
                // ActiveReport印刷用のクラスのインスタンスを作成する
                if (obic7RadioButton_H31_KisoJouhou.Checked == true)
                {
                    using (reportMA_ShouhizeiSinkokusho_R5_SanshutuKisoSin = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_R5_SanshutuKisoSin())
                    {

                        // レポートクラスのデータ抽出
                        kensuuCount = reportMA_ShouhizeiSinkokusho_R5_SanshutuKisoSin.DataRead(
                                                      ref errorKBN
                                                    , ref errorCD1
                                                    , ref errorCD2
                                                    , ref errorProcName
                                                    , ref errorSummary
                                                    , ref errorDetail
                                                    , ref errorBtnKBN
                                                    , ref errorBtnDefault
                                                    , ref errorMsgDetail
                                                    , obic7TextDatetimeKikanST.Text
                                                    , obic7TextDatetimeKikanED.Text
                                                    , zigyoushoSitei
                                                    , obic7TextStringZigyoushoCD.Value
                                                    , obic7LabelStringZigyoushoName.Value
                                                    , 1
                                                    , uriageZeiTumiageKBN
                                                    , siireZeiTumiageKBN
                                                    , _r5NenOctIgoUriageZeiTumiageKBN
                                                    , _r5NenOctIgoSiireZeiTumiageKBN
                                                    , zeiKoujoKBN
                                                    , koujoSitei);
                        if (kensuuCount == 0)
                        {
                            //システムエラー発生時
                            if (errorKBN == 1)
                            {
                                //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                            }

                            //個別エラー発生時
                            if (errorKBN == 2)
                            //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                            {
                                ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                return false;
                            }

                            // データがなければエラー処理
                            Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                , CUSTOMIZESEQ
                                                                , MESSAGE_CD1
                                                                , "00107"
                                                                , null
                                                                , null
                                                                , this.GetType()
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                );
                            return false;
                        }
                        else
                        {
                            // 検索結果が存在すれば印刷処理を行う
                            printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                            // 印刷用クラスInitialize
                            printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho_R5_SanshutuKisoSin);
                            // 印刷ダイアログ表示
                            printClass.ShowPrintMenu();
                        }
                    }
                }
                else if (obic7RadioButton_H31_KisoJouhouKyuu.Checked == true)
                {
                    using (reportMA_ShouhizeiSinkokusho_R5_SanshutuKisoKyuu = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_R5_SanshutuKisoKyuu())
                    {

                        // レポートクラスのデータ抽出
                        kensuuCount = reportMA_ShouhizeiSinkokusho_R5_SanshutuKisoKyuu.DataRead(
                                                      ref errorKBN
                                                    , ref errorCD1
                                                    , ref errorCD2
                                                    , ref errorProcName
                                                    , ref errorSummary
                                                    , ref errorDetail
                                                    , ref errorBtnKBN
                                                    , ref errorBtnDefault
                                                    , ref errorMsgDetail
                                                    , obic7TextDatetimeKikanST.Text
                                                    , obic7TextDatetimeKikanED.Text
                                                    , zigyoushoSitei
                                                    , obic7TextStringZigyoushoCD.Value
                                                    , obic7LabelStringZigyoushoName.Value
                                                    , 7
                                                    , uriageZeiTumiageKBN
                                                    , siireZeiTumiageKBN
                                                    , _r5NenOctIgoUriageZeiTumiageKBN
                                                    , _r5NenOctIgoSiireZeiTumiageKBN
                                                    , zeiKoujoKBN
                                                    , koujoSitei);
                        if (kensuuCount == 0)
                        {
                            //システムエラー発生時
                            if (errorKBN == 1)
                            {
                                //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                            }

                            //個別エラー発生時
                            if (errorKBN == 2)
                            //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                            {
                                ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                return false;
                            }

                            // データがなければエラー処理
                            Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                , CUSTOMIZESEQ
                                                                , MESSAGE_CD1
                                                                , "00107"
                                                                , null
                                                                , null
                                                                , this.GetType()
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                );
                            return false;
                        }
                        else
                        {
                            // 検索結果が存在すれば印刷処理を行う
                            printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                            // 印刷用クラスInitialize
                            printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho_R5_SanshutuKisoKyuu);
                            // 印刷ダイアログ表示
                            printClass.ShowPrintMenu();
                        }
                    }

                }
                else if (obic7RadioButton_H31_Sinkokusho.Checked == true)
                {
                    //H31年度改正後:申告書
                    using (reportMA_ShouhizeiSinkokusho_H31_Sinkoku = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_H31_Sinkoku())
                    {

                        // レポートクラスのデータ抽出
                        kensuuCount = reportMA_ShouhizeiSinkokusho_H31_Sinkoku.DataRead(
                                                      ref errorKBN
                                                    , ref errorCD1
                                                    , ref errorCD2
                                                    , ref errorProcName
                                                    , ref errorSummary
                                                    , ref errorDetail
                                                    , ref errorBtnKBN
                                                    , ref errorBtnDefault
                                                    , ref errorMsgDetail
                                                    , obic7TextDatetimeKikanST.Text
                                                    , obic7TextDatetimeKikanED.Text
                                                    , zigyoushoSitei
                                                    , obic7TextStringZigyoushoCD.Value
                                                    , obic7LabelStringZigyoushoName.Value
                                                    , 2
                                                    , uriageZeiTumiageKBN
                                                    , siireZeiTumiageKBN
                                                    , _r5NenOctIgoUriageZeiTumiageKBN
                                                    , _r5NenOctIgoSiireZeiTumiageKBN
                                                    , zeiKoujoKBN
                                                    , koujoSitei
                                                    , houzinNO);
                        if (kensuuCount == 0)
                        {
                            //システムエラー発生時
                            if (errorKBN == 1)
                            {
                                //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                            }

                            //個別エラー発生時
                            if (errorKBN == 2)
                            //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                            {
                                ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                return false;
                            }

                            // データがなければエラー処理
                            Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                , CUSTOMIZESEQ
                                                                , MESSAGE_CD1
                                                                , "00107"
                                                                , null
                                                                , null
                                                                , this.GetType()
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                );
                            return false;
                        }
                        else
                        {
                            // 検索結果が存在すれば印刷処理を行う
                            printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                            // 印刷用クラスInitialize
                            printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho_H31_Sinkoku);
                            // 印刷ダイアログ表示
                            printClass.ShowPrintMenu();
                        }
                    }
                }
                else if (obic7RadioButton_H31_Utiwakesho.Checked == true)
                {
                    //H31年度改正後:内訳書
                    using (reportMA_ShouhizeiSinkokusho_H31_Utiwake = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_H31_Utiwake())
                    {
                        // レポートクラスのデータ抽出
                        kensuuCount = reportMA_ShouhizeiSinkokusho_H31_Utiwake.DataRead(
                                                      ref errorKBN
                                                    , ref errorCD1
                                                    , ref errorCD2
                                                    , ref errorProcName
                                                    , ref errorSummary
                                                    , ref errorDetail
                                                    , ref errorBtnKBN
                                                    , ref errorBtnDefault
                                                    , ref errorMsgDetail
                                                    , obic7TextDatetimeKikanST.Text
                                                    , obic7TextDatetimeKikanED.Text
                                                    , zigyoushoSitei
                                                    , obic7TextStringZigyoushoCD.Value
                                                    , obic7LabelStringZigyoushoName.Value
                                                    , 6
                                                    , uriageZeiTumiageKBN
                                                    , siireZeiTumiageKBN
                                                    , _r5NenOctIgoUriageZeiTumiageKBN
                                                    , _r5NenOctIgoSiireZeiTumiageKBN
                                                    , zeiKoujoKBN
                                                    , koujoSitei
                                                    , houzinNO);
                        if (kensuuCount == 0)
                        {
                            //システムエラー発生時
                            if (errorKBN == 1)
                            {
                                //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                            }

                            //個別エラー発生時
                            if (errorKBN == 2)
                            //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                            {
                                ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                return false;
                            }

                            // データがなければエラー処理
                            Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                , CUSTOMIZESEQ
                                                                , MESSAGE_CD1
                                                                , "00107"
                                                                , null
                                                                , null
                                                                , this.GetType()
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                );
                            return false;
                        }
                        else
                        {
                            // 検索結果が存在すれば印刷処理を行う
                            printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                            // 印刷用クラスInitialize
                            printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho_H31_Utiwake);
                            // 印刷ダイアログ表示
                            printClass.ShowPrintMenu();
                        }
                    }
                }
                else if (obic7RadioButton_H31_Huhyou11.Checked == true)
                {
                    //H31年度改正後:付表1-1
                    using (reportMA_ShouhizeiSinkokusho_R5_Huhyou11 = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_R5_Huhyou11())
                    {
                        // レポートクラスのデータ抽出
                        kensuuCount = reportMA_ShouhizeiSinkokusho_R5_Huhyou11.DataRead(
                                                      ref errorKBN
                                                    , ref errorCD1
                                                    , ref errorCD2
                                                    , ref errorProcName
                                                    , ref errorSummary
                                                    , ref errorDetail
                                                    , ref errorBtnKBN
                                                    , ref errorBtnDefault
                                                    , ref errorMsgDetail
                                                    , obic7TextDatetimeKikanST.Text
                                                    , obic7TextDatetimeKikanED.Text
                                                    , zigyoushoSitei
                                                    , obic7TextStringZigyoushoCD.Value
                                                    , zigyoushoName
                                                    , 8
                                                    , uriageZeiTumiageKBN
                                                    , siireZeiTumiageKBN
                                                    , _r5NenOctIgoUriageZeiTumiageKBN
                                                    , _r5NenOctIgoSiireZeiTumiageKBN
                                                    , zeiKoujoKBN
                                                    , koujoSitei);
                        if (kensuuCount == 0)
                        {
                            //システムエラー発生時
                            if (errorKBN == 1)
                            {
                                //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                            }

                            //個別エラー発生時
                            if (errorKBN == 2)
                            //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                            {
                                ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                return false;
                            }

                            // データがなければエラー処理
                            Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                , CUSTOMIZESEQ
                                                                , MESSAGE_CD1
                                                                , "00107"
                                                                , null
                                                                , null
                                                                , this.GetType()
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                );
                            return false;
                        }
                        else
                        {
                            // 検索結果が存在すれば印刷処理を行う
                            printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                            // 印刷用クラスInitialize
                            printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho_R5_Huhyou11);
                            // 印刷ダイアログ表示
                            printClass.ShowPrintMenu();
                        }
                    }
                }
                else if (obic7RadioButton_H31_Huhyou12.Checked == true)
                {
                    //H31年度改正後:付表1-2
                    using (reportMA_ShouhizeiSinkokusho_R5_Huhyou12 = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_R5_Huhyou12())
                    {
                        // レポートクラスのデータ抽出
                        kensuuCount = reportMA_ShouhizeiSinkokusho_R5_Huhyou12.DataRead(
                                                      ref errorKBN
                                                    , ref errorCD1
                                                    , ref errorCD2
                                                    , ref errorProcName
                                                    , ref errorSummary
                                                    , ref errorDetail
                                                    , ref errorBtnKBN
                                                    , ref errorBtnDefault
                                                    , ref errorMsgDetail
                                                    , obic7TextDatetimeKikanST.Text
                                                    , obic7TextDatetimeKikanED.Text
                                                    , zigyoushoSitei
                                                    , obic7TextStringZigyoushoCD.Value
                                                    , zigyoushoName
                                                    , 8
                                                    , uriageZeiTumiageKBN
                                                    , siireZeiTumiageKBN
                                                    , _r5NenOctIgoUriageZeiTumiageKBN
                                                    , _r5NenOctIgoSiireZeiTumiageKBN
                                                    , zeiKoujoKBN
                                                    , koujoSitei);
                        if (kensuuCount == 0)
                        {
                            //システムエラー発生時
                            if (errorKBN == 1)
                            {
                                //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                            }

                            //個別エラー発生時
                            if (errorKBN == 2)
                            //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                            {
                                ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                return false;
                            }

                            // データがなければエラー処理
                            Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                , CUSTOMIZESEQ
                                                                , MESSAGE_CD1
                                                                , "00107"
                                                                , null
                                                                , null
                                                                , this.GetType()
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                );
                            return false;
                        }
                        else
                        {
                            // 検索結果が存在すれば印刷処理を行う
                            printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                            // 印刷用クラスInitialize
                            printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho_R5_Huhyou12);
                            // 印刷ダイアログ表示
                            printClass.ShowPrintMenu();
                        }
                    }
                }
                else if (obic7RadioButton_H31_Huhyou13.Checked == true)
                {
                    //H31年度改正後:付表2-1
                    using (reportMA_ShouhizeiSinkokusho_R5_Huhyou13 = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_R5_Huhyou13())
                    {
                        // レポートクラスのデータ抽出
                        kensuuCount = reportMA_ShouhizeiSinkokusho_R5_Huhyou13.DataRead(
                                                      ref errorKBN
                                                    , ref errorCD1
                                                    , ref errorCD2
                                                    , ref errorProcName
                                                    , ref errorSummary
                                                    , ref errorDetail
                                                    , ref errorBtnKBN
                                                    , ref errorBtnDefault
                                                    , ref errorMsgDetail
                                                    , obic7TextDatetimeKikanST.Text
                                                    , obic7TextDatetimeKikanED.Text
                                                    , zigyoushoSitei
                                                    , obic7TextStringZigyoushoCD.Value
                                                    , zigyoushoName
                                                    , 8
                                                    , uriageZeiTumiageKBN
                                                    , siireZeiTumiageKBN
                                                    , _r5NenOctIgoUriageZeiTumiageKBN
                                                    , _r5NenOctIgoSiireZeiTumiageKBN
                                                    , zeiKoujoKBN
                                                    , koujoSitei);
                        if (kensuuCount == 0)
                        {
                            //システムエラー発生時
                            if (errorKBN == 1)
                            {
                                //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                            }

                            //個別エラー発生時
                            if (errorKBN == 2)
                            //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                            {
                                ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                return false;
                            }

                            // データがなければエラー処理
                            Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                , CUSTOMIZESEQ
                                                                , MESSAGE_CD1
                                                                , "00107"
                                                                , null
                                                                , null
                                                                , this.GetType()
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                );
                            return false;
                        }
                        else
                        {
                            // 検索結果が存在すれば印刷処理を行う
                            printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                            // 印刷用クラスInitialize
                            printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho_R5_Huhyou13);
                            // 印刷ダイアログ表示
                            printClass.ShowPrintMenu();
                        }
                    }
                }
                else if (obic7RadioButton_H31_Huhyou21.Checked == true)
                {
                    //H31年度改正後:付表2-1
                    using (reportMA_ShouhizeiSinkokusho_R5_Huhyou21 = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_R5_Huhyou21())
                    {
                        // レポートクラスのデータ抽出
                        kensuuCount = reportMA_ShouhizeiSinkokusho_R5_Huhyou21.DataRead(
                                                      ref errorKBN
                                                    , ref errorCD1
                                                    , ref errorCD2
                                                    , ref errorProcName
                                                    , ref errorSummary
                                                    , ref errorDetail
                                                    , ref errorBtnKBN
                                                    , ref errorBtnDefault
                                                    , ref errorMsgDetail
                                                    , obic7TextDatetimeKikanST.Text
                                                    , obic7TextDatetimeKikanED.Text
                                                    , zigyoushoSitei
                                                    , obic7TextStringZigyoushoCD.Value
                                                    , zigyoushoName
                                                    , 8
                                                    , uriageZeiTumiageKBN
                                                    , siireZeiTumiageKBN
                                                    , _r5NenOctIgoUriageZeiTumiageKBN
                                                    , _r5NenOctIgoSiireZeiTumiageKBN
                                                    , zeiKoujoKBN
                                                    , koujoSitei);
                        if (kensuuCount == 0)
                        {
                            //システムエラー発生時
                            if (errorKBN == 1)
                            {
                                //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                            }

                            //個別エラー発生時
                            if (errorKBN == 2)
                            //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                            {
                                ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                return false;
                            }

                            // データがなければエラー処理
                            Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                , CUSTOMIZESEQ
                                                                , MESSAGE_CD1
                                                                , "00107"
                                                                , null
                                                                , null
                                                                , this.GetType()
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                );
                            return false;
                        }
                        else
                        {
                            // 検索結果が存在すれば印刷処理を行う
                            printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                            // 印刷用クラスInitialize
                            printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho_R5_Huhyou21);
                            // 印刷ダイアログ表示
                            printClass.ShowPrintMenu();
                        }
                    }
                }
                else if (obic7RadioButton_H31_Huhyou22.Checked == true)
                {
                    //H31年度改正後:付表2-2
                    using (reportMA_ShouhizeiSinkokusho_R5_Huhyou22 = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_R5_Huhyou22())
                    {
                        // レポートクラスのデータ抽出
                        kensuuCount = reportMA_ShouhizeiSinkokusho_R5_Huhyou22.DataRead(
                                                      ref errorKBN
                                                    , ref errorCD1
                                                    , ref errorCD2
                                                    , ref errorProcName
                                                    , ref errorSummary
                                                    , ref errorDetail
                                                    , ref errorBtnKBN
                                                    , ref errorBtnDefault
                                                    , ref errorMsgDetail
                                                    , obic7TextDatetimeKikanST.Text
                                                    , obic7TextDatetimeKikanED.Text
                                                    , zigyoushoSitei
                                                    , obic7TextStringZigyoushoCD.Value
                                                    , zigyoushoName
                                                    , 8
                                                    , uriageZeiTumiageKBN
                                                    , siireZeiTumiageKBN
                                                    , _r5NenOctIgoUriageZeiTumiageKBN
                                                    , _r5NenOctIgoSiireZeiTumiageKBN
                                                    , zeiKoujoKBN
                                                    , koujoSitei);
                        if (kensuuCount == 0)
                        {
                            //システムエラー発生時
                            if (errorKBN == 1)
                            {
                                //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                            }

                            //個別エラー発生時
                            if (errorKBN == 2)
                            //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                            {
                                ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                return false;
                            }

                            // データがなければエラー処理
                            Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                , CUSTOMIZESEQ
                                                                , MESSAGE_CD1
                                                                , "00107"
                                                                , null
                                                                , null
                                                                , this.GetType()
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                );
                            return false;
                        }
                        else
                        {
                            // 検索結果が存在すれば印刷処理を行う
                            printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                            // 印刷用クラスInitialize
                            printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho_R5_Huhyou22);
                            // 印刷ダイアログ表示
                            printClass.ShowPrintMenu();
                        }
                    }
                }
                else if (obic7RadioButton_H31_Huhyou23.Checked == true)
                {
                    //H31年度改正後:付表2-1
                    using (reportMA_ShouhizeiSinkokusho_R5_Huhyou23 = new Obic7fx.Win.M0000.Report.MA_ShouhizeiSinkokusho.MA_ShouhizeiSinkokusho_R5_Huhyou23())
                    {
                        // レポートクラスのデータ抽出
                        kensuuCount = reportMA_ShouhizeiSinkokusho_R5_Huhyou23.DataRead(
                                                      ref errorKBN
                                                    , ref errorCD1
                                                    , ref errorCD2
                                                    , ref errorProcName
                                                    , ref errorSummary
                                                    , ref errorDetail
                                                    , ref errorBtnKBN
                                                    , ref errorBtnDefault
                                                    , ref errorMsgDetail
                                                    , obic7TextDatetimeKikanST.Text
                                                    , obic7TextDatetimeKikanED.Text
                                                    , zigyoushoSitei
                                                    , obic7TextStringZigyoushoCD.Value
                                                    , zigyoushoName
                                                    , 8
                                                    , uriageZeiTumiageKBN
                                                    , siireZeiTumiageKBN
                                                    , _r5NenOctIgoUriageZeiTumiageKBN
                                                    , _r5NenOctIgoSiireZeiTumiageKBN
                                                    , zeiKoujoKBN
                                                    , koujoSitei);
                        if (kensuuCount == 0)
                        {
                            //システムエラー発生時
                            if (errorKBN == 1)
                            {
                                //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                                //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                                throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + errorProcName.ToString() + " エラー内容 = " + errorSummary.ToString());
                            }

                            //個別エラー発生時
                            if (errorKBN == 2)
                            //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                            {
                                ProcError(errorCD1, errorCD2, errorProcName, errorSummary, errorDetail, errorBtnKBN, errorBtnDefault);
                                return false;
                            }

                            // データがなければエラー処理
                            Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                , CUSTOMIZESEQ
                                                                , MESSAGE_CD1
                                                                , "00107"
                                                                , null
                                                                , null
                                                                , this.GetType()
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                );
                            return false;
                        }
                        else
                        {
                            // 検索結果が存在すれば印刷処理を行う
                            printClass = new Obic7fx.Win.A0000.Tool.Logic.Print.Obic7Print();
                            // 印刷用クラスInitialize
                            printClass.PrintInitialize(reportMA_ShouhizeiSinkokusho_R5_Huhyou23);
                            // 印刷ダイアログ表示
                            printClass.ShowPrintMenu();
                        }
                    }
                }
            }
            //2023/07/25    Y.Matsuzawa      修正      (221366)【消費税申告管理】R5年インボイス対応 ↑
            return true;
        }
        #endregion

        // 2019/09/02  変更 M.Washizu (139334-139359)【電子申告対応】開始
        #region Xml確認用ファイル出力
        /// <summary>
        /// ファイル出力
        /// </summary>
        /// <returns>true:エラーなし false:エラーあり</returns>
        private bool OutPutFile()
        {
            // ファイル出力用
            DataTable selectMainData = new DataTable();
            // 変数宣言
            int zigyoushoSitei = 0;
            string kikanST = obic7TextDatetimeKikanST.Text.Replace("/", "");
            string kikanED = obic7TextDatetimeKikanED.Text.Replace("/", "");
            string fileName = "消費税申告_" + kikanST + "～" + kikanED + ".xlsx";
            string filePath = @obic7TextStringOutPath.Text + fileName;

            //ファイルが既に存在している場合
            if (CheckExistFile(fileName, filePath) == false)
            {
                return false;
            }

            // 事業所指定
            if (obic7RadioButtonZensha.Checked == true)
            {
                zigyoushoSitei = 1;
            }
            else
            {
                zigyoushoSitei = 2;
            }

            //  2023/07/24  修正        Y.Nakajima       (221366)【消費税申告管理】R5年インボイス対応(消費税申告の達人連携対応分) 開始
            //selectMainData = Data_Shuturyoku(
            //                  obic7TextDatetimeKikanST.Text
            //                , obic7TextDatetimeKikanED.Text
            //                , zigyoushoSitei
            //                , shuturyokuKBN
            //                , obic7TextStringZigyoushoCD.Value
            //            );

            //対象期間を変えてストアドを実行するため、計算結果格納用の一時WKを作成
            CreateTempCalcResultWK(businessDB);

            if (((uriageZeiTumiageKBN != _r5NenOctIgoUriageZeiTumiageKBN) || (siireZeiTumiageKBN != _r5NenOctIgoSiireZeiTumiageKBN))
                &&
                DateTime.Parse(this.obic7TextDatetimeKikanST.Text).CompareTo(DateTime.Parse("2023/09/30")) <= 0
                &&
                DateTime.Parse(this.obic7TextDatetimeKikanED.Text).CompareTo(DateTime.Parse("2023/10/01")) >= 0)
            {

                //①対象期間開始～R5.09.30の期間
                selectMainData = Data_Shuturyoku(
                                              obic7TextDatetimeKikanST.Text
                                            , "2023/09/30"
                                            , zigyoushoSitei
                                            , shuturyokuKBN
                                            , obic7TextStringZigyoushoCD.Value
                                            , uriageZeiTumiageKBN
                                            , siireZeiTumiageKBN
                                            , 1 //脱出する
                                        );

                //変数再初期化
                selectMainData = new DataTable();

                //②R5.10.01～対象期間終了の期間
                selectMainData = Data_Shuturyoku(
                                             "2023/10/01"
                                           , obic7TextDatetimeKikanED.Text
                                           , zigyoushoSitei
                                           , shuturyokuKBN
                                           , obic7TextStringZigyoushoCD.Value
                                           , _r5NenOctIgoUriageZeiTumiageKBN
                                           , _r5NenOctIgoSiireZeiTumiageKBN
                                           , 0 //脱出しない
                                       );
            }
            else if (DateTime.Parse(this.obic7TextDatetimeKikanED.Text).CompareTo(DateTime.Parse("2023/09/30")) <= 0)
            {
                selectMainData = Data_Shuturyoku(
                                              obic7TextDatetimeKikanST.Text
                                            , obic7TextDatetimeKikanED.Text
                                            , zigyoushoSitei
                                            , shuturyokuKBN
                                            , obic7TextStringZigyoushoCD.Value
                                            , uriageZeiTumiageKBN
                                            , siireZeiTumiageKBN
                                            , 0 //脱出しない
                                        );
            }
            else
            {
                selectMainData = Data_Shuturyoku(
                                              obic7TextDatetimeKikanST.Text
                                            , obic7TextDatetimeKikanED.Text
                                            , zigyoushoSitei
                                            , shuturyokuKBN
                                            , obic7TextStringZigyoushoCD.Value
                                            , _r5NenOctIgoUriageZeiTumiageKBN
                                            , _r5NenOctIgoSiireZeiTumiageKBN
                                            , 0 //脱出しない
                                        );
            }
            //  2023/07/24  修正        Y.Nakajima       (221366)【消費税申告管理】R5年インボイス対応(消費税申告の達人連携対応分) 終了

            // 取得データがない場合
            if (selectMainData == null)
            {
                return false;
            }

            if (selectMainData.Rows.Count == 0)
            {
                // データがなければエラー処理
                WinTool.Logic.UI.Obic7MessageBox.Show("M"
                                                    , "0000"
                                                    , "000"
                                                    , "00107"
                                                    , null
                                                    , null
                                                    , this.GetType()
                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                    );
                return false;
            }

            // 使用してない税区分を除外する
            // i=0(連動先業務プレフィックス)～i=12(2割特例の適用)は、必須項目のためチェックしない
            for (int i = selectMainData.Columns.Count - 1; i >= 13; --i)
            {
                string column = selectMainData.Rows[0][i].ToString();
                if (String.IsNullOrEmpty(column) || column == "0")
                {
                    // 列を削除
                    selectMainData.Columns.RemoveAt(i);
                }
            }

            // FileIOクラス
            Win.A0000.Tool.Logic.UI.FileIO fileIOMA_ShouhizeiSinkoku_DensiSinkoku = new Win.A0000.Tool.Logic.UI.FileIO();

            // 出力データをファイル出力用変数にセット
            fileIOMA_ShouhizeiSinkoku_DensiSinkoku.FileData = selectMainData;

            //ファイルの出力
            if (fileIOMA_ShouhizeiSinkoku_DensiSinkoku.WriteExcel(filePath) == false)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region Xmlファイル出力
        /// <summary>
        /// xmlファイル出力
        /// </summary>
        /// <returns></returns>
        private bool OutPutXmlFile()
        {
            // ファイル出力用
            DataTable selectMainData = new DataTable();
            // 変数宣言
            int zigyoushoSitei = 0;
            string kikanST = obic7TextDatetimeKikanST.Text.Replace("/", "");
            string kikanED = obic7TextDatetimeKikanED.Text.Replace("/", "");
            string fileName = "消費税申告_" + kikanST + "～" + kikanED + ".xml";
            string filePath = @obic7TextStringOutPath.Text + fileName;

            //ファイルが既に存在している場合
            if (CheckExistFile(fileName, filePath) == false)
            {
                return false;
            }

            // 事業所指定
            if (obic7RadioButtonZensha.Checked == true)
            {
                zigyoushoSitei = 1;
            }
            else
            {
                zigyoushoSitei = 2;
            }

            //  2023/07/24  修正        Y.Nakajima       (221366)【消費税申告管理】R5年インボイス対応(消費税申告の達人連携対応分) 開始
            //selectMainData = Data_Shuturyoku(
            //                              obic7TextDatetimeKikanST.Text
            //                            , obic7TextDatetimeKikanED.Text
            //                            , zigyoushoSitei
            //                            , shuturyokuKBN
            //                            , obic7TextStringZigyoushoCD.Value
            //                        );

            //対象期間を変えてストアドを実行するため、計算結果格納用の一時WKを作成
            CreateTempCalcResultWK(businessDB);
            if (((uriageZeiTumiageKBN != _r5NenOctIgoUriageZeiTumiageKBN) || (siireZeiTumiageKBN != _r5NenOctIgoSiireZeiTumiageKBN))
                &&
                DateTime.Parse(this.obic7TextDatetimeKikanST.Text).CompareTo(DateTime.Parse("2023/09/30")) <= 0
                &&
                DateTime.Parse(this.obic7TextDatetimeKikanED.Text).CompareTo(DateTime.Parse("2023/10/01")) >= 0)
            {
                //①対象期間開始～R5.09.30の期間
                selectMainData = Data_Shuturyoku(
                                              obic7TextDatetimeKikanST.Text
                                            , "2023/09/30"
                                            , zigyoushoSitei
                                            , shuturyokuKBN
                                            , obic7TextStringZigyoushoCD.Value
                                            , uriageZeiTumiageKBN
                                            , siireZeiTumiageKBN
                                            , 1 //脱出する
                                        );

                //変数再初期化
                selectMainData = new DataTable();

                //②R5.10.01～対象期間終了の期間
                selectMainData = Data_Shuturyoku(
                                             "2023/10/01"
                                           , obic7TextDatetimeKikanED.Text
                                           , zigyoushoSitei
                                           , shuturyokuKBN
                                           , obic7TextStringZigyoushoCD.Value
                                           , _r5NenOctIgoUriageZeiTumiageKBN
                                           , _r5NenOctIgoSiireZeiTumiageKBN
                                           , 0 //脱出しない
                                       );
            }
            else if (DateTime.Parse(this.obic7TextDatetimeKikanED.Text).CompareTo(DateTime.Parse("2023/09/30")) <= 0)
            {
                selectMainData = Data_Shuturyoku(
                                              obic7TextDatetimeKikanST.Text
                                            , obic7TextDatetimeKikanED.Text
                                            , zigyoushoSitei
                                            , shuturyokuKBN
                                            , obic7TextStringZigyoushoCD.Value
                                            , uriageZeiTumiageKBN
                                            , siireZeiTumiageKBN
                                            , 0 //脱出しない
                                        );
            }
            else
            {
                selectMainData = Data_Shuturyoku(
                                              obic7TextDatetimeKikanST.Text
                                            , obic7TextDatetimeKikanED.Text
                                            , zigyoushoSitei
                                            , shuturyokuKBN
                                            , obic7TextStringZigyoushoCD.Value
                                            , _r5NenOctIgoUriageZeiTumiageKBN
                                            , _r5NenOctIgoSiireZeiTumiageKBN
                                            , 0 //脱出しない
                                        );
            }
            //  2023/07/24  修正        Y.Nakajima       (221366)【消費税申告管理】R5年インボイス対応(消費税申告の達人連携対応分) 終了

            // 取得データがない場合
            if (selectMainData == null)
            {
                return false;
            }

            if (selectMainData.Rows.Count == 0)
            {
                // データがなければエラー処理
                WinTool.Logic.UI.Obic7MessageBox.Show("M"
                                                    , "0000"
                                                    , "000"
                                                    , "00107"
                                                    , null
                                                    , null
                                                    , this.GetType()
                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                    );
                return false;
            }

            XmlDocument retXmlDoc = new XmlDocument();
            String strXml = String.Empty;

            //雛形に値を取得した値をセット
            //  2023/07/24  修正        Y.Nakajima       (221366)【消費税申告管理】R5年インボイス対応(消費税申告の達人連携対応分) 開始
            if (zigyounendoSeigyoKBN >= 5)
            {
                var dataInfoInvoice = DataSetXmlInvoice(selectMainData);

                //名前空間出力抑制
                System.Xml.Serialization.XmlSerializerNamespaces ns = new System.Xml.Serialization.XmlSerializerNamespaces();
                ns.Add(String.Empty, String.Empty);

                //シリアライズ
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(MA_ShouhizeiSinkokusho_DensiSinkoku_Invoice.DataInfo));
                using (MemoryStream ms = new MemoryStream())
                {
                    XmlWriterSettings xws = new XmlWriterSettings();
                    xws.Encoding = Encoding.GetEncoding("Shift_JIS");

                    using (XmlWriter xw = XmlWriter.Create(ms, xws))
                    {
                        serializer.Serialize(xw, dataInfoInvoice, ns);
                    }

                    strXml = Encoding.GetEncoding("Shift_JIS").GetString(ms.ToArray());

                }
            }
            else if (zigyounendoSeigyoKBN < 5)
            {
                //  2023/07/24  修正        Y.Nakajima       (221366)【消費税申告管理】R5年インボイス対応(消費税申告の達人連携対応分) 終了
                var dataInfo = DataSetXml(selectMainData);

                //名前空間出力抑制
                System.Xml.Serialization.XmlSerializerNamespaces ns = new System.Xml.Serialization.XmlSerializerNamespaces();
                ns.Add(String.Empty, String.Empty);

                //シリアライズ
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(MA_ShouhizeiSinkokusho_DensiSinkoku.DataInfo));
                using (MemoryStream ms = new MemoryStream())
                {
                    XmlWriterSettings xws = new XmlWriterSettings();
                    xws.Encoding = Encoding.GetEncoding("Shift_JIS");

                    using (XmlWriter xw = XmlWriter.Create(ms, xws))
                    {
                        serializer.Serialize(xw, dataInfo, ns);
                    }

                    strXml = Encoding.GetEncoding("Shift_JIS").GetString(ms.ToArray());

                }
                //  2023/07/24  修正        Y.Nakajima       (221366)【消費税申告管理】R5年インボイス対応(消費税申告の達人連携対応分) 開始
            }
            //  2023/07/24  修正        Y.Nakajima       (221366)【消費税申告管理】R5年インボイス対応(消費税申告の達人連携対応分) 終了

            retXmlDoc.LoadXml(strXml);
            //不要なタグを削除
            RemoveElementXml(retXmlDoc);
            //指定したパスに出力
            retXmlDoc.Save(filePath);

            return true;
        }
        #endregion

        #region Xml雛形クラスに値をセット
        /// <summary>
        /// 雛形クラスに値をセット
        /// </summary>
        /// <returns></returns>
        private MA_ShouhizeiSinkokusho_DensiSinkoku.DataInfo DataSetXml(DataTable selectMainData)
        {
            MA_ShouhizeiSinkokusho_DensiSinkoku.DataInfo dataInfo = new MA_ShouhizeiSinkokusho_DensiSinkoku.DataInfo();

            //<SYSTEM>
            dataInfo.SystemTag.GYOUMUPREFIX = selectMainData.Rows[0]["GYOUMUPREFIX"].ToString();
            dataInfo.SystemTag.VERSION = selectMainData.Rows[0]["VERSION"].ToString();
            dataInfo.SystemTag.FUNCTIONID = selectMainData.Rows[0]["FUNCTIONID"].ToString();
            dataInfo.SystemTag.CONNECTEDPROGRAMVER = selectMainData.Rows[0]["CONNECTEDPROGRAMVER"].ToString();
            dataInfo.SystemTag.PROGRAMNAME = selectMainData.Rows[0]["PROGRAMNAME"].ToString();
            dataInfo.SystemTag.PROGRAMVER = selectMainData.Rows[0]["PROGRAMVER"].ToString();

            //<BASIC>
            dataInfo.BASIC.KOMONSAKINAME = selectMainData.Rows[0]["KOMONSAKINAME"].ToString();
            dataInfo.BASIC.JIGYOUNENDOFROM = selectMainData.Rows[0]["JIGYOUNENDOFROM"].ToString();
            dataInfo.BASIC.JIGYOUNENDOTO = selectMainData.Rows[0]["JIGYOUNENDOTO"].ToString();
            dataInfo.BASIC.HOUJINKOJINKUBUN = selectMainData.Rows[0]["HOUJINKOJINKUBUN"].ToString();
            dataInfo.BASIC.KAZEIKUBUN = selectMainData.Rows[0]["KAZEIKUBUN"].ToString();
            dataInfo.BASIC.KEIKASHOCHIKAZEISHISAN = selectMainData.Rows[0]["KEIKASHOCHIKAZEISHISAN"].ToString();
            dataInfo.BASIC.URIAGEZEIGAKUKEISAN = selectMainData.Rows[0]["URIAGEZEIGAKUKEISAN"].ToString();
            dataInfo.BASIC.SHIIREZEIGAKUKEISAN = selectMainData.Rows[0]["SHIIREZEIGAKUKEISAN"].ToString();

            //<DATA>
            //<D020114>
            dataInfo.DATA.D020114.KAZEIURIAGEZEINUKI10 = selectMainData.Rows[0]["KAZEIURIAGEZEINUKI10"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEINUKI10"].ToString();
            dataInfo.DATA.D020114.KAZEIURIAGEZEIGAKU10 = selectMainData.Rows[0]["KAZEIURIAGEZEIGAKU10"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEIGAKU10"].ToString();
            dataInfo.DATA.D020114.KAZEIURIAGEZEIKOMI10 = selectMainData.Rows[0]["KAZEIURIAGEZEIKOMI10"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEIKOMI10"].ToString();
            dataInfo.DATA.D020114.MENZEIURIAGEZEINUKI10 = selectMainData.Rows[0]["MENZEIURIAGEZEINUKI10"].ToString() == "0" ? null : selectMainData.Rows[0]["MENZEIURIAGEZEINUKI10"].ToString();
            dataInfo.DATA.D020114.HIKAZEIURIAGEYUUKAZEINUKI10 = selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAZEINUKI10"].ToString() == "0" ? null : selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAZEINUKI10"].ToString();
            dataInfo.DATA.D020114.HIKAZEIURIAGEYUUKAIGAIZEINUKI10 = selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAIGAIZEINUKI10"].ToString() == "0" ? null : selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAIGAIZEINUKI10"].ToString();
            dataInfo.DATA.D020114.HIKAZEISHISANYUSHUTSUZEINUKI10 = selectMainData.Rows[0]["HIKAZEISHISANYUSHUTSUZEINUKI10"].ToString() == "0" ? null : selectMainData.Rows[0]["HIKAZEISHISANYUSHUTSUZEINUKI10"].ToString();
            dataInfo.DATA.D020114.KAZEIURIAGEHENKANZEINUKI10 = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEINUKI10"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEINUKI10"].ToString();
            dataInfo.DATA.D020114.KAZEIURIAGEHENKANZEIGAKU10 = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIGAKU10"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIGAKU10"].ToString();
            dataInfo.DATA.D020114.KAZEIURIAGEHENKANZEIKOMI10 = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIKOMI10"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIKOMI10"].ToString();

            //<D020113>
            dataInfo.DATA.D020113.KAZEIURIAGEZEINUKI8K = selectMainData.Rows[0]["KAZEIURIAGEZEINUKI8K"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEINUKI8K"].ToString();
            dataInfo.DATA.D020113.KAZEIURIAGEZEIGAKU8K = selectMainData.Rows[0]["KAZEIURIAGEZEIGAKU8K"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEIGAKU8K"].ToString();
            dataInfo.DATA.D020113.KAZEIURIAGEZEIKOMI8K = selectMainData.Rows[0]["KAZEIURIAGEZEIKOMI8K"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEIKOMI8K"].ToString();
            dataInfo.DATA.D020113.MENZEIURIAGEZEINUKI8K = selectMainData.Rows[0]["MENZEIURIAGEZEINUKI8K"].ToString() == "0" ? null : selectMainData.Rows[0]["MENZEIURIAGEZEINUKI8K"].ToString();
            dataInfo.DATA.D020113.HIKAZEIURIAGEYUUKAZEINUKI8K = selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAZEINUKI8K"].ToString() == "0" ? null : selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAZEINUKI8K"].ToString();
            dataInfo.DATA.D020113.HIKAZEIURIAGEYUUKAIGAIZEINUKI8K = selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAIGAIZEINUKI8K"].ToString() == "0" ? null : selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAIGAIZEINUKI8K"].ToString();
            dataInfo.DATA.D020113.HIKAZEISHISANYUSHUTSUZEINUKI8K = selectMainData.Rows[0]["HIKAZEISHISANYUSHUTSUZEINUKI8K"].ToString() == "0" ? null : selectMainData.Rows[0]["HIKAZEISHISANYUSHUTSUZEINUKI8K"].ToString();
            dataInfo.DATA.D020113.KAZEIURIAGEHENKANZEINUKI8K = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEINUKI8K"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEINUKI8K"].ToString();
            dataInfo.DATA.D020113.KAZEIURIAGEHENKANZEIGAKU8K = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIGAKU8K"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIGAKU8K"].ToString();
            dataInfo.DATA.D020113.KAZEIURIAGEHENKANZEIKOMI8K = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIKOMI8K"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIKOMI8K"].ToString();

            //<D020110>
            dataInfo.DATA.D020110.KAZEIURIAGEZEINUKI8 = selectMainData.Rows[0]["KAZEIURIAGEZEINUKI8"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEINUKI8"].ToString();
            dataInfo.DATA.D020110.KAZEIURIAGEZEIGAKU8 = selectMainData.Rows[0]["KAZEIURIAGEZEIGAKU8"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEIGAKU8"].ToString();
            dataInfo.DATA.D020110.KAZEIURIAGEZEIKOMI8 = selectMainData.Rows[0]["KAZEIURIAGEZEIKOMI8"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEIKOMI8"].ToString();
            dataInfo.DATA.D020110.MENZEIURIAGEZEINUKI8 = selectMainData.Rows[0]["MENZEIURIAGEZEINUKI8"].ToString() == "0" ? null : selectMainData.Rows[0]["MENZEIURIAGEZEINUKI8"].ToString();
            dataInfo.DATA.D020110.HIKAZEIURIAGEYUUKAZEINUKI8 = selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAZEINUKI8"].ToString() == "0" ? null : selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAZEINUKI8"].ToString();
            dataInfo.DATA.D020110.HIKAZEIURIAGEYUUKAIGAIZEINUKI8 = selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAIGAIZEINUKI8"].ToString() == "0" ? null : selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAIGAIZEINUKI8"].ToString();
            dataInfo.DATA.D020110.HIKAZEISHISANYUSHUTSUZEINUKI8 = selectMainData.Rows[0]["HIKAZEISHISANYUSHUTSUZEINUKI8"].ToString() == "0" ? null : selectMainData.Rows[0]["HIKAZEISHISANYUSHUTSUZEINUKI8"].ToString();
            dataInfo.DATA.D020110.KAZEIURIAGEHENKANZEINUKI8 = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEINUKI8"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEINUKI8"].ToString();
            dataInfo.DATA.D020110.KAZEIURIAGEHENKANZEIGAKU8 = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIGAKU8"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIGAKU8"].ToString();
            dataInfo.DATA.D020110.KAZEIURIAGEHENKANZEIKOMI8 = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIKOMI8"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIKOMI8"].ToString();

            //<D020111>
            dataInfo.DATA.D020111.KAZEIURIAGEZEINUKI5 = selectMainData.Rows[0]["KAZEIURIAGEZEINUKI5"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEINUKI5"].ToString();
            dataInfo.DATA.D020111.KAZEIURIAGEZEIGAKU5 = selectMainData.Rows[0]["KAZEIURIAGEZEIGAKU5"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEIGAKU5"].ToString();
            dataInfo.DATA.D020111.KAZEIURIAGEZEIKOMI5 = selectMainData.Rows[0]["KAZEIURIAGEZEIKOMI5"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEIKOMI5"].ToString();
            dataInfo.DATA.D020111.MENZEIURIAGEZEINUKI5 = selectMainData.Rows[0]["MENZEIURIAGEZEINUKI5"].ToString() == "0" ? null : selectMainData.Rows[0]["MENZEIURIAGEZEINUKI5"].ToString();
            dataInfo.DATA.D020111.HIKAZEIURIAGEYUUKAZEINUKI5 = selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAZEINUKI5"].ToString() == "0" ? null : selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAZEINUKI5"].ToString();
            dataInfo.DATA.D020111.HIKAZEIURIAGEYUUKAIGAIZEINUKI5 = selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAIGAIZEINUKI5"].ToString() == "0" ? null : selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAIGAIZEINUKI5"].ToString();
            dataInfo.DATA.D020111.HIKAZEISHISANYUSHUTSUZEINUKI5 = selectMainData.Rows[0]["HIKAZEISHISANYUSHUTSUZEINUKI5"].ToString() == "0" ? null : selectMainData.Rows[0]["HIKAZEISHISANYUSHUTSUZEINUKI5"].ToString();
            dataInfo.DATA.D020111.KAZEIURIAGEHENKANZEINUKI5 = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEINUKI5"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEINUKI5"].ToString();
            dataInfo.DATA.D020111.KAZEIURIAGEHENKANZEIGAKU5 = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIGAKU5"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIGAKU5"].ToString();
            dataInfo.DATA.D020111.KAZEIURIAGEHENKANZEIKOMI5 = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIKOMI5"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIKOMI5"].ToString();

            //<D020112>
            dataInfo.DATA.D020112.KAZEIURIAGEZEINUKI3 = selectMainData.Rows[0]["KAZEIURIAGEZEINUKI3"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEINUKI3"].ToString();
            dataInfo.DATA.D020112.KAZEIURIAGEZEIGAKU3 = selectMainData.Rows[0]["KAZEIURIAGEZEIGAKU3"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEIGAKU3"].ToString();
            dataInfo.DATA.D020112.KAZEIURIAGEZEIKOMI3 = selectMainData.Rows[0]["KAZEIURIAGEZEIKOMI3"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEIKOMI3"].ToString();
            dataInfo.DATA.D020112.MENZEIURIAGEZEINUKI3 = selectMainData.Rows[0]["MENZEIURIAGEZEINUKI3"].ToString() == "0" ? null : selectMainData.Rows[0]["MENZEIURIAGEZEINUKI3"].ToString();
            dataInfo.DATA.D020112.HIKAZEIURIAGEYUUKAZEINUKI3 = selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAZEINUKI3"].ToString() == "0" ? null : selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAZEINUKI3"].ToString();
            dataInfo.DATA.D020112.HIKAZEIURIAGEYUUKAIGAIZEINUKI3 = selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAIGAIZEINUKI3"].ToString() == "0" ? null : selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAIGAIZEINUKI3"].ToString();
            dataInfo.DATA.D020112.HIKAZEISHISANYUSHUTSUZEINUKI3 = selectMainData.Rows[0]["HIKAZEISHISANYUSHUTSUZEINUKI3"].ToString() == "0" ? null : selectMainData.Rows[0]["HIKAZEISHISANYUSHUTSUZEINUKI3"].ToString();
            dataInfo.DATA.D020112.KAZEIURIAGEHENKANZEINUKI3 = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEINUKI3"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEINUKI3"].ToString();
            dataInfo.DATA.D020112.KAZEIURIAGEHENKANZEIGAKU3 = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIGAKU3"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIGAKU3"].ToString();
            dataInfo.DATA.D020112.KAZEIURIAGEHENKANZEIKOMI3 = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIKOMI3"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIKOMI3"].ToString();

            //<D020124>
            dataInfo.DATA.D020124.SHIIREKAZEIZEINUKI10 = selectMainData.Rows[0]["SHIIREKAZEIZEINUKI10"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEINUKI10"].ToString();
            dataInfo.DATA.D020124.SHIIREKAZEIZEIGAKU10 = selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU10"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU10"].ToString();
            dataInfo.DATA.D020124.SHIIREKAZEIZEIKOMI10 = selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI10"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI10"].ToString();
            dataInfo.DATA.D020124.SHIIREHIKAZEIZEINUKI10 = selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI10"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI10"].ToString();
            dataInfo.DATA.D020124.SHIIREHIKAZEIZEIGAKU10 = selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU10"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU10"].ToString();
            dataInfo.DATA.D020124.SHIIREHIKAZEIZEIKOMI10 = selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI10"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI10"].ToString();
            dataInfo.DATA.D020124.SHIIREKYOTUUZEINUKI10 = selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI10"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI10"].ToString();
            dataInfo.DATA.D020124.SHIIREKYOTUUZEIGAKU10 = selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU10"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU10"].ToString();
            dataInfo.DATA.D020124.SHIIREKYOTUUZEIKOMI10 = selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI10"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI10"].ToString();
            dataInfo.DATA.D020124.TOKUTEISHIIREKAZEIZEINUKI10 = selectMainData.Rows[0]["TOKUTEISHIIREKAZEIZEINUKI10"].ToString() == "0" ? null : selectMainData.Rows[0]["TOKUTEISHIIREKAZEIZEINUKI10"].ToString();
            dataInfo.DATA.D020124.TOKUTEISHIIREHIKAZEIZEINUKI10 = selectMainData.Rows[0]["TOKUTEISHIIREHIKAZEIZEINUKI10"].ToString() == "0" ? null : selectMainData.Rows[0]["TOKUTEISHIIREHIKAZEIZEINUKI10"].ToString();
            dataInfo.DATA.D020124.TOKUTEISHIIREKYOTUUZEINUKI10 = selectMainData.Rows[0]["TOKUTEISHIIREKYOTUUZEINUKI10"].ToString() == "0" ? null : selectMainData.Rows[0]["TOKUTEISHIIREKYOTUUZEINUKI10"].ToString();
            dataInfo.DATA.D020124.YUNYUSHIIREKAZEIZEIGAKU10 = selectMainData.Rows[0]["YUNYUSHIIREKAZEIZEIGAKU10"].ToString() == "0" ? null : selectMainData.Rows[0]["YUNYUSHIIREKAZEIZEIGAKU10"].ToString();
            dataInfo.DATA.D020124.YUNYUSHIIREHIKAZEIZEIGAKU10 = selectMainData.Rows[0]["YUNYUSHIIREHIKAZEIZEIGAKU10"].ToString() == "0" ? null : selectMainData.Rows[0]["YUNYUSHIIREHIKAZEIZEIGAKU10"].ToString();
            dataInfo.DATA.D020124.YUNYUSHIIREKYOTUUZEIGAKU10 = selectMainData.Rows[0]["YUNYUSHIIREKYOTUUZEIGAKU10"].ToString() == "0" ? null : selectMainData.Rows[0]["YUNYUSHIIREKYOTUUZEIGAKU10"].ToString();
            dataInfo.DATA.D020124.YUNYUSHIIRETIHO10 = selectMainData.Rows[0]["YUNYUSHIIRETIHO10"].ToString() == "0" ? null : selectMainData.Rows[0]["YUNYUSHIIRETIHO10"].ToString();
            dataInfo.DATA.D020124.TOKUTEISHIIREHENKANKAZEIZEINUKI10 = selectMainData.Rows[0]["TOKUTEISHIIREHENKANKAZEIZEINUKI10"].ToString() == "0" ? null : selectMainData.Rows[0]["TOKUTEISHIIREHENKANKAZEIZEINUKI10"].ToString();
            dataInfo.DATA.D020124.TOKUTEISHIIREHENKANHIKAZEIZEINUKI10 = selectMainData.Rows[0]["TOKUTEISHIIREHENKANHIKAZEIZEINUKI10"].ToString() == "0" ? null : selectMainData.Rows[0]["TOKUTEISHIIREHENKANHIKAZEIZEINUKI10"].ToString();
            dataInfo.DATA.D020124.TOKUTEISHIIREHENKANKYOTUUZEINUKI10 = selectMainData.Rows[0]["TOKUTEISHIIREHENKANKYOTUUZEINUKI10"].ToString() == "0" ? null : selectMainData.Rows[0]["TOKUTEISHIIREHENKANKYOTUUZEINUKI10"].ToString();

            //<D020123>
            dataInfo.DATA.D020123.SHIIREKAZEIZEINUKI8K = selectMainData.Rows[0]["SHIIREKAZEIZEINUKI8K"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEINUKI8K"].ToString();
            dataInfo.DATA.D020123.SHIIREKAZEIZEIGAKU8K = selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU8K"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU8K"].ToString();
            dataInfo.DATA.D020123.SHIIREKAZEIZEIKOMI8K = selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI8K"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI8K"].ToString();
            dataInfo.DATA.D020123.SHIIREHIKAZEIZEINUKI8K = selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI8K"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI8K"].ToString();
            dataInfo.DATA.D020123.SHIIREHIKAZEIZEIGAKU8K = selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU8K"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU8K"].ToString();
            dataInfo.DATA.D020123.SHIIREHIKAZEIZEIKOMI8K = selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI8K"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI8K"].ToString();
            dataInfo.DATA.D020123.SHIIREKYOTUUZEINUKI8K = selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI8K"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI8K"].ToString();
            dataInfo.DATA.D020123.SHIIREKYOTUUZEIGAKU8K = selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU8K"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU8K"].ToString();
            dataInfo.DATA.D020123.SHIIREKYOTUUZEIKOMI8K = selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI8K"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI8K"].ToString();
            dataInfo.DATA.D020123.YUNYUSHIIREKAZEIZEIGAKU8K = selectMainData.Rows[0]["YUNYUSHIIREKAZEIZEIGAKU8K"].ToString() == "0" ? null : selectMainData.Rows[0]["YUNYUSHIIREKAZEIZEIGAKU8K"].ToString();
            dataInfo.DATA.D020123.YUNYUSHIIREHIKAZEIZEIGAKU8K = selectMainData.Rows[0]["YUNYUSHIIREHIKAZEIZEIGAKU8K"].ToString() == "0" ? null : selectMainData.Rows[0]["YUNYUSHIIREHIKAZEIZEIGAKU8K"].ToString();
            dataInfo.DATA.D020123.YUNYUSHIIREKYOTUUZEIGAKU8K = selectMainData.Rows[0]["YUNYUSHIIREKYOTUUZEIGAKU8K"].ToString() == "0" ? null : selectMainData.Rows[0]["YUNYUSHIIREKYOTUUZEIGAKU8K"].ToString();
            dataInfo.DATA.D020123.YUNYUSHIIRETIHO8K = selectMainData.Rows[0]["YUNYUSHIIRETIHO8K"].ToString() == "0" ? null : selectMainData.Rows[0]["YUNYUSHIIRETIHO8K"].ToString();

            //<D020120>
            dataInfo.DATA.D020120.SHIIREKAZEIZEINUKI8 = selectMainData.Rows[0]["SHIIREKAZEIZEINUKI8"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEINUKI8"].ToString();
            dataInfo.DATA.D020120.SHIIREKAZEIZEIGAKU8 = selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU8"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU8"].ToString();
            dataInfo.DATA.D020120.SHIIREKAZEIZEIKOMI8 = selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI8"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI8"].ToString();
            dataInfo.DATA.D020120.SHIIREHIKAZEIZEINUKI8 = selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI8"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI8"].ToString();
            dataInfo.DATA.D020120.SHIIREHIKAZEIZEIGAKU8 = selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU8"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU8"].ToString();
            dataInfo.DATA.D020120.SHIIREHIKAZEIZEIKOMI8 = selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI8"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI8"].ToString();
            dataInfo.DATA.D020120.SHIIREKYOTUUZEINUKI8 = selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI8"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI8"].ToString();
            dataInfo.DATA.D020120.SHIIREKYOTUUZEIGAKU8 = selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU8"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU8"].ToString();
            dataInfo.DATA.D020120.SHIIREKYOTUUZEIKOMI8 = selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI8"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI8"].ToString();
            dataInfo.DATA.D020120.TOKUTEISHIIREKAZEIZEINUKI8 = selectMainData.Rows[0]["TOKUTEISHIIREKAZEIZEINUKI8"].ToString() == "0" ? null : selectMainData.Rows[0]["TOKUTEISHIIREKAZEIZEINUKI8"].ToString();
            dataInfo.DATA.D020120.TOKUTEISHIIREHIKAZEIZEINUKI8 = selectMainData.Rows[0]["TOKUTEISHIIREHIKAZEIZEINUKI8"].ToString() == "0" ? null : selectMainData.Rows[0]["TOKUTEISHIIREHIKAZEIZEINUKI8"].ToString();
            dataInfo.DATA.D020120.TOKUTEISHIIREKYOTUUZEINUKI8 = selectMainData.Rows[0]["TOKUTEISHIIREKYOTUUZEINUKI8"].ToString() == "0" ? null : selectMainData.Rows[0]["TOKUTEISHIIREKYOTUUZEINUKI8"].ToString();
            dataInfo.DATA.D020120.YUNYUSHIIREKAZEIZEIGAKU8 = selectMainData.Rows[0]["YUNYUSHIIREKAZEIZEIGAKU8"].ToString() == "0" ? null : selectMainData.Rows[0]["YUNYUSHIIREKAZEIZEIGAKU8"].ToString();
            dataInfo.DATA.D020120.YUNYUSHIIREHIKAZEIZEIGAKU8 = selectMainData.Rows[0]["YUNYUSHIIREHIKAZEIZEIGAKU8"].ToString() == "0" ? null : selectMainData.Rows[0]["YUNYUSHIIREHIKAZEIZEIGAKU8"].ToString();
            dataInfo.DATA.D020120.YUNYUSHIIREKYOTUUZEIGAKU8 = selectMainData.Rows[0]["YUNYUSHIIREKYOTUUZEIGAKU8"].ToString() == "0" ? null : selectMainData.Rows[0]["YUNYUSHIIREKYOTUUZEIGAKU8"].ToString();
            dataInfo.DATA.D020120.YUNYUSHIIRETIHO8 = selectMainData.Rows[0]["YUNYUSHIIRETIHO8"].ToString() == "0" ? null : selectMainData.Rows[0]["YUNYUSHIIRETIHO8"].ToString();
            dataInfo.DATA.D020120.TOKUTEISHIIREHENKANKAZEIZEINUKI8 = selectMainData.Rows[0]["TOKUTEISHIIREHENKANKAZEIZEINUKI8"].ToString() == "0" ? null : selectMainData.Rows[0]["TOKUTEISHIIREHENKANKAZEIZEINUKI8"].ToString();
            dataInfo.DATA.D020120.TOKUTEISHIIREHENKANHIKAZEIZEINUKI8 = selectMainData.Rows[0]["TOKUTEISHIIREHENKANHIKAZEIZEINUKI8"].ToString() == "0" ? null : selectMainData.Rows[0]["TOKUTEISHIIREHENKANHIKAZEIZEINUKI8"].ToString();
            dataInfo.DATA.D020120.TOKUTEISHIIREHENKANKYOTUUZEINUKI8 = selectMainData.Rows[0]["TOKUTEISHIIREHENKANKYOTUUZEINUKI8"].ToString() == "0" ? null : selectMainData.Rows[0]["TOKUTEISHIIREHENKANKYOTUUZEINUKI8"].ToString();

            //<D020121>
            dataInfo.DATA.D020121.SHIIREKAZEIZEINUKI5 = selectMainData.Rows[0]["SHIIREKAZEIZEINUKI5"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEINUKI5"].ToString();
            dataInfo.DATA.D020121.SHIIREKAZEIZEIGAKU5 = selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU5"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU5"].ToString();
            dataInfo.DATA.D020121.SHIIREKAZEIZEIKOMI5 = selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI5"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI5"].ToString();
            dataInfo.DATA.D020121.SHIIREHIKAZEIZEINUKI5 = selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI5"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI5"].ToString();
            dataInfo.DATA.D020121.SHIIREHIKAZEIZEIGAKU5 = selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU5"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU5"].ToString();
            dataInfo.DATA.D020121.SHIIREHIKAZEIZEIKOMI5 = selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI5"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI5"].ToString();
            dataInfo.DATA.D020121.SHIIREKYOTUUZEINUKI5 = selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI5"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI5"].ToString();
            dataInfo.DATA.D020121.SHIIREKYOTUUZEIGAKU5 = selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU5"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU5"].ToString();
            dataInfo.DATA.D020121.SHIIREKYOTUUZEIKOMI5 = selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI5"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI5"].ToString();

            //<D020122>
            dataInfo.DATA.D020122.SHIIREKAZEIZEINUKI3 = selectMainData.Rows[0]["SHIIREKAZEIZEINUKI3"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEINUKI3"].ToString();
            dataInfo.DATA.D020122.SHIIREKAZEIZEIGAKU3 = selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU3"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU3"].ToString();
            dataInfo.DATA.D020122.SHIIREKAZEIZEIKOMI3 = selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI3"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI3"].ToString();
            dataInfo.DATA.D020122.SHIIREHIKAZEIZEINUKI3 = selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI3"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI3"].ToString();
            dataInfo.DATA.D020122.SHIIREHIKAZEIZEIGAKU3 = selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU3"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU3"].ToString();
            dataInfo.DATA.D020122.SHIIREHIKAZEIZEIKOMI3 = selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI3"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI3"].ToString();
            dataInfo.DATA.D020122.SHIIREKYOTUUZEINUKI3 = selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI3"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI3"].ToString();
            dataInfo.DATA.D020122.SHIIREKYOTUUZEIGAKU3 = selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU3"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU3"].ToString();
            dataInfo.DATA.D020122.SHIIREKYOTUUZEIKOMI3 = selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI3"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI3"].ToString();

            //<D020134>
            dataInfo.DATA.D020134.KASHIDAOREZEINUKI10 = selectMainData.Rows[0]["KASHIDAOREZEINUKI10"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEINUKI10"].ToString();
            dataInfo.DATA.D020134.KASHIDAOREZEIGAKU10 = selectMainData.Rows[0]["KASHIDAOREZEIGAKU10"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEIGAKU10"].ToString();
            dataInfo.DATA.D020134.KASHIDAOREZEIKOMI10 = selectMainData.Rows[0]["KASHIDAOREZEIKOMI10"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEIKOMI10"].ToString();
            dataInfo.DATA.D020134.KASHIDAOREKAISHUZEINUKI10 = selectMainData.Rows[0]["KASHIDAOREKAISHUZEINUKI10"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEINUKI10"].ToString();
            dataInfo.DATA.D020134.KASHIDAOREKAISHUZEIGAKU10 = selectMainData.Rows[0]["KASHIDAOREKAISHUZEIGAKU10"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEIGAKU10"].ToString();
            dataInfo.DATA.D020134.KASHIDAOREKAISHUZEIKOMI10 = selectMainData.Rows[0]["KASHIDAOREKAISHUZEIKOMI10"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEIKOMI10"].ToString();

            //<D020133>
            dataInfo.DATA.D020133.KASHIDAOREZEINUKI8K = selectMainData.Rows[0]["KASHIDAOREZEINUKI8K"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEINUKI8K"].ToString();
            dataInfo.DATA.D020133.KASHIDAOREZEIGAKU8K = selectMainData.Rows[0]["KASHIDAOREZEIGAKU8K"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEIGAKU8K"].ToString();
            dataInfo.DATA.D020133.KASHIDAOREZEIKOMI8K = selectMainData.Rows[0]["KASHIDAOREZEIKOMI8K"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEIKOMI8K"].ToString();
            dataInfo.DATA.D020133.KASHIDAOREKAISHUZEINUKI8K = selectMainData.Rows[0]["KASHIDAOREKAISHUZEINUKI8K"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEINUKI8K"].ToString();
            dataInfo.DATA.D020133.KASHIDAOREKAISHUZEIGAKU8K = selectMainData.Rows[0]["KASHIDAOREKAISHUZEIGAKU8K"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEIGAKU8K"].ToString();
            dataInfo.DATA.D020133.KASHIDAOREKAISHUZEIKOMI8K = selectMainData.Rows[0]["KASHIDAOREKAISHUZEIKOMI8K"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEIKOMI8K"].ToString();

            //<D020130>
            dataInfo.DATA.D020130.KASHIDAOREZEINUKI8 = selectMainData.Rows[0]["KASHIDAOREZEINUKI8"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEINUKI8"].ToString();
            dataInfo.DATA.D020130.KASHIDAOREZEIGAKU8 = selectMainData.Rows[0]["KASHIDAOREZEIGAKU8"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEIGAKU8"].ToString();
            dataInfo.DATA.D020130.KASHIDAOREZEIKOMI8 = selectMainData.Rows[0]["KASHIDAOREZEIKOMI8"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEIKOMI8"].ToString();
            dataInfo.DATA.D020130.KASHIDAOREKAISHUZEINUKI8 = selectMainData.Rows[0]["KASHIDAOREKAISHUZEINUKI8"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEINUKI8"].ToString();
            dataInfo.DATA.D020130.KASHIDAOREKAISHUZEIGAKU8 = selectMainData.Rows[0]["KASHIDAOREKAISHUZEIGAKU8"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEIGAKU8"].ToString();
            dataInfo.DATA.D020130.KASHIDAOREKAISHUZEIKOMI8 = selectMainData.Rows[0]["KASHIDAOREKAISHUZEIKOMI8"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEIKOMI8"].ToString();

            //<D020131>
            dataInfo.DATA.D020131.KASHIDAOREZEINUKI5 = selectMainData.Rows[0]["KASHIDAOREZEINUKI5"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEINUKI5"].ToString();
            dataInfo.DATA.D020131.KASHIDAOREZEIGAKU5 = selectMainData.Rows[0]["KASHIDAOREZEIGAKU5"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEIGAKU5"].ToString();
            dataInfo.DATA.D020131.KASHIDAOREZEIKOMI5 = selectMainData.Rows[0]["KASHIDAOREZEIKOMI5"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEIKOMI5"].ToString();
            dataInfo.DATA.D020131.KASHIDAOREKAISHUZEINUKI5 = selectMainData.Rows[0]["KASHIDAOREKAISHUZEINUKI5"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEINUKI5"].ToString();
            dataInfo.DATA.D020131.KASHIDAOREKAISHUZEIGAKU5 = selectMainData.Rows[0]["KASHIDAOREKAISHUZEIGAKU5"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEIGAKU5"].ToString();
            dataInfo.DATA.D020131.KASHIDAOREKAISHUZEIKOMI5 = selectMainData.Rows[0]["KASHIDAOREKAISHUZEIKOMI5"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEIKOMI5"].ToString();

            //<D020132>
            dataInfo.DATA.D020132.KASHIDAOREZEINUKI3 = selectMainData.Rows[0]["KASHIDAOREZEINUKI3"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEINUKI3"].ToString();
            dataInfo.DATA.D020132.KASHIDAOREZEIGAKU3 = selectMainData.Rows[0]["KASHIDAOREZEIGAKU3"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEIGAKU3"].ToString();
            dataInfo.DATA.D020132.KASHIDAOREZEIKOMI3 = selectMainData.Rows[0]["KASHIDAOREZEIKOMI3"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEIKOMI3"].ToString();
            dataInfo.DATA.D020132.KASHIDAOREKAISHUZEINUKI3 = selectMainData.Rows[0]["KASHIDAOREKAISHUZEINUKI3"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEINUKI3"].ToString();
            dataInfo.DATA.D020132.KASHIDAOREKAISHUZEIGAKU3 = selectMainData.Rows[0]["KASHIDAOREKAISHUZEIGAKU3"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEIGAKU3"].ToString();
            dataInfo.DATA.D020132.KASHIDAOREKAISHUZEIKOMI3 = selectMainData.Rows[0]["KASHIDAOREKAISHUZEIKOMI3"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEIKOMI3"].ToString();

            //<D02204>
            dataInfo.DATA.D02204.GIMUMENJOKAZEI10 = selectMainData.Rows[0]["GIMUMENJOKAZEI10"].ToString() == "0" ? null : selectMainData.Rows[0]["GIMUMENJOKAZEI10"].ToString();
            dataInfo.DATA.D02204.GIMUMENJOHIKAZEI10 = selectMainData.Rows[0]["GIMUMENJOHIKAZEI10"].ToString() == "0" ? null : selectMainData.Rows[0]["GIMUMENJOHIKAZEI10"].ToString();
            dataInfo.DATA.D02204.GIMUMENJOKYOTUU10 = selectMainData.Rows[0]["GIMUMENJOKYOTUU10"].ToString() == "0" ? null : selectMainData.Rows[0]["GIMUMENJOKYOTUU10"].ToString();
            dataInfo.DATA.D02204.KAZEIURIAGEWARIAIHENDO10 = selectMainData.Rows[0]["KAZEIURIAGEWARIAIHENDO10"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEWARIAIHENDO10"].ToString();
            dataInfo.DATA.D02204.KAZEIGYOMUYOTENYO10 = selectMainData.Rows[0]["KAZEIGYOMUYOTENYO10"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIGYOMUYOTENYO10"].ToString();
            //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↓
            dataInfo.DATA.D02204.KYOJUYOCHINTAI10 = selectMainData.Rows[0]["KYOJUYOCHINTAI10"].ToString() == "0" ? null : selectMainData.Rows[0]["KYOJUYOCHINTAI10"].ToString();
            //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↑

            //<D02203>
            dataInfo.DATA.D02203.GIMUMENJOKAZEI8K = selectMainData.Rows[0]["GIMUMENJOKAZEI8K"].ToString() == "0" ? null : selectMainData.Rows[0]["GIMUMENJOKAZEI8K"].ToString();
            dataInfo.DATA.D02203.GIMUMENJOHIKAZEI8K = selectMainData.Rows[0]["GIMUMENJOHIKAZEI8K"].ToString() == "0" ? null : selectMainData.Rows[0]["GIMUMENJOHIKAZEI8K"].ToString();
            dataInfo.DATA.D02203.KAZEIURIAGEWARIAIHENDO8K = selectMainData.Rows[0]["KAZEIURIAGEWARIAIHENDO8K"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEWARIAIHENDO8K"].ToString();
            dataInfo.DATA.D02203.GIMUMENJOKYOTUU8K = selectMainData.Rows[0]["GIMUMENJOKYOTUU8K"].ToString() == "0" ? null : selectMainData.Rows[0]["GIMUMENJOKYOTUU8K"].ToString();
            dataInfo.DATA.D02203.KAZEIGYOMUYOTENYO8K = selectMainData.Rows[0]["KAZEIGYOMUYOTENYO8K"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIGYOMUYOTENYO8K"].ToString();
            //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↓
            dataInfo.DATA.D02203.KYOJUYOCHINTAI8K = selectMainData.Rows[0]["KYOJUYOCHINTAI8K"].ToString() == "0" ? null : selectMainData.Rows[0]["KYOJUYOCHINTAI8K"].ToString();
            //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↑

            //<D02200>
            dataInfo.DATA.D02200.GIMUMENJOKAZEI8 = selectMainData.Rows[0]["GIMUMENJOKAZEI8"].ToString() == "0" ? null : selectMainData.Rows[0]["GIMUMENJOKAZEI8"].ToString();
            dataInfo.DATA.D02200.GIMUMENJOHIKAZEI8 = selectMainData.Rows[0]["GIMUMENJOHIKAZEI8"].ToString() == "0" ? null : selectMainData.Rows[0]["GIMUMENJOHIKAZEI8"].ToString();
            dataInfo.DATA.D02200.GIMUMENJOKYOTUU8 = selectMainData.Rows[0]["GIMUMENJOKYOTUU8"].ToString() == "0" ? null : selectMainData.Rows[0]["GIMUMENJOKYOTUU8"].ToString();
            dataInfo.DATA.D02200.KAZEIURIAGEWARIAIHENDO8 = selectMainData.Rows[0]["KAZEIURIAGEWARIAIHENDO8"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEWARIAIHENDO8"].ToString();
            dataInfo.DATA.D02200.KAZEIGYOMUYOTENYO8 = selectMainData.Rows[0]["KAZEIGYOMUYOTENYO8"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIGYOMUYOTENYO8"].ToString();
            //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↓
            dataInfo.DATA.D02200.KYOJUYOCHINTAI8 = selectMainData.Rows[0]["KYOJUYOCHINTAI8"].ToString() == "0" ? null : selectMainData.Rows[0]["KYOJUYOCHINTAI8"].ToString();
            //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↑

            //<D02201>
            dataInfo.DATA.D02201.GIMUMENJOKAZEI5 = selectMainData.Rows[0]["GIMUMENJOKAZEI5"].ToString() == "0" ? null : selectMainData.Rows[0]["GIMUMENJOKAZEI5"].ToString();
            dataInfo.DATA.D02201.GIMUMENJOHIKAZEI5 = selectMainData.Rows[0]["GIMUMENJOHIKAZEI5"].ToString() == "0" ? null : selectMainData.Rows[0]["GIMUMENJOHIKAZEI5"].ToString();
            dataInfo.DATA.D02201.GIMUMENJOKYOTUU5 = selectMainData.Rows[0]["GIMUMENJOKYOTUU5"].ToString() == "0" ? null : selectMainData.Rows[0]["GIMUMENJOKYOTUU5"].ToString();
            dataInfo.DATA.D02201.KAZEIURIAGEWARIAIHENDO5 = selectMainData.Rows[0]["KAZEIURIAGEWARIAIHENDO5"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEWARIAIHENDO5"].ToString();
            dataInfo.DATA.D02201.KAZEIGYOMUYOTENYO5 = selectMainData.Rows[0]["KAZEIGYOMUYOTENYO5"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIGYOMUYOTENYO5"].ToString();
            //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↓
            dataInfo.DATA.D02201.KYOJUYOCHINTAI5 = selectMainData.Rows[0]["KYOJUYOCHINTAI5"].ToString() == "0" ? null : selectMainData.Rows[0]["KYOJUYOCHINTAI5"].ToString();
            //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↑

            //<D02202>
            dataInfo.DATA.D02202.GIMUMENJOKAZEI3 = selectMainData.Rows[0]["GIMUMENJOKAZEI3"].ToString() == "0" ? null : selectMainData.Rows[0]["GIMUMENJOKAZEI3"].ToString();
            dataInfo.DATA.D02202.GIMUMENJOHIKAZEI3 = selectMainData.Rows[0]["GIMUMENJOHIKAZEI3"].ToString() == "0" ? null : selectMainData.Rows[0]["GIMUMENJOHIKAZEI3"].ToString();
            dataInfo.DATA.D02202.GIMUMENJOKYOTUU3 = selectMainData.Rows[0]["GIMUMENJOKYOTUU3"].ToString() == "0" ? null : selectMainData.Rows[0]["GIMUMENJOKYOTUU3"].ToString();
            dataInfo.DATA.D02202.KAZEIURIAGEWARIAIHENDO3 = selectMainData.Rows[0]["KAZEIURIAGEWARIAIHENDO3"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEWARIAIHENDO3"].ToString();
            dataInfo.DATA.D02202.KAZEIGYOMUYOTENYO3 = selectMainData.Rows[0]["KAZEIGYOMUYOTENYO3"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIGYOMUYOTENYO3"].ToString();
            //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↓
            dataInfo.DATA.D02202.KYOJUYOCHINTAI3 = selectMainData.Rows[0]["KYOJUYOCHINTAI3"].ToString() == "0" ? null : selectMainData.Rows[0]["KYOJUYOCHINTAI3"].ToString();
            //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↑

            return dataInfo;
        }

        //  2023/07/24  修正        Y.Nakajima       (221366)【消費税申告管理】R5年インボイス対応(消費税申告の達人連携対応分) 開始
        /// <summary>
        /// 雛形クラスに値をセット
        /// インボイス用
        /// </summary>
        /// <returns></returns>
        private MA_ShouhizeiSinkokusho_DensiSinkoku_Invoice.DataInfo DataSetXmlInvoice(DataTable selectMainData)
        {
            MA_ShouhizeiSinkokusho_DensiSinkoku_Invoice.DataInfo dataInfo = new MA_ShouhizeiSinkokusho_DensiSinkoku_Invoice.DataInfo();

            //<SYSTEM>
            dataInfo.SystemTag.GYOUMUPREFIX = selectMainData.Rows[0]["GYOUMUPREFIX"].ToString();
            dataInfo.SystemTag.VERSION = selectMainData.Rows[0]["VERSION"].ToString();
            dataInfo.SystemTag.FUNCTIONID = selectMainData.Rows[0]["FUNCTIONID"].ToString();
            dataInfo.SystemTag.CONNECTEDPROGRAMVER = selectMainData.Rows[0]["CONNECTEDPROGRAMVER"].ToString();
            dataInfo.SystemTag.PROGRAMNAME = selectMainData.Rows[0]["PROGRAMNAME"].ToString();
            dataInfo.SystemTag.PROGRAMVER = selectMainData.Rows[0]["PROGRAMVER"].ToString();

            //<BASIC>
            dataInfo.BASIC.KOMONSAKINAME = selectMainData.Rows[0]["KOMONSAKINAME"].ToString();
            dataInfo.BASIC.JIGYOUNENDOFROM = selectMainData.Rows[0]["JIGYOUNENDOFROM"].ToString();
            dataInfo.BASIC.JIGYOUNENDOTO = selectMainData.Rows[0]["JIGYOUNENDOTO"].ToString();
            dataInfo.BASIC.HOUJINKOJINKUBUN = selectMainData.Rows[0]["HOUJINKOJINKUBUN"].ToString();
            dataInfo.BASIC.KAZEIKUBUN = selectMainData.Rows[0]["KAZEIKUBUN"].ToString();
            dataInfo.BASIC.KEIKASHOCHIKAZEISHISAN = selectMainData.Rows[0]["KEIKASHOCHIKAZEISHISAN"].ToString();
            dataInfo.BASIC.NIWARITOKUREI = selectMainData.Rows[0]["NIWARITOKUREI"].ToString();

            //<DATA>
            //<D020114>
            dataInfo.DATA.D020114.KAZEIURIAGEZEINUKI10_TSUMIAGE = selectMainData.Rows[0]["KAZEIURIAGEZEINUKI10_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEINUKI10_TSUMIAGE"].ToString();
            dataInfo.DATA.D020114.KAZEIURIAGEZEIGAKU10_TSUMIAGE = selectMainData.Rows[0]["KAZEIURIAGEZEIGAKU10_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEIGAKU10_TSUMIAGE"].ToString();
            dataInfo.DATA.D020114.KAZEIURIAGEZEIKOMI10_TSUMIAGE = selectMainData.Rows[0]["KAZEIURIAGEZEIKOMI10_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEIKOMI10_TSUMIAGE"].ToString();
            dataInfo.DATA.D020114.KAZEIURIAGEZEINUKI10_WARIMODOSHI = selectMainData.Rows[0]["KAZEIURIAGEZEINUKI10_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEINUKI10_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020114.KAZEIURIAGEZEIGAKU10_WARIMODOSHI = selectMainData.Rows[0]["KAZEIURIAGEZEIGAKU10_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEIGAKU10_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020114.KAZEIURIAGEZEIKOMI10_WARIMODOSHI = selectMainData.Rows[0]["KAZEIURIAGEZEIKOMI10_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEIKOMI10_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020114.MENZEIURIAGEZEINUKI10 = selectMainData.Rows[0]["MENZEIURIAGEZEINUKI10"].ToString() == "0" ? null : selectMainData.Rows[0]["MENZEIURIAGEZEINUKI10"].ToString();
            dataInfo.DATA.D020114.HIKAZEIURIAGEYUUKAZEINUKI10 = selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAZEINUKI10"].ToString() == "0" ? null : selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAZEINUKI10"].ToString();
            dataInfo.DATA.D020114.HIKAZEIURIAGEYUUKAIGAIZEINUKI10 = selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAIGAIZEINUKI10"].ToString() == "0" ? null : selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAIGAIZEINUKI10"].ToString();
            dataInfo.DATA.D020114.HIKAZEISHISANYUSHUTSUZEINUKI10 = selectMainData.Rows[0]["HIKAZEISHISANYUSHUTSUZEINUKI10"].ToString() == "0" ? null : selectMainData.Rows[0]["HIKAZEISHISANYUSHUTSUZEINUKI10"].ToString();
            dataInfo.DATA.D020114.KAZEIURIAGEHENKANZEINUKI10_TSUMIAGE = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEINUKI10_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEINUKI10_TSUMIAGE"].ToString();
            dataInfo.DATA.D020114.KAZEIURIAGEHENKANZEIGAKU10_TSUMIAGE = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIGAKU10_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIGAKU10_TSUMIAGE"].ToString();
            dataInfo.DATA.D020114.KAZEIURIAGEHENKANZEIKOMI10_TSUMIAGE = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIKOMI10_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIKOMI10_TSUMIAGE"].ToString();
            dataInfo.DATA.D020114.KAZEIURIAGEHENKANZEINUKI10_WARIMODOSHI = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEINUKI10_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEINUKI10_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020114.KAZEIURIAGEHENKANZEIGAKU10_WARIMODOSHI = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIGAKU10_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIGAKU10_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020114.KAZEIURIAGEHENKANZEIKOMI10_WARIMODOSHI = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIKOMI10_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIKOMI10_WARIMODOSHI"].ToString();

            //<D020113>
            dataInfo.DATA.D020113.KAZEIURIAGEZEINUKI8K_TSUMIAGE = selectMainData.Rows[0]["KAZEIURIAGEZEINUKI8K_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEINUKI8K_TSUMIAGE"].ToString();
            dataInfo.DATA.D020113.KAZEIURIAGEZEIGAKU8K_TSUMIAGE = selectMainData.Rows[0]["KAZEIURIAGEZEIGAKU8K_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEIGAKU8K_TSUMIAGE"].ToString();
            dataInfo.DATA.D020113.KAZEIURIAGEZEIKOMI8K_TSUMIAGE = selectMainData.Rows[0]["KAZEIURIAGEZEIKOMI8K_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEIKOMI8K_TSUMIAGE"].ToString();
            dataInfo.DATA.D020113.KAZEIURIAGEZEINUKI8K_WARIMODOSHI = selectMainData.Rows[0]["KAZEIURIAGEZEINUKI8K_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEINUKI8K_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020113.KAZEIURIAGEZEIGAKU8K_WARIMODOSHI = selectMainData.Rows[0]["KAZEIURIAGEZEIGAKU8K_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEIGAKU8K_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020113.KAZEIURIAGEZEIKOMI8K_WARIMODOSHI = selectMainData.Rows[0]["KAZEIURIAGEZEIKOMI8K_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEIKOMI8K_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020113.MENZEIURIAGEZEINUKI8K = selectMainData.Rows[0]["MENZEIURIAGEZEINUKI8K"].ToString() == "0" ? null : selectMainData.Rows[0]["MENZEIURIAGEZEINUKI8K"].ToString();
            dataInfo.DATA.D020113.HIKAZEIURIAGEYUUKAZEINUKI8K = selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAZEINUKI8K"].ToString() == "0" ? null : selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAZEINUKI8K"].ToString();
            dataInfo.DATA.D020113.HIKAZEIURIAGEYUUKAIGAIZEINUKI8K = selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAIGAIZEINUKI8K"].ToString() == "0" ? null : selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAIGAIZEINUKI8K"].ToString();
            dataInfo.DATA.D020113.HIKAZEISHISANYUSHUTSUZEINUKI8K = selectMainData.Rows[0]["HIKAZEISHISANYUSHUTSUZEINUKI8K"].ToString() == "0" ? null : selectMainData.Rows[0]["HIKAZEISHISANYUSHUTSUZEINUKI8K"].ToString();
            dataInfo.DATA.D020113.KAZEIURIAGEHENKANZEINUKI8K_TSUMIAGE = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEINUKI8K_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEINUKI8K_TSUMIAGE"].ToString();
            dataInfo.DATA.D020113.KAZEIURIAGEHENKANZEIGAKU8K_TSUMIAGE = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIGAKU8K_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIGAKU8K_TSUMIAGE"].ToString();
            dataInfo.DATA.D020113.KAZEIURIAGEHENKANZEIKOMI8K_TSUMIAGE = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIKOMI8K_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIKOMI8K_TSUMIAGE"].ToString();
            dataInfo.DATA.D020113.KAZEIURIAGEHENKANZEINUKI8K_WARIMODOSHI = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEINUKI8K_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEINUKI8K_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020113.KAZEIURIAGEHENKANZEIGAKU8K_WARIMODOSHI = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIGAKU8K_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIGAKU8K_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020113.KAZEIURIAGEHENKANZEIKOMI8K_WARIMODOSHI = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIKOMI8K_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIKOMI8K_WARIMODOSHI"].ToString();

            //<D020110>
            dataInfo.DATA.D020110.KAZEIURIAGEZEINUKI8_TSUMIAGE = selectMainData.Rows[0]["KAZEIURIAGEZEINUKI8_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEINUKI8_TSUMIAGE"].ToString();
            dataInfo.DATA.D020110.KAZEIURIAGEZEIGAKU8_TSUMIAGE = selectMainData.Rows[0]["KAZEIURIAGEZEIGAKU8_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEIGAKU8_TSUMIAGE"].ToString();
            dataInfo.DATA.D020110.KAZEIURIAGEZEIKOMI8_TSUMIAGE = selectMainData.Rows[0]["KAZEIURIAGEZEIKOMI8_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEIKOMI8_TSUMIAGE"].ToString();
            dataInfo.DATA.D020110.KAZEIURIAGEZEINUKI8_WARIMODOSHI = selectMainData.Rows[0]["KAZEIURIAGEZEINUKI8_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEINUKI8_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020110.KAZEIURIAGEZEIGAKU8_WARIMODOSHI = selectMainData.Rows[0]["KAZEIURIAGEZEIGAKU8_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEIGAKU8_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020110.KAZEIURIAGEZEIKOMI8_WARIMODOSHI = selectMainData.Rows[0]["KAZEIURIAGEZEIKOMI8_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEIKOMI8_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020110.MENZEIURIAGEZEINUKI8 = selectMainData.Rows[0]["MENZEIURIAGEZEINUKI8"].ToString() == "0" ? null : selectMainData.Rows[0]["MENZEIURIAGEZEINUKI8"].ToString();
            dataInfo.DATA.D020110.HIKAZEIURIAGEYUUKAZEINUKI8 = selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAZEINUKI8"].ToString() == "0" ? null : selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAZEINUKI8"].ToString();
            dataInfo.DATA.D020110.HIKAZEIURIAGEYUUKAIGAIZEINUKI8 = selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAIGAIZEINUKI8"].ToString() == "0" ? null : selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAIGAIZEINUKI8"].ToString();
            dataInfo.DATA.D020110.HIKAZEISHISANYUSHUTSUZEINUKI8 = selectMainData.Rows[0]["HIKAZEISHISANYUSHUTSUZEINUKI8"].ToString() == "0" ? null : selectMainData.Rows[0]["HIKAZEISHISANYUSHUTSUZEINUKI8"].ToString();
            dataInfo.DATA.D020110.KAZEIURIAGEHENKANZEINUKI8_TSUMIAGE = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEINUKI8_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEINUKI8_TSUMIAGE"].ToString();
            dataInfo.DATA.D020110.KAZEIURIAGEHENKANZEIGAKU8_TSUMIAGE = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIGAKU8_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIGAKU8_TSUMIAGE"].ToString();
            dataInfo.DATA.D020110.KAZEIURIAGEHENKANZEIKOMI8_TSUMIAGE = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIKOMI8_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIKOMI8_TSUMIAGE"].ToString();
            dataInfo.DATA.D020110.KAZEIURIAGEHENKANZEINUKI8_WARIMODOSHI = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEINUKI8_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEINUKI8_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020110.KAZEIURIAGEHENKANZEIGAKU8_WARIMODOSHI = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIGAKU8_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIGAKU8_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020110.KAZEIURIAGEHENKANZEIKOMI8_WARIMODOSHI = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIKOMI8_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIKOMI8_WARIMODOSHI"].ToString();

            //<D020111>
            dataInfo.DATA.D020111.KAZEIURIAGEZEINUKI5_TSUMIAGE = selectMainData.Rows[0]["KAZEIURIAGEZEINUKI5_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEINUKI5_TSUMIAGE"].ToString();
            dataInfo.DATA.D020111.KAZEIURIAGEZEIGAKU5_TSUMIAGE = selectMainData.Rows[0]["KAZEIURIAGEZEIGAKU5_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEIGAKU5_TSUMIAGE"].ToString();
            dataInfo.DATA.D020111.KAZEIURIAGEZEIKOMI5_TSUMIAGE = selectMainData.Rows[0]["KAZEIURIAGEZEIKOMI5_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEIKOMI5_TSUMIAGE"].ToString();
            dataInfo.DATA.D020111.KAZEIURIAGEZEINUKI5_WARIMODOSHI = selectMainData.Rows[0]["KAZEIURIAGEZEINUKI5_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEINUKI5_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020111.KAZEIURIAGEZEIGAKU5_WARIMODOSHI = selectMainData.Rows[0]["KAZEIURIAGEZEIGAKU5_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEIGAKU5_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020111.KAZEIURIAGEZEIKOMI5_WARIMODOSHI = selectMainData.Rows[0]["KAZEIURIAGEZEIKOMI5_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEIKOMI5_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020111.MENZEIURIAGEZEINUKI5 = selectMainData.Rows[0]["MENZEIURIAGEZEINUKI5"].ToString() == "0" ? null : selectMainData.Rows[0]["MENZEIURIAGEZEINUKI5"].ToString();
            dataInfo.DATA.D020111.HIKAZEIURIAGEYUUKAZEINUKI5 = selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAZEINUKI5"].ToString() == "0" ? null : selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAZEINUKI5"].ToString();
            dataInfo.DATA.D020111.HIKAZEIURIAGEYUUKAIGAIZEINUKI5 = selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAIGAIZEINUKI5"].ToString() == "0" ? null : selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAIGAIZEINUKI5"].ToString();
            dataInfo.DATA.D020111.HIKAZEISHISANYUSHUTSUZEINUKI5 = selectMainData.Rows[0]["HIKAZEISHISANYUSHUTSUZEINUKI5"].ToString() == "0" ? null : selectMainData.Rows[0]["HIKAZEISHISANYUSHUTSUZEINUKI5"].ToString();
            dataInfo.DATA.D020111.KAZEIURIAGEHENKANZEINUKI5_TSUMIAGE = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEINUKI5_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEINUKI5_TSUMIAGE"].ToString();
            dataInfo.DATA.D020111.KAZEIURIAGEHENKANZEIGAKU5_TSUMIAGE = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIGAKU5_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIGAKU5_TSUMIAGE"].ToString();
            dataInfo.DATA.D020111.KAZEIURIAGEHENKANZEIKOMI5_TSUMIAGE = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIKOMI5_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIKOMI5_TSUMIAGE"].ToString();
            dataInfo.DATA.D020111.KAZEIURIAGEHENKANZEINUKI5_WARIMODOSHI = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEINUKI5_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEINUKI5_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020111.KAZEIURIAGEHENKANZEIGAKU5_WARIMODOSHI = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIGAKU5_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIGAKU5_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020111.KAZEIURIAGEHENKANZEIKOMI5_WARIMODOSHI = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIKOMI5_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIKOMI5_WARIMODOSHI"].ToString();

            //<D020112>
            dataInfo.DATA.D020112.KAZEIURIAGEZEINUKI3_TSUMIAGE = selectMainData.Rows[0]["KAZEIURIAGEZEINUKI3_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEINUKI3_TSUMIAGE"].ToString();
            dataInfo.DATA.D020112.KAZEIURIAGEZEIGAKU3_TSUMIAGE = selectMainData.Rows[0]["KAZEIURIAGEZEIGAKU3_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEIGAKU3_TSUMIAGE"].ToString();
            dataInfo.DATA.D020112.KAZEIURIAGEZEIKOMI3_TSUMIAGE = selectMainData.Rows[0]["KAZEIURIAGEZEIKOMI3_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEIKOMI3_TSUMIAGE"].ToString();
            dataInfo.DATA.D020112.KAZEIURIAGEZEINUKI3_WARIMODOSHI = selectMainData.Rows[0]["KAZEIURIAGEZEINUKI3_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEINUKI3_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020112.KAZEIURIAGEZEIGAKU3_WARIMODOSHI = selectMainData.Rows[0]["KAZEIURIAGEZEIGAKU3_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEIGAKU3_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020112.KAZEIURIAGEZEIKOMI3_WARIMODOSHI = selectMainData.Rows[0]["KAZEIURIAGEZEIKOMI3_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEZEIKOMI3_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020112.MENZEIURIAGEZEINUKI3 = selectMainData.Rows[0]["MENZEIURIAGEZEINUKI3"].ToString() == "0" ? null : selectMainData.Rows[0]["MENZEIURIAGEZEINUKI3"].ToString();
            dataInfo.DATA.D020112.HIKAZEIURIAGEYUUKAZEINUKI3 = selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAZEINUKI3"].ToString() == "0" ? null : selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAZEINUKI3"].ToString();
            dataInfo.DATA.D020112.HIKAZEIURIAGEYUUKAIGAIZEINUKI3 = selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAIGAIZEINUKI3"].ToString() == "0" ? null : selectMainData.Rows[0]["HIKAZEIURIAGEYUUKAIGAIZEINUKI3"].ToString();
            dataInfo.DATA.D020112.HIKAZEISHISANYUSHUTSUZEINUKI3 = selectMainData.Rows[0]["HIKAZEISHISANYUSHUTSUZEINUKI3"].ToString() == "0" ? null : selectMainData.Rows[0]["HIKAZEISHISANYUSHUTSUZEINUKI3"].ToString();
            dataInfo.DATA.D020112.KAZEIURIAGEHENKANZEINUKI3_TSUMIAGE = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEINUKI3_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEINUKI3_TSUMIAGE"].ToString();
            dataInfo.DATA.D020112.KAZEIURIAGEHENKANZEIGAKU3_TSUMIAGE = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIGAKU3_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIGAKU3_TSUMIAGE"].ToString();
            dataInfo.DATA.D020112.KAZEIURIAGEHENKANZEIKOMI3_TSUMIAGE = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIKOMI3_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIKOMI3_TSUMIAGE"].ToString();
            dataInfo.DATA.D020112.KAZEIURIAGEHENKANZEINUKI3_WARIMODOSHI = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEINUKI3_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEINUKI3_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020112.KAZEIURIAGEHENKANZEIGAKU3_WARIMODOSHI = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIGAKU3_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIGAKU3_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020112.KAZEIURIAGEHENKANZEIKOMI3_WARIMODOSHI = selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIKOMI3_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEHENKANZEIKOMI3_WARIMODOSHI"].ToString();

            //<D020124>
            dataInfo.DATA.D020124.SHIIREKAZEIZEINUKI10_TSUMIAGE = selectMainData.Rows[0]["SHIIREKAZEIZEINUKI10_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEINUKI10_TSUMIAGE"].ToString();
            dataInfo.DATA.D020124.SHIIREKAZEIZEIGAKU10_TSUMIAGE = selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU10_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU10_TSUMIAGE"].ToString();
            dataInfo.DATA.D020124.SHIIREKAZEIZEIKOMI10_TSUMIAGE = selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI10_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI10_TSUMIAGE"].ToString();
            dataInfo.DATA.D020124.SHIIREHIKAZEIZEINUKI10_TSUMIAGE = selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI10_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI10_TSUMIAGE"].ToString();
            dataInfo.DATA.D020124.SHIIREHIKAZEIZEIGAKU10_TSUMIAGE = selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU10_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU10_TSUMIAGE"].ToString();
            dataInfo.DATA.D020124.SHIIREHIKAZEIZEIKOMI10_TSUMIAGE = selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI10_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI10_TSUMIAGE"].ToString();
            dataInfo.DATA.D020124.SHIIREKYOTUUZEINUKI10_TSUMIAGE = selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI10_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI10_TSUMIAGE"].ToString();
            dataInfo.DATA.D020124.SHIIREKYOTUUZEIGAKU10_TSUMIAGE = selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU10_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU10_TSUMIAGE"].ToString();
            dataInfo.DATA.D020124.SHIIREKYOTUUZEIKOMI10_TSUMIAGE = selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI10_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI10_TSUMIAGE"].ToString();
            dataInfo.DATA.D020124.SHIIREKAZEIZEINUKI10_WARIMODOSHI = selectMainData.Rows[0]["SHIIREKAZEIZEINUKI10_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEINUKI10_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020124.SHIIREKAZEIZEIGAKU10_WARIMODOSHI = selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU10_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU10_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020124.SHIIREKAZEIZEIKOMI10_WARIMODOSHI = selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI10_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI10_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020124.SHIIREHIKAZEIZEINUKI10_WARIMODOSHI = selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI10_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI10_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020124.SHIIREHIKAZEIZEIGAKU10_WARIMODOSHI = selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU10_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU10_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020124.SHIIREHIKAZEIZEIKOMI10_WARIMODOSHI = selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI10_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI10_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020124.SHIIREKYOTUUZEINUKI10_WARIMODOSHI = selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI10_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI10_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020124.SHIIREKYOTUUZEIGAKU10_WARIMODOSHI = selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU10_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU10_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020124.SHIIREKYOTUUZEIKOMI10_WARIMODOSHI = selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI10_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI10_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020124.SHIIREKAZEIZEIGAKU10_INVOICEIGAI_TSUMIAGE_KANBEN = selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU10_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU10_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString();
            dataInfo.DATA.D020124.SHIIREKAZEIZEIKOMI10_INVOICEIGAI_TSUMIAGE_KANBEN = selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI10_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI10_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString();
            dataInfo.DATA.D020124.SHIIREHIKAZEIZEIGAKU10_INVOICEIGAI_TSUMIAGE_KANBEN = selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU10_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU10_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString();
            dataInfo.DATA.D020124.SHIIREHIKAZEIZEIKOMI10_INVOICEIGAI_TSUMIAGE_KANBEN = selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI10_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI10_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString();
            dataInfo.DATA.D020124.SHIIREKYOTUUZEIGAKU10_INVOICEIGAI_TSUMIAGE_KANBEN = selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU10_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU10_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString();
            dataInfo.DATA.D020124.SHIIREKYOTUUZEIKOMI10_INVOICEIGAI_TSUMIAGE_KANBEN = selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI10_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI10_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString();
            dataInfo.DATA.D020124.SHIIREKAZEIZEINUKI10_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREKAZEIZEINUKI10_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEINUKI10_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020124.SHIIREKAZEIZEIGAKU10_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU10_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU10_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020124.SHIIREKAZEIZEIKOMI10_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI10_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI10_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020124.SHIIREHIKAZEIZEINUKI10_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI10_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI10_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020124.SHIIREHIKAZEIZEIGAKU10_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU10_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU10_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020124.SHIIREHIKAZEIZEIKOMI10_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI10_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI10_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020124.SHIIREKYOTUUZEINUKI10_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI10_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI10_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020124.SHIIREKYOTUUZEIGAKU10_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU10_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU10_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020124.SHIIREKYOTUUZEIKOMI10_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI10_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI10_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020124.SHIIREKAZEIZEINUKI10_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREKAZEIZEINUKI10_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEINUKI10_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020124.SHIIREKAZEIZEIGAKU10_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU10_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU10_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020124.SHIIREKAZEIZEIKOMI10_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI10_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI10_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020124.SHIIREHIKAZEIZEINUKI10_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI10_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI10_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020124.SHIIREHIKAZEIZEIGAKU10_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU10_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU10_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020124.SHIIREHIKAZEIZEIKOMI10_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI10_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI10_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020124.SHIIREKYOTUUZEINUKI10_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI10_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI10_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020124.SHIIREKYOTUUZEIGAKU10_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU10_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU10_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020124.SHIIREKYOTUUZEIKOMI10_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI10_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI10_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020124.TOKUTEISHIIREKAZEIZEINUKI10 = selectMainData.Rows[0]["TOKUTEISHIIREKAZEIZEINUKI10"].ToString() == "0" ? null : selectMainData.Rows[0]["TOKUTEISHIIREKAZEIZEINUKI10"].ToString();
            dataInfo.DATA.D020124.TOKUTEISHIIREHIKAZEIZEINUKI10 = selectMainData.Rows[0]["TOKUTEISHIIREHIKAZEIZEINUKI10"].ToString() == "0" ? null : selectMainData.Rows[0]["TOKUTEISHIIREHIKAZEIZEINUKI10"].ToString();
            dataInfo.DATA.D020124.TOKUTEISHIIREKYOTUUZEINUKI10 = selectMainData.Rows[0]["TOKUTEISHIIREKYOTUUZEINUKI10"].ToString() == "0" ? null : selectMainData.Rows[0]["TOKUTEISHIIREKYOTUUZEINUKI10"].ToString();
            dataInfo.DATA.D020124.YUNYUSHIIREKAZEIZEIGAKU10 = selectMainData.Rows[0]["YUNYUSHIIREKAZEIZEIGAKU10"].ToString() == "0" ? null : selectMainData.Rows[0]["YUNYUSHIIREKAZEIZEIGAKU10"].ToString();
            dataInfo.DATA.D020124.YUNYUSHIIREHIKAZEIZEIGAKU10 = selectMainData.Rows[0]["YUNYUSHIIREHIKAZEIZEIGAKU10"].ToString() == "0" ? null : selectMainData.Rows[0]["YUNYUSHIIREHIKAZEIZEIGAKU10"].ToString();
            dataInfo.DATA.D020124.YUNYUSHIIREKYOTUUZEIGAKU10 = selectMainData.Rows[0]["YUNYUSHIIREKYOTUUZEIGAKU10"].ToString() == "0" ? null : selectMainData.Rows[0]["YUNYUSHIIREKYOTUUZEIGAKU10"].ToString();
            dataInfo.DATA.D020124.YUNYUSHIIRETIHO10 = selectMainData.Rows[0]["YUNYUSHIIRETIHO10"].ToString() == "0" ? null : selectMainData.Rows[0]["YUNYUSHIIRETIHO10"].ToString();
            dataInfo.DATA.D020124.TOKUTEISHIIREHENKANKAZEIZEINUKI10 = selectMainData.Rows[0]["TOKUTEISHIIREHENKANKAZEIZEINUKI10"].ToString() == "0" ? null : selectMainData.Rows[0]["TOKUTEISHIIREHENKANKAZEIZEINUKI10"].ToString();
            dataInfo.DATA.D020124.TOKUTEISHIIREHENKANHIKAZEIZEINUKI10 = selectMainData.Rows[0]["TOKUTEISHIIREHENKANHIKAZEIZEINUKI10"].ToString() == "0" ? null : selectMainData.Rows[0]["TOKUTEISHIIREHENKANHIKAZEIZEINUKI10"].ToString();
            dataInfo.DATA.D020124.TOKUTEISHIIREHENKANKYOTUUZEINUKI10 = selectMainData.Rows[0]["TOKUTEISHIIREHENKANKYOTUUZEINUKI10"].ToString() == "0" ? null : selectMainData.Rows[0]["TOKUTEISHIIREHENKANKYOTUUZEINUKI10"].ToString();

            //<D020123>
            dataInfo.DATA.D020123.SHIIREKAZEIZEINUKI8K_TSUMIAGE = selectMainData.Rows[0]["SHIIREKAZEIZEINUKI8K_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEINUKI8K_TSUMIAGE"].ToString();
            dataInfo.DATA.D020123.SHIIREKAZEIZEIGAKU8K_TSUMIAGE = selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU8K_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU8K_TSUMIAGE"].ToString();
            dataInfo.DATA.D020123.SHIIREKAZEIZEIKOMI8K_TSUMIAGE = selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI8K_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI8K_TSUMIAGE"].ToString();
            dataInfo.DATA.D020123.SHIIREHIKAZEIZEINUKI8K_TSUMIAGE = selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI8K_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI8K_TSUMIAGE"].ToString();
            dataInfo.DATA.D020123.SHIIREHIKAZEIZEIGAKU8K_TSUMIAGE = selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU8K_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU8K_TSUMIAGE"].ToString();
            dataInfo.DATA.D020123.SHIIREHIKAZEIZEIKOMI8K_TSUMIAGE = selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI8K_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI8K_TSUMIAGE"].ToString();
            dataInfo.DATA.D020123.SHIIREKYOTUUZEINUKI8K_TSUMIAGE = selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI8K_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI8K_TSUMIAGE"].ToString();
            dataInfo.DATA.D020123.SHIIREKYOTUUZEIGAKU8K_TSUMIAGE = selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU8K_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU8K_TSUMIAGE"].ToString();
            dataInfo.DATA.D020123.SHIIREKYOTUUZEIKOMI8K_TSUMIAGE = selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI8K_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI8K_TSUMIAGE"].ToString();
            dataInfo.DATA.D020123.SHIIREKAZEIZEINUKI8K_WARIMODOSHI = selectMainData.Rows[0]["SHIIREKAZEIZEINUKI8K_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEINUKI8K_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020123.SHIIREKAZEIZEIGAKU8K_WARIMODOSHI = selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU8K_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU8K_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020123.SHIIREKAZEIZEIKOMI8K_WARIMODOSHI = selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI8K_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI8K_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020123.SHIIREHIKAZEIZEINUKI8K_WARIMODOSHI = selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI8K_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI8K_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020123.SHIIREHIKAZEIZEIGAKU8K_WARIMODOSHI = selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU8K_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU8K_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020123.SHIIREHIKAZEIZEIKOMI8K_WARIMODOSHI = selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI8K_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI8K_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020123.SHIIREKYOTUUZEINUKI8K_WARIMODOSHI = selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI8K_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI8K_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020123.SHIIREKYOTUUZEIGAKU8K_WARIMODOSHI = selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU8K_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU8K_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020123.SHIIREKYOTUUZEIKOMI8K_WARIMODOSHI = selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI8K_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI8K_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020123.SHIIREKAZEIZEIGAKU8K_INVOICEIGAI_TSUMIAGE_KANBEN = selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU8K_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU8K_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString();
            dataInfo.DATA.D020123.SHIIREKAZEIZEIKOMI8K_INVOICEIGAI_TSUMIAGE_KANBEN = selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI8K_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI8K_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString();
            dataInfo.DATA.D020123.SHIIREHIKAZEIZEIGAKU8K_INVOICEIGAI_TSUMIAGE_KANBEN = selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU8K_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU8K_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString();
            dataInfo.DATA.D020123.SHIIREHIKAZEIZEIKOMI8K_INVOICEIGAI_TSUMIAGE_KANBEN = selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI8K_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI8K_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString();
            dataInfo.DATA.D020123.SHIIREKYOTUUZEIGAKU8K_INVOICEIGAI_TSUMIAGE_KANBEN = selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU8K_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU8K_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString();
            dataInfo.DATA.D020123.SHIIREKYOTUUZEIKOMI8K_INVOICEIGAI_TSUMIAGE_KANBEN = selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI8K_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI8K_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString();
            dataInfo.DATA.D020123.SHIIREKAZEIZEINUKI8K_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREKAZEIZEINUKI8K_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEINUKI8K_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020123.SHIIREKAZEIZEIGAKU8K_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU8K_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU8K_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020123.SHIIREKAZEIZEIKOMI8K_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI8K_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI8K_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020123.SHIIREHIKAZEIZEINUKI8K_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI8K_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI8K_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020123.SHIIREHIKAZEIZEIGAKU8K_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU8K_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU8K_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020123.SHIIREHIKAZEIZEIKOMI8K_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI8K_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI8K_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020123.SHIIREKYOTUUZEINUKI8K_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI8K_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI8K_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020123.SHIIREKYOTUUZEIGAKU8K_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU8K_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU8K_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020123.SHIIREKYOTUUZEIKOMI8K_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI8K_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI8K_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020123.SHIIREKAZEIZEINUKI8K_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREKAZEIZEINUKI8K_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEINUKI8K_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020123.SHIIREKAZEIZEIGAKU8K_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU8K_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU8K_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020123.SHIIREKAZEIZEIKOMI8K_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI8K_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI8K_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020123.SHIIREHIKAZEIZEINUKI8K_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI8K_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI8K_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020123.SHIIREHIKAZEIZEIGAKU8K_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU8K_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU8K_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020123.SHIIREHIKAZEIZEIKOMI8K_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI8K_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI8K_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020123.SHIIREKYOTUUZEINUKI8K_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI8K_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI8K_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020123.SHIIREKYOTUUZEIGAKU8K_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU8K_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU8K_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020123.SHIIREKYOTUUZEIKOMI8K_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI8K_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI8K_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020123.YUNYUSHIIREKAZEIZEIGAKU8K = selectMainData.Rows[0]["YUNYUSHIIREKAZEIZEIGAKU8K"].ToString() == "0" ? null : selectMainData.Rows[0]["YUNYUSHIIREKAZEIZEIGAKU8K"].ToString();
            dataInfo.DATA.D020123.YUNYUSHIIREHIKAZEIZEIGAKU8K = selectMainData.Rows[0]["YUNYUSHIIREHIKAZEIZEIGAKU8K"].ToString() == "0" ? null : selectMainData.Rows[0]["YUNYUSHIIREHIKAZEIZEIGAKU8K"].ToString();
            dataInfo.DATA.D020123.YUNYUSHIIREKYOTUUZEIGAKU8K = selectMainData.Rows[0]["YUNYUSHIIREKYOTUUZEIGAKU8K"].ToString() == "0" ? null : selectMainData.Rows[0]["YUNYUSHIIREKYOTUUZEIGAKU8K"].ToString();
            dataInfo.DATA.D020123.YUNYUSHIIRETIHO8K = selectMainData.Rows[0]["YUNYUSHIIRETIHO8K"].ToString() == "0" ? null : selectMainData.Rows[0]["YUNYUSHIIRETIHO8K"].ToString();

            //<D020120>
            dataInfo.DATA.D020120.SHIIREKAZEIZEINUKI8_TSUMIAGE = selectMainData.Rows[0]["SHIIREKAZEIZEINUKI8_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEINUKI8_TSUMIAGE"].ToString();
            dataInfo.DATA.D020120.SHIIREKAZEIZEIGAKU8_TSUMIAGE = selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU8_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU8_TSUMIAGE"].ToString();
            dataInfo.DATA.D020120.SHIIREKAZEIZEIKOMI8_TSUMIAGE = selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI8_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI8_TSUMIAGE"].ToString();
            dataInfo.DATA.D020120.SHIIREHIKAZEIZEINUKI8_TSUMIAGE = selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI8_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI8_TSUMIAGE"].ToString();
            dataInfo.DATA.D020120.SHIIREHIKAZEIZEIGAKU8_TSUMIAGE = selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU8_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU8_TSUMIAGE"].ToString();
            dataInfo.DATA.D020120.SHIIREHIKAZEIZEIKOMI8_TSUMIAGE = selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI8_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI8_TSUMIAGE"].ToString();
            dataInfo.DATA.D020120.SHIIREKYOTUUZEINUKI8_TSUMIAGE = selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI8_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI8_TSUMIAGE"].ToString();
            dataInfo.DATA.D020120.SHIIREKYOTUUZEIGAKU8_TSUMIAGE = selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU8_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU8_TSUMIAGE"].ToString();
            dataInfo.DATA.D020120.SHIIREKYOTUUZEIKOMI8_TSUMIAGE = selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI8_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI8_TSUMIAGE"].ToString();
            dataInfo.DATA.D020120.SHIIREKAZEIZEINUKI8_WARIMODOSHI = selectMainData.Rows[0]["SHIIREKAZEIZEINUKI8_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEINUKI8_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020120.SHIIREKAZEIZEIGAKU8_WARIMODOSHI = selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU8_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU8_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020120.SHIIREKAZEIZEIKOMI8_WARIMODOSHI = selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI8_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI8_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020120.SHIIREHIKAZEIZEINUKI8_WARIMODOSHI = selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI8_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI8_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020120.SHIIREHIKAZEIZEIGAKU8_WARIMODOSHI = selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU8_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU8_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020120.SHIIREHIKAZEIZEIKOMI8_WARIMODOSHI = selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI8_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI8_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020120.SHIIREKYOTUUZEINUKI8_WARIMODOSHI = selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI8_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI8_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020120.SHIIREKYOTUUZEIGAKU8_WARIMODOSHI = selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU8_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU8_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020120.SHIIREKYOTUUZEIKOMI8_WARIMODOSHI = selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI8_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI8_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020120.SHIIREKAZEIZEIGAKU8_INVOICEIGAI_TSUMIAGE_KANBEN = selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU8_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU8_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString();
            dataInfo.DATA.D020120.SHIIREKAZEIZEIKOMI8_INVOICEIGAI_TSUMIAGE_KANBEN = selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI8_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI8_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString();
            dataInfo.DATA.D020120.SHIIREHIKAZEIZEIGAKU8_INVOICEIGAI_TSUMIAGE_KANBEN = selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU8_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU8_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString();
            dataInfo.DATA.D020120.SHIIREHIKAZEIZEIKOMI8_INVOICEIGAI_TSUMIAGE_KANBEN = selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI8_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI8_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString();
            dataInfo.DATA.D020120.SHIIREKYOTUUZEIGAKU8_INVOICEIGAI_TSUMIAGE_KANBEN = selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU8_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU8_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString();
            dataInfo.DATA.D020120.SHIIREKYOTUUZEIKOMI8_INVOICEIGAI_TSUMIAGE_KANBEN = selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI8_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI8_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString();
            dataInfo.DATA.D020120.SHIIREKAZEIZEINUKI8_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREKAZEIZEINUKI8_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEINUKI8_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020120.SHIIREKAZEIZEIGAKU8_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU8_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU8_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020120.SHIIREKAZEIZEIKOMI8_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI8_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI8_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020120.SHIIREHIKAZEIZEINUKI8_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI8_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI8_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020120.SHIIREHIKAZEIZEIGAKU8_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU8_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU8_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020120.SHIIREHIKAZEIZEIKOMI8_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI8_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI8_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020120.SHIIREKYOTUUZEINUKI8_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI8_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI8_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020120.SHIIREKYOTUUZEIGAKU8_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU8_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU8_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020120.SHIIREKYOTUUZEIKOMI8_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI8_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI8_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020120.SHIIREKAZEIZEINUKI8_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREKAZEIZEINUKI8_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEINUKI8_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020120.SHIIREKAZEIZEIGAKU8_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU8_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU8_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020120.SHIIREKAZEIZEIKOMI8_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI8_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI8_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020120.SHIIREHIKAZEIZEINUKI8_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI8_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI8_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020120.SHIIREHIKAZEIZEIGAKU8_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU8_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU8_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020120.SHIIREHIKAZEIZEIKOMI8_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI8_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI8_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020120.SHIIREKYOTUUZEINUKI8_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI8_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI8_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020120.SHIIREKYOTUUZEIGAKU8_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU8_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU8_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020120.SHIIREKYOTUUZEIKOMI8_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI8_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI8_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020120.TOKUTEISHIIREKAZEIZEINUKI8 = selectMainData.Rows[0]["TOKUTEISHIIREKAZEIZEINUKI8"].ToString() == "0" ? null : selectMainData.Rows[0]["TOKUTEISHIIREKAZEIZEINUKI8"].ToString();
            dataInfo.DATA.D020120.TOKUTEISHIIREHIKAZEIZEINUKI8 = selectMainData.Rows[0]["TOKUTEISHIIREHIKAZEIZEINUKI8"].ToString() == "0" ? null : selectMainData.Rows[0]["TOKUTEISHIIREHIKAZEIZEINUKI8"].ToString();
            dataInfo.DATA.D020120.TOKUTEISHIIREKYOTUUZEINUKI8 = selectMainData.Rows[0]["TOKUTEISHIIREKYOTUUZEINUKI8"].ToString() == "0" ? null : selectMainData.Rows[0]["TOKUTEISHIIREKYOTUUZEINUKI8"].ToString();
            dataInfo.DATA.D020120.YUNYUSHIIREKAZEIZEIGAKU8 = selectMainData.Rows[0]["YUNYUSHIIREKAZEIZEIGAKU8"].ToString() == "0" ? null : selectMainData.Rows[0]["YUNYUSHIIREKAZEIZEIGAKU8"].ToString();
            dataInfo.DATA.D020120.YUNYUSHIIREHIKAZEIZEIGAKU8 = selectMainData.Rows[0]["YUNYUSHIIREHIKAZEIZEIGAKU8"].ToString() == "0" ? null : selectMainData.Rows[0]["YUNYUSHIIREHIKAZEIZEIGAKU8"].ToString();
            dataInfo.DATA.D020120.YUNYUSHIIREKYOTUUZEIGAKU8 = selectMainData.Rows[0]["YUNYUSHIIREKYOTUUZEIGAKU8"].ToString() == "0" ? null : selectMainData.Rows[0]["YUNYUSHIIREKYOTUUZEIGAKU8"].ToString();
            dataInfo.DATA.D020120.YUNYUSHIIRETIHO8 = selectMainData.Rows[0]["YUNYUSHIIRETIHO8"].ToString() == "0" ? null : selectMainData.Rows[0]["YUNYUSHIIRETIHO8"].ToString();
            dataInfo.DATA.D020120.TOKUTEISHIIREHENKANKAZEIZEINUKI8 = selectMainData.Rows[0]["TOKUTEISHIIREHENKANKAZEIZEINUKI8"].ToString() == "0" ? null : selectMainData.Rows[0]["TOKUTEISHIIREHENKANKAZEIZEINUKI8"].ToString();
            dataInfo.DATA.D020120.TOKUTEISHIIREHENKANHIKAZEIZEINUKI8 = selectMainData.Rows[0]["TOKUTEISHIIREHENKANHIKAZEIZEINUKI8"].ToString() == "0" ? null : selectMainData.Rows[0]["TOKUTEISHIIREHENKANHIKAZEIZEINUKI8"].ToString();
            dataInfo.DATA.D020120.TOKUTEISHIIREHENKANKYOTUUZEINUKI8 = selectMainData.Rows[0]["TOKUTEISHIIREHENKANKYOTUUZEINUKI8"].ToString() == "0" ? null : selectMainData.Rows[0]["TOKUTEISHIIREHENKANKYOTUUZEINUKI8"].ToString();

            //<D020121>
            dataInfo.DATA.D020121.SHIIREKAZEIZEINUKI5_TSUMIAGE = selectMainData.Rows[0]["SHIIREKAZEIZEINUKI5_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEINUKI5_TSUMIAGE"].ToString();
            dataInfo.DATA.D020121.SHIIREKAZEIZEIGAKU5_TSUMIAGE = selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU5_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU5_TSUMIAGE"].ToString();
            dataInfo.DATA.D020121.SHIIREKAZEIZEIKOMI5_TSUMIAGE = selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI5_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI5_TSUMIAGE"].ToString();
            dataInfo.DATA.D020121.SHIIREHIKAZEIZEINUKI5_TSUMIAGE = selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI5_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI5_TSUMIAGE"].ToString();
            dataInfo.DATA.D020121.SHIIREHIKAZEIZEIGAKU5_TSUMIAGE = selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU5_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU5_TSUMIAGE"].ToString();
            dataInfo.DATA.D020121.SHIIREHIKAZEIZEIKOMI5_TSUMIAGE = selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI5_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI5_TSUMIAGE"].ToString();
            dataInfo.DATA.D020121.SHIIREKYOTUUZEINUKI5_TSUMIAGE = selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI5_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI5_TSUMIAGE"].ToString();
            dataInfo.DATA.D020121.SHIIREKYOTUUZEIGAKU5_TSUMIAGE = selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU5_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU5_TSUMIAGE"].ToString();
            dataInfo.DATA.D020121.SHIIREKYOTUUZEIKOMI5_TSUMIAGE = selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI5_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI5_TSUMIAGE"].ToString();
            dataInfo.DATA.D020121.SHIIREKAZEIZEINUKI5_WARIMODOSHI = selectMainData.Rows[0]["SHIIREKAZEIZEINUKI5_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEINUKI5_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020121.SHIIREKAZEIZEIGAKU5_WARIMODOSHI = selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU5_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU5_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020121.SHIIREKAZEIZEIKOMI5_WARIMODOSHI = selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI5_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI5_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020121.SHIIREHIKAZEIZEINUKI5_WARIMODOSHI = selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI5_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI5_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020121.SHIIREHIKAZEIZEIGAKU5_WARIMODOSHI = selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU5_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU5_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020121.SHIIREHIKAZEIZEIKOMI5_WARIMODOSHI = selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI5_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI5_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020121.SHIIREKYOTUUZEINUKI5_WARIMODOSHI = selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI5_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI5_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020121.SHIIREKYOTUUZEIGAKU5_WARIMODOSHI = selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU5_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU5_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020121.SHIIREKYOTUUZEIKOMI5_WARIMODOSHI = selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI5_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI5_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020121.SHIIREKAZEIZEIGAKU5_INVOICEIGAI_TSUMIAGE_KANBEN = selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU5_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU5_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString();
            dataInfo.DATA.D020121.SHIIREKAZEIZEIKOMI5_INVOICEIGAI_TSUMIAGE_KANBEN = selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI5_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI5_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString();
            dataInfo.DATA.D020121.SHIIREHIKAZEIZEIGAKU5_INVOICEIGAI_TSUMIAGE_KANBEN = selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU5_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU5_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString();
            dataInfo.DATA.D020121.SHIIREHIKAZEIZEIKOMI5_INVOICEIGAI_TSUMIAGE_KANBEN = selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI5_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI5_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString();
            dataInfo.DATA.D020121.SHIIREKYOTUUZEIGAKU5_INVOICEIGAI_TSUMIAGE_KANBEN = selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU5_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU5_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString();
            dataInfo.DATA.D020121.SHIIREKYOTUUZEIKOMI5_INVOICEIGAI_TSUMIAGE_KANBEN = selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI5_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI5_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString();
            dataInfo.DATA.D020121.SHIIREKAZEIZEINUKI5_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREKAZEIZEINUKI5_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEINUKI5_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020121.SHIIREKAZEIZEIGAKU5_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU5_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU5_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020121.SHIIREKAZEIZEIKOMI5_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI5_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI5_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020121.SHIIREHIKAZEIZEINUKI5_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI5_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI5_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020121.SHIIREHIKAZEIZEIGAKU5_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU5_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU5_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020121.SHIIREHIKAZEIZEIKOMI5_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI5_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI5_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020121.SHIIREKYOTUUZEINUKI5_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI5_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI5_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020121.SHIIREKYOTUUZEIGAKU5_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU5_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU5_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020121.SHIIREKYOTUUZEIKOMI5_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI5_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI5_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020121.SHIIREKAZEIZEINUKI5_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREKAZEIZEINUKI5_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEINUKI5_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020121.SHIIREKAZEIZEIGAKU5_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU5_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU5_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020121.SHIIREKAZEIZEIKOMI5_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI5_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI5_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020121.SHIIREHIKAZEIZEINUKI5_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI5_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI5_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020121.SHIIREHIKAZEIZEIGAKU5_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU5_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU5_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020121.SHIIREHIKAZEIZEIKOMI5_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI5_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI5_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020121.SHIIREKYOTUUZEINUKI5_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI5_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI5_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020121.SHIIREKYOTUUZEIGAKU5_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU5_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU5_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020121.SHIIREKYOTUUZEIKOMI5_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI5_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI5_INVOICEIGAI_WARIMODOSHI50"].ToString();

            //<D020122>
            dataInfo.DATA.D020122.SHIIREKAZEIZEINUKI3_TSUMIAGE = selectMainData.Rows[0]["SHIIREKAZEIZEINUKI3_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEINUKI3_TSUMIAGE"].ToString();
            dataInfo.DATA.D020122.SHIIREKAZEIZEIGAKU3_TSUMIAGE = selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU3_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU3_TSUMIAGE"].ToString();
            dataInfo.DATA.D020122.SHIIREKAZEIZEIKOMI3_TSUMIAGE = selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI3_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI3_TSUMIAGE"].ToString();
            dataInfo.DATA.D020122.SHIIREHIKAZEIZEINUKI3_TSUMIAGE = selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI3_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI3_TSUMIAGE"].ToString();
            dataInfo.DATA.D020122.SHIIREHIKAZEIZEIGAKU3_TSUMIAGE = selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU3_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU3_TSUMIAGE"].ToString();
            dataInfo.DATA.D020122.SHIIREHIKAZEIZEIKOMI3_TSUMIAGE = selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI3_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI3_TSUMIAGE"].ToString();
            dataInfo.DATA.D020122.SHIIREKYOTUUZEINUKI3_TSUMIAGE = selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI3_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI3_TSUMIAGE"].ToString();
            dataInfo.DATA.D020122.SHIIREKYOTUUZEIGAKU3_TSUMIAGE = selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU3_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU3_TSUMIAGE"].ToString();
            dataInfo.DATA.D020122.SHIIREKYOTUUZEIKOMI3_TSUMIAGE = selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI3_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI3_TSUMIAGE"].ToString();
            dataInfo.DATA.D020122.SHIIREKAZEIZEINUKI3_WARIMODOSHI = selectMainData.Rows[0]["SHIIREKAZEIZEINUKI3_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEINUKI3_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020122.SHIIREKAZEIZEIGAKU3_WARIMODOSHI = selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU3_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU3_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020122.SHIIREKAZEIZEIKOMI3_WARIMODOSHI = selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI3_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI3_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020122.SHIIREHIKAZEIZEINUKI3_WARIMODOSHI = selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI3_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI3_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020122.SHIIREHIKAZEIZEIGAKU3_WARIMODOSHI = selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU3_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU3_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020122.SHIIREHIKAZEIZEIKOMI3_WARIMODOSHI = selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI3_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI3_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020122.SHIIREKYOTUUZEINUKI3_WARIMODOSHI = selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI3_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI3_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020122.SHIIREKYOTUUZEIGAKU3_WARIMODOSHI = selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU3_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU3_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020122.SHIIREKYOTUUZEIKOMI3_WARIMODOSHI = selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI3_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI3_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020122.SHIIREKAZEIZEIGAKU3_INVOICEIGAI_TSUMIAGE_KANBEN = selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU3_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU3_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString();
            dataInfo.DATA.D020122.SHIIREKAZEIZEIKOMI3_INVOICEIGAI_TSUMIAGE_KANBEN = selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI3_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI3_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString();
            dataInfo.DATA.D020122.SHIIREHIKAZEIZEIGAKU3_INVOICEIGAI_TSUMIAGE_KANBEN = selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU3_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU3_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString();
            dataInfo.DATA.D020122.SHIIREHIKAZEIZEIKOMI3_INVOICEIGAI_TSUMIAGE_KANBEN = selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI3_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI3_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString();
            dataInfo.DATA.D020122.SHIIREKYOTUUZEIGAKU3_INVOICEIGAI_TSUMIAGE_KANBEN = selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU3_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU3_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString();
            dataInfo.DATA.D020122.SHIIREKYOTUUZEIKOMI3_INVOICEIGAI_TSUMIAGE_KANBEN = selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI3_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI3_INVOICEIGAI_TSUMIAGE_KANBEN"].ToString();
            dataInfo.DATA.D020122.SHIIREKAZEIZEINUKI3_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREKAZEIZEINUKI3_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEINUKI3_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020122.SHIIREKAZEIZEIGAKU3_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU3_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU3_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020122.SHIIREKAZEIZEIKOMI3_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI3_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI3_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020122.SHIIREHIKAZEIZEINUKI3_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI3_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI3_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020122.SHIIREHIKAZEIZEIGAKU3_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU3_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU3_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020122.SHIIREHIKAZEIZEIKOMI3_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI3_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI3_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020122.SHIIREKYOTUUZEINUKI3_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI3_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI3_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020122.SHIIREKYOTUUZEIGAKU3_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU3_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU3_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020122.SHIIREKYOTUUZEIKOMI3_INVOICEIGAI_WARIMODOSHI80 = selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI3_INVOICEIGAI_WARIMODOSHI80"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI3_INVOICEIGAI_WARIMODOSHI80"].ToString();
            dataInfo.DATA.D020122.SHIIREKAZEIZEINUKI3_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREKAZEIZEINUKI3_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEINUKI3_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020122.SHIIREKAZEIZEIGAKU3_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU3_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIGAKU3_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020122.SHIIREKAZEIZEIKOMI3_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI3_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKAZEIZEIKOMI3_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020122.SHIIREHIKAZEIZEINUKI3_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI3_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEINUKI3_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020122.SHIIREHIKAZEIZEIGAKU3_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU3_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIGAKU3_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020122.SHIIREHIKAZEIZEIKOMI3_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI3_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREHIKAZEIZEIKOMI3_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020122.SHIIREKYOTUUZEINUKI3_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI3_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEINUKI3_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020122.SHIIREKYOTUUZEIGAKU3_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU3_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIGAKU3_INVOICEIGAI_WARIMODOSHI50"].ToString();
            dataInfo.DATA.D020122.SHIIREKYOTUUZEIKOMI3_INVOICEIGAI_WARIMODOSHI50 = selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI3_INVOICEIGAI_WARIMODOSHI50"].ToString() == "0" ? null : selectMainData.Rows[0]["SHIIREKYOTUUZEIKOMI3_INVOICEIGAI_WARIMODOSHI50"].ToString();

            //<D020134>
            dataInfo.DATA.D020134.KASHIDAOREZEINUKI10_TSUMIAGE = selectMainData.Rows[0]["KASHIDAOREZEINUKI10_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEINUKI10_TSUMIAGE"].ToString();
            dataInfo.DATA.D020134.KASHIDAOREZEIGAKU10_TSUMIAGE = selectMainData.Rows[0]["KASHIDAOREZEIGAKU10_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEIGAKU10_TSUMIAGE"].ToString();
            dataInfo.DATA.D020134.KASHIDAOREZEIKOMI10_TSUMIAGE = selectMainData.Rows[0]["KASHIDAOREZEIKOMI10_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEIKOMI10_TSUMIAGE"].ToString();
            dataInfo.DATA.D020134.KASHIDAOREKAISHUZEINUKI10_TSUMIAGE = selectMainData.Rows[0]["KASHIDAOREKAISHUZEINUKI10_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEINUKI10_TSUMIAGE"].ToString();
            dataInfo.DATA.D020134.KASHIDAOREKAISHUZEIGAKU10_TSUMIAGE = selectMainData.Rows[0]["KASHIDAOREKAISHUZEIGAKU10_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEIGAKU10_TSUMIAGE"].ToString();
            dataInfo.DATA.D020134.KASHIDAOREKAISHUZEIKOMI10_TSUMIAGE = selectMainData.Rows[0]["KASHIDAOREKAISHUZEIKOMI10_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEIKOMI10_TSUMIAGE"].ToString();
            dataInfo.DATA.D020134.KASHIDAOREZEINUKI10_WARIMODOSHI = selectMainData.Rows[0]["KASHIDAOREZEINUKI10_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEINUKI10_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020134.KASHIDAOREZEIGAKU10_WARIMODOSHI = selectMainData.Rows[0]["KASHIDAOREZEIGAKU10_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEIGAKU10_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020134.KASHIDAOREZEIKOMI10_WARIMODOSHI = selectMainData.Rows[0]["KASHIDAOREZEIKOMI10_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEIKOMI10_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020134.KASHIDAOREKAISHUZEINUKI10_WARIMODOSHI = selectMainData.Rows[0]["KASHIDAOREKAISHUZEINUKI10_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEINUKI10_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020134.KASHIDAOREKAISHUZEIGAKU10_WARIMODOSHI = selectMainData.Rows[0]["KASHIDAOREKAISHUZEIGAKU10_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEIGAKU10_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020134.KASHIDAOREKAISHUZEIKOMI10_WARIMODOSHI = selectMainData.Rows[0]["KASHIDAOREKAISHUZEIKOMI10_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEIKOMI10_WARIMODOSHI"].ToString();

            //<D020133>
            dataInfo.DATA.D020133.KASHIDAOREZEINUKI8K_TSUMIAGE = selectMainData.Rows[0]["KASHIDAOREZEINUKI8K_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEINUKI8K_TSUMIAGE"].ToString();
            dataInfo.DATA.D020133.KASHIDAOREZEIGAKU8K_TSUMIAGE = selectMainData.Rows[0]["KASHIDAOREZEIGAKU8K_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEIGAKU8K_TSUMIAGE"].ToString();
            dataInfo.DATA.D020133.KASHIDAOREZEIKOMI8K_TSUMIAGE = selectMainData.Rows[0]["KASHIDAOREZEIKOMI8K_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEIKOMI8K_TSUMIAGE"].ToString();
            dataInfo.DATA.D020133.KASHIDAOREKAISHUZEINUKI8K_TSUMIAGE = selectMainData.Rows[0]["KASHIDAOREKAISHUZEINUKI8K_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEINUKI8K_TSUMIAGE"].ToString();
            dataInfo.DATA.D020133.KASHIDAOREKAISHUZEIGAKU8K_TSUMIAGE = selectMainData.Rows[0]["KASHIDAOREKAISHUZEIGAKU8K_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEIGAKU8K_TSUMIAGE"].ToString();
            dataInfo.DATA.D020133.KASHIDAOREKAISHUZEIKOMI8K_TSUMIAGE = selectMainData.Rows[0]["KASHIDAOREKAISHUZEIKOMI8K_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEIKOMI8K_TSUMIAGE"].ToString();
            dataInfo.DATA.D020133.KASHIDAOREZEINUKI8K_WARIMODOSHI = selectMainData.Rows[0]["KASHIDAOREZEINUKI8K_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEINUKI8K_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020133.KASHIDAOREZEIGAKU8K_WARIMODOSHI = selectMainData.Rows[0]["KASHIDAOREZEIGAKU8K_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEIGAKU8K_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020133.KASHIDAOREZEIKOMI8K_WARIMODOSHI = selectMainData.Rows[0]["KASHIDAOREZEIKOMI8K_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEIKOMI8K_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020133.KASHIDAOREKAISHUZEINUKI8K_WARIMODOSHI = selectMainData.Rows[0]["KASHIDAOREKAISHUZEINUKI8K_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEINUKI8K_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020133.KASHIDAOREKAISHUZEIGAKU8K_WARIMODOSHI = selectMainData.Rows[0]["KASHIDAOREKAISHUZEIGAKU8K_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEIGAKU8K_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020133.KASHIDAOREKAISHUZEIKOMI8K_WARIMODOSHI = selectMainData.Rows[0]["KASHIDAOREKAISHUZEIKOMI8K_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEIKOMI8K_WARIMODOSHI"].ToString();

            //<D020130>
            dataInfo.DATA.D020130.KASHIDAOREZEINUKI8_TSUMIAGE = selectMainData.Rows[0]["KASHIDAOREZEINUKI8_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEINUKI8_TSUMIAGE"].ToString();
            dataInfo.DATA.D020130.KASHIDAOREZEIGAKU8_TSUMIAGE = selectMainData.Rows[0]["KASHIDAOREZEIGAKU8_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEIGAKU8_TSUMIAGE"].ToString();
            dataInfo.DATA.D020130.KASHIDAOREZEIKOMI8_TSUMIAGE = selectMainData.Rows[0]["KASHIDAOREZEIKOMI8_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEIKOMI8_TSUMIAGE"].ToString();
            dataInfo.DATA.D020130.KASHIDAOREKAISHUZEINUKI8_TSUMIAGE = selectMainData.Rows[0]["KASHIDAOREKAISHUZEINUKI8_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEINUKI8_TSUMIAGE"].ToString();
            dataInfo.DATA.D020130.KASHIDAOREKAISHUZEIGAKU8_TSUMIAGE = selectMainData.Rows[0]["KASHIDAOREKAISHUZEIGAKU8_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEIGAKU8_TSUMIAGE"].ToString();
            dataInfo.DATA.D020130.KASHIDAOREKAISHUZEIKOMI8_TSUMIAGE = selectMainData.Rows[0]["KASHIDAOREKAISHUZEIKOMI8_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEIKOMI8_TSUMIAGE"].ToString();
            dataInfo.DATA.D020130.KASHIDAOREZEINUKI8_WARIMODOSHI = selectMainData.Rows[0]["KASHIDAOREZEINUKI8_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEINUKI8_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020130.KASHIDAOREZEIGAKU8_WARIMODOSHI = selectMainData.Rows[0]["KASHIDAOREZEIGAKU8_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEIGAKU8_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020130.KASHIDAOREZEIKOMI8_WARIMODOSHI = selectMainData.Rows[0]["KASHIDAOREZEIKOMI8_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEIKOMI8_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020130.KASHIDAOREKAISHUZEINUKI8_WARIMODOSHI = selectMainData.Rows[0]["KASHIDAOREKAISHUZEINUKI8_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEINUKI8_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020130.KASHIDAOREKAISHUZEIGAKU8_WARIMODOSHI = selectMainData.Rows[0]["KASHIDAOREKAISHUZEIGAKU8_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEIGAKU8_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020130.KASHIDAOREKAISHUZEIKOMI8_WARIMODOSHI = selectMainData.Rows[0]["KASHIDAOREKAISHUZEIKOMI8_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEIKOMI8_WARIMODOSHI"].ToString();

            //<D020131>
            dataInfo.DATA.D020131.KASHIDAOREZEINUKI5_TSUMIAGE = selectMainData.Rows[0]["KASHIDAOREZEINUKI5_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEINUKI5_TSUMIAGE"].ToString();
            dataInfo.DATA.D020131.KASHIDAOREZEIGAKU5_TSUMIAGE = selectMainData.Rows[0]["KASHIDAOREZEIGAKU5_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEIGAKU5_TSUMIAGE"].ToString();
            dataInfo.DATA.D020131.KASHIDAOREZEIKOMI5_TSUMIAGE = selectMainData.Rows[0]["KASHIDAOREZEIKOMI5_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEIKOMI5_TSUMIAGE"].ToString();
            dataInfo.DATA.D020131.KASHIDAOREKAISHUZEINUKI5_TSUMIAGE = selectMainData.Rows[0]["KASHIDAOREKAISHUZEINUKI5_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEINUKI5_TSUMIAGE"].ToString();
            dataInfo.DATA.D020131.KASHIDAOREKAISHUZEIGAKU5_TSUMIAGE = selectMainData.Rows[0]["KASHIDAOREKAISHUZEIGAKU5_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEIGAKU5_TSUMIAGE"].ToString();
            dataInfo.DATA.D020131.KASHIDAOREKAISHUZEIKOMI5_TSUMIAGE = selectMainData.Rows[0]["KASHIDAOREKAISHUZEIKOMI5_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEIKOMI5_TSUMIAGE"].ToString();
            dataInfo.DATA.D020131.KASHIDAOREZEINUKI5_WARIMODOSHI = selectMainData.Rows[0]["KASHIDAOREZEINUKI5_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEINUKI5_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020131.KASHIDAOREZEIGAKU5_WARIMODOSHI = selectMainData.Rows[0]["KASHIDAOREZEIGAKU5_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEIGAKU5_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020131.KASHIDAOREZEIKOMI5_WARIMODOSHI = selectMainData.Rows[0]["KASHIDAOREZEIKOMI5_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEIKOMI5_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020131.KASHIDAOREKAISHUZEINUKI5_WARIMODOSHI = selectMainData.Rows[0]["KASHIDAOREKAISHUZEINUKI5_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEINUKI5_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020131.KASHIDAOREKAISHUZEIGAKU5_WARIMODOSHI = selectMainData.Rows[0]["KASHIDAOREKAISHUZEIGAKU5_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEIGAKU5_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020131.KASHIDAOREKAISHUZEIKOMI5_WARIMODOSHI = selectMainData.Rows[0]["KASHIDAOREKAISHUZEIKOMI5_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEIKOMI5_WARIMODOSHI"].ToString();


            //<D020132>
            dataInfo.DATA.D020132.KASHIDAOREZEINUKI3_TSUMIAGE = selectMainData.Rows[0]["KASHIDAOREZEINUKI3_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEINUKI3_TSUMIAGE"].ToString();
            dataInfo.DATA.D020132.KASHIDAOREZEIGAKU3_TSUMIAGE = selectMainData.Rows[0]["KASHIDAOREZEIGAKU3_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEIGAKU3_TSUMIAGE"].ToString();
            dataInfo.DATA.D020132.KASHIDAOREZEIKOMI3_TSUMIAGE = selectMainData.Rows[0]["KASHIDAOREZEIKOMI3_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEIKOMI3_TSUMIAGE"].ToString();
            dataInfo.DATA.D020132.KASHIDAOREKAISHUZEINUKI3_TSUMIAGE = selectMainData.Rows[0]["KASHIDAOREKAISHUZEINUKI3_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEINUKI3_TSUMIAGE"].ToString();
            dataInfo.DATA.D020132.KASHIDAOREKAISHUZEIGAKU3_TSUMIAGE = selectMainData.Rows[0]["KASHIDAOREKAISHUZEIGAKU3_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEIGAKU3_TSUMIAGE"].ToString();
            dataInfo.DATA.D020132.KASHIDAOREKAISHUZEIKOMI3_TSUMIAGE = selectMainData.Rows[0]["KASHIDAOREKAISHUZEIKOMI3_TSUMIAGE"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEIKOMI3_TSUMIAGE"].ToString();
            dataInfo.DATA.D020132.KASHIDAOREZEINUKI3_WARIMODOSHI = selectMainData.Rows[0]["KASHIDAOREZEINUKI3_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEINUKI3_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020132.KASHIDAOREZEIGAKU3_WARIMODOSHI = selectMainData.Rows[0]["KASHIDAOREZEIGAKU3_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEIGAKU3_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020132.KASHIDAOREZEIKOMI3_WARIMODOSHI = selectMainData.Rows[0]["KASHIDAOREZEIKOMI3_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREZEIKOMI3_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020132.KASHIDAOREKAISHUZEINUKI3_WARIMODOSHI = selectMainData.Rows[0]["KASHIDAOREKAISHUZEINUKI3_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEINUKI3_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020132.KASHIDAOREKAISHUZEIGAKU3_WARIMODOSHI = selectMainData.Rows[0]["KASHIDAOREKAISHUZEIGAKU3_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEIGAKU3_WARIMODOSHI"].ToString();
            dataInfo.DATA.D020132.KASHIDAOREKAISHUZEIKOMI3_WARIMODOSHI = selectMainData.Rows[0]["KASHIDAOREKAISHUZEIKOMI3_WARIMODOSHI"].ToString() == "0" ? null : selectMainData.Rows[0]["KASHIDAOREKAISHUZEIKOMI3_WARIMODOSHI"].ToString();

            //<D02204>
            dataInfo.DATA.D02204.GIMUMENJOKAZEI10 = selectMainData.Rows[0]["GIMUMENJOKAZEI10"].ToString() == "0" ? null : selectMainData.Rows[0]["GIMUMENJOKAZEI10"].ToString();
            dataInfo.DATA.D02204.GIMUMENJOHIKAZEI10 = selectMainData.Rows[0]["GIMUMENJOHIKAZEI10"].ToString() == "0" ? null : selectMainData.Rows[0]["GIMUMENJOHIKAZEI10"].ToString();
            dataInfo.DATA.D02204.GIMUMENJOKYOTUU10 = selectMainData.Rows[0]["GIMUMENJOKYOTUU10"].ToString() == "0" ? null : selectMainData.Rows[0]["GIMUMENJOKYOTUU10"].ToString();
            dataInfo.DATA.D02204.KAZEIURIAGEWARIAIHENDO10 = selectMainData.Rows[0]["KAZEIURIAGEWARIAIHENDO10"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEWARIAIHENDO10"].ToString();
            dataInfo.DATA.D02204.KAZEIGYOMUYOTENYO10 = selectMainData.Rows[0]["KAZEIGYOMUYOTENYO10"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIGYOMUYOTENYO10"].ToString();
            dataInfo.DATA.D02204.KYOJUYOCHINTAI10 = selectMainData.Rows[0]["KYOJUYOCHINTAI10"].ToString() == "0" ? null : selectMainData.Rows[0]["KYOJUYOCHINTAI10"].ToString();

            //<D02203>
            dataInfo.DATA.D02203.GIMUMENJOKAZEI8K = selectMainData.Rows[0]["GIMUMENJOKAZEI8K"].ToString() == "0" ? null : selectMainData.Rows[0]["GIMUMENJOKAZEI8K"].ToString();
            dataInfo.DATA.D02203.GIMUMENJOHIKAZEI8K = selectMainData.Rows[0]["GIMUMENJOHIKAZEI8K"].ToString() == "0" ? null : selectMainData.Rows[0]["GIMUMENJOHIKAZEI8K"].ToString();
            dataInfo.DATA.D02203.KAZEIURIAGEWARIAIHENDO8K = selectMainData.Rows[0]["KAZEIURIAGEWARIAIHENDO8K"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEWARIAIHENDO8K"].ToString();
            dataInfo.DATA.D02203.GIMUMENJOKYOTUU8K = selectMainData.Rows[0]["GIMUMENJOKYOTUU8K"].ToString() == "0" ? null : selectMainData.Rows[0]["GIMUMENJOKYOTUU8K"].ToString();
            dataInfo.DATA.D02203.KAZEIGYOMUYOTENYO8K = selectMainData.Rows[0]["KAZEIGYOMUYOTENYO8K"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIGYOMUYOTENYO8K"].ToString();
            dataInfo.DATA.D02203.KYOJUYOCHINTAI8K = selectMainData.Rows[0]["KYOJUYOCHINTAI8K"].ToString() == "0" ? null : selectMainData.Rows[0]["KYOJUYOCHINTAI8K"].ToString();

            //<D02200>
            dataInfo.DATA.D02200.GIMUMENJOKAZEI8 = selectMainData.Rows[0]["GIMUMENJOKAZEI8"].ToString() == "0" ? null : selectMainData.Rows[0]["GIMUMENJOKAZEI8"].ToString();
            dataInfo.DATA.D02200.GIMUMENJOHIKAZEI8 = selectMainData.Rows[0]["GIMUMENJOHIKAZEI8"].ToString() == "0" ? null : selectMainData.Rows[0]["GIMUMENJOHIKAZEI8"].ToString();
            dataInfo.DATA.D02200.GIMUMENJOKYOTUU8 = selectMainData.Rows[0]["GIMUMENJOKYOTUU8"].ToString() == "0" ? null : selectMainData.Rows[0]["GIMUMENJOKYOTUU8"].ToString();
            dataInfo.DATA.D02200.KAZEIURIAGEWARIAIHENDO8 = selectMainData.Rows[0]["KAZEIURIAGEWARIAIHENDO8"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEWARIAIHENDO8"].ToString();
            dataInfo.DATA.D02200.KAZEIGYOMUYOTENYO8 = selectMainData.Rows[0]["KAZEIGYOMUYOTENYO8"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIGYOMUYOTENYO8"].ToString();
            dataInfo.DATA.D02200.KYOJUYOCHINTAI8 = selectMainData.Rows[0]["KYOJUYOCHINTAI8"].ToString() == "0" ? null : selectMainData.Rows[0]["KYOJUYOCHINTAI8"].ToString();

            //<D02201>
            dataInfo.DATA.D02201.GIMUMENJOKAZEI5 = selectMainData.Rows[0]["GIMUMENJOKAZEI5"].ToString() == "0" ? null : selectMainData.Rows[0]["GIMUMENJOKAZEI5"].ToString();
            dataInfo.DATA.D02201.GIMUMENJOHIKAZEI5 = selectMainData.Rows[0]["GIMUMENJOHIKAZEI5"].ToString() == "0" ? null : selectMainData.Rows[0]["GIMUMENJOHIKAZEI5"].ToString();
            dataInfo.DATA.D02201.GIMUMENJOKYOTUU5 = selectMainData.Rows[0]["GIMUMENJOKYOTUU5"].ToString() == "0" ? null : selectMainData.Rows[0]["GIMUMENJOKYOTUU5"].ToString();
            dataInfo.DATA.D02201.KAZEIURIAGEWARIAIHENDO5 = selectMainData.Rows[0]["KAZEIURIAGEWARIAIHENDO5"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEWARIAIHENDO5"].ToString();
            dataInfo.DATA.D02201.KAZEIGYOMUYOTENYO5 = selectMainData.Rows[0]["KAZEIGYOMUYOTENYO5"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIGYOMUYOTENYO5"].ToString();
            dataInfo.DATA.D02201.KYOJUYOCHINTAI5 = selectMainData.Rows[0]["KYOJUYOCHINTAI5"].ToString() == "0" ? null : selectMainData.Rows[0]["KYOJUYOCHINTAI5"].ToString();

            //<D02202>
            dataInfo.DATA.D02202.GIMUMENJOKAZEI3 = selectMainData.Rows[0]["GIMUMENJOKAZEI3"].ToString() == "0" ? null : selectMainData.Rows[0]["GIMUMENJOKAZEI3"].ToString();
            dataInfo.DATA.D02202.GIMUMENJOHIKAZEI3 = selectMainData.Rows[0]["GIMUMENJOHIKAZEI3"].ToString() == "0" ? null : selectMainData.Rows[0]["GIMUMENJOHIKAZEI3"].ToString();
            dataInfo.DATA.D02202.GIMUMENJOKYOTUU3 = selectMainData.Rows[0]["GIMUMENJOKYOTUU3"].ToString() == "0" ? null : selectMainData.Rows[0]["GIMUMENJOKYOTUU3"].ToString();
            dataInfo.DATA.D02202.KAZEIURIAGEWARIAIHENDO3 = selectMainData.Rows[0]["KAZEIURIAGEWARIAIHENDO3"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIURIAGEWARIAIHENDO3"].ToString();
            dataInfo.DATA.D02202.KAZEIGYOMUYOTENYO3 = selectMainData.Rows[0]["KAZEIGYOMUYOTENYO3"].ToString() == "0" ? null : selectMainData.Rows[0]["KAZEIGYOMUYOTENYO3"].ToString();
            dataInfo.DATA.D02202.KYOJUYOCHINTAI3 = selectMainData.Rows[0]["KYOJUYOCHINTAI3"].ToString() == "0" ? null : selectMainData.Rows[0]["KYOJUYOCHINTAI3"].ToString();

            return dataInfo;
        }
        //  2023/07/24  修正        Y.Nakajima       (221366)【消費税申告管理】R5年インボイス対応(消費税申告の達人連携対応分) 終了
        #endregion

        #region Xml 不要なタグを削除
        /// <summary>
        /// XMLの不要な大分類を削除する
        /// </summary>
        /// <param name="retXmlDoc"></param>
        private void RemoveElementXml(XmlDocument retXmlDoc)
        {
            CheckElementXml("D020114", retXmlDoc);
            CheckElementXml("D020113", retXmlDoc);
            CheckElementXml("D020110", retXmlDoc);
            CheckElementXml("D020111", retXmlDoc);
            CheckElementXml("D020112", retXmlDoc);
            CheckElementXml("D020124", retXmlDoc);
            CheckElementXml("D020123", retXmlDoc);
            CheckElementXml("D020120", retXmlDoc);
            CheckElementXml("D020121", retXmlDoc);
            CheckElementXml("D020122", retXmlDoc);
            CheckElementXml("D020134", retXmlDoc);
            CheckElementXml("D020133", retXmlDoc);
            CheckElementXml("D020130", retXmlDoc);
            CheckElementXml("D020131", retXmlDoc);
            CheckElementXml("D020132", retXmlDoc);
            CheckElementXml("D02204", retXmlDoc);
            CheckElementXml("D02203", retXmlDoc);
            CheckElementXml("D02200", retXmlDoc);
            CheckElementXml("D02201", retXmlDoc);
            CheckElementXml("D02202", retXmlDoc);
        }
        #endregion

        #region Xmlのタグ要否チェック
        /// <summary>
        /// Xmlの大分類が小分類を持つかチェックする
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="retXmlDoc"></param>
        private void CheckElementXml(string tagName, XmlDocument retXmlDoc)
        {
            XmlNode NodeName = retXmlDoc.GetElementsByTagName(tagName)[0];
            XmlNodeList NodeList = retXmlDoc.GetElementsByTagName(tagName);

            if (NodeName == null || NodeName.HasChildNodes == false)
            {
                XmlNode node = NodeList[0];
                node.ParentNode.RemoveChild(NodeName);
            }
        }
        #endregion

        #region
        /// <summary>
        /// ファイルが既に存在する場合、上書きするか確認メッセージを表示
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        private bool CheckExistFile(string fileName,string filePath)
        {
            if (File.Exists(filePath))
            {
                //メッセージパラメータ
                string[] messageParam = new string[] { fileName };
                //確認メッセージ
                if (Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD, CUSTOMIZESEQ, MESSAGE_CD1, "30042", null, messageParam, this.GetType(),
                                                      Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.YesNo,
                                                      Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    //元の項目へフォーカス移動
                    this.PrevControl.Focus();
                    return false;
                }
                else
                {
                    //Yesの場合、ファイルを削除
                    File.Delete(filePath);
                    return true;
                }
            }
            return true;
        }
        #endregion

        // 2019/09/02  変更 M.Washizu (139334-139359)【電子申告対応】終了

        #region セキュリティ情報取得
        /// <summary>
        /// セキュリティ情報取得
        /// </summary>
        private void GetSecurity()
        {
            //事業所セキュリティ取得
            using (DataTable zigSec = WinMToolSearchCom.M0000MS_MA_Security.GetZigyoushoSecurity(Com.A0000.Tool.Logic.CurrentUser.UserID))
            {
                //セキュリティパターンコードセット
                zigSecurityPatternCD = Com.A0000.Tool.Function.AnyToString(zigSec.Rows[0]["SecurityPatternCD"]);
                //セキュリティ状態区分セット
                zigSecurityJoutaiKbn = Com.A0000.Tool.Function.AnyToInt32(zigSec.Rows[0]["SecurityJoutaiKBN"]);
            }

            //初期事業所コード取得
            using (DataTable dtDefZig = Win.M0000.Tool.SearchCom.M0000MS_MA_Security.GetDefaultZigyoushoCD(Com.A0000.Tool.Logic.CurrentUser.UserID))
            {
                //初期表示事業所コードセット
                defaultZigyoushoCD = Function.AnyToString(dtDefZig.Rows[0]["ZigyoushoCD"]);
            }
            // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 開始
            using (DataTable dtKakeiControl = businessDB.GetDataTable("SELECT KaishaUnyouKeitaiKBN FROM M0000MS_MA_KaikeiControl AS M0000MS_MA_KaikeiControl WHERE ControlKey = 1"))
            {
                //会社運用形態区分セット
                kaishaUnyouKeitaiKBN = Function.AnyToInt32(dtKakeiControl.Rows[0]["KaishaUnyouKeitaiKBN"]);
            }
            // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 終了
            //セキュリティ状態区分がALL以外の場合は事業所全社選択不可
            // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 開始
            //if (zigSecurityJoutaiKbn != 0)
            if (zigSecurityJoutaiKbn != 0 || kaishaUnyouKeitaiKBN == 1)
            // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 終了
            {
                obic7RadioButtonZensha.Enabled = false;
            }

            //2023/07/25    Y.Matsuzawa      削除      (221366)【消費税申告管理】R5年インボイス対応 ↓
            //// 2019/09/02  変更 M.Washizu (139334-139359)【電子申告対応】開始
            //using (DataTable dtZaimuControl = businessDB.GetDataTable("SELECT KeiriHousikiKBN FROM M0000MS_MA_Control AS M0000MS_MA_Control WHERE ControlKey = 1"))
            //{
            //    //経理方式区分セット
            //    keiriHousikiKBN = Function.AnyToInt32(dtZaimuControl.Rows[0]["KeiriHousikiKBN"]);
            //}
            //// 2019/09/02  変更 M.Washizu (139334-139359)【電子申告対応】開始
            //2023/07/25    Y.Matsuzawa      削除      (221366)【消費税申告管理】R5年インボイス対応 ↑

            //2023/07/25    Y.Matsuzawa      修正      (221366)【消費税申告管理】R5年インボイス対応 ↓
            using (DataTable dtZaimuControl = businessDB.GetDataTable(GetMAControl()))
            {
                keiriHousikiKBN = Function.AnyToInt32(dtZaimuControl.Rows[0]["KeiriHousikiKBN"]);
                _r5NenOctIgoUriageZeiTumiageKBN = Function.AnyToByte(dtZaimuControl.Rows[0]["R5NenOctIgoUriageZeiTumiageKBN"]);
                _r5NenOctIgoSiireZeiTumiageKBN = Function.AnyToByte(dtZaimuControl.Rows[0]["R5NenOctIgoSiireZeiTumiageKBN"]);
            }
            //2023/07/25    Y.Matsuzawa      修正      (221366)【消費税申告管理】R5年インボイス対応 ↑
        }

        #endregion

        //2012/01/25 修正 OBIC T.Shiraishi (M04737) 開始
        #region 事業年度制御区分取得
        private void GetZigyounendoSeigyoKBN()
        {
            using (DataTable dt = WinMToolSearchCom.M0000MS_MA_Control.SelectByKey(1, ""))
            {
                zigyounendoSeigyoKBN = Com.A0000.Tool.Function.AnyToInt32(dt.Rows[0]["ZigyounendoSeigyoKBN"]);
                //平成23年度以前の開始事業年度の場合
                if (zigyounendoSeigyoKBN == 0)
                {
                    this.obic7RadioButtonZengakuKoujo.Checked = true;
                    this.obic7PanelKeisanHouhou.Enabled = false;
                }
                else
                {
                    this.obic7PanelKeisanHouhou.Enabled = true;
                    this.obic7RadioButtonBubunKoujo.Checked = true;
                }
                // 2018/05/11 T.Kimura 変更 (99108-99004)【消費税率10%対応】↓
                if (zigyounendoSeigyoKBN == 0
                || zigyounendoSeigyoKBN == 1
                || zigyounendoSeigyoKBN == 2
                || zigyounendoSeigyoKBN == 3)
                {
                    // 2018/05/11 T.Kimura 変更 (99108-99004)【消費税率10%対応】↑
                    // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 開始
                    //課税標準額等の内訳書の選択項目制御
                    if ((zigyounendoSeigyoKBN == 0) || (zigyounendoSeigyoKBN == 1) || (zigyounendoSeigyoKBN == 2))
                    {
                        this.obic7RadioButtonUtiwakesho.Enabled = false;
                    }
                    else
                    {
                        this.obic7RadioButtonUtiwakesho.Enabled = true;
                    }
                    // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 終了
                    // 2018/05/11 T.Kimura 変更 (99108-99004)【消費税率10%対応】↓
                    //帳票選択パネルを切り替える
                    this.obic7Panel1.Visible = true;
                    this.obic7Panel1.Enabled = true;

                    this.obic7PanelH31.Visible = false;
                    this.obic7PanelH31.Enabled = false;

                    this.obic7PanelKeisanHouhou.Location = new Point(24, 312);
                }
                //平成31年度以降の開始事業年度の場合
                else
                {
                    //帳票選択パネルを切り替える
                    this.obic7Panel1.Visible = false;
                    this.obic7Panel1.Enabled = false;

                    this.obic7PanelH31.Visible = true;
                    this.obic7PanelH31.Enabled = true;
                    this.obic7PanelH31.Location = new Point(24, 144);

                    //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↓
                    //this.obic7PanelKeisanHouhou.Location = new Point(24, 363);
                    this.obic7PanelKeisanHouhou.Location = new Point(24, 399);
                    //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↑
                }
                // 2018/05/11 T.Kimura 変更 (99108-99004)【消費税率10%対応】↑
            }

        }
        #endregion
        //2012/01/25 修正 OBIC T.Shiraishi (M04737) 終了

        #region 検索処理
        /// <summary>
        /// 検索ダイアログ表示処理
        /// </summary>
        /// <param name="textAbstract">対象テキストボックス</param>
        private bool ShowSearchDailog(Obic7fx.Win.A0000.Tool.UI.Obic7TextString textAbstract)
        {
            //関連検索ダイアログを連結
            if (textAbstract == obic7TextStringZigyoushoCD)
            {
                // 対象項目が事業所コードであれば独自の検索ダイアログを表示する
                using (MA_ZigyoushoCD zigyoushoCDSearch = new MA_ZigyoushoCD())
                {
                    // パブリックプロパティに値をセット
                    zigyoushoCDSearch.BasicDate = (DateTime)obic7TextDatetimeKikanED.Value;
                    zigyoushoCDSearch.SecurityPatternCD = zigSecurityPatternCD;
                    //ALL/SUM区分は0をセット
                    zigyoushoCDSearch.AllSumKubun = 0;
                    //全事業所区分は0をセット
                    zigyoushoCDSearch.AllZigyoushoKubun = 0;
                    //名称NULL区分
                    zigyoushoCDSearch.NameNullKubun = 1;
                    //ダイアログを関連付ける
                    textAbstract.AttachedSearchDialog = zigyoushoCDSearch;

                    // 検索呼び出し
                    if (textAbstract.ShowDialog() == DialogResult.OK)
                    {
                        return true;
                    }
                }
            }
            // 2019/09/02  変更 M.Washizu (139334-139359)【電子申告対応】開始
            if (textAbstract == this.obic7TextStringOutPath)
            {
                //フォルダ選択ダイアログのインスタンス生成
                Obic7fx.Win.A0000.Tool.Logic.UI.Obic7FolderBrowserDialog folderDialog = new WinToolLogic.UI.Obic7FolderBrowserDialog();

                //プロパティに値をセット
                folderDialog.SelectedPath = this.obic7TextStringOutPath.Text;

                //ダイアログ表示
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    //出力パスをセットする
                    this.obic7TextStringOutPath.Text = ComTool.Function.AddDirSep(folderDialog.SelectedPath);
                    return true;
                }
            }
            // 2019/09/02  変更 M.Washizu (139334-139359)【電子申告対応】終了
            // 検索でOKが押されなかった場合
            return false;
        }
        #endregion

        #region 入力チェック
        /// <summary>
        /// 入力チェック
        /// </summary>
        /// <returns></returns>
        private bool InputCheck()
        {
            // チェック結果
            bool result = true;

            // フォーカス移動 要・不要フラグ
            bool isNeedFocusSet = true;

            // エラープロバイダの初期化
            ClearErrorProvider();

            // 未入力チェック
            if (!MinyuryokuCheck(isNeedFocusSet))
            {
                // 今回のチェックでエラーが発生すれば今後はフォーカス移動は不要
                isNeedFocusSet = false;
                // エラーあり
                result = false;

                // データがなければエラー処理
                WinTool.Logic.UI.Obic7MessageBox.Show("M"
                                                    , "0000"
                                                    , "000"
                                                    , "00103"
                                                    , null
                                                    , null
                                                    , this.GetType()
                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                    , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                    );
            }

            if (result == true)
            {
                // 大小チェック
                if (!DaishouCheck(isNeedFocusSet))
                {
                    // 今回のチェックでエラーが発生すれば今後はフォーカス移動は不要
                    isNeedFocusSet = false;
                    // エラーあり
                    result = false;
                }
            }

            if (result == true)
            {
                // 対象期間が同一会計年度外かチェック
                if (!TishouKaikeiNendoCheck())
                {
                    return false;
                }
            }

            if (result == true)
            {
                // 決算期マスタチェック
                if (!GetudoKikanCheck())
                {
                    return false;
                }
            }

            //  2026/10/11  追加  (5525)事業所別消費税計算方式対応 開始
            if (result == true)
            {
                // 消費税計算方式区分を出力区分(全社・事業所)に応じて再取得する
                SetShouhizeiCalcKBN();
            }
            //  2026/10/11  追加  (5525)事業所別消費税計算方式対応 終了

            //2023/07/25    Y.Matsuzawa      修正      (221366)【消費税申告管理】R5年インボイス対応 ↓
            if (result == true)
            {
                // 消費税計算方式区分チェック
                if (!CheckShouhizeiCalcKBN())
                {
                    return false;
                }
            }
            //2023/07/25    Y.Matsuzawa      修正      (221366)【消費税申告管理】R5年インボイス対応 ↑

            if (result == true)
            {
                if (obic7RadioButtonZigyousho.Checked == true)
                {
                    // 事業所マスタチェック
                    if (!GetZigyousho(obic7TextStringZigyoushoCD, obic7LabelStringZigyoushoName, true))
                    {
                        obic7TextStringZigyoushoCD.Focus();
                        return false;
                    }
                }
            }


            return result;
        }
        #endregion

        #region 事業所名取得
        /// <summary>
        /// 事業所名取得
        /// </summary>
        /// <param name="textCode"></param>
        /// <param name="labelCode"></param>
        /// <param name="dispMessage">メッセージ表示有無</param>
        /// <returns>true:取得成功 false:取得失敗</returns>
        private bool GetZigyousho(Obic7TextString textCode, Obic7LabelString labelCode, bool dispMessage)
        {
            //データテーブル
            DataTable zigyoushoData;
            //メッセージコード2
            string messageCD = "";
            //返却値用
            bool result = true;

            //事業所データ取得
            using (zigyoushoData = Obic7fx.Win.M0000.Tool.SearchCom.M0000MS_MA_Zigyousho.SelectByKaiteiDate(textCode.Value, (DateTime)obic7TextDatetimeKikanED.Value, zigSecurityPatternCD, 0, 0, 0, ""))
            {

                if (zigyoushoData.Rows.Count == 1)
                {
                    //2008/06/13 変更 N.Sasaki 開始
                    //if (zigyoushoData.Rows[0]["ZigyoushoNameRyaku"] == DBNull.Value)
                    //{
                    //    messageCD = "00026";
                    //    labelCode.Value = "";
                    //}
                    //// 取得した事業所名がNULLだった場合エラー
                    //else if (string.IsNullOrEmpty(ComTool.Function.AnyToString(zigyoushoData.Rows[0]["ZigyoushoName"])) == true)
                    if (string.IsNullOrEmpty(ComTool.Function.AnyToString(zigyoushoData.Rows[0]["ZigyoushoName"])) == true)
                    //2008/06/13 変更 N.Sasaki 終了
                    {
                        // 事業所名をラベルにセット
                        labelCode.Value = "";

                        //メッセージ表示判定
                        if (dispMessage)
                        {
                            // メッセージを表示
                            Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD
                                                                , CUSTOMIZESEQ
                                                                , MESSAGECD1
                                                                , "00026"
                                                                , null
                                                                , null
                                                                , this.GetType()
                                                                , Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                                , Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                                );
                        }
                        return false;
                    }
                    else
                    {
                        labelCode.Value = Function.AnyToString(zigyoushoData.Rows[0]["ZigyoushoNameRyaku"]);

                        //2009/02/06　変更　T.Nakano  開始　(M04156)事業所名を正式名称を表示出来るようし、それに伴いレイアウトも修正
                        zigyoushoName = Function.AnyToString(zigyoushoData.Rows[0]["ZigyoushoName"]);
                        //2009/02/06　変更　T.Nakano  終了
                    }
                }
                else
                {
                    // 事業所名をラベルにセット
                    labelCode.Value = "";
                    messageCD = "00024";

                }
                if (!string.IsNullOrEmpty(messageCD))
                {
                    //メッセージ表示判定
                    if (dispMessage)
                    {
                        //エラーありの場合メッセージを表示し、処理を中止
                        Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show("M", "0000", MESSAGECD1, messageCD, null, null, this.GetType(),
                                                            Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK,
                                                            Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1);
                    }

                    result = false; ;
                }
            }

            return result;
        }
        #endregion

        #region 初期情報取得
        /// <summary>
        /// 初期情報取得
        /// </summary>
        private void GetDefaultValue()
        {
            //コントロールマスタ取得
            using (DataTable dtControl = WinMToolSearchCom.M0000MS_MA_Control.SelectByKey(1, ""))
            {
                // データが取得有無
                if ((ComTool.Function.IsValidDataTable(dtControl)) == true && (String.IsNullOrEmpty(ComTool.Function.AnyToString(dtControl.Rows[0]["KijunDate"])) == false))
                {
                    //基準日
                    kijunDate = (DateTime)dtControl.Rows[0]["KijunDate"];

                    obic7TextDatetimeKijunBi.Text = kijunDate.ToString("yyyy/MM/dd");
                }
                else
                {
                    //システム日付を取得し変数に退避
                    systemDate = (DateTime)businessDB.ExecuteScalar("SELECT GETDATE()");
                    kijunDate = systemDate;
                    obic7TextDatetimeKijunBi.Text = systemDate.ToString("yyyy/MM/dd");
                }

                if ((ComTool.Function.IsValidDataTable(dtControl)) == true)
                {
                    //売上消費税積上方式区分
                    uriageZeiTumiageKBN = ComTool.Function.AnyToInt32(dtControl.Rows[0]["UriageZeiTumiageKBN"]);

                    //仕入消費税積上方式区分
                    siireZeiTumiageKBN = ComTool.Function.AnyToInt32(dtControl.Rows[0]["SiireZeiTumiageKBN"]);

                    //消費税控除方式区分
                    zeiKoujoKBN = ComTool.Function.AnyToInt32(dtControl.Rows[0]["ZeiKoujoKBN"]);
                }
            }

            // 事業所マスタ情報取得
            using (DataTable zigyoushoData = Win.M0000.Tool.SearchCom.M0000MS_MA_Zigyousho.SelectByKaiteiDate(
                                                          defaultZigyoushoCD
                                                        , kijunDate
                                                        , this.zigSecurityPatternCD
                                                        , 0
                                                        , 0
                                                        , 1
                                                        , ""
                                                        )
                  )
            {

                // データが取得有無
                if (ComTool.Function.IsValidDataTable(zigyoushoData) == true)
                {
                    // 事業所コードセット
                    obic7TextStringZigyoushoCD.Text = defaultZigyoushoCD;
                    // 事業所名をラベルにセット
                    obic7LabelStringZigyoushoName.Text = ComTool.Function.AnyToString(zigyoushoData.Rows[0]["ZigyoushoNameRyaku"]);
                }
                else
                {
                    // 事業所コードセット
                    obic7TextStringZigyoushoCD.Text = "";
                    // 事業所名をラベルにセット
                    obic7LabelStringZigyoushoName.Text = "";
                }
            }
            // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 開始
            if (kaishaUnyouKeitaiKBN != 1)
            {
                using (DataTable dtKakeiControl = businessDB.GetDataTable("SELECT HouzinNO FROM M0000MS_MA_KaikeiControl AS M0000MS_MA_KaikeiControl WHERE ControlKey = 1"))
                {
                    //法人番号をセット
                    houzinNO = Function.AnyToString(dtKakeiControl.Rows[0]["HouzinNO"]);
                }
            }
            // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 終了
        }
        #endregion

        #region 初期値セット
        /// <summary>
        /// 初期値セット
        /// </summary>
        private void SetDefaultValue()
        {
            //月度期間マスタ取得
            using (DataTable dtGetudo = WinMToolSearchCom.M0000MS_MA_GetudoKikan.SelectByKijunbi((DateTime)obic7TextDatetimeKijunBi.Value, ""))
            {
                // データが取得有無
                if (ComTool.Function.IsValidDataTable(dtGetudo) == true)
                {
                    //期間開始セット
                    obic7TextDatetimeKikanST.Text = ((DateTime)dtGetudo.Rows[0]["GetudoKaisiDate"]).ToString("yyyy/MM/dd");

                    //期間終了セット
                    obic7TextDatetimeKikanED.Text = ((DateTime)dtGetudo.Rows[0]["GetudoShuuryouDate"]).ToString("yyyy/MM/dd");

                }
            }
        }
        #endregion

        #region エラープロバイダ表示
        /// <summary>
        /// エラープロバイダを表示
        /// </summary>
        /// <param name="errorControl">エラープロバイダ表示対象コントロール</param>
        /// <param name="errorCD2">表示エラーCD（エラーCD2）</param>
        /// <param name="errorParam">エラー表示用パラメータ（埋め込み文字列）</param>
        private void DispErrorProvider(Control errorControl, string errorCD2, string errorParam)
        {
            //エラーパラメータ
            object[] param = { errorParam };
            //メッセージクラス
            ComToolLogic.Message message = new ComToolLogic.Message();
            //メッセージを取得
            message.GetMessage("M", "0000", MESSAGECD1, errorCD2, param, null);
            //エラープロバイダを表示する
            Win.A0000.Tool.UI.Obic7ErrorProvider.SetError(errorControl, message.MessageSummary);
        }
        #endregion

        #region エラープロバイダ解除
        /// <summary>
        /// エラープロバイダの解除
        /// </summary>
        private void ClearErrorProvider()
        {
            // エラープロバイダの初期化
            Obic7ErrorProvider.Clear(obic7TextDatetimeKijunBi);
            Obic7ErrorProvider.Clear(obic7TextDatetimeKikanST);
            Obic7ErrorProvider.Clear(obic7TextDatetimeKikanED);
            Obic7ErrorProvider.Clear(obic7TextStringZigyoushoCD);
        }
        #endregion

        #region 未入力チェック
        /// <summary>
        /// 未入力チェック
        /// </summary>
        /// <returns>true:エラーなし false:エラーあり</returns>
        private bool MinyuryokuCheck(bool isNeedFocusSet)
        {
            // チェック結果
            bool result = true;

            // 基準日
            if (String.IsNullOrEmpty(obic7TextDatetimeKijunBi.Text))
            {
                // フォーカスセット要 かつ エラー項目がない場合フォーカスを移動する
                if ((isNeedFocusSet == true) && (result == true))
                {
                    obic7TextDatetimeKijunBi.Focus();
                }
                // 基準日が未入力であればエラーを表示する
                DispErrorProvider(obic7TextDatetimeKijunBi, "00104", null);
                // 今回のチェックでエラーが発生すれば今後はフォーカス移動は不要
                isNeedFocusSet = false;
                result = false;
            }

            // 対象期間_開始
            if (String.IsNullOrEmpty(obic7TextDatetimeKikanST.Text))
            {
                // フォーカスセット要 かつ エラー項目がない場合フォーカスを移動する
                if ((isNeedFocusSet == true) && (result == true))
                {
                    obic7TextDatetimeKikanST.Focus();
                }
                // 対象期間_開始が未入力であればエラーを表示する
                DispErrorProvider(obic7TextDatetimeKikanST, "00104", null);
                // 今回のチェックでエラーが発生すれば今後はフォーカス移動は不要
                isNeedFocusSet = false;
                result = false;
            }

            // 対象期間_終了
            if (String.IsNullOrEmpty(obic7TextDatetimeKikanED.Text))
            {
                // フォーカスセット要 かつ エラー項目がない場合フォーカスを移動する
                if ((isNeedFocusSet == true) && (result == true))
                {
                    obic7TextDatetimeKikanED.Focus();
                }
                // 対象期間_開始が未入力であればエラーを表示する
                DispErrorProvider(obic7TextDatetimeKikanED, "00104", null);
                // 今回のチェックでエラーが発生すれば今後はフォーカス移動は不要
                isNeedFocusSet = false;
                result = false;
            }

            //事業所CD
            if ((obic7RadioButtonZigyousho.Checked == true) && (String.IsNullOrEmpty(obic7TextStringZigyoushoCD.Text) == true))
            {
                // フォーカスセット要 かつ エラー項目がない場合フォーカスを移動する
                if ((isNeedFocusSet == true) && (result == true))
                {
                    obic7TextStringZigyoushoCD.Focus();
                }
                //エラープロバイダを表示
                DispErrorProvider(obic7TextStringZigyoushoCD, "00104", null);

                obic7LabelStringZigyoushoName.Text = "";

                // 今回のチェックでエラーが発生すれば今後はフォーカス移動は不要
                isNeedFocusSet = false;
                result = false;
            }

            //格納先
            //F9またはF10押下の場合のみチェック
            if (shuturyokuKBN == 0 || shuturyokuKBN == 1)
            {
                if (String.IsNullOrEmpty(obic7TextStringOutPath.Text))
                {
                    // フォーカスセット要 かつ エラー項目がない場合フォーカスを移動する
                    if ((isNeedFocusSet == true) && (result == true))
                    {
                        obic7TextStringOutPath.Focus();
                    }
                    //エラープロバイダを表示
                    DispErrorProvider(obic7TextStringOutPath, "00104", null);
                    // 今回のチェックでエラーが発生すれば今後はフォーカス移動は不要
                    isNeedFocusSet = false;
                    result = false;
                }
            }

            return result;
        }

        #endregion

        #region 大小チェック
        /// <summary>
        /// 大小チェック
        /// </summary>
        /// <returns>true:エラーなし false:エラーあり</returns>
        private bool DaishouCheck(bool isNeedFocusSet)
        {
            // チェック結果
            bool result = true;

            // 対象期間(大小チェック)
            if (!InputItemCheck(obic7TextDatetimeKikanST, obic7TextDatetimeKikanED, "対象期間", isNeedFocusSet))
            {
                // 今回のチェックでエラーが発生すれば今後はフォーカス移動は不要
                isNeedFocusSet = false;
                // エラーあり
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 入力項目チェック_日付用
        /// </summary>
        /// <param name="textFrom">条件（開始）入力テキストボックス</param>
        /// <param name="textTo">条件（終了）入力テキストボックス</param>
        /// <param name="itemName">対象項目名（エラー表示用）</param>
        /// <param name="isNeedSetFocus">フォーカスセット要・不要フラグ</param>
        /// <returns>true:エラーなし false:エラーあり</returns>
        private bool InputItemCheck(Obic7TextDatetime textFrom, Obic7TextDatetime textTo, string itemName, bool isNeedSetFocus)
        {
            // チェック結果
            bool result = true;

            // エラープロバイダを解除
            Obic7ErrorProvider.Clear(textFrom);
            Obic7ErrorProvider.Clear(textTo);

            if ((textFrom.Value != null) && (textTo.Value != null))
            {
                // 両方が入力されていた場合は大小比較を行う
                if (DateTime.Compare((DateTime)textFrom.Value, (DateTime)textTo.Value) > 0)
                {
                    // 開始＞終了ならば以下の処理を行う
                    // フォーカスセット要 かつ エラー項目がない場合フォーカスを移動する
                    if ((isNeedSetFocus == true) && (result == true))
                    {
                        textFrom.Focus();
                    }
                    // エラープロバイダに範囲指定エラーを表示
                    // 開始・終了両方に表示するようにする
                    DispErrorProvider(textFrom, "00105", null);
                    DispErrorProvider(textTo, "00105", null);
                    result = false;

                }
            }
            return result;
        }
        #endregion

        #region 対象期間が同一会計年度外かチェック
        /// <summary>
        /// 対象期間が同一会計年度外かチェック
        /// </summary>
        /// <returns>true:エラーなし false:エラーあり</returns>
        private bool TishouKaikeiNendoCheck()
        {
            string kessankiST = "";
            string kessankiED = "";

            // 月度期間マスタクラスを対象期間_開始を引数として呼ぶ
            using (DataTable getudoKikanSTData = Win.M0000.Tool.SearchCom.M0000MS_MA_GetudoKikan.SelectByKijunbi((DateTime)obic7TextDatetimeKikanST.Value, ""))
            {
                if (Com.A0000.Tool.Function.IsValidDataTable(getudoKikanSTData))
                {
                    kessankiST = Function.AnyToString(getudoKikanSTData.Rows[0]["Kessanki"]);
                }
            }

            // 月度期間マスタクラスを対象期間_終了を引数として呼ぶ
            using (DataTable getudoKikanEDData = Win.M0000.Tool.SearchCom.M0000MS_MA_GetudoKikan.SelectByKijunbi((DateTime)obic7TextDatetimeKikanED.Value, ""))
            {
                if (Com.A0000.Tool.Function.IsValidDataTable(getudoKikanEDData))
                {
                    kessankiED = Function.AnyToString(getudoKikanEDData.Rows[0]["Kessanki"]);
                }
            }

            //2011/01/06 OBIC T.Sugawa 変更 開始 (M04472-01)決算期を跨いで出力出来るよう変更(2010/08/30 OBIC H.Sudo 変更分マージ)
            //if (kessankiST != kessankiED)
            //{

            //    // データがなければエラー処理
            //    Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show("M"
            //                                        , "0000"
            //                                        , "000"
            //                                        , "00387"
            //                                        , null
            //                                        , null
            //                                        , this.GetType()
            //                                        , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
            //                                        , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
            //                                        );

            //    obic7TextDatetimeKikanST.Focus();
            //    return false;

            //}
            //2011/01/06 OBIC T.Sugawa 変更 終了(M04472-01)決算期を跨いで出力出来るよう変更(2010/08/30 OBIC H.Sudo 変更分マージ)

            return true;

        }
        #endregion

        #region 決算期マスタチェック
        /// <summary>
        /// 決算期マスタチェック
        /// </summary>
        /// <returns>true:エラーなし false:エラーあり</returns>
        private bool GetudoKikanCheck()
        {
            // 決算期マスタクラスを対象期間_開始を引数として呼ぶ
            //2008/06/17 変更 N.Sasaki 開始
            //            using (DataTable getudoKikanData = Win.M0000.Tool.SearchCom.M0000MS_MA_GetudoKikan.SelectByKijunbi((DateTime)obic7TextDatetimeKikanST.Value, ""))
            using (DataTable getudoKikanData = Win.M0000.Tool.SearchCom.M0000MS_MA_Kessanki.SelectByKijunbi((DateTime)obic7TextDatetimeKikanST.Value, ""))
            //2008/06/17 変更 N.Sasaki 終了
            {
                // データが取得有無
                if (Com.A0000.Tool.Function.IsValidDataTable(getudoKikanData) != true)
                {
                    // データがなければエラー処理
                    Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show("M"
                                                        , "0000"
                                                        , "000"
                                                        , "00388"
                                                        , null
                                                        , null
                                                        , this.GetType()
                                                        , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                        , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                        );
                    obic7TextDatetimeKikanST.Focus();

                    return false;
                }
            }

            // 決算期マスタクラスを対象期間_終了を引数として呼ぶ
            //2008/06/17 変更 N.Sasaki 開始
            //            using (DataTable getudoKikanData = Win.M0000.Tool.SearchCom.M0000MS_MA_GetudoKikan.SelectByKijunbi((DateTime)obic7TextDatetimeKikanED.Value, ""))
            using (DataTable getudoKikanData = Win.M0000.Tool.SearchCom.M0000MS_MA_Kessanki.SelectByKijunbi((DateTime)obic7TextDatetimeKikanED.Value, ""))
            //2008/06/17 変更 N.Sasaki 終了
            {
                // データが取得有無
                if (Com.A0000.Tool.Function.IsValidDataTable(getudoKikanData) != true)
                {
                    // データがなければエラー処理
                    Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show("M"
                                                        , "0000"
                                                        , "000"
                                                        , "00388"
                                                        , null
                                                        , null
                                                        , this.GetType()
                                                        , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK
                                                        , Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1
                                                        );
                    obic7TextDatetimeKikanED.Focus();

                    return false;
                }
            }

            return true;

        }
        #endregion

        //2023/07/25    Y.Matsuzawa      修正      (221366)【消費税申告管理】R5年インボイス対応 ↓
        #region 消費税計算方式区分チェック
        /// <summary>
        /// 消費税計算方式区分チェック
        /// </summary>
        /// <returns>true:エラーなし false:エラーあり</returns>
        private bool CheckShouhizeiCalcKBN()
        {
            //(R5.10.1以後売上消費税計算方式区分 = 0(割戻) かつ　R5.10.1以後仕入消費税計算方式区分 = 1(積上))
            //かつ
            //(売上消費税計算方式区分 != R5.10.1以後売上消費税計算方式区分 または　仕入消費税計算方式区分 != R5.10.1以後仕入消費税計算方式区分)
            if ((_r5NenOctIgoUriageZeiTumiageKBN == 0 && _r5NenOctIgoSiireZeiTumiageKBN == 1)
                &&
                ((uriageZeiTumiageKBN != _r5NenOctIgoUriageZeiTumiageKBN) || (siireZeiTumiageKBN != _r5NenOctIgoSiireZeiTumiageKBN)))
            {
                //確認メッセージ
                if (Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show(SYSTEMCATEGORYCD,
                                                                         CUSTOMIZESEQ,
                                                                         MESSAGE_CD1,
                                                                         "30444",
                                                                         null,
                                                                         null,
                                                                         this.GetType(),
                                                                         Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.YesNo,
                                                                         Obic7fx.Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    //元の項目へフォーカス移動
                    this.PrevControl.Focus();
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region 財務コントロールマスタ取得クエリ
        /// <summary>
        /// 財務コントロールマスタ取得クエリ
        /// </summary>
        /// <returns></returns>
        private string GetMAControl()
        {
            //SQL文
            StringBuilder sqlSelect = new StringBuilder();

            //SQL文生成
            sqlSelect.AppendLine(" SELECT ");
            sqlSelect.AppendLine("        KeiriHousikiKBN ");
            sqlSelect.AppendLine("       ,R5NenOctIgoUriageZeiTumiageKBN ");
            sqlSelect.AppendLine("       ,R5NenOctIgoSiireZeiTumiageKBN ");
            sqlSelect.AppendLine(" FROM ");
            sqlSelect.AppendLine("        M0000MS_MA_Control ");
            sqlSelect.AppendLine(" WHERE  ControlKey = 1 ");
            return sqlSelect.ToString();
        }
        #endregion

        //  2026/10/11  追加  (5525)事業所別消費税計算方式対応 開始
        #region 消費税計算方式区分の取得(事業所別)
        /// <summary>
        /// 消費税計算方式区分を取得し、変数にセットする
        /// 出力区分＝事業所の場合：事業所別制御マスタの値(未設定の区分は財務コントロールマスタの値)
        /// 出力区分＝全社の場合　：財務コントロールマスタの値
        /// </summary>
        private void SetShouhizeiCalcKBN()
        {
            string zigyoushoCD = null;
            if (obic7RadioButtonZigyousho.Checked == true && !String.IsNullOrEmpty(obic7TextStringZigyoushoCD.Value))
            {
                zigyoushoCD = obic7TextStringZigyoushoCD.Value;
            }

            using (DataTable dtCalcKBN = businessDB.GetDataTable(GetShouhizeiCalcKBNSQL(zigyoushoCD)))
            {
                if (!ComTool.Function.IsValidDataTable(dtCalcKBN))
                {
                    return;
                }
                //R5.9.30以前売上消費税計算方式区分
                uriageZeiTumiageKBN = ComTool.Function.AnyToInt32(dtCalcKBN.Rows[0]["UriageZeiTumiageKBN"]);
                //R5.9.30以前仕入消費税計算方式区分
                siireZeiTumiageKBN = ComTool.Function.AnyToInt32(dtCalcKBN.Rows[0]["SiireZeiTumiageKBN"]);
                //R5.10.1以後売上消費税計算方式区分
                _r5NenOctIgoUriageZeiTumiageKBN = ComTool.Function.AnyToInt32(dtCalcKBN.Rows[0]["R5NenOctIgoUriageZeiTumiageKBN"]);
                //R5.10.1以後仕入消費税計算方式区分
                _r5NenOctIgoSiireZeiTumiageKBN = ComTool.Function.AnyToInt32(dtCalcKBN.Rows[0]["R5NenOctIgoSiireZeiTumiageKBN"]);
            }
        }

        /// <summary>
        /// 消費税計算方式区分取得SQL
        /// </summary>
        /// <param name="zigyoushoCD">事業所コード(全社の場合はnull)</param>
        /// <returns>SQL文</returns>
        private string GetShouhizeiCalcKBNSQL(string zigyoushoCD)
        {
            string zigyoushoCDValue = (zigyoushoCD == null) ? "NULL" : ComTool.Function.SqlEscapeVarchar(zigyoushoCD);

            StringBuilder sqlSelect = new StringBuilder();
            sqlSelect.AppendLine(" SELECT ");
            sqlSelect.AppendLine("        ISNULL(ZSG.UriageZeiTumiageKBN, CTL.UriageZeiTumiageKBN) AS UriageZeiTumiageKBN ");
            sqlSelect.AppendLine("       ,ISNULL(ZSG.SiireZeiTumiageKBN, CTL.SiireZeiTumiageKBN) AS SiireZeiTumiageKBN ");
            sqlSelect.AppendLine("       ,ISNULL(ZSG.R5NenOctIgoUriageZeiTumiageKBN, CTL.R5NenOctIgoUriageZeiTumiageKBN) AS R5NenOctIgoUriageZeiTumiageKBN ");
            sqlSelect.AppendLine("       ,ISNULL(ZSG.R5NenOctIgoSiireZeiTumiageKBN, CTL.R5NenOctIgoSiireZeiTumiageKBN) AS R5NenOctIgoSiireZeiTumiageKBN ");
            sqlSelect.AppendLine(" FROM   M0000MS_MA_Control AS CTL ");
            sqlSelect.AppendLine(" LEFT JOIN M5525MS_MA_ZigyoushoSeigyo AS ZSG ");
            sqlSelect.AppendLine("        ON ZSG.ZigyoushoCD = " + zigyoushoCDValue + " ");
            sqlSelect.AppendLine(" WHERE  CTL.ControlKey = 1 ");
            return sqlSelect.ToString();
        }
        #endregion
        //  2026/10/11  追加  (5525)事業所別消費税計算方式対応 終了
        //2023/07/25    Y.Matsuzawa      修正      (221366)【消費税申告管理】R5年インボイス対応 ↑

        #region GetXml - 前回値が保存されているxmlファイルを取得する
        /// <summary>
        /// 前回値が保存されているxmlファイルを取得する
        /// </summary>
        /// <returns>True:成功 / False:失敗</returns>
        private bool GetXml(out System.Xml.XmlDocument xmldoc)
        {
            return Obic7fx.Win.A0000.Tool.Logic.Xml.ReadXml(ComToolLogic.CurrentUser.UserID, ComToolLogic.CurrentUser.JobID, out xmldoc);
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
            DateTime dtwork;
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
            }
            //Obic7TextAbstractを継承しているコントロールならば同一コントロールに値をセット
            else if (control is Obic7fx.Win.A0000.Tool.UI.Obic7TextAbstract)
            {
                if (((Obic7fx.Win.A0000.Tool.UI.Obic7TextAbstract)control).Name.Equals(xmlelement.Name) && ((Obic7fx.Win.A0000.Tool.UI.Obic7TextAbstract)control).Enabled)
                {
                    ((Obic7fx.Win.A0000.Tool.UI.Obic7TextAbstract)control).Text = xmlelement.Attributes.GetNamedItem("value").Value;
                }
                // 2019/09/02  変更 M.Washizu (139334-139359)【電子申告対応】開始
                //格納先
                if (((Obic7fx.Win.A0000.Tool.UI.Obic7TextAbstract)control).Name.Equals(this.obic7TextStringOutPath.Name))
                {
                    if (((Obic7fx.Win.A0000.Tool.UI.Obic7TextAbstract)control).Name.Equals(xmlelement.Name))
                    {
                        if (string.IsNullOrEmpty(ComTool.Function.AnyToString(xmlelement.Attributes.GetNamedItem("value").Value)))
                        {
                            //前回情報が空の場合は、格納先・ファイル名は表示しない
                            this.obic7TextStringOutPath.Text = string.Empty;
                        }
                        else
                        {
                            if (Obic7fx.Win.A0000.Tool.Logic.UI.Obic7FolderBrowserDialog.CheckDirectorySecurity(xmlelement.Attributes.GetNamedItem("value").Value) == false)
                            {
                                //デフォルトパスの権限を失っていた場合は、格納先・ファイル名は表示しない
                                this.obic7TextStringOutPath.Text = string.Empty;
                            }
                            else
                            {
                                ((Obic7fx.Win.A0000.Tool.UI.Obic7TextAbstract)control).Text = xmlelement.Attributes.GetNamedItem("value").Value;
                            }
                        }
                    }
                }
                // 2019/09/02  変更 M.Washizu (139334-139359)【電子申告対応】終了

            }
            //チェックボックスならば同一コントロールに値をセット
            else if (control is Obic7fx.Win.A0000.Tool.UI.Obic7CheckBox)
            {
                if (((Obic7fx.Win.A0000.Tool.UI.Obic7CheckBox)control).Name.Equals(xmlelement.Name) && ((Obic7fx.Win.A0000.Tool.UI.Obic7CheckBox)control).Enabled)
                {
                    if (xmlelement.Attributes.GetNamedItem("value").Value.Equals("True"))
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
            //ラベルコントロールならば同一コントロールに値をセット
            else if (control is Obic7fx.Win.A0000.Tool.UI.Obic7LabelString)
            {
                //（ラベル項目については任意検索コントロールのみXMLに保存されている想定）
                if (((Obic7fx.Win.A0000.Tool.UI.Obic7LabelString)control).Name.Equals(xmlelement.Name))
                {
                }
            }

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
                // 基準日は除く(当ジョブ基本設計より)
                if ((Obic7fx.Win.A0000.Tool.UI.Obic7TextAbstract)control != obic7TextDatetimeKijunBi)
                {
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
            return Obic7fx.Win.A0000.Tool.Logic.Xml.SaveXml(ComToolLogic.CurrentUser.UserID, ComToolLogic.CurrentUser.JobID, xmldoc);
        }
        #endregion

        #region SetValueFromXml - xmlから取得した値に応じた名称を取得する
        /// <summary>
        /// xmlから取得した値に応じた名称を取得する
        /// </summary>
        private void SetValueFromXml()
        {
            DateTime dtwork;
            //==============================================================
            // 各々の項目の検証イベントより名称取得
            // ただし、エラー処理は行わない(会計共通設計規約より)
            //=============================================================

            // 対象期間開始が日付に変換可能な場合
            if (DateTime.TryParse(this.obic7TextDatetimeKikanST.Text, out dtwork))
            {
                // 日付変換可能であれば、変換を行う
                this.obic7TextDatetimeKikanST.Text = ComTool.Function.FormatDate(this.obic7TextDatetimeKikanST.Text);

            }
            // 対象期間終了が日付に変換可能な場合
            if (DateTime.TryParse(this.obic7TextDatetimeKikanED.Text, out dtwork))
            {
                // 日付変換可能であれば、変換を行う
                this.obic7TextDatetimeKikanED.Text = ComTool.Function.FormatDate(this.obic7TextDatetimeKikanED.Text);
            }
            // 対象期間終了が日付に変換可能な場合
            if (DateTime.TryParse(this.obic7TextDatetimeKikanED.Text, out dtwork))
            {
                // 事業所
                SetNameByF06(obic7TextStringZigyoushoCD);
            }
        }
        #endregion

        #region xmlから取得時、各項目のラベル値を取得
        /// <summary>
        /// xmlから取得時、各項目のラベル値を取得
        /// (Obic7TextString時)
        /// </summary>
        /// <param name="ctr"></param>
        private void SetNameByF06(WinTool.UI.Obic7TextString ctr)
        {
            // コントロール判定用-ラベル
            WinTool.UI.Obic7LabelString labelAbstract = null;
            // コントロールに対応するデータテーブル
            DataTable abstractData = null;
            // 取得する名称
            string abstractName = "";
            // エラーCD2(未使用)
            string abstractErrorCD2 = "";

            MultiTextStringInfoSet(ctr, out labelAbstract, out abstractData, out abstractName, out abstractErrorCD2);
            // 入力値 == "" 
            if (String.IsNullOrEmpty(ctr.Text))
            {
                labelAbstract.Text = "";
            }
            // 入力値 != ""
            else
            {
                // 事業所コードの場合
                if (ctr == obic7TextStringZigyoushoCD)
                {
                    // 事業所略名取得
                    GetZigyousho(obic7TextStringZigyoushoCD, obic7LabelStringZigyoushoName, false);
                }
                else
                {
                    // データテーブルの件数が１件の場合
                    if (abstractData.Rows.Count == 1)
                    {
                        // 取得したマスタクラスからコードに対する名称を取得し、画面の名称にセット
                        labelAbstract.Text = ComTool.Function.AnyToString(abstractData.Rows[0][abstractName]);
                    }
                    else
                    {
                        // 画面の名称コントロールを初期化
                        labelAbstract.Value = "";
                    }

                    // テーブルの解放
                    abstractData.Dispose();
                }
            }
        }

        /// <summary>
        /// xmlから取得時、各項目のラベル値を取得
        /// (Obic7TextDatetime時)
        /// </summary>
        /// <param name="ctr"></param>
        private void SetNameByF06(WinTool.UI.Obic7TextDatetime ctr)
        {
            // 入力値 == "" 
            if (String.IsNullOrEmpty(ctr.Text))
            {
                return;
            }
            // 入力値 != ""
            else
            {
                //日付変換できなければ脱出
                try
                {
                    System.Convert.ToDateTime(ctr.Text);
                }
                catch
                {
                    return;
                }
            }
        }


        #region データ読み込みする項目の情報セット
        /// <summary>
        /// データ読み込みする項目の情報セット
        /// </summary>
        /// <param name="textAbstract">コントロール判定用-テキストボックス</param>
        /// <param name="labelAbstract">コントロール判定用-ラベル</param>
        /// <param name="abstractData">コントロールに対応するデータテーブル</param>
        /// <param name="abstractName">取得する名称</param>
        /// <param name="abstractErrorCD2">エラーCD2</param>
        private void MultiTextStringInfoSet(
                                              WinTool.UI.Obic7TextString textAbstract
                                            , out WinTool.UI.Obic7LabelString labelAbstract
                                            , out DataTable abstractData
                                            , out string abstractName
                                            , out string abstractErrorCD2
                                            )
        {
            // out用引数初期化
            labelAbstract = null;
            abstractData = null;
            abstractName = null;
            abstractErrorCD2 = null;
            // 事業所コードの場合
            if (textAbstract == obic7TextStringZigyoushoCD)
            {
                // 対応される名称コントロール(名称クリア用)
                labelAbstract = this.obic7LabelStringZigyoushoName;
            }
        }

        #endregion

        #endregion

        #region ストアドエラー処理
        /// <summary>
        /// ストアドエラー処理
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

            if (!String.IsNullOrEmpty(errorSummary.Value.ToString()))
            {
                objErrSummary = errorSummary.Value.ToString().Split('\v');
            }

            if (!String.IsNullOrEmpty(errorDetail.Value.ToString()))
            {
                objErrDetail = errorDetail.Value.ToString().Split('\v');
            }

            //ストアドからの戻り値より、メッセージボックスに表示するボタン種類とデフォルトボタンを設定する。
            Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons btnType;
            Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton btnDefault;

            switch (Com.A0000.Tool.Function.AnyToInt16(errorBtnKBN.Value.ToString()))
            {
                case 0:
                    btnType = Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK;
                    break;

                case 1:
                    btnType = Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.YesNo;
                    break;

                case 2:
                    btnType = Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.YesNoCancel;
                    break;

                default:
                    btnType = Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxButtons.OK;
                    break;
            }

            switch (Com.A0000.Tool.Function.AnyToInt16(errorBtnDefault.Value.ToString()))
            {
                case 0:
                    btnDefault = Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1;
                    break;

                case 1:
                    btnDefault = Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button2;
                    break;

                case 2:
                    btnDefault = Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button3;
                    break;

                default:
                    btnDefault = Win.A0000.Tool.Logic.UI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1;
                    break;
            }

            Win.A0000.Tool.Logic.UI.Obic7MessageBox.Show("M", "0000", errorCD1.Value.ToString(), errorCD2.Value.ToString(), objErrSummary, objErrDetail, GetType(),
                                                        btnType,
                                                        btnDefault);

        }
        #endregion

        #region 法人番号を取得
        //  2015/07/02  変更        R.Aoki           (25192-25347-25385)リバースチャージ対応  開始
        /// <summary>
        /// 法人番号を取得 
        /// </summary>
        /// <returns>DBより法人番号を取得</returns>
        private void GetHouzinNO()
        {
            //SQL文
            StringBuilder sqlSelect = new StringBuilder();
            //データテーブル
            DataTable selectHouzinNO;

            //SQL文生成
            sqlSelect.Append(" SELECT ");
            sqlSelect.Append("      Z.HouzinNO ");
            sqlSelect.Append(" FROM ");
            sqlSelect.Append("      M0000MS_MA_Zigyousho AS Z ");
            sqlSelect.Append(" INNER JOIN ");
            sqlSelect.Append(" (SELECT  Z.ZigyoushoCD ");
            sqlSelect.Append("         ,MAX(Z.KaiteiDate) AS KaiteiDate ");
            sqlSelect.Append(" FROM     M0000MS_MA_Zigyousho AS Z ");
            sqlSelect.Append(" WHERE    Z.KaiteiDate <= " + ComTool.Function.SqlEscapeDatetime(this.obic7TextDatetimeKikanED.Value, "yyyy/MM/dd") + " ");
            sqlSelect.Append(" GROUP BY Z.ZigyoushoCD ");
            sqlSelect.Append("       ) Z1 ");
            sqlSelect.Append(" ON Z1.ZigyoushoCD = Z.ZigyoushoCD ");
            sqlSelect.Append(" AND Z1.KaiteiDate = Z.KaiteiDate ");
            sqlSelect.Append(" AND    Z.ZigyoushoCD = " + ComTool.Function.SqlEscapeVarchar(obic7TextStringZigyoushoCD.Value) + " ");
            sqlSelect.Append(" ORDER BY Z.ZigyoushoCD ASC");
            selectHouzinNO = businessDB.GetDataTable(sqlSelect.ToString());

            if (ComTool.Function.IsValidDataTable(selectHouzinNO))
            {
                if (kaishaUnyouKeitaiKBN == 1)
                {
                    houzinNO = ComTool.Function.AnyToString(selectHouzinNO.Rows[0]["HouzinNO"]);
                }
            }
            else
            {
                return;
            }
        }
        #endregion
        //  2015/07/02  変更        R.Aoki           (25192-25347-25385)リバースチャージ対応  終了

        // 2019/09/02  変更 M.Washizu (139334-139359)【電子申告対応】開始
        #region DBアクセス

        //  2023/07/24  修正        Y.Nakajima       (221366)【消費税申告管理】R5年インボイス対応(消費税申告の達人連携対応分) 開始
        #region SELECT結果一時退避WK作成
        /// <summary>
        /// SELECT結果一時退避WK作成
        /// </summary>
        private void CreateTempCalcResultWK(WinToolLogic.DBAccess businessDB)
        {
            StringBuilder strSQL = new StringBuilder();

            //SQL文生成
            #region SELECT結果一時退避WK作成SQL文
            strSQL.AppendLine(" IF EXISTS (SELECT * FROM tempdb.sys.objects WHERE object_id = OBJECT_ID('tempdb..#M0000WK_TempCalcResult') AND type = N'U') ");
            strSQL.AppendLine(" BEGIN ");
            strSQL.AppendLine(" DROP TABLE #M0000WK_TempCalcResult ");
            strSQL.AppendLine(" END ");
            strSQL.AppendLine(" CREATE TABLE #M0000WK_TempCalcResult ");
            strSQL.AppendLine(" ( ");
            strSQL.AppendLine("    TableKey   int                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEZEINUKI10_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEZEIGAKU10_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEZEIKOMI10_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEZEINUKI10_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEZEIGAKU10_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEZEIKOMI10_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,MENZEIURIAGEZEINUKI10   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,HIKAZEIURIAGEYUUKAZEINUKI10   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,HIKAZEIURIAGEYUUKAIGAIZEINUKI10   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,HIKAZEISHISANYUSHUTSUZEINUKI10   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEHENKANZEINUKI10_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEHENKANZEIGAKU10_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEHENKANZEIKOMI10_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEHENKANZEINUKI10_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEHENKANZEIGAKU10_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEHENKANZEIKOMI10_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEZEINUKI8K_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEZEIGAKU8K_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEZEIKOMI8K_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEZEINUKI8K_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEZEIGAKU8K_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEZEIKOMI8K_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,MENZEIURIAGEZEINUKI8K   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,HIKAZEIURIAGEYUUKAZEINUKI8K   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,HIKAZEIURIAGEYUUKAIGAIZEINUKI8K   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,HIKAZEISHISANYUSHUTSUZEINUKI8K   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEHENKANZEINUKI8K_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEHENKANZEIGAKU8K_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEHENKANZEIKOMI8K_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEHENKANZEINUKI8K_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEHENKANZEIGAKU8K_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEHENKANZEIKOMI8K_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEZEINUKI8_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEZEIGAKU8_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEZEIKOMI8_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEZEINUKI8_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEZEIGAKU8_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEZEIKOMI8_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,MENZEIURIAGEZEINUKI8   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,HIKAZEIURIAGEYUUKAZEINUKI8   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,HIKAZEIURIAGEYUUKAIGAIZEINUKI8   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,HIKAZEISHISANYUSHUTSUZEINUKI8   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEHENKANZEINUKI8_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEHENKANZEIGAKU8_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEHENKANZEIKOMI8_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEHENKANZEINUKI8_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEHENKANZEIGAKU8_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEHENKANZEIKOMI8_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEZEINUKI5_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEZEIGAKU5_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEZEIKOMI5_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEZEINUKI5_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEZEIGAKU5_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEZEIKOMI5_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,MENZEIURIAGEZEINUKI5   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,HIKAZEIURIAGEYUUKAZEINUKI5   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,HIKAZEIURIAGEYUUKAIGAIZEINUKI5   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,HIKAZEISHISANYUSHUTSUZEINUKI5   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEHENKANZEINUKI5_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEHENKANZEIGAKU5_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEHENKANZEIKOMI5_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEHENKANZEINUKI5_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEHENKANZEIGAKU5_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEHENKANZEIKOMI5_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEZEINUKI3_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEZEIGAKU3_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEZEIKOMI3_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEZEINUKI3_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEZEIGAKU3_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEZEIKOMI3_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,MENZEIURIAGEZEINUKI3   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,HIKAZEIURIAGEYUUKAZEINUKI3   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,HIKAZEIURIAGEYUUKAIGAIZEINUKI3   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,HIKAZEISHISANYUSHUTSUZEINUKI3   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEHENKANZEINUKI3_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEHENKANZEIGAKU3_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEHENKANZEIKOMI3_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEHENKANZEINUKI3_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEHENKANZEIGAKU3_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEHENKANZEIKOMI3_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEINUKI10_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIGAKU10_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIKOMI10_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEINUKI10_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIGAKU10_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIKOMI10_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEINUKI10_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIGAKU10_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIKOMI10_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEINUKI10_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIGAKU10_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIKOMI10_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEINUKI10_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIGAKU10_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIKOMI10_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEINUKI10_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIGAKU10_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIKOMI10_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIGAKU10_INVOICEIGAI_TSUMIAGE_KANBEN   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIKOMI10_INVOICEIGAI_TSUMIAGE_KANBEN   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIGAKU10_INVOICEIGAI_TSUMIAGE_KANBEN   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIKOMI10_INVOICEIGAI_TSUMIAGE_KANBEN   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIGAKU10_INVOICEIGAI_TSUMIAGE_KANBEN   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIKOMI10_INVOICEIGAI_TSUMIAGE_KANBEN   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEINUKI10_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIGAKU10_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIKOMI10_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEINUKI10_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIGAKU10_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIKOMI10_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEINUKI10_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIGAKU10_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIKOMI10_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEINUKI10_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIGAKU10_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIKOMI10_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEINUKI10_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIGAKU10_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIKOMI10_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEINUKI10_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIGAKU10_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIKOMI10_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,TOKUTEISHIIREKAZEIZEINUKI10   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,TOKUTEISHIIREHIKAZEIZEINUKI10   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,TOKUTEISHIIREKYOTUUZEINUKI10   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,YUNYUSHIIREKAZEIZEIGAKU10   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,YUNYUSHIIREHIKAZEIZEIGAKU10   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,YUNYUSHIIREKYOTUUZEIGAKU10   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,YUNYUSHIIRETIHO10   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,TOKUTEISHIIREHENKANKAZEIZEINUKI10   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,TOKUTEISHIIREHENKANHIKAZEIZEINUKI10   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,TOKUTEISHIIREHENKANKYOTUUZEINUKI10   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEINUKI8K_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIGAKU8K_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIKOMI8K_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEINUKI8K_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIGAKU8K_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIKOMI8K_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEINUKI8K_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIGAKU8K_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIKOMI8K_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEINUKI8K_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIGAKU8K_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIKOMI8K_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEINUKI8K_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIGAKU8K_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIKOMI8K_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEINUKI8K_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIGAKU8K_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIKOMI8K_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIGAKU8K_INVOICEIGAI_TSUMIAGE_KANBEN   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIKOMI8K_INVOICEIGAI_TSUMIAGE_KANBEN   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIGAKU8K_INVOICEIGAI_TSUMIAGE_KANBEN   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIKOMI8K_INVOICEIGAI_TSUMIAGE_KANBEN   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIGAKU8K_INVOICEIGAI_TSUMIAGE_KANBEN   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIKOMI8K_INVOICEIGAI_TSUMIAGE_KANBEN   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEINUKI8K_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIGAKU8K_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIKOMI8K_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEINUKI8K_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIGAKU8K_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIKOMI8K_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEINUKI8K_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIGAKU8K_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIKOMI8K_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEINUKI8K_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIGAKU8K_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIKOMI8K_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEINUKI8K_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIGAKU8K_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIKOMI8K_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEINUKI8K_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIGAKU8K_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIKOMI8K_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,YUNYUSHIIREKAZEIZEIGAKU8K   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,YUNYUSHIIREHIKAZEIZEIGAKU8K   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,YUNYUSHIIREKYOTUUZEIGAKU8K   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,YUNYUSHIIRETIHO8K   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEINUKI8_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIGAKU8_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIKOMI8_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEINUKI8_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIGAKU8_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIKOMI8_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEINUKI8_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIGAKU8_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIKOMI8_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEINUKI8_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIGAKU8_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIKOMI8_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEINUKI8_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIGAKU8_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIKOMI8_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEINUKI8_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIGAKU8_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIKOMI8_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIGAKU8_INVOICEIGAI_TSUMIAGE_KANBEN   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIKOMI8_INVOICEIGAI_TSUMIAGE_KANBEN   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIGAKU8_INVOICEIGAI_TSUMIAGE_KANBEN   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIKOMI8_INVOICEIGAI_TSUMIAGE_KANBEN   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIGAKU8_INVOICEIGAI_TSUMIAGE_KANBEN   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIKOMI8_INVOICEIGAI_TSUMIAGE_KANBEN   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEINUKI8_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIGAKU8_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIKOMI8_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEINUKI8_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIGAKU8_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIKOMI8_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEINUKI8_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIGAKU8_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIKOMI8_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEINUKI8_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIGAKU8_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIKOMI8_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEINUKI8_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIGAKU8_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIKOMI8_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEINUKI8_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIGAKU8_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIKOMI8_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,TOKUTEISHIIREKAZEIZEINUKI8   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,TOKUTEISHIIREHIKAZEIZEINUKI8   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,TOKUTEISHIIREKYOTUUZEINUKI8   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,YUNYUSHIIREKAZEIZEIGAKU8   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,YUNYUSHIIREHIKAZEIZEIGAKU8   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,YUNYUSHIIREKYOTUUZEIGAKU8   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,YUNYUSHIIRETIHO8   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,TOKUTEISHIIREHENKANKAZEIZEINUKI8   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,TOKUTEISHIIREHENKANHIKAZEIZEINUKI8   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,TOKUTEISHIIREHENKANKYOTUUZEINUKI8   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEINUKI5_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIGAKU5_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIKOMI5_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEINUKI5_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIGAKU5_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIKOMI5_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEINUKI5_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIGAKU5_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIKOMI5_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEINUKI5_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIGAKU5_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIKOMI5_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEINUKI5_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIGAKU5_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIKOMI5_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEINUKI5_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIGAKU5_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIKOMI5_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIGAKU5_INVOICEIGAI_TSUMIAGE_KANBEN   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIKOMI5_INVOICEIGAI_TSUMIAGE_KANBEN   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIGAKU5_INVOICEIGAI_TSUMIAGE_KANBEN   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIKOMI5_INVOICEIGAI_TSUMIAGE_KANBEN   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIGAKU5_INVOICEIGAI_TSUMIAGE_KANBEN   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIKOMI5_INVOICEIGAI_TSUMIAGE_KANBEN   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEINUKI5_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIGAKU5_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIKOMI5_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEINUKI5_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIGAKU5_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIKOMI5_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEINUKI5_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIGAKU5_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIKOMI5_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEINUKI5_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIGAKU5_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIKOMI5_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEINUKI5_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIGAKU5_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIKOMI5_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEINUKI5_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIGAKU5_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIKOMI5_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEINUKI3_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIGAKU3_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIKOMI3_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEINUKI3_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIGAKU3_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIKOMI3_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEINUKI3_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIGAKU3_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIKOMI3_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEINUKI3_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIGAKU3_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIKOMI3_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEINUKI3_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIGAKU3_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIKOMI3_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEINUKI3_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIGAKU3_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIKOMI3_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIGAKU3_INVOICEIGAI_TSUMIAGE_KANBEN   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIKOMI3_INVOICEIGAI_TSUMIAGE_KANBEN   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIGAKU3_INVOICEIGAI_TSUMIAGE_KANBEN   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIKOMI3_INVOICEIGAI_TSUMIAGE_KANBEN   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIGAKU3_INVOICEIGAI_TSUMIAGE_KANBEN   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIKOMI3_INVOICEIGAI_TSUMIAGE_KANBEN   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEINUKI3_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIGAKU3_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIKOMI3_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEINUKI3_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIGAKU3_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIKOMI3_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEINUKI3_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIGAKU3_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIKOMI3_INVOICEIGAI_WARIMODOSHI80   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEINUKI3_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIGAKU3_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKAZEIZEIKOMI3_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEINUKI3_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIGAKU3_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREHIKAZEIZEIKOMI3_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEINUKI3_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIGAKU3_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,SHIIREKYOTUUZEIKOMI3_INVOICEIGAI_WARIMODOSHI50   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREZEINUKI10_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREZEIGAKU10_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREZEIKOMI10_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREZEINUKI10_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREZEIGAKU10_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREZEIKOMI10_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREKAISHUZEINUKI10_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREKAISHUZEIGAKU10_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREKAISHUZEIKOMI10_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREKAISHUZEINUKI10_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREKAISHUZEIGAKU10_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREKAISHUZEIKOMI10_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREZEINUKI8K_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREZEIGAKU8K_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREZEIKOMI8K_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREZEINUKI8K_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREZEIGAKU8K_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREZEIKOMI8K_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREKAISHUZEINUKI8K_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREKAISHUZEIGAKU8K_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREKAISHUZEIKOMI8K_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREKAISHUZEINUKI8K_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREKAISHUZEIGAKU8K_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREKAISHUZEIKOMI8K_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREZEINUKI8_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREZEIGAKU8_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREZEIKOMI8_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREZEINUKI8_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREZEIGAKU8_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREZEIKOMI8_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREKAISHUZEINUKI8_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREKAISHUZEIGAKU8_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREKAISHUZEIKOMI8_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREKAISHUZEINUKI8_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREKAISHUZEIGAKU8_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREKAISHUZEIKOMI8_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREZEINUKI5_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREZEIGAKU5_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREZEIKOMI5_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREZEINUKI5_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREZEIGAKU5_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREZEIKOMI5_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREKAISHUZEINUKI5_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREKAISHUZEIGAKU5_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREKAISHUZEIKOMI5_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREKAISHUZEINUKI5_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREKAISHUZEIGAKU5_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREKAISHUZEIKOMI5_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREZEINUKI3_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREZEIGAKU3_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREZEIKOMI3_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREZEINUKI3_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREZEIGAKU3_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREZEIKOMI3_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREKAISHUZEINUKI3_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREKAISHUZEIGAKU3_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREKAISHUZEIKOMI3_TSUMIAGE   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREKAISHUZEINUKI3_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREKAISHUZEIGAKU3_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KASHIDAOREKAISHUZEIKOMI3_WARIMODOSHI   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,GIMUMENJOKAZEI10   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,GIMUMENJOHIKAZEI10   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,GIMUMENJOKYOTUU10   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEWARIAIHENDO10   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIGYOMUYOTENYO10   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KYOJUYOCHINTAI10   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,GIMUMENJOKAZEI8K   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,GIMUMENJOHIKAZEI8K   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,GIMUMENJOKYOTUU8K   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEWARIAIHENDO8K   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIGYOMUYOTENYO8K   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KYOJUYOCHINTAI8K   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,GIMUMENJOKAZEI8   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,GIMUMENJOHIKAZEI8   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,GIMUMENJOKYOTUU8   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEWARIAIHENDO8   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIGYOMUYOTENYO8   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KYOJUYOCHINTAI8   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,GIMUMENJOKAZEI5   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,GIMUMENJOHIKAZEI5   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,GIMUMENJOKYOTUU5   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEWARIAIHENDO5   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIGYOMUYOTENYO5   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KYOJUYOCHINTAI5   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,GIMUMENJOKAZEI3   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,GIMUMENJOHIKAZEI3   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,GIMUMENJOKYOTUU3   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIURIAGEWARIAIHENDO3   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KAZEIGYOMUYOTENYO3   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine("   ,KYOJUYOCHINTAI3   decimal(15, 0)                          NULL    ");
            strSQL.AppendLine(" ) ");
            #endregion

            businessDB.ExecuteNonQuery(strSQL.ToString());
            return;
        }
        #endregion
        //  2023/07/24  修正        Y.Nakajima       (221366)【消費税申告管理】R5年インボイス対応(消費税申告の達人連携対応分) 終了

        #region ワークシートデータ抽出処理
        /// <summary>
        /// データ抽出処理
        /// </summary>
        /// <param name="kikanST"></param>
        /// <param name="kikanED"></param>
        /// <param name="zigyoushoSitei"></param>
        /// <param name="shuturyokuKBN"></param>
        /// <param name="zigyoushoCD"></param>
        /// <returns></returns>
        private DataTable Data_Shuturyoku(string kikanST
                                         , string kikanED
                                         , int zigyoushoSitei
                                         , int shuturyokuKBN
            //  2023/07/24  修正        Y.Nakajima       (221366)【消費税申告管理】R5年インボイス対応(消費税申告の達人連携対応分) 開始
            //, string zigyoushoCD)
                                         , string zigyoushoCD
                                         , int uriageCalcKBN
                                         , int siireCalcKBN
                                         , int shoriDasshutuKBN)
        //  2023/07/24  修正        Y.Nakajima       (221366)【消費税申告管理】R5年インボイス対応(消費税申告の達人連携対応分) 終了
        {
            // 接続情報更新後処理を行う
            DataTable selectMainData = new DataTable();

            using (SqlCommand cm = businessDB.NewSqlCommand())
            {

                // プロシージャー呼び出し
                cm.CommandType = CommandType.StoredProcedure;
                cm.CommandText = DENSISINKOKU_SELECT;


                //パラメータ作成
                SqlParameter outparam1;
                outparam1 = cm.Parameters.Add("@po_ErrorKBN", SqlDbType.Int);
                outparam1.Direction = ParameterDirection.Output;

                SqlParameter outparam2;
                outparam2 = cm.Parameters.Add("@po_ErrorCD1", SqlDbType.VarChar, 128);
                outparam2.Direction = ParameterDirection.Output;

                SqlParameter outparam3;
                outparam3 = cm.Parameters.Add("@po_ErrorCD2", SqlDbType.VarChar, 128);
                outparam3.Direction = ParameterDirection.Output;

                SqlParameter outparam4;
                outparam4 = cm.Parameters.Add("@po_ErrorProcName", SqlDbType.VarChar, 128);
                outparam4.Direction = ParameterDirection.Output;

                SqlParameter outparam5;
                outparam5 = cm.Parameters.Add("@po_ErrorSummary", SqlDbType.VarChar, 256);
                outparam5.Direction = ParameterDirection.Output;

                SqlParameter outparam6;
                outparam6 = cm.Parameters.Add("@po_ErrorDetail", SqlDbType.VarChar, 2560);
                outparam6.Direction = ParameterDirection.Output;

                SqlParameter outparam7;
                outparam7 = cm.Parameters.Add("@po_ErrorBtnKBN", SqlDbType.Int);
                outparam7.Direction = ParameterDirection.Output;

                SqlParameter outparam8;
                outparam8 = cm.Parameters.Add("@po_ErrorBtnDefault", SqlDbType.Int);
                outparam8.Direction = ParameterDirection.Output;

                // 対象期間_開始
                SqlParameter param1;
                param1 = cm.Parameters.Add("@p_KikanBegin", SqlDbType.VarChar, 10);
                param1.Direction = ParameterDirection.Input;
                param1.Value = ConvertParamData(kikanST);

                // 対象期間_終了 
                SqlParameter param2;
                param2 = cm.Parameters.Add("@p_KikanEnd", SqlDbType.VarChar, 10);
                param2.Direction = ParameterDirection.Input;
                param2.Value = ConvertParamData(kikanED);

                // 事業所指定
                SqlParameter param3;
                param3 = cm.Parameters.Add("@p_ZigyoushoSitei", SqlDbType.Int);
                param3.Direction = ParameterDirection.Input;
                param3.Value = zigyoushoSitei;

                // 事業所コード
                SqlParameter param4;
                param4 = cm.Parameters.Add("@p_ZigyoushoCD", SqlDbType.VarChar, 6);
                param4.Direction = ParameterDirection.Input;
                param4.Value = ConvertParamData(zigyoushoCD);

                // 経理区分
                SqlParameter param5;
                param5 = cm.Parameters.Add("@p_KeiriHousiki", SqlDbType.Int);
                param5.Direction = ParameterDirection.Input;
                param5.Value = keiriHousikiKBN;

                // 会社正式名
                SqlParameter param6;
                param6 = cm.Parameters.Add("@p_CompanyName", SqlDbType.VarChar, 128);
                param6.Direction = ParameterDirection.Input;
                param6.Value = ComToolLogic.CurrentUser.CompanyName;

                // 売上消費税積上方式区分
                SqlParameter param7;
                param7 = cm.Parameters.Add("@p_UriageZeiTumiageKBN", SqlDbType.Int);
                param7.Direction = ParameterDirection.Input;
                //  2023/07/24  修正        Y.Nakajima       (221366)【消費税申告管理】R5年インボイス対応(消費税申告の達人連携対応分) 開始
                //param7.Value = uriageZeiTumiageKBN;
                param7.Value = uriageCalcKBN;
                //  2023/07/24  修正        Y.Nakajima       (221366)【消費税申告管理】R5年インボイス対応(消費税申告の達人連携対応分) 終了

                // 仕入消費税積上方式区分
                SqlParameter param8;
                param8 = cm.Parameters.Add("@p_SiireZeiTumiageKBN", SqlDbType.Int);
                param8.Direction = ParameterDirection.Input;
                //  2023/07/24  修正        Y.Nakajima       (221366)【消費税申告管理】R5年インボイス対応(消費税申告の達人連携対応分) 開始
                //param8.Value = siireZeiTumiageKBN;
                param8.Value = siireCalcKBN;
                //  2023/07/24  修正        Y.Nakajima       (221366)【消費税申告管理】R5年インボイス対応(消費税申告の達人連携対応分) 終了

                // 出力区分
                SqlParameter param9;
                param9 = cm.Parameters.Add("@p_ShuturyokuKBN", SqlDbType.Int);
                param9.Direction = ParameterDirection.Input;
                param9.Value = shuturyokuKBN;

                // オペレーションPC名
                SqlParameter param10;
                param10 = cm.Parameters.Add("@p_OperationComputerName", SqlDbType.VarChar, 128);
                param10.Direction = ParameterDirection.Input;
                param10.Value = ComToolLogic.CurrentUser.ComputerName;

                // オペレーションユーザID
                SqlParameter param11;
                param11 = cm.Parameters.Add("@p_OperationUserID", SqlDbType.VarChar, 128);
                param11.Direction = ParameterDirection.Input;
                param11.Value = ComToolLogic.CurrentUser.UserID;

                // オペレーションJobID
                SqlParameter param12;
                param12 = cm.Parameters.Add("@p_OperationJobID", SqlDbType.VarChar, 1211);
                param12.Direction = ParameterDirection.Input;
                param12.Value = ComToolLogic.CurrentUser.JobID;

                // 会社コード
                SqlParameter param13;
                param13 = cm.Parameters.Add("@p_CompanyNo", SqlDbType.Int);
                param13.Direction = ParameterDirection.Input;
                param13.Value = ComToolLogic.CurrentUser.CompanyNO;

                // 会社名
                SqlParameter param14;
                param14 = cm.Parameters.Add("@p_CompanyNameRyakue", SqlDbType.VarChar, 128);
                param14.Direction = ParameterDirection.Input;
                param14.Value = ComToolLogic.CurrentUser.CompanyNameRyaku;

                // 更新日時
                SqlParameter param15;
                param15 = cm.Parameters.Add("@p_UpdateDatetime", SqlDbType.DateTime);
                param15.Direction = ParameterDirection.Input;
                param15.Value = ((DateTime)businessDB.ExecuteScalar("SELECT GETDATE() AS DATETIME")).ToString("yyyy年MM月dd日 HH:mm");

                //  2023/07/24  修正        Y.Nakajima       (221366)【消費税申告管理】R5年インボイス対応(消費税申告の達人連携対応分) 開始
                // 事業年度制御区分
                SqlParameter param16;
                param16 = cm.Parameters.Add("@p_ZigyouNendoSeigyoKBN", SqlDbType.Int);
                param16.Direction = ParameterDirection.Input;
                param16.Value = zigyounendoSeigyoKBN;

                // 処理脱出フラグ
                SqlParameter param17;
                param17 = cm.Parameters.Add("@p_ShoriDasshutuFlg", SqlDbType.TinyInt);
                param17.Direction = ParameterDirection.Input;
                param17.Value = shoriDasshutuKBN;
                //  2023/07/24  修正        Y.Nakajima       (221366)【消費税申告管理】R5年インボイス対応(消費税申告の達人連携対応分) 終了

                SqlParameter returnParam;
                returnParam = cm.Parameters.Add("returnValue", SqlDbType.TinyInt);
                returnParam.Direction = ParameterDirection.ReturnValue;

                //SQL実行
                try
                {
                    using (SqlDataAdapter sa = new SqlDataAdapter(cm))
                    {
                        sa.Fill(selectMainData);
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
                    string[] objMsgDetail = new string[3];

                    //ストアド名
                    objMsgDetail[0] = ex.Procedure.ToString();
                    //エラーコード
                    objMsgDetail[1] = ex.Number.ToString();
                    //エラー内容
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        objMsgDetail[2] += ex.Errors[i].ToString();
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


                        //メッセージボックス表示
                        Obic7MessageBox.Show(SYSTEMCATEGORYCD, CUSTOMIZESEQ, MESSAGE_CD1, "00119", null, objMsgDetail, GetType(),
                                                             Obic7MessageBox.Obic7MessageBoxButtons.OK,
                                                             Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1);
                        return null;
                    }
                    else
                    {
                        //デッドロック以外のエラーはthrowして、共通クラスのエラー処理に委ねる。
                        throw ex;
                    }
                }

                //システムエラー発生時
                if (Com.A0000.Tool.Function.AnyToInt16(outparam1.Value.ToString()) == 1)
                {
                    //業務にて想定の範囲外のエラーが発生した場合は、共通部品を使用して明示的に例外をスローし、呼び出し元にてCatchする。
                    //(例外スローによる上位処理(共通部品側処理)終了時、当JOBは強制終了される。)
                    throw new ComTool.Obic7Exception("想定外のエラーが発生しました。【詳細】ストアド名 = " + ComTool.Function.AnyToString(outparam4.Value) + " エラー内容 = " + ComTool.Function.AnyToString(outparam5.Value));
                }

                //個別エラー発生時
                if (Com.A0000.Tool.Function.AnyToInt16(outparam1.Value.ToString()) == 2)
                //業務にて想定の範囲内のエラーが発生した場合は、ストアドからの戻り値を取得して、メッセージボックスを表示する。
                {
                    ProcError(outparam2, outparam3, outparam4, outparam5, outparam6, outparam7, outparam8);
                    return null;
                }

            }

            return selectMainData;
        }
        #endregion

        #region ストアド　引数セット
        /// <summary>
        /// ストアドプロシージャへの引数引渡しの際に使用
        /// 未入力項目は""ではなくDBNull.Valueをセットするようにする
        /// </summary>
        /// <param name="inputText"></param>
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
        #endregion
        #endregion
        // 2019/09/02  変更 M.Washizu (139334-139359)【電子申告対応】終了

        #endregion

        #region イベント

        #region フォーム上でのKeyDownイベント
        /// <summary>
        /// フォーム上でのKeyDownイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MA_ShouhizeiSinkokusho_KeyDown(object sender, KeyEventArgs e)
        {
            if ((!e.Alt) && (!e.Control) && (!e.Shift))
            {
                if (e.KeyCode == Keys.Enter)
                {
                    // オプションボタンでEnter時
                    if (PrevControl is Obic7RadioButton)
                    {
                        switch (PrevControl.Name)
                        {
                            // 全社
                            case "obic7RadioButtonZensha":
                                // 2018/05/11 T.Kimura 変更 (99108-99004)【消費税率10%対応】↓
                                if (zigyounendoSeigyoKBN == 0
                                || zigyounendoSeigyoKBN == 1
                                || zigyounendoSeigyoKBN == 2
                                || zigyounendoSeigyoKBN == 3)
                                {
                                    // 2018/05/11 T.Kimura 変更 (99108-99004)【消費税率10%対応】↑
                                    if (obic7RadioButtonKijunJouhou.Checked == true)
                                    {
                                        this.obic7RadioButtonKijunJouhou.Focus();
                                    }
                                    else if (obic7RadioButtonSinkokusho.Checked == true)
                                    {
                                        this.obic7RadioButtonSinkokusho.Focus();
                                    }
                                    // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 開始
                                    else if (obic7RadioButtonUtiwakesho.Checked == true)
                                    {
                                        this.obic7RadioButtonUtiwakesho.Focus();
                                    }
                                    // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 終了
                                    else if (obic7RadioButtonHuhyou1.Checked == true)
                                    {
                                        this.obic7RadioButtonHuhyou1.Focus();
                                    }
                                    else if (obic7RadioButtonHuhyou2.Checked == true)
                                    {
                                        this.obic7RadioButtonHuhyou2.Focus();
                                    }
                                    else if (obic7RadioButtonHuhyou3.Checked == true)
                                    {
                                        this.obic7RadioButtonHuhyou3.Focus();
                                    }
                                    // 2018/05/11 T.Kimura 変更 (99108-99004)【消費税率10%対応】↓
                                }
                                else
                                {
                                    if (obic7RadioButton_H31_KisoJouhou.Checked == true)
                                    {
                                        this.obic7RadioButton_H31_KisoJouhou.Focus();
                                    }
                                    else if (obic7RadioButton_H31_KisoJouhouKyuu.Checked == true)
                                    {
                                        this.obic7RadioButton_H31_KisoJouhouKyuu.Focus();
                                    }
                                    else if (obic7RadioButton_H31_Sinkokusho.Checked == true)
                                    {
                                        this.obic7RadioButton_H31_Sinkokusho.Focus();
                                    }
                                    else if (obic7RadioButton_H31_Utiwakesho.Checked == true)
                                    {
                                        this.obic7RadioButton_H31_Utiwakesho.Focus();
                                    }
                                    else if (obic7RadioButton_H31_Huhyou11.Checked == true)
                                    {
                                        this.obic7RadioButton_H31_Huhyou11.Focus();
                                    }
                                    else if (obic7RadioButton_H31_Huhyou12.Checked == true)
                                    {
                                        this.obic7RadioButton_H31_Huhyou12.Focus();
                                    }
                                    else if (obic7RadioButton_H31_Huhyou21.Checked == true)
                                    {
                                        this.obic7RadioButton_H31_Huhyou21.Focus();
                                    }
                                    else if (obic7RadioButton_H31_Huhyou22.Checked == true)
                                    {
                                        this.obic7RadioButton_H31_Huhyou22.Focus();
                                    }
                                    //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↓
                                    else if (obic7RadioButton_H31_Huhyou23.Checked == true)
                                    {
                                        this.obic7RadioButton_H31_Huhyou23.Focus();
                                    }
                                    else if (obic7RadioButton_H31_Huhyou13.Checked == true)
                                    {
                                        this.obic7RadioButton_H31_Huhyou13.Focus();
                                    }
                                    //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↑

                                }
                                // 2018/05/11 T.Kimura 変更 (99108-99004)【消費税率10%対応】↑
                                break;
                            // 事業所
                            case "obic7RadioButtonZigyousho":
                                this.obic7TextStringZigyoushoCD.Focus();
                                break;
                            // それ以外
                            default:
                                // 次項目にフォーカスを移動
                                base.SelectNextInputControl(true);
                                break;
                        }
                    }
                    else
                    {
                        // 次項目にフォーカスを移動
                        base.SelectNextInputControl(true);
                    }
                }
            }
        }
        #endregion

        #region フォームロード処理
        /// <summary>
        /// フォームロード時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MA_ShouhizeiSinkokusho_Load(object sender, EventArgs e)
        {
            //セキュリティ情報取得
            GetSecurity();

            //2012/01/25 修正 OBIC T.Shiraishi (M04737) 開始
            //事業年度制御区分を取得
            GetZigyounendoSeigyoKBN();
            //2012/01/25 修正 OBIC T.Shiraishi (M04737) 終了

            //入力項目初期化
            ClearInput();

            //初期情報取得
            GetDefaultValue();

            //初期値セット
            SetDefaultValue();
        }
        #endregion

        #region フォーム表示処理
        /// <summary>
        /// フォーム表示時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MA_ShouhizeiSinkokusho_Shown(object sender, EventArgs e)
        {
            //初期セットフォーカス
            obic7TextDatetimeKikanST.Focus();
        }
        #endregion

        #region フォームクローズ処理
        /// <summary>
        /// フォームクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MA_ShouhizeiSinkokusho_FormClosing(object sender, FormClosingEventArgs e)
        {
            //  2008/07/16  変更 G.Ukita 開始    前回入力情報保存 を、F12:閉じる→帳票出力用データ作成の前に変更
            ////xml定義
            //System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();
            //System.Xml.XmlNode xmlnode = xmldoc.CreateNode(System.Xml.XmlNodeType.Element, ComToolLogic.CurrentUser.JobID, "");

            ////コントロール数分xmlを作成
            //foreach (object obj in this.Controls)
            //{
            //    MakeXml(obj, xmldoc, xmlnode);
            //}

            ////xmlを閉じる
            //xmldoc.AppendChild(xmlnode);

            ////xmlを保存する
            //SaveXml(xmldoc);
            //  2008/07/16  変更 G.Ukita 終了
        }
        #endregion

        #region ボタンクリック時の処理

        #region 決算期ボタンクリック
        /// <summary>
        /// 決算期ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7ButtonWithLogicKessankiSerch_Click(object sender, EventArgs e)
        {
            // 決算期クラスのインスタンス作成
            using (Win.M0000.Tool.Search.MA_Kessanki kessankiSearch = new Win.M0000.Tool.Search.MA_Kessanki())
            {
                kessankiSearch.BasicDate = obic7TextDatetimeKijunBi.Value;
                if (kessankiSearch.ShowDialog() == DialogResult.OK)
                {
                    // 返り値をそれぞれの項目にセット
                    obic7TextDatetimeKikanST.Text = kessankiSearch.KishuDate.ToString("yyyy/MM/dd"); ;
                    obic7TextDatetimeKikanED.Text = kessankiSearch.KimatuDate.ToString("yyyy/MM/dd"); ;

                    // 事業所名再取得
                    GetZigyousho(obic7TextStringZigyoushoCD, obic7LabelStringZigyoushoName, false);

                    // フォーカスをセット
                    obic7TextDatetimeKikanST.Focus();
                }
                else
                {
                    PrevControl.Focus();
                }
            }
        }
        #endregion

        #region 累計ボタンクリック
        /// <summary>
        /// 累計ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7ButtonWithLogicRuikei_Click(object sender, EventArgs e)
        {
            //累計セット
            int kessanki;

            //月度期間マスタ取得
            using (DataTable dtGetudo = WinMToolSearchCom.M0000MS_MA_GetudoKikan.SelectByKijunbi((DateTime)obic7TextDatetimeKijunBi.Value, ""))
            {
                // 2013/10/01 修正 Y.Katsumata 開始↓ GX対応(M00005:帳票範囲指定に「前月セット」追加) 戻り対応
                //obic7TextDatetimeKikanED.Text = Obic7fx.Com.A0000.Tool.Function.AnyToString(dtGetudo.Rows[0]["GetudoShuuryouDate"]);
                //kessanki = Obic7fx.Com.A0000.Tool.Function.AnyToInt32(dtGetudo.Rows[0]["Kessanki"]);

                // データが取得有無
                if (ComTool.Function.IsValidDataTable(dtGetudo) == true)
                {
                    obic7TextDatetimeKikanED.Text = Obic7fx.Com.A0000.Tool.Function.AnyToString(dtGetudo.Rows[0]["GetudoShuuryouDate"]);
                    kessanki = Obic7fx.Com.A0000.Tool.Function.AnyToInt32(dtGetudo.Rows[0]["Kessanki"]);

                    //決算期マスタ取得
                    using (DataTable dtKessanki = WinMToolSearchCom.M0000MS_MA_Kessanki.SelectByKey(kessanki, ""))
                    {
                        // データが取得有無
                        if (ComTool.Function.IsValidDataTable(dtKessanki) == true)
                        {
                            obic7TextDatetimeKikanST.Text = ((DateTime)dtKessanki.Rows[0]["KishuDate"]).ToString("yyyy/MM/dd");
                        }
                    }
                }
                // 2013/10/01 修正 Y.Katsumata 終了↑ GX対応(M00005:帳票範囲指定に「前月セット」追加) 戻り対応
            }

            // 2013/10/01 修正 Y.Katsumata 開始↓ GX対応(M00005:帳票範囲指定に「前月セット」追加) 戻り対応
            //if (!String.IsNullOrEmpty(Obic7fx.Com.A0000.Tool.Function.AnyToString(kessanki)))
            //{
            //    //決算期マスタ取得
            //    using (DataTable dtKessanki = WinMToolSearchCom.M0000MS_MA_Kessanki.SelectByKey(kessanki, ""))
            //    {
            //        obic7TextDatetimeKikanST.Text = ((DateTime)dtKessanki.Rows[0]["KishuDate"]).ToString("yyyy/MM/dd");
            //    }
            //}
            // 2013/10/01 修正 Y.Katsumata 終了↑ GX対応(M00005:帳票範囲指定に「前月セット」追加) 戻り対応

            // 事業所名再取得
            GetZigyousho(obic7TextStringZigyoushoCD, obic7LabelStringZigyoushoName, false);

            obic7TextDatetimeKikanST.Focus();
        }
        #endregion

        // 2013/07/09 修正 Y.Katsumata 開始↓ GX対応(M00005:帳票範囲指定に「前月セット」追加)
        #region 前月ボタンクリック
        /// <summary>
        /// 前月ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7ButtonWithLogicZengetuSet_Click(object sender, EventArgs e)
        {
            //前月月度データを取得し、画面表示
            this.GetZengetuInfo();

            // 事業所名再取得
            GetZigyousho(obic7TextStringZigyoushoCD, obic7LabelStringZigyoushoName, false);

            // 作表上の期首にフォーカスをセット
            this.obic7TextDatetimeKikanST.Focus();
        }
        #endregion
        // 2013/07/09 修正 Y.Katsumata 終了↑ GX対応(M00005:帳票範囲指定に「前月セット」追加)

        #region 当月ボタンクリック
        /// <summary>
        /// 当月ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7ButtonWithLogicTougetu_Click(object sender, EventArgs e)
        {
            //当月セット
            //月度期間マスタ取得
            using (DataTable dtGetudo = WinMToolSearchCom.M0000MS_MA_GetudoKikan.SelectByKijunbi((DateTime)obic7TextDatetimeKijunBi.Value, ""))
            {
                // 2013/10/01 修正 Y.Katsumata 開始↓ GX対応(M00005:帳票範囲指定に「前月セット」追加) 戻り対応
                //obic7TextDatetimeKikanST.Text = ((DateTime)dtGetudo.Rows[0]["GetudoKaisiDate"]).ToString("yyyy/MM/dd");
                //obic7TextDatetimeKikanED.Text = ((DateTime)dtGetudo.Rows[0]["GetudoShuuryouDate"]).ToString("yyyy/MM/dd");

                // データが取得有無
                if (ComTool.Function.IsValidDataTable(dtGetudo) == true)
                {
                    obic7TextDatetimeKikanST.Text = ((DateTime)dtGetudo.Rows[0]["GetudoKaisiDate"]).ToString("yyyy/MM/dd");
                    obic7TextDatetimeKikanED.Text = ((DateTime)dtGetudo.Rows[0]["GetudoShuuryouDate"]).ToString("yyyy/MM/dd");
                }
                // 2013/10/01 修正 Y.Katsumata 終了↑ GX対応(M00005:帳票範囲指定に「前月セット」追加) 戻り対応
            }

            // 事業所名再取得
            GetZigyousho(obic7TextStringZigyoushoCD, obic7LabelStringZigyoushoName, false);

            obic7TextDatetimeKikanST.Focus();
        }
        #endregion

        #region 事業所検索ボタンクリック
        /// <summary>
        /// 事業所検索ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7ButtonWithLogicZigyoushoSerch_Click(object sender, EventArgs e)
        {
            //直前のコントロールにフォーカスセット
            obic7TextStringZigyoushoCD.Focus();

            //F4イベントを発生させる
            obic7ToolStripButtonWithLogicF4.PerformClick();
        }
        #endregion

        // 2019/09/02  変更 M.Washizu (139334-139359)【電子申告対応】開始
        #region ファイル検索ボタンクリック
        /// <summary>
        /// ファイル検索ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7ButtonWithLogicSearchOutPath_Click(object sender, EventArgs e)
        {
            //直前のコントロールにフォーカスセット
            obic7TextStringOutPath.Focus();

            //F4イベントを発生させる
            obic7ToolStripButtonWithLogicF4.PerformClick();
        }

        #endregion
        // 2019/09/02  変更 M.Washizu (139334-139359)【電子申告対応】終了

        #endregion

        #region ファンクションボタン使用可能設定
        /// <summary>
        /// 入力項目フォーカス取得時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputControl_Enter(object sender, EventArgs e)
        {
            switch (((Control)sender).Name)
            {
                case "obic7TextDatetimeKijunBi":
                case "obic7TextDatetimeKikanST":
                case "obic7TextDatetimeKikanED":
                // 2013/07/10 修正 Y.Katsumata 開始↓ GX対応(M00005:帳票範囲指定に「前月セット」追加)
                case "obic7ButtonWithLogicZengetuSet":
                // 2013/07/10 修正 Y.Katsumata 終了↑ GX対応(M00005:帳票範囲指定に「前月セット」追加)
                case "obic7ButtonWithLogicTougetu":
                case "obic7ButtonWithLogicRuikei":
                case "obic7ButtonWithLogicKessankiSerch":
                case "obic7RadioButtonZensha":
                case "obic7RadioButtonZigyousho":
                case "obic7RadioButtonKijunJouhou":
                case "obic7RadioButtonSinkokusho":
                // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 開始
                case "obic7RadioButtonUtiwakesho":
                // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 終了
                case "obic7RadioButtonHuhyou1":
                case "obic7RadioButtonHuhyou2":
                case "obic7RadioButtonHuhyou3":
                // 2018/05/11 T.Kimura 変更 (99108-99004)【消費税率10%対応】↓
                case "obic7RadioButton_H31_KisoJouhou":
                case "obic7RadioButton_H31_KisoJouhouKyuu":
                case "obic7RadioButton_H31_Sinkokusho":
                case "obic7RadioButton_H31_Utiwakesho":
                case "obic7RadioButton_H31_Huhyou11":
                case "obic7RadioButton_H31_Huhyou12":
                case "obic7RadioButton_H31_Huhyou21":
                case "obic7RadioButton_H31_Huhyou22":
                //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↓
                case "obic7RadioButton_H31_Huhyou23":
                case "obic7RadioButton_H31_Huhyou13":
                    //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↑
                    // 2018/05/11 T.Kimura 変更 (99108-99004)【消費税率10%対応】↑
                    base.SetFunctionButton(
                          false      // F1
                        , true       // F2
                        , false      // F3
                        , false      // F4
                        , true       // F5
                        , true       // F6
                        , true       // F7
                        , false      // F8
                        // 2019/09/02  変更 M.Washizu (139334-139359)【電子申告対応】開始

                        //, false       // F9
                        , true       // F9
                        //, false      // F10
                        , true      // F10
                        // 2019/09/02  変更 M.Washizu (139334-139359)【電子申告対応】終了
                        , false      // F11
                        , true       // F12
                        );
                    break;

                case "obic7TextStringZigyoushoCD":
                    base.SetFunctionButton(
                          false      // F1
                        , true       // F2
                        , false      // F3
                        , true       // F4
                        , true       // F5
                        , true       // F6
                        , true       // F7
                        , false      // F8
                        // 2019/09/02  変更 M.Washizu (139334-139359)【電子申告対応】開始
                        //, false       // F9
                        , true       // F9
                        //, false      // F10
                        , true      // F10
                        // 2019/09/02  変更 M.Washizu (139334-139359)【電子申告対応】終了
                        , false      // F11
                        , true       // F12
                        );
                    break;
                // 2019/09/02  変更 M.Washizu (139334-139359)【電子申告対応】開始
                case "obic7TextStringOutPath":
                    base.SetFunctionButton(
                          false      // F1
                        , true       // F2
                        , false      // F3
                        , true       // F4
                        , true       // F5
                        , true       // F6
                        , true       // F7
                        , false      // F8
                        , true       // F9
                        , true      // F10
                        , false      // F11
                        , true       // F12
                        );
                    break;
                // 2019/09/02  変更 M.Washizu (139334-139359)【電子申告対応】終了

                default:
                    break;
            }
        }
        #endregion

        #region 基準日検証時
        /// <summary>
        /// 基準日検証時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7TextDatetimeKijunBi_Validating(object sender, CancelEventArgs e)
        {
            if (obic7TextDatetimeKijunBi.IsChangedText)
            {
                //必須チェック
                if (String.IsNullOrEmpty(obic7TextDatetimeKijunBi.Text))
                {
                    //エラープロバイダを表示
                    DispErrorProvider(obic7TextDatetimeKijunBi, "00104", null);

                    e.Cancel = true;
                    return;
                }
            }
        }
        #endregion

        #region 期間_開始検証時
        /// <summary>
        /// 期間_開始検証時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7TextDatetimeKikanST_Validating(object sender, CancelEventArgs e)
        {
            if (obic7TextDatetimeKikanST.IsChangedText)
            {
                //必須チェック
                if (String.IsNullOrEmpty(obic7TextDatetimeKikanST.Text))
                {
                    //エラープロバイダを表示
                    DispErrorProvider(obic7TextDatetimeKikanST, "00104", null);

                    e.Cancel = true;
                    return;
                }
            }
        }
        #endregion

        #region 期間_終了検証時
        /// <summary>
        /// 期間_終了検証時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7TextDatetimeKikanED_Validating(object sender, CancelEventArgs e)
        {
            if (obic7TextDatetimeKikanED.IsChangedText)
            {
                //必須チェック
                if (!String.IsNullOrEmpty(obic7TextDatetimeKikanED.Text))
                {
                    //事業所略名取得
                    GetZigyousho(obic7TextStringZigyoushoCD, obic7LabelStringZigyoushoName, false);
                }
                else
                {
                    //エラープロバイダを表示
                    DispErrorProvider(obic7TextDatetimeKikanED, "00104", null);

                    e.Cancel = true;
                    return;
                }
            }
        }
        #endregion

        #region 事業所CD検証時
        /// <summary>
        /// 事業所CD検証時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7TextStringZigyoushoCD_Validating(object sender, CancelEventArgs e)
        {
            //値に変更があった場合のみ検証を行う
            if (obic7TextStringZigyoushoCD.IsChangedText == true)
            {
                if (!String.IsNullOrEmpty(obic7TextStringZigyoushoCD.Text))
                {
                    //事業所名取得
                    if (!GetZigyousho(obic7TextStringZigyoushoCD, obic7LabelStringZigyoushoName, true))
                    {
                        obic7LabelStringZigyoushoName.Text = "";

                        e.Cancel = true;
                    }

                    //チェックON
                    obic7RadioButtonZigyousho.Checked = true;

                    //チェックOFF
                    obic7RadioButtonZensha.Checked = false;

                }
                // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 開始
                //else if ((zigSecurityJoutaiKbn != 0) && (String.IsNullOrEmpty(obic7TextStringZigyoushoCD.Text) == true))
                else if (((zigSecurityJoutaiKbn != 0) || (kaishaUnyouKeitaiKBN == 1)) && (String.IsNullOrEmpty(obic7TextStringZigyoushoCD.Text) == true))
                // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 終了
                {
                    //エラープロバイダを表示
                    DispErrorProvider(obic7TextStringZigyoushoCD, "00104", null);

                    obic7LabelStringZigyoushoName.Text = "";

                    e.Cancel = true;
                }
                else
                {
                    obic7LabelStringZigyoushoName.Text = "";

                    //セキュリティ状態区分がALL以外の場合は事業所全社選択不可
                    // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 開始
                    //if (zigSecurityJoutaiKbn == 0)
                    if (zigSecurityJoutaiKbn == 0 && kaishaUnyouKeitaiKBN != 1)
                    // 2015/07/02 R.Aoki 変更 (25192-25347-25385)リバースチャージ対応 終了
                    {
                        obic7RadioButtonZensha.Checked = true;
                    }

                }
            }
        }
        #endregion


        #endregion
    }
}