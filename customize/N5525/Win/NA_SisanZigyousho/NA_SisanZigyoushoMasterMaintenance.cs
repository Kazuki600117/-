//
//    2008/12/01    新規作成    T.Wakumoto
//    2008/12/15    修正        T.Wakumoto
//                  第1次単体テスト結果修正
//                        ・不要なusingを削除
//                        ・デザインの修正
//                        ・this.の追加
//                        ・検索ボタンのEnterイベントを修正
//    2008/12/16    修正　　　　T.Wakumoto
//                  第2次単体テスト結果修正
//                        ・ヘッダ部のファンクション制御を修正
//                        ・表示ボタン押下時のフォーカス制御のため、obic7TextSeparatorのIsRefrectLowerOrderを
//                          trueに変更
//    2008/12/22    修正　　　　T.Wakumoto
//　　　　　　　　　追加単体テスト結果修正
//                        ・基準日取得のメソッドを修正
//　　　　　　　　　　　　・基準日Validatingのイベントを修正
//    2009/02/19    修正        T.Wakumoto
//                        ・資本金のMaxValueとMinValueを設定
//                        ・事業種目の項目名を変更
//                        ・削除失敗時にKey項目がクリアされないよう変更
//    2009/02/23    修正        T.Wakumoto
//                        ・事業所種目の項目名を変更
//                        ・ヘッダ部におけるF5取消を可能にするため、F5押下時に事業所コードValidatingが走らないように変更
//    2009/02/24    修正        T.Wakumoto
//                        ・修正履歴不備の為、修正履歴を追加
//    2009/02/27    修正        T.Wakumoto
//                        ・明細部でのF5(取消)押下時処理,ヘッダ部でのF5(取消)押下後の処理を修正
//    2009/03/04    修正        T.Wakumoto
//                        ・F4押下時、値がセットされた場合に明細部を表示させるよう修正
//    2009/09/02    修正        M.Kasayuki
//                        ・ヘッダ部・事業所コード名ラベルに事業所略名が表示されるよう修正(SEQ:550)
//                        ・申告電話番号と会計電話番号のラベルの文字の位置をずらす(SEQ:551)
//    2026/10/11    変更        (5525)事業所別償却資産申告対応
//                        ・N0000 標準画面を複製
//                        ・「地方税申告(連携)」項目(納税者コード、利用者ID、代表者資格、代表者郵便番号・住所・電話番号)を追加
//                          事業所別地方税申告情報マスタ(N5525MS_NA_ZigyoushoSinkokuJouhou)へ資産事業所マスタと同一トランザクションで登録・削除する
//                          地方税申告(連携)項目がすべて未入力の場合はレコードを持たない(償却資産申告書では空欄で出力)
//                        ・カスタマイズSEQを 5525 に変更(メッセージ取得は標準 0000 のまま)


using System;
// 2008/12/15    変更    T.Wakumoto 開始    不要なusingの削除 
//using System.Collections.Generic;
// 2008/12/15    変更    T.Wakumoto 終了
using System.ComponentModel;
using System.Data;
// 2008/12/15    変更    T.Wakumoto 開始    不要なusingの削除
//using System.Drawing;
// 2008/12/15    変更    T.Wakumoto 終了
using System.Text;
using System.Windows.Forms;
using WinToolLogicUI = Obic7fx.Win.A0000.Tool.Logic.UI;
using WinToolUI = Obic7fx.Win.A0000.Tool.UI;
using WinToolLogic = Obic7fx.Win.A0000.Tool.Logic;
using ComTool = Obic7fx.Com.A0000.Tool;
using ComToolLogic = Obic7fx.Com.A0000.Tool.Logic;
using M0000Search = Obic7fx.Win.M0000.Tool.Search;
using M0000SearchCom = Obic7fx.Win.M0000.Tool.SearchCom;
using N0000Search = Obic7fx.Win.N0000.Tool.Search;
using N0000SearchCom = Obic7fx.Win.N0000.Tool.SearchCom;
using System.Data.SqlClient;



namespace Obic7fx.Win.N5525.MasterM.NA_SisanZigyousho
{
    /// 資産事業所マスタメンテ
    /// <summary>
    /// 資産事業所マスタメンテ
    /// </summary>
    public partial class NA_SisanZigyoushoMasterMaintenance : Obic7fx.Win.M0000.Tool.UI.Obic7MBussinessFormBase
    {
        #region 定数

        //画面制御用
        //新規追加時
        private const string MODE_INSERT = "新規";
        //修正入力時
        private const string MODE_UPDATE = "修正";

        //メッセージCD1
        private const string MESSAGECD1 = "000";
        //システム定数
        private const string SYS_BUNRUI = "N";
        //カスタマイズナンバー
        // 2026/10/11 変更 (5525)事業所別償却資産申告対応 開始
        //private const string CUSTOM_SEQ = "0000";
        private const string CUSTOM_SEQ = "5525";
        //メッセージ取得用カスタマイズナンバー(メッセージは標準 0000 の定義を使用する)
        private const string MESSAGE_SEQ = "0000";
        // 2026/10/11 変更 (5525)事業所別償却資産申告対応 終了

        // 2026/10/11 追加 (5525)事業所別償却資産申告対応 開始
        //代表者資格の既定値(01:代表者)
        private const byte DAIHYOUSHA_SIKAKU_DEFAULT = 1;
        // 2026/10/11 追加 (5525)事業所別償却資産申告対応 終了
        
        #endregion

        #region 変数

        //DB接続用クラス（ステートフル）
        private WinToolLogic.DBAccess businessDB;
        //更新タイムスタンプ格納用
        private byte[] timestamp;
        // 2009/02/23 追加 OBIC T.Wakumoto 開始 F5が押下されたかどうかの判定用
        private bool isF5Check = false;
        // 2009/02/23 追加 OBIC T.Wakumoto 終了
        // 2026/10/11 追加 (5525)事業所別償却資産申告対応 開始
        //事業所別地方税申告情報マスタの更新タイムスタンプ格納用(レコードが存在しない場合はnull)
        private byte[] timestampTihouzei;
        // 2026/10/11 追加 (5525)事業所別償却資産申告対応 終了


        #endregion

        #region メソッド

        /// 引数ありコンストラクタ
        /// <summary>
        /// 引数ありコンストラクタ
        /// </summary>
        public NA_SisanZigyoushoMasterMaintenance(string jobID)
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
        public NA_SisanZigyoushoMasterMaintenance()
            : base()
        {
            InitializeComponent();
        }

        /// 明細データ取得
        /// <summary>
        /// 明細データ取得
        /// </summary>
        /// <returns>カラムの文字列</returns>
        private string GetColumns()
        {
            StringBuilder columns = new StringBuilder();
            columns.AppendLine(" ,N0000MS_NA_SisanZigyousho.YuubinNO1 ");                   //郵便番号1
            columns.AppendLine(" ,N0000MS_NA_SisanZigyousho.YuubinNO2 ");                   //郵便番号2
            columns.AppendLine(" ,N0000MS_NA_SisanZigyousho.Juusho1Kana ");                 //住所1カナ
            columns.AppendLine(" ,N0000MS_NA_SisanZigyousho.Juusho2Kana ");                 //住所2カナ
            columns.AppendLine(" ,N0000MS_NA_SisanZigyousho.Juusho1 ");                     //住所1
            columns.AppendLine(" ,N0000MS_NA_SisanZigyousho.Juusho2 ");                     //住所2
            columns.AppendLine(" ,N0000MS_NA_SisanZigyousho.TELNO1 ");                      //電話番号1
            columns.AppendLine(" ,N0000MS_NA_SisanZigyousho.TELNO2 ");                      //電話番号2
            columns.AppendLine(" ,N0000MS_NA_SisanZigyousho.TELNO3 ");                      //電話番号3
            columns.AppendLine(" ,N0000MS_NA_SisanZigyousho.CompanyName ");                 //会社名
            columns.AppendLine(" ,N0000MS_NA_SisanZigyousho.CompanyKanaName ");             //会社カナ名
            columns.AppendLine(" ,N0000MS_NA_SisanZigyousho.DaihyoushaName ");              //代表者氏名
            columns.AppendLine(" ,N0000MS_NA_SisanZigyousho.DaihyoushaKanaName ");          //代表者カナ名
            columns.AppendLine(" ,N0000MS_NA_SisanZigyousho.Yagou ");                       //屋号
            // 2009/02/19 修正 OBIC T.Wakumoto 開始 ZigyoushumokuをZigyouShumokuに変更
            //columns.AppendLine(" ,N0000MS_NA_SisanZigyousho.Zigyoushumoku ");             //事業種目
            columns.AppendLine(" ,N0000MS_NA_SisanZigyousho.ZigyouShumoku ");               //事業種目
            // 2009/02/19 修正 OBIC T.Wakumoto 終了
            columns.AppendLine(" ,N0000MS_NA_SisanZigyousho.Sihonkin ");                    //資本金
            columns.AppendLine(" ,N0000MS_NA_SisanZigyousho.SeturituDate ");                //設立年月日
            columns.AppendLine(" ,N0000MS_NA_SisanZigyousho.SinkokuTantoushaName ");        //申告担当者氏名
            columns.AppendLine(" ,N0000MS_NA_SisanZigyousho.TantoushaTELNO1 ");             //申告担当者電話番号1
            columns.AppendLine(" ,N0000MS_NA_SisanZigyousho.TantoushaTELNO2 ");             //申告担当者電話番号2
            columns.AppendLine(" ,N0000MS_NA_SisanZigyousho.TantoushaTELNO3 ");             //申告担当者電話番号3
            columns.AppendLine(" ,N0000MS_NA_SisanZigyousho.KaikeiZimushoName ");           //会計事務所名
            columns.AppendLine(" ,N0000MS_NA_SisanZigyousho.KaikeiZimushoTELNO1 ");         //会計事務所電話番号1
            columns.AppendLine(" ,N0000MS_NA_SisanZigyousho.KaikeiZimushoTELNO2 ");         //会計事務所電話番号2
            columns.AppendLine(" ,N0000MS_NA_SisanZigyousho.KaikeiZimushoTELNO3 ");         //会計事務所電話番号3
            return columns.ToString();
        }

        /// SQL文生成（SELECT用）
        /// <summary>
        /// SQL文生成（SELECT用）
        /// </summary>
        /// <returns>生成したSQL文（SELECT用）を返却</returns>
        private string MakeSQLSelectMaster()
        {
            //SQL文
            StringBuilder sqlSelect = new StringBuilder();
            //SELECT文
            sqlSelect.AppendLine(" SELECT ");
            sqlSelect.AppendLine(" N0000MS_NA_SisanZigyousho.ZititaiCD ");
            sqlSelect.AppendLine(" ,N0000MS_NA_Zititai.ZititaiName ");
            sqlSelect.AppendLine(GetColumns());
            sqlSelect.AppendLine(" ,N0000MS_NA_SisanZigyousho.UpdateTimestamp ");
            sqlSelect.AppendLine(" FROM N0000MS_NA_SisanZigyousho ");
            sqlSelect.AppendLine(" LEFT OUTER JOIN N0000MS_NA_Zititai ");
            sqlSelect.AppendLine(" ON N0000MS_NA_SisanZigyousho.ZititaiCD = N0000MS_NA_Zititai.ZititaiCD ");
            sqlSelect.AppendLine(" WHERE N0000MS_NA_SisanZigyousho.ZigyoushoCD = " + ComTool.Function.SqlEscapeVarchar(this.obic7TextStringZigyoushoCD.Value) + " ");
            return sqlSelect.ToString();
        }

        /// SQL文生成（DELETE用）
        /// <summary>
        /// SQL文生成（DELETE用）
        /// </summary>
        /// <returns>生成したSQL文（DELETE用）を返却</returns>
        private string MakeSQLDeleteMaster()
        {
            //SQL文
            StringBuilder sqlDelete = new StringBuilder();
            //DELETE文
            sqlDelete.AppendLine(" DELETE FROM N0000MS_NA_SisanZigyousho ");
            sqlDelete.AppendLine(" WHERE N0000MS_NA_SisanZigyousho.ZigyoushoCD = " + ComTool.Function.SqlEscapeVarchar(this.obic7TextStringZigyoushoCD.Value) + " ");
            sqlDelete.AppendLine("   AND N0000MS_NA_SisanZigyousho.UpdateTimestamp = " + ComTool.Function.SqlEscapeBinary(this.timestamp) + " ");
            return sqlDelete.ToString();
        }

        /// SQL文生成（INSERT用）
        /// <summary>
        /// SQL文生成（INSERT用）
        /// </summary>
        /// <returns>生成したSQL文（INSERT用）を返却</returns>
        private string MakeSQLInsertMaster()
        {
            //SQL文
            StringBuilder sqlInsert = new StringBuilder();
            //INSERT文
            sqlInsert.AppendLine(" INSERT INTO N0000MS_NA_SisanZigyousho ( ");
            sqlInsert.AppendLine("        N0000MS_NA_SisanZigyousho.ZigyoushoCD ");
            sqlInsert.AppendLine("        ,N0000MS_NA_SisanZigyousho.ZititaiCD ");
            sqlInsert.AppendLine(GetColumns());
            sqlInsert.AppendLine("        ,N0000MS_NA_SisanZigyousho.CompanyNO ");
            sqlInsert.AppendLine("        ,N0000MS_NA_SisanZigyousho.UpdateTimestamp ");
            sqlInsert.AppendLine("        ,N0000MS_NA_SisanZigyousho.UpdateDatetime ");
            sqlInsert.AppendLine("        ,N0000MS_NA_SisanZigyousho.UpdateCertificatedGuid ");
            sqlInsert.AppendLine("        ,N0000MS_NA_SisanZigyousho.UpdateJobID ");
            sqlInsert.AppendLine("        ,N0000MS_NA_SisanZigyousho.UpdateUserID ");
            sqlInsert.AppendLine("        ,N0000MS_NA_SisanZigyousho.UpdateComputerName ");
            sqlInsert.AppendLine("        ,N0000MS_NA_SisanZigyousho.UpdateComputerUserName ");
            sqlInsert.AppendLine("        ,N0000MS_NA_SisanZigyousho.UpdateComputerDomainName ");
            sqlInsert.AppendLine("        ) ");
            sqlInsert.AppendLine("        SELECT ");
            sqlInsert.AppendLine(ComTool.Function.SqlEscapeVarchar(this.obic7TextStringZigyoushoCD.Value));
            //申告自治体コード
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(String.IsNullOrEmpty(this.obic7TextStringZititaiCD.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringZititaiCD.Value));
            //郵便番号1
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(String.IsNullOrEmpty(this.obic7TextStringPostCD1.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringPostCD1.Value));
            //郵便番号2
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(String.IsNullOrEmpty(this.obic7TextStringPostCD2.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringPostCD2.Value));
            //住所1カナ
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(String.IsNullOrEmpty(this.obic7TextStringAddress1Kana.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringAddress1Kana.Value));
            //住所2カナ
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(String.IsNullOrEmpty(this.obic7TextStringAddress2Kana.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringAddress2Kana.Value));
            //住所1
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(String.IsNullOrEmpty(this.obic7TextStringAddress1.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringAddress1.Value));
            //住所2
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(String.IsNullOrEmpty(this.obic7TextStringAddress2.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringAddress2.Value));
            //電話番号1
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(String.IsNullOrEmpty(this.obic7TextStringTelNo1.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringTelNo1.Value));
            //電話番号2
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(String.IsNullOrEmpty(this.obic7TextStringTelNo2.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringTelNo2.Value));
            //電話番号3
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(String.IsNullOrEmpty(this.obic7TextStringTelNo3.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringTelNo3.Value));
            //会社名
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(String.IsNullOrEmpty(this.obic7TextStringCompanyName.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringCompanyName.Value));
            //会社カナ名
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(String.IsNullOrEmpty(this.obic7TextStringCompanyKanaName.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringCompanyKanaName.Value));
            //代表者氏名
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(String.IsNullOrEmpty(this.obic7TextStringDaihyoushaName.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringDaihyoushaName.Value));
            //代表者カナ名
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(String.IsNullOrEmpty(this.obic7TextStringDaihyoushaKanaName.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringDaihyoushaKanaName.Value));
            //屋号
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(String.IsNullOrEmpty(this.obic7TextStringYagou.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringYagou.Value));
            //事業種目
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(String.IsNullOrEmpty(this.obic7TextStringZigyoushumoku.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringZigyoushumoku.Value));
            //資本金
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(ComTool.Function.SqlEscapeNumber(this.obic7TextNumberSihonkin.Value));
            //設立年月日
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(String.IsNullOrEmpty(this.obic7TextDatetimeSeturituDate.Text) ?
                "NULL" : ComTool.Function.SqlEscapeDatetime(this.obic7TextDatetimeSeturituDate.Value));
            //申告担当者氏名
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(String.IsNullOrEmpty(this.obic7TextStringSinkokuTantoushaName.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringSinkokuTantoushaName.Value));
            //申告担当者電話番号1
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(String.IsNullOrEmpty(this.obic7TextStringTantoushaTelNo1.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringTantoushaTelNo1.Value));
            //申告担当者電話番号2
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(String.IsNullOrEmpty(this.obic7TextStringTantoushaTelNo2.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringTantoushaTelNo2.Value));
            //申告担当者電話番号3
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(String.IsNullOrEmpty(this.obic7TextStringTantoushaTelNo3.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringTantoushaTelNo3.Value));
            //会計事務所名
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(String.IsNullOrEmpty(this.obic7TextStringKaikeiZimushoName.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringKaikeiZimushoName.Value));
            //会計事務所電話番号1
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(String.IsNullOrEmpty(this.obic7TextStringKaikeiZimushoTelNo1.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringKaikeiZimushoTelNo1.Value));
            //会計事務所電話番号2
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(String.IsNullOrEmpty(this.obic7TextStringKaikeiZimushoTelNo2.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringKaikeiZimushoTelNo2.Value));
            //会計事務所電話番号3
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(String.IsNullOrEmpty(this.obic7TextStringKaikeiZimushoTelNo3.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringKaikeiZimushoTelNo3.Value));
            //会社No
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(ComTool.Function.SqlEscapeNumber(ComToolLogic.CurrentUser.CompanyNO));
            //更新タイムスタンプ
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(" NULL ");
            //更新日時
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(" GETDATE() ");
            //更新認証済Guid
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.CertificatedGuid));
            //更新JobID
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.JobID));
            //更新ユーザID
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.UserID));
            //更新PC名
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.ComputerName));
            //更新PCユーザ名
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.ComputerUserName));
            //更新PCドメイン名
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.ComputerDomainName));
            sqlInsert.AppendLine("        WHERE NOT EXISTS ( ");
            sqlInsert.AppendLine("                SELECT N0000MS_NA_SisanZigyousho.ZigyoushoCD ");
            sqlInsert.AppendLine("                FROM   N0000MS_NA_SisanZigyousho ");
            sqlInsert.AppendLine("                WHERE  N0000MS_NA_SisanZigyousho.ZigyoushoCD = ");
            sqlInsert.AppendLine(ComTool.Function.SqlEscapeVarchar(this.obic7TextStringZigyoushoCD.Value));
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
            //SQL文
            StringBuilder sqlUpdate = new StringBuilder();
            //UPDATE文
            sqlUpdate.AppendLine(" UPDATE N0000MS_NA_SisanZigyousho ");
            //申告自治体コード
            sqlUpdate.AppendLine(" SET    N0000MS_NA_SisanZigyousho.ZititaiCD = ");
            sqlUpdate.AppendLine(String.IsNullOrEmpty(this.obic7TextStringZititaiCD.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringZititaiCD.Value));
            //郵便番号1
            sqlUpdate.AppendLine("        ,N0000MS_NA_SisanZigyousho.YuubinNO1 = ");
            sqlUpdate.AppendLine(String.IsNullOrEmpty(this.obic7TextStringPostCD1.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringPostCD1.Value));
            //郵便番号2
            sqlUpdate.AppendLine("        ,N0000MS_NA_SisanZigyousho.YuubinNO2 = ");
            sqlUpdate.AppendLine(String.IsNullOrEmpty(this.obic7TextStringPostCD2.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringPostCD2.Value));
            //住所1カナ
            sqlUpdate.AppendLine("        ,N0000MS_NA_SisanZigyousho.Juusho1Kana = ");
            sqlUpdate.AppendLine(String.IsNullOrEmpty(this.obic7TextStringAddress1Kana.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringAddress1Kana.Value));
            //住所2カナ
            sqlUpdate.AppendLine("        ,N0000MS_NA_SisanZigyousho.Juusho2Kana = ");
            sqlUpdate.AppendLine(String.IsNullOrEmpty(this.obic7TextStringAddress2Kana.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringAddress2Kana.Value));
            //住所1
            sqlUpdate.AppendLine("        ,N0000MS_NA_SisanZigyousho.Juusho1 = ");
            sqlUpdate.AppendLine(String.IsNullOrEmpty(this.obic7TextStringAddress1.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringAddress1.Value));
            //住所2
            sqlUpdate.AppendLine("        ,N0000MS_NA_SisanZigyousho.Juusho2 = ");
            sqlUpdate.AppendLine(String.IsNullOrEmpty(this.obic7TextStringAddress2.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringAddress2.Value));
            //電話番号1
            sqlUpdate.AppendLine("        ,N0000MS_NA_SisanZigyousho.TELNO1 = ");
            sqlUpdate.AppendLine(String.IsNullOrEmpty(this.obic7TextStringTelNo1.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringTelNo1.Value));
            //電話番号2
            sqlUpdate.AppendLine("        ,N0000MS_NA_SisanZigyousho.TELNO2 = ");
            sqlUpdate.AppendLine(String.IsNullOrEmpty(this.obic7TextStringTelNo2.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringTelNo2.Value));
            //電話番号3
            sqlUpdate.AppendLine("        ,N0000MS_NA_SisanZigyousho.TELNO3 = ");
            sqlUpdate.AppendLine(String.IsNullOrEmpty(this.obic7TextStringTelNo3.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringTelNo3.Value));
            //会社名
            sqlUpdate.AppendLine("        ,N0000MS_NA_SisanZigyousho.CompanyName = ");
            sqlUpdate.AppendLine(String.IsNullOrEmpty(this.obic7TextStringCompanyName.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringCompanyName.Value));
            //会社カナ名
            sqlUpdate.AppendLine("        ,N0000MS_NA_SisanZigyousho.CompanyKanaName = ");
            sqlUpdate.AppendLine(String.IsNullOrEmpty(this.obic7TextStringCompanyKanaName.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringCompanyKanaName.Value));
            //代表者氏名
            sqlUpdate.AppendLine("        ,N0000MS_NA_SisanZigyousho.DaihyoushaName = ");
            sqlUpdate.AppendLine(String.IsNullOrEmpty(this.obic7TextStringDaihyoushaName.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringDaihyoushaName.Value));
            //代表者カナ名
            sqlUpdate.AppendLine("        ,N0000MS_NA_SisanZigyousho.DaihyoushaKanaName = ");
            sqlUpdate.AppendLine(String.IsNullOrEmpty(this.obic7TextStringDaihyoushaKanaName.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringDaihyoushaKanaName.Value));
            //屋号
            sqlUpdate.AppendLine("        ,N0000MS_NA_SisanZigyousho.Yagou = ");
            sqlUpdate.AppendLine(String.IsNullOrEmpty(this.obic7TextStringYagou.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringYagou.Value));
            //事業種目
            // 2009/02/23 修正 OBIC T.Wakumoto 開始 Column名の変更
            //sqlUpdate.AppendLine("        ,N0000MS_NA_SisanZigyousho.Zigyoushumoku = ");
            sqlUpdate.AppendLine("        ,N0000MS_NA_SisanZigyousho.ZigyouShumoku = "); 
            // 2009/02/23 修正 OBIC T.Wakumoto 終了
            sqlUpdate.AppendLine(String.IsNullOrEmpty(this.obic7TextStringZigyoushumoku.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringZigyoushumoku.Value));
            //資本金
            sqlUpdate.AppendLine("        ,N0000MS_NA_SisanZigyousho.Sihonkin = ");
            sqlUpdate.AppendLine(ComTool.Function.SqlEscapeNumber(this.obic7TextNumberSihonkin.Value));
            //設立年月日
            sqlUpdate.AppendLine("        ,N0000MS_NA_SisanZigyousho.SeturituDate = ");
            sqlUpdate.AppendLine(String.IsNullOrEmpty(this.obic7TextDatetimeSeturituDate.Text) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextDatetimeSeturituDate.Text));
            //申告担当者氏名
            sqlUpdate.AppendLine("        ,N0000MS_NA_SisanZigyousho.SinkokuTantoushaName = ");
            sqlUpdate.AppendLine(String.IsNullOrEmpty(this.obic7TextStringSinkokuTantoushaName.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringSinkokuTantoushaName.Value));
            //申告担当者電話番号1
            sqlUpdate.AppendLine("        ,N0000MS_NA_SisanZigyousho.TantoushaTELNO1 = ");
            sqlUpdate.AppendLine(String.IsNullOrEmpty(this.obic7TextStringTantoushaTelNo1.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringTantoushaTelNo1.Value));
            //申告担当者電話番号2
            sqlUpdate.AppendLine("        ,N0000MS_NA_SisanZigyousho.TantoushaTELNO2 = ");
            sqlUpdate.AppendLine(String.IsNullOrEmpty(this.obic7TextStringTantoushaTelNo2.Value) ?
                            "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringTantoushaTelNo2.Value));
            //申告担当者電話番号3
            sqlUpdate.AppendLine("        ,N0000MS_NA_SisanZigyousho.TantoushaTELNO3 = ");
            sqlUpdate.AppendLine(String.IsNullOrEmpty(this.obic7TextStringTantoushaTelNo3.Value) ?
                            "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringTantoushaTelNo3.Value));
            //会計事務所名
            sqlUpdate.AppendLine("        ,N0000MS_NA_SisanZigyousho.KaikeiZimushoName = ");
            sqlUpdate.AppendLine(String.IsNullOrEmpty(this.obic7TextStringKaikeiZimushoName.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringKaikeiZimushoName.Value));
            //会計事務所電話番号1
            sqlUpdate.AppendLine("        ,N0000MS_NA_SisanZigyousho.KaikeiZimushoTELNO1 = ");
            sqlUpdate.AppendLine(String.IsNullOrEmpty(this.obic7TextStringKaikeiZimushoTelNo1.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringKaikeiZimushoTelNo1.Value));
            //会計事務所電話番号2
            sqlUpdate.AppendLine("        ,N0000MS_NA_SisanZigyousho.KaikeiZimushoTELNO2 = ");
            sqlUpdate.AppendLine(String.IsNullOrEmpty(this.obic7TextStringKaikeiZimushoTelNo2.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringKaikeiZimushoTelNo2.Value));
            //会計事務所電話番号3
            sqlUpdate.AppendLine("        ,N0000MS_NA_SisanZigyousho.KaikeiZimushoTELNO3 = ");
            sqlUpdate.AppendLine(String.IsNullOrEmpty(this.obic7TextStringKaikeiZimushoTelNo3.Value) ?
                "NULL" : ComTool.Function.SqlEscapeVarchar(this.obic7TextStringKaikeiZimushoTelNo3.Value));
            //会社No
            sqlUpdate.AppendLine("        ,N0000MS_NA_SisanZigyousho.CompanyNO = ");
            sqlUpdate.AppendLine(ComTool.Function.SqlEscapeNumber(ComToolLogic.CurrentUser.CompanyNO));
            //更新日時
            sqlUpdate.AppendLine("        ,N0000MS_NA_SisanZigyousho.UpdateDatetime = ");
            sqlUpdate.AppendLine(" GETDATE() ");
            //更新認証済Guid
            sqlUpdate.AppendLine("        ,N0000MS_NA_SisanZigyousho.UpdateCertificatedGuid = ");
            sqlUpdate.AppendLine(ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.CertificatedGuid));
            //更新JobID
            sqlUpdate.AppendLine("        ,N0000MS_NA_SisanZigyousho.UpdateJobID = ");
            sqlUpdate.AppendLine(ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.JobID));
            //更新ユーザID
            sqlUpdate.AppendLine("        ,N0000MS_NA_SisanZigyousho.UpdateUserID = ");
            sqlUpdate.AppendLine(ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.UserID));
            //更新PC名
            sqlUpdate.AppendLine("        ,N0000MS_NA_SisanZigyousho.UpdateComputerName = ");
            sqlUpdate.AppendLine(ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.ComputerName));
            //更新PCユーザ名
            sqlUpdate.AppendLine("        ,N0000MS_NA_SisanZigyousho.UpdateComputerUserName = ");
            sqlUpdate.AppendLine(ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.ComputerUserName));
            //更新PCドメイン名
            sqlUpdate.AppendLine("        ,N0000MS_NA_SisanZigyousho.UpdateComputerDomainName = ");
            sqlUpdate.AppendLine(ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.ComputerDomainName));
            sqlUpdate.AppendLine("        WHERE N0000MS_NA_SisanZigyousho.ZigyoushoCD = ");
            sqlUpdate.AppendLine(ComTool.Function.SqlEscapeVarchar(this.obic7TextStringZigyoushoCD.Value));
            sqlUpdate.AppendLine("        AND N0000MS_NA_SisanZigyousho.UpdateTimestamp = ");
            sqlUpdate.AppendLine(ComTool.Function.SqlEscapeBinary(this.timestamp));

            return sqlUpdate.ToString();
        }

        // 2026/10/11 追加 (5525)事業所別償却資産申告対応 開始
        /// SQL文生成（事業所別地方税申告情報マスタ SELECT用）
        /// <summary>
        /// SQL文生成（事業所別地方税申告情報マスタ SELECT用）
        /// </summary>
        /// <returns>生成したSQL文（SELECT用）を返却</returns>
        private string MakeSQLSelectTihouzei()
        {
            StringBuilder sqlSelect = new StringBuilder();
            sqlSelect.AppendLine(" SELECT ");
            sqlSelect.AppendLine("        N5525MS_NA_ZigyoushoSinkokuJouhou.NouzeishaCD ");
            sqlSelect.AppendLine("       ,N5525MS_NA_ZigyoushoSinkokuJouhou.RiyoushaID ");
            sqlSelect.AppendLine("       ,N5525MS_NA_ZigyoushoSinkokuJouhou.DaihyoushaSikaku ");
            sqlSelect.AppendLine("       ,N5525MS_NA_ZigyoushoSinkokuJouhou.DaihyoushaYuubinNO1 ");
            sqlSelect.AppendLine("       ,N5525MS_NA_ZigyoushoSinkokuJouhou.DaihyoushaYuubinNO2 ");
            sqlSelect.AppendLine("       ,N5525MS_NA_ZigyoushoSinkokuJouhou.DaihyoushaJuusho1 ");
            sqlSelect.AppendLine("       ,N5525MS_NA_ZigyoushoSinkokuJouhou.DaihyoushaJuusho2 ");
            sqlSelect.AppendLine("       ,N5525MS_NA_ZigyoushoSinkokuJouhou.DaihyoushaTELNO1 ");
            sqlSelect.AppendLine("       ,N5525MS_NA_ZigyoushoSinkokuJouhou.DaihyoushaTELNO2 ");
            sqlSelect.AppendLine("       ,N5525MS_NA_ZigyoushoSinkokuJouhou.DaihyoushaTELNO3 ");
            sqlSelect.AppendLine("       ,N5525MS_NA_ZigyoushoSinkokuJouhou.UpdateTimestamp ");
            sqlSelect.AppendLine(" FROM N5525MS_NA_ZigyoushoSinkokuJouhou ");
            sqlSelect.AppendLine(" WHERE N5525MS_NA_ZigyoushoSinkokuJouhou.ZigyoushoCD = " + ComTool.Function.SqlEscapeVarchar(this.obic7TextStringZigyoushoCD.Value) + " ");
            return sqlSelect.ToString();
        }

        /// SQL文生成（事業所別地方税申告情報マスタ DELETE用）
        /// <summary>
        /// SQL文生成（事業所別地方税申告情報マスタ DELETE用）
        /// </summary>
        /// <returns>生成したSQL文（DELETE用）を返却</returns>
        private string MakeSQLDeleteTihouzei()
        {
            StringBuilder sqlDelete = new StringBuilder();
            sqlDelete.AppendLine(" DELETE FROM N5525MS_NA_ZigyoushoSinkokuJouhou ");
            sqlDelete.AppendLine(" WHERE N5525MS_NA_ZigyoushoSinkokuJouhou.ZigyoushoCD = " + ComTool.Function.SqlEscapeVarchar(this.obic7TextStringZigyoushoCD.Value) + " ");
            sqlDelete.AppendLine("   AND N5525MS_NA_ZigyoushoSinkokuJouhou.UpdateTimestamp = " + ComTool.Function.SqlEscapeBinary(this.timestampTihouzei) + " ");
            return sqlDelete.ToString();
        }

        /// SQL文生成（事業所別地方税申告情報マスタ INSERT用）
        /// <summary>
        /// SQL文生成（事業所別地方税申告情報マスタ INSERT用）
        /// </summary>
        /// <returns>生成したSQL文（INSERT用）を返却</returns>
        private string MakeSQLInsertTihouzei()
        {
            StringBuilder sqlInsert = new StringBuilder();
            sqlInsert.AppendLine(" INSERT INTO N5525MS_NA_ZigyoushoSinkokuJouhou ( ");
            sqlInsert.AppendLine("        ZigyoushoCD ");
            sqlInsert.AppendLine("       ,NouzeishaCD ");
            sqlInsert.AppendLine("       ,RiyoushaID ");
            sqlInsert.AppendLine("       ,DaihyoushaSikaku ");
            sqlInsert.AppendLine("       ,DaihyoushaYuubinNO1 ");
            sqlInsert.AppendLine("       ,DaihyoushaYuubinNO2 ");
            sqlInsert.AppendLine("       ,DaihyoushaJuusho1 ");
            sqlInsert.AppendLine("       ,DaihyoushaJuusho2 ");
            sqlInsert.AppendLine("       ,DaihyoushaTELNO1 ");
            sqlInsert.AppendLine("       ,DaihyoushaTELNO2 ");
            sqlInsert.AppendLine("       ,DaihyoushaTELNO3 ");
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
            //納税者コード
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(ToSqlVarcharOrNull(this.obic7TextStringNouzeishaCD.Value));
            //利用者ID
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(ToSqlVarcharOrNull(this.obic7TextStringRiyoushaID.Value));
            //代表者資格
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(ComTool.Function.SqlEscapeNumber(GetDaihyoushaSikaku()));
            //代表者郵便番号1
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(ToSqlVarcharOrNull(this.obic7TextStringDaihyoushaPostCD1.Value));
            //代表者郵便番号2
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(ToSqlVarcharOrNull(this.obic7TextStringDaihyoushaPostCD2.Value));
            //代表者住所1
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(ToSqlVarcharOrNull(this.obic7TextStringDaihyoushaAddress1.Value));
            //代表者住所2
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(ToSqlVarcharOrNull(this.obic7TextStringDaihyoushaAddress2.Value));
            //代表者電話番号1
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(ToSqlVarcharOrNull(this.obic7TextStringDaihyoushaTelNo1.Value));
            //代表者電話番号2
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(ToSqlVarcharOrNull(this.obic7TextStringDaihyoushaTelNo2.Value));
            //代表者電話番号3
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(ToSqlVarcharOrNull(this.obic7TextStringDaihyoushaTelNo3.Value));
            //会社No
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(ComTool.Function.SqlEscapeNumber(ComToolLogic.CurrentUser.CompanyNO));
            //更新日時
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(" GETDATE() ");
            //更新認証済Guid
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.CertificatedGuid));
            //更新JobID
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.JobID));
            //更新ユーザID
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.UserID));
            //更新PC名
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.ComputerName));
            //更新PCユーザ名
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.ComputerUserName));
            //更新PCドメイン名
            sqlInsert.AppendLine(" , ");
            sqlInsert.AppendLine(ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.ComputerDomainName));
            sqlInsert.AppendLine("        WHERE NOT EXISTS ( ");
            sqlInsert.AppendLine("                SELECT N5525MS_NA_ZigyoushoSinkokuJouhou.ZigyoushoCD ");
            sqlInsert.AppendLine("                FROM   N5525MS_NA_ZigyoushoSinkokuJouhou ");
            sqlInsert.AppendLine("                WHERE  N5525MS_NA_ZigyoushoSinkokuJouhou.ZigyoushoCD = ");
            sqlInsert.AppendLine(ComTool.Function.SqlEscapeVarchar(this.obic7TextStringZigyoushoCD.Value));
            sqlInsert.AppendLine(" ) ");
            return sqlInsert.ToString();
        }

        /// SQL文生成（事業所別地方税申告情報マスタ UPDATE用）
        /// <summary>
        /// SQL文生成（事業所別地方税申告情報マスタ UPDATE用）
        /// </summary>
        /// <returns>生成したSQL文（UPDATE用）を返却</returns>
        private string MakeSQLUpdateTihouzei()
        {
            StringBuilder sqlUpdate = new StringBuilder();
            sqlUpdate.AppendLine(" UPDATE N5525MS_NA_ZigyoushoSinkokuJouhou ");
            sqlUpdate.AppendLine(" SET    NouzeishaCD = " + ToSqlVarcharOrNull(this.obic7TextStringNouzeishaCD.Value));
            sqlUpdate.AppendLine("       ,RiyoushaID = " + ToSqlVarcharOrNull(this.obic7TextStringRiyoushaID.Value));
            sqlUpdate.AppendLine("       ,DaihyoushaSikaku = " + ComTool.Function.SqlEscapeNumber(GetDaihyoushaSikaku()));
            sqlUpdate.AppendLine("       ,DaihyoushaYuubinNO1 = " + ToSqlVarcharOrNull(this.obic7TextStringDaihyoushaPostCD1.Value));
            sqlUpdate.AppendLine("       ,DaihyoushaYuubinNO2 = " + ToSqlVarcharOrNull(this.obic7TextStringDaihyoushaPostCD2.Value));
            sqlUpdate.AppendLine("       ,DaihyoushaJuusho1 = " + ToSqlVarcharOrNull(this.obic7TextStringDaihyoushaAddress1.Value));
            sqlUpdate.AppendLine("       ,DaihyoushaJuusho2 = " + ToSqlVarcharOrNull(this.obic7TextStringDaihyoushaAddress2.Value));
            sqlUpdate.AppendLine("       ,DaihyoushaTELNO1 = " + ToSqlVarcharOrNull(this.obic7TextStringDaihyoushaTelNo1.Value));
            sqlUpdate.AppendLine("       ,DaihyoushaTELNO2 = " + ToSqlVarcharOrNull(this.obic7TextStringDaihyoushaTelNo2.Value));
            sqlUpdate.AppendLine("       ,DaihyoushaTELNO3 = " + ToSqlVarcharOrNull(this.obic7TextStringDaihyoushaTelNo3.Value));
            sqlUpdate.AppendLine("       ,CompanyNO = " + ComTool.Function.SqlEscapeNumber(ComToolLogic.CurrentUser.CompanyNO));
            sqlUpdate.AppendLine("       ,UpdateDatetime = GETDATE() ");
            sqlUpdate.AppendLine("       ,UpdateCertificatedGuid = " + ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.CertificatedGuid));
            sqlUpdate.AppendLine("       ,UpdateJobID = " + ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.JobID));
            sqlUpdate.AppendLine("       ,UpdateUserID = " + ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.UserID));
            sqlUpdate.AppendLine("       ,UpdateComputerName = " + ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.ComputerName));
            sqlUpdate.AppendLine("       ,UpdateComputerUserName = " + ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.ComputerUserName));
            sqlUpdate.AppendLine("       ,UpdateComputerDomainName = " + ComTool.Function.SqlEscapeVarchar(ComToolLogic.CurrentUser.ComputerDomainName));
            sqlUpdate.AppendLine(" WHERE  ZigyoushoCD = " + ComTool.Function.SqlEscapeVarchar(this.obic7TextStringZigyoushoCD.Value));
            sqlUpdate.AppendLine("   AND  UpdateTimestamp = " + ComTool.Function.SqlEscapeBinary(this.timestampTihouzei));
            return sqlUpdate.ToString();
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

        /// 代表者資格の取得（未入力は既定値[01:代表者]）
        /// <summary>
        /// 代表者資格の取得（未入力は既定値[01:代表者]）
        /// </summary>
        /// <returns>代表者資格</returns>
        private byte GetDaihyoushaSikaku()
        {
            byte daihyoushaSikaku;
            if (byte.TryParse(this.obic7TextStringDaihyoushaSikaku.Value, out daihyoushaSikaku))
            {
                return daihyoushaSikaku;
            }
            return DAIHYOUSHA_SIKAKU_DEFAULT;
        }

        /// 代表者資格名称の取得
        /// <summary>
        /// 代表者資格名称の取得（固定資産コントロールマスタの代表者資格と同じ区分）
        /// </summary>
        /// <param name="daihyoushaSikaku">代表者資格</param>
        /// <returns>名称(区分外の場合はnull)</returns>
        private string GetDaihyoushaSikakuName(byte daihyoushaSikaku)
        {
            switch (daihyoushaSikaku)
            {
                case 1:
                    return "代表者";
                case 2:
                    return "破産管財人";
                case 3:
                    return "清算人";
                case 99:
                    return "その他";
                default:
                    return null;
            }
        }

        /// 地方税申告(連携)項目の入力有無
        /// <summary>
        /// 地方税申告(連携)項目の入力有無
        /// </summary>
        /// <returns>true:いずれかの項目が入力されている　false:すべて未入力</returns>
        private bool IsInputTihouzei()
        {
            return !String.IsNullOrEmpty(this.obic7TextStringNouzeishaCD.Value)
                || !String.IsNullOrEmpty(this.obic7TextStringRiyoushaID.Value)
                || !String.IsNullOrEmpty(this.obic7TextStringDaihyoushaSikaku.Value)
                || !String.IsNullOrEmpty(this.obic7TextStringDaihyoushaPostCD1.Value)
                || !String.IsNullOrEmpty(this.obic7TextStringDaihyoushaPostCD2.Value)
                || !String.IsNullOrEmpty(this.obic7TextStringDaihyoushaAddress1.Value)
                || !String.IsNullOrEmpty(this.obic7TextStringDaihyoushaAddress2.Value)
                || !String.IsNullOrEmpty(this.obic7TextStringDaihyoushaTelNo1.Value)
                || !String.IsNullOrEmpty(this.obic7TextStringDaihyoushaTelNo2.Value)
                || !String.IsNullOrEmpty(this.obic7TextStringDaihyoushaTelNo3.Value);
        }

        /// 事業所別地方税申告情報マスタの更新SQL文を取得
        /// <summary>
        /// 事業所別地方税申告情報マスタの更新SQL文を取得
        /// 入力あり・レコードあり：UPDATE、入力あり・レコードなし：INSERT
        /// 入力なし・レコードあり：DELETE、入力なし・レコードなし：更新なし(null)
        /// </summary>
        /// <returns>SQL文(更新不要の場合はnull)</returns>
        private string MakeSQLRegisterTihouzei()
        {
            if (IsInputTihouzei())
            {
                return (this.timestampTihouzei == null) ? MakeSQLInsertTihouzei() : MakeSQLUpdateTihouzei();
            }
            return (this.timestampTihouzei == null) ? null : MakeSQLDeleteTihouzei();
        }

        /// 事業所別地方税申告情報マスタのデータを画面に表示
        /// <summary>
        /// 事業所別地方税申告情報マスタのデータを画面に表示
        /// </summary>
        private void SetTihouzeiData()
        {
            using (DataTable tihouzeiData = this.businessDB.GetDataTable(MakeSQLSelectTihouzei()))
            {
                if (!ComTool.Function.IsValidDataTable(tihouzeiData))
                {
                    this.timestampTihouzei = null;
                    return;
                }
                DataRow row = tihouzeiData.Rows[0];
                this.obic7TextStringNouzeishaCD.Value = ComTool.Function.AnyToString(row["NouzeishaCD"]);
                this.obic7TextStringRiyoushaID.Value = ComTool.Function.AnyToString(row["RiyoushaID"]);
                byte daihyoushaSikaku = Convert.ToByte(row["DaihyoushaSikaku"]);
                this.obic7TextStringDaihyoushaSikaku.Value = daihyoushaSikaku.ToString("00");
                this.obic7LabelStringDaihyoushaSikakuName.Value = GetDaihyoushaSikakuName(daihyoushaSikaku);
                this.obic7TextStringDaihyoushaPostCD1.Value = ComTool.Function.AnyToString(row["DaihyoushaYuubinNO1"]);
                this.obic7TextStringDaihyoushaPostCD2.Value = ComTool.Function.AnyToString(row["DaihyoushaYuubinNO2"]);
                this.obic7TextStringDaihyoushaAddress1.Value = ComTool.Function.AnyToString(row["DaihyoushaJuusho1"]);
                this.obic7TextStringDaihyoushaAddress2.Value = ComTool.Function.AnyToString(row["DaihyoushaJuusho2"]);
                this.obic7TextStringDaihyoushaTelNo1.Value = ComTool.Function.AnyToString(row["DaihyoushaTELNO1"]);
                this.obic7TextStringDaihyoushaTelNo2.Value = ComTool.Function.AnyToString(row["DaihyoushaTELNO2"]);
                this.obic7TextStringDaihyoushaTelNo3.Value = ComTool.Function.AnyToString(row["DaihyoushaTELNO3"]);
                this.timestampTihouzei = (byte[])row["UpdateTimestamp"];
            }
        }

        /// 代表者郵便番号[郵便番号1]+[郵便番号2]を○○○－○○○○形式に変換
        /// <summary>
        /// 代表者郵便番号[郵便番号1]+[郵便番号2]を○○○－○○○○形式に変換
        /// </summary>
        /// <returns>変換した郵便番号</returns>
        private string CreateDaihyoushaPostNo()
        {
            if (String.IsNullOrEmpty(this.obic7TextStringDaihyoushaPostCD1.Text))
            {
                return "";
            }
            if (String.IsNullOrEmpty(this.obic7TextStringDaihyoushaPostCD2.Text))
            {
                return this.obic7TextStringDaihyoushaPostCD1.Text;
            }
            return String.Concat(this.obic7TextStringDaihyoushaPostCD1.Text, "-", this.obic7TextStringDaihyoushaPostCD2.Text);
        }
        // 2026/10/11 追加 (5525)事業所別償却資産申告対応 終了

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
            //事務所コード_検索ボタン
            this.obic7ButtonWithLogicZigyoushoCDSearch.Enabled = isEnabled;
            //表示ボタン
            this.obic7ButtonWithLogicDisp.Enabled = isEnabled;

            //処理モードの設定値
            if (this.obic7LabelStringMode.Value != isNeedFormatMode)
            {
                //処理モードを設定値に
                this.obic7LabelStringMode.Value = isNeedFormatMode;
            }

            //必要に応じクリア
            if (isNeedClear == true)
            {
                //基準日
                this.obic7TextDatetimeBaseDate.Value = null;
                //事業所コード
                this.obic7TextStringZigyoushoCD.Value = "";
                //事業所名
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
            //基本情報
            //申告自治体コード
            this.obic7TextStringZititaiCD.Enabled = isEnabled;
            //申告自治体コード_検索ボタン
            this.obic7ButtonWithLogicZititaiCDSearch.Enabled = isEnabled;

            //償却資産申告書項目
            //郵便番号1
            this.obic7TextStringPostCD1.Enabled = isEnabled;
            //郵便番号2
            this.obic7TextStringPostCD2.Enabled = isEnabled;
            //郵便番号_検索ボタン
            this.obic7ButtonWithLogicPostCDSearch.Enabled = isEnabled;
            //住所1
            this.obic7TextStringAddress1.Enabled = isEnabled;
            //住所2
            this.obic7TextStringAddress2.Enabled = isEnabled;
            //住所1カナ
            this.obic7TextStringAddress1Kana.Enabled = isEnabled;
            //住所2カナ
            this.obic7TextStringAddress2Kana.Enabled = isEnabled;
            //電話番号1
            this.obic7TextStringTelNo1.Enabled = isEnabled;
            //電話番号2
            this.obic7TextStringTelNo2.Enabled = isEnabled;
            //電話番号3
            this.obic7TextStringTelNo3.Enabled = isEnabled;
            //会社名
            this.obic7TextStringCompanyName.Enabled = isEnabled;
            //会社カナ名
            this.obic7TextStringCompanyKanaName.Enabled = isEnabled;
            //代表者氏名
            this.obic7TextStringDaihyoushaName.Enabled = isEnabled;
            //代表者カナ名
            this.obic7TextStringDaihyoushaKanaName.Enabled = isEnabled;
            //屋号
            this.obic7TextStringYagou.Enabled = isEnabled;
            //事業種目
            this.obic7TextStringZigyoushumoku.Enabled = isEnabled;
            //資本金
            this.obic7TextNumberSihonkin.Enabled = isEnabled;
            //設立年月日
            this.obic7TextDatetimeSeturituDate.Enabled = isEnabled;
            //申告担当者名
            this.obic7TextStringSinkokuTantoushaName.Enabled = isEnabled;
            //担当者電話番号1
            this.obic7TextStringTantoushaTelNo1.Enabled = isEnabled;
            //担当者電話番号2
            this.obic7TextStringTantoushaTelNo2.Enabled = isEnabled;
            //担当者電話番号3
            this.obic7TextStringTantoushaTelNo3.Enabled = isEnabled;
            //会計事務所名
            this.obic7TextStringKaikeiZimushoName.Enabled = isEnabled;
            //会計事務所電話番号1
            this.obic7TextStringKaikeiZimushoTelNo1.Enabled = isEnabled;
            //会計事務所電話番号2
            this.obic7TextStringKaikeiZimushoTelNo2.Enabled = isEnabled;
            //会計事務所電話番号3
            this.obic7TextStringKaikeiZimushoTelNo3.Enabled = isEnabled;
            // 2026/10/11 追加 (5525)事業所別償却資産申告対応 開始
            //地方税申告(連携)
            this.obic7TextStringNouzeishaCD.Enabled = isEnabled;
            this.obic7TextStringRiyoushaID.Enabled = isEnabled;
            this.obic7TextStringDaihyoushaSikaku.Enabled = isEnabled;
            this.obic7TextStringDaihyoushaPostCD1.Enabled = isEnabled;
            this.obic7TextStringDaihyoushaPostCD2.Enabled = isEnabled;
            this.obic7ButtonWithLogicDaihyoushaPostCDSearch.Enabled = isEnabled;
            this.obic7TextStringDaihyoushaAddress1.Enabled = isEnabled;
            this.obic7TextStringDaihyoushaAddress2.Enabled = isEnabled;
            this.obic7TextStringDaihyoushaTelNo1.Enabled = isEnabled;
            this.obic7TextStringDaihyoushaTelNo2.Enabled = isEnabled;
            this.obic7TextStringDaihyoushaTelNo3.Enabled = isEnabled;
            // 2026/10/11 追加 (5525)事業所別償却資産申告対応 終了

            if (isNeedClear)
            {
                //基本情報
                //申告自治体コード
                this.obic7TextStringZititaiCD.Value = "";
                //申告自治体名
                this.obic7LabelStringZititaiName.Value = "";
                //償却資産申告書項目
                //郵便番号1
                this.obic7TextStringPostCD1.Value = "";
                //郵便番号2
                this.obic7TextStringPostCD2.Value = "";
                //住所1
                this.obic7TextStringAddress1.Value = "";
                //住所2
                this.obic7TextStringAddress2.Value = "";
                //住所1カナ
                this.obic7TextStringAddress1Kana.Value = "";
                //住所2カナ
                this.obic7TextStringAddress2Kana.Value = "";
                //電話番号1
                this.obic7TextStringTelNo1.Value = "";
                //電話番号2
                this.obic7TextStringTelNo2.Value = "";
                //電話番号3
                this.obic7TextStringTelNo3.Value = "";
                //会社名
                this.obic7TextStringCompanyName.Value = "";
                //会社カナ名
                this.obic7TextStringCompanyKanaName.Value = "";
                //代表者氏名
                this.obic7TextStringDaihyoushaName.Value = "";
                //代表者カナ名
                this.obic7TextStringDaihyoushaKanaName.Value = "";
                //屋号
                this.obic7TextStringYagou.Value = "";
                //事業種目
                this.obic7TextStringZigyoushumoku.Value = "";
                //資本金
                this.obic7TextNumberSihonkin.Value = null;
                //設立年月日
                this.obic7TextDatetimeSeturituDate.Value = null;
                //申告担当者名
                this.obic7TextStringSinkokuTantoushaName.Value = "";
                //担当者電話番号1
                this.obic7TextStringTantoushaTelNo1.Value = "";
                //担当者電話番号2
                this.obic7TextStringTantoushaTelNo2.Value = "";
                //担当者電話番号3
                this.obic7TextStringTantoushaTelNo3.Value = "";
                //会計事務所名
                this.obic7TextStringKaikeiZimushoName.Value = "";
                //会計事務所電話番号1
                this.obic7TextStringKaikeiZimushoTelNo1.Value = "";
                //会計事務所電話番号2
                this.obic7TextStringKaikeiZimushoTelNo2.Value = "";
                //会計事務所電話番号3
                this.obic7TextStringKaikeiZimushoTelNo3.Value = "";
                //タイムスタンプ
                this.timestamp = null;
                // 2026/10/11 追加 (5525)事業所別償却資産申告対応 開始
                //地方税申告(連携)
                this.obic7TextStringNouzeishaCD.Value = "";
                this.obic7TextStringRiyoushaID.Value = "";
                this.obic7TextStringDaihyoushaSikaku.Value = "";
                this.obic7LabelStringDaihyoushaSikakuName.Value = "";
                this.obic7TextStringDaihyoushaPostCD1.Value = "";
                this.obic7TextStringDaihyoushaPostCD2.Value = "";
                this.obic7TextStringDaihyoushaAddress1.Value = "";
                this.obic7TextStringDaihyoushaAddress2.Value = "";
                this.obic7TextStringDaihyoushaTelNo1.Value = "";
                this.obic7TextStringDaihyoushaTelNo2.Value = "";
                this.obic7TextStringDaihyoushaTelNo3.Value = "";
                this.timestampTihouzei = null;
                // 2026/10/11 追加 (5525)事業所別償却資産申告対応 終了
            }
        }

        /// 取得したマスタデータの表示
        /// <summary>
        /// 取得したマスタデータの表示
        /// </summary>
        /// <param name="masterData">表示対象のDataTable</param>
        /// <param name="mode">処理モード</param>
        private void SetDetailData(DataTable masterData, string mode)
        {
            //処理モードにより処理を分岐
            switch (mode)
            {
                case MODE_INSERT:
                    //新規モード
                    //明細部分のEnabledをtrueにし、表示内容をクリア
                    SwitchDetail(true, true);
                    break;
                case MODE_UPDATE:
                    //修正モード
                    //ヘッダ部のEnabledをfalseに
                    SwitchHeader(false, false, mode);
                    //明細部分のEnabledをtrueにし、表示内容をクリア
                    SwitchDetail(true, true);
                    //データテーブルの値を明細部にセット
                    //申告自治体コード
                    this.obic7TextStringZititaiCD.Value = ComTool.Function.AnyToString(masterData.Rows[0]["ZititaiCD"]);
                    //申告自治体名
                    this.obic7LabelStringZititaiName.Value = ComTool.Function.AnyToString(masterData.Rows[0]["ZititaiName"]);
                    //郵便番号1
                    this.obic7TextStringPostCD1.Value = ComTool.Function.AnyToString(masterData.Rows[0]["YuubinNO1"]);
                    //郵便番号2
                    this.obic7TextStringPostCD2.Value = ComTool.Function.AnyToString(masterData.Rows[0]["YuubinNO2"]);
                    //住所1
                    this.obic7TextStringAddress1.Value = ComTool.Function.AnyToString(masterData.Rows[0]["Juusho1"]);
                    //住所2
                    this.obic7TextStringAddress2.Value = ComTool.Function.AnyToString(masterData.Rows[0]["Juusho2"]);
                    //住所1カナ
                    this.obic7TextStringAddress1Kana.Value = ComTool.Function.AnyToString(masterData.Rows[0]["Juusho1Kana"]);
                    //住所2カナ
                    this.obic7TextStringAddress2Kana.Value = ComTool.Function.AnyToString(masterData.Rows[0]["Juusho2Kana"]);
                    //電話番号1
                    this.obic7TextStringTelNo1.Value = ComTool.Function.AnyToString(masterData.Rows[0]["TELNO1"]);
                    //電話番号2
                    this.obic7TextStringTelNo2.Value = ComTool.Function.AnyToString(masterData.Rows[0]["TELNO2"]);
                    //電話番号3
                    this.obic7TextStringTelNo3.Value = ComTool.Function.AnyToString(masterData.Rows[0]["TELNO3"]);
                    //会社名
                    this.obic7TextStringCompanyName.Value = ComTool.Function.AnyToString(masterData.Rows[0]["CompanyName"]);
                    //会社カナ名
                    this.obic7TextStringCompanyKanaName.Value = ComTool.Function.AnyToString(masterData.Rows[0]["CompanyKanaName"]);
                    //代表者氏名
                    this.obic7TextStringDaihyoushaName.Value = ComTool.Function.AnyToString(masterData.Rows[0]["DaihyoushaName"]);
                    //代表者カナ名
                    this.obic7TextStringDaihyoushaKanaName.Value = ComTool.Function.AnyToString(masterData.Rows[0]["DaihyoushaKanaName"]);
                    //屋号
                    this.obic7TextStringYagou.Value = ComTool.Function.AnyToString(masterData.Rows[0]["Yagou"]);
                    //事業種目
                    // 2009/02/23 修正 OBIC T.Wakumoto 開始 Column名を変更
                    //this.obic7TextStringZigyoushumoku.Value = ComTool.Function.AnyToString(masterData.Rows[0]["Zigyoushumoku"]);
                    this.obic7TextStringZigyoushumoku.Value = ComTool.Function.AnyToString(masterData.Rows[0]["ZigyouShumoku"]); 
                    // 2009/02/23 修正 OBIC T.Wakumoto 終了
                    //資本金
                    this.obic7TextNumberSihonkin.Value = ComTool.Function.AnyToDecimal(masterData.Rows[0]["Sihonkin"]);
                    //設立年月日
                    this.obic7TextDatetimeSeturituDate.Value = masterData.Rows[0]["SeturituDate"] as DateTime?;
                    //申告担当者氏名
                    this.obic7TextStringSinkokuTantoushaName.Value = ComTool.Function.AnyToString(masterData.Rows[0]["SinkokuTantoushaName"]);
                    //申告担当者電話番号1
                    this.obic7TextStringTantoushaTelNo1.Value = ComTool.Function.AnyToString(masterData.Rows[0]["TantoushaTELNO1"]);
                    //申告担当者電話番号2
                    this.obic7TextStringTantoushaTelNo2.Value = ComTool.Function.AnyToString(masterData.Rows[0]["TantoushaTELNO2"]);
                    //申告担当者電話番号3
                    this.obic7TextStringTantoushaTelNo3.Value = ComTool.Function.AnyToString(masterData.Rows[0]["TantoushaTELNO3"]);
                    //会計事務所名
                    this.obic7TextStringKaikeiZimushoName.Value = ComTool.Function.AnyToString(masterData.Rows[0]["KaikeiZimushoName"]);
                    //会計事務所電話番号1
                    this.obic7TextStringKaikeiZimushoTelNo1.Value = ComTool.Function.AnyToString(masterData.Rows[0]["KaikeiZimushoTELNO1"]);
                    //会計事務所電話番号2
                    this.obic7TextStringKaikeiZimushoTelNo2.Value = ComTool.Function.AnyToString(masterData.Rows[0]["KaikeiZimushoTELNO2"]);
                    //会計事務所電話場号3
                    this.obic7TextStringKaikeiZimushoTelNo3.Value = ComTool.Function.AnyToString(masterData.Rows[0]["KaikeiZimushoTELNO3"]);
                    //タイムスタンプ
                    // 2008/12/15    変更    T.Wakumoto 開始    thisの追加
                    //timestamp = (byte[])masterData.Rows[0]["UpdateTimestamp"];
                    this.timestamp = (byte[])masterData.Rows[0]["UpdateTimestamp"];
                    // 2008/12/15    変更    T.Wakumoto 終了

                    break;
            }
        }

        /// 基準日の取得
        /// <summary>
        /// 基準日の取得
        /// </summary>
        /// <returns>基準日にセットする日付</returns>
        private string BaseDateSet()
        {
            //基準日格納用
            string baseDate = "";

            //基準日の取得
            using (DataTable sisanData = N0000SearchCom.N0000MS_NA_KoteiSisanControl.SelectByKey(0, ""))
            {
                //2008/12/22 変更    T.Wakumoto 開始   固定資産コントロールマスタの基準日がNULLだった場合を考慮
                ////基準日の格納
                //baseDate = ComTool.Function.AnyToString(sisanData.Rows[0]["KijunDate"]);
                ////基準日の取得チェック
                //if (!String.IsNullOrEmpty(baseDate))
                //{
                //    //基準日が存在する場合、基準日を返却
                //    return baseDate;
                //}
                //else
                //{
                //    //システム日付を取得
                //    baseDate = ComTool.Function.AnyToString(this.businessDB.ExecuteScalar("SELECT GETDATE()"));
                //    //システム日付を返却
                //    return baseDate;
                //}
                //基準日のチェック
                if (!ComTool.Function.IsValidDataTable(sisanData) ||
                    (String.IsNullOrEmpty(sisanData.Rows[0]["KijunDate"].ToString())))
                {
                    //システム日付を取得
                    baseDate = ComTool.Function.AnyToString(businessDB.ExecuteScalar("SELECT GETDATE()"));
                    return baseDate;
                }
                else
                {
                    //基準日のセット
                    baseDate = sisanData.Rows[0]["KijunDate"].ToString();
                    return baseDate;
                }
                // 2008/12/22 変更    T.Wakumoto 終了
            }
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
            //メッセージクラスをインスタンス化
            ComToolLogic.Message message = new ComToolLogic.Message();
            //メッセージを取得
            message.GetMessage(SYS_BUNRUI, MESSAGE_SEQ, MESSAGECD1, messageCD2, messageParameters, null);
            //エラープロバイダの表示
            WinToolUI.Obic7ErrorProvider.SetError(control, message.MessageSummary);
        }

        /// 郵便番号[郵便番号1]+[郵便番号2]を○○○－○○○○形式に変換
        /// <summary>
        /// 郵便番号[郵便番号1]+[郵便番号2]を○○○－○○○○形式に変換
        /// </summary>
        /// <returns>変換した郵便番号</returns>
        private string CreatePostNo()
        {
            //戻り値の郵便番号格納用
            string postNo = "";
            //郵便番号の入力チェック
            if (String.IsNullOrEmpty(this.obic7TextStringPostCD2.Text))
            {
                if (!String.IsNullOrEmpty(this.obic7TextStringPostCD1.Text))
                {
                    //郵便番号2が未入力の場合、郵便番号1を返す
                    postNo = this.obic7TextStringPostCD1.Text;
                }
            }
            else if (!String.IsNullOrEmpty(this.obic7TextStringPostCD1.Text))
            {
                //郵便番号1,2が入力されている場合、郵便番号[郵便番号1]+[郵便番号2]を○○○－○○○○形式に変換
                postNo = String.Concat(this.obic7TextStringPostCD1.Text, "-", this.obic7TextStringPostCD2.Text);
            }
            //郵便番号を返す
            return postNo;
        }

        /// F4(検索)処理
        /// <summary>
        /// F4(検索)処理
        /// </summary>
        private void F4Action()
        {
            //検索実行の有無
            bool isNotSearch = false;
            //コントロール判別用
            WinToolUI.Obic7TextAbstract textAbstract = null;
            //テキストボックスに変換
            textAbstract = this.PrevControl as WinToolUI.Obic7TextAbstract;
            if (textAbstract != null)
            {
                //ダイアログ種別設定
                switch (textAbstract.Name)
                {
                    //事業所コード検索
                    case "obic7TextStringZigyoushoCD":
                        using (M0000Search.MA_ZigyoushoCD zigyoushoCDdlg = new M0000Search.MA_ZigyoushoCD())
                        {
                            textAbstract.AttachedSearchDialog = zigyoushoCDdlg;
                            zigyoushoCDdlg.BasicDate = (DateTime)this.obic7TextDatetimeBaseDate.Value;
                            zigyoushoCDdlg.SecurityPatternCD = "";
                            zigyoushoCDdlg.AllSumKubun = 0;
                            zigyoushoCDdlg.AllZigyoushoKubun = 0;
                            zigyoushoCDdlg.NameNullKubun = 1;
                            //直前コントロールがTextBoxであればTextBoxのShowDialogメソッドを実行
                            if (textAbstract.ShowDialog() == DialogResult.OK)
                            {
                                // 2009/03/04 修正 OBIC T.Wakumoto 開始 表示ボタン押下時の処理に変更
                                ////OK押下時、次の項目へフォーカスを移動
                                //base.SelectNextInputControl(true);
                                //OK押下時、次の項目へフォーカスを移動
                                this.obic7ButtonWithLogicDisp.PerformClick();
                                // 2009/03/04 修正 OBIC T.Wakumoto 終了
                            }
                            else
                            {
                                isNotSearch = true;
                            }
                        }
                        break;
                    //申告自治体コード検索
                    case "obic7TextStringZititaiCD":
                        using (N0000Search.NA_ZititaiCD zititaiCDdlg = new N0000Search.NA_ZititaiCD())
                        {
                            textAbstract.AttachedSearchDialog = zititaiCDdlg;
                            //直前コントロールがTextBoxであればTextBoxのShowDialogメソッドを実行
                            if (textAbstract.ShowDialog() == DialogResult.OK)
                            {
                                //OK押下時、次の項目へフォーカスを移動
                                base.SelectNextInputControl(true);
                            }
                            else
                            {
                                isNotSearch = true;
                            }
                        }
                        break;
                    //住所検索ダイアログ
                    case "obic7TextStringPostCD1":
                    case "obic7TextStringPostCD2":
                        using (M0000Search.MA_JuushoSougou juushoSougou = new M0000Search.MA_JuushoSougou())
                        {
                            this.obic7TextStringPostCD1.AttachedSearchDialog = juushoSougou;
                            this.obic7TextStringPostCD2.AttachedSearchDialog = juushoSougou;

                            //住所総合検索の引数を設定
                            //処理パターン引数
                            juushoSougou.ShoriMode = 1;
                            //郵便番号
                            juushoSougou.YuubinNO = CreatePostNo();
                            if (juushoSougou.ShowDialog() == DialogResult.OK)
                            {
                                //検索ダイアログの値格納用（連番）
                                int renban = juushoSougou.Renban;
                                //検索ダイアログの値格納用（郵便番号）
                                string postNo = juushoSougou.YuubinNO;
                                //郵便番号が○○○－○○○○形式がどうかのチェック
                                if (postNo.IndexOf("-") >= 0)
                                {
                                    //[郵便番号]（○○○－○○○○形式）を分解し、[郵便番号1]、[郵便番号2]に表示
                                    this.obic7TextStringPostCD1.Value = postNo.Substring(0, postNo.IndexOf("-"));
                                    this.obic7TextStringPostCD2.Value = postNo.Substring(postNo.IndexOf("-") + 1);
                                }
                                else
                                {
                                    //○○○－○○○○形式では無い場合、郵便番号1に値をセット
                                    this.obic7TextStringPostCD1.Value = postNo;
                                    this.obic7TextStringPostCD2.Value = String.Empty;
                                    return;
                                }
                                //データを取得
                                using (DataTable juushozisho = M0000SearchCom.M0000MS_MA_Juushozisho.SelectByKey(renban, postNo, ""))
                                {
                                    if (ComTool.Function.IsValidDataTable(juushozisho))
                                    {
                                        this.obic7TextStringAddress1.Value = ComTool.Function.AnyToString(juushozisho.Rows[0]["Juusho"]);
                                    }
                                    //住所1にフォーカスをセット
                                    this.obic7TextStringAddress1.Focus();
                                }
                            }
                            else
                            {
                                //OK以外の値が戻された場合は検索実行なしとする
                                isNotSearch = true;
                            }
                        }
                        break;
                    // 2026/10/11 追加 (5525)事業所別償却資産申告対応 開始
                    //代表者住所検索ダイアログ
                    case "obic7TextStringDaihyoushaPostCD1":
                    case "obic7TextStringDaihyoushaPostCD2":
                        using (M0000Search.MA_JuushoSougou juushoSougou = new M0000Search.MA_JuushoSougou())
                        {
                            this.obic7TextStringDaihyoushaPostCD1.AttachedSearchDialog = juushoSougou;
                            this.obic7TextStringDaihyoushaPostCD2.AttachedSearchDialog = juushoSougou;

                            //住所総合検索の引数を設定
                            juushoSougou.ShoriMode = 1;
                            juushoSougou.YuubinNO = CreateDaihyoushaPostNo();
                            if (juushoSougou.ShowDialog() == DialogResult.OK)
                            {
                                int renban = juushoSougou.Renban;
                                string postNo = juushoSougou.YuubinNO;
                                if (postNo.IndexOf("-") >= 0)
                                {
                                    this.obic7TextStringDaihyoushaPostCD1.Value = postNo.Substring(0, postNo.IndexOf("-"));
                                    this.obic7TextStringDaihyoushaPostCD2.Value = postNo.Substring(postNo.IndexOf("-") + 1);
                                }
                                else
                                {
                                    this.obic7TextStringDaihyoushaPostCD1.Value = postNo;
                                    this.obic7TextStringDaihyoushaPostCD2.Value = String.Empty;
                                    return;
                                }
                                using (DataTable juushozisho = M0000SearchCom.M0000MS_MA_Juushozisho.SelectByKey(renban, postNo, ""))
                                {
                                    if (ComTool.Function.IsValidDataTable(juushozisho))
                                    {
                                        this.obic7TextStringDaihyoushaAddress1.Value = ComTool.Function.AnyToString(juushozisho.Rows[0]["Juusho"]);
                                    }
                                    this.obic7TextStringDaihyoushaAddress1.Focus();
                                }
                            }
                            else
                            {
                                isNotSearch = true;
                            }
                        }
                        break;
                    // 2026/10/11 追加 (5525)事業所別償却資産申告対応 終了
                    default:
                        break;
                }
            }
            if (isNotSearch)
            {
                //検索実行がなかった場合は直前のコントロールにフォーカスを移動させる
                this.PrevControl.Focus();
            }
        }

        /// 入力項目チェック
        /// <summary>
        /// 入力項目チェック
        /// </summary>
        /// <returns>true:エラーなし　false:エラーあり</returns>
        private bool InputChecks()
        {
            //チェック結果格納用
            bool result = true;

            //基準日
            if (String.IsNullOrEmpty(this.obic7TextDatetimeBaseDate.Text))
            {
                //エラープロバイダを表示
                DispErrorProvider(this.obic7TextDatetimeBaseDate, "00104", null);
                //一つ目のエラー項目であればフォーカスを移動
                this.obic7TextDatetimeBaseDate.Focus();
                result = false;
            }
            //事務所コード
            if (String.IsNullOrEmpty(this.obic7TextStringZigyoushoCD.Text))
            {
                //エラープロバイダを表示
                DispErrorProvider(this.obic7TextStringZigyoushoCD, "00104", null);
                if (result == true)
                {
                    //一つ目のエラー項目であればフォーカスを移動
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
            //更新件数
            int deleteCount = 0;

            //確認メッセージ
            if (WinToolLogicUI.Obic7MessageBox.Show(SYS_BUNRUI, MESSAGE_SEQ, MESSAGECD1, "00111", null, null, this.GetType(), WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxButtons.YesNo, WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                //削除処理
                //トランザクション開始
                this.businessDB.BeginTransaction();
                //更新処理
                try
                {
                    //削除SQL文を実行
                    deleteCount = this.businessDB.ExecuteNonQuery(MakeSQLDeleteMaster());
                    // 2026/10/11 追加 (5525)事業所別償却資産申告対応 開始
                    //事業所別地方税申告情報マスタも削除(表示時にレコードが存在した場合のみ。排他エラーは資産事業所マスタと同じ扱い)
                    if ((deleteCount != 0) && (this.timestampTihouzei != null))
                    {
                        deleteCount = this.businessDB.ExecuteNonQuery(MakeSQLDeleteTihouzei());
                    }
                    // 2026/10/11 追加 (5525)事業所別償却資産申告対応 終了
                    if (deleteCount == 0)
                    {
                        //更新件数が0件の場合
                        //エラーメッセージの表示
                        WinToolLogicUI.Obic7MessageBox.Show(SYS_BUNRUI, MESSAGE_SEQ, MESSAGECD1, "00102", null, null, this.GetType(), WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxButtons.OK, WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1);
                        //ロールバック
                        this.businessDB.RollBackTransaction();
                        //明細部をクリア
                        SwitchDetail(true, true);
                        //事業所名をクリア
                        // 2009/02/19 削除 OBIC T.Wakumoto 開始 削除失敗時にKey項目が削除されるためクリア処理を削除
                        //this.obic7LabelStringZigyoushoName.Value = ""; 
                        // 2009/02/19 削除 OBIC T.Wakumoto 終了

                    }
                    else
                    {
                        //削除成功
                        //コミット
                        this.businessDB.CommitTransaction();
                        //明細部をクリア
                        SwitchDetail(true, true);
                        //事業所コード、事業所名をクリア
                        this.obic7TextStringZigyoushoCD.Value = "";
                        this.obic7LabelStringZigyoushoName.Value = "";
                    }
                }
                catch
                {
                    //例外が発生した場合はロールバックを行うだけとし、その他独自の例外処理は行わずない
                    //ロールバック
                    this.businessDB.RollBackTransaction();
                    //throwして例外処理はフォーム基底クラスに処理を任せる
                    throw;
                }
                //ヘッダ部の切替
                // 2009/02/19 修正 OBIC T.Wakumoto 開始 削除件数0件の場合を考慮
                //SwitchHeader(true, true, MODE_INSERT);
                if (deleteCount != 0)
                {
                    //削除件数が0件以上の場合
                    SwitchHeader(true, true, MODE_INSERT);
                }
                // 2009/02/23 追加 OBIC T.Wakumoto 開始 削除件数0件の場合を考慮し、追加
                else
                {
                    //削除件数が0件の場合
                    SwitchHeader(true, false, MODE_INSERT);
                }
                // 2009/02/23 追加 OBIC T.Wakumoto 終了
                // 2009/02/19 修正 OBIC T.Wakumoto 終了
                //処理モードに「新規」と表示
                this.obic7LabelStringMode.Value = MODE_INSERT;
                //基準日をセット
                this.obic7TextDatetimeBaseDate.Text = BaseDateSet();
                //フォーカスを基準日へセット
                this.obic7TextDatetimeBaseDate.Focus();
                //明細部の切替
                SwitchDetail(false, true);
            }
        }

        /// F5:取消処理
        /// <summary>
        /// F5:取消処理
        /// </summary>
        private void F5Action()
        {
            // 2009/02/27 追加 OBIC T.Wakumoto 開始 F5押下フラグを初期化
            //this.isF5Check = false;
            // 2009/02/27 追加 OBIC T.Wakumoto 終了

            //フォーカスがヘッダ部に存在する場合
            if ((this.PrevControl == this.obic7TextDatetimeBaseDate) ||
               (this.PrevControl == this.obic7TextStringZigyoushoCD) ||
               (this.ActiveControl == this.obic7ButtonWithLogicZigyoushoCDSearch) ||
               (this.ActiveControl == this.obic7ButtonWithLogicDisp))
            {
                // 2009/02/23 追加 OBIC T.Wakumoto 開始 ヘッダ部のValidatingを走らせないよう、フラグを追加
                this.isF5Check = true;
                // 2009/02/23 追加 OBIC T.Wakumoto 終了
                // 2009/02/27 削除 OBIC T.Wakumoto 開始 事業所コードのIsChangedTextの挙動を正常にする為
                ////基準日にフォーカスを移動させる
                //this.obic7TextDatetimeBaseDate.Focus();
                // 2009/02/27 削除 OBIC T.Wakumoto 終了
                // 2009/02/23 追加 OBIC T.Wakumoto 開始 基準日を再取得、事業所コードをクリアを追加
                this.obic7TextDatetimeBaseDate.Text = BaseDateSet();
                // 2009/02/27 修正 OBIC T.Wakumoto 開始 事業所コードのIsChangedTextの挙動を正常にする為,クリア処理の前後にフォーカス移動追加
                //this.obic7TextStringZigyoushoCD.Text = "";
                this.obic7TextStringZigyoushoCD.Focus();
                this.obic7TextStringZigyoushoCD.Text = "";
                this.obic7TextDatetimeBaseDate.Focus();
                // 2009/02/27 修正 OBIC T.Wakumoto 終了
                // 2009/02/27 追加 OBIC T.Wakumoto 開始 事業所名クリアを追加
                this.obic7LabelStringZigyoushoName.Text = "";
                // 2009/02/27 追加 OBIC T.Wakumoto 終了
                // 2009/02/23 追加 OBIC T.Wakumoto 終了

            }
            else
            {
                //確認メッセージ
                if (WinToolLogicUI.Obic7MessageBox.Show(SYS_BUNRUI, MESSAGE_SEQ, MESSAGECD1, "00113", null, null, this.GetType(), WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxButtons.YesNo, WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    //Yesの場合
                    //明細部をクリア
                    SwitchDetail(true, true);
                    // 2009/02/27 削除 OBIC T.Wakumoto 開始 明細部でのF5(取消)押下時、事業所名称をクリアしないよう削除
                    //事業所名をクリア
                    //this.obic7LabelStringZigyoushoName.Value = "";                    
                    // 2009/02/27 削除 OBIC T.Wakumoto 終了
                    //ヘッダ部切替
                    SwitchHeader(true, false, MODE_INSERT);
                    //処理モードに「新規」と表示させる
                    this.obic7LabelStringMode.Value = MODE_INSERT;
                    //基準日をセット
                    this.obic7TextDatetimeBaseDate.Text = BaseDateSet();
                    //フォーカスを基準日へ移動
                    this.obic7TextDatetimeBaseDate.Focus();
                    //明細部の切替
                    SwitchDetail(false, true);
                }
                else
                {
                    //処理を中止した場合は直前コントロールにフォーカスを移動させる
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
            //更新件数
            int updateCount = 0;
            //クエリ文
            string sqlExecute = "";
            //処理モード
            string mode = this.obic7LabelStringMode.Value;
            //エラーコード
            string errorCD2 = "";
            //必須項目チェック
            if (this.Validate() == false)
            {
                //エラーがあった場合、後続の処理を中止
                return;
            }
            else
            {
                //エラーが無かった場合
                //確認メッセージの表示
                if (WinToolLogicUI.Obic7MessageBox.Show(SYS_BUNRUI, MESSAGE_SEQ, MESSAGECD1, "00112", null, null, this.GetType(), WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxButtons.YesNo, WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    //トランザクション開始
                    this.businessDB.BeginTransaction();
                    //更新処理
                    try
                    {
                        //モードに応じてSQL文を生成する
                        if (mode == MODE_INSERT)
                        {
                            //INSERT用SQL文生成
                            sqlExecute = MakeSQLInsertMaster();
                        }
                        else if (mode == MODE_UPDATE)
                        {
                            //Update用のSQL文を生成
                            sqlExecute = MakeSQLUpdateMaster();
                        }
                        //SQL実行
                        updateCount = this.businessDB.ExecuteNonQuery(sqlExecute);
                        // 2026/10/11 追加 (5525)事業所別償却資産申告対応 開始
                        //事業所別地方税申告情報マスタの登録(排他エラーは資産事業所マスタと同じ扱い)
                        if (updateCount != 0)
                        {
                            string sqlTihouzei = MakeSQLRegisterTihouzei();
                            if (sqlTihouzei != null)
                            {
                                updateCount = this.businessDB.ExecuteNonQuery(sqlTihouzei);
                            }
                        }
                        // 2026/10/11 追加 (5525)事業所別償却資産申告対応 終了
                        if (updateCount == 0)
                        {
                            //処理件数が0件の場合排他エラーとして処理をする
                            //処理モードに応じてメッセージを設定
                            if (mode == MODE_INSERT)
                            {
                                errorCD2 = "00100";
                            }
                            else if (mode == MODE_UPDATE)
                            {
                                errorCD2 = "00102";
                            }
                            //ロールバック
                            this.businessDB.RollBackTransaction();
                            //事業所名のクリア
                            this.obic7LabelStringZigyoushoName.Value = "";
                        }
                        else
                        {
                            //コミット
                            this.businessDB.CommitTransaction();
                            //事業所コードのクリア
                            this.obic7TextStringZigyoushoCD.Value = "";
                            //事業所名のクリア
                            this.obic7LabelStringZigyoushoName.Value = "";
                        }
                    }
                    catch
                    {
                        //例外が発生した場合はロールバックを行うだけとし、その他独自の例外処理は行わずない
                        //ロールバック
                        this.businessDB.RollBackTransaction();
                        //throwして例外処理はフォーム基底クラスに処理を任せる
                        throw;
                    }

                    //ヘッダの切替
                    SwitchHeader(true, false, MODE_INSERT);
                    //基準日へフォーカス移動
                    this.obic7TextDatetimeBaseDate.Focus();
                    //明細部の切替
                    SwitchDetail(false, true);
                }
                //表示するべきメッセージが存在していればメッセージを表示する
                if (!String.IsNullOrEmpty(errorCD2))
                {
                    WinToolLogicUI.Obic7MessageBox.Show(SYS_BUNRUI, MESSAGE_SEQ, MESSAGECD1, errorCD2, null, null, this.GetType(), WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxButtons.OK, WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1);
                }
            }
        }        

        /// 明細部（検索なし）ファンクションボタン制御
        /// <summary>
        /// 明細部（検索なし）ファンクションボタン制御
        /// </summary>
        private void SetFunctionButtonDetail()
        {
            //新規の場合
            if (this.obic7LabelStringMode.Text == MODE_INSERT)
            {
                //F5,F10,F12使用可
                base.SetFunctionButton(false, false, false, false, true, false, false, false, false, true, false, true);
            }
            //修正の場合
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
            //新規の場合
            if (this.obic7LabelStringMode.Text == MODE_INSERT)
            {
                //F4,F5,F10,F12使用可
                base.SetFunctionButton(false, false, false, true, true, false, false, false, false, true, false, true);
            }
            //修正の場合
            else if(this.obic7LabelStringMode.Text == MODE_UPDATE)
            {
                //F3,F4,F5,F10,F12使用可
                base.SetFunctionButton(false, false, true, true, true, false, false, false, false, true, false, true);
            }
        }

        /// ヘッダ部のファンクションボタン制御
        /// <summary>
        /// ヘッダ部のファンクションボタン制御
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // 2008/12/15    変更    T.Wakumoto 開始    ヘッダ部のファンクション制御を追加
        private void SetFunctionButtonHeader(object sender, EventArgs e)
        {
            //ファンクションボタン切替(使用可能：閉じる）
            // 2009/02/23 修正 OBIC T.Wakumoto 開始 F5取消を使用可に変更
            //base.SetFunctionButton(false, false, false, false, false, false, false, false, false, false, false, true);
            base.SetFunctionButton(false, false, false, false, true, false, false, false, false, false, false, true); 
            // 2009/02/23 修正 OBIC T.Wakumoto 終了

        }
        // 2008/12/15    変更    T.Wakumoto 終了

        #endregion

        #region イベント
        
        /// F3:削除押下時の処理
        /// <summary>
        /// F3:削除押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7ToolStripButtonWithLogicF3_Click(object sender, EventArgs e)
        {
            F3Action();
        }

        /// F4:検索押下時の処理
        /// <summary>
        /// F4:検索押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7ToolStripButtonWithLogicF4_Click(object sender, EventArgs e)
        {
            F4Action();
        }

        /// F5:取消押下時の処理
        /// <summary>
        /// F5:取消押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7ToolStripButtonWithLogicF5_Click(object sender, EventArgs e)
        {
            F5Action();
        }

        /// F10:登録押下時の処理
        /// <summary>
        /// F10:登録押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        private void NA_SisanZigyoushoMasterMaintenance_Load(object sender, EventArgs e)
        {
            //DBアクセスクラス（ステートフル）のインスタンスを作成
            this.businessDB = new WinToolLogic.DBAccess(SYS_BUNRUI, CUSTOM_SEQ, ComToolLogic.DBAccess.Obic7DBAccessDBCategory.Business);

            //初期処理
            //処理モードに「新規」と表示させる
            this.obic7LabelStringMode.Value = MODE_INSERT;
            //基準日のセット
            this.obic7TextDatetimeBaseDate.Text = BaseDateSet();
            //明細部表示切替
            SwitchDetail(false, false);
        }

        /// フォーム表示時の処理
        /// <summary>
        /// フォーム表示時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NA_SisanZigyoushoMasterMaintenance_Shown(object sender, EventArgs e)
        {
            //フォーカスを基準日にセット
            this.obic7TextDatetimeBaseDate.Focus();
        }

        /// フォーム上でのKeyDownイベント
        /// <summary>
        /// フォーム上でのKeyDownイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NA_SisanZigyoushoMasterMaintenance_KeyDown(object sender, KeyEventArgs e)
        {
            //ALT | Shift | Control を押さない状態でEnterキーを押した場合
            if ((e.Alt == false) && (e.Control == false) && (e.Shift == false))
            {
                if (e.KeyCode == Keys.Enter)
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
                        //上記以外は次のコントロールへ移動
                        this.SelectNextInputControl(true);
                    }
                }
            }
        }

        /// フォームを閉じようとした際の処理
        /// <summary>
        /// フォームを閉じようとした際の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NA_SisanZigyoushoMasterMaintenance_FormClosing(object sender, FormClosingEventArgs e)
        {
            //編集内容があれば確認メッセージを表示する
            if ((this.obic7TextStringZititaiCD.Enabled == true) && (this.obic7TextStringZititaiCD.ReadOnly == false))
            {
                if (WinToolLogicUI.Obic7MessageBox.Show(SYS_BUNRUI, MESSAGE_SEQ, MESSAGECD1, "00110", null, null, this.GetType(), WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxButtons.YesNo, WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                {
                    //Yes以外を選択した場合はフォームを閉じる処理をキャンセル
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
                //基準日が未入力の場合、エラープロバイダを表示
                DispErrorProvider(this.obic7TextDatetimeBaseDate, "00104", null);
                //イベントをキャンセル
                e.Cancel = true;
                //事業所名のクリア
                this.obic7LabelStringZigyoushoName.Value = "";
            }
            else
            {
                // 2008/12/22 修正 T.Wakumoto 開始 事業所データが取得できない場合と事業所名がnullの場合の処理を追加                
                //事業所名の洗い換えを行う
                //using (DataTable zigyoushoData = M0000SearchCom.M0000MS_MA_Zigyousho.SelectByKaiteiDate(this.obic7TextStringZigyoushoCD.Text, (DateTime)this.obic7TextDatetimeBaseDate.Value, null, 0, 0, 1, ""))
                //{
                //    if (ComTool.Function.IsValidDataTable(zigyoushoData))
                //    {
                //        //データが取得できた場合、事業所名をセット
                //        this.obic7LabelStringZigyoushoName.Value = ComTool.Function.AnyToString(zigyoushoData.Rows[0]["ZigyoushoName"]);
                //    }
                //    else
                //    {
                //        //データが取得できなかった場合、事業所名をクリア
                //        this.obic7LabelStringZigyoushoName.Value = "";
                //    }
                //}
                //事業所名格納用
                string zigyoushoName = "";
                //メッセージボックス用
                string messageCD2 = "";
                //事業所名をクリア
                this.obic7LabelStringZigyoushoName.Text = "";
                if (!String.IsNullOrEmpty(this.obic7TextStringZigyoushoCD.Text))
                {
                    //事業所コードをチェック
                    using (DataTable zigyoushoData = M0000SearchCom.M0000MS_MA_Zigyousho.SelectByKaiteiDate(
                        this.obic7TextStringZigyoushoCD.Text, (DateTime)this.obic7TextDatetimeBaseDate.Value, null, 0, 0, 0, ""))
                    {
                        if (ComTool.Function.IsValidDataTable(zigyoushoData))
                        {
                            //事業所名を格納
                            //  2009/09/02  修正開始  M.Kasayuki 事業所略名を表示
                            //zigyoushoName = ComTool.Function.AnyToString(zigyoushoData.Rows[0]["ZigyoushoName"]);
                            zigyoushoName = ComTool.Function.AnyToString(zigyoushoData.Rows[0]["ZigyoushoNameRyaku"]);
                            //  2009/09/02  修正終了  M.Kasayuki
                            //データが取得できた場合、事業所名チェック
                            if (String.IsNullOrEmpty(zigyoushoName))
                            {
                                //メッセージボックス用
                                messageCD2 = "00014";
                                //イベントをキャンセルする
                                e.Cancel = true;
                            }
                            else
                            {
                                //事務所名をセット
                                this.obic7LabelStringZigyoushoName.Value = zigyoushoName;
                            }
                        }
                        else
                        {
                            //メッセージボックス用
                            messageCD2 = "00013";
                            //イベントをキャンセルする
                            e.Cancel = true;
                        }
                    }
                    if (!String.IsNullOrEmpty(messageCD2))
                    {
                        //エラーメッセージの表示
                        WinToolLogicUI.Obic7MessageBox.Show(SYS_BUNRUI, MESSAGE_SEQ, MESSAGECD1, messageCD2, null, null, this.GetType(),
                                                            WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxButtons.OK,
                                                            WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1);
                    }
                }
                // 2008/12/22 修正 T.Wakumoto 終了
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
            // 2009/02/23 追加 OBIC T.Wakumoto 開始 ヘッダ部でF5が押下された場合、Validatingが走らないよう修正
            if (!this.isF5Check)
            {
            // 2009/02/23 追加 OBIC T.Wakumoto 終了
                if (String.IsNullOrEmpty(this.obic7TextStringZigyoushoCD.Text))
                {
                    //事業所コードが未入力の場合、エラープロバイダを表示
                    DispErrorProvider(this.obic7TextStringZigyoushoCD, "00104", null, null);
                    //事業所名をクリア
                    this.obic7LabelStringZigyoushoName.Text = "";
                    //イベントをキャンセル
                    e.Cancel = true;
                }
                else
                {
                    if (this.obic7TextStringZigyoushoCD.IsChangedText)
                    {
                        //事業所名格納用
                        string zigyoushoName = "";
                        //メッセージボックス用
                        string messageCD2 = "";
                        //事業所名をクリア
                        this.obic7LabelStringZigyoushoName.Text = "";
                        //事業所コードをチェック
                        using (DataTable zigyoushoData = M0000SearchCom.M0000MS_MA_Zigyousho.SelectByKaiteiDate(
                            this.obic7TextStringZigyoushoCD.Text, (DateTime)this.obic7TextDatetimeBaseDate.Value, null, 0, 0, 0, ""))
                        {
                            if (ComTool.Function.IsValidDataTable(zigyoushoData))
                            {
                                //事業所名を格納
                                //  2009/09/02  修正開始  M.Kasayuki 事業所略名を表示
                                //zigyoushoName = ComTool.Function.AnyToString(zigyoushoData.Rows[0]["ZigyoushoName"]);
                                zigyoushoName = ComTool.Function.AnyToString(zigyoushoData.Rows[0]["ZigyoushoNameRyaku"]);
                                //  2009/09/02  修正終了  M.Kasayuki
                                //データが取得できた場合、事業所名チェック
                                if (String.IsNullOrEmpty(zigyoushoName))
                                {
                                    //メッセージボックス用
                                    messageCD2 = "00014";
                                    //イベントをキャンセルする
                                    e.Cancel = true;
                                }
                                else
                                {
                                    //事務所名をセット
                                    this.obic7LabelStringZigyoushoName.Value = zigyoushoName;
                                    //表示ボタンと同様の処理を行う
                                    this.obic7ButtonWithLogicDisp.PerformClick();
                                }
                            }
                            else
                            {
                                //メッセージボックス用
                                messageCD2 = "00013";
                                //イベントをキャンセルする
                                e.Cancel = true;
                            }
                        }
                        if (!String.IsNullOrEmpty(messageCD2))
                        {
                            //エラーメッセージの表示
                            WinToolLogicUI.Obic7MessageBox.Show(SYS_BUNRUI, MESSAGE_SEQ, MESSAGECD1, messageCD2, null, null, this.GetType(),
                                                                WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxButtons.OK,
                                                                WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1);
                        }
                    }
                    else
                    {
                        // 最終項目なので表示ボタン押下時と同様な処理を行う。
                        this.obic7ButtonWithLogicDisp.PerformClick();
                        // 後続の処理を中止する。
                        return;
                    }
                }
            // 2009/02/23 追加 OBIC T.Wakumoto 開始 ヘッダ部でF5が押下された場合、Validatingが走らないよう修正
            }
            else
            {
                //F5押下フラグをfalseに
                this.isF5Check = false;
            }
            // 2009/02/23 追加 OBIC T.Wakumoto 終了
        }

        /// 事務所コード_検索ボタン押下時の処理
        /// <summary>
        /// 事務所コード_検索ボタン押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7ButtonWithLogicZigyoushoCDSearch_Click(object sender, EventArgs e)
        {
            //フォーカスを事業所コードにセット
            this.obic7TextStringZigyoushoCD.Focus();
            //F4押下時と同様の処理を行う
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
            //処理モード
            string mode = this.obic7LabelStringMode.Text;
            //明細表示をクリア
            SwitchDetail(false, true);
            //基準日と事業所コードをチェック
            if (InputChecks())
            {
                //資産事業所マスタからデータを取得
                using (DataTable sisanZigyousho = this.businessDB.GetDataTable(MakeSQLSelectMaster()))
                {
                    if (ComTool.Function.IsValidDataTable(sisanZigyousho))
                    {
                        //データが取得できた場合
                        //処理モードに「修正」と表示
                        this.obic7LabelStringMode.Value = MODE_UPDATE;
                        mode = this.obic7LabelStringMode.Value;
                        //明細部のEnabledをtrueにする
                        SwitchDetail(true, false);
                        //コントロールにデータをセット
                        SetDetailData(sisanZigyousho, mode);
                    }
                    else
                    {
                        //新規モードの場合
                        SwitchDetail(true, false);
                        //ヘッダを無効にする
                        SwitchHeader(false, false, MODE_INSERT);
                        //申告自治体コード
                        this.obic7TextStringZititaiCD.Value = "";
                        //申告自治体名
                        this.obic7LabelStringZititaiName.Value = "";
                        //郵便番号1
                        this.obic7TextStringPostCD1.Value = "";
                        //郵便番号2
                        this.obic7TextStringPostCD2.Value = "";
                        //住所1
                        this.obic7TextStringAddress1.Value = "";
                        //住所2
                        this.obic7TextStringAddress2.Value = "";
                        //住所1カナ
                        this.obic7TextStringAddress1Kana.Value = "";
                        //住所2カナ
                        this.obic7TextStringAddress2Kana.Value = "";
                        //電話番号1
                        this.obic7TextStringTelNo1.Value = "";
                        //電話番号2
                        this.obic7TextStringTelNo2.Value = "";
                        //電話番号3
                        this.obic7TextStringTelNo3.Value = "";
                        //会社名
                        this.obic7TextStringCompanyName.Value = "";
                        //会社カナ名
                        this.obic7TextStringCompanyKanaName.Value = "";
                        //代表者氏名
                        this.obic7TextStringDaihyoushaName.Value = "";
                        //代表者カナ名
                        this.obic7TextStringDaihyoushaKanaName.Value = "";
                        //屋号
                        this.obic7TextStringYagou.Value = "";
                        //事業種目
                        this.obic7TextStringZigyoushumoku.Value = "";
                        //資本金
                        this.obic7TextNumberSihonkin.Value = 0;
                        //設立年月日
                        this.obic7TextDatetimeSeturituDate.Value = null;
                        //申告担当者氏名
                        this.obic7TextStringSinkokuTantoushaName.Value = "";
                        //申告担当者電話番号1
                        this.obic7TextStringTantoushaTelNo1.Value = "";
                        //申告担当者電話番号2
                        this.obic7TextStringTantoushaTelNo2.Value = "";
                        //申告担当者電話番号3
                        this.obic7TextStringTantoushaTelNo3.Value = "";
                        //会計事務所名
                        this.obic7TextStringKaikeiZimushoName.Value = "";
                        //会計事務所電話番号1
                        this.obic7TextStringKaikeiZimushoTelNo1.Value = "";
                        //会計事務所電話番号2
                        this.obic7TextStringKaikeiZimushoTelNo2.Value = "";
                        //会計事務所電話場号3
                        this.obic7TextStringKaikeiZimushoTelNo3.Value = "";
                    }
                }
                // 2026/10/11 追加 (5525)事業所別償却資産申告対応 開始
                //事業所別地方税申告情報マスタからデータを取得(資産事業所マスタの有無に関わらず読み込む)
                SetTihouzeiData();
                // 2026/10/11 追加 (5525)事業所別償却資産申告対応 終了
                //【注意】
                //　申告自治体コードのoldTextプロパティに取得したデータを格納するために記述。
                //　この記述が無い場合、表示ボタンクリック後の申告自治体コードのoldTextは
                //　""になっているため、申告自治体コードのテキストを""に変更した場合の
                //　IsChangedTextが走らないため。
                this.obic7TextSeparatorZititaiCD.Focus();
                
            }
        }

        /// 申告自治体コードValidating
        /// <summary>
        /// 申告自治体コードValidating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7TextStringZititaiCD_Validating(object sender, CancelEventArgs e)
        {
            if (this.obic7TextStringZititaiCD.IsChangedText)
            {
                //入力値が変更された場合
                //申告自治体名をクリア
                this.obic7LabelStringZititaiName.Text = "";
                if (String.IsNullOrEmpty(this.obic7TextStringZititaiCD.Text))
                {
                    //イベントをキャンセル
                    return;
                }
                else
                {
                    //自治体データを取得
                    using (DataTable zititaiData = N0000SearchCom.N0000MS_NA_Zititai.SelectByKey(this.obic7TextStringZititaiCD.Text, ""))
                    {
                        if (ComTool.Function.IsValidDataTable(zititaiData))
                        {
                            //データが取得できた場合
                            //申告自治体名に値をセット
                            this.obic7LabelStringZititaiName.Value = ComTool.Function.AnyToString(zititaiData.Rows[0]["ZititaiName"]);
                        }
                        else
                        {
                            //エラーメッセージを表示
                            WinToolLogicUI.Obic7MessageBox.Show(SYS_BUNRUI, MESSAGE_SEQ, MESSAGECD1, "00028", null, null, this.GetType(), WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxButtons.OK, WinToolLogicUI.Obic7MessageBox.Obic7MessageBoxDefaultButton.Button1);
                            //イベントをキャンセルする
                            e.Cancel = true;
                        }
                    }
                }

            }
        }

        /// 郵便番号2Validating
        /// <summary>
        /// 郵便番号2Validating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7TextStringPostCD2_Validating(object sender, CancelEventArgs e)
        {
            if ((String.IsNullOrEmpty(this.obic7TextStringAddress1.Value)) && (!String.IsNullOrEmpty(this.obic7TextStringPostCD1.Value)))
            {
                //住所1が未入力かつ郵便番号1が入力されている場合
                //データを取得
                using (DataTable juushoData = M0000SearchCom.M0000MS_MA_Juushozisho.SelectByYuubinNO(CreatePostNo(), "", ""))
                {
                    //取得レコード件数=1のとき
                    if (juushoData.Rows.Count == 1)
                    {
                        //住所1にデータをセット
                        this.obic7TextStringAddress1.Value = ComTool.Function.AnyToString(juushoData.Rows[0]["Juusho"]);
                    }
                    //取得レコード件数>1のとき
                    else if (juushoData.Rows.Count > 1)
                    {
                        //郵便番号検索ボタンと同じ処理を行う
                        this.obic7ToolStripButtonWithLogicF4.PerformClick();
                        //this.obic7ButtonWithLogicPostCDSearch.PerformClick();

                    }
                }
            }
        }

        /// 資本金Validating
        /// <summary>
        /// 資本金Validating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7TextNumberSihonkin_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(this.obic7TextNumberSihonkin.Text))
            {
                //未入力の場合、0をセットする。
                this.obic7TextNumberSihonkin.Value = 0;
            }
        }

        /// 申告自治体コード_検索ボタン押下時
        /// <summary>
        /// 申告自治体コード_検索ボタン押下時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7ButtonWithLogicZititaiCDSearch_Click(object sender, EventArgs e)
        {
            //フォーカスを申告自治体コードにセット
            this.obic7TextStringZititaiCD.Focus();
            //F4押下時と同様の処理を行う
            this.obic7ToolStripButtonWithLogicF4.PerformClick();
        }

        /// 郵便番号_検索ボタン押下時の処理
        /// <summary>
        /// 郵便番号_検索ボタン押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7ButtonWithLogicPostCDSearch_Click(object sender, EventArgs e)
        {
            //フォーカスを郵便番号1にセット
            this.obic7TextStringPostCD1.Focus();
            //F4押下時と同様の処理を行う
            this.obic7ToolStripButtonWithLogicF4.PerformClick();
        }

        /// 事業所コードフォーカス取得時の処理
        /// <summary>
        /// 事業所コードフォーカス取得時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7TextStringZigyoushoCD_Enter(object sender, EventArgs e)
        {
            //ファンクションボタン切替（使用可能：検索、閉じる）
            // 2009/02/23 修正 OBIC T.Wakumoto 開始 F5取消を使用可能に変更
            //base.SetFunctionButton(false, false, false, true, false, false, false, false, false, false, false, true);
            base.SetFunctionButton(false, false, false, true, true, false, false, false, false, false, false, true); 
            // 2009/02/23 修正 OBIC T.Wakumoto 終了

        }       

        #region 変更前
        // 2008/12/16    変更    T.Wakumoto 開始    ヘッダ部のファンクションボタン制御をまとめた為、削除
        ///// 基準日フォーカス取得時の処理
        ///// <summary>
        ///// 基準日フォーカス取得時の処理
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void obic7TextDatetimeBaseDate_Enter(object sender, EventArgs e)
        //{
        //    //ファンクションボタン切替(使用可能：閉じる）
        //    base.SetFunctionButton(false, false, false, false, false, false, false, false, false, false, false, true);
        //}
        // 2008/12/16    変更    T.Wakumoto 終了
        #endregion

        /// 明細部（検索ボタン有）フォーカス取得時の処理
        /// <summary>
        /// 明細部（検索ボタン有）フォーカス取得時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void controlDetailSearch_Enter(object sender, EventArgs e)
        {
            SetFunctionButtonDetailSearch();
        }

        /// 明細部(検索ボタン無し)フォーカス取得時の処理
        /// <summary>
        /// 明細部(検索ボタン無し)フォーカス取得時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void controlDetail_Enter(object sender, EventArgs e)
        {
            SetFunctionButtonDetail();
        }

        // 2026/10/11 追加 (5525)事業所別償却資産申告対応 開始
        /// 代表者資格Validating
        /// <summary>
        /// 代表者資格Validating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7TextStringDaihyoushaSikaku_Validating(object sender, CancelEventArgs e)
        {
            WinToolUI.Obic7ErrorProvider.Clear(this.obic7TextStringDaihyoushaSikaku);
            this.obic7LabelStringDaihyoushaSikakuName.Value = "";

            if (String.IsNullOrEmpty(this.obic7TextStringDaihyoushaSikaku.Value))
            {
                return;
            }

            byte daihyoushaSikaku;
            string daihyoushaSikakuName = null;
            if (byte.TryParse(this.obic7TextStringDaihyoushaSikaku.Value, out daihyoushaSikaku))
            {
                daihyoushaSikakuName = GetDaihyoushaSikakuName(daihyoushaSikaku);
            }

            if (daihyoushaSikakuName == null)
            {
                //区分外の値
                WinToolUI.Obic7ErrorProvider.SetError(this.obic7TextStringDaihyoushaSikaku, "代表者資格は 01:代表者、02:破産管財人、03:清算人、99:その他 のいずれかを入力してください。");
                e.Cancel = true;
                return;
            }

            this.obic7TextStringDaihyoushaSikaku.Value = daihyoushaSikaku.ToString("00");
            this.obic7LabelStringDaihyoushaSikakuName.Value = daihyoushaSikakuName;
        }

        /// 代表者郵便番号2Validating
        /// <summary>
        /// 代表者郵便番号2Validating（資産事業所の郵便番号と同じく、住所1未入力時に住所辞書から補完）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7TextStringDaihyoushaPostCD2_Validating(object sender, CancelEventArgs e)
        {
            if ((String.IsNullOrEmpty(this.obic7TextStringDaihyoushaAddress1.Value)) && (!String.IsNullOrEmpty(this.obic7TextStringDaihyoushaPostCD1.Value)))
            {
                using (DataTable juushoData = M0000SearchCom.M0000MS_MA_Juushozisho.SelectByYuubinNO(CreateDaihyoushaPostNo(), "", ""))
                {
                    if (juushoData.Rows.Count == 1)
                    {
                        this.obic7TextStringDaihyoushaAddress1.Value = ComTool.Function.AnyToString(juushoData.Rows[0]["Juusho"]);
                    }
                    else if (juushoData.Rows.Count > 1)
                    {
                        this.obic7ToolStripButtonWithLogicF4.PerformClick();
                    }
                }
            }
        }

        /// 代表者郵便番号_検索ボタン押下時の処理
        /// <summary>
        /// 代表者郵便番号_検索ボタン押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obic7ButtonWithLogicDaihyoushaPostCDSearch_Click(object sender, EventArgs e)
        {
            this.obic7TextStringDaihyoushaPostCD1.Focus();
            this.obic7ToolStripButtonWithLogicF4.PerformClick();
        }
        // 2026/10/11 追加 (5525)事業所別償却資産申告対応 終了

        #endregion
    }
}