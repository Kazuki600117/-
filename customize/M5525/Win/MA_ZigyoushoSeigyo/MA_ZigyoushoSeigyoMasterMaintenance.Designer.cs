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
            this.obic7TextStringUriageZeiTumiageKBN = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7LabelStringUriageZeiTumiageKBNName = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelStringSiireZeiTumiageKBNCaption = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringSiireZeiTumiageKBN = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7LabelStringSiireZeiTumiageKBNName = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelStringTorihikisakiCDCaption = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringTorihikisakiCD = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7ButtonWithLogicTorihikisakiCDSearch = new Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic();
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
            this.obic7LabelStringUriageZeiTumiageKBNCaption.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelStringUriageZeiTumiageKBNCaption.ColumnNumber = 0;
            this.obic7LabelStringUriageZeiTumiageKBNCaption.EnableFalseLock = false;
            this.obic7LabelStringUriageZeiTumiageKBNCaption.ErrorString = null;
            this.obic7LabelStringUriageZeiTumiageKBNCaption.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.obic7LabelStringUriageZeiTumiageKBNCaption.Location = new System.Drawing.Point(6, 24);
            this.obic7LabelStringUriageZeiTumiageKBNCaption.Name = "obic7LabelStringUriageZeiTumiageKBNCaption";
            this.obic7LabelStringUriageZeiTumiageKBNCaption.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringUriageZeiTumiageKBNCaption.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringUriageZeiTumiageKBNCaption.Size = new System.Drawing.Size(210, 19);
            this.obic7LabelStringUriageZeiTumiageKBNCaption.TabIndex = 1000000101;
            this.obic7LabelStringUriageZeiTumiageKBNCaption.Text = "R5.9.30以前売上消費税計算方式";
            this.obic7LabelStringUriageZeiTumiageKBNCaption.Value = "R5.9.30以前売上消費税計算方式";
            this.obic7LabelStringUriageZeiTumiageKBNCaption.VisibleFalseLock = false;
            // 
            // obic7TextStringUriageZeiTumiageKBN
            // 
            this.obic7TextStringUriageZeiTumiageKBN.AllowAlphabet = false;
            this.obic7TextStringUriageZeiTumiageKBN.AllowKana = false;
            this.obic7TextStringUriageZeiTumiageKBN.AllowNumber = true;
            this.obic7TextStringUriageZeiTumiageKBN.AllowSymbol = false;
            this.obic7TextStringUriageZeiTumiageKBN.AllowUnicode = false;
            this.obic7TextStringUriageZeiTumiageKBN.AllowZenkaku = false;
            this.obic7TextStringUriageZeiTumiageKBN.AttachedButton = null;
            this.obic7TextStringUriageZeiTumiageKBN.AttachedLabel = this.obic7LabelStringUriageZeiTumiageKBNCaption;
            this.obic7TextStringUriageZeiTumiageKBN.AttachedSearchDialog = null;
            this.obic7TextStringUriageZeiTumiageKBN.CheckOnClose = false;
            this.obic7TextStringUriageZeiTumiageKBN.ColumnNumber = 0;
            this.obic7TextStringUriageZeiTumiageKBN.DispFuriganaText = null;
            this.obic7TextStringUriageZeiTumiageKBN.EnableFalseLock = false;
            this.obic7TextStringUriageZeiTumiageKBN.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringUriageZeiTumiageKBN.ErrorString = null;
            this.obic7TextStringUriageZeiTumiageKBN.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringUriageZeiTumiageKBN.IsKeyColumn = false;
            this.obic7TextStringUriageZeiTumiageKBN.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringUriageZeiTumiageKBN.Location = new System.Drawing.Point(222, 24);
            this.obic7TextStringUriageZeiTumiageKBN.MaxLengthByte = 1;
            this.obic7TextStringUriageZeiTumiageKBN.Name = "obic7TextStringUriageZeiTumiageKBN";
            this.obic7TextStringUriageZeiTumiageKBN.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringUriageZeiTumiageKBN.Size = new System.Drawing.Size(18, 19);
            this.obic7TextStringUriageZeiTumiageKBN.TabIndex = 2010;
            this.obic7TextStringUriageZeiTumiageKBN.Value = "";
            this.obic7TextStringUriageZeiTumiageKBN.VisibleFalseLock = false;
            this.obic7TextStringUriageZeiTumiageKBN.Enter += new System.EventHandler(this.controlDetail_Enter);
            this.obic7TextStringUriageZeiTumiageKBN.Validating += new System.ComponentModel.CancelEventHandler(this.obic7TextStringCalcKBN_Validating);
            // 
            // obic7LabelStringUriageZeiTumiageKBNName
            // 
            this.obic7LabelStringUriageZeiTumiageKBNName.AutoEllipsis = true;
            this.obic7LabelStringUriageZeiTumiageKBNName.BackColor = System.Drawing.SystemColors.Control;
            this.obic7LabelStringUriageZeiTumiageKBNName.ColumnNumber = 0;
            this.obic7LabelStringUriageZeiTumiageKBNName.EnableFalseLock = false;
            this.obic7LabelStringUriageZeiTumiageKBNName.ErrorString = null;
            this.obic7LabelStringUriageZeiTumiageKBNName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelStringUriageZeiTumiageKBNName.Location = new System.Drawing.Point(246, 24);
            this.obic7LabelStringUriageZeiTumiageKBNName.Name = "obic7LabelStringUriageZeiTumiageKBNName";
            this.obic7LabelStringUriageZeiTumiageKBNName.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7LabelStringUriageZeiTumiageKBNName.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringUriageZeiTumiageKBNName.Size = new System.Drawing.Size(300, 19);
            this.obic7LabelStringUriageZeiTumiageKBNName.TabIndex = 1000000102;
            this.obic7LabelStringUriageZeiTumiageKBNName.Value = "";
            this.obic7LabelStringUriageZeiTumiageKBNName.VisibleFalseLock = false;
            // 
            // obic7LabelStringSiireZeiTumiageKBNCaption
            // 
            this.obic7LabelStringSiireZeiTumiageKBNCaption.AutoEllipsis = true;
            this.obic7LabelStringSiireZeiTumiageKBNCaption.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelStringSiireZeiTumiageKBNCaption.ColumnNumber = 0;
            this.obic7LabelStringSiireZeiTumiageKBNCaption.EnableFalseLock = false;
            this.obic7LabelStringSiireZeiTumiageKBNCaption.ErrorString = null;
            this.obic7LabelStringSiireZeiTumiageKBNCaption.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.obic7LabelStringSiireZeiTumiageKBNCaption.Location = new System.Drawing.Point(6, 48);
            this.obic7LabelStringSiireZeiTumiageKBNCaption.Name = "obic7LabelStringSiireZeiTumiageKBNCaption";
            this.obic7LabelStringSiireZeiTumiageKBNCaption.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringSiireZeiTumiageKBNCaption.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringSiireZeiTumiageKBNCaption.Size = new System.Drawing.Size(210, 19);
            this.obic7LabelStringSiireZeiTumiageKBNCaption.TabIndex = 1000000103;
            this.obic7LabelStringSiireZeiTumiageKBNCaption.Text = "R5.9.30以前仕入消費税計算方式";
            this.obic7LabelStringSiireZeiTumiageKBNCaption.Value = "R5.9.30以前仕入消費税計算方式";
            this.obic7LabelStringSiireZeiTumiageKBNCaption.VisibleFalseLock = false;
            // 
            // obic7TextStringSiireZeiTumiageKBN
            // 
            this.obic7TextStringSiireZeiTumiageKBN.AllowAlphabet = false;
            this.obic7TextStringSiireZeiTumiageKBN.AllowKana = false;
            this.obic7TextStringSiireZeiTumiageKBN.AllowNumber = true;
            this.obic7TextStringSiireZeiTumiageKBN.AllowSymbol = false;
            this.obic7TextStringSiireZeiTumiageKBN.AllowUnicode = false;
            this.obic7TextStringSiireZeiTumiageKBN.AllowZenkaku = false;
            this.obic7TextStringSiireZeiTumiageKBN.AttachedButton = null;
            this.obic7TextStringSiireZeiTumiageKBN.AttachedLabel = this.obic7LabelStringSiireZeiTumiageKBNCaption;
            this.obic7TextStringSiireZeiTumiageKBN.AttachedSearchDialog = null;
            this.obic7TextStringSiireZeiTumiageKBN.CheckOnClose = false;
            this.obic7TextStringSiireZeiTumiageKBN.ColumnNumber = 0;
            this.obic7TextStringSiireZeiTumiageKBN.DispFuriganaText = null;
            this.obic7TextStringSiireZeiTumiageKBN.EnableFalseLock = false;
            this.obic7TextStringSiireZeiTumiageKBN.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringSiireZeiTumiageKBN.ErrorString = null;
            this.obic7TextStringSiireZeiTumiageKBN.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringSiireZeiTumiageKBN.IsKeyColumn = false;
            this.obic7TextStringSiireZeiTumiageKBN.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringSiireZeiTumiageKBN.Location = new System.Drawing.Point(222, 48);
            this.obic7TextStringSiireZeiTumiageKBN.MaxLengthByte = 1;
            this.obic7TextStringSiireZeiTumiageKBN.Name = "obic7TextStringSiireZeiTumiageKBN";
            this.obic7TextStringSiireZeiTumiageKBN.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringSiireZeiTumiageKBN.Size = new System.Drawing.Size(18, 19);
            this.obic7TextStringSiireZeiTumiageKBN.TabIndex = 2020;
            this.obic7TextStringSiireZeiTumiageKBN.Value = "";
            this.obic7TextStringSiireZeiTumiageKBN.VisibleFalseLock = false;
            this.obic7TextStringSiireZeiTumiageKBN.Enter += new System.EventHandler(this.controlDetail_Enter);
            this.obic7TextStringSiireZeiTumiageKBN.Validating += new System.ComponentModel.CancelEventHandler(this.obic7TextStringCalcKBN_Validating);
            // 
            // obic7LabelStringSiireZeiTumiageKBNName
            // 
            this.obic7LabelStringSiireZeiTumiageKBNName.AutoEllipsis = true;
            this.obic7LabelStringSiireZeiTumiageKBNName.BackColor = System.Drawing.SystemColors.Control;
            this.obic7LabelStringSiireZeiTumiageKBNName.ColumnNumber = 0;
            this.obic7LabelStringSiireZeiTumiageKBNName.EnableFalseLock = false;
            this.obic7LabelStringSiireZeiTumiageKBNName.ErrorString = null;
            this.obic7LabelStringSiireZeiTumiageKBNName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelStringSiireZeiTumiageKBNName.Location = new System.Drawing.Point(246, 48);
            this.obic7LabelStringSiireZeiTumiageKBNName.Name = "obic7LabelStringSiireZeiTumiageKBNName";
            this.obic7LabelStringSiireZeiTumiageKBNName.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7LabelStringSiireZeiTumiageKBNName.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringSiireZeiTumiageKBNName.Size = new System.Drawing.Size(300, 19);
            this.obic7LabelStringSiireZeiTumiageKBNName.TabIndex = 1000000104;
            this.obic7LabelStringSiireZeiTumiageKBNName.Value = "";
            this.obic7LabelStringSiireZeiTumiageKBNName.VisibleFalseLock = false;
            // 
            // obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption
            // 
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption.AutoEllipsis = true;
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption.ColumnNumber = 0;
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption.EnableFalseLock = false;
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption.ErrorString = null;
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption.Location = new System.Drawing.Point(6, 72);
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption.Name = "obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption";
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption.Size = new System.Drawing.Size(210, 19);
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption.TabIndex = 1000000105;
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption.Text = "R5.10.1以後売上消費税計算方式";
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption.Value = "R5.10.1以後売上消費税計算方式";
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption.VisibleFalseLock = false;
            // 
            // obic7TextStringR5NenOctIgoUriageZeiTumiageKBN
            // 
            this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN.AllowAlphabet = false;
            this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN.AllowKana = false;
            this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN.AllowNumber = true;
            this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN.AllowSymbol = false;
            this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN.AllowUnicode = false;
            this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN.AllowZenkaku = false;
            this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN.AttachedButton = null;
            this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN.AttachedLabel = this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption;
            this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN.AttachedSearchDialog = null;
            this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN.CheckOnClose = false;
            this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN.ColumnNumber = 0;
            this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN.DispFuriganaText = null;
            this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN.EnableFalseLock = false;
            this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN.ErrorString = null;
            this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN.IsKeyColumn = false;
            this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN.Location = new System.Drawing.Point(222, 72);
            this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN.MaxLengthByte = 1;
            this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN.Name = "obic7TextStringR5NenOctIgoUriageZeiTumiageKBN";
            this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN.Size = new System.Drawing.Size(18, 19);
            this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN.TabIndex = 2030;
            this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN.Value = "";
            this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN.VisibleFalseLock = false;
            this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN.Enter += new System.EventHandler(this.controlDetail_Enter);
            this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN.Validating += new System.ComponentModel.CancelEventHandler(this.obic7TextStringCalcKBN_Validating);
            // 
            // obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName
            // 
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName.AutoEllipsis = true;
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName.BackColor = System.Drawing.SystemColors.Control;
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName.ColumnNumber = 0;
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName.EnableFalseLock = false;
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName.ErrorString = null;
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName.Location = new System.Drawing.Point(246, 72);
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName.Name = "obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName";
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName.Size = new System.Drawing.Size(300, 19);
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName.TabIndex = 1000000106;
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName.Value = "";
            this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName.VisibleFalseLock = false;
            // 
            // obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption
            // 
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption.AutoEllipsis = true;
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption.ColumnNumber = 0;
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption.EnableFalseLock = false;
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption.ErrorString = null;
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption.Location = new System.Drawing.Point(6, 96);
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption.Name = "obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption";
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption.Size = new System.Drawing.Size(210, 19);
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption.TabIndex = 1000000107;
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption.Text = "R5.10.1以後仕入消費税計算方式";
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption.Value = "R5.10.1以後仕入消費税計算方式";
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption.VisibleFalseLock = false;
            // 
            // obic7TextStringR5NenOctIgoSiireZeiTumiageKBN
            // 
            this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN.AllowAlphabet = false;
            this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN.AllowKana = false;
            this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN.AllowNumber = true;
            this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN.AllowSymbol = false;
            this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN.AllowUnicode = false;
            this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN.AllowZenkaku = false;
            this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN.AttachedButton = null;
            this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN.AttachedLabel = this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption;
            this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN.AttachedSearchDialog = null;
            this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN.CheckOnClose = false;
            this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN.ColumnNumber = 0;
            this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN.DispFuriganaText = null;
            this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN.EnableFalseLock = false;
            this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN.ErrorString = null;
            this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN.IsKeyColumn = false;
            this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN.Location = new System.Drawing.Point(222, 96);
            this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN.MaxLengthByte = 1;
            this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN.Name = "obic7TextStringR5NenOctIgoSiireZeiTumiageKBN";
            this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN.Size = new System.Drawing.Size(18, 19);
            this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN.TabIndex = 2040;
            this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN.Value = "";
            this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN.VisibleFalseLock = false;
            this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN.Enter += new System.EventHandler(this.controlDetail_Enter);
            this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN.Validating += new System.ComponentModel.CancelEventHandler(this.obic7TextStringCalcKBN_Validating);
            // 
            // obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName
            // 
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName.AutoEllipsis = true;
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName.BackColor = System.Drawing.SystemColors.Control;
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName.ColumnNumber = 0;
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName.EnableFalseLock = false;
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName.ErrorString = null;
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName.Location = new System.Drawing.Point(246, 96);
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName.Name = "obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName";
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName.Size = new System.Drawing.Size(300, 19);
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
            // obic7GroupBoxShouhizeiCalc
            // 
            this.obic7GroupBoxShouhizeiCalc.ColumnNumber = 0;
            this.obic7GroupBoxShouhizeiCalc.Controls.Add(this.obic7LabelStringUriageZeiTumiageKBNCaption);
            this.obic7GroupBoxShouhizeiCalc.Controls.Add(this.obic7TextStringUriageZeiTumiageKBN);
            this.obic7GroupBoxShouhizeiCalc.Controls.Add(this.obic7LabelStringUriageZeiTumiageKBNName);
            this.obic7GroupBoxShouhizeiCalc.Controls.Add(this.obic7LabelStringSiireZeiTumiageKBNCaption);
            this.obic7GroupBoxShouhizeiCalc.Controls.Add(this.obic7TextStringSiireZeiTumiageKBN);
            this.obic7GroupBoxShouhizeiCalc.Controls.Add(this.obic7LabelStringSiireZeiTumiageKBNName);
            this.obic7GroupBoxShouhizeiCalc.Controls.Add(this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption);
            this.obic7GroupBoxShouhizeiCalc.Controls.Add(this.obic7TextStringR5NenOctIgoUriageZeiTumiageKBN);
            this.obic7GroupBoxShouhizeiCalc.Controls.Add(this.obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName);
            this.obic7GroupBoxShouhizeiCalc.Controls.Add(this.obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption);
            this.obic7GroupBoxShouhizeiCalc.Controls.Add(this.obic7TextStringR5NenOctIgoSiireZeiTumiageKBN);
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
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringUriageZeiTumiageKBN;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringUriageZeiTumiageKBNName;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringSiireZeiTumiageKBNCaption;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringSiireZeiTumiageKBN;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringSiireZeiTumiageKBNName;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNCaption;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringR5NenOctIgoUriageZeiTumiageKBN;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringR5NenOctIgoUriageZeiTumiageKBNName;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNCaption;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringR5NenOctIgoSiireZeiTumiageKBN;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringR5NenOctIgoSiireZeiTumiageKBNName;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringTorihikisakiCDCaption;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringTorihikisakiCD;
        private Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic obic7ButtonWithLogicTorihikisakiCDSearch;
        private Obic7fx.Win.A0000.Tool.UI.Obic7GroupBox obic7GroupBoxShouhizeiCalc;
        private Obic7fx.Win.A0000.Tool.UI.Obic7GroupBox obic7GroupBoxTorihikisaki;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextSeparator obic7TextSeparatorDetail;
    }
}
