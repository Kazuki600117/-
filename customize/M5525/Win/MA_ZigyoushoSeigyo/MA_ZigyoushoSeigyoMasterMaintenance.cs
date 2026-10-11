//
//    2026/10/11    新規作成    (5525)事業所別消費税計算方式対応
//                        ・事業所別制御マスタメンテ
//                          事業所ごとに消費税計算方式(売上・仕入 × R5.9.30以前・R5.10.1以後)と取引先コードを
//                          事業所別制御マスタ(M5525MS_MA_ZigyoushoSeigyo)に登録する
//                        ・消費税計算方式が未入力の区分は、財務コントロールマスタの値に従う
//                        ・画面構成・操作は資産事業所マスタメンテ(N0000)に合わせる
//                        ・取引先コードは取引先マスタ(P0000MS_Torihikisaki)の存在チェック(基準日時点の改定)と検索(F4)を行う
//

using System;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using WinToolLogicUI = Obic7fx.Win.A0000.Tool.Logic.UI;
using WinToolUI = Obic7fx.Win.A0000.Tool.UI;
using WinToolLogic = Obic7fx.Win.A0000.Tool.Logic;
using ComTool = Obic7fx.Com.A0000.Tool;
using ComToolLogic = Obic7fx.Com.A0000.Tool.Logic;
using M0000Search = Obic7fx.Win.M0000.Tool.Search;
using M0000SearchCom = Obic7fx.Win.M0000.Tool.SearchCom;
using P0000Search = Obic7fx.Win.P0000.Tool.Search;

namespace Obic7fx.Win.M5525.MasterM.MA_ZigyoushoSeigyo
{
    /// 事業所別制御マスタメンテ
    /// <summary>
    /// 事業所別制御マスタメンテ
    /// </summary>
    public partial class MA_ZigyoushoSeigyoMasterMaintenance : Obic7fx.Win.M0000.Tool.UI.Obic7MBussinessFormBase
    {
        #region 定数

        //画面制御用
        //新規追加時
        private const string MODE_INSERT = "新規";
        //修正入力時
        private const string MODE_UPDATE = "修正";

        //メッセージCD1
        private const string MESSAGECD1 = "000";
        //システム分類
        private const string SYS_BUNRUI = "M";
        //カスタマイズSEQ(DBアクセス・メッセージは標準と同じ 0000 を使用する)
        private const string CUSTOM_SEQ = "0000";

        //消費税計算方式区分
        //割戻方式
        private const int CALC_KBN_WARIMODOSI = 0;
        //積上方式
        private const int CALC_KBN_TUMIAGE = 1;

        #endregion

        #region 変数

        //DB接続用クラス（ステートフル）
        private WinToolLogic.DBAccess businessDB;
        //更新タイムスタンプ格納用
        private byte[] timestamp;
        //F5が押下されたかどうかの判定用
        private bool isF5Check = false;

        //財務コントロールマスタの消費税計算方式区分(未入力時の表示用)
        //R5.9.30以前売上消費税計算方式区分
        private int controlUriageZeiTumiageKBN;
        //R5.9.30以前仕入消費税計算方式区分
        private int controlSiireZeiTumiageKBN;
        //R5.10.1以後売上消費税計算方式区分
        private int controlR5NenOctIgoUriageZeiTumiageKBN;
        //R5.10.1以後仕入消費税計算方式区分
        private int controlR5NenOctIgoSiireZeiTumiageKBN;

        #endregion

        #region メソッド

        /// 引数ありコンストラクタ
        /// <summary>
        /// 引数ありコンストラクタ
        /// </summary>
        public MA_ZigyoushoSeigyoMasterMaintenance(string jobID)
            : base(jobID)
        {
            InitializeComponent();
            //メインフォームフラグ
            base.isMainForm = true;
            //項目初期化
            InitializeForm();
        }

        /// 引数なしコンストラクタ
        /// <summary>
        /// 引数なしコンストラクタ
        /// </summary>
        public MA_ZigyoushoSeigyoMasterMaintenance()
            : base()
        {
            InitializeComponent();
        }

        /// SQL文生成（SELECT用）
        /// <summary>
        /// SQL文生成（SELECT用）
        /// </summary>
        /// <returns>生成したSQL文（SELECT用）を返却</returns>
        private string MakeSQLSelectMaster()
        {
            StringBuilder sqlSelect = new StringBuilder();
            sqlSelect.AppendLine(" SELECT ");
            sqlSelect.AppendLine("        UriageZeiTumiageKBN ");
            sqlSelect.AppendLine("       ,SiireZeiTumiageKBN ");
            sqlSelect.AppendLine("       ,R5NenOctIgoUriageZeiTumiageKBN ");
            sqlSelect.AppendLine("       ,R5NenOctIgoSiireZeiTumiageKBN ");
            sqlSelect.AppendLine("       ,TorihikisakiCD ");
            sqlSelect.AppendLine("       ,UpdateTimestamp ");
            sqlSelect.AppendLine(" FROM   M5525MS_MA_ZigyoushoSeigyo ");
            sqlSelect.AppendLine(" WHERE  ZigyoushoCD = " + ComTool.Function.SqlEscapeVarchar(this.obic7TextStringZigyoushoCD.Value) + " ");
            return sqlSelect.ToString();
        }

        /// SQL文生成（DELETE用）
        /// <summary>
        /// SQL文生成（DELETE用）
        /// </summary>
        /// <returns>生成したSQL文（DELETE用）を返却</returns>
        private string MakeSQLDeleteMaster()
        {
            StringBuilder sqlDelete = new StringBuilder();
            sqlDelete.AppendLine(" DELETE FROM M5525MS_MA_ZigyoushoSeigyo ");
            sqlDelete.AppendLine(" WHERE  ZigyoushoCD = " + ComTool.Function.SqlEscapeVarchar(this.obic7TextStringZigyoushoCD.Value) + " ");
            sqlDelete.AppendLine("   AND  UpdateTimestamp = " + ComTool.Function.SqlEscapeBinary(this.timestamp) + " ");
            return sqlDelete.ToString();
        }

        /// SQL文生成（INSERT用）
        /// <summary>
        /// SQL文生成（INSERT用）
        /// </summary>
        /// <returns>生成したSQL文（INSERT用）を返却</returns>
        private string MakeSQLInsertMaster()
        {
            StringBuilder sqlInsert = new StringBuilder();
            sqlInsert.AppendLine(" INSERT INTO M5525MS_MA_ZigyoushoSeigyo ( ");
            sqlInsert.AppendLine("        ZigyoushoCD ");
            sqlInsert.AppendLine("       ,UriageZeiTumiageKBN ");
            sqlInsert.AppendLine("       ,SiireZeiTumiageKBN ");
            sqlInsert.AppendLine("       ,R5NenOctIgoUriageZeiTumiageKBN ");
            sqlInsert.AppendLine("       ,R5NenOctIgoSiireZeiTumiageKBN ");
            sqlInsert.AppendLine("       ,TorihikisakiCD ");
            sqlInsert.AppendLine("       ,CompanyNO ");
            sqlInsert.AppendLine("       ,UpdateDatetime ");
            sqlInsert.AppendLine("       ,UpdateCertificatedGuid ");
            sqlInsert.AppendLine("       ,UpdateJobID ");
            sqlInsert.AppendLine("       ,UpdateUserID ");
            sqlInsert.AppendLine("       ,UpdateComputerName ");
            sqlInsert.AppendLine("       ,UpdateComputerUserName ");
            sqlInsert.AppendLine("       ,UpdateComputerDomainName ");
            sqlInsert.AppendLine("        ) ");
            sqlInsert.AppendLine("        SELECT ");
            sqlInsert.AppendLine(ComTool.Function.SqlEscapeVarchar(this.obic7TextStringZigyoushoCD.Value));
            //R5.9.30以前売上消費税計算方式区分
            sqlInsert.AppendLine(" , " + ToSqlNumberOrNull(this.obic7TextStringUriageZeiTumiageKBN.Value));
            //R5.9.30以前仕入消費税計算方式区分
            sqlInsert.AppendLine(" , " + ToSqlNumberOrNull(this.obic7TextStringSiireZeiTumiageKBN.Value));
            //R5.10.1以後売上消費税計算方式区分
            sqlInsert.AppendLine(" , " + ToSqlNumberOrNull(this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN.Value));
            //R5.10.1以後仕入消費税計算方式区分
            sqlInsert.AppendLine(" , " + ToSqlNumberOrNull(this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN.Value));
            //取引先コード
            sqlInsert.AppendLine(" , " + ToSqlVarcharOrNull(this.obic7TextStringTorihikisakiCD.Value));
            //会社No
            sqlInsert.AppendLine(" , " + ComTool.Function.SqlEscapeNumber(ComToolLogic.CurrentUser.CompanyNO));
            //更新日時
            sqlInsert.AppendLine(" , GETDATE() ");
            //更新認証済Guid
            sqlInsert.AppendLine(" , " + ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.CertificatedGuid));
            //更新JobID
            sqlInsert.AppendLine(" , " + ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.JobID));
            //更新ユーザID
            sqlInsert.AppendLine(" , " + ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.UserID));
            //更新PC名
            sqlInsert.AppendLine(" , " + ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.ComputerName));
            //更新PCユーザ名
            sqlInsert.AppendLine(" , " + ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.ComputerUserName));
            //更新PCドメイン名
            sqlInsert.AppendLine(" , " + ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.ComputerDomainName));
            sqlInsert.AppendLine("        WHERE NOT EXISTS ( ");
            sqlInsert.AppendLine("                SELECT ZigyoushoCD ");
            sqlInsert.AppendLine("                FROM   M5525MS_MA_ZigyoushoSeigyo ");
            sqlInsert.AppendLine("                WHERE  ZigyoushoCD = " + ComTool.Function.SqlEscapeVarchar(this.obic7TextStringZigyoushoCD.Value));
            sqlInsert.AppendLine(" ) ");
            return sqlInsert.ToString();
        }

        /// SQL文生成（UPDATE用）
        /// <summary>
        /// SQL文生成（UPDATE用）
        /// </summary>
        /// <returns>生成したSQL文（UPDATE用）を返却</returns>
        private string MakeSQLUpdateMaster()
        {
            StringBuilder sqlUpdate = new StringBuilder();
            sqlUpdate.AppendLine(" UPDATE M5525MS_MA_ZigyoushoSeigyo ");
            sqlUpdate.AppendLine(" SET    UriageZeiTumiageKBN = " + ToSqlNumberOrNull(this.obic7TextStringUriageZeiTumiageKBN.Value));
            sqlUpdate.AppendLine("       ,SiireZeiTumiageKBN = " + ToSqlNumberOrNull(this.obic7TextStringSiireZeiTumiageKBN.Value));
            sqlUpdate.AppendLine("       ,R5NenOctIgoUriageZeiTumiageKBN = " + ToSqlNumberOrNull(this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN.Value));
            sqlUpdate.AppendLine("       ,R5NenOctIgoSiireZeiTumiageKBN = " + ToSqlNumberOrNull(this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN.Value));
            sqlUpdate.AppendLine("       ,TorihikisakiCD = " + ToSqlVarcharOrNull(this.obic7TextStringTorihikisakiCD.Value));
            sqlUpdate.AppendLine("       ,CompanyNO = " + ComTool.Function.SqlEscapeNumber(ComToolLogic.CurrentUser.CompanyNO));
            sqlUpdate.AppendLine("       ,UpdateDatetime = GETDATE() ");
            sqlUpdate.AppendLine("       ,UpdateCertificatedGuid = " + ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.CertificatedGuid));
            sqlUpdate.AppendLine("       ,UpdateJobID = " + ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.JobID));
            sqlUpdate.AppendLine("       ,UpdateUserID = " + ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.UserID));
            sqlUpdate.AppendLine("       ,UpdateComputerName = " + ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.ComputerName));
            sqlUpdate.AppendLine("       ,UpdateComputerUserName = " + ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.ComputerUserName));
            sqlUpdate.AppendLine("       ,UpdateComputerDomainName = " + ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.ComputerDomainName));
            sqlUpdate.AppendLine(" WHERE  ZigyoushoCD = " + ComTool.Function.SqlEscapeVarchar(this.obic7TextStringZigyoushoCD.Value));
            sqlUpdate.AppendLine("   AND  UpdateTimestamp = " + ComTool.Function.SqlEscapeBinary(this.timestamp));
            return sqlUpdate.ToString();
        }

        /// SQL文生成（財務コントロールマスタ SELECT用）
        /// <summary>
        /// SQL文生成（財務コントロールマスタ SELECT用）
        /// </summary>
        /// <returns>生成したSQL文</returns>
        private string MakeSQLSelectControl()
        {
            StringBuilder sqlSelect = new StringBuilder();
            sqlSelect.AppendLine(" SELECT ");
            sqlSelect.AppendLine("        KijunDate ");
            sqlSelect.AppendLine("       ,UriageZeiTumiageKBN ");
            sqlSelect.AppendLine("       ,SiireZeiTumiageKBN ");
            sqlSelect.AppendLine("       ,R5NenOctIgoUriageZeiTumiageKBN ");
            sqlSelect.AppendLine("       ,R5NenOctIgoSiireZeiTumiageKBN ");
            sqlSelect.AppendLine(" FROM   M0000MS_MA_Control ");
            sqlSelect.AppendLine(" WHERE  ControlKey = 1 ");
            return sqlSelect.ToString();
        }

        /// SQL文生成（取引先マスタ存在チェック用）
        /// <summary>
        /// SQL文生成（取引先マスタ存在チェック用）
        /// 基準日以前の改定が存在する取引先を有効とする
        /// </summary>
        /// <returns>生成したSQL文</returns>
        private string MakeSQLExistsTorihikisaki()
        {
            StringBuilder sqlSelect = new StringBuilder();
            sqlSelect.AppendLine(" SELECT COUNT(*) ");
            sqlSelect.AppendLine(" FROM   P0000MS_Torihikisaki ");
            sqlSelect.AppendLine(" WHERE  TorihikisakiCD = " + ComTool.Function.SqlEscapeVarchar(this.obic7TextStringTorihikisakiCD.Value) + " ");
            sqlSelect.AppendLine("   AND  TorihikisakiKaiteiDate <= " + ComTool.Function.SqlEscapeDatetime(this.obic7TextDatetimeBaseDate.Value) + " ");
            return sqlSelect.ToString();
        }

        /// 数値をSQLの値に変換（未入力はNULL）
        /// <summary>
        /// 数値をSQLの値に変換（未入力はNULL）
        /// </summary>
        /// <param name="value">入力値</param>
        /// <returns>SQLの値</returns>
        private string ToSqlNumberOrNull(string value)
        {
            int number;
            if (String.IsNullOrEmpty(value) || !int.TryParse(value, out number))
            {
                return "NULL";
            }
            return ComTool.Function.SqlEscapeNumber(number);
        }

        /// 文字列をSQLの値に変換（未入力はNULL）
        /// <summary>
        /// 文字列をSQLの値に変換（未入力はNULL）
        /// </summary>
        /// <param name="value">入力値</param>
        /// <returns>SQLの値</returns>
        private string ToSqlVarcharOrNull(string value)
        {
            return String.IsNullOrEmpty(value) ? "NULL" : ComTool.Function.SqlEscapeVarchar(value);
        }

        /// フォーム項目初期化
        /// <summary>
        /// フォーム項目初期化
        /// </summary>
        private void InitializeForm()
        {
            //ファンクションボタンの初期化
            //F1
            base.obic7ToolStripButtonWithLogicF1.Text = F1.BLANK;
            base.obic7ToolStripButtonWithLogicF1.Obic7JobSecurity = ComTool.Obic7JobSecurities.NoSecurity;
            //F2
            base.obic7ToolStripButtonWithLogicF2.Text = F2.BLANK;
            base.obic7ToolStripButtonWithLogicF2.Obic7JobSecurity = ComTool.Obic7JobSecurities.NoSecurity;
            //F3 削除
            base.obic7ToolStripButtonWithLogicF3.Text = F3.DELETE_DATA;
            base.obic7ToolStripButtonWithLogicF3.Obic7JobSecurity = ComTool.Obic7JobSecurities.NoSecurity;
            base.obic7ToolStripButtonWithLogicF3.Click += new EventHandler(obic7ToolStripButtonWithLogicF3_Click);
            //F4 検索
            base.obic7ToolStripButtonWithLogicF4.Text = F4.SEARCH;
            base.obic7ToolStripButtonWithLogicF4.Obic7JobSecurity = ComTool.Obic7JobSecurities.NoSecurity;
            base.obic7ToolStripButtonWithLogicF4.Click += new EventHandler(obic7ToolStripButtonWithLogicF4_Click);
            //F5 取消
            base.obic7ToolStripButtonWithLogicF5.Text = F5.CANCEL;
            base.obic7ToolStripButtonWithLogicF5.Obic7JobSecurity = ComTool.Obic7JobSecurities.NoSecurity;
            base.obic7ToolStripButtonWithLogicF5.Click += new EventHandler(obic7ToolStripButtonWithLogicF5_Click);
            //F6
            base.obic7ToolStripButtonWithLogicF6.Text = F6.BLANK;
            base.obic7ToolStripButtonWithLogicF6.Obic7JobSecurity = ComTool.Obic7JobSecurities.NoSecurity;
            //F7
            base.obic7ToolStripButtonWithLogicF7.Text = F7.BLANK;
            base.obic7ToolStripButtonWithLogicF7.Obic7JobSecurity = ComTool.Obic7JobSecurities.NoSecurity;
            //F8
            base.obic7ToolStripButtonWithLogicF8.Text = F8.BLANK;
            base.obic7ToolStripButtonWithLogicF8.Obic7JobSecurity = ComTool.Obic7JobSecurities.NoSecurity;
            //F9
            base.obic7ToolStripButtonWithLogicF9.Text = F9.BLANK;
            base.obic7ToolStripButtonWithLogicF9.Obic7JobSecurity = ComTool.Obic7JobSecurities.NoSecurity;
            //F10 登録
            base.obic7ToolStripButtonWithLogicF10.Text = F10.UPDATE;
            base.obic7ToolStripButtonWithLogicF10.Obic7JobSecurity = ComTool.Obic7JobSecurities.NoSecurity;
            base.obic7ToolStripButtonWithLogicF10.Click += new EventHandler(obic7ToolStripButtonWithLogicF10_Click);
            //F11
            base.obic7ToolStripButtonWithLogicF11.Text = F11.BLANK;
            base.obic7ToolStripButtonWithLogicF11.Obic7JobSecurity = ComTool.Obic7JobSecurities.NoSecurity;
            //F12 閉じる
            base.obic7ToolStripButtonWithLogicF12.Text = F12.CLOSE;
            base.obic7ToolStripButtonWithLogicF12.Obic7JobSecurity = ComTool.Obic7JobSecurities.NoSecurity;
        }

        /// ヘッダの表示切替
        /// <summary>
        /// ヘッダの表示切替
        /// </summary>
        /// <param name="isEnabled">Enabledプロパティの設定値</param>
        /// <param name="isNeedClear">テキストボックスのクリアの要・不要</param>
        /// <param name="isNeedFormatMode">処理モード設定値</param>
        private void SwitchHeader(bool isEnabled, bool isNeedClear, string isNeedFormatMode)
        {
            //基準日
            this.obic7TextDatetimeBaseDate.Enabled = isEnabled;
            //事業所コード
            this.obic7TextStringZigyoushoCD.Enabled = isEnabled;
            //事業所コード_検索ボタン
            this.obic7ButtonWithLogicZigyoushoCDSearch.Enabled = isEnabled;
            //表示ボタン
            this.obic7ButtonWithLogicDisp.Enabled = isEnabled;

            //処理モードの設定値
            if (this.obic7LabelStringMode.Value != isNeedFormatMode)
            {
                this.obic7LabelStringMode.Value = isNeedFormatMode;
            }

            //必要に応じクリア
            if (isNeedClear == true)
            {
                this.obic7TextDatetimeBaseDate.Value = null;
                this.obic7TextStringZigyoushoCD.Value = "";
                this.obic7LabelStringZigyoushoName.Value = "";
            }
        }

        /// 明細項目表示切替
        /// <summary>
        /// 明細項目表示切替
        /// </summary>
        /// <param name="isEnabled">Enabledプロパティの設定値</param>
        /// <param name="isNeedClear">テキストボックスのクリアの要・不要</param>
        private void SwitchDetail(bool isEnabled, bool isNeedClear)
        {
            //消費税計算方式
            this.obic7TextStringUriageZeiTumiageKBN.Enabled = isEnabled;
            this.obic7TextStringSiireZeiTumiageKBN.Enabled = isEnabled;
            this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN.Enabled = isEnabled;
            this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN.Enabled = isEnabled;
            //取引先
            this.obic7TextStringTorihikisakiCD.Enabled = isEnabled;
            this.obic7ButtonWithLogicTorihikisakiCDSearch.Enabled = isEnabled;

            if (isNeedClear)
            {
                this.obic7TextStringUriageZeiTumiageKBN.Value = "";
                this.obic7TextStringSiireZeiTumiageKBN.Value = "";
                this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN.Value = "";
                this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN.Value = "";
                this.obic7TextStringTorihikisakiCD.Value = "";
                this.obic7LabelStringUriageZeiTumiageKBNName.Value = "";
                this.obic7LabelStringSiireZeiTumiageKBNName.Value = "";
                this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName.Value = "";
                this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName.Value = "";
                //タイムスタンプ
                this.timestamp = null;
            }
        }

        /// 取得したマスタデータの表示
        /// <summary>
        /// 取得したマスタデータの表示
        /// </summary>
        /// <param name="masterData">表示対象のDataTable</param>
        private void SetDetailData(DataTable masterData)
        {
            DataRow row = masterData.Rows[0];
            this.obic7TextStringUriageZeiTumiageKBN.Value = ComTool.Function.AnyToString(row["UriageZeiTumiageKBN"]);
            this.obic7TextStringSiireZeiTumiageKBN.Value = ComTool.Function.AnyToString(row["SiireZeiTumiageKBN"]);
            this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN.Value = ComTool.Function.AnyToString(row["R5NenOctIgoUriageZeiTumiageKBN"]);
            this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN.Value = ComTool.Function.AnyToString(row["R5NenOctIgoSiireZeiTumiageKBN"]);
            this.obic7TextStringTorihikisakiCD.Value = ComTool.Function.AnyToString(row["TorihikisakiCD"]);
            this.timestamp = (byte[])row["UpdateTimestamp"];
        }

        /// 消費税計算方式名称の表示
        /// <summary>
        /// 消費税計算方式名称の表示(未入力の場合は財務コントロールマスタの値を表示)
        /// </summary>
        private void SetCalcKBNNames()
        {
            this.obic7LabelStringUriageZeiTumiageKBNName.Value = GetCalcKBNDispName(this.obic7TextStringUriageZeiTumiageKBN.Value, this.controlUriageZeiTumiageKBN);
            this.obic7LabelStringSiireZeiTumiageKBNName.Value = GetCalcKBNDispName(this.obic7TextStringSiireZeiTumiageKBN.Value, this.controlSiireZeiTumiageKBN);
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName.Value = GetCalcKBNDispName(this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN.Value, this.controlR5NenOctIgoUriageZeiTumiageKBN);
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName.Value = GetCalcKBNDispName(this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN.Value, this.controlR5NenOctIgoSiireZeiTumiageKBN);
        }

        /// 消費税計算方式の名称を取得
        /// <summary>
        /// 消費税計算方式の名称を取得
        /// </summary>
        /// <param name="calcKBN">消費税計算方式区分</param>
        /// <returns>名称(区分外の場合はnull)</returns>
        private string GetCalcKBNName(int calcKBN)
        {
            switch (calcKBN)
            {
                case CALC_KBN_WARIMODOSI:
                    return "割戻方式";
                case CALC_KBN_TUMIAGE:
                    return "積上方式";
                default:
                    return null;
            }
        }

        /// 消費税計算方式の表示名称を取得
        /// <summary>
        /// 消費税計算方式の表示名称を取得
        /// </summary>
        /// <param name="inputValue">入力値</param>
        /// <param name="controlCalcKBN">財務コントロールマスタの区分</param>
        /// <returns>表示名称</returns>
        private string GetCalcKBNDispName(string inputValue, int controlCalcKBN)
        {
            int calcKBN;
            if (String.IsNullOrEmpty(inputValue))
            {
                return "未設定（財務コントロール：" + GetCalcKBNName(controlCalcKBN) + "）";
            }
            if (int.TryParse(inputValue, out calcKBN))
            {
                string name = GetCalcKBNName(calcKBN);
                if (name != null)
                {
                    return name;
                }
            }
            return "";
        }

        /// 財務コントロールマスタの読込
        /// <summary>
        /// 財務コントロールマスタの読込(基準日・消費税計算方式区分)
        /// </summary>
        /// <returns>基準日にセットする日付</returns>
        private string LoadControl()
        {
            using (DataTable controlData = this.businessDB.GetDataTable(MakeSQLSelectControl()))
            {
                if (ComTool.Function.IsValidDataTable(controlData))
                {
                    this.controlUriageZeiTumiageKBN = ComTool.Function.AnyToInt32(controlData.Rows[0]["UriageZeiTumiageKBN"]);
                    this.controlSiireZeiTumiageKBN = ComTool.Function.AnyToInt32(controlData.Rows[0]["SiireZeiTumiageKBN"]);
                    this.controlR5NenOctIgoUriageZeiTumiageKBN = ComTool.Function.AnyToInt32(controlData.Rows[0]["R5NenOctIgoUriageZeiTumiageKBN"]);
                    this.controlR5NenOctIgoSiireZeiTumiageKBN = ComTool.Function.AnyToInt32(controlData.Rows[0]["R5NenOctIgoSiireZeiTumiageKBN"]);

                    if (!String.IsNullOrEmpty(controlData.Rows[0]["KijunDate"].ToString()))
                    {
                        return controlData.Rows[0]["KijunDate"].ToString();
                    }
                }
            }
            //基準日が未設定の場合はシステム日付
            return ComTool.Function.AnyToString(this.businessDB.ExecuteScalar("SELECT GETDATE()"));
        }

        /// エラープロバイダの表示
        /// <summary>
        /// エラープロバイダの表示
        /// </summary>
        /// <param name="control">表示対象コントロール</param>
        /// <param name="messageCD2">メッセージCD2</param>
        /// <param name="messageParameters">メッセージパラメータ用</param>
        private void DispErrorProvider(Control control, string messageCD2, params object[] messageParameters)
        {
            ComToolLogic.Message message = new ComToolLogic.Message();
            message.GetMessage(SYS_BUNRUI, CUSTOM_SEQ, MESSAGECD1, messageCD2, messageParameters, null);
            WinToolUI.Obic7ErrorProvider.SetError(control, message.MessageSummary);
        }

        /// 事業所コードのチェック
        /// <summary>
        /// 事業所コードのチェック(事業所名の表示)
        /// </summary>
        /// <returns>true:エラーなし　false:エラーあり</returns>
        private bool CheckZigyousho()
        {
            string messageCD2 = "";
            this.obic7LabelStringZigyoushoName.Text = "";

            using (DataTable zigyoushoData = M0000SearchCom.M0000MS_MA_Zigyousho.SelectByKaiteiDate(
                this.obic7TextStringZigyoushoCD.Text, (DateTime)this.obic7TextDatetimeBaseDate.Value, null, 0, 0, 0, ""))
            {
                if (ComTool.Function.IsValidDataTable(zigyoushoData))
                {
                    if (String.IsNullOrEmpty(ComTool.Function.AnyToString(zigyoushoData.Rows[0]["ZigyoushoName"])))
                    {
                        //事業所名がNULL(廃止)
                        messageCD2 = "00026";
                    }
                    else
                    {
                        this.obic7LabelStringZigyoushoName.Value = ComTool.Function.AnyToString(zigyoushoData.Rows[0]["ZigyoushoNameRyaku"]);
                    }
                }
                else
                {
                    //事業所が存在しない
                    messageCD2 = "00024";
                }
            }

            if (!String.IsNullOrEmpty(messageCD2))
            {
                WinToolLogicUI.Obic7MessageBox.Show(SYS_BUNRUI, CUSTOM_SEQ, MESSAGECD1, messageCD2, null, null, this.GetType(),
                                                    WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxButtons.OK,
                                                    WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1);
                return false;
            }
            return true;
        }

        /// F4(検索)処理
        /// <summary>
        /// F4(検索)処理
        /// </summary>
        private void F4Action()
        {
            WinToolUI.Obic7TextAbstract textAbstract = this.PrevControl as WinToolUI.Obic7TextAbstract;
            if (textAbstract == null)
            {
                return;
            }

            if (textAbstract.Name == "obic7TextStringZigyoushoCD")
            {
                using (M0000Search.MA_ZigyoushoCD zigyoushoCDdlg = new M0000Search.MA_ZigyoushoCD())
                {
                    textAbstract.AttachedSearchDialog = zigyoushoCDdlg;
                    zigyoushoCDdlg.BasicDate = (DateTime)this.obic7TextDatetimeBaseDate.Value;
                    zigyoushoCDdlg.SecurityPatternCD = "";
                    zigyoushoCDdlg.AllSumKubun = 0;
                    zigyoushoCDdlg.AllZigyoushoKubun = 0;
                    zigyoushoCDdlg.NameNullKubun = 1;
                    if (textAbstract.ShowDialog() == DialogResult.OK)
                    {
                        //OK押下時、表示ボタン押下時の処理を行う
                        this.obic7ButtonWithLogicDisp.PerformClick();
                        return;
                    }
                }
            }
            else if (textAbstract.Name == "obic7TextStringTorihikisakiCD")
            {
                using (P0000Search.TorihikisakiCD torihikisakiCDdlg = new P0000Search.TorihikisakiCD())
                {
                    textAbstract.AttachedSearchDialog = torihikisakiCDdlg;
                    if (textAbstract.ShowDialog() == DialogResult.OK)
                    {
                        //OK押下時、次の項目へフォーカスを移動
                        base.SelectNextInputControl(true);
                        return;
                    }
                }
            }
            //検索実行がなかった場合は直前のコントロールにフォーカスを移動させる
            this.PrevControl.Focus();
        }

        /// 入力項目チェック
        /// <summary>
        /// 入力項目チェック
        /// </summary>
        /// <returns>true:エラーなし　false:エラーあり</returns>
        private bool InputChecks()
        {
            bool result = true;

            //基準日
            if (String.IsNullOrEmpty(this.obic7TextDatetimeBaseDate.Text))
            {
                DispErrorProvider(this.obic7TextDatetimeBaseDate, "00104", null);
                this.obic7TextDatetimeBaseDate.Focus();
                result = false;
            }
            //事業所コード
            if (String.IsNullOrEmpty(this.obic7TextStringZigyoushoCD.Text))
            {
                DispErrorProvider(this.obic7TextStringZigyoushoCD, "00104", null);
                if (result == true)
                {
                    this.obic7TextStringZigyoushoCD.Focus();
                    result = false;
                }
            }
            return result;
        }

        /// F3：削除
        /// <summary>
        /// F3：削除
        /// </summary>
        private void F3Action()
        {
            int deleteCount = 0;

            //確認メッセージ
            if (WinToolLogicUI.Obic7MessageBox.Show(SYS_BUNRUI, CUSTOM_SEQ, MESSAGECD1, "00111", null, null, this.GetType(), WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxButtons.YesNo, WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }

            this.businessDB.BeginTransaction();
            try
            {
                deleteCount = this.businessDB.ExecuteNonQuery(MakeSQLDeleteMaster());
                if (deleteCount == 0)
                {
                    //排他エラー
                    WinToolLogicUI.Obic7MessageBox.Show(SYS_BUNRUI, CUSTOM_SEQ, MESSAGECD1, "00102", null, null, this.GetType(), WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxButtons.OK, WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1);
                    this.businessDB.RollBackTransaction();
                }
                else
                {
                    this.businessDB.CommitTransaction();
                }
            }
            catch
            {
                //ロールバックのみ行い、例外処理はフォーム基底クラスに任せる
                this.businessDB.RollBackTransaction();
                throw;
            }

            //削除成功時は事業所コードもクリアする
            SwitchHeader(true, deleteCount != 0, MODE_INSERT);
            this.obic7TextDatetimeBaseDate.Text = LoadControl();
            this.obic7TextDatetimeBaseDate.Focus();
            SwitchDetail(false, true);
        }

        /// F5:取消処理
        /// <summary>
        /// F5:取消処理
        /// </summary>
        private void F5Action()
        {
            //フォーカスがヘッダ部に存在する場合
            if ((this.PrevControl == this.obic7TextDatetimeBaseDate) ||
               (this.PrevControl == this.obic7TextStringZigyoushoCD) ||
               (this.ActiveControl == this.obic7ButtonWithLogicZigyoushoCDSearch) ||
               (this.ActiveControl == this.obic7ButtonWithLogicDisp))
            {
                //ヘッダ部のValidatingを走らせない
                this.isF5Check = true;
                this.obic7TextDatetimeBaseDate.Text = LoadControl();
                this.obic7TextStringZigyoushoCD.Focus();
                this.obic7TextStringZigyoushoCD.Text = "";
                this.obic7TextDatetimeBaseDate.Focus();
                this.obic7LabelStringZigyoushoName.Text = "";
            }
            else
            {
                //確認メッセージ
                if (WinToolLogicUI.Obic7MessageBox.Show(SYS_BUNRUI, CUSTOM_SEQ, MESSAGECD1, "00113", null, null, this.GetType(), WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxButtons.YesNo, WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    SwitchHeader(true, false, MODE_INSERT);
                    this.obic7TextDatetimeBaseDate.Text = LoadControl();
                    this.obic7TextDatetimeBaseDate.Focus();
                    SwitchDetail(false, true);
                }
                else
                {
                    this.PrevControl.Focus();
                }
            }
        }

        /// F10:登録処理
        /// <summary>
        /// F10:登録処理
        /// </summary>
        private void F10Action()
        {
            int updateCount = 0;
            string mode = this.obic7LabelStringMode.Value;
            string errorCD2 = "";

            //入力項目のValidating
            if (this.Validate() == false)
            {
                return;
            }

            //確認メッセージ
            if (WinToolLogicUI.Obic7MessageBox.Show(SYS_BUNRUI, CUSTOM_SEQ, MESSAGECD1, "00112", null, null, this.GetType(), WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxButtons.YesNo, WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1) != DialogResult.Yes)
            {
                return;
            }

            this.businessDB.BeginTransaction();
            try
            {
                updateCount = this.businessDB.ExecuteNonQuery((mode == MODE_INSERT) ? MakeSQLInsertMaster() : MakeSQLUpdateMaster());
                if (updateCount == 0)
                {
                    //排他エラー
                    errorCD2 = (mode == MODE_INSERT) ? "00100" : "00102";
                    this.businessDB.RollBackTransaction();
                    this.obic7LabelStringZigyoushoName.Value = "";
                }
                else
                {
                    this.businessDB.CommitTransaction();
                    this.obic7TextStringZigyoushoCD.Value = "";
                    this.obic7LabelStringZigyoushoName.Value = "";
                }
            }
            catch
            {
                //ロールバックのみ行い、例外処理はフォーム基底クラスに任せる
                this.businessDB.RollBackTransaction();
                throw;
            }

            SwitchHeader(true, false, MODE_INSERT);
            this.obic7TextDatetimeBaseDate.Focus();
            SwitchDetail(false, true);

            if (!String.IsNullOrEmpty(errorCD2))
            {
                WinToolLogicUI.Obic7MessageBox.Show(SYS_BUNRUI, CUSTOM_SEQ, MESSAGECD1, errorCD2, null, null, this.GetType(), WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxButtons.OK, WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1);
            }
        }

        /// 明細部ファンクションボタン制御
        /// <summary>
        /// 明細部ファンクションボタン制御
        /// </summary>
        private void SetFunctionButtonDetail()
        {
            if (this.obic7LabelStringMode.Text == MODE_INSERT)
            {
                //F5,F10,F12使用可
                base.SetFunctionButton(false, false, false, false, true, false, false, false, false, true, false, true);
            }
            else if (this.obic7LabelStringMode.Text == MODE_UPDATE)
            {
                //F3,F5,F10,F12使用可
                base.SetFunctionButton(false, false, true, false, true, false, false, false, false, true, false, true);
            }
        }

        /// 明細部（検索有）ファンクションボタン制御
        /// <summary>
        /// 明細部（検索有）ファンクションボタン制御
        /// </summary>
        private void SetFunctionButtonDetailSearch()
        {
            if (this.obic7LabelStringMode.Text == MODE_INSERT)
            {
                //F4,F5,F10,F12使用可
                base.SetFunctionButton(false, false, false, true, true, false, false, false, false, true, false, true);
            }
            else if (this.obic7LabelStringMode.Text == MODE_UPDATE)
            {
                //F3,F4,F5,F10,F12使用可
                base.SetFunctionButton(false, false, true, true, true, false, false, false, false, true, false, true);
            }
        }

        #endregion

        #region イベント

        /// F3:削除押下時の処理
        private void obic7ToolStripButtonWithLogicF3_Click(object sender, EventArgs e)
        {
            F3Action();
        }

        /// F4:検索押下時の処理
        private void obic7ToolStripButtonWithLogicF4_Click(object sender, EventArgs e)
        {
            F4Action();
        }

        /// F5:取消押下時の処理
        private void obic7ToolStripButtonWithLogicF5_Click(object sender, EventArgs e)
        {
            F5Action();
        }

        /// F10:登録押下時の処理
        private void obic7ToolStripButtonWithLogicF10_Click(object sender, EventArgs e)
        {
            F10Action();
        }

        /// フォームLoad時の処理
        /// <summary>
        /// フォームLoad時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MA_ZigyoushoSeigyoMasterMaintenance_Load(object sender, EventArgs e)
        {
            //DBアクセスクラス（ステートフル）のインスタンスを作成
            this.businessDB = new WinToolLogic.DBAccess(SYS_BUNRUI, CUSTOM_SEQ, ComToolLogic.DBAccess.Obic7DBAccessDBCategory.Business);

            this.obic7LabelStringMode.Value = MODE_INSERT;
            this.obic7TextDatetimeBaseDate.Text = LoadControl();
            SwitchDetail(false, false);
        }

        /// フォーム表示時の処理
        private void MA_ZigyoushoSeigyoMasterMaintenance_Shown(object sender, EventArgs e)
        {
            this.obic7TextDatetimeBaseDate.Focus();
        }

        /// フォーム上でのKeyDownイベント
        /// <summary>
        /// フォーム上でのKeyDownイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MA_ZigyoushoSeigyoMasterMaintenance_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Alt == false) && (e.Control == false) && (e.Shift == false) && (e.KeyCode == Keys.Enter))
            {
                if (this.ActiveControl == this.obic7TextStringZigyoushoCD)
                {
                    this.obic7TextStringZigyoushoCD.CheckOnClose = true;
                    if (this.Validate())
                    {
                        //事業所コードでEnterが押下された場合、表示ボタンクリックと同様の動きをする
                        this.obic7ButtonWithLogicDisp.PerformClick();
                    }
                    this.obic7TextStringZigyoushoCD.CheckOnClose = false;
                }
                else
                {
                    this.SelectNextInputControl(true);
                }
            }
        }

        /// フォームを閉じようとした際の処理
        private void MA_ZigyoushoSeigyoMasterMaintenance_FormClosing(object sender, FormClosingEventArgs e)
        {
            //編集中であれば確認メッセージを表示する
            if ((this.obic7TextStringUriageZeiTumiageKBN.Enabled == true) && (this.obic7TextStringUriageZeiTumiageKBN.ReadOnly == false))
            {
                if (WinToolLogicUI.Obic7MessageBox.Show(SYS_BUNRUI, CUSTOM_SEQ, MESSAGECD1, "00110", null, null, this.GetType(), WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxButtons.YesNo, WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        /// 基準日Validatingイベント
        /// <summary>
        /// 基準日Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7TextDatetimeBaseDate_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(this.obic7TextDatetimeBaseDate.Text))
            {
                DispErrorProvider(this.obic7TextDatetimeBaseDate, "00104", null);
                e.Cancel = true;
                this.obic7LabelStringZigyoushoName.Value = "";
                return;
            }
            //事業所名の洗い換え
            if (!String.IsNullOrEmpty(this.obic7TextStringZigyoushoCD.Text) && !CheckZigyousho())
            {
                e.Cancel = true;
            }
        }

        /// 事業所コードValidatingイベント
        /// <summary>
        /// 事業所コードValidatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7TextStringZigyoushoCD_Validating(object sender, CancelEventArgs e)
        {
            //ヘッダ部でF5が押下された場合はチェックしない
            if (this.isF5Check)
            {
                this.isF5Check = false;
                return;
            }

            if (String.IsNullOrEmpty(this.obic7TextStringZigyoushoCD.Text))
            {
                DispErrorProvider(this.obic7TextStringZigyoushoCD, "00104", null);
                this.obic7LabelStringZigyoushoName.Text = "";
                e.Cancel = true;
                return;
            }

            if (this.obic7TextStringZigyoushoCD.IsChangedText)
            {
                if (!CheckZigyousho())
                {
                    e.Cancel = true;
                    return;
                }
            }
            //最終項目なので表示ボタン押下時と同様な処理を行う
            this.obic7ButtonWithLogicDisp.PerformClick();
        }

        /// 事業所コード_検索ボタン押下時の処理
        private void obic7ButtonWithLogicZigyoushoCDSearch_Click(object sender, EventArgs e)
        {
            this.obic7TextStringZigyoushoCD.Focus();
            this.obic7ToolStripButtonWithLogicF4.PerformClick();
        }

        /// 表示ボタン押下時の処理
        /// <summary>
        /// 表示ボタン押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7ButtonWithLogicDisp_Click(object sender, EventArgs e)
        {
            SwitchDetail(false, true);
            if (!InputChecks())
            {
                return;
            }

            using (DataTable masterData = this.businessDB.GetDataTable(MakeSQLSelectMaster()))
            {
                SwitchDetail(true, false);
                if (ComTool.Function.IsValidDataTable(masterData))
                {
                    //修正モード
                    SwitchHeader(false, false, MODE_UPDATE);
                    SetDetailData(masterData);
                }
                else
                {
                    //新規モード
                    SwitchHeader(false, false, MODE_INSERT);
                }
            }
            SetCalcKBNNames();

            //明細部の先頭項目へフォーカスを移動
            this.obic7TextSeparatorDetail.Focus();
        }

        /// 消費税計算方式区分Validating
        /// <summary>
        /// 消費税計算方式区分Validating(未入力、0:割戻方式、1:積上方式のみ可)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7TextStringCalcKBN_Validating(object sender, CancelEventArgs e)
        {
            WinToolUI.Obic7TextString textCalcKBN = sender as WinToolUI.Obic7TextString;
            if (textCalcKBN == null)
            {
                return;
            }
            WinToolUI.Obic7ErrorProvider.Clear(textCalcKBN);

            int calcKBN;
            if (!String.IsNullOrEmpty(textCalcKBN.Value)
                && (!int.TryParse(textCalcKBN.Value, out calcKBN) || GetCalcKBNName(calcKBN) == null))
            {
                WinToolUI.Obic7ErrorProvider.SetError(textCalcKBN, "0:割戻方式、1:積上方式 のいずれかを入力してください。未入力の場合は財務コントロールマスタの設定に従います。");
                e.Cancel = true;
            }
            SetCalcKBNNames();
        }

        /// 事業所コードフォーカス取得時の処理
        private void obic7TextStringZigyoushoCD_Enter(object sender, EventArgs e)
        {
            //ファンクションボタン切替（使用可能：検索、取消、閉じる）
            base.SetFunctionButton(false, false, false, true, true, false, false, false, false, false, false, true);
        }

        /// ヘッダ部のファンクションボタン制御
        private void SetFunctionButtonHeader(object sender, EventArgs e)
        {
            //ファンクションボタン切替（使用可能：取消、閉じる）
            base.SetFunctionButton(false, false, false, false, true, false, false, false, false, false, false, true);
        }

        /// 明細部フォーカス取得時の処理
        private void controlDetail_Enter(object sender, EventArgs e)
        {
            SetFunctionButtonDetail();
        }

        /// 明細部（検索ボタン有）フォーカス取得時の処理
        private void controlDetailSearch_Enter(object sender, EventArgs e)
        {
            SetFunctionButtonDetailSearch();
        }

        /// 取引先コード_検索ボタン押下時の処理
        private void obic7ButtonWithLogicTorihikisakiCDSearch_Click(object sender, EventArgs e)
        {
            this.obic7TextStringTorihikisakiCD.Focus();
            this.obic7ToolStripButtonWithLogicF4.PerformClick();
        }

        /// 取引先コードValidating
        /// <summary>
        /// 取引先コードValidating(取引先マスタの存在チェック。基準日時点で有効な改定があること)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7TextStringTorihikisakiCD_Validating(object sender, CancelEventArgs e)
        {
            WinToolUI.Obic7ErrorProvider.Clear(this.obic7TextStringTorihikisakiCD);

            if (String.IsNullOrEmpty(this.obic7TextStringTorihikisakiCD.Value))
            {
                return;
            }

            if (ComTool.Function.AnyToInt32(this.businessDB.ExecuteScalar(MakeSQLExistsTorihikisaki())) == 0)
            {
                WinToolUI.Obic7ErrorProvider.SetError(this.obic7TextStringTorihikisakiCD, "取引先マスタに存在しない取引先コードです(基準日時点)。");
                e.Cancel = true;
            }
        }

        #endregion
    }
}
