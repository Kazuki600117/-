namespace Obic7fx.Win.M5525.Input.MA_HonsitenSiwake
{
    /// <summary>
    /// 本支店仕訳入力
    /// </summary>
    partial class MA_HonsitenSiwake
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
            //ステートフルをDispose()
            if (disposing && (businessDB != null))
            {
                businessDB.Dispose();
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
            this.obic7ComboBoxMode = new Obic7fx.Win.A0000.Tool.UI.Obic7ComboBox();
            this.obic7TextDatetimeHasseiDate = new Obic7fx.Win.A0000.Tool.UI.Obic7TextDatetime();
            this.obic7LabelStringHasseiDate = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringZigyoushoCD = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7ButtonWithLogicSearchZigyoushoCD = new Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic();
            this.obic7LabelStringZigyousho = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringDenpyouTekiyou = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7ButtonWithLogicSearchDenpyouTekiyou = new Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic();
            this.obic7LabelStringDenpyouTekiyou = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelStringZigyoushoName = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelStringDenpyouKBN = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelStringDenpyouNO = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringDenpyouNO = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringDenpyouKBN = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7ButtonWithLogicSearchDenpyouKBN = new Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic();
            this.obic7LabelStringDenpyouKBNName = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelStringPrevDenpyouNO = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelStringCalledDenpyouNO = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelStringSokudenhakkou = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringCalledDenpyouNO = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7ButtonWithLogicSearchCalledDenpyouNO = new Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic();
            this.obic7TextNumberSokudenHakkou = new Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber();
            this.obic7ButtonWithLogicSearchSokudenHakkou = new Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic();
            this.obic7ButtonWithLogicDisplay = new Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic();
            this.obic7LabelStringKariKanjou = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelStringSokudenHakkouName = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelString3 = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelString4 = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelString14 = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelString15 = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelString20 = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelString22 = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelString23 = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelString24 = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelString25 = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7PanelLine = new Obic7fx.Win.A0000.Tool.UI.Obic7Panel();
            this.obic7TextStringKariTorihikisakiCD = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKariUtiwakeCD = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKariHojoCD = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKariKanjouCD = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKasiMemo = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextNumberKasiMemoBValue = new Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber();
            this.obic7TextNumberKasiMemoGValue = new Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber();
            this.obic7TextNumberKasiMemoRValue = new Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber();
            this.obic7TextStringKariMemo = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextNumberKariMemoBValue = new Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber();
            this.obic7TextNumberKariMemoGValue = new Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber();
            this.obic7TextNumberKariMemoRValue = new Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber();
            this.obic7ButtonWithLogicKasiMemo = new Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic();
            this.obic7ButtonWithLogicKariMemo = new Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic();
            this.obic7LabelNumberGyou = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelNumber();
            this.obic7TextStringKariProjectCD = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKasiProjectCD = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextSeparatorKasiUtiwake = new Obic7fx.Win.A0000.Tool.UI.Obic7TextSeparator();
            this.obic7TextSeparatorKasiHojo = new Obic7fx.Win.A0000.Tool.UI.Obic7TextSeparator();
            this.obic7TextSeparatorKariUtiwake = new Obic7fx.Win.A0000.Tool.UI.Obic7TextSeparator();
            this.obic7TextSeparatorKariHojo = new Obic7fx.Win.A0000.Tool.UI.Obic7TextSeparator();
            this.obic7TextStringKariSikinCD = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKariBunsekiCD3 = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKariBunsekiCD2 = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKariBunsekiCD1 = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKariTorihikisakiShanaiName = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextNumberKariZeigaku = new Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber();
            this.obic7TextStringKariZeiHontaiNameRyaku = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKariZeiHontaiCD = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKariUtiwakeNameRyaku = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextNumberKariKingaku = new Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber();
            this.obic7TextStringKariBumonNameRyaku = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKariBumonCD = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKariHojoNameRyaku = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKariZeikomiKBNName = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextNumberKariZeikomiKBN = new Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber();
            this.obic7TextStringKariZeiKBN = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKariZigyoushoNameRyaku = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKariZigyoushoCD = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKariKanjouNameRyaku = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKasiSikinCD = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKasiBunsekiCD3 = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKasiBunsekiCD2 = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKasiBunsekiCD1 = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKasiTorihikisakiShanaiName = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKasiTorihikisakiCD = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextNumberKasiZeigaku = new Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber();
            this.obic7TextStringKasiZeiHontaiNameRyaku = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKasiZeiHontaiCD = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKasiUtiwakeNameRyaku = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKasiUtiwakeCD = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextNumberKasiKingaku = new Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber();
            this.obic7TextStringKasiBumonNameRyaku = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKasiBumonCD = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKasiHojoNameRyaku = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKasiHojoCD = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKasiZeikomiKBNName = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextNumberKasiZeikomiKBN = new Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber();
            this.obic7TextStringKasiZeiKBN = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKasiZigyoushoNameRyaku = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKasiZigyoushoCD = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKasiKanjouNameRyaku = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKasiKanjouCD = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKasiBunsekiCD4 = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKariBunsekiCD4 = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKasiBunsekiCD5 = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringKariBunsekiCD5 = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextNumberGyouNO = new Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber();
            this.obic7TextStringMeisaiTekiyou = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringDummy = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextStringDummy2 = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextNumberTotalKasiSougaku = new Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber();
            this.obic7TextNumberTotalKariSougaku = new Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber();
            this.obic7TextNumberTotalKasiKingaku = new Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber();
            this.obic7TextNumberTotalKariKingaku = new Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber();
            this.obic7TextNumberTotalKariZeigaku = new Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber();
            this.obic7TextNumberTotalKasiZeigaku = new Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber();
            this.obic7LabelString1 = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelString2 = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelString5 = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelString6 = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelString7 = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelString8 = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelString9 = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelString10 = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelString11 = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelString12 = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelString13 = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelString16 = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelString17 = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7TextStringPrevDenpyouNO = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextSeparatorDenpyouNo = new Obic7fx.Win.A0000.Tool.UI.Obic7TextSeparator();
            this.toolTip1 = new Obic7fx.Win.A0000.Tool.UI.Obic7ToolTip();
            this.obic7TextStringHeadDummy = new Obic7fx.Win.A0000.Tool.UI.Obic7TextString();
            this.obic7TextNumberKijunKBN = new Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber();
            this.obic7ButtonWithLogicSearchKijunKBN = new Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic();
            this.obic7LabelStringKijunKBN = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelStringKijunKBNName = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelString21 = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelString18 = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelString19 = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelStringKasiMemo = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7LabelStringKariMemo = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7ButtonWithLogicDensiShouhyou = new Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic();
            this.obic7LabelStringDensiShouhyou = new Obic7fx.Win.A0000.Tool.UI.Obic7LabelString();
            this.obic7PanelHeader.SuspendLayout();
            this.obic7PanelDetail.SuspendLayout();
            this.obic7PanelLine.SuspendLayout();
            this.SuspendLayout();
            // 
            // obic7PanelHeader
            // 
            this.obic7PanelHeader.Controls.Add(this.obic7LabelStringDensiShouhyou);
            this.obic7PanelHeader.Controls.Add(this.obic7ButtonWithLogicDensiShouhyou);
            this.obic7PanelHeader.Controls.Add(this.obic7TextNumberKijunKBN);
            this.obic7PanelHeader.Controls.Add(this.obic7ButtonWithLogicSearchKijunKBN);
            this.obic7PanelHeader.Controls.Add(this.obic7LabelStringKijunKBN);
            this.obic7PanelHeader.Controls.Add(this.obic7LabelStringKijunKBNName);
            this.obic7PanelHeader.Controls.Add(this.obic7TextSeparatorDenpyouNo);
            this.obic7PanelHeader.Controls.Add(this.obic7TextStringPrevDenpyouNO);
            this.obic7PanelHeader.Controls.Add(this.obic7TextStringZigyoushoCD);
            this.obic7PanelHeader.Controls.Add(this.obic7LabelStringSokudenHakkouName);
            this.obic7PanelHeader.Controls.Add(this.obic7ButtonWithLogicSearchDenpyouTekiyou);
            this.obic7PanelHeader.Controls.Add(this.obic7ButtonWithLogicDisplay);
            this.obic7PanelHeader.Controls.Add(this.obic7ButtonWithLogicSearchCalledDenpyouNO);
            this.obic7PanelHeader.Controls.Add(this.obic7ButtonWithLogicSearchSokudenHakkou);
            this.obic7PanelHeader.Controls.Add(this.obic7TextNumberSokudenHakkou);
            this.obic7PanelHeader.Controls.Add(this.obic7LabelStringSokudenhakkou);
            this.obic7PanelHeader.Controls.Add(this.obic7LabelStringCalledDenpyouNO);
            this.obic7PanelHeader.Controls.Add(this.obic7LabelStringPrevDenpyouNO);
            this.obic7PanelHeader.Controls.Add(this.obic7LabelStringDenpyouKBNName);
            this.obic7PanelHeader.Controls.Add(this.obic7ButtonWithLogicSearchDenpyouKBN);
            this.obic7PanelHeader.Controls.Add(this.obic7TextStringDenpyouKBN);
            this.obic7PanelHeader.Controls.Add(this.obic7TextStringDenpyouNO);
            this.obic7PanelHeader.Controls.Add(this.obic7LabelStringDenpyouNO);
            this.obic7PanelHeader.Controls.Add(this.obic7LabelStringDenpyouKBN);
            this.obic7PanelHeader.Controls.Add(this.obic7LabelStringZigyoushoName);
            this.obic7PanelHeader.Controls.Add(this.obic7ButtonWithLogicSearchZigyoushoCD);
            this.obic7PanelHeader.Controls.Add(this.obic7LabelStringDenpyouTekiyou);
            this.obic7PanelHeader.Controls.Add(this.obic7TextStringDenpyouTekiyou);
            this.obic7PanelHeader.Controls.Add(this.obic7TextDatetimeHasseiDate);
            this.obic7PanelHeader.Controls.Add(this.obic7LabelStringHasseiDate);
            this.obic7PanelHeader.Controls.Add(this.obic7LabelStringZigyousho);
            this.obic7PanelHeader.Controls.Add(this.obic7ComboBoxMode);
            this.obic7PanelHeader.Controls.Add(this.obic7TextStringCalledDenpyouNO);
            this.obic7PanelHeader.Controls.Add(this.obic7TextStringHeadDummy);
            this.obic7PanelHeader.Size = new System.Drawing.Size(1228, 75);
            this.obic7PanelHeader.TabIndex = 1000;
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7TextStringHeadDummy, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7TextStringCalledDenpyouNO, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7ComboBoxMode, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7LabelStringZigyousho, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7LabelStringHasseiDate, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7TextDatetimeHasseiDate, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7TextStringDenpyouTekiyou, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7LabelStringDenpyouTekiyou, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7ButtonWithLogicSearchZigyoushoCD, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7LabelStringZigyoushoName, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7LabelStringDenpyouKBN, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7LabelStringDenpyouNO, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7TextStringDenpyouNO, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7TextStringDenpyouKBN, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7ButtonWithLogicSearchDenpyouKBN, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7LabelStringDenpyouKBNName, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7LabelStringPrevDenpyouNO, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7LabelStringCalledDenpyouNO, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7LabelStringSokudenhakkou, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7TextNumberSokudenHakkou, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7ButtonWithLogicSearchSokudenHakkou, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7ButtonWithLogicSearchCalledDenpyouNO, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7ButtonWithLogicDisplay, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7ButtonWithLogicSearchDenpyouTekiyou, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7LabelStringSokudenHakkouName, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7TextStringZigyoushoCD, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7TextStringPrevDenpyouNO, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7TextSeparatorDenpyouNo, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7LabelStringKijunKBNName, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7LabelStringKijunKBN, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7ButtonWithLogicSearchKijunKBN, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7TextNumberKijunKBN, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7ButtonWithLogicDensiShouhyou, 0);
            this.obic7PanelHeader.Controls.SetChildIndex(this.obic7LabelStringDensiShouhyou, 0);
            // 
            // obic7PanelDetail
            // 
            this.obic7PanelDetail.Controls.Add(this.obic7LabelStringKariMemo);
            this.obic7PanelDetail.Controls.Add(this.obic7LabelStringKasiMemo);
            this.obic7PanelDetail.Controls.Add(this.obic7LabelString19);
            this.obic7PanelDetail.Controls.Add(this.obic7LabelString18);
            this.obic7PanelDetail.Controls.Add(this.obic7LabelString21);
            this.obic7PanelDetail.Controls.Add(this.obic7TextNumberTotalKasiSougaku);
            this.obic7PanelDetail.Controls.Add(this.obic7TextNumberTotalKasiZeigaku);
            this.obic7PanelDetail.Controls.Add(this.obic7TextNumberTotalKasiKingaku);
            this.obic7PanelDetail.Controls.Add(this.obic7TextNumberTotalKariSougaku);
            this.obic7PanelDetail.Controls.Add(this.obic7TextNumberTotalKariZeigaku);
            this.obic7PanelDetail.Controls.Add(this.obic7TextNumberTotalKariKingaku);
            this.obic7PanelDetail.Controls.Add(this.obic7LabelString17);
            this.obic7PanelDetail.Controls.Add(this.obic7LabelString16);
            this.obic7PanelDetail.Controls.Add(this.obic7LabelString13);
            this.obic7PanelDetail.Controls.Add(this.obic7LabelString1);
            this.obic7PanelDetail.Controls.Add(this.obic7LabelString2);
            this.obic7PanelDetail.Controls.Add(this.obic7LabelString5);
            this.obic7PanelDetail.Controls.Add(this.obic7LabelString6);
            this.obic7PanelDetail.Controls.Add(this.obic7LabelString7);
            this.obic7PanelDetail.Controls.Add(this.obic7LabelString8);
            this.obic7PanelDetail.Controls.Add(this.obic7LabelString9);
            this.obic7PanelDetail.Controls.Add(this.obic7LabelString10);
            this.obic7PanelDetail.Controls.Add(this.obic7LabelString11);
            this.obic7PanelDetail.Controls.Add(this.obic7LabelString12);
            this.obic7PanelDetail.Controls.Add(this.obic7PanelLine);
            this.obic7PanelDetail.Controls.Add(this.obic7LabelString25);
            this.obic7PanelDetail.Controls.Add(this.obic7LabelString24);
            this.obic7PanelDetail.Controls.Add(this.obic7LabelString23);
            this.obic7PanelDetail.Controls.Add(this.obic7LabelString22);
            this.obic7PanelDetail.Controls.Add(this.obic7LabelString20);
            this.obic7PanelDetail.Controls.Add(this.obic7LabelString15);
            this.obic7PanelDetail.Controls.Add(this.obic7LabelString4);
            this.obic7PanelDetail.Controls.Add(this.obic7LabelString14);
            this.obic7PanelDetail.Controls.Add(this.obic7LabelString3);
            this.obic7PanelDetail.Controls.Add(this.obic7LabelStringKariKanjou);
            this.obic7PanelDetail.Location = new System.Drawing.Point(0, 75);
            this.obic7PanelDetail.Size = new System.Drawing.Size(1228, 544);
            this.obic7PanelDetail.TabIndex = 3000;
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7LabelStringKariKanjou, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7LabelString3, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7LabelString14, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7LabelString4, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7LabelString15, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7LabelString20, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7LabelString22, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7LabelString23, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7LabelString24, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7LabelString25, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7PanelLine, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7LabelString12, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7LabelString11, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7LabelString10, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7LabelString9, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7LabelString8, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7LabelString7, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7LabelString6, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7LabelString5, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7LabelString2, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7LabelString1, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7LabelString13, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7LabelString16, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7LabelString17, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7TextNumberTotalKariKingaku, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7TextNumberTotalKariZeigaku, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7TextNumberTotalKariSougaku, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7TextNumberTotalKasiKingaku, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7TextNumberTotalKasiZeigaku, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7TextNumberTotalKasiSougaku, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7LabelString21, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7LabelString18, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7LabelString19, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7LabelStringKasiMemo, 0);
            this.obic7PanelDetail.Controls.SetChildIndex(this.obic7LabelStringKariMemo, 0);
            // 
            // obic7ComboBoxMode
            // 
            this.obic7ComboBoxMode.ColumnNumber = 0;
            this.obic7ComboBoxMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.obic7ComboBoxMode.EnableFalseLock = false;
            this.obic7ComboBoxMode.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7ComboBoxMode.Font = new System.Drawing.Font("ＭＳ ゴシック", 12F);
            this.obic7ComboBoxMode.FormattingEnabled = true;
            this.obic7ComboBoxMode.Items.AddRange(new object[] {
            "新規",
            "修正",
            "照会",
            "削除"});
            this.obic7ComboBoxMode.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7ComboBoxMode.Location = new System.Drawing.Point(6, 3);
            this.obic7ComboBoxMode.Name = "obic7ComboBoxMode";
            this.obic7ComboBoxMode.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7ComboBoxMode.Size = new System.Drawing.Size(66, 24);
            this.obic7ComboBoxMode.TabIndex = 1010;
            this.obic7ComboBoxMode.VisibleFalseLock = false;
            this.obic7ComboBoxMode.Enter += new System.EventHandler(this.obic7ComboBoxMode_Enter);
            this.obic7ComboBoxMode.Validating += new System.ComponentModel.CancelEventHandler(this.obic7ComboBoxMode_Validating);
            // 
            // obic7TextDatetimeHasseiDate
            // 
            this.obic7TextDatetimeHasseiDate.AttachedButton = null;
            this.obic7TextDatetimeHasseiDate.AttachedCopyText = null;
            this.obic7TextDatetimeHasseiDate.AttachedLabel = this.obic7LabelStringHasseiDate;
            this.obic7TextDatetimeHasseiDate.AttachedSearchDialog = null;
            this.obic7TextDatetimeHasseiDate.CheckOnClose = false;
            this.obic7TextDatetimeHasseiDate.ColumnNumber = 0;
            this.obic7TextDatetimeHasseiDate.EnableFalseLock = false;
            this.obic7TextDatetimeHasseiDate.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextDatetimeHasseiDate.ErrorString = null;
            this.obic7TextDatetimeHasseiDate.Format = "yyyy/MM/dd";
            this.obic7TextDatetimeHasseiDate.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.obic7TextDatetimeHasseiDate.IsKeyColumn = false;
            this.obic7TextDatetimeHasseiDate.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextDatetimeHasseiDate.Location = new System.Drawing.Point(144, 27);
            this.obic7TextDatetimeHasseiDate.MaxLengthByte = 32767;
            this.obic7TextDatetimeHasseiDate.MaxValue = new System.DateTime(2100, 1, 1, 0, 0, 0, 0);
            this.obic7TextDatetimeHasseiDate.MinValue = new System.DateTime(1868, 9, 8, 0, 0, 0, 0);
            this.obic7TextDatetimeHasseiDate.Name = "obic7TextDatetimeHasseiDate";
            this.obic7TextDatetimeHasseiDate.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextDatetimeHasseiDate.Size = new System.Drawing.Size(72, 19);
            this.obic7TextDatetimeHasseiDate.TabIndex = 1050;
            this.obic7TextDatetimeHasseiDate.UseJapaneseCalendar = false;
            this.obic7TextDatetimeHasseiDate.Value = null;
            this.obic7TextDatetimeHasseiDate.VisibleFalseLock = false;
            this.obic7TextDatetimeHasseiDate.Enter += new System.EventHandler(this.obic7TextDatetimeHasseiDate_Enter);
            this.obic7TextDatetimeHasseiDate.Validating += new System.ComponentModel.CancelEventHandler(this.obic7TextDatetimeHasseiDate_Validating);
            // 
            // obic7LabelStringHasseiDate
            // 
            this.obic7LabelStringHasseiDate.AutoEllipsis = true;
            this.obic7LabelStringHasseiDate.BackColor = System.Drawing.SystemColors.Control;
            this.obic7LabelStringHasseiDate.ColumnNumber = 0;
            this.obic7LabelStringHasseiDate.EnableFalseLock = false;
            this.obic7LabelStringHasseiDate.ErrorString = null;
            this.obic7LabelStringHasseiDate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelStringHasseiDate.HasHelp = false;
            this.obic7LabelStringHasseiDate.Location = new System.Drawing.Point(78, 27);
            this.obic7LabelStringHasseiDate.Name = "obic7LabelStringHasseiDate";
            this.obic7LabelStringHasseiDate.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.RequiredLabel;
            this.obic7LabelStringHasseiDate.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringHasseiDate.Size = new System.Drawing.Size(60, 19);
            this.obic7LabelStringHasseiDate.TabIndex = 152;
            this.obic7LabelStringHasseiDate.Text = "発生日";
            this.obic7LabelStringHasseiDate.Value = "発生日";
            this.obic7LabelStringHasseiDate.VisibleFalseLock = false;
            // 
            // obic7TextStringZigyoushoCD
            // 
            this.obic7TextStringZigyoushoCD.AllowAlphabet = true;
            this.obic7TextStringZigyoushoCD.AllowKana = false;
            this.obic7TextStringZigyoushoCD.AllowNumber = true;
            this.obic7TextStringZigyoushoCD.AllowSymbol = false;
            this.obic7TextStringZigyoushoCD.AllowUnicode = false;
            this.obic7TextStringZigyoushoCD.AllowZenkaku = false;
            this.obic7TextStringZigyoushoCD.AttachedButton = this.obic7ButtonWithLogicSearchZigyoushoCD;
            this.obic7TextStringZigyoushoCD.AttachedCopyText = null;
            this.obic7TextStringZigyoushoCD.AttachedLabel = this.obic7LabelStringZigyousho;
            this.obic7TextStringZigyoushoCD.AttachedSearchDialog = null;
            this.obic7TextStringZigyoushoCD.CheckOnClose = false;
            this.obic7TextStringZigyoushoCD.ColumnNumber = 0;
            this.obic7TextStringZigyoushoCD.DispFuriganaText = null;
            this.obic7TextStringZigyoushoCD.EnableFalseLock = false;
            this.obic7TextStringZigyoushoCD.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringZigyoushoCD.ErrorString = null;
            this.obic7TextStringZigyoushoCD.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringZigyoushoCD.IsKeyColumn = false;
            this.obic7TextStringZigyoushoCD.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringZigyoushoCD.Location = new System.Drawing.Point(144, 3);
            this.obic7TextStringZigyoushoCD.MaxLengthByte = 6;
            this.obic7TextStringZigyoushoCD.Name = "obic7TextStringZigyoushoCD";
            this.obic7TextStringZigyoushoCD.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringZigyoushoCD.Size = new System.Drawing.Size(48, 19);
            this.obic7TextStringZigyoushoCD.TabIndex = 1020;
            this.obic7TextStringZigyoushoCD.Value = "";
            this.obic7TextStringZigyoushoCD.VisibleFalseLock = false;
            this.obic7TextStringZigyoushoCD.Enter += new System.EventHandler(this.obic7TextStringZigyoushoCD_Enter);
            this.obic7TextStringZigyoushoCD.Validating += new System.ComponentModel.CancelEventHandler(this.obic7TextStringZigyoushoCD_Validating);
            // 
            // obic7ButtonWithLogicSearchZigyoushoCD
            // 
            this.obic7ButtonWithLogicSearchZigyoushoCD.ColumnNumber = 0;
            this.obic7ButtonWithLogicSearchZigyoushoCD.EnableFalseLock = false;
            this.obic7ButtonWithLogicSearchZigyoushoCD.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.obic7ButtonWithLogicSearchZigyoushoCD.Location = new System.Drawing.Point(192, 1);
            this.obic7ButtonWithLogicSearchZigyoushoCD.Name = "obic7ButtonWithLogicSearchZigyoushoCD";
            this.obic7ButtonWithLogicSearchZigyoushoCD.Obic7JobSecurity = Obic7fx.Com.A0000.Tool.Obic7JobSecurities.NoSecurity;
            this.obic7ButtonWithLogicSearchZigyoushoCD.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7ButtonWithLogicSearchZigyoushoCD.Size = new System.Drawing.Size(24, 23);
            this.obic7ButtonWithLogicSearchZigyoushoCD.TabIndex = 1030;
            this.obic7ButtonWithLogicSearchZigyoushoCD.Text = "...";
            this.obic7ButtonWithLogicSearchZigyoushoCD.UseVisualStyleBackColor = true;
            this.obic7ButtonWithLogicSearchZigyoushoCD.VisibleFalseLock = false;
            this.obic7ButtonWithLogicSearchZigyoushoCD.Click += new System.EventHandler(this.obic7ButtonWithLogicSearchZigyoushoCD_Click);
            this.obic7ButtonWithLogicSearchZigyoushoCD.Enter += new System.EventHandler(this.obic7ButtonWithLogicSearchZigyoushoCD_Enter);
            // 
            // obic7LabelStringZigyousho
            // 
            this.obic7LabelStringZigyousho.AutoEllipsis = true;
            this.obic7LabelStringZigyousho.BackColor = System.Drawing.SystemColors.Control;
            this.obic7LabelStringZigyousho.ColumnNumber = 0;
            this.obic7LabelStringZigyousho.EnableFalseLock = false;
            this.obic7LabelStringZigyousho.ErrorString = null;
            this.obic7LabelStringZigyousho.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelStringZigyousho.HasHelp = false;
            this.obic7LabelStringZigyousho.Location = new System.Drawing.Point(78, 3);
            this.obic7LabelStringZigyousho.Name = "obic7LabelStringZigyousho";
            this.obic7LabelStringZigyousho.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.RequiredLabel;
            this.obic7LabelStringZigyousho.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringZigyousho.Size = new System.Drawing.Size(60, 19);
            this.obic7LabelStringZigyousho.TabIndex = 151;
            this.obic7LabelStringZigyousho.Text = "事業所";
            this.obic7LabelStringZigyousho.Value = "事業所";
            this.obic7LabelStringZigyousho.VisibleFalseLock = false;
            // 
            // obic7TextStringDenpyouTekiyou
            // 
            this.obic7TextStringDenpyouTekiyou.AllowAlphabet = true;
            this.obic7TextStringDenpyouTekiyou.AllowKana = true;
            this.obic7TextStringDenpyouTekiyou.AllowNumber = true;
            this.obic7TextStringDenpyouTekiyou.AllowSymbol = true;
            this.obic7TextStringDenpyouTekiyou.AllowUnicode = false;
            this.obic7TextStringDenpyouTekiyou.AllowZenkaku = true;
            this.obic7TextStringDenpyouTekiyou.AttachedButton = this.obic7ButtonWithLogicSearchDenpyouTekiyou;
            this.obic7TextStringDenpyouTekiyou.AttachedCopyText = null;
            this.obic7TextStringDenpyouTekiyou.AttachedLabel = this.obic7LabelStringDenpyouTekiyou;
            this.obic7TextStringDenpyouTekiyou.AttachedSearchDialog = null;
            this.obic7TextStringDenpyouTekiyou.CheckOnClose = false;
            this.obic7TextStringDenpyouTekiyou.ColumnNumber = 0;
            this.obic7TextStringDenpyouTekiyou.DispFuriganaText = null;
            this.obic7TextStringDenpyouTekiyou.EnableFalseLock = false;
            this.obic7TextStringDenpyouTekiyou.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringDenpyouTekiyou.ErrorString = null;
            this.obic7TextStringDenpyouTekiyou.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.obic7TextStringDenpyouTekiyou.IsKeyColumn = false;
            this.obic7TextStringDenpyouTekiyou.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringDenpyouTekiyou.Location = new System.Drawing.Point(144, 51);
            this.obic7TextStringDenpyouTekiyou.MaxLengthByte = 60;
            this.obic7TextStringDenpyouTekiyou.Name = "obic7TextStringDenpyouTekiyou";
            this.obic7TextStringDenpyouTekiyou.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringDenpyouTekiyou.Size = new System.Drawing.Size(372, 19);
            this.obic7TextStringDenpyouTekiyou.TabIndex = 1160;
            this.obic7TextStringDenpyouTekiyou.Value = "";
            this.obic7TextStringDenpyouTekiyou.VisibleFalseLock = false;
            this.obic7TextStringDenpyouTekiyou.Enter += new System.EventHandler(this.obic7TextStringDenpyouTekiyou_Enter);
            // 
            // obic7ButtonWithLogicSearchDenpyouTekiyou
            // 
            this.obic7ButtonWithLogicSearchDenpyouTekiyou.ColumnNumber = 0;
            this.obic7ButtonWithLogicSearchDenpyouTekiyou.EnableFalseLock = false;
            this.obic7ButtonWithLogicSearchDenpyouTekiyou.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.obic7ButtonWithLogicSearchDenpyouTekiyou.Location = new System.Drawing.Point(516, 49);
            this.obic7ButtonWithLogicSearchDenpyouTekiyou.Name = "obic7ButtonWithLogicSearchDenpyouTekiyou";
            this.obic7ButtonWithLogicSearchDenpyouTekiyou.Obic7JobSecurity = Obic7fx.Com.A0000.Tool.Obic7JobSecurities.NoSecurity;
            this.obic7ButtonWithLogicSearchDenpyouTekiyou.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7ButtonWithLogicSearchDenpyouTekiyou.Size = new System.Drawing.Size(24, 23);
            this.obic7ButtonWithLogicSearchDenpyouTekiyou.TabIndex = 1170;
            this.obic7ButtonWithLogicSearchDenpyouTekiyou.Text = "...";
            this.obic7ButtonWithLogicSearchDenpyouTekiyou.UseVisualStyleBackColor = true;
            this.obic7ButtonWithLogicSearchDenpyouTekiyou.VisibleFalseLock = false;
            this.obic7ButtonWithLogicSearchDenpyouTekiyou.Click += new System.EventHandler(this.obic7ButtonWithLogicSearchDenpyouTekiyou_Click);
            this.obic7ButtonWithLogicSearchDenpyouTekiyou.Enter += new System.EventHandler(this.obic7ButtonWithLogicSearchDenpyouTekiyou_Enter);
            // 
            // obic7LabelStringDenpyouTekiyou
            // 
            this.obic7LabelStringDenpyouTekiyou.AutoEllipsis = true;
            this.obic7LabelStringDenpyouTekiyou.BackColor = System.Drawing.SystemColors.Control;
            this.obic7LabelStringDenpyouTekiyou.ColumnNumber = 0;
            this.obic7LabelStringDenpyouTekiyou.EnableFalseLock = false;
            this.obic7LabelStringDenpyouTekiyou.ErrorString = null;
            this.obic7LabelStringDenpyouTekiyou.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelStringDenpyouTekiyou.HasHelp = false;
            this.obic7LabelStringDenpyouTekiyou.Location = new System.Drawing.Point(78, 51);
            this.obic7LabelStringDenpyouTekiyou.Name = "obic7LabelStringDenpyouTekiyou";
            this.obic7LabelStringDenpyouTekiyou.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringDenpyouTekiyou.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringDenpyouTekiyou.Size = new System.Drawing.Size(60, 19);
            this.obic7LabelStringDenpyouTekiyou.TabIndex = 156;
            this.obic7LabelStringDenpyouTekiyou.Text = "伝票摘要";
            this.obic7LabelStringDenpyouTekiyou.Value = "伝票摘要";
            this.obic7LabelStringDenpyouTekiyou.VisibleFalseLock = false;
            // 
            // obic7LabelStringZigyoushoName
            // 
            this.obic7LabelStringZigyoushoName.AutoEllipsis = true;
            this.obic7LabelStringZigyoushoName.BackColor = System.Drawing.SystemColors.Desktop;
            this.obic7LabelStringZigyoushoName.ColumnNumber = 0;
            this.obic7LabelStringZigyoushoName.EnableFalseLock = false;
            this.obic7LabelStringZigyoushoName.ErrorString = null;
            this.obic7LabelStringZigyoushoName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelStringZigyoushoName.HasHelp = false;
            this.obic7LabelStringZigyoushoName.Location = new System.Drawing.Point(216, 3);
            this.obic7LabelStringZigyoushoName.Name = "obic7LabelStringZigyoushoName";
            this.obic7LabelStringZigyoushoName.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7LabelStringZigyoushoName.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringZigyoushoName.Size = new System.Drawing.Size(132, 19);
            this.obic7LabelStringZigyoushoName.TabIndex = 1040;
            this.obic7LabelStringZigyoushoName.Value = "";
            this.obic7LabelStringZigyoushoName.VisibleFalseLock = false;
            // 
            // obic7LabelStringDenpyouKBN
            // 
            this.obic7LabelStringDenpyouKBN.AutoEllipsis = true;
            this.obic7LabelStringDenpyouKBN.BackColor = System.Drawing.SystemColors.Control;
            this.obic7LabelStringDenpyouKBN.ColumnNumber = 0;
            this.obic7LabelStringDenpyouKBN.EnableFalseLock = false;
            this.obic7LabelStringDenpyouKBN.ErrorString = null;
            this.obic7LabelStringDenpyouKBN.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelStringDenpyouKBN.HasHelp = false;
            this.obic7LabelStringDenpyouKBN.Location = new System.Drawing.Point(348, 27);
            this.obic7LabelStringDenpyouKBN.Name = "obic7LabelStringDenpyouKBN";
            this.obic7LabelStringDenpyouKBN.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.RequiredLabel;
            this.obic7LabelStringDenpyouKBN.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringDenpyouKBN.Size = new System.Drawing.Size(60, 19);
            this.obic7LabelStringDenpyouKBN.TabIndex = 1133;
            this.obic7LabelStringDenpyouKBN.Text = "伝票区分";
            this.obic7LabelStringDenpyouKBN.Value = "伝票区分";
            this.obic7LabelStringDenpyouKBN.VisibleFalseLock = false;
            // 
            // obic7LabelStringDenpyouNO
            // 
            this.obic7LabelStringDenpyouNO.AutoEllipsis = true;
            this.obic7LabelStringDenpyouNO.BackColor = System.Drawing.SystemColors.Control;
            this.obic7LabelStringDenpyouNO.ColumnNumber = 0;
            this.obic7LabelStringDenpyouNO.EnableFalseLock = false;
            this.obic7LabelStringDenpyouNO.ErrorString = null;
            this.obic7LabelStringDenpyouNO.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelStringDenpyouNO.HasHelp = false;
            this.obic7LabelStringDenpyouNO.Location = new System.Drawing.Point(600, 3);
            this.obic7LabelStringDenpyouNO.Name = "obic7LabelStringDenpyouNO";
            this.obic7LabelStringDenpyouNO.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringDenpyouNO.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringDenpyouNO.Size = new System.Drawing.Size(84, 19);
            this.obic7LabelStringDenpyouNO.TabIndex = 1134;
            this.obic7LabelStringDenpyouNO.Text = "伝票番号";
            this.obic7LabelStringDenpyouNO.Value = "伝票番号";
            this.obic7LabelStringDenpyouNO.VisibleFalseLock = false;
            // 
            // obic7TextStringDenpyouNO
            // 
            this.obic7TextStringDenpyouNO.AllowAlphabet = true;
            this.obic7TextStringDenpyouNO.AllowKana = true;
            this.obic7TextStringDenpyouNO.AllowNumber = true;
            this.obic7TextStringDenpyouNO.AllowSymbol = true;
            this.obic7TextStringDenpyouNO.AllowUnicode = false;
            this.obic7TextStringDenpyouNO.AllowZenkaku = true;
            this.obic7TextStringDenpyouNO.AttachedButton = null;
            this.obic7TextStringDenpyouNO.AttachedCopyText = null;
            this.obic7TextStringDenpyouNO.AttachedLabel = this.obic7LabelStringDenpyouNO;
            this.obic7TextStringDenpyouNO.AttachedSearchDialog = null;
            this.obic7TextStringDenpyouNO.CheckOnClose = false;
            this.obic7TextStringDenpyouNO.ColumnNumber = 0;
            this.obic7TextStringDenpyouNO.DispFuriganaText = null;
            this.obic7TextStringDenpyouNO.EnableFalseLock = false;
            this.obic7TextStringDenpyouNO.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringDenpyouNO.ErrorString = null;
            this.obic7TextStringDenpyouNO.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringDenpyouNO.IsKeyColumn = false;
            this.obic7TextStringDenpyouNO.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringDenpyouNO.Location = new System.Drawing.Point(690, 3);
            this.obic7TextStringDenpyouNO.MaxLengthByte = 12;
            this.obic7TextStringDenpyouNO.Name = "obic7TextStringDenpyouNO";
            this.obic7TextStringDenpyouNO.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringDenpyouNO.Size = new System.Drawing.Size(84, 19);
            this.obic7TextStringDenpyouNO.TabIndex = 1120;
            this.obic7TextStringDenpyouNO.Value = "";
            this.obic7TextStringDenpyouNO.VisibleFalseLock = false;
            this.obic7TextStringDenpyouNO.Enter += new System.EventHandler(this.obic7TextStringDenpyouNO_Enter);
            this.obic7TextStringDenpyouNO.Validating += new System.ComponentModel.CancelEventHandler(this.obic7TextStringDenpyouNO_Validating);
            // 
            // obic7TextStringDenpyouKBN
            // 
            this.obic7TextStringDenpyouKBN.AllowAlphabet = false;
            this.obic7TextStringDenpyouKBN.AllowKana = false;
            this.obic7TextStringDenpyouKBN.AllowNumber = true;
            this.obic7TextStringDenpyouKBN.AllowSymbol = false;
            this.obic7TextStringDenpyouKBN.AllowUnicode = false;
            this.obic7TextStringDenpyouKBN.AllowZenkaku = false;
            this.obic7TextStringDenpyouKBN.AttachedButton = this.obic7ButtonWithLogicSearchDenpyouKBN;
            this.obic7TextStringDenpyouKBN.AttachedCopyText = null;
            this.obic7TextStringDenpyouKBN.AttachedLabel = this.obic7LabelStringDenpyouKBN;
            this.obic7TextStringDenpyouKBN.AttachedSearchDialog = null;
            this.obic7TextStringDenpyouKBN.CheckOnClose = false;
            this.obic7TextStringDenpyouKBN.ColumnNumber = 0;
            this.obic7TextStringDenpyouKBN.DispFuriganaText = null;
            this.obic7TextStringDenpyouKBN.EnableFalseLock = false;
            this.obic7TextStringDenpyouKBN.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringDenpyouKBN.ErrorString = null;
            this.obic7TextStringDenpyouKBN.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringDenpyouKBN.IsKeyColumn = false;
            this.obic7TextStringDenpyouKBN.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringDenpyouKBN.Location = new System.Drawing.Point(414, 27);
            this.obic7TextStringDenpyouKBN.MaxLengthByte = 3;
            this.obic7TextStringDenpyouKBN.Name = "obic7TextStringDenpyouKBN";
            this.obic7TextStringDenpyouKBN.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringDenpyouKBN.Size = new System.Drawing.Size(30, 19);
            this.obic7TextStringDenpyouKBN.TabIndex = 1090;
            this.obic7TextStringDenpyouKBN.Value = "";
            this.obic7TextStringDenpyouKBN.VisibleFalseLock = false;
            this.obic7TextStringDenpyouKBN.Enter += new System.EventHandler(this.obic7TextStringDenpyouKBN_Enter);
            this.obic7TextStringDenpyouKBN.Validating += new System.ComponentModel.CancelEventHandler(this.obic7TextStringDenpyouKBN_Validating);
            // 
            // obic7ButtonWithLogicSearchDenpyouKBN
            // 
            this.obic7ButtonWithLogicSearchDenpyouKBN.ColumnNumber = 0;
            this.obic7ButtonWithLogicSearchDenpyouKBN.EnableFalseLock = false;
            this.obic7ButtonWithLogicSearchDenpyouKBN.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.obic7ButtonWithLogicSearchDenpyouKBN.Location = new System.Drawing.Point(444, 25);
            this.obic7ButtonWithLogicSearchDenpyouKBN.Name = "obic7ButtonWithLogicSearchDenpyouKBN";
            this.obic7ButtonWithLogicSearchDenpyouKBN.Obic7JobSecurity = Obic7fx.Com.A0000.Tool.Obic7JobSecurities.NoSecurity;
            this.obic7ButtonWithLogicSearchDenpyouKBN.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7ButtonWithLogicSearchDenpyouKBN.Size = new System.Drawing.Size(24, 23);
            this.obic7ButtonWithLogicSearchDenpyouKBN.TabIndex = 1100;
            this.obic7ButtonWithLogicSearchDenpyouKBN.Text = "...";
            this.obic7ButtonWithLogicSearchDenpyouKBN.UseVisualStyleBackColor = true;
            this.obic7ButtonWithLogicSearchDenpyouKBN.VisibleFalseLock = false;
            this.obic7ButtonWithLogicSearchDenpyouKBN.Click += new System.EventHandler(this.obic7ButtonWithLogicSearchDenpyouKBN_Click);
            this.obic7ButtonWithLogicSearchDenpyouKBN.Enter += new System.EventHandler(this.obic7ButtonWithLogicSearchDenpyouKBN_Enter);
            // 
            // obic7LabelStringDenpyouKBNName
            // 
            this.obic7LabelStringDenpyouKBNName.AutoEllipsis = true;
            this.obic7LabelStringDenpyouKBNName.BackColor = System.Drawing.SystemColors.Desktop;
            this.obic7LabelStringDenpyouKBNName.ColumnNumber = 0;
            this.obic7LabelStringDenpyouKBNName.EnableFalseLock = false;
            this.obic7LabelStringDenpyouKBNName.ErrorString = null;
            this.obic7LabelStringDenpyouKBNName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelStringDenpyouKBNName.HasHelp = false;
            this.obic7LabelStringDenpyouKBNName.Location = new System.Drawing.Point(468, 27);
            this.obic7LabelStringDenpyouKBNName.Name = "obic7LabelStringDenpyouKBNName";
            this.obic7LabelStringDenpyouKBNName.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7LabelStringDenpyouKBNName.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringDenpyouKBNName.Size = new System.Drawing.Size(132, 19);
            this.obic7LabelStringDenpyouKBNName.TabIndex = 1110;
            this.obic7LabelStringDenpyouKBNName.Value = "";
            this.obic7LabelStringDenpyouKBNName.VisibleFalseLock = false;
            // 
            // obic7LabelStringPrevDenpyouNO
            // 
            this.obic7LabelStringPrevDenpyouNO.AutoEllipsis = true;
            this.obic7LabelStringPrevDenpyouNO.BackColor = System.Drawing.SystemColors.Control;
            this.obic7LabelStringPrevDenpyouNO.ColumnNumber = 0;
            this.obic7LabelStringPrevDenpyouNO.EnableFalseLock = false;
            this.obic7LabelStringPrevDenpyouNO.ErrorString = null;
            this.obic7LabelStringPrevDenpyouNO.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelStringPrevDenpyouNO.HasHelp = false;
            this.obic7LabelStringPrevDenpyouNO.Location = new System.Drawing.Point(804, 3);
            this.obic7LabelStringPrevDenpyouNO.Name = "obic7LabelStringPrevDenpyouNO";
            this.obic7LabelStringPrevDenpyouNO.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringPrevDenpyouNO.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringPrevDenpyouNO.Size = new System.Drawing.Size(84, 19);
            this.obic7LabelStringPrevDenpyouNO.TabIndex = 1139;
            this.obic7LabelStringPrevDenpyouNO.Text = "前回伝票番号";
            this.obic7LabelStringPrevDenpyouNO.Value = "前回伝票番号";
            this.obic7LabelStringPrevDenpyouNO.VisibleFalseLock = false;
            // 
            // obic7LabelStringCalledDenpyouNO
            // 
            this.obic7LabelStringCalledDenpyouNO.AutoEllipsis = true;
            this.obic7LabelStringCalledDenpyouNO.BackColor = System.Drawing.SystemColors.Control;
            this.obic7LabelStringCalledDenpyouNO.ColumnNumber = 0;
            this.obic7LabelStringCalledDenpyouNO.EnableFalseLock = false;
            this.obic7LabelStringCalledDenpyouNO.ErrorString = null;
            this.obic7LabelStringCalledDenpyouNO.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelStringCalledDenpyouNO.HasHelp = false;
            this.obic7LabelStringCalledDenpyouNO.Location = new System.Drawing.Point(600, 27);
            this.obic7LabelStringCalledDenpyouNO.Name = "obic7LabelStringCalledDenpyouNO";
            this.obic7LabelStringCalledDenpyouNO.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringCalledDenpyouNO.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringCalledDenpyouNO.Size = new System.Drawing.Size(84, 19);
            this.obic7LabelStringCalledDenpyouNO.TabIndex = 1140;
            this.obic7LabelStringCalledDenpyouNO.Text = "呼出伝票番号";
            this.obic7LabelStringCalledDenpyouNO.Value = "呼出伝票番号";
            this.obic7LabelStringCalledDenpyouNO.VisibleFalseLock = false;
            // 
            // obic7LabelStringSokudenhakkou
            // 
            this.obic7LabelStringSokudenhakkou.AutoEllipsis = true;
            this.obic7LabelStringSokudenhakkou.BackColor = System.Drawing.SystemColors.Control;
            this.obic7LabelStringSokudenhakkou.ColumnNumber = 0;
            this.obic7LabelStringSokudenhakkou.EnableFalseLock = false;
            this.obic7LabelStringSokudenhakkou.ErrorString = null;
            this.obic7LabelStringSokudenhakkou.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelStringSokudenhakkou.HasHelp = false;
            this.obic7LabelStringSokudenhakkou.Location = new System.Drawing.Point(600, 51);
            this.obic7LabelStringSokudenhakkou.Name = "obic7LabelStringSokudenhakkou";
            this.obic7LabelStringSokudenhakkou.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.RequiredLabel;
            this.obic7LabelStringSokudenhakkou.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringSokudenhakkou.Size = new System.Drawing.Size(84, 19);
            this.obic7LabelStringSokudenhakkou.TabIndex = 1141;
            this.obic7LabelStringSokudenhakkou.Text = "即伝発行";
            this.obic7LabelStringSokudenhakkou.Value = "即伝発行";
            this.obic7LabelStringSokudenhakkou.VisibleFalseLock = false;
            // 
            // obic7TextStringCalledDenpyouNO
            // 
            this.obic7TextStringCalledDenpyouNO.AllowAlphabet = true;
            this.obic7TextStringCalledDenpyouNO.AllowKana = true;
            this.obic7TextStringCalledDenpyouNO.AllowNumber = true;
            this.obic7TextStringCalledDenpyouNO.AllowSymbol = true;
            this.obic7TextStringCalledDenpyouNO.AllowUnicode = false;
            this.obic7TextStringCalledDenpyouNO.AllowZenkaku = true;
            this.obic7TextStringCalledDenpyouNO.AttachedButton = this.obic7ButtonWithLogicSearchCalledDenpyouNO;
            this.obic7TextStringCalledDenpyouNO.AttachedCopyText = null;
            this.obic7TextStringCalledDenpyouNO.AttachedLabel = this.obic7LabelStringCalledDenpyouNO;
            this.obic7TextStringCalledDenpyouNO.AttachedSearchDialog = null;
            this.obic7TextStringCalledDenpyouNO.CheckOnClose = false;
            this.obic7TextStringCalledDenpyouNO.ColumnNumber = 0;
            this.obic7TextStringCalledDenpyouNO.DispFuriganaText = null;
            this.obic7TextStringCalledDenpyouNO.EnableFalseLock = false;
            this.obic7TextStringCalledDenpyouNO.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringCalledDenpyouNO.ErrorString = null;
            this.obic7TextStringCalledDenpyouNO.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringCalledDenpyouNO.IsKeyColumn = false;
            this.obic7TextStringCalledDenpyouNO.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringCalledDenpyouNO.Location = new System.Drawing.Point(690, 27);
            this.obic7TextStringCalledDenpyouNO.MaxLengthByte = 12;
            this.obic7TextStringCalledDenpyouNO.Name = "obic7TextStringCalledDenpyouNO";
            this.obic7TextStringCalledDenpyouNO.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringCalledDenpyouNO.Size = new System.Drawing.Size(84, 19);
            this.obic7TextStringCalledDenpyouNO.TabIndex = 1140;
            this.obic7TextStringCalledDenpyouNO.Value = "";
            this.obic7TextStringCalledDenpyouNO.VisibleFalseLock = false;
            this.obic7TextStringCalledDenpyouNO.Enter += new System.EventHandler(this.obic7TextStringCalledDenpyouNO_Enter);
            this.obic7TextStringCalledDenpyouNO.Validating += new System.ComponentModel.CancelEventHandler(this.obic7TextStringCalledDenpyouNO_Validating);
            // 
            // obic7ButtonWithLogicSearchCalledDenpyouNO
            // 
            this.obic7ButtonWithLogicSearchCalledDenpyouNO.ColumnNumber = 0;
            this.obic7ButtonWithLogicSearchCalledDenpyouNO.EnableFalseLock = false;
            this.obic7ButtonWithLogicSearchCalledDenpyouNO.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.obic7ButtonWithLogicSearchCalledDenpyouNO.Location = new System.Drawing.Point(774, 25);
            this.obic7ButtonWithLogicSearchCalledDenpyouNO.Name = "obic7ButtonWithLogicSearchCalledDenpyouNO";
            this.obic7ButtonWithLogicSearchCalledDenpyouNO.Obic7JobSecurity = Obic7fx.Com.A0000.Tool.Obic7JobSecurities.NoSecurity;
            this.obic7ButtonWithLogicSearchCalledDenpyouNO.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7ButtonWithLogicSearchCalledDenpyouNO.Size = new System.Drawing.Size(24, 23);
            this.obic7ButtonWithLogicSearchCalledDenpyouNO.TabIndex = 1150;
            this.obic7ButtonWithLogicSearchCalledDenpyouNO.Text = "...";
            this.obic7ButtonWithLogicSearchCalledDenpyouNO.UseVisualStyleBackColor = true;
            this.obic7ButtonWithLogicSearchCalledDenpyouNO.VisibleFalseLock = false;
            this.obic7ButtonWithLogicSearchCalledDenpyouNO.Click += new System.EventHandler(this.obic7ButtonWithLogicSearchCalledDenpyouNO_Click);
            this.obic7ButtonWithLogicSearchCalledDenpyouNO.Enter += new System.EventHandler(this.obic7ButtonWithLogicSearchCalledDenpyouNO_Enter);
            // 
            // obic7TextNumberSokudenHakkou
            // 
            this.obic7TextNumberSokudenHakkou.AttachedButton = this.obic7ButtonWithLogicSearchSokudenHakkou;
            this.obic7TextNumberSokudenHakkou.AttachedCopyText = null;
            this.obic7TextNumberSokudenHakkou.AttachedLabel = this.obic7LabelStringSokudenhakkou;
            this.obic7TextNumberSokudenHakkou.AttachedSearchDialog = null;
            this.obic7TextNumberSokudenHakkou.CheckOnClose = false;
            this.obic7TextNumberSokudenHakkou.ColumnNumber = 0;
            this.obic7TextNumberSokudenHakkou.EnableFalseLock = false;
            this.obic7TextNumberSokudenHakkou.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberSokudenHakkou.ErrorString = null;
            this.obic7TextNumberSokudenHakkou.ForeColor = System.Drawing.SystemColors.WindowText;
            this.obic7TextNumberSokudenHakkou.Format = "0";
            this.obic7TextNumberSokudenHakkou.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.obic7TextNumberSokudenHakkou.IsKeyColumn = false;
            this.obic7TextNumberSokudenHakkou.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberSokudenHakkou.Location = new System.Drawing.Point(690, 51);
            this.obic7TextNumberSokudenHakkou.MaxLength = 1;
            this.obic7TextNumberSokudenHakkou.MaxLengthByte = 1;
            this.obic7TextNumberSokudenHakkou.MaxValue = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.obic7TextNumberSokudenHakkou.MinusColor = System.Drawing.Color.Red;
            this.obic7TextNumberSokudenHakkou.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.obic7TextNumberSokudenHakkou.Name = "obic7TextNumberSokudenHakkou";
            this.obic7TextNumberSokudenHakkou.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextNumberSokudenHakkou.Size = new System.Drawing.Size(24, 19);
            this.obic7TextNumberSokudenHakkou.TabIndex = 1180;
            this.obic7TextNumberSokudenHakkou.Text = "0";
            this.obic7TextNumberSokudenHakkou.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.obic7TextNumberSokudenHakkou.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.obic7TextNumberSokudenHakkou.VisibleFalseLock = false;
            this.obic7TextNumberSokudenHakkou.Enter += new System.EventHandler(this.obic7TextNumberSokudenHakkou_Enter);
            this.obic7TextNumberSokudenHakkou.Validating += new System.ComponentModel.CancelEventHandler(this.obic7TextNumberSokudenHakkou_Validating);
            // 
            // obic7ButtonWithLogicSearchSokudenHakkou
            // 
            this.obic7ButtonWithLogicSearchSokudenHakkou.ColumnNumber = 0;
            this.obic7ButtonWithLogicSearchSokudenHakkou.EnableFalseLock = false;
            this.obic7ButtonWithLogicSearchSokudenHakkou.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.obic7ButtonWithLogicSearchSokudenHakkou.Location = new System.Drawing.Point(714, 49);
            this.obic7ButtonWithLogicSearchSokudenHakkou.Name = "obic7ButtonWithLogicSearchSokudenHakkou";
            this.obic7ButtonWithLogicSearchSokudenHakkou.Obic7JobSecurity = Obic7fx.Com.A0000.Tool.Obic7JobSecurities.NoSecurity;
            this.obic7ButtonWithLogicSearchSokudenHakkou.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7ButtonWithLogicSearchSokudenHakkou.Size = new System.Drawing.Size(24, 23);
            this.obic7ButtonWithLogicSearchSokudenHakkou.TabIndex = 1190;
            this.obic7ButtonWithLogicSearchSokudenHakkou.Text = "...";
            this.obic7ButtonWithLogicSearchSokudenHakkou.UseVisualStyleBackColor = true;
            this.obic7ButtonWithLogicSearchSokudenHakkou.VisibleFalseLock = false;
            this.obic7ButtonWithLogicSearchSokudenHakkou.Click += new System.EventHandler(this.obic7ButtonWithLogicSearchSokudenHakkou_Click);
            this.obic7ButtonWithLogicSearchSokudenHakkou.Enter += new System.EventHandler(this.obic7ButtonWithLogicSearchSokudenHakkou_Enter);
            // 
            // obic7ButtonWithLogicDisplay
            // 
            this.obic7ButtonWithLogicDisplay.ColumnNumber = 0;
            this.obic7ButtonWithLogicDisplay.EnableFalseLock = false;
            this.obic7ButtonWithLogicDisplay.Location = new System.Drawing.Point(894, 49);
            this.obic7ButtonWithLogicDisplay.Name = "obic7ButtonWithLogicDisplay";
            this.obic7ButtonWithLogicDisplay.Obic7JobSecurity = Obic7fx.Com.A0000.Tool.Obic7JobSecurities.NoSecurity;
            this.obic7ButtonWithLogicDisplay.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7ButtonWithLogicDisplay.Size = new System.Drawing.Size(84, 23);
            this.obic7ButtonWithLogicDisplay.TabIndex = 1210;
            this.obic7ButtonWithLogicDisplay.Text = "表示";
            this.obic7ButtonWithLogicDisplay.UseVisualStyleBackColor = true;
            this.obic7ButtonWithLogicDisplay.VisibleFalseLock = false;
            this.obic7ButtonWithLogicDisplay.Click += new System.EventHandler(this.obic7ButtonWithLogicDisplay_Click);
            this.obic7ButtonWithLogicDisplay.Enter += new System.EventHandler(this.obic7ButtonWithLogicDisplay_Enter);
            // 
            // obic7LabelStringKariKanjou
            // 
            this.obic7LabelStringKariKanjou.AutoEllipsis = true;
            this.obic7LabelStringKariKanjou.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelStringKariKanjou.ColumnNumber = 0;
            this.obic7LabelStringKariKanjou.EnableFalseLock = false;
            this.obic7LabelStringKariKanjou.ErrorString = null;
            this.obic7LabelStringKariKanjou.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelStringKariKanjou.HasHelp = false;
            this.obic7LabelStringKariKanjou.Location = new System.Drawing.Point(55, 3);
            this.obic7LabelStringKariKanjou.Name = "obic7LabelStringKariKanjou";
            this.obic7LabelStringKariKanjou.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringKariKanjou.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringKariKanjou.Size = new System.Drawing.Size(190, 37);
            this.obic7LabelStringKariKanjou.TabIndex = 1000000000;
            this.obic7LabelStringKariKanjou.Text = "【借方】\r\n総勘定/補助/内訳";
            this.obic7LabelStringKariKanjou.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.obic7LabelStringKariKanjou.Value = "【借方】\r\n総勘定/補助/内訳";
            this.obic7LabelStringKariKanjou.VisibleFalseLock = false;
            // 
            // obic7LabelStringSokudenHakkouName
            // 
            this.obic7LabelStringSokudenHakkouName.AutoEllipsis = true;
            this.obic7LabelStringSokudenHakkouName.BackColor = System.Drawing.SystemColors.Desktop;
            this.obic7LabelStringSokudenHakkouName.ColumnNumber = 0;
            this.obic7LabelStringSokudenHakkouName.EnableFalseLock = false;
            this.obic7LabelStringSokudenHakkouName.ErrorString = null;
            this.obic7LabelStringSokudenHakkouName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelStringSokudenHakkouName.HasHelp = false;
            this.obic7LabelStringSokudenHakkouName.Location = new System.Drawing.Point(738, 51);
            this.obic7LabelStringSokudenHakkouName.Name = "obic7LabelStringSokudenHakkouName";
            this.obic7LabelStringSokudenHakkouName.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7LabelStringSokudenHakkouName.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringSokudenHakkouName.Size = new System.Drawing.Size(48, 19);
            this.obic7LabelStringSokudenHakkouName.TabIndex = 1200;
            this.obic7LabelStringSokudenHakkouName.Value = "";
            this.obic7LabelStringSokudenHakkouName.VisibleFalseLock = false;
            // 
            // obic7LabelString3
            // 
            this.obic7LabelString3.AutoEllipsis = true;
            this.obic7LabelString3.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelString3.ColumnNumber = 0;
            this.obic7LabelString3.EnableFalseLock = false;
            this.obic7LabelString3.ErrorString = null;
            this.obic7LabelString3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelString3.HasHelp = false;
            this.obic7LabelString3.Location = new System.Drawing.Point(246, 3);
            this.obic7LabelString3.Name = "obic7LabelString3";
            this.obic7LabelString3.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelString3.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelString3.Size = new System.Drawing.Size(197, 18);
            this.obic7LabelString3.TabIndex = 1000000003;
            this.obic7LabelString3.Text = "事業所/部門";
            this.obic7LabelString3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.obic7LabelString3.Value = "事業所/部門";
            this.obic7LabelString3.VisibleFalseLock = false;
            // 
            // obic7LabelString4
            // 
            this.obic7LabelString4.AutoEllipsis = true;
            this.obic7LabelString4.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelString4.ColumnNumber = 0;
            this.obic7LabelString4.EnableFalseLock = false;
            this.obic7LabelString4.ErrorString = null;
            this.obic7LabelString4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelString4.HasHelp = false;
            this.obic7LabelString4.Location = new System.Drawing.Point(444, 3);
            this.obic7LabelString4.Name = "obic7LabelString4";
            this.obic7LabelString4.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelString4.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelString4.Size = new System.Drawing.Size(131, 18);
            this.obic7LabelString4.TabIndex = 1000000004;
            this.obic7LabelString4.Text = "税区分";
            this.obic7LabelString4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.obic7LabelString4.Value = "税区分";
            this.obic7LabelString4.VisibleFalseLock = false;
            // 
            // obic7LabelString14
            // 
            this.obic7LabelString14.AutoEllipsis = true;
            this.obic7LabelString14.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelString14.ColumnNumber = 0;
            this.obic7LabelString14.EnableFalseLock = false;
            this.obic7LabelString14.ErrorString = null;
            this.obic7LabelString14.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelString14.HasHelp = false;
            this.obic7LabelString14.Location = new System.Drawing.Point(246, 22);
            this.obic7LabelString14.Name = "obic7LabelString14";
            this.obic7LabelString14.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelString14.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelString14.Size = new System.Drawing.Size(197, 18);
            this.obic7LabelString14.TabIndex = 1000000014;
            this.obic7LabelString14.Text = "消費税本体科目";
            this.obic7LabelString14.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.obic7LabelString14.Value = "消費税本体科目";
            this.obic7LabelString14.VisibleFalseLock = false;
            // 
            // obic7LabelString15
            // 
            this.obic7LabelString15.AutoEllipsis = true;
            this.obic7LabelString15.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelString15.ColumnNumber = 0;
            this.obic7LabelString15.EnableFalseLock = false;
            this.obic7LabelString15.ErrorString = null;
            this.obic7LabelString15.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelString15.HasHelp = false;
            this.obic7LabelString15.Location = new System.Drawing.Point(444, 22);
            this.obic7LabelString15.Name = "obic7LabelString15";
            this.obic7LabelString15.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelString15.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelString15.Size = new System.Drawing.Size(131, 18);
            this.obic7LabelString15.TabIndex = 1000000015;
            this.obic7LabelString15.Text = "金額/消費税額";
            this.obic7LabelString15.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.obic7LabelString15.Value = "金額/消費税額";
            this.obic7LabelString15.VisibleFalseLock = false;
            // 
            // obic7LabelString20
            // 
            this.obic7LabelString20.AutoEllipsis = true;
            this.obic7LabelString20.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelString20.ColumnNumber = 0;
            this.obic7LabelString20.EnableFalseLock = false;
            this.obic7LabelString20.ErrorString = null;
            this.obic7LabelString20.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelString20.HasHelp = false;
            this.obic7LabelString20.Location = new System.Drawing.Point(55, 41);
            this.obic7LabelString20.Name = "obic7LabelString20";
            this.obic7LabelString20.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelString20.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelString20.Size = new System.Drawing.Size(190, 18);
            this.obic7LabelString20.TabIndex = 1000000020;
            this.obic7LabelString20.Text = "取引先";
            this.obic7LabelString20.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.obic7LabelString20.Value = "取引先";
            this.obic7LabelString20.VisibleFalseLock = false;
            // 
            // obic7LabelString22
            // 
            this.obic7LabelString22.AutoEllipsis = true;
            this.obic7LabelString22.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelString22.ColumnNumber = 0;
            this.obic7LabelString22.EnableFalseLock = false;
            this.obic7LabelString22.ErrorString = null;
            this.obic7LabelString22.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelString22.HasHelp = false;
            this.obic7LabelString22.Location = new System.Drawing.Point(246, 41);
            this.obic7LabelString22.Name = "obic7LabelString22";
            this.obic7LabelString22.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelString22.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelString22.Size = new System.Drawing.Size(65, 18);
            this.obic7LabelString22.TabIndex = 1000000022;
            this.obic7LabelString22.Text = "分析1";
            this.obic7LabelString22.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.obic7LabelString22.Value = "分析1";
            this.obic7LabelString22.VisibleFalseLock = false;
            // 
            // obic7LabelString23
            // 
            this.obic7LabelString23.AutoEllipsis = true;
            this.obic7LabelString23.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelString23.ColumnNumber = 0;
            this.obic7LabelString23.EnableFalseLock = false;
            this.obic7LabelString23.ErrorString = null;
            this.obic7LabelString23.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelString23.HasHelp = false;
            this.obic7LabelString23.Location = new System.Drawing.Point(312, 41);
            this.obic7LabelString23.Name = "obic7LabelString23";
            this.obic7LabelString23.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelString23.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelString23.Size = new System.Drawing.Size(65, 18);
            this.obic7LabelString23.TabIndex = 1000000023;
            this.obic7LabelString23.Text = "分析2";
            this.obic7LabelString23.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.obic7LabelString23.Value = "分析2";
            this.obic7LabelString23.VisibleFalseLock = false;
            // 
            // obic7LabelString24
            // 
            this.obic7LabelString24.AutoEllipsis = true;
            this.obic7LabelString24.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelString24.ColumnNumber = 0;
            this.obic7LabelString24.EnableFalseLock = false;
            this.obic7LabelString24.ErrorString = null;
            this.obic7LabelString24.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelString24.HasHelp = false;
            this.obic7LabelString24.Location = new System.Drawing.Point(378, 41);
            this.obic7LabelString24.Name = "obic7LabelString24";
            this.obic7LabelString24.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelString24.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelString24.Size = new System.Drawing.Size(65, 18);
            this.obic7LabelString24.TabIndex = 1000000024;
            this.obic7LabelString24.Text = "分析3";
            this.obic7LabelString24.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.obic7LabelString24.Value = "分析3";
            this.obic7LabelString24.VisibleFalseLock = false;
            // 
            // obic7LabelString25
            // 
            this.obic7LabelString25.AutoEllipsis = true;
            this.obic7LabelString25.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelString25.ColumnNumber = 0;
            this.obic7LabelString25.EnableFalseLock = false;
            this.obic7LabelString25.ErrorString = null;
            this.obic7LabelString25.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelString25.HasHelp = false;
            this.obic7LabelString25.Location = new System.Drawing.Point(444, 41);
            this.obic7LabelString25.Name = "obic7LabelString25";
            this.obic7LabelString25.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelString25.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelString25.Size = new System.Drawing.Size(65, 18);
            this.obic7LabelString25.TabIndex = 1000000025;
            this.obic7LabelString25.Text = "資金";
            this.obic7LabelString25.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.obic7LabelString25.Value = "資金";
            this.obic7LabelString25.VisibleFalseLock = false;
            // 
            // obic7PanelLine
            // 
            this.obic7PanelLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKariTorihikisakiCD);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKariUtiwakeCD);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKariHojoCD);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKariKanjouCD);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKasiMemo);
            this.obic7PanelLine.Controls.Add(this.obic7TextNumberKasiMemoBValue);
            this.obic7PanelLine.Controls.Add(this.obic7TextNumberKasiMemoGValue);
            this.obic7PanelLine.Controls.Add(this.obic7TextNumberKasiMemoRValue);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKariMemo);
            this.obic7PanelLine.Controls.Add(this.obic7TextNumberKariMemoBValue);
            this.obic7PanelLine.Controls.Add(this.obic7TextNumberKariMemoGValue);
            this.obic7PanelLine.Controls.Add(this.obic7TextNumberKariMemoRValue);
            this.obic7PanelLine.Controls.Add(this.obic7ButtonWithLogicKasiMemo);
            this.obic7PanelLine.Controls.Add(this.obic7ButtonWithLogicKariMemo);
            this.obic7PanelLine.Controls.Add(this.obic7LabelNumberGyou);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKariProjectCD);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKasiProjectCD);
            this.obic7PanelLine.Controls.Add(this.obic7TextSeparatorKasiUtiwake);
            this.obic7PanelLine.Controls.Add(this.obic7TextSeparatorKasiHojo);
            this.obic7PanelLine.Controls.Add(this.obic7TextSeparatorKariUtiwake);
            this.obic7PanelLine.Controls.Add(this.obic7TextSeparatorKariHojo);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKariSikinCD);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKariBunsekiCD3);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKariBunsekiCD2);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKariBunsekiCD1);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKariTorihikisakiShanaiName);
            this.obic7PanelLine.Controls.Add(this.obic7TextNumberKariZeigaku);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKariZeiHontaiNameRyaku);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKariZeiHontaiCD);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKariUtiwakeNameRyaku);
            this.obic7PanelLine.Controls.Add(this.obic7TextNumberKariKingaku);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKariBumonNameRyaku);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKariBumonCD);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKariHojoNameRyaku);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKariZeikomiKBNName);
            this.obic7PanelLine.Controls.Add(this.obic7TextNumberKariZeikomiKBN);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKariZeiKBN);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKariZigyoushoNameRyaku);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKariZigyoushoCD);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKariKanjouNameRyaku);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKasiSikinCD);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKasiBunsekiCD3);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKasiBunsekiCD2);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKasiBunsekiCD1);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKasiTorihikisakiShanaiName);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKasiTorihikisakiCD);
            this.obic7PanelLine.Controls.Add(this.obic7TextNumberKasiZeigaku);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKasiZeiHontaiNameRyaku);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKasiZeiHontaiCD);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKasiUtiwakeNameRyaku);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKasiUtiwakeCD);
            this.obic7PanelLine.Controls.Add(this.obic7TextNumberKasiKingaku);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKasiBumonNameRyaku);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKasiBumonCD);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKasiHojoNameRyaku);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKasiHojoCD);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKasiZeikomiKBNName);
            this.obic7PanelLine.Controls.Add(this.obic7TextNumberKasiZeikomiKBN);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKasiZeiKBN);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKasiZigyoushoNameRyaku);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKasiZigyoushoCD);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKasiKanjouNameRyaku);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKasiKanjouCD);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKasiBunsekiCD4);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKariBunsekiCD4);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKasiBunsekiCD5);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringKariBunsekiCD5);
            this.obic7PanelLine.Controls.Add(this.obic7TextNumberGyouNO);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringMeisaiTekiyou);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringDummy);
            this.obic7PanelLine.Controls.Add(this.obic7TextStringDummy2);
            this.obic7PanelLine.EnableFalseLock = false;
            this.obic7PanelLine.Location = new System.Drawing.Point(5, 60);
            this.obic7PanelLine.Name = "obic7PanelLine";
            this.obic7PanelLine.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7PanelLine.Size = new System.Drawing.Size(1195, 72);
            this.obic7PanelLine.TabIndex = 3010;
            this.obic7PanelLine.VisibleFalseLock = false;
            // 
            // obic7TextStringKariTorihikisakiCD
            // 
            this.obic7TextStringKariTorihikisakiCD.AllowAlphabet = true;
            this.obic7TextStringKariTorihikisakiCD.AllowKana = false;
            this.obic7TextStringKariTorihikisakiCD.AllowNumber = true;
            this.obic7TextStringKariTorihikisakiCD.AllowSymbol = true;
            this.obic7TextStringKariTorihikisakiCD.AllowUnicode = false;
            this.obic7TextStringKariTorihikisakiCD.AllowZenkaku = false;
            this.obic7TextStringKariTorihikisakiCD.AttachedButton = null;
            this.obic7TextStringKariTorihikisakiCD.AttachedCopyText = null;
            this.obic7TextStringKariTorihikisakiCD.AttachedLabel = null;
            this.obic7TextStringKariTorihikisakiCD.AttachedSearchDialog = null;
            this.obic7TextStringKariTorihikisakiCD.CheckOnClose = false;
            this.obic7TextStringKariTorihikisakiCD.ColumnNumber = 6;
            this.obic7TextStringKariTorihikisakiCD.DispFuriganaText = null;
            this.obic7TextStringKariTorihikisakiCD.EnableFalseLock = false;
            this.obic7TextStringKariTorihikisakiCD.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariTorihikisakiCD.ErrorString = null;
            this.obic7TextStringKariTorihikisakiCD.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringKariTorihikisakiCD.IsKeyColumn = false;
            this.obic7TextStringKariTorihikisakiCD.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariTorihikisakiCD.Location = new System.Drawing.Point(48, 53);
            this.obic7TextStringKariTorihikisakiCD.MaxLengthByte = 10;
            this.obic7TextStringKariTorihikisakiCD.Name = "obic7TextStringKariTorihikisakiCD";
            this.obic7TextStringKariTorihikisakiCD.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKariTorihikisakiCD.Size = new System.Drawing.Size(67, 19);
            this.obic7TextStringKariTorihikisakiCD.TabIndex = 3160;
            this.obic7TextStringKariTorihikisakiCD.Value = "";
            this.obic7TextStringKariTorihikisakiCD.VisibleFalseLock = false;
            // 
            // obic7TextStringKariUtiwakeCD
            // 
            this.obic7TextStringKariUtiwakeCD.AllowAlphabet = true;
            this.obic7TextStringKariUtiwakeCD.AllowKana = false;
            this.obic7TextStringKariUtiwakeCD.AllowNumber = true;
            this.obic7TextStringKariUtiwakeCD.AllowSymbol = true;
            this.obic7TextStringKariUtiwakeCD.AllowUnicode = false;
            this.obic7TextStringKariUtiwakeCD.AllowZenkaku = false;
            this.obic7TextStringKariUtiwakeCD.AttachedButton = null;
            this.obic7TextStringKariUtiwakeCD.AttachedCopyText = null;
            this.obic7TextStringKariUtiwakeCD.AttachedLabel = null;
            this.obic7TextStringKariUtiwakeCD.AttachedSearchDialog = null;
            this.obic7TextStringKariUtiwakeCD.CheckOnClose = false;
            this.obic7TextStringKariUtiwakeCD.ColumnNumber = 4;
            this.obic7TextStringKariUtiwakeCD.DispFuriganaText = null;
            this.obic7TextStringKariUtiwakeCD.EnableFalseLock = false;
            this.obic7TextStringKariUtiwakeCD.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariUtiwakeCD.ErrorString = null;
            this.obic7TextStringKariUtiwakeCD.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringKariUtiwakeCD.IsKeyColumn = false;
            this.obic7TextStringKariUtiwakeCD.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariUtiwakeCD.Location = new System.Drawing.Point(48, 35);
            this.obic7TextStringKariUtiwakeCD.MaxLengthByte = 10;
            this.obic7TextStringKariUtiwakeCD.Name = "obic7TextStringKariUtiwakeCD";
            this.obic7TextStringKariUtiwakeCD.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKariUtiwakeCD.Size = new System.Drawing.Size(67, 19);
            this.obic7TextStringKariUtiwakeCD.TabIndex = 3140;
            this.obic7TextStringKariUtiwakeCD.Value = "";
            this.obic7TextStringKariUtiwakeCD.VisibleFalseLock = false;
            // 
            // obic7TextStringKariHojoCD
            // 
            this.obic7TextStringKariHojoCD.AllowAlphabet = true;
            this.obic7TextStringKariHojoCD.AllowKana = false;
            this.obic7TextStringKariHojoCD.AllowNumber = true;
            this.obic7TextStringKariHojoCD.AllowSymbol = true;
            this.obic7TextStringKariHojoCD.AllowUnicode = false;
            this.obic7TextStringKariHojoCD.AllowZenkaku = false;
            this.obic7TextStringKariHojoCD.AttachedButton = null;
            this.obic7TextStringKariHojoCD.AttachedCopyText = null;
            this.obic7TextStringKariHojoCD.AttachedLabel = null;
            this.obic7TextStringKariHojoCD.AttachedSearchDialog = null;
            this.obic7TextStringKariHojoCD.CheckOnClose = false;
            this.obic7TextStringKariHojoCD.ColumnNumber = 2;
            this.obic7TextStringKariHojoCD.DispFuriganaText = null;
            this.obic7TextStringKariHojoCD.EnableFalseLock = false;
            this.obic7TextStringKariHojoCD.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariHojoCD.ErrorString = null;
            this.obic7TextStringKariHojoCD.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringKariHojoCD.IsKeyColumn = false;
            this.obic7TextStringKariHojoCD.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariHojoCD.Location = new System.Drawing.Point(48, 17);
            this.obic7TextStringKariHojoCD.MaxLengthByte = 10;
            this.obic7TextStringKariHojoCD.Name = "obic7TextStringKariHojoCD";
            this.obic7TextStringKariHojoCD.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKariHojoCD.Size = new System.Drawing.Size(67, 19);
            this.obic7TextStringKariHojoCD.TabIndex = 3120;
            this.obic7TextStringKariHojoCD.Value = "";
            this.obic7TextStringKariHojoCD.VisibleFalseLock = false;
            // 
            // obic7TextStringKariKanjouCD
            // 
            this.obic7TextStringKariKanjouCD.AllowAlphabet = true;
            this.obic7TextStringKariKanjouCD.AllowKana = false;
            this.obic7TextStringKariKanjouCD.AllowNumber = true;
            this.obic7TextStringKariKanjouCD.AllowSymbol = false;
            this.obic7TextStringKariKanjouCD.AllowUnicode = false;
            this.obic7TextStringKariKanjouCD.AllowZenkaku = false;
            this.obic7TextStringKariKanjouCD.AttachedButton = null;
            this.obic7TextStringKariKanjouCD.AttachedCopyText = null;
            this.obic7TextStringKariKanjouCD.AttachedLabel = null;
            this.obic7TextStringKariKanjouCD.AttachedSearchDialog = null;
            this.obic7TextStringKariKanjouCD.CheckOnClose = false;
            this.obic7TextStringKariKanjouCD.ColumnNumber = 0;
            this.obic7TextStringKariKanjouCD.DispFuriganaText = null;
            this.obic7TextStringKariKanjouCD.EnableFalseLock = false;
            this.obic7TextStringKariKanjouCD.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariKanjouCD.ErrorString = null;
            this.obic7TextStringKariKanjouCD.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringKariKanjouCD.IsKeyColumn = false;
            this.obic7TextStringKariKanjouCD.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariKanjouCD.Location = new System.Drawing.Point(48, -1);
            this.obic7TextStringKariKanjouCD.MaxLengthByte = 10;
            this.obic7TextStringKariKanjouCD.Name = "obic7TextStringKariKanjouCD";
            this.obic7TextStringKariKanjouCD.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKariKanjouCD.Size = new System.Drawing.Size(67, 19);
            this.obic7TextStringKariKanjouCD.TabIndex = 3100;
            this.obic7TextStringKariKanjouCD.Value = "";
            this.obic7TextStringKariKanjouCD.VisibleFalseLock = false;
            // 
            // obic7TextStringKasiMemo
            // 
            this.obic7TextStringKasiMemo.AllowAlphabet = true;
            this.obic7TextStringKasiMemo.AllowKana = true;
            this.obic7TextStringKasiMemo.AllowNumber = true;
            this.obic7TextStringKasiMemo.AllowSymbol = true;
            this.obic7TextStringKasiMemo.AllowUnicode = false;
            this.obic7TextStringKasiMemo.AllowZenkaku = true;
            this.obic7TextStringKasiMemo.AttachedButton = null;
            this.obic7TextStringKasiMemo.AttachedCopyText = null;
            this.obic7TextStringKasiMemo.AttachedLabel = null;
            this.obic7TextStringKasiMemo.AttachedSearchDialog = null;
            this.obic7TextStringKasiMemo.BackColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiMemo.CheckOnClose = false;
            this.obic7TextStringKasiMemo.ColumnNumber = 63;
            this.obic7TextStringKasiMemo.DispFuriganaText = null;
            this.obic7TextStringKasiMemo.EnableFalseLock = false;
            this.obic7TextStringKasiMemo.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiMemo.ErrorString = null;
            this.obic7TextStringKasiMemo.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.obic7TextStringKasiMemo.IsKeyColumn = false;
            this.obic7TextStringKasiMemo.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiMemo.Location = new System.Drawing.Point(708, 80);
            this.obic7TextStringKasiMemo.MaxLengthByte = 40;
            this.obic7TextStringKasiMemo.Name = "obic7TextStringKasiMemo";
            this.obic7TextStringKasiMemo.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKasiMemo.Size = new System.Drawing.Size(30, 19);
            this.obic7TextStringKasiMemo.TabIndex = 3855;
            this.obic7TextStringKasiMemo.TabStop = false;
            this.obic7TextStringKasiMemo.Value = "";
            this.obic7TextStringKasiMemo.Visible = false;
            this.obic7TextStringKasiMemo.VisibleFalseLock = true;
            // 
            // obic7TextNumberKasiMemoBValue
            // 
            this.obic7TextNumberKasiMemoBValue.AttachedButton = null;
            this.obic7TextNumberKasiMemoBValue.AttachedCopyText = null;
            this.obic7TextNumberKasiMemoBValue.AttachedLabel = null;
            this.obic7TextNumberKasiMemoBValue.AttachedSearchDialog = null;
            this.obic7TextNumberKasiMemoBValue.BackColor = System.Drawing.Color.Blue;
            this.obic7TextNumberKasiMemoBValue.CheckOnClose = false;
            this.obic7TextNumberKasiMemoBValue.ColumnNumber = 62;
            this.obic7TextNumberKasiMemoBValue.EnableFalseLock = false;
            this.obic7TextNumberKasiMemoBValue.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberKasiMemoBValue.ErrorString = null;
            this.obic7TextNumberKasiMemoBValue.ForeColor = System.Drawing.SystemColors.WindowText;
            this.obic7TextNumberKasiMemoBValue.Format = "";
            this.obic7TextNumberKasiMemoBValue.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.obic7TextNumberKasiMemoBValue.IsKeyColumn = false;
            this.obic7TextNumberKasiMemoBValue.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberKasiMemoBValue.Location = new System.Drawing.Point(689, 80);
            this.obic7TextNumberKasiMemoBValue.MaxLengthByte = 3;
            this.obic7TextNumberKasiMemoBValue.MaxValue = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.obic7TextNumberKasiMemoBValue.MinusColor = System.Drawing.Color.Red;
            this.obic7TextNumberKasiMemoBValue.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.obic7TextNumberKasiMemoBValue.Name = "obic7TextNumberKasiMemoBValue";
            this.obic7TextNumberKasiMemoBValue.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextNumberKasiMemoBValue.Size = new System.Drawing.Size(18, 19);
            this.obic7TextNumberKasiMemoBValue.TabIndex = 3854;
            this.obic7TextNumberKasiMemoBValue.TabStop = false;
            this.obic7TextNumberKasiMemoBValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.obic7TextNumberKasiMemoBValue.Value = null;
            this.obic7TextNumberKasiMemoBValue.Visible = false;
            this.obic7TextNumberKasiMemoBValue.VisibleFalseLock = true;
            // 
            // obic7TextNumberKasiMemoGValue
            // 
            this.obic7TextNumberKasiMemoGValue.AttachedButton = null;
            this.obic7TextNumberKasiMemoGValue.AttachedCopyText = null;
            this.obic7TextNumberKasiMemoGValue.AttachedLabel = null;
            this.obic7TextNumberKasiMemoGValue.AttachedSearchDialog = null;
            this.obic7TextNumberKasiMemoGValue.BackColor = System.Drawing.Color.Lime;
            this.obic7TextNumberKasiMemoGValue.CheckOnClose = false;
            this.obic7TextNumberKasiMemoGValue.ColumnNumber = 61;
            this.obic7TextNumberKasiMemoGValue.EnableFalseLock = false;
            this.obic7TextNumberKasiMemoGValue.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberKasiMemoGValue.ErrorString = null;
            this.obic7TextNumberKasiMemoGValue.ForeColor = System.Drawing.SystemColors.WindowText;
            this.obic7TextNumberKasiMemoGValue.Format = "";
            this.obic7TextNumberKasiMemoGValue.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.obic7TextNumberKasiMemoGValue.IsKeyColumn = false;
            this.obic7TextNumberKasiMemoGValue.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberKasiMemoGValue.Location = new System.Drawing.Point(670, 80);
            this.obic7TextNumberKasiMemoGValue.MaxLengthByte = 3;
            this.obic7TextNumberKasiMemoGValue.MaxValue = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.obic7TextNumberKasiMemoGValue.MinusColor = System.Drawing.Color.Red;
            this.obic7TextNumberKasiMemoGValue.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.obic7TextNumberKasiMemoGValue.Name = "obic7TextNumberKasiMemoGValue";
            this.obic7TextNumberKasiMemoGValue.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextNumberKasiMemoGValue.Size = new System.Drawing.Size(18, 19);
            this.obic7TextNumberKasiMemoGValue.TabIndex = 3853;
            this.obic7TextNumberKasiMemoGValue.TabStop = false;
            this.obic7TextNumberKasiMemoGValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.obic7TextNumberKasiMemoGValue.Value = null;
            this.obic7TextNumberKasiMemoGValue.Visible = false;
            this.obic7TextNumberKasiMemoGValue.VisibleFalseLock = true;
            // 
            // obic7TextNumberKasiMemoRValue
            // 
            this.obic7TextNumberKasiMemoRValue.AttachedButton = null;
            this.obic7TextNumberKasiMemoRValue.AttachedCopyText = null;
            this.obic7TextNumberKasiMemoRValue.AttachedLabel = null;
            this.obic7TextNumberKasiMemoRValue.AttachedSearchDialog = null;
            this.obic7TextNumberKasiMemoRValue.BackColor = System.Drawing.Color.Red;
            this.obic7TextNumberKasiMemoRValue.CheckOnClose = false;
            this.obic7TextNumberKasiMemoRValue.ColumnNumber = 60;
            this.obic7TextNumberKasiMemoRValue.EnableFalseLock = false;
            this.obic7TextNumberKasiMemoRValue.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberKasiMemoRValue.ErrorString = null;
            this.obic7TextNumberKasiMemoRValue.ForeColor = System.Drawing.SystemColors.WindowText;
            this.obic7TextNumberKasiMemoRValue.Format = "";
            this.obic7TextNumberKasiMemoRValue.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.obic7TextNumberKasiMemoRValue.IsKeyColumn = false;
            this.obic7TextNumberKasiMemoRValue.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberKasiMemoRValue.Location = new System.Drawing.Point(651, 80);
            this.obic7TextNumberKasiMemoRValue.MaxLengthByte = 3;
            this.obic7TextNumberKasiMemoRValue.MaxValue = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.obic7TextNumberKasiMemoRValue.MinusColor = System.Drawing.Color.Red;
            this.obic7TextNumberKasiMemoRValue.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.obic7TextNumberKasiMemoRValue.Name = "obic7TextNumberKasiMemoRValue";
            this.obic7TextNumberKasiMemoRValue.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextNumberKasiMemoRValue.Size = new System.Drawing.Size(18, 19);
            this.obic7TextNumberKasiMemoRValue.TabIndex = 3852;
            this.obic7TextNumberKasiMemoRValue.TabStop = false;
            this.obic7TextNumberKasiMemoRValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.obic7TextNumberKasiMemoRValue.Value = null;
            this.obic7TextNumberKasiMemoRValue.Visible = false;
            this.obic7TextNumberKasiMemoRValue.VisibleFalseLock = true;
            // 
            // obic7TextStringKariMemo
            // 
            this.obic7TextStringKariMemo.AllowAlphabet = true;
            this.obic7TextStringKariMemo.AllowKana = true;
            this.obic7TextStringKariMemo.AllowNumber = true;
            this.obic7TextStringKariMemo.AllowSymbol = true;
            this.obic7TextStringKariMemo.AllowUnicode = false;
            this.obic7TextStringKariMemo.AllowZenkaku = true;
            this.obic7TextStringKariMemo.AttachedButton = null;
            this.obic7TextStringKariMemo.AttachedCopyText = null;
            this.obic7TextStringKariMemo.AttachedLabel = null;
            this.obic7TextStringKariMemo.AttachedSearchDialog = null;
            this.obic7TextStringKariMemo.BackColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariMemo.CheckOnClose = false;
            this.obic7TextStringKariMemo.ColumnNumber = 59;
            this.obic7TextStringKariMemo.DispFuriganaText = null;
            this.obic7TextStringKariMemo.EnableFalseLock = false;
            this.obic7TextStringKariMemo.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariMemo.ErrorString = null;
            this.obic7TextStringKariMemo.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.obic7TextStringKariMemo.IsKeyColumn = false;
            this.obic7TextStringKariMemo.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariMemo.Location = new System.Drawing.Point(182, 80);
            this.obic7TextStringKariMemo.MaxLengthByte = 40;
            this.obic7TextStringKariMemo.Name = "obic7TextStringKariMemo";
            this.obic7TextStringKariMemo.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKariMemo.Size = new System.Drawing.Size(30, 19);
            this.obic7TextStringKariMemo.TabIndex = 3095;
            this.obic7TextStringKariMemo.TabStop = false;
            this.obic7TextStringKariMemo.Value = "";
            this.obic7TextStringKariMemo.Visible = false;
            this.obic7TextStringKariMemo.VisibleFalseLock = true;
            // 
            // obic7TextNumberKariMemoBValue
            // 
            this.obic7TextNumberKariMemoBValue.AttachedButton = null;
            this.obic7TextNumberKariMemoBValue.AttachedCopyText = null;
            this.obic7TextNumberKariMemoBValue.AttachedLabel = null;
            this.obic7TextNumberKariMemoBValue.AttachedSearchDialog = null;
            this.obic7TextNumberKariMemoBValue.BackColor = System.Drawing.Color.Blue;
            this.obic7TextNumberKariMemoBValue.CheckOnClose = false;
            this.obic7TextNumberKariMemoBValue.ColumnNumber = 58;
            this.obic7TextNumberKariMemoBValue.EnableFalseLock = false;
            this.obic7TextNumberKariMemoBValue.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberKariMemoBValue.ErrorString = null;
            this.obic7TextNumberKariMemoBValue.ForeColor = System.Drawing.SystemColors.WindowText;
            this.obic7TextNumberKariMemoBValue.Format = "";
            this.obic7TextNumberKariMemoBValue.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.obic7TextNumberKariMemoBValue.IsKeyColumn = false;
            this.obic7TextNumberKariMemoBValue.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberKariMemoBValue.Location = new System.Drawing.Point(163, 80);
            this.obic7TextNumberKariMemoBValue.MaxLengthByte = 3;
            this.obic7TextNumberKariMemoBValue.MaxValue = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.obic7TextNumberKariMemoBValue.MinusColor = System.Drawing.Color.Red;
            this.obic7TextNumberKariMemoBValue.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.obic7TextNumberKariMemoBValue.Name = "obic7TextNumberKariMemoBValue";
            this.obic7TextNumberKariMemoBValue.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextNumberKariMemoBValue.Size = new System.Drawing.Size(18, 19);
            this.obic7TextNumberKariMemoBValue.TabIndex = 3094;
            this.obic7TextNumberKariMemoBValue.TabStop = false;
            this.obic7TextNumberKariMemoBValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.obic7TextNumberKariMemoBValue.Value = null;
            this.obic7TextNumberKariMemoBValue.Visible = false;
            this.obic7TextNumberKariMemoBValue.VisibleFalseLock = true;
            // 
            // obic7TextNumberKariMemoGValue
            // 
            this.obic7TextNumberKariMemoGValue.AttachedButton = null;
            this.obic7TextNumberKariMemoGValue.AttachedCopyText = null;
            this.obic7TextNumberKariMemoGValue.AttachedLabel = null;
            this.obic7TextNumberKariMemoGValue.AttachedSearchDialog = null;
            this.obic7TextNumberKariMemoGValue.BackColor = System.Drawing.Color.Lime;
            this.obic7TextNumberKariMemoGValue.CheckOnClose = false;
            this.obic7TextNumberKariMemoGValue.ColumnNumber = 57;
            this.obic7TextNumberKariMemoGValue.EnableFalseLock = false;
            this.obic7TextNumberKariMemoGValue.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberKariMemoGValue.ErrorString = null;
            this.obic7TextNumberKariMemoGValue.ForeColor = System.Drawing.SystemColors.WindowText;
            this.obic7TextNumberKariMemoGValue.Format = "";
            this.obic7TextNumberKariMemoGValue.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.obic7TextNumberKariMemoGValue.IsKeyColumn = false;
            this.obic7TextNumberKariMemoGValue.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberKariMemoGValue.Location = new System.Drawing.Point(144, 80);
            this.obic7TextNumberKariMemoGValue.MaxLengthByte = 3;
            this.obic7TextNumberKariMemoGValue.MaxValue = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.obic7TextNumberKariMemoGValue.MinusColor = System.Drawing.Color.Red;
            this.obic7TextNumberKariMemoGValue.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.obic7TextNumberKariMemoGValue.Name = "obic7TextNumberKariMemoGValue";
            this.obic7TextNumberKariMemoGValue.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextNumberKariMemoGValue.Size = new System.Drawing.Size(18, 19);
            this.obic7TextNumberKariMemoGValue.TabIndex = 3093;
            this.obic7TextNumberKariMemoGValue.TabStop = false;
            this.obic7TextNumberKariMemoGValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.obic7TextNumberKariMemoGValue.Value = null;
            this.obic7TextNumberKariMemoGValue.Visible = false;
            this.obic7TextNumberKariMemoGValue.VisibleFalseLock = true;
            // 
            // obic7TextNumberKariMemoRValue
            // 
            this.obic7TextNumberKariMemoRValue.AttachedButton = null;
            this.obic7TextNumberKariMemoRValue.AttachedCopyText = null;
            this.obic7TextNumberKariMemoRValue.AttachedLabel = null;
            this.obic7TextNumberKariMemoRValue.AttachedSearchDialog = null;
            this.obic7TextNumberKariMemoRValue.BackColor = System.Drawing.Color.Red;
            this.obic7TextNumberKariMemoRValue.CheckOnClose = false;
            this.obic7TextNumberKariMemoRValue.ColumnNumber = 56;
            this.obic7TextNumberKariMemoRValue.EnableFalseLock = false;
            this.obic7TextNumberKariMemoRValue.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberKariMemoRValue.ErrorString = null;
            this.obic7TextNumberKariMemoRValue.ForeColor = System.Drawing.SystemColors.WindowText;
            this.obic7TextNumberKariMemoRValue.Format = "";
            this.obic7TextNumberKariMemoRValue.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.obic7TextNumberKariMemoRValue.IsKeyColumn = false;
            this.obic7TextNumberKariMemoRValue.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberKariMemoRValue.Location = new System.Drawing.Point(125, 80);
            this.obic7TextNumberKariMemoRValue.MaxLengthByte = 3;
            this.obic7TextNumberKariMemoRValue.MaxValue = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.obic7TextNumberKariMemoRValue.MinusColor = System.Drawing.Color.Red;
            this.obic7TextNumberKariMemoRValue.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.obic7TextNumberKariMemoRValue.Name = "obic7TextNumberKariMemoRValue";
            this.obic7TextNumberKariMemoRValue.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextNumberKariMemoRValue.Size = new System.Drawing.Size(18, 19);
            this.obic7TextNumberKariMemoRValue.TabIndex = 3092;
            this.obic7TextNumberKariMemoRValue.TabStop = false;
            this.obic7TextNumberKariMemoRValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.obic7TextNumberKariMemoRValue.Value = null;
            this.obic7TextNumberKariMemoRValue.Visible = false;
            this.obic7TextNumberKariMemoRValue.VisibleFalseLock = true;
            // 
            // obic7ButtonWithLogicKasiMemo
            // 
            this.obic7ButtonWithLogicKasiMemo.ColumnNumber = -1;
            this.obic7ButtonWithLogicKasiMemo.EnableFalseLock = false;
            this.obic7ButtonWithLogicKasiMemo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.obic7ButtonWithLogicKasiMemo.Location = new System.Drawing.Point(1091, -1);
            this.obic7ButtonWithLogicKasiMemo.Name = "obic7ButtonWithLogicKasiMemo";
            this.obic7ButtonWithLogicKasiMemo.Obic7JobSecurity = Obic7fx.Com.A0000.Tool.Obic7JobSecurities.NoSecurity;
            this.obic7ButtonWithLogicKasiMemo.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Original;
            this.obic7ButtonWithLogicKasiMemo.Size = new System.Drawing.Size(18, 71);
            this.obic7ButtonWithLogicKasiMemo.TabIndex = 3851;
            this.obic7ButtonWithLogicKasiMemo.UseVisualStyleBackColor = true;
            this.obic7ButtonWithLogicKasiMemo.VisibleFalseLock = false;
            // 
            // obic7ButtonWithLogicKariMemo
            // 
            this.obic7ButtonWithLogicKariMemo.ColumnNumber = -1;
            this.obic7ButtonWithLogicKariMemo.EnableFalseLock = false;
            this.obic7ButtonWithLogicKariMemo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.obic7ButtonWithLogicKariMemo.Location = new System.Drawing.Point(30, -1);
            this.obic7ButtonWithLogicKariMemo.Name = "obic7ButtonWithLogicKariMemo";
            this.obic7ButtonWithLogicKariMemo.Obic7JobSecurity = Obic7fx.Com.A0000.Tool.Obic7JobSecurities.NoSecurity;
            this.obic7ButtonWithLogicKariMemo.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Original;
            this.obic7ButtonWithLogicKariMemo.Size = new System.Drawing.Size(18, 71);
            this.obic7ButtonWithLogicKariMemo.TabIndex = 3091;
            this.obic7ButtonWithLogicKariMemo.UseVisualStyleBackColor = true;
            this.obic7ButtonWithLogicKariMemo.VisibleFalseLock = false;
            // 
            // obic7LabelNumberGyou
            // 
            this.obic7LabelNumberGyou.AutoEllipsis = true;
            this.obic7LabelNumberGyou.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.obic7LabelNumberGyou.ColumnNumber = -1;
            this.obic7LabelNumberGyou.EnableFalseLock = false;
            this.obic7LabelNumberGyou.ErrorString = null;
            this.obic7LabelNumberGyou.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelNumberGyou.Format = "";
            this.obic7LabelNumberGyou.HasHelp = false;
            this.obic7LabelNumberGyou.Location = new System.Drawing.Point(-1, -1);
            this.obic7LabelNumberGyou.MinusColor = System.Drawing.Color.Red;
            this.obic7LabelNumberGyou.Name = "obic7LabelNumberGyou";
            this.obic7LabelNumberGyou.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7LabelNumberGyou.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelNumberGyou.Size = new System.Drawing.Size(30, 71);
            this.obic7LabelNumberGyou.TabIndex = 3090;
            this.obic7LabelNumberGyou.Text = "0";
            this.obic7LabelNumberGyou.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.obic7LabelNumberGyou.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.obic7LabelNumberGyou.Visible = false;
            this.obic7LabelNumberGyou.VisibleFalseLock = false;
            // 
            // obic7TextStringKariProjectCD
            // 
            this.obic7TextStringKariProjectCD.AllowAlphabet = true;
            this.obic7TextStringKariProjectCD.AllowKana = true;
            this.obic7TextStringKariProjectCD.AllowNumber = true;
            this.obic7TextStringKariProjectCD.AllowSymbol = true;
            this.obic7TextStringKariProjectCD.AllowUnicode = false;
            this.obic7TextStringKariProjectCD.AllowZenkaku = true;
            this.obic7TextStringKariProjectCD.AttachedButton = null;
            this.obic7TextStringKariProjectCD.AttachedCopyText = null;
            this.obic7TextStringKariProjectCD.AttachedLabel = null;
            this.obic7TextStringKariProjectCD.AttachedSearchDialog = null;
            this.obic7TextStringKariProjectCD.CheckOnClose = false;
            this.obic7TextStringKariProjectCD.ColumnNumber = 50;
            this.obic7TextStringKariProjectCD.DispFuriganaText = null;
            this.obic7TextStringKariProjectCD.EnableFalseLock = false;
            this.obic7TextStringKariProjectCD.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariProjectCD.ErrorString = null;
            this.obic7TextStringKariProjectCD.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringKariProjectCD.IsKeyColumn = false;
            this.obic7TextStringKariProjectCD.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariProjectCD.Location = new System.Drawing.Point(504, 53);
            this.obic7TextStringKariProjectCD.MaxLengthByte = 30;
            this.obic7TextStringKariProjectCD.Name = "obic7TextStringKariProjectCD";
            this.obic7TextStringKariProjectCD.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKariProjectCD.Size = new System.Drawing.Size(66, 19);
            this.obic7TextStringKariProjectCD.TabIndex = 3350;
            this.obic7TextStringKariProjectCD.Value = "";
            this.obic7TextStringKariProjectCD.VisibleFalseLock = false;
            // 
            // obic7TextStringKasiProjectCD
            // 
            this.obic7TextStringKasiProjectCD.AllowAlphabet = true;
            this.obic7TextStringKasiProjectCD.AllowKana = true;
            this.obic7TextStringKasiProjectCD.AllowNumber = true;
            this.obic7TextStringKasiProjectCD.AllowSymbol = true;
            this.obic7TextStringKasiProjectCD.AllowUnicode = false;
            this.obic7TextStringKasiProjectCD.AllowZenkaku = true;
            this.obic7TextStringKasiProjectCD.AttachedButton = null;
            this.obic7TextStringKasiProjectCD.AttachedCopyText = null;
            this.obic7TextStringKasiProjectCD.AttachedLabel = null;
            this.obic7TextStringKasiProjectCD.AttachedSearchDialog = null;
            this.obic7TextStringKasiProjectCD.CheckOnClose = false;
            this.obic7TextStringKasiProjectCD.ColumnNumber = 53;
            this.obic7TextStringKasiProjectCD.DispFuriganaText = null;
            this.obic7TextStringKasiProjectCD.EnableFalseLock = false;
            this.obic7TextStringKasiProjectCD.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiProjectCD.ErrorString = null;
            this.obic7TextStringKasiProjectCD.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringKasiProjectCD.IsKeyColumn = false;
            this.obic7TextStringKasiProjectCD.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiProjectCD.Location = new System.Drawing.Point(1026, 53);
            this.obic7TextStringKasiProjectCD.MaxLengthByte = 30;
            this.obic7TextStringKasiProjectCD.Name = "obic7TextStringKasiProjectCD";
            this.obic7TextStringKasiProjectCD.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKasiProjectCD.Size = new System.Drawing.Size(66, 19);
            this.obic7TextStringKasiProjectCD.TabIndex = 3850;
            this.obic7TextStringKasiProjectCD.Value = "";
            this.obic7TextStringKasiProjectCD.VisibleFalseLock = false;
            // 
            // obic7TextSeparatorKasiUtiwake
            // 
            this.obic7TextSeparatorKasiUtiwake.ColumnNumber = 0;
            this.obic7TextSeparatorKasiUtiwake.IsRefrectLowerOrder = false;
            this.obic7TextSeparatorKasiUtiwake.IsRefrectUpperOrder = false;
            this.obic7TextSeparatorKasiUtiwake.IsTerminator = false;
            this.obic7TextSeparatorKasiUtiwake.Location = new System.Drawing.Point(459, 28);
            this.obic7TextSeparatorKasiUtiwake.Name = "obic7TextSeparatorKasiUtiwake";
            this.obic7TextSeparatorKasiUtiwake.Size = new System.Drawing.Size(0, 19);
            this.obic7TextSeparatorKasiUtiwake.TabIndex = 3625;
            // 
            // obic7TextSeparatorKasiHojo
            // 
            this.obic7TextSeparatorKasiHojo.ColumnNumber = 0;
            this.obic7TextSeparatorKasiHojo.IsRefrectLowerOrder = false;
            this.obic7TextSeparatorKasiHojo.IsRefrectUpperOrder = false;
            this.obic7TextSeparatorKasiHojo.IsTerminator = false;
            this.obic7TextSeparatorKasiHojo.Location = new System.Drawing.Point(459, 10);
            this.obic7TextSeparatorKasiHojo.Name = "obic7TextSeparatorKasiHojo";
            this.obic7TextSeparatorKasiHojo.Size = new System.Drawing.Size(0, 19);
            this.obic7TextSeparatorKasiHojo.TabIndex = 3605;
            // 
            // obic7TextSeparatorKariUtiwake
            // 
            this.obic7TextSeparatorKariUtiwake.ColumnNumber = 0;
            this.obic7TextSeparatorKariUtiwake.IsRefrectLowerOrder = false;
            this.obic7TextSeparatorKariUtiwake.IsRefrectUpperOrder = false;
            this.obic7TextSeparatorKariUtiwake.IsTerminator = false;
            this.obic7TextSeparatorKariUtiwake.Location = new System.Drawing.Point(37, 28);
            this.obic7TextSeparatorKariUtiwake.Name = "obic7TextSeparatorKariUtiwake";
            this.obic7TextSeparatorKariUtiwake.Size = new System.Drawing.Size(0, 19);
            this.obic7TextSeparatorKariUtiwake.TabIndex = 3125;
            // 
            // obic7TextSeparatorKariHojo
            // 
            this.obic7TextSeparatorKariHojo.ColumnNumber = 0;
            this.obic7TextSeparatorKariHojo.IsRefrectLowerOrder = false;
            this.obic7TextSeparatorKariHojo.IsRefrectUpperOrder = false;
            this.obic7TextSeparatorKariHojo.IsTerminator = false;
            this.obic7TextSeparatorKariHojo.Location = new System.Drawing.Point(27, 10);
            this.obic7TextSeparatorKariHojo.Name = "obic7TextSeparatorKariHojo";
            this.obic7TextSeparatorKariHojo.Size = new System.Drawing.Size(0, 19);
            this.obic7TextSeparatorKariHojo.TabIndex = 3105;
            // 
            // obic7TextStringKariSikinCD
            // 
            this.obic7TextStringKariSikinCD.AllowAlphabet = true;
            this.obic7TextStringKariSikinCD.AllowKana = true;
            this.obic7TextStringKariSikinCD.AllowNumber = true;
            this.obic7TextStringKariSikinCD.AllowSymbol = true;
            this.obic7TextStringKariSikinCD.AllowUnicode = false;
            this.obic7TextStringKariSikinCD.AllowZenkaku = true;
            this.obic7TextStringKariSikinCD.AttachedButton = null;
            this.obic7TextStringKariSikinCD.AttachedCopyText = null;
            this.obic7TextStringKariSikinCD.AttachedLabel = null;
            this.obic7TextStringKariSikinCD.AttachedSearchDialog = null;
            this.obic7TextStringKariSikinCD.CheckOnClose = false;
            this.obic7TextStringKariSikinCD.ColumnNumber = 22;
            this.obic7TextStringKariSikinCD.DispFuriganaText = null;
            this.obic7TextStringKariSikinCD.EnableFalseLock = false;
            this.obic7TextStringKariSikinCD.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariSikinCD.ErrorString = null;
            this.obic7TextStringKariSikinCD.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringKariSikinCD.IsKeyColumn = false;
            this.obic7TextStringKariSikinCD.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariSikinCD.Location = new System.Drawing.Point(438, 53);
            this.obic7TextStringKariSikinCD.MaxLengthByte = 30;
            this.obic7TextStringKariSikinCD.Name = "obic7TextStringKariSikinCD";
            this.obic7TextStringKariSikinCD.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKariSikinCD.Size = new System.Drawing.Size(67, 19);
            this.obic7TextStringKariSikinCD.TabIndex = 3340;
            this.obic7TextStringKariSikinCD.Value = "";
            this.obic7TextStringKariSikinCD.VisibleFalseLock = false;
            // 
            // obic7TextStringKariBunsekiCD3
            // 
            this.obic7TextStringKariBunsekiCD3.AllowAlphabet = true;
            this.obic7TextStringKariBunsekiCD3.AllowKana = true;
            this.obic7TextStringKariBunsekiCD3.AllowNumber = true;
            this.obic7TextStringKariBunsekiCD3.AllowSymbol = true;
            this.obic7TextStringKariBunsekiCD3.AllowUnicode = false;
            this.obic7TextStringKariBunsekiCD3.AllowZenkaku = true;
            this.obic7TextStringKariBunsekiCD3.AttachedButton = null;
            this.obic7TextStringKariBunsekiCD3.AttachedCopyText = null;
            this.obic7TextStringKariBunsekiCD3.AttachedLabel = null;
            this.obic7TextStringKariBunsekiCD3.AttachedSearchDialog = null;
            this.obic7TextStringKariBunsekiCD3.CheckOnClose = false;
            this.obic7TextStringKariBunsekiCD3.ColumnNumber = 21;
            this.obic7TextStringKariBunsekiCD3.DispFuriganaText = null;
            this.obic7TextStringKariBunsekiCD3.EnableFalseLock = false;
            this.obic7TextStringKariBunsekiCD3.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariBunsekiCD3.ErrorString = null;
            this.obic7TextStringKariBunsekiCD3.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringKariBunsekiCD3.IsKeyColumn = false;
            this.obic7TextStringKariBunsekiCD3.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariBunsekiCD3.Location = new System.Drawing.Point(372, 53);
            this.obic7TextStringKariBunsekiCD3.MaxLengthByte = 30;
            this.obic7TextStringKariBunsekiCD3.Name = "obic7TextStringKariBunsekiCD3";
            this.obic7TextStringKariBunsekiCD3.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKariBunsekiCD3.Size = new System.Drawing.Size(67, 19);
            this.obic7TextStringKariBunsekiCD3.TabIndex = 3310;
            this.obic7TextStringKariBunsekiCD3.Value = "";
            this.obic7TextStringKariBunsekiCD3.VisibleFalseLock = false;
            // 
            // obic7TextStringKariBunsekiCD2
            // 
            this.obic7TextStringKariBunsekiCD2.AllowAlphabet = true;
            this.obic7TextStringKariBunsekiCD2.AllowKana = true;
            this.obic7TextStringKariBunsekiCD2.AllowNumber = true;
            this.obic7TextStringKariBunsekiCD2.AllowSymbol = true;
            this.obic7TextStringKariBunsekiCD2.AllowUnicode = false;
            this.obic7TextStringKariBunsekiCD2.AllowZenkaku = true;
            this.obic7TextStringKariBunsekiCD2.AttachedButton = null;
            this.obic7TextStringKariBunsekiCD2.AttachedCopyText = null;
            this.obic7TextStringKariBunsekiCD2.AttachedLabel = null;
            this.obic7TextStringKariBunsekiCD2.AttachedSearchDialog = null;
            this.obic7TextStringKariBunsekiCD2.CheckOnClose = false;
            this.obic7TextStringKariBunsekiCD2.ColumnNumber = 20;
            this.obic7TextStringKariBunsekiCD2.DispFuriganaText = null;
            this.obic7TextStringKariBunsekiCD2.EnableFalseLock = false;
            this.obic7TextStringKariBunsekiCD2.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariBunsekiCD2.ErrorString = null;
            this.obic7TextStringKariBunsekiCD2.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringKariBunsekiCD2.IsKeyColumn = false;
            this.obic7TextStringKariBunsekiCD2.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariBunsekiCD2.Location = new System.Drawing.Point(306, 53);
            this.obic7TextStringKariBunsekiCD2.MaxLengthByte = 30;
            this.obic7TextStringKariBunsekiCD2.Name = "obic7TextStringKariBunsekiCD2";
            this.obic7TextStringKariBunsekiCD2.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKariBunsekiCD2.Size = new System.Drawing.Size(67, 19);
            this.obic7TextStringKariBunsekiCD2.TabIndex = 3300;
            this.obic7TextStringKariBunsekiCD2.Value = "";
            this.obic7TextStringKariBunsekiCD2.VisibleFalseLock = false;
            // 
            // obic7TextStringKariBunsekiCD1
            // 
            this.obic7TextStringKariBunsekiCD1.AllowAlphabet = true;
            this.obic7TextStringKariBunsekiCD1.AllowKana = true;
            this.obic7TextStringKariBunsekiCD1.AllowNumber = true;
            this.obic7TextStringKariBunsekiCD1.AllowSymbol = true;
            this.obic7TextStringKariBunsekiCD1.AllowUnicode = false;
            this.obic7TextStringKariBunsekiCD1.AllowZenkaku = true;
            this.obic7TextStringKariBunsekiCD1.AttachedButton = null;
            this.obic7TextStringKariBunsekiCD1.AttachedCopyText = null;
            this.obic7TextStringKariBunsekiCD1.AttachedLabel = null;
            this.obic7TextStringKariBunsekiCD1.AttachedSearchDialog = null;
            this.obic7TextStringKariBunsekiCD1.CheckOnClose = false;
            this.obic7TextStringKariBunsekiCD1.ColumnNumber = 19;
            this.obic7TextStringKariBunsekiCD1.DispFuriganaText = null;
            this.obic7TextStringKariBunsekiCD1.EnableFalseLock = false;
            this.obic7TextStringKariBunsekiCD1.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariBunsekiCD1.ErrorString = null;
            this.obic7TextStringKariBunsekiCD1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringKariBunsekiCD1.IsKeyColumn = false;
            this.obic7TextStringKariBunsekiCD1.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariBunsekiCD1.Location = new System.Drawing.Point(240, 53);
            this.obic7TextStringKariBunsekiCD1.MaxLengthByte = 30;
            this.obic7TextStringKariBunsekiCD1.Name = "obic7TextStringKariBunsekiCD1";
            this.obic7TextStringKariBunsekiCD1.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKariBunsekiCD1.Size = new System.Drawing.Size(67, 19);
            this.obic7TextStringKariBunsekiCD1.TabIndex = 3290;
            this.obic7TextStringKariBunsekiCD1.Value = "";
            this.obic7TextStringKariBunsekiCD1.VisibleFalseLock = false;
            // 
            // obic7TextStringKariTorihikisakiShanaiName
            // 
            this.obic7TextStringKariTorihikisakiShanaiName.AllowAlphabet = true;
            this.obic7TextStringKariTorihikisakiShanaiName.AllowKana = true;
            this.obic7TextStringKariTorihikisakiShanaiName.AllowNumber = true;
            this.obic7TextStringKariTorihikisakiShanaiName.AllowSymbol = true;
            this.obic7TextStringKariTorihikisakiShanaiName.AllowUnicode = false;
            this.obic7TextStringKariTorihikisakiShanaiName.AllowZenkaku = true;
            this.obic7TextStringKariTorihikisakiShanaiName.AttachedButton = null;
            this.obic7TextStringKariTorihikisakiShanaiName.AttachedCopyText = null;
            this.obic7TextStringKariTorihikisakiShanaiName.AttachedLabel = null;
            this.obic7TextStringKariTorihikisakiShanaiName.AttachedSearchDialog = null;
            this.obic7TextStringKariTorihikisakiShanaiName.CheckOnClose = false;
            this.obic7TextStringKariTorihikisakiShanaiName.ColumnNumber = 7;
            this.obic7TextStringKariTorihikisakiShanaiName.DispFuriganaText = null;
            this.obic7TextStringKariTorihikisakiShanaiName.EnableFalseLock = false;
            this.obic7TextStringKariTorihikisakiShanaiName.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariTorihikisakiShanaiName.ErrorString = null;
            this.obic7TextStringKariTorihikisakiShanaiName.ImeMode = System.Windows.Forms.ImeMode.On;
            this.obic7TextStringKariTorihikisakiShanaiName.IsKeyColumn = false;
            this.obic7TextStringKariTorihikisakiShanaiName.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariTorihikisakiShanaiName.Location = new System.Drawing.Point(114, 53);
            this.obic7TextStringKariTorihikisakiShanaiName.MaxLengthByte = 60;
            this.obic7TextStringKariTorihikisakiShanaiName.Name = "obic7TextStringKariTorihikisakiShanaiName";
            this.obic7TextStringKariTorihikisakiShanaiName.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKariTorihikisakiShanaiName.ReadOnly = true;
            this.obic7TextStringKariTorihikisakiShanaiName.Size = new System.Drawing.Size(127, 19);
            this.obic7TextStringKariTorihikisakiShanaiName.TabIndex = 3170;
            this.obic7TextStringKariTorihikisakiShanaiName.TabStop = false;
            this.obic7TextStringKariTorihikisakiShanaiName.Value = "";
            this.obic7TextStringKariTorihikisakiShanaiName.VisibleFalseLock = false;
            // 
            // obic7TextNumberKariZeigaku
            // 
            this.obic7TextNumberKariZeigaku.AttachedButton = null;
            this.obic7TextNumberKariZeigaku.AttachedCopyText = null;
            this.obic7TextNumberKariZeigaku.AttachedLabel = null;
            this.obic7TextNumberKariZeigaku.AttachedSearchDialog = null;
            this.obic7TextNumberKariZeigaku.CheckOnClose = false;
            this.obic7TextNumberKariZeigaku.ColumnNumber = 18;
            this.obic7TextNumberKariZeigaku.EnableFalseLock = false;
            this.obic7TextNumberKariZeigaku.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberKariZeigaku.ErrorString = null;
            this.obic7TextNumberKariZeigaku.ForeColor = System.Drawing.Color.Red;
            this.obic7TextNumberKariZeigaku.Format = "#,##0";
            this.obic7TextNumberKariZeigaku.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.obic7TextNumberKariZeigaku.IsKeyColumn = false;
            this.obic7TextNumberKariZeigaku.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberKariZeigaku.Location = new System.Drawing.Point(438, 35);
            this.obic7TextNumberKariZeigaku.MaxLengthByte = 16;
            this.obic7TextNumberKariZeigaku.MaxValue = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.obic7TextNumberKariZeigaku.MinusColor = System.Drawing.Color.Red;
            this.obic7TextNumberKariZeigaku.MinValue = new decimal(new int[] {
            -727379969,
            232,
            0,
            -2147483648});
            this.obic7TextNumberKariZeigaku.Name = "obic7TextNumberKariZeigaku";
            this.obic7TextNumberKariZeigaku.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextNumberKariZeigaku.Size = new System.Drawing.Size(132, 19);
            this.obic7TextNumberKariZeigaku.TabIndex = 3280;
            this.obic7TextNumberKariZeigaku.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.obic7TextNumberKariZeigaku.Value = null;
            this.obic7TextNumberKariZeigaku.VisibleFalseLock = false;
            // 
            // obic7TextStringKariZeiHontaiNameRyaku
            // 
            this.obic7TextStringKariZeiHontaiNameRyaku.AllowAlphabet = true;
            this.obic7TextStringKariZeiHontaiNameRyaku.AllowKana = true;
            this.obic7TextStringKariZeiHontaiNameRyaku.AllowNumber = true;
            this.obic7TextStringKariZeiHontaiNameRyaku.AllowSymbol = true;
            this.obic7TextStringKariZeiHontaiNameRyaku.AllowUnicode = false;
            this.obic7TextStringKariZeiHontaiNameRyaku.AllowZenkaku = true;
            this.obic7TextStringKariZeiHontaiNameRyaku.AttachedButton = null;
            this.obic7TextStringKariZeiHontaiNameRyaku.AttachedCopyText = null;
            this.obic7TextStringKariZeiHontaiNameRyaku.AttachedLabel = null;
            this.obic7TextStringKariZeiHontaiNameRyaku.AttachedSearchDialog = null;
            this.obic7TextStringKariZeiHontaiNameRyaku.CheckOnClose = false;
            this.obic7TextStringKariZeiHontaiNameRyaku.ColumnNumber = 13;
            this.obic7TextStringKariZeiHontaiNameRyaku.DispFuriganaText = null;
            this.obic7TextStringKariZeiHontaiNameRyaku.EnableFalseLock = false;
            this.obic7TextStringKariZeiHontaiNameRyaku.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariZeiHontaiNameRyaku.ErrorString = null;
            this.obic7TextStringKariZeiHontaiNameRyaku.ImeMode = System.Windows.Forms.ImeMode.On;
            this.obic7TextStringKariZeiHontaiNameRyaku.IsKeyColumn = false;
            this.obic7TextStringKariZeiHontaiNameRyaku.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariZeiHontaiNameRyaku.Location = new System.Drawing.Point(306, 35);
            this.obic7TextStringKariZeiHontaiNameRyaku.MaxLengthByte = 60;
            this.obic7TextStringKariZeiHontaiNameRyaku.Name = "obic7TextStringKariZeiHontaiNameRyaku";
            this.obic7TextStringKariZeiHontaiNameRyaku.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKariZeiHontaiNameRyaku.ReadOnly = true;
            this.obic7TextStringKariZeiHontaiNameRyaku.Size = new System.Drawing.Size(133, 19);
            this.obic7TextStringKariZeiHontaiNameRyaku.TabIndex = 3230;
            this.obic7TextStringKariZeiHontaiNameRyaku.TabStop = false;
            this.obic7TextStringKariZeiHontaiNameRyaku.Value = "";
            this.obic7TextStringKariZeiHontaiNameRyaku.VisibleFalseLock = false;
            // 
            // obic7TextStringKariZeiHontaiCD
            // 
            this.obic7TextStringKariZeiHontaiCD.AllowAlphabet = true;
            this.obic7TextStringKariZeiHontaiCD.AllowKana = false;
            this.obic7TextStringKariZeiHontaiCD.AllowNumber = true;
            this.obic7TextStringKariZeiHontaiCD.AllowSymbol = false;
            this.obic7TextStringKariZeiHontaiCD.AllowUnicode = false;
            this.obic7TextStringKariZeiHontaiCD.AllowZenkaku = false;
            this.obic7TextStringKariZeiHontaiCD.AttachedButton = null;
            this.obic7TextStringKariZeiHontaiCD.AttachedCopyText = null;
            this.obic7TextStringKariZeiHontaiCD.AttachedLabel = null;
            this.obic7TextStringKariZeiHontaiCD.AttachedSearchDialog = null;
            this.obic7TextStringKariZeiHontaiCD.CheckOnClose = false;
            this.obic7TextStringKariZeiHontaiCD.ColumnNumber = 12;
            this.obic7TextStringKariZeiHontaiCD.DispFuriganaText = null;
            this.obic7TextStringKariZeiHontaiCD.EnableFalseLock = false;
            this.obic7TextStringKariZeiHontaiCD.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariZeiHontaiCD.ErrorString = null;
            this.obic7TextStringKariZeiHontaiCD.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringKariZeiHontaiCD.IsKeyColumn = false;
            this.obic7TextStringKariZeiHontaiCD.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariZeiHontaiCD.Location = new System.Drawing.Point(240, 35);
            this.obic7TextStringKariZeiHontaiCD.MaxLengthByte = 10;
            this.obic7TextStringKariZeiHontaiCD.Name = "obic7TextStringKariZeiHontaiCD";
            this.obic7TextStringKariZeiHontaiCD.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKariZeiHontaiCD.Size = new System.Drawing.Size(67, 19);
            this.obic7TextStringKariZeiHontaiCD.TabIndex = 3220;
            this.obic7TextStringKariZeiHontaiCD.Value = "";
            this.obic7TextStringKariZeiHontaiCD.VisibleFalseLock = false;
            // 
            // obic7TextStringKariUtiwakeNameRyaku
            // 
            this.obic7TextStringKariUtiwakeNameRyaku.AllowAlphabet = true;
            this.obic7TextStringKariUtiwakeNameRyaku.AllowKana = true;
            this.obic7TextStringKariUtiwakeNameRyaku.AllowNumber = true;
            this.obic7TextStringKariUtiwakeNameRyaku.AllowSymbol = true;
            this.obic7TextStringKariUtiwakeNameRyaku.AllowUnicode = false;
            this.obic7TextStringKariUtiwakeNameRyaku.AllowZenkaku = true;
            this.obic7TextStringKariUtiwakeNameRyaku.AttachedButton = null;
            this.obic7TextStringKariUtiwakeNameRyaku.AttachedCopyText = null;
            this.obic7TextStringKariUtiwakeNameRyaku.AttachedLabel = null;
            this.obic7TextStringKariUtiwakeNameRyaku.AttachedSearchDialog = null;
            this.obic7TextStringKariUtiwakeNameRyaku.CheckOnClose = false;
            this.obic7TextStringKariUtiwakeNameRyaku.ColumnNumber = 5;
            this.obic7TextStringKariUtiwakeNameRyaku.DispFuriganaText = null;
            this.obic7TextStringKariUtiwakeNameRyaku.EnableFalseLock = false;
            this.obic7TextStringKariUtiwakeNameRyaku.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariUtiwakeNameRyaku.ErrorString = null;
            this.obic7TextStringKariUtiwakeNameRyaku.ImeMode = System.Windows.Forms.ImeMode.On;
            this.obic7TextStringKariUtiwakeNameRyaku.IsKeyColumn = false;
            this.obic7TextStringKariUtiwakeNameRyaku.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariUtiwakeNameRyaku.Location = new System.Drawing.Point(114, 35);
            this.obic7TextStringKariUtiwakeNameRyaku.MaxLengthByte = 60;
            this.obic7TextStringKariUtiwakeNameRyaku.Name = "obic7TextStringKariUtiwakeNameRyaku";
            this.obic7TextStringKariUtiwakeNameRyaku.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKariUtiwakeNameRyaku.ReadOnly = true;
            this.obic7TextStringKariUtiwakeNameRyaku.Size = new System.Drawing.Size(127, 19);
            this.obic7TextStringKariUtiwakeNameRyaku.TabIndex = 3150;
            this.obic7TextStringKariUtiwakeNameRyaku.TabStop = false;
            this.obic7TextStringKariUtiwakeNameRyaku.Value = "";
            this.obic7TextStringKariUtiwakeNameRyaku.VisibleFalseLock = false;
            // 
            // obic7TextNumberKariKingaku
            // 
            this.obic7TextNumberKariKingaku.AttachedButton = null;
            this.obic7TextNumberKariKingaku.AttachedCopyText = null;
            this.obic7TextNumberKariKingaku.AttachedLabel = null;
            this.obic7TextNumberKariKingaku.AttachedSearchDialog = null;
            this.obic7TextNumberKariKingaku.CheckOnClose = false;
            this.obic7TextNumberKariKingaku.ColumnNumber = 17;
            this.obic7TextNumberKariKingaku.EnableFalseLock = false;
            this.obic7TextNumberKariKingaku.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberKariKingaku.ErrorString = null;
            this.obic7TextNumberKariKingaku.ForeColor = System.Drawing.Color.Red;
            this.obic7TextNumberKariKingaku.Format = "#,##0";
            this.obic7TextNumberKariKingaku.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.obic7TextNumberKariKingaku.IsKeyColumn = false;
            this.obic7TextNumberKariKingaku.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberKariKingaku.Location = new System.Drawing.Point(438, 17);
            this.obic7TextNumberKariKingaku.MaxLengthByte = 18;
            this.obic7TextNumberKariKingaku.MaxValue = new decimal(new int[] {
            1316134911,
            2328,
            0,
            0});
            this.obic7TextNumberKariKingaku.MinusColor = System.Drawing.Color.Red;
            this.obic7TextNumberKariKingaku.MinValue = new decimal(new int[] {
            1316134911,
            2328,
            0,
            -2147483648});
            this.obic7TextNumberKariKingaku.Name = "obic7TextNumberKariKingaku";
            this.obic7TextNumberKariKingaku.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextNumberKariKingaku.Size = new System.Drawing.Size(132, 19);
            this.obic7TextNumberKariKingaku.TabIndex = 3270;
            this.obic7TextNumberKariKingaku.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.obic7TextNumberKariKingaku.Value = null;
            this.obic7TextNumberKariKingaku.VisibleFalseLock = false;
            // 
            // obic7TextStringKariBumonNameRyaku
            // 
            this.obic7TextStringKariBumonNameRyaku.AllowAlphabet = true;
            this.obic7TextStringKariBumonNameRyaku.AllowKana = true;
            this.obic7TextStringKariBumonNameRyaku.AllowNumber = true;
            this.obic7TextStringKariBumonNameRyaku.AllowSymbol = true;
            this.obic7TextStringKariBumonNameRyaku.AllowUnicode = false;
            this.obic7TextStringKariBumonNameRyaku.AllowZenkaku = true;
            this.obic7TextStringKariBumonNameRyaku.AttachedButton = null;
            this.obic7TextStringKariBumonNameRyaku.AttachedCopyText = null;
            this.obic7TextStringKariBumonNameRyaku.AttachedLabel = null;
            this.obic7TextStringKariBumonNameRyaku.AttachedSearchDialog = null;
            this.obic7TextStringKariBumonNameRyaku.CheckOnClose = false;
            this.obic7TextStringKariBumonNameRyaku.ColumnNumber = 11;
            this.obic7TextStringKariBumonNameRyaku.DispFuriganaText = null;
            this.obic7TextStringKariBumonNameRyaku.EnableFalseLock = false;
            this.obic7TextStringKariBumonNameRyaku.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariBumonNameRyaku.ErrorString = null;
            this.obic7TextStringKariBumonNameRyaku.ImeMode = System.Windows.Forms.ImeMode.On;
            this.obic7TextStringKariBumonNameRyaku.IsKeyColumn = false;
            this.obic7TextStringKariBumonNameRyaku.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariBumonNameRyaku.Location = new System.Drawing.Point(306, 17);
            this.obic7TextStringKariBumonNameRyaku.MaxLengthByte = 60;
            this.obic7TextStringKariBumonNameRyaku.Name = "obic7TextStringKariBumonNameRyaku";
            this.obic7TextStringKariBumonNameRyaku.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKariBumonNameRyaku.ReadOnly = true;
            this.obic7TextStringKariBumonNameRyaku.Size = new System.Drawing.Size(133, 19);
            this.obic7TextStringKariBumonNameRyaku.TabIndex = 3210;
            this.obic7TextStringKariBumonNameRyaku.TabStop = false;
            this.obic7TextStringKariBumonNameRyaku.Value = "";
            this.obic7TextStringKariBumonNameRyaku.VisibleFalseLock = false;
            // 
            // obic7TextStringKariBumonCD
            // 
            this.obic7TextStringKariBumonCD.AllowAlphabet = true;
            this.obic7TextStringKariBumonCD.AllowKana = false;
            this.obic7TextStringKariBumonCD.AllowNumber = true;
            this.obic7TextStringKariBumonCD.AllowSymbol = true;
            this.obic7TextStringKariBumonCD.AllowUnicode = false;
            this.obic7TextStringKariBumonCD.AllowZenkaku = false;
            this.obic7TextStringKariBumonCD.AttachedButton = null;
            this.obic7TextStringKariBumonCD.AttachedCopyText = null;
            this.obic7TextStringKariBumonCD.AttachedLabel = null;
            this.obic7TextStringKariBumonCD.AttachedSearchDialog = null;
            this.obic7TextStringKariBumonCD.CheckOnClose = false;
            this.obic7TextStringKariBumonCD.ColumnNumber = 10;
            this.obic7TextStringKariBumonCD.DispFuriganaText = null;
            this.obic7TextStringKariBumonCD.EnableFalseLock = false;
            this.obic7TextStringKariBumonCD.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariBumonCD.ErrorString = null;
            this.obic7TextStringKariBumonCD.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringKariBumonCD.IsKeyColumn = false;
            this.obic7TextStringKariBumonCD.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariBumonCD.Location = new System.Drawing.Point(240, 17);
            this.obic7TextStringKariBumonCD.MaxLengthByte = 10;
            this.obic7TextStringKariBumonCD.Name = "obic7TextStringKariBumonCD";
            this.obic7TextStringKariBumonCD.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKariBumonCD.Size = new System.Drawing.Size(67, 19);
            this.obic7TextStringKariBumonCD.TabIndex = 3200;
            this.obic7TextStringKariBumonCD.Value = "";
            this.obic7TextStringKariBumonCD.VisibleFalseLock = false;
            // 
            // obic7TextStringKariHojoNameRyaku
            // 
            this.obic7TextStringKariHojoNameRyaku.AllowAlphabet = true;
            this.obic7TextStringKariHojoNameRyaku.AllowKana = true;
            this.obic7TextStringKariHojoNameRyaku.AllowNumber = true;
            this.obic7TextStringKariHojoNameRyaku.AllowSymbol = true;
            this.obic7TextStringKariHojoNameRyaku.AllowUnicode = false;
            this.obic7TextStringKariHojoNameRyaku.AllowZenkaku = true;
            this.obic7TextStringKariHojoNameRyaku.AttachedButton = null;
            this.obic7TextStringKariHojoNameRyaku.AttachedCopyText = null;
            this.obic7TextStringKariHojoNameRyaku.AttachedLabel = null;
            this.obic7TextStringKariHojoNameRyaku.AttachedSearchDialog = null;
            this.obic7TextStringKariHojoNameRyaku.CheckOnClose = false;
            this.obic7TextStringKariHojoNameRyaku.ColumnNumber = 3;
            this.obic7TextStringKariHojoNameRyaku.DispFuriganaText = null;
            this.obic7TextStringKariHojoNameRyaku.EnableFalseLock = false;
            this.obic7TextStringKariHojoNameRyaku.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariHojoNameRyaku.ErrorString = null;
            this.obic7TextStringKariHojoNameRyaku.ImeMode = System.Windows.Forms.ImeMode.On;
            this.obic7TextStringKariHojoNameRyaku.IsKeyColumn = false;
            this.obic7TextStringKariHojoNameRyaku.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariHojoNameRyaku.Location = new System.Drawing.Point(114, 17);
            this.obic7TextStringKariHojoNameRyaku.MaxLengthByte = 60;
            this.obic7TextStringKariHojoNameRyaku.Name = "obic7TextStringKariHojoNameRyaku";
            this.obic7TextStringKariHojoNameRyaku.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKariHojoNameRyaku.ReadOnly = true;
            this.obic7TextStringKariHojoNameRyaku.Size = new System.Drawing.Size(127, 19);
            this.obic7TextStringKariHojoNameRyaku.TabIndex = 3130;
            this.obic7TextStringKariHojoNameRyaku.TabStop = false;
            this.obic7TextStringKariHojoNameRyaku.Value = "";
            this.obic7TextStringKariHojoNameRyaku.VisibleFalseLock = false;
            // 
            // obic7TextStringKariZeikomiKBNName
            // 
            this.obic7TextStringKariZeikomiKBNName.AllowAlphabet = true;
            this.obic7TextStringKariZeikomiKBNName.AllowKana = true;
            this.obic7TextStringKariZeikomiKBNName.AllowNumber = true;
            this.obic7TextStringKariZeikomiKBNName.AllowSymbol = true;
            this.obic7TextStringKariZeikomiKBNName.AllowUnicode = false;
            this.obic7TextStringKariZeikomiKBNName.AllowZenkaku = true;
            this.obic7TextStringKariZeikomiKBNName.AttachedButton = null;
            this.obic7TextStringKariZeikomiKBNName.AttachedCopyText = null;
            this.obic7TextStringKariZeikomiKBNName.AttachedLabel = null;
            this.obic7TextStringKariZeikomiKBNName.AttachedSearchDialog = null;
            this.obic7TextStringKariZeikomiKBNName.CheckOnClose = false;
            this.obic7TextStringKariZeikomiKBNName.ColumnNumber = 16;
            this.obic7TextStringKariZeikomiKBNName.DispFuriganaText = null;
            this.obic7TextStringKariZeikomiKBNName.EnableFalseLock = false;
            this.obic7TextStringKariZeikomiKBNName.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariZeikomiKBNName.ErrorString = null;
            this.obic7TextStringKariZeikomiKBNName.ImeMode = System.Windows.Forms.ImeMode.On;
            this.obic7TextStringKariZeikomiKBNName.IsKeyColumn = false;
            this.obic7TextStringKariZeikomiKBNName.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariZeikomiKBNName.Location = new System.Drawing.Point(486, -1);
            this.obic7TextStringKariZeikomiKBNName.MaxLengthByte = 60;
            this.obic7TextStringKariZeikomiKBNName.Name = "obic7TextStringKariZeikomiKBNName";
            this.obic7TextStringKariZeikomiKBNName.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKariZeikomiKBNName.ReadOnly = true;
            this.obic7TextStringKariZeikomiKBNName.Size = new System.Drawing.Size(84, 19);
            this.obic7TextStringKariZeikomiKBNName.TabIndex = 2360;
            this.obic7TextStringKariZeikomiKBNName.TabStop = false;
            this.obic7TextStringKariZeikomiKBNName.Value = "";
            this.obic7TextStringKariZeikomiKBNName.VisibleFalseLock = false;
            // 
            // obic7TextNumberKariZeikomiKBN
            // 
            this.obic7TextNumberKariZeikomiKBN.AttachedButton = null;
            this.obic7TextNumberKariZeikomiKBN.AttachedCopyText = null;
            this.obic7TextNumberKariZeikomiKBN.AttachedLabel = null;
            this.obic7TextNumberKariZeikomiKBN.AttachedSearchDialog = null;
            this.obic7TextNumberKariZeikomiKBN.CheckOnClose = false;
            this.obic7TextNumberKariZeikomiKBN.ColumnNumber = 15;
            this.obic7TextNumberKariZeikomiKBN.EnableFalseLock = false;
            this.obic7TextNumberKariZeikomiKBN.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberKariZeikomiKBN.ErrorString = null;
            this.obic7TextNumberKariZeikomiKBN.ForeColor = System.Drawing.SystemColors.WindowText;
            this.obic7TextNumberKariZeikomiKBN.Format = "0";
            this.obic7TextNumberKariZeikomiKBN.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.obic7TextNumberKariZeikomiKBN.IsKeyColumn = false;
            this.obic7TextNumberKariZeikomiKBN.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberKariZeikomiKBN.Location = new System.Drawing.Point(468, -1);
            this.obic7TextNumberKariZeikomiKBN.MaxLengthByte = 1;
            this.obic7TextNumberKariZeikomiKBN.MaxValue = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.obic7TextNumberKariZeikomiKBN.MinusColor = System.Drawing.Color.Red;
            this.obic7TextNumberKariZeikomiKBN.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.obic7TextNumberKariZeikomiKBN.Name = "obic7TextNumberKariZeikomiKBN";
            this.obic7TextNumberKariZeikomiKBN.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextNumberKariZeikomiKBN.Size = new System.Drawing.Size(19, 19);
            this.obic7TextNumberKariZeikomiKBN.TabIndex = 3250;
            this.obic7TextNumberKariZeikomiKBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.obic7TextNumberKariZeikomiKBN.Value = null;
            this.obic7TextNumberKariZeikomiKBN.VisibleFalseLock = false;
            // 
            // obic7TextStringKariZeiKBN
            // 
            this.obic7TextStringKariZeiKBN.AllowAlphabet = true;
            this.obic7TextStringKariZeiKBN.AllowKana = false;
            this.obic7TextStringKariZeiKBN.AllowNumber = true;
            this.obic7TextStringKariZeiKBN.AllowSymbol = false;
            this.obic7TextStringKariZeiKBN.AllowUnicode = false;
            this.obic7TextStringKariZeiKBN.AllowZenkaku = false;
            this.obic7TextStringKariZeiKBN.AttachedButton = null;
            this.obic7TextStringKariZeiKBN.AttachedCopyText = null;
            this.obic7TextStringKariZeiKBN.AttachedLabel = null;
            this.obic7TextStringKariZeiKBN.AttachedSearchDialog = null;
            this.obic7TextStringKariZeiKBN.CheckOnClose = false;
            this.obic7TextStringKariZeiKBN.ColumnNumber = 14;
            this.obic7TextStringKariZeiKBN.DispFuriganaText = null;
            this.obic7TextStringKariZeiKBN.EnableFalseLock = false;
            this.obic7TextStringKariZeiKBN.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariZeiKBN.ErrorString = null;
            this.obic7TextStringKariZeiKBN.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringKariZeiKBN.IsKeyColumn = false;
            this.obic7TextStringKariZeiKBN.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariZeiKBN.Location = new System.Drawing.Point(438, -1);
            this.obic7TextStringKariZeiKBN.MaxLengthByte = 3;
            this.obic7TextStringKariZeiKBN.Name = "obic7TextStringKariZeiKBN";
            this.obic7TextStringKariZeiKBN.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKariZeiKBN.Size = new System.Drawing.Size(31, 19);
            this.obic7TextStringKariZeiKBN.TabIndex = 3240;
            this.obic7TextStringKariZeiKBN.Value = "";
            this.obic7TextStringKariZeiKBN.VisibleFalseLock = false;
            // 
            // obic7TextStringKariZigyoushoNameRyaku
            // 
            this.obic7TextStringKariZigyoushoNameRyaku.AllowAlphabet = true;
            this.obic7TextStringKariZigyoushoNameRyaku.AllowKana = true;
            this.obic7TextStringKariZigyoushoNameRyaku.AllowNumber = true;
            this.obic7TextStringKariZigyoushoNameRyaku.AllowSymbol = true;
            this.obic7TextStringKariZigyoushoNameRyaku.AllowUnicode = false;
            this.obic7TextStringKariZigyoushoNameRyaku.AllowZenkaku = true;
            this.obic7TextStringKariZigyoushoNameRyaku.AttachedButton = null;
            this.obic7TextStringKariZigyoushoNameRyaku.AttachedCopyText = null;
            this.obic7TextStringKariZigyoushoNameRyaku.AttachedLabel = null;
            this.obic7TextStringKariZigyoushoNameRyaku.AttachedSearchDialog = null;
            this.obic7TextStringKariZigyoushoNameRyaku.CheckOnClose = false;
            this.obic7TextStringKariZigyoushoNameRyaku.ColumnNumber = 9;
            this.obic7TextStringKariZigyoushoNameRyaku.DispFuriganaText = null;
            this.obic7TextStringKariZigyoushoNameRyaku.EnableFalseLock = false;
            this.obic7TextStringKariZigyoushoNameRyaku.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariZigyoushoNameRyaku.ErrorString = null;
            this.obic7TextStringKariZigyoushoNameRyaku.ImeMode = System.Windows.Forms.ImeMode.On;
            this.obic7TextStringKariZigyoushoNameRyaku.IsKeyColumn = false;
            this.obic7TextStringKariZigyoushoNameRyaku.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariZigyoushoNameRyaku.Location = new System.Drawing.Point(306, -1);
            this.obic7TextStringKariZigyoushoNameRyaku.MaxLengthByte = 60;
            this.obic7TextStringKariZigyoushoNameRyaku.Name = "obic7TextStringKariZigyoushoNameRyaku";
            this.obic7TextStringKariZigyoushoNameRyaku.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKariZigyoushoNameRyaku.ReadOnly = true;
            this.obic7TextStringKariZigyoushoNameRyaku.Size = new System.Drawing.Size(133, 19);
            this.obic7TextStringKariZigyoushoNameRyaku.TabIndex = 3190;
            this.obic7TextStringKariZigyoushoNameRyaku.TabStop = false;
            this.obic7TextStringKariZigyoushoNameRyaku.Value = "";
            this.obic7TextStringKariZigyoushoNameRyaku.VisibleFalseLock = false;
            // 
            // obic7TextStringKariZigyoushoCD
            // 
            this.obic7TextStringKariZigyoushoCD.AllowAlphabet = true;
            this.obic7TextStringKariZigyoushoCD.AllowKana = false;
            this.obic7TextStringKariZigyoushoCD.AllowNumber = true;
            this.obic7TextStringKariZigyoushoCD.AllowSymbol = false;
            this.obic7TextStringKariZigyoushoCD.AllowUnicode = false;
            this.obic7TextStringKariZigyoushoCD.AllowZenkaku = false;
            this.obic7TextStringKariZigyoushoCD.AttachedButton = null;
            this.obic7TextStringKariZigyoushoCD.AttachedCopyText = null;
            this.obic7TextStringKariZigyoushoCD.AttachedLabel = null;
            this.obic7TextStringKariZigyoushoCD.AttachedSearchDialog = null;
            this.obic7TextStringKariZigyoushoCD.CheckOnClose = false;
            this.obic7TextStringKariZigyoushoCD.ColumnNumber = 8;
            this.obic7TextStringKariZigyoushoCD.DispFuriganaText = null;
            this.obic7TextStringKariZigyoushoCD.EnableFalseLock = false;
            this.obic7TextStringKariZigyoushoCD.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariZigyoushoCD.ErrorString = null;
            this.obic7TextStringKariZigyoushoCD.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringKariZigyoushoCD.IsKeyColumn = false;
            this.obic7TextStringKariZigyoushoCD.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariZigyoushoCD.Location = new System.Drawing.Point(240, -1);
            this.obic7TextStringKariZigyoushoCD.MaxLengthByte = 6;
            this.obic7TextStringKariZigyoushoCD.Name = "obic7TextStringKariZigyoushoCD";
            this.obic7TextStringKariZigyoushoCD.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKariZigyoushoCD.Size = new System.Drawing.Size(67, 19);
            this.obic7TextStringKariZigyoushoCD.TabIndex = 3180;
            this.obic7TextStringKariZigyoushoCD.Value = "";
            this.obic7TextStringKariZigyoushoCD.VisibleFalseLock = false;
            // 
            // obic7TextStringKariKanjouNameRyaku
            // 
            this.obic7TextStringKariKanjouNameRyaku.AllowAlphabet = true;
            this.obic7TextStringKariKanjouNameRyaku.AllowKana = true;
            this.obic7TextStringKariKanjouNameRyaku.AllowNumber = true;
            this.obic7TextStringKariKanjouNameRyaku.AllowSymbol = true;
            this.obic7TextStringKariKanjouNameRyaku.AllowUnicode = false;
            this.obic7TextStringKariKanjouNameRyaku.AllowZenkaku = true;
            this.obic7TextStringKariKanjouNameRyaku.AttachedButton = null;
            this.obic7TextStringKariKanjouNameRyaku.AttachedCopyText = null;
            this.obic7TextStringKariKanjouNameRyaku.AttachedLabel = null;
            this.obic7TextStringKariKanjouNameRyaku.AttachedSearchDialog = null;
            this.obic7TextStringKariKanjouNameRyaku.CheckOnClose = false;
            this.obic7TextStringKariKanjouNameRyaku.ColumnNumber = 1;
            this.obic7TextStringKariKanjouNameRyaku.DispFuriganaText = null;
            this.obic7TextStringKariKanjouNameRyaku.EnableFalseLock = false;
            this.obic7TextStringKariKanjouNameRyaku.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariKanjouNameRyaku.ErrorString = null;
            this.obic7TextStringKariKanjouNameRyaku.ImeMode = System.Windows.Forms.ImeMode.On;
            this.obic7TextStringKariKanjouNameRyaku.IsKeyColumn = false;
            this.obic7TextStringKariKanjouNameRyaku.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariKanjouNameRyaku.Location = new System.Drawing.Point(114, -1);
            this.obic7TextStringKariKanjouNameRyaku.MaxLengthByte = 60;
            this.obic7TextStringKariKanjouNameRyaku.Name = "obic7TextStringKariKanjouNameRyaku";
            this.obic7TextStringKariKanjouNameRyaku.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKariKanjouNameRyaku.ReadOnly = true;
            this.obic7TextStringKariKanjouNameRyaku.Size = new System.Drawing.Size(127, 19);
            this.obic7TextStringKariKanjouNameRyaku.TabIndex = 3110;
            this.obic7TextStringKariKanjouNameRyaku.TabStop = false;
            this.obic7TextStringKariKanjouNameRyaku.Value = "";
            this.obic7TextStringKariKanjouNameRyaku.VisibleFalseLock = false;
            // 
            // obic7TextStringKasiSikinCD
            // 
            this.obic7TextStringKasiSikinCD.AllowAlphabet = true;
            this.obic7TextStringKasiSikinCD.AllowKana = true;
            this.obic7TextStringKasiSikinCD.AllowNumber = true;
            this.obic7TextStringKasiSikinCD.AllowSymbol = true;
            this.obic7TextStringKasiSikinCD.AllowUnicode = false;
            this.obic7TextStringKasiSikinCD.AllowZenkaku = true;
            this.obic7TextStringKasiSikinCD.AttachedButton = null;
            this.obic7TextStringKasiSikinCD.AttachedCopyText = null;
            this.obic7TextStringKasiSikinCD.AttachedLabel = null;
            this.obic7TextStringKasiSikinCD.AttachedSearchDialog = null;
            this.obic7TextStringKasiSikinCD.CheckOnClose = false;
            this.obic7TextStringKasiSikinCD.ColumnNumber = 45;
            this.obic7TextStringKasiSikinCD.DispFuriganaText = null;
            this.obic7TextStringKasiSikinCD.EnableFalseLock = false;
            this.obic7TextStringKasiSikinCD.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiSikinCD.ErrorString = null;
            this.obic7TextStringKasiSikinCD.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringKasiSikinCD.IsKeyColumn = false;
            this.obic7TextStringKasiSikinCD.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiSikinCD.Location = new System.Drawing.Point(960, 53);
            this.obic7TextStringKasiSikinCD.MaxLengthByte = 30;
            this.obic7TextStringKasiSikinCD.Name = "obic7TextStringKasiSikinCD";
            this.obic7TextStringKasiSikinCD.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKasiSikinCD.Size = new System.Drawing.Size(67, 19);
            this.obic7TextStringKasiSikinCD.TabIndex = 3840;
            this.obic7TextStringKasiSikinCD.Value = "";
            this.obic7TextStringKasiSikinCD.VisibleFalseLock = false;
            // 
            // obic7TextStringKasiBunsekiCD3
            // 
            this.obic7TextStringKasiBunsekiCD3.AllowAlphabet = true;
            this.obic7TextStringKasiBunsekiCD3.AllowKana = true;
            this.obic7TextStringKasiBunsekiCD3.AllowNumber = true;
            this.obic7TextStringKasiBunsekiCD3.AllowSymbol = true;
            this.obic7TextStringKasiBunsekiCD3.AllowUnicode = false;
            this.obic7TextStringKasiBunsekiCD3.AllowZenkaku = true;
            this.obic7TextStringKasiBunsekiCD3.AttachedButton = null;
            this.obic7TextStringKasiBunsekiCD3.AttachedCopyText = null;
            this.obic7TextStringKasiBunsekiCD3.AttachedLabel = null;
            this.obic7TextStringKasiBunsekiCD3.AttachedSearchDialog = null;
            this.obic7TextStringKasiBunsekiCD3.CheckOnClose = false;
            this.obic7TextStringKasiBunsekiCD3.ColumnNumber = 44;
            this.obic7TextStringKasiBunsekiCD3.DispFuriganaText = null;
            this.obic7TextStringKasiBunsekiCD3.EnableFalseLock = false;
            this.obic7TextStringKasiBunsekiCD3.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiBunsekiCD3.ErrorString = null;
            this.obic7TextStringKasiBunsekiCD3.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringKasiBunsekiCD3.IsKeyColumn = false;
            this.obic7TextStringKasiBunsekiCD3.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiBunsekiCD3.Location = new System.Drawing.Point(894, 53);
            this.obic7TextStringKasiBunsekiCD3.MaxLengthByte = 30;
            this.obic7TextStringKasiBunsekiCD3.Name = "obic7TextStringKasiBunsekiCD3";
            this.obic7TextStringKasiBunsekiCD3.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKasiBunsekiCD3.Size = new System.Drawing.Size(67, 19);
            this.obic7TextStringKasiBunsekiCD3.TabIndex = 3810;
            this.obic7TextStringKasiBunsekiCD3.Value = "";
            this.obic7TextStringKasiBunsekiCD3.VisibleFalseLock = false;
            // 
            // obic7TextStringKasiBunsekiCD2
            // 
            this.obic7TextStringKasiBunsekiCD2.AllowAlphabet = true;
            this.obic7TextStringKasiBunsekiCD2.AllowKana = true;
            this.obic7TextStringKasiBunsekiCD2.AllowNumber = true;
            this.obic7TextStringKasiBunsekiCD2.AllowSymbol = true;
            this.obic7TextStringKasiBunsekiCD2.AllowUnicode = false;
            this.obic7TextStringKasiBunsekiCD2.AllowZenkaku = true;
            this.obic7TextStringKasiBunsekiCD2.AttachedButton = null;
            this.obic7TextStringKasiBunsekiCD2.AttachedCopyText = null;
            this.obic7TextStringKasiBunsekiCD2.AttachedLabel = null;
            this.obic7TextStringKasiBunsekiCD2.AttachedSearchDialog = null;
            this.obic7TextStringKasiBunsekiCD2.CheckOnClose = false;
            this.obic7TextStringKasiBunsekiCD2.ColumnNumber = 43;
            this.obic7TextStringKasiBunsekiCD2.DispFuriganaText = null;
            this.obic7TextStringKasiBunsekiCD2.EnableFalseLock = false;
            this.obic7TextStringKasiBunsekiCD2.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiBunsekiCD2.ErrorString = null;
            this.obic7TextStringKasiBunsekiCD2.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringKasiBunsekiCD2.IsKeyColumn = false;
            this.obic7TextStringKasiBunsekiCD2.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiBunsekiCD2.Location = new System.Drawing.Point(828, 53);
            this.obic7TextStringKasiBunsekiCD2.MaxLengthByte = 30;
            this.obic7TextStringKasiBunsekiCD2.Name = "obic7TextStringKasiBunsekiCD2";
            this.obic7TextStringKasiBunsekiCD2.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKasiBunsekiCD2.Size = new System.Drawing.Size(67, 19);
            this.obic7TextStringKasiBunsekiCD2.TabIndex = 3800;
            this.obic7TextStringKasiBunsekiCD2.Value = "";
            this.obic7TextStringKasiBunsekiCD2.VisibleFalseLock = false;
            // 
            // obic7TextStringKasiBunsekiCD1
            // 
            this.obic7TextStringKasiBunsekiCD1.AllowAlphabet = true;
            this.obic7TextStringKasiBunsekiCD1.AllowKana = true;
            this.obic7TextStringKasiBunsekiCD1.AllowNumber = true;
            this.obic7TextStringKasiBunsekiCD1.AllowSymbol = true;
            this.obic7TextStringKasiBunsekiCD1.AllowUnicode = false;
            this.obic7TextStringKasiBunsekiCD1.AllowZenkaku = true;
            this.obic7TextStringKasiBunsekiCD1.AttachedButton = null;
            this.obic7TextStringKasiBunsekiCD1.AttachedCopyText = null;
            this.obic7TextStringKasiBunsekiCD1.AttachedLabel = null;
            this.obic7TextStringKasiBunsekiCD1.AttachedSearchDialog = null;
            this.obic7TextStringKasiBunsekiCD1.CheckOnClose = false;
            this.obic7TextStringKasiBunsekiCD1.ColumnNumber = 42;
            this.obic7TextStringKasiBunsekiCD1.DispFuriganaText = null;
            this.obic7TextStringKasiBunsekiCD1.EnableFalseLock = false;
            this.obic7TextStringKasiBunsekiCD1.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiBunsekiCD1.ErrorString = null;
            this.obic7TextStringKasiBunsekiCD1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringKasiBunsekiCD1.IsKeyColumn = false;
            this.obic7TextStringKasiBunsekiCD1.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiBunsekiCD1.Location = new System.Drawing.Point(762, 53);
            this.obic7TextStringKasiBunsekiCD1.MaxLengthByte = 30;
            this.obic7TextStringKasiBunsekiCD1.Name = "obic7TextStringKasiBunsekiCD1";
            this.obic7TextStringKasiBunsekiCD1.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKasiBunsekiCD1.Size = new System.Drawing.Size(67, 19);
            this.obic7TextStringKasiBunsekiCD1.TabIndex = 3790;
            this.obic7TextStringKasiBunsekiCD1.Value = "";
            this.obic7TextStringKasiBunsekiCD1.VisibleFalseLock = false;
            // 
            // obic7TextStringKasiTorihikisakiShanaiName
            // 
            this.obic7TextStringKasiTorihikisakiShanaiName.AllowAlphabet = true;
            this.obic7TextStringKasiTorihikisakiShanaiName.AllowKana = true;
            this.obic7TextStringKasiTorihikisakiShanaiName.AllowNumber = true;
            this.obic7TextStringKasiTorihikisakiShanaiName.AllowSymbol = true;
            this.obic7TextStringKasiTorihikisakiShanaiName.AllowUnicode = false;
            this.obic7TextStringKasiTorihikisakiShanaiName.AllowZenkaku = true;
            this.obic7TextStringKasiTorihikisakiShanaiName.AttachedButton = null;
            this.obic7TextStringKasiTorihikisakiShanaiName.AttachedCopyText = null;
            this.obic7TextStringKasiTorihikisakiShanaiName.AttachedLabel = null;
            this.obic7TextStringKasiTorihikisakiShanaiName.AttachedSearchDialog = null;
            this.obic7TextStringKasiTorihikisakiShanaiName.CheckOnClose = false;
            this.obic7TextStringKasiTorihikisakiShanaiName.ColumnNumber = 30;
            this.obic7TextStringKasiTorihikisakiShanaiName.DispFuriganaText = null;
            this.obic7TextStringKasiTorihikisakiShanaiName.EnableFalseLock = false;
            this.obic7TextStringKasiTorihikisakiShanaiName.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiTorihikisakiShanaiName.ErrorString = null;
            this.obic7TextStringKasiTorihikisakiShanaiName.ImeMode = System.Windows.Forms.ImeMode.On;
            this.obic7TextStringKasiTorihikisakiShanaiName.IsKeyColumn = false;
            this.obic7TextStringKasiTorihikisakiShanaiName.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiTorihikisakiShanaiName.Location = new System.Drawing.Point(636, 53);
            this.obic7TextStringKasiTorihikisakiShanaiName.MaxLengthByte = 60;
            this.obic7TextStringKasiTorihikisakiShanaiName.Name = "obic7TextStringKasiTorihikisakiShanaiName";
            this.obic7TextStringKasiTorihikisakiShanaiName.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKasiTorihikisakiShanaiName.ReadOnly = true;
            this.obic7TextStringKasiTorihikisakiShanaiName.Size = new System.Drawing.Size(127, 19);
            this.obic7TextStringKasiTorihikisakiShanaiName.TabIndex = 3670;
            this.obic7TextStringKasiTorihikisakiShanaiName.TabStop = false;
            this.obic7TextStringKasiTorihikisakiShanaiName.Value = "";
            this.obic7TextStringKasiTorihikisakiShanaiName.VisibleFalseLock = false;
            // 
            // obic7TextStringKasiTorihikisakiCD
            // 
            this.obic7TextStringKasiTorihikisakiCD.AllowAlphabet = true;
            this.obic7TextStringKasiTorihikisakiCD.AllowKana = false;
            this.obic7TextStringKasiTorihikisakiCD.AllowNumber = true;
            this.obic7TextStringKasiTorihikisakiCD.AllowSymbol = true;
            this.obic7TextStringKasiTorihikisakiCD.AllowUnicode = false;
            this.obic7TextStringKasiTorihikisakiCD.AllowZenkaku = false;
            this.obic7TextStringKasiTorihikisakiCD.AttachedButton = null;
            this.obic7TextStringKasiTorihikisakiCD.AttachedCopyText = null;
            this.obic7TextStringKasiTorihikisakiCD.AttachedLabel = null;
            this.obic7TextStringKasiTorihikisakiCD.AttachedSearchDialog = null;
            this.obic7TextStringKasiTorihikisakiCD.CheckOnClose = false;
            this.obic7TextStringKasiTorihikisakiCD.ColumnNumber = 29;
            this.obic7TextStringKasiTorihikisakiCD.DispFuriganaText = null;
            this.obic7TextStringKasiTorihikisakiCD.EnableFalseLock = false;
            this.obic7TextStringKasiTorihikisakiCD.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiTorihikisakiCD.ErrorString = null;
            this.obic7TextStringKasiTorihikisakiCD.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringKasiTorihikisakiCD.IsKeyColumn = false;
            this.obic7TextStringKasiTorihikisakiCD.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiTorihikisakiCD.Location = new System.Drawing.Point(570, 53);
            this.obic7TextStringKasiTorihikisakiCD.MaxLengthByte = 10;
            this.obic7TextStringKasiTorihikisakiCD.Name = "obic7TextStringKasiTorihikisakiCD";
            this.obic7TextStringKasiTorihikisakiCD.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKasiTorihikisakiCD.Size = new System.Drawing.Size(67, 19);
            this.obic7TextStringKasiTorihikisakiCD.TabIndex = 3660;
            this.obic7TextStringKasiTorihikisakiCD.Value = "";
            this.obic7TextStringKasiTorihikisakiCD.VisibleFalseLock = false;
            // 
            // obic7TextNumberKasiZeigaku
            // 
            this.obic7TextNumberKasiZeigaku.AttachedButton = null;
            this.obic7TextNumberKasiZeigaku.AttachedCopyText = null;
            this.obic7TextNumberKasiZeigaku.AttachedLabel = null;
            this.obic7TextNumberKasiZeigaku.AttachedSearchDialog = null;
            this.obic7TextNumberKasiZeigaku.CheckOnClose = false;
            this.obic7TextNumberKasiZeigaku.ColumnNumber = 41;
            this.obic7TextNumberKasiZeigaku.EnableFalseLock = false;
            this.obic7TextNumberKasiZeigaku.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberKasiZeigaku.ErrorString = null;
            this.obic7TextNumberKasiZeigaku.ForeColor = System.Drawing.Color.Red;
            this.obic7TextNumberKasiZeigaku.Format = "#,##0";
            this.obic7TextNumberKasiZeigaku.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.obic7TextNumberKasiZeigaku.IsKeyColumn = false;
            this.obic7TextNumberKasiZeigaku.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberKasiZeigaku.Location = new System.Drawing.Point(960, 35);
            this.obic7TextNumberKasiZeigaku.MaxLengthByte = 16;
            this.obic7TextNumberKasiZeigaku.MaxValue = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.obic7TextNumberKasiZeigaku.MinusColor = System.Drawing.Color.Red;
            this.obic7TextNumberKasiZeigaku.MinValue = new decimal(new int[] {
            -727379969,
            232,
            0,
            -2147483648});
            this.obic7TextNumberKasiZeigaku.Name = "obic7TextNumberKasiZeigaku";
            this.obic7TextNumberKasiZeigaku.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextNumberKasiZeigaku.Size = new System.Drawing.Size(132, 19);
            this.obic7TextNumberKasiZeigaku.TabIndex = 3780;
            this.obic7TextNumberKasiZeigaku.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.obic7TextNumberKasiZeigaku.Value = null;
            this.obic7TextNumberKasiZeigaku.VisibleFalseLock = false;
            // 
            // obic7TextStringKasiZeiHontaiNameRyaku
            // 
            this.obic7TextStringKasiZeiHontaiNameRyaku.AllowAlphabet = true;
            this.obic7TextStringKasiZeiHontaiNameRyaku.AllowKana = true;
            this.obic7TextStringKasiZeiHontaiNameRyaku.AllowNumber = true;
            this.obic7TextStringKasiZeiHontaiNameRyaku.AllowSymbol = true;
            this.obic7TextStringKasiZeiHontaiNameRyaku.AllowUnicode = false;
            this.obic7TextStringKasiZeiHontaiNameRyaku.AllowZenkaku = true;
            this.obic7TextStringKasiZeiHontaiNameRyaku.AttachedButton = null;
            this.obic7TextStringKasiZeiHontaiNameRyaku.AttachedCopyText = null;
            this.obic7TextStringKasiZeiHontaiNameRyaku.AttachedLabel = null;
            this.obic7TextStringKasiZeiHontaiNameRyaku.AttachedSearchDialog = null;
            this.obic7TextStringKasiZeiHontaiNameRyaku.CheckOnClose = false;
            this.obic7TextStringKasiZeiHontaiNameRyaku.ColumnNumber = 36;
            this.obic7TextStringKasiZeiHontaiNameRyaku.DispFuriganaText = null;
            this.obic7TextStringKasiZeiHontaiNameRyaku.EnableFalseLock = false;
            this.obic7TextStringKasiZeiHontaiNameRyaku.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiZeiHontaiNameRyaku.ErrorString = null;
            this.obic7TextStringKasiZeiHontaiNameRyaku.ImeMode = System.Windows.Forms.ImeMode.On;
            this.obic7TextStringKasiZeiHontaiNameRyaku.IsKeyColumn = false;
            this.obic7TextStringKasiZeiHontaiNameRyaku.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiZeiHontaiNameRyaku.Location = new System.Drawing.Point(828, 35);
            this.obic7TextStringKasiZeiHontaiNameRyaku.MaxLengthByte = 60;
            this.obic7TextStringKasiZeiHontaiNameRyaku.Name = "obic7TextStringKasiZeiHontaiNameRyaku";
            this.obic7TextStringKasiZeiHontaiNameRyaku.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKasiZeiHontaiNameRyaku.ReadOnly = true;
            this.obic7TextStringKasiZeiHontaiNameRyaku.ShortcutsEnabled = false;
            this.obic7TextStringKasiZeiHontaiNameRyaku.Size = new System.Drawing.Size(133, 19);
            this.obic7TextStringKasiZeiHontaiNameRyaku.TabIndex = 3730;
            this.obic7TextStringKasiZeiHontaiNameRyaku.TabStop = false;
            this.obic7TextStringKasiZeiHontaiNameRyaku.Value = "";
            this.obic7TextStringKasiZeiHontaiNameRyaku.VisibleFalseLock = false;
            // 
            // obic7TextStringKasiZeiHontaiCD
            // 
            this.obic7TextStringKasiZeiHontaiCD.AllowAlphabet = true;
            this.obic7TextStringKasiZeiHontaiCD.AllowKana = false;
            this.obic7TextStringKasiZeiHontaiCD.AllowNumber = true;
            this.obic7TextStringKasiZeiHontaiCD.AllowSymbol = false;
            this.obic7TextStringKasiZeiHontaiCD.AllowUnicode = false;
            this.obic7TextStringKasiZeiHontaiCD.AllowZenkaku = false;
            this.obic7TextStringKasiZeiHontaiCD.AttachedButton = null;
            this.obic7TextStringKasiZeiHontaiCD.AttachedCopyText = null;
            this.obic7TextStringKasiZeiHontaiCD.AttachedLabel = null;
            this.obic7TextStringKasiZeiHontaiCD.AttachedSearchDialog = null;
            this.obic7TextStringKasiZeiHontaiCD.CheckOnClose = false;
            this.obic7TextStringKasiZeiHontaiCD.ColumnNumber = 35;
            this.obic7TextStringKasiZeiHontaiCD.DispFuriganaText = null;
            this.obic7TextStringKasiZeiHontaiCD.EnableFalseLock = false;
            this.obic7TextStringKasiZeiHontaiCD.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiZeiHontaiCD.ErrorString = null;
            this.obic7TextStringKasiZeiHontaiCD.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringKasiZeiHontaiCD.IsKeyColumn = false;
            this.obic7TextStringKasiZeiHontaiCD.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiZeiHontaiCD.Location = new System.Drawing.Point(762, 35);
            this.obic7TextStringKasiZeiHontaiCD.MaxLengthByte = 10;
            this.obic7TextStringKasiZeiHontaiCD.Name = "obic7TextStringKasiZeiHontaiCD";
            this.obic7TextStringKasiZeiHontaiCD.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKasiZeiHontaiCD.Size = new System.Drawing.Size(67, 19);
            this.obic7TextStringKasiZeiHontaiCD.TabIndex = 3720;
            this.obic7TextStringKasiZeiHontaiCD.Value = "";
            this.obic7TextStringKasiZeiHontaiCD.VisibleFalseLock = false;
            // 
            // obic7TextStringKasiUtiwakeNameRyaku
            // 
            this.obic7TextStringKasiUtiwakeNameRyaku.AllowAlphabet = true;
            this.obic7TextStringKasiUtiwakeNameRyaku.AllowKana = true;
            this.obic7TextStringKasiUtiwakeNameRyaku.AllowNumber = true;
            this.obic7TextStringKasiUtiwakeNameRyaku.AllowSymbol = true;
            this.obic7TextStringKasiUtiwakeNameRyaku.AllowUnicode = false;
            this.obic7TextStringKasiUtiwakeNameRyaku.AllowZenkaku = true;
            this.obic7TextStringKasiUtiwakeNameRyaku.AttachedButton = null;
            this.obic7TextStringKasiUtiwakeNameRyaku.AttachedCopyText = null;
            this.obic7TextStringKasiUtiwakeNameRyaku.AttachedLabel = null;
            this.obic7TextStringKasiUtiwakeNameRyaku.AttachedSearchDialog = null;
            this.obic7TextStringKasiUtiwakeNameRyaku.CheckOnClose = false;
            this.obic7TextStringKasiUtiwakeNameRyaku.ColumnNumber = 28;
            this.obic7TextStringKasiUtiwakeNameRyaku.DispFuriganaText = null;
            this.obic7TextStringKasiUtiwakeNameRyaku.EnableFalseLock = false;
            this.obic7TextStringKasiUtiwakeNameRyaku.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiUtiwakeNameRyaku.ErrorString = null;
            this.obic7TextStringKasiUtiwakeNameRyaku.ImeMode = System.Windows.Forms.ImeMode.On;
            this.obic7TextStringKasiUtiwakeNameRyaku.IsKeyColumn = false;
            this.obic7TextStringKasiUtiwakeNameRyaku.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiUtiwakeNameRyaku.Location = new System.Drawing.Point(636, 35);
            this.obic7TextStringKasiUtiwakeNameRyaku.MaxLengthByte = 60;
            this.obic7TextStringKasiUtiwakeNameRyaku.Name = "obic7TextStringKasiUtiwakeNameRyaku";
            this.obic7TextStringKasiUtiwakeNameRyaku.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKasiUtiwakeNameRyaku.ReadOnly = true;
            this.obic7TextStringKasiUtiwakeNameRyaku.Size = new System.Drawing.Size(127, 19);
            this.obic7TextStringKasiUtiwakeNameRyaku.TabIndex = 3650;
            this.obic7TextStringKasiUtiwakeNameRyaku.TabStop = false;
            this.obic7TextStringKasiUtiwakeNameRyaku.Value = "";
            this.obic7TextStringKasiUtiwakeNameRyaku.VisibleFalseLock = false;
            // 
            // obic7TextStringKasiUtiwakeCD
            // 
            this.obic7TextStringKasiUtiwakeCD.AllowAlphabet = true;
            this.obic7TextStringKasiUtiwakeCD.AllowKana = false;
            this.obic7TextStringKasiUtiwakeCD.AllowNumber = true;
            this.obic7TextStringKasiUtiwakeCD.AllowSymbol = true;
            this.obic7TextStringKasiUtiwakeCD.AllowUnicode = false;
            this.obic7TextStringKasiUtiwakeCD.AllowZenkaku = false;
            this.obic7TextStringKasiUtiwakeCD.AttachedButton = null;
            this.obic7TextStringKasiUtiwakeCD.AttachedCopyText = null;
            this.obic7TextStringKasiUtiwakeCD.AttachedLabel = null;
            this.obic7TextStringKasiUtiwakeCD.AttachedSearchDialog = null;
            this.obic7TextStringKasiUtiwakeCD.CheckOnClose = false;
            this.obic7TextStringKasiUtiwakeCD.ColumnNumber = 27;
            this.obic7TextStringKasiUtiwakeCD.DispFuriganaText = null;
            this.obic7TextStringKasiUtiwakeCD.EnableFalseLock = false;
            this.obic7TextStringKasiUtiwakeCD.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiUtiwakeCD.ErrorString = null;
            this.obic7TextStringKasiUtiwakeCD.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringKasiUtiwakeCD.IsKeyColumn = false;
            this.obic7TextStringKasiUtiwakeCD.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiUtiwakeCD.Location = new System.Drawing.Point(570, 35);
            this.obic7TextStringKasiUtiwakeCD.MaxLengthByte = 10;
            this.obic7TextStringKasiUtiwakeCD.Name = "obic7TextStringKasiUtiwakeCD";
            this.obic7TextStringKasiUtiwakeCD.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKasiUtiwakeCD.Size = new System.Drawing.Size(67, 19);
            this.obic7TextStringKasiUtiwakeCD.TabIndex = 3640;
            this.obic7TextStringKasiUtiwakeCD.Value = "";
            this.obic7TextStringKasiUtiwakeCD.VisibleFalseLock = false;
            // 
            // obic7TextNumberKasiKingaku
            // 
            this.obic7TextNumberKasiKingaku.AttachedButton = null;
            this.obic7TextNumberKasiKingaku.AttachedCopyText = null;
            this.obic7TextNumberKasiKingaku.AttachedLabel = null;
            this.obic7TextNumberKasiKingaku.AttachedSearchDialog = null;
            this.obic7TextNumberKasiKingaku.CheckOnClose = false;
            this.obic7TextNumberKasiKingaku.ColumnNumber = 40;
            this.obic7TextNumberKasiKingaku.EnableFalseLock = false;
            this.obic7TextNumberKasiKingaku.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberKasiKingaku.ErrorString = null;
            this.obic7TextNumberKasiKingaku.ForeColor = System.Drawing.Color.Red;
            this.obic7TextNumberKasiKingaku.Format = "#,##0";
            this.obic7TextNumberKasiKingaku.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.obic7TextNumberKasiKingaku.IsKeyColumn = false;
            this.obic7TextNumberKasiKingaku.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberKasiKingaku.Location = new System.Drawing.Point(960, 17);
            this.obic7TextNumberKasiKingaku.MaxLengthByte = 18;
            this.obic7TextNumberKasiKingaku.MaxValue = new decimal(new int[] {
            1316134911,
            2328,
            0,
            0});
            this.obic7TextNumberKasiKingaku.MinusColor = System.Drawing.Color.Red;
            this.obic7TextNumberKasiKingaku.MinValue = new decimal(new int[] {
            1316134911,
            2328,
            0,
            -2147483648});
            this.obic7TextNumberKasiKingaku.Name = "obic7TextNumberKasiKingaku";
            this.obic7TextNumberKasiKingaku.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextNumberKasiKingaku.Size = new System.Drawing.Size(132, 19);
            this.obic7TextNumberKasiKingaku.TabIndex = 3770;
            this.obic7TextNumberKasiKingaku.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.obic7TextNumberKasiKingaku.Value = null;
            this.obic7TextNumberKasiKingaku.VisibleFalseLock = false;
            // 
            // obic7TextStringKasiBumonNameRyaku
            // 
            this.obic7TextStringKasiBumonNameRyaku.AllowAlphabet = true;
            this.obic7TextStringKasiBumonNameRyaku.AllowKana = true;
            this.obic7TextStringKasiBumonNameRyaku.AllowNumber = true;
            this.obic7TextStringKasiBumonNameRyaku.AllowSymbol = true;
            this.obic7TextStringKasiBumonNameRyaku.AllowUnicode = false;
            this.obic7TextStringKasiBumonNameRyaku.AllowZenkaku = true;
            this.obic7TextStringKasiBumonNameRyaku.AttachedButton = null;
            this.obic7TextStringKasiBumonNameRyaku.AttachedCopyText = null;
            this.obic7TextStringKasiBumonNameRyaku.AttachedLabel = null;
            this.obic7TextStringKasiBumonNameRyaku.AttachedSearchDialog = null;
            this.obic7TextStringKasiBumonNameRyaku.CheckOnClose = false;
            this.obic7TextStringKasiBumonNameRyaku.ColumnNumber = 34;
            this.obic7TextStringKasiBumonNameRyaku.DispFuriganaText = null;
            this.obic7TextStringKasiBumonNameRyaku.EnableFalseLock = false;
            this.obic7TextStringKasiBumonNameRyaku.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiBumonNameRyaku.ErrorString = null;
            this.obic7TextStringKasiBumonNameRyaku.ImeMode = System.Windows.Forms.ImeMode.On;
            this.obic7TextStringKasiBumonNameRyaku.IsKeyColumn = false;
            this.obic7TextStringKasiBumonNameRyaku.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiBumonNameRyaku.Location = new System.Drawing.Point(828, 17);
            this.obic7TextStringKasiBumonNameRyaku.MaxLengthByte = 60;
            this.obic7TextStringKasiBumonNameRyaku.Name = "obic7TextStringKasiBumonNameRyaku";
            this.obic7TextStringKasiBumonNameRyaku.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKasiBumonNameRyaku.ReadOnly = true;
            this.obic7TextStringKasiBumonNameRyaku.Size = new System.Drawing.Size(133, 19);
            this.obic7TextStringKasiBumonNameRyaku.TabIndex = 3710;
            this.obic7TextStringKasiBumonNameRyaku.TabStop = false;
            this.obic7TextStringKasiBumonNameRyaku.Value = "";
            this.obic7TextStringKasiBumonNameRyaku.VisibleFalseLock = false;
            // 
            // obic7TextStringKasiBumonCD
            // 
            this.obic7TextStringKasiBumonCD.AllowAlphabet = true;
            this.obic7TextStringKasiBumonCD.AllowKana = false;
            this.obic7TextStringKasiBumonCD.AllowNumber = true;
            this.obic7TextStringKasiBumonCD.AllowSymbol = true;
            this.obic7TextStringKasiBumonCD.AllowUnicode = false;
            this.obic7TextStringKasiBumonCD.AllowZenkaku = false;
            this.obic7TextStringKasiBumonCD.AttachedButton = null;
            this.obic7TextStringKasiBumonCD.AttachedCopyText = null;
            this.obic7TextStringKasiBumonCD.AttachedLabel = null;
            this.obic7TextStringKasiBumonCD.AttachedSearchDialog = null;
            this.obic7TextStringKasiBumonCD.CheckOnClose = false;
            this.obic7TextStringKasiBumonCD.ColumnNumber = 33;
            this.obic7TextStringKasiBumonCD.DispFuriganaText = null;
            this.obic7TextStringKasiBumonCD.EnableFalseLock = false;
            this.obic7TextStringKasiBumonCD.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiBumonCD.ErrorString = null;
            this.obic7TextStringKasiBumonCD.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringKasiBumonCD.IsKeyColumn = false;
            this.obic7TextStringKasiBumonCD.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiBumonCD.Location = new System.Drawing.Point(762, 17);
            this.obic7TextStringKasiBumonCD.MaxLengthByte = 10;
            this.obic7TextStringKasiBumonCD.Name = "obic7TextStringKasiBumonCD";
            this.obic7TextStringKasiBumonCD.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKasiBumonCD.Size = new System.Drawing.Size(67, 19);
            this.obic7TextStringKasiBumonCD.TabIndex = 3700;
            this.obic7TextStringKasiBumonCD.Tag = "";
            this.obic7TextStringKasiBumonCD.Value = "";
            this.obic7TextStringKasiBumonCD.VisibleFalseLock = false;
            // 
            // obic7TextStringKasiHojoNameRyaku
            // 
            this.obic7TextStringKasiHojoNameRyaku.AllowAlphabet = true;
            this.obic7TextStringKasiHojoNameRyaku.AllowKana = true;
            this.obic7TextStringKasiHojoNameRyaku.AllowNumber = true;
            this.obic7TextStringKasiHojoNameRyaku.AllowSymbol = true;
            this.obic7TextStringKasiHojoNameRyaku.AllowUnicode = false;
            this.obic7TextStringKasiHojoNameRyaku.AllowZenkaku = true;
            this.obic7TextStringKasiHojoNameRyaku.AttachedButton = null;
            this.obic7TextStringKasiHojoNameRyaku.AttachedCopyText = null;
            this.obic7TextStringKasiHojoNameRyaku.AttachedLabel = null;
            this.obic7TextStringKasiHojoNameRyaku.AttachedSearchDialog = null;
            this.obic7TextStringKasiHojoNameRyaku.CheckOnClose = false;
            this.obic7TextStringKasiHojoNameRyaku.ColumnNumber = 26;
            this.obic7TextStringKasiHojoNameRyaku.DispFuriganaText = null;
            this.obic7TextStringKasiHojoNameRyaku.EnableFalseLock = false;
            this.obic7TextStringKasiHojoNameRyaku.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiHojoNameRyaku.ErrorString = null;
            this.obic7TextStringKasiHojoNameRyaku.ImeMode = System.Windows.Forms.ImeMode.On;
            this.obic7TextStringKasiHojoNameRyaku.IsKeyColumn = false;
            this.obic7TextStringKasiHojoNameRyaku.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiHojoNameRyaku.Location = new System.Drawing.Point(636, 17);
            this.obic7TextStringKasiHojoNameRyaku.MaxLengthByte = 60;
            this.obic7TextStringKasiHojoNameRyaku.Name = "obic7TextStringKasiHojoNameRyaku";
            this.obic7TextStringKasiHojoNameRyaku.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKasiHojoNameRyaku.ReadOnly = true;
            this.obic7TextStringKasiHojoNameRyaku.Size = new System.Drawing.Size(127, 19);
            this.obic7TextStringKasiHojoNameRyaku.TabIndex = 3630;
            this.obic7TextStringKasiHojoNameRyaku.TabStop = false;
            this.obic7TextStringKasiHojoNameRyaku.Value = "";
            this.obic7TextStringKasiHojoNameRyaku.VisibleFalseLock = false;
            // 
            // obic7TextStringKasiHojoCD
            // 
            this.obic7TextStringKasiHojoCD.AllowAlphabet = true;
            this.obic7TextStringKasiHojoCD.AllowKana = false;
            this.obic7TextStringKasiHojoCD.AllowNumber = true;
            this.obic7TextStringKasiHojoCD.AllowSymbol = true;
            this.obic7TextStringKasiHojoCD.AllowUnicode = false;
            this.obic7TextStringKasiHojoCD.AllowZenkaku = false;
            this.obic7TextStringKasiHojoCD.AttachedButton = null;
            this.obic7TextStringKasiHojoCD.AttachedCopyText = null;
            this.obic7TextStringKasiHojoCD.AttachedLabel = null;
            this.obic7TextStringKasiHojoCD.AttachedSearchDialog = null;
            this.obic7TextStringKasiHojoCD.CheckOnClose = false;
            this.obic7TextStringKasiHojoCD.ColumnNumber = 25;
            this.obic7TextStringKasiHojoCD.DispFuriganaText = null;
            this.obic7TextStringKasiHojoCD.EnableFalseLock = false;
            this.obic7TextStringKasiHojoCD.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiHojoCD.ErrorString = null;
            this.obic7TextStringKasiHojoCD.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringKasiHojoCD.IsKeyColumn = false;
            this.obic7TextStringKasiHojoCD.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiHojoCD.Location = new System.Drawing.Point(570, 17);
            this.obic7TextStringKasiHojoCD.MaxLengthByte = 10;
            this.obic7TextStringKasiHojoCD.Name = "obic7TextStringKasiHojoCD";
            this.obic7TextStringKasiHojoCD.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKasiHojoCD.Size = new System.Drawing.Size(67, 19);
            this.obic7TextStringKasiHojoCD.TabIndex = 3620;
            this.obic7TextStringKasiHojoCD.Value = "";
            this.obic7TextStringKasiHojoCD.VisibleFalseLock = false;
            // 
            // obic7TextStringKasiZeikomiKBNName
            // 
            this.obic7TextStringKasiZeikomiKBNName.AllowAlphabet = true;
            this.obic7TextStringKasiZeikomiKBNName.AllowKana = true;
            this.obic7TextStringKasiZeikomiKBNName.AllowNumber = true;
            this.obic7TextStringKasiZeikomiKBNName.AllowSymbol = true;
            this.obic7TextStringKasiZeikomiKBNName.AllowUnicode = false;
            this.obic7TextStringKasiZeikomiKBNName.AllowZenkaku = true;
            this.obic7TextStringKasiZeikomiKBNName.AttachedButton = null;
            this.obic7TextStringKasiZeikomiKBNName.AttachedCopyText = null;
            this.obic7TextStringKasiZeikomiKBNName.AttachedLabel = null;
            this.obic7TextStringKasiZeikomiKBNName.AttachedSearchDialog = null;
            this.obic7TextStringKasiZeikomiKBNName.CheckOnClose = false;
            this.obic7TextStringKasiZeikomiKBNName.ColumnNumber = 39;
            this.obic7TextStringKasiZeikomiKBNName.DispFuriganaText = null;
            this.obic7TextStringKasiZeikomiKBNName.EnableFalseLock = false;
            this.obic7TextStringKasiZeikomiKBNName.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiZeikomiKBNName.ErrorString = null;
            this.obic7TextStringKasiZeikomiKBNName.ImeMode = System.Windows.Forms.ImeMode.On;
            this.obic7TextStringKasiZeikomiKBNName.IsKeyColumn = false;
            this.obic7TextStringKasiZeikomiKBNName.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiZeikomiKBNName.Location = new System.Drawing.Point(1008, -1);
            this.obic7TextStringKasiZeikomiKBNName.MaxLengthByte = 60;
            this.obic7TextStringKasiZeikomiKBNName.Name = "obic7TextStringKasiZeikomiKBNName";
            this.obic7TextStringKasiZeikomiKBNName.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKasiZeikomiKBNName.ReadOnly = true;
            this.obic7TextStringKasiZeikomiKBNName.Size = new System.Drawing.Size(84, 19);
            this.obic7TextStringKasiZeikomiKBNName.TabIndex = 3760;
            this.obic7TextStringKasiZeikomiKBNName.TabStop = false;
            this.obic7TextStringKasiZeikomiKBNName.Value = "";
            this.obic7TextStringKasiZeikomiKBNName.VisibleFalseLock = false;
            // 
            // obic7TextNumberKasiZeikomiKBN
            // 
            this.obic7TextNumberKasiZeikomiKBN.AttachedButton = null;
            this.obic7TextNumberKasiZeikomiKBN.AttachedCopyText = null;
            this.obic7TextNumberKasiZeikomiKBN.AttachedLabel = null;
            this.obic7TextNumberKasiZeikomiKBN.AttachedSearchDialog = null;
            this.obic7TextNumberKasiZeikomiKBN.CheckOnClose = false;
            this.obic7TextNumberKasiZeikomiKBN.ColumnNumber = 38;
            this.obic7TextNumberKasiZeikomiKBN.EnableFalseLock = false;
            this.obic7TextNumberKasiZeikomiKBN.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberKasiZeikomiKBN.ErrorString = null;
            this.obic7TextNumberKasiZeikomiKBN.ForeColor = System.Drawing.SystemColors.WindowText;
            this.obic7TextNumberKasiZeikomiKBN.Format = "0";
            this.obic7TextNumberKasiZeikomiKBN.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.obic7TextNumberKasiZeikomiKBN.IsKeyColumn = false;
            this.obic7TextNumberKasiZeikomiKBN.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberKasiZeikomiKBN.Location = new System.Drawing.Point(990, -1);
            this.obic7TextNumberKasiZeikomiKBN.MaxLengthByte = 1;
            this.obic7TextNumberKasiZeikomiKBN.MaxValue = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.obic7TextNumberKasiZeikomiKBN.MinusColor = System.Drawing.Color.Red;
            this.obic7TextNumberKasiZeikomiKBN.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.obic7TextNumberKasiZeikomiKBN.Name = "obic7TextNumberKasiZeikomiKBN";
            this.obic7TextNumberKasiZeikomiKBN.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextNumberKasiZeikomiKBN.Size = new System.Drawing.Size(19, 19);
            this.obic7TextNumberKasiZeikomiKBN.TabIndex = 3750;
            this.obic7TextNumberKasiZeikomiKBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.obic7TextNumberKasiZeikomiKBN.Value = null;
            this.obic7TextNumberKasiZeikomiKBN.VisibleFalseLock = false;
            // 
            // obic7TextStringKasiZeiKBN
            // 
            this.obic7TextStringKasiZeiKBN.AllowAlphabet = true;
            this.obic7TextStringKasiZeiKBN.AllowKana = false;
            this.obic7TextStringKasiZeiKBN.AllowNumber = true;
            this.obic7TextStringKasiZeiKBN.AllowSymbol = false;
            this.obic7TextStringKasiZeiKBN.AllowUnicode = false;
            this.obic7TextStringKasiZeiKBN.AllowZenkaku = false;
            this.obic7TextStringKasiZeiKBN.AttachedButton = null;
            this.obic7TextStringKasiZeiKBN.AttachedCopyText = null;
            this.obic7TextStringKasiZeiKBN.AttachedLabel = null;
            this.obic7TextStringKasiZeiKBN.AttachedSearchDialog = null;
            this.obic7TextStringKasiZeiKBN.CheckOnClose = false;
            this.obic7TextStringKasiZeiKBN.ColumnNumber = 37;
            this.obic7TextStringKasiZeiKBN.DispFuriganaText = null;
            this.obic7TextStringKasiZeiKBN.EnableFalseLock = false;
            this.obic7TextStringKasiZeiKBN.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiZeiKBN.ErrorString = null;
            this.obic7TextStringKasiZeiKBN.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringKasiZeiKBN.IsKeyColumn = false;
            this.obic7TextStringKasiZeiKBN.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiZeiKBN.Location = new System.Drawing.Point(960, -1);
            this.obic7TextStringKasiZeiKBN.MaxLengthByte = 3;
            this.obic7TextStringKasiZeiKBN.Name = "obic7TextStringKasiZeiKBN";
            this.obic7TextStringKasiZeiKBN.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKasiZeiKBN.Size = new System.Drawing.Size(31, 19);
            this.obic7TextStringKasiZeiKBN.TabIndex = 3740;
            this.obic7TextStringKasiZeiKBN.Value = "";
            this.obic7TextStringKasiZeiKBN.VisibleFalseLock = false;
            // 
            // obic7TextStringKasiZigyoushoNameRyaku
            // 
            this.obic7TextStringKasiZigyoushoNameRyaku.AllowAlphabet = true;
            this.obic7TextStringKasiZigyoushoNameRyaku.AllowKana = true;
            this.obic7TextStringKasiZigyoushoNameRyaku.AllowNumber = true;
            this.obic7TextStringKasiZigyoushoNameRyaku.AllowSymbol = true;
            this.obic7TextStringKasiZigyoushoNameRyaku.AllowUnicode = false;
            this.obic7TextStringKasiZigyoushoNameRyaku.AllowZenkaku = true;
            this.obic7TextStringKasiZigyoushoNameRyaku.AttachedButton = null;
            this.obic7TextStringKasiZigyoushoNameRyaku.AttachedCopyText = null;
            this.obic7TextStringKasiZigyoushoNameRyaku.AttachedLabel = null;
            this.obic7TextStringKasiZigyoushoNameRyaku.AttachedSearchDialog = null;
            this.obic7TextStringKasiZigyoushoNameRyaku.CheckOnClose = false;
            this.obic7TextStringKasiZigyoushoNameRyaku.ColumnNumber = 32;
            this.obic7TextStringKasiZigyoushoNameRyaku.DispFuriganaText = null;
            this.obic7TextStringKasiZigyoushoNameRyaku.EnableFalseLock = false;
            this.obic7TextStringKasiZigyoushoNameRyaku.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiZigyoushoNameRyaku.ErrorString = null;
            this.obic7TextStringKasiZigyoushoNameRyaku.ImeMode = System.Windows.Forms.ImeMode.On;
            this.obic7TextStringKasiZigyoushoNameRyaku.IsKeyColumn = false;
            this.obic7TextStringKasiZigyoushoNameRyaku.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiZigyoushoNameRyaku.Location = new System.Drawing.Point(828, -1);
            this.obic7TextStringKasiZigyoushoNameRyaku.MaxLengthByte = 60;
            this.obic7TextStringKasiZigyoushoNameRyaku.Name = "obic7TextStringKasiZigyoushoNameRyaku";
            this.obic7TextStringKasiZigyoushoNameRyaku.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKasiZigyoushoNameRyaku.ReadOnly = true;
            this.obic7TextStringKasiZigyoushoNameRyaku.Size = new System.Drawing.Size(133, 19);
            this.obic7TextStringKasiZigyoushoNameRyaku.TabIndex = 3690;
            this.obic7TextStringKasiZigyoushoNameRyaku.TabStop = false;
            this.obic7TextStringKasiZigyoushoNameRyaku.Value = "";
            this.obic7TextStringKasiZigyoushoNameRyaku.VisibleFalseLock = false;
            // 
            // obic7TextStringKasiZigyoushoCD
            // 
            this.obic7TextStringKasiZigyoushoCD.AllowAlphabet = true;
            this.obic7TextStringKasiZigyoushoCD.AllowKana = false;
            this.obic7TextStringKasiZigyoushoCD.AllowNumber = true;
            this.obic7TextStringKasiZigyoushoCD.AllowSymbol = false;
            this.obic7TextStringKasiZigyoushoCD.AllowUnicode = false;
            this.obic7TextStringKasiZigyoushoCD.AllowZenkaku = false;
            this.obic7TextStringKasiZigyoushoCD.AttachedButton = null;
            this.obic7TextStringKasiZigyoushoCD.AttachedCopyText = null;
            this.obic7TextStringKasiZigyoushoCD.AttachedLabel = null;
            this.obic7TextStringKasiZigyoushoCD.AttachedSearchDialog = null;
            this.obic7TextStringKasiZigyoushoCD.CheckOnClose = false;
            this.obic7TextStringKasiZigyoushoCD.ColumnNumber = 31;
            this.obic7TextStringKasiZigyoushoCD.DispFuriganaText = null;
            this.obic7TextStringKasiZigyoushoCD.EnableFalseLock = false;
            this.obic7TextStringKasiZigyoushoCD.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiZigyoushoCD.ErrorString = null;
            this.obic7TextStringKasiZigyoushoCD.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringKasiZigyoushoCD.IsKeyColumn = false;
            this.obic7TextStringKasiZigyoushoCD.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiZigyoushoCD.Location = new System.Drawing.Point(762, -1);
            this.obic7TextStringKasiZigyoushoCD.MaxLengthByte = 6;
            this.obic7TextStringKasiZigyoushoCD.Name = "obic7TextStringKasiZigyoushoCD";
            this.obic7TextStringKasiZigyoushoCD.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKasiZigyoushoCD.Size = new System.Drawing.Size(67, 19);
            this.obic7TextStringKasiZigyoushoCD.TabIndex = 3680;
            this.obic7TextStringKasiZigyoushoCD.Value = "";
            this.obic7TextStringKasiZigyoushoCD.VisibleFalseLock = false;
            // 
            // obic7TextStringKasiKanjouNameRyaku
            // 
            this.obic7TextStringKasiKanjouNameRyaku.AllowAlphabet = true;
            this.obic7TextStringKasiKanjouNameRyaku.AllowKana = true;
            this.obic7TextStringKasiKanjouNameRyaku.AllowNumber = true;
            this.obic7TextStringKasiKanjouNameRyaku.AllowSymbol = true;
            this.obic7TextStringKasiKanjouNameRyaku.AllowUnicode = false;
            this.obic7TextStringKasiKanjouNameRyaku.AllowZenkaku = true;
            this.obic7TextStringKasiKanjouNameRyaku.AttachedButton = null;
            this.obic7TextStringKasiKanjouNameRyaku.AttachedCopyText = null;
            this.obic7TextStringKasiKanjouNameRyaku.AttachedLabel = null;
            this.obic7TextStringKasiKanjouNameRyaku.AttachedSearchDialog = null;
            this.obic7TextStringKasiKanjouNameRyaku.CheckOnClose = false;
            this.obic7TextStringKasiKanjouNameRyaku.ColumnNumber = 24;
            this.obic7TextStringKasiKanjouNameRyaku.DispFuriganaText = null;
            this.obic7TextStringKasiKanjouNameRyaku.EnableFalseLock = false;
            this.obic7TextStringKasiKanjouNameRyaku.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiKanjouNameRyaku.ErrorString = null;
            this.obic7TextStringKasiKanjouNameRyaku.ImeMode = System.Windows.Forms.ImeMode.On;
            this.obic7TextStringKasiKanjouNameRyaku.IsKeyColumn = false;
            this.obic7TextStringKasiKanjouNameRyaku.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiKanjouNameRyaku.Location = new System.Drawing.Point(636, -1);
            this.obic7TextStringKasiKanjouNameRyaku.MaxLengthByte = 60;
            this.obic7TextStringKasiKanjouNameRyaku.Name = "obic7TextStringKasiKanjouNameRyaku";
            this.obic7TextStringKasiKanjouNameRyaku.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKasiKanjouNameRyaku.ReadOnly = true;
            this.obic7TextStringKasiKanjouNameRyaku.Size = new System.Drawing.Size(127, 19);
            this.obic7TextStringKasiKanjouNameRyaku.TabIndex = 3610;
            this.obic7TextStringKasiKanjouNameRyaku.TabStop = false;
            this.obic7TextStringKasiKanjouNameRyaku.Value = "";
            this.obic7TextStringKasiKanjouNameRyaku.VisibleFalseLock = false;
            // 
            // obic7TextStringKasiKanjouCD
            // 
            this.obic7TextStringKasiKanjouCD.AllowAlphabet = true;
            this.obic7TextStringKasiKanjouCD.AllowKana = false;
            this.obic7TextStringKasiKanjouCD.AllowNumber = true;
            this.obic7TextStringKasiKanjouCD.AllowSymbol = false;
            this.obic7TextStringKasiKanjouCD.AllowUnicode = false;
            this.obic7TextStringKasiKanjouCD.AllowZenkaku = false;
            this.obic7TextStringKasiKanjouCD.AttachedButton = null;
            this.obic7TextStringKasiKanjouCD.AttachedCopyText = null;
            this.obic7TextStringKasiKanjouCD.AttachedLabel = null;
            this.obic7TextStringKasiKanjouCD.AttachedSearchDialog = null;
            this.obic7TextStringKasiKanjouCD.CheckOnClose = false;
            this.obic7TextStringKasiKanjouCD.ColumnNumber = 23;
            this.obic7TextStringKasiKanjouCD.DispFuriganaText = null;
            this.obic7TextStringKasiKanjouCD.EnableFalseLock = false;
            this.obic7TextStringKasiKanjouCD.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiKanjouCD.ErrorString = null;
            this.obic7TextStringKasiKanjouCD.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringKasiKanjouCD.IsKeyColumn = false;
            this.obic7TextStringKasiKanjouCD.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiKanjouCD.Location = new System.Drawing.Point(570, -1);
            this.obic7TextStringKasiKanjouCD.MaxLengthByte = 10;
            this.obic7TextStringKasiKanjouCD.Name = "obic7TextStringKasiKanjouCD";
            this.obic7TextStringKasiKanjouCD.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKasiKanjouCD.Size = new System.Drawing.Size(67, 19);
            this.obic7TextStringKasiKanjouCD.TabIndex = 3600;
            this.obic7TextStringKasiKanjouCD.Value = "";
            this.obic7TextStringKasiKanjouCD.VisibleFalseLock = false;
            // 
            // obic7TextStringKasiBunsekiCD4
            // 
            this.obic7TextStringKasiBunsekiCD4.AllowAlphabet = true;
            this.obic7TextStringKasiBunsekiCD4.AllowKana = true;
            this.obic7TextStringKasiBunsekiCD4.AllowNumber = true;
            this.obic7TextStringKasiBunsekiCD4.AllowSymbol = true;
            this.obic7TextStringKasiBunsekiCD4.AllowUnicode = false;
            this.obic7TextStringKasiBunsekiCD4.AllowZenkaku = true;
            this.obic7TextStringKasiBunsekiCD4.AttachedButton = null;
            this.obic7TextStringKasiBunsekiCD4.AttachedCopyText = null;
            this.obic7TextStringKasiBunsekiCD4.AttachedLabel = null;
            this.obic7TextStringKasiBunsekiCD4.AttachedSearchDialog = null;
            this.obic7TextStringKasiBunsekiCD4.CheckOnClose = false;
            this.obic7TextStringKasiBunsekiCD4.ColumnNumber = 51;
            this.obic7TextStringKasiBunsekiCD4.DispFuriganaText = null;
            this.obic7TextStringKasiBunsekiCD4.EnableFalseLock = false;
            this.obic7TextStringKasiBunsekiCD4.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiBunsekiCD4.ErrorString = null;
            this.obic7TextStringKasiBunsekiCD4.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringKasiBunsekiCD4.IsKeyColumn = false;
            this.obic7TextStringKasiBunsekiCD4.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiBunsekiCD4.Location = new System.Drawing.Point(532, 78);
            this.obic7TextStringKasiBunsekiCD4.MaxLengthByte = 32767;
            this.obic7TextStringKasiBunsekiCD4.Name = "obic7TextStringKasiBunsekiCD4";
            this.obic7TextStringKasiBunsekiCD4.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKasiBunsekiCD4.Size = new System.Drawing.Size(31, 19);
            this.obic7TextStringKasiBunsekiCD4.TabIndex = 4576;
            this.obic7TextStringKasiBunsekiCD4.Value = "";
            this.obic7TextStringKasiBunsekiCD4.Visible = false;
            this.obic7TextStringKasiBunsekiCD4.VisibleFalseLock = true;
            // 
            // obic7TextStringKariBunsekiCD4
            // 
            this.obic7TextStringKariBunsekiCD4.AllowAlphabet = true;
            this.obic7TextStringKariBunsekiCD4.AllowKana = true;
            this.obic7TextStringKariBunsekiCD4.AllowNumber = true;
            this.obic7TextStringKariBunsekiCD4.AllowSymbol = true;
            this.obic7TextStringKariBunsekiCD4.AllowUnicode = false;
            this.obic7TextStringKariBunsekiCD4.AllowZenkaku = true;
            this.obic7TextStringKariBunsekiCD4.AttachedButton = null;
            this.obic7TextStringKariBunsekiCD4.AttachedCopyText = null;
            this.obic7TextStringKariBunsekiCD4.AttachedLabel = null;
            this.obic7TextStringKariBunsekiCD4.AttachedSearchDialog = null;
            this.obic7TextStringKariBunsekiCD4.CheckOnClose = false;
            this.obic7TextStringKariBunsekiCD4.ColumnNumber = 48;
            this.obic7TextStringKariBunsekiCD4.DispFuriganaText = null;
            this.obic7TextStringKariBunsekiCD4.EnableFalseLock = false;
            this.obic7TextStringKariBunsekiCD4.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariBunsekiCD4.ErrorString = null;
            this.obic7TextStringKariBunsekiCD4.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringKariBunsekiCD4.IsKeyColumn = false;
            this.obic7TextStringKariBunsekiCD4.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariBunsekiCD4.Location = new System.Drawing.Point(87, 78);
            this.obic7TextStringKariBunsekiCD4.MaxLengthByte = 32767;
            this.obic7TextStringKariBunsekiCD4.Name = "obic7TextStringKariBunsekiCD4";
            this.obic7TextStringKariBunsekiCD4.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKariBunsekiCD4.Size = new System.Drawing.Size(31, 19);
            this.obic7TextStringKariBunsekiCD4.TabIndex = 4575;
            this.obic7TextStringKariBunsekiCD4.Value = "";
            this.obic7TextStringKariBunsekiCD4.Visible = false;
            this.obic7TextStringKariBunsekiCD4.VisibleFalseLock = true;
            // 
            // obic7TextStringKasiBunsekiCD5
            // 
            this.obic7TextStringKasiBunsekiCD5.AllowAlphabet = true;
            this.obic7TextStringKasiBunsekiCD5.AllowKana = true;
            this.obic7TextStringKasiBunsekiCD5.AllowNumber = true;
            this.obic7TextStringKasiBunsekiCD5.AllowSymbol = true;
            this.obic7TextStringKasiBunsekiCD5.AllowUnicode = false;
            this.obic7TextStringKasiBunsekiCD5.AllowZenkaku = true;
            this.obic7TextStringKasiBunsekiCD5.AttachedButton = null;
            this.obic7TextStringKasiBunsekiCD5.AttachedCopyText = null;
            this.obic7TextStringKasiBunsekiCD5.AttachedLabel = null;
            this.obic7TextStringKasiBunsekiCD5.AttachedSearchDialog = null;
            this.obic7TextStringKasiBunsekiCD5.CheckOnClose = false;
            this.obic7TextStringKasiBunsekiCD5.ColumnNumber = 52;
            this.obic7TextStringKasiBunsekiCD5.DispFuriganaText = null;
            this.obic7TextStringKasiBunsekiCD5.EnableFalseLock = false;
            this.obic7TextStringKasiBunsekiCD5.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiBunsekiCD5.ErrorString = null;
            this.obic7TextStringKasiBunsekiCD5.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringKasiBunsekiCD5.IsKeyColumn = false;
            this.obic7TextStringKasiBunsekiCD5.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKasiBunsekiCD5.Location = new System.Drawing.Point(568, 78);
            this.obic7TextStringKasiBunsekiCD5.MaxLengthByte = 32767;
            this.obic7TextStringKasiBunsekiCD5.Name = "obic7TextStringKasiBunsekiCD5";
            this.obic7TextStringKasiBunsekiCD5.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKasiBunsekiCD5.Size = new System.Drawing.Size(31, 19);
            this.obic7TextStringKasiBunsekiCD5.TabIndex = 4543;
            this.obic7TextStringKasiBunsekiCD5.Value = "";
            this.obic7TextStringKasiBunsekiCD5.Visible = false;
            this.obic7TextStringKasiBunsekiCD5.VisibleFalseLock = true;
            // 
            // obic7TextStringKariBunsekiCD5
            // 
            this.obic7TextStringKariBunsekiCD5.AllowAlphabet = true;
            this.obic7TextStringKariBunsekiCD5.AllowKana = true;
            this.obic7TextStringKariBunsekiCD5.AllowNumber = true;
            this.obic7TextStringKariBunsekiCD5.AllowSymbol = true;
            this.obic7TextStringKariBunsekiCD5.AllowUnicode = false;
            this.obic7TextStringKariBunsekiCD5.AllowZenkaku = true;
            this.obic7TextStringKariBunsekiCD5.AttachedButton = null;
            this.obic7TextStringKariBunsekiCD5.AttachedCopyText = null;
            this.obic7TextStringKariBunsekiCD5.AttachedLabel = null;
            this.obic7TextStringKariBunsekiCD5.AttachedSearchDialog = null;
            this.obic7TextStringKariBunsekiCD5.CheckOnClose = false;
            this.obic7TextStringKariBunsekiCD5.ColumnNumber = 49;
            this.obic7TextStringKariBunsekiCD5.DispFuriganaText = null;
            this.obic7TextStringKariBunsekiCD5.EnableFalseLock = false;
            this.obic7TextStringKariBunsekiCD5.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariBunsekiCD5.ErrorString = null;
            this.obic7TextStringKariBunsekiCD5.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringKariBunsekiCD5.IsKeyColumn = false;
            this.obic7TextStringKariBunsekiCD5.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringKariBunsekiCD5.Location = new System.Drawing.Point(124, 78);
            this.obic7TextStringKariBunsekiCD5.MaxLengthByte = 32767;
            this.obic7TextStringKariBunsekiCD5.Name = "obic7TextStringKariBunsekiCD5";
            this.obic7TextStringKariBunsekiCD5.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringKariBunsekiCD5.Size = new System.Drawing.Size(31, 19);
            this.obic7TextStringKariBunsekiCD5.TabIndex = 4542;
            this.obic7TextStringKariBunsekiCD5.Value = "";
            this.obic7TextStringKariBunsekiCD5.Visible = false;
            this.obic7TextStringKariBunsekiCD5.VisibleFalseLock = true;
            // 
            // obic7TextNumberGyouNO
            // 
            this.obic7TextNumberGyouNO.AttachedButton = null;
            this.obic7TextNumberGyouNO.AttachedCopyText = null;
            this.obic7TextNumberGyouNO.AttachedLabel = null;
            this.obic7TextNumberGyouNO.AttachedSearchDialog = null;
            this.obic7TextNumberGyouNO.CheckOnClose = false;
            this.obic7TextNumberGyouNO.ColumnNumber = 47;
            this.obic7TextNumberGyouNO.EnableFalseLock = false;
            this.obic7TextNumberGyouNO.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberGyouNO.ErrorString = null;
            this.obic7TextNumberGyouNO.ForeColor = System.Drawing.SystemColors.WindowText;
            this.obic7TextNumberGyouNO.Format = "";
            this.obic7TextNumberGyouNO.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.obic7TextNumberGyouNO.IsKeyColumn = true;
            this.obic7TextNumberGyouNO.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberGyouNO.Location = new System.Drawing.Point(0, 26);
            this.obic7TextNumberGyouNO.MaxLengthByte = 13;
            this.obic7TextNumberGyouNO.MaxValue = new decimal(new int[] {
            1316134911,
            2328,
            0,
            0});
            this.obic7TextNumberGyouNO.MinusColor = System.Drawing.Color.Red;
            this.obic7TextNumberGyouNO.MinValue = new decimal(new int[] {
            1316134911,
            2328,
            0,
            -2147483648});
            this.obic7TextNumberGyouNO.Name = "obic7TextNumberGyouNO";
            this.obic7TextNumberGyouNO.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextNumberGyouNO.ReadOnly = true;
            this.obic7TextNumberGyouNO.Size = new System.Drawing.Size(30, 19);
            this.obic7TextNumberGyouNO.TabIndex = 4300;
            this.obic7TextNumberGyouNO.TabStop = false;
            this.obic7TextNumberGyouNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.obic7TextNumberGyouNO.Value = null;
            this.obic7TextNumberGyouNO.Visible = false;
            this.obic7TextNumberGyouNO.VisibleFalseLock = true;
            // 
            // obic7TextStringMeisaiTekiyou
            // 
            this.obic7TextStringMeisaiTekiyou.AllowAlphabet = true;
            this.obic7TextStringMeisaiTekiyou.AllowKana = true;
            this.obic7TextStringMeisaiTekiyou.AllowNumber = true;
            this.obic7TextStringMeisaiTekiyou.AllowSymbol = true;
            this.obic7TextStringMeisaiTekiyou.AllowUnicode = false;
            this.obic7TextStringMeisaiTekiyou.AllowZenkaku = true;
            this.obic7TextStringMeisaiTekiyou.AttachedButton = null;
            this.obic7TextStringMeisaiTekiyou.AttachedCopyText = null;
            this.obic7TextStringMeisaiTekiyou.AttachedLabel = null;
            this.obic7TextStringMeisaiTekiyou.AttachedSearchDialog = null;
            this.obic7TextStringMeisaiTekiyou.CheckOnClose = false;
            this.obic7TextStringMeisaiTekiyou.ColumnNumber = 46;
            this.obic7TextStringMeisaiTekiyou.DispFuriganaText = null;
            this.obic7TextStringMeisaiTekiyou.EnableFalseLock = false;
            this.obic7TextStringMeisaiTekiyou.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringMeisaiTekiyou.ErrorString = null;
            this.obic7TextStringMeisaiTekiyou.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.obic7TextStringMeisaiTekiyou.IsKeyColumn = false;
            this.obic7TextStringMeisaiTekiyou.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringMeisaiTekiyou.Location = new System.Drawing.Point(1110, -1);
            this.obic7TextStringMeisaiTekiyou.MaxLengthByte = 60;
            this.obic7TextStringMeisaiTekiyou.Multiline = true;
            this.obic7TextStringMeisaiTekiyou.Name = "obic7TextStringMeisaiTekiyou";
            this.obic7TextStringMeisaiTekiyou.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringMeisaiTekiyou.Size = new System.Drawing.Size(83, 73);
            this.obic7TextStringMeisaiTekiyou.TabIndex = 4100;
            this.obic7TextStringMeisaiTekiyou.Value = "";
            this.obic7TextStringMeisaiTekiyou.VisibleFalseLock = false;
            // 
            // obic7TextStringDummy
            // 
            this.obic7TextStringDummy.AllowAlphabet = true;
            this.obic7TextStringDummy.AllowKana = false;
            this.obic7TextStringDummy.AllowNumber = true;
            this.obic7TextStringDummy.AllowSymbol = false;
            this.obic7TextStringDummy.AllowUnicode = false;
            this.obic7TextStringDummy.AllowZenkaku = false;
            this.obic7TextStringDummy.AttachedButton = null;
            this.obic7TextStringDummy.AttachedCopyText = null;
            this.obic7TextStringDummy.AttachedLabel = null;
            this.obic7TextStringDummy.AttachedSearchDialog = null;
            this.obic7TextStringDummy.CheckOnClose = false;
            this.obic7TextStringDummy.ColumnNumber = 54;
            this.obic7TextStringDummy.DispFuriganaText = null;
            this.obic7TextStringDummy.EnableFalseLock = false;
            this.obic7TextStringDummy.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringDummy.ErrorString = null;
            this.obic7TextStringDummy.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringDummy.IsKeyColumn = false;
            this.obic7TextStringDummy.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringDummy.Location = new System.Drawing.Point(17, 78);
            this.obic7TextStringDummy.MaxLengthByte = 5;
            this.obic7TextStringDummy.Name = "obic7TextStringDummy";
            this.obic7TextStringDummy.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringDummy.Size = new System.Drawing.Size(14, 19);
            this.obic7TextStringDummy.TabIndex = 3050;
            this.obic7TextStringDummy.Value = "";
            this.obic7TextStringDummy.VisibleFalseLock = false;
            // 
            // obic7TextStringDummy2
            // 
            this.obic7TextStringDummy2.AllowAlphabet = true;
            this.obic7TextStringDummy2.AllowKana = false;
            this.obic7TextStringDummy2.AllowNumber = true;
            this.obic7TextStringDummy2.AllowSymbol = false;
            this.obic7TextStringDummy2.AllowUnicode = false;
            this.obic7TextStringDummy2.AllowZenkaku = false;
            this.obic7TextStringDummy2.AttachedButton = null;
            this.obic7TextStringDummy2.AttachedCopyText = null;
            this.obic7TextStringDummy2.AttachedLabel = null;
            this.obic7TextStringDummy2.AttachedSearchDialog = null;
            this.obic7TextStringDummy2.CheckOnClose = false;
            this.obic7TextStringDummy2.ColumnNumber = 55;
            this.obic7TextStringDummy2.DispFuriganaText = null;
            this.obic7TextStringDummy2.EnableFalseLock = false;
            this.obic7TextStringDummy2.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringDummy2.ErrorString = null;
            this.obic7TextStringDummy2.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringDummy2.IsKeyColumn = false;
            this.obic7TextStringDummy2.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringDummy2.Location = new System.Drawing.Point(37, 78);
            this.obic7TextStringDummy2.MaxLengthByte = 12;
            this.obic7TextStringDummy2.Name = "obic7TextStringDummy2";
            this.obic7TextStringDummy2.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringDummy2.Size = new System.Drawing.Size(16, 19);
            this.obic7TextStringDummy2.TabIndex = 5132;
            this.obic7TextStringDummy2.TabStop = false;
            this.obic7TextStringDummy2.Value = "";
            this.obic7TextStringDummy2.VisibleFalseLock = false;
            // 
            // obic7TextNumberTotalKasiSougaku
            // 
            this.obic7TextNumberTotalKasiSougaku.AttachedButton = null;
            this.obic7TextNumberTotalKasiSougaku.AttachedCopyText = null;
            this.obic7TextNumberTotalKasiSougaku.AttachedLabel = null;
            this.obic7TextNumberTotalKasiSougaku.AttachedSearchDialog = null;
            this.obic7TextNumberTotalKasiSougaku.CheckOnClose = false;
            this.obic7TextNumberTotalKasiSougaku.ColumnNumber = 10;
            this.obic7TextNumberTotalKasiSougaku.EnableFalseLock = false;
            this.obic7TextNumberTotalKasiSougaku.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberTotalKasiSougaku.ErrorString = null;
            this.obic7TextNumberTotalKasiSougaku.ForeColor = System.Drawing.Color.Red;
            this.obic7TextNumberTotalKasiSougaku.Format = "#,##0";
            this.obic7TextNumberTotalKasiSougaku.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.obic7TextNumberTotalKasiSougaku.IsKeyColumn = false;
            this.obic7TextNumberTotalKasiSougaku.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberTotalKasiSougaku.Location = new System.Drawing.Point(966, 517);
            this.obic7TextNumberTotalKasiSougaku.MaxLengthByte = 22;
            this.obic7TextNumberTotalKasiSougaku.MaxValue = new decimal(new int[] {
            1874919423,
            2328306,
            0,
            0});
            this.obic7TextNumberTotalKasiSougaku.MinusColor = System.Drawing.Color.Red;
            this.obic7TextNumberTotalKasiSougaku.MinValue = new decimal(new int[] {
            1874919423,
            2328306,
            0,
            -2147483648});
            this.obic7TextNumberTotalKasiSougaku.Name = "obic7TextNumberTotalKasiSougaku";
            this.obic7TextNumberTotalKasiSougaku.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextNumberTotalKasiSougaku.ReadOnly = true;
            this.obic7TextNumberTotalKasiSougaku.Size = new System.Drawing.Size(132, 19);
            this.obic7TextNumberTotalKasiSougaku.TabIndex = 5120;
            this.obic7TextNumberTotalKasiSougaku.TabStop = false;
            this.obic7TextNumberTotalKasiSougaku.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.obic7TextNumberTotalKasiSougaku.Value = null;
            this.obic7TextNumberTotalKasiSougaku.VisibleFalseLock = false;
            this.obic7TextNumberTotalKasiSougaku.Enter += new System.EventHandler(this.obic7TextNumberTotalKasiSougaku_Enter);
            // 
            // obic7TextNumberTotalKariSougaku
            // 
            this.obic7TextNumberTotalKariSougaku.AttachedButton = null;
            this.obic7TextNumberTotalKariSougaku.AttachedCopyText = null;
            this.obic7TextNumberTotalKariSougaku.AttachedLabel = null;
            this.obic7TextNumberTotalKariSougaku.AttachedSearchDialog = null;
            this.obic7TextNumberTotalKariSougaku.CheckOnClose = false;
            this.obic7TextNumberTotalKariSougaku.ColumnNumber = 10;
            this.obic7TextNumberTotalKariSougaku.EnableFalseLock = false;
            this.obic7TextNumberTotalKariSougaku.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberTotalKariSougaku.ErrorString = null;
            this.obic7TextNumberTotalKariSougaku.ForeColor = System.Drawing.Color.Red;
            this.obic7TextNumberTotalKariSougaku.Format = "#,##0";
            this.obic7TextNumberTotalKariSougaku.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.obic7TextNumberTotalKariSougaku.IsKeyColumn = false;
            this.obic7TextNumberTotalKariSougaku.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberTotalKariSougaku.Location = new System.Drawing.Point(444, 517);
            this.obic7TextNumberTotalKariSougaku.MaxLengthByte = 22;
            this.obic7TextNumberTotalKariSougaku.MaxValue = new decimal(new int[] {
            1874919423,
            2328306,
            0,
            0});
            this.obic7TextNumberTotalKariSougaku.MinusColor = System.Drawing.Color.Red;
            this.obic7TextNumberTotalKariSougaku.MinValue = new decimal(new int[] {
            1874919423,
            2328306,
            0,
            -2147483648});
            this.obic7TextNumberTotalKariSougaku.Name = "obic7TextNumberTotalKariSougaku";
            this.obic7TextNumberTotalKariSougaku.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextNumberTotalKariSougaku.ReadOnly = true;
            this.obic7TextNumberTotalKariSougaku.Size = new System.Drawing.Size(132, 19);
            this.obic7TextNumberTotalKariSougaku.TabIndex = 5120;
            this.obic7TextNumberTotalKariSougaku.TabStop = false;
            this.obic7TextNumberTotalKariSougaku.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.obic7TextNumberTotalKariSougaku.Value = null;
            this.obic7TextNumberTotalKariSougaku.VisibleFalseLock = false;
            this.obic7TextNumberTotalKariSougaku.Enter += new System.EventHandler(this.obic7TextNumberTotalKariSougaku_Enter);
            // 
            // obic7TextNumberTotalKasiKingaku
            // 
            this.obic7TextNumberTotalKasiKingaku.AttachedButton = null;
            this.obic7TextNumberTotalKasiKingaku.AttachedCopyText = null;
            this.obic7TextNumberTotalKasiKingaku.AttachedLabel = null;
            this.obic7TextNumberTotalKasiKingaku.AttachedSearchDialog = null;
            this.obic7TextNumberTotalKasiKingaku.CheckOnClose = false;
            this.obic7TextNumberTotalKasiKingaku.ColumnNumber = 10;
            this.obic7TextNumberTotalKasiKingaku.EnableFalseLock = false;
            this.obic7TextNumberTotalKasiKingaku.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberTotalKasiKingaku.ErrorString = null;
            this.obic7TextNumberTotalKasiKingaku.ForeColor = System.Drawing.Color.Red;
            this.obic7TextNumberTotalKasiKingaku.Format = "#,##0";
            this.obic7TextNumberTotalKasiKingaku.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.obic7TextNumberTotalKasiKingaku.IsKeyColumn = false;
            this.obic7TextNumberTotalKasiKingaku.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberTotalKasiKingaku.Location = new System.Drawing.Point(702, 517);
            this.obic7TextNumberTotalKasiKingaku.MaxLengthByte = 22;
            this.obic7TextNumberTotalKasiKingaku.MaxValue = new decimal(new int[] {
            1874919423,
            2328306,
            0,
            0});
            this.obic7TextNumberTotalKasiKingaku.MinusColor = System.Drawing.Color.Red;
            this.obic7TextNumberTotalKasiKingaku.MinValue = new decimal(new int[] {
            1874919423,
            2328306,
            0,
            -2147483648});
            this.obic7TextNumberTotalKasiKingaku.Name = "obic7TextNumberTotalKasiKingaku";
            this.obic7TextNumberTotalKasiKingaku.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextNumberTotalKasiKingaku.ReadOnly = true;
            this.obic7TextNumberTotalKasiKingaku.Size = new System.Drawing.Size(132, 19);
            this.obic7TextNumberTotalKasiKingaku.TabIndex = 5200;
            this.obic7TextNumberTotalKasiKingaku.TabStop = false;
            this.obic7TextNumberTotalKasiKingaku.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.obic7TextNumberTotalKasiKingaku.Value = null;
            this.obic7TextNumberTotalKasiKingaku.VisibleFalseLock = false;
            this.obic7TextNumberTotalKasiKingaku.Enter += new System.EventHandler(this.obic7TextNumberTotalKasiKingaku_Enter);
            // 
            // obic7TextNumberTotalKariKingaku
            // 
            this.obic7TextNumberTotalKariKingaku.AttachedButton = null;
            this.obic7TextNumberTotalKariKingaku.AttachedCopyText = null;
            this.obic7TextNumberTotalKariKingaku.AttachedLabel = null;
            this.obic7TextNumberTotalKariKingaku.AttachedSearchDialog = null;
            this.obic7TextNumberTotalKariKingaku.CheckOnClose = false;
            this.obic7TextNumberTotalKariKingaku.ColumnNumber = 10;
            this.obic7TextNumberTotalKariKingaku.EnableFalseLock = false;
            this.obic7TextNumberTotalKariKingaku.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberTotalKariKingaku.ErrorString = null;
            this.obic7TextNumberTotalKariKingaku.ForeColor = System.Drawing.Color.Red;
            this.obic7TextNumberTotalKariKingaku.Format = "#,##0";
            this.obic7TextNumberTotalKariKingaku.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.obic7TextNumberTotalKariKingaku.IsKeyColumn = false;
            this.obic7TextNumberTotalKariKingaku.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberTotalKariKingaku.Location = new System.Drawing.Point(180, 517);
            this.obic7TextNumberTotalKariKingaku.MaxLengthByte = 22;
            this.obic7TextNumberTotalKariKingaku.MaxValue = new decimal(new int[] {
            1874919423,
            2328306,
            0,
            0});
            this.obic7TextNumberTotalKariKingaku.MinusColor = System.Drawing.Color.Red;
            this.obic7TextNumberTotalKariKingaku.MinValue = new decimal(new int[] {
            1874919423,
            2328306,
            0,
            -2147483648});
            this.obic7TextNumberTotalKariKingaku.Name = "obic7TextNumberTotalKariKingaku";
            this.obic7TextNumberTotalKariKingaku.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextNumberTotalKariKingaku.ReadOnly = true;
            this.obic7TextNumberTotalKariKingaku.Size = new System.Drawing.Size(132, 19);
            this.obic7TextNumberTotalKariKingaku.TabIndex = 5100;
            this.obic7TextNumberTotalKariKingaku.TabStop = false;
            this.obic7TextNumberTotalKariKingaku.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.obic7TextNumberTotalKariKingaku.Value = null;
            this.obic7TextNumberTotalKariKingaku.VisibleFalseLock = false;
            this.obic7TextNumberTotalKariKingaku.Enter += new System.EventHandler(this.obic7TextNumberTotalKariKingaku_Enter);
            // 
            // obic7TextNumberTotalKariZeigaku
            // 
            this.obic7TextNumberTotalKariZeigaku.AttachedButton = null;
            this.obic7TextNumberTotalKariZeigaku.AttachedCopyText = null;
            this.obic7TextNumberTotalKariZeigaku.AttachedLabel = null;
            this.obic7TextNumberTotalKariZeigaku.AttachedSearchDialog = null;
            this.obic7TextNumberTotalKariZeigaku.CheckOnClose = false;
            this.obic7TextNumberTotalKariZeigaku.ColumnNumber = 10;
            this.obic7TextNumberTotalKariZeigaku.EnableFalseLock = false;
            this.obic7TextNumberTotalKariZeigaku.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberTotalKariZeigaku.ErrorString = null;
            this.obic7TextNumberTotalKariZeigaku.ForeColor = System.Drawing.Color.Red;
            this.obic7TextNumberTotalKariZeigaku.Format = "#,##0";
            this.obic7TextNumberTotalKariZeigaku.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.obic7TextNumberTotalKariZeigaku.IsKeyColumn = false;
            this.obic7TextNumberTotalKariZeigaku.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberTotalKariZeigaku.Location = new System.Drawing.Point(312, 517);
            this.obic7TextNumberTotalKariZeigaku.MaxLengthByte = 22;
            this.obic7TextNumberTotalKariZeigaku.MaxValue = new decimal(new int[] {
            1874919423,
            2328306,
            0,
            0});
            this.obic7TextNumberTotalKariZeigaku.MinusColor = System.Drawing.Color.Red;
            this.obic7TextNumberTotalKariZeigaku.MinValue = new decimal(new int[] {
            1874919423,
            2328306,
            0,
            -2147483648});
            this.obic7TextNumberTotalKariZeigaku.Name = "obic7TextNumberTotalKariZeigaku";
            this.obic7TextNumberTotalKariZeigaku.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextNumberTotalKariZeigaku.ReadOnly = true;
            this.obic7TextNumberTotalKariZeigaku.Size = new System.Drawing.Size(132, 19);
            this.obic7TextNumberTotalKariZeigaku.TabIndex = 5110;
            this.obic7TextNumberTotalKariZeigaku.TabStop = false;
            this.obic7TextNumberTotalKariZeigaku.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.obic7TextNumberTotalKariZeigaku.Value = null;
            this.obic7TextNumberTotalKariZeigaku.VisibleFalseLock = false;
            this.obic7TextNumberTotalKariZeigaku.Enter += new System.EventHandler(this.obic7TextNumberTotalKariZeigaku_Enter);
            // 
            // obic7TextNumberTotalKasiZeigaku
            // 
            this.obic7TextNumberTotalKasiZeigaku.AttachedButton = null;
            this.obic7TextNumberTotalKasiZeigaku.AttachedCopyText = null;
            this.obic7TextNumberTotalKasiZeigaku.AttachedLabel = null;
            this.obic7TextNumberTotalKasiZeigaku.AttachedSearchDialog = null;
            this.obic7TextNumberTotalKasiZeigaku.CheckOnClose = false;
            this.obic7TextNumberTotalKasiZeigaku.ColumnNumber = 10;
            this.obic7TextNumberTotalKasiZeigaku.EnableFalseLock = false;
            this.obic7TextNumberTotalKasiZeigaku.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberTotalKasiZeigaku.ErrorString = null;
            this.obic7TextNumberTotalKasiZeigaku.ForeColor = System.Drawing.Color.Red;
            this.obic7TextNumberTotalKasiZeigaku.Format = "#,##0";
            this.obic7TextNumberTotalKasiZeigaku.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.obic7TextNumberTotalKasiZeigaku.IsKeyColumn = false;
            this.obic7TextNumberTotalKasiZeigaku.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberTotalKasiZeigaku.Location = new System.Drawing.Point(834, 517);
            this.obic7TextNumberTotalKasiZeigaku.MaxLengthByte = 22;
            this.obic7TextNumberTotalKasiZeigaku.MaxValue = new decimal(new int[] {
            1874919423,
            2328306,
            0,
            0});
            this.obic7TextNumberTotalKasiZeigaku.MinusColor = System.Drawing.Color.Red;
            this.obic7TextNumberTotalKasiZeigaku.MinValue = new decimal(new int[] {
            1874919423,
            2328306,
            0,
            -2147483648});
            this.obic7TextNumberTotalKasiZeigaku.Name = "obic7TextNumberTotalKasiZeigaku";
            this.obic7TextNumberTotalKasiZeigaku.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextNumberTotalKasiZeigaku.ReadOnly = true;
            this.obic7TextNumberTotalKasiZeigaku.Size = new System.Drawing.Size(132, 19);
            this.obic7TextNumberTotalKasiZeigaku.TabIndex = 5110;
            this.obic7TextNumberTotalKasiZeigaku.TabStop = false;
            this.obic7TextNumberTotalKasiZeigaku.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.obic7TextNumberTotalKasiZeigaku.Value = null;
            this.obic7TextNumberTotalKasiZeigaku.VisibleFalseLock = false;
            this.obic7TextNumberTotalKasiZeigaku.Enter += new System.EventHandler(this.obic7TextNumberTotalKasiZeigaku_Enter);
            // 
            // obic7LabelString1
            // 
            this.obic7LabelString1.AutoEllipsis = true;
            this.obic7LabelString1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelString1.ColumnNumber = 0;
            this.obic7LabelString1.EnableFalseLock = false;
            this.obic7LabelString1.ErrorString = null;
            this.obic7LabelString1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelString1.HasHelp = false;
            this.obic7LabelString1.Location = new System.Drawing.Point(966, 41);
            this.obic7LabelString1.Name = "obic7LabelString1";
            this.obic7LabelString1.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelString1.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelString1.Size = new System.Drawing.Size(65, 18);
            this.obic7LabelString1.TabIndex = 1000000053;
            this.obic7LabelString1.Text = "資金";
            this.obic7LabelString1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.obic7LabelString1.Value = "資金";
            this.obic7LabelString1.VisibleFalseLock = false;
            // 
            // obic7LabelString2
            // 
            this.obic7LabelString2.AutoEllipsis = true;
            this.obic7LabelString2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelString2.ColumnNumber = 0;
            this.obic7LabelString2.EnableFalseLock = false;
            this.obic7LabelString2.ErrorString = null;
            this.obic7LabelString2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelString2.HasHelp = false;
            this.obic7LabelString2.Location = new System.Drawing.Point(900, 41);
            this.obic7LabelString2.Name = "obic7LabelString2";
            this.obic7LabelString2.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelString2.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelString2.Size = new System.Drawing.Size(65, 18);
            this.obic7LabelString2.TabIndex = 1000000052;
            this.obic7LabelString2.Text = "分析3";
            this.obic7LabelString2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.obic7LabelString2.Value = "分析3";
            this.obic7LabelString2.VisibleFalseLock = false;
            // 
            // obic7LabelString5
            // 
            this.obic7LabelString5.AutoEllipsis = true;
            this.obic7LabelString5.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelString5.ColumnNumber = 0;
            this.obic7LabelString5.EnableFalseLock = false;
            this.obic7LabelString5.ErrorString = null;
            this.obic7LabelString5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelString5.HasHelp = false;
            this.obic7LabelString5.Location = new System.Drawing.Point(834, 41);
            this.obic7LabelString5.Name = "obic7LabelString5";
            this.obic7LabelString5.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelString5.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelString5.Size = new System.Drawing.Size(65, 18);
            this.obic7LabelString5.TabIndex = 1000000051;
            this.obic7LabelString5.Text = "分析2";
            this.obic7LabelString5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.obic7LabelString5.Value = "分析2";
            this.obic7LabelString5.VisibleFalseLock = false;
            // 
            // obic7LabelString6
            // 
            this.obic7LabelString6.AutoEllipsis = true;
            this.obic7LabelString6.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelString6.ColumnNumber = 0;
            this.obic7LabelString6.EnableFalseLock = false;
            this.obic7LabelString6.ErrorString = null;
            this.obic7LabelString6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelString6.HasHelp = false;
            this.obic7LabelString6.Location = new System.Drawing.Point(768, 41);
            this.obic7LabelString6.Name = "obic7LabelString6";
            this.obic7LabelString6.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelString6.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelString6.Size = new System.Drawing.Size(65, 18);
            this.obic7LabelString6.TabIndex = 1000000050;
            this.obic7LabelString6.Text = "分析1";
            this.obic7LabelString6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.obic7LabelString6.Value = "分析1";
            this.obic7LabelString6.VisibleFalseLock = false;
            // 
            // obic7LabelString7
            // 
            this.obic7LabelString7.AutoEllipsis = true;
            this.obic7LabelString7.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelString7.ColumnNumber = 0;
            this.obic7LabelString7.EnableFalseLock = false;
            this.obic7LabelString7.ErrorString = null;
            this.obic7LabelString7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelString7.HasHelp = false;
            this.obic7LabelString7.Location = new System.Drawing.Point(577, 41);
            this.obic7LabelString7.Name = "obic7LabelString7";
            this.obic7LabelString7.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelString7.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelString7.Size = new System.Drawing.Size(190, 18);
            this.obic7LabelString7.TabIndex = 1000000049;
            this.obic7LabelString7.Text = "取引先";
            this.obic7LabelString7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.obic7LabelString7.Value = "取引先";
            this.obic7LabelString7.VisibleFalseLock = false;
            // 
            // obic7LabelString8
            // 
            this.obic7LabelString8.AutoEllipsis = true;
            this.obic7LabelString8.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelString8.ColumnNumber = 0;
            this.obic7LabelString8.EnableFalseLock = false;
            this.obic7LabelString8.ErrorString = null;
            this.obic7LabelString8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelString8.HasHelp = false;
            this.obic7LabelString8.Location = new System.Drawing.Point(966, 22);
            this.obic7LabelString8.Name = "obic7LabelString8";
            this.obic7LabelString8.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelString8.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelString8.Size = new System.Drawing.Size(131, 18);
            this.obic7LabelString8.TabIndex = 1000000048;
            this.obic7LabelString8.Text = "金額/消費税額";
            this.obic7LabelString8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.obic7LabelString8.Value = "金額/消費税額";
            this.obic7LabelString8.VisibleFalseLock = false;
            // 
            // obic7LabelString9
            // 
            this.obic7LabelString9.AutoEllipsis = true;
            this.obic7LabelString9.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelString9.ColumnNumber = 0;
            this.obic7LabelString9.EnableFalseLock = false;
            this.obic7LabelString9.ErrorString = null;
            this.obic7LabelString9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelString9.HasHelp = false;
            this.obic7LabelString9.Location = new System.Drawing.Point(966, 3);
            this.obic7LabelString9.Name = "obic7LabelString9";
            this.obic7LabelString9.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelString9.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelString9.Size = new System.Drawing.Size(131, 18);
            this.obic7LabelString9.TabIndex = 1000000046;
            this.obic7LabelString9.Text = "税区分";
            this.obic7LabelString9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.obic7LabelString9.Value = "税区分";
            this.obic7LabelString9.VisibleFalseLock = false;
            // 
            // obic7LabelString10
            // 
            this.obic7LabelString10.AutoEllipsis = true;
            this.obic7LabelString10.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelString10.ColumnNumber = 0;
            this.obic7LabelString10.EnableFalseLock = false;
            this.obic7LabelString10.ErrorString = null;
            this.obic7LabelString10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelString10.HasHelp = false;
            this.obic7LabelString10.Location = new System.Drawing.Point(768, 22);
            this.obic7LabelString10.Name = "obic7LabelString10";
            this.obic7LabelString10.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelString10.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelString10.Size = new System.Drawing.Size(197, 18);
            this.obic7LabelString10.TabIndex = 1000000047;
            this.obic7LabelString10.Text = "消費税本体科目";
            this.obic7LabelString10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.obic7LabelString10.Value = "消費税本体科目";
            this.obic7LabelString10.VisibleFalseLock = false;
            // 
            // obic7LabelString11
            // 
            this.obic7LabelString11.AutoEllipsis = true;
            this.obic7LabelString11.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelString11.ColumnNumber = 0;
            this.obic7LabelString11.EnableFalseLock = false;
            this.obic7LabelString11.ErrorString = null;
            this.obic7LabelString11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelString11.HasHelp = false;
            this.obic7LabelString11.Location = new System.Drawing.Point(768, 3);
            this.obic7LabelString11.Name = "obic7LabelString11";
            this.obic7LabelString11.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelString11.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelString11.Size = new System.Drawing.Size(197, 18);
            this.obic7LabelString11.TabIndex = 1000000045;
            this.obic7LabelString11.Text = "事業所/部門";
            this.obic7LabelString11.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.obic7LabelString11.Value = "事業所/部門";
            this.obic7LabelString11.VisibleFalseLock = false;
            // 
            // obic7LabelString12
            // 
            this.obic7LabelString12.AutoEllipsis = true;
            this.obic7LabelString12.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelString12.ColumnNumber = 0;
            this.obic7LabelString12.EnableFalseLock = false;
            this.obic7LabelString12.ErrorString = null;
            this.obic7LabelString12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelString12.HasHelp = false;
            this.obic7LabelString12.Location = new System.Drawing.Point(577, 3);
            this.obic7LabelString12.Name = "obic7LabelString12";
            this.obic7LabelString12.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelString12.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelString12.Size = new System.Drawing.Size(190, 37);
            this.obic7LabelString12.TabIndex = 1000000044;
            this.obic7LabelString12.Text = "【貸方】\r\n総勘定/補助/内訳";
            this.obic7LabelString12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.obic7LabelString12.Value = "【貸方】\r\n総勘定/補助/内訳";
            this.obic7LabelString12.VisibleFalseLock = false;
            // 
            // obic7LabelString13
            // 
            this.obic7LabelString13.AutoEllipsis = true;
            this.obic7LabelString13.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelString13.ColumnNumber = 0;
            this.obic7LabelString13.EnableFalseLock = false;
            this.obic7LabelString13.ErrorString = null;
            this.obic7LabelString13.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelString13.HasHelp = false;
            this.obic7LabelString13.Location = new System.Drawing.Point(1117, 3);
            this.obic7LabelString13.Name = "obic7LabelString13";
            this.obic7LabelString13.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelString13.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelString13.Size = new System.Drawing.Size(82, 56);
            this.obic7LabelString13.TabIndex = 1000000054;
            this.obic7LabelString13.Text = "明細摘要";
            this.obic7LabelString13.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.obic7LabelString13.Value = "明細摘要";
            this.obic7LabelString13.VisibleFalseLock = false;
            // 
            // obic7LabelString16
            // 
            this.obic7LabelString16.AutoEllipsis = true;
            this.obic7LabelString16.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelString16.ColumnNumber = 0;
            this.obic7LabelString16.EnableFalseLock = false;
            this.obic7LabelString16.ErrorString = null;
            this.obic7LabelString16.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelString16.HasHelp = false;
            this.obic7LabelString16.Location = new System.Drawing.Point(118, 517);
            this.obic7LabelString16.Name = "obic7LabelString16";
            this.obic7LabelString16.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelString16.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelString16.Size = new System.Drawing.Size(60, 19);
            this.obic7LabelString16.TabIndex = 1000000055;
            this.obic7LabelString16.Text = "借方合計";
            this.obic7LabelString16.Value = "借方合計";
            this.obic7LabelString16.VisibleFalseLock = false;
            // 
            // obic7LabelString17
            // 
            this.obic7LabelString17.AutoEllipsis = true;
            this.obic7LabelString17.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelString17.ColumnNumber = 0;
            this.obic7LabelString17.EnableFalseLock = false;
            this.obic7LabelString17.ErrorString = null;
            this.obic7LabelString17.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelString17.HasHelp = false;
            this.obic7LabelString17.Location = new System.Drawing.Point(640, 517);
            this.obic7LabelString17.Name = "obic7LabelString17";
            this.obic7LabelString17.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelString17.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelString17.Size = new System.Drawing.Size(60, 19);
            this.obic7LabelString17.TabIndex = 1000000056;
            this.obic7LabelString17.Text = "貸方合計";
            this.obic7LabelString17.Value = "貸方合計";
            this.obic7LabelString17.VisibleFalseLock = false;
            // 
            // obic7TextStringPrevDenpyouNO
            // 
            this.obic7TextStringPrevDenpyouNO.AllowAlphabet = true;
            this.obic7TextStringPrevDenpyouNO.AllowKana = true;
            this.obic7TextStringPrevDenpyouNO.AllowNumber = true;
            this.obic7TextStringPrevDenpyouNO.AllowSymbol = true;
            this.obic7TextStringPrevDenpyouNO.AllowUnicode = false;
            this.obic7TextStringPrevDenpyouNO.AllowZenkaku = true;
            this.obic7TextStringPrevDenpyouNO.AttachedButton = null;
            this.obic7TextStringPrevDenpyouNO.AttachedCopyText = null;
            this.obic7TextStringPrevDenpyouNO.AttachedLabel = this.obic7LabelStringDenpyouNO;
            this.obic7TextStringPrevDenpyouNO.AttachedSearchDialog = null;
            this.obic7TextStringPrevDenpyouNO.CheckOnClose = false;
            this.obic7TextStringPrevDenpyouNO.ColumnNumber = 0;
            this.obic7TextStringPrevDenpyouNO.DispFuriganaText = null;
            this.obic7TextStringPrevDenpyouNO.EnableFalseLock = false;
            this.obic7TextStringPrevDenpyouNO.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringPrevDenpyouNO.ErrorString = null;
            this.obic7TextStringPrevDenpyouNO.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.obic7TextStringPrevDenpyouNO.IsKeyColumn = false;
            this.obic7TextStringPrevDenpyouNO.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringPrevDenpyouNO.Location = new System.Drawing.Point(894, 3);
            this.obic7TextStringPrevDenpyouNO.MaxLengthByte = 12;
            this.obic7TextStringPrevDenpyouNO.Name = "obic7TextStringPrevDenpyouNO";
            this.obic7TextStringPrevDenpyouNO.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringPrevDenpyouNO.ReadOnly = true;
            this.obic7TextStringPrevDenpyouNO.Size = new System.Drawing.Size(84, 19);
            this.obic7TextStringPrevDenpyouNO.TabIndex = 1130;
            this.obic7TextStringPrevDenpyouNO.TabStop = false;
            this.obic7TextStringPrevDenpyouNO.Value = "";
            this.obic7TextStringPrevDenpyouNO.VisibleFalseLock = false;
            this.obic7TextStringPrevDenpyouNO.Enter += new System.EventHandler(this.obic7TextStringPrevDenpyouNO_Enter);
            // 
            // obic7TextSeparatorDenpyouNo
            // 
            this.obic7TextSeparatorDenpyouNo.ColumnNumber = 0;
            this.obic7TextSeparatorDenpyouNo.IsRefrectLowerOrder = false;
            this.obic7TextSeparatorDenpyouNo.IsRefrectUpperOrder = false;
            this.obic7TextSeparatorDenpyouNo.IsTerminator = false;
            this.obic7TextSeparatorDenpyouNo.Location = new System.Drawing.Point(523, 4);
            this.obic7TextSeparatorDenpyouNo.Name = "obic7TextSeparatorDenpyouNo";
            this.obic7TextSeparatorDenpyouNo.Size = new System.Drawing.Size(0, 19);
            this.obic7TextSeparatorDenpyouNo.TabIndex = 1075;
            // 
            // toolTip1
            // 
            this.toolTip1.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Nothing;
            // 
            // obic7TextStringHeadDummy
            // 
            this.obic7TextStringHeadDummy.AllowAlphabet = true;
            this.obic7TextStringHeadDummy.AllowKana = true;
            this.obic7TextStringHeadDummy.AllowNumber = true;
            this.obic7TextStringHeadDummy.AllowSymbol = true;
            this.obic7TextStringHeadDummy.AllowUnicode = false;
            this.obic7TextStringHeadDummy.AllowZenkaku = true;
            this.obic7TextStringHeadDummy.AttachedButton = null;
            this.obic7TextStringHeadDummy.AttachedCopyText = null;
            this.obic7TextStringHeadDummy.AttachedLabel = null;
            this.obic7TextStringHeadDummy.AttachedSearchDialog = null;
            this.obic7TextStringHeadDummy.CheckOnClose = false;
            this.obic7TextStringHeadDummy.ColumnNumber = 0;
            this.obic7TextStringHeadDummy.DispFuriganaText = null;
            this.obic7TextStringHeadDummy.EnableFalseLock = false;
            this.obic7TextStringHeadDummy.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringHeadDummy.ErrorString = null;
            this.obic7TextStringHeadDummy.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.obic7TextStringHeadDummy.IsKeyColumn = false;
            this.obic7TextStringHeadDummy.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextStringHeadDummy.Location = new System.Drawing.Point(706, 27);
            this.obic7TextStringHeadDummy.MaxLengthByte = 60;
            this.obic7TextStringHeadDummy.Name = "obic7TextStringHeadDummy";
            this.obic7TextStringHeadDummy.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextStringHeadDummy.Size = new System.Drawing.Size(14, 19);
            this.obic7TextStringHeadDummy.TabIndex = 1181;
            this.obic7TextStringHeadDummy.TabStop = false;
            this.obic7TextStringHeadDummy.Value = "";
            this.obic7TextStringHeadDummy.VisibleFalseLock = false;
            // 
            // obic7TextNumberKijunKBN
            // 
            this.obic7TextNumberKijunKBN.AttachedButton = this.obic7ButtonWithLogicSearchKijunKBN;
            this.obic7TextNumberKijunKBN.AttachedCopyText = null;
            this.obic7TextNumberKijunKBN.AttachedLabel = this.obic7LabelStringKijunKBN;
            this.obic7TextNumberKijunKBN.AttachedSearchDialog = null;
            this.obic7TextNumberKijunKBN.CheckOnClose = false;
            this.obic7TextNumberKijunKBN.ColumnNumber = 0;
            this.obic7TextNumberKijunKBN.EnableFalseLock = false;
            this.obic7TextNumberKijunKBN.EnterColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberKijunKBN.ErrorString = null;
            this.obic7TextNumberKijunKBN.ForeColor = System.Drawing.SystemColors.WindowText;
            this.obic7TextNumberKijunKBN.Format = "0";
            this.obic7TextNumberKijunKBN.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.obic7TextNumberKijunKBN.IsKeyColumn = false;
            this.obic7TextNumberKijunKBN.LeaveColor = System.Drawing.SystemColors.Window;
            this.obic7TextNumberKijunKBN.Location = new System.Drawing.Point(414, 3);
            this.obic7TextNumberKijunKBN.MaxLength = 1;
            this.obic7TextNumberKijunKBN.MaxLengthByte = 1;
            this.obic7TextNumberKijunKBN.MaxValue = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.obic7TextNumberKijunKBN.MinusColor = System.Drawing.Color.Red;
            this.obic7TextNumberKijunKBN.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.obic7TextNumberKijunKBN.Name = "obic7TextNumberKijunKBN";
            this.obic7TextNumberKijunKBN.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7TextNumberKijunKBN.Size = new System.Drawing.Size(18, 19);
            this.obic7TextNumberKijunKBN.TabIndex = 1060;
            this.obic7TextNumberKijunKBN.Text = "0";
            this.obic7TextNumberKijunKBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.obic7TextNumberKijunKBN.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.obic7TextNumberKijunKBN.VisibleFalseLock = false;
            this.obic7TextNumberKijunKBN.Enter += new System.EventHandler(this.obic7TextNumberKijunKBN_Enter);
            this.obic7TextNumberKijunKBN.Validating += new System.ComponentModel.CancelEventHandler(this.obic7TextNumberKijunKBN_Validating);
            // 
            // obic7ButtonWithLogicSearchKijunKBN
            // 
            this.obic7ButtonWithLogicSearchKijunKBN.ColumnNumber = 0;
            this.obic7ButtonWithLogicSearchKijunKBN.EnableFalseLock = false;
            this.obic7ButtonWithLogicSearchKijunKBN.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.obic7ButtonWithLogicSearchKijunKBN.Location = new System.Drawing.Point(432, 1);
            this.obic7ButtonWithLogicSearchKijunKBN.Name = "obic7ButtonWithLogicSearchKijunKBN";
            this.obic7ButtonWithLogicSearchKijunKBN.Obic7JobSecurity = Obic7fx.Com.A0000.Tool.Obic7JobSecurities.NoSecurity;
            this.obic7ButtonWithLogicSearchKijunKBN.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7ButtonWithLogicSearchKijunKBN.Size = new System.Drawing.Size(24, 23);
            this.obic7ButtonWithLogicSearchKijunKBN.TabIndex = 1070;
            this.obic7ButtonWithLogicSearchKijunKBN.Text = "...";
            this.obic7ButtonWithLogicSearchKijunKBN.UseVisualStyleBackColor = true;
            this.obic7ButtonWithLogicSearchKijunKBN.VisibleFalseLock = false;
            this.obic7ButtonWithLogicSearchKijunKBN.Click += new System.EventHandler(this.obic7ButtonWithLogicSearchKijunKBN_Click);
            this.obic7ButtonWithLogicSearchKijunKBN.Enter += new System.EventHandler(this.obic7ButtonWithLogicSearchKijunKBN_Enter);
            // 
            // obic7LabelStringKijunKBN
            // 
            this.obic7LabelStringKijunKBN.AutoEllipsis = true;
            this.obic7LabelStringKijunKBN.BackColor = System.Drawing.SystemColors.Control;
            this.obic7LabelStringKijunKBN.ColumnNumber = 0;
            this.obic7LabelStringKijunKBN.EnableFalseLock = false;
            this.obic7LabelStringKijunKBN.ErrorString = null;
            this.obic7LabelStringKijunKBN.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelStringKijunKBN.HasHelp = false;
            this.obic7LabelStringKijunKBN.Location = new System.Drawing.Point(348, 3);
            this.obic7LabelStringKijunKBN.Name = "obic7LabelStringKijunKBN";
            this.obic7LabelStringKijunKBN.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.RequiredLabel;
            this.obic7LabelStringKijunKBN.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringKijunKBN.Size = new System.Drawing.Size(60, 19);
            this.obic7LabelStringKijunKBN.TabIndex = 1193;
            this.obic7LabelStringKijunKBN.Text = "基準区分";
            this.obic7LabelStringKijunKBN.Value = "基準区分";
            this.obic7LabelStringKijunKBN.VisibleFalseLock = false;
            // 
            // obic7LabelStringKijunKBNName
            // 
            this.obic7LabelStringKijunKBNName.AutoEllipsis = true;
            this.obic7LabelStringKijunKBNName.BackColor = System.Drawing.SystemColors.Desktop;
            this.obic7LabelStringKijunKBNName.ColumnNumber = 0;
            this.obic7LabelStringKijunKBNName.EnableFalseLock = false;
            this.obic7LabelStringKijunKBNName.ErrorString = null;
            this.obic7LabelStringKijunKBNName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelStringKijunKBNName.HasHelp = false;
            this.obic7LabelStringKijunKBNName.Location = new System.Drawing.Point(456, 3);
            this.obic7LabelStringKijunKBNName.Name = "obic7LabelStringKijunKBNName";
            this.obic7LabelStringKijunKBNName.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7LabelStringKijunKBNName.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringKijunKBNName.Size = new System.Drawing.Size(132, 19);
            this.obic7LabelStringKijunKBNName.TabIndex = 1080;
            this.obic7LabelStringKijunKBNName.Value = "";
            this.obic7LabelStringKijunKBNName.VisibleFalseLock = false;
            // 
            // obic7LabelString21
            // 
            this.obic7LabelString21.AutoEllipsis = true;
            this.obic7LabelString21.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelString21.ColumnNumber = 0;
            this.obic7LabelString21.EnableFalseLock = false;
            this.obic7LabelString21.ErrorString = null;
            this.obic7LabelString21.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelString21.HasHelp = false;
            this.obic7LabelString21.Location = new System.Drawing.Point(6, 3);
            this.obic7LabelString21.Name = "obic7LabelString21";
            this.obic7LabelString21.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelString21.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelString21.Size = new System.Drawing.Size(29, 56);
            this.obic7LabelString21.TabIndex = 1000000060;
            this.obic7LabelString21.Text = "行";
            this.obic7LabelString21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.obic7LabelString21.Value = "行";
            this.obic7LabelString21.VisibleFalseLock = false;
            // 
            // obic7LabelString18
            // 
            this.obic7LabelString18.AutoEllipsis = true;
            this.obic7LabelString18.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelString18.ColumnNumber = 0;
            this.obic7LabelString18.EnableFalseLock = false;
            this.obic7LabelString18.ErrorString = null;
            this.obic7LabelString18.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelString18.HasHelp = false;
            this.obic7LabelString18.Location = new System.Drawing.Point(1032, 41);
            this.obic7LabelString18.Name = "obic7LabelString18";
            this.obic7LabelString18.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelString18.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelString18.Size = new System.Drawing.Size(65, 18);
            this.obic7LabelString18.TabIndex = 1000000061;
            this.obic7LabelString18.Text = "ﾌﾟﾛｼﾞｪｸﾄ";
            this.obic7LabelString18.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.obic7LabelString18.Value = "ﾌﾟﾛｼﾞｪｸﾄ";
            this.obic7LabelString18.VisibleFalseLock = false;
            // 
            // obic7LabelString19
            // 
            this.obic7LabelString19.AutoEllipsis = true;
            this.obic7LabelString19.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelString19.ColumnNumber = 0;
            this.obic7LabelString19.EnableFalseLock = false;
            this.obic7LabelString19.ErrorString = null;
            this.obic7LabelString19.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelString19.HasHelp = false;
            this.obic7LabelString19.Location = new System.Drawing.Point(510, 41);
            this.obic7LabelString19.Name = "obic7LabelString19";
            this.obic7LabelString19.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelString19.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelString19.Size = new System.Drawing.Size(65, 18);
            this.obic7LabelString19.TabIndex = 1000000062;
            this.obic7LabelString19.Text = "ﾌﾟﾛｼﾞｪｸﾄ";
            this.obic7LabelString19.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.obic7LabelString19.Value = "ﾌﾟﾛｼﾞｪｸﾄ";
            this.obic7LabelString19.VisibleFalseLock = false;
            // 
            // obic7LabelStringKasiMemo
            // 
            this.obic7LabelStringKasiMemo.AutoEllipsis = false;
            this.obic7LabelStringKasiMemo.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelStringKasiMemo.ColumnNumber = 0;
            this.obic7LabelStringKasiMemo.EnableFalseLock = false;
            this.obic7LabelStringKasiMemo.ErrorString = null;
            this.obic7LabelStringKasiMemo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelStringKasiMemo.HasHelp = false;
            this.obic7LabelStringKasiMemo.Location = new System.Drawing.Point(1098, 3);
            this.obic7LabelStringKasiMemo.Name = "obic7LabelStringKasiMemo";
            this.obic7LabelStringKasiMemo.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringKasiMemo.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringKasiMemo.Size = new System.Drawing.Size(17, 56);
            this.obic7LabelStringKasiMemo.TabIndex = 1000000063;
            this.obic7LabelStringKasiMemo.Text = "メモ";
            this.obic7LabelStringKasiMemo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.obic7LabelStringKasiMemo.Value = "メモ";
            this.obic7LabelStringKasiMemo.VisibleFalseLock = false;
            // 
            // obic7LabelStringKariMemo
            // 
            this.obic7LabelStringKariMemo.AutoEllipsis = false;
            this.obic7LabelStringKariMemo.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.obic7LabelStringKariMemo.ColumnNumber = 0;
            this.obic7LabelStringKariMemo.EnableFalseLock = false;
            this.obic7LabelStringKariMemo.ErrorString = null;
            this.obic7LabelStringKariMemo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelStringKariMemo.HasHelp = false;
            this.obic7LabelStringKariMemo.Location = new System.Drawing.Point(37, 3);
            this.obic7LabelStringKariMemo.Name = "obic7LabelStringKariMemo";
            this.obic7LabelStringKariMemo.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.NormalLabel;
            this.obic7LabelStringKariMemo.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringKariMemo.Size = new System.Drawing.Size(17, 56);
            this.obic7LabelStringKariMemo.TabIndex = 1000000064;
            this.obic7LabelStringKariMemo.Text = "メモ";
            this.obic7LabelStringKariMemo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.obic7LabelStringKariMemo.Value = "メモ";
            this.obic7LabelStringKariMemo.VisibleFalseLock = false;
            // 
            // obic7ButtonWithLogicDensiShouhyou
            // 
            this.obic7ButtonWithLogicDensiShouhyou.ColumnNumber = 0;
            this.obic7ButtonWithLogicDensiShouhyou.EnableFalseLock = false;
            this.obic7ButtonWithLogicDensiShouhyou.Location = new System.Drawing.Point(894, 25);
            this.obic7ButtonWithLogicDensiShouhyou.Name = "obic7ButtonWithLogicDensiShouhyou";
            this.obic7ButtonWithLogicDensiShouhyou.Obic7JobSecurity = Obic7fx.Com.A0000.Tool.Obic7JobSecurities.NoSecurity;
            this.obic7ButtonWithLogicDensiShouhyou.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7ButtonWithLogicDensiShouhyou.Size = new System.Drawing.Size(84, 23);
            this.obic7ButtonWithLogicDensiShouhyou.TabIndex = 1230;
            this.obic7ButtonWithLogicDensiShouhyou.Text = "添付";
            this.obic7ButtonWithLogicDensiShouhyou.UseVisualStyleBackColor = true;
            this.obic7ButtonWithLogicDensiShouhyou.VisibleFalseLock = false;
            this.obic7ButtonWithLogicDensiShouhyou.Click += new System.EventHandler(this.obic7ButtonDensiShouhyou_Click);
            this.obic7ButtonWithLogicDensiShouhyou.Enter += new System.EventHandler(this.obic7ButtonWithLogicDensiShouhyou_Enter);
            // 
            // obic7LabelStringDensiShouhyou
            // 
            this.obic7LabelStringDensiShouhyou.AutoEllipsis = true;
            this.obic7LabelStringDensiShouhyou.BackColor = System.Drawing.SystemColors.Desktop;
            this.obic7LabelStringDensiShouhyou.ColumnNumber = 0;
            this.obic7LabelStringDensiShouhyou.EnableFalseLock = false;
            this.obic7LabelStringDensiShouhyou.ErrorString = null;
            this.obic7LabelStringDensiShouhyou.ForeColor = System.Drawing.SystemColors.ControlText;
            this.obic7LabelStringDensiShouhyou.HasHelp = false;
            this.obic7LabelStringDensiShouhyou.Location = new System.Drawing.Point(979, 27);
            this.obic7LabelStringDensiShouhyou.Name = "obic7LabelStringDensiShouhyou";
            this.obic7LabelStringDensiShouhyou.Obic7Styles = Obic7fx.Win.A0000.Tool.UI.Obic7Styles.Normal;
            this.obic7LabelStringDensiShouhyou.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.obic7LabelStringDensiShouhyou.Size = new System.Drawing.Size(237, 19);
            this.obic7LabelStringDensiShouhyou.TabIndex = 1220;
            this.obic7LabelStringDensiShouhyou.Value = "";
            this.obic7LabelStringDensiShouhyou.VisibleFalseLock = false;
            // 
            // MA_HonsitenSiwake
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1228, 666);
            this.Name = "MA_HonsitenSiwake";
            this.Text = "本支店仕訳入力";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MA_HonsitenSiwake_FormClosing);
            this.Load += new System.EventHandler(this.MA_HonsitenSiwake_Load);
            this.Shown += new System.EventHandler(this.MA_HonsitenSiwake_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MA_HonsitenSiwake_KeyDown);
            this.obic7PanelHeader.ResumeLayout(false);
            this.obic7PanelHeader.PerformLayout();
            this.obic7PanelDetail.ResumeLayout(false);
            this.obic7PanelDetail.PerformLayout();
            this.obic7PanelLine.ResumeLayout(false);
            this.obic7PanelLine.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Obic7fx.Win.A0000.Tool.UI.Obic7ComboBox obic7ComboBoxMode;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextDatetime obic7TextDatetimeHasseiDate;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringZigyoushoCD;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringZigyousho;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringHasseiDate;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringDenpyouTekiyou;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringDenpyouTekiyou;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringZigyoushoName;
        private Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic obic7ButtonWithLogicSearchZigyoushoCD;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringDenpyouNO;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringDenpyouKBN;
        private Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic obic7ButtonWithLogicSearchDenpyouKBN;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringDenpyouKBN;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringDenpyouNO;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringDenpyouKBNName;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringCalledDenpyouNO;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringPrevDenpyouNO;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber obic7TextNumberSokudenHakkou;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringCalledDenpyouNO;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringSokudenhakkou;
        private Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic obic7ButtonWithLogicDisplay;
        private Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic obic7ButtonWithLogicSearchCalledDenpyouNO;
        private Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic obic7ButtonWithLogicSearchSokudenHakkou;
        private Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic obic7ButtonWithLogicSearchDenpyouTekiyou;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringKariKanjou;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringSokudenHakkouName;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelString3;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelString4;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelString15;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelString14;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelString25;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelString24;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelString23;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelString22;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelString20;
        private Obic7fx.Win.A0000.Tool.UI.Obic7Panel obic7PanelLine;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKariBumonCD;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKasiBumonCD;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringMeisaiTekiyou;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKasiSikinCD;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKasiBunsekiCD3;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKasiBunsekiCD2;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKasiBunsekiCD1;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKariSikinCD;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKariBunsekiCD3;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKariBunsekiCD2;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKariBunsekiCD1;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber obic7TextNumberKasiZeigaku;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber obic7TextNumberKasiKingaku;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKasiUtiwakeNameRyaku;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKasiUtiwakeCD;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKasiHojoNameRyaku;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKasiHojoCD;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKasiBumonNameRyaku;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber obic7TextNumberKariZeigaku;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber obic7TextNumberKariKingaku;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKariUtiwakeNameRyaku;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKariUtiwakeCD;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKariHojoNameRyaku;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKasiKanjouNameRyaku;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKasiKanjouCD;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKariBumonNameRyaku;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKariHojoCD;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKariKanjouNameRyaku;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKariKanjouCD;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKariTorihikisakiCD;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKariTorihikisakiShanaiName;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKariZeiKBN;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKariZeiHontaiCD;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber obic7TextNumberKariZeikomiKBN;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKariZeiHontaiNameRyaku;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKasiZeiHontaiNameRyaku;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKasiZeiHontaiCD;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber obic7TextNumberKasiZeikomiKBN;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKasiZeiKBN;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKasiTorihikisakiShanaiName;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKasiTorihikisakiCD;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber obic7TextNumberGyouNO;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber obic7TextNumberTotalKasiSougaku;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber obic7TextNumberTotalKariSougaku;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber obic7TextNumberTotalKasiKingaku;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber obic7TextNumberTotalKariKingaku;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber obic7TextNumberTotalKasiZeigaku;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber obic7TextNumberTotalKariZeigaku;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKariProjectCD;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKasiProjectCD;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKasiBunsekiCD5;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKariBunsekiCD5;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelString13;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelString1;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelString2;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelString5;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelString6;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelString7;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelString8;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelString9;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelString10;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelString11;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelString12;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKariZeikomiKBNName;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKariZigyoushoNameRyaku;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKariZigyoushoCD;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKasiZeikomiKBNName;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKasiZigyoushoNameRyaku;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKasiZigyoushoCD;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelString16;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelString17;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKasiBunsekiCD4;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringKariBunsekiCD4;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringPrevDenpyouNO;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringDummy;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextSeparator obic7TextSeparatorDenpyouNo;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringDummy2;
        private Obic7fx.Win.A0000.Tool.UI.Obic7ToolTip toolTip1;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextSeparator obic7TextSeparatorKariHojo;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextSeparator obic7TextSeparatorKariUtiwake;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextSeparator obic7TextSeparatorKasiHojo;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextSeparator obic7TextSeparatorKasiUtiwake;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextString obic7TextStringHeadDummy;
        private Obic7fx.Win.A0000.Tool.UI.Obic7TextNumber obic7TextNumberKijunKBN;
        private Obic7fx.Win.A0000.Tool.Logic.UI.Obic7ButtonWithLogic obic7ButtonWithLogicSearchKijunKBN;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringKijunKBN;
        private Obic7fx.Win.A0000.Tool.UI.Obic7LabelString obic7LabelStringKijunKBNName;
        private A0000.Tool.UI.Obic7LabelString obic7LabelString19;
        private A0000.Tool.UI.Obic7LabelString obic7LabelString18;
        private A0000.Tool.UI.Obic7LabelString obic7LabelString21;
        private A0000.Tool.UI.Obic7LabelNumber obic7LabelNumberGyou;
        private A0000.Tool.UI.Obic7LabelString obic7LabelStringKasiMemo;
        private A0000.Tool.UI.Obic7LabelString obic7LabelStringKariMemo;
        private A0000.Tool.Logic.UI.Obic7ButtonWithLogic obic7ButtonWithLogicKariMemo;
        private A0000.Tool.Logic.UI.Obic7ButtonWithLogic obic7ButtonWithLogicKasiMemo;
        private A0000.Tool.UI.Obic7TextNumber obic7TextNumberKariMemoRValue;
        private A0000.Tool.UI.Obic7TextNumber obic7TextNumberKariMemoGValue;
        private A0000.Tool.UI.Obic7TextNumber obic7TextNumberKariMemoBValue;
        private A0000.Tool.UI.Obic7TextString obic7TextStringKariMemo;
        private A0000.Tool.UI.Obic7TextString obic7TextStringKasiMemo;
        private A0000.Tool.UI.Obic7TextNumber obic7TextNumberKasiMemoBValue;
        private A0000.Tool.UI.Obic7TextNumber obic7TextNumberKasiMemoGValue;
        private A0000.Tool.UI.Obic7TextNumber obic7TextNumberKasiMemoRValue;
        private A0000.Tool.UI.Obic7LabelString obic7LabelStringDensiShouhyou;
        private A0000.Tool.Logic.UI.Obic7ButtonWithLogic obic7ButtonWithLogicDensiShouhyou;
    }
}

