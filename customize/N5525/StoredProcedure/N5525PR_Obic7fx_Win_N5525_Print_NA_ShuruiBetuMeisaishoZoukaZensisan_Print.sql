if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[N5525PR_Obic7fx_Win_N5525_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_Print]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[N5525PR_Obic7fx_Win_N5525_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_Print]
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE  N5525PR_Obic7fx_Win_N5525_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_Print(
     @po_ErrorKBN                       int                 OUTPUT  -- エラー区分 (0：正常、0以外：異常)
    ,@po_ErrorCD1                       varchar(128)        OUTPUT  -- エラーCD1
    ,@po_ErrorCD2                       varchar(128)        OUTPUT  -- エラーCD2
    ,@po_ErrorProcName                  varchar(128)        OUTPUT  -- エラーストアド名
    ,@po_ErrorSummary                   varchar(256)        OUTPUT  -- エラーメッセージ概要
    ,@po_ErrorDetail                    varchar(2560)       OUTPUT  -- エラーメッセージ詳細
    ,@po_ErrorBtnKBN                    tinyint             OUTPUT  -- ボタン区分 (0：OK、1：YES・NO、2：YES・NO・CANCEL)
    ,@po_ErrorBtnDefault                tinyint             OUTPUT  -- デフォルトボタン区分 (0：ボタン1、1：ボタン2、2：ボタン3)
    ,@p_KijunDate                       datetime                    -- 基準日
    ,@p_SisanKBN                        tinyint                     -- 資産区分(0:本資産のみ、1:仮資産のみ、2:全て含む)
    ,@p_SinkokuKBN                      tinyint                     -- 申告区分(全資産申告:0 増減申告:1)
    ,@p_SisanCDPrintKBN                 tinyint                     -- 資産コード印字
    ,@p_KagakuPrintKBN                  tinyint                     -- 価額印字
    ,@p_KazeiHyoujunGakuPrintKBN        tinyint                     -- 課税標準額印字
    ,@p_ShuturyokuKBN                   tinyint                     -- 出力区分(0:全社、1:事業所別)
    ,@p_SinkokuNendo                    datetime                    -- 申告年度
    ,@p_ZititaiCDBegin                  varchar(6)                  -- 自治体コード開始
    ,@p_ZititaiCDEnd                    varchar(6)                  -- 自治体コード終了
    ,@p_ZigyoushoCDBegin                varchar(6)                  -- 事業所コード開始
    ,@p_ZigyoushoCDEnd                  varchar(6)                  -- 事業所コード終了
    ,@p_ZigSecPtnCD                     varchar(4)                  -- 事業所セキュリティパターンコード
    ,@p_CompanyName                     varchar(50)  = NULL         -- 会社名
--2012/11/05 E.Sato ST (N00970)eLTax対応
    ,@p_ShuturyokuSiteiKBN              tinyint = 0                 -- 出力指定区分(0:印刷、1:ファイル出力)
--2012/11/05 E.Sato ED    
)
--WITH RECOMPILE
AS

/*
******************************************************************************
** 名前：種類別明細書(増加・全資産)
** 機能：種類別明細書(増加・全資産)に出力するデータを作成する
** パラメータ：
**       @po_ErrorKBN                       int                 OUTPUT  -- エラー区分 (0：正常、0以外：異常)
**      ,@po_ErrorCD1                       varchar(128)        OUTPUT  -- エラーCD1
**      ,@po_ErrorCD2                       varchar(128)        OUTPUT  -- エラーCD2
**      ,@po_ErrorProcName                  varchar(128)        OUTPUT  -- エラーストアド名
**      ,@po_ErrorSummary                   varchar(256)        OUTPUT  -- エラーメッセージ概要
**      ,@po_ErrorDetail                    varchar(2560)       OUTPUT  -- エラーメッセージ詳細
**      ,@po_ErrorBtnKBN                    tinyint             OUTPUT  -- ボタン区分 (0：OK、1：YES・NO、2：YES・NO・CANCEL)
**      ,@po_ErrorBtnDefault                tinyint             OUTPUT  -- デフォルトボタン区分 (0：ボタン1、1：ボタン2、2：ボタン3)
**      ,@p_KijunDate                       datetime                    -- 基準日
**      ,@p_SisanKBN                        tinyint                     -- 資産区分(0:本資産のみ、1:仮資産のみ、2:全て含む)
**      ,@p_SinkokuKBN                      tinyint                     -- 申告区分(全資産申告:0 増減申告:1)
**      ,@p_SisanCDPrintKBN                 tinyint                     -- 資産コード印字
**      ,@p_KagakuPrintKBN                  tinyint                     -- 価額印字
**      ,@p_KazeiHyoujunGakuPrintKBN        tinyint                     -- 課税標準額印字
**      ,@p_ShuturyokuKBN                   tinyint                     -- 出力区分(0:全社、1:事業所別)
**      ,@p_SinkokuNendo                    datetime                    -- 申告年度
**      ,@p_ZititaiCDBegin                  varchar(6)                  -- 自治体コード開始
**      ,@p_ZititaiCDEnd                    varchar(6)                  -- 自治体コード終了
**      ,@p_ZigyoushoCDBegin                varchar(6)                  -- 事業所コード開始
**      ,@p_ZigyoushoCDEnd                  varchar(6)                  -- 事業所コード終了
**      ,@p_ZigSecPtnCD                     varchar(4)                  -- 事業所セキュリティパターンコード
**      ,@p_CompanyName                     varchar(50)  = NULL         -- 会社名
--2012/11/05 E.Sato ST (N00970)eLTax対応
**      ,@p_ShuturyokuSiteiKBN              tinyint = 0                 -- 出力指定区分(0:印刷、1:ファイル出力)
--2012/11/05 E.Sato ED
***************************************************************************************************
**      履歴
**              2009/02/12 Y.Ishizawa   新規作成
**              2009/06/26 Y.Ishizawa   資産種類で改ページした場合、総ページ数を振りなおさない
**              2009/07/06 Y.Ishizawa   自治体の範囲指定条件変更
**              2009/07/13 Y.Ishizawa   全資産申告の場合、取得価額を地方税取得価額から転送に修正
**              2010/12/07 H.Sudo       (N00694-00)増加事由のロジックを修正
**              2010/12/13 M.Shinbo     (N00697)IFRS・ダブルスタンダード対応
**              2011/01/19 M.Shinbo     (N00697)IFRS・ダブルスタンダード対応 戻り対応
**              2011/01/20 Y.Nakazawa   (N00748)分割などの異動により抽出結果が不正になる不具合修正
**              2011/12/22 S.Harada     (N00878)資産種類で改ページ指定の時、小計が印字されない不具合修正
**              2012/11/05 E.Sato       (N00970)eLTax対応
**              2013/01/09 S.Murakami   (N00995)分割合併対応
**              2013/03/15 E.Sato       (N00995)分割合併対応の追加修正
**              2015/01/05 K.Morita     (19288-19289-19291)eLTax仕様変更対応
**              2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする
**              2016/02/29 K.Shigematsu (35373)増加申告で自治体コードの範囲指定を使用した場合、分割合併で増加した資産を抽出できない
**              2018/04/26 T.Maeda      (97532-97598)改元対応
**              2020/11/16 N.Maeda      (149912)特例率ゼロが登録できません
**              2020/12/25 Y.Matuzawa   (182011)半角を全角に置き換え
**              2020/12/25 Y.Matuzawa   (182011) 「提出先市町村コード」の取得元を地方公共団体コードに変更
**              2021/05/13 Y.Nakajima   (184642)種類別明細書のデータ出力にて、登録された数量が1未満の場合0でなく1で出力する
**              2021 06/03 Y.Matuzawa   (183279) 会社分割によって増加した資産の備考が「合併による」と表示される
**              2026/10/11              (5525)事業所別償却資産申告対応
**                                      N0000 標準ストアドを複製し、帳票出力前ワークに事業所コードを保持
**                                      ファイル出力(eLTax)の末尾へファイル分割用の[N5525_事業所コード]列を出力する（画面側で削除してから出力）
**
***************************************************************************************************
*/
BEGIN

        SET NOCOUNT ON
    
        -- 変数定義
        DECLARE @ErrorCode                      int                 -- @@ERRORの待避領域
        DECLARE @SinkokuKikanBegin              datetime   --申告期間開始
        DECLARE @SinkokuKikanEnd                datetime   --申告期間終了
        DECLARE @ShuruibetuMeisaishoKaiPageKBN  tinyint    --種類別明細書改ページ区分
        DECLARE @CNT                            int        --データ件数
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↓
        DECLARE @ShutokuDateHanteiKBN               tinyint
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↑
    
        -- ストアド変数
        DECLARE @strSQL    AS varchar(MAX)

        -- カーソル用変数
        DECLARE @curShoyuushaName          varchar(50)
        DECLARE @curSisanNO                varchar(12)
        DECLARE @curEdaban                 int
        DECLARE @curSisanShubetuNO         tinyint
        DECLARE @curSisanName              varchar(40)
        DECLARE @curSuuryou                decimal(11,2)
        DECLARE @curNengou                 varchar(4)
        DECLARE @curShutokuNen             int
        DECLARE @curShutokuTuki            smallint
        DECLARE @curShutokuKagaku          decimal(15,0)
        DECLARE @curTaiyouNensuu           int
        DECLARE @curGenkazanzonritu        decimal(4,3)
        DECLARE @curKagaku                 decimal(15,0)
        DECLARE @curTokureiritu            varchar(4)
        DECLARE @curKazeiKagaku            decimal(15,0)
        DECLARE @curZiyuuKBN               tinyint
--  2021 06/03 Y.Matuzawa (183279) 会社分割によって増加した資産の備考が「合併による」と表示される 対応 ↓
        --DECLARE @curBikou                  varchar(16)
        DECLARE @curBikou                  varchar(20)
--  2021 06/03 Y.Matuzawa (183279) 会社分割によって増加した資産の備考が「合併による」と表示される 対応 ↑
        DECLARE @curZititaiCD              varchar(6)
        DECLARE @curZigyoushoCD            varchar(6)

        DECLARE @intGyouNO                 int        --行番号
        DECLARE @intKaiPageTani            int        --改ページ単位
        DECLARE @intPageNO                 int        --改ページ単位毎の頁番号
        DECLARE @intAbsPageNO              int        --絶対ページ番号
        DECLARE @savZititaiCD              varchar(6) --比較用自治体コード
        DECLARE @savZigyoushoCD            varchar(6) --比較用事業所コード
        DECLARE @savSisanShubetuNO         tinyint    --比較用資産種別番号

        -- 引数初期化
        SET @po_ErrorKBN = 0
        SET @po_ErrorCD1 = ''
        SET @po_ErrorCD2 = ''
        SET @po_ErrorProcName = ''
        SET @po_ErrorSummary = ''
        SET @po_ErrorDetail = ''
        SET @po_ErrorBtnKBN = 0
        SET @po_ErrorBtnDefault = 0

        -- 帳票データワーク
        CREATE TABLE #N0000WK_Obic7fx_Win_N0000_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_Data (
                 RirekiSystemNO            decimal(18,0)    NULL
                ,SisanNO                   varchar(12)      COLLATE database_default NULL
                ,Edaban                    int              NULL
                ,SisanName                 varchar(40)      COLLATE database_default NULL
                ,ZititaiCD                 varchar(6)       COLLATE database_default NULL
                ,SisanShubetuNO            tinyint          NULL
                ,Suuryou                   decimal(11,2)    NULL
                ,ShutokuDate               datetime         NULL
                ,Nengou                    varchar(4)       COLLATE database_default NULL
                ,ShutokuNen                int              NULL
                ,ShutokuTuki               smallint         NULL
                ,ShutokuKagaku             decimal(15,0)    NULL
                ,TaiyouNensuu              int              NULL
                ,ZiyuuKBN                  tinyint          NULL
                ,RirekiDate                datetime         NULL
                ,Kagaku                    decimal(15,0)    NULL
                ,KazeiKagaku               decimal(15,0)    NULL
                ,Genkazanzonritu           decimal(4,3)     NULL
                ,Tokureiritu               varchar(4)       COLLATE database_default NULL
                ,TokureirituNumerator      tinyint          NULL
                ,TokureirituDenominator    tinyint          NULL
                ,ShutokuKBN                tinyint          NULL
                ,SinkokuKBN                tinyint          NULL
                ,ShoukyakuKaisiDate        datetime         NULL
                ,TokureiKikan              datetime         NULL
                ,ZigyoushoCD               varchar(6)       COLLATE database_default NULL
                ,CompanyName               varchar(50)      COLLATE database_default NULL
--  2021 06/03 Y.Matuzawa   (183279) 会社分割によって増加した資産の備考が「合併による」と表示される ↓
                --,Bikou                     varchar(16)      COLLATE database_default NULL
                ,Bikou                     varchar(20)      COLLATE database_default NULL
--  2021 06/03 Y.Matuzawa   (183279) 会社分割によって増加した資産の備考が「合併による」と表示される ↑
                )

        --資産情報ワーク
        CREATE TABLE #N0000WK_Obic7fx_Win_N0000_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_SisanJouhou (
                 [ID]                      int identity(1,1) NOT NULL
                ,SisanNO                   varchar(12)      COLLATE database_default NULL
                ,Edaban                    int              NULL
                ,ZititaiCD                 varchar(6)       COLLATE database_default NULL
                ,ZigyoushoCD               varchar(6)       COLLATE database_default NULL
                ,ZiyuuKBN                  tinyint          NULL
                ,SisanName                 varchar(40)      COLLATE database_default NULL
                ,Suuryou                   decimal(11,2)    NULL
                ,ShutokuDate               datetime         NULL
                ,ShutokuKagaku             decimal(15,0)    NULL
                ,ShutokuKBN                tinyint          NULL
                ,SinkokuKBN                tinyint          NULL
                ,ShoukyakuKaisiDate        datetime         NULL
                ,SisanShubetuNO            tinyint          NULL
                )

        --帳票出力前ワーク
        CREATE TABLE #N0000WK_Obic7fx_Win_N0000_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_Print (
                 [ID]                      int identity(1,1) NOT NULL
                ,GyouNO                    int              NULL
                ,PageNO                    int              NULL
                ,MaxPageNO                 int              NULL
                ,AbsPageNO                 int              NULL
                ,KaiPageTani               int              NULL
                ,ShoyuushaName             varchar(50)      COLLATE database_default NULL
                ,ShoyuushaCD               varchar(30)      COLLATE database_default NULL
                ,SisanNO                   varchar(12)      COLLATE database_default NULL
                ,Edaban                    int              NULL
                ,SisanShubetuNO            tinyint          NULL
                ,SisanName                 varchar(40)      COLLATE database_default NULL
                ,Suuryou                   decimal(11,2)    NULL
                ,Nengou                    varchar(4)       COLLATE database_default NULL
                ,ShutokuNen                int              NULL
                ,ShutokuTuki               smallint         NULL
                ,ShutokuKagaku             decimal(15,0)    NULL
                ,TaiyouNensuu              int              NULL
                ,Genkazanzonritu           decimal(4,3)     NULL
                ,Kagaku                    decimal(15,0)    NULL
                ,Tokureiritu               varchar(4)       COLLATE database_default NULL
                ,KazeiKagaku               decimal(15,0)    NULL
                ,ZiyuuKBN                  tinyint          NULL
--  2021 06/03 Y.Matuzawa (183279) 会社分割によって増加した資産の備考が「合併による」と表示される 対応 ↓
                --,Bikou                     varchar(16)      COLLATE database_default NULL
                ,Bikou                     varchar(20)      COLLATE database_default NULL
--  2021 06/03 Y.Matuzawa (183279) 会社分割によって増加した資産の備考が「合併による」と表示される 対応 ↑
                ,ZititaiCD                 varchar(6)       COLLATE database_default NULL
                ,Shoukei                   decimal(15,0)    NULL
                --2026/10/11 (5525)事業所別償却資産申告対応 ↓
                ,ZigyoushoCD               varchar(6)       COLLATE database_default NULL
                --2026/10/11 (5525)事業所別償却資産申告対応 ↑
                )

        --1.必要項目の取得
        --1-1.画面で設定した[申告年度]から、申告期間を求めておく
        SET @SinkokuKikanBegin = DATEADD(day,1,DATEADD(year,-1,@p_SinkokuNendo))
        SET @SinkokuKikanEnd   = @p_SinkokuNendo

        --1-2.地方税コントロールマスタから、必要項目を取得
        SELECT @ShuruibetuMeisaishoKaiPageKBN = ShuruibetuMeisaishoKaiPageKBN
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↓
              ,@ShutokuDateHanteiKBN          = ShutokuDateHanteiKBN
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↑
        FROM N0000MS_NA_TihouzeiControl
        WHERE NendoID = 0

        SET @ErrorCode = @@ERROR
        IF @ErrorCode <> 0 GOTO ERR_SHORI

        --2.データ抽出
        --2-1.画面で増加申告を選択した場合
        IF @p_SinkokuKBN = 1
        BEGIN
            --2-1-1.事由区分=0(取得)のデータを抽出
            SET @strSQL = ''
            --INSERT句
            SET @strSQL = @strSQL + ' INSERT INTO #N0000WK_Obic7fx_Win_N0000_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_Data ( ' +CHAR(13)
            SET @strSQL = @strSQL + '          RirekiSystemNO ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,SisanNO ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,Edaban ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,SisanName ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,ZititaiCD ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,SisanShubetuNO ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,Suuryou ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,ShutokuDate ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,Nengou ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,ShutokuNen ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,ShutokuTuki ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,ShutokuKagaku ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,TaiyouNensuu ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,ZiyuuKBN ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,RirekiDate ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,Kagaku ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,KazeiKagaku ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,Genkazanzonritu ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,Tokureiritu ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,TokureirituNumerator ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,TokureirituDenominator ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,ShutokuKBN ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,SinkokuKBN ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,ShoukyakuKaisiDate ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,TokureiKikan ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,ZigyoushoCD ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,CompanyName ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,Bikou ' +CHAR(13)
            SET @strSQL = @strSQL + '         ) ' +CHAR(13)
            --SELECT句
            SET @strSQL = @strSQL + ' SELECT  ' +CHAR(13)
            SET @strSQL = @strSQL + '          RIR.RirekiSystemNO ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,SIS.SisanNO ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,SIS.Edaban ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,SIS.SisanName ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,CASE WHEN IDO.SisanNO IS NOT NULL ' +CHAR(13)
            SET @strSQL = @strSQL + '               THEN IDO.IdousakiZititaiCD ' +CHAR(13)
            SET @strSQL = @strSQL + '               ELSE SHT.ShutokuZititaiCD ' +CHAR(13)
            SET @strSQL = @strSQL + '          END AS ZititaiCD ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,SIS.SisanShubetuNO ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,SIS.Suuryou ' +CHAR(13)
--  2013/01/09 S.Murakami  (N00995)分割合併対応↓
            --SET @strSQL = @strSQL + '         ,CASE WHEN MONTH(SIS.ShutokuDate) = 1 AND DAY(SIS.ShutokuDate) = 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '               THEN DATEADD(day,-1,SIS.ShutokuDate) ' +CHAR(13)
            --SET @strSQL = @strSQL + '               ELSE SIS.ShutokuDate ' +CHAR(13)
            --SET @strSQL = @strSQL + '          END AS ShutokuDate ' +CHAR(13)
            --SET @strSQL = @strSQL + '         ,CASE WHEN MONTH(SIS.ShutokuDate) = 1 AND DAY(SIS.ShutokuDate) = 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '               THEN CASE WHEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) >= 1989 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN 4 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) >= 1926 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN 3 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) >= 1912 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN 2 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) >= 1867 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                    END ' +CHAR(13)
            --SET @strSQL = @strSQL + '               ELSE CASE WHEN YEAR(SIS.ShutokuDate) >= 1989 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN 4 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(SIS.ShutokuDate) >= 1926 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN 3 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(SIS.ShutokuDate) >= 1912 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN 2 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(SIS.ShutokuDate) >= 1867 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                    END ' +CHAR(13)
            --SET @strSQL = @strSQL + '          END AS Nengou ' +CHAR(13)
            --SET @strSQL = @strSQL + '         ,CASE WHEN MONTH(SIS.ShutokuDate) = 1 AND DAY(SIS.ShutokuDate) = 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '               THEN CASE WHEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) >= 1989 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) - 1989 + 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) >= 1926 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) - 1926 + 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) >= 1912 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) - 1912 + 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) >= 1867 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) - 1867 + 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                    END ' +CHAR(13)
            --SET @strSQL = @strSQL + '               ELSE CASE WHEN YEAR(SIS.ShutokuDate) >= 1989 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN YEAR(SIS.ShutokuDate) - 1989 + 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(SIS.ShutokuDate) >= 1926 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN YEAR(SIS.ShutokuDate) - 1926 + 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(SIS.ShutokuDate) >= 1912 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN YEAR(SIS.ShutokuDate) - 1912 + 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(SIS.ShutokuDate) >= 1867 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN YEAR(SIS.ShutokuDate) - 1867 + 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                    END ' +CHAR(13)
            --SET @strSQL = @strSQL + '          END AS ShutokuNen ' +CHAR(13)
            --SET @strSQL = @strSQL + '         ,CASE WHEN MONTH(SIS.ShutokuDate) = 1 AND DAY(SIS.ShutokuDate) = 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '               THEN MONTH(DATEADD(day,-1,SIS.ShutokuDate)) ' +CHAR(13)
            --SET @strSQL = @strSQL + '               ELSE MONTH(SIS.ShutokuDate) ' +CHAR(13)
            --SET @strSQL = @strSQL + '          END AS ShutokuTuki ' +CHAR(13)
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↓
            IF (@ShutokuDateHanteiKBN = 0)
            BEGIN
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↑
                SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '               THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN DATEADD(day,-1,BGJ.BunkatuGappeiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE BGJ.BunkatuGappeiDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '               ELSE CASE WHEN MONTH(SIS.ShutokuDate) = 1 AND DAY(SIS.ShutokuDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN DATEADD(day,-1,SIS.ShutokuDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE SIS.ShutokuDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '          END AS ShutokuDate ' +CHAR(13)
--2018/04/26 T.Maeda      (97532-97598)改元対応(年号・取得年の抽出方法を変更)↓↓
                --SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '               THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         THEN CASE WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 4 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 3 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 2 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         ELSE CASE WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 4 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 3 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 2 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                --SET @strSQL = @strSQL + '               ELSE CASE WHEN MONTH(SIS.ShutokuDate) = 1 AND DAY(SIS.ShutokuDate) = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         THEN CASE WHEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 4 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 3 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 2 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         ELSE CASE WHEN YEAR(SIS.ShutokuDate) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 4 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(SIS.ShutokuDate) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 3 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(SIS.ShutokuDate) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 2 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(SIS.ShutokuDate) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                --SET @strSQL = @strSQL + '          END AS Nengou ' +CHAR(13)
                SET @strSQL = @strSQL + '          ,Gengou.GengouKBN AS Nengou ' +CHAR(13)
                --SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '               THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         THEN CASE WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - 1989 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - 1926 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - 1912 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - 1867 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         ELSE CASE WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(BGJ.BunkatuGappeiDate) - 1989 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(BGJ.BunkatuGappeiDate) - 1926 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(BGJ.BunkatuGappeiDate) - 1912 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(BGJ.BunkatuGappeiDate) - 1867 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                --SET @strSQL = @strSQL + '               ELSE CASE WHEN MONTH(SIS.ShutokuDate) = 1 AND DAY(SIS.ShutokuDate) = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         THEN CASE WHEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) - 1989 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) - 1926 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) - 1912 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) - 1867 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         ELSE CASE WHEN YEAR(SIS.ShutokuDate) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(SIS.ShutokuDate) - 1989 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(SIS.ShutokuDate) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(SIS.ShutokuDate) - 1926 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(SIS.ShutokuDate) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(SIS.ShutokuDate) - 1912 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(SIS.ShutokuDate) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(SIS.ShutokuDate) - 1867 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '               THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1  ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - YEAR(Gengou.GengouStartDate) + 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE YEAR(BGJ.BunkatuGappeiDate) - YEAR(Gengou.GengouStartDate) + 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '               ELSE CASE WHEN MONTH(SIS.ShutokuDate) = 1 AND DAY(SIS.ShutokuDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) - YEAR(Gengou.GengouStartDate) + 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE YEAR(SIS.ShutokuDate) - YEAR(Gengou.GengouStartDate) + 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
--2018/04/26 T.Maeda      (97532-97598)改元対応(年号・取得年の抽出方法を変更)↑↑
                SET @strSQL = @strSQL + '          END AS ShutokuNen ' +CHAR(13)
                SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)    
                SET @strSQL = @strSQL + '               THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN MONTH(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE MONTH(BGJ.BunkatuGappeiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '               ELSE CASE WHEN MONTH(SIS.ShutokuDate) = 1 AND DAY(SIS.ShutokuDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN MONTH(DATEADD(day,-1,SIS.ShutokuDate)) ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE MONTH(SIS.ShutokuDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '          END AS ShutokuTuki ' +CHAR(13)
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↓
            END
            ELSE
            BEGIN
                SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '               THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN DATEADD(day,-1,BGJ.BunkatuGappeiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE BGJ.BunkatuGappeiDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '               ELSE CASE WHEN MONTH(SIS.ShoukyakuKaisiDate) = 1 AND DAY(SIS.ShoukyakuKaisiDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN DATEADD(day,-1,SIS.ShoukyakuKaisiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE SIS.ShoukyakuKaisiDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '          END AS ShoukyakuKaisiDate ' +CHAR(13)
--2018/04/26 T.Maeda      (97532-97598)改元対応(年号・取得年の抽出方法を変更)↓↓
                --SET @strSQL = @strSQL + '          END AS ShutokuDate ' +CHAR(13)
                --SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '               THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         THEN CASE WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 4 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 3 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 2 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         ELSE CASE WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 4 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 3 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 2 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                --SET @strSQL = @strSQL + '               ELSE CASE WHEN MONTH(SIS.ShoukyakuKaisiDate) = 1 AND DAY(SIS.ShoukyakuKaisiDate) = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         THEN CASE WHEN YEAR(DATEADD(day,-1,SIS.ShoukyakuKaisiDate)) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 4 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,SIS.ShoukyakuKaisiDate)) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 3 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,SIS.ShoukyakuKaisiDate)) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 2 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,SIS.ShoukyakuKaisiDate)) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         ELSE CASE WHEN YEAR(SIS.ShoukyakuKaisiDate) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 4 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(SIS.ShoukyakuKaisiDate) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 3 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(SIS.ShoukyakuKaisiDate) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 2 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(SIS.ShoukyakuKaisiDate) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                --SET @strSQL = @strSQL + '          END AS Nengou ' +CHAR(13)
                SET @strSQL = @strSQL + '          ,Gengou.GengouKBN AS Nengou ' +CHAR(13)
                --SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '               THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         THEN CASE WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - 1989 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - 1926 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - 1912 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - 1867 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         ELSE CASE WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(BGJ.BunkatuGappeiDate) - 1989 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(BGJ.BunkatuGappeiDate) - 1926 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(BGJ.BunkatuGappeiDate) - 1912 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(BGJ.BunkatuGappeiDate) - 1867 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                --SET @strSQL = @strSQL + '               ELSE CASE WHEN MONTH(SIS.ShoukyakuKaisiDate) = 1 AND DAY(SIS.ShoukyakuKaisiDate) = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         THEN CASE WHEN YEAR(DATEADD(day,-1,SIS.ShoukyakuKaisiDate)) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,SIS.ShoukyakuKaisiDate)) - 1989 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,SIS.ShoukyakuKaisiDate)) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,SIS.ShoukyakuKaisiDate)) - 1926 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,SIS.ShoukyakuKaisiDate)) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,SIS.ShoukyakuKaisiDate)) - 1912 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,SIS.ShoukyakuKaisiDate)) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,SIS.ShoukyakuKaisiDate)) - 1867 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         ELSE CASE WHEN YEAR(SIS.ShoukyakuKaisiDate) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(SIS.ShoukyakuKaisiDate) - 1989 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(SIS.ShoukyakuKaisiDate) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(SIS.ShoukyakuKaisiDate) - 1926 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(SIS.ShoukyakuKaisiDate) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(SIS.ShoukyakuKaisiDate) - 1912 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(SIS.ShoukyakuKaisiDate) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(SIS.ShoukyakuKaisiDate) - 1867 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '               THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1  ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - YEAR(Gengou.GengouStartDate) + 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE YEAR(BGJ.BunkatuGappeiDate) - YEAR(Gengou.GengouStartDate) + 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '               ELSE CASE WHEN MONTH(SIS.ShoukyakuKaisiDate) = 1 AND DAY(SIS.ShoukyakuKaisiDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN YEAR(DATEADD(day,-1,SIS.ShoukyakuKaisiDate)) - YEAR(Gengou.GengouStartDate) + 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE YEAR(SIS.ShoukyakuKaisiDate) - YEAR(Gengou.GengouStartDate) + 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
--2018/04/26 T.Maeda      (97532-97598)改元対応(年号・取得年の抽出方法を変更)↑↑
                SET @strSQL = @strSQL + '          END AS ShutokuNen ' +CHAR(13)
                SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)    
                SET @strSQL = @strSQL + '               THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN MONTH(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE MONTH(BGJ.BunkatuGappeiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '               ELSE CASE WHEN MONTH(SIS.ShoukyakuKaisiDate) = 1 AND DAY(SIS.ShoukyakuKaisiDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN MONTH(DATEADD(day,-1,SIS.ShoukyakuKaisiDate)) ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE MONTH(SIS.ShoukyakuKaisiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '          END AS ShutokuTuki ' +CHAR(13)
            END
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↑
--  2013/01/09 S.Murakami  (N00995)分割合併対応↑
            SET @strSQL = @strSQL + '         ,SIS.TihouzeiShutokuKagaku ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,TIH.TaiyouNensuu ' +CHAR(13)
            --  2013/01/09 S.Murakami  (N00995)分割合併対応↓
            --SET @strSQL = @strSQL + '         ,RIR.ZiyuuKBN ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,CASE WHEN RIR.ZiyuuKBN = 19 THEN 4 ELSE RIR.ZiyuuKBN END AS ZiyuuKBN ' +CHAR(13)
            --  2013/01/09 S.Murakami  (N00995)分割合併対応↑
            SET @strSQL = @strSQL + '         ,RIR.RirekiDate ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,TIH.HonnendoHyoukagaku ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,TIH.HonnendoHyoukagaku ' +CHAR(13)
            --  2013/01/09 S.Murakami  (N00995)分割合併対応↓
            --SET @strSQL = @strSQL + '         ,CASE WHEN SIS.ShutokuDate >= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanBegin,111)) +  ' AND SIS.ShutokuDate <= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanEnd,111)) +  ' ' +CHAR(13)
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↓
            IF (@ShutokuDateHanteiKBN = 0)
            BEGIN
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↑
                SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '               THEN CASE WHEN BGJ.BunkatuGappeiDate >= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanBegin,111)) +  ' AND BGJ.BunkatuGappeiDate <= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanEnd,111)) +  ' ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN SKM.ZennenChuuGenkazanzonritu ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE SKM.ZennenzenGenkazanzonritu ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '               ELSE CASE WHEN SIS.ShutokuDate >= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanBegin,111)) +  ' AND SIS.ShutokuDate <= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanEnd,111)) +  ' ' +CHAR(13)
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↓
            END
            ELSE 
            BEGIN 
                SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '               THEN CASE WHEN BGJ.BunkatuGappeiDate >= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanBegin,111)) +  ' AND BGJ.BunkatuGappeiDate <= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanEnd,111)) +  ' ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN SKM.ZennenChuuGenkazanzonritu ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE SKM.ZennenzenGenkazanzonritu ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '               ELSE CASE WHEN SIS.ShoukyakuKaisiDate >= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanBegin,111)) +  ' AND SIS.ShoukyakuKaisiDate <= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanEnd,111)) +  ' ' +CHAR(13)
            END
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↑
            --  2013/01/09 S.Murakami  (N00995)分割合併対応↑
            SET @strSQL = @strSQL + '                         THEN SKM.ZennenChuuGenkazanzonritu ' +CHAR(13)
            SET @strSQL = @strSQL + '                         ELSE SKM.ZennenzenGenkazanzonritu ' +CHAR(13)
            --  2013/01/09 S.Murakami  (N00995)分割合併対応↓
            SET @strSQL = @strSQL + '                    END ' +CHAR(13)
            --  2013/01/09 S.Murakami  (N00995)分割合併対応↑
            SET @strSQL = @strSQL + '          END AS Genkazanzonritu ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,NULL AS TokureiRitu' +CHAR(13)
            SET @strSQL = @strSQL + '         ,TIH.TokureirituNumerator ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,TIH.TokureirituDenominator ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,SIS.ShutokuKBN ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,SIS.SinkokuKBN ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,SIS.ShoukyakuKaisiDate AS ShoukyakuDate ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,TIH.TokureiKikan ' +CHAR(13)
            IF @p_ShuturyokuKBN = 1
            BEGIN
                SET @strSQL = @strSQL + '         ,CASE WHEN IDO.SisanNO IS NOT NULL ' +CHAR(13)
                SET @strSQL = @strSQL + '               THEN IDO.IdousakiZigyoushoCD ' +CHAR(13)
                SET @strSQL = @strSQL + '               ELSE SHT.ShutokuZigyoushoCD ' +CHAR(13)
                SET @strSQL = @strSQL + '          END AS ZigyoushoCD ' +CHAR(13)
                SET @strSQL = @strSQL + '         ,SZG.CompanyName AS CompanyName ' +CHAR(13)
            END
            ELSE
            BEGIN
                SET @strSQL = @strSQL + '         ,''*'' AS ZigyoushoCD ' +CHAR(13)
                SET @strSQL = @strSQL + '         ,NULL AS CompanyName ' +CHAR(13)
            END
--  2013/01/09 S.Murakami  (N00995)分割合併対応↓
            --SET @strSQL = @strSQL + '         ,NULL AS Bikou ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.BunkatuGappeiDate IS NOT NULL ' +CHAR(13)
--2013/03/15 E.Sato ST (N00995)分割合併対応の追加修正
            SET @strSQL = @strSQL + '                    AND BGJ.BunkatuGappeiDate BETWEEN ''' + CONVERT(varchar, @SinkokuKikanBegin, 111) + ''' AND ''' + CONVERT(varchar, @SinkokuKikanEnd, 111) + ''' ' +CHAR(13)
--2013/03/15 E.Sato ED
--  2018/04/26 T.Maeda      (97532-97598)改元対応(備考欄の取得)↓↓
            --SET @strSQL = @strSQL + '               THEN CASE WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1989 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN ''H'' + CAST(YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - 1989 + 1 AS varchar(5)) + ''.'' + CAST(MONTH(BGJ.BunkatuGappeiDate) AS varchar(5)) ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1926 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN ''S'' + CAST(YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - 1926 + 1 AS varchar(5)) + ''.'' + CAST(MONTH(BGJ.BunkatuGappeiDate) AS varchar(5)) ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1912 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN ''T'' + CAST(YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - 1912 + 1 AS varchar(5)) + ''.'' + CAST(MONTH(BGJ.BunkatuGappeiDate) AS varchar(5)) ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1867 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN ''M'' + CAST(YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - 1867 + 1 AS varchar(5)) + ''.'' + CAST(MONTH(BGJ.BunkatuGappeiDate) AS varchar(5)) ' +CHAR(13)
            --SET @strSQL = @strSQL + '                    END ' +CHAR(13)
            SET @strSQL = @strSQL + '                    THEN GengouByBunkatuGappeiDate.GengouKigou + CAST(YEAR(BGJ.BunkatuGappeiDate) - YEAR(GengouByBunkatuGappeiDate.GengouStartDate) + 1  AS varchar(5)) + ''.'' + CAST(MONTH(BGJ.BunkatuGappeiDate) AS varchar(5))' +CHAR(13)
--  2018/04/26 T.Maeda      (97532-97598)改元対応(備考欄の取得)↑↑
--  2021 06/03 Y.Matuzawa (183279) 会社分割によって増加した資産の備考が「合併による」と表示される 対応 ↓
            --SET @strSQL = @strSQL + '                 +  ''合併による'' ' +CHAR(13)
            SET @strSQL = @strSQL + '                 +  ''分割合併による'' ' +CHAR(13)
--  2021 06/03 Y.Matuzawa (183279) 会社分割によって増加した資産の備考が「合併による」と表示される 対応 ↑
            SET @strSQL = @strSQL + '               ELSE NULL ' +CHAR(13)
            SET @strSQL = @strSQL + '          END AS Bikou ' +CHAR(13)
--  2013/01/09 S.Murakami  (N00995)分割合併対応↑
            --FROM句
            SET @strSQL = @strSQL + ' FROM N0000RR_NA_SisanJouhou SIS ' +CHAR(13)
            SET @strSQL = @strSQL + ' INNER JOIN N0000RR_NA_RirekiJouhou RIR ' +CHAR(13)
            SET @strSQL = @strSQL + ' ON  RIR.SisanNO   = SIS.SisanNO ' +CHAR(13)
            SET @strSQL = @strSQL + ' AND RIR.Edaban    = SIS.Edaban ' +CHAR(13)
--2010/12/13 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 BEGIN↓
            SET @strSQL = @strSQL + ' AND RIR.ShoukyakuKBN = 0 ' +CHAR(13)
--2010/12/13 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 END↑
            --  2013/01/09 S.Murakami  (N00995)分割合併対応↓
            --SET @strSQL = @strSQL + ' INNER JOIN N0000RR_NA_ShutokuJouhou SHT ' +CHAR(13)
            SET @strSQL = @strSQL + ' LEFT JOIN N0000RR_NA_ShutokuJouhou SHT ' +CHAR(13)
            --  2013/01/09 S.Murakami  (N00995)分割合併対応↑
            --2016/02/29 K.Shigematsu (35373)増加申告で自治体コードの範囲指定を使用した場合、分割合併で増加した資産を抽出できない  開始
            --SET @strSQL = @strSQL + ' ON  SHT.RirekiSystemNO = RIR.RirekiSystemNO ' +CHAR(13)
            SET @strSQL = @strSQL + ' ON  SHT.SisanNO      = RIR.SisanNO ' +CHAR(13)
            SET @strSQL = @strSQL + ' AND SHT.Edaban       = RIR.Edaban ' +CHAR(13)
            SET @strSQL = @strSQL + ' AND SHT.ShoukyakuKBN = 0 ' +CHAR(13)                    -- 0：(税務/会計)
            --2016/02/29 K.Shigematsu (35373)増加申告で自治体コードの範囲指定を使用した場合、分割合併で増加した資産を抽出できない 終了
            SET @strSQL = @strSQL + ' INNER JOIN N0000RR_NA_TihouzeiJouhou TIH ' +CHAR(13)
            SET @strSQL = @strSQL + ' ON  TIH.SisanNO   = SIS.SisanNO ' +CHAR(13)
            SET @strSQL = @strSQL + ' AND TIH.Edaban    = SIS.Edaban ' +CHAR(13)
            SET @strSQL = @strSQL + ' AND YEAR(TIH.SinkokuNendo) = ' + CONVERT(varchar(4),YEAR(@p_SinkokuNendo)) +CHAR(13)
            SET @strSQL = @strSQL + ' LEFT JOIN M0000MS_CommonCode CDM ' +CHAR(13)
            SET @strSQL = @strSQL + ' ON  CDM.Shubetu   = 2950 ' +CHAR(13)
            SET @strSQL = @strSQL + ' AND CDM.CodeValue = SIS.SisanShubetuNO ' +CHAR(13)
            SET @strSQL = @strSQL + ' LEFT JOIN N0000MS_NA_Shoukyakuritu SKM ' +CHAR(13)
            SET @strSQL = @strSQL + ' ON  SKM.TaiyouNensuu = TIH.TaiyouNensuu ' +CHAR(13)
            SET @strSQL = @strSQL + ' LEFT JOIN (SELECT IDM.SisanNO ' +CHAR(13)
            SET @strSQL = @strSQL + '                  ,IDM.Edaban ' +CHAR(13)
            SET @strSQL = @strSQL + '                  ,IDM.IdousakiZigyoushoCD ' +CHAR(13)
            SET @strSQL = @strSQL + '                  ,IDM.IdousakiZititaiCD ' +CHAR(13)
            SET @strSQL = @strSQL + '            FROM N0000RR_NA_IdouJouhou IDM ' +CHAR(13)
            SET @strSQL = @strSQL + '            INNER JOIN (SELECT SisanNO ' +CHAR(13)
            SET @strSQL = @strSQL + '                             ,Edaban ' +CHAR(13)
            SET @strSQL = @strSQL + '                             ,MAX(IdouDate) AS IdouDate ' +CHAR(13)
            SET @strSQL = @strSQL + '                       FROM N0000RR_NA_IdouJouhou ' +CHAR(13)
            SET @strSQL = @strSQL + '                       WHERE IdouDate >= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanBegin,111)) +CHAR(13)
            SET @strSQL = @strSQL + '                       AND   IdouDate <= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanEnd,111)) +CHAR(13)
--2011/01/19 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 戻り対応 BEGIN↓
            SET @strSQL = @strSQL + '                       AND   ShoukyakuKBN = 0 ' +CHAR(13)
--2011/01/19 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 戻り対応 END↑            
            SET @strSQL = @strSQL + '                       GROUP BY SisanNO ,Edaban ' +CHAR(13)
            SET @strSQL = @strSQL + '                      ) IDS ' +CHAR(13)
            SET @strSQL = @strSQL + '            ON  IDS.SisanNO  = IDM.SisanNO ' +CHAR(13)
            SET @strSQL = @strSQL + '            AND IDS.Edaban   = IDM.Edaban ' +CHAR(13)
            SET @strSQL = @strSQL + '            AND IDS.IdouDate = IDM.IdouDate ' +CHAR(13)
--2010/12/13 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 BEGIN↓
            SET @strSQL = @strSQL + '            AND IDM.ShoukyakuKBN = 0 ' +CHAR(13)
--2010/12/13 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 END↑
            SET @strSQL = @strSQL + '          ) IDO  ' +CHAR(13)
            SET @strSQL = @strSQL + ' ON  IDO.SisanNO   = SIS.SisanNO ' +CHAR(13)
            SET @strSQL = @strSQL + ' AND IDO.Edaban    = SIS.Edaban ' +CHAR(13)
            SET @strSQL = @strSQL + ' LEFT JOIN N0000MS_NA_SisanZigyousho SZG ' +CHAR(13)
            SET @strSQL = @strSQL + ' ON  SZG.ZigyoushoCD = CASE WHEN IDO.SisanNO IS NOT NULL ' +CHAR(13)
            SET @strSQL = @strSQL + '                            THEN IDO.IdousakiZigyoushoCD ' +CHAR(13)
            SET @strSQL = @strSQL + '                            ELSE SIS.ZigyoushoCD ' +CHAR(13)
            SET @strSQL = @strSQL + '                       END ' +CHAR(13)
--  2013/01/09 S.Murakami  (N00995)分割合併対応↓
            SET @strSQL = @strSQL + ' LEFT JOIN N0000RR_NA_BunkatuGappeiJouhou BGJ ' +CHAR(13)
            SET @strSQL = @strSQL + ' ON  BGJ.SisanNO = SIS.SisanNO ' +CHAR(13)
            SET @strSQL = @strSQL + ' AND  BGJ.Edaban = SIS.Edaban ' +CHAR(13)
            SET @strSQL = @strSQL + ' AND  BGJ.ShoukyakuKBN = 0 ' +CHAR(13)
            SET @strSQL = @strSQL + ' AND  BGJ.ZouGenKBN = 0 ' +CHAR(13)
--  2013/01/09 S.Murakami  (N00995)分割合併対応↑
            -- 引数.事業所セキュリティパターンコードが""でないとき、以下のJOIN句を追加
            IF (@p_ZigSecPtnCD IS NOT NULL)
            BEGIN
                SET @strSQL = @strSQL + ' INNER JOIN ' +CHAR(13)
                SET @strSQL = @strSQL + '     M0000MS_MA_ZigyoushoSecurityYouso MZSY ' +CHAR(13) -- 事業所セキュリティ要素マスタ
                SET @strSQL = @strSQL + ' ON  MZSY.ZigyoushoCDYouso   = CASE WHEN IDO.SisanNO IS NOT NULL ' +CHAR(13)
                SET @strSQL = @strSQL + '                                    THEN IDO.IdousakiZigyoushoCD ' +CHAR(13)
                SET @strSQL = @strSQL + '                                    ELSE SIS.ZigyoushoCD ' +CHAR(13)
                SET @strSQL = @strSQL + '                               END ' +CHAR(13)
                SET @strSQL = @strSQL + ' AND MZSY.SecurityPatternCD  = ' + dbo.N0000FC_Escape_Varchar(@p_ZigSecPtnCD) +CHAR(13)
            END
--  2018/04/26 T.Maeda      (97532-97598)改元対応(元号マスタを結合)↓↓
            SET @strSQL = @strSQL + ' LEFT JOIN A0000MS_Gengou AS Gengou ' +CHAR(13)
            --資産の取得判定を、取得日もしくは償却開始日で行うかで元号マスタの結合条件を変更
            --取得日で判定を行う場合
            IF(@ShutokuDateHanteiKBN =0)
            BEGIN
            --元号開始日の範囲指定
            --合併区分が1：売買の時、分割合併日で考慮
                SET @strSQL = @strSQL + ' ON Gengou.GengouStartDate<= CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            THEN DATEADD(DAY,-1,BGJ.BunkatuGappeiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            ELSE BGJ.BunkatuGappeiDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  END ' +CHAR(13)
            --合併区分が0：簿価引継の時、取得日で考慮
                SET @strSQL = @strSQL + '                                  ELSE CASE WHEN MONTH(SIS.ShutokuDate) = 1 AND DAY(SIS.ShutokuDate) = 1  ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            THEN DATEADD(DAY,-1,SIS.ShutokuDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            ELSE SIS.ShutokuDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  END ' +CHAR(13)
                SET @strSQL = @strSQL + '                             END ' +CHAR(13)

            --元号終了日の範囲指定
            --合併区分が1：売買の時、分割合併日で考慮
                SET @strSQL = @strSQL + ' AND DATEADD(DAY,1,Gengou.GengouEndDate) > CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            THEN DATEADD(DAY,-1,BGJ.BunkatuGappeiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            ELSE BGJ.BunkatuGappeiDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  END ' +CHAR(13)
            --合併区分が0：簿価引継の時、取得日で考慮
                SET @strSQL = @strSQL + '                                  ELSE CASE WHEN MONTH(SIS.ShutokuDate) = 1 AND DAY(SIS.ShutokuDate) = 1  ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            THEN DATEADD(DAY,-1,SIS.ShutokuDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            ELSE SIS.ShutokuDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  END ' +CHAR(13)
                SET @strSQL = @strSQL + '                             END ' +CHAR(13)
            END

            --償却開始日で判定を行う場合
            ELSE
            BEGIN
            --合併区分が1：売買の時、分割合併日で考慮
                SET @strSQL = @strSQL + ' ON Gengou.GengouStartDate<= CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            THEN DATEADD(DAY,-1,BGJ.BunkatuGappeiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            ELSE BGJ.BunkatuGappeiDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  END ' +CHAR(13)
            --合併区分が0：簿価引継の時、償却開始日で考慮
                SET @strSQL = @strSQL + '                                  ELSE CASE WHEN MONTH(SIS.ShoukyakuKaisiDate) = 1 AND DAY(SIS.ShoukyakuKaisiDate) = 1  ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            THEN DATEADD(DAY,-1,SIS.ShoukyakuKaisiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            ELSE SIS.ShoukyakuKaisiDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  END ' +CHAR(13)
                SET @strSQL = @strSQL + '                             END ' +CHAR(13)

            --元号終了日の範囲指定
            --合併区分が1：売買の時、分割合併日で考慮
                SET @strSQL = @strSQL + ' AND DATEADD(DAY,1,Gengou.GengouEndDate) > CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            THEN DATEADD(DAY,-1,BGJ.BunkatuGappeiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            ELSE BGJ.BunkatuGappeiDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  END ' +CHAR(13)
            --合併区分が0：簿価引継の時、償却開始日で考慮
                SET @strSQL = @strSQL + '                                  ELSE CASE WHEN MONTH(SIS.ShoukyakuKaisiDate) = 1 AND DAY(SIS.ShoukyakuKaisiDate) = 1  ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            THEN DATEADD(DAY,-1,SIS.ShoukyakuKaisiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            ELSE SIS.ShoukyakuKaisiDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  END ' +CHAR(13)
                SET @strSQL = @strSQL + '                             END ' +CHAR(13)
            END

            --分割合併資産の備考に記載する為に必要な元号マスタを結合
            SET @strSQL = @strSQL + ' LEFT JOIN A0000MS_Gengou AS GengouByBunkatuGappeiDate ' +CHAR(13)
            SET @strSQL = @strSQL + ' ON GengouByBunkatuGappeiDate.GengouStartDate<= BGJ.BunkatuGappeiDate ' +CHAR(13)
            SET @strSQL = @strSQL + ' AND DATEADD(DAY,1,GengouByBunkatuGappeiDate.GengouEndDate) > BGJ.BunkatuGappeiDate ' +CHAR(13)
--  2018/04/26 T.Maeda      (97532-97598)改元対応(元号マスタを結合)↑↑
            --WHERE句
            SET @strSQL = @strSQL + ' WHERE 1 = 1 ' +CHAR(13)
            IF @p_SisanKBN <> 2
            BEGIN
                --仮資産フラグ
                SET @strSQL = @strSQL + ' AND SIS.KarisisanFlag = ' + CONVERT(varchar(1),@p_SisanKBN) +CHAR(13)
            END
--2010/12/13 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 BEGIN↓
            IF @p_SisanKBN = 2
            BEGIN
                --仮資産フラグ
                SET @strSQL = @strSQL + ' AND (SIS.KarisisanFlag = 0 OR SIS.KarisisanFlag = 1) ' +CHAR(13)
            END
--2010/12/13 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 END↑
            --事由区分
            --  2013/01/09 S.Murakami  (N00995)分割合併対応↓
            --SET @strSQL = @strSQL + ' AND RIR.ZiyuuKBN = 1 ' +CHAR(13)
            SET @strSQL = @strSQL + ' AND RIR.ZiyuuKBN = CASE WHEN BGJ.BunkatuGappeiDate IS NULL THEN 1 ELSE 19 END ' +CHAR(13)
            --  2013/01/09 S.Murakami  (N00995)分割合併対応↑
            --申告区分
            SET @strSQL = @strSQL + ' AND SIS.SinkokuKBN IN (0,2) ' +CHAR(13)
            --履歴日付
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↓
            IF (@ShutokuDateHanteiKBN = 0)
            BEGIN
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↑
                SET @strSQL = @strSQL + ' AND RIR.RirekiDate >= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanBegin,111)) +CHAR(13)
                SET @strSQL = @strSQL + ' AND RIR.RirekiDate <= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanEnd,111)) +CHAR(13)
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↓
            END
            ELSE
            BEGIN
                SET @strSQL = @strSQL + ' AND SIS.ShoukyakuKaisiDate >= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanBegin,111)) +CHAR(13)
                SET @strSQL = @strSQL + ' AND SIS.ShoukyakuKaisiDate <= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanEnd,111)) +CHAR(13)            
            END
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↑
            --資産種別NO
            SET @strSQL = @strSQL + ' AND SIS.SisanShubetuNO BETWEEN 1 AND 6 ' +CHAR(13)
            --自治体CD
            IF @p_ZititaiCDBegin IS NOT NULL
            BEGIN
--↓2009/07/06 Y.Ishizawa 自治体の範囲指定条件変更
--                 SET @strSQL = @strSQL + ' AND SHT.ShutokuZititaiCD >= ''' + @p_ZititaiCDBegin + '''' +CHAR(13)
                SET @strSQL = @strSQL + ' AND CASE WHEN IDO.SisanNO IS NOT NULL ' +CHAR(13)
                SET @strSQL = @strSQL + '          THEN IDO.IdousakiZititaiCD ' +CHAR(13)
                SET @strSQL = @strSQL + '          ELSE SHT.ShutokuZititaiCD ' +CHAR(13)
                SET @strSQL = @strSQL + '     END >= ''' + @p_ZititaiCDBegin + '''' +CHAR(13)
--↑2009/07/06 Y.Ishizawa 自治体の範囲指定条件変更
            END                
            IF @p_ZititaiCDEnd IS NOT NULL
            BEGIN
--↓2009/07/06 Y.Ishizawa 自治体の範囲指定条件変更
--                 SET @strSQL = @strSQL + ' AND SHT.ShutokuZititaiCD <= ''' + @p_ZititaiCDEnd + '''' +CHAR(13)
                SET @strSQL = @strSQL + ' AND CASE WHEN IDO.SisanNO IS NOT NULL ' +CHAR(13)
                SET @strSQL = @strSQL + '          THEN IDO.IdousakiZititaiCD ' +CHAR(13)
                SET @strSQL = @strSQL + '          ELSE SHT.ShutokuZititaiCD ' +CHAR(13)
                SET @strSQL = @strSQL + '     END <= ''' + @p_ZititaiCDEnd + '''' +CHAR(13)
--↑2009/07/06 Y.Ishizawa 自治体の範囲指定条件変更
            END
            --事業所CD
            IF @p_ShuturyokuKBN = 1
            BEGIN
                IF @p_ZigyoushoCDBegin IS NOT NULL
                BEGIN
                    SET @strSQL = @strSQL + ' AND CASE WHEN IDO.SisanNO IS NOT NULL ' +CHAR(13)
                    SET @strSQL = @strSQL + '          THEN IDO.IdousakiZigyoushoCD ' +CHAR(13)
                    SET @strSQL = @strSQL + '          ELSE SIS.ZigyoushoCD ' +CHAR(13)
                    SET @strSQL = @strSQL + '     END >= ''' + @p_ZigyoushoCDBegin + '''' +CHAR(13)
                END
                IF @p_ZigyoushoCDEnd IS NOT NULL
                BEGIN
                    SET @strSQL = @strSQL + ' AND CASE WHEN IDO.SisanNO IS NOT NULL ' +CHAR(13)
                    SET @strSQL = @strSQL + '          THEN IDO.IdousakiZigyoushoCD ' +CHAR(13)
                    SET @strSQL = @strSQL + '          ELSE SIS.ZigyoushoCD ' +CHAR(13)
                    SET @strSQL = @strSQL + '     END <= ''' + @p_ZigyoushoCDEnd + '''' +CHAR(13)
                END
            END
            --
            SET @strSQL = @strSQL + ' AND NOT EXISTS ( ' +CHAR(13)
            SET @strSQL = @strSQL + '         SELECT * FROM N0000RR_NA_RirekiJouhou RIR ' +CHAR(13)
            SET @strSQL = @strSQL + '         WHERE RIR.SisanNO     = SIS.SisanNO ' +CHAR(13)
            SET @strSQL = @strSQL + '         AND   RIR.Edaban      = SIS.Edaban ' +CHAR(13)
            SET @strSQL = @strSQL + '         AND   RIR.RirekiDate <= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanEnd,111)) + '' +CHAR(13)
--2010/12/13 M.Shinbo 変更 (N00697)IFRS・ダブルスタンダード対応 BEGIN↓
--            SET @strSQL = @strSQL + '         AND   RIR.ZiyuuKBN    = 2 ) ' +CHAR(13)
            --  2013/01/09 S.Murakami  (N00995)分割合併対応↓
            --SET @strSQL = @strSQL + '         AND   RIR.ZiyuuKBN    = 2 ' +CHAR(13)
            SET @strSQL = @strSQL + '         AND   RIR.ZiyuuKBN    IN(2,20) ' +CHAR(13)
            --  2013/01/09 S.Murakami  (N00995)分割合併対応↑
            SET @strSQL = @strSQL + '         AND   RIR.ShoukyakuKBN = 0 ) ' +CHAR(13)
--2010/12/13 M.Shinbo 変更 (N00697)IFRS・ダブルスタンダード対応 END↑

            EXEC (@strSQL)

            SET @ErrorCode = @@ERROR
            IF @ErrorCode <> 0 GOTO ERR_SHORI

            --2-1-1.事由区分=3(移動)のデータを抽出
            SET @strSQL = ''
            --INSERT句
            SET @strSQL = @strSQL + ' INSERT INTO #N0000WK_Obic7fx_Win_N0000_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_Data ( ' +CHAR(13)
            SET @strSQL = @strSQL + '          RirekiSystemNO ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,SisanNO ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,Edaban ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,SisanName ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,ZititaiCD ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,SisanShubetuNO ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,Suuryou ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,ShutokuDate ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,Nengou ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,ShutokuNen ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,ShutokuTuki ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,ShutokuKagaku ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,TaiyouNensuu ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,ZiyuuKBN ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,RirekiDate ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,Kagaku ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,KazeiKagaku ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,Genkazanzonritu ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,Tokureiritu ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,TokureirituNumerator ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,TokureirituDenominator ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,ShutokuKBN ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,SinkokuKBN ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,ShoukyakuKaisiDate ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,TokureiKikan ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,ZigyoushoCD ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,CompanyName ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,Bikou ' +CHAR(13)
            SET @strSQL = @strSQL + '         ) ' +CHAR(13)
            --SELECT句
            SET @strSQL = @strSQL + ' SELECT  ' +CHAR(13)
            SET @strSQL = @strSQL + '          RIR.MAXRirekiSystemNO ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,SIS.SisanNO ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,SIS.Edaban ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,SIS.SisanName ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,IDMX.IdousakiZititaiCD AS ZititaiCD ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,SIS.SisanShubetuNO ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,SIS.Suuryou ' +CHAR(13)
--  2013/01/09 S.Murakami  (N00995)分割合併対応↓
            --SET @strSQL = @strSQL + '         ,CASE WHEN MONTH(SIS.ShutokuDate) = 1 AND DAY(SIS.ShutokuDate) = 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '               THEN DATEADD(day,-1,SIS.ShutokuDate) ' +CHAR(13)
            --SET @strSQL = @strSQL + '               ELSE SIS.ShutokuDate ' +CHAR(13)
            --SET @strSQL = @strSQL + '          END AS ShutokuDate ' +CHAR(13)
            --SET @strSQL = @strSQL + '         ,CASE WHEN MONTH(SIS.ShutokuDate) = 1 AND DAY(SIS.ShutokuDate) = 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '               THEN CASE WHEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) >= 1989 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN 4 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) >= 1926 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN 3 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) >= 1912 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN 2 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) >= 1867 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                    END ' +CHAR(13)
            --SET @strSQL = @strSQL + '               ELSE CASE WHEN YEAR(SIS.ShutokuDate) >= 1989 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN 4 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(SIS.ShutokuDate) >= 1926 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN 3 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(SIS.ShutokuDate) >= 1912 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN 2 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(SIS.ShutokuDate) >= 1867 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                    END ' +CHAR(13)
            --SET @strSQL = @strSQL + '          END AS Nengou ' +CHAR(13)
            --SET @strSQL = @strSQL + '         ,CASE WHEN MONTH(SIS.ShutokuDate) = 1 AND DAY(SIS.ShutokuDate) = 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '               THEN CASE WHEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) >= 1989 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) - 1989 + 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) >= 1926 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) - 1926 + 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) >= 1912 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) - 1912 + 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) >= 1867 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) - 1867 + 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                    END ' +CHAR(13)
            --SET @strSQL = @strSQL + '               ELSE CASE WHEN YEAR(SIS.ShutokuDate) >= 1989 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN YEAR(SIS.ShutokuDate) - 1989 + 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(SIS.ShutokuDate) >= 1926 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN YEAR(SIS.ShutokuDate) - 1926 + 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(SIS.ShutokuDate) >= 1912 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN YEAR(SIS.ShutokuDate) - 1912 + 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(SIS.ShutokuDate) >= 1867 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN YEAR(SIS.ShutokuDate) - 1867 + 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                    END ' +CHAR(13)
            --SET @strSQL = @strSQL + '          END AS ShutokuNen ' +CHAR(13)
            --SET @strSQL = @strSQL + '         ,CASE WHEN MONTH(SIS.ShutokuDate) = 1 AND DAY(SIS.ShutokuDate) = 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '               THEN MONTH(DATEADD(day,-1,SIS.ShutokuDate)) ' +CHAR(13)
            --SET @strSQL = @strSQL + '               ELSE MONTH(SIS.ShutokuDate) ' +CHAR(13)
            --SET @strSQL = @strSQL + '          END AS ShutokuTuki ' +CHAR(13)
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↓
            IF (@ShutokuDateHanteiKBN = 0)
            BEGIN
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↑
                SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '               THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN DATEADD(day,-1,BGJ.BunkatuGappeiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE BGJ.BunkatuGappeiDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '               ELSE CASE WHEN MONTH(SIS.ShutokuDate) = 1 AND DAY(SIS.ShutokuDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN DATEADD(day,-1,SIS.ShutokuDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE SIS.ShutokuDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '          END AS ShutokuDate ' +CHAR(13)
--2018/04/26 T.Maeda      (97532-97598)改元対応(年号・取得年の抽出方法を変更)↓↓
                --SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '               THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         THEN CASE WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 4 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 3 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 2 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         ELSE CASE WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 4 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 3 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 2 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                --SET @strSQL = @strSQL + '               ELSE CASE WHEN MONTH(SIS.ShutokuDate) = 1 AND DAY(SIS.ShutokuDate) = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         THEN CASE WHEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 4 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 3 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 2 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         ELSE CASE WHEN YEAR(SIS.ShutokuDate) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 4 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(SIS.ShutokuDate) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 3 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(SIS.ShutokuDate) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 2 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(SIS.ShutokuDate) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                --SET @strSQL = @strSQL + '          END AS Nengou ' +CHAR(13)
                SET @strSQL = @strSQL + '          ,Gengou.GengouKBN AS Nengou ' +CHAR(13)
                --SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '               THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         THEN CASE WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - 1989 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - 1926 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - 1912 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - 1867 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         ELSE CASE WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(BGJ.BunkatuGappeiDate) - 1989 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(BGJ.BunkatuGappeiDate) - 1926 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(BGJ.BunkatuGappeiDate) - 1912 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(BGJ.BunkatuGappeiDate) - 1867 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                --SET @strSQL = @strSQL + '               ELSE CASE WHEN MONTH(SIS.ShutokuDate) = 1 AND DAY(SIS.ShutokuDate) = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         THEN CASE WHEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) - 1989 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) - 1926 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) - 1912 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) - 1867 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         ELSE CASE WHEN YEAR(SIS.ShutokuDate) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(SIS.ShutokuDate) - 1989 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(SIS.ShutokuDate) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(SIS.ShutokuDate) - 1926 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(SIS.ShutokuDate) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(SIS.ShutokuDate) - 1912 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(SIS.ShutokuDate) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(SIS.ShutokuDate) - 1867 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '               THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1  ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - YEAR(Gengou.GengouStartDate) + 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE YEAR(BGJ.BunkatuGappeiDate) - YEAR(Gengou.GengouStartDate) + 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '               ELSE CASE WHEN MONTH(SIS.ShutokuDate) = 1 AND DAY(SIS.ShutokuDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN YEAR(DATEADD(day,-1,SIS.ShutokuDate)) - YEAR(Gengou.GengouStartDate) + 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE YEAR(SIS.ShutokuDate) - YEAR(Gengou.GengouStartDate) + 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
--2018/04/26 T.Maeda      (97532-97598)改元対応(年号・取得年の抽出方法を変更)↑↑
                SET @strSQL = @strSQL + '          END AS ShutokuNen ' +CHAR(13)
                SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)    
                SET @strSQL = @strSQL + '               THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN MONTH(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE MONTH(BGJ.BunkatuGappeiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '               ELSE CASE WHEN MONTH(SIS.ShutokuDate) = 1 AND DAY(SIS.ShutokuDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN MONTH(DATEADD(day,-1,SIS.ShutokuDate)) ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE MONTH(SIS.ShutokuDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '          END AS ShutokuTuki ' +CHAR(13)
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↓
            END 
            ELSE
            BEGIN
                SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '               THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN DATEADD(day,-1,BGJ.BunkatuGappeiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE BGJ.BunkatuGappeiDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '               ELSE CASE WHEN MONTH(SIS.ShoukyakuKaisiDate) = 1 AND DAY(SIS.ShoukyakuKaisiDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN DATEADD(day,-1,SIS.ShoukyakuKaisiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE SIS.ShoukyakuKaisiDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '          END AS ShoukyakuKaisiDate ' +CHAR(13)
--2018/04/26 T.Maeda      (97532-97598)改元対応(年号・取得年の抽出方法を変更)↓↓
                --SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '               THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         THEN CASE WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 4 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 3 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 2 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         ELSE CASE WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 4 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 3 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 2 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                --SET @strSQL = @strSQL + '               ELSE CASE WHEN MONTH(SIS.ShoukyakuKaisiDate) = 1 AND DAY(SIS.ShoukyakuKaisiDate) = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         THEN CASE WHEN YEAR(DATEADD(day,-1,SIS.ShoukyakuKaisiDate)) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 4 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,SIS.ShoukyakuKaisiDate)) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 3 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,SIS.ShoukyakuKaisiDate)) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 2 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,SIS.ShoukyakuKaisiDate)) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         ELSE CASE WHEN YEAR(SIS.ShoukyakuKaisiDate) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 4 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(SIS.ShoukyakuKaisiDate) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 3 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(SIS.ShoukyakuKaisiDate) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 2 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(SIS.ShoukyakuKaisiDate) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                --SET @strSQL = @strSQL + '          END AS Nengou ' +CHAR(13)
                SET @strSQL = @strSQL + '          ,Gengou.GengouKBN AS Nengou ' +CHAR(13)
                --SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '               THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         THEN CASE WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - 1989 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - 1926 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - 1912 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - 1867 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         ELSE CASE WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(BGJ.BunkatuGappeiDate) - 1989 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(BGJ.BunkatuGappeiDate) - 1926 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(BGJ.BunkatuGappeiDate) - 1912 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(BGJ.BunkatuGappeiDate) - 1867 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                --SET @strSQL = @strSQL + '               ELSE CASE WHEN MONTH(SIS.ShoukyakuKaisiDate) = 1 AND DAY(SIS.ShoukyakuKaisiDate) = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         THEN CASE WHEN YEAR(DATEADD(day,-1,SIS.ShoukyakuKaisiDate)) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,SIS.ShoukyakuKaisiDate)) - 1989 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,SIS.ShoukyakuKaisiDate)) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,SIS.ShoukyakuKaisiDate)) - 1926 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,SIS.ShoukyakuKaisiDate)) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,SIS.ShoukyakuKaisiDate)) - 1912 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,SIS.ShoukyakuKaisiDate)) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,SIS.ShoukyakuKaisiDate)) - 1867 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         ELSE CASE WHEN YEAR(SIS.ShoukyakuKaisiDate) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(SIS.ShoukyakuKaisiDate) - 1989 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(SIS.ShoukyakuKaisiDate) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(SIS.ShoukyakuKaisiDate) - 1926 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(SIS.ShoukyakuKaisiDate) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(SIS.ShoukyakuKaisiDate) - 1912 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(SIS.ShoukyakuKaisiDate) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(SIS.ShoukyakuKaisiDate) - 1867 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '               THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1  ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - YEAR(Gengou.GengouStartDate) + 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE YEAR(BGJ.BunkatuGappeiDate) - YEAR(Gengou.GengouStartDate) + 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '               ELSE CASE WHEN MONTH(SIS.ShoukyakuKaisiDate) = 1 AND DAY(SIS.ShoukyakuKaisiDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN YEAR(DATEADD(day,-1,SIS.ShoukyakuKaisiDate)) - YEAR(Gengou.GengouStartDate) + 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE YEAR(SIS.ShoukyakuKaisiDate) - YEAR(Gengou.GengouStartDate) + 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
--2018/04/26 T.Maeda      (97532-97598)改元対応(年号・取得年の抽出方法を変更)↑↑
                SET @strSQL = @strSQL + '          END AS ShutokuNen' +CHAR(13)
                SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)    
                SET @strSQL = @strSQL + '               THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN MONTH(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE MONTH(BGJ.BunkatuGappeiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '               ELSE CASE WHEN MONTH(SIS.ShoukyakuKaisiDate) = 1 AND DAY(SIS.ShoukyakuKaisiDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN MONTH(DATEADD(day,-1,SIS.ShoukyakuKaisiDate)) ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE MONTH(SIS.ShoukyakuKaisiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '          END AS ShutokuTuki ' +CHAR(13)
            END
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↑
--  2013/01/09 S.Murakami  (N00995)分割合併対応↑
            SET @strSQL = @strSQL + '         ,SIS.TihouzeiShutokuKagaku ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,TIH.TaiyouNensuu ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,RIR.ZiyuuKBN ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,RIR.RirekiDate ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,TIH.HonnendoHyoukagaku ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,TIH.HonnendoHyoukagaku ' +CHAR(13)
            --  2013/01/09 S.Murakami  (N00995)分割合併対応↓
            --SET @strSQL = @strSQL + '         ,CASE WHEN SIS.ShutokuDate >= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanBegin,111)) +  ' AND SIS.ShutokuDate <= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanEnd,111)) +  ' ' +CHAR(13)
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↓
            IF (@ShutokuDateHanteiKBN = 0)
            BEGIN
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↑
                SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '               THEN CASE WHEN BGJ.BunkatuGappeiDate >= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanBegin,111)) +  ' AND BGJ.BunkatuGappeiDate <= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanEnd,111)) +  ' ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN SKM.ZennenChuuGenkazanzonritu ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE SKM.ZennenzenGenkazanzonritu ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '               ELSE CASE WHEN SIS.ShutokuDate >= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanBegin,111)) +  ' AND SIS.ShutokuDate <= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanEnd,111)) +  ' ' +CHAR(13)
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↓
            END
            ELSE
            BEGIN
                SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '               THEN CASE WHEN BGJ.BunkatuGappeiDate >= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanBegin,111)) +  ' AND BGJ.BunkatuGappeiDate <= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanEnd,111)) +  ' ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN SKM.ZennenChuuGenkazanzonritu ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE SKM.ZennenzenGenkazanzonritu ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '               ELSE CASE WHEN SIS.ShoukyakuKaisiDate >= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanBegin,111)) +  ' AND SIS.ShoukyakuKaisiDate <= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanEnd,111)) +  ' ' +CHAR(13)
            END
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↑
            --  2013/01/09 S.Murakami  (N00995)分割合併対応↑
            SET @strSQL = @strSQL + '                         THEN SKM.ZennenChuuGenkazanzonritu ' +CHAR(13)
            SET @strSQL = @strSQL + '                         ELSE SKM.ZennenzenGenkazanzonritu ' +CHAR(13)
            --  2013/01/09 S.Murakami  (N00995)分割合併対応↓
            SET @strSQL = @strSQL + '                    END ' +CHAR(13)
            --  2013/01/09 S.Murakami  (N00995)分割合併対応↑
            SET @strSQL = @strSQL + '          END AS Genkazanzonritu ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,NULL AS TokureiRitu' +CHAR(13)
            SET @strSQL = @strSQL + '         ,TIH.TokureirituNumerator ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,TIH.TokureirituDenominator ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,SIS.ShutokuKBN ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,SIS.SinkokuKBN ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,SIS.ShoukyakuKaisiDate AS ShoukyakuDate ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,TIH.TokureiKikan ' +CHAR(13)
            IF @p_ShuturyokuKBN = 1
            BEGIN
                SET @strSQL = @strSQL + '         ,IDMX.IdousakiZigyoushoCD AS ZigyoushoCD ' +CHAR(13)
                SET @strSQL = @strSQL + '         ,SZG.CompanyName AS CompanyName ' +CHAR(13)
            END
            ELSE
            BEGIN
                SET @strSQL = @strSQL + '         ,''*'' AS ZigyoushoCD ' +CHAR(13)
                SET @strSQL = @strSQL + '         ,NULL AS CompanyName ' +CHAR(13)
            END
            SET @strSQL = @strSQL + '         ,NULL AS Bikou ' +CHAR(13)
            --FROM句
            SET @strSQL = @strSQL + ' FROM N0000RR_NA_SisanJouhou SIS ' +CHAR(13)
            SET @strSQL = @strSQL + ' INNER JOIN (SELECT RIMX.SisanNO ' +CHAR(13)
            SET @strSQL = @strSQL + '                   ,RIMX.Edaban ' +CHAR(13)
            SET @strSQL = @strSQL + '                   ,RIMX.RirekiSystemNO AS MAXRirekiSystemNO ' +CHAR(13)
            SET @strSQL = @strSQL + '                   ,RIMN.RirekiSystemNO AS MINRirekiSystemNO ' +CHAR(13)
            SET @strSQL = @strSQL + '                   ,RIMX.RirekiDate ' +CHAR(13)
            SET @strSQL = @strSQL + '                   ,RIMX.ZiyuuKBN ' +CHAR(13)
            SET @strSQL = @strSQL + '            FROM (SELECT SisanNO ' +CHAR(13)
            SET @strSQL = @strSQL + '                        ,Edaban ' +CHAR(13)
            SET @strSQL = @strSQL + '                        ,MAX(RirekiDate) AS RirekiDateMAX ' +CHAR(13)
            SET @strSQL = @strSQL + '                        ,MIN(RirekiDate) AS RirekiDateMIN ' +CHAR(13)
            SET @strSQL = @strSQL + '                  FROM N0000RR_NA_RirekiJouhou ' +CHAR(13)
            SET @strSQL = @strSQL + '                  WHERE RirekiDate >= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanBegin,111)) +CHAR(13)
            SET @strSQL = @strSQL + '                  AND   RirekiDate <= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanEnd,111)) +CHAR(13)
--↓2011/01/20 Y.Nakazawa (N00748)分割などの異動により抽出結果が不正になる不具合修正
            SET @strSQL = @strSQL + '                  AND   ZiyuuKBN = 3' +CHAR(13)
--↑2011/01/20 Y.Nakazawa (N00748)分割などの異動により抽出結果が不正になる不具合修正
--2010/12/13 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 BEGIN↓
            SET @strSQL = @strSQL + '                  AND   ShoukyakuKBN = 0 ' +CHAR(13)
--2010/12/13 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 END↑
            SET @strSQL = @strSQL + '                  GROUP BY SisanNO ,Edaban ' +CHAR(13)
            SET @strSQL = @strSQL + '                 ) RIS ' +CHAR(13)
            SET @strSQL = @strSQL + '            LEFT JOIN N0000RR_NA_RirekiJouhou RIMX ' +CHAR(13)
            SET @strSQL = @strSQL + '            ON  RIMX.SisanNO     = RIS.SisanNO ' +CHAR(13)
            SET @strSQL = @strSQL + '            AND RIMX.Edaban      = RIS.Edaban ' +CHAR(13)
            SET @strSQL = @strSQL + '            AND RIMX.RirekiDate  = RIS.RirekiDateMAX ' +CHAR(13)
--2011/01/19 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 戻り対応 BEGIN↓
            SET @strSQL = @strSQL + '            AND RIMX.ShoukyakuKBN = 0 ' +CHAR(13)
--2011/01/19 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 戻り対応 END↑
            SET @strSQL = @strSQL + '            LEFT JOIN N0000RR_NA_RirekiJouhou RIMN ' +CHAR(13)
            SET @strSQL = @strSQL + '            ON  RIMN.SisanNO     = RIS.SisanNO ' +CHAR(13)
            SET @strSQL = @strSQL + '            AND RIMN.Edaban      = RIS.Edaban ' +CHAR(13)
            SET @strSQL = @strSQL + '            AND RIMN.RirekiDate  = RIS.RirekiDateMIN ' +CHAR(13)
--2011/01/19 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 戻り対応 BEGIN↓
            SET @strSQL = @strSQL + '            AND RIMN.ShoukyakuKBN = 0 ' +CHAR(13)
--2011/01/19 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 戻り対応 END↑
            SET @strSQL = @strSQL + '          ) RIR  ' +CHAR(13)
            SET @strSQL = @strSQL + ' ON  RIR.SisanNO   = SIS.SisanNO ' +CHAR(13)
            SET @strSQL = @strSQL + ' AND RIR.Edaban    = SIS.Edaban ' +CHAR(13)
            SET @strSQL = @strSQL + ' INNER JOIN N0000RR_NA_IdouJouhou IDMX ' +CHAR(13)
            SET @strSQL = @strSQL + ' ON  IDMX.RirekiSystemNO   = RIR.MAXRirekiSystemNO ' +CHAR(13)
            SET @strSQL = @strSQL + ' INNER JOIN N0000RR_NA_IdouJouhou IDMN ' +CHAR(13)
            SET @strSQL = @strSQL + ' ON  IDMN.RirekiSystemNO   = RIR.MINRirekiSystemNO ' +CHAR(13)
            SET @strSQL = @strSQL + ' INNER JOIN N0000RR_NA_TihouzeiJouhou TIH ' +CHAR(13)
            SET @strSQL = @strSQL + ' ON  TIH.SisanNO      = SIS.SisanNO ' +CHAR(13)
            SET @strSQL = @strSQL + ' AND TIH.Edaban       = SIS.Edaban ' +CHAR(13)
            SET @strSQL = @strSQL + ' AND YEAR(TIH.SinkokuNendo) = ' + CONVERT(varchar(4),YEAR(@p_SinkokuNendo)) +CHAR(13)
            SET @strSQL = @strSQL + ' LEFT JOIN M0000MS_CommonCode CDM ' +CHAR(13)
            SET @strSQL = @strSQL + ' ON  CDM.Shubetu   = 2950 ' +CHAR(13)
            SET @strSQL = @strSQL + ' AND CDM.CodeValue = SIS.SisanShubetuNO ' +CHAR(13)
            SET @strSQL = @strSQL + ' LEFT JOIN N0000MS_NA_Shoukyakuritu SKM ' +CHAR(13)
            SET @strSQL = @strSQL + ' ON  SKM.TaiyouNensuu = TIH.TaiyouNensuu ' +CHAR(13)
            SET @strSQL = @strSQL + ' LEFT JOIN N0000MS_NA_SisanZigyousho SZG ' +CHAR(13)
            SET @strSQL = @strSQL + ' ON  SZG.ZigyoushoCD = IDMX.IdousakiZigyoushoCD ' +CHAR(13)
            SET @strSQL = @strSQL + ' LEFT JOIN #N0000WK_Obic7fx_Win_N0000_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_Data WK ' +CHAR(13)
            SET @strSQL = @strSQL + ' ON  WK.SisanNO   = SIS.SisanNO ' +CHAR(13)
            SET @strSQL = @strSQL + ' AND WK.Edaban    = SIS.Edaban ' +CHAR(13)
--  2013/01/09 S.Murakami  (N00995)分割合併対応↓
            SET @strSQL = @strSQL + ' LEFT JOIN N0000RR_NA_BunkatuGappeiJouhou BGJ ' +CHAR(13)
            SET @strSQL = @strSQL + ' ON  BGJ.SisanNO = SIS.SisanNO ' +CHAR(13)
            SET @strSQL = @strSQL + ' AND  BGJ.Edaban = SIS.Edaban ' +CHAR(13)
            SET @strSQL = @strSQL + ' AND  BGJ.ShoukyakuKBN = 0 ' +CHAR(13)
            SET @strSQL = @strSQL + ' AND  BGJ.ZouGenKBN = 0 ' +CHAR(13)
--  2013/01/09 S.Murakami  (N00995)分割合併対応↑
            -- 引数.事業所セキュリティパターンコードが""でないとき、以下のJOIN句を追加
            IF (@p_ZigSecPtnCD IS NOT NULL)
            BEGIN
                SET @strSQL = @strSQL + ' INNER JOIN ' +CHAR(13)
                SET @strSQL = @strSQL + '     M0000MS_MA_ZigyoushoSecurityYouso MZSY ' +CHAR(13) -- 事業所セキュリティ要素マスタ
                SET @strSQL = @strSQL + ' ON  MZSY.ZigyoushoCDYouso   = IDMX.IdousakiZigyoushoCD ' +CHAR(13)
                SET @strSQL = @strSQL + ' AND MZSY.SecurityPatternCD  = ' + dbo.N0000FC_Escape_Varchar(@p_ZigSecPtnCD) +CHAR(13)
            END

--  2018/04/26 T.Maeda      (97532-97598)改元対応(元号マスタを結合)↓↓
            SET @strSQL = @strSQL + ' LEFT JOIN A0000MS_Gengou AS Gengou ' +CHAR(13)
            --資産の取得判定を、取得日もしくは償却開始日で行うかで元号マスタの結合条件を変更
            --取得日で判定を行う場合
            IF(@ShutokuDateHanteiKBN =0)
            BEGIN
            --元号開始日の範囲指定
            --合併区分が1：売買の時、分割合併日で考慮
                SET @strSQL = @strSQL + ' ON Gengou.GengouStartDate<= CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            THEN DATEADD(DAY,-1,BGJ.BunkatuGappeiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            ELSE BGJ.BunkatuGappeiDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  END ' +CHAR(13)
            --合併区分が0：簿価引継の時、取得日で考慮
                SET @strSQL = @strSQL + '                                  ELSE CASE WHEN MONTH(SIS.ShutokuDate) = 1 AND DAY(SIS.ShutokuDate) = 1  ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            THEN DATEADD(DAY,-1,SIS.ShutokuDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            ELSE SIS.ShutokuDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  END ' +CHAR(13)
                SET @strSQL = @strSQL + '                             END ' +CHAR(13)

            --元号終了日の範囲指定
            --合併区分が1：売買の時、分割合併日で考慮
                SET @strSQL = @strSQL + ' AND DATEADD(DAY,1,Gengou.GengouEndDate) > CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            THEN DATEADD(DAY,-1,BGJ.BunkatuGappeiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            ELSE BGJ.BunkatuGappeiDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  END ' +CHAR(13)
            --合併区分が0：簿価引継の時、取得日で考慮
                SET @strSQL = @strSQL + '                                  ELSE CASE WHEN MONTH(SIS.ShutokuDate) = 1 AND DAY(SIS.ShutokuDate) = 1  ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            THEN DATEADD(DAY,-1,SIS.ShutokuDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            ELSE SIS.ShutokuDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  END ' +CHAR(13)
                SET @strSQL = @strSQL + '                             END ' +CHAR(13)
            END

            --償却開始日で判定を行う場合
            ELSE
            BEGIN
            --合併区分が1：売買の時、分割合併日で考慮
                SET @strSQL = @strSQL + ' ON Gengou.GengouStartDate<= CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            THEN DATEADD(DAY,-1,BGJ.BunkatuGappeiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            ELSE BGJ.BunkatuGappeiDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  END ' +CHAR(13)
            --合併区分が0：簿価引継の時、償却開始日で考慮
                SET @strSQL = @strSQL + '                                  ELSE CASE WHEN MONTH(SIS.ShoukyakuKaisiDate) = 1 AND DAY(SIS.ShoukyakuKaisiDate) = 1  ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            THEN DATEADD(DAY,-1,SIS.ShoukyakuKaisiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            ELSE SIS.ShoukyakuKaisiDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  END ' +CHAR(13)
                SET @strSQL = @strSQL + '                             END ' +CHAR(13)

            --元号終了日の範囲指定
            --合併区分が1：売買の時、分割合併日で考慮
                SET @strSQL = @strSQL + ' AND DATEADD(DAY,1,Gengou.GengouEndDate) > CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            THEN DATEADD(DAY,-1,BGJ.BunkatuGappeiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            ELSE BGJ.BunkatuGappeiDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  END ' +CHAR(13)
            --合併区分が0：簿価引継の時、償却開始日で考慮
                SET @strSQL = @strSQL + '                                  ELSE CASE WHEN MONTH(SIS.ShoukyakuKaisiDate) = 1 AND DAY(SIS.ShoukyakuKaisiDate) = 1  ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            THEN DATEADD(DAY,-1,SIS.ShoukyakuKaisiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            ELSE SIS.ShoukyakuKaisiDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  END ' +CHAR(13)
                SET @strSQL = @strSQL + '                             END ' +CHAR(13)
            END
--  2018/04/26 T.Maeda      (97532-97598)改元対応(元号マスタを結合)↑↑

            --WHERE句
            SET @strSQL = @strSQL + ' WHERE 1 = 1 ' +CHAR(13)
            IF @p_SisanKBN <> 2
            BEGIN
                --仮資産フラグ
                SET @strSQL = @strSQL + ' AND SIS.KarisisanFlag = ' + CONVERT(varchar(1),@p_SisanKBN) +CHAR(13)
            END
 --2010/12/13 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 BEGIN↓
            IF @p_SisanKBN = 2
            BEGIN
                --仮資産フラグ
                SET @strSQL = @strSQL + ' AND (SIS.KarisisanFlag = 0 OR SIS.KarisisanFlag = 1) ' +CHAR(13)
            END
--2010/12/13 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 END↑
            --事由区分
            SET @strSQL = @strSQL + ' AND RIR.ZiyuuKBN = 3 ' +CHAR(13)
            --申告区分
            SET @strSQL = @strSQL + ' AND SIS.SinkokuKBN IN (0,2) ' +CHAR(13)
            --履歴日付
            SET @strSQL = @strSQL + ' AND RIR.RirekiDate >= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanBegin,111)) +CHAR(13)
            SET @strSQL = @strSQL + ' AND RIR.RirekiDate <= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanEnd,111)) +CHAR(13)
            --資産種別NO
            SET @strSQL = @strSQL + ' AND SIS.SisanShubetuNO BETWEEN 1 AND 6 ' +CHAR(13)
            --自治体CD
            IF @p_ZititaiCDBegin IS NOT NULL
            BEGIN
                SET @strSQL = @strSQL + ' AND IDMX.IdousakiZititaiCD >= ''' + @p_ZititaiCDBegin + '''' +CHAR(13)
            END                
            IF @p_ZititaiCDEnd IS NOT NULL
            BEGIN
                SET @strSQL = @strSQL + ' AND IDMX.IdousakiZititaiCD <= ''' + @p_ZititaiCDEnd + '''' +CHAR(13)
            END
            --事業所CD
            IF @p_ShuturyokuKBN = 1
            BEGIN
                IF @p_ZigyoushoCDBegin IS NOT NULL
                BEGIN
                    SET @strSQL = @strSQL + ' AND IDMX.IdousakiZigyoushoCD >= ''' + @p_ZigyoushoCDBegin + '''' +CHAR(13)
                END
                IF @p_ZigyoushoCDEnd IS NOT NULL
                BEGIN
                    SET @strSQL = @strSQL + ' AND IDMX.IdousakiZigyoushoCD <= ''' + @p_ZigyoushoCDEnd + '''' +CHAR(13)
                END
                SET @strSQL = @strSQL + ' AND (IDMN.IdoumotoZititaiCD   <> IDMX.IdousakiZititaiCD ' +CHAR(13)
                SET @strSQL = @strSQL + ' OR   IDMN.IdoumotoZigyoushoCD <> IDMX.IdousakiZigyoushoCD) ' +CHAR(13)
            END
            ELSE
            BEGIN
                SET @strSQL = @strSQL + ' AND IDMN.IdoumotoZititaiCD   <> IDMX.IdousakiZititaiCD ' +CHAR(13)
            END
            SET @strSQL = @strSQL + ' AND NOT EXISTS ( ' +CHAR(13)
            SET @strSQL = @strSQL + '         SELECT * FROM N0000RR_NA_RirekiJouhou RIR ' +CHAR(13)
            SET @strSQL = @strSQL + '         WHERE RIR.SisanNO     = SIS.SisanNO ' +CHAR(13)
            SET @strSQL = @strSQL + '         AND   RIR.Edaban      = SIS.Edaban ' +CHAR(13)
            SET @strSQL = @strSQL + '         AND   RIR.RirekiDate <= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanEnd,111)) + '' +CHAR(13)
--2010/12/13 M.Shinbo 変更 (N00697)IFRS・ダブルスタンダード対応 BEGIN↓
--            SET @strSQL = @strSQL + '         AND   RIR.ZiyuuKBN    = 2 ) ' +CHAR(13)
            --  2013/01/09 S.Murakami  (N00995)分割合併対応↓
            --SET @strSQL = @strSQL + '         AND   RIR.ZiyuuKBN    = 2 ' +CHAR(13)
            SET @strSQL = @strSQL + '         AND   RIR.ZiyuuKBN    IN(2,20) ' +CHAR(13)
            --  2013/01/09 S.Murakami  (N00995)分割合併対応↑
            SET @strSQL = @strSQL + '         AND   RIR.ShoukyakuKBN = 0 ) ' +CHAR(13)
--2010/12/13 M.Shinbo 変更 (N00697)IFRS・ダブルスタンダード対応 END↑
            SET @strSQL = @strSQL + ' AND WK.SisanNO IS NULL ' +CHAR(13)

            EXEC (@strSQL)

            SET @ErrorCode = @@ERROR
            IF @ErrorCode <> 0 GOTO ERR_SHORI

            --2-1-2.資産情報ワークの自治体・事業所を、申告時点の自治体・事業所へ変更
            UPDATE WK
            SET WK.ZititaiCD   = IDO.IdoumotoZititaiCD
               ,WK.ZigyoushoCD = IDO.IdoumotoZigyoushoCD
               ,WK.CompanyName = ZIG.CompanyName
            FROM #N0000WK_Obic7fx_Win_N0000_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_Data WK
            INNER JOIN (SELECT   IDM.SisanNO
                                ,IDM.Edaban
                                ,IDM.IdoumotoZititaiCD
                                ,IDM.IdoumotoZigyoushoCD
                        FROM N0000RR_NA_IdouJouhou IDM
                        INNER JOIN(SELECT SisanNO
                                         ,Edaban
                                         ,MIN(IdouDate) AS IdouDate
                                   FROM N0000RR_NA_IdouJouhou
                                   WHERE IdouDate > @SinkokuKikanEnd
--2011/01/19 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 戻り対応 BEGIN↓
                                   AND   ShoukyakuKBN = 0
--2011/01/19 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 戻り対応 END↑
                                   GROUP BY SisanNO ,Edaban
                                  ) IDS
                        ON  IDS.SisanNO  = IDM.SisanNO
                        AND IDS.Edaban   = IDM.Edaban
                        AND IDS.IdouDate = IDM.IdouDate
--2010/12/13 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 BEGIN↓
                        AND IDM.ShoukyakuKBN = 0
--2010/12/13 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 END↑
                        
                       ) IDO
            ON  IDO.SisanNO   = WK.SisanNO
            AND IDO.Edaban    = WK.Edaban
            LEFT JOIN N0000MS_NA_SisanZigyousho ZIG
            ON  ZIG.ZigyoushoCD = IDO.IdoumotoZigyoushoCD
            AND NOT (WK.ZigyoushoCD = '*' AND WK.CompanyName IS NULL)

            SET @ErrorCode = @@ERROR
            IF @ErrorCode <> 0 GOTO ERR_SHORI

        END --IF

        --2-2.画面で全資産申告を選択した時
        IF @p_SinkokuKBN = 0
        BEGIN
            --2-2-1.対象となる資産情報を退避
            INSERT INTO #N0000WK_Obic7fx_Win_N0000_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_SisanJouhou (
                     SisanNO
                    ,Edaban
                    ,ZititaiCD
                    ,ZigyoushoCD
                    ,ZiyuuKBN
                    ,SisanName
                    ,Suuryou
                    ,ShutokuDate
                    ,ShutokuKagaku
                    ,ShutokuKBN
                    ,SinkokuKBN
                    ,ShoukyakuKaisiDate
                    ,SisanShubetuNO
                    )
            SELECT
                     SIS.SisanNO
                    ,SIS.Edaban
                    ,SIS.ZititaiCD
                    ,SIS.ZigyoushoCD
                    ,0
                    ,SIS.SisanName
                    ,SIS.Suuryou
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↓
                    --,SIS.ShutokuDate
                    ,CASE @ShutokuDateHanteiKBN WHEN 0 THEN SIS.ShutokuDate ELSE SIS.ShoukyakuKaisiDate END
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↑
--↓2009/07/13 Y.Ishizawa 全資産申告の場合、取得価額を地方税取得価額から転送に修正
--                     ,SIS.ShutokuKagaku
                    ,SIS.TihouzeiShutokuKagaku
--↑2009/07/13 Y.Ishizawa 全資産申告の場合、取得価額を地方税取得価額から転送に修正
                    ,SIS.ShutokuKBN
                    ,SIS.SinkokuKBN
                    ,SIS.ShoukyakuKaisiDate
                    ,SIS.SisanShubetuNO
            FROM N0000RR_NA_SisanJouhou SIS
--  2013/01/09 S.Murakami  (N00995)分割合併対応↓
            LEFT JOIN N0000RR_NA_BunkatuGappeiJouhou BGJ
            ON  BGJ.SisanNO = SIS.SisanNO
            AND BGJ.Edaban = SIS.Edaban
            AND BGJ.ShoukyakuKBN = 0
            AND BGJ.ZouGenKBN = 0
--  2013/01/09 S.Murakami  (N00995)分割合併対応↑
--2010/12/13 M.Shinbo 変更 (N00697)IFRS・ダブルスタンダード対応 BEGIN↓            
--            WHERE (@p_SisanKBN = 2 OR SIS.KarisisanFlag = @p_SisanKBN)
            WHERE ((@p_SisanKBN <> 2 AND SIS.KarisisanFlag = @p_SisanKBN)
            OR     (@p_SisanKBN = 2 AND (SIS.KarisisanFlag = 0 OR SIS.KarisisanFlag = 1)))
--2010/12/13 M.Shinbo 変更 (N00697)IFRS・ダブルスタンダード対応 END↑            
            AND   SIS.SinkokuKBN IN (0,2)
--  2013/01/09 S.Murakami  (N00995)分割合併対応↓
            --AND   SIS.ShutokuDate <= @SinkokuKikanEnd
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↓
            --AND   ISNULL(BGJ.BunkatuGappeiDate, SIS.ShutokuDate) <= @SinkokuKikanEnd
            AND   ISNULL(BGJ.BunkatuGappeiDate, CASE @ShutokuDateHanteiKBN WHEN 0 THEN SIS.ShutokuDate ELSE SIS.ShoukyakuKaisiDate END) <= @SinkokuKikanEnd
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↑
--  2013/01/09 S.Murakami  (N00995)分割合併対応↑
            AND   SIS.SisanShubetuNO BETWEEN 1 AND 6
            AND NOT EXISTS (
                    SELECT * FROM N0000RR_NA_RirekiJouhou RIR
                    WHERE RIR.SisanNO     = SIS.SisanNO
                    AND   RIR.Edaban      = SIS.Edaban
                    AND   RIR.RirekiDate <= @SinkokuKikanEnd
--2010/12/13 M.Shinbo 変更 (N00697)IFRS・ダブルスタンダード対応 BEGIN↓
--                    AND   RIR.ZiyuuKBN    = 2)
                    --  2013/01/09 S.Murakami  (N00995)分割合併対応↓
                    --AND   RIR.ZiyuuKBN    = 2
                    AND   RIR.ZiyuuKBN    IN(2,20)
                    --  2013/01/09 S.Murakami  (N00995)分割合併対応↑
                    AND   RIR.ShoukyakuKBN = 0)
--2010/12/13 M.Shinbo 変更 (N00697)IFRS・ダブルスタンダード対応 END↑
            ORDER BY SIS.ZigyoushoCD ,SIS.ZititaiCD ,SIS.SisanNO ,SIS.Edaban

            SET @ErrorCode = @@ERROR
            IF @ErrorCode <> 0 GOTO ERR_SHORI
            
--  2013/01/09 S.Murakami  (N00995)分割合併対応↓
            --2-2-2.資産情報ワークの事由区分を更新
            UPDATE WK
            SET WK.ZiyuuKBN  = 4
            FROM #N0000WK_Obic7fx_Win_N0000_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_SisanJouhou WK
            INNER JOIN N0000RR_NA_BunkatuGappeiJouhou BGJ
            ON  BGJ.SisanNO      = WK.SisanNO
            AND BGJ.Edaban       = WK.Edaban
            AND BGJ.ShoukyakuKBN = 0
            AND BGJ.ZouGenKBN    = 0
            INNER JOIN N0000RR_NA_SisanJouhou SIS
            ON  SIS.SisanNO   = WK.SisanNO
            AND SIS.Edaban    = WK.Edaban
            WHERE BGJ.BunkatuGappeiDate <= @SinkokuKikanEnd
            AND   BGJ.BunkatuGappeiDate >= @SinkokuKikanBegin
            
            SET @ErrorCode = @@ERROR
            IF @ErrorCode <> 0 GOTO ERR_SHORI
--  2013/01/09 S.Murakami  (N00995)分割合併対応↑

            --2-2-2.資産情報ワークの自治体・事業所を、申告時点の自治体・事業所へ変更
            UPDATE WK
            SET WK.ZititaiCD   = IDO.IdoumotoZititaiCD
               ,WK.ZigyoushoCD = IDO.IdoumotoZigyoushoCD
            FROM #N0000WK_Obic7fx_Win_N0000_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_SisanJouhou WK
            INNER JOIN (SELECT   IDM.SisanNO
                                ,IDM.Edaban
                                ,IDM.IdoumotoZititaiCD
                                ,IDM.IdoumotoZigyoushoCD
                        FROM N0000RR_NA_IdouJouhou IDM
                        INNER JOIN(SELECT SisanNO
                                         ,Edaban
                                         ,MIN(IdouDate) AS IdouDate
                                   FROM N0000RR_NA_IdouJouhou
                                   WHERE IdouDate > @SinkokuKikanEnd
--2011/01/19 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 戻り対応 BEGIN↓
                                   AND   ShoukyakuKBN = 0
--2011/01/19 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 戻り対応 END↑
                                   GROUP BY SisanNO ,Edaban
                                  ) IDS
                        ON  IDS.SisanNO  = IDM.SisanNO
                        AND IDS.Edaban   = IDM.Edaban
                        AND IDS.IdouDate = IDM.IdouDate
--2010/12/13 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 BEGIN↓
                        AND IDM.ShoukyakuKBN = 0
--2010/12/13 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 END↑                        
                       ) IDO
            ON  IDO.SisanNO   = WK.SisanNO
            AND IDO.Edaban    = WK.Edaban

            SET @ErrorCode = @@ERROR
            IF @ErrorCode <> 0 GOTO ERR_SHORI

            --2-2-3.資産情報ワークの事由区分を更新
            UPDATE WK
            SET WK.ZiyuuKBN  = 3
            FROM #N0000WK_Obic7fx_Win_N0000_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_SisanJouhou WK
            INNER JOIN (SELECT SisanNO
                              ,Edaban
                              ,MIN(IdouDate) AS MinIdouDate
                              ,MAX(IdouDate) AS MaxIdouDate
                        FROM N0000RR_NA_IdouJouhou
                        WHERE IdouDate >= @SinkokuKikanBegin
                        AND   IdouDate <= @SinkokuKikanEnd
--2011/01/19 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 戻り対応 BEGIN↓
                        AND   ShoukyakuKBN = 0
--2011/01/19 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 戻り対応 END↑
                        GROUP BY SisanNO ,Edaban
                       ) IDO
            ON  IDO.SisanNO   = WK.SisanNO
            AND IDO.Edaban    = WK.Edaban
            INNER JOIN N0000RR_NA_IdouJouhou IDMN
            ON  IDMN.SisanNO  = IDO.SisanNO
            AND IDMN.Edaban   = IDO.Edaban
            AND IDMN.IdouDate = IDO.MinIdouDate
--2010/12/13 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 BEGIN↓
            AND IDMN.ShoukyakuKBN = 0
--2010/12/13 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 END↑
            INNER JOIN N0000RR_NA_IdouJouhou IDMX
            ON  IDMX.SisanNO  = IDO.SisanNO
            AND IDMX.Edaban   = IDO.Edaban
            AND IDMX.IdouDate = IDO.MaxIdouDate
--2010/12/13 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 BEGIN↓
            AND IDMX.ShoukyakuKBN = 0
--2010/12/13 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 END↑
            INNER JOIN N0000RR_NA_SisanJouhou SIS
            ON  SIS.SisanNO   = WK.SisanNO
            AND SIS.Edaban    = WK.Edaban
--  2013/01/09 S.Murakami  (N00995)分割合併対応↓
            LEFT JOIN N0000RR_NA_BunkatuGappeiJouhou BGJ
            ON  BGJ.SisanNO      = SIS.SisanNO
            AND BGJ.Edaban       = SIS.Edaban
            AND BGJ.ShoukyakuKBN = 0
            AND BGJ.ZouGenKBN    = 0
            --WHERE SIS.ShutokuDate         < @SinkokuKikanBegin
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↓
            --WHERE ISNULL(BGJ.BunkatuGappeiDate,SIS.ShutokuDate) < @SinkokuKikanBegin
            WHERE ISNULL(BGJ.BunkatuGappeiDate,CASE @ShutokuDateHanteiKBN WHEN 0 THEN SIS.ShutokuDate ELSE SIS.ShoukyakuKaisiDate END) < @SinkokuKikanBegin
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↑
--  2013/01/09 S.Murakami  (N00995)分割合併対応↑
            AND   IDMN.IdoumotoZititaiCD <> IDMX.IdousakiZititaiCD

            SET @ErrorCode = @@ERROR
            IF @ErrorCode <> 0 GOTO ERR_SHORI

            --2-2-4.資産情報ワークを元にデータを抽出
            SET @strSQL = ''
            --INSERT句
            SET @strSQL = @strSQL + ' INSERT INTO #N0000WK_Obic7fx_Win_N0000_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_Data ( ' +CHAR(13)
            SET @strSQL = @strSQL + '          RirekiSystemNO ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,SisanNO ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,Edaban ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,SisanName ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,ZititaiCD ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,SisanShubetuNO ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,Suuryou ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,ShutokuDate ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,Nengou ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,ShutokuNen ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,ShutokuTuki ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,ShutokuKagaku ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,TaiyouNensuu ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,ZiyuuKBN ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,RirekiDate ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,Kagaku ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,KazeiKagaku ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,Genkazanzonritu ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,Tokureiritu ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,TokureirituNumerator ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,TokureirituDenominator ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,ShutokuKBN ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,SinkokuKBN ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,ShoukyakuKaisiDate ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,TokureiKikan ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,ZigyoushoCD ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,CompanyName ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,Bikou ' +CHAR(13)
            SET @strSQL = @strSQL + '         ) ' +CHAR(13)
            --SELECT句
            SET @strSQL = @strSQL + ' SELECT  ' +CHAR(13)
            SET @strSQL = @strSQL + '          [ID] ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,WK.SisanNO ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,WK.Edaban ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,WK.SisanName ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,WK.ZititaiCD ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,WK.SisanShubetuNO ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,WK.Suuryou ' +CHAR(13)
--  2013/01/09 S.Murakami  (N00995)分割合併対応↓
            --SET @strSQL = @strSQL + '         ,CASE WHEN MONTH(WK.ShutokuDate) = 1 AND DAY(WK.ShutokuDate) = 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '               THEN DATEADD(day,-1,WK.ShutokuDate) ' +CHAR(13)
            --SET @strSQL = @strSQL + '               ELSE WK.ShutokuDate ' +CHAR(13)
            --SET @strSQL = @strSQL + '          END AS ShutokuDate ' +CHAR(13)
            --SET @strSQL = @strSQL + '         ,CASE WHEN MONTH(WK.ShutokuDate) = 1 AND DAY(WK.ShutokuDate) = 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '               THEN CASE WHEN YEAR(DATEADD(day,-1,WK.ShutokuDate)) >= 1989 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN 4 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(DATEADD(day,-1,WK.ShutokuDate)) >= 1926 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN 3 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(DATEADD(day,-1,WK.ShutokuDate)) >= 1912 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN 2 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(DATEADD(day,-1,WK.ShutokuDate)) >= 1867 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                    END ' +CHAR(13)
            --SET @strSQL = @strSQL + '               ELSE CASE WHEN YEAR(WK.ShutokuDate) >= 1989 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN 4 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(WK.ShutokuDate) >= 1926 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN 3 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(WK.ShutokuDate) >= 1912 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN 2 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(WK.ShutokuDate) >= 1867 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                    END ' +CHAR(13)
            --SET @strSQL = @strSQL + '          END AS Nengou ' +CHAR(13)
            --SET @strSQL = @strSQL + '         ,CASE WHEN MONTH(WK.ShutokuDate) = 1 AND DAY(WK.ShutokuDate) = 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '               THEN CASE WHEN YEAR(DATEADD(day,-1,WK.ShutokuDate)) >= 1989 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN YEAR(DATEADD(day,-1,WK.ShutokuDate)) - 1989 + 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(DATEADD(day,-1,WK.ShutokuDate)) >= 1926 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN YEAR(DATEADD(day,-1,WK.ShutokuDate)) - 1926 + 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(DATEADD(day,-1,WK.ShutokuDate)) >= 1912 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN YEAR(DATEADD(day,-1,WK.ShutokuDate)) - 1912 + 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(DATEADD(day,-1,WK.ShutokuDate)) >= 1867 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN YEAR(DATEADD(day,-1,WK.ShutokuDate)) - 1867 + 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                    END ' +CHAR(13)
            --SET @strSQL = @strSQL + '               ELSE CASE WHEN YEAR(WK.ShutokuDate) >= 1989 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN YEAR(WK.ShutokuDate) - 1989 + 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(WK.ShutokuDate) >= 1926 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN YEAR(WK.ShutokuDate) - 1926 + 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(WK.ShutokuDate) >= 1912 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN YEAR(WK.ShutokuDate) - 1912 + 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         WHEN YEAR(WK.ShutokuDate) >= 1867 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                         THEN YEAR(WK.ShutokuDate) - 1867 + 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                    END ' +CHAR(13)
            --SET @strSQL = @strSQL + '          END AS ShutokuNen ' +CHAR(13)
            --SET @strSQL = @strSQL + '         ,CASE WHEN MONTH(WK.ShutokuDate) = 1 AND DAY(WK.ShutokuDate) = 1 ' +CHAR(13)
            --SET @strSQL = @strSQL + '               THEN MONTH(DATEADD(day,-1,WK.ShutokuDate)) ' +CHAR(13)
            --SET @strSQL = @strSQL + '               ELSE MONTH(WK.ShutokuDate) ' +CHAR(13)
            --SET @strSQL = @strSQL + '          END AS ShutokuTuki ' +CHAR(13)
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↓
            IF (@ShutokuDateHanteiKBN = 0)
            BEGIN
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↑
                SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '               THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN DATEADD(day,-1,BGJ.BunkatuGappeiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE BGJ.BunkatuGappeiDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '               ELSE CASE WHEN MONTH(WK.ShutokuDate) = 1 AND DAY(WK.ShutokuDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN DATEADD(day,-1,WK.ShutokuDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE WK.ShutokuDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '          END AS ShutokuDate ' +CHAR(13)
--2018/04/26 T.Maeda      (97532-97598)改元対応(年号・取得年の抽出方法を変更)↓↓
                --SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '               THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         THEN CASE WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 4 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 3 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 2 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         ELSE CASE WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 4 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 3 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 2 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                --SET @strSQL = @strSQL + '               ELSE CASE WHEN MONTH(WK.ShutokuDate) = 1 AND DAY(WK.ShutokuDate) = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         THEN CASE WHEN YEAR(DATEADD(day,-1,WK.ShutokuDate)) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 4 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,WK.ShutokuDate)) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 3 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,WK.ShutokuDate)) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 2 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,WK.ShutokuDate)) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         ELSE CASE WHEN YEAR(WK.ShutokuDate) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 4 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(WK.ShutokuDate) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 3 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(WK.ShutokuDate) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 2 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(WK.ShutokuDate) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                --SET @strSQL = @strSQL + '          END AS Nengou ' +CHAR(13)
                SET @strSQL = @strSQL + '          ,Gengou.GengouKBN AS Nengou ' +CHAR(13)
                --SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '               THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         THEN CASE WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - 1989 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - 1926 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - 1912 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - 1867 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         ELSE CASE WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(BGJ.BunkatuGappeiDate) - 1989 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(BGJ.BunkatuGappeiDate) - 1926 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(BGJ.BunkatuGappeiDate) - 1912 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(BGJ.BunkatuGappeiDate) - 1867 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                --SET @strSQL = @strSQL + '               ELSE CASE WHEN MONTH(WK.ShutokuDate) = 1 AND DAY(WK.ShutokuDate) = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         THEN CASE WHEN YEAR(DATEADD(day,-1,WK.ShutokuDate)) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,WK.ShutokuDate)) - 1989 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,WK.ShutokuDate)) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,WK.ShutokuDate)) - 1926 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,WK.ShutokuDate)) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,WK.ShutokuDate)) - 1912 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,WK.ShutokuDate)) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,WK.ShutokuDate)) - 1867 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         ELSE CASE WHEN YEAR(WK.ShutokuDate) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(WK.ShutokuDate) - 1989 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(WK.ShutokuDate) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(WK.ShutokuDate) - 1926 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(WK.ShutokuDate) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(WK.ShutokuDate) - 1912 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(WK.ShutokuDate) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(WK.ShutokuDate) - 1867 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '               THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1  ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - YEAR(Gengou.GengouStartDate) + 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE YEAR(BGJ.BunkatuGappeiDate) - YEAR(Gengou.GengouStartDate) + 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '               ELSE CASE WHEN MONTH(WK.ShutokuDate) = 1 AND DAY(WK.ShutokuDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN YEAR(DATEADD(day,-1,WK.ShutokuDate)) - YEAR(Gengou.GengouStartDate) + 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE YEAR(WK.ShutokuDate) - YEAR(Gengou.GengouStartDate) + 1 ' +CHAR(13)
--2018/04/26 T.Maeda      (97532-97598)改元対応(年号・取得年の抽出方法を変更)↑↑
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '          END AS ShutokuNen ' +CHAR(13)
                SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)    
                SET @strSQL = @strSQL + '               THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN MONTH(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE MONTH(BGJ.BunkatuGappeiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '               ELSE CASE WHEN MONTH(WK.ShutokuDate) = 1 AND DAY(WK.ShutokuDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN MONTH(DATEADD(day,-1,WK.ShutokuDate)) ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE MONTH(WK.ShutokuDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '          END AS ShutokuTuki ' +CHAR(13)
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↓
            END
            ELSE
            BEGIN
                SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '               THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN DATEADD(day,-1,BGJ.BunkatuGappeiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE BGJ.BunkatuGappeiDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '               ELSE CASE WHEN MONTH(WK.ShoukyakuKaisiDate) = 1 AND DAY(WK.ShoukyakuKaisiDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN DATEADD(day,-1,WK.ShoukyakuKaisiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE WK.ShoukyakuKaisiDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '          END AS ShoukyakuKaisiDate ' +CHAR(13)
--2018/04/26 T.Maeda      (97532-97598)改元対応(年号・取得年の抽出方法を変更)↓↓
                --SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '               THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         THEN CASE WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 4 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 3 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 2 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         ELSE CASE WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 4 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 3 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 2 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                --SET @strSQL = @strSQL + '               ELSE CASE WHEN MONTH(WK.ShoukyakuKaisiDate) = 1 AND DAY(WK.ShoukyakuKaisiDate) = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         THEN CASE WHEN YEAR(DATEADD(day,-1,WK.ShoukyakuKaisiDate)) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 4 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,WK.ShoukyakuKaisiDate)) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 3 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,WK.ShoukyakuKaisiDate)) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 2 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,WK.ShoukyakuKaisiDate)) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         ELSE CASE WHEN YEAR(WK.ShoukyakuKaisiDate) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 4 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(WK.ShoukyakuKaisiDate) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 3 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(WK.ShoukyakuKaisiDate) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 2 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(WK.ShoukyakuKaisiDate) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                --SET @strSQL = @strSQL + '          END AS Nengou ' +CHAR(13)
                SET @strSQL = @strSQL + '          ,Gengou.GengouKBN AS Nengou ' +CHAR(13)
                --SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '               THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         THEN CASE WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - 1989 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - 1926 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - 1912 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - 1867 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         ELSE CASE WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(BGJ.BunkatuGappeiDate) - 1989 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(BGJ.BunkatuGappeiDate) - 1926 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(BGJ.BunkatuGappeiDate) - 1912 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(BGJ.BunkatuGappeiDate) - 1867 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                --SET @strSQL = @strSQL + '               ELSE CASE WHEN MONTH(WK.ShoukyakuKaisiDate) = 1 AND DAY(WK.ShoukyakuKaisiDate) = 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         THEN CASE WHEN YEAR(DATEADD(day,-1,WK.ShoukyakuKaisiDate)) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,WK.ShoukyakuKaisiDate)) - 1989 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,WK.ShoukyakuKaisiDate)) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,WK.ShoukyakuKaisiDate)) - 1926 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,WK.ShoukyakuKaisiDate)) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,WK.ShoukyakuKaisiDate)) - 1912 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,WK.ShoukyakuKaisiDate)) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(DATEADD(day,-1,WK.ShoukyakuKaisiDate)) - 1867 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                --SET @strSQL = @strSQL + '                         ELSE CASE WHEN YEAR(WK.ShoukyakuKaisiDate) >= 1989 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(WK.ShoukyakuKaisiDate) - 1989 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(WK.ShoukyakuKaisiDate) >= 1926 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(WK.ShoukyakuKaisiDate) - 1926 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(WK.ShoukyakuKaisiDate) >= 1912 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(WK.ShoukyakuKaisiDate) - 1912 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   WHEN YEAR(WK.ShoukyakuKaisiDate) >= 1867 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                                   THEN YEAR(WK.ShoukyakuKaisiDate) - 1867 + 1 ' +CHAR(13)
                --SET @strSQL = @strSQL + '                              END ' +CHAR(13)
                SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '               THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1  ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - YEAR(Gengou.GengouStartDate) + 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE YEAR(BGJ.BunkatuGappeiDate) - YEAR(Gengou.GengouStartDate) + 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '               ELSE CASE WHEN MONTH(WK.ShoukyakuKaisiDate) = 1 AND DAY(WK.ShoukyakuKaisiDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN YEAR(DATEADD(day,-1,WK.ShoukyakuKaisiDate)) - YEAR(Gengou.GengouStartDate) + 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE YEAR(WK.ShoukyakuKaisiDate) - YEAR(Gengou.GengouStartDate) + 1 ' +CHAR(13)
--2018/04/26 T.Maeda      (97532-97598)改元対応(年号・取得年の抽出方法を変更)↑↑
                SET @strSQL = @strSQL + '               END ' +CHAR(13)
                SET @strSQL = @strSQL + '          END AS ShutokuNen ' +CHAR(13)
                SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)    
                SET @strSQL = @strSQL + '               THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN MONTH(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE MONTH(BGJ.BunkatuGappeiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '               ELSE CASE WHEN MONTH(WK.ShoukyakuKaisiDate) = 1 AND DAY(WK.ShoukyakuKaisiDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN MONTH(DATEADD(day,-1,WK.ShoukyakuKaisiDate)) ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE MONTH(WK.ShoukyakuKaisiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '          END AS ShutokuTuki ' +CHAR(13)
            END
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↑                
--  2013/01/09 S.Murakami  (N00995)分割合併対応↑
            SET @strSQL = @strSQL + '         ,WK.ShutokuKagaku ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,TIH.TaiyouNensuu ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,WK.ZiyuuKBN ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,NULL ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,TIH.HonnendoHyoukagaku ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,TIH.HonnendoHyoukagaku ' +CHAR(13)
            --  2013/01/09 S.Murakami  (N00995)分割合併対応↓
            --SET @strSQL = @strSQL + '         ,CASE WHEN WK.ShutokuDate >= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanBegin,111)) +  ' AND WK.ShutokuDate <= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanEnd,111)) +  ' ' +CHAR(13)
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↓
            IF (@ShutokuDateHanteiKBN = 0)
            BEGIN
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↑
                SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '               THEN CASE WHEN BGJ.BunkatuGappeiDate >= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanBegin,111)) +  ' AND BGJ.BunkatuGappeiDate <= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanEnd,111)) +  ' ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN SKM.ZennenChuuGenkazanzonritu ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE SKM.ZennenzenGenkazanzonritu ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '               ELSE CASE WHEN WK.ShutokuDate >= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanBegin,111)) +  ' AND WK.ShutokuDate <= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanEnd,111)) +  ' ' +CHAR(13)
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↓
            END
            ELSE
            BEGIN
                SET @strSQL = @strSQL + '         ,CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '               THEN CASE WHEN BGJ.BunkatuGappeiDate >= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanBegin,111)) +  ' AND BGJ.BunkatuGappeiDate <= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanEnd,111)) +  ' ' +CHAR(13)
                SET @strSQL = @strSQL + '                         THEN SKM.ZennenChuuGenkazanzonritu ' +CHAR(13)
                SET @strSQL = @strSQL + '                         ELSE SKM.ZennenzenGenkazanzonritu ' +CHAR(13)
                SET @strSQL = @strSQL + '                    END ' +CHAR(13)
                SET @strSQL = @strSQL + '               ELSE CASE WHEN WK.ShoukyakuKaisiDate >= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanBegin,111)) +  ' AND WK.ShoukyakuKaisiDate <= ' + dbo.N0000FC_Escape_Varchar(CONVERT(varchar(10),@SinkokuKikanEnd,111)) +  ' ' +CHAR(13)
            END
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↑
            --  2013/01/09 S.Murakami  (N00995)分割合併対応↑
            SET @strSQL = @strSQL + '                         THEN SKM.ZennenChuuGenkazanzonritu ' +CHAR(13)
            SET @strSQL = @strSQL + '                         ELSE SKM.ZennenzenGenkazanzonritu ' +CHAR(13)
            --  2013/01/09 S.Murakami  (N00995)分割合併対応↓
            SET @strSQL = @strSQL + '                    END ' +CHAR(13)
            --  2013/01/09 S.Murakami  (N00995)分割合併対応↑
            SET @strSQL = @strSQL + '          END AS Genkazanzonritu ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,NULL AS TokureiRitu' +CHAR(13)
            SET @strSQL = @strSQL + '         ,TIH.TokureirituNumerator ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,TIH.TokureirituDenominator ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,WK.ShutokuKBN ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,WK.SinkokuKBN ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,WK.ShoukyakuKaisiDate AS ShoukyakuDate ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,TIH.TokureiKikan ' +CHAR(13)
            IF @p_ShuturyokuKBN = 1
            BEGIN
                SET @strSQL = @strSQL + '         ,WK.ZigyoushoCD ' +CHAR(13)
                SET @strSQL = @strSQL + '         ,SZG.CompanyName AS CompanyName ' +CHAR(13)
            END
            ELSE
            BEGIN
                SET @strSQL = @strSQL + '         ,''*'' AS ZigyoushoCD ' +CHAR(13)
                SET @strSQL = @strSQL + '         ,NULL AS CompanyName ' +CHAR(13)
            END
--  2013/01/09 S.Murakami  (N00995)分割合併対応↓
            --SET @strSQL = @strSQL + '         ,THJ.Bikou ' +CHAR(13)
            SET @strSQL = @strSQL + '         ,CASE WHEN THJ.Bikou IS NOT NULL ' +CHAR(13)
            SET @strSQL = @strSQL + '               THEN THJ.Bikou ' +CHAR(13)
            SET @strSQL = @strSQL + '               ELSE CASE WHEN BGJ.BunkatuGappeiDate IS NOT NULL ' +CHAR(13)
--2013/03/15 E.Sato ST (N00995)分割合併対応の追加修正
            SET @strSQL = @strSQL + '                              AND BGJ.BunkatuGappeiDate BETWEEN ''' + CONVERT(varchar, @SinkokuKikanBegin, 111) + ''' AND ''' + CONVERT(varchar, @SinkokuKikanEnd, 111) + '''  ' +CHAR(13)
--2013/03/15 E.Sato ED
--  2018/04/26 T.Maeda      (97532-97598)改元対応(備考欄の取得)↓↓
            --SET @strSQL = @strSQL + '                         THEN CASE WHEN YEAR(BGJ.BunkatuGappeiDate) >= 1989 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                                   THEN ''H'' + CAST(YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - 1989 + 1 AS varchar(5)) + ''.'' + CAST(MONTH(BGJ.BunkatuGappeiDate) AS varchar(5)) ' +CHAR(13)
            --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1926 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                                   THEN ''S'' + CAST(YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - 1926 + 1 AS varchar(5)) + ''.'' + CAST(MONTH(BGJ.BunkatuGappeiDate) AS varchar(5)) ' +CHAR(13)
            --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1912 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                                   THEN ''T'' + CAST(YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - 1912 + 1 AS varchar(5)) + ''.'' + CAST(MONTH(BGJ.BunkatuGappeiDate) AS varchar(5)) ' +CHAR(13)
            --SET @strSQL = @strSQL + '                                   WHEN YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) >= 1867 ' +CHAR(13)
            --SET @strSQL = @strSQL + '                                   THEN ''M'' + CAST(YEAR(DATEADD(day,-1,BGJ.BunkatuGappeiDate)) - 1867 + 1 AS varchar(5)) + ''.'' + CAST(MONTH(BGJ.BunkatuGappeiDate) AS varchar(5)) ' +CHAR(13)
            --SET @strSQL = @strSQL + '                              END +  ''合併による'' ' +CHAR(13)
            SET @strSQL = @strSQL + '                         THEN GengouByBunkatuGappeiDate.GengouKigou + CAST(YEAR(BGJ.BunkatuGappeiDate) - YEAR(GengouByBunkatuGappeiDate.GengouStartDate) + 1  AS varchar(5)) + ''.'' + CAST(MONTH(BGJ.BunkatuGappeiDate) AS varchar(5))' +CHAR(13)
--  2021 06/03 Y.Matuzawa (183279) 会社分割によって増加した資産の備考が「合併による」と表示される 対応 ↓
            --SET @strSQL = @strSQL + '                 +  ''合併による'' ' +CHAR(13)
            SET @strSQL = @strSQL + '                 +  ''分割合併による'' ' +CHAR(13)
--  2021 06/03 Y.Matuzawa (183279) 会社分割によって増加した資産の備考が「合併による」と表示される 対応 ↑
--  2018/04/26 T.Maeda      (97532-97598)改元対応(備考欄の取得)↑↑
            SET @strSQL = @strSQL + '                         ELSE NULL ' +CHAR(13)
            SET @strSQL = @strSQL + '                    END ' +CHAR(13)
            SET @strSQL = @strSQL + '          END AS Bikou ' +CHAR(13)
--  2013/01/09 S.Murakami  (N00995)分割合併対応↑
            --FROM句
            SET @strSQL = @strSQL + ' FROM #N0000WK_Obic7fx_Win_N0000_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_SisanJouhou WK ' +CHAR(13)
            SET @strSQL = @strSQL + ' INNER JOIN N0000RR_NA_TihouzeiJouhou TIH ' +CHAR(13)
            SET @strSQL = @strSQL + ' ON  TIH.SisanNO            = WK.SisanNO ' +CHAR(13)
            SET @strSQL = @strSQL + ' AND TIH.Edaban             = WK.Edaban ' +CHAR(13)
            SET @strSQL = @strSQL + ' AND YEAR(TIH.SinkokuNendo) = ' + CONVERT(varchar(4),YEAR(@p_SinkokuNendo)) +CHAR(13)
            SET @strSQL = @strSQL + ' LEFT JOIN N0000MS_NA_Shoukyakuritu SKM ' +CHAR(13)
            SET @strSQL = @strSQL + ' ON  SKM.TaiyouNensuu = TIH.TaiyouNensuu ' +CHAR(13)
            SET @strSQL = @strSQL + ' LEFT JOIN N0000MS_NA_SisanZigyousho SZG ' +CHAR(13)
            SET @strSQL = @strSQL + ' ON  SZG.ZigyoushoCD  = WK.ZigyoushoCD ' +CHAR(13)
            SET @strSQL = @strSQL + ' LEFT JOIN N0000RR_NA_TihouTaiyouHenkouJouhou THJ ' +CHAR(13)
            SET @strSQL = @strSQL + ' ON  THJ.SisanNO         = WK.SisanNO ' +CHAR(13)
            SET @strSQL = @strSQL + ' AND THJ.Edaban          = WK.Edaban ' +CHAR(13)
            SET @strSQL = @strSQL + ' AND THJ.KaiteiNendoDate = ''' + CONVERT(varchar(10),@p_SinkokuNendo) + '''' +CHAR(13)
            --  2013/01/09 S.Murakami  (N00995)分割合併対応↓
            SET @strSQL = @strSQL + ' LEFT JOIN N0000RR_NA_BunkatuGappeiJouhou BGJ ' +CHAR(13)
            SET @strSQL = @strSQL + ' ON  BGJ.SisanNO = WK.SisanNO ' +CHAR(13)
            SET @strSQL = @strSQL + ' AND  BGJ.Edaban = WK.Edaban ' +CHAR(13)
            SET @strSQL = @strSQL + ' AND  BGJ.ShoukyakuKBN = 0 ' +CHAR(13)
            SET @strSQL = @strSQL + ' AND  BGJ.ZouGenKBN = 0 ' +CHAR(13)
            --  2013/01/09 S.Murakami  (N00995)分割合併対応↑
            -- 引数.事業所セキュリティパターンコードが""でないとき、以下のJOIN句を追加
            IF (@p_ZigSecPtnCD IS NOT NULL)
            BEGIN
                SET @strSQL = @strSQL + ' INNER JOIN ' +CHAR(13)
                SET @strSQL = @strSQL + '     M0000MS_MA_ZigyoushoSecurityYouso MZSY ' +CHAR(13) -- 事業所セキュリティ要素マスタ
                SET @strSQL = @strSQL + ' ON  MZSY.ZigyoushoCDYouso  = WK.ZigyoushoCD ' +CHAR(13)
                SET @strSQL = @strSQL + ' AND MZSY.SecurityPatternCD = ' + dbo.N0000FC_Escape_Varchar(@p_ZigSecPtnCD) +CHAR(13)
            END
--  2018/04/26 T.Maeda      (97532-97598)改元対応(元号マスタを結合)↓↓
            SET @strSQL = @strSQL + ' LEFT JOIN A0000MS_Gengou AS Gengou ' +CHAR(13)
            --資産の取得判定を、取得日もしくは償却開始日で行うかで元号マスタの結合条件を変更
            --取得日で判定を行う場合
            IF(@ShutokuDateHanteiKBN =0)
            BEGIN
            --元号開始日の範囲指定
            --合併区分が1：売買の時、分割合併日で考慮
                SET @strSQL = @strSQL + ' ON Gengou.GengouStartDate<= CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            THEN DATEADD(DAY,-1,BGJ.BunkatuGappeiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            ELSE BGJ.BunkatuGappeiDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  END ' +CHAR(13)
            --合併区分が0：簿価引継の時、取得日で考慮
                SET @strSQL = @strSQL + '                                  ELSE CASE WHEN MONTH(WK.ShutokuDate) = 1 AND DAY(WK.ShutokuDate) = 1  ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            THEN DATEADD(DAY,-1,WK.ShutokuDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            ELSE WK.ShutokuDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  END ' +CHAR(13)
                SET @strSQL = @strSQL + '                             END ' +CHAR(13)

            --元号終了日の範囲指定
            --合併区分が1：売買の時、分割合併日で考慮
                SET @strSQL = @strSQL + ' AND DATEADD(DAY,1,Gengou.GengouEndDate) > CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            THEN DATEADD(DAY,-1,BGJ.BunkatuGappeiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            ELSE BGJ.BunkatuGappeiDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  END ' +CHAR(13)
            --合併区分が0：簿価引継の時、取得日で考慮
                SET @strSQL = @strSQL + '                                  ELSE CASE WHEN MONTH(WK.ShutokuDate) = 1 AND DAY(WK.ShutokuDate) = 1  ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            THEN DATEADD(DAY,-1,WK.ShutokuDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            ELSE WK.ShutokuDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  END ' +CHAR(13)
                SET @strSQL = @strSQL + '                             END ' +CHAR(13)
            END

            --償却開始日で判定を行う場合
            ELSE
            BEGIN
            --合併区分が1：売買の時、分割合併日で考慮
                SET @strSQL = @strSQL + ' ON Gengou.GengouStartDate<= CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            THEN DATEADD(DAY,-1,BGJ.BunkatuGappeiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            ELSE BGJ.BunkatuGappeiDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  END ' +CHAR(13)
            --合併区分が0：簿価引継の時、償却開始日で考慮
                SET @strSQL = @strSQL + '                                  ELSE CASE WHEN MONTH(WK.ShoukyakuKaisiDate) = 1 AND DAY(WK.ShoukyakuKaisiDate) = 1  ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            THEN DATEADD(DAY,-1,WK.ShoukyakuKaisiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            ELSE WK.ShoukyakuKaisiDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  END ' +CHAR(13)
                SET @strSQL = @strSQL + '                             END ' +CHAR(13)

            --元号終了日の範囲指定
            --合併区分が1：売買の時、分割合併日で考慮
                SET @strSQL = @strSQL + ' AND DATEADD(DAY,1,Gengou.GengouEndDate) > CASE WHEN BGJ.ZeimuBunkatuGappeiKBN = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  THEN CASE WHEN MONTH(BGJ.BunkatuGappeiDate) = 1 AND DAY(BGJ.BunkatuGappeiDate) = 1 ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            THEN DATEADD(DAY,-1,BGJ.BunkatuGappeiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            ELSE BGJ.BunkatuGappeiDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  END ' +CHAR(13)
            --合併区分が0：簿価引継の時、償却開始日で考慮
                SET @strSQL = @strSQL + '                                  ELSE CASE WHEN MONTH(WK.ShoukyakuKaisiDate) = 1 AND DAY(WK.ShoukyakuKaisiDate) = 1  ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            THEN DATEADD(DAY,-1,WK.ShoukyakuKaisiDate) ' +CHAR(13)
                SET @strSQL = @strSQL + '                                            ELSE WK.ShoukyakuKaisiDate ' +CHAR(13)
                SET @strSQL = @strSQL + '                                  END ' +CHAR(13)
                SET @strSQL = @strSQL + '                             END ' +CHAR(13)
            END

            --分割合併資産の備考に記載する為に必要な元号マスタを結合
            SET @strSQL = @strSQL + ' LEFT JOIN A0000MS_Gengou AS GengouByBunkatuGappeiDate ' +CHAR(13)
            SET @strSQL = @strSQL + ' ON GengouByBunkatuGappeiDate.GengouStartDate<= BGJ.BunkatuGappeiDate ' +CHAR(13)
            SET @strSQL = @strSQL + ' AND DATEADD(DAY,1,GengouByBunkatuGappeiDate.GengouEndDate) > BGJ.BunkatuGappeiDate ' +CHAR(13)
--  2018/04/26 T.Maeda      (97532-97598)改元対応(元号マスタを結合)↑↑

            --WHERE句
            SET @strSQL = @strSQL + ' WHERE 1 = 1 ' +CHAR(13)
            --自治体CD
            IF @p_ZititaiCDBegin IS NOT NULL
            BEGIN
                SET @strSQL = @strSQL + ' AND WK.ZititaiCD >= ''' + @p_ZititaiCDBegin + '''' +CHAR(13)
            END                
            IF @p_ZititaiCDEnd IS NOT NULL
            BEGIN
                SET @strSQL = @strSQL + ' AND WK.ZititaiCD <= ''' + @p_ZititaiCDEnd + '''' +CHAR(13)
            END
            --事業所CD
            IF @p_ShuturyokuKBN = 1
            BEGIN
                IF @p_ZigyoushoCDBegin IS NOT NULL
                BEGIN
                    SET @strSQL = @strSQL + ' AND WK.ZigyoushoCD >= ''' + @p_ZigyoushoCDBegin + '''' +CHAR(13)
                END
                IF @p_ZigyoushoCDEnd IS NOT NULL
                BEGIN
                    SET @strSQL = @strSQL + ' AND WK.ZigyoushoCD <= ''' + @p_ZigyoushoCDEnd + '''' +CHAR(13)
                END
            END

            EXEC (@strSQL)

            SET @ErrorCode = @@ERROR
            IF @ErrorCode <> 0 GOTO ERR_SHORI

        END

        --3.データ件数をカウント
        SELECT @CNT = COUNT(*)
        FROM #N0000WK_Obic7fx_Win_N0000_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_Data

        SET @ErrorCode = @@ERROR
        IF @ErrorCode <> 0 GOTO ERR_SHORI

        IF @CNT = 0
        BEGIN
            SELECT * FROM #N0000WK_Obic7fx_Win_N0000_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_Data

            SET @ErrorCode = @@ERROR
            IF @ErrorCode <> 0 GOTO ERR_SHORI

            RETURN @ErrorCode
        END
        --4.帳票データワークに対して、事由区分、減価残存率を再セット
--2010/12/07 OBIC H.Sudo 変更 開始 (N00694-00)増加事由のロジックを修正
        IF @p_SinkokuKBN = 0
        BEGIN
--2010/12/07 OBIC H.Sudo 変更 終了 (N00694-00)増加事由のロジックを修正

        UPDATE #N0000WK_Obic7fx_Win_N0000_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_Data
        SET  ZiyuuKBN         = CASE WHEN ZiyuuKBN = 0
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↓
                                     --THEN CASE WHEN ShutokuDate BETWEEN @SinkokuKikanBegin AND @SinkokuKikanEnd 
                                     THEN CASE WHEN CASE @ShutokuDateHanteiKBN WHEN 0 THEN ShutokuDate ELSE ShoukyakuKaisiDate END BETWEEN @SinkokuKikanBegin AND @SinkokuKikanEnd 
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↑
                                               THEN ISNULL(ShutokuKBN,0) + 1
                                               ELSE 0
                                          END
                                     WHEN ZiyuuKBN = 3
                                     THEN 3
                                     ELSE ZiyuuKBN
                                END
            ,Genkazanzonritu  = CASE WHEN TaiyouNensuu = 1
                                     THEN NULL
                                     ELSE Genkazanzonritu
                                END

        SET @ErrorCode = @@ERROR
        IF @ErrorCode <> 0 GOTO ERR_SHORI

--2010/12/07 OBIC H.Sudo 変更 開始 (N00694-00)増加事由のロジックを修正
        END
        ELSE
        BEGIN

        UPDATE #N0000WK_Obic7fx_Win_N0000_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_Data
        SET  ZiyuuKBN         = CASE WHEN ZiyuuKBN = 1
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↓
                                     --THEN CASE WHEN ShutokuDate BETWEEN @SinkokuKikanBegin AND @SinkokuKikanEnd 
                                     THEN CASE WHEN CASE @ShutokuDateHanteiKBN WHEN 0 THEN ShutokuDate ELSE ShoukyakuKaisiDate END BETWEEN @SinkokuKikanBegin AND @SinkokuKikanEnd 
--2015/12/09 T.Shiraishi  (33522)地方税の申告対象となる日付を取得日ではなく償却開始日にする↑
                                               THEN ISNULL(ShutokuKBN,0) + 1
                                               ELSE 0
                                          END
                                     WHEN ZiyuuKBN = 3
                                     THEN 3
--  2013/01/09 S.Murakami  (N00995)分割合併対応↓
                                     WHEN ZiyuuKBN = 4
                                     THEN 4
--  2013/01/09 S.Murakami  (N00995)分割合併対応↑
                                     ELSE ZiyuuKBN
                                END
            ,Genkazanzonritu  = CASE WHEN TaiyouNensuu = 1
                                     THEN NULL
                                     ELSE Genkazanzonritu
                                END

        SET @ErrorCode = @@ERROR
        IF @ErrorCode <> 0 GOTO ERR_SHORI

        END
--2010/12/07 OBIC H.Sudo 変更 終了 (N00694-00)増加事由のロジックを修正

        --5.帳票データワークに対して、特例率と課税価額を更新
        UPDATE #N0000WK_Obic7fx_Win_N0000_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_Data
        SET  Tokureiritu = CONVERT(varchar(2),TokureirituNumerator) + RIGHT('00' + CONVERT(varchar(2),TokureirituDenominator),2)
            ,KazeiKagaku = ROUND(KazeiKagaku * TokureirituNumerator / TokureirituDenominator,0)
--              2020/11/16 N.Maeda      (149912)特例率ゼロが登録できません 開始
        WHERE TokureirituNumerator   <> 0
        AND   TokureirituDenominator <> 0
--              2020/11/16 N.Maeda      (149912)特例率ゼロが登録できません 終了
        AND  (YEAR(TokureiKikan) >= YEAR(@p_SinkokuNendo)
        OR    TokureiKikan       IS NULL)

--              2020/11/16 N.Maeda      (149912)特例率ゼロが登録できません 開始
        UPDATE #N0000WK_Obic7fx_Win_N0000_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_Data
        SET  Tokureiritu = '000'
            ,KazeiKagaku = 0
        WHERE (TokureirituNumerator   = 0
        OR   TokureirituDenominator = 0)
        AND  (YEAR(TokureiKikan) >= YEAR(@p_SinkokuNendo)
        OR    TokureiKikan       IS NULL)
--              2020/11/16 N.Maeda      (149912)特例率ゼロが登録できません 終了

        SET @ErrorCode = @@ERROR
        IF @ErrorCode <> 0 GOTO ERR_SHORI

        --6.ページ番号採番
        SET @strSQL = ''
        SET @strSQL = @strSQL + ' DECLARE curPrint CURSOR ' +CHAR(13)
        SET @strSQL = @strSQL + ' FOR SELECT ' +CHAR(13)
        SET @strSQL = @strSQL + '          CompanyName AS ShoyuushaName ' +CHAR(13)
        SET @strSQL = @strSQL + '         ,SisanNO ' +CHAR(13)
        SET @strSQL = @strSQL + '         ,Edaban ' +CHAR(13)
        SET @strSQL = @strSQL + '         ,SisanShubetuNO ' +CHAR(13)
        SET @strSQL = @strSQL + '         ,SisanName ' +CHAR(13)
        SET @strSQL = @strSQL + '         ,Suuryou ' +CHAR(13)
        SET @strSQL = @strSQL + '         ,Nengou ' +CHAR(13)
        SET @strSQL = @strSQL + '         ,ShutokuNen ' +CHAR(13)
        SET @strSQL = @strSQL + '         ,ShutokuTuki ' +CHAR(13)
        SET @strSQL = @strSQL + '         ,ShutokuKagaku ' +CHAR(13)
        SET @strSQL = @strSQL + '         ,TaiyouNensuu ' +CHAR(13)
        SET @strSQL = @strSQL + '         ,Genkazanzonritu ' +CHAR(13)
        SET @strSQL = @strSQL + '         ,Kagaku ' +CHAR(13)
        SET @strSQL = @strSQL + '         ,Tokureiritu ' +CHAR(13)
        SET @strSQL = @strSQL + '         ,KazeiKagaku ' +CHAR(13)
        SET @strSQL = @strSQL + '         ,ZiyuuKBN ' +CHAR(13)
        SET @strSQL = @strSQL + '         ,Bikou ' +CHAR(13)
        SET @strSQL = @strSQL + '         ,ZititaiCD ' +CHAR(13)
        SET @strSQL = @strSQL + '         ,ZigyoushoCD ' +CHAR(13)
        SET @strSQL = @strSQL + ' FROM #N0000WK_Obic7fx_Win_N0000_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_Data ' +CHAR(13)
        IF @p_ShuturyokuKBN = 0
        BEGIN
            SET @strSQL = @strSQL + ' ORDER BY ZititaiCD ,SisanShubetuNO ,SisanNO ,Edaban ,RirekiDate ' +CHAR(13)
        END
        ELSE
        BEGIN
            SET @strSQL = @strSQL + ' ORDER BY ZigyoushoCD ,ZititaiCD ,SisanShubetuNO ,SisanNO ,Edaban ,RirekiDate ' +CHAR(13)
        END

        EXEC (@strSQL)

        OPEN curPrint
        FETCH NEXT FROM curPrint 
        INTO @curShoyuushaName   ,@curSisanNO     ,@curEdaban
            ,@curSisanShubetuNO  ,@curSisanName   ,@curSuuryou       ,@curNengou
            ,@curShutokuNen      ,@curShutokuTuki ,@curShutokuKagaku ,@curTaiyouNensuu
            ,@curGenkazanzonritu ,@curKagaku      ,@curTokureiritu   ,@curKazeiKagaku
            ,@curZiyuuKBN        ,@curBikou       ,@curZititaiCD     ,@curZigyoushoCD

        SET @intGyouNO = 1
        SET @intPageNO = 1
        SET @intAbsPageNO      = 0
        SET @intKaiPageTani    = 0
        SET @savZititaiCD      = ''
        SET @savZigyoushoCD    = ''
        SET @savSisanShubetuNO = 0

        WHILE ( @@FETCH_STATUS = 0 )
        BEGIN
            --改ページ条件ブレイク時
            IF @curZititaiCD <> @savZititaiCD OR (@p_ShuturyokuKBN = 1 AND @curZigyoushoCD <> @savZigyoushoCD) OR (@ShuruibetuMeisaishoKaiPageKBN = 1 AND @curSisanShubetuNO <> @savSisanShubetuNO)
            BEGIN
                SET @intGyouNO = 1
--↓2009/06/26 Y.Ishizawa   資産種類で改ページした場合、総ページ数を振りなおさない
--                 SET @intPageNO = 1
                IF (@curZititaiCD <> @savZititaiCD OR (@p_ShuturyokuKBN = 1 AND @curZigyoushoCD <> @savZigyoushoCD))
                BEGIN
                    SET @intPageNO = 1
                    SET @intKaiPageTani    = @intKaiPageTani + 1
                END
                ELSE
                BEGIN
                    SET @intPageNO = @intPageNO + 1
                END
--↑2009/06/26 Y.Ishizawa   資産種類で改ページした場合、総ページ数を振りなおさない
                SET @intAbsPageNO      = @intAbsPageNO + 1
--↓2009/06/26 Y.Ishizawa   資産種類で改ページした場合、総ページ数を振りなおさない
--                 SET @intKaiPageTani    = @intKaiPageTani + 1
--↑2009/06/26 Y.Ishizawa   資産種類で改ページした場合、総ページ数を振りなおさない
                SET @savZititaiCD      = @curZititaiCD
                SET @savZigyoushoCD    = @curZigyoushoCD
                SET @savSisanShubetuNO = @curSisanShubetuNO
            END

            --明細が20行を超えるとき
            IF @intGyouNO > 20
            BEGIN
                SET @intGyouNO    = 1
                SET @intPageNO    = @intPageNO + 1
                SET @intAbsPageNO = @intAbsPageNO + 1
            END
    
            INSERT INTO #N0000WK_Obic7fx_Win_N0000_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_Print (
                     GyouNO
                    ,PageNO
                    ,AbsPageNO
                    ,KaiPageTani
                    ,ShoyuushaName
                    ,SisanNO
                    ,Edaban
                    ,SisanShubetuNO
                    ,SisanName
                    ,Suuryou
                    ,Nengou
                    ,ShutokuNen
                    ,ShutokuTuki
                    ,ShutokuKagaku
                    ,TaiyouNensuu
                    ,Genkazanzonritu
                    ,Kagaku
                    ,Tokureiritu
                    ,KazeiKagaku
                    ,ZiyuuKBN
                    ,Bikou
                    ,ZititaiCD
                    --2026/10/11 (5525)事業所別償却資産申告対応 ↓
                    ,ZigyoushoCD
                    --2026/10/11 (5525)事業所別償却資産申告対応 ↑
                    )
            SELECT
                     @intGyouNO
                    ,@intPageNO
                    ,@intAbsPageNO
                    ,@intKaiPageTani
                    ,@curShoyuushaName
                    ,@curSisanNO
                    ,@curEdaban
                    ,@curSisanShubetuNO
                    ,@curSisanName
                    ,@curSuuryou
                    ,@curNengou
                    ,@curShutokuNen
                    ,@curShutokuTuki
                    ,@curShutokuKagaku
                    ,@curTaiyouNensuu
                    ,@curGenkazanzonritu
                    ,@curKagaku
                    ,@curTokureiritu
                    ,@curKazeiKagaku
                    ,@curZiyuuKBN
                    ,@curBikou
                    ,@curZititaiCD
                    --2026/10/11 (5525)事業所別償却資産申告対応 ↓
                    ,@curZigyoushoCD
                    --2026/10/11 (5525)事業所別償却資産申告対応 ↑

            SET @ErrorCode = @@ERROR
            IF @ErrorCode <> 0 GOTO ERR_SHORI

            SET @intGyouNO = @intGyouNO + 1

            FETCH NEXT FROM curPrint 
            INTO @curShoyuushaName   ,@curSisanNO     ,@curEdaban
                ,@curSisanShubetuNO  ,@curSisanName   ,@curSuuryou       ,@curNengou
                ,@curShutokuNen      ,@curShutokuTuki ,@curShutokuKagaku ,@curTaiyouNensuu
                ,@curGenkazanzonritu ,@curKagaku      ,@curTokureiritu   ,@curKazeiKagaku
                ,@curZiyuuKBN        ,@curBikou       ,@curZititaiCD     ,@curZigyoushoCD
        END --WHILE

        CLOSE curPrint
        DEALLOCATE curPrint

        --7.各種値調整
        --7-1.ブレイク条件毎のMAXページ採番、小計算出
-- ↓ 2011/12/22 S.Harada (N00878)資産種類で改ページ指定の時、小計が印字されない不具合修正
        IF (@ShuruibetuMeisaishoKaiPageKBN = 0)
        BEGIN
-- ↑ 2011/12/22 S.Harada (N00878)資産種類で改ページ指定の時、小計が印字されない不具合修正
            UPDATE MAIN
            SET  MaxPageNO = SUB.MaxPageNO
                ,Shoukei   = CASE WHEN MAIN.PageNO = SUB.MaxPageNO
                                  THEN SUB.Shoukei
                                  ELSE NULL
                             END
            FROM #N0000WK_Obic7fx_Win_N0000_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_Print MAIN
            INNER JOIN (SELECT KaiPageTani
                              ,MAX(PageNO) AS MaxPageNO
                              ,SUM(ISNULL(ShutokuKagaku,0)) AS Shoukei
                        FROM #N0000WK_Obic7fx_Win_N0000_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_Print
                        GROUP BY KaiPageTani
                        ) SUB
            ON  SUB.KaiPageTani  = MAIN.KaiPageTani

            SET @ErrorCode = @@ERROR
            IF @ErrorCode <> 0 GOTO ERR_SHORI
-- ↓ 2011/12/22 S.Harada (N00878)資産種類で改ページ指定の時、小計が印字されない不具合修正
        END
        ELSE
        BEGIN
            UPDATE MAIN
            SET  MaxPageNO = SUB3.MaxPageNO
                ,Shoukei   = CASE WHEN MAIN.PageNO = SUB.MaxPageNO
                                  THEN SUB.Shoukei
                                  ELSE NULL
                             END
            FROM #N0000WK_Obic7fx_Win_N0000_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_Print MAIN
            INNER JOIN (SELECT KaiPageTani
                              ,MAX(PageNO) AS MaxPageNO
                              ,SUM(ISNULL(ShutokuKagaku,0)) AS Shoukei
                              ,SisanShubetuNO
                        FROM #N0000WK_Obic7fx_Win_N0000_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_Print
                        GROUP BY KaiPageTani, SisanShubetuNO
                        ) SUB
            ON  SUB.KaiPageTani       =  MAIN.KaiPageTani
            AND SUB.SisanShubetuNO    =  MAIN.SisanShubetuNO
            INNER JOIN (SELECT COUNT(AbsPageNO) AS MaxPageNO
                              ,KaiPageTani
                              ,SisanShubetuNO
                        FROM
                              (SELECT AbsPageNO, KaiPageTani, SisanShubetuNO
                               FROM #N0000WK_Obic7fx_Win_N0000_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_Print
                               GROUP BY AbsPageNO, KaiPageTani, SisanShubetuNO
                               ) AS SUB_PAGE
                        GROUP BY SUB_PAGE.KaiPageTani, SUB_PAGE.SisanShubetuNO) SUB2
            ON  SUB2.KaiPageTani      =  MAIN.KaiPageTani
            AND SUB2.SisanShubetuNO   =  MAIN.SisanShubetuNO
            INNER JOIN (SELECT COUNT(AbsPageNO) AS MaxPageNO
                              ,ZititaiCD
                        FROM
                              (SELECT AbsPageNO, ZititaiCD
                               FROM #N0000WK_Obic7fx_Win_N0000_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_Print
                               GROUP BY AbsPageNO, ZititaiCD
                               ) AS SUB_PAGE
                        GROUP BY SUB_PAGE.ZititaiCD) SUB3
            ON  SUB3.ZititaiCD   =  MAIN.ZititaiCD

            SET @ErrorCode = @@ERROR
            IF @ErrorCode <> 0 GOTO ERR_SHORI
        END
-- ↑ 2011/12/22 S.Harada (N00878)資産種類で改ページ指定の時、小計が印字されない不具合修正

        --7-2.印字制御
        --7-2-1.｢資産コードを印字｣がOFFの場合
        IF @p_SisanCDPrintKBN = 0
        BEGIN
            UPDATE #N0000WK_Obic7fx_Win_N0000_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_Print
            SET  SisanNO = NULL
                ,Edaban  = NULL

            SET @ErrorCode = @@ERROR
            IF @ErrorCode <> 0 GOTO ERR_SHORI
        END
        --7-2-2.｢価額を印字｣がOFFの場合
        IF @p_KagakuPrintKBN = 0
        BEGIN
            UPDATE #N0000WK_Obic7fx_Win_N0000_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_Print
            SET  Kagaku = NULL

            SET @ErrorCode = @@ERROR
            IF @ErrorCode <> 0 GOTO ERR_SHORI
        END
        --7-2-3.｢課税標準額を印字｣がOFFの場合
        IF @p_KazeiHyoujunGakuPrintKBN = 0
        BEGIN
            UPDATE #N0000WK_Obic7fx_Win_N0000_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_Print
            SET  KazeiKagaku = NULL

            SET @ErrorCode = @@ERROR
            IF @ErrorCode <> 0 GOTO ERR_SHORI
        END

        --8.帳票出力
--2012/11/05 E.Sato ST (N00970)eLTax対応    
		--印刷の場合
        IF (@p_ShuturyokuSiteiKBN = 0)
        BEGIN
--2012/11/05 E.Sato ED    
        SELECT
                 WK.GyouNO
                ,WK.PageNO
                ,WK.MaxPageNO
                ,WK.AbsPageNO
                ,CASE WHEN @p_ShuturyokuKBN = 0
                      THEN @p_CompanyName
                      ELSE WK.ShoyuushaName
                 END AS ShoyuushaName
                ,ZIT.ShoyuushaCD
                ,WK.SisanNO
                ,RIGHT('000' + CONVERT(varchar(3),WK.Edaban),3) AS Edaban
                ,RIGHT('00' + CONVERT(varchar,WK.SisanShubetuNO),2) AS SisanShubetuNO
                ,WK.SisanName
                ,CASE WHEN FLOOR(WK.Suuryou) = WK.Suuryou
                      THEN CONVERT(varchar,FLOOR(WK.Suuryou))
                      ELSE CONVERT(varchar,WK.Suuryou)
                 END AS Suuryou
                ,WK.Nengou
                ,WK.ShutokuNen
                ,WK.ShutokuTuki
                ,CASE WHEN ISNULL(WK.ShutokuKagaku,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku,3,4,1) END AS TihouzeiShutokuKagaku1
                ,CASE WHEN ISNULL(WK.ShutokuKagaku,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku,3,3,1) END AS TihouzeiShutokuKagaku2
                ,CASE WHEN ISNULL(WK.ShutokuKagaku,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku,3,2,1) END AS TihouzeiShutokuKagaku3
                ,CASE WHEN ISNULL(WK.ShutokuKagaku,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku,3,1,1) END AS TihouzeiShutokuKagaku4
                ,WK.TaiyouNensuu
                ,WK.Genkazanzonritu
                ,CASE WHEN ISNULL(WK.Kagaku,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Kagaku,3,4,1) END AS Kagaku1
                ,CASE WHEN ISNULL(WK.Kagaku,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Kagaku,3,3,1) END AS Kagaku2
                ,CASE WHEN ISNULL(WK.Kagaku,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Kagaku,3,2,1) END AS Kagaku3
                ,CASE WHEN ISNULL(WK.Kagaku,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Kagaku,3,1,1) END AS Kagaku4
                ,WK.Tokureiritu
                ,CASE WHEN ISNULL(WK.KazeiKagaku,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiKagaku,3,4,1) END AS KazeiKagaku1
                ,CASE WHEN ISNULL(WK.KazeiKagaku,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiKagaku,3,3,1) END AS KazeiKagaku2
                ,CASE WHEN ISNULL(WK.KazeiKagaku,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiKagaku,3,2,1) END AS KazeiKagaku3
                ,CASE WHEN ISNULL(WK.KazeiKagaku,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiKagaku,3,1,1) END AS KazeiKagaku4
                ,WK.ZiyuuKBN
--  2021 06/03 Y.Matuzawa (183279) 会社分割によって増加した資産の備考が「合併による」と表示される 対応 ↓
                --,dbo.A0000FC_InsertReturn(WK.Bikou, 8) AS Bikou
                ,dbo.A0000FC_InsertReturn(WK.Bikou, 10) AS Bikou
--  2021 06/03 Y.Matuzawa (183279) 会社分割によって増加した資産の備考が「合併による」と表示される 対応 ↑
                ,WK.ZititaiCD
                ,ZIT.ZititaiName
                ,CASE WHEN ISNULL(WK.Shoukei,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Shoukei,3,4,1) END AS Shoukei1
                ,CASE WHEN ISNULL(WK.Shoukei,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Shoukei,3,3,1) END AS Shoukei2
                ,CASE WHEN ISNULL(WK.Shoukei,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Shoukei,3,2,1) END AS Shoukei3
                ,CASE WHEN ISNULL(WK.Shoukei,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Shoukei,3,1,1) END AS Shoukei4
        FROM #N0000WK_Obic7fx_Win_N0000_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_Print WK
        LEFT JOIN N0000MS_NA_Zititai ZIT
        ON  ZIT.ZititaiCD = WK.ZititaiCD        
        ORDER BY [ID]

        SET @ErrorCode = @@ERROR
        IF @ErrorCode <> 0 GOTO ERR_SHORI

--2012/11/05 E.Sato ST (N00970)eLTax対応
        END
        --ファイル出力の場合
        ELSE 
        BEGIN

            SELECT
                 
                -- 2015/01/05 K.Morita 変更 (19288-19289-19291)eLTax仕様変更対応 ↓
                -- WK.ZititaiCD AS [提出先市区町村コード]
                --,NULL AS [税務事業所コード]
                 1 AS [増減区分]
                ,NULL AS [行番号]
                ,1 AS [異動区分]
                ,4 AS [異動事由]
                ,NULL AS [プレ明細資産の種類]
                ,NULL AS [プレ明細抹消コード]
                ,NULL AS [プレ明細資産の名称等]
                ,NULL AS [プレ明細数量]
                ,NULL AS [プレ明細取得元号]
                ,NULL AS [プレ明細取得年]
                ,NULL AS [プレ明細取得月]
                ,NULL AS [プレ明細取得価額（イ）]
                ,NULL AS [プレ明細耐用年数]
                ,NULL AS [プレ明細申告年度元号]
                ,NULL AS [プレ明細申告年度年]
                ,NULL AS [プレ明細事由]
                ,NULL AS [プレ明細区分]
                ,NULL AS [プレ明細摘要]
                -- 2015/01/05 K.Morita 変更 (19288-19289-19291)eLTax仕様変更対応 ↑
                ,WK.SisanShubetuNO AS [資産の種類]
                ,WK.SisanNO + '-'+ RIGHT('000' + CONVERT(varchar(3),WK.Edaban),3) AS [資産コード]
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↓
                --,WK.SisanName AS [資産の名称等]
                ,LEFT(dbo.N0000FC_Convert_HankakuToZenkaku(WK.SisanName),30) AS [資産の名称等]
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↑
                --2021/05/13 Y.Nakajima   (184642)種類別明細書のデータ出力にて、登録された数量が1未満の場合0でなく1で出力する↓↓
                --,FLOOR(WK.Suuryou) AS [数量]
                ,CASE WHEN FLOOR(WK.Suuryou) < 1 THEN 1
                      ELSE FLOOR(WK.Suuryou)
                 END AS [数量]
                --2021/05/13 Y.Nakajima   (184642)種類別明細書のデータ出力にて、登録された数量が1未満の場合0でなく1で出力する↑↑
                ,WK.Nengou AS [年号]
                -- 2015/01/05 K.Morita 変更 (19288-19289-19291)eLTax仕様変更対応 ↓
                --,WK.ShutokuNen AS [年]
                --,WK.ShutokuTuki AS [月]
                ,RIGHT('00' + CONVERT(varchar(2), WK.ShutokuNen) ,2) AS [年]
                ,RIGHT('00' + CONVERT(varchar(2), WK.ShutokuTuki) ,2) AS [月]
                -- 2015/01/05 K.Morita 変更 (19288-19289-19291)eLTax仕様変更対応 ↑
                ,WK.ShutokuKagaku AS [取得価額（イ）]
                ,WK.TaiyouNensuu AS [耐用年数]
                -- 2015/01/05 K.Morita 追加 (19288-19289-19291)eLTax仕様変更対応 ↓
                ,NULL AS [申告年度元号]
                ,NULL AS [申告年度年]
                -- 2015/01/05 K.Morita 追加 (19288-19289-19291)eLTax仕様変更対応 ↑
                ,WK.Genkazanzonritu AS [減価残存率（ロ）]
                ,WK.Kagaku AS [価額（ハ）]
                ,WK.Tokureiritu AS [課税標準の特例　率]
                ,NULL AS [コード]
                ,WK.KazeiKagaku AS [課税標準額]
                ,KSC.GendogakuHyouzi AS [限度額表示]
                ,NULLIF(WK.ZiyuuKBN, 0) AS [事由]
                -- 2015/01/05 K.Morita 変更 (19288-19289-19291)eLTax仕様変更対応 ↓
                --,WK.Bikou AS [摘要]
                ,NULL AS [区分]
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↓
                --,WK.Bikou AS [摘要]
                ,dbo.N0000FC_Convert_HankakuToZenkaku(WK.Bikou) AS [摘要]
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↑
                ,NULL AS [団体独自項目01]
                ,NULL AS [団体独自項目02]
                ,NULL AS [団体独自項目03]
                ,NULL AS [団体独自項目04]
                ,NULL AS [団体独自項目05]
                ,NULL AS [団体独自項目06]
                ,NULL AS [団体独自項目07]
                ,NULL AS [団体独自項目08]
                ,NULL AS [団体独自項目09]
                ,NULL AS [団体独自項目10]
              --2020/12/25 Y.Matuzawa   修正  (182011) 「提出先市町村コード」の取得元を地方公共団体コードに変更 ↓
                --,WK.ZititaiCD AS [提出先市町村コード]
                ,ISNULL(ZIT.TihoukoukyoudantaiCD, WK.ZititaiCD) AS [提出先市町村コード]
              --2020/12/25 Y.Matuzawa   修正  (182011) 「提出先市町村コード」の取得元を地方公共団体コードに変更 ↑
                ,NULL AS [提出先区・事務所コード]
                ,LEFT(ZIT.ShoyuushaCD,20) AS [所有者コード]
                --2026/10/11 (5525)事業所別償却資産申告対応 ↓
                -- ファイル分割用(事業所別のみ有効、全社は'*')。画面側でファイル出力前に列を削除する
                ,CASE WHEN @p_ShuturyokuKBN = 1 THEN WK.ZigyoushoCD ELSE '*' END AS [N5525_事業所コード]
                --2026/10/11 (5525)事業所別償却資産申告対応 ↑
                -- 2015/01/05 K.Morita 変更 (19288-19289-19291)eLTax仕様変更対応 ↑
            FROM #N0000WK_Obic7fx_Win_N0000_Print_NA_ShuruiBetuMeisaishoZoukaZensisan_Print WK
            LEFT JOIN N0000MS_NA_Zititai ZIT
            ON  ZIT.ZititaiCD = WK.ZititaiCD        
            LEFT JOIN N0000MS_NA_KoteiSisanControl KSC
            ON  KSC.NendoID = 0
            WHERE ZIT.eLTaxSiyouKBN = 1            
            ORDER BY 
                --2026/10/11 (5525)事業所別償却資産申告対応 ↓
                 CASE WHEN @p_ShuturyokuKBN = 1 THEN WK.ZigyoushoCD ELSE '*' END
                ,WK.ZititaiCD
                --WK.ZititaiCD
                --2026/10/11 (5525)事業所別償却資産申告対応 ↑
                ,WK.SisanShubetuNO
                ,WK.SisanNO
                ,WK.Edaban

            SET @ErrorCode = @@ERROR
            IF @ErrorCode <> 0 GOTO ERR_SHORI
        END
--2012/11/05 E.Sato ED        
        
        RETURN @ErrorCode
        --SET NOCOUNT OFF

-------------------------------------------------------------------------------
/* ラベル：ERR_SHORI */
ERR_SHORI:
    IF @po_ErrorKBN = 0
    BEGIN
        -- 引数に戻り値をセット
        SET @po_ErrorKBN        = 1                    --1：システムエラー
        SET @po_ErrorCD1        = '000'                  --会計は"000"で統一
        SET @po_ErrorCD2        = '00119'                --固定資産は"00119"で統一
        SET @po_ErrorProcName   = OBJECT_NAME(@@PROCID)
        SET @po_ErrorSummary    = ''
        SET @po_ErrorDetail     = ''
        SET @po_ErrorBtnKBN     = 0
        SET @po_ErrorBtnDefault = 0
    END

    RETURN @ErrorCode

END

GO

