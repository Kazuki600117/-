//
//    2008/12/1    T.Wakumoto    DB接続用クラス（ステートフル）のDisposeを追加
//    2026/10/11                  (5525)事業所別償却資産申告対応　「地方税申告（連携）」項目を追加
//

namespace Obic7fx.Win.N5525.MasterM.NA_SisanZigyousho
{
    partial class NA_SisanZigyoushoMasterMaintenance
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (businessDB != null))
            {
                businessDB.Dispose();
            }
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.obic7LabelStringMode = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelStringBaseDate = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelStringZigyoushoCD = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextDatetimeBaseDate = new Obic7fx.Win.A0000.Tool.UI.Obic7TextDatetime();
            this.obic7TextStringZigyoushoCD = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7ButtonWithLogicZigyoushoCDSearch = new Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic();
            this.obic7LabelStringZigyoushoName = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7ButtonWithLogicDisp = new Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic();
            this.obic7GroupBoxBasicInfo = new Obic7fx.Win.A0000.Tool.UI.Obic7GroupBox();
            this.obic7LabelStringZititaiName = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7ButtonWithLogicZititaiCDSearch = new Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic();
            this.obic7LabelStringZititai = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringZititaiCD = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7LabelStringPostCD = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringPostCD1 = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7ButtonWithLogicPostCDSearch = new Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic();
            this.obic7LabelStringPostCDHaihun = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringPostCD2 = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7LabelStringAddress1 = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringAddress1 = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringAddress1Kana = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7LabelStringAddress1Kana = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelStringAddress2 = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringAddress2 = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringAddress2Kana = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7LabelStringAddress2Kana = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelStringTelNo = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringTelNo1 = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7LabelStringTelNoHaihun1 = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringTelNo2 = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7LabelStringTelNoHaihun2 = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringTelNo3 = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7LabelStringCompanyName = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringCompanyName = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7LabelStringCompanyKanaName = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringCompanyKanaName = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7LabelStringDaihyoushaName = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringDaihyoushaName = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7LabelStringDaihyoushaKanaName = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringDaihyoushaKanaName = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7LabelStringYagou = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringYagou = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7LabelStringZigyoushumoku = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringZigyoushumoku = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7LabelStringSihonkin = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextNumberSihonkin = new Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber();
            this.obic7LabelStringSeturituDate = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextDatetimeSeturituDate = new Obic7fx.Win.A0000.Tool.UI.Obic7TextDatetime();
            this.obic7LabelStringSinkokuTantoushaName = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringSinkokuTantoushaName = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringTantoushaTelNo3 = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7LabelStringTelNoHaihun4 = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringTantoushaTelNo2 = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7LabelStringTelNoHaihun3 = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringTantoushaTelNo1 = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7LabelStringTantoushaTelNo = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelStringKaikeiZimushoName = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringKaikeiZimushoName = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKaikeiZimushoTelNo3 = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7LabelStringTelNoHaihun6 = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringKaikeiZimushoTelNo2 = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7LabelStringTelNoHaihun5 = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringKaikeiZimushoTelNo1 = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7LabelStringKaikeiZimushoTelNo = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7GroupBoxSinkokushoKoumoku = new Obic7fx.Win.A0000.Tool.UI.Obic7GroupBox();
            this.obic7TextSeparatorZititaiCD = new Obic7fx.Win.A0000.Tool.UI.Obic7TextSeparator();
            // 2026/10/11 追加 (5525)事業所別償却資産申告対応 開始
            this.obic7LabelStringNouzeishaCD = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringNouzeishaCD = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7LabelStringRiyoushaID = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringRiyoushaID = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7LabelStringDaihyoushaSikaku = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringDaihyoushaSikaku = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7LabelStringDaihyoushaSikakuName = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelStringDaihyoushaTelNo = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringDaihyoushaTelNo1 = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7LabelStringDaihyoushaTelNoHaihun1 = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringDaihyoushaTelNo2 = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7LabelStringDaihyoushaTelNoHaihun2 = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringDaihyoushaTelNo3 = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7LabelStringDaihyoushaPostCD = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringDaihyoushaPostCD1 = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7LabelStringDaihyoushaPostCDHaihun = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringDaihyoushaPostCD2 = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7ButtonWithLogicDaihyoushaPostCDSearch = new Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic();
            this.obic7LabelStringDaihyoushaAddress = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringDaihyoushaAddress1 = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringDaihyoushaAddress2 = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7GroupBoxTihouzeiSinkoku = new Obic7fx.Win.A0000.Tool.UI.Obic7GroupBox();
            // 2026/10/11 追加 (5525)事業所別償却資産申告対応 終了
            this.obic7PanelHeader.SuspendLayout();
            this.obic7PanelDetail.SuspendLayout();
            this.obic7GroupBoxBasicInfo.SuspendLayout();
            this.obic7GroupBoxSinkokushoKoumoku.SuspendLayout();
            this.obic7GroupBoxTihouzeiSinkoku.SuspendLayout();
            this.SuspendLayout();
            // 
            // obic7PanelHeader
            // 
            this.obic7PanelHeader.Controls.Add(this.obic7ButtonWithLogicDisp);
            this.obic7PanelHeader.Controls.Add(this.obic7LabelStringZigyoushoName);
            this.obic7PanelHeader.Controls.Add(this.obic7ButtonWithLogicZigyoushoCDSearch);
            this.obic7PanelHeader.Controls.Add(this.obic7TextStringZigyoushoCD);
            this.obic7PanelHeader.Controls.Add(this.obic7TextDatetimeBaseDate);
            this.obic7PanelHeader.Controls.Add(this.obic7LabelStringZigyoushoCD);
            this.obic7PanelHeader.Controls.Add(this.obic7LabelStringMode);
            this.obic7PanelHeader.Controls.Add(this.obic7LabelStringBaseDate);
            this.obic7PanelHeader.TabIndex = 1000;
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7LabelStringBaseDate, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7LabelStringMode, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7LabelStringZigyoushoCD, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7TextDatetimeBaseDate, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7TextStringZigyoushoCD, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7ButtonWithLogicZigyoushoCDSearch, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7LabelStringZigyoushoName, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7ButtonWithLogicDisp, 0);
            // 
            // obic7PanelDetail
            // 
            this.obic7PanelDetail.Controls.Add(this.obic7TextSeparatorZititaiCD);
            this.obic7PanelDetail.Controls.Add(this.obic7GroupBoxBasicInfo);
            this.obic7PanelDetail.Controls.Add(this.obic7GroupBoxSinkokushoKoumoku);
            this.obic7PanelDetail.Controls.Add(this.obic7GroupBoxTihouzeiSinkoku);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7GroupBoxTihouzeiSinkoku, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7GroupBoxSinkokushoKoumoku, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7GroupBoxBasicInfo, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7TextSeparatorZititaiCD, 0);
            // 
            // obic7LabelStringMode
            // 
            this.obic7LabelStringMode.AutoEllipsis = true;
            this.obic7LabelStringMode.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.obic7LabelStringMode.ColumnNumber = 0;
            this.obic7LabelStringMode.EnableFalseLock = false;
            this.obic7LabelStringMode.ErrorString = null;
            this.obic7LabelStringMode.Font = new System.Drawing.Font("ＭＳ ゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.obic7LabelStringMode.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.obic7LabelStringMode.Location = new System.Drawing.Point(6, 3);
            this.obic7LabelStringMode.Name = "obic7LabelStringMode";
            this.obic7LabelStringMode.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Original;
            this.obic7LabelStringMode.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringMode.Size = new System.Drawing.Size(48, 24);
            this.obic7LabelStringMode.TabIndex = 1010;
            this.obic7LabelStringMode.Text = "新規";
            this.obic7LabelStringMode.Value = "新規";
            this.obic7LabelStringMode.VisibleFalseLock = false;
            // 
            // obic7LabelStringBaseDate
            // 
            this.obic7LabelStringBaseDate.AutoEllipsis = true;
            this.obic7LabelStringBaseDate.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.obic7LabelStringBaseDate.ColumnNumber = 0;
            this.obic7LabelStringBaseDate.EnableFalseLock = false;
            this.obic7LabelStringBaseDate.ErrorString = null;
            this.obic7LabelStringBaseDate.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.obic7LabelStringBaseDate.Location = new System.Drawing.Point(60, 51);
            this.obic7LabelStringBaseDate.Name = "obic7LabelStringBaseDate";
            this.obic7LabelStringBaseDate.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.RequiredLabel;
            this.obic7LabelStringBaseDate.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringBaseDate.Size = new System.Drawing.Size(84, 19);
            this.obic7LabelStringBaseDate.TabIndex = 2;
            this.obic7LabelStringBaseDate.Text = "基準日";
            this.obic7LabelStringBaseDate.Value = "基準日";
            this.obic7LabelStringBaseDate.VisibleFalseLock = false;
            // 
            // obic7LabelStringZigyoushoCD
            // 
            this.obic7LabelStringZigyoushoCD.AutoEllipsis = true;
            this.obic7LabelStringZigyoushoCD.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.obic7LabelStringZigyoushoCD.ColumnNumber = 0;
            this.obic7LabelStringZigyoushoCD.EnableFalseLock = false;
            this.obic7LabelStringZigyoushoCD.ErrorString = null;
            this.obic7LabelStringZigyoushoCD.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.obic7LabelStringZigyoushoCD.Location = new System.Drawing.Point(60, 75);
            this.obic7LabelStringZigyoushoCD.Name = "obic7LabelStringZigyoushoCD";
            this.obic7LabelStringZigyoushoCD.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.RequiredLabel;
            this.obic7LabelStringZigyoushoCD.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringZigyoushoCD.Size = new System.Drawing.Size(84, 19);
            this.obic7LabelStringZigyoushoCD.TabIndex = 3;
            this.obic7LabelStringZigyoushoCD.Text = "事業所コード";
            this.obic7LabelStringZigyoushoCD.Value = "事業所コード";
            this.obic7LabelStringZigyoushoCD.VisibleFalseLock = false;
            // 
            // obic7TextDatetimeBaseDate
            // 
            this.obic7TextDatetimeBaseDate.AttachedButton = null;
            this.obic7TextDatetimeBaseDate.AttachedLabel = null;
            this.obic7TextDatetimeBaseDate.AttachedSearchDialog = null;
            this.obic7TextDatetimeBaseDate.CheckOnClose = false;
            this.obic7TextDatetimeBaseDate.ColumnNumber = 0;
            this.obic7TextDatetimeBaseDate.EnableFalseLock = false;
            this.obic7TextDatetimeBaseDate.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextDatetimeBaseDate.ErrorString = null;
            this.obic7TextDatetimeBaseDate.Format = "yyyy/MM/dd";
            this.obic7TextDatetimeBaseDate.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.obic7TextDatetimeBaseDate.IsKeyColumn = false;
            this.obic7TextDatetimeBaseDate.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextDatetimeBaseDate.Location = new System.Drawing.Point(150, 51);
            this.obic7TextDatetimeBaseDate.MaxLengthByte = 10;
            this.obic7TextDatetimeBaseDate.MaxValue = new System.DateTime(2100, 1, 1, 0, 0, 0, 0);
            this.obic7TextDatetimeBaseDate.MinValue = new System.DateTime(1868, 9, 8, 0, 0, 0, 0);
            this.obic7TextDatetimeBaseDate.Name = "obic7TextDatetimeBaseDate";
            this.obic7TextDatetimeBaseDate.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextDatetimeBaseDate.Size = new System.Drawing.Size(72, 19);
            this.obic7TextDatetimeBaseDate.TabIndex = 1020;
            this.obic7TextDatetimeBaseDate.UseJapaneseCalendar = false;
            this.obic7TextDatetimeBaseDate.Value = null;
            this.obic7TextDatetimeBaseDate.VisibleFalseLock = false;
            this.obic7TextDatetimeBaseDate.Enter += new System.EventHandler(this.SetFunctionButtonHeader);
            this.obic7TextDatetimeBaseDate.Validating += new System.ComponentModel.CancelEventHandler(this.obic7TextDatetimeBaseDate_Validating);
            // 
            // obic7TextStringZigyoushoCD
            // 
            this.obic7TextStringZigyoushoCD.AllowAlphabet = true;
            this.obic7TextStringZigyoushoCD.AllowKana = false;
            this.obic7TextStringZigyoushoCD.AllowNumber = true;
            this.obic7TextStringZigyoushoCD.AllowSymbol = false;
            this.obic7TextStringZigyoushoCD.AllowUnicode = false;
            this.obic7TextStringZigyoushoCD.AllowZenkaku = false;
            this.obic7TextStringZigyoushoCD.AttachedButton = this.obic7ButtonWithLogicZigyoushoCDSearch;
            this.obic7TextStringZigyoushoCD.AttachedLabel = this.obic7LabelStringZigyoushoCD;
            this.obic7TextStringZigyoushoCD.AttachedSearchDialog = null;
            this.obic7TextStringZigyoushoCD.CheckOnClose = false;
            this.obic7TextStringZigyoushoCD.ColumnNumber = 0;
            this.obic7TextStringZigyoushoCD.DispFuriganaText = null;
            this.obic7TextStringZigyoushoCD.EnableFalseLock = false;
            this.obic7TextStringZigyoushoCD.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringZigyoushoCD.ErrorString = null;
            this.obic7TextStringZigyoushoCD.IsKeyColumn = true;
            this.obic7TextStringZigyoushoCD.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringZigyoushoCD.Location = new System.Drawing.Point(150, 75);
            this.obic7TextStringZigyoushoCD.MaxLengthByte = 6;
            this.obic7TextStringZigyoushoCD.Name = "obic7TextStringZigyoushoCD";
            this.obic7TextStringZigyoushoCD.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringZigyoushoCD.Size = new System.Drawing.Size(48, 19);
            this.obic7TextStringZigyoushoCD.TabIndex = 1030;
            this.obic7TextStringZigyoushoCD.Value = "";
            this.obic7TextStringZigyoushoCD.VisibleFalseLock = false;
            this.obic7TextStringZigyoushoCD.Enter += new System.EventHandler(this.obic7TextStringZigyoushoCD_Enter);
            this.obic7TextStringZigyoushoCD.Validating += new System.ComponentModel.CancelEventHandler(this.obic7TextStringZigyoushoCD_Validating);
            // 
            // obic7ButtonWithLogicZigyoushoCDSearch
            // 
            this.obic7ButtonWithLogicZigyoushoCDSearch.ColumnNumber = 0;
            this.obic7ButtonWithLogicZigyoushoCDSearch.EnableFalseLock = false;
            this.obic7ButtonWithLogicZigyoushoCDSearch.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.obic7ButtonWithLogicZigyoushoCDSearch.Location = new System.Drawing.Point(198, 73);
            this.obic7ButtonWithLogicZigyoushoCDSearch.Name = "obic7ButtonWithLogicZigyoushoCDSearch";
            this.obic7ButtonWithLogicZigyoushoCDSearch.Obic7JobSecurity = Obic7fx.Com.A0000.Tool.Obic7JobSecurities.NoSecurity;
            this.obic7ButtonWithLogicZigyoushoCDSearch.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7ButtonWithLogicZigyoushoCDSearch.Size = new System.Drawing.Size(24, 23);
            this.obic7ButtonWithLogicZigyoushoCDSearch.TabIndex = 1040;
            this.obic7ButtonWithLogicZigyoushoCDSearch.Text = "...";
            this.obic7ButtonWithLogicZigyoushoCDSearch.UseVisualStyleBackColor = true;
            this.obic7ButtonWithLogicZigyoushoCDSearch.VisibleFalseLock = false;
            this.obic7ButtonWithLogicZigyoushoCDSearch.Click += new System.EventHandler(this.obic7ButtonWithLogicZigyoushoCDSearch_Click);
            this.obic7ButtonWithLogicZigyoushoCDSearch.Enter += new System.EventHandler(this.SetFunctionButtonHeader);
            // 
            // obic7LabelStringZigyoushoName
            // 
            this.obic7LabelStringZigyoushoName.AutoEllipsis = true;
            this.obic7LabelStringZigyoushoName.ColumnNumber = 0;
            this.obic7LabelStringZigyoushoName.EnableFalseLock = false;
            this.obic7LabelStringZigyoushoName.ErrorString = null;
            this.obic7LabelStringZigyoushoName.Location = new System.Drawing.Point(222, 75);
            this.obic7LabelStringZigyoushoName.Name = "obic7LabelStringZigyoushoName";
            this.obic7LabelStringZigyoushoName.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7LabelStringZigyoushoName.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringZigyoushoName.Size = new System.Drawing.Size(132, 19);
            this.obic7LabelStringZigyoushoName.TabIndex = 1050;
            this.obic7LabelStringZigyoushoName.Value = "";
            this.obic7LabelStringZigyoushoName.VisibleFalseLock = false;
            // 
            // obic7ButtonWithLogicDisp
            // 
            this.obic7ButtonWithLogicDisp.ColumnNumber = 0;
            this.obic7ButtonWithLogicDisp.EnableFalseLock = false;
            this.obic7ButtonWithLogicDisp.Location = new System.Drawing.Point(354, 73);
            this.obic7ButtonWithLogicDisp.Name = "obic7ButtonWithLogicDisp";
            this.obic7ButtonWithLogicDisp.Obic7JobSecurity = Obic7fx.Com.A0000.Tool.Obic7JobSecurities.NoSecurity;
            this.obic7ButtonWithLogicDisp.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7ButtonWithLogicDisp.Size = new System.Drawing.Size(78, 23);
            this.obic7ButtonWithLogicDisp.TabIndex = 1060;
            this.obic7ButtonWithLogicDisp.Text = "表示";
            this.obic7ButtonWithLogicDisp.UseVisualStyleBackColor = true;
            this.obic7ButtonWithLogicDisp.VisibleFalseLock = false;
            this.obic7ButtonWithLogicDisp.Click += new System.EventHandler(this.obic7ButtonWithLogicDisp_Click);
            this.obic7ButtonWithLogicDisp.Enter += new System.EventHandler(this.SetFunctionButtonHeader);
            // 
            // obic7GroupBoxBasicInfo
            // 
            this.obic7GroupBoxBasicInfo.ColumnNumber = 0;
            this.obic7GroupBoxBasicInfo.Controls.Add(this.obic7LabelStringZititaiName);
            this.obic7GroupBoxBasicInfo.Controls.Add(this.obic7ButtonWithLogicZititaiCDSearch);
            this.obic7GroupBoxBasicInfo.Controls.Add(this.obic7LabelStringZititai);
            this.obic7GroupBoxBasicInfo.Controls.Add(this.obic7TextStringZititaiCD);
            this.obic7GroupBoxBasicInfo.EnableFalseLock = false;
            this.obic7GroupBoxBasicInfo.Location = new System.Drawing.Point(54, 27);
            this.obic7GroupBoxBasicInfo.Name = "obic7GroupBoxBasicInfo";
            this.obic7GroupBoxBasicInfo.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7GroupBoxBasicInfo.Size = new System.Drawing.Size(852, 48);
            this.obic7GroupBoxBasicInfo.TabIndex = 2000;
            this.obic7GroupBoxBasicInfo.TabStop = false;
            this.obic7GroupBoxBasicInfo.Text = "基本情報";
            this.obic7GroupBoxBasicInfo.VisibleFalseLock = false;
            // 
            // obic7LabelStringZititaiName
            // 
            this.obic7LabelStringZititaiName.AutoEllipsis = true;
            this.obic7LabelStringZititaiName.BackColor = System.Drawing.SystemColors.Control;
            this.obic7LabelStringZititaiName.ColumnNumber = 0;
            this.obic7LabelStringZititaiName.EnableFalseLock = false;
            this.obic7LabelStringZititaiName.ErrorString = null;
            this.obic7LabelStringZititaiName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelStringZititaiName.Location = new System.Drawing.Point(168, 24);
            this.obic7LabelStringZititaiName.Name = "obic7LabelStringZititaiName";
            this.obic7LabelStringZititaiName.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7LabelStringZititaiName.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringZititaiName.Size = new System.Drawing.Size(252, 19);
            this.obic7LabelStringZititaiName.TabIndex = 2030;
            this.obic7LabelStringZititaiName.Value = "";
            this.obic7LabelStringZititaiName.VisibleFalseLock = false;
            // 
            // obic7ButtonWithLogicZititaiCDSearch
            // 
            this.obic7ButtonWithLogicZititaiCDSearch.ColumnNumber = 0;
            this.obic7ButtonWithLogicZititaiCDSearch.EnableFalseLock = false;
            this.obic7ButtonWithLogicZititaiCDSearch.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.obic7ButtonWithLogicZititaiCDSearch.Location = new System.Drawing.Point(144, 22);
            this.obic7ButtonWithLogicZititaiCDSearch.Name = "obic7ButtonWithLogicZititaiCDSearch";
            this.obic7ButtonWithLogicZititaiCDSearch.Obic7JobSecurity = Obic7fx.Com.A0000.Tool.Obic7JobSecurities.NoSecurity;
            this.obic7ButtonWithLogicZititaiCDSearch.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7ButtonWithLogicZititaiCDSearch.Size = new System.Drawing.Size(24, 23);
            this.obic7ButtonWithLogicZititaiCDSearch.TabIndex = 2020;
            this.obic7ButtonWithLogicZititaiCDSearch.Text = "...";
            this.obic7ButtonWithLogicZititaiCDSearch.UseVisualStyleBackColor = true;
            this.obic7ButtonWithLogicZititaiCDSearch.VisibleFalseLock = false;
            this.obic7ButtonWithLogicZititaiCDSearch.Click += new System.EventHandler(this.obic7ButtonWithLogicZititaiCDSearch_Click);
            this.obic7ButtonWithLogicZititaiCDSearch.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7LabelStringZititai
            // 
            this.obic7LabelStringZititai.AutoEllipsis = true;
            this.obic7LabelStringZititai.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelStringZititai.ColumnNumber = 0;
            this.obic7LabelStringZititai.EnableFalseLock = false;
            this.obic7LabelStringZititai.ErrorString = null;
            this.obic7LabelStringZititai.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.obic7LabelStringZititai.Location = new System.Drawing.Point(6, 24);
            this.obic7LabelStringZititai.Name = "obic7LabelStringZititai";
            this.obic7LabelStringZititai.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringZititai.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringZititai.Size = new System.Drawing.Size(84, 19);
            this.obic7LabelStringZititai.TabIndex = 0;
            this.obic7LabelStringZititai.Text = "申告自治体";
            this.obic7LabelStringZititai.Value = "申告自治体";
            this.obic7LabelStringZititai.VisibleFalseLock = false;
            // 
            // obic7TextStringZititaiCD
            // 
            this.obic7TextStringZititaiCD.AllowAlphabet = true;
            this.obic7TextStringZititaiCD.AllowKana = false;
            this.obic7TextStringZititaiCD.AllowNumber = true;
            this.obic7TextStringZititaiCD.AllowSymbol = false;
            this.obic7TextStringZititaiCD.AllowUnicode = false;
            this.obic7TextStringZititaiCD.AllowZenkaku = false;
            this.obic7TextStringZititaiCD.AttachedButton = this.obic7ButtonWithLogicZititaiCDSearch;
            this.obic7TextStringZititaiCD.AttachedLabel = this.obic7LabelStringZititai;
            this.obic7TextStringZititaiCD.AttachedSearchDialog = null;
            this.obic7TextStringZititaiCD.CheckOnClose = false;
            this.obic7TextStringZititaiCD.ColumnNumber = 0;
            this.obic7TextStringZititaiCD.DispFuriganaText = null;
            this.obic7TextStringZititaiCD.EnableFalseLock = false;
            this.obic7TextStringZititaiCD.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringZititaiCD.ErrorString = null;
            this.obic7TextStringZititaiCD.IsKeyColumn = false;
            this.obic7TextStringZititaiCD.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringZititaiCD.Location = new System.Drawing.Point(96, 24);
            this.obic7TextStringZititaiCD.MaxLengthByte = 6;
            this.obic7TextStringZititaiCD.Name = "obic7TextStringZititaiCD";
            this.obic7TextStringZititaiCD.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringZititaiCD.Size = new System.Drawing.Size(48, 19);
            this.obic7TextStringZititaiCD.TabIndex = 2010;
            this.obic7TextStringZititaiCD.Value = "";
            this.obic7TextStringZititaiCD.VisibleFalseLock = false;
            this.obic7TextStringZititaiCD.Enter += new System.EventHandler(this.controlDetailSearch_Enter);
            this.obic7TextStringZititaiCD.Validating += new System.ComponentModel.CancelEventHandler(this.obic7TextStringZititaiCD_Validating);
            // 
            // obic7LabelStringPostCD
            // 
            this.obic7LabelStringPostCD.AutoEllipsis = true;
            this.obic7LabelStringPostCD.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelStringPostCD.ColumnNumber = 0;
            this.obic7LabelStringPostCD.EnableFalseLock = false;
            this.obic7LabelStringPostCD.ErrorString = null;
            this.obic7LabelStringPostCD.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.obic7LabelStringPostCD.Location = new System.Drawing.Point(6, 24);
            this.obic7LabelStringPostCD.Name = "obic7LabelStringPostCD";
            this.obic7LabelStringPostCD.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringPostCD.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringPostCD.Size = new System.Drawing.Size(84, 19);
            this.obic7LabelStringPostCD.TabIndex = 1000000001;
            this.obic7LabelStringPostCD.Text = "郵便番号";
            this.obic7LabelStringPostCD.Value = "郵便番号";
            this.obic7LabelStringPostCD.VisibleFalseLock = false;
            // 
            // obic7TextStringPostCD1
            // 
            this.obic7TextStringPostCD1.AllowAlphabet = false;
            this.obic7TextStringPostCD1.AllowKana = false;
            this.obic7TextStringPostCD1.AllowNumber = true;
            this.obic7TextStringPostCD1.AllowSymbol = false;
            this.obic7TextStringPostCD1.AllowUnicode = false;
            this.obic7TextStringPostCD1.AllowZenkaku = false;
            this.obic7TextStringPostCD1.AttachedButton = this.obic7ButtonWithLogicPostCDSearch;
            this.obic7TextStringPostCD1.AttachedLabel = null;
            this.obic7TextStringPostCD1.AttachedSearchDialog = null;
            this.obic7TextStringPostCD1.CheckOnClose = false;
            this.obic7TextStringPostCD1.ColumnNumber = 0;
            this.obic7TextStringPostCD1.DispFuriganaText = null;
            this.obic7TextStringPostCD1.EnableFalseLock = false;
            this.obic7TextStringPostCD1.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringPostCD1.ErrorString = null;
            this.obic7TextStringPostCD1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringPostCD1.IsKeyColumn = false;
            this.obic7TextStringPostCD1.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringPostCD1.Location = new System.Drawing.Point(96, 24);
            this.obic7TextStringPostCD1.MaxLengthByte = 3;
            this.obic7TextStringPostCD1.Name = "obic7TextStringPostCD1";
            this.obic7TextStringPostCD1.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringPostCD1.Size = new System.Drawing.Size(30, 19);
            this.obic7TextStringPostCD1.TabIndex = 2110;
            this.obic7TextStringPostCD1.Value = "";
            this.obic7TextStringPostCD1.VisibleFalseLock = false;
            this.obic7TextStringPostCD1.Enter += new System.EventHandler(this.controlDetailSearch_Enter);
            // 
            // obic7ButtonWithLogicPostCDSearch
            // 
            this.obic7ButtonWithLogicPostCDSearch.ColumnNumber = 0;
            this.obic7ButtonWithLogicPostCDSearch.EnableFalseLock = false;
            this.obic7ButtonWithLogicPostCDSearch.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.obic7ButtonWithLogicPostCDSearch.Location = new System.Drawing.Point(174, 22);
            this.obic7ButtonWithLogicPostCDSearch.Name = "obic7ButtonWithLogicPostCDSearch";
            this.obic7ButtonWithLogicPostCDSearch.Obic7JobSecurity = Obic7fx.Com.A0000.Tool.Obic7JobSecurities.NoSecurity;
            this.obic7ButtonWithLogicPostCDSearch.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7ButtonWithLogicPostCDSearch.Size = new System.Drawing.Size(24, 23);
            this.obic7ButtonWithLogicPostCDSearch.TabIndex = 2130;
            this.obic7ButtonWithLogicPostCDSearch.Text = "...";
            this.obic7ButtonWithLogicPostCDSearch.UseVisualStyleBackColor = true;
            this.obic7ButtonWithLogicPostCDSearch.VisibleFalseLock = false;
            this.obic7ButtonWithLogicPostCDSearch.Click += new System.EventHandler(this.obic7ButtonWithLogicPostCDSearch_Click);
            this.obic7ButtonWithLogicPostCDSearch.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7LabelStringPostCDHaihun
            // 
            this.obic7LabelStringPostCDHaihun.AutoEllipsis = true;
            this.obic7LabelStringPostCDHaihun.ColumnNumber = 0;
            this.obic7LabelStringPostCDHaihun.EnableFalseLock = false;
            this.obic7LabelStringPostCDHaihun.ErrorString = null;
            this.obic7LabelStringPostCDHaihun.Location = new System.Drawing.Point(125, 24);
            this.obic7LabelStringPostCDHaihun.Name = "obic7LabelStringPostCDHaihun";
            this.obic7LabelStringPostCDHaihun.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7LabelStringPostCDHaihun.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringPostCDHaihun.Size = new System.Drawing.Size(13, 19);
            this.obic7LabelStringPostCDHaihun.TabIndex = 1000000003;
            this.obic7LabelStringPostCDHaihun.Text = "-";
            this.obic7LabelStringPostCDHaihun.Value = "-";
            this.obic7LabelStringPostCDHaihun.VisibleFalseLock = false;
            // 
            // obic7TextStringPostCD2
            // 
            this.obic7TextStringPostCD2.AllowAlphabet = false;
            this.obic7TextStringPostCD2.AllowKana = false;
            this.obic7TextStringPostCD2.AllowNumber = true;
            this.obic7TextStringPostCD2.AllowSymbol = false;
            this.obic7TextStringPostCD2.AllowUnicode = false;
            this.obic7TextStringPostCD2.AllowZenkaku = false;
            this.obic7TextStringPostCD2.AttachedButton = this.obic7ButtonWithLogicPostCDSearch;
            this.obic7TextStringPostCD2.AttachedLabel = null;
            this.obic7TextStringPostCD2.AttachedSearchDialog = null;
            this.obic7TextStringPostCD2.CheckOnClose = false;
            this.obic7TextStringPostCD2.ColumnNumber = 0;
            this.obic7TextStringPostCD2.DispFuriganaText = null;
            this.obic7TextStringPostCD2.EnableFalseLock = false;
            this.obic7TextStringPostCD2.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringPostCD2.ErrorString = null;
            this.obic7TextStringPostCD2.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringPostCD2.IsKeyColumn = false;
            this.obic7TextStringPostCD2.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringPostCD2.Location = new System.Drawing.Point(138, 24);
            this.obic7TextStringPostCD2.MaxLengthByte = 4;
            this.obic7TextStringPostCD2.Name = "obic7TextStringPostCD2";
            this.obic7TextStringPostCD2.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringPostCD2.Size = new System.Drawing.Size(36, 19);
            this.obic7TextStringPostCD2.TabIndex = 2120;
            this.obic7TextStringPostCD2.Value = "";
            this.obic7TextStringPostCD2.VisibleFalseLock = false;
            this.obic7TextStringPostCD2.Enter += new System.EventHandler(this.controlDetailSearch_Enter);
            this.obic7TextStringPostCD2.Validating += new System.ComponentModel.CancelEventHandler(this.obic7TextStringPostCD2_Validating);
            // 
            // obic7LabelStringAddress1
            // 
            this.obic7LabelStringAddress1.AutoEllipsis = true;
            this.obic7LabelStringAddress1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelStringAddress1.ColumnNumber = 0;
            this.obic7LabelStringAddress1.EnableFalseLock = false;
            this.obic7LabelStringAddress1.ErrorString = null;
            this.obic7LabelStringAddress1.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.obic7LabelStringAddress1.Location = new System.Drawing.Point(6, 48);
            this.obic7LabelStringAddress1.Name = "obic7LabelStringAddress1";
            this.obic7LabelStringAddress1.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringAddress1.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringAddress1.Size = new System.Drawing.Size(84, 19);
            this.obic7LabelStringAddress1.TabIndex = 1000000006;
            this.obic7LabelStringAddress1.Text = "住所1";
            this.obic7LabelStringAddress1.Value = "住所1";
            this.obic7LabelStringAddress1.VisibleFalseLock = false;
            // 
            // obic7TextStringAddress1
            // 
            this.obic7TextStringAddress1.AllowAlphabet = true;
            this.obic7TextStringAddress1.AllowKana = true;
            this.obic7TextStringAddress1.AllowNumber = true;
            this.obic7TextStringAddress1.AllowSymbol = true;
            this.obic7TextStringAddress1.AllowUnicode = false;
            this.obic7TextStringAddress1.AllowZenkaku = true;
            this.obic7TextStringAddress1.AttachedButton = null;
            this.obic7TextStringAddress1.AttachedLabel = this.obic7LabelStringAddress1;
            this.obic7TextStringAddress1.AttachedSearchDialog = null;
            this.obic7TextStringAddress1.CheckOnClose = false;
            this.obic7TextStringAddress1.ColumnNumber = 0;
            this.obic7TextStringAddress1.DispFuriganaText = null;
            this.obic7TextStringAddress1.EnableFalseLock = false;
            this.obic7TextStringAddress1.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringAddress1.ErrorString = null;
            this.obic7TextStringAddress1.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.obic7TextStringAddress1.IsKeyColumn = false;
            this.obic7TextStringAddress1.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringAddress1.Location = new System.Drawing.Point(96, 48);
            this.obic7TextStringAddress1.MaxLengthByte = 40;
            this.obic7TextStringAddress1.Name = "obic7TextStringAddress1";
            this.obic7TextStringAddress1.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringAddress1.Size = new System.Drawing.Size(252, 19);
            this.obic7TextStringAddress1.TabIndex = 2140;
            this.obic7TextStringAddress1.Value = "";
            this.obic7TextStringAddress1.VisibleFalseLock = false;
            this.obic7TextStringAddress1.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7TextStringAddress1Kana
            // 
            this.obic7TextStringAddress1Kana.AllowAlphabet = true;
            this.obic7TextStringAddress1Kana.AllowKana = true;
            this.obic7TextStringAddress1Kana.AllowNumber = true;
            this.obic7TextStringAddress1Kana.AllowSymbol = true;
            this.obic7TextStringAddress1Kana.AllowUnicode = false;
            this.obic7TextStringAddress1Kana.AllowZenkaku = true;
            this.obic7TextStringAddress1Kana.AttachedButton = null;
            this.obic7TextStringAddress1Kana.AttachedLabel = this.obic7LabelStringAddress1Kana;
            this.obic7TextStringAddress1Kana.AttachedSearchDialog = null;
            this.obic7TextStringAddress1Kana.CheckOnClose = false;
            this.obic7TextStringAddress1Kana.ColumnNumber = 0;
            this.obic7TextStringAddress1Kana.DispFuriganaText = null;
            this.obic7TextStringAddress1Kana.EnableFalseLock = false;
            this.obic7TextStringAddress1Kana.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringAddress1Kana.ErrorString = null;
            this.obic7TextStringAddress1Kana.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.obic7TextStringAddress1Kana.IsKeyColumn = false;
            this.obic7TextStringAddress1Kana.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringAddress1Kana.Location = new System.Drawing.Point(96, 96);
            this.obic7TextStringAddress1Kana.MaxLengthByte = 40;
            this.obic7TextStringAddress1Kana.Name = "obic7TextStringAddress1Kana";
            this.obic7TextStringAddress1Kana.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringAddress1Kana.Size = new System.Drawing.Size(252, 19);
            this.obic7TextStringAddress1Kana.TabIndex = 2160;
            this.obic7TextStringAddress1Kana.Value = "";
            this.obic7TextStringAddress1Kana.VisibleFalseLock = false;
            this.obic7TextStringAddress1Kana.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7LabelStringAddress1Kana
            // 
            this.obic7LabelStringAddress1Kana.AutoEllipsis = true;
            this.obic7LabelStringAddress1Kana.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelStringAddress1Kana.ColumnNumber = 0;
            this.obic7LabelStringAddress1Kana.EnableFalseLock = false;
            this.obic7LabelStringAddress1Kana.ErrorString = null;
            this.obic7LabelStringAddress1Kana.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.obic7LabelStringAddress1Kana.Location = new System.Drawing.Point(6, 96);
            this.obic7LabelStringAddress1Kana.Name = "obic7LabelStringAddress1Kana";
            this.obic7LabelStringAddress1Kana.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringAddress1Kana.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringAddress1Kana.Size = new System.Drawing.Size(84, 19);
            this.obic7LabelStringAddress1Kana.TabIndex = 1000000010;
            this.obic7LabelStringAddress1Kana.Text = "住所1カナ";
            this.obic7LabelStringAddress1Kana.Value = "住所1カナ";
            this.obic7LabelStringAddress1Kana.VisibleFalseLock = false;
            // 
            // obic7LabelStringAddress2
            // 
            this.obic7LabelStringAddress2.AutoEllipsis = true;
            this.obic7LabelStringAddress2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelStringAddress2.ColumnNumber = 0;
            this.obic7LabelStringAddress2.EnableFalseLock = false;
            this.obic7LabelStringAddress2.ErrorString = null;
            this.obic7LabelStringAddress2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.obic7LabelStringAddress2.Location = new System.Drawing.Point(6, 72);
            this.obic7LabelStringAddress2.Name = "obic7LabelStringAddress2";
            this.obic7LabelStringAddress2.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringAddress2.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringAddress2.Size = new System.Drawing.Size(84, 19);
            this.obic7LabelStringAddress2.TabIndex = 1000000008;
            this.obic7LabelStringAddress2.Text = "住所2";
            this.obic7LabelStringAddress2.Value = "住所2";
            this.obic7LabelStringAddress2.VisibleFalseLock = false;
            // 
            // obic7TextStringAddress2
            // 
            this.obic7TextStringAddress2.AllowAlphabet = true;
            this.obic7TextStringAddress2.AllowKana = true;
            this.obic7TextStringAddress2.AllowNumber = true;
            this.obic7TextStringAddress2.AllowSymbol = true;
            this.obic7TextStringAddress2.AllowUnicode = false;
            this.obic7TextStringAddress2.AllowZenkaku = true;
            this.obic7TextStringAddress2.AttachedButton = null;
            this.obic7TextStringAddress2.AttachedLabel = this.obic7LabelStringAddress2;
            this.obic7TextStringAddress2.AttachedSearchDialog = null;
            this.obic7TextStringAddress2.CheckOnClose = false;
            this.obic7TextStringAddress2.ColumnNumber = 0;
            this.obic7TextStringAddress2.DispFuriganaText = null;
            this.obic7TextStringAddress2.EnableFalseLock = false;
            this.obic7TextStringAddress2.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringAddress2.ErrorString = null;
            this.obic7TextStringAddress2.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.obic7TextStringAddress2.IsKeyColumn = false;
            this.obic7TextStringAddress2.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringAddress2.Location = new System.Drawing.Point(96, 72);
            this.obic7TextStringAddress2.MaxLengthByte = 40;
            this.obic7TextStringAddress2.Name = "obic7TextStringAddress2";
            this.obic7TextStringAddress2.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringAddress2.Size = new System.Drawing.Size(252, 19);
            this.obic7TextStringAddress2.TabIndex = 2150;
            this.obic7TextStringAddress2.Value = "";
            this.obic7TextStringAddress2.VisibleFalseLock = false;
            this.obic7TextStringAddress2.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7TextStringAddress2Kana
            // 
            this.obic7TextStringAddress2Kana.AllowAlphabet = true;
            this.obic7TextStringAddress2Kana.AllowKana = true;
            this.obic7TextStringAddress2Kana.AllowNumber = true;
            this.obic7TextStringAddress2Kana.AllowSymbol = true;
            this.obic7TextStringAddress2Kana.AllowUnicode = false;
            this.obic7TextStringAddress2Kana.AllowZenkaku = true;
            this.obic7TextStringAddress2Kana.AttachedButton = null;
            this.obic7TextStringAddress2Kana.AttachedLabel = this.obic7LabelStringAddress2Kana;
            this.obic7TextStringAddress2Kana.AttachedSearchDialog = null;
            this.obic7TextStringAddress2Kana.CheckOnClose = false;
            this.obic7TextStringAddress2Kana.ColumnNumber = 0;
            this.obic7TextStringAddress2Kana.DispFuriganaText = null;
            this.obic7TextStringAddress2Kana.EnableFalseLock = false;
            this.obic7TextStringAddress2Kana.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringAddress2Kana.ErrorString = null;
            this.obic7TextStringAddress2Kana.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.obic7TextStringAddress2Kana.IsKeyColumn = false;
            this.obic7TextStringAddress2Kana.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringAddress2Kana.Location = new System.Drawing.Point(96, 120);
            this.obic7TextStringAddress2Kana.MaxLengthByte = 40;
            this.obic7TextStringAddress2Kana.Name = "obic7TextStringAddress2Kana";
            this.obic7TextStringAddress2Kana.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringAddress2Kana.Size = new System.Drawing.Size(252, 19);
            this.obic7TextStringAddress2Kana.TabIndex = 2170;
            this.obic7TextStringAddress2Kana.Value = "";
            this.obic7TextStringAddress2Kana.VisibleFalseLock = false;
            this.obic7TextStringAddress2Kana.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7LabelStringAddress2Kana
            // 
            this.obic7LabelStringAddress2Kana.AutoEllipsis = true;
            this.obic7LabelStringAddress2Kana.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelStringAddress2Kana.ColumnNumber = 0;
            this.obic7LabelStringAddress2Kana.EnableFalseLock = false;
            this.obic7LabelStringAddress2Kana.ErrorString = null;
            this.obic7LabelStringAddress2Kana.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.obic7LabelStringAddress2Kana.Location = new System.Drawing.Point(6, 120);
            this.obic7LabelStringAddress2Kana.Name = "obic7LabelStringAddress2Kana";
            this.obic7LabelStringAddress2Kana.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringAddress2Kana.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringAddress2Kana.Size = new System.Drawing.Size(84, 19);
            this.obic7LabelStringAddress2Kana.TabIndex = 1000000012;
            this.obic7LabelStringAddress2Kana.Text = "住所2カナ";
            this.obic7LabelStringAddress2Kana.Value = "住所2カナ";
            this.obic7LabelStringAddress2Kana.VisibleFalseLock = false;
            // 
            // obic7LabelStringTelNo
            // 
            this.obic7LabelStringTelNo.AutoEllipsis = true;
            this.obic7LabelStringTelNo.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelStringTelNo.ColumnNumber = 0;
            this.obic7LabelStringTelNo.EnableFalseLock = false;
            this.obic7LabelStringTelNo.ErrorString = null;
            this.obic7LabelStringTelNo.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.obic7LabelStringTelNo.Location = new System.Drawing.Point(6, 144);
            this.obic7LabelStringTelNo.Name = "obic7LabelStringTelNo";
            this.obic7LabelStringTelNo.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringTelNo.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringTelNo.Size = new System.Drawing.Size(84, 19);
            this.obic7LabelStringTelNo.TabIndex = 1000000014;
            this.obic7LabelStringTelNo.Text = "電話番号";
            this.obic7LabelStringTelNo.Value = "電話番号";
            this.obic7LabelStringTelNo.VisibleFalseLock = false;
            // 
            // obic7TextStringTelNo1
            // 
            this.obic7TextStringTelNo1.AllowAlphabet = false;
            this.obic7TextStringTelNo1.AllowKana = false;
            this.obic7TextStringTelNo1.AllowNumber = true;
            this.obic7TextStringTelNo1.AllowSymbol = false;
            this.obic7TextStringTelNo1.AllowUnicode = false;
            this.obic7TextStringTelNo1.AllowZenkaku = false;
            this.obic7TextStringTelNo1.AttachedButton = null;
            this.obic7TextStringTelNo1.AttachedLabel = null;
            this.obic7TextStringTelNo1.AttachedSearchDialog = null;
            this.obic7TextStringTelNo1.CheckOnClose = false;
            this.obic7TextStringTelNo1.ColumnNumber = 0;
            this.obic7TextStringTelNo1.DispFuriganaText = null;
            this.obic7TextStringTelNo1.EnableFalseLock = false;
            this.obic7TextStringTelNo1.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringTelNo1.ErrorString = null;
            this.obic7TextStringTelNo1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringTelNo1.IsKeyColumn = false;
            this.obic7TextStringTelNo1.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringTelNo1.Location = new System.Drawing.Point(96, 144);
            this.obic7TextStringTelNo1.MaxLengthByte = 6;
            this.obic7TextStringTelNo1.Name = "obic7TextStringTelNo1";
            this.obic7TextStringTelNo1.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringTelNo1.Size = new System.Drawing.Size(48, 19);
            this.obic7TextStringTelNo1.TabIndex = 2180;
            this.obic7TextStringTelNo1.Value = "";
            this.obic7TextStringTelNo1.VisibleFalseLock = false;
            this.obic7TextStringTelNo1.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7LabelStringTelNoHaihun1
            // 
            this.obic7LabelStringTelNoHaihun1.AutoEllipsis = true;
            this.obic7LabelStringTelNoHaihun1.ColumnNumber = 0;
            this.obic7LabelStringTelNoHaihun1.EnableFalseLock = false;
            this.obic7LabelStringTelNoHaihun1.ErrorString = null;
            this.obic7LabelStringTelNoHaihun1.Location = new System.Drawing.Point(143, 144);
            this.obic7LabelStringTelNoHaihun1.Name = "obic7LabelStringTelNoHaihun1";
            this.obic7LabelStringTelNoHaihun1.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7LabelStringTelNoHaihun1.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringTelNoHaihun1.Size = new System.Drawing.Size(13, 19);
            this.obic7LabelStringTelNoHaihun1.TabIndex = 1000000017;
            this.obic7LabelStringTelNoHaihun1.Text = "-";
            this.obic7LabelStringTelNoHaihun1.Value = "-";
            this.obic7LabelStringTelNoHaihun1.VisibleFalseLock = false;
            // 
            // obic7TextStringTelNo2
            // 
            this.obic7TextStringTelNo2.AllowAlphabet = false;
            this.obic7TextStringTelNo2.AllowKana = false;
            this.obic7TextStringTelNo2.AllowNumber = true;
            this.obic7TextStringTelNo2.AllowSymbol = false;
            this.obic7TextStringTelNo2.AllowUnicode = false;
            this.obic7TextStringTelNo2.AllowZenkaku = false;
            this.obic7TextStringTelNo2.AttachedButton = null;
            this.obic7TextStringTelNo2.AttachedLabel = null;
            this.obic7TextStringTelNo2.AttachedSearchDialog = null;
            this.obic7TextStringTelNo2.CheckOnClose = false;
            this.obic7TextStringTelNo2.ColumnNumber = 0;
            this.obic7TextStringTelNo2.DispFuriganaText = null;
            this.obic7TextStringTelNo2.EnableFalseLock = false;
            this.obic7TextStringTelNo2.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringTelNo2.ErrorString = null;
            this.obic7TextStringTelNo2.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringTelNo2.IsKeyColumn = false;
            this.obic7TextStringTelNo2.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringTelNo2.Location = new System.Drawing.Point(156, 144);
            this.obic7TextStringTelNo2.MaxLengthByte = 5;
            this.obic7TextStringTelNo2.Name = "obic7TextStringTelNo2";
            this.obic7TextStringTelNo2.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringTelNo2.Size = new System.Drawing.Size(42, 19);
            this.obic7TextStringTelNo2.TabIndex = 2190;
            this.obic7TextStringTelNo2.Value = "";
            this.obic7TextStringTelNo2.VisibleFalseLock = false;
            this.obic7TextStringTelNo2.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7LabelStringTelNoHaihun2
            // 
            this.obic7LabelStringTelNoHaihun2.AutoEllipsis = true;
            this.obic7LabelStringTelNoHaihun2.ColumnNumber = 0;
            this.obic7LabelStringTelNoHaihun2.EnableFalseLock = false;
            this.obic7LabelStringTelNoHaihun2.ErrorString = null;
            this.obic7LabelStringTelNoHaihun2.Location = new System.Drawing.Point(197, 144);
            this.obic7LabelStringTelNoHaihun2.Name = "obic7LabelStringTelNoHaihun2";
            this.obic7LabelStringTelNoHaihun2.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7LabelStringTelNoHaihun2.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringTelNoHaihun2.Size = new System.Drawing.Size(13, 19);
            this.obic7LabelStringTelNoHaihun2.TabIndex = 1000000019;
            this.obic7LabelStringTelNoHaihun2.Text = "-";
            this.obic7LabelStringTelNoHaihun2.Value = "-";
            this.obic7LabelStringTelNoHaihun2.VisibleFalseLock = false;
            // 
            // obic7TextStringTelNo3
            // 
            this.obic7TextStringTelNo3.AllowAlphabet = false;
            this.obic7TextStringTelNo3.AllowKana = false;
            this.obic7TextStringTelNo3.AllowNumber = true;
            this.obic7TextStringTelNo3.AllowSymbol = false;
            this.obic7TextStringTelNo3.AllowUnicode = false;
            this.obic7TextStringTelNo3.AllowZenkaku = false;
            this.obic7TextStringTelNo3.AttachedButton = null;
            this.obic7TextStringTelNo3.AttachedLabel = null;
            this.obic7TextStringTelNo3.AttachedSearchDialog = null;
            this.obic7TextStringTelNo3.CheckOnClose = false;
            this.obic7TextStringTelNo3.ColumnNumber = 0;
            this.obic7TextStringTelNo3.DispFuriganaText = null;
            this.obic7TextStringTelNo3.EnableFalseLock = false;
            this.obic7TextStringTelNo3.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringTelNo3.ErrorString = null;
            this.obic7TextStringTelNo3.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringTelNo3.IsKeyColumn = false;
            this.obic7TextStringTelNo3.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringTelNo3.Location = new System.Drawing.Point(210, 144);
            this.obic7TextStringTelNo3.MaxLengthByte = 5;
            this.obic7TextStringTelNo3.Name = "obic7TextStringTelNo3";
            this.obic7TextStringTelNo3.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringTelNo3.Size = new System.Drawing.Size(42, 19);
            this.obic7TextStringTelNo3.TabIndex = 2200;
            this.obic7TextStringTelNo3.Value = "";
            this.obic7TextStringTelNo3.VisibleFalseLock = false;
            this.obic7TextStringTelNo3.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7LabelStringCompanyName
            // 
            this.obic7LabelStringCompanyName.AutoEllipsis = true;
            this.obic7LabelStringCompanyName.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelStringCompanyName.ColumnNumber = 0;
            this.obic7LabelStringCompanyName.EnableFalseLock = false;
            this.obic7LabelStringCompanyName.ErrorString = null;
            this.obic7LabelStringCompanyName.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.obic7LabelStringCompanyName.Location = new System.Drawing.Point(6, 168);
            this.obic7LabelStringCompanyName.Name = "obic7LabelStringCompanyName";
            this.obic7LabelStringCompanyName.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringCompanyName.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringCompanyName.Size = new System.Drawing.Size(84, 19);
            this.obic7LabelStringCompanyName.TabIndex = 1000000021;
            this.obic7LabelStringCompanyName.Text = "会社名";
            this.obic7LabelStringCompanyName.Value = "会社名";
            this.obic7LabelStringCompanyName.VisibleFalseLock = false;
            // 
            // obic7TextStringCompanyName
            // 
            this.obic7TextStringCompanyName.AllowAlphabet = true;
            this.obic7TextStringCompanyName.AllowKana = true;
            this.obic7TextStringCompanyName.AllowNumber = true;
            this.obic7TextStringCompanyName.AllowSymbol = true;
            this.obic7TextStringCompanyName.AllowUnicode = false;
            this.obic7TextStringCompanyName.AllowZenkaku = true;
            this.obic7TextStringCompanyName.AttachedButton = null;
            this.obic7TextStringCompanyName.AttachedLabel = this.obic7LabelStringCompanyName;
            this.obic7TextStringCompanyName.AttachedSearchDialog = null;
            this.obic7TextStringCompanyName.CheckOnClose = false;
            this.obic7TextStringCompanyName.ColumnNumber = 0;
            this.obic7TextStringCompanyName.DispFuriganaText = null;
            this.obic7TextStringCompanyName.EnableFalseLock = false;
            this.obic7TextStringCompanyName.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringCompanyName.ErrorString = null;
            this.obic7TextStringCompanyName.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.obic7TextStringCompanyName.IsKeyColumn = false;
            this.obic7TextStringCompanyName.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringCompanyName.Location = new System.Drawing.Point(96, 168);
            this.obic7TextStringCompanyName.MaxLengthByte = 40;
            this.obic7TextStringCompanyName.Name = "obic7TextStringCompanyName";
            this.obic7TextStringCompanyName.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringCompanyName.Size = new System.Drawing.Size(252, 19);
            this.obic7TextStringCompanyName.TabIndex = 2210;
            this.obic7TextStringCompanyName.Value = "";
            this.obic7TextStringCompanyName.VisibleFalseLock = false;
            this.obic7TextStringCompanyName.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7LabelStringCompanyKanaName
            // 
            this.obic7LabelStringCompanyKanaName.AutoEllipsis = true;
            this.obic7LabelStringCompanyKanaName.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelStringCompanyKanaName.ColumnNumber = 0;
            this.obic7LabelStringCompanyKanaName.EnableFalseLock = false;
            this.obic7LabelStringCompanyKanaName.ErrorString = null;
            this.obic7LabelStringCompanyKanaName.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.obic7LabelStringCompanyKanaName.Location = new System.Drawing.Point(6, 192);
            this.obic7LabelStringCompanyKanaName.Name = "obic7LabelStringCompanyKanaName";
            this.obic7LabelStringCompanyKanaName.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringCompanyKanaName.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringCompanyKanaName.Size = new System.Drawing.Size(84, 19);
            this.obic7LabelStringCompanyKanaName.TabIndex = 1000000023;
            this.obic7LabelStringCompanyKanaName.Text = "会社名カナ";
            this.obic7LabelStringCompanyKanaName.Value = "会社名カナ";
            this.obic7LabelStringCompanyKanaName.VisibleFalseLock = false;
            // 
            // obic7TextStringCompanyKanaName
            // 
            this.obic7TextStringCompanyKanaName.AllowAlphabet = true;
            this.obic7TextStringCompanyKanaName.AllowKana = true;
            this.obic7TextStringCompanyKanaName.AllowNumber = true;
            this.obic7TextStringCompanyKanaName.AllowSymbol = true;
            this.obic7TextStringCompanyKanaName.AllowUnicode = false;
            this.obic7TextStringCompanyKanaName.AllowZenkaku = true;
            this.obic7TextStringCompanyKanaName.AttachedButton = null;
            this.obic7TextStringCompanyKanaName.AttachedLabel = this.obic7LabelStringCompanyKanaName;
            this.obic7TextStringCompanyKanaName.AttachedSearchDialog = null;
            this.obic7TextStringCompanyKanaName.CheckOnClose = false;
            this.obic7TextStringCompanyKanaName.ColumnNumber = 0;
            this.obic7TextStringCompanyKanaName.DispFuriganaText = null;
            this.obic7TextStringCompanyKanaName.EnableFalseLock = false;
            this.obic7TextStringCompanyKanaName.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringCompanyKanaName.ErrorString = null;
            this.obic7TextStringCompanyKanaName.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.obic7TextStringCompanyKanaName.IsKeyColumn = false;
            this.obic7TextStringCompanyKanaName.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringCompanyKanaName.Location = new System.Drawing.Point(96, 192);
            this.obic7TextStringCompanyKanaName.MaxLengthByte = 40;
            this.obic7TextStringCompanyKanaName.Name = "obic7TextStringCompanyKanaName";
            this.obic7TextStringCompanyKanaName.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringCompanyKanaName.Size = new System.Drawing.Size(252, 19);
            this.obic7TextStringCompanyKanaName.TabIndex = 2220;
            this.obic7TextStringCompanyKanaName.Value = "";
            this.obic7TextStringCompanyKanaName.VisibleFalseLock = false;
            this.obic7TextStringCompanyKanaName.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7LabelStringDaihyoushaName
            // 
            this.obic7LabelStringDaihyoushaName.AutoEllipsis = true;
            this.obic7LabelStringDaihyoushaName.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelStringDaihyoushaName.ColumnNumber = 0;
            this.obic7LabelStringDaihyoushaName.EnableFalseLock = false;
            this.obic7LabelStringDaihyoushaName.ErrorString = null;
            this.obic7LabelStringDaihyoushaName.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.obic7LabelStringDaihyoushaName.Location = new System.Drawing.Point(6, 216);
            this.obic7LabelStringDaihyoushaName.Name = "obic7LabelStringDaihyoushaName";
            this.obic7LabelStringDaihyoushaName.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringDaihyoushaName.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringDaihyoushaName.Size = new System.Drawing.Size(84, 19);
            this.obic7LabelStringDaihyoushaName.TabIndex = 1000000025;
            this.obic7LabelStringDaihyoushaName.Text = "代表者氏名";
            this.obic7LabelStringDaihyoushaName.Value = "代表者氏名";
            this.obic7LabelStringDaihyoushaName.VisibleFalseLock = false;
            // 
            // obic7TextStringDaihyoushaName
            // 
            this.obic7TextStringDaihyoushaName.AllowAlphabet = true;
            this.obic7TextStringDaihyoushaName.AllowKana = true;
            this.obic7TextStringDaihyoushaName.AllowNumber = true;
            this.obic7TextStringDaihyoushaName.AllowSymbol = true;
            this.obic7TextStringDaihyoushaName.AllowUnicode = false;
            this.obic7TextStringDaihyoushaName.AllowZenkaku = true;
            this.obic7TextStringDaihyoushaName.AttachedButton = null;
            this.obic7TextStringDaihyoushaName.AttachedLabel = this.obic7LabelStringDaihyoushaName;
            this.obic7TextStringDaihyoushaName.AttachedSearchDialog = null;
            this.obic7TextStringDaihyoushaName.CheckOnClose = false;
            this.obic7TextStringDaihyoushaName.ColumnNumber = 0;
            this.obic7TextStringDaihyoushaName.DispFuriganaText = null;
            this.obic7TextStringDaihyoushaName.EnableFalseLock = false;
            this.obic7TextStringDaihyoushaName.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringDaihyoushaName.ErrorString = null;
            this.obic7TextStringDaihyoushaName.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.obic7TextStringDaihyoushaName.IsKeyColumn = false;
            this.obic7TextStringDaihyoushaName.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringDaihyoushaName.Location = new System.Drawing.Point(96, 216);
            this.obic7TextStringDaihyoushaName.MaxLengthByte = 30;
            this.obic7TextStringDaihyoushaName.Name = "obic7TextStringDaihyoushaName";
            this.obic7TextStringDaihyoushaName.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringDaihyoushaName.Size = new System.Drawing.Size(192, 19);
            this.obic7TextStringDaihyoushaName.TabIndex = 2230;
            this.obic7TextStringDaihyoushaName.Value = "";
            this.obic7TextStringDaihyoushaName.VisibleFalseLock = false;
            this.obic7TextStringDaihyoushaName.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7LabelStringDaihyoushaKanaName
            // 
            this.obic7LabelStringDaihyoushaKanaName.AutoEllipsis = true;
            this.obic7LabelStringDaihyoushaKanaName.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelStringDaihyoushaKanaName.ColumnNumber = 0;
            this.obic7LabelStringDaihyoushaKanaName.EnableFalseLock = false;
            this.obic7LabelStringDaihyoushaKanaName.ErrorString = null;
            this.obic7LabelStringDaihyoushaKanaName.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.obic7LabelStringDaihyoushaKanaName.Location = new System.Drawing.Point(6, 240);
            this.obic7LabelStringDaihyoushaKanaName.Name = "obic7LabelStringDaihyoushaKanaName";
            this.obic7LabelStringDaihyoushaKanaName.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringDaihyoushaKanaName.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringDaihyoushaKanaName.Size = new System.Drawing.Size(84, 19);
            this.obic7LabelStringDaihyoushaKanaName.TabIndex = 1000000027;
            this.obic7LabelStringDaihyoushaKanaName.Text = "代表者カナ";
            this.obic7LabelStringDaihyoushaKanaName.Value = "代表者カナ";
            this.obic7LabelStringDaihyoushaKanaName.VisibleFalseLock = false;
            // 
            // obic7TextStringDaihyoushaKanaName
            // 
            this.obic7TextStringDaihyoushaKanaName.AllowAlphabet = true;
            this.obic7TextStringDaihyoushaKanaName.AllowKana = true;
            this.obic7TextStringDaihyoushaKanaName.AllowNumber = true;
            this.obic7TextStringDaihyoushaKanaName.AllowSymbol = true;
            this.obic7TextStringDaihyoushaKanaName.AllowUnicode = false;
            this.obic7TextStringDaihyoushaKanaName.AllowZenkaku = true;
            this.obic7TextStringDaihyoushaKanaName.AttachedButton = null;
            this.obic7TextStringDaihyoushaKanaName.AttachedLabel = this.obic7LabelStringCompanyKanaName;
            this.obic7TextStringDaihyoushaKanaName.AttachedSearchDialog = null;
            this.obic7TextStringDaihyoushaKanaName.CheckOnClose = false;
            this.obic7TextStringDaihyoushaKanaName.ColumnNumber = 0;
            this.obic7TextStringDaihyoushaKanaName.DispFuriganaText = null;
            this.obic7TextStringDaihyoushaKanaName.EnableFalseLock = false;
            this.obic7TextStringDaihyoushaKanaName.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringDaihyoushaKanaName.ErrorString = null;
            this.obic7TextStringDaihyoushaKanaName.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.obic7TextStringDaihyoushaKanaName.IsKeyColumn = false;
            this.obic7TextStringDaihyoushaKanaName.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringDaihyoushaKanaName.Location = new System.Drawing.Point(96, 240);
            this.obic7TextStringDaihyoushaKanaName.MaxLengthByte = 30;
            this.obic7TextStringDaihyoushaKanaName.Name = "obic7TextStringDaihyoushaKanaName";
            this.obic7TextStringDaihyoushaKanaName.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringDaihyoushaKanaName.Size = new System.Drawing.Size(192, 19);
            this.obic7TextStringDaihyoushaKanaName.TabIndex = 2240;
            this.obic7TextStringDaihyoushaKanaName.Value = "";
            this.obic7TextStringDaihyoushaKanaName.VisibleFalseLock = false;
            this.obic7TextStringDaihyoushaKanaName.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7LabelStringYagou
            // 
            this.obic7LabelStringYagou.AutoEllipsis = true;
            this.obic7LabelStringYagou.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelStringYagou.ColumnNumber = 0;
            this.obic7LabelStringYagou.EnableFalseLock = false;
            this.obic7LabelStringYagou.ErrorString = null;
            this.obic7LabelStringYagou.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.obic7LabelStringYagou.Location = new System.Drawing.Point(414, 24);
            this.obic7LabelStringYagou.Name = "obic7LabelStringYagou";
            this.obic7LabelStringYagou.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringYagou.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringYagou.Size = new System.Drawing.Size(84, 19);
            this.obic7LabelStringYagou.TabIndex = 1000000029;
            this.obic7LabelStringYagou.Text = "屋号";
            this.obic7LabelStringYagou.Value = "屋号";
            this.obic7LabelStringYagou.VisibleFalseLock = false;
            // 
            // obic7TextStringYagou
            // 
            this.obic7TextStringYagou.AllowAlphabet = true;
            this.obic7TextStringYagou.AllowKana = true;
            this.obic7TextStringYagou.AllowNumber = true;
            this.obic7TextStringYagou.AllowSymbol = true;
            this.obic7TextStringYagou.AllowUnicode = false;
            this.obic7TextStringYagou.AllowZenkaku = true;
            this.obic7TextStringYagou.AttachedButton = null;
            this.obic7TextStringYagou.AttachedLabel = this.obic7LabelStringYagou;
            this.obic7TextStringYagou.AttachedSearchDialog = null;
            this.obic7TextStringYagou.CheckOnClose = false;
            this.obic7TextStringYagou.ColumnNumber = 0;
            this.obic7TextStringYagou.DispFuriganaText = null;
            this.obic7TextStringYagou.EnableFalseLock = false;
            this.obic7TextStringYagou.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringYagou.ErrorString = null;
            this.obic7TextStringYagou.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.obic7TextStringYagou.IsKeyColumn = false;
            this.obic7TextStringYagou.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringYagou.Location = new System.Drawing.Point(504, 24);
            this.obic7TextStringYagou.MaxLengthByte = 20;
            this.obic7TextStringYagou.Name = "obic7TextStringYagou";
            this.obic7TextStringYagou.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringYagou.Size = new System.Drawing.Size(132, 19);
            this.obic7TextStringYagou.TabIndex = 2250;
            this.obic7TextStringYagou.Value = "";
            this.obic7TextStringYagou.VisibleFalseLock = false;
            this.obic7TextStringYagou.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7LabelStringZigyoushumoku
            // 
            this.obic7LabelStringZigyoushumoku.AutoEllipsis = true;
            this.obic7LabelStringZigyoushumoku.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelStringZigyoushumoku.ColumnNumber = 0;
            this.obic7LabelStringZigyoushumoku.EnableFalseLock = false;
            this.obic7LabelStringZigyoushumoku.ErrorString = null;
            this.obic7LabelStringZigyoushumoku.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.obic7LabelStringZigyoushumoku.Location = new System.Drawing.Point(414, 48);
            this.obic7LabelStringZigyoushumoku.Name = "obic7LabelStringZigyoushumoku";
            this.obic7LabelStringZigyoushumoku.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringZigyoushumoku.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringZigyoushumoku.Size = new System.Drawing.Size(84, 19);
            this.obic7LabelStringZigyoushumoku.TabIndex = 1000000031;
            this.obic7LabelStringZigyoushumoku.Text = "事業種目";
            this.obic7LabelStringZigyoushumoku.Value = "事業種目";
            this.obic7LabelStringZigyoushumoku.VisibleFalseLock = false;
            // 
            // obic7TextStringZigyoushumoku
            // 
            this.obic7TextStringZigyoushumoku.AllowAlphabet = true;
            this.obic7TextStringZigyoushumoku.AllowKana = true;
            this.obic7TextStringZigyoushumoku.AllowNumber = true;
            this.obic7TextStringZigyoushumoku.AllowSymbol = true;
            this.obic7TextStringZigyoushumoku.AllowUnicode = false;
            this.obic7TextStringZigyoushumoku.AllowZenkaku = true;
            this.obic7TextStringZigyoushumoku.AttachedButton = null;
            this.obic7TextStringZigyoushumoku.AttachedLabel = this.obic7LabelStringZigyoushumoku;
            this.obic7TextStringZigyoushumoku.AttachedSearchDialog = null;
            this.obic7TextStringZigyoushumoku.CheckOnClose = false;
            this.obic7TextStringZigyoushumoku.ColumnNumber = 0;
            this.obic7TextStringZigyoushumoku.DispFuriganaText = null;
            this.obic7TextStringZigyoushumoku.EnableFalseLock = false;
            this.obic7TextStringZigyoushumoku.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringZigyoushumoku.ErrorString = null;
            this.obic7TextStringZigyoushumoku.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.obic7TextStringZigyoushumoku.IsKeyColumn = false;
            this.obic7TextStringZigyoushumoku.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringZigyoushumoku.Location = new System.Drawing.Point(504, 48);
            this.obic7TextStringZigyoushumoku.MaxLengthByte = 20;
            this.obic7TextStringZigyoushumoku.Name = "obic7TextStringZigyoushumoku";
            this.obic7TextStringZigyoushumoku.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringZigyoushumoku.Size = new System.Drawing.Size(132, 19);
            this.obic7TextStringZigyoushumoku.TabIndex = 2260;
            this.obic7TextStringZigyoushumoku.Value = "";
            this.obic7TextStringZigyoushumoku.VisibleFalseLock = false;
            this.obic7TextStringZigyoushumoku.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7LabelStringSihonkin
            // 
            this.obic7LabelStringSihonkin.AutoEllipsis = true;
            this.obic7LabelStringSihonkin.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelStringSihonkin.ColumnNumber = 0;
            this.obic7LabelStringSihonkin.EnableFalseLock = false;
            this.obic7LabelStringSihonkin.ErrorString = null;
            this.obic7LabelStringSihonkin.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.obic7LabelStringSihonkin.Location = new System.Drawing.Point(414, 72);
            this.obic7LabelStringSihonkin.Name = "obic7LabelStringSihonkin";
            this.obic7LabelStringSihonkin.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringSihonkin.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringSihonkin.Size = new System.Drawing.Size(84, 19);
            this.obic7LabelStringSihonkin.TabIndex = 1000000033;
            this.obic7LabelStringSihonkin.Text = "資本金";
            this.obic7LabelStringSihonkin.Value = "資本金";
            this.obic7LabelStringSihonkin.VisibleFalseLock = false;
            // 
            // obic7TextNumberSihonkin
            // 
            this.obic7TextNumberSihonkin.AttachedButton = null;
            this.obic7TextNumberSihonkin.AttachedLabel = this.obic7LabelStringSihonkin;
            this.obic7TextNumberSihonkin.AttachedSearchDialog = null;
            this.obic7TextNumberSihonkin.CheckOnClose = false;
            this.obic7TextNumberSihonkin.ColumnNumber = 0;
            this.obic7TextNumberSihonkin.EnableFalseLock = false;
            this.obic7TextNumberSihonkin.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberSihonkin.ErrorString = null;
            this.obic7TextNumberSihonkin.ForeColor = System.Drawing.SystemColors.WindowText;
            this.obic7TextNumberSihonkin.Format = "#,##0";
            this.obic7TextNumberSihonkin.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.obic7TextNumberSihonkin.IsKeyColumn = false;
            this.obic7TextNumberSihonkin.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberSihonkin.Location = new System.Drawing.Point(504, 72);
            this.obic7TextNumberSihonkin.MaxLengthByte = 19;
            this.obic7TextNumberSihonkin.MaxValue = new decimal(new int[] {
            1316134911,
            2328,
            0,
            0});
            this.obic7TextNumberSihonkin.MinusColor = System.Drawing.Color.Red;
            this.obic7TextNumberSihonkin.MinValue = new decimal(new int[] {
            1316134911,
            2328,
            0,
            -2147483648});
            this.obic7TextNumberSihonkin.Name = "obic7TextNumberSihonkin";
            this.obic7TextNumberSihonkin.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextNumberSihonkin.Size = new System.Drawing.Size(126, 19);
            this.obic7TextNumberSihonkin.TabIndex = 2270;
            this.obic7TextNumberSihonkin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.obic7TextNumberSihonkin.Value = null;
            this.obic7TextNumberSihonkin.VisibleFalseLock = false;
            this.obic7TextNumberSihonkin.Enter += new System.EventHandler(this.controlDetail_Enter);
            this.obic7TextNumberSihonkin.Validating += new System.ComponentModel.CancelEventHandler(this.obic7TextNumberSihonkin_Validating);
            // 
            // obic7LabelStringSeturituDate
            // 
            this.obic7LabelStringSeturituDate.AutoEllipsis = true;
            this.obic7LabelStringSeturituDate.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelStringSeturituDate.ColumnNumber = 0;
            this.obic7LabelStringSeturituDate.EnableFalseLock = false;
            this.obic7LabelStringSeturituDate.ErrorString = null;
            this.obic7LabelStringSeturituDate.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.obic7LabelStringSeturituDate.Location = new System.Drawing.Point(414, 96);
            this.obic7LabelStringSeturituDate.Name = "obic7LabelStringSeturituDate";
            this.obic7LabelStringSeturituDate.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringSeturituDate.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringSeturituDate.Size = new System.Drawing.Size(84, 19);
            this.obic7LabelStringSeturituDate.TabIndex = 1000000035;
            this.obic7LabelStringSeturituDate.Text = "設立年月日";
            this.obic7LabelStringSeturituDate.Value = "設立年月日";
            this.obic7LabelStringSeturituDate.VisibleFalseLock = false;
            // 
            // obic7TextDatetimeSeturituDate
            // 
            this.obic7TextDatetimeSeturituDate.AttachedButton = null;
            this.obic7TextDatetimeSeturituDate.AttachedLabel = this.obic7LabelStringSeturituDate;
            this.obic7TextDatetimeSeturituDate.AttachedSearchDialog = null;
            this.obic7TextDatetimeSeturituDate.CheckOnClose = false;
            this.obic7TextDatetimeSeturituDate.ColumnNumber = 0;
            this.obic7TextDatetimeSeturituDate.EnableFalseLock = false;
            this.obic7TextDatetimeSeturituDate.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextDatetimeSeturituDate.ErrorString = null;
            this.obic7TextDatetimeSeturituDate.Format = "yyyy/MM/dd";
            this.obic7TextDatetimeSeturituDate.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.obic7TextDatetimeSeturituDate.IsKeyColumn = false;
            this.obic7TextDatetimeSeturituDate.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextDatetimeSeturituDate.Location = new System.Drawing.Point(504, 96);
            this.obic7TextDatetimeSeturituDate.MaxLengthByte = 10;
            this.obic7TextDatetimeSeturituDate.MaxValue = new System.DateTime(2100, 1, 1, 0, 0, 0, 0);
            this.obic7TextDatetimeSeturituDate.MinValue = new System.DateTime(1868, 9, 8, 0, 0, 0, 0);
            this.obic7TextDatetimeSeturituDate.Name = "obic7TextDatetimeSeturituDate";
            this.obic7TextDatetimeSeturituDate.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextDatetimeSeturituDate.Size = new System.Drawing.Size(72, 19);
            this.obic7TextDatetimeSeturituDate.TabIndex = 2280;
            this.obic7TextDatetimeSeturituDate.UseJapaneseCalendar = false;
            this.obic7TextDatetimeSeturituDate.Value = null;
            this.obic7TextDatetimeSeturituDate.VisibleFalseLock = false;
            this.obic7TextDatetimeSeturituDate.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7LabelStringSinkokuTantoushaName
            // 
            this.obic7LabelStringSinkokuTantoushaName.AutoEllipsis = true;
            this.obic7LabelStringSinkokuTantoushaName.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelStringSinkokuTantoushaName.ColumnNumber = 0;
            this.obic7LabelStringSinkokuTantoushaName.EnableFalseLock = false;
            this.obic7LabelStringSinkokuTantoushaName.ErrorString = null;
            this.obic7LabelStringSinkokuTantoushaName.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.obic7LabelStringSinkokuTantoushaName.Location = new System.Drawing.Point(414, 120);
            this.obic7LabelStringSinkokuTantoushaName.Name = "obic7LabelStringSinkokuTantoushaName";
            this.obic7LabelStringSinkokuTantoushaName.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringSinkokuTantoushaName.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringSinkokuTantoushaName.Size = new System.Drawing.Size(84, 19);
            this.obic7LabelStringSinkokuTantoushaName.TabIndex = 1000000037;
            this.obic7LabelStringSinkokuTantoushaName.Text = "申告担当者名";
            this.obic7LabelStringSinkokuTantoushaName.Value = "申告担当者名";
            this.obic7LabelStringSinkokuTantoushaName.VisibleFalseLock = false;
            // 
            // obic7TextStringSinkokuTantoushaName
            // 
            this.obic7TextStringSinkokuTantoushaName.AllowAlphabet = true;
            this.obic7TextStringSinkokuTantoushaName.AllowKana = true;
            this.obic7TextStringSinkokuTantoushaName.AllowNumber = true;
            this.obic7TextStringSinkokuTantoushaName.AllowSymbol = true;
            this.obic7TextStringSinkokuTantoushaName.AllowUnicode = false;
            this.obic7TextStringSinkokuTantoushaName.AllowZenkaku = true;
            this.obic7TextStringSinkokuTantoushaName.AttachedButton = null;
            this.obic7TextStringSinkokuTantoushaName.AttachedLabel = this.obic7LabelStringSinkokuTantoushaName;
            this.obic7TextStringSinkokuTantoushaName.AttachedSearchDialog = null;
            this.obic7TextStringSinkokuTantoushaName.CheckOnClose = false;
            this.obic7TextStringSinkokuTantoushaName.ColumnNumber = 0;
            this.obic7TextStringSinkokuTantoushaName.DispFuriganaText = null;
            this.obic7TextStringSinkokuTantoushaName.EnableFalseLock = false;
            this.obic7TextStringSinkokuTantoushaName.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringSinkokuTantoushaName.ErrorString = null;
            this.obic7TextStringSinkokuTantoushaName.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.obic7TextStringSinkokuTantoushaName.IsKeyColumn = false;
            this.obic7TextStringSinkokuTantoushaName.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringSinkokuTantoushaName.Location = new System.Drawing.Point(504, 120);
            this.obic7TextStringSinkokuTantoushaName.MaxLengthByte = 30;
            this.obic7TextStringSinkokuTantoushaName.Name = "obic7TextStringSinkokuTantoushaName";
            this.obic7TextStringSinkokuTantoushaName.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringSinkokuTantoushaName.Size = new System.Drawing.Size(192, 19);
            this.obic7TextStringSinkokuTantoushaName.TabIndex = 2290;
            this.obic7TextStringSinkokuTantoushaName.Value = "";
            this.obic7TextStringSinkokuTantoushaName.VisibleFalseLock = false;
            this.obic7TextStringSinkokuTantoushaName.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7TextStringTantoushaTelNo3
            // 
            this.obic7TextStringTantoushaTelNo3.AllowAlphabet = false;
            this.obic7TextStringTantoushaTelNo3.AllowKana = false;
            this.obic7TextStringTantoushaTelNo3.AllowNumber = true;
            this.obic7TextStringTantoushaTelNo3.AllowSymbol = false;
            this.obic7TextStringTantoushaTelNo3.AllowUnicode = false;
            this.obic7TextStringTantoushaTelNo3.AllowZenkaku = false;
            this.obic7TextStringTantoushaTelNo3.AttachedButton = null;
            this.obic7TextStringTantoushaTelNo3.AttachedLabel = null;
            this.obic7TextStringTantoushaTelNo3.AttachedSearchDialog = null;
            this.obic7TextStringTantoushaTelNo3.CheckOnClose = false;
            this.obic7TextStringTantoushaTelNo3.ColumnNumber = 0;
            this.obic7TextStringTantoushaTelNo3.DispFuriganaText = null;
            this.obic7TextStringTantoushaTelNo3.EnableFalseLock = false;
            this.obic7TextStringTantoushaTelNo3.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringTantoushaTelNo3.ErrorString = null;
            this.obic7TextStringTantoushaTelNo3.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringTantoushaTelNo3.IsKeyColumn = false;
            this.obic7TextStringTantoushaTelNo3.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringTantoushaTelNo3.Location = new System.Drawing.Point(618, 144);
            this.obic7TextStringTantoushaTelNo3.MaxLengthByte = 5;
            this.obic7TextStringTantoushaTelNo3.Name = "obic7TextStringTantoushaTelNo3";
            this.obic7TextStringTantoushaTelNo3.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringTantoushaTelNo3.Size = new System.Drawing.Size(42, 19);
            this.obic7TextStringTantoushaTelNo3.TabIndex = 2320;
            this.obic7TextStringTantoushaTelNo3.Value = "";
            this.obic7TextStringTantoushaTelNo3.VisibleFalseLock = false;
            this.obic7TextStringTantoushaTelNo3.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7LabelStringTelNoHaihun4
            // 
            this.obic7LabelStringTelNoHaihun4.AutoEllipsis = true;
            this.obic7LabelStringTelNoHaihun4.ColumnNumber = 0;
            this.obic7LabelStringTelNoHaihun4.EnableFalseLock = false;
            this.obic7LabelStringTelNoHaihun4.ErrorString = null;
            this.obic7LabelStringTelNoHaihun4.Location = new System.Drawing.Point(606, 144);
            this.obic7LabelStringTelNoHaihun4.Name = "obic7LabelStringTelNoHaihun4";
            this.obic7LabelStringTelNoHaihun4.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7LabelStringTelNoHaihun4.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringTelNoHaihun4.Size = new System.Drawing.Size(13, 19);
            this.obic7LabelStringTelNoHaihun4.TabIndex = 1000000043;
            this.obic7LabelStringTelNoHaihun4.Text = "-";
            this.obic7LabelStringTelNoHaihun4.Value = "-";
            this.obic7LabelStringTelNoHaihun4.VisibleFalseLock = false;
            // 
            // obic7TextStringTantoushaTelNo2
            // 
            this.obic7TextStringTantoushaTelNo2.AllowAlphabet = false;
            this.obic7TextStringTantoushaTelNo2.AllowKana = false;
            this.obic7TextStringTantoushaTelNo2.AllowNumber = true;
            this.obic7TextStringTantoushaTelNo2.AllowSymbol = false;
            this.obic7TextStringTantoushaTelNo2.AllowUnicode = false;
            this.obic7TextStringTantoushaTelNo2.AllowZenkaku = false;
            this.obic7TextStringTantoushaTelNo2.AttachedButton = null;
            this.obic7TextStringTantoushaTelNo2.AttachedLabel = null;
            this.obic7TextStringTantoushaTelNo2.AttachedSearchDialog = null;
            this.obic7TextStringTantoushaTelNo2.CheckOnClose = false;
            this.obic7TextStringTantoushaTelNo2.ColumnNumber = 0;
            this.obic7TextStringTantoushaTelNo2.DispFuriganaText = null;
            this.obic7TextStringTantoushaTelNo2.EnableFalseLock = false;
            this.obic7TextStringTantoushaTelNo2.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringTantoushaTelNo2.ErrorString = null;
            this.obic7TextStringTantoushaTelNo2.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringTantoushaTelNo2.IsKeyColumn = false;
            this.obic7TextStringTantoushaTelNo2.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringTantoushaTelNo2.Location = new System.Drawing.Point(564, 144);
            this.obic7TextStringTantoushaTelNo2.MaxLengthByte = 5;
            this.obic7TextStringTantoushaTelNo2.Name = "obic7TextStringTantoushaTelNo2";
            this.obic7TextStringTantoushaTelNo2.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringTantoushaTelNo2.Size = new System.Drawing.Size(42, 19);
            this.obic7TextStringTantoushaTelNo2.TabIndex = 2310;
            this.obic7TextStringTantoushaTelNo2.Value = "";
            this.obic7TextStringTantoushaTelNo2.VisibleFalseLock = false;
            this.obic7TextStringTantoushaTelNo2.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7LabelStringTelNoHaihun3
            // 
            this.obic7LabelStringTelNoHaihun3.AutoEllipsis = true;
            this.obic7LabelStringTelNoHaihun3.ColumnNumber = 0;
            this.obic7LabelStringTelNoHaihun3.EnableFalseLock = false;
            this.obic7LabelStringTelNoHaihun3.ErrorString = null;
            this.obic7LabelStringTelNoHaihun3.Location = new System.Drawing.Point(552, 144);
            this.obic7LabelStringTelNoHaihun3.Name = "obic7LabelStringTelNoHaihun3";
            this.obic7LabelStringTelNoHaihun3.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7LabelStringTelNoHaihun3.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringTelNoHaihun3.Size = new System.Drawing.Size(13, 19);
            this.obic7LabelStringTelNoHaihun3.TabIndex = 1000000041;
            this.obic7LabelStringTelNoHaihun3.Text = "-";
            this.obic7LabelStringTelNoHaihun3.Value = "-";
            this.obic7LabelStringTelNoHaihun3.VisibleFalseLock = false;
            // 
            // obic7TextStringTantoushaTelNo1
            // 
            this.obic7TextStringTantoushaTelNo1.AllowAlphabet = false;
            this.obic7TextStringTantoushaTelNo1.AllowKana = false;
            this.obic7TextStringTantoushaTelNo1.AllowNumber = true;
            this.obic7TextStringTantoushaTelNo1.AllowSymbol = false;
            this.obic7TextStringTantoushaTelNo1.AllowUnicode = false;
            this.obic7TextStringTantoushaTelNo1.AllowZenkaku = false;
            this.obic7TextStringTantoushaTelNo1.AttachedButton = null;
            this.obic7TextStringTantoushaTelNo1.AttachedLabel = null;
            this.obic7TextStringTantoushaTelNo1.AttachedSearchDialog = null;
            this.obic7TextStringTantoushaTelNo1.CheckOnClose = false;
            this.obic7TextStringTantoushaTelNo1.ColumnNumber = 0;
            this.obic7TextStringTantoushaTelNo1.DispFuriganaText = null;
            this.obic7TextStringTantoushaTelNo1.EnableFalseLock = false;
            this.obic7TextStringTantoushaTelNo1.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringTantoushaTelNo1.ErrorString = null;
            this.obic7TextStringTantoushaTelNo1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringTantoushaTelNo1.IsKeyColumn = false;
            this.obic7TextStringTantoushaTelNo1.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringTantoushaTelNo1.Location = new System.Drawing.Point(504, 144);
            this.obic7TextStringTantoushaTelNo1.MaxLengthByte = 6;
            this.obic7TextStringTantoushaTelNo1.Name = "obic7TextStringTantoushaTelNo1";
            this.obic7TextStringTantoushaTelNo1.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringTantoushaTelNo1.Size = new System.Drawing.Size(48, 19);
            this.obic7TextStringTantoushaTelNo1.TabIndex = 2300;
            this.obic7TextStringTantoushaTelNo1.Value = "";
            this.obic7TextStringTantoushaTelNo1.VisibleFalseLock = false;
            this.obic7TextStringTantoushaTelNo1.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7LabelStringTantoushaTelNo
            // 
            this.obic7LabelStringTantoushaTelNo.AutoEllipsis = true;
            this.obic7LabelStringTantoushaTelNo.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelStringTantoushaTelNo.ColumnNumber = 0;
            this.obic7LabelStringTantoushaTelNo.EnableFalseLock = false;
            this.obic7LabelStringTantoushaTelNo.ErrorString = null;
            this.obic7LabelStringTantoushaTelNo.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.obic7LabelStringTantoushaTelNo.Location = new System.Drawing.Point(414, 144);
            this.obic7LabelStringTantoushaTelNo.Name = "obic7LabelStringTantoushaTelNo";
            this.obic7LabelStringTantoushaTelNo.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringTantoushaTelNo.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringTantoushaTelNo.Size = new System.Drawing.Size(84, 19);
            this.obic7LabelStringTantoushaTelNo.TabIndex = 1000000039;
            this.obic7LabelStringTantoushaTelNo.Text = "    電話番号";
            this.obic7LabelStringTantoushaTelNo.Value = "    電話番号";
            this.obic7LabelStringTantoushaTelNo.VisibleFalseLock = false;
            // 
            // obic7LabelStringKaikeiZimushoName
            // 
            this.obic7LabelStringKaikeiZimushoName.AutoEllipsis = true;
            this.obic7LabelStringKaikeiZimushoName.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelStringKaikeiZimushoName.ColumnNumber = 0;
            this.obic7LabelStringKaikeiZimushoName.EnableFalseLock = false;
            this.obic7LabelStringKaikeiZimushoName.ErrorString = null;
            this.obic7LabelStringKaikeiZimushoName.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.obic7LabelStringKaikeiZimushoName.Location = new System.Drawing.Point(414, 168);
            this.obic7LabelStringKaikeiZimushoName.Name = "obic7LabelStringKaikeiZimushoName";
            this.obic7LabelStringKaikeiZimushoName.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringKaikeiZimushoName.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringKaikeiZimushoName.Size = new System.Drawing.Size(84, 19);
            this.obic7LabelStringKaikeiZimushoName.TabIndex = 1000000045;
            this.obic7LabelStringKaikeiZimushoName.Text = "会計事務所名";
            this.obic7LabelStringKaikeiZimushoName.Value = "会計事務所名";
            this.obic7LabelStringKaikeiZimushoName.VisibleFalseLock = false;
            // 
            // obic7TextStringKaikeiZimushoName
            // 
            this.obic7TextStringKaikeiZimushoName.AllowAlphabet = true;
            this.obic7TextStringKaikeiZimushoName.AllowKana = true;
            this.obic7TextStringKaikeiZimushoName.AllowNumber = true;
            this.obic7TextStringKaikeiZimushoName.AllowSymbol = true;
            this.obic7TextStringKaikeiZimushoName.AllowUnicode = false;
            this.obic7TextStringKaikeiZimushoName.AllowZenkaku = true;
            this.obic7TextStringKaikeiZimushoName.AttachedButton = null;
            this.obic7TextStringKaikeiZimushoName.AttachedLabel = this.obic7LabelStringKaikeiZimushoName;
            this.obic7TextStringKaikeiZimushoName.AttachedSearchDialog = null;
            this.obic7TextStringKaikeiZimushoName.CheckOnClose = false;
            this.obic7TextStringKaikeiZimushoName.ColumnNumber = 0;
            this.obic7TextStringKaikeiZimushoName.DispFuriganaText = null;
            this.obic7TextStringKaikeiZimushoName.EnableFalseLock = false;
            this.obic7TextStringKaikeiZimushoName.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKaikeiZimushoName.ErrorString = null;
            this.obic7TextStringKaikeiZimushoName.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.obic7TextStringKaikeiZimushoName.IsKeyColumn = false;
            this.obic7TextStringKaikeiZimushoName.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKaikeiZimushoName.Location = new System.Drawing.Point(504, 168);
            this.obic7TextStringKaikeiZimushoName.MaxLengthByte = 40;
            this.obic7TextStringKaikeiZimushoName.Name = "obic7TextStringKaikeiZimushoName";
            this.obic7TextStringKaikeiZimushoName.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKaikeiZimushoName.Size = new System.Drawing.Size(252, 19);
            this.obic7TextStringKaikeiZimushoName.TabIndex = 2330;
            this.obic7TextStringKaikeiZimushoName.Value = "";
            this.obic7TextStringKaikeiZimushoName.VisibleFalseLock = false;
            this.obic7TextStringKaikeiZimushoName.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7TextStringKaikeiZimushoTelNo3
            // 
            this.obic7TextStringKaikeiZimushoTelNo3.AllowAlphabet = false;
            this.obic7TextStringKaikeiZimushoTelNo3.AllowKana = false;
            this.obic7TextStringKaikeiZimushoTelNo3.AllowNumber = true;
            this.obic7TextStringKaikeiZimushoTelNo3.AllowSymbol = false;
            this.obic7TextStringKaikeiZimushoTelNo3.AllowUnicode = false;
            this.obic7TextStringKaikeiZimushoTelNo3.AllowZenkaku = false;
            this.obic7TextStringKaikeiZimushoTelNo3.AttachedButton = null;
            this.obic7TextStringKaikeiZimushoTelNo3.AttachedLabel = null;
            this.obic7TextStringKaikeiZimushoTelNo3.AttachedSearchDialog = null;
            this.obic7TextStringKaikeiZimushoTelNo3.CheckOnClose = false;
            this.obic7TextStringKaikeiZimushoTelNo3.ColumnNumber = 0;
            this.obic7TextStringKaikeiZimushoTelNo3.DispFuriganaText = null;
            this.obic7TextStringKaikeiZimushoTelNo3.EnableFalseLock = false;
            this.obic7TextStringKaikeiZimushoTelNo3.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKaikeiZimushoTelNo3.ErrorString = null;
            this.obic7TextStringKaikeiZimushoTelNo3.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringKaikeiZimushoTelNo3.IsKeyColumn = false;
            this.obic7TextStringKaikeiZimushoTelNo3.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKaikeiZimushoTelNo3.Location = new System.Drawing.Point(618, 192);
            this.obic7TextStringKaikeiZimushoTelNo3.MaxLengthByte = 5;
            this.obic7TextStringKaikeiZimushoTelNo3.Name = "obic7TextStringKaikeiZimushoTelNo3";
            this.obic7TextStringKaikeiZimushoTelNo3.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKaikeiZimushoTelNo3.Size = new System.Drawing.Size(42, 19);
            this.obic7TextStringKaikeiZimushoTelNo3.TabIndex = 2360;
            this.obic7TextStringKaikeiZimushoTelNo3.Value = "";
            this.obic7TextStringKaikeiZimushoTelNo3.VisibleFalseLock = false;
            this.obic7TextStringKaikeiZimushoTelNo3.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7LabelStringTelNoHaihun6
            // 
            this.obic7LabelStringTelNoHaihun6.AutoEllipsis = true;
            this.obic7LabelStringTelNoHaihun6.ColumnNumber = 0;
            this.obic7LabelStringTelNoHaihun6.EnableFalseLock = false;
            this.obic7LabelStringTelNoHaihun6.ErrorString = null;
            this.obic7LabelStringTelNoHaihun6.Location = new System.Drawing.Point(606, 192);
            this.obic7LabelStringTelNoHaihun6.Name = "obic7LabelStringTelNoHaihun6";
            this.obic7LabelStringTelNoHaihun6.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7LabelStringTelNoHaihun6.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringTelNoHaihun6.Size = new System.Drawing.Size(13, 19);
            this.obic7LabelStringTelNoHaihun6.TabIndex = 1000000051;
            this.obic7LabelStringTelNoHaihun6.Text = "-";
            this.obic7LabelStringTelNoHaihun6.Value = "-";
            this.obic7LabelStringTelNoHaihun6.VisibleFalseLock = false;
            // 
            // obic7TextStringKaikeiZimushoTelNo2
            // 
            this.obic7TextStringKaikeiZimushoTelNo2.AllowAlphabet = false;
            this.obic7TextStringKaikeiZimushoTelNo2.AllowKana = false;
            this.obic7TextStringKaikeiZimushoTelNo2.AllowNumber = true;
            this.obic7TextStringKaikeiZimushoTelNo2.AllowSymbol = false;
            this.obic7TextStringKaikeiZimushoTelNo2.AllowUnicode = false;
            this.obic7TextStringKaikeiZimushoTelNo2.AllowZenkaku = false;
            this.obic7TextStringKaikeiZimushoTelNo2.AttachedButton = null;
            this.obic7TextStringKaikeiZimushoTelNo2.AttachedLabel = null;
            this.obic7TextStringKaikeiZimushoTelNo2.AttachedSearchDialog = null;
            this.obic7TextStringKaikeiZimushoTelNo2.CheckOnClose = false;
            this.obic7TextStringKaikeiZimushoTelNo2.ColumnNumber = 0;
            this.obic7TextStringKaikeiZimushoTelNo2.DispFuriganaText = null;
            this.obic7TextStringKaikeiZimushoTelNo2.EnableFalseLock = false;
            this.obic7TextStringKaikeiZimushoTelNo2.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKaikeiZimushoTelNo2.ErrorString = null;
            this.obic7TextStringKaikeiZimushoTelNo2.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringKaikeiZimushoTelNo2.IsKeyColumn = false;
            this.obic7TextStringKaikeiZimushoTelNo2.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKaikeiZimushoTelNo2.Location = new System.Drawing.Point(564, 192);
            this.obic7TextStringKaikeiZimushoTelNo2.MaxLengthByte = 5;
            this.obic7TextStringKaikeiZimushoTelNo2.Name = "obic7TextStringKaikeiZimushoTelNo2";
            this.obic7TextStringKaikeiZimushoTelNo2.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKaikeiZimushoTelNo2.Size = new System.Drawing.Size(42, 19);
            this.obic7TextStringKaikeiZimushoTelNo2.TabIndex = 2350;
            this.obic7TextStringKaikeiZimushoTelNo2.Value = "";
            this.obic7TextStringKaikeiZimushoTelNo2.VisibleFalseLock = false;
            this.obic7TextStringKaikeiZimushoTelNo2.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7LabelStringTelNoHaihun5
            // 
            this.obic7LabelStringTelNoHaihun5.AutoEllipsis = true;
            this.obic7LabelStringTelNoHaihun5.ColumnNumber = 0;
            this.obic7LabelStringTelNoHaihun5.EnableFalseLock = false;
            this.obic7LabelStringTelNoHaihun5.ErrorString = null;
            this.obic7LabelStringTelNoHaihun5.Location = new System.Drawing.Point(552, 192);
            this.obic7LabelStringTelNoHaihun5.Name = "obic7LabelStringTelNoHaihun5";
            this.obic7LabelStringTelNoHaihun5.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7LabelStringTelNoHaihun5.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringTelNoHaihun5.Size = new System.Drawing.Size(13, 19);
            this.obic7LabelStringTelNoHaihun5.TabIndex = 1000000049;
            this.obic7LabelStringTelNoHaihun5.Text = "-";
            this.obic7LabelStringTelNoHaihun5.Value = "-";
            this.obic7LabelStringTelNoHaihun5.VisibleFalseLock = false;
            // 
            // obic7TextStringKaikeiZimushoTelNo1
            // 
            this.obic7TextStringKaikeiZimushoTelNo1.AllowAlphabet = false;
            this.obic7TextStringKaikeiZimushoTelNo1.AllowKana = false;
            this.obic7TextStringKaikeiZimushoTelNo1.AllowNumber = true;
            this.obic7TextStringKaikeiZimushoTelNo1.AllowSymbol = false;
            this.obic7TextStringKaikeiZimushoTelNo1.AllowUnicode = false;
            this.obic7TextStringKaikeiZimushoTelNo1.AllowZenkaku = false;
            this.obic7TextStringKaikeiZimushoTelNo1.AttachedButton = null;
            this.obic7TextStringKaikeiZimushoTelNo1.AttachedLabel = null;
            this.obic7TextStringKaikeiZimushoTelNo1.AttachedSearchDialog = null;
            this.obic7TextStringKaikeiZimushoTelNo1.CheckOnClose = false;
            this.obic7TextStringKaikeiZimushoTelNo1.ColumnNumber = 0;
            this.obic7TextStringKaikeiZimushoTelNo1.DispFuriganaText = null;
            this.obic7TextStringKaikeiZimushoTelNo1.EnableFalseLock = false;
            this.obic7TextStringKaikeiZimushoTelNo1.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKaikeiZimushoTelNo1.ErrorString = null;
            this.obic7TextStringKaikeiZimushoTelNo1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringKaikeiZimushoTelNo1.IsKeyColumn = false;
            this.obic7TextStringKaikeiZimushoTelNo1.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKaikeiZimushoTelNo1.Location = new System.Drawing.Point(504, 192);
            this.obic7TextStringKaikeiZimushoTelNo1.MaxLengthByte = 6;
            this.obic7TextStringKaikeiZimushoTelNo1.Name = "obic7TextStringKaikeiZimushoTelNo1";
            this.obic7TextStringKaikeiZimushoTelNo1.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKaikeiZimushoTelNo1.Size = new System.Drawing.Size(48, 19);
            this.obic7TextStringKaikeiZimushoTelNo1.TabIndex = 2340;
            this.obic7TextStringKaikeiZimushoTelNo1.Value = "";
            this.obic7TextStringKaikeiZimushoTelNo1.VisibleFalseLock = false;
            this.obic7TextStringKaikeiZimushoTelNo1.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7LabelStringKaikeiZimushoTelNo
            // 
            this.obic7LabelStringKaikeiZimushoTelNo.AutoEllipsis = true;
            this.obic7LabelStringKaikeiZimushoTelNo.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelStringKaikeiZimushoTelNo.ColumnNumber = 0;
            this.obic7LabelStringKaikeiZimushoTelNo.EnableFalseLock = false;
            this.obic7LabelStringKaikeiZimushoTelNo.ErrorString = null;
            this.obic7LabelStringKaikeiZimushoTelNo.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.obic7LabelStringKaikeiZimushoTelNo.Location = new System.Drawing.Point(414, 192);
            this.obic7LabelStringKaikeiZimushoTelNo.Name = "obic7LabelStringKaikeiZimushoTelNo";
            this.obic7LabelStringKaikeiZimushoTelNo.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringKaikeiZimushoTelNo.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringKaikeiZimushoTelNo.Size = new System.Drawing.Size(84, 19);
            this.obic7LabelStringKaikeiZimushoTelNo.TabIndex = 1000000047;
            this.obic7LabelStringKaikeiZimushoTelNo.Text = "    電話番号";
            this.obic7LabelStringKaikeiZimushoTelNo.Value = "    電話番号";
            this.obic7LabelStringKaikeiZimushoTelNo.VisibleFalseLock = false;
            // 
            // obic7GroupBoxSinkokushoKoumoku
            // 
            this.obic7GroupBoxSinkokushoKoumoku.ColumnNumber = 0;
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7TextStringTelNo2);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7TextStringTelNo3);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7TextStringPostCD2);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7TextStringKaikeiZimushoTelNo3);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7TextStringAddress1);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7LabelStringTelNoHaihun6);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7LabelStringPostCD);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7TextStringKaikeiZimushoTelNo2);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7TextStringPostCD1);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7LabelStringTelNoHaihun5);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7LabelStringPostCDHaihun);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7TextStringKaikeiZimushoTelNo1);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7LabelStringKaikeiZimushoTelNo);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7ButtonWithLogicPostCDSearch);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7TextStringKaikeiZimushoName);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7LabelStringAddress1);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7LabelStringKaikeiZimushoName);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7LabelStringAddress2);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7TextStringTantoushaTelNo3);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7TextStringAddress2);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7LabelStringTelNoHaihun4);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7LabelStringAddress1Kana);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7TextStringTantoushaTelNo2);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7TextStringAddress1Kana);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7LabelStringTelNoHaihun3);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7LabelStringAddress2Kana);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7TextStringTantoushaTelNo1);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7TextStringAddress2Kana);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7LabelStringTantoushaTelNo);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7LabelStringTelNo);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7TextStringSinkokuTantoushaName);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7TextStringTelNo1);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7LabelStringSinkokuTantoushaName);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7LabelStringTelNoHaihun1);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7TextDatetimeSeturituDate);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7LabelStringSeturituDate);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7LabelStringTelNoHaihun2);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7TextNumberSihonkin);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7LabelStringSihonkin);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7LabelStringCompanyName);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7TextStringZigyoushumoku);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7TextStringCompanyName);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7LabelStringZigyoushumoku);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7LabelStringCompanyKanaName);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7TextStringYagou);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7TextStringCompanyKanaName);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7LabelStringYagou);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7LabelStringDaihyoushaName);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7TextStringDaihyoushaKanaName);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7TextStringDaihyoushaName);
            this.obic7GroupBoxSinkokushoKoumoku.Controls.Add(this.obic7LabelStringDaihyoushaKanaName);
            this.obic7GroupBoxSinkokushoKoumoku.EnableFalseLock = false;
            this.obic7GroupBoxSinkokushoKoumoku.Location = new System.Drawing.Point(54, 99);
            this.obic7GroupBoxSinkokushoKoumoku.Name = "obic7GroupBoxSinkokushoKoumoku";
            this.obic7GroupBoxSinkokushoKoumoku.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7GroupBoxSinkokushoKoumoku.Size = new System.Drawing.Size(852, 264);
            this.obic7GroupBoxSinkokushoKoumoku.TabIndex = 2100;
            this.obic7GroupBoxSinkokushoKoumoku.TabStop = false;
            this.obic7GroupBoxSinkokushoKoumoku.Text = "償却資産申告書項目";
            this.obic7GroupBoxSinkokushoKoumoku.VisibleFalseLock = false;
            // 
            // obic7TextSeparatorZititaiCD
            // 
            this.obic7TextSeparatorZititaiCD.ColumnNumber = 0;
            this.obic7TextSeparatorZititaiCD.IsRefrectLowerOrder = true;
            this.obic7TextSeparatorZititaiCD.IsRefrectUpperOrder = false;
            this.obic7TextSeparatorZititaiCD.IsTerminator = false;
            this.obic7TextSeparatorZititaiCD.Location = new System.Drawing.Point(230, 7);
            this.obic7TextSeparatorZititaiCD.Name = "obic7TextSeparatorZititaiCD";
            this.obic7TextSeparatorZititaiCD.Size = new System.Drawing.Size(0, 19);
            this.obic7TextSeparatorZititaiCD.TabIndex = 1990;
            // 2026/10/11 追加 (5525)事業所別償却資産申告対応 開始
            // 
            // obic7LabelStringNouzeishaCD
            // 
            this.obic7LabelStringNouzeishaCD.AutoEllipsis = true;
            this.obic7LabelStringNouzeishaCD.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelStringNouzeishaCD.ColumnNumber = 0;
            this.obic7LabelStringNouzeishaCD.EnableFalseLock = false;
            this.obic7LabelStringNouzeishaCD.ErrorString = null;
            this.obic7LabelStringNouzeishaCD.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.obic7LabelStringNouzeishaCD.Location = new System.Drawing.Point(6, 24);
            this.obic7LabelStringNouzeishaCD.Name = "obic7LabelStringNouzeishaCD";
            this.obic7LabelStringNouzeishaCD.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringNouzeishaCD.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringNouzeishaCD.Size = new System.Drawing.Size(84, 19);
            this.obic7LabelStringNouzeishaCD.TabIndex = 1000000101;
            this.obic7LabelStringNouzeishaCD.Text = "納税者コード";
            this.obic7LabelStringNouzeishaCD.Value = "納税者コード";
            this.obic7LabelStringNouzeishaCD.VisibleFalseLock = false;
            // 
            // obic7TextStringNouzeishaCD
            // 
            this.obic7TextStringNouzeishaCD.AllowAlphabet = true;
            this.obic7TextStringNouzeishaCD.AllowKana = false;
            this.obic7TextStringNouzeishaCD.AllowNumber = true;
            this.obic7TextStringNouzeishaCD.AllowSymbol = false;
            this.obic7TextStringNouzeishaCD.AllowUnicode = false;
            this.obic7TextStringNouzeishaCD.AllowZenkaku = false;
            this.obic7TextStringNouzeishaCD.AttachedButton = null;
            this.obic7TextStringNouzeishaCD.AttachedLabel = this.obic7LabelStringNouzeishaCD;
            this.obic7TextStringNouzeishaCD.AttachedSearchDialog = null;
            this.obic7TextStringNouzeishaCD.CheckOnClose = false;
            this.obic7TextStringNouzeishaCD.ColumnNumber = 0;
            this.obic7TextStringNouzeishaCD.DispFuriganaText = null;
            this.obic7TextStringNouzeishaCD.EnableFalseLock = false;
            this.obic7TextStringNouzeishaCD.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringNouzeishaCD.ErrorString = null;
            this.obic7TextStringNouzeishaCD.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringNouzeishaCD.IsKeyColumn = false;
            this.obic7TextStringNouzeishaCD.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringNouzeishaCD.Location = new System.Drawing.Point(96, 24);
            this.obic7TextStringNouzeishaCD.MaxLengthByte = 10;
            this.obic7TextStringNouzeishaCD.Name = "obic7TextStringNouzeishaCD";
            this.obic7TextStringNouzeishaCD.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringNouzeishaCD.Size = new System.Drawing.Size(72, 19);
            this.obic7TextStringNouzeishaCD.TabIndex = 2410;
            this.obic7TextStringNouzeishaCD.Value = "";
            this.obic7TextStringNouzeishaCD.VisibleFalseLock = false;
            this.obic7TextStringNouzeishaCD.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7LabelStringRiyoushaID
            // 
            this.obic7LabelStringRiyoushaID.AutoEllipsis = true;
            this.obic7LabelStringRiyoushaID.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelStringRiyoushaID.ColumnNumber = 0;
            this.obic7LabelStringRiyoushaID.EnableFalseLock = false;
            this.obic7LabelStringRiyoushaID.ErrorString = null;
            this.obic7LabelStringRiyoushaID.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.obic7LabelStringRiyoushaID.Location = new System.Drawing.Point(6, 48);
            this.obic7LabelStringRiyoushaID.Name = "obic7LabelStringRiyoushaID";
            this.obic7LabelStringRiyoushaID.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringRiyoushaID.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringRiyoushaID.Size = new System.Drawing.Size(84, 19);
            this.obic7LabelStringRiyoushaID.TabIndex = 1000000102;
            this.obic7LabelStringRiyoushaID.Text = "利用者ID";
            this.obic7LabelStringRiyoushaID.Value = "利用者ID";
            this.obic7LabelStringRiyoushaID.VisibleFalseLock = false;
            // 
            // obic7TextStringRiyoushaID
            // 
            this.obic7TextStringRiyoushaID.AllowAlphabet = true;
            this.obic7TextStringRiyoushaID.AllowKana = false;
            this.obic7TextStringRiyoushaID.AllowNumber = true;
            this.obic7TextStringRiyoushaID.AllowSymbol = false;
            this.obic7TextStringRiyoushaID.AllowUnicode = false;
            this.obic7TextStringRiyoushaID.AllowZenkaku = false;
            this.obic7TextStringRiyoushaID.AttachedButton = null;
            this.obic7TextStringRiyoushaID.AttachedLabel = this.obic7LabelStringRiyoushaID;
            this.obic7TextStringRiyoushaID.AttachedSearchDialog = null;
            this.obic7TextStringRiyoushaID.CheckOnClose = false;
            this.obic7TextStringRiyoushaID.ColumnNumber = 0;
            this.obic7TextStringRiyoushaID.DispFuriganaText = null;
            this.obic7TextStringRiyoushaID.EnableFalseLock = false;
            this.obic7TextStringRiyoushaID.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringRiyoushaID.ErrorString = null;
            this.obic7TextStringRiyoushaID.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringRiyoushaID.IsKeyColumn = false;
            this.obic7TextStringRiyoushaID.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringRiyoushaID.Location = new System.Drawing.Point(96, 48);
            this.obic7TextStringRiyoushaID.MaxLengthByte = 11;
            this.obic7TextStringRiyoushaID.Name = "obic7TextStringRiyoushaID";
            this.obic7TextStringRiyoushaID.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringRiyoushaID.Size = new System.Drawing.Size(78, 19);
            this.obic7TextStringRiyoushaID.TabIndex = 2420;
            this.obic7TextStringRiyoushaID.Value = "";
            this.obic7TextStringRiyoushaID.VisibleFalseLock = false;
            this.obic7TextStringRiyoushaID.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7LabelStringDaihyoushaSikaku
            // 
            this.obic7LabelStringDaihyoushaSikaku.AutoEllipsis = true;
            this.obic7LabelStringDaihyoushaSikaku.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelStringDaihyoushaSikaku.ColumnNumber = 0;
            this.obic7LabelStringDaihyoushaSikaku.EnableFalseLock = false;
            this.obic7LabelStringDaihyoushaSikaku.ErrorString = null;
            this.obic7LabelStringDaihyoushaSikaku.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.obic7LabelStringDaihyoushaSikaku.Location = new System.Drawing.Point(6, 72);
            this.obic7LabelStringDaihyoushaSikaku.Name = "obic7LabelStringDaihyoushaSikaku";
            this.obic7LabelStringDaihyoushaSikaku.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringDaihyoushaSikaku.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringDaihyoushaSikaku.Size = new System.Drawing.Size(84, 19);
            this.obic7LabelStringDaihyoushaSikaku.TabIndex = 1000000103;
            this.obic7LabelStringDaihyoushaSikaku.Text = "代表者資格";
            this.obic7LabelStringDaihyoushaSikaku.Value = "代表者資格";
            this.obic7LabelStringDaihyoushaSikaku.VisibleFalseLock = false;
            // 
            // obic7TextStringDaihyoushaSikaku
            // 
            this.obic7TextStringDaihyoushaSikaku.AllowAlphabet = false;
            this.obic7TextStringDaihyoushaSikaku.AllowKana = false;
            this.obic7TextStringDaihyoushaSikaku.AllowNumber = true;
            this.obic7TextStringDaihyoushaSikaku.AllowSymbol = false;
            this.obic7TextStringDaihyoushaSikaku.AllowUnicode = false;
            this.obic7TextStringDaihyoushaSikaku.AllowZenkaku = false;
            this.obic7TextStringDaihyoushaSikaku.AttachedButton = null;
            this.obic7TextStringDaihyoushaSikaku.AttachedLabel = this.obic7LabelStringDaihyoushaSikaku;
            this.obic7TextStringDaihyoushaSikaku.AttachedSearchDialog = null;
            this.obic7TextStringDaihyoushaSikaku.CheckOnClose = false;
            this.obic7TextStringDaihyoushaSikaku.ColumnNumber = 0;
            this.obic7TextStringDaihyoushaSikaku.DispFuriganaText = null;
            this.obic7TextStringDaihyoushaSikaku.EnableFalseLock = false;
            this.obic7TextStringDaihyoushaSikaku.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringDaihyoushaSikaku.ErrorString = null;
            this.obic7TextStringDaihyoushaSikaku.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringDaihyoushaSikaku.IsKeyColumn = false;
            this.obic7TextStringDaihyoushaSikaku.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringDaihyoushaSikaku.Location = new System.Drawing.Point(96, 72);
            this.obic7TextStringDaihyoushaSikaku.MaxLengthByte = 2;
            this.obic7TextStringDaihyoushaSikaku.Name = "obic7TextStringDaihyoushaSikaku";
            this.obic7TextStringDaihyoushaSikaku.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringDaihyoushaSikaku.Size = new System.Drawing.Size(24, 19);
            this.obic7TextStringDaihyoushaSikaku.TabIndex = 2430;
            this.obic7TextStringDaihyoushaSikaku.Value = "";
            this.obic7TextStringDaihyoushaSikaku.VisibleFalseLock = false;
            this.obic7TextStringDaihyoushaSikaku.Enter += new System.EventHandler(this.controlDetail_Enter);
            this.obic7TextStringDaihyoushaSikaku.Validating += new System.ComponentModel.CancelEventHandler(this.obic7TextStringDaihyoushaSikaku_Validating);
            // 
            // obic7LabelStringDaihyoushaSikakuName
            // 
            this.obic7LabelStringDaihyoushaSikakuName.AutoEllipsis = true;
            this.obic7LabelStringDaihyoushaSikakuName.BackColor = System.Drawing.SystemColors.Control;
            this.obic7LabelStringDaihyoushaSikakuName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelStringDaihyoushaSikakuName.ColumnNumber = 0;
            this.obic7LabelStringDaihyoushaSikakuName.EnableFalseLock = false;
            this.obic7LabelStringDaihyoushaSikakuName.ErrorString = null;
            this.obic7LabelStringDaihyoushaSikakuName.Location = new System.Drawing.Point(126, 72);
            this.obic7LabelStringDaihyoushaSikakuName.Name = "obic7LabelStringDaihyoushaSikakuName";
            this.obic7LabelStringDaihyoushaSikakuName.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7LabelStringDaihyoushaSikakuName.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringDaihyoushaSikakuName.Size = new System.Drawing.Size(120, 19);
            this.obic7LabelStringDaihyoushaSikakuName.TabIndex = 1000000104;
            this.obic7LabelStringDaihyoushaSikakuName.Text = "";
            this.obic7LabelStringDaihyoushaSikakuName.Value = "";
            this.obic7LabelStringDaihyoushaSikakuName.VisibleFalseLock = false;
            // 
            // obic7LabelStringDaihyoushaTelNo
            // 
            this.obic7LabelStringDaihyoushaTelNo.AutoEllipsis = true;
            this.obic7LabelStringDaihyoushaTelNo.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelStringDaihyoushaTelNo.ColumnNumber = 0;
            this.obic7LabelStringDaihyoushaTelNo.EnableFalseLock = false;
            this.obic7LabelStringDaihyoushaTelNo.ErrorString = null;
            this.obic7LabelStringDaihyoushaTelNo.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.obic7LabelStringDaihyoushaTelNo.Location = new System.Drawing.Point(6, 96);
            this.obic7LabelStringDaihyoushaTelNo.Name = "obic7LabelStringDaihyoushaTelNo";
            this.obic7LabelStringDaihyoushaTelNo.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringDaihyoushaTelNo.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringDaihyoushaTelNo.Size = new System.Drawing.Size(84, 19);
            this.obic7LabelStringDaihyoushaTelNo.TabIndex = 1000000105;
            this.obic7LabelStringDaihyoushaTelNo.Text = "代表者電話番号";
            this.obic7LabelStringDaihyoushaTelNo.Value = "代表者電話番号";
            this.obic7LabelStringDaihyoushaTelNo.VisibleFalseLock = false;
            // 
            // obic7TextStringDaihyoushaTelNo1
            // 
            this.obic7TextStringDaihyoushaTelNo1.AllowAlphabet = false;
            this.obic7TextStringDaihyoushaTelNo1.AllowKana = false;
            this.obic7TextStringDaihyoushaTelNo1.AllowNumber = true;
            this.obic7TextStringDaihyoushaTelNo1.AllowSymbol = false;
            this.obic7TextStringDaihyoushaTelNo1.AllowUnicode = false;
            this.obic7TextStringDaihyoushaTelNo1.AllowZenkaku = false;
            this.obic7TextStringDaihyoushaTelNo1.AttachedButton = null;
            this.obic7TextStringDaihyoushaTelNo1.AttachedLabel = this.obic7LabelStringDaihyoushaTelNo;
            this.obic7TextStringDaihyoushaTelNo1.AttachedSearchDialog = null;
            this.obic7TextStringDaihyoushaTelNo1.CheckOnClose = false;
            this.obic7TextStringDaihyoushaTelNo1.ColumnNumber = 0;
            this.obic7TextStringDaihyoushaTelNo1.DispFuriganaText = null;
            this.obic7TextStringDaihyoushaTelNo1.EnableFalseLock = false;
            this.obic7TextStringDaihyoushaTelNo1.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringDaihyoushaTelNo1.ErrorString = null;
            this.obic7TextStringDaihyoushaTelNo1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringDaihyoushaTelNo1.IsKeyColumn = false;
            this.obic7TextStringDaihyoushaTelNo1.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringDaihyoushaTelNo1.Location = new System.Drawing.Point(96, 96);
            this.obic7TextStringDaihyoushaTelNo1.MaxLengthByte = 6;
            this.obic7TextStringDaihyoushaTelNo1.Name = "obic7TextStringDaihyoushaTelNo1";
            this.obic7TextStringDaihyoushaTelNo1.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringDaihyoushaTelNo1.Size = new System.Drawing.Size(48, 19);
            this.obic7TextStringDaihyoushaTelNo1.TabIndex = 2440;
            this.obic7TextStringDaihyoushaTelNo1.Value = "";
            this.obic7TextStringDaihyoushaTelNo1.VisibleFalseLock = false;
            this.obic7TextStringDaihyoushaTelNo1.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7LabelStringDaihyoushaTelNoHaihun1
            // 
            this.obic7LabelStringDaihyoushaTelNoHaihun1.AutoEllipsis = true;
            this.obic7LabelStringDaihyoushaTelNoHaihun1.ColumnNumber = 0;
            this.obic7LabelStringDaihyoushaTelNoHaihun1.EnableFalseLock = false;
            this.obic7LabelStringDaihyoushaTelNoHaihun1.ErrorString = null;
            this.obic7LabelStringDaihyoushaTelNoHaihun1.Location = new System.Drawing.Point(143, 96);
            this.obic7LabelStringDaihyoushaTelNoHaihun1.Name = "obic7LabelStringDaihyoushaTelNoHaihun1";
            this.obic7LabelStringDaihyoushaTelNoHaihun1.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7LabelStringDaihyoushaTelNoHaihun1.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringDaihyoushaTelNoHaihun1.Size = new System.Drawing.Size(13, 19);
            this.obic7LabelStringDaihyoushaTelNoHaihun1.TabIndex = 1000000106;
            this.obic7LabelStringDaihyoushaTelNoHaihun1.Text = "-";
            this.obic7LabelStringDaihyoushaTelNoHaihun1.Value = "-";
            this.obic7LabelStringDaihyoushaTelNoHaihun1.VisibleFalseLock = false;
            // 
            // obic7TextStringDaihyoushaTelNo2
            // 
            this.obic7TextStringDaihyoushaTelNo2.AllowAlphabet = false;
            this.obic7TextStringDaihyoushaTelNo2.AllowKana = false;
            this.obic7TextStringDaihyoushaTelNo2.AllowNumber = true;
            this.obic7TextStringDaihyoushaTelNo2.AllowSymbol = false;
            this.obic7TextStringDaihyoushaTelNo2.AllowUnicode = false;
            this.obic7TextStringDaihyoushaTelNo2.AllowZenkaku = false;
            this.obic7TextStringDaihyoushaTelNo2.AttachedButton = null;
            this.obic7TextStringDaihyoushaTelNo2.AttachedLabel = null;
            this.obic7TextStringDaihyoushaTelNo2.AttachedSearchDialog = null;
            this.obic7TextStringDaihyoushaTelNo2.CheckOnClose = false;
            this.obic7TextStringDaihyoushaTelNo2.ColumnNumber = 0;
            this.obic7TextStringDaihyoushaTelNo2.DispFuriganaText = null;
            this.obic7TextStringDaihyoushaTelNo2.EnableFalseLock = false;
            this.obic7TextStringDaihyoushaTelNo2.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringDaihyoushaTelNo2.ErrorString = null;
            this.obic7TextStringDaihyoushaTelNo2.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringDaihyoushaTelNo2.IsKeyColumn = false;
            this.obic7TextStringDaihyoushaTelNo2.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringDaihyoushaTelNo2.Location = new System.Drawing.Point(156, 96);
            this.obic7TextStringDaihyoushaTelNo2.MaxLengthByte = 5;
            this.obic7TextStringDaihyoushaTelNo2.Name = "obic7TextStringDaihyoushaTelNo2";
            this.obic7TextStringDaihyoushaTelNo2.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringDaihyoushaTelNo2.Size = new System.Drawing.Size(42, 19);
            this.obic7TextStringDaihyoushaTelNo2.TabIndex = 2450;
            this.obic7TextStringDaihyoushaTelNo2.Value = "";
            this.obic7TextStringDaihyoushaTelNo2.VisibleFalseLock = false;
            this.obic7TextStringDaihyoushaTelNo2.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7LabelStringDaihyoushaTelNoHaihun2
            // 
            this.obic7LabelStringDaihyoushaTelNoHaihun2.AutoEllipsis = true;
            this.obic7LabelStringDaihyoushaTelNoHaihun2.ColumnNumber = 0;
            this.obic7LabelStringDaihyoushaTelNoHaihun2.EnableFalseLock = false;
            this.obic7LabelStringDaihyoushaTelNoHaihun2.ErrorString = null;
            this.obic7LabelStringDaihyoushaTelNoHaihun2.Location = new System.Drawing.Point(197, 96);
            this.obic7LabelStringDaihyoushaTelNoHaihun2.Name = "obic7LabelStringDaihyoushaTelNoHaihun2";
            this.obic7LabelStringDaihyoushaTelNoHaihun2.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7LabelStringDaihyoushaTelNoHaihun2.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringDaihyoushaTelNoHaihun2.Size = new System.Drawing.Size(13, 19);
            this.obic7LabelStringDaihyoushaTelNoHaihun2.TabIndex = 1000000107;
            this.obic7LabelStringDaihyoushaTelNoHaihun2.Text = "-";
            this.obic7LabelStringDaihyoushaTelNoHaihun2.Value = "-";
            this.obic7LabelStringDaihyoushaTelNoHaihun2.VisibleFalseLock = false;
            // 
            // obic7TextStringDaihyoushaTelNo3
            // 
            this.obic7TextStringDaihyoushaTelNo3.AllowAlphabet = false;
            this.obic7TextStringDaihyoushaTelNo3.AllowKana = false;
            this.obic7TextStringDaihyoushaTelNo3.AllowNumber = true;
            this.obic7TextStringDaihyoushaTelNo3.AllowSymbol = false;
            this.obic7TextStringDaihyoushaTelNo3.AllowUnicode = false;
            this.obic7TextStringDaihyoushaTelNo3.AllowZenkaku = false;
            this.obic7TextStringDaihyoushaTelNo3.AttachedButton = null;
            this.obic7TextStringDaihyoushaTelNo3.AttachedLabel = null;
            this.obic7TextStringDaihyoushaTelNo3.AttachedSearchDialog = null;
            this.obic7TextStringDaihyoushaTelNo3.CheckOnClose = false;
            this.obic7TextStringDaihyoushaTelNo3.ColumnNumber = 0;
            this.obic7TextStringDaihyoushaTelNo3.DispFuriganaText = null;
            this.obic7TextStringDaihyoushaTelNo3.EnableFalseLock = false;
            this.obic7TextStringDaihyoushaTelNo3.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringDaihyoushaTelNo3.ErrorString = null;
            this.obic7TextStringDaihyoushaTelNo3.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringDaihyoushaTelNo3.IsKeyColumn = false;
            this.obic7TextStringDaihyoushaTelNo3.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringDaihyoushaTelNo3.Location = new System.Drawing.Point(210, 96);
            this.obic7TextStringDaihyoushaTelNo3.MaxLengthByte = 5;
            this.obic7TextStringDaihyoushaTelNo3.Name = "obic7TextStringDaihyoushaTelNo3";
            this.obic7TextStringDaihyoushaTelNo3.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringDaihyoushaTelNo3.Size = new System.Drawing.Size(42, 19);
            this.obic7TextStringDaihyoushaTelNo3.TabIndex = 2460;
            this.obic7TextStringDaihyoushaTelNo3.Value = "";
            this.obic7TextStringDaihyoushaTelNo3.VisibleFalseLock = false;
            this.obic7TextStringDaihyoushaTelNo3.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7LabelStringDaihyoushaPostCD
            // 
            this.obic7LabelStringDaihyoushaPostCD.AutoEllipsis = true;
            this.obic7LabelStringDaihyoushaPostCD.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelStringDaihyoushaPostCD.ColumnNumber = 0;
            this.obic7LabelStringDaihyoushaPostCD.EnableFalseLock = false;
            this.obic7LabelStringDaihyoushaPostCD.ErrorString = null;
            this.obic7LabelStringDaihyoushaPostCD.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.obic7LabelStringDaihyoushaPostCD.Location = new System.Drawing.Point(414, 24);
            this.obic7LabelStringDaihyoushaPostCD.Name = "obic7LabelStringDaihyoushaPostCD";
            this.obic7LabelStringDaihyoushaPostCD.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringDaihyoushaPostCD.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringDaihyoushaPostCD.Size = new System.Drawing.Size(84, 19);
            this.obic7LabelStringDaihyoushaPostCD.TabIndex = 1000000108;
            this.obic7LabelStringDaihyoushaPostCD.Text = "代表者郵便番号";
            this.obic7LabelStringDaihyoushaPostCD.Value = "代表者郵便番号";
            this.obic7LabelStringDaihyoushaPostCD.VisibleFalseLock = false;
            // 
            // obic7TextStringDaihyoushaPostCD1
            // 
            this.obic7TextStringDaihyoushaPostCD1.AllowAlphabet = false;
            this.obic7TextStringDaihyoushaPostCD1.AllowKana = false;
            this.obic7TextStringDaihyoushaPostCD1.AllowNumber = true;
            this.obic7TextStringDaihyoushaPostCD1.AllowSymbol = false;
            this.obic7TextStringDaihyoushaPostCD1.AllowUnicode = false;
            this.obic7TextStringDaihyoushaPostCD1.AllowZenkaku = false;
            this.obic7TextStringDaihyoushaPostCD1.AttachedButton = this.obic7ButtonWithLogicDaihyoushaPostCDSearch;
            this.obic7TextStringDaihyoushaPostCD1.AttachedLabel = this.obic7LabelStringDaihyoushaPostCD;
            this.obic7TextStringDaihyoushaPostCD1.AttachedSearchDialog = null;
            this.obic7TextStringDaihyoushaPostCD1.CheckOnClose = false;
            this.obic7TextStringDaihyoushaPostCD1.ColumnNumber = 0;
            this.obic7TextStringDaihyoushaPostCD1.DispFuriganaText = null;
            this.obic7TextStringDaihyoushaPostCD1.EnableFalseLock = false;
            this.obic7TextStringDaihyoushaPostCD1.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringDaihyoushaPostCD1.ErrorString = null;
            this.obic7TextStringDaihyoushaPostCD1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringDaihyoushaPostCD1.IsKeyColumn = false;
            this.obic7TextStringDaihyoushaPostCD1.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringDaihyoushaPostCD1.Location = new System.Drawing.Point(504, 24);
            this.obic7TextStringDaihyoushaPostCD1.MaxLengthByte = 3;
            this.obic7TextStringDaihyoushaPostCD1.Name = "obic7TextStringDaihyoushaPostCD1";
            this.obic7TextStringDaihyoushaPostCD1.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringDaihyoushaPostCD1.Size = new System.Drawing.Size(30, 19);
            this.obic7TextStringDaihyoushaPostCD1.TabIndex = 2470;
            this.obic7TextStringDaihyoushaPostCD1.Value = "";
            this.obic7TextStringDaihyoushaPostCD1.VisibleFalseLock = false;
            this.obic7TextStringDaihyoushaPostCD1.Enter += new System.EventHandler(this.controlDetailSearch_Enter);
            // 
            // obic7LabelStringDaihyoushaPostCDHaihun
            // 
            this.obic7LabelStringDaihyoushaPostCDHaihun.AutoEllipsis = true;
            this.obic7LabelStringDaihyoushaPostCDHaihun.ColumnNumber = 0;
            this.obic7LabelStringDaihyoushaPostCDHaihun.EnableFalseLock = false;
            this.obic7LabelStringDaihyoushaPostCDHaihun.ErrorString = null;
            this.obic7LabelStringDaihyoushaPostCDHaihun.Location = new System.Drawing.Point(533, 24);
            this.obic7LabelStringDaihyoushaPostCDHaihun.Name = "obic7LabelStringDaihyoushaPostCDHaihun";
            this.obic7LabelStringDaihyoushaPostCDHaihun.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7LabelStringDaihyoushaPostCDHaihun.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringDaihyoushaPostCDHaihun.Size = new System.Drawing.Size(13, 19);
            this.obic7LabelStringDaihyoushaPostCDHaihun.TabIndex = 1000000109;
            this.obic7LabelStringDaihyoushaPostCDHaihun.Text = "-";
            this.obic7LabelStringDaihyoushaPostCDHaihun.Value = "-";
            this.obic7LabelStringDaihyoushaPostCDHaihun.VisibleFalseLock = false;
            // 
            // obic7TextStringDaihyoushaPostCD2
            // 
            this.obic7TextStringDaihyoushaPostCD2.AllowAlphabet = false;
            this.obic7TextStringDaihyoushaPostCD2.AllowKana = false;
            this.obic7TextStringDaihyoushaPostCD2.AllowNumber = true;
            this.obic7TextStringDaihyoushaPostCD2.AllowSymbol = false;
            this.obic7TextStringDaihyoushaPostCD2.AllowUnicode = false;
            this.obic7TextStringDaihyoushaPostCD2.AllowZenkaku = false;
            this.obic7TextStringDaihyoushaPostCD2.AttachedButton = this.obic7ButtonWithLogicDaihyoushaPostCDSearch;
            this.obic7TextStringDaihyoushaPostCD2.AttachedLabel = null;
            this.obic7TextStringDaihyoushaPostCD2.AttachedSearchDialog = null;
            this.obic7TextStringDaihyoushaPostCD2.CheckOnClose = false;
            this.obic7TextStringDaihyoushaPostCD2.ColumnNumber = 0;
            this.obic7TextStringDaihyoushaPostCD2.DispFuriganaText = null;
            this.obic7TextStringDaihyoushaPostCD2.EnableFalseLock = false;
            this.obic7TextStringDaihyoushaPostCD2.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringDaihyoushaPostCD2.ErrorString = null;
            this.obic7TextStringDaihyoushaPostCD2.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringDaihyoushaPostCD2.IsKeyColumn = false;
            this.obic7TextStringDaihyoushaPostCD2.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringDaihyoushaPostCD2.Location = new System.Drawing.Point(546, 24);
            this.obic7TextStringDaihyoushaPostCD2.MaxLengthByte = 4;
            this.obic7TextStringDaihyoushaPostCD2.Name = "obic7TextStringDaihyoushaPostCD2";
            this.obic7TextStringDaihyoushaPostCD2.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringDaihyoushaPostCD2.Size = new System.Drawing.Size(36, 19);
            this.obic7TextStringDaihyoushaPostCD2.TabIndex = 2480;
            this.obic7TextStringDaihyoushaPostCD2.Value = "";
            this.obic7TextStringDaihyoushaPostCD2.VisibleFalseLock = false;
            this.obic7TextStringDaihyoushaPostCD2.Enter += new System.EventHandler(this.controlDetailSearch_Enter);
            this.obic7TextStringDaihyoushaPostCD2.Validating += new System.ComponentModel.CancelEventHandler(this.obic7TextStringDaihyoushaPostCD2_Validating);
            // 
            // obic7ButtonWithLogicDaihyoushaPostCDSearch
            // 
            this.obic7ButtonWithLogicDaihyoushaPostCDSearch.ColumnNumber = 0;
            this.obic7ButtonWithLogicDaihyoushaPostCDSearch.EnableFalseLock = false;
            this.obic7ButtonWithLogicDaihyoushaPostCDSearch.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.obic7ButtonWithLogicDaihyoushaPostCDSearch.Location = new System.Drawing.Point(582, 22);
            this.obic7ButtonWithLogicDaihyoushaPostCDSearch.Name = "obic7ButtonWithLogicDaihyoushaPostCDSearch";
            this.obic7ButtonWithLogicDaihyoushaPostCDSearch.Obic7JobSecurity = Obic7fx.Com.A0000.Tool.Obic7JobSecurities.NoSecurity;
            this.obic7ButtonWithLogicDaihyoushaPostCDSearch.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7ButtonWithLogicDaihyoushaPostCDSearch.Size = new System.Drawing.Size(24, 23);
            this.obic7ButtonWithLogicDaihyoushaPostCDSearch.TabIndex = 2490;
            this.obic7ButtonWithLogicDaihyoushaPostCDSearch.Text = "...";
            this.obic7ButtonWithLogicDaihyoushaPostCDSearch.UseVisualStyleBackColor = true;
            this.obic7ButtonWithLogicDaihyoushaPostCDSearch.VisibleFalseLock = false;
            this.obic7ButtonWithLogicDaihyoushaPostCDSearch.Click += new System.EventHandler(this.obic7ButtonWithLogicDaihyoushaPostCDSearch_Click);
            this.obic7ButtonWithLogicDaihyoushaPostCDSearch.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7LabelStringDaihyoushaAddress
            // 
            this.obic7LabelStringDaihyoushaAddress.AutoEllipsis = true;
            this.obic7LabelStringDaihyoushaAddress.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelStringDaihyoushaAddress.ColumnNumber = 0;
            this.obic7LabelStringDaihyoushaAddress.EnableFalseLock = false;
            this.obic7LabelStringDaihyoushaAddress.ErrorString = null;
            this.obic7LabelStringDaihyoushaAddress.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.obic7LabelStringDaihyoushaAddress.Location = new System.Drawing.Point(414, 48);
            this.obic7LabelStringDaihyoushaAddress.Name = "obic7LabelStringDaihyoushaAddress";
            this.obic7LabelStringDaihyoushaAddress.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringDaihyoushaAddress.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringDaihyoushaAddress.Size = new System.Drawing.Size(84, 19);
            this.obic7LabelStringDaihyoushaAddress.TabIndex = 1000000110;
            this.obic7LabelStringDaihyoushaAddress.Text = "代表者住所";
            this.obic7LabelStringDaihyoushaAddress.Value = "代表者住所";
            this.obic7LabelStringDaihyoushaAddress.VisibleFalseLock = false;
            // 
            // obic7TextStringDaihyoushaAddress1
            // 
            this.obic7TextStringDaihyoushaAddress1.AllowAlphabet = true;
            this.obic7TextStringDaihyoushaAddress1.AllowKana = true;
            this.obic7TextStringDaihyoushaAddress1.AllowNumber = true;
            this.obic7TextStringDaihyoushaAddress1.AllowSymbol = true;
            this.obic7TextStringDaihyoushaAddress1.AllowUnicode = false;
            this.obic7TextStringDaihyoushaAddress1.AllowZenkaku = true;
            this.obic7TextStringDaihyoushaAddress1.AttachedButton = null;
            this.obic7TextStringDaihyoushaAddress1.AttachedLabel = this.obic7LabelStringDaihyoushaAddress;
            this.obic7TextStringDaihyoushaAddress1.AttachedSearchDialog = null;
            this.obic7TextStringDaihyoushaAddress1.CheckOnClose = false;
            this.obic7TextStringDaihyoushaAddress1.ColumnNumber = 0;
            this.obic7TextStringDaihyoushaAddress1.DispFuriganaText = null;
            this.obic7TextStringDaihyoushaAddress1.EnableFalseLock = false;
            this.obic7TextStringDaihyoushaAddress1.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringDaihyoushaAddress1.ErrorString = null;
            this.obic7TextStringDaihyoushaAddress1.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.obic7TextStringDaihyoushaAddress1.IsKeyColumn = false;
            this.obic7TextStringDaihyoushaAddress1.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringDaihyoushaAddress1.Location = new System.Drawing.Point(504, 48);
            this.obic7TextStringDaihyoushaAddress1.MaxLengthByte = 50;
            this.obic7TextStringDaihyoushaAddress1.Name = "obic7TextStringDaihyoushaAddress1";
            this.obic7TextStringDaihyoushaAddress1.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringDaihyoushaAddress1.Size = new System.Drawing.Size(330, 19);
            this.obic7TextStringDaihyoushaAddress1.TabIndex = 2500;
            this.obic7TextStringDaihyoushaAddress1.Value = "";
            this.obic7TextStringDaihyoushaAddress1.VisibleFalseLock = false;
            this.obic7TextStringDaihyoushaAddress1.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7TextStringDaihyoushaAddress2
            // 
            this.obic7TextStringDaihyoushaAddress2.AllowAlphabet = true;
            this.obic7TextStringDaihyoushaAddress2.AllowKana = true;
            this.obic7TextStringDaihyoushaAddress2.AllowNumber = true;
            this.obic7TextStringDaihyoushaAddress2.AllowSymbol = true;
            this.obic7TextStringDaihyoushaAddress2.AllowUnicode = false;
            this.obic7TextStringDaihyoushaAddress2.AllowZenkaku = true;
            this.obic7TextStringDaihyoushaAddress2.AttachedButton = null;
            this.obic7TextStringDaihyoushaAddress2.AttachedLabel = null;
            this.obic7TextStringDaihyoushaAddress2.AttachedSearchDialog = null;
            this.obic7TextStringDaihyoushaAddress2.CheckOnClose = false;
            this.obic7TextStringDaihyoushaAddress2.ColumnNumber = 0;
            this.obic7TextStringDaihyoushaAddress2.DispFuriganaText = null;
            this.obic7TextStringDaihyoushaAddress2.EnableFalseLock = false;
            this.obic7TextStringDaihyoushaAddress2.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringDaihyoushaAddress2.ErrorString = null;
            this.obic7TextStringDaihyoushaAddress2.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.obic7TextStringDaihyoushaAddress2.IsKeyColumn = false;
            this.obic7TextStringDaihyoushaAddress2.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringDaihyoushaAddress2.Location = new System.Drawing.Point(504, 72);
            this.obic7TextStringDaihyoushaAddress2.MaxLengthByte = 50;
            this.obic7TextStringDaihyoushaAddress2.Name = "obic7TextStringDaihyoushaAddress2";
            this.obic7TextStringDaihyoushaAddress2.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringDaihyoushaAddress2.Size = new System.Drawing.Size(330, 19);
            this.obic7TextStringDaihyoushaAddress2.TabIndex = 2510;
            this.obic7TextStringDaihyoushaAddress2.Value = "";
            this.obic7TextStringDaihyoushaAddress2.VisibleFalseLock = false;
            this.obic7TextStringDaihyoushaAddress2.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7GroupBoxTihouzeiSinkoku
            // 
            this.obic7GroupBoxTihouzeiSinkoku.ColumnNumber = 0;
            this.obic7GroupBoxTihouzeiSinkoku.Controls.Add(this.obic7LabelStringNouzeishaCD);
            this.obic7GroupBoxTihouzeiSinkoku.Controls.Add(this.obic7TextStringNouzeishaCD);
            this.obic7GroupBoxTihouzeiSinkoku.Controls.Add(this.obic7LabelStringRiyoushaID);
            this.obic7GroupBoxTihouzeiSinkoku.Controls.Add(this.obic7TextStringRiyoushaID);
            this.obic7GroupBoxTihouzeiSinkoku.Controls.Add(this.obic7LabelStringDaihyoushaSikaku);
            this.obic7GroupBoxTihouzeiSinkoku.Controls.Add(this.obic7TextStringDaihyoushaSikaku);
            this.obic7GroupBoxTihouzeiSinkoku.Controls.Add(this.obic7LabelStringDaihyoushaSikakuName);
            this.obic7GroupBoxTihouzeiSinkoku.Controls.Add(this.obic7LabelStringDaihyoushaTelNo);
            this.obic7GroupBoxTihouzeiSinkoku.Controls.Add(this.obic7TextStringDaihyoushaTelNo1);
            this.obic7GroupBoxTihouzeiSinkoku.Controls.Add(this.obic7LabelStringDaihyoushaTelNoHaihun1);
            this.obic7GroupBoxTihouzeiSinkoku.Controls.Add(this.obic7TextStringDaihyoushaTelNo2);
            this.obic7GroupBoxTihouzeiSinkoku.Controls.Add(this.obic7LabelStringDaihyoushaTelNoHaihun2);
            this.obic7GroupBoxTihouzeiSinkoku.Controls.Add(this.obic7TextStringDaihyoushaTelNo3);
            this.obic7GroupBoxTihouzeiSinkoku.Controls.Add(this.obic7LabelStringDaihyoushaPostCD);
            this.obic7GroupBoxTihouzeiSinkoku.Controls.Add(this.obic7TextStringDaihyoushaPostCD1);
            this.obic7GroupBoxTihouzeiSinkoku.Controls.Add(this.obic7LabelStringDaihyoushaPostCDHaihun);
            this.obic7GroupBoxTihouzeiSinkoku.Controls.Add(this.obic7TextStringDaihyoushaPostCD2);
            this.obic7GroupBoxTihouzeiSinkoku.Controls.Add(this.obic7ButtonWithLogicDaihyoushaPostCDSearch);
            this.obic7GroupBoxTihouzeiSinkoku.Controls.Add(this.obic7LabelStringDaihyoushaAddress);
            this.obic7GroupBoxTihouzeiSinkoku.Controls.Add(this.obic7TextStringDaihyoushaAddress1);
            this.obic7GroupBoxTihouzeiSinkoku.Controls.Add(this.obic7TextStringDaihyoushaAddress2);
            this.obic7GroupBoxTihouzeiSinkoku.EnableFalseLock = false;
            this.obic7GroupBoxTihouzeiSinkoku.Location = new System.Drawing.Point(54, 369);
            this.obic7GroupBoxTihouzeiSinkoku.Name = "obic7GroupBoxTihouzeiSinkoku";
            this.obic7GroupBoxTihouzeiSinkoku.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7GroupBoxTihouzeiSinkoku.Size = new System.Drawing.Size(852, 124);
            this.obic7GroupBoxTihouzeiSinkoku.TabIndex = 2400;
            this.obic7GroupBoxTihouzeiSinkoku.TabStop = false;
            this.obic7GroupBoxTihouzeiSinkoku.Text = "地方税申告（連携）";
            this.obic7GroupBoxTihouzeiSinkoku.VisibleFalseLock = false;
            // 2026/10/11 追加 (5525)事業所別償却資産申告対応 終了
            // 
            // NA_SisanZigyoushoMasterMaintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1228, 666);
            this.Name = "NA_SisanZigyoushoMasterMaintenance";
            this.Text = "資産事業所マスタメンテ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NA_SisanZigyoushoMasterMaintenance_FormClosing);
            this.Load += new System.EventHandler(this.NA_SisanZigyoushoMasterMaintenance_Load);
            this.Shown += new System.EventHandler(this.NA_SisanZigyoushoMasterMaintenance_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NA_SisanZigyoushoMasterMaintenance_KeyDown);
            this.obic7PanelHeader.ResumeLayout(false);
            this.obic7PanelHeader.PerformLayout();
            this.obic7PanelDetail.ResumeLayout(false);
            this.obic7PanelDetail.PerformLayout();
            this.obic7GroupBoxBasicInfo.ResumeLayout(false);
            this.obic7GroupBoxBasicInfo.PerformLayout();
            this.obic7GroupBoxSinkokushoKoumoku.ResumeLayout(false);
            this.obic7GroupBoxSinkokushoKoumoku.PerformLayout();
            this.obic7GroupBoxTihouzeiSinkoku.ResumeLayout(false);
            this.obic7GroupBoxTihouzeiSinkoku.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringMode;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringBaseDate;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringZigyoushoCD;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextDatetime obic7TextDatetimeBaseDate;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringZigyoushoCD;
        private Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic obic7ButtonWithLogicZigyoushoCDSearch;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringZigyoushoName;
        private Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic obic7ButtonWithLogicDisp;
        private Obic7fx.Win.A0000.Tool.UI.Obic7GroupBox obic7GroupBoxBasicInfo;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringZititai;
        private Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic obic7ButtonWithLogicZititaiCDSearch;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringZititaiCD;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringZititaiName;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringPostCD;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringPostCDHaihun;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringPostCD1;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringPostCD2;
        private Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic obic7ButtonWithLogicPostCDSearch;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringAddress1;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringAddress1;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringAddress1Kana;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringAddress2;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringAddress2;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringAddress2Kana;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringAddress2Kana;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringAddress1Kana;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringTelNo1;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringTelNo;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringCompanyName;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringTelNo3;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringTelNoHaihun2;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringTelNo2;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringTelNoHaihun1;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringCompanyKanaName;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringCompanyKanaName;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringCompanyName;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringDaihyoushaKanaName;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringDaihyoushaName;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringDaihyoushaName;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringDaihyoushaKanaName;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringZigyoushumoku;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringZigyoushumoku;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringYagou;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringYagou;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringSihonkin;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber obic7TextNumberSihonkin;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextDatetime obic7TextDatetimeSeturituDate;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringSeturituDate;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringSinkokuTantoushaName;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringSinkokuTantoushaName;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringKaikeiZimushoName;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringTantoushaTelNo3;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringTelNoHaihun4;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringTantoushaTelNo2;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringTelNoHaihun3;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringTantoushaTelNo1;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringTantoushaTelNo;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKaikeiZimushoTelNo3;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringTelNoHaihun6;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKaikeiZimushoTelNo2;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringTelNoHaihun5;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKaikeiZimushoTelNo1;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringKaikeiZimushoTelNo;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKaikeiZimushoName;
        private Obic7fx.Win.A0000.Tool.UI.Obic7GroupBox obic7GroupBoxSinkokushoKoumoku;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextSeparator obic7TextSeparatorZititaiCD;
        // 2026/10/11 追加 (5525)事業所別償却資産申告対応 開始
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringNouzeishaCD;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringNouzeishaCD;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringRiyoushaID;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringRiyoushaID;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringDaihyoushaSikaku;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringDaihyoushaSikaku;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringDaihyoushaSikakuName;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringDaihyoushaTelNo;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringDaihyoushaTelNo1;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringDaihyoushaTelNoHaihun1;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringDaihyoushaTelNo2;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringDaihyoushaTelNoHaihun2;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringDaihyoushaTelNo3;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringDaihyoushaPostCD;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringDaihyoushaPostCD1;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringDaihyoushaPostCDHaihun;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringDaihyoushaPostCD2;
        private Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic obic7ButtonWithLogicDaihyoushaPostCDSearch;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringDaihyoushaAddress;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringDaihyoushaAddress1;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringDaihyoushaAddress2;
        private Obic7fx.Win.A0000.Tool.UI.Obic7GroupBox obic7GroupBoxTihouzeiSinkoku;
        // 2026/10/11 追加 (5525)事業所別償却資産申告対応 終了

    }
}

