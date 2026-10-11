//
//    2026/10/11    新規作成    (5525)事業所別消費税計算方式対応　事業所別制御マスタメンテ
//

namespace Obic7fx.Win.M5525.MasterM.MA_ZigyoushoSeigyo
{
    partial class MA_ZigyoushoSeigyoMasterMaintenance
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
            this.obic7LabelStringUriageZeiTumiageKBNCaption = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextNumberUriageZeiTumiageKBN = new Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber();
            this.obic7ButtonWithLogicUriageZeiTumiageKBNSearch = new Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic();
            this.obic7LabelStringUriageZeiTumiageKBNName = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelStringSiireZeiTumiageKBNCaption = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextNumberSiireZeiTumiageKBN = new Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber();
            this.obic7ButtonWithLogicSiireZeiTumiageKBNSearch = new Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic();
            this.obic7LabelStringSiireZeiTumiageKBNName = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextNumberR5NenOctIgoUriageZeiTumiageKBN = new Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber();
            this.obic7ButtonWithLogicR5NenOctIgoUriageZeiTumiageKBNSearch = new Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic();
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextNumberR5NenOctIgoSiireZeiTumiageKBN = new Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber();
            this.obic7ButtonWithLogicR5NenOctIgoSiireZeiTumiageKBNSearch = new Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic();
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelStringTorihikisakiCDCaption = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringTorihikisakiCD = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7ButtonWithLogicTorihikisakiCDSearch = new Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic();
            this.obic7LabelStringTorihikisakiName = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7GroupBoxShouhizeiCalc = new Obic7fx.Win.A0000.Tool.UI.Obic7GroupBox();
            this.obic7GroupBoxTorihikisaki = new Obic7fx.Win.A0000.Tool.UI.Obic7GroupBox();
            this.obic7TextSeparatorDetail = new Obic7fx.Win.A0000.Tool.UI.Obic7TextSeparator();
            this.obic7PanelHeader.SuspendLayout();
            this.obic7PanelDetail.SuspendLayout();
            this.obic7GroupBoxShouhizeiCalc.SuspendLayout();
            this.obic7GroupBoxTorihikisaki.SuspendLayout();
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
            this.obic7PanelHeader.Controls.Add(this.obic7LabelStringBaseDate);
            this.obic7PanelHeader.Controls.Add(this.obic7LabelStringMode);
            this.obic7PanelHeader.TabIndex = 1000;
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7LabelStringMode, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7LabelStringBaseDate, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7LabelStringZigyoushoCD, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7TextDatetimeBaseDate, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7TextStringZigyoushoCD, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7ButtonWithLogicZigyoushoCDSearch, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7LabelStringZigyoushoName, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7ButtonWithLogicDisp, 0);
            // 
            // obic7PanelDetail
            // 
            this.obic7PanelDetail.Controls.Add(this.obic7TextSeparatorDetail);
            this.obic7PanelDetail.Controls.Add(this.obic7GroupBoxShouhizeiCalc);
            this.obic7PanelDetail.Controls.Add(this.obic7GroupBoxTorihikisaki);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7GroupBoxTorihikisaki, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7GroupBoxShouhizeiCalc, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7TextSeparatorDetail, 0);
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
            // obic7LabelStringUriageZeiTumiageKBNCaption
            // 
            this.obic7LabelStringUriageZeiTumiageKBNCaption.AutoEllipsis = true;
            this.obic7LabelStringUriageZeiTumiageKBNCaption.ColumnNumber = 0;
            this.obic7LabelStringUriageZeiTumiageKBNCaption.EnableFalseLock = false;
            this.obic7LabelStringUriageZeiTumiageKBNCaption.ErrorString = null;
            this.obic7LabelStringUriageZeiTumiageKBNCaption.Location = new System.Drawing.Point(6, 24);
            this.obic7LabelStringUriageZeiTumiageKBNCaption.Name = "obic7LabelStringUriageZeiTumiageKBNCaption";
            this.obic7LabelStringUriageZeiTumiageKBNCaption.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.RequiredLabel;
            this.obic7LabelStringUriageZeiTumiageKBNCaption.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringUriageZeiTumiageKBNCaption.Size = new System.Drawing.Size(210, 19);
            this.obic7LabelStringUriageZeiTumiageKBNCaption.TabIndex = 1000000101;
            this.obic7LabelStringUriageZeiTumiageKBNCaption.Text = "R5.9.30以前売上消費税計算方式";
            this.obic7LabelStringUriageZeiTumiageKBNCaption.Value = "R5.9.30以前売上消費税計算方式";
            this.obic7LabelStringUriageZeiTumiageKBNCaption.VisibleFalseLock = false;
            // 
            // obic7TextNumberUriageZeiTumiageKBN
            // 
            this.obic7TextNumberUriageZeiTumiageKBN.AttachedButton = this.obic7ButtonWithLogicUriageZeiTumiageKBNSearch;
            this.obic7TextNumberUriageZeiTumiageKBN.AttachedCopyText = null;
            this.obic7TextNumberUriageZeiTumiageKBN.AttachedLabel = null;
            this.obic7TextNumberUriageZeiTumiageKBN.AttachedSearchDialog = null;
            this.obic7TextNumberUriageZeiTumiageKBN.CheckOnClose = false;
            this.obic7TextNumberUriageZeiTumiageKBN.ColumnNumber = 0;
            this.obic7TextNumberUriageZeiTumiageKBN.EnableFalseLock = false;
            this.obic7TextNumberUriageZeiTumiageKBN.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberUriageZeiTumiageKBN.ErrorString = null;
            this.obic7TextNumberUriageZeiTumiageKBN.ForeColor = System.Drawing.SystemColors.WindowText;
            this.obic7TextNumberUriageZeiTumiageKBN.Format = "0";
            this.obic7TextNumberUriageZeiTumiageKBN.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.obic7TextNumberUriageZeiTumiageKBN.IsKeyColumn = false;
            this.obic7TextNumberUriageZeiTumiageKBN.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberUriageZeiTumiageKBN.Location = new System.Drawing.Point(222, 24);
            this.obic7TextNumberUriageZeiTumiageKBN.MaxLengthByte = 1;
            this.obic7TextNumberUriageZeiTumiageKBN.MaxValue = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.obic7TextNumberUriageZeiTumiageKBN.MinusColor = System.Drawing.Color.Red;
            this.obic7TextNumberUriageZeiTumiageKBN.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.obic7TextNumberUriageZeiTumiageKBN.Name = "obic7TextNumberUriageZeiTumiageKBN";
            this.obic7TextNumberUriageZeiTumiageKBN.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextNumberUriageZeiTumiageKBN.Size = new System.Drawing.Size(18, 19);
            this.obic7TextNumberUriageZeiTumiageKBN.TabIndex = 2010;
            this.obic7TextNumberUriageZeiTumiageKBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.obic7TextNumberUriageZeiTumiageKBN.Value = null;
            this.obic7TextNumberUriageZeiTumiageKBN.VisibleFalseLock = false;
            this.obic7TextNumberUriageZeiTumiageKBN.Enter += new System.EventHandler(this.controlDetailSearch_Enter);
            this.obic7TextNumberUriageZeiTumiageKBN.Validating += new System.ComponentModel.CancelEventHandler(this.obic7TextNumberCalcKBN_Validating);
            // 
            // obic7ButtonWithLogicUriageZeiTumiageKBNSearch
            // 
            this.obic7ButtonWithLogicUriageZeiTumiageKBNSearch.ColumnNumber = 0;
            this.obic7ButtonWithLogicUriageZeiTumiageKBNSearch.EnableFalseLock = false;
            this.obic7ButtonWithLogicUriageZeiTumiageKBNSearch.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.obic7ButtonWithLogicUriageZeiTumiageKBNSearch.Location = new System.Drawing.Point(240, 22);
            this.obic7ButtonWithLogicUriageZeiTumiageKBNSearch.Name = "obic7ButtonWithLogicUriageZeiTumiageKBNSearch";
            this.obic7ButtonWithLogicUriageZeiTumiageKBNSearch.Obic7JobSecurity = Obic7fx.Com.A0000.Tool.Obic7JobSecurities.NoSecurity;
            this.obic7ButtonWithLogicUriageZeiTumiageKBNSearch.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7ButtonWithLogicUriageZeiTumiageKBNSearch.Size = new System.Drawing.Size(24, 23);
            this.obic7ButtonWithLogicUriageZeiTumiageKBNSearch.TabIndex = 2015;
            this.obic7ButtonWithLogicUriageZeiTumiageKBNSearch.Text = "...";
            this.obic7ButtonWithLogicUriageZeiTumiageKBNSearch.UseVisualStyleBackColor = true;
            this.obic7ButtonWithLogicUriageZeiTumiageKBNSearch.VisibleFalseLock = false;
            this.obic7ButtonWithLogicUriageZeiTumiageKBNSearch.Click += new System.EventHandler(this.obic7ButtonWithLogicCalcKBNSearch_Click);
            this.obic7ButtonWithLogicUriageZeiTumiageKBNSearch.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7LabelStringUriageZeiTumiageKBNName
            // 
            this.obic7LabelStringUriageZeiTumiageKBNName.AutoEllipsis = true;
            this.obic7LabelStringUriageZeiTumiageKBNName.BackColor = System.Drawing.SystemColors.Control;
            this.obic7LabelStringUriageZeiTumiageKBNName.ColumnNumber = 0;
            this.obic7LabelStringUriageZeiTumiageKBNName.EnableFalseLock = false;
            this.obic7LabelStringUriageZeiTumiageKBNName.ErrorString = null;
            this.obic7LabelStringUriageZeiTumiageKBNName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelStringUriageZeiTumiageKBNName.Location = new System.Drawing.Point(264, 24);
            this.obic7LabelStringUriageZeiTumiageKBNName.Name = "obic7LabelStringUriageZeiTumiageKBNName";
            this.obic7LabelStringUriageZeiTumiageKBNName.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7LabelStringUriageZeiTumiageKBNName.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringUriageZeiTumiageKBNName.Size = new System.Drawing.Size(282, 19);
            this.obic7LabelStringUriageZeiTumiageKBNName.TabIndex = 1000000102;
            this.obic7LabelStringUriageZeiTumiageKBNName.Value = "";
            this.obic7LabelStringUriageZeiTumiageKBNName.VisibleFalseLock = false;
            // 
            // obic7LabelStringSiireZeiTumiageKBNCaption
            // 
            this.obic7LabelStringSiireZeiTumiageKBNCaption.AutoEllipsis = true;
            this.obic7LabelStringSiireZeiTumiageKBNCaption.ColumnNumber = 0;
            this.obic7LabelStringSiireZeiTumiageKBNCaption.EnableFalseLock = false;
            this.obic7LabelStringSiireZeiTumiageKBNCaption.ErrorString = null;
            this.obic7LabelStringSiireZeiTumiageKBNCaption.Location = new System.Drawing.Point(6, 48);
            this.obic7LabelStringSiireZeiTumiageKBNCaption.Name = "obic7LabelStringSiireZeiTumiageKBNCaption";
            this.obic7LabelStringSiireZeiTumiageKBNCaption.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.RequiredLabel;
            this.obic7LabelStringSiireZeiTumiageKBNCaption.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringSiireZeiTumiageKBNCaption.Size = new System.Drawing.Size(210, 19);
            this.obic7LabelStringSiireZeiTumiageKBNCaption.TabIndex = 1000000103;
            this.obic7LabelStringSiireZeiTumiageKBNCaption.Text = "R5.9.30以前仕入消費税計算方式";
            this.obic7LabelStringSiireZeiTumiageKBNCaption.Value = "R5.9.30以前仕入消費税計算方式";
            this.obic7LabelStringSiireZeiTumiageKBNCaption.VisibleFalseLock = false;
            // 
            // obic7TextNumberSiireZeiTumiageKBN
            // 
            this.obic7TextNumberSiireZeiTumiageKBN.AttachedButton = this.obic7ButtonWithLogicSiireZeiTumiageKBNSearch;
            this.obic7TextNumberSiireZeiTumiageKBN.AttachedCopyText = null;
            this.obic7TextNumberSiireZeiTumiageKBN.AttachedLabel = null;
            this.obic7TextNumberSiireZeiTumiageKBN.AttachedSearchDialog = null;
            this.obic7TextNumberSiireZeiTumiageKBN.CheckOnClose = false;
            this.obic7TextNumberSiireZeiTumiageKBN.ColumnNumber = 0;
            this.obic7TextNumberSiireZeiTumiageKBN.EnableFalseLock = false;
            this.obic7TextNumberSiireZeiTumiageKBN.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberSiireZeiTumiageKBN.ErrorString = null;
            this.obic7TextNumberSiireZeiTumiageKBN.ForeColor = System.Drawing.SystemColors.WindowText;
            this.obic7TextNumberSiireZeiTumiageKBN.Format = "0";
            this.obic7TextNumberSiireZeiTumiageKBN.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.obic7TextNumberSiireZeiTumiageKBN.IsKeyColumn = false;
            this.obic7TextNumberSiireZeiTumiageKBN.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberSiireZeiTumiageKBN.Location = new System.Drawing.Point(222, 48);
            this.obic7TextNumberSiireZeiTumiageKBN.MaxLengthByte = 1;
            this.obic7TextNumberSiireZeiTumiageKBN.MaxValue = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.obic7TextNumberSiireZeiTumiageKBN.MinusColor = System.Drawing.Color.Red;
            this.obic7TextNumberSiireZeiTumiageKBN.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.obic7TextNumberSiireZeiTumiageKBN.Name = "obic7TextNumberSiireZeiTumiageKBN";
            this.obic7TextNumberSiireZeiTumiageKBN.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextNumberSiireZeiTumiageKBN.Size = new System.Drawing.Size(18, 19);
            this.obic7TextNumberSiireZeiTumiageKBN.TabIndex = 2020;
            this.obic7TextNumberSiireZeiTumiageKBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.obic7TextNumberSiireZeiTumiageKBN.Value = null;
            this.obic7TextNumberSiireZeiTumiageKBN.VisibleFalseLock = false;
            this.obic7TextNumberSiireZeiTumiageKBN.Enter += new System.EventHandler(this.controlDetailSearch_Enter);
            this.obic7TextNumberSiireZeiTumiageKBN.Validating += new System.ComponentModel.CancelEventHandler(this.obic7TextNumberCalcKBN_Validating);
            // 
            // obic7ButtonWithLogicSiireZeiTumiageKBNSearch
            // 
            this.obic7ButtonWithLogicSiireZeiTumiageKBNSearch.ColumnNumber = 0;
            this.obic7ButtonWithLogicSiireZeiTumiageKBNSearch.EnableFalseLock = false;
            this.obic7ButtonWithLogicSiireZeiTumiageKBNSearch.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.obic7ButtonWithLogicSiireZeiTumiageKBNSearch.Location = new System.Drawing.Point(240, 46);
            this.obic7ButtonWithLogicSiireZeiTumiageKBNSearch.Name = "obic7ButtonWithLogicSiireZeiTumiageKBNSearch";
            this.obic7ButtonWithLogicSiireZeiTumiageKBNSearch.Obic7JobSecurity = Obic7fx.Com.A0000.Tool.Obic7JobSecurities.NoSecurity;
            this.obic7ButtonWithLogicSiireZeiTumiageKBNSearch.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7ButtonWithLogicSiireZeiTumiageKBNSearch.Size = new System.Drawing.Size(24, 23);
            this.obic7ButtonWithLogicSiireZeiTumiageKBNSearch.TabIndex = 2025;
            this.obic7ButtonWithLogicSiireZeiTumiageKBNSearch.Text = "...";
            this.obic7ButtonWithLogicSiireZeiTumiageKBNSearch.UseVisualStyleBackColor = true;
            this.obic7ButtonWithLogicSiireZeiTumiageKBNSearch.VisibleFalseLock = false;
            this.obic7ButtonWithLogicSiireZeiTumiageKBNSearch.Click += new System.EventHandler(this.obic7ButtonWithLogicCalcKBNSearch_Click);
            this.obic7ButtonWithLogicSiireZeiTumiageKBNSearch.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7LabelStringSiireZeiTumiageKBNName
            // 
            this.obic7LabelStringSiireZeiTumiageKBNName.AutoEllipsis = true;
            this.obic7LabelStringSiireZeiTumiageKBNName.BackColor = System.Drawing.SystemColors.Control;
            this.obic7LabelStringSiireZeiTumiageKBNName.ColumnNumber = 0;
            this.obic7LabelStringSiireZeiTumiageKBNName.EnableFalseLock = false;
            this.obic7LabelStringSiireZeiTumiageKBNName.ErrorString = null;
            this.obic7LabelStringSiireZeiTumiageKBNName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelStringSiireZeiTumiageKBNName.Location = new System.Drawing.Point(264, 48);
            this.obic7LabelStringSiireZeiTumiageKBNName.Name = "obic7LabelStringSiireZeiTumiageKBNName";
            this.obic7LabelStringSiireZeiTumiageKBNName.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7LabelStringSiireZeiTumiageKBNName.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringSiireZeiTumiageKBNName.Size = new System.Drawing.Size(282, 19);
            this.obic7LabelStringSiireZeiTumiageKBNName.TabIndex = 1000000104;
            this.obic7LabelStringSiireZeiTumiageKBNName.Value = "";
            this.obic7LabelStringSiireZeiTumiageKBNName.VisibleFalseLock = false;
            // 
            // obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption
            // 
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption.AutoEllipsis = true;
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption.ColumnNumber = 0;
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption.EnableFalseLock = false;
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption.ErrorString = null;
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption.Location = new System.Drawing.Point(6, 72);
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption.Name = "obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption";
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.RequiredLabel;
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption.Size = new System.Drawing.Size(210, 19);
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption.TabIndex = 1000000105;
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption.Text = "R5.10.1以後売上消費税計算方式";
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption.Value = "R5.10.1以後売上消費税計算方式";
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption.VisibleFalseLock = false;
            // 
            // obic7TextNumberR5NenOctIgoUriageZeiTumiageKBN
            // 
            this.obic7TextNumberR5NenOctIgoUriageZeiTumiageKBN.AttachedButton = this.obic7ButtonWithLogicR5NenOctIgoUriageZeiTumiageKBNSearch;
            this.obic7TextNumberR5NenOctIgoUriageZeiTumiageKBN.AttachedCopyText = null;
            this.obic7TextNumberR5NenOctIgoUriageZeiTumiageKBN.AttachedLabel = null;
            this.obic7TextNumberR5NenOctIgoUriageZeiTumiageKBN.AttachedSearchDialog = null;
            this.obic7TextNumberR5NenOctIgoUriageZeiTumiageKBN.CheckOnClose = false;
            this.obic7TextNumberR5NenOctIgoUriageZeiTumiageKBN.ColumnNumber = 0;
            this.obic7TextNumberR5NenOctIgoUriageZeiTumiageKBN.EnableFalseLock = false;
            this.obic7TextNumberR5NenOctIgoUriageZeiTumiageKBN.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberR5NenOctIgoUriageZeiTumiageKBN.ErrorString = null;
            this.obic7TextNumberR5NenOctIgoUriageZeiTumiageKBN.ForeColor = System.Drawing.SystemColors.WindowText;
            this.obic7TextNumberR5NenOctIgoUriageZeiTumiageKBN.Format = "0";
            this.obic7TextNumberR5NenOctIgoUriageZeiTumiageKBN.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.obic7TextNumberR5NenOctIgoUriageZeiTumiageKBN.IsKeyColumn = false;
            this.obic7TextNumberR5NenOctIgoUriageZeiTumiageKBN.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberR5NenOctIgoUriageZeiTumiageKBN.Location = new System.Drawing.Point(222, 72);
            this.obic7TextNumberR5NenOctIgoUriageZeiTumiageKBN.MaxLengthByte = 1;
            this.obic7TextNumberR5NenOctIgoUriageZeiTumiageKBN.MaxValue = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.obic7TextNumberR5NenOctIgoUriageZeiTumiageKBN.MinusColor = System.Drawing.Color.Red;
            this.obic7TextNumberR5NenOctIgoUriageZeiTumiageKBN.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.obic7TextNumberR5NenOctIgoUriageZeiTumiageKBN.Name = "obic7TextNumberR5NenOctIgoUriageZeiTumiageKBN";
            this.obic7TextNumberR5NenOctIgoUriageZeiTumiageKBN.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextNumberR5NenOctIgoUriageZeiTumiageKBN.Size = new System.Drawing.Size(18, 19);
            this.obic7TextNumberR5NenOctIgoUriageZeiTumiageKBN.TabIndex = 2030;
            this.obic7TextNumberR5NenOctIgoUriageZeiTumiageKBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.obic7TextNumberR5NenOctIgoUriageZeiTumiageKBN.Value = null;
            this.obic7TextNumberR5NenOctIgoUriageZeiTumiageKBN.VisibleFalseLock = false;
            this.obic7TextNumberR5NenOctIgoUriageZeiTumiageKBN.Enter += new System.EventHandler(this.controlDetailSearch_Enter);
            this.obic7TextNumberR5NenOctIgoUriageZeiTumiageKBN.Validating += new System.ComponentModel.CancelEventHandler(this.obic7TextNumberCalcKBN_Validating);
            // 
            // obic7ButtonWithLogicR5NenOctIgoUriageZeiTumiageKBNSearch
            // 
            this.obic7ButtonWithLogicR5NenOctIgoUriageZeiTumiageKBNSearch.ColumnNumber = 0;
            this.obic7ButtonWithLogicR5NenOctIgoUriageZeiTumiageKBNSearch.EnableFalseLock = false;
            this.obic7ButtonWithLogicR5NenOctIgoUriageZeiTumiageKBNSearch.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.obic7ButtonWithLogicR5NenOctIgoUriageZeiTumiageKBNSearch.Location = new System.Drawing.Point(240, 70);
            this.obic7ButtonWithLogicR5NenOctIgoUriageZeiTumiageKBNSearch.Name = "obic7ButtonWithLogicR5NenOctIgoUriageZeiTumiageKBNSearch";
            this.obic7ButtonWithLogicR5NenOctIgoUriageZeiTumiageKBNSearch.Obic7JobSecurity = Obic7fx.Com.A0000.Tool.Obic7JobSecurities.NoSecurity;
            this.obic7ButtonWithLogicR5NenOctIgoUriageZeiTumiageKBNSearch.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7ButtonWithLogicR5NenOctIgoUriageZeiTumiageKBNSearch.Size = new System.Drawing.Size(24, 23);
            this.obic7ButtonWithLogicR5NenOctIgoUriageZeiTumiageKBNSearch.TabIndex = 2035;
            this.obic7ButtonWithLogicR5NenOctIgoUriageZeiTumiageKBNSearch.Text = "...";
            this.obic7ButtonWithLogicR5NenOctIgoUriageZeiTumiageKBNSearch.UseVisualStyleBackColor = true;
            this.obic7ButtonWithLogicR5NenOctIgoUriageZeiTumiageKBNSearch.VisibleFalseLock = false;
            this.obic7ButtonWithLogicR5NenOctIgoUriageZeiTumiageKBNSearch.Click += new System.EventHandler(this.obic7ButtonWithLogicCalcKBNSearch_Click);
            this.obic7ButtonWithLogicR5NenOctIgoUriageZeiTumiageKBNSearch.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName
            // 
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName.AutoEllipsis = true;
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName.BackColor = System.Drawing.SystemColors.Control;
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName.ColumnNumber = 0;
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName.EnableFalseLock = false;
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName.ErrorString = null;
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName.Location = new System.Drawing.Point(264, 72);
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName.Name = "obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName";
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName.Size = new System.Drawing.Size(282, 19);
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName.TabIndex = 1000000106;
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName.Value = "";
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName.VisibleFalseLock = false;
            // 
            // obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption
            // 
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption.AutoEllipsis = true;
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption.ColumnNumber = 0;
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption.EnableFalseLock = false;
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption.ErrorString = null;
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption.Location = new System.Drawing.Point(6, 96);
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption.Name = "obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption";
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.RequiredLabel;
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption.Size = new System.Drawing.Size(210, 19);
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption.TabIndex = 1000000107;
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption.Text = "R5.10.1以後仕入消費税計算方式";
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption.Value = "R5.10.1以後仕入消費税計算方式";
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption.VisibleFalseLock = false;
            // 
            // obic7TextNumberR5NenOctIgoSiireZeiTumiageKBN
            // 
            this.obic7TextNumberR5NenOctIgoSiireZeiTumiageKBN.AttachedButton = this.obic7ButtonWithLogicR5NenOctIgoSiireZeiTumiageKBNSearch;
            this.obic7TextNumberR5NenOctIgoSiireZeiTumiageKBN.AttachedCopyText = null;
            this.obic7TextNumberR5NenOctIgoSiireZeiTumiageKBN.AttachedLabel = null;
            this.obic7TextNumberR5NenOctIgoSiireZeiTumiageKBN.AttachedSearchDialog = null;
            this.obic7TextNumberR5NenOctIgoSiireZeiTumiageKBN.CheckOnClose = false;
            this.obic7TextNumberR5NenOctIgoSiireZeiTumiageKBN.ColumnNumber = 0;
            this.obic7TextNumberR5NenOctIgoSiireZeiTumiageKBN.EnableFalseLock = false;
            this.obic7TextNumberR5NenOctIgoSiireZeiTumiageKBN.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberR5NenOctIgoSiireZeiTumiageKBN.ErrorString = null;
            this.obic7TextNumberR5NenOctIgoSiireZeiTumiageKBN.ForeColor = System.Drawing.SystemColors.WindowText;
            this.obic7TextNumberR5NenOctIgoSiireZeiTumiageKBN.Format = "0";
            this.obic7TextNumberR5NenOctIgoSiireZeiTumiageKBN.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.obic7TextNumberR5NenOctIgoSiireZeiTumiageKBN.IsKeyColumn = false;
            this.obic7TextNumberR5NenOctIgoSiireZeiTumiageKBN.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberR5NenOctIgoSiireZeiTumiageKBN.Location = new System.Drawing.Point(222, 96);
            this.obic7TextNumberR5NenOctIgoSiireZeiTumiageKBN.MaxLengthByte = 1;
            this.obic7TextNumberR5NenOctIgoSiireZeiTumiageKBN.MaxValue = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.obic7TextNumberR5NenOctIgoSiireZeiTumiageKBN.MinusColor = System.Drawing.Color.Red;
            this.obic7TextNumberR5NenOctIgoSiireZeiTumiageKBN.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.obic7TextNumberR5NenOctIgoSiireZeiTumiageKBN.Name = "obic7TextNumberR5NenOctIgoSiireZeiTumiageKBN";
            this.obic7TextNumberR5NenOctIgoSiireZeiTumiageKBN.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextNumberR5NenOctIgoSiireZeiTumiageKBN.Size = new System.Drawing.Size(18, 19);
            this.obic7TextNumberR5NenOctIgoSiireZeiTumiageKBN.TabIndex = 2040;
            this.obic7TextNumberR5NenOctIgoSiireZeiTumiageKBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.obic7TextNumberR5NenOctIgoSiireZeiTumiageKBN.Value = null;
            this.obic7TextNumberR5NenOctIgoSiireZeiTumiageKBN.VisibleFalseLock = false;
            this.obic7TextNumberR5NenOctIgoSiireZeiTumiageKBN.Enter += new System.EventHandler(this.controlDetailSearch_Enter);
            this.obic7TextNumberR5NenOctIgoSiireZeiTumiageKBN.Validating += new System.ComponentModel.CancelEventHandler(this.obic7TextNumberCalcKBN_Validating);
            // 
            // obic7ButtonWithLogicR5NenOctIgoSiireZeiTumiageKBNSearch
            // 
            this.obic7ButtonWithLogicR5NenOctIgoSiireZeiTumiageKBNSearch.ColumnNumber = 0;
            this.obic7ButtonWithLogicR5NenOctIgoSiireZeiTumiageKBNSearch.EnableFalseLock = false;
            this.obic7ButtonWithLogicR5NenOctIgoSiireZeiTumiageKBNSearch.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.obic7ButtonWithLogicR5NenOctIgoSiireZeiTumiageKBNSearch.Location = new System.Drawing.Point(240, 94);
            this.obic7ButtonWithLogicR5NenOctIgoSiireZeiTumiageKBNSearch.Name = "obic7ButtonWithLogicR5NenOctIgoSiireZeiTumiageKBNSearch";
            this.obic7ButtonWithLogicR5NenOctIgoSiireZeiTumiageKBNSearch.Obic7JobSecurity = Obic7fx.Com.A0000.Tool.Obic7JobSecurities.NoSecurity;
            this.obic7ButtonWithLogicR5NenOctIgoSiireZeiTumiageKBNSearch.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7ButtonWithLogicR5NenOctIgoSiireZeiTumiageKBNSearch.Size = new System.Drawing.Size(24, 23);
            this.obic7ButtonWithLogicR5NenOctIgoSiireZeiTumiageKBNSearch.TabIndex = 2045;
            this.obic7ButtonWithLogicR5NenOctIgoSiireZeiTumiageKBNSearch.Text = "...";
            this.obic7ButtonWithLogicR5NenOctIgoSiireZeiTumiageKBNSearch.UseVisualStyleBackColor = true;
            this.obic7ButtonWithLogicR5NenOctIgoSiireZeiTumiageKBNSearch.VisibleFalseLock = false;
            this.obic7ButtonWithLogicR5NenOctIgoSiireZeiTumiageKBNSearch.Click += new System.EventHandler(this.obic7ButtonWithLogicCalcKBNSearch_Click);
            this.obic7ButtonWithLogicR5NenOctIgoSiireZeiTumiageKBNSearch.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName
            // 
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName.AutoEllipsis = true;
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName.BackColor = System.Drawing.SystemColors.Control;
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName.ColumnNumber = 0;
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName.EnableFalseLock = false;
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName.ErrorString = null;
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName.Location = new System.Drawing.Point(264, 96);
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName.Name = "obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName";
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName.Size = new System.Drawing.Size(282, 19);
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName.TabIndex = 1000000108;
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName.Value = "";
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName.VisibleFalseLock = false;
            // 
            // obic7LabelStringTorihikisakiCDCaption
            // 
            this.obic7LabelStringTorihikisakiCDCaption.AutoEllipsis = true;
            this.obic7LabelStringTorihikisakiCDCaption.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelStringTorihikisakiCDCaption.ColumnNumber = 0;
            this.obic7LabelStringTorihikisakiCDCaption.EnableFalseLock = false;
            this.obic7LabelStringTorihikisakiCDCaption.ErrorString = null;
            this.obic7LabelStringTorihikisakiCDCaption.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.obic7LabelStringTorihikisakiCDCaption.Location = new System.Drawing.Point(6, 24);
            this.obic7LabelStringTorihikisakiCDCaption.Name = "obic7LabelStringTorihikisakiCDCaption";
            this.obic7LabelStringTorihikisakiCDCaption.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringTorihikisakiCDCaption.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringTorihikisakiCDCaption.Size = new System.Drawing.Size(210, 19);
            this.obic7LabelStringTorihikisakiCDCaption.TabIndex = 1000000109;
            this.obic7LabelStringTorihikisakiCDCaption.Text = "取引先コード";
            this.obic7LabelStringTorihikisakiCDCaption.Value = "取引先コード";
            this.obic7LabelStringTorihikisakiCDCaption.VisibleFalseLock = false;
            // 
            // obic7TextStringTorihikisakiCD
            // 
            this.obic7TextStringTorihikisakiCD.AllowAlphabet = true;
            this.obic7TextStringTorihikisakiCD.AllowKana = false;
            this.obic7TextStringTorihikisakiCD.AllowNumber = true;
            this.obic7TextStringTorihikisakiCD.AllowSymbol = false;
            this.obic7TextStringTorihikisakiCD.AllowUnicode = false;
            this.obic7TextStringTorihikisakiCD.AllowZenkaku = false;
            this.obic7TextStringTorihikisakiCD.AttachedButton = this.obic7ButtonWithLogicTorihikisakiCDSearch;
            this.obic7TextStringTorihikisakiCD.AttachedLabel = this.obic7LabelStringTorihikisakiCDCaption;
            this.obic7TextStringTorihikisakiCD.AttachedSearchDialog = null;
            this.obic7TextStringTorihikisakiCD.CheckOnClose = false;
            this.obic7TextStringTorihikisakiCD.ColumnNumber = 0;
            this.obic7TextStringTorihikisakiCD.DispFuriganaText = null;
            this.obic7TextStringTorihikisakiCD.EnableFalseLock = false;
            this.obic7TextStringTorihikisakiCD.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringTorihikisakiCD.ErrorString = null;
            this.obic7TextStringTorihikisakiCD.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringTorihikisakiCD.IsKeyColumn = false;
            this.obic7TextStringTorihikisakiCD.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringTorihikisakiCD.Location = new System.Drawing.Point(222, 24);
            this.obic7TextStringTorihikisakiCD.MaxLengthByte = 10;
            this.obic7TextStringTorihikisakiCD.Name = "obic7TextStringTorihikisakiCD";
            this.obic7TextStringTorihikisakiCD.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringTorihikisakiCD.Size = new System.Drawing.Size(78, 19);
            this.obic7TextStringTorihikisakiCD.TabIndex = 2110;
            this.obic7TextStringTorihikisakiCD.Value = "";
            this.obic7TextStringTorihikisakiCD.VisibleFalseLock = false;
            this.obic7TextStringTorihikisakiCD.Enter += new System.EventHandler(this.controlDetailSearch_Enter);
            this.obic7TextStringTorihikisakiCD.Validating += new System.ComponentModel.CancelEventHandler(this.obic7TextStringTorihikisakiCD_Validating);
            // 
            // obic7ButtonWithLogicTorihikisakiCDSearch
            // 
            this.obic7ButtonWithLogicTorihikisakiCDSearch.ColumnNumber = 0;
            this.obic7ButtonWithLogicTorihikisakiCDSearch.EnableFalseLock = false;
            this.obic7ButtonWithLogicTorihikisakiCDSearch.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.obic7ButtonWithLogicTorihikisakiCDSearch.Location = new System.Drawing.Point(300, 22);
            this.obic7ButtonWithLogicTorihikisakiCDSearch.Name = "obic7ButtonWithLogicTorihikisakiCDSearch";
            this.obic7ButtonWithLogicTorihikisakiCDSearch.Obic7JobSecurity = Obic7fx.Com.A0000.Tool.Obic7JobSecurities.NoSecurity;
            this.obic7ButtonWithLogicTorihikisakiCDSearch.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7ButtonWithLogicTorihikisakiCDSearch.Size = new System.Drawing.Size(24, 23);
            this.obic7ButtonWithLogicTorihikisakiCDSearch.TabIndex = 2120;
            this.obic7ButtonWithLogicTorihikisakiCDSearch.Text = "...";
            this.obic7ButtonWithLogicTorihikisakiCDSearch.UseVisualStyleBackColor = true;
            this.obic7ButtonWithLogicTorihikisakiCDSearch.VisibleFalseLock = false;
            this.obic7ButtonWithLogicTorihikisakiCDSearch.Click += new System.EventHandler(this.obic7ButtonWithLogicTorihikisakiCDSearch_Click);
            this.obic7ButtonWithLogicTorihikisakiCDSearch.Enter += new System.EventHandler(this.controlDetail_Enter);
            // 
            // obic7LabelStringTorihikisakiName
            // 
            this.obic7LabelStringTorihikisakiName.AutoEllipsis = true;
            this.obic7LabelStringTorihikisakiName.BackColor = System.Drawing.SystemColors.Control;
            this.obic7LabelStringTorihikisakiName.ColumnNumber = 0;
            this.obic7LabelStringTorihikisakiName.EnableFalseLock = false;
            this.obic7LabelStringTorihikisakiName.ErrorString = null;
            this.obic7LabelStringTorihikisakiName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelStringTorihikisakiName.Location = new System.Drawing.Point(330, 24);
            this.obic7LabelStringTorihikisakiName.Name = "obic7LabelStringTorihikisakiName";
            this.obic7LabelStringTorihikisakiName.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7LabelStringTorihikisakiName.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringTorihikisakiName.Size = new System.Drawing.Size(300, 19);
            this.obic7LabelStringTorihikisakiName.TabIndex = 1000000110;
            this.obic7LabelStringTorihikisakiName.Value = "";
            this.obic7LabelStringTorihikisakiName.VisibleFalseLock = false;
            // 
            // obic7GroupBoxShouhizeiCalc
            // 
            this.obic7GroupBoxShouhizeiCalc.ColumnNumber = 0;
            this.obic7GroupBoxShouhizeiCalc.Controls.Add(this.obic7LabelStringUriageZeiTumiageKBNCaption);
            this.obic7GroupBoxShouhizeiCalc.Controls.Add(this.obic7TextNumberUriageZeiTumiageKBN);
            this.obic7GroupBoxShouhizeiCalc.Controls.Add(this.obic7ButtonWithLogicUriageZeiTumiageKBNSearch);
            this.obic7GroupBoxShouhizeiCalc.Controls.Add(this.obic7LabelStringUriageZeiTumiageKBNName);
            this.obic7GroupBoxShouhizeiCalc.Controls.Add(this.obic7LabelStringSiireZeiTumiageKBNCaption);
            this.obic7GroupBoxShouhizeiCalc.Controls.Add(this.obic7TextNumberSiireZeiTumiageKBN);
            this.obic7GroupBoxShouhizeiCalc.Controls.Add(this.obic7ButtonWithLogicSiireZeiTumiageKBNSearch);
            this.obic7GroupBoxShouhizeiCalc.Controls.Add(this.obic7LabelStringSiireZeiTumiageKBNName);
            this.obic7GroupBoxShouhizeiCalc.Controls.Add(this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption);
            this.obic7GroupBoxShouhizeiCalc.Controls.Add(this.obic7TextNumberR5NenOctIgoUriageZeiTumiageKBN);
            this.obic7GroupBoxShouhizeiCalc.Controls.Add(this.obic7ButtonWithLogicR5NenOctIgoUriageZeiTumiageKBNSearch);
            this.obic7GroupBoxShouhizeiCalc.Controls.Add(this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName);
            this.obic7GroupBoxShouhizeiCalc.Controls.Add(this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption);
            this.obic7GroupBoxShouhizeiCalc.Controls.Add(this.obic7TextNumberR5NenOctIgoSiireZeiTumiageKBN);
            this.obic7GroupBoxShouhizeiCalc.Controls.Add(this.obic7ButtonWithLogicR5NenOctIgoSiireZeiTumiageKBNSearch);
            this.obic7GroupBoxShouhizeiCalc.Controls.Add(this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName);
            this.obic7GroupBoxShouhizeiCalc.EnableFalseLock = false;
            this.obic7GroupBoxShouhizeiCalc.Location = new System.Drawing.Point(54, 27);
            this.obic7GroupBoxShouhizeiCalc.Name = "obic7GroupBoxShouhizeiCalc";
            this.obic7GroupBoxShouhizeiCalc.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7GroupBoxShouhizeiCalc.Size = new System.Drawing.Size(852, 124);
            this.obic7GroupBoxShouhizeiCalc.TabIndex = 2000;
            this.obic7GroupBoxShouhizeiCalc.TabStop = false;
            this.obic7GroupBoxShouhizeiCalc.Text = "消費税計算方式";
            this.obic7GroupBoxShouhizeiCalc.VisibleFalseLock = false;
            // 
            // obic7GroupBoxTorihikisaki
            // 
            this.obic7GroupBoxTorihikisaki.ColumnNumber = 0;
            this.obic7GroupBoxTorihikisaki.Controls.Add(this.obic7LabelStringTorihikisakiCDCaption);
            this.obic7GroupBoxTorihikisaki.Controls.Add(this.obic7TextStringTorihikisakiCD);
            this.obic7GroupBoxTorihikisaki.Controls.Add(this.obic7ButtonWithLogicTorihikisakiCDSearch);
            this.obic7GroupBoxTorihikisaki.Controls.Add(this.obic7LabelStringTorihikisakiName);
            this.obic7GroupBoxTorihikisaki.EnableFalseLock = false;
            this.obic7GroupBoxTorihikisaki.Location = new System.Drawing.Point(54, 160);
            this.obic7GroupBoxTorihikisaki.Name = "obic7GroupBoxTorihikisaki";
            this.obic7GroupBoxTorihikisaki.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7GroupBoxTorihikisaki.Size = new System.Drawing.Size(852, 52);
            this.obic7GroupBoxTorihikisaki.TabIndex = 2100;
            this.obic7GroupBoxTorihikisaki.TabStop = false;
            this.obic7GroupBoxTorihikisaki.Text = "取引先";
            this.obic7GroupBoxTorihikisaki.VisibleFalseLock = false;
            // 
            // obic7TextSeparatorDetail
            // 
            this.obic7TextSeparatorDetail.ColumnNumber = 0;
            this.obic7TextSeparatorDetail.IsRefrectLowerOrder = true;
            this.obic7TextSeparatorDetail.IsRefrectUpperOrder = false;
            this.obic7TextSeparatorDetail.IsTerminator = false;
            this.obic7TextSeparatorDetail.Location = new System.Drawing.Point(230, 7);
            this.obic7TextSeparatorDetail.Name = "obic7TextSeparatorDetail";
            this.obic7TextSeparatorDetail.Size = new System.Drawing.Size(0, 19);
            this.obic7TextSeparatorDetail.TabIndex = 1990;
            // 
            // MA_ZigyoushoSeigyoMasterMaintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1228, 666);
            this.Name = "MA_ZigyoushoSeigyoMasterMaintenance";
            this.Text = "事業所別制御マスタメンテ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MA_ZigyoushoSeigyoMasterMaintenance_FormClosing);
            this.Load += new System.EventHandler(this.MA_ZigyoushoSeigyoMasterMaintenance_Load);
            this.Shown += new System.EventHandler(this.MA_ZigyoushoSeigyoMasterMaintenance_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MA_ZigyoushoSeigyoMasterMaintenance_KeyDown);
            this.obic7PanelHeader.ResumeLayout(false);
            this.obic7PanelHeader.PerformLayout();
            this.obic7PanelDetail.ResumeLayout(false);
            this.obic7PanelDetail.PerformLayout();
            this.obic7GroupBoxShouhizeiCalc.ResumeLayout(false);
            this.obic7GroupBoxShouhizeiCalc.PerformLayout();
            this.obic7GroupBoxTorihikisaki.ResumeLayout(false);
            this.obic7GroupBoxTorihikisaki.PerformLayout();
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
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringUriageZeiTumiageKBNCaption;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber obic7TextNumberUriageZeiTumiageKBN;
        private Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic obic7ButtonWithLogicUriageZeiTumiageKBNSearch;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringUriageZeiTumiageKBNName;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringSiireZeiTumiageKBNCaption;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber obic7TextNumberSiireZeiTumiageKBN;
        private Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic obic7ButtonWithLogicSiireZeiTumiageKBNSearch;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringSiireZeiTumiageKBNName;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber obic7TextNumberR5NenOctIgoUriageZeiTumiageKBN;
        private Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic obic7ButtonWithLogicR5NenOctIgoUriageZeiTumiageKBNSearch;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber obic7TextNumberR5NenOctIgoSiireZeiTumiageKBN;
        private Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic obic7ButtonWithLogicR5NenOctIgoSiireZeiTumiageKBNSearch;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringTorihikisakiCDCaption;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringTorihikisakiCD;
        private Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic obic7ButtonWithLogicTorihikisakiCDSearch;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringTorihikisakiName;
        private Obic7fx.Win.A0000.Tool.UI.Obic7GroupBox obic7GroupBoxShouhizeiCalc;
        private Obic7fx.Win.A0000.Tool.UI.Obic7GroupBox obic7GroupBoxTorihikisaki;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextSeparator obic7TextSeparatorDetail;
    }
}
