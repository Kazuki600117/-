SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[M0000PR_MA_HonsitenBunkai]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[M0000PR_MA_HonsitenBunkai]
GO


CREATE PROCEDURE  [dbo].[M0000PR_MA_HonsitenBunkai](

      @po_ErrorKBN                      int              OUTPUT     /* エラー区分 */
    , @po_ErrorCD1                      varchar(128)     OUTPUT     /* エラーCD1 */
    , @po_ErrorCD2                      varchar(128)     OUTPUT     /* エラーCD2 */
    , @po_ErrorProcName                 varchar(128)     OUTPUT     /* エラーストアド名 */
--2008/03/28 修正 N.Sasaki 開始 戻り値の桁数変更
--    , @po_ErrorSummary                  varchar(128)     OUTPUT     /* エラーメッセージ概要 */
    , @po_ErrorSummary                  varchar(256)     OUTPUT     /* エラーメッセージ概要 */
--2008/03/28 修正 N.Sasaki 終了
    , @po_ErrorDetail                   varchar(2560)    OUTPUT     /* エラーメッセージ詳細 */
    , @po_ErrorBtnKBN                   tinyint          OUTPUT     /* ボタン区分(0：OK、1：YES・NO、2：YES・NO・CANCEL) */
    , @po_ErrorBtnDefault               tinyint          OUTPUT     /* デフォルトボタン区分(0：ボタン１、1：ボタン２、2：ボタン３) */
    , @p_BumonSecurityPatternCD         varchar(4)                  /* 部門セキュリティパターンコード */
    , @p_KanjouSecurityPatternCD        varchar(4)                  /* 総勘定科目セキュリティパターンコード */
--2008/02/06 追加 N.Sasaki 開始 振替伝票仕訳チェック処理に振替伝票仕訳チェック区分を追加し、チェックを行うか判断する。
    , @p_HurikaeDenpyouCheckKBN         tinyint                     /* 振替伝票仕訳チェック区分(0：振替伝票仕訳チェックを通す、1：セキュリティチェックを除いて振替伝票仕訳チェックを通す、2：振替伝票仕訳チェックを通さない) */
    , @p_ZigyoushoSecurityPatternCD     varchar(4)                  /* 事業所セキュリティパターンコード */
--2008/02/06 追加 N.Sasaki 終了
    , @p_UpdateDatetime                 datetime                    /* 更新日時 */
    , @p_UpdateCertificatedGuid         uniqueidentifier            /* 更新認証済Guid */
    , @p_UpdateJobID                    varchar(128)                /* 更新JobID */
    , @p_UpdateUserID                   varchar(128)                /* 更新ユーザID */
    , @p_UpdateComputerName             varchar(128)                /* 更新PC名 */
    , @p_UpdateComputerUserName         varchar(128)                /* 更新PCユーザ名 */
    , @p_UpdateComputerDomainName       varchar(256)                /* 更新PCドメイン名 */
)
AS  

/*
***************************************************************************************************
**      名称：本支店分解処理
**      機能：本支店の分解処理を行う。
**      パラメータ
**      @po_ErrorKBN                      int              OUTPUT          -- エラー区分 
**    , @po_ErrorCD1                      varchar(128)     OUTPUT          -- エラーCD1 
**    , @po_ErrorCD2                      varchar(128)     OUTPUT          -- エラーCD2 
**    , @po_ErrorProcName                 varchar(128)     OUTPUT          -- エラーストアド名
--2008/03/28 修正 N.Sasaki 開始 戻り値の桁数変更
--**    , @po_ErrorSummary                  varchar(128)     OUTPUT          -- エラーメッセージ概要
**    , @po_ErrorSummary                  varchar(256)     OUTPUT          -- エラーメッセージ概要
--2008/03/28 修正 N.Sasaki 終了
**    , @po_ErrorDetail                   varchar(2560)    OUTPUT          -- エラーメッセージ詳細
**    , @po_ErrorBtnKBN                   tinyint          OUTPUT          -- ボタン区分(0：OK、1：YES・NO、2：YES・NO・CANCEL)
**    , @po_ErrorBtnDefault               tinyint          OUTPUT          -- デフォルトボタン区分(0：ボタン１、1：ボタン２、2：ボタン３
**    , @p_BumonSecurityPatternCD         varchar(4)                       -- 部門セキュリティパターンコード
**    , @p_KanjouSecurityPatternCD        varchar(4)                       -- 総勘定科目セキュリティパターンコード
**    , @p_HurikaeDenpyouCheckKBN         tinyint                          -- 振替伝票仕訳チェック区分(0：振替伝票仕訳チェックを通す、1：セキュリティチェックを除いて振替伝票仕訳チェックを通す、2：振替伝票仕訳チェックを通さない)
**    , @p_ZigyoushoSecurityPatternCD     varchar(4)                       -- 事業所セキュリティパターンコード
**    , @p_UpdateDatetime                 datetime                         -- 更新日時
**    , @p_UpdateCertificatedGuid         uniqueidentifier                 -- 更新認証済Guid
**    , @p_UpdateJobID                    varchar(128)                     -- 更新JobID
**    , @p_UpdateUserID                   varchar(128)                     -- 更新ユーザID
**    , @p_UpdateComputerName             varchar(128)                     -- 更新PC名
**    , @p_UpdateComputerUserName         varchar(128)                     -- 更新PCユーザ名
**    , @p_UpdateComputerDomainName       varchar(256)                     -- 更新PCドメイン名
***************************************************************************************************
**      履歴
**              2007/11/27 N.Sasaki   新規作成
**              2008/02/06 N.Sasaki   変更          振替伝票仕訳チェック処理に振替伝票仕訳チェック区分を追加し、チェックを行うか判断する。
**                                                  事業所のチェックを追加
**              2008/03/28 N.Sasaki   変更          戻り値の桁数変更
**              2008/04/01 N.Sasaki   変更          入力事業所、明細の借方事業所、明細貸方事業所の本支店勘定科目マスタチェック追加
**              2008/04/10 H.Suzuki   変更          *セット条件見直し
**              2008/04/15 K.Sato     変更          EXEC文のシングルクォーテーション対応
**              2008/05/14 N.Sasaki   変更          事業所のセキュリティは考慮しない
**                                                  振替伝票仕訳チェック区分 = 0 の場合のみ入力事業所存在チェックを行なう。
**                                                  本支店勘定科目ワーク抽出時の不具合修正
**                                                  集計処理時の発生区分の判断は金額の絶対値でおこなう
**              2008/05/22 H.Suzuki   変更          *セット条件見直し
**              2008/05/23 H.Suzuki   変更          振替事業所チェックの条件見直し
**              2008/07/01 N.Sasaki   変更          本支店分解する場合のみ入力事業所、明細の借方事業所、明細貸方事業所の本支店勘定科目マスタチェックを行なう
**              2008/07/11 OSE.Ikoma  変更          一時ワークテーブルのインデックスを変更
**              2011/04/04 OSE.Ikoma  変更          (M04583)入力事業所で発生した本店勘定については集約しないよう修正
**              2011/04/04 OSE.Ikoma  変更          (M04584)振替先伝票のソート順を変更(不正に複合仕訳になる現象を解消)
**              2013/04/22 周俊        変更 桁数拡張
**              2013/07/18 Y.Katsumata 変更 GX Azure対応
***************************************************************************************************
*/
BEGIN
    
    SET NOCOUNT ON
    /* 初期処理 */
    -- 変数定義
    DECLARE @ErrorCode                   int             /* @@ERRORの待避領域 */
    
    DECLARE @FETCH_STATUS_CURSIWAKE      int             /* @@FETCH_STATUSの待避領域 */
    DECLARE @DenpyouTani                 int             /* カーソル用伝票単位 */
    DECLARE @KariKingaku                 decimal         /* カーソル用借方金額 */
    DECLARE @KariZeigaku                 decimal         /* カーソル用借方消費税額 */
    DECLARE @KasiKingaku                 decimal         /* カーソル用貸方金額 */
    DECLARE @KasiZeigaku                 decimal         /* カーソル用貸方消費税額 */
    DECLARE @GyouNO                      int             /* カーソル用行番号 */
    DECLARE @ZigyoushoCD                 varchar(6)      /* カーソル用事業所 */
    DECLARE @SiwakeTani                  int             /* カーソル用仕訳単位 */
    DECLARE @HasseiKBN                   tinyint         /* カーソル用発生区分 */
    DECLARE @GyouNOEdaban                int             /* カーソル用行NO枝番 */
    DECLARE @ID                          int             /* カーソル用行ID */
    DECLARE @Kensuu                      int             /* 件数退避領域 */
    DECLARE @DenpyouTani_BK              int             /* 伝票単位待避 */
    DECLARE @StartGyou                   int             /* 行番号開始 */
    DECLARE @KariGoukeigaku              decimal         /* 借方合計額 */
    DECLARE @KasiGoukeigaku              decimal         /* 貸方合計額 */
    DECLARE @HontenShuutyuuFlag          tinyint         /* 本店集中制度摘要フラグ */
    DECLARE @ZigyoushoCD_BK              varchar(6)      /* 事業所待避 */
    DECLARE @SiwakeTani_BK               int             /* 仕訳単位待避 */
    DECLARE @GyouNO_BK                   int             /* 行番号待避 */
    DECLARE @HasseiKBN_BK                tinyint         /* 発生区分待避 */
    DECLARE @GyouNOEdaban_BK             int             /* 行NO枝番待避 */
    DECLARE @GyouNOEdaban_CNT            int             /* 行NO枝番カウンタ */
    DECLARE @DenpyouTaniNew              int             /* 新伝票単位 */
    DECLARE @GyouNONew                   int             /* 新伝票単位 */
    DECLARE @ZigyoushoBumonCheckKBN      tinyint         /* 事業所部門パターンチェック */
    DECLARE @SUB_ErrorCode               int             /* サブストアドからの戻り値:エラーコード */
    DECLARE @SUB_ErrorKBN                tinyint         /* サブストアドからの戻り値:エラー区分 */    
    DECLARE @SUB_ErrorCD1                varchar(500)    /* サブストアドからの戻り値:エラーコード1 */
    DECLARE @SUB_ErrorCD2                varchar(6)      /* サブストアドからの戻り値:エラーコード2 */
    DECLARE @SUB_ErrorProcName           varchar(128)    /* サブストアドのエラーストアド名 */
--2008/03/28 修正 N.Sasaki 開始 戻り値の桁数変更
--    DECLARE @SUB_ErrorSummary            varchar(128)    /* サブストアドのエラーメッセージ概要 */
    DECLARE @SUB_ErrorSummary            varchar(256)    /* サブストアドのエラーメッセージ概要 */
--2008/03/28 修正 N.Sasaki 終了
    DECLARE @SUB_ErrorDetail             varchar(2560)   /* サブストアドのエラーメッセージ詳細 */
    DECLARE @SUB_ErrorBtnKBN             tinyint         /* サブストアドのボタン区分 */
    DECLARE @SUB_ErrorBtnDefault         tinyint         /* サブストアドのデフォルトボタン区分 */

    DECLARE @SQL                         varchar (4000)  /* SQL文 */

    -- 引数初期化
    SET @po_ErrorKBN = 0
    SET @po_ErrorCD1 = ''
    SET @po_ErrorCD2 = ''
    SET @po_ErrorProcName = ''
    SET @po_ErrorSummary = ''
    SET @po_ErrorDetail = ''
    SET @po_ErrorBtnKBN = 0
    SET @po_ErrorBtnDefault = 0

--2008/07/01 削除 N.Sasaki 開始 
----2008/04/01 追加 N.Sasaki 開始 入力事業所、明細の借方事業所、明細貸方事業所の本支店勘定科目マスタチェック追加
--    SELECT
--        @Kensuu = COUNT(*)
--    FROM (
--        SELECT
--                HasseiDate         AS HasseiDate
--            ,   ZigyoushoCD        AS ZigyoushoCD
--        FROM #M0000WK_MA_HonsitenBunkai
--        WHERE OperationComputerName    =   @p_UpdateComputerName
--        AND   OperationUserID          =   @p_UpdateUserID
--        AND   OperationJobID           =   @p_UpdateJobID
--        UNION ALL
--        SELECT
--                HasseiDate         AS HasseiDate        
--            ,   KariZigyoushoCD    AS ZigyoushoCD
--        FROM #M0000WK_MA_HonsitenBunkai
--        WHERE OperationComputerName    =   @p_UpdateComputerName
--        AND   OperationUserID          =   @p_UpdateUserID
--        AND   OperationJobID           =   @p_UpdateJobID
--        AND   KariZigyoushoCD IS NOT NULL
--        UNION ALL
--        SELECT
--                HasseiDate         AS HasseiDate
--            ,   KasiZigyoushoCD    AS ZigyoushoCD
--        FROM #M0000WK_MA_HonsitenBunkai
--        WHERE OperationComputerName    =   @p_UpdateComputerName
--        AND   OperationUserID          =   @p_UpdateUserID
--        AND   OperationJobID           =   @p_UpdateJobID
--        AND   KasiZigyoushoCD IS NOT NULL
--    ) BUN                
--    LEFT JOIN  M0000MS_MA_HonsitenKanjou MHSK
--    ON  MHSK.ZigyoushoCD   =     BUN.ZigyoushoCD
--    AND MHSK.KaiteiDate    <=    BUN.HasseiDate
--    WHERE MHSK.ZigyoushoCD IS NULL
--
--    -- @@ERROR を取得し、変数に保存する。
--    SET @ErrorCode = @@ERROR
--
--    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
--    IF  @ErrorCode <> 0
--    BEGIN
--        GOTO ERR_SHORI
--    END
--    
--    IF @Kensuu <> 0
--    BEGIN
--        SET @po_ErrorKBN = 2
--        SET @po_ErrorCD1 = '000'
--        SET @po_ErrorCD2 = '00771'
--        SET @po_ErrorProcName   = OBJECT_NAME(@@PROCID)
--        SET @po_ErrorSummary    = ''
--        SET @po_ErrorDetail     = ''
--        SET @po_ErrorBtnKBN     = 0
--        SET @po_ErrorBtnDefault = 0
--        GOTO ERR_SHORI        
--    END
--
----2008/04/01 追加 N.Sasaki 終了
--2008/07/01 削除 N.Sasaki 終了 
    /****************************************************************************************/
    /* ワークテーブルの作成                                                                 */
    /****************************************************************************************/
    -- 本支店分解実行伝票格納用ワークをCREATE
    CREATE TABLE #M0000WK_MA_HonsitenBunkaiTaishou
    (
             DenpyouTani              int                                       NOT NULL

    )
    CREATE INDEX IDX_#M0000WK_MA_HonsitenBunkaiTaishou_01 ON #M0000WK_MA_HonsitenBunkaiTaishou 
    (    
             DenpyouTani
    )

    -- 分解仕訳格納用ワークをCREATE
    CREATE TABLE #M0000WK_MA_HonsitenBunkaiKakunou
    (
             DenpyouTani              int                                       NOT NULL
        ,    SiwakeTani               int                                       NOT NULL
        ,    HasseiDate               datetime                                  NOT NULL
        ,    GyouNO                   int                                       NOT NULL
        ,    KariNyuuryokuKBN         tinyint                                   NULL     --借方データ入力区分(0:画面入力データ, 1:振替データ)
        ,    KariZigyoushoCD          varchar(6)     COLLATE database_default   NULL
      --2013/04/22 周俊　修正 開始
      --,    KariBumonCD              varchar(6)     COLLATE database_default   NULL
        ,    KariBumonCD              varchar(10)     COLLATE database_default   NULL
      --2013/04/22 周俊　修正 完了 

        ,    KariTorihikisakiCD       varchar(10)    COLLATE database_default   NULL
      --2013/04/22 周俊　修正 開始
      --,    KariKanjouCD             varchar(5)     COLLATE database_default   NULL
        ,    KariKanjouCD             varchar(10)     COLLATE database_default   NULL
      --2013/04/22 周俊　修正 完了 

        ,    KariHojoCD               varchar(10)    COLLATE database_default   NULL
        ,    KariUtiwakeCD            varchar(10)    COLLATE database_default   NULL
        ,    KariKingaku              decimal(15, 0)                            NULL
        ,    KariZeigaku              decimal(15, 0)                            NULL
        ,    KariHonshaKBN            tinyint                                   NULL     --借方データ本社区分(0:支店, 1:本社)
        ,    KasiNyuuryokuKBN         tinyint                                   NULL     --貸方データ入力区分(0:画面入力データ, 1:振替データ)
        ,    KasiZigyoushoCD          varchar(6)     COLLATE database_default   NULL
      --2013/04/22 周俊　修正 開始
      --,    KasiBumonCD              varchar(6)     COLLATE database_default   NULL
        ,    KasiBumonCD              varchar(10)     COLLATE database_default   NULL
      --2013/04/22 周俊　修正 完了 

        ,    KasiTorihikisakiCD       varchar(10)    COLLATE database_default   NULL
      --2013/04/22 周俊　修正 開始
      --,    KasiKanjouCD             varchar(5)     COLLATE database_default   NULL
        ,    KasiKanjouCD             varchar(10)     COLLATE database_default   NULL
      --2013/04/22 周俊　修正 完了 

        ,    KasiHojoCD               varchar(10)    COLLATE database_default   NULL
        ,    KasiUtiwakeCD            varchar(10)    COLLATE database_default   NULL
        ,    KasiKingaku              decimal(15, 0)                            NULL
        ,    KasiZeigaku              decimal(15, 0)                            NULL
        ,    KasiHonshaKBN            tinyint                                   NULL     --貸方データ本社区分(0:支店, 1:本社)
--2011/04/04 修正 OSE.Ikoma 開始 (M04583)入力事業所で発生した本店勘定については集約しないよう修正
        ,    NyuuryokuZigyoushoCD     varchar(6)     COLLATE database_default   NULL
--2011/04/04 修正 OSE.Ikoma 終了 (M04583)入力事業所で発生した本店勘定については集約しないよう修正
    )
    CREATE INDEX IDX_#M0000WK_MA_HonsitenBunkaiKakunou_01 ON #M0000WK_MA_HonsitenBunkaiKakunou 
    (    
             HasseiDate   
        ,    KariZigyoushoCD
        ,    KariBumonCD
        ,    KariKanjouCD
        ,    KariHojoCD
        ,    KariUtiwakeCD 
    )
    
    CREATE INDEX IDX_#M0000WK_MA_HonsitenBunkaiKakunou_02 ON #M0000WK_MA_HonsitenBunkaiKakunou 
    ( 
             HasseiDate   
        ,    KasiZigyoushoCD
        ,    KasiBumonCD
        ,    KasiKanjouCD
        ,    KasiHojoCD
        ,    KasiUtiwakeCD 
    )

    --集計仕訳格納用ワーク
    CREATE TABLE #M0000WK_MA_HonsitenBunkaiShukei
    (
             [ID]                      int            IDENTITY (1, 1)          NOT NULL
        ,    DenpyouTani               int                                     NOT NULL
        ,    SiwakeTani                int                                     NOT NULL
        ,    HasseiDate                datetime                                NOT NULL
        ,    GyouNO                    int                                     NOT NULL --採番前のGyouNO(画面入力データの情報を取得する時の条件として使用)
        ,    GyouNONew                 int                                     NULL     --採番後のGyouNO
        ,    GyouNOEdaban              int                                     NULL 
        ,    DataKBN                   tinyint                                 NOT NULL --データ入力区分(0:画面入力データ, 1:振替データ)
        ,    HasseiKBN                 tinyint                                 NOT NULL
        ,    ZigyoushoCD               varchar(6)     COLLATE database_default NULL
      --2013/04/22 周俊　修正 開始
      --,    BumonCD                   varchar(6)     COLLATE database_default NULL
        ,    BumonCD                   varchar(10)     COLLATE database_default NULL
      --2013/04/22 周俊　修正 完了 

      --2013/04/22 周俊　修正 開始
      --,    KanjouCD                  varchar(5)     COLLATE database_default NULL
        ,    KanjouCD                  varchar(10)     COLLATE database_default NULL
      --2013/04/22 周俊　修正 完了 

        ,    HojoCD                    varchar(10)    COLLATE database_default NULL
        ,    UtiwakeCD                 varchar(10)    COLLATE database_default NULL
        ,    TorihikisakiCD            varchar(10)    COLLATE database_default NULL
        ,    Kingaku                   decimal(15, 0)                          NULL
        ,    Zeigaku                   decimal(15, 0)                          NULL
    )
    CREATE INDEX IDX_#M0000WK_MA_HonsitenBunkaiShukei_01 ON #M0000WK_MA_HonsitenBunkaiShukei 
    ( 
             DenpyouTani
        ,    ZigyoushoCD
        ,    SiwakeTani
        ,    GyouNO
        ,    GyouNOEdaban
        ,    HasseiKBN
    )

--2008/07/11 修正 OSE.Ikoma 開始 一時ワークテーブルのインデックスを変更
    CREATE INDEX IDX_#M0000WK_MA_HonsitenBunkaiShukei_02 ON #M0000WK_MA_HonsitenBunkaiShukei 
    ( 
             [ID] 
    )
--2008/07/11 修正 OSE.Ikoma 終了 一時ワークテーブルのインデックスを変更


    --消費税額格納ワーク
    CREATE TABLE #M0000WK_MA_HonsitenBunkai_Zeigaku
    (
             HasseiKBN                 tinyint                                   NOT NULL
        ,    DenpyouTaniNew            int                                       NULL        --#M0000WK_MA_HonsitenBunkaiKakkaの[DenpyouTaniNew]を待避する
        ,    ZigyoushoCD               varchar(6)       COLLATE database_default NULL        --#M0000WK_MA_HonsitenBunkaiKakkaの[Zigyousho]を待避する
        ,    GyouNONew                 int                                       NULL        --#M0000WK_MA_HonsitenBunkaiKakkaの[GyouNONew]を待避する
        ,    ZeiRate                   decimal(4, 3)                             NULL
        ,    HasuuKBN                  tinyint                                   NULL
        ,    ZeiKeisanKBN              tinyint                                   NULL
        ,    ZeiKamokuKBN              tinyint                                   NULL
        ,    Zeigaku                   decimal(15, 0)                            NULL
    )
    CREATE INDEX IDX_M0000WK_MA_HonsitenBunkai_Zeigaku_01 ON #M0000WK_MA_HonsitenBunkai_Zeigaku
    ( 
             HasseiKBN
        ,    DenpyouTaniNew
        ,    ZigyoushoCD
        ,    GyouNONew
    )
    
    -- 総勘定科目ワークをCREATE
    CREATE TABLE #M0000WK_MA_HonsitenBunkai_Kanjou
    (
             HasseiDate               datetime                                  NOT NULL
      --2013/04/22 周俊　修正 開始
      --,    KanjouCD                 varchar(5)    COLLATE Database_Default    NOT NULL
        ,    KanjouCD                 varchar(10)    COLLATE Database_Default    NOT NULL
      --2013/04/22 周俊　修正 完了 

        ,    KaiteiDate               datetime                                  NULL
        ,    KanjouName               varchar(30)   COLLATE Database_Default    NULL
        ,    TaishakuKBN              tinyint                                   NULL
        ,    TorihikisakiKBN          tinyint                                   NULL
        ,    HojoKBN                  tinyint                                   NULL
        ,    BumonKBN                 tinyint                                   NULL
      --2013/04/22 周俊　修正 開始
      --,    KariZeiKBN               varchar(2)    COLLATE Database_Default    NULL
        ,    KariZeiKBN               varchar(3)    COLLATE Database_Default    NULL
      --2013/04/22 周俊　修正 完了 

      --2013/04/22 周俊　修正 開始
      --,    KasiZeiKBN               varchar(2)    COLLATE Database_Default    NULL
        ,    KasiZeiKBN               varchar(3)    COLLATE Database_Default    NULL
      --2013/04/22 周俊　修正 完了 

        ,    KariZeikomiKBN           tinyint                                   NULL
        ,    KasiZeikomiKBN           tinyint                                   NULL
    )
    CREATE INDEX IDX_#M0000WK_MA_HonsitenBunkai_Kanjou ON #M0000WK_MA_HonsitenBunkai_Kanjou
    (
             HasseiDate
        ,    KanjouCD
    )


    -- 補助科目ワークをCREATE
    CREATE TABLE #M0000WK_MA_HonsitenBunkai_Hojo
    (
             HasseiDate     datetime                                    NOT NULL
      --2013/04/22 周俊　修正 開始
      --,    KanjouCD       varchar(5)      COLLATE Database_Default    NOT NULL
        ,    KanjouCD       varchar(10)      COLLATE Database_Default    NOT NULL
      --2013/04/22 周俊　修正 完了 

        ,    HojoCD         varchar(10)     COLLATE Database_Default    NOT NULL
        ,    KaiteiDate     datetime                                    NULL
        ,    UtiwakeKBN     tinyint                                     NULL
    )
    CREATE INDEX IDX_#M0000WK_MA_HonsitenBunkai_Hojo ON #M0000WK_MA_HonsitenBunkai_Hojo
    (
             HasseiDate
        ,    KanjouCD
        ,    HojoCD
    )

    -- 補助内訳科目ワークをCREATE
    CREATE TABLE #M0000WK_MA_HonsitenBunkai_Utiwake
    (
             HasseiDate         datetime                                    NOT NULL
      --2013/04/22 周俊　修正 開始
      --,    KanjouCD           varchar(5)      COLLATE Database_Default    NOT NULL
        ,    KanjouCD           varchar(10)      COLLATE Database_Default    NOT NULL
      --2013/04/22 周俊　修正 完了 

        ,    HojoCD             varchar(10)     COLLATE Database_Default    NOT NULL
        ,    UtiwakeCD          varchar(10)     COLLATE Database_Default    NOT NULL
        ,    KaiteiDate         datetime                                    NULL
    )
    CREATE INDEX IDX_#M0000WK_MA_HonsitenBunkai_Utiwake ON #M0000WK_MA_HonsitenBunkai_Utiwake
    (
             HasseiDate
        ,    KanjouCD
        ,    HojoCD
        ,    UtiwakeCD
    )

    -- 部門ワークをCREATE
    CREATE TABLE #M0000WK_MA_HonsitenBunkai_Bumon
    (
             HasseiDate     datetime                                NOT NULL
      --2013/04/22 周俊　修正 開始
      --,    BumonCD        varchar(6)  COLLATE Database_Default    NOT NULL
        ,    BumonCD        varchar(10)  COLLATE Database_Default    NOT NULL
      --2013/04/22 周俊　修正 完了 

        ,    KaiteiDate     datetime                                NULL
    )
    CREATE INDEX IDX_#M0000WK_MA_HonsitenBunkai_Bumon ON #M0000WK_MA_HonsitenBunkai_Bumon
    (
             HasseiDate
        ,    BumonCD
    )

    -- 事業所ワークをCREATE
    CREATE TABLE #M0000WK_MA_HonsitenBunkai_Zigyousho
    (
             HasseiDate             datetime                                        NOT NULL
        ,    ZigyoushoCD            varchar(6)      COLLATE Database_Default        NOT NULL
        ,    KaiteiDate             datetime                                        NULL
    )
    CREATE INDEX IDX_#M0000WK_MA_HonsitenBunkai_Zigyousho ON #M0000WK_MA_HonsitenBunkai_Zigyousho
    (
             HasseiDate
        ,    ZigyoushoCD
    )

    -- 本支店勘定科目ワークをCREATE
    CREATE TABLE #M0000WK_MA_HonsitenBunkai_HonsitenKanjou
    (
             HasseiDate             datetime                                        NOT NULL
        ,    ZigyoushoCD            varchar(6)      COLLATE database_default        NOT NULL
        ,    KaiteiDate             datetime                                        NULL
      --2013/04/22 周俊　修正 開始
      --,    BumonCD                varchar(6)      COLLATE database_default        NULL
        ,    BumonCD                varchar(10)      COLLATE database_default        NULL
      --2013/04/22 周俊　修正 完了 

        ,    HonshaZigyoushoCD      varchar(6)      COLLATE database_default        NULL
      --2013/04/22 周俊　修正 開始
      --,    KanjouCD               varchar(5)      COLLATE database_default        NULL
        ,    KanjouCD               varchar(10)      COLLATE database_default        NULL
      --2013/04/22 周俊　修正 完了 

        ,    HojoCD                 varchar(10)     COLLATE database_default        NULL
        ,    UtiwakeCD              varchar(10)     COLLATE database_default        NULL
    )
    CREATE INDEX IDX_#M0000WK_MA_HonsitenBunkai_HonsitenKanjou ON #M0000WK_MA_HonsitenBunkai_HonsitenKanjou
    (
             HasseiDate
        ,    ZigyoushoCD
--2008/07/11 修正 OSE.Ikoma 開始 一時ワークテーブルのインデックスを変更
        ,    KanjouCD
        ,    HojoCD
        ,    UtiwakeCD
--2008/07/11 修正 OSE.Ikoma 終了 一時ワークテーブルのインデックスを変更
    )

    -- 総勘定科目コントロールワークをCREATE
    CREATE TABLE #M0000WK_MA_HonsitenBunkai_KanjouControl
    (
             HasseiDate                datetime                                            NOT NULL
        ,    KaiteiDate                datetime                                            NULL
      --2013/04/22 周俊　修正 開始
      --,    KaribaraiZeiCD            varchar(5)  COLLATE Database_Default                NULL
        ,    KaribaraiZeiCD            varchar(10)  COLLATE Database_Default                NULL
      --2013/04/22 周俊　修正 完了 

        ,    KaribaraiZeiKaiteiDate    datetime                                            NULL
        ,    KaribaraiZeiTaishakuKBN   tinyint                                             NULL
      --2013/04/22 周俊　修正 開始
      --,    KariukeZeiCD              varchar(5)  COLLATE Database_Default                NULL
        ,    KariukeZeiCD              varchar(10)  COLLATE Database_Default                NULL
      --2013/04/22 周俊　修正 完了 

        ,    KariukeZeiKaiteiDate      datetime                                            NULL
        ,    KariukeZeiTaishakuKBN     tinyint                                             NULL
      --2013/04/22 周俊　修正 開始
      --,    ShokutiCD                 varchar(5)  COLLATE Database_Default                NULL
        ,    ShokutiCD                 varchar(10)  COLLATE Database_Default                NULL
      --2013/04/22 周俊　修正 完了 

    )
    CREATE INDEX IDX_#M0000WK_MA_HonsitenBunkai_KanjouControl ON #M0000WK_MA_HonsitenBunkai_KanjouControl
    (
             HasseiDate
    )


    -- 本支店分解実行伝票格納用ワークに待避
    INSERT INTO #M0000WK_MA_HonsitenBunkaiTaishou (
          DenpyouTani
        )
    SELECT
          DenpyouTani
    FROM #M0000WK_MA_HonsitenBunkai
    WHERE KariZigyoushoCD <> ZigyoushoCD
    OR    KasiZigyoushoCD <> ZigyoushoCD
    GROUP BY DenpyouTani

    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR
    
    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END
    

--2008/07/01 追加 N.Sasaki 開始 本支店分解する事業所がある場合のみ、入力事業所、明細の借方事業所、明細貸方事業所の本支店勘定科目マスタチェック追加

    SELECT
        @Kensuu = COUNT(*)
    FROM (
        SELECT
                HasseiDate         AS HasseiDate
            ,   ZigyoushoCD        AS ZigyoushoCD
        FROM #M0000WK_MA_HonsitenBunkai BUN
        INNER JOIN #M0000WK_MA_HonsitenBunkaiTaishou TAI
        ON BUN.DenpyouTani = TAI.DenpyouTani
        WHERE BUN.OperationComputerName    =   @p_UpdateComputerName
        AND   BUN.OperationUserID          =   @p_UpdateUserID
        AND   BUN.OperationJobID           =   @p_UpdateJobID
        UNION ALL
        SELECT
                HasseiDate         AS HasseiDate        
            ,   KariZigyoushoCD    AS ZigyoushoCD
        FROM #M0000WK_MA_HonsitenBunkai BUN
        INNER JOIN #M0000WK_MA_HonsitenBunkaiTaishou TAI
        ON BUN.DenpyouTani = TAI.DenpyouTani
        WHERE BUN.OperationComputerName    =   @p_UpdateComputerName
        AND   BUN.OperationUserID          =   @p_UpdateUserID
        AND   BUN.OperationJobID           =   @p_UpdateJobID
        AND   BUN.KariZigyoushoCD IS NOT NULL
        UNION ALL
        SELECT
                HasseiDate         AS HasseiDate
            ,   KasiZigyoushoCD    AS ZigyoushoCD
        FROM #M0000WK_MA_HonsitenBunkai BUN
        INNER JOIN #M0000WK_MA_HonsitenBunkaiTaishou TAI
        ON BUN.DenpyouTani = TAI.DenpyouTani
        WHERE BUN.OperationComputerName    =   @p_UpdateComputerName
        AND   BUN.OperationUserID          =   @p_UpdateUserID
        AND   BUN.OperationJobID           =   @p_UpdateJobID
        AND   BUN.KasiZigyoushoCD IS NOT NULL
    ) BUN                
    LEFT JOIN  M0000MS_MA_HonsitenKanjou MHSK
    ON  MHSK.ZigyoushoCD   =     BUN.ZigyoushoCD
    AND MHSK.KaiteiDate    <=    BUN.HasseiDate
    WHERE MHSK.ZigyoushoCD IS NULL

    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR

    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END
    
    IF @Kensuu <> 0
    BEGIN
        SET @po_ErrorKBN = 2
        SET @po_ErrorCD1 = '000'
        SET @po_ErrorCD2 = '00771'
        SET @po_ErrorProcName   = OBJECT_NAME(@@PROCID)
        SET @po_ErrorSummary    = ''
        SET @po_ErrorDetail     = ''
        SET @po_ErrorBtnKBN     = 0
        SET @po_ErrorBtnDefault = 0
        GOTO ERR_SHORI        
    END

--2008/07/01 追加 N.Sasaki 終了

    --本店集中制度摘要フラグを取得する
    SELECT @HontenShuutyuuFlag = HontenShuutyuuFlag
    FROM  M0000MS_MA_KaikeiControl
    WHERE ControlKey = 1

    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR
    
    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END
        
    --仕訳単位をセットする

    -- 変数初期化
    SET  @StartGyou = 0
    SET  @SiwakeTani = 0
    SET  @KariGoukeigaku = 0
    SET  @KasiGoukeigaku = 0
    SET  @DenpyouTani = 0
    SET  @DenpyouTani_BK = 0
        
    -- カーソル宣言
    DECLARE CURSIWAKE CURSOR FOR
    SELECT
          DenpyouTani
        , KariKingaku
        , KariZeigaku
        , KasiKingaku
        , KasiZeigaku
        , GyouNO
    FROM #M0000WK_MA_HonsitenBunkai
    ORDER BY   DenpyouTani, GyouNO

    -- CURSIWAKEをオープンし、先頭レコードをフェッチし、変数に待避する
    OPEN CURSIWAKE
    FETCH NEXT FROM CURSIWAKE INTO
          @DenpyouTani
        , @KariKingaku
        , @KariZeigaku
        , @KasiKingaku
        , @KasiZeigaku
        , @GyouNO

    SELECT   @FETCH_STATUS_CURSIWAKE = @@FETCH_STATUS
           , @ErrorCode              = @@ERROR

    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。   
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END

    IF  @FETCH_STATUS_CURSIWAKE <> 0
    BEGIN
        -- 引数に戻り値をセット
        SET @po_ErrorKBN        = 1                      --1：システムエラー
        SET @po_ErrorCD1        = '000'                  --会計は"000"で統一
        SET @po_ErrorCD2        = '00119'                --基幹会計は"00119"で統一
        SET @po_ErrorProcName   = OBJECT_NAME(@@PROCID)
        SET @po_ErrorSummary    = ''
        SET @po_ErrorDetail     = ''
        SET @po_ErrorBtnKBN     = 0
        SET @po_ErrorBtnDefault = 0
        GOTO ERR_SHORI
    END

    -- 変数.FETCH ステートメントステータス = 0 の間、ループ
    WHILE @@FETCH_STATUS = 0
    BEGIN
        IF @DenpyouTani <> @DenpyouTani_BK
        BEGIN
            SET  @StartGyou = 0
            SET  @SiwakeTani = 0
            SET  @KariGoukeigaku = 0
            SET  @KasiGoukeigaku = 0
        END
         
        IF @StartGyou = 0
        BEGIN
            SET @StartGyou  = @GyouNO
            SET @SiwakeTani = @SiwakeTani + 1
        END

        -- 変数に以下の値をセット
        SET @KariGoukeigaku = @KariGoukeigaku + ISNULL(@KariKingaku,0) + ISNULL(@KariZeigaku,0) 
        SET @KasiGoukeigaku = @KasiGoukeigaku + ISNULL(@KasiKingaku,0) + ISNULL(@KasiZeigaku,0) 

        -- 変数.借方合計額 = 変数.貸方合計額の場合、仕訳貸借判断ワークに、仕訳区分を更新。
        IF @KariGoukeigaku = @KasiGoukeigaku
        BEGIN
            UPDATE BUN SET
                  BUN.SiwakeTani          = @SiwakeTani
            FROM #M0000WK_MA_HonsitenBunkai BUN
            WHERE OperationComputerName = @p_UpdateComputerName
            AND   OperationUserID       = @p_UpdateUserID
            AND   OperationJobID        = @p_UpdateJobID
            AND   DenpyouTani           = @DenpyouTani
            AND   GyouNO                >= @StartGyou
            AND   GyouNO                <= @GyouNO
                
            -- @@ERROR を取得し、変数に保存する。
            SET @ErrorCode = @@ERROR
            
            -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
            IF  @ErrorCode <> 0
            BEGIN
                GOTO ERR_SHORI
            END

            -- 変数初期化
            SET  @StartGyou = 0
            SET  @KariGoukeigaku = 0
            SET  @KasiGoukeigaku = 0
        END
        
        --伝票単位待避
        SET @DenpyouTani_BK = @DenpyouTani
        
        FETCH NEXT FROM CURSIWAKE INTO
              @DenpyouTani
            , @KariKingaku
            , @KariZeigaku
            , @KasiKingaku
            , @KasiZeigaku
            , @GyouNO
        
        -- @@ERROR を取得し、変数に保存する。
        SET @ErrorCode = @@ERROR
        
        -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
        IF  @ErrorCode <> 0
        BEGIN
            GOTO ERR_SHORI
        END
    END
    -- カーソル：CURSIWAKEの開放
    CLOSE CURSIWAKE
    DEALLOCATE CURSIWAKE
    

    --マスタ待避
    -------------------------------------------------------------------------------------
    -- 本支店勘定マスタを待避                                                           -
    -------------------------------------------------------------------------------------
    INSERT INTO #M0000WK_MA_HonsitenBunkai_HonsitenKanjou (
              HasseiDate
            , ZigyoushoCD
            , KaiteiDate
            , BumonCD
            , HonshaZigyoushoCD
            , KanjouCD
            , HojoCD
            , UtiwakeCD
            )
    SELECT
              WHSK.HasseiDate
            , MHSK.ZigyoushoCD
            , MHSK.KaiteiDate
            , MHSK.BumonCD
            , MHSK.HonshaZigyoushoCD
            , MHSK.KanjouCD
            , MHSK.HojoCD
            , MHSK.UtiwakeCD
    FROM M0000MS_MA_HonsitenKanjou MHSK
    INNER JOIN (
        SELECT
            BUN.HasseiDate          AS HasseiDate
        ,   BUN.ZigyoushoCD         AS ZigyoushoCD
        ,   MAX(MHSK.KaiteiDate)     AS KaiteiDate
        FROM (

            SELECT
                    HasseiDate         AS HasseiDate
                ,   ZigyoushoCD        AS ZigyoushoCD
            FROM #M0000WK_MA_HonsitenBunkai
            WHERE OperationComputerName    =   @p_UpdateComputerName
            AND   OperationUserID          =   @p_UpdateUserID
            AND   OperationJobID           =   @p_UpdateJobID
            
            UNION 
            
            SELECT
                    HasseiDate         AS HasseiDate        
                ,   KariZigyoushoCD    AS ZigyoushoCD
            FROM #M0000WK_MA_HonsitenBunkai
            WHERE OperationComputerName    =   @p_UpdateComputerName
            AND   OperationUserID          =   @p_UpdateUserID
            AND   OperationJobID           =   @p_UpdateJobID
            AND   KariZigyoushoCD IS NOT NULL
            
            UNION 
            
            SELECT
                    HasseiDate         AS HasseiDate
                ,   KasiZigyoushoCD    AS ZigyoushoCD
            FROM #M0000WK_MA_HonsitenBunkai
            WHERE OperationComputerName    =   @p_UpdateComputerName
            AND   OperationUserID          =   @p_UpdateUserID
            AND   OperationJobID           =   @p_UpdateJobID
            AND   KasiZigyoushoCD IS NOT NULL
            
        ) BUN                
        INNER JOIN  M0000MS_MA_HonsitenKanjou MHSK
        ON  MHSK.ZigyoushoCD   =     BUN.ZigyoushoCD
        AND MHSK.KaiteiDate    <=    BUN.HasseiDate
        GROUP BY
                BUN.HasseiDate
            ,   BUN.ZigyoushoCD
        
    ) WHSK
    ON  WHSK.ZigyoushoCD = MHSK.ZigyoushoCD
    AND WHSK.KaiteiDate  = MHSK.KaiteiDate
    
    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR

    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END

    
    -------------------------------------------------------------------------------------
    -- 本支店勘定マスタを待避(本社事業所分)                                              -
    -------------------------------------------------------------------------------------
    INSERT INTO #M0000WK_MA_HonsitenBunkai_HonsitenKanjou (
              HasseiDate
            , ZigyoushoCD
            , KaiteiDate
            , BumonCD
            , HonshaZigyoushoCD
            , KanjouCD
            , HojoCD
            , UtiwakeCD
            )
    SELECT
              WHSK.HasseiDate
            , MHSK.ZigyoushoCD
            , MHSK.KaiteiDate
            , MHSK.BumonCD
            , MHSK.HonshaZigyoushoCD
            , MHSK.KanjouCD
            , MHSK.HojoCD
            , MHSK.UtiwakeCD
    FROM M0000MS_MA_HonsitenKanjou MHSK
    INNER JOIN (
        SELECT
                BUN.HasseiDate          AS HasseiDate
            ,   MHSK.ZigyoushoCD        AS ZigyoushoCD
            ,   MAX(MHSK.KaiteiDate)    AS KaiteiDate
        FROM (

            SELECT
                    BUN.HasseiDate         AS HasseiDate
--2008/05/14 N.Sasaki 変更 開始 本支店勘定科目ワーク抽出時の不具合修正
--                ,   HSK.ZigyoushoCD        AS ZigyoushoCD
                ,   HSK.HonshaZigyoushoCD  AS ZigyoushoCD
--2008/05/14 N.Sasaki 変更 終了
            FROM #M0000WK_MA_HonsitenBunkai BUN
            INNER JOIN #M0000WK_MA_HonsitenBunkai_HonsitenKanjou HSK
            ON  HSK.ZigyoushoCD   =     BUN.ZigyoushoCD
            AND HSK.HasseiDate    =     BUN.HasseiDate
            WHERE BUN.OperationComputerName    =   @p_UpdateComputerName
            AND   BUN.OperationUserID          =   @p_UpdateUserID
            AND   BUN.OperationJobID           =   @p_UpdateJobID
            
            UNION 
            
            SELECT
                    BUN.HasseiDate         AS HasseiDate
                ,   HSK.HonshaZigyoushoCD  AS ZigyoushoCD
            FROM #M0000WK_MA_HonsitenBunkai BUN
            INNER JOIN #M0000WK_MA_HonsitenBunkai_HonsitenKanjou HSK
            ON  HSK.ZigyoushoCD   =     BUN.KariZigyoushoCD
            AND HSK.HasseiDate    =     BUN.HasseiDate
            WHERE BUN.OperationComputerName    =   @p_UpdateComputerName
            AND   BUN.OperationUserID          =   @p_UpdateUserID
            AND   BUN.OperationJobID           =   @p_UpdateJobID
            AND   BUN.KariZigyoushoCD IS NOT NULL
            
            UNION 
            
            SELECT
                    BUN.HasseiDate         AS HasseiDate
                ,   HSK.HonshaZigyoushoCD  AS ZigyoushoCD
            FROM #M0000WK_MA_HonsitenBunkai BUN
            INNER JOIN #M0000WK_MA_HonsitenBunkai_HonsitenKanjou HSK
            ON  HSK.ZigyoushoCD   =     BUN.KasiZigyoushoCD
            AND HSK.HasseiDate    =     BUN.HasseiDate
            WHERE BUN.OperationComputerName    =   @p_UpdateComputerName
            AND   BUN.OperationUserID          =   @p_UpdateUserID
            AND   BUN.OperationJobID           =   @p_UpdateJobID
            AND   BUN.KasiZigyoushoCD IS NOT NULL
        ) BUN
        INNER JOIN  M0000MS_MA_HonsitenKanjou MHSK
        ON  MHSK.ZigyoushoCD   =     BUN.ZigyoushoCD
        AND MHSK.KaiteiDate    <=    BUN.HasseiDate
        LEFT JOIN #M0000WK_MA_HonsitenBunkai_HonsitenKanjou WK
        ON  WK.ZigyoushoCD = BUN.ZigyoushoCD
        AND WK.HasseiDate  = BUN.HasseiDate
        WHERE WK.ZigyoushoCD IS NULL
        GROUP BY
                BUN.HasseiDate
            ,   MHSK.ZigyoushoCD            
    ) WHSK
    ON  WHSK.ZigyoushoCD    = MHSK.ZigyoushoCD
    AND WHSK.KaiteiDate     = MHSK.KaiteiDate

    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR

    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END


    -------------------------------------------------------------------------------------
    -- 事業所マスタを待避                                                               -
    -------------------------------------------------------------------------------------
    INSERT INTO #M0000WK_MA_HonsitenBunkai_Zigyousho
    (
            HasseiDate
        ,   ZigyoushoCD
        ,   KaiteiDate
    )
    SELECT
            WZIG.HasseiDate
        ,   MZIG.ZigyoushoCD
        ,   MZIG.KaiteiDate
    FROM    M0000MS_MA_Zigyousho MZIG
    INNER JOIN
    (
        SELECT
                BUN.HasseiDate          AS HasseiDate
            ,   BUN.ZigyoushoCD         AS ZigyoushoCD
            ,   MAX(MZIG.KaiteiDate)    AS KaiteiDate
        FROM (
            SELECT
                    HasseiDate         AS HasseiDate
                ,   ZigyoushoCD        AS ZigyoushoCD
            FROM #M0000WK_MA_HonsitenBunkai
            WHERE OperationComputerName    =   @p_UpdateComputerName
            AND   OperationUserID          =   @p_UpdateUserID
            AND   OperationJobID           =   @p_UpdateJobID
            
            UNION 
            
            SELECT
                    HasseiDate         AS HasseiDate
                ,   KariZigyoushoCD    AS ZigyoushoCD
            FROM #M0000WK_MA_HonsitenBunkai
            WHERE OperationComputerName    =   @p_UpdateComputerName
            AND   OperationUserID          =   @p_UpdateUserID
            AND   OperationJobID           =   @p_UpdateJobID
            AND   KariZigyoushoCD IS NOT NULL
            
            UNION 
            
            SELECT
                    HasseiDate         AS HasseiDate
                ,   KasiZigyoushoCD    AS ZigyoushoCD
            FROM #M0000WK_MA_HonsitenBunkai
            WHERE OperationComputerName    =   @p_UpdateComputerName
            AND   OperationUserID          =   @p_UpdateUserID
            AND   OperationJobID           =   @p_UpdateJobID
            AND   KasiZigyoushoCD IS NOT NULL

            UNION 
            
            SELECT
                    BUN.HasseiDate         AS HasseiDate
                ,   HSK.HonshaZigyoushoCD  AS ZigyoushoCD
            FROM #M0000WK_MA_HonsitenBunkai BUN
            INNER JOIN #M0000WK_MA_HonsitenBunkai_HonsitenKanjou HSK
            ON  HSK.ZigyoushoCD   =     BUN.ZigyoushoCD
            AND HSK.HasseiDate    =     BUN.HasseiDate
            WHERE BUN.OperationComputerName    =   @p_UpdateComputerName
            AND   BUN.OperationUserID          =   @p_UpdateUserID
            AND   BUN.OperationJobID           =   @p_UpdateJobID
            AND   BUN.ZigyoushoCD IS NOT NULL

            UNION 

            SELECT
                    BUN.HasseiDate         AS HasseiDate
                ,   HSK.HonshaZigyoushoCD     AS ZigyoushoCD
            FROM #M0000WK_MA_HonsitenBunkai BUN
            INNER JOIN #M0000WK_MA_HonsitenBunkai_HonsitenKanjou HSK
            ON  HSK.ZigyoushoCD   =     BUN.KariZigyoushoCD
            AND HSK.HasseiDate    =     BUN.HasseiDate
            WHERE BUN.OperationComputerName    =   @p_UpdateComputerName
            AND   BUN.OperationUserID          =   @p_UpdateUserID
            AND   BUN.OperationJobID           =   @p_UpdateJobID
            AND   BUN.KariZigyoushoCD IS NOT NULL

            UNION 

            SELECT
                    BUN.HasseiDate         AS HasseiDate
                ,   HSK.HonshaZigyoushoCD  AS ZigyoushoCD
            FROM #M0000WK_MA_HonsitenBunkai BUN
            INNER JOIN #M0000WK_MA_HonsitenBunkai_HonsitenKanjou HSK
            ON  HSK.ZigyoushoCD   =     BUN.KasiZigyoushoCD
            AND HSK.HasseiDate    =     BUN.HasseiDate
            WHERE BUN.OperationComputerName    =   @p_UpdateComputerName
            AND   BUN.OperationUserID          =   @p_UpdateUserID
            AND   BUN.OperationJobID           =   @p_UpdateJobID
            AND   BUN.KasiZigyoushoCD IS NOT NULL
            
        ) BUN
        INNER JOIN M0000MS_MA_Zigyousho MZIG
        ON    MZIG.ZigyoushoCD             =    BUN.ZigyoushoCD
        AND   MZIG.KaiteiDate             <=    BUN.HasseiDate
        GROUP BY
                BUN.HasseiDate
            ,   BUN.ZigyoushoCD
    ) WZIG
    ON  WZIG.ZigyoushoCD    =   MZIG.ZigyoushoCD
    AND WZIG.KaiteiDate     =   MZIG.KaiteiDate

    -- @@ERRORを取得し、変数に保存する。
    SET @ErrorCode      = @@ERROR
    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END
    
    -------------------------------------------------------------------------------------
    -- 部門マスタを待避                                                                 -
    -------------------------------------------------------------------------------------
    INSERT INTO #M0000WK_MA_HonsitenBunkai_Bumon
    (
            HasseiDate
        ,   BumonCD
        ,   KaiteiDate
    )
    SELECT
            WBUM.HasseiDate
        ,   MBUM.BumonCD
        ,   MBUM.KaiteiDate
    FROM    M0000MS_MA_Bumon MBUM
    INNER JOIN
    (
        SELECT
                BUN.HasseiDate         AS HasseiDate
            ,   BUN.BumonCD            AS BumonCD
            ,   MAX(MBUM.KaiteiDate)   AS KaiteiDate
        FROM (
            SELECT
                    HasseiDate         AS HasseiDate
                ,   KariBumonCD        AS BumonCD
            FROM #M0000WK_MA_HonsitenBunkai
            WHERE OperationComputerName    =   @p_UpdateComputerName
            AND   OperationUserID          =   @p_UpdateUserID
            AND   OperationJobID           =   @p_UpdateJobID
            AND   KariBumonCD IS NOT NULL
            
            UNION
            
            SELECT
                    HasseiDate          AS HasseiDate
                ,   KasiBumonCD         AS BumonCD
            FROM #M0000WK_MA_HonsitenBunkai
            WHERE OperationComputerName    =   @p_UpdateComputerName
            AND   OperationUserID          =   @p_UpdateUserID
            AND   OperationJobID           =   @p_UpdateJobID
            AND   KasiBumonCD IS NOT NULL

            UNION

            SELECT
                    HasseiDate          AS HasseiDate
                ,   BumonCD             AS BumonCD
            FROM #M0000WK_MA_HonsitenBunkai_HonsitenKanjou
            
        ) BUN
        INNER JOIN M0000MS_MA_Bumon MBUM
        ON  MBUM.BumonCD                   =   BUN.BumonCD
        AND MBUM.KaiteiDate                <=  BUN.HasseiDate
        GROUP BY
                BUN.HasseiDate
            ,   BUN.BumonCD
    ) WBUM
    ON  WBUM.BumonCD    =    MBUM.BumonCD
    AND WBUM.KaiteiDate =    MBUM.KaiteiDate

    -- @@ERRORを取得し、変数に保存する。
    SET @ErrorCode      = @@ERROR
    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END

    -------------------------------------------------------------------------------------
    -- 総勘定科目コントロールマスタを待避                                               -
    -------------------------------------------------------------------------------------
    INSERT INTO #M0000WK_MA_HonsitenBunkai_KanjouControl
    (
            HasseiDate
        ,   KaiteiDate
        ,   KaribaraiZeiCD
        ,   KariukeZeiCD
        ,   ShokutiCD
    )
    SELECT
            WKAC.HasseiDate
        ,   MKAC.KaiteiDate
        ,   MKAC.KaribaraiZeiCD
        ,   MKAC.KariukeZeiCD
        ,   MKAC.ShokutiCD
    FROM    M0000MS_MA_KanjouControl MKAC
    INNER JOIN
    (
        SELECT
                BUN.HasseiDate          AS HasseiDate
            ,   MAX(MKAC.KaiteiDate)    AS KaiteiDate
        FROM #M0000WK_MA_HonsitenBunkai BUN
        INNER JOIN M0000MS_MA_KanjouControl MKAC
        ON MKAC.KaiteiDate  <=   BUN.HasseiDate
        WHERE OperationComputerName    =   @p_UpdateComputerName
        AND   OperationUserID          =   @p_UpdateUserID
        AND   OperationJobID           =   @p_UpdateJobID
        GROUP BY
                BUN.HasseiDate
    ) WKAC
    ON  WKAC.KaiteiDate =   MKAC.KaiteiDate

    -- @@ERRORを取得し、変数に保存する。
    SET @ErrorCode      = @@ERROR
    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END

    -------------------------------------------------------------------------------------
    -- 総勘定科目マスタを待避                                                           -
    -------------------------------------------------------------------------------------
    INSERT INTO #M0000WK_MA_HonsitenBunkai_Kanjou
    (
            HasseiDate
        ,   KanjouCD
        ,   KaiteiDate
        ,   KanjouName
        ,   TaishakuKBN
        ,   TorihikisakiKBN
        ,   HojoKBN
        ,   BumonKBN
        ,   KariZeiKBN
        ,   KasiZeiKBN
        ,   KariZeikomiKBN
        ,   KasiZeikomiKBN
    )
    SELECT
            WKAN.HasseiDate
        ,   MKAN.KanjouCD
        ,   MKAN.KaiteiDate
        ,   MKAN.KanjouName
        ,   MKAN.TaishakuKBN
        ,   MKAN.TorihikisakiKBN
        ,   MKAN.HojoKBN
        ,   MKAN.BumonKBN
        ,   MKAN.KariZeiKBN
        ,   MKAN.KasiZeiKBN
        ,   MKAN.KariZeikomiKBN
        ,   MKAN.KasiZeikomiKBN
    FROM       M0000MS_MA_Kanjou MKAN
    INNER JOIN
    (
        SELECT
                BUN.HasseiDate          AS HasseiDate
            ,   MKAN.KanjouCD           AS KanjouCD
            ,   MAX(MKAN.KaiteiDate)    AS KaiteiDate
        FROM    M0000MS_MA_Kanjou MKAN
        INNER JOIN
        (
            SELECT
                    HasseiDate         AS HasseiDate
                ,   KariKanjouCD       AS KanjouCD
            FROM #M0000WK_MA_HonsitenBunkai
            WHERE OperationComputerName    =   @p_UpdateComputerName
            AND   OperationUserID          =   @p_UpdateUserID
            AND   OperationJobID           =   @p_UpdateJobID
            AND   KariKanjouCD IS NOT NULL
            
            UNION

            SELECT
                    HasseiDate         AS HasseiDate
                ,   KasiKanjouCD       AS KanjouCD
            FROM #M0000WK_MA_HonsitenBunkai
            WHERE OperationComputerName    =   @p_UpdateComputerName
            AND   OperationUserID          =   @p_UpdateUserID
            AND   OperationJobID           =   @p_UpdateJobID
            AND   KasiKanjouCD IS NOT NULL

            UNION

            SELECT
                    HasseiDate         AS HasseiDate
                ,   KaribaraiZeiCD     AS KanjouCD
            FROM #M0000WK_MA_HonsitenBunkai_KanjouControl

            UNION

            SELECT
                    HasseiDate         AS HasseiDate
                ,   KariukeZeiCD       AS KanjouCD
            FROM #M0000WK_MA_HonsitenBunkai_KanjouControl

            UNION

            SELECT
                    HasseiDate         AS HasseiDate
                ,   ShokutiCD          AS KanjouCD
            FROM #M0000WK_MA_HonsitenBunkai_KanjouControl
            
            UNION

            SELECT
                    HasseiDate          AS HasseiDate
                ,   KanjouCD            AS KanjouCD
            FROM #M0000WK_MA_HonsitenBunkai_HonsitenKanjou
            
        ) BUN
        ON    BUN.KanjouCD     =    MKAN.KanjouCD
        WHERE MKAN.KaiteiDate   <=   BUN.HasseiDate
        GROUP BY
                BUN.HasseiDate
            ,   MKAN.KanjouCD
    ) WKAN
    ON  WKAN.KanjouCD       =   MKAN.KanjouCD
    AND WKAN.KaiteiDate     =   MKAN.KaiteiDate

    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR

    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END

    -------------------------------------------------------------------------------------
    -- 総勘定科目コントロールマスタを更新                                                -
    -------------------------------------------------------------------------------------
    --仮払消費税コード情報更新
    UPDATE CTL SET
          CTL.KaribaraiZeiKaiteiDate  = KAN.KaiteiDate
        , CTL.KaribaraiZeiTaishakuKBN = KAN.TaishakuKBN
    FROM #M0000WK_MA_HonsitenBunkai_KanjouControl CTL
    INNER JOIN #M0000WK_MA_HonsitenBunkai_Kanjou KAN
    ON  KAN.HasseiDate = CTL.HasseiDate
    AND KAN.KanjouCD   = CTL.KaribaraiZeiCD

    --仮受消費税コード情報更新
    UPDATE CTL SET
          CTL.KariukeZeiKaiteiDate  = KAN.KaiteiDate
        , CTL.KariukeZeiTaishakuKBN = KAN.TaishakuKBN
    FROM #M0000WK_MA_HonsitenBunkai_KanjouControl CTL
    INNER JOIN #M0000WK_MA_HonsitenBunkai_Kanjou KAN
    ON  KAN.HasseiDate = CTL.HasseiDate
    AND KAN.KanjouCD   = CTL.KariukeZeiCD

    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR

    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END


    
    -------------------------------------------------------------------------------------
    -- 補助マスタを待避                                                                 -
    -------------------------------------------------------------------------------------
    INSERT INTO #M0000WK_MA_HonsitenBunkai_Hojo
    (
            HasseiDate
        ,   KanjouCD
        ,   HojoCD
        ,   KaiteiDate
        ,   UtiwakeKBN
    )
    SELECT
            WHOJ.HasseiDate
        ,   MHOJ.KanjouCD
        ,   MHOJ.HojoCD
        ,   MHOJ.KaiteiDate
        ,   MHOJ.UtiwakeKBN
    FROM    M0000MS_MA_Hojo MHOJ
    INNER JOIN
    (
        SELECT
                BUN.HasseiDate          AS HasseiDate
            ,   BUN.KanjouCD            AS KanjouCD
            ,   BUN.HojoCD              AS HojoCD
            ,   MAX(MHOJ.KaiteiDate)    AS KaiteiDate
        FROM    M0000MS_MA_Hojo MHOJ
        INNER JOIN
        (
            SELECT
                    HasseiDate        AS HasseiDate
                ,   KariKanjouCD      AS KanjouCD
                ,   KariHojoCD        AS HojoCD
            FROM #M0000WK_MA_HonsitenBunkai
            WHERE OperationComputerName    =   @p_UpdateComputerName
            AND   OperationUserID          =   @p_UpdateUserID
            AND   OperationJobID           =   @p_UpdateJobID
            
            UNION
            
            SELECT
                    HasseiDate        AS HasseiDate
                ,   KasiKanjouCD      AS KanjouCD
                ,   KasiHojoCD        AS HojoCD
            FROM #M0000WK_MA_HonsitenBunkai
            WHERE OperationComputerName    =   @p_UpdateComputerName
            AND   OperationUserID          =   @p_UpdateUserID
            AND   OperationJobID           =   @p_UpdateJobID
            
            UNION

            SELECT
                    HasseiDate        AS HasseiDate
                ,   KanjouCD          AS KanjouCD
                ,   HojoCD            AS HojoCD
            FROM #M0000WK_MA_HonsitenBunkai_HonsitenKanjou


        ) BUN
        ON  BUN.KanjouCD       =   MHOJ.KanjouCD
        AND BUN.HojoCD         =   MHOJ.HojoCD
        WHERE MHOJ.KaiteiDate    <=  BUN.HasseiDate
        GROUP BY
                BUN.HasseiDate
            ,   BUN.KanjouCD
            ,   BUN.HojoCD
    ) WHOJ
    ON  WHOJ.KanjouCD   =    MHOJ.KanjouCD
    AND WHOJ.HojoCD     =    MHOJ.HojoCD
    AND WHOJ.KaiteiDate =    MHOJ.KaiteiDate


    -- @@ERRORを取得し、変数に保存する。
    SET @ErrorCode      = @@ERROR
    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END


    -------------------------------------------------------------------------------------
    -- 補助内訳マスタを待避                                                                 -
    -------------------------------------------------------------------------------------
    INSERT INTO #M0000WK_MA_HonsitenBunkai_Utiwake
    (
            HasseiDate
        ,   KanjouCD
        ,   HojoCD
        ,   UtiwakeCD
        ,   KaiteiDate
    )
    SELECT
            WUTI.HasseiDate
        ,   MUTI.KanjouCD
        ,   MUTI.HojoCD
        ,   MUTI.UtiwakeCD
        ,   MUTI.KaiteiDate
    FROM    M0000MS_MA_Utiwake MUTI
    INNER JOIN
    (
        SELECT
                BUN.HasseiDate         AS HasseiDate
            ,   BUN.KanjouCD           AS KanjouCD
            ,   BUN.HojoCD             AS HojoCD
            ,   BUN.UtiwakeCD          AS UtiwakeCD
            ,   MAX(MUTI.KaiteiDate)   AS KaiteiDate
        FROM    M0000MS_MA_Utiwake MUTI
        INNER JOIN
        (
            SELECT
                    HasseiDate     AS HasseiDate
                ,   KariKanjouCD   AS KanjouCD
                ,   KariHojoCD     AS HojoCD
                ,   KariUtiwakeCD  AS UtiwakeCD
            FROM #M0000WK_MA_HonsitenBunkai
            WHERE OperationComputerName    =   @p_UpdateComputerName
            AND   OperationUserID          =   @p_UpdateUserID
            AND   OperationJobID           =   @p_UpdateJobID

            UNION

            SELECT
                    HasseiDate     AS HasseiDate
                ,   KasiKanjouCD   AS KanjouCD
                ,   KasiHojoCD     AS HojoCD
                ,   KasiUtiwakeCD  AS UtiwakeCD
            FROM #M0000WK_MA_HonsitenBunkai
            WHERE OperationComputerName    =   @p_UpdateComputerName
            AND   OperationUserID          =   @p_UpdateUserID
            AND   OperationJobID           =   @p_UpdateJobID

            UNION

            SELECT
                    HasseiDate        AS HasseiDate
                ,   KanjouCD          AS KanjouCD
                ,   HojoCD            AS HojoCD
                ,   UtiwakeCD         AS UtiwakeCD
            FROM #M0000WK_MA_HonsitenBunkai_HonsitenKanjou

        ) BUN
        ON  BUN.KanjouCD   =   MUTI.KanjouCD
        AND BUN.HojoCD     =   MUTI.HojoCD
        AND BUN.UtiwakeCD  =   MUTI.UtiwakeCD
        WHERE MUTI.KaiteiDate   <=    BUN.HasseiDate
        GROUP BY
                BUN.HasseiDate
            ,   BUN.KanjouCD
            ,   BUN.HojoCD
            ,   BUN.UtiwakeCD
    ) WUTI
    ON  WUTI.KanjouCD       =   MUTI.KanjouCD
    AND WUTI.HojoCD         =   MUTI.HojoCD
    AND WUTI.UtiwakeCD      =   MUTI.UtiwakeCD
    AND WUTI.KaiteiDate     =   MUTI.KaiteiDate

    -- @@ERRORを取得し、変数に保存する。
    SET @ErrorCode      = @@ERROR
    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END

    -------------------------------------------------------------------------------------
    -- 分解処理                                                                         -
    -------------------------------------------------------------------------------------
    
    --  画面借方側の情報で処理
    INSERT INTO #M0000WK_MA_HonsitenBunkaiKakunou (
           DenpyouTani
        ,  SiwakeTani
        ,  HasseiDate
        ,  GyouNO
        ,  KariNyuuryokuKBN
        ,  KariZigyoushoCD
        ,  KariBumonCD
        ,  KariTorihikisakiCD
        ,  KariKanjouCD
        ,  KariHojoCD
        ,  KariUtiwakeCD
        ,  KariKingaku
        ,  KariZeigaku
        ,  KariHonshaKBN
        ,  KasiNyuuryokuKBN
        ,  KasiZigyoushoCD
        ,  KasiBumonCD
        ,  KasiTorihikisakiCD
        ,  KasiKanjouCD
        ,  KasiHojoCD
        ,  KasiUtiwakeCD
        ,  KasiKingaku
        ,  KasiZeigaku
        ,  KasiHonshaKBN
--2011/04/04 修正 OSE.Ikoma 開始 (M04583)入力事業所で発生した本店勘定については集約しないよう修正
        ,  NyuuryokuZigyoushoCD 
--2011/04/04 修正 OSE.Ikoma 終了 (M04583)入力事業所で発生した本店勘定については集約しないよう修正
    )
    SELECT
           BUN.DenpyouTani
        ,  BUN.SiwakeTani
        ,  BUN.HasseiDate
        ,  BUN.GyouNO
        ,  0
        ,  BUN.KariZigyoushoCD
        ,  BUN.KariBumonCD
        ,  BUN.KariTorihikisakiCD
        ,  BUN.KariKanjouCD
        ,  BUN.KariHojoCD
        ,  BUN.KariUtiwakeCD
        ,  BUN.KariKingaku
        ,  BUN.KariZeigaku
        ,  CASE WHEN KRHZG.ZigyoushoCD = KRHZG.HonshaZigyoushoCD THEN 1 ELSE 0 END
        ,  1
        ,  BUN.KariZigyoushoCD
        ,  KRHZG.BumonCD
        ,  NULL
        ,  HDZIG.KanjouCD
        ,  HDZIG.HojoCD
        ,  HDZIG.UtiwakeCD
        ,  BUN.KariKingaku + ISNULL(BUN.KariZeigaku,0)
        ,  NULL
        ,  CASE WHEN KRHZG.ZigyoushoCD = KRHZG.HonshaZigyoushoCD THEN 1 ELSE 0 END
--2011/04/04 修正 OSE.Ikoma 開始 (M04583)入力事業所で発生した本店勘定については集約しないよう修正
        ,  BUN.ZigyoushoCD                -- 入力事業所
--2011/04/04 修正 OSE.Ikoma 終了 (M04583)入力事業所で発生した本店勘定については集約しないよう修正
    FROM       #M0000WK_MA_HonsitenBunkai BUN
    INNER JOIN #M0000WK_MA_HonsitenBunkaiTaishou HBT
    ON    HBT.DenpyouTani     = BUN.DenpyouTani
    --<借方事業所>
    LEFT JOIN #M0000WK_MA_HonsitenBunkai_HonsitenKanjou KRHZG
    ON    KRHZG.HasseiDate    = BUN.HasseiDate
    AND   KRHZG.ZigyoushoCD   = BUN.KariZigyoushoCD
    --<入力事業所 OR 借方事業所の本社事業所>
    LEFT JOIN #M0000WK_MA_HonsitenBunkai_HonsitenKanjou HDZIG
    ON    HDZIG.HasseiDate    = (CASE WHEN @HontenShuutyuuFlag = 0 THEN BUN.HasseiDate ELSE KRHZG.HasseiDate END)
    AND   HDZIG.ZigyoushoCD   = (CASE WHEN @HontenShuutyuuFlag = 0 THEN BUN.ZigyoushoCD ELSE KRHZG.HonshaZigyoushoCD END)
    WHERE BUN.OperationComputerName    =   @p_UpdateComputerName
    AND   BUN.OperationUserID          =   @p_UpdateUserID
    AND   BUN.OperationJobID           =   @p_UpdateJobID        
    AND   BUN.KariKanjouCD IS NOT NULL
    ORDER BY BUN.GyouNO

    -- @@ERRORを取得し、変数に保存する。
    SET @ErrorCode      = @@ERROR
    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END

    --  画面貸方側の情報で処理
    INSERT INTO #M0000WK_MA_HonsitenBunkaiKakunou (
           DenpyouTani
        ,  SiwakeTani
        ,  HasseiDate
        ,  GyouNO
        ,  KariNyuuryokuKBN
        ,  KariZigyoushoCD
        ,  KariBumonCD
        ,  KariTorihikisakiCD
        ,  KariKanjouCD
        ,  KariHojoCD
        ,  KariUtiwakeCD
        ,  KariKingaku
        ,  KariZeigaku
        ,  KariHonshaKBN
        ,  KasiNyuuryokuKBN
        ,  KasiZigyoushoCD
        ,  KasiBumonCD
        ,  KasiTorihikisakiCD
        ,  KasiKanjouCD
        ,  KasiHojoCD
        ,  KasiUtiwakeCD
        ,  KasiKingaku
        ,  KasiZeigaku
        ,  KasiHonshaKBN
--2011/04/04 修正 OSE.Ikoma 開始 (M04583)入力事業所で発生した本店勘定については集約しないよう修正
        ,  NyuuryokuZigyoushoCD 
--2011/04/04 修正 OSE.Ikoma 終了 (M04583)入力事業所で発生した本店勘定については集約しないよう修正
    )
    SELECT
           BUN.DenpyouTani
        ,  BUN.SiwakeTani
        ,  BUN.HasseiDate
        ,  BUN.GyouNO
        ,  1
        ,  BUN.KasiZigyoushoCD
        ,  KSHZG.BumonCD
        ,  NULL
        ,  HDZIG.KanjouCD
        ,  HDZIG.HojoCD
        ,  HDZIG.UtiwakeCD
        ,  BUN.KasiKingaku + ISNULL(BUN.KasiZeigaku,0)
        ,  NULL
        ,  CASE WHEN KSHZG.ZigyoushoCD = KSHZG.HonshaZigyoushoCD THEN 1 ELSE 0 END
        ,  0
        ,  BUN.KasiZigyoushoCD
        ,  BUN.KasiBumonCD
        ,  BUN.KasiTorihikisakiCD
        ,  BUN.KasiKanjouCD
        ,  BUN.KasiHojoCD
        ,  BUN.KasiUtiwakeCD
        ,  BUN.KasiKingaku
        ,  BUN.KasiZeigaku
        ,  CASE WHEN KSHZG.ZigyoushoCD = KSHZG.HonshaZigyoushoCD THEN 1 ELSE 0 END
--2011/04/04 修正 OSE.Ikoma 開始 (M04583)入力事業所で発生した本店勘定については集約しないよう修正
        ,  BUN.ZigyoushoCD 
--2011/04/04 修正 OSE.Ikoma 終了 (M04583)入力事業所で発生した本店勘定については集約しないよう修正
    FROM #M0000WK_MA_HonsitenBunkai BUN             --画面内容
    INNER JOIN #M0000WK_MA_HonsitenBunkaiTaishou HBT
    ON    HBT.DenpyouTani     = BUN.DenpyouTani
    --<貸方事業所>
    LEFT JOIN #M0000WK_MA_HonsitenBunkai_HonsitenKanjou KSHZG        --本支店勘定登録マスタ
    ON    KSHZG.HasseiDate    = BUN.HasseiDate
    AND   KSHZG.ZigyoushoCD   = BUN.KasiZigyoushoCD
    --<入力事業所 OR 貸方事業所の本社事業所>
    LEFT JOIN #M0000WK_MA_HonsitenBunkai_HonsitenKanjou HDZIG        --本支店勘定登録マスタ
    ON    HDZIG.HasseiDate    = (CASE WHEN @HontenShuutyuuFlag = 0 THEN BUN.HasseiDate ELSE KSHZG.HasseiDate END)
    AND   HDZIG.ZigyoushoCD   = (CASE WHEN @HontenShuutyuuFlag = 0 THEN BUN.ZigyoushoCD ELSE KSHZG.HonshaZigyoushoCD END)
    WHERE BUN.OperationComputerName    =   @p_UpdateComputerName
    AND   BUN.OperationUserID          =   @p_UpdateUserID
    AND   BUN.OperationJobID           =   @p_UpdateJobID
    AND   BUN.KasiKanjouCD IS NOT NULL
    ORDER BY BUN.GyouNO
    
    -- @@ERRORを取得し、変数に保存する。
    SET @ErrorCode      = @@ERROR
    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END


    --事業所、もしくは借方事業所の本社事業所分(本店集中制度適用フラグにより場合分け)を借方側の情報で作成
    INSERT INTO #M0000WK_MA_HonsitenBunkaiKakunou (
           DenpyouTani
        ,  SiwakeTani
        ,  HasseiDate
        ,  GyouNO
        ,  KariNyuuryokuKBN
        ,  KariZigyoushoCD
        ,  KariBumonCD
        ,  KariTorihikisakiCD
        ,  KariKanjouCD
        ,  KariHojoCD
        ,  KariUtiwakeCD
        ,  KariKingaku
        ,  KariZeigaku
        ,  KariHonshaKBN
        ,  KasiNyuuryokuKBN
        ,  KasiZigyoushoCD
        ,  KasiBumonCD
        ,  KasiTorihikisakiCD
        ,  KasiKanjouCD
        ,  KasiHojoCD
        ,  KasiUtiwakeCD
        ,  KasiKingaku
        ,  KasiZeigaku
        ,  KasiHonshaKBN
--2011/04/04 修正 OSE.Ikoma 開始 (M04583)入力事業所で発生した本店勘定については集約しないよう修正
        ,  NyuuryokuZigyoushoCD 
--2011/04/04 修正 OSE.Ikoma 終了 (M04583)入力事業所で発生した本店勘定については集約しないよう修正
    )
    SELECT
           BUN.DenpyouTani
        ,  BUN.SiwakeTani
        ,  BUN.HasseiDate
        ,  BUN.GyouNO
        ,  1
        ,  CASE WHEN @HontenShuutyuuFlag = 0 THEN HDZIG.ZigyoushoCD ELSE KRHZG.ZigyoushoCD END
        ,  CASE WHEN @HontenShuutyuuFlag = 0 THEN HDZIG.BumonCD   ELSE KRHZG.BumonCD        END
        ,  NULL
        ,  KRZIG.KanjouCD
        ,  KRZIG.HojoCD
        ,  KRZIG.UtiwakeCD
        ,  BUN.KariKingaku + ISNULL(BUN.KariZeigaku,0)
        ,  NULL
        ,  CASE WHEN @HontenShuutyuuFlag = 0 THEN CASE WHEN HDZIG.ZigyoushoCD = HDZIG.HonshaZigyoushoCD THEN 1 ELSE 0 END ELSE 1 END
        ,  1
        ,  CASE WHEN @HontenShuutyuuFlag = 0 THEN HDZIG.ZigyoushoCD ELSE KRHZG.ZigyoushoCD END
        ,  CASE WHEN @HontenShuutyuuFlag = 0 THEN HDZIG.BumonCD   ELSE KRHZG.BumonCD        END
        ,  NULL
        ,  CASE WHEN @HontenShuutyuuFlag = 0 THEN HDZIG.KanjouCD ELSE HDHZG.KanjouCD END
        ,  CASE WHEN @HontenShuutyuuFlag = 0 THEN HDZIG.HojoCD ELSE HDHZG.HojoCD END
        ,  CASE WHEN @HontenShuutyuuFlag = 0 THEN HDZIG.UtiwakeCD ELSE HDHZG.UtiwakeCD END
        ,  BUN.KariKingaku + ISNULL(BUN.KariZeigaku,0)
        ,  NULL
        ,  CASE WHEN @HontenShuutyuuFlag = 0 THEN CASE WHEN HDZIG.ZigyoushoCD = HDZIG.HonshaZigyoushoCD THEN 1 ELSE 0 END ELSE 1 END
--2011/04/04 修正 OSE.Ikoma 開始 (M04583)入力事業所で発生した本店勘定については集約しないよう修正
        ,  BUN.ZigyoushoCD 
--2011/04/04 修正 OSE.Ikoma 終了 (M04583)入力事業所で発生した本店勘定については集約しないよう修正
    FROM #M0000WK_MA_HonsitenBunkai BUN             --画面内容
    INNER JOIN #M0000WK_MA_HonsitenBunkaiTaishou HBT
    ON    HBT.DenpyouTani   = BUN.DenpyouTani
    --<借方事業所>
    LEFT JOIN #M0000WK_MA_HonsitenBunkai_HonsitenKanjou KRZIG        --本支店勘定登録マスタ
    ON    KRZIG.HasseiDate  = BUN.HasseiDate
    AND   KRZIG.ZigyoushoCD = BUN.KariZigyoushoCD
    --<借方事業所の本社事業所>
    LEFT JOIN #M0000WK_MA_HonsitenBunkai_HonsitenKanjou KRHZG        --本支店勘定登録マスタ
    ON    KRHZG.HasseiDate  = KRZIG.HasseiDate
    AND   KRHZG.ZigyoushoCD = KRZIG.HonshaZigyoushoCD
    --<ヘッダ事業所>
    LEFT JOIN #M0000WK_MA_HonsitenBunkai_HonsitenKanjou HDZIG        --本支店勘定登録マスタ
    ON    HDZIG.HasseiDate  = BUN.HasseiDate
    AND   HDZIG.ZigyoushoCD = BUN.ZigyoushoCD
    --<ヘッダ事業所の本社事業所>
    LEFT JOIN #M0000WK_MA_HonsitenBunkai_HonsitenKanjou HDHZG        --本支店勘定登録マスタ
    ON    HDHZG.HasseiDate  = HDZIG.HasseiDate
    AND   HDHZG.ZigyoushoCD = HDZIG.HonshaZigyoushoCD
    WHERE BUN.OperationComputerName    =   @p_UpdateComputerName
    AND   BUN.OperationUserID          =   @p_UpdateUserID
    AND   BUN.OperationJobID           =   @p_UpdateJobID
    AND   BUN.KariKanjouCD IS NOT NULL
    ORDER BY BUN.GyouNO
    
    -- @@ERRORを取得し、変数に保存する。
    SET @ErrorCode      = @@ERROR
    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END
    
    
    
    --事業所、もしくは貸方事業所の本社事業所分(本店集中制度適用フラグにより場合分け)を貸方側の情報で作成
    INSERT INTO #M0000WK_MA_HonsitenBunkaiKakunou (
           DenpyouTani
        ,  SiwakeTani
        ,  HasseiDate
        ,  GyouNO
        ,  KariNyuuryokuKBN
        ,  KariZigyoushoCD
        ,  KariBumonCD
        ,  KariTorihikisakiCD
        ,  KariKanjouCD
        ,  KariHojoCD
        ,  KariUtiwakeCD
        ,  KariKingaku
        ,  KariZeigaku
        ,  KariHonshaKBN
        ,  KasiNyuuryokuKBN
        ,  KasiZigyoushoCD
        ,  KasiBumonCD
        ,  KasiTorihikisakiCD
        ,  KasiKanjouCD
        ,  KasiHojoCD   
        ,  KasiUtiwakeCD
        ,  KasiKingaku
        ,  KasiZeigaku
        ,  KasiHonshaKBN
--2011/04/04 修正 OSE.Ikoma 開始 (M04583)入力事業所で発生した本店勘定については集約しないよう修正
        ,  NyuuryokuZigyoushoCD 
--2011/04/04 修正 OSE.Ikoma 終了 (M04583)入力事業所で発生した本店勘定については集約しないよう修正
    )
    SELECT
           BUN.DenpyouTani
        ,  BUN.SiwakeTani
        ,  BUN.HasseiDate
        ,  BUN.GyouNO
        ,  1
        ,  CASE WHEN @HontenShuutyuuFlag = 0 THEN HDZIG.ZigyoushoCD ELSE KSHZG.ZigyoushoCD END
        ,  CASE WHEN @HontenShuutyuuFlag = 0 THEN HDZIG.BumonCD   ELSE KSHZG.BumonCD        END
        ,  NULL
        ,  CASE WHEN @HontenShuutyuuFlag = 0 THEN HDZIG.KanjouCD ELSE HDHZG.KanjouCD END
        ,  CASE WHEN @HontenShuutyuuFlag = 0 THEN HDZIG.HojoCD ELSE HDHZG.HojoCD END
        ,  CASE WHEN @HontenShuutyuuFlag = 0 THEN HDZIG.UtiwakeCD ELSE HDHZG.UtiwakeCD END
        ,  BUN.KasiKingaku + ISNULL(BUN.KasiZeigaku,0)
        ,  NULL
        ,  CASE WHEN @HontenShuutyuuFlag = 0 THEN CASE WHEN HDZIG.ZigyoushoCD = HDZIG.HonshaZigyoushoCD THEN 1 ELSE 0 END ELSE 1 END
        ,  1
        ,  CASE WHEN @HontenShuutyuuFlag = 0 THEN HDZIG.ZigyoushoCD ELSE KSHZG.ZigyoushoCD END
        ,  CASE WHEN @HontenShuutyuuFlag = 0 THEN HDZIG.BumonCD   ELSE KSHZG.BumonCD        END
        ,  NULL
        ,  KSZIG.KanjouCD
        ,  KSZIG.HojoCD
        ,  KSZIG.UtiwakeCD
        ,  BUN.KasiKingaku + ISNULL(BUN.KasiZeigaku,0)
        ,  NULL
        ,  CASE WHEN @HontenShuutyuuFlag = 0 THEN CASE WHEN HDZIG.ZigyoushoCD = HDZIG.HonshaZigyoushoCD THEN 1 ELSE 0 END ELSE 1 END
--2011/04/04 修正 OSE.Ikoma 開始 (M04583)入力事業所で発生した本店勘定については集約しないよう修正
        ,  BUN.ZigyoushoCD 
--2011/04/04 修正 OSE.Ikoma 終了 (M04583)入力事業所で発生した本店勘定については集約しないよう修正
    FROM #M0000WK_MA_HonsitenBunkai BUN             --画面内容
    INNER JOIN #M0000WK_MA_HonsitenBunkaiTaishou HBT
    ON    HBT.DenpyouTani   = BUN.DenpyouTani
    --<貸方事業所>
    LEFT JOIN #M0000WK_MA_HonsitenBunkai_HonsitenKanjou KSZIG        --本支店勘定登録マスタ
    ON    KSZIG.HasseiDate  = BUN.HasseiDate
    AND   KSZIG.ZigyoushoCD = BUN.KasiZigyoushoCD
    --<貸方事業所の本社事業所>
    LEFT JOIN #M0000WK_MA_HonsitenBunkai_HonsitenKanjou KSHZG        --本支店勘定登録マスタ
    ON    KSHZG.HasseiDate  = KSZIG.HasseiDate
    AND   KSHZG.ZigyoushoCD = KSZIG.HonshaZigyoushoCD
    --<ヘッダ事業所>
    LEFT JOIN #M0000WK_MA_HonsitenBunkai_HonsitenKanjou HDZIG        --本支店勘定登録マスタ
    ON    HDZIG.HasseiDate  = BUN.HasseiDate
    AND   HDZIG.ZigyoushoCD = BUN.ZigyoushoCD
    --<ヘッダ事業所の本社事業所>
    LEFT JOIN #M0000WK_MA_HonsitenBunkai_HonsitenKanjou HDHZG        --本支店勘定登録マスタ
    ON    HDHZG.HasseiDate  = HDZIG.HasseiDate
    AND   HDHZG.ZigyoushoCD = HDZIG.HonshaZigyoushoCD
    WHERE BUN.OperationComputerName    =   @p_UpdateComputerName
    AND   BUN.OperationUserID          =   @p_UpdateUserID
    AND   BUN.OperationJobID           =   @p_UpdateJobID
    AND   BUN.KasiKanjouCD IS NOT NULL
    ORDER BY BUN.GyouNO
    
    -- @@ERRORを取得し、変数に保存する。
    SET @ErrorCode      = @@ERROR
    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END
    
    
    
    --画面本支店集中区分:適用する時のみ
    IF @HontenShuutyuuFlag = 1
    BEGIN
        --事業所の本社事業所分を借方側の情報で作成
        INSERT INTO #M0000WK_MA_HonsitenBunkaiKakunou (
               DenpyouTani
            ,  SiwakeTani
            ,  HasseiDate
            ,  GyouNO
            ,  KariNyuuryokuKBN
            ,  KariZigyoushoCD
            ,  KariBumonCD
            ,  KariTorihikisakiCD
            ,  KariKanjouCD
            ,  KariHojoCD
            ,  KariUtiwakeCD
            ,  KariKingaku
            ,  KariZeigaku
            ,  KariHonshaKBN
            ,  KasiNyuuryokuKBN
            ,  KasiZigyoushoCD
            ,  KasiBumonCD
            ,  KasiTorihikisakiCD
            ,  KasiKanjouCD
            ,  KasiHojoCD   
            ,  KasiUtiwakeCD
            ,  KasiKingaku
            ,  KasiZeigaku
            ,  KasiHonshaKBN
--2011/04/04 修正 OSE.Ikoma 開始 (M04583)入力事業所で発生した本店勘定については集約しないよう修正
            ,  NyuuryokuZigyoushoCD 
--2011/04/04 修正 OSE.Ikoma 終了 (M04583)入力事業所で発生した本店勘定については集約しないよう修正
        )
        SELECT
               BUN.DenpyouTani
            ,  BUN.SiwakeTani
            ,  BUN.HasseiDate
            ,  BUN.GyouNO
            ,  1
            ,  HDHZG.ZigyoushoCD
            ,  HDHZG.BumonCD
            ,  NULL
            ,  KRHZG.KanjouCD
            ,  KRHZG.HojoCD
            ,  KRHZG.UtiwakeCD
            ,  BUN.KariKingaku + ISNULL(BUN.KariZeigaku,0)
            ,  NULL
            ,  1
            ,  1
            ,  HDHZG.ZigyoushoCD
            ,  HDHZG.BumonCD
            ,  NULL
            ,  HDHZG.KanjouCD
            ,  HDHZG.HojoCD
            ,  HDHZG.UtiwakeCD
            ,  BUN.KariKingaku + ISNULL(BUN.KariZeigaku,0)
            ,  NULL
            ,  1
--2011/04/04 修正 OSE.Ikoma 開始 (M04583)入力事業所で発生した本店勘定については集約しないよう修正
            ,  BUN.ZigyoushoCD 
--2011/04/04 修正 OSE.Ikoma 終了 (M04583)入力事業所で発生した本店勘定については集約しないよう修正
        FROM #M0000WK_MA_HonsitenBunkai BUN             --画面内容
        INNER JOIN #M0000WK_MA_HonsitenBunkaiTaishou HBT
        ON    HBT.DenpyouTani   = BUN.DenpyouTani
        --<借方事業所>
        LEFT JOIN #M0000WK_MA_HonsitenBunkai_HonsitenKanjou KRZIG        --本支店勘定登録マスタ
        ON    KRZIG.HasseiDate  = BUN.HasseiDate
        AND   KRZIG.ZigyoushoCD = BUN.KariZigyoushoCD
        --<借方事業所の本社事業所>
        LEFT JOIN #M0000WK_MA_HonsitenBunkai_HonsitenKanjou KRHZG        --本支店勘定登録マスタ
        ON    KRHZG.HasseiDate  = KRZIG.HasseiDate
        AND   KRHZG.ZigyoushoCD = KRZIG.HonshaZigyoushoCD
        --<ヘッダ事業所>
        LEFT JOIN #M0000WK_MA_HonsitenBunkai_HonsitenKanjou HDZIG        --本支店勘定登録マスタ
        ON    HDZIG.HasseiDate  = BUN.HasseiDate
        AND   HDZIG.ZigyoushoCD = BUN.ZigyoushoCD
        --<ヘッダ事業所の本社事業所>
        LEFT JOIN #M0000WK_MA_HonsitenBunkai_HonsitenKanjou HDHZG        --本支店勘定登録マスタ
        ON    HDHZG.HasseiDate  = HDZIG.HasseiDate
        AND   HDHZG.ZigyoushoCD = HDZIG.HonshaZigyoushoCD
        WHERE BUN.OperationComputerName    =   @p_UpdateComputerName
        AND   BUN.OperationUserID          =   @p_UpdateUserID
        AND   BUN.OperationJobID           =   @p_UpdateJobID
        AND   BUN.KariKanjouCD IS NOT NULL
        ORDER BY BUN.GyouNO

        -- @@ERRORを取得し、変数に保存する。
        SET @ErrorCode      = @@ERROR
        -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
        IF  @ErrorCode <> 0
        BEGIN
            GOTO ERR_SHORI
        END 




        --事業所の本社事業所分を貸方側の情報で作成
        INSERT INTO #M0000WK_MA_HonsitenBunkaiKakunou  (
               DenpyouTani
            ,  SiwakeTani
            ,  HasseiDate
            ,  GyouNO
            ,  KariNyuuryokuKBN
            ,  KariZigyoushoCD
            ,  KariBumonCD
            ,  KariTorihikisakiCD
            ,  KariKanjouCD
            ,  KariHojoCD
            ,  KariUtiwakeCD
            ,  KariKingaku
            ,  KariZeigaku
            ,  KariHonshaKBN
            ,  KasiNyuuryokuKBN
            ,  KasiZigyoushoCD
            ,  KasiBumonCD
            ,  KasiTorihikisakiCD
            ,  KasiKanjouCD
            ,  KasiHojoCD   
            ,  KasiUtiwakeCD
            ,  KasiKingaku
            ,  KasiZeigaku
            ,  KasiHonshaKBN
--2011/04/04 修正 OSE.Ikoma 開始 (M04583)入力事業所で発生した本店勘定については集約しないよう修正
            ,  NyuuryokuZigyoushoCD 
--2011/04/04 修正 OSE.Ikoma 終了 (M04583)入力事業所で発生した本店勘定については集約しないよう修正
        )
        SELECT
               BUN.DenpyouTani
            ,  BUN.SiwakeTani
            ,  BUN.HasseiDate
            ,  BUN.GyouNO
            ,  1
            ,  HDHZG.ZigyoushoCD
            ,  HDHZG.BumonCD
            ,  NULL
            ,  HDHZG.KanjouCD
            ,  HDHZG.HojoCD
            ,  HDHZG.UtiwakeCD
            ,  BUN.KasiKingaku + ISNULL(BUN.KasiZeigaku,0)
            ,  NULL
            ,  1
            ,  1
            ,  HDHZG.ZigyoushoCD
            ,  HDHZG.BumonCD
            ,  NULL
            ,  KSHZG.KanjouCD
            ,  KSHZG.HojoCD
            ,  KSHZG.UtiwakeCD
            ,  BUN.KasiKingaku + ISNULL(BUN.KasiZeigaku,0)
            ,  NULL
            ,  1
--2011/04/04 修正 OSE.Ikoma 開始 (M04583)入力事業所で発生した本店勘定については集約しないよう修正
            ,  BUN.ZigyoushoCD 
--2011/04/04 修正 OSE.Ikoma 終了 (M04583)入力事業所で発生した本店勘定については集約しないよう修正
        FROM #M0000WK_MA_HonsitenBunkai BUN             --画面内容
        INNER JOIN #M0000WK_MA_HonsitenBunkaiTaishou HBT
        ON    HBT.DenpyouTani   = BUN.DenpyouTani
        --<貸方事業所>
        LEFT JOIN #M0000WK_MA_HonsitenBunkai_HonsitenKanjou KSZIG        --本支店勘定登録マスタ
        ON    KSZIG.HasseiDate  = BUN.HasseiDate
        AND   KSZIG.ZigyoushoCD = BUN.KasiZigyoushoCD
        --<貸方事業所の本社事業所>
        LEFT JOIN #M0000WK_MA_HonsitenBunkai_HonsitenKanjou KSHZG        --本支店勘定登録マスタ
        ON    KSHZG.HasseiDate  = KSZIG.HasseiDate
        AND   KSHZG.ZigyoushoCD = KSZIG.HonshaZigyoushoCD
        --<ヘッダ事業所>
        LEFT JOIN #M0000WK_MA_HonsitenBunkai_HonsitenKanjou HDZIG        --本支店勘定登録マスタ
        ON    HDZIG.HasseiDate  = BUN.HasseiDate
        AND   HDZIG.ZigyoushoCD = BUN.ZigyoushoCD
        --<ヘッダ事業所の本社事業所>
        LEFT JOIN #M0000WK_MA_HonsitenBunkai_HonsitenKanjou HDHZG        --本支店勘定登録マスタ
        ON    HDHZG.HasseiDate  = HDZIG.HasseiDate
        AND   HDHZG.ZigyoushoCD = HDZIG.HonshaZigyoushoCD
        WHERE BUN.OperationComputerName    =   @p_UpdateComputerName
        AND   BUN.OperationUserID          =   @p_UpdateUserID
        AND   BUN.OperationJobID           =   @p_UpdateJobID
        AND   BUN.KasiKanjouCD IS NOT NULL
        ORDER BY BUN.GyouNO
        
        -- @@ERRORを取得し、変数に保存する。
        SET @ErrorCode      = @@ERROR
        -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
        IF  @ErrorCode <> 0
        BEGIN
            GOTO ERR_SHORI
        END 
        
    END
        

    -------------------------------------------------------------------------------------
    -- 集計処理                                                                         -
    -------------------------------------------------------------------------------------
    
    
    --振替科目をまとめる処理
    --画面入力データを待避(画面入力データはまとめないで、そのまま落とす)
    INSERT INTO #M0000WK_MA_HonsitenBunkaiShukei (
              DenpyouTani
            , SiwakeTani
            , HasseiDate
            , GyouNO
            , DataKBN
            , HasseiKBN
            , ZigyoushoCD
            , BumonCD
            , KanjouCD
            , HojoCD
            , UtiwakeCD
            , TorihikisakiCD
            , Kingaku
            , Zeigaku
            , GyouNONew
    )
    --借方
    SELECT
              DenpyouTani
            , SiwakeTani
            , HasseiDate
            , GyouNO
            , 0                    AS DataKBN        --0:画面入力データ
            , 0                    AS HasseiKBN
            , KariZigyoushoCD      AS ZigyoushoCD
            , KariBumonCD          AS BumonCD
            , KariKanjouCD         AS KanjouCD
            , KariHojoCD           AS HojoCD
            , KariUtiwakeCD        AS UtiwakeCD
            , KariTorihikisakiCD   AS TorihikisakiCD
            , KariKingaku          AS Kingaku
            , KariZeigaku          AS Zeigaku
            , GyouNO
    FROM #M0000WK_MA_HonsitenBunkaiKakunou
    WHERE KariNyuuryokuKBN = 0        --0:画面入力データ
    UNION ALL
    --貸方
    SELECT
              DenpyouTani
            , SiwakeTani
            , HasseiDate
            , GyouNO
            , 0                    AS DataKBN        --0:画面入力データ
            , 1                    AS HasseiKBN
            , KasiZigyoushoCD      AS ZigyoushoCD
            , KasiBumonCD          AS BumonCD
            , KasiKanjouCD         AS KanjouCD
            , KasiHojoCD           AS HojoCD
            , KasiUtiwakeCD        AS UtiwakeCD
            , KasiTorihikisakiCD   AS TorihikisakiCD
            , KasiKingaku          AS Kingaku
            , KasiZeigaku          AS Zeigaku
            , GyouNO
    FROM #M0000WK_MA_HonsitenBunkaiKakunou
    WHERE KasiNyuuryokuKBN = 0        --0:画面入力データ

    -- @@ERRORを取得し、変数に保存する。
    SET @ErrorCode      = @@ERROR
    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END 



    --振替データ(借方、及び貸方) ※借方/貸方事業所が本社事業所分のみ
    --(待避する際、明細事業所で取得した本支店勘定登録マスタの内容と明細の内容が同一のものは省く) 
    INSERT INTO #M0000WK_MA_HonsitenBunkaiShukei (
              DenpyouTani
            , SiwakeTani
            , HasseiDate
            , GyouNO
            , DataKBN
            , HasseiKBN
            , ZigyoushoCD
            , BumonCD
            , KanjouCD
            , HojoCD
            , UtiwakeCD
            , TorihikisakiCD
            , Kingaku
            , Zeigaku
            , GyouNONew
    )
    SELECT
              DenpyouTani
            , SiwakeTani
            , HasseiDate
            , GyouNO
            , 1 AS DataKBN        --1:振替データ
            , HasseiKBN
            , ZigyoushoCD
            , BumonCD
            , KanjouCD
            , HojoCD
            , UtiwakeCD
            , TorihikisakiCD
            , Kingaku
            , NULL AS Zeigaku
            , GyouNO
    FROM (
            --借方
            SELECT
                      KAK.DenpyouTani
                    , KAK.SiwakeTani
                    , KAK.HasseiDate
                    , KAK.GyouNO
                    , 0                         AS HasseiKBN
                    , KAK.KariZigyoushoCD       AS ZigyoushoCD
                    , KAK.KariBumonCD           AS BumonCD
                    , KAK.KariKanjouCD          AS KanjouCD
                    , KAK.KariHojoCD            AS HojoCD
                    , KAK.KariUtiwakeCD         AS UtiwakeCD
                    , KAK.KariTorihikisakiCD    AS TorihikisakiCD
                    , KAK.KariKingaku           AS Kingaku
            FROM #M0000WK_MA_HonsitenBunkaiKakunou KAK
            LEFT JOIN #M0000WK_MA_HonsitenBunkai_HonsitenKanjou HSK
            ON  HSK.HasseiDate  = KAK.HasseiDate
            AND HSK.ZigyoushoCD = KAK.KariZigyoushoCD
            AND HSK.BumonCD     = KAK.KariBumonCD
            AND HSK.KanjouCD    = KAK.KariKanjouCD
            AND HSK.HojoCD      = KAK.KariHojoCD
            AND HSK.UtiwakeCD   = KAK.KariUtiwakeCD
            WHERE KAK.KariNyuuryokuKBN = 1   --1:振替データ
            AND   KAK.KariHonshaKBN = 1      --1:本社データ
            AND   HSK.ZigyoushoCD IS NULL
            
            UNION ALL
            --貸方
            SELECT
                      KAK.DenpyouTani
                    , KAK.SiwakeTani
                    , KAK.HasseiDate
                    , KAK.GyouNO
                    , 1                         AS HasseiKBN
                    , KAK.KasiZigyoushoCD       AS ZigyoushoCD
                    , KAK.KasiBumonCD           AS BumonCD
                    , KAK.KasiKanjouCD          AS KanjouCD
                    , KAK.KasiHojoCD            AS HojoCD
                    , KAK.KasiUtiwakeCD         AS UtiwakeCD
                    , KAK.KasiTorihikisakiCD    AS TorihikisakiCD
                    , KAK.KasiKingaku           AS Kingaku
            FROM #M0000WK_MA_HonsitenBunkaiKakunou KAK
            LEFT JOIN #M0000WK_MA_HonsitenBunkai_HonsitenKanjou HSK
            ON  HSK.HasseiDate  = KAK.HasseiDate
            AND HSK.ZigyoushoCD = KAK.KasiZigyoushoCD
            AND HSK.BumonCD     = KAK.KasiBumonCD
            AND HSK.KanjouCD    = KAK.KasiKanjouCD
            AND HSK.HojoCD      = KAK.KasiHojoCD
            AND HSK.UtiwakeCD   = KAK.KasiUtiwakeCD
            WHERE KAK.KasiNyuuryokuKBN = 1   --1:振替データ
            AND   KAK.KasiHonshaKBN = 1      --1:本社データ
            AND   HSK.ZigyoushoCD IS NULL
    ) KAK


    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR

    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END



    --振替データ(借方、及び貸方) ※借方/貸方事業所が本社事業所分以外
    --(待避する際、明細事業所で取得した本支店勘定登録マスタの内容と明細の内容が同一のものは省く) 
    INSERT INTO #M0000WK_MA_HonsitenBunkaiShukei (
              DenpyouTani
            , SiwakeTani
            , HasseiDate
            , GyouNO
            , DataKBN
            , HasseiKBN
            , ZigyoushoCD
            , BumonCD
            , KanjouCD
            , HojoCD
            , UtiwakeCD
            , TorihikisakiCD
            , Kingaku
            , Zeigaku
    )
    SELECT
              DenpyouTani
            , SiwakeTani
            , MAX(HasseiDate) AS HasseiDate 
            , GyouNO
            , 1 AS DataKBN        --1:振替データ
--2008/05/14 変更 N.Sasaki 開始 集計処理時の発生区分の判断は金額の絶対値でおこなう
--            , MIN(CASE WHEN KariKingaku > KasiKingaku THEN 0          ELSE 1          END) AS HasseiKBN
            , MIN(CASE WHEN ABS(KariKingaku) > ABS(KasiKingaku) THEN 0          ELSE 1          END) AS HasseiKBN
--2008/05/14 変更 N.Sasaki 終了
            , ZigyoushoCD
            , BumonCD
            , KanjouCD
            , HojoCD
            , UtiwakeCD
            , TorihikisakiCD
--2008/05/14 変更 N.Sasaki 開始  集計処理時の発生区分の判断は金額の絶対値でおこなう
--            , SUM(CASE WHEN KariKingaku > KasiKingaku THEN KariKingaku - KasiKingaku ELSE KasiKingaku - KariKingaku END) AS Kingaku
            , SUM(CASE WHEN ABS(KariKingaku) > ABS(KasiKingaku) THEN KariKingaku - KasiKingaku ELSE KasiKingaku - KariKingaku END) AS Kingaku
--2008/05/14 変更 N.Sasaki 終了
            , NULL AS Zeigaku
    FROM (
            SELECT
                      DenpyouTani
                    , SiwakeTani
                    , MAX(HasseiDate)    AS HasseiDate
                    , GyouNO
                    , ZigyoushoCD
                    , BumonCD
                    , KanjouCD
                    , HojoCD
                    , UtiwakeCD
                    , TorihikisakiCD
                    , SUM(KariKingaku) AS KariKingaku
                    , SUM(KasiKingaku) AS KasiKingaku
            FROM (
                    --借方
                    SELECT
                              KAK.DenpyouTani
                            , KAK.SiwakeTani
                            , MAX(KAK.HasseiDate)    AS HasseiDate
                            , KAK.GyouNO
                            , KAK.KariZigyoushoCD    AS ZigyoushoCD
                            , KAK.KariBumonCD        AS BumonCD
                            , KAK.KariKanjouCD       AS KanjouCD
                            , KAK.KariHojoCD         AS HojoCD
                            , KAK.KariUtiwakeCD      AS UtiwakeCD
                            , KAK.KariTorihikisakiCD AS TorihikisakiCD
                            , SUM(KAK.KariKingaku)   AS KariKingaku
                            , 0                      AS KasiKingaku
                    FROM #M0000WK_MA_HonsitenBunkaiKakunou KAK
                    LEFT JOIN #M0000WK_MA_HonsitenBunkai_HonsitenKanjou HSK
                    ON  HSK.HasseiDate  = KAK.HasseiDate
                    AND HSK.ZigyoushoCD = KAK.KariZigyoushoCD
                    AND HSK.BumonCD     = KAK.KariBumonCD
                    AND HSK.KanjouCD    = KAK.KariKanjouCD
                    AND HSK.HojoCD      = KAK.KariHojoCD
                    AND HSK.UtiwakeCD   = KAK.KariUtiwakeCD
                    WHERE KAK.KariNyuuryokuKBN = 1        --1:振替データ
                    AND   KAK.KariHonshaKBN = 0      --0:支店データ
                    AND   HSK.ZigyoushoCD IS NULL
                    --2011/04/04 修正 OSE.Ikoma 開始 (M04583)入力事業所で発生した本店勘定については集約しないよう修正
                    AND   KAK.NyuuryokuZigyoushoCD <> KAK.KariZigyoushoCD
                    --2011/04/04 修正 OSE.Ikoma 終了 (M04583)入力事業所で発生した本店勘定については集約しないよう修正
                    GROUP BY KAK.DenpyouTani, KAK.KariZigyoushoCD, KAK.SiwakeTani,  KAK.GyouNO, KAK.KariBumonCD, KAK.KariKanjouCD, KAK.KariHojoCD, KAK.KariUtiwakeCD, KAK.KariTorihikisakiCD
                    UNION ALL
                    --貸方
                    SELECT
                              KAK.DenpyouTani
                            , KAK.SiwakeTani
                            , MAX(KAK.HasseiDate)    AS HasseiDate
                            , KAK.GyouNO
                            , KAK.KasiZigyoushoCD    AS ZigyoushoCD
                            , KAK.KasiBumonCD        AS BumonCD
                            , KAK.KasiKanjouCD       AS KanjouCD
                            , KAK.KasiHojoCD         AS HojoCD
                            , KAK.KasiUtiwakeCD      AS UtiwakeCD
                            , KAK.KasiTorihikisakiCD AS TorihikisakiCD
                            , 0                      AS KariKingaku
                            , SUM(KAK.KasiKingaku)   AS KasiKingaku
                    FROM #M0000WK_MA_HonsitenBunkaiKakunou KAK
                    LEFT JOIN #M0000WK_MA_HonsitenBunkai_HonsitenKanjou HSK
                    ON  HSK.HasseiDate  = KAK.HasseiDate
                    AND HSK.ZigyoushoCD = KAK.KasiZigyoushoCD
                    AND HSK.BumonCD     = KAK.KasiBumonCD
                    AND HSK.KanjouCD    = KAK.KasiKanjouCD
                    AND HSK.HojoCD      = KAK.KasiHojoCD
                    AND HSK.UtiwakeCD   = KAK.KasiUtiwakeCD
                    WHERE KAK.KasiNyuuryokuKBN = 1        --1:振替データ
                    AND   KAK.KasiHonshaKBN = 0      --0:支店データ
                    AND   HSK.ZigyoushoCD IS NULL
                    --2011/04/04 修正 OSE.Ikoma 開始 (M04583)入力事業所で発生した本店勘定については集約しないよう修正
                    AND   KAK.NyuuryokuZigyoushoCD <> KAK.KasiZigyoushoCD
                    --2011/04/04 修正 OSE.Ikoma 終了 (M04583)入力事業所で発生した本店勘定については集約しないよう修正
                    GROUP BY KAK.DenpyouTani, KAK.KasiZigyoushoCD, KAK.SiwakeTani, KAK.GyouNO, KAK.KasiBumonCD, KAK.KasiKanjouCD, KAK.KasiHojoCD, KAK.KasiUtiwakeCD, KAK.KasiTorihikisakiCD
            ) KAK
            GROUP BY KAK.DenpyouTani, KAK.ZigyoushoCD, KAK.SiwakeTani, KAK.GyouNO, KAK.BumonCD, KAK.KanjouCD, KAK.HojoCD, KAK.UtiwakeCD, KAK.TorihikisakiCD
    ) KAK
    GROUP BY KAK.DenpyouTani, KAK.ZigyoushoCD, KAK.SiwakeTani, KAK.GyouNO, KAK.BumonCD, KAK.KanjouCD, KAK.HojoCD, KAK.UtiwakeCD, KAK.TorihikisakiCD
    HAVING SUM(KariKingaku - KasiKingaku) <> 0

    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR

    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END

--2011/04/04 修正 OSE.Ikoma 開始 (M04583)入力事業所で発生した本店勘定については集約しないよう修正
    --振替データ(借方、及び貸方) ※借方/貸方事業所が本社事業所分のみ
    INSERT INTO #M0000WK_MA_HonsitenBunkaiShukei (
              DenpyouTani
            , SiwakeTani
            , HasseiDate
            , GyouNO
            , DataKBN
            , HasseiKBN
            , ZigyoushoCD
            , BumonCD
            , KanjouCD
            , HojoCD
            , UtiwakeCD
            , TorihikisakiCD
            , Kingaku
            , Zeigaku
            , GyouNONew
    )
    SELECT
              DenpyouTani
            , SiwakeTani
            , HasseiDate
            , GyouNO
            , 1 AS DataKBN        --1:振替データ
            , HasseiKBN
            , ZigyoushoCD
            , BumonCD
            , KanjouCD
            , HojoCD
            , UtiwakeCD
            , TorihikisakiCD
            , Kingaku
            , NULL AS Zeigaku
            , GyouNO
    FROM (
            --借方
            SELECT
                      KAK.DenpyouTani
                    , KAK.SiwakeTani
                    , KAK.HasseiDate
                    , KAK.GyouNO
                    , 0                         AS HasseiKBN
                    , KAK.KariZigyoushoCD       AS ZigyoushoCD
                    , KAK.KariBumonCD           AS BumonCD
                    , KAK.KariKanjouCD          AS KanjouCD
                    , KAK.KariHojoCD            AS HojoCD
                    , KAK.KariUtiwakeCD         AS UtiwakeCD
                    , KAK.KariTorihikisakiCD    AS TorihikisakiCD
                    , KAK.KariKingaku           AS Kingaku
            FROM #M0000WK_MA_HonsitenBunkaiKakunou KAK
            LEFT JOIN #M0000WK_MA_HonsitenBunkai_HonsitenKanjou HSK
            ON  HSK.HasseiDate  = KAK.HasseiDate
            AND HSK.ZigyoushoCD = KAK.KariZigyoushoCD
            AND HSK.BumonCD     = KAK.KariBumonCD
            AND HSK.KanjouCD    = KAK.KariKanjouCD
            AND HSK.HojoCD      = KAK.KariHojoCD
            AND HSK.UtiwakeCD   = KAK.KariUtiwakeCD
            WHERE KAK.KariNyuuryokuKBN = 1   --1:振替データ
            AND   KAK.KariHonshaKBN = 0      --0:支店データ
            AND   KAK.NyuuryokuZigyoushoCD = KAK.KariZigyoushoCD
            AND   HSK.ZigyoushoCD IS NULL
            
            UNION ALL
            --貸方
            SELECT
                      KAK.DenpyouTani
                    , KAK.SiwakeTani
                    , KAK.HasseiDate
                    , KAK.GyouNO
                    , 1                         AS HasseiKBN
                    , KAK.KasiZigyoushoCD       AS ZigyoushoCD
                    , KAK.KasiBumonCD           AS BumonCD
                    , KAK.KasiKanjouCD          AS KanjouCD
                    , KAK.KasiHojoCD            AS HojoCD
                    , KAK.KasiUtiwakeCD         AS UtiwakeCD
                    , KAK.KasiTorihikisakiCD    AS TorihikisakiCD
                    , KAK.KasiKingaku           AS Kingaku
            FROM #M0000WK_MA_HonsitenBunkaiKakunou KAK
            LEFT JOIN #M0000WK_MA_HonsitenBunkai_HonsitenKanjou HSK
            ON  HSK.HasseiDate  = KAK.HasseiDate
            AND HSK.ZigyoushoCD = KAK.KasiZigyoushoCD
            AND HSK.BumonCD     = KAK.KasiBumonCD
            AND HSK.KanjouCD    = KAK.KasiKanjouCD
            AND HSK.HojoCD      = KAK.KasiHojoCD
            AND HSK.UtiwakeCD   = KAK.KasiUtiwakeCD
            WHERE KAK.KasiNyuuryokuKBN = 1   --1:振替データ
            AND   KAK.KasiHonshaKBN = 0      --0:支店データ
            AND   KAK.NyuuryokuZigyoushoCD = KAK.KasiZigyoushoCD
            AND   HSK.ZigyoushoCD IS NULL
    ) KAK

    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR

    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END
--2011/04/04 修正 OSE.Ikoma 開始 (M04583)入力事業所で発生した本店勘定については集約しないよう修正

    -------------------------------------------------------------------------------------
    -- 行NO枝番の採番                                                                        -
    -------------------------------------------------------------------------------------

    -- 事業所＞仕訳単位＞行番号＞発生区分毎に、行番号枝番を採番
     DECLARE CURSIWAKE CURSOR  LOCAL STATIC
     FOR
         SELECT
                   DenpyouTani
                 , ZigyoushoCD
                 , SiwakeTani
                 , GyouNO
                 , HasseiKBN
                 , [ID]
         FROM #M0000WK_MA_HonsitenBunkaiShukei
--2011/04/04 修正 OSE.Ikoma 開始 (M04584)振替先伝票のソート順を変更(不正に複合仕訳になる現象を解消)
--         ORDER BY DenpyouTani, ZigyoushoCD, SiwakeTani, GyouNO, HasseiKBN, DataKBN, BumonCD, KanjouCD, HojoCD, UtiwakeCD, TorihikisakiCD, [ID]
         ORDER BY DenpyouTani, ZigyoushoCD, SiwakeTani, GyouNO, HasseiKBN
                , CASE WHEN HasseiKBN = 0 AND DataKBN = 0 THEN 0
                       WHEN HasseiKBN = 1 AND DataKBN = 1 THEN 0
                       ELSE 1
                  END
                , BumonCD, KanjouCD, HojoCD, UtiwakeCD, TorihikisakiCD, [ID]
--2011/04/04 修正 OSE.Ikoma 終了 (M04584)振替先伝票のソート順を変更(不正に複合仕訳になる現象を解消)
     OPEN    CURSIWAKE
     FETCH NEXT FROM CURSIWAKE INTO
                       @DenpyouTani
                     , @ZigyoushoCD
                     , @SiwakeTani
                     , @GyouNO
                     , @HasseiKBN
                     , @ID

    SELECT   @FETCH_STATUS_CURSIWAKE = @@FETCH_STATUS
           , @ErrorCode              = @@ERROR

    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。   
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END

    --本支店分解を全く行わない場合もある為、ここではフェッチ対象レコードが存在するかのチェックは行わない。
--     IF  @FETCH_STATUS_CURSIWAKE <> 0
--     BEGIN
--         -- 引数に戻り値をセット
--         SET @po_ErrorKBN        = '1'                    --1：システムエラー
--         SET @po_ErrorCD1        = '000'                  --会計は"000"で統一
--         SET @po_ErrorCD2        = '00119'                --基幹会計は"00119"で統一
--         SET @po_ErrorProcName   = OBJECT_NAME(@@PROCID)
--         SET @po_ErrorSummary    = ''
--         SET @po_ErrorDetail     = ''
--         SET @po_ErrorBtnKBN     = 0
--         SET @po_ErrorBtnDefault = 0
--         GOTO ERR_SHORI
--     END
         
    --変数初期化
    SET @GyouNOEdaban_CNT     = 1
    SET @DenpyouTani_BK = @DenpyouTani
    SET @ZigyoushoCD_BK = @ZigyoushoCD
    SET @SiwakeTani_BK  = @SiwakeTani
    SET @GyouNO_BK      = @GyouNO
    SET @HasseiKBN_BK   = @HasseiKBN

    WHILE @@FETCH_STATUS = 0
    BEGIN
        --伝票単位、事業所、仕訳単位、行番号、発生区分ごとにBREAK
        IF @DenpyouTani <> @DenpyouTani_BK OR @ZigyoushoCD <> @ZigyoushoCD_BK OR @SiwakeTani <> @SiwakeTani_BK OR @GyouNO <> @GyouNO_BK OR @HasseiKBN <> @HasseiKBN_BK
        BEGIN
            --変数初期化
            SET @GyouNOEdaban_CNT     = 1
            SET @DenpyouTani_BK = @DenpyouTani
            SET @ZigyoushoCD_BK = @ZigyoushoCD
            SET @SiwakeTani_BK  = @SiwakeTani
            SET @GyouNO_BK      = @GyouNO
            SET @HasseiKBN_BK   = @HasseiKBN
        END --IF

        --行ID更新
        UPDATE #M0000WK_MA_HonsitenBunkaiShukei SET
                GyouNOEdaban = @GyouNOEdaban_CNT
        WHERE [ID] = @ID

        -- @@ERROR を取得し、変数に保存する。
        SET @ErrorCode = @@ERROR

        -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
        IF  @ErrorCode <> 0
        BEGIN
            GOTO ERR_SHORI
        END
          
        --行IDカウントアップ
        SET @GyouNOEdaban_CNT  = @GyouNOEdaban_CNT + 1
 
        FETCH NEXT FROM CURSIWAKE INTO 
                          @DenpyouTani
                        , @ZigyoushoCD
                        , @SiwakeTani
                        , @GyouNO
                        , @HasseiKBN
                        , @ID
 
        -- @@ERROR を取得し、変数に保存する。
        SET @ErrorCode = @@ERROR
        
        -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
        IF  @ErrorCode <> 0
        BEGIN
            GOTO ERR_SHORI
        END
    END --WHILE
    
    CLOSE CURSIWAKE
    DEALLOCATE CURSIWAKE

    -------------------------------------------------------------------------------------
    -- 結合処理。本支店分解結果ワークにINSERT                                           -
    -------------------------------------------------------------------------------------
    --借方、貸方共に存在するレコード分
    INSERT INTO #M0000WK_MA_HonsitenBunkaiKekka (
              OperationComputerName
            , OperationUserID
            , OperationDatetime
            , OperationJobID
            , DenpyouTani
            , HasseiDate
            , ZigyoushoCD
            , ZigyoushoKaiteiDate
            , SiwakeTani
            , SiwakeKBN
            , KariBumonCD
            , KariBumonKaiteiDate
            , KariKanjouCD
            , KariKanjouKaiteiDate
            , KariHojoCD
            , KariHojoKaiteiDate
            , KariUtiwakeCD
            , KariUtiwakeKaiteiDate
            , KariTorihikisakiCD
            , KariTorihikisakiKaiteiDate
            , KariTaishakuKBN
            , KariHasseiKBN
            , KariKingaku
            , KariZeigaku
            , KariZeiKamokuCD
            , KariZeiKaiteiDate
            , KariZeiTaishakuKBN
            , KariZeiHontaiCD
            , KariZeiHontaiKaiteiDate
            , KariZeiKBN
            , KariZeiRate
            , KariHasuuKBN
            , KariZeiKeisanKBN
            , KariZeiKamokuKBN
            , KariZeikomiKBN
            , KariTorihikisakiKBN
            , KariHojoKBN
            , KariUtiwakeKBN
            , KariBumonKBN
            , KariBunsekiCD1
            , KariBunsekiCD2
            , KariBunsekiCD3
            , KariBunsekiCD4
            , KariBunsekiCD5
            , KariSikinCD
            , KariProjectCD
            , KariBunsekiCustomizeCD1
            , KariBunsekiCustomizeCD2
            , KariBunsekiCustomizeCD3
            , KariBunsekiCustomizeCD4
            , KariBunsekiCustomizeCD5
            , KasiBumonCD
            , KasiBumonKaiteiDate
            , KasiKanjouCD
            , KasiKanjouKaiteiDate
            , KasiHojoCD
            , KasiHojoKaiteiDate
            , KasiUtiwakeCD
            , KasiUtiwakeKaiteiDate
            , KasiTorihikisakiCD
            , KasiTorihikisakiKaiteiDate
            , KasiTaishakuKBN
            , KasiHasseiKBN
            , KasiKingaku
            , KasiZeigaku
            , KasiZeiKamokuCD
            , KasiZeiKaiteiDate
            , KasiZeiTaishakuKBN
            , KasiZeiHontaiCD
            , KasiZeiHontaiKaiteiDate
            , KasiZeiKBN
            , KasiZeiRate
            , KasiHasuuKBN
            , KasiZeiKeisanKBN
            , KasiZeiKamokuKBN
            , KasiZeikomiKBN
            , KasiTorihikisakiKBN
            , KasiHojoKBN
            , KasiUtiwakeKBN
            , KasiBumonKBN
            , KasiBunsekiCD1
            , KasiBunsekiCD2
            , KasiBunsekiCD3
            , KasiBunsekiCD4
            , KasiBunsekiCD5
            , KasiSikinCD
            , KasiProjectCD
            , KasiBunsekiCustomizeCD1
            , KasiBunsekiCustomizeCD2
            , KasiBunsekiCustomizeCD3
            , KasiBunsekiCustomizeCD4
            , KasiBunsekiCustomizeCD5
            , MeisaiTekiyou
            , GyouNO
            , GyouNOEdaban
            , GyouNONew
            , CompanyNO
            , UpdateDatetime
            , UpdateCertificatedGuid
            , UpdateJobID
            , UpdateUserID
            , UpdateComputerName
            , UpdateComputerUserName
            , UpdateComputerDomainName
            )
    SELECT
              @p_UpdateComputerName
            , @p_UpdateUserID
            , @p_UpdateDatetime
            , @p_UpdateJobID    
            , KRSHU.DenpyouTani                                                                                     AS DenpyouTani
            , KRSHU.HasseiDate                                                                                      AS HasseiDate
            , KRSHU.ZigyoushoCD                                                                                     AS KariZigyoushoCD
            , KRZIG.KaiteiDate                                                                                      AS KariZiKaiteiDate
            , KRSHU.SiwakeTani                                                                                      AS SiwakeTani
            , KRBUN.SiwakeKBN                                                                                       AS SiwakeKBN
            , KRSHU.BumonCD                                                                                         AS KariBumonCD
            , KRBMN.KaiteiDate                                                                                      AS KariBuKaiteiDate
            , KRSHU.KanjouCD                                                                                        AS KariKanjouCD
            , KRKAN.KaiteiDate                                                                                      AS KariKaKaiteiDate
            , KRSHU.HojoCD                                                                                          AS KariHojoCD
            , KRHOJ.KaiteiDate                                                                                      AS KariHoKaiteiDate
            , KRSHU.UtiwakeCD                                                                                       AS KariUtiwakeCD
            , KRUTI.KaiteiDate                                                                                      AS KariUtiKaiteiDate
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariTorihikisakiCD             ELSE NULL                END) AS KariTorihikisakiCD
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariTorihikisakiKaiteiDate     ELSE NULL                END) AS KariTorihikisakiKaiteiDate
            , KRKAN.TaishakuKBN                                                                                     AS KariTaishakuKBN
            , 0                                                                                                     AS KariHasseiKBN
            , KRSHU.Kingaku                                                                                         AS KariKingaku
            , KRSHU.Zeigaku                                                                                         AS KariZeigaku
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariZeiKamokuCD         ELSE NULL                       END) AS KariZeikamokuCD
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariZeiKaiteiDate       ELSE NULL                       END) AS KariZeiKaiteiDate
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariZeiTaishakuKBN      ELSE NULL                       END) AS KariZeiTaishakuKBN
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariZeiHontaiCD         ELSE NULL                       END) AS KariHontaiCD
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariZeiHontaiKaiteiDate ELSE NULL                       END) AS KariHonKaiteiDate
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariZeiKBN              ELSE KRKAN.KariZeiKBN           END) AS KariZeiKBN
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariZeiRate             ELSE NULL                       END) AS KariZeiRate
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariHasuuKBN            ELSE NULL                       END) AS KariHasuuKBN
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariZeiKeisanKBN        ELSE NULL                       END) AS KariZeiKeisanKBN
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariZeiKamokuKBN        ELSE NULL                       END) AS KariZeiKamokuKBN
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariZeikomiKBN          ELSE KRKAN.KariZeikomiKBN       END) AS KariZeikomiKBN
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariTorihikisakiKBN     ELSE KRKAN.TorihikisakiKBN      END) AS KariTorihikisakiKBN
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariHojoKBN             ELSE KRKAN.HojoKBN              END) AS KariHojoKBN
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariUtiwakeKBN          ELSE ISNULL(KRHOJ.UtiwakeKBN,0) END) AS KariUtiwakeKBN
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariBumonKBN            ELSE KRKAN.BumonKBN             END) AS KariBumonKBN
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariBunsekiCD1          ELSE NULL                       END) AS KariBunsekiCD1
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariBunsekiCD2          ELSE NULL                       END) AS KariBunsekiCD2
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariBunsekiCD3          ELSE NULL                       END) AS KariBunsekiCD3
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariBunsekiCD4          ELSE NULL                       END) AS KariBunsekiCD4
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariBunsekiCD5          ELSE NULL                       END) AS KariBunsekiCD5
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariSikinCD             ELSE NULL                       END) AS KariSikinCD
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariProjectCD           ELSE NULL                       END) AS KariProjectCD
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariBunsekiCustomizeCD1 ELSE NULL                       END) AS KariBunsekiCustomizeCD1
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariBunsekiCustomizeCD2 ELSE NULL                       END) AS KariBunsekiCustomizeCD2
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariBunsekiCustomizeCD3 ELSE NULL                       END) AS KariBunsekiCustomizeCD3
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariBunsekiCustomizeCD4 ELSE NULL                       END) AS KariBunsekiCustomizeCD4
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariBunsekiCustomizeCD5 ELSE NULL                       END) AS KariBunsekiCustomizeCD5
            , KSSHU.BumonCD                                                                                         AS KasiBumonCD
            , KSBMN.KaiteiDate                                                                                      AS KasiBumonKaiteiDate
            , KSSHU.KanjouCD                                                                                        AS KasiKanjouCD
            , KSKAN.KaiteiDate                                                                                      AS KasiKanjouKaiteiDate
            , KSSHU.HojoCD                                                                                          AS KasiHojoCD
            , KSHOJ.KaiteiDate                                                                                      AS KasiHojoKaiteiDate
            , KSSHU.UtiwakeCD                                                                                       AS KasiUtiwakeCD
            , KSUTI.KaiteiDate                                                                                      AS KasiUtiwakeKaiteiDate
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiTorihikisakiCD             ELSE NULL                END) AS KasiTorihikisakiCD
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiTorihikisakiKaiteiDate     ELSE NULL                END) AS KasiTorihikisakiKaiteiDate
            , KSKAN.TaishakuKBN                                                                                     AS KasiTaishakuKBN
            , 1                                                                                                     AS KasiHasseiKBN
            , KSSHU.Kingaku                                                                                         AS KasiKingaku
            , KSSHU.Zeigaku                                                                                         AS KasiZeigaku
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiZeiKamokuCD         ELSE NULL                       END) AS KasiZeikamokuCD
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiZeiKaiteiDate       ELSE NULL                       END) AS KasiZeiKaiteiDate
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiZeiTaishakuKBN      ELSE NULL                       END) AS KasiZeiTaishakuKBN
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiZeiHontaiCD         ELSE NULL                       END) AS KasiHontaiCD
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiZeiHontaiKaiteiDate ELSE NULL                       END) AS KasiHonKaiteiDate
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiZeiKBN              ELSE KSKAN.KasiZeiKBN           END) AS KasiZeiKBN
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiZeiRate             ELSE NULL                       END) AS KasiZeiRate
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiHasuuKBN            ELSE NULL                       END) AS KasiHasuuKBN
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiZeiKeisanKBN        ELSE NULL                       END) AS KasiZeiKeisanKBN
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiZeiKamokuKBN        ELSE NULL                       END) AS KasiZeiKamokuKBN
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiZeikomiKBN          ELSE KSKAN.KasiZeikomiKBN       END) AS KasiZeikomiKBN
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiTorihikisakiKBN     ELSE KSKAN.TorihikisakiKBN      END) AS KasiTorihikisakiKBN
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiHojoKBN             ELSE KSKAN.HojoKBN              END) AS KasiHojoKBN
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiUtiwakeKBN          ELSE ISNULL(KSHOJ.UtiwakeKBN,0) END) AS KasiUtiwakeKBN
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiBumonKBN            ELSE KSKAN.BumonKBN             END) AS KasiBumonKBN
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiBunsekiCD1          ELSE NULL                       END) AS KasiBunsekiCD1
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiBunsekiCD2          ELSE NULL                       END) AS KasiBunsekiCD2
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiBunsekiCD3          ELSE NULL                       END) AS KasiBunsekiCD3
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiBunsekiCD4          ELSE NULL                       END) AS KasiBunsekiCD4
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiBunsekiCD5          ELSE NULL                       END) AS KasiBunsekiCD5
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiSikinCD             ELSE NULL                       END) AS KasiSikinCD
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiProjectCD           ELSE NULL                       END) AS KasiProjectCD
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiBunsekiCustomizeCD1 ELSE NULL                       END) AS KasiBunsekiCustomizeCD1
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiBunsekiCustomizeCD2 ELSE NULL                       END) AS KasiBunsekiCustomizeCD2
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiBunsekiCustomizeCD3 ELSE NULL                       END) AS KasiBunsekiCustomizeCD3
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiBunsekiCustomizeCD4 ELSE NULL                       END) AS KasiBunsekiCustomizeCD4
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiBunsekiCustomizeCD5 ELSE NULL                       END) AS KasiBunsekiCustomizeCD5
            , KRBUN.MeisaiTekiyou                                                                                   AS MeisaiTekiyou
            , KRSHU.GyouNO                                                                                          AS GyouNO
            , KRSHU.GyouNOEdaban                                                                                    AS GyouNOEdaban
            , KRSHU.GyouNONew                                                                                       AS GyouNONew
            , KRBUN.CompanyNO                                                                                       AS CompanyNO
            , @p_UpdateDatetime
            , @p_UpdateCertificatedGuid
            , @p_UpdateJobID
            , @p_UpdateUserID
            , @p_UpdateComputerName
            , @p_UpdateComputerUserName
            , @p_UpdateComputerDomainName
    FROM #M0000WK_MA_HonsitenBunkaiShukei KRSHU                --借方
    INNER JOIN #M0000WK_MA_HonsitenBunkaiShukei KSSHU          --貸方
    ON  KSSHU.DenpyouTani    = KRSHU.DenpyouTani
    AND KSSHU.ZigyoushoCD    = KRSHU.ZigyoushoCD
    AND KSSHU.SiwakeTani     = KRSHU.SiwakeTani
    AND KSSHU.GyouNO         = KRSHU.GyouNO
    AND KSSHU.GyouNOEdaban   = KRSHU.GyouNOEdaban
    AND KSSHU.HasseiKBN   = 1
    --<借方>
    LEFT JOIN #M0000WK_MA_HonsitenBunkai KRBUN                 --画面入力データ
    ON  KRBUN.DenpyouTani = KRSHU.DenpyouTani
    AND KRBUN.GyouNO      = KRSHU.GyouNO
    AND KRBUN.OperationComputerName    =   @p_UpdateComputerName
    AND KRBUN.OperationUserID          =   @p_UpdateUserID
    AND KRBUN.OperationJobID           =   @p_UpdateJobID

    LEFT JOIN #M0000WK_MA_HonsitenBunkai_Kanjou KRKAN          --総勘定科目マスタ
    ON  KRKAN.HasseiDate  = KRSHU.HasseiDate
    AND KRKAN.KanjouCD    = KRSHU.KanjouCD
    LEFT JOIN #M0000WK_MA_HonsitenBunkai_Hojo KRHOJ            --補助科目マスタ
    ON  KRHOJ.HasseiDate  = KRSHU.HasseiDate
    AND KRHOJ.KanjouCD    = KRSHU.KanjouCD
    AND KRHOJ.HojoCD      = KRSHU.HojoCD
    LEFT JOIN #M0000WK_MA_HonsitenBunkai_Utiwake KRUTI         --補助内訳科目マスタ
    ON  KRUTI.HasseiDate  = KRSHU.HasseiDate
    AND KRUTI.KanjouCD    = KRSHU.KanjouCD
    AND KRUTI.HojoCD      = KRSHU.HojoCD
    AND KRUTI.UtiwakeCD   = KRSHU.UtiwakeCD
    LEFT JOIN #M0000WK_MA_HonsitenBunkai_Bumon KRBMN          --部門マスタ
    ON  KRBMN.HasseiDate  = KRSHU.HasseiDate
    AND KRBMN.BumonCD     = KRSHU.BumonCD
    LEFT JOIN #M0000WK_MA_HonsitenBunkai_Zigyousho KRZIG          --事業所マスタ
    ON  KRZIG.HasseiDate  = KRSHU.HasseiDate
    AND KRZIG.ZigyoushoCD = KRSHU.ZigyoushoCD
    --<貸方>
    LEFT JOIN #M0000WK_MA_HonsitenBunkai KSBUN
    ON  KSBUN.DenpyouTani = KSSHU.DenpyouTani
    AND KSBUN.GyouNO      = KSSHU.GyouNO
    AND KSBUN.OperationComputerName    =   @p_UpdateComputerName
    AND KSBUN.OperationUserID          =   @p_UpdateUserID
    AND KSBUN.OperationJobID           =   @p_UpdateJobID        
    LEFT JOIN #M0000WK_MA_HonsitenBunkai_Kanjou KSKAN          --総勘定科目マスタ
    ON  KSKAN.HasseiDate  = KSSHU.HasseiDate
    AND KSKAN.KanjouCD    = KSSHU.KanjouCD
    LEFT JOIN #M0000WK_MA_HonsitenBunkai_Hojo KSHOJ          --補助科目マスタ
    ON  KSHOJ.HasseiDate  = KSSHU.HasseiDate
    AND KSHOJ.KanjouCD    = KSSHU.KanjouCD
    AND KSHOJ.HojoCD      = KSSHU.HojoCD
    LEFT JOIN #M0000WK_MA_HonsitenBunkai_Utiwake KSUTI          --補助内訳科目マスタ
    ON  KSUTI.HasseiDate  = KSSHU.HasseiDate
    AND KSUTI.KanjouCD    = KSSHU.KanjouCD
    AND KSUTI.HojoCD      = KSSHU.HojoCD
    AND KSUTI.UtiwakeCD   = KSSHU.UtiwakeCD
    LEFT JOIN #M0000WK_MA_HonsitenBunkai_Bumon KSBMN          --部門マスタ
    ON  KSBMN.HasseiDate  = KSSHU.HasseiDate
    AND KSBMN.BumonCD     = KSSHU.BumonCD
    LEFT JOIN #M0000WK_MA_HonsitenBunkai_Zigyousho KSZIG          --事業所マスタ
    ON  KSZIG.HasseiDate  = KSSHU.HasseiDate
    AND KSZIG.ZigyoushoCD = KSSHU.ZigyoushoCD
    WHERE KRSHU.HasseiKBN = 0
    
    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR
    
    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END




    --借方のみ存在するレコード分
    INSERT INTO #M0000WK_MA_HonsitenBunkaiKekka (
              OperationComputerName
            , OperationUserID
            , OperationDatetime
            , OperationJobID
            , DenpyouTani
            , HasseiDate
            , ZigyoushoCD
            , ZigyoushoKaiteiDate
            , SiwakeTani
            , SiwakeKBN
            , KariBumonCD
            , KariBumonKaiteiDate
            , KariKanjouCD
            , KariKanjouKaiteiDate
            , KariHojoCD
            , KariHojoKaiteiDate
            , KariUtiwakeCD
            , KariUtiwakeKaiteiDate
            , KariTorihikisakiCD
            , KariTorihikisakiKaiteiDate
            , KariTaishakuKBN
            , KariHasseiKBN
            , KariKingaku
            , KariZeigaku
            , KariZeiKamokuCD
            , KariZeiKaiteiDate
            , KariZeiTaishakuKBN
            , KariZeiHontaiCD
            , KariZeiHontaiKaiteiDate
            , KariZeiKBN
            , KariZeiRate
            , KariHasuuKBN
            , KariZeiKeisanKBN
            , KariZeiKamokuKBN
            , KariZeikomiKBN
            , KariTorihikisakiKBN
            , KariHojoKBN
            , KariUtiwakeKBN
            , KariBumonKBN
            , KariBunsekiCD1
            , KariBunsekiCD2
            , KariBunsekiCD3
            , KariBunsekiCD4
            , KariBunsekiCD5
            , KariSikinCD
            , KariProjectCD
            , KariBunsekiCustomizeCD1
            , KariBunsekiCustomizeCD2
            , KariBunsekiCustomizeCD3
            , KariBunsekiCustomizeCD4
            , KariBunsekiCustomizeCD5
            , KasiBumonCD
            , KasiBumonKaiteiDate
            , KasiKanjouCD
            , KasiKanjouKaiteiDate
            , KasiHojoCD
            , KasiHojoKaiteiDate
            , KasiUtiwakeCD
            , KasiUtiwakeKaiteiDate
            , KasiTorihikisakiCD
            , KasiTorihikisakiKaiteiDate
            , KasiTaishakuKBN
            , KasiHasseiKBN
            , KasiKingaku
            , KasiZeigaku
            , KasiZeiKamokuCD
            , KasiZeiKaiteiDate
            , KasiZeiTaishakuKBN
            , KasiZeiHontaiCD
            , KasiZeiHontaiKaiteiDate
            , KasiZeiKBN
            , KasiZeiRate
            , KasiHasuuKBN
            , KasiZeiKeisanKBN
            , KasiZeiKamokuKBN
            , KasiZeikomiKBN
            , KasiTorihikisakiKBN
            , KasiHojoKBN
            , KasiUtiwakeKBN
            , KasiBumonKBN
            , KasiBunsekiCD1
            , KasiBunsekiCD2
            , KasiBunsekiCD3
            , KasiBunsekiCD4
            , KasiBunsekiCD5
            , KasiSikinCD
            , KasiProjectCD
            , KasiBunsekiCustomizeCD1
            , KasiBunsekiCustomizeCD2
            , KasiBunsekiCustomizeCD3
            , KasiBunsekiCustomizeCD4
            , KasiBunsekiCustomizeCD5
            , MeisaiTekiyou
            , GyouNO
            , GyouNOEdaban
            , GyouNONew
            , CompanyNO
            , UpdateDatetime
            , UpdateCertificatedGuid
            , UpdateJobID
            , UpdateUserID
            , UpdateComputerName
            , UpdateComputerUserName
            , UpdateComputerDomainName
            )
    SELECT
              @p_UpdateComputerName
            , @p_UpdateUserID
            , @p_UpdateDatetime
            , @p_UpdateJobID    
            , KRSHU.DenpyouTani                                                                                     AS DenpyouTani
            , KRSHU.HasseiDate                                                                                      AS HasseiDate
            , KRSHU.ZigyoushoCD                                                                                     AS KariZigyoushoCD
            , KRZIG.KaiteiDate                                                                                      AS KariZiKaiteiDate
            , KRSHU.SiwakeTani                                                                                      AS SiwakeTani
            , KRBUN.SiwakeKBN                                                                                       AS SiwakeKBN
            , KRSHU.BumonCD                                                                                         AS KariBumonCD
            , KRBMN.KaiteiDate                                                                                      AS KariBuKaiteiDate
            , KRSHU.KanjouCD                                                                                        AS KariKanjouCD
            , KRKAN.KaiteiDate                                                                                      AS KariKaKaiteiDate
            , KRSHU.HojoCD                                                                                          AS KariHojoCD
            , KRHOJ.KaiteiDate                                                                                      AS KariHoKaiteiDate
            , KRSHU.UtiwakeCD                                                                                       AS KariUtiwakeCD
            , KRUTI.KaiteiDate                                                                                      AS KariUtiKaiteiDate
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariTorihikisakiCD             ELSE NULL                END) AS KariTorihikisakiCD
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariTorihikisakiKaiteiDate     ELSE NULL                END) AS KariTorihikisakiKaiteiDate
            , KRKAN.TaishakuKBN                                                                                     AS KariTaishakuKBN
            , 0                                                                                                     AS KariHasseiKBN
            , KRSHU.Kingaku                                                                                         AS KariKingaku
            , KRSHU.Zeigaku                                                                                         AS KariZeigaku
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariZeiKamokuCD         ELSE NULL                       END) AS KariZeikamokuCD
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariZeiKaiteiDate       ELSE NULL                       END) AS KariZeiKaiteiDate
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariZeiTaishakuKBN      ELSE NULL                       END) AS KariZeiTaishakuKBN
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariZeiHontaiCD         ELSE NULL                       END) AS KariHontaiCD
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariZeiHontaiKaiteiDate ELSE NULL                       END) AS KariHonKaiteiDate
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariZeiKBN              ELSE KRKAN.KariZeiKBN           END) AS KariZeiKBN
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariZeiRate             ELSE NULL                       END) AS KariZeiRate
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariHasuuKBN            ELSE NULL                       END) AS KariHasuuKBN
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariZeiKeisanKBN        ELSE NULL                       END) AS KariZeiKeisanKBN
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariZeiKamokuKBN        ELSE NULL                       END) AS KariZeiKamokuKBN
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariZeikomiKBN          ELSE KRKAN.KariZeikomiKBN       END) AS KariZeikomiKBN
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariTorihikisakiKBN     ELSE KRKAN.TorihikisakiKBN      END) AS KariTorihikisakiKBN
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariHojoKBN             ELSE KRKAN.HojoKBN              END) AS KariHojoKBN
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariUtiwakeKBN          ELSE ISNULL(KRHOJ.UtiwakeKBN,0) END) AS KariUtiwakeKBN
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariBumonKBN            ELSE KRKAN.BumonKBN             END) AS KariBumonKBN
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariBunsekiCD1          ELSE NULL                       END) AS KariBunsekiCD1
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariBunsekiCD2          ELSE NULL                       END) AS KariBunsekiCD2
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariBunsekiCD3          ELSE NULL                       END) AS KariBunsekiCD3
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariBunsekiCD4          ELSE NULL                       END) AS KariBunsekiCD4
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariBunsekiCD5          ELSE NULL                       END) AS KariBunsekiCD5
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariSikinCD             ELSE NULL                       END) AS KariSikinCD
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariProjectCD           ELSE NULL                       END) AS KariProjectCD
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariBunsekiCustomizeCD1 ELSE NULL                       END) AS KariBunsekiCustomizeCD1
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariBunsekiCustomizeCD2 ELSE NULL                       END) AS KariBunsekiCustomizeCD2
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariBunsekiCustomizeCD3 ELSE NULL                       END) AS KariBunsekiCustomizeCD3
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariBunsekiCustomizeCD4 ELSE NULL                       END) AS KariBunsekiCustomizeCD4
            , (CASE WHEN KRSHU.DataKBN =  0 THEN KRBUN.KariBunsekiCustomizeCD5 ELSE NULL                       END) AS KariBunsekiCustomizeCD5
            , NULL                                                                                                  AS KasiBumonCD
            , NULL                                                                                                  AS KasiBumonKaiteiDate
            , NULL                                                                                                  AS KasiKanjouCD
            , NULL                                                                                                  AS KasiKanjouKaiteiDate
            , NULL                                                                                                  AS KasiHojoCD
            , NULL                                                                                                  AS KasiHojoKaiteiDate
            , NULL                                                                                                  AS KasiUtiwakeCD
            , NULL                                                                                                  AS KasiUtiwakeKaiteiDate
            , NULL                                                                                                  AS KasiTorihikisakiCD
            , NULL                                                                                                  AS KasiTorihikisakiKaiteiDate
            , NULL                                                                                                  AS KasiTaishakuKBN
            , NULL                                                                                                  AS KasiHasseiKBN
            , NULL                                                                                                  AS KasiKingaku
            , NULL                                                                                                  AS KasiZeigaku
            , NULL                                                                                                  AS KasiZeikamokuCD
            , NULL                                                                                                  AS KasiZeiKaiteiDate
            , NULL                                                                                                  AS KasiZeiTaishakuKBN
            , NULL                                                                                                  AS KasiHontaiCD
            , NULL                                                                                                  AS KasiHonKaiteiDate
            , NULL                                                                                                  AS KasiZeiKBN
            , NULL                                                                                                  AS KasiZeiRate
            , NULL                                                                                                  AS KasiHasuuKBN
            , NULL                                                                                                  AS KasiZeiKeisanKBN
            , NULL                                                                                                  AS KasiZeiKamokuKBN
            , NULL                                                                                                  AS KasiZeikomiKBN
            , NULL                                                                                                  AS KasiTorihikisakiKBN
            , NULL                                                                                                  AS KasiHojoKBN
            , NULL                                                                                                  AS KasiUtiwakeKBN
            , NULL                                                                                                  AS KasiBumonKBN
            , NULL                                                                                                  AS KasiBunsekiCD1
            , NULL                                                                                                  AS KasiBunsekiCD2
            , NULL                                                                                                  AS KasiBunsekiCD3
            , NULL                                                                                                  AS KasiBunsekiCD4
            , NULL                                                                                                  AS KasiBunsekiCD5
            , NULL                                                                                                  AS KasiSikinCD
            , NULL                                                                                                  AS KasiProjectCD
            , NULL                                                                                                  AS KasiBunsekiCustomizeCD1
            , NULL                                                                                                  AS KasiBunsekiCustomizeCD2
            , NULL                                                                                                  AS KasiBunsekiCustomizeCD3
            , NULL                                                                                                  AS KasiBunsekiCustomizeCD4
            , NULL                                                                                                  AS KasiBunsekiCustomizeCD5
            , KRBUN.MeisaiTekiyou                                                                                   AS MeisaiTekiyou
            , KRSHU.GyouNO                                                                                          AS GyouNO
            , KRSHU.GyouNOEdaban                                                                                    AS GyouNOEdaban
            , KRSHU.GyouNONew                                                                                       AS GyouNONew
            , KRBUN.CompanyNO                                                                                       AS CompanyNO
            , @p_UpdateDatetime
            , @p_UpdateCertificatedGuid
            , @p_UpdateJobID
            , @p_UpdateUserID
            , @p_UpdateComputerName
            , @p_UpdateComputerUserName
            , @p_UpdateComputerDomainName
    FROM #M0000WK_MA_HonsitenBunkaiShukei KRSHU                --借方
    LEFT JOIN #M0000WK_MA_HonsitenBunkaiShukei KSSHU          --貸方
    ON  KSSHU.DenpyouTani    = KRSHU.DenpyouTani
    AND KSSHU.ZigyoushoCD    = KRSHU.ZigyoushoCD
    AND KSSHU.SiwakeTani     = KRSHU.SiwakeTani
    AND KSSHU.GyouNO         = KRSHU.GyouNO
    AND KSSHU.GyouNOEdaban   = KRSHU.GyouNOEdaban
    AND KSSHU.HasseiKBN   = 1
    --<借方>
    LEFT JOIN #M0000WK_MA_HonsitenBunkai KRBUN                    --画面入力データ
    ON  KRBUN.DenpyouTani = KRSHU.DenpyouTani
    AND KRBUN.GyouNO      = KRSHU.GyouNO
    AND KRBUN.OperationComputerName    =   @p_UpdateComputerName
    AND KRBUN.OperationUserID          =   @p_UpdateUserID
    AND KRBUN.OperationJobID           =   @p_UpdateJobID        
    LEFT JOIN #M0000WK_MA_HonsitenBunkai_Kanjou KRKAN             --総勘定科目マスタ
    ON  KRKAN.HasseiDate  = KRSHU.HasseiDate
    AND KRKAN.KanjouCD    = KRSHU.KanjouCD
    LEFT JOIN #M0000WK_MA_HonsitenBunkai_Hojo KRHOJ               --補助科目マスタ
    ON  KRHOJ.HasseiDate  = KRSHU.HasseiDate
    AND KRHOJ.KanjouCD    = KRSHU.KanjouCD
    AND KRHOJ.HojoCD      = KRSHU.HojoCD
    LEFT JOIN #M0000WK_MA_HonsitenBunkai_Utiwake KRUTI            --補助内訳科目マスタ
    ON  KRUTI.HasseiDate  = KRSHU.HasseiDate
    AND KRUTI.KanjouCD    = KRSHU.KanjouCD
    AND KRUTI.HojoCD      = KRSHU.HojoCD
    AND KRUTI.UtiwakeCD   = KRSHU.UtiwakeCD
    LEFT JOIN #M0000WK_MA_HonsitenBunkai_Bumon KRBMN              --部門マスタ
    ON  KRBMN.HasseiDate  = KRSHU.HasseiDate
    AND KRBMN.BumonCD     = KRSHU.BumonCD
    LEFT JOIN #M0000WK_MA_HonsitenBunkai_Zigyousho KRZIG          --事業所マスタ
    ON  KRZIG.HasseiDate  = KRSHU.HasseiDate
    AND KRZIG.ZigyoushoCD = KRSHU.ZigyoushoCD
    WHERE KRSHU.HasseiKBN = 0
    AND   KSSHU.DenpyouTani IS NULL
    
    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR
    
    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END




    --貸方のみ存在するレコード分
    INSERT INTO #M0000WK_MA_HonsitenBunkaiKekka (
              OperationComputerName
            , OperationUserID
            , OperationDatetime
            , OperationJobID
            , DenpyouTani
            , HasseiDate
            , ZigyoushoCD
            , ZigyoushoKaiteiDate
            , SiwakeTani
            , SiwakeKBN
            , KariBumonCD
            , KariBumonKaiteiDate
            , KariKanjouCD
            , KariKanjouKaiteiDate
            , KariHojoCD
            , KariHojoKaiteiDate
            , KariUtiwakeCD
            , KariUtiwakeKaiteiDate
            , KariTorihikisakiCD
            , KariTorihikisakiKaiteiDate
            , KariTaishakuKBN
            , KariHasseiKBN
            , KariKingaku
            , KariZeigaku
            , KariZeiKamokuCD
            , KariZeiKaiteiDate
            , KariZeiTaishakuKBN
            , KariZeiHontaiCD
            , KariZeiHontaiKaiteiDate
            , KariZeiKBN
            , KariZeiRate
            , KariHasuuKBN
            , KariZeiKeisanKBN
            , KariZeiKamokuKBN
            , KariZeikomiKBN
            , KariTorihikisakiKBN
            , KariHojoKBN
            , KariUtiwakeKBN
            , KariBumonKBN
            , KariBunsekiCD1
            , KariBunsekiCD2
            , KariBunsekiCD3
            , KariBunsekiCD4
            , KariBunsekiCD5
            , KariSikinCD
            , KariProjectCD
            , KariBunsekiCustomizeCD1
            , KariBunsekiCustomizeCD2
            , KariBunsekiCustomizeCD3
            , KariBunsekiCustomizeCD4
            , KariBunsekiCustomizeCD5
            , KasiBumonCD
            , KasiBumonKaiteiDate
            , KasiKanjouCD
            , KasiKanjouKaiteiDate
            , KasiHojoCD
            , KasiHojoKaiteiDate
            , KasiUtiwakeCD
            , KasiUtiwakeKaiteiDate
            , KasiTorihikisakiCD
            , KasiTorihikisakiKaiteiDate
            , KasiTaishakuKBN
            , KasiHasseiKBN
            , KasiKingaku
            , KasiZeigaku
            , KasiZeiKamokuCD
            , KasiZeiKaiteiDate
            , KasiZeiTaishakuKBN
            , KasiZeiHontaiCD
            , KasiZeiHontaiKaiteiDate
            , KasiZeiKBN
            , KasiZeiRate
            , KasiHasuuKBN
            , KasiZeiKeisanKBN
            , KasiZeiKamokuKBN
            , KasiZeikomiKBN
            , KasiTorihikisakiKBN
            , KasiHojoKBN
            , KasiUtiwakeKBN
            , KasiBumonKBN
            , KasiBunsekiCD1
            , KasiBunsekiCD2
            , KasiBunsekiCD3
            , KasiBunsekiCD4
            , KasiBunsekiCD5
            , KasiSikinCD
            , KasiProjectCD
            , KasiBunsekiCustomizeCD1
            , KasiBunsekiCustomizeCD2
            , KasiBunsekiCustomizeCD3
            , KasiBunsekiCustomizeCD4
            , KasiBunsekiCustomizeCD5
            , MeisaiTekiyou
            , GyouNO
            , GyouNOEdaban
            , GyouNONew
            , CompanyNO
            , UpdateDatetime
            , UpdateCertificatedGuid
            , UpdateJobID
            , UpdateUserID
            , UpdateComputerName
            , UpdateComputerUserName
            , UpdateComputerDomainName
            )
    SELECT
              @p_UpdateComputerName
            , @p_UpdateUserID
            , @p_UpdateDatetime
            , @p_UpdateJobID    
            , KSSHU.DenpyouTani                                                                                     AS DenpyouTani
            , KSSHU.HasseiDate                                                                                      AS HasseiDate
            , KSSHU.ZigyoushoCD                                                                                     AS KariZigyoushoCD
            , KSZIG.KaiteiDate                                                                                      AS KariZiKaiteiDate
            , KSSHU.SiwakeTani                                                                                      AS SiwakeTani
            , KSBUN.SiwakeKBN                                                                                       AS SiwakeKBN
            , NULL                                                                                                  AS KariBumonCD
            , NULL                                                                                                  AS KariBuKaiteiDate
            , NULL                                                                                                  AS KariKanjouCD
            , NULL                                                                                                  AS KariKaKaiteiDate
            , NULL                                                                                                  AS KariHojoCD
            , NULL                                                                                                  AS KariHoKaiteiDate
            , NULL                                                                                                  AS KariUtiwakeCD
            , NULL                                                                                                  AS KariUtiKaiteiDate
            , NULL                                                                                                  AS KariTorihikisakiCD
            , NULL                                                                                                  AS KariTorihikisakiKaiteiDate
            , NULL                                                                                                  AS KariTaishakuKBN
            , NULL                                                                                                  AS KariHasseiKBN
            , NULL                                                                                                  AS KariKingaku
            , NULL                                                                                                  AS KariZeigaku
            , NULL                                                                                                  AS KariZeikamokuCD
            , NULL                                                                                                  AS KariZeiKaiteiDate
            , NULL                                                                                                  AS KariZeiTaishakuKBN
            , NULL                                                                                                  AS KariHontaiCD
            , NULL                                                                                                  AS KariHonKaiteiDate
            , NULL                                                                                                  AS KariZeiKBN
            , NULL                                                                                                  AS KariZeiRate
            , NULL                                                                                                  AS KariHasuuKBN
            , NULL                                                                                                  AS KariZeiKeisanKBN
            , NULL                                                                                                  AS KariZeiKamokuKBN
            , NULL                                                                                                  AS KariZeikomiKBN
            , NULL                                                                                                  AS KariTorihikisakiKBN
            , NULL                                                                                                  AS KariHojoKBN
            , NULL                                                                                                  AS KariUtiwakeKBN
            , NULL                                                                                                  AS KariBumonKBN
            , NULL                                                                                                  AS KariBunsekiCD1
            , NULL                                                                                                  AS KariBunsekiCD2
            , NULL                                                                                                  AS KariBunsekiCD3
            , NULL                                                                                                  AS KariBunsekiCD4
            , NULL                                                                                                  AS KariBunsekiCD5
            , NULL                                                                                                  AS KariSikinCD
            , NULL                                                                                                  AS KariProjectCD
            , NULL                                                                                                  AS KariBunsekiCustomizeCD1
            , NULL                                                                                                  AS KariBunsekiCustomizeCD2
            , NULL                                                                                                  AS KariBunsekiCustomizeCD3
            , NULL                                                                                                  AS KariBunsekiCustomizeCD4
            , NULL                                                                                                  AS KariBunsekiCustomizeCD5
            , KSSHU.BumonCD                                                                                         AS KasiBumonCD
            , KSBMN.KaiteiDate                                                                                      AS KasiBumonKaiteiDate
            , KSSHU.KanjouCD                                                                                        AS KasiKanjouCD
            , KSKAN.KaiteiDate                                                                                      AS KasiKanjouKaiteiDate
            , KSSHU.HojoCD                                                                                          AS KasiHojoCD
            , KSHOJ.KaiteiDate                                                                                      AS KasiHojoKaiteiDate
            , KSSHU.UtiwakeCD                                                                                       AS KasiUtiwakeCD
            , KSUTI.KaiteiDate                                                                                      AS KasiUtiwakeKaiteiDate
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiTorihikisakiCD             ELSE NULL                END) AS KasiTorihikisakiCD
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiTorihikisakiKaiteiDate     ELSE NULL                END) AS KasiTorihikisakiKaiteiDate
            , KSKAN.TaishakuKBN                                                                                     AS KasiTaishakuKBN
            , 1                                                                                                     AS KariHasseiKBN
            , KSSHU.Kingaku                                                                                         AS KasiKingaku
            , KSSHU.Zeigaku                                                                                         AS KasiZeigaku
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiZeiKamokuCD         ELSE NULL                       END) AS KasiZeikamokuCD
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiZeiKaiteiDate       ELSE NULL                       END) AS KasiZeiKaiteiDate
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiZeiTaishakuKBN      ELSE NULL                       END) AS KasiZeiTaishakuKBN
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiZeiHontaiCD         ELSE NULL                       END) AS KasiHontaiCD
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiZeiHontaiKaiteiDate ELSE NULL                       END) AS KasiHonKaiteiDate
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiZeiKBN              ELSE KSKAN.KasiZeiKBN           END) AS KasiZeiKBN
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiZeiRate             ELSE NULL                       END) AS KasiZeiRate
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiHasuuKBN            ELSE NULL                       END) AS KasiHasuuKBN
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiZeiKeisanKBN        ELSE NULL                       END) AS KasiZeiKeisanKBN
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiZeiKamokuKBN        ELSE NULL                       END) AS KasiZeiKamokuKBN
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiZeikomiKBN          ELSE KSKAN.KasiZeikomiKBN       END) AS KasiZeikomiKBN
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiTorihikisakiKBN     ELSE KSKAN.TorihikisakiKBN      END) AS KasiTorihikisakiKBN
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiHojoKBN             ELSE KSKAN.HojoKBN              END) AS KasiHojoKBN
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiUtiwakeKBN          ELSE ISNULL(KSHOJ.UtiwakeKBN,0) END) AS KasiUtiwakeKBN
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiBumonKBN            ELSE KSKAN.BumonKBN             END) AS KasiBumonKBN
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiBunsekiCD1          ELSE NULL                       END) AS KasiBunsekiCD1
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiBunsekiCD2          ELSE NULL                       END) AS KasiBunsekiCD2
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiBunsekiCD3          ELSE NULL                       END) AS KasiBunsekiCD3
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiBunsekiCD4          ELSE NULL                       END) AS KasiBunsekiCD4
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiBunsekiCD5          ELSE NULL                       END) AS KasiBunsekiCD5
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiSikinCD             ELSE NULL                       END) AS KasiSikinCD
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiProjectCD           ELSE NULL                       END) AS KasiProjectCD
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiBunsekiCustomizeCD1 ELSE NULL                       END) AS KasiBunsekiCustomizeCD1
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiBunsekiCustomizeCD2 ELSE NULL                       END) AS KasiBunsekiCustomizeCD2
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiBunsekiCustomizeCD3 ELSE NULL                       END) AS KasiBunsekiCustomizeCD3
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiBunsekiCustomizeCD4 ELSE NULL                       END) AS KasiBunsekiCustomizeCD4
            , (CASE WHEN KSSHU.DataKBN =  0 THEN KSBUN.KasiBunsekiCustomizeCD5 ELSE NULL                       END) AS KasiBunsekiCustomizeCD5
            , KSBUN.MeisaiTekiyou                                                                                   AS MeisaiTekiyou
            , KSSHU.GyouNO                                                                                          AS GyouNO
            , KSSHU.GyouNOEdaban                                                                                    AS GyouNOEdaban
            , KSSHU.GyouNONew                                                                                       AS GyouNONew
            , KSBUN.CompanyNO                                                                                       AS CompanyNO
            , @p_UpdateDatetime
            , @p_UpdateCertificatedGuid
            , @p_UpdateJobID
            , @p_UpdateUserID
            , @p_UpdateComputerName
            , @p_UpdateComputerUserName
            , @p_UpdateComputerDomainName
    FROM #M0000WK_MA_HonsitenBunkaiShukei KSSHU                --貸方
    LEFT JOIN #M0000WK_MA_HonsitenBunkaiShukei KRSHU           --借方
    ON  KRSHU.DenpyouTani    = KSSHU.DenpyouTani
    AND KRSHU.ZigyoushoCD    = KSSHU.ZigyoushoCD
    AND KRSHU.SiwakeTani     = KSSHU.SiwakeTani
    AND KRSHU.GyouNO         = KSSHU.GyouNO
    AND KRSHU.GyouNOEdaban   = KSSHU.GyouNOEdaban
    AND KRSHU.HasseiKBN   = 0
    --<貸方>
    LEFT JOIN #M0000WK_MA_HonsitenBunkai KSBUN
    ON  KSBUN.DenpyouTani = KSSHU.DenpyouTani
    AND KSBUN.GyouNO      = KSSHU.GyouNO
    AND KSBUN.OperationComputerName    =   @p_UpdateComputerName
    AND KSBUN.OperationUserID          =   @p_UpdateUserID
    AND KSBUN.OperationJobID           =   @p_UpdateJobID                
    LEFT JOIN #M0000WK_MA_HonsitenBunkai_Kanjou KSKAN          --総勘定科目マスタ
    ON  KSKAN.HasseiDate  = KSSHU.HasseiDate
    AND KSKAN.KanjouCD    = KSSHU.KanjouCD
    LEFT JOIN #M0000WK_MA_HonsitenBunkai_Hojo KSHOJ            --補助科目マスタ
    ON  KSHOJ.HasseiDate  = KSSHU.HasseiDate
    AND KSHOJ.KanjouCD    = KSSHU.KanjouCD
    AND KSHOJ.HojoCD      = KSSHU.HojoCD
    LEFT JOIN #M0000WK_MA_HonsitenBunkai_Utiwake KSUTI         --補助内訳科目マスタ
    ON  KSUTI.HasseiDate  = KSSHU.HasseiDate
    AND KSUTI.KanjouCD    = KSSHU.KanjouCD
    AND KSUTI.HojoCD      = KSSHU.HojoCD
    AND KSUTI.UtiwakeCD   = KSSHU.UtiwakeCD
    LEFT JOIN #M0000WK_MA_HonsitenBunkai_Bumon KSBMN           --部門マスタ
    ON  KSBMN.HasseiDate  = KSSHU.HasseiDate
    AND KSBMN.BumonCD     = KSSHU.BumonCD
    LEFT JOIN #M0000WK_MA_HonsitenBunkai_Zigyousho KSZIG       --事業所マスタ
    ON  KSZIG.HasseiDate  = KSSHU.HasseiDate
    AND KSZIG.ZigyoushoCD = KSSHU.ZigyoushoCD
    WHERE KSSHU.HasseiKBN = 1
    AND   KRSHU.DenpyouTani IS NULL
    
    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR
    
    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END 


    -------------------------------------------------------------------------------------
    -- 本支店分解を行わない伝票の場合の処理                                             -
    -------------------------------------------------------------------------------------

    --明細事業所がヘッダ事業所と異なるものがない場合結果ワークにそのまま更新
    INSERT INTO #M0000WK_MA_HonsitenBunkaiKekka (
              OperationComputerName
            , OperationUserID
            , OperationDatetime
            , OperationJobID
            , DenpyouTani
            , DenpyouTaniNew
            , HasseiDate
            , ZigyoushoCD
            , ZigyoushoKaiteiDate
            , SiwakeTani
            , SiwakeKBN
            , KariBumonCD
            , KariBumonKaiteiDate
            , KariKanjouCD
            , KariKanjouKaiteiDate
            , KariHojoCD
            , KariHojoKaiteiDate
            , KariUtiwakeCD
            , KariUtiwakeKaiteiDate
            , KariTorihikisakiCD
            , KariTorihikisakiKaiteiDate
            , KariTaishakuKBN
            , KariHasseiKBN
            , KariKingaku
            , KariZeigaku
            , KariZeiKamokuCD
            , KariZeiKaiteiDate
            , KariZeiTaishakuKBN
            , KariZeiHontaiCD
            , KariZeiHontaiKaiteiDate
            , KariZeiKBN
            , KariZeiRate
            , KariHasuuKBN
            , KariZeiKeisanKBN
            , KariZeiKamokuKBN
            , KariZeikomiKBN
            , KariTorihikisakiKBN
            , KariHojoKBN
            , KariUtiwakeKBN
            , KariBumonKBN
            , KariBunsekiCD1
            , KariBunsekiCD2
            , KariBunsekiCD3
            , KariBunsekiCD4
            , KariBunsekiCD5
            , KariSikinCD
            , KariProjectCD
            , KariBunsekiCustomizeCD1
            , KariBunsekiCustomizeCD2
            , KariBunsekiCustomizeCD3
            , KariBunsekiCustomizeCD4
            , KariBunsekiCustomizeCD5
            , KasiBumonCD
            , KasiBumonKaiteiDate
            , KasiKanjouCD
            , KasiKanjouKaiteiDate
            , KasiHojoCD
            , KasiHojoKaiteiDate
            , KasiUtiwakeCD
            , KasiUtiwakeKaiteiDate
            , KasiTorihikisakiCD
            , KasiTorihikisakiKaiteiDate
            , KasiTaishakuKBN
            , KasiHasseiKBN
            , KasiKingaku
            , KasiZeigaku
            , KasiZeiKamokuCD
            , KasiZeiKaiteiDate
            , KasiZeiTaishakuKBN
            , KasiZeiHontaiCD
            , KasiZeiHontaiKaiteiDate
            , KasiZeiKBN
            , KasiZeiRate
            , KasiHasuuKBN
            , KasiZeiKeisanKBN
            , KasiZeiKamokuKBN
            , KasiZeikomiKBN
            , KasiTorihikisakiKBN
            , KasiHojoKBN
            , KasiUtiwakeKBN
            , KasiBumonKBN
            , KasiBunsekiCD1
            , KasiBunsekiCD2
            , KasiBunsekiCD3
            , KasiBunsekiCD4
            , KasiBunsekiCD5
            , KasiSikinCD
            , KasiProjectCD
            , KasiBunsekiCustomizeCD1
            , KasiBunsekiCustomizeCD2
            , KasiBunsekiCustomizeCD3
            , KasiBunsekiCustomizeCD4
            , KasiBunsekiCustomizeCD5
            , MeisaiTekiyou
            , GyouNO
            , GyouNOEdaban
            , GyouNONew
            , CompanyNO
            , UpdateDatetime
            , UpdateCertificatedGuid
            , UpdateJobID
            , UpdateUserID
            , UpdateComputerName
            , UpdateComputerUserName
            , UpdateComputerDomainName
            )
    SELECT
              @p_UpdateComputerName
            , @p_UpdateUserID
            , @p_UpdateDatetime
            , @p_UpdateJobID    
            , BUN.DenpyouTani
            , BUN.DenpyouTani
            , BUN.HasseiDate
            , BUN.ZigyoushoCD
            , BUN.ZigyoushoKaiteiDate
            , BUN.SiwakeTani
            , BUN.SiwakeKBN
            , BUN.KariBumonCD
            , BUN.KariBumonKaiteiDate
            , BUN.KariKanjouCD
            , BUN.KariKanjouKaiteiDate
            , BUN.KariHojoCD
            , BUN.KariHojoKaiteiDate
            , BUN.KariUtiwakeCD
            , BUN.KariUtiwakeKaiteiDate
            , BUN.KariTorihikisakiCD
            , BUN.KariTorihikisakiKaiteiDate
            , BUN.KariTaishakuKBN
            , BUN.KariHasseiKBN
            , BUN.KariKingaku
            , BUN.KariZeigaku
            , BUN.KariZeiKamokuCD
            , BUN.KariZeiKaiteiDate
            , BUN.KariZeiTaishakuKBN
            , BUN.KariZeiHontaiCD
            , BUN.KariZeiHontaiKaiteiDate
            , BUN.KariZeiKBN
            , BUN.KariZeiRate
            , BUN.KariHasuuKBN
            , BUN.KariZeiKeisanKBN
            , BUN.KariZeiKamokuKBN
            , BUN.KariZeikomiKBN
            , BUN.KariTorihikisakiKBN
            , BUN.KariHojoKBN
            , BUN.KariUtiwakeKBN
            , BUN.KariBumonKBN
            , BUN.KariBunsekiCD1
            , BUN.KariBunsekiCD2
            , BUN.KariBunsekiCD3
            , BUN.KariBunsekiCD4
            , BUN.KariBunsekiCD5
            , BUN.KariSikinCD
            , BUN.KariProjectCD
            , BUN.KariBunsekiCustomizeCD1
            , BUN.KariBunsekiCustomizeCD2
            , BUN.KariBunsekiCustomizeCD3
            , BUN.KariBunsekiCustomizeCD4
            , BUN.KariBunsekiCustomizeCD5
            , BUN.KasiBumonCD
            , BUN.KasiBumonKaiteiDate
            , BUN.KasiKanjouCD
            , BUN.KasiKanjouKaiteiDate
            , BUN.KasiHojoCD
            , BUN.KasiHojoKaiteiDate
            , BUN.KasiUtiwakeCD
            , BUN.KasiUtiwakeKaiteiDate
            , BUN.KasiTorihikisakiCD
            , BUN.KasiTorihikisakiKaiteiDate
            , BUN.KasiTaishakuKBN
            , BUN.KasiHasseiKBN
            , BUN.KasiKingaku
            , BUN.KasiZeigaku
            , BUN.KasiZeiKamokuCD
            , BUN.KasiZeiKaiteiDate
            , BUN.KasiZeiTaishakuKBN
            , BUN.KasiZeiHontaiCD
            , BUN.KasiZeiHontaiKaiteiDate
            , BUN.KasiZeiKBN
            , BUN.KasiZeiRate
            , BUN.KasiHasuuKBN
            , BUN.KasiZeiKeisanKBN
            , BUN.KasiZeiKamokuKBN
            , BUN.KasiZeikomiKBN
            , BUN.KasiTorihikisakiKBN
            , BUN.KasiHojoKBN
            , BUN.KasiUtiwakeKBN
            , BUN.KasiBumonKBN
            , BUN.KasiBunsekiCD1
            , BUN.KasiBunsekiCD2
            , BUN.KasiBunsekiCD3
            , BUN.KasiBunsekiCD4
            , BUN.KasiBunsekiCD5
            , BUN.KasiSikinCD
            , BUN.KasiProjectCD
            , BUN.KasiBunsekiCustomizeCD1
            , BUN.KasiBunsekiCustomizeCD2
            , BUN.KasiBunsekiCustomizeCD3
            , BUN.KasiBunsekiCustomizeCD4
            , BUN.KasiBunsekiCustomizeCD5
            , BUN.MeisaiTekiyou
            , BUN.GyouNO
            , 0
            , BUN.GyouNO
            , BUN.CompanyNO
            , @p_UpdateDatetime
            , @p_UpdateCertificatedGuid
            , @p_UpdateJobID
            , @p_UpdateUserID
            , @p_UpdateComputerName
            , @p_UpdateComputerUserName
            , @p_UpdateComputerDomainName
    FROM      #M0000WK_MA_HonsitenBunkai        BUN
    LEFT JOIN #M0000WK_MA_HonsitenBunkaiTaishou HBT
    ON    HBT.DenpyouTani           = BUN.DenpyouTani
    WHERE BUN.OperationComputerName = @p_UpdateComputerName
    AND   BUN.OperationUserID       = @p_UpdateUserID
    AND   BUN.OperationJobID        = @p_UpdateJobID        
    AND   HBT.DenpyouTani           IS NULL
    
    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR

    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END

    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR
    
    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END


    -------------------------------------------------------------------------------------
    -- 新伝票単位を採番                                                                 -
    -------------------------------------------------------------------------------------
    -- カーソル宣言
    DECLARE CURSIWAKE CURSOR FOR
    SELECT
          DenpyouTani
        , ZigyoushoCD
        , SiwakeTani
        , GyouNO
        , GyouNOEdaban
    FROM #M0000WK_MA_HonsitenBunkaiKekka
    WHERE OperationComputerName    =   @p_UpdateComputerName
    AND   OperationUserID          =   @p_UpdateUserID
    AND   OperationJobID           =   @p_UpdateJobID
    ORDER BY   DenpyouTani, ZigyoushoCD, SiwakeTani, GyouNO, GyouNOEdaban

    -- CURSIWAKEをオープンし、先頭レコードをフェッチし、変数に待避する
    OPEN CURSIWAKE
    FETCH NEXT FROM CURSIWAKE INTO
          @DenpyouTani
        , @ZigyoushoCD
        , @SiwakeTani
        , @GyouNO
        , @GyouNOEdaban


    SELECT   @FETCH_STATUS_CURSIWAKE = @@FETCH_STATUS
           , @ErrorCode              = @@ERROR

    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。   
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END

    IF  @FETCH_STATUS_CURSIWAKE <> 0
    BEGIN
        -- 引数に戻り値をセット
        SET @po_ErrorKBN        = '1'                    --1：システムエラー
        SET @po_ErrorCD1        = '000'                  --会計は"000"で統一
        SET @po_ErrorCD2        = '00119'                --基幹会計は"00119"で統一
        SET @po_ErrorProcName   = OBJECT_NAME(@@PROCID)
        SET @po_ErrorSummary    = ''
        SET @po_ErrorDetail     = ''
        SET @po_ErrorBtnKBN     = 0
        SET @po_ErrorBtnDefault = 0
        GOTO ERR_SHORI
    END

    SET @DenpyouTaniNew = 0
    SET @GyouNONew = 0
    SET @DenpyouTani_BK = 0
    SET @ZigyoushoCD_BK = ''
    SET @SiwakeTani_BK = 0
    SET @GyouNO_BK = 0
    SET @GyouNOEdaban_BK = 0

    
    -- 変数.FETCH ステートメントステータス = 0 の間、ループ
    WHILE @@FETCH_STATUS = 0
    BEGIN
    
        --伝票単位、事業所ごとに新伝票単位をセット
        IF @DenpyouTani <> @DenpyouTani_BK OR @ZigyoushoCD <> @ZigyoushoCD_BK
        BEGIN
            SET  @DenpyouTaniNew = @DenpyouTaniNew + 1
            SET  @GyouNONew = 0
            
            UPDATE   KEK SET
                  DenpyouTaniNew = @DenpyouTaniNew
            FROM #M0000WK_MA_HonsitenBunkaiKekka KEK
            WHERE OperationComputerName    =   @p_UpdateComputerName
            AND   OperationUserID          =   @p_UpdateUserID
            AND   OperationJobID           =   @p_UpdateJobID
            AND   DenpyouTani              =   @DenpyouTani
            AND   ZigyoushoCD              =   @ZigyoushoCD

            -- @@ERROR を取得し、変数に保存する。
            SET @ErrorCode = @@ERROR
                
            -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
            IF  @ErrorCode <> 0
            BEGIN
                GOTO ERR_SHORI
            END 
    
        END

        --伝票単位、事業所、仕訳単位、行番号、行番号枝番ごとに新行番号をセット
        IF @DenpyouTani <> @DenpyouTani_BK OR @ZigyoushoCD <> @ZigyoushoCD_BK OR @SiwakeTani <> @SiwakeTani_BK OR @GyouNO <> @GyouNO_BK OR @GyouNOEdaban <> @GyouNOEdaban_BK
        BEGIN
            SET  @GyouNONew = @GyouNONew + 1

            UPDATE KEK SET
                  GyouNONew = @GyouNONew
            FROM #M0000WK_MA_HonsitenBunkaiKekka KEK
            WHERE OperationComputerName    =   @p_UpdateComputerName
            AND   OperationUserID          =   @p_UpdateUserID
            AND   OperationJobID           =   @p_UpdateJobID
            AND   DenpyouTani              =   @DenpyouTani
            AND   ZigyoushoCD              =   @ZigyoushoCD
            AND   SiwakeTani               =   @SiwakeTani
            AND   GyouNO                   =   @GyouNO
            AND   GyouNOEdaban             =   @GyouNOEdaban

            -- @@ERROR を取得し、変数に保存する。
            SET @ErrorCode = @@ERROR
            
            -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
            IF  @ErrorCode <> 0
            BEGIN
                GOTO ERR_SHORI
            END 
                
        END
        
        -- 変数に以下の値をセット
        SET @DenpyouTani_BK  = @DenpyouTani
        SET @ZigyoushoCD_BK  = @ZigyoushoCD
        SET @SiwakeTani_BK   = @SiwakeTani
        SET @GyouNO_BK       = @GyouNO
        SET @GyouNOEdaban_BK = @GyouNOEdaban
        
        FETCH NEXT FROM CURSIWAKE INTO
              @DenpyouTani
            , @ZigyoushoCD
            , @SiwakeTani
            , @GyouNO
            , @GyouNOEdaban

        -- @@ERROR を取得し、変数に保存する。
        SET @ErrorCode = @@ERROR
        
        -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
        IF  @ErrorCode <> 0
        BEGIN
            GOTO ERR_SHORI
        END
    END
    -- カーソル：CURSIWAKEの開放
    CLOSE CURSIWAKE
    DEALLOCATE CURSIWAKE

    -------------------------------------------------------------------------------------
    -- 消費税計算処理。                                                                 -
    -------------------------------------------------------------------------------------
    --振替科目の税区分、税込区分で消費税額等を再計算する(但し、本支店分解処理を行った伝票のみ)
    --消費税額の待避
    --(税込区分=4(外税)時も内税の計算となる→外税で計算すると貸借一致しなくなる為(相手金額は変わらない))
    INSERT INTO #M0000WK_MA_HonsitenBunkai_Zeigaku (
              HasseiKBN
            , DenpyouTaniNew
            , ZigyoushoCD
            , GyouNONew
            , ZeiRate
            , HasuuKBN
            , ZeiKeisanKBN
            , ZeiKamokuKBN
            , Zeigaku
            )
    SELECT
              KEK.HasseiKBN
            , KEK.DenpyouTaniNew
            , KEK.ZigyoushoCD
            , KEK.GyouNONew
            , KEK.ZeiRate
            , KEK.HasuuKBN
            , KEK.ZeiKeisanKBN
            , KEK.ZeiKamokuKBN
            , (CASE KEK.HasuuKBN WHEN 0 THEN SIGN(KEK.Zeigaku) * FLOOR(ABS(KEK.Zeigaku))                --切り捨て

                                           WHEN 1 THEN SIGN(KEK.Zeigaku) * FLOOR(ABS(KEK.Zeigaku) + 0.5)          --四捨五入
                                           WHEN 2 THEN (CASE WHEN (KEK.Zeigaku - SIGN(KEK.Zeigaku) * FLOOR(ABS(KEK.Zeigaku))) = 0 
                                                             THEN SIGN(KEK.Zeigaku) * FLOOR(ABS(KEK.Zeigaku)) 
                                                             ELSE SIGN(KEK.Zeigaku) * FLOOR(ABS(KEK.Zeigaku)) + 1                --端数があれば、切り上げ
                                                       END)                                                               --切り上げ
              END) AS Zeigaku
    FROM (
            --借方
            SELECT
                      0 AS HasseiKBN
                    , KEK.DenpyouTaniNew
                    , KEK.ZigyoushoCD
                    , KEK.GyouNONew
                    , ZKB.ZeiRate
                    , ZKB.HasuuKBN
                    , ZKB.ZeiKeisanKBN
                    , ZKB.ZeiKamokuKBN
                    , ((KEK.KariKingaku / (1 + ZKB.ZeiRate)) * ZKB.ZeiRate) AS Zeigaku
            FROM #M0000WK_MA_HonsitenBunkaiKekka KEK
            INNER JOIN #M0000WK_MA_HonsitenBunkaiTaishou HBT
            ON    HBT.DenpyouTani           = KEK.DenpyouTani
            INNER JOIN M0000MS_MA_ZeiKBN ZKB
            ON  ZKB.ZeiKBN = KEK.KariZeiKBN
            WHERE KEK.KariZeigaku IS NULL         --消費税再計算対象のもの(振替時に固定でNULLをセットしている為)
            AND   KEK.KariZeikomiKBN IN (3,4)
            AND   ZKB.ZeiKeisanKBN <> 0
            UNION ALL
            --貸方
            SELECT
                      1 AS HasseiKBN
                    , KEK.DenpyouTaniNew
                    , KEK.ZigyoushoCD
                    , KEK.GyouNONew
                    , ZKB.ZeiRate
                    , ZKB.HasuuKBN
                    , ZKB.ZeiKeisanKBN
                    , ZKB.ZeiKamokuKBN
                    , ((KEK.KasiKingaku / (1 + ZKB.ZeiRate)) * ZKB.ZeiRate) AS Zeigaku
            FROM #M0000WK_MA_HonsitenBunkaiKekka KEK
            INNER JOIN #M0000WK_MA_HonsitenBunkaiTaishou HBT
            ON    HBT.DenpyouTani           = KEK.DenpyouTani
            INNER JOIN M0000MS_MA_ZeiKBN ZKB
            ON  ZKB.ZeiKBN = KEK.KasiZeiKBN
            WHERE KEK.KasiZeigaku IS NULL         --消費税再計算対象のもの(振替時に固定でNULLをセットしている為)
            AND   KEK.KasiZeikomiKBN IN (3,4)
            AND   ZKB.ZeiKeisanKBN <> 0
    ) KEK

    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR
    
    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END
    
    --計算した金額、消費税のセット
    --借方
    UPDATE KEK SET
              KEK.KariZeiRate          = ZGK.ZeiRate
            , KEK.KariHasuuKBN         = ZGK.HasuuKBN
            , KEK.KariZeiKeisanKBN     = ZGK.ZeiKeisanKBN
            , KEK.KariZeiKamokuKBN     = ZGK.ZeiKamokuKBN
            , KEK.KariKingaku          = (KEK.KariKingaku - ZGK.Zeigaku)
            , KEK.KariZeigaku          = ZGK.Zeigaku
            , KEK.KariZeiKamokuCD      = (CASE WHEN ZGK.ZeiKamokuKBN = 1 THEN KCL.KariukeZeiCD          ELSE KCL.KaribaraiZeiCD          END)
            , KEK.KariZeiKaiteiDate    = (CASE WHEN ZGK.ZeiKamokuKBN = 1 THEN KCL.KariukeZeiKaiteiDate  ELSE KCL.KaribaraiZeiKaiteiDate  END)
            , KEK.KariZeiTaishakuKBN   = (CASE WHEN ZGK.ZeiKamokuKBN = 1 THEN KCL.KariukeZeiTaishakuKBN ELSE KCL.KaribaraiZeiTaishakuKBN END)
    FROM #M0000WK_MA_HonsitenBunkaiKekka KEK
    INNER JOIN #M0000WK_MA_HonsitenBunkaiTaishou HBT
    ON    HBT.DenpyouTani           = KEK.DenpyouTani
    INNER JOIN #M0000WK_MA_HonsitenBunkai_Zeigaku ZGK
    ON  ZGK.HasseiKBN   = 0
    AND ZGK.DenpyouTaniNew = KEK.DenpyouTaniNew
    AND ZGK.ZigyoushoCD    = KEK.ZigyoushoCD
    AND ZGK.GyouNONew      = KEK.GyouNONew
    INNER JOIN #M0000WK_MA_HonsitenBunkai_KanjouControl KCL
    ON  KCL.HasseiDate      = KEK.HasseiDate

    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR

    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END

    --貸方
    UPDATE KEK SET
              KEK.KasiZeiRate          = ZGK.ZeiRate
            , KEK.KasiHasuuKBN         = ZGK.HasuuKBN
            , KEK.KasiZeiKeisanKBN     = ZGK.ZeiKeisanKBN
            , KEK.KasiZeiKamokuKBN     = ZGK.ZeiKamokuKBN
            , KEK.KasiKingaku          = (KEK.KasiKingaku - ZGK.Zeigaku)
            , KEK.KasiZeigaku          = ZGK.Zeigaku
            , KEK.KasiZeiKamokuCD      = (CASE WHEN ZGK.ZeiKamokuKBN = 1 THEN KCL.KariukeZeiCD          ELSE KCL.KaribaraiZeiCD          END)
            , KEK.KasiZeiKaiteiDate    = (CASE WHEN ZGK.ZeiKamokuKBN = 1 THEN KCL.KariukeZeiKaiteiDate  ELSE KCL.KaribaraiZeiKaiteiDate  END)
            , KEK.KasiZeiTaishakuKBN   = (CASE WHEN ZGK.ZeiKamokuKBN = 1 THEN KCL.KariukeZeiTaishakuKBN ELSE KCL.KaribaraiZeiTaishakuKBN END)
    FROM #M0000WK_MA_HonsitenBunkaiKekka KEK
    INNER JOIN #M0000WK_MA_HonsitenBunkaiTaishou HBT
    ON    HBT.DenpyouTani           = KEK.DenpyouTani
    INNER JOIN #M0000WK_MA_HonsitenBunkai_Zeigaku ZGK
    ON  ZGK.HasseiKBN   = 1
    AND ZGK.DenpyouTaniNew = KEK.DenpyouTaniNew
    AND ZGK.ZigyoushoCD    = KEK.ZigyoushoCD
    AND ZGK.GyouNONew      = KEK.GyouNONew
    INNER JOIN #M0000WK_MA_HonsitenBunkai_KanjouControl KCL
    ON  KCL.HasseiDate      = KEK.HasseiDate
    
    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR

    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END
    

    -------------------------------------------------------------------------------------
    -- 結果ワークを管理区分の値により更新する                                            -
    -------------------------------------------------------------------------------------
    UPDATE WK
    SET
--2008/04/10 H.Suzuki 変更 開始 *セット条件見直し
--         --補助科目、補助内訳科目、部門については、この後の仕訳整合性チェックで、チェックを行う為、管理区分による更新は行わない。
--         --但し、改定日だけは更新する(部門マスタに"*"は登録されているので、"*"の改定日が部門管理しない場合でもセットされるのを防ぐ為)
--           WK.KariBumonCD                = ISNULL(WK.KariBumonCD  , '*')
--         , WK.KariBumonKaiteiDate        = CASE WHEN WK.KariBumonKBN = 0 THEN NULL ELSE WK.KariBumonKaiteiDate END
--         , WK.KariHojoCD                 = ISNULL(WK.KariHojoCD   , '*')
--         , WK.KariHojoKaiteiDate         = CASE WHEN WK.KariHojoKBN  = 0 THEN NULL ELSE WK.KariHojoKaiteiDate END
--         , WK.KariUtiwakeCD              = ISNULL(WK.KariUtiwakeCD, '*')
--         , WK.KariUtiwakeKaiteiDate      = CASE WHEN WK.KariHojoKBN  = 0 OR WK.KariUtiwakeKBN = 0 THEN NULL ELSE WK.KariUtiwakeKaiteiDate END
--         , WK.KariTorihikisakiCD         = CASE WHEN WK.KariTorihikisakiKBN = 0 THEN '*'  ELSE WK.KariTorihikisakiCD         END
--         , WK.KariTorihikisakiKaiteiDate = CASE WHEN WK.KariTorihikisakiKBN = 0 THEN NULL ELSE WK.KariTorihikisakiKaiteiDate END
--         , WK.KariZeiKamokuCD            = CASE WHEN WK.KariZeiKeisanKBN <> 0 AND WK.KariZeiKamokuKBN <> 0 AND WK.KariZeikomiKBN IN (3,4) THEN WK.KariZeiKamokuCD    ELSE NULL END
--         , WK.KariZeiKaiteiDate          = CASE WHEN WK.KariZeiKeisanKBN <> 0 AND WK.KariZeiKamokuKBN <> 0 AND WK.KariZeikomiKBN IN (3,4) THEN WK.KariZeiKaiteiDate  ELSE NULL END
--         , WK.KariZeiTaishakuKBN         = CASE WHEN WK.KariZeiKeisanKBN <> 0 AND WK.KariZeiKamokuKBN <> 0 AND WK.KariZeikomiKBN IN (3,4) THEN WK.KariZeiTaishakuKBN ELSE NULL END
--         , WK.KasiBumonCD                = ISNULL(WK.KasiBumonCD  , '*')
--         , WK.KasiBumonKaiteiDate        = CASE WHEN WK.KasiBumonKBN = 0 THEN NULL ELSE WK.KasiBumonKaiteiDate END
--         , WK.KasiHojoCD                 = ISNULL(WK.KasiHojoCD   , '*')
--         , WK.KasiHojoKaiteiDate         = CASE WHEN WK.KasiHojoKBN  = 0 THEN NULL ELSE WK.KasiHojoKaiteiDate END
--         , WK.KasiUtiwakeCD              = ISNULL(WK.KasiUtiwakeCD, '*')
--         , WK.KasiUtiwakeKaiteiDate      = CASE WHEN WK.KasiHojoKBN  = 0 OR WK.KasiUtiwakeKBN = 0 THEN NULL ELSE WK.KasiUtiwakeKaiteiDate END
--         , WK.KasiTorihikisakiCD         = CASE WHEN WK.KasiTorihikisakiKBN = 0 THEN '*'  ELSE WK.KasiTorihikisakiCD         END
--         , WK.KasiTorihikisakiKaiteiDate = CASE WHEN WK.KasiTorihikisakiKBN = 0 THEN NULL ELSE WK.KasiTorihikisakiKaiteiDate END
--         , WK.KasiZeiKamokuCD            = CASE WHEN WK.KasiZeiKeisanKBN <> 0 AND WK.KasiZeiKamokuKBN <> 0 AND WK.KasiZeikomiKBN IN (3,4) THEN WK.KasiZeiKamokuCD    ELSE NULL END
--         , WK.KasiZeiKaiteiDate          = CASE WHEN WK.KasiZeiKeisanKBN <> 0 AND WK.KasiZeiKamokuKBN <> 0 AND WK.KasiZeikomiKBN IN (3,4) THEN WK.KasiZeiKaiteiDate  ELSE NULL END
--         , WK.KasiZeiTaishakuKBN         = CASE WHEN WK.KasiZeiKeisanKBN <> 0 AND WK.KasiZeiKamokuKBN <> 0 AND WK.KasiZeikomiKBN IN (3,4) THEN WK.KasiZeiTaishakuKBN ELSE NULL END
        --補助科目、補助内訳科目、部門については、この後の仕訳整合性チェックで、チェックを行う為、管理しない場合に、強制的に*をセットしない。NULLの時のみ'*'セットする
        --改定日も更新する(部門マスタに"*"は登録されているので、"*"の改定日が部門管理しない場合でもセットされるのを防ぐ為)
        --また、補助科目、補助内訳科目、部門のコードについては、管理しない、かつNULLの時、'*'をセットする
          WK.KariBumonCD                = CASE WHEN WK.KariBumonKBN = 0 THEN ISNULL(WK.KariBumonCD  , '*') ELSE WK.KariBumonCD END
        , WK.KariBumonKaiteiDate        = CASE WHEN WK.KariBumonKBN = 0 THEN NULL ELSE WK.KariBumonKaiteiDate END
        , WK.KariHojoCD                 = CASE WHEN WK.KariHojoKBN  = 0 THEN ISNULL(WK.KariHojoCD   , '*') ELSE WK.KariHojoCD  END
        , WK.KariHojoKaiteiDate         = CASE WHEN WK.KariHojoKBN  = 0 THEN NULL ELSE WK.KariHojoKaiteiDate END
        , WK.KariUtiwakeCD              = CASE WHEN WK.KariHojoKBN  = 0 OR WK.KariUtiwakeKBN = 0 THEN ISNULL(WK.KariUtiwakeCD, '*') ELSE WK.KariUtiwakeCD END
        , WK.KariUtiwakeKaiteiDate      = CASE WHEN WK.KariHojoKBN  = 0 OR WK.KariUtiwakeKBN = 0 THEN NULL ELSE WK.KariUtiwakeKaiteiDate END
--2008/05/22 H.Suzuki 変更 開始 *セット条件見直し
--         , WK.KariTorihikisakiCD         = CASE WHEN WK.KariTorihikisakiKBN = 0 THEN '*'  ELSE WK.KariTorihikisakiCD         END
        , WK.KariTorihikisakiCD         = CASE WHEN WK.KariTorihikisakiKBN = 0 THEN ISNULL(WK.KariTorihikisakiCD, '*')  ELSE WK.KariTorihikisakiCD         END
--2008/05/22 H.Suzuki 変更 終了
        , WK.KariTorihikisakiKaiteiDate = CASE WHEN WK.KariTorihikisakiKBN = 0 THEN NULL ELSE WK.KariTorihikisakiKaiteiDate END
        , WK.KariZeiKamokuCD            = CASE WHEN WK.KariZeiKeisanKBN <> 0 AND WK.KariZeiKamokuKBN <> 0 AND WK.KariZeikomiKBN IN (3,4) THEN WK.KariZeiKamokuCD    ELSE NULL END
        , WK.KariZeiKaiteiDate          = CASE WHEN WK.KariZeiKeisanKBN <> 0 AND WK.KariZeiKamokuKBN <> 0 AND WK.KariZeikomiKBN IN (3,4) THEN WK.KariZeiKaiteiDate  ELSE NULL END
        , WK.KariZeiTaishakuKBN         = CASE WHEN WK.KariZeiKeisanKBN <> 0 AND WK.KariZeiKamokuKBN <> 0 AND WK.KariZeikomiKBN IN (3,4) THEN WK.KariZeiTaishakuKBN ELSE NULL END
        , WK.KasiBumonCD                = CASE WHEN WK.KasiBumonKBN = 0 THEN ISNULL(WK.KasiBumonCD  , '*') ELSE WK.KasiBumonCD END
        , WK.KasiBumonKaiteiDate        = CASE WHEN WK.KasiBumonKBN = 0 THEN NULL ELSE WK.KasiBumonKaiteiDate END
        , WK.KasiHojoCD                 = CASE WHEN WK.KasiHojoKBN  = 0 THEN ISNULL(WK.KasiHojoCD   , '*') ELSE WK.KasiHojoCD  END
        , WK.KasiHojoKaiteiDate         = CASE WHEN WK.KasiHojoKBN  = 0 THEN NULL ELSE WK.KasiHojoKaiteiDate END
        , WK.KasiUtiwakeCD              = CASE WHEN WK.KasiHojoKBN  = 0 OR WK.KasiUtiwakeKBN = 0 THEN ISNULL(WK.KasiUtiwakeCD, '*') ELSE WK.KasiUtiwakeCD END
        , WK.KasiUtiwakeKaiteiDate      = CASE WHEN WK.KasiHojoKBN  = 0 OR WK.KasiUtiwakeKBN = 0 THEN NULL ELSE WK.KasiUtiwakeKaiteiDate END
--2008/05/22 H.Suzuki 変更 開始 *セット条件見直し
--         , WK.KasiTorihikisakiCD         = CASE WHEN WK.KasiTorihikisakiKBN = 0 THEN '*'  ELSE WK.KasiTorihikisakiCD         END
        , WK.KasiTorihikisakiCD         = CASE WHEN WK.KasiTorihikisakiKBN = 0 THEN ISNULL(WK.KasiTorihikisakiCD, '*')  ELSE WK.KasiTorihikisakiCD         END
--2008/05/22 H.Suzuki 変更 終了
        , WK.KasiTorihikisakiKaiteiDate = CASE WHEN WK.KasiTorihikisakiKBN = 0 THEN NULL ELSE WK.KasiTorihikisakiKaiteiDate END
        , WK.KasiZeiKamokuCD            = CASE WHEN WK.KasiZeiKeisanKBN <> 0 AND WK.KasiZeiKamokuKBN <> 0 AND WK.KasiZeikomiKBN IN (3,4) THEN WK.KasiZeiKamokuCD    ELSE NULL END
        , WK.KasiZeiKaiteiDate          = CASE WHEN WK.KasiZeiKeisanKBN <> 0 AND WK.KasiZeiKamokuKBN <> 0 AND WK.KasiZeikomiKBN IN (3,4) THEN WK.KasiZeiKaiteiDate  ELSE NULL END
        , WK.KasiZeiTaishakuKBN         = CASE WHEN WK.KasiZeiKeisanKBN <> 0 AND WK.KasiZeiKamokuKBN <> 0 AND WK.KasiZeikomiKBN IN (3,4) THEN WK.KasiZeiTaishakuKBN ELSE NULL END
--2008/04/10 H.Suzuki 変更 変更
    FROM #M0000WK_MA_HonsitenBunkaiKekka WK
    
    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR

    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END
    
    -------------------------------------------------------------------------------------
    -- 貸借判断処理                                                                     -
    -------------------------------------------------------------------------------------
    -- ワーク削除
    DELETE FROM M0000WK_MA_SiwakeTaishakuHandan
    WHERE OperationComputerName = @p_UpdateComputerName
    AND   OperationUserID       = @p_UpdateUserID
    AND   OperationJobID        = @p_UpdateJobID
    
    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR

    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END

    -- 本支店仕訳分解結果ワークから、仕訳貸借判断ワークに待避
    INSERT INTO M0000WK_MA_SiwakeTaishakuHandan
    (
          OperationComputerName
        , OperationUserID
        , OperationDatetime
        , OperationJobID
        , ControlKey
        , SiwakeKBN
        , KariKanjouCD
        , KariKingaku
        , KariZeigaku
        , KariZeiKamokuCD
        , KasiKanjouCD
        , KasiKingaku
        , KasiZeigaku
        , KasiZeiKamokuCD
        , GyouNO
        , CompanyNO
        , UpdateDatetime
        , UpdateCertificatedGuid
        , UpdateJobID
        , UpdateUserID
        , UpdateComputerName
        , UpdateComputerUserName
        , UpdateComputerDomainName
    )
    SELECT 
          @p_UpdateComputerName
        , @p_UpdateUserID
        , @p_UpdateDatetime
        , @p_UpdateJobID
        , DenpyouTaniNew
        , SiwakeKBN
        , KariKanjouCD
        , KariKingaku
        , KariZeigaku
        , KariZeiKamokuCD
        , KasiKanjouCD
        , KasiKingaku
        , KasiZeigaku
        , KasiZeiKamokuCD
        , GyouNONew
        , CompanyNO
        , @p_UpdateDatetime
        , @p_UpdateCertificatedGuid
        , @p_UpdateJobID
        , @p_UpdateUserID
        , @p_UpdateComputerName
        , @p_UpdateComputerUserName
        , @p_UpdateComputerDomainName
    FROM #M0000WK_MA_HonsitenBunkaiKekka
    WHERE OperationComputerName = @p_UpdateComputerName
    AND   OperationUserID       = @p_UpdateUserID
    AND   OperationJobID        = @p_UpdateJobID
    
    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR

    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END

    -- 共通ストアド：貸借判断処理を実行
    EXECUTE @SUB_ErrorCode = M0000PR_MA_TaishakuHandan
          @po_ErrorKBN                = @SUB_ErrorKBN        OUTPUT
        , @po_ErrorCD1                = @SUB_ErrorCD1        OUTPUT
        , @po_ErrorCD2                = @SUB_ErrorCD2        OUTPUT
        , @po_ErrorProcName           = @SUB_ErrorProcName   OUTPUT
        , @po_ErrorSummary            = @SUB_ErrorSummary    OUTPUT
        , @po_ErrorDetail             = @SUB_ErrorDetail     OUTPUT
        , @po_ErrorBtnKBN             = @SUB_ErrorBtnKBN     OUTPUT
        , @po_ErrorBtnDefault         = @SUB_ErrorBtnDefault OUTPUT
        , @p_UpdateDatetime           = @p_UpdateDatetime          
        , @p_UpdateCertificatedGuid   = @p_UpdateCertificatedGuid  
        , @p_UpdateJobID              = @p_UpdateJobID             
        , @p_UpdateUserID             = @p_UpdateUserID            
        , @p_UpdateComputerName       = @p_UpdateComputerName      
        , @p_UpdateComputerUserName   = @p_UpdateComputerUserName  
        , @p_UpdateComputerDomainName = @p_UpdateComputerDomainName

    -- 変数.サブエラーコード <> 0 の時
    IF @SUB_ErrorCode <> 0
    BEGIN
        SET  @ErrorCode          = @SUB_ErrorCode
        SET  @po_ErrorKBN        = @SUB_ErrorKBN
        SET  @po_ErrorCD1        = @SUB_ErrorCD1
        SET  @po_ErrorCD2        = @SUB_ErrorCD2
        SET  @po_ErrorProcName   = @SUB_ErrorProcName
        SET  @po_ErrorSummary    = @SUB_ErrorSummary
        SET  @po_ErrorDetail     = @SUB_ErrorDetail
        SET  @po_ErrorBtnKBN     = @SUB_ErrorBtnKBN
        SET  @po_ErrorBtnDefault = @SUB_ErrorBtnDefault
        GOTO ERR_SHORI
    END
        
    -- 変数.サブエラー区分 <> 0 の時
    IF @SUB_ErrorKBN <> 0
    BEGIN
        SET  @po_ErrorKBN        = @SUB_ErrorKBN
        SET  @po_ErrorCD1        = @SUB_ErrorCD1
        SET  @po_ErrorCD2        = @SUB_ErrorCD2
        SET  @po_ErrorProcName   = @SUB_ErrorProcName
        SET  @po_ErrorSummary    = @SUB_ErrorSummary
        SET  @po_ErrorDetail     = @SUB_ErrorDetail
        SET  @po_ErrorBtnKBN     = @SUB_ErrorBtnKBN
        SET  @po_ErrorBtnDefault = @SUB_ErrorBtnDefault
        GOTO ERR_SHORI
    END
    
    
    -- 貸借判断結果を、仕訳入力画面明細ワークに更新
    UPDATE MEI SET
        MEI.SiwakeKBN = TAI.SiwakeKBN
    FROM #M0000WK_MA_HonsitenBunkaiKekka MEI
    INNER JOIN M0000WK_MA_SiwakeTaishakuHandan TAI
    ON  TAI.OperationComputerName = MEI.OperationComputerName
    AND TAI.OperationUserID       = MEI.OperationUserID
    AND TAI.OperationJobID        = MEI.OperationJobID
    AND TAI.ControlKey            = MEI.DenpyouTaniNew
    AND TAI.GyouNO                = MEI.GyouNONew
    WHERE MEI.OperationComputerName = @p_UpdateComputerName
    AND   MEI.OperationUserID       = @p_UpdateUserID
    AND   MEI.OperationJobID        = @p_UpdateJobID
    
    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR

    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END

--2008/02/06 追加 N.Sasaki 開始 振替伝票仕訳チェック処理に振替伝票仕訳チェック区分を追加し、チェックを行うか判断する。
    IF @p_HurikaeDenpyouCheckKBN <> 2 
    BEGIN
--2008/02/06 追加 N.Sasaki 終了
        -------------------------------------------------------------------------------------
        -- 事業所チェック                                                                   -
        -------------------------------------------------------------------------------------

--2008/05/14 追加 N.Sasaki 開始 振替伝票仕訳チェック区分 = 0 の場合のみ入力事業所存在チェックを行なう。
        IF @p_HurikaeDenpyouCheckKBN = 0
        BEGIN
--2008/05/14 追加 N.Sasaki 終了

            -- 入力事業所がない場合エラー
            SELECT @Kensuu = COUNT(*)
            FROM #M0000WK_MA_HonsitenBunkai BUN
            LEFT JOIN ( SELECT 
                              DenpyouTani
                            , ZigyoushoCD
                        FROM #M0000WK_MA_HonsitenBunkaiKekka
                        WHERE OperationComputerName = @p_UpdateComputerName
                        AND   OperationUserID       = @p_UpdateUserID
                        AND   OperationJobID        = @p_UpdateJobID
            ) KEK
            ON  BUN.DenpyouTani = KEK.DenpyouTani
            AND BUN.ZigyoushoCD = KEK.ZigyoushoCD
            WHERE BUN.OperationComputerName = @p_UpdateComputerName
            AND   BUN.OperationUserID       = @p_UpdateUserID
            AND   BUN.OperationJobID        = @p_UpdateJobID
            AND   KEK.DenpyouTani IS NULL
            
            SET   @ErrorCode     = @@ERROR

            -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
            IF  @ErrorCode    <> 0
            BEGIN
                GOTO ERR_SHORI
            END

            IF @Kensuu <> 0
            BEGIN
                SET @po_ErrorKBN = 2
                SET @po_ErrorCD1 = '000'
                SET @po_ErrorCD2 = '00461'
                SET @po_ErrorProcName   = OBJECT_NAME(@@PROCID)
                SET @po_ErrorSummary    = ''
                SET @po_ErrorDetail     = ''
                SET @po_ErrorBtnKBN     = 0
                SET @po_ErrorBtnDefault = 0
                GOTO ERR_SHORI        
            END

--2008/05/14 追加 N.Sasaki 開始 振替伝票仕訳チェック区分 = 0 の場合のみ入力事業所存在チェックを行なう。
        END
--2008/05/14 追加 N.Sasaki 終了
        
--2008/02/06 追加 N.Sasaki 開始 事業所のチェックを追加

        -- 事業所インバリッドチェック

        SET @SQL = ''

        SET @SQL = @SQL + ' SELECT '
        SET @SQL = @SQL + '       KEK.ZigyoushoCD '
        SET @SQL = @SQL + ' FROM #M0000WK_MA_HonsitenBunkaiKekka KEK'
        SET @SQL = @SQL + ' LEFT JOIN M0000MS_MA_Zigyousho MZI '
        SET @SQL = @SQL + ' ON  MZI.ZigyoushoCD = KEK.ZigyoushoCD '
        SET @SQL = @SQL + ' AND MZI.KaiteiDate  = KEK.ZigyoushoKaiteiDate '

--2008/05/14 変更 N.Sasaki 開始 事業所のセキュリティは考慮しない

/*
        -- 引数.事業所セキュリティパターンコード<>""かつ 振替伝票仕訳チェック区分 = 0の場合
        IF ISNULL(@p_ZigyoushoSecurityPatternCD,'') <> '' AND @p_HurikaeDenpyouCheckKBN = 0
        BEGIN
            SET @SQL = @SQL + ' LEFT JOIN M0000MS_MA_ZigyoushoSecurityYouso YOS '
            SET @SQL = @SQL + ' ON  YOS.SecurityPatternCD = ' + dbo.M0000FC_Escape_Varchar(@p_ZigyoushoSecurityPatternCD)
            SET @SQL = @SQL + ' AND YOS.ZigyoushoCDYouso     = KEK.ZigyoushoCD '
            SET @SQL = @SQL + ' WHERE (MZI.ZigyoushoCD IS NULL OR YOS.ZigyoushoCDYouso IS NULL ) '
            SET @SQL = @SQL + ' AND   KEK.OperationComputerName = ' + dbo.M0000FC_Escape_Varchar(@p_UpdateComputerName)
            SET @SQL = @SQL + ' AND   KEK.OperationUserID       = ' + dbo.M0000FC_Escape_Varchar(@p_UpdateUserID)
            SET @SQL = @SQL + ' AND   KEK.OperationJobID        = ' + dbo.M0000FC_Escape_Varchar(@p_UpdateJobID + '''' )
        END
        ELSE
        BEGIN
            SET @SQL = @SQL + ' WHERE MZI.ZigyoushoCD IS NULL '
            SET @SQL = @SQL + ' AND   KEK.OperationComputerName = ' + dbo.M0000FC_Escape_Varchar(@p_UpdateComputerName)
            SET @SQL = @SQL + ' AND   KEK.OperationUserID       = ' + dbo.M0000FC_Escape_Varchar(@p_UpdateUserID)
            SET @SQL = @SQL + ' AND   KEK.OperationJobID        = ' + dbo.M0000FC_Escape_Varchar(@p_UpdateJobID + '''' )
        END

*/

        SET @SQL = @SQL + ' WHERE MZI.ZigyoushoCD IS NULL '
        SET @SQL = @SQL + ' AND   KEK.OperationComputerName = ' + dbo.M0000FC_Escape_Varchar(@p_UpdateComputerName)
        SET @SQL = @SQL + ' AND   KEK.OperationUserID       = ' + dbo.M0000FC_Escape_Varchar(@p_UpdateUserID)
--2008/05/23 変更 H.Suzuki 開始 振替事業所チェックの条件見直し
--         SET @SQL = @SQL + ' AND   KEK.OperationJobID        = ' + dbo.M0000FC_Escape_Varchar(@p_UpdateJobID + '''' )
        SET @SQL = @SQL + ' AND   KEK.OperationJobID        = ' + dbo.M0000FC_Escape_Varchar(@p_UpdateJobID)
--2008/05/23 変更 H.Suzuki 終了

--2008/05/14 変更 N.Sasaki 終了

        EXECUTE (@SQL)

        -- @@ERROR を取得し、変数に保存する。
        SELECT @ErrorCode = @@ERROR
               , @Kensuu = @@ROWCOUNT
               
        -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
        IF  @ErrorCode <> 0
        BEGIN
            GOTO ERR_SHORI
        END


        IF @Kensuu <> 0
        BEGIN
            SET @po_ErrorKBN = 2
            SET @po_ErrorCD1 = '000'
            SET @po_ErrorCD2 = '00751'
            SET @po_ErrorProcName   = OBJECT_NAME(@@PROCID)
            SET @po_ErrorSummary    = ''
            SET @po_ErrorDetail     = ''
            SET @po_ErrorBtnKBN     = 0
            SET @po_ErrorBtnDefault = 0
            GOTO ERR_SHORI        
        END

        -- 事業所名未入力チェック
        SELECT 
            @Kensuu = COUNT(*)
        FROM #M0000WK_MA_HonsitenBunkaiKekka KEK 
        INNER JOIN M0000MS_MA_Zigyousho MZI 
        ON  MZI.ZigyoushoCD   = KEK.ZigyoushoCD 
        AND MZI.KaiteiDate    = KEK.ZigyoushoKaiteiDate
        WHERE KEK.OperationComputerName =   @p_UpdateComputerName  
        AND   KEK.OperationUserID       =   @p_UpdateUserID  
        AND   KEK.OperationJobID        =   @p_UpdateJobID  
        AND   MZI.ZigyoushoName IS NULL

        -- @@ERROR を取得し、変数に保存する。
        SET @ErrorCode = @@ERROR

        -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
        IF  @ErrorCode <> 0
        BEGIN
            GOTO ERR_SHORI
        END

        IF @Kensuu <> 0
        BEGIN
            SET @po_ErrorKBN = 2
            SET @po_ErrorCD1 = '000'
            SET @po_ErrorCD2 = '00752'
            SET @po_ErrorProcName   = OBJECT_NAME(@@PROCID)
            SET @po_ErrorSummary    = ''
            SET @po_ErrorDetail     = ''
            SET @po_ErrorBtnKBN     = 0
            SET @po_ErrorBtnDefault = 0
            GOTO ERR_SHORI        
        END

--2008/02/06 追加 N.Sasaki 終了

        -------------------------------------------------------------------------------------
        -- 総勘定科目チェック                                                               -
        -------------------------------------------------------------------------------------
        
        -- 総勘定科目インバリッドチェック

        SET @SQL = ''

        SET @SQL = @SQL + ' SELECT '
        SET @SQL = @SQL + '   SKEK.KanjouCD '
        SET @SQL = @SQL + ' FROM ( '
        SET @SQL = @SQL + '     SELECT  '
        SET @SQL = @SQL + '           KariKanjouCD         AS KanjouCD '
        SET @SQL = @SQL + '         , KariKanjouKaiteiDate AS KanjouKaiteiDate '    
        SET @SQL = @SQL + '     FROM #M0000WK_MA_HonsitenBunkaiKekka '
        SET @SQL = @SQL + '     WHERE KariKanjouCD IS NOT NULL '
        SET @SQL = @SQL + '     AND   OperationComputerName = ' + dbo.M0000FC_Escape_Varchar(@p_UpdateComputerName)
        SET @SQL = @SQL + '     AND   OperationUserID       = ' + dbo.M0000FC_Escape_Varchar(@p_UpdateUserID)
        SET @SQL = @SQL + '     AND   OperationJobID        = ' + dbo.M0000FC_Escape_Varchar(@p_UpdateJobID)
        SET @SQL = @SQL + '     UNION  '
        SET @SQL = @SQL + '     SELECT  '
        SET @SQL = @SQL + '           KasiKanjouCD         AS KanjouCD '
        SET @SQL = @SQL + '         , KasiKanjouKaiteiDate AS KanjouKaiteiDate '
        SET @SQL = @SQL + '     FROM #M0000WK_MA_HonsitenBunkaiKekka '
        SET @SQL = @SQL + '     WHERE KasiKanjouCD IS NOT NULL '
        SET @SQL = @SQL + '     AND   OperationComputerName = ' + dbo.M0000FC_Escape_Varchar(@p_UpdateComputerName)
        SET @SQL = @SQL + '     AND   OperationUserID       = ' + dbo.M0000FC_Escape_Varchar(@p_UpdateUserID)
        SET @SQL = @SQL + '     AND   OperationJobID        = ' + dbo.M0000FC_Escape_Varchar(@p_UpdateJobID)
        SET @SQL = @SQL + ' ) SKEK '
        SET @SQL = @SQL + ' LEFT JOIN M0000MS_MA_Kanjou MKA '
        SET @SQL = @SQL + ' ON  MKA.KanjouCD         = SKEK.KanjouCD '
        SET @SQL = @SQL + ' AND MKA.KaiteiDate = SKEK.KanjouKaiteiDate '

--2008/02/06 変更 N.Sasaki 開始 振替伝票仕訳チェック処理に振替伝票仕訳チェック区分を追加し、チェックを行うか判断する。
--    -- 引数.総勘定セキュリティパターンコード<>""の場合
--    IF ISNULL(@p_KanjouSecurityPatternCD,'') <> ''
        -- 引数.総勘定セキュリティパターンコード<>""かつ 振替伝票仕訳チェック区分 = 0の場合
        IF ISNULL(@p_KanjouSecurityPatternCD,'') <> '' AND @p_HurikaeDenpyouCheckKBN = 0
--2008/02/06 変更 N.Sasaki 終了
        BEGIN        
            SET @SQL = @SQL + ' LEFT JOIN M0000MS_MA_KanjouSecurityYouso YOS '
            SET @SQL = @SQL + ' ON  YOS.SecurityPatternCD = ' + dbo.M0000FC_Escape_Varchar(@p_KanjouSecurityPatternCD)
            SET @SQL = @SQL + ' AND YOS.KanjouCDYouso     = SKEK.KanjouCD '
            SET @SQL = @SQL + ' WHERE MKA.KanjouCD IS NULL OR YOS.KanjouCDYouso IS NOT NULL '        
        END
        ELSE
        BEGIN
            SET @SQL = @SQL + ' WHERE MKA.KanjouCD IS NULL '
        END

        EXECUTE (@SQL)

        -- @@ERROR を取得し、変数に保存する。
        SELECT @ErrorCode = @@ERROR
               , @Kensuu = @@ROWCOUNT
               
        -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
        IF  @ErrorCode <> 0
        BEGIN
            GOTO ERR_SHORI
        END


        IF @Kensuu <> 0
        BEGIN
            SET @po_ErrorKBN = 2
            SET @po_ErrorCD1 = '000'
            SET @po_ErrorCD2 = '00462'
            SET @po_ErrorProcName   = OBJECT_NAME(@@PROCID)
            SET @po_ErrorSummary    = ''
            SET @po_ErrorDetail     = ''
            SET @po_ErrorBtnKBN     = 0
            SET @po_ErrorBtnDefault = 0
            GOTO ERR_SHORI        
        END


        -- 総勘定科目名未入力チェック
        SELECT 
            @Kensuu = COUNT(*)
        FROM ( 
            SELECT  
                  KariKanjouCD         AS KanjouCD 
                , KariKanjouKaiteiDate AS KanjouKaiteiDate
            FROM #M0000WK_MA_HonsitenBunkaiKekka 
            WHERE KariKanjouCD IS NOT NULL 
            AND   OperationComputerName =   @p_UpdateComputerName  
            AND   OperationUserID       =   @p_UpdateUserID  
            AND   OperationJobID        =   @p_UpdateJobID  
            UNION  
            SELECT  
                  KasiKanjouCD         AS KanjouCD 
                , KasiKanjouKaiteiDate AS KanjouKaiteiDate 
            FROM #M0000WK_MA_HonsitenBunkaiKekka 
            WHERE KasiKanjouCD IS NOT NULL 
            AND   OperationComputerName =   @p_UpdateComputerName  
            AND   OperationUserID       =   @p_UpdateUserID  
            AND   OperationJobID        =   @p_UpdateJobID  
        ) SKEK    
        INNER JOIN M0000MS_MA_Kanjou MKA 
        ON  MKA.KanjouCD   = SKEK.KanjouCD 
        AND MKA.KaiteiDate = SKEK.KanjouKaiteiDate
        WHERE MKA.KanjouName IS NULL

        -- @@ERROR を取得し、変数に保存する。
        SET @ErrorCode = @@ERROR

        -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
        IF  @ErrorCode <> 0
        BEGIN
            GOTO ERR_SHORI
        END

        IF @Kensuu <> 0
        BEGIN
            SET @po_ErrorKBN = 2
            SET @po_ErrorCD1 = '000'
            SET @po_ErrorCD2 = '00463'
            SET @po_ErrorProcName   = OBJECT_NAME(@@PROCID)
            SET @po_ErrorSummary    = ''
            SET @po_ErrorDetail     = ''
            SET @po_ErrorBtnKBN     = 0
            SET @po_ErrorBtnDefault = 0
            GOTO ERR_SHORI        
        END



        -- 取引先区分チェック
        SELECT 
            @Kensuu = COUNT(*)
        FROM ( 
            SELECT  
                  KariKanjouCD         AS KanjouCD 
                , KariKanjouKaiteiDate AS KanjouKaiteiDate
                , HasseiDate
                , ZigyoushoCD
            FROM #M0000WK_MA_HonsitenBunkaiKekka 
            WHERE KariKanjouCD IS NOT NULL 
            AND   OperationComputerName =   @p_UpdateComputerName  
            AND   OperationUserID       =   @p_UpdateUserID  
            AND   OperationJobID        =   @p_UpdateJobID  
            UNION  
            SELECT  
                  KasiKanjouCD         AS KanjouCD 
                , KasiKanjouKaiteiDate AS KanjouKaiteiDate 
                , HasseiDate
                , ZigyoushoCD
            FROM #M0000WK_MA_HonsitenBunkaiKekka 
            WHERE KasiKanjouCD IS NOT NULL 
            AND   OperationComputerName =   @p_UpdateComputerName  
            AND   OperationUserID       =   @p_UpdateUserID  
            AND   OperationJobID        =   @p_UpdateJobID  
        ) SKEK    
        INNER JOIN #M0000WK_MA_HonsitenBunkai_HonsitenKanjou HSK 
        ON  HSK.HasseiDate    = SKEK.HasseiDate 
        AND HSK.KanjouCD      = SKEK.KanjouCD 
        INNER JOIN M0000MS_MA_Kanjou MKA
        ON  MKA.KanjouCD   = SKEK.KanjouCD 
        AND MKA.KaiteiDate = SKEK.KanjouKaiteiDate 
        WHERE MKA.TorihikisakiKBN <> 0
        
        -- @@ERROR を取得し、変数に保存する。
        SET @ErrorCode = @@ERROR

        -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
        IF  @ErrorCode <> 0
        BEGIN
            GOTO ERR_SHORI
        END

        IF @Kensuu <> 0
        BEGIN
            SET @po_ErrorKBN = 2
            SET @po_ErrorCD1 = '000'
            SET @po_ErrorCD2 = '00489'
            SET @po_ErrorProcName   = OBJECT_NAME(@@PROCID)
            SET @po_ErrorSummary    = ''
            SET @po_ErrorDetail     = ''
            SET @po_ErrorBtnKBN     = 0
            SET @po_ErrorBtnDefault = 0
            GOTO ERR_SHORI        
        END


        -------------------------------------------------------------------------------------
        -- 補助科目チェック                                                                 -
        -------------------------------------------------------------------------------------
        
        -- 補助科目インバリッドチェック
        SELECT 
            SKEK.HojoCD 
        FROM ( 
            SELECT  
                  KariKanjouCD         AS KanjouCD 
                , KariHojoCD           AS HojoCD 
                , KariHojoKaiteiDate   AS HojoKaiteiDate
            FROM #M0000WK_MA_HonsitenBunkaiKekka 
            WHERE ISNULL(KariHojoKBN,0) <> 0
            AND   OperationComputerName =   @p_UpdateComputerName  
            AND   OperationUserID       =   @p_UpdateUserID  
            AND   OperationJobID        =   @p_UpdateJobID  
            UNION  
            SELECT  
                  KasiKanjouCD         AS KanjouCD 
                , KasiHojoCD           AS HojoCD 
                , KasiHojoKaiteiDate   AS HojoKaiteiDate 
            FROM #M0000WK_MA_HonsitenBunkaiKekka 
            WHERE ISNULL(KasiHojoKBN,0) <> 0
            AND   OperationComputerName =   @p_UpdateComputerName  
            AND   OperationUserID       =   @p_UpdateUserID  
            AND   OperationJobID        =   @p_UpdateJobID  
        ) SKEK 
        LEFT JOIN M0000MS_MA_Hojo MHO 
        ON  MHO.KanjouCD     = SKEK.KanjouCD 
        AND MHO.HojoCD       = SKEK.HojoCD 
        AND MHO.KaiteiDate   = SKEK.HojoKaiteiDate 
        WHERE MHO.HojoCD IS NULL

        -- @@ERROR を取得し、変数に保存する。
        SELECT @ErrorCode = @@ERROR
               , @Kensuu = @@ROWCOUNT
               
        -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
        IF  @ErrorCode <> 0
        BEGIN
            GOTO ERR_SHORI
        END


        IF @Kensuu <> 0
        BEGIN
            SET @po_ErrorKBN = 2
            SET @po_ErrorCD1 = '000'
            SET @po_ErrorCD2 = '00464'
            SET @po_ErrorProcName   = OBJECT_NAME(@@PROCID)
            SET @po_ErrorSummary    = ''
            SET @po_ErrorDetail     = ''
            SET @po_ErrorBtnKBN     = 0
            SET @po_ErrorBtnDefault = 0
            GOTO ERR_SHORI        
        END


        -- 補助科目名未入力チェック
        SELECT 
            @Kensuu = COUNT(*)
        FROM ( 
            SELECT  
                  KariKanjouCD         AS KanjouCD 
                , KariHojoCD           AS HojoCD 
                , KariHojoKaiteiDate   AS HojoKaiteiDate
            FROM #M0000WK_MA_HonsitenBunkaiKekka 
            WHERE ISNULL(KariHojoKBN,0) <> 0
            AND   OperationComputerName =   @p_UpdateComputerName  
            AND   OperationUserID       =   @p_UpdateUserID  
            AND   OperationJobID        =   @p_UpdateJobID  
            UNION  
            SELECT  
                  KasiKanjouCD         AS KanjouCD 
                , KasiHojoCD           AS HojoCD 
                , KasiHojoKaiteiDate   AS HojoKaiteiDate 
            FROM #M0000WK_MA_HonsitenBunkaiKekka 
            WHERE ISNULL(KasiHojoKBN,0) <> 0
            AND   OperationComputerName =   @p_UpdateComputerName  
            AND   OperationUserID       =   @p_UpdateUserID  
            AND   OperationJobID        =   @p_UpdateJobID  
        ) SKEK    
        INNER JOIN M0000MS_MA_Hojo MHO 
        ON  MHO.KanjouCD     = SKEK.KanjouCD 
        AND MHO.HojoCD       = SKEK.HojoCD 
        AND MHO.KaiteiDate   = SKEK.HojoKaiteiDate 
        WHERE MHO.HojoName IS NULL

        -- @@ERROR を取得し、変数に保存する。
        SET @ErrorCode = @@ERROR

        -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
        IF  @ErrorCode <> 0
        BEGIN
            GOTO ERR_SHORI
        END

        IF @Kensuu <> 0
        BEGIN
            SET @po_ErrorKBN = 2
            SET @po_ErrorCD1 = '000'
            SET @po_ErrorCD2 = '00465'
            SET @po_ErrorProcName   = OBJECT_NAME(@@PROCID)
            SET @po_ErrorSummary    = ''
            SET @po_ErrorDetail     = ''
            SET @po_ErrorBtnKBN     = 0
            SET @po_ErrorBtnDefault = 0
            GOTO ERR_SHORI        
        END



        -- 補助区分チェック
        SELECT 
            @Kensuu = COUNT(*)
        FROM ( 
            SELECT  
                  KariKanjouCD         AS KanjouCD 
                , KariKanjouKaiteiDate AS KanjouKaiteiDate
                , KariHojoCD           AS HojoCD 
            FROM #M0000WK_MA_HonsitenBunkaiKekka 
            WHERE KariKanjouCD IS NOT NULL 
            AND   ISNULL(KariHojoKBN,0) =   0
            AND   OperationComputerName =   @p_UpdateComputerName  
            AND   OperationUserID       =   @p_UpdateUserID  
            AND   OperationJobID        =   @p_UpdateJobID  
            UNION  
            SELECT  
                  KasiKanjouCD         AS KanjouCD 
                , KasiKanjouKaiteiDate AS KanjouKaiteiDate 
                , KasiHojoCD           AS HojoCD 
            FROM #M0000WK_MA_HonsitenBunkaiKekka 
            WHERE KasiKanjouCD IS NOT NULL 
            AND   ISNULL(KasiHojoKBN,0) =   0
            AND   OperationComputerName =   @p_UpdateComputerName  
            AND   OperationUserID       =   @p_UpdateUserID  
            AND   OperationJobID        =   @p_UpdateJobID  
        ) SKEK
        WHERE ISNULL(SKEK.HojoCD,'*') <> '*'
        
        -- @@ERROR を取得し、変数に保存する。
        SET @ErrorCode = @@ERROR

        -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
        IF  @ErrorCode <> 0
        BEGIN
            GOTO ERR_SHORI
        END

        IF @Kensuu <> 0
        BEGIN
            SET @po_ErrorKBN = 2
            SET @po_ErrorCD1 = '000'
            SET @po_ErrorCD2 = '00490'
            SET @po_ErrorProcName   = OBJECT_NAME(@@PROCID)
            SET @po_ErrorSummary    = ''
            SET @po_ErrorDetail     = ''
            SET @po_ErrorBtnKBN     = 0
            SET @po_ErrorBtnDefault = 0
            GOTO ERR_SHORI        
        END


        -------------------------------------------------------------------------------------
        -- 補助内訳科目チェック                                                                 -
        -------------------------------------------------------------------------------------
        
        -- 補助内訳科目インバリッドチェック
        SELECT 
            SKEK.UtiwakeCD 
        FROM ( 
            SELECT  
                  KariKanjouCD          AS KanjouCD 
                , KariHojoCD            AS HojoCD 
                , KariUtiwakeCD         AS UtiwakeCD 
                , KariUtiwakeKaiteiDate AS UtiwakeKaiteiDate
            FROM #M0000WK_MA_HonsitenBunkaiKekka 
            WHERE ISNULL(KariUtiwakeKBN,0) <> 0
            AND   OperationComputerName =   @p_UpdateComputerName  
            AND   OperationUserID       =   @p_UpdateUserID  
            AND   OperationJobID        =   @p_UpdateJobID  
            UNION  
            SELECT  
                  KasiKanjouCD          AS KanjouCD 
                , KasiHojoCD            AS HojoCD 
                , KasiUtiwakeCD         AS UtiwakeCD 
                , KasiUtiwakeKaiteiDate AS UtiwakeKaiteiDate
            FROM #M0000WK_MA_HonsitenBunkaiKekka 
            WHERE ISNULL(KasiUtiwakeKBN,0) <> 0
            AND   OperationComputerName =   @p_UpdateComputerName  
            AND   OperationUserID       =   @p_UpdateUserID  
            AND   OperationJobID        =   @p_UpdateJobID  
        ) SKEK 
        LEFT JOIN M0000MS_MA_Utiwake MUT
        ON  MUT.KanjouCD     = SKEK.KanjouCD 
        AND MUT.HojoCD       = SKEK.HojoCD 
        AND MUT.UtiwakeCD    = SKEK.UtiwakeCD 
        AND MUT.KaiteiDate   = SKEK.UtiwakeKaiteiDate 
        WHERE MUT.UtiwakeCD IS NULL

        -- @@ERROR を取得し、変数に保存する。
        SELECT @ErrorCode = @@ERROR
               , @Kensuu = @@ROWCOUNT
               
        -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
        IF  @ErrorCode <> 0
        BEGIN
            GOTO ERR_SHORI
        END


        IF @Kensuu <> 0
        BEGIN
            SET @po_ErrorKBN = 2
            SET @po_ErrorCD1 = '000'
            SET @po_ErrorCD2 = '00466'
            SET @po_ErrorProcName   = OBJECT_NAME(@@PROCID)
            SET @po_ErrorSummary    = ''
            SET @po_ErrorDetail     = ''
            SET @po_ErrorBtnKBN     = 0
            SET @po_ErrorBtnDefault = 0
            GOTO ERR_SHORI        
        END


        -- 補助科目名未入力チェック
        SELECT 
            @Kensuu = COUNT(*)
        FROM ( 
            SELECT  
                  KariKanjouCD          AS KanjouCD 
                , KariHojoCD            AS HojoCD 
                , KariUtiwakeCD         AS UtiwakeCD 
                , KariUtiwakeKaiteiDate AS UtiwakeKaiteiDate
            FROM #M0000WK_MA_HonsitenBunkaiKekka 
            WHERE ISNULL(KariUtiwakeKBN,0) <> 0
            AND   OperationComputerName =   @p_UpdateComputerName  
            AND   OperationUserID       =   @p_UpdateUserID  
            AND   OperationJobID        =   @p_UpdateJobID  
            UNION  
            SELECT  
                  KasiKanjouCD          AS KanjouCD 
                , KasiHojoCD            AS HojoCD 
                , KasiUtiwakeCD         AS UtiwakeCD 
                , KasiUtiwakeKaiteiDate AS UtiwakeKaiteiDate
            FROM #M0000WK_MA_HonsitenBunkaiKekka 
            WHERE ISNULL(KasiUtiwakeKBN,0) <> 0
            AND   OperationComputerName =   @p_UpdateComputerName  
            AND   OperationUserID       =   @p_UpdateUserID  
            AND   OperationJobID        =   @p_UpdateJobID  
        ) SKEK
        INNER JOIN M0000MS_MA_Utiwake MUT
        ON  MUT.KanjouCD     = SKEK.KanjouCD 
        AND MUT.HojoCD       = SKEK.HojoCD 
        AND MUT.UtiwakeCD    = SKEK.UtiwakeCD 
        AND MUT.KaiteiDate   = SKEK.UtiwakeKaiteiDate 
        WHERE MUT.UtiwakeName IS NULL

        -- @@ERROR を取得し、変数に保存する。
        SET @ErrorCode = @@ERROR

        -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
        IF  @ErrorCode <> 0
        BEGIN
            GOTO ERR_SHORI
        END

        IF @Kensuu <> 0
        BEGIN
            SET @po_ErrorKBN = 2
            SET @po_ErrorCD1 = '000'
            SET @po_ErrorCD2 = '00467'
            SET @po_ErrorProcName   = OBJECT_NAME(@@PROCID)
            SET @po_ErrorSummary    = ''
            SET @po_ErrorDetail     = ''
            SET @po_ErrorBtnKBN     = 0
            SET @po_ErrorBtnDefault = 0
            GOTO ERR_SHORI        
        END



        -- 内訳区分チェック
        SELECT 
            @Kensuu = COUNT(*)
        FROM ( 
            SELECT  
                  KariKanjouCD          AS KanjouCD 
                , KariHojoCD            AS HojoCD 
                , KariHojoKaiteiDate    AS HojoKaiteiDate 
                , KariUtiwakeCD         AS UtiwakeCD 
            FROM #M0000WK_MA_HonsitenBunkaiKekka 
            WHERE KariKanjouCD IS NOT NULL 
            AND  (ISNULL(KariHojoKBN,0) = 0 OR ISNULL(KariUtiwakeKBN,0) = 0)
            AND   OperationComputerName =   @p_UpdateComputerName  
            AND   OperationUserID       =   @p_UpdateUserID  
            AND   OperationJobID        =   @p_UpdateJobID  
            UNION
            SELECT  
                  KasiKanjouCD          AS KanjouCD 
                , KasiHojoCD            AS HojoCD 
                , KasiHojoKaiteiDate    AS HojoKaiteiDate 
                , KasiUtiwakeCD         AS UtiwakeCD 
            FROM #M0000WK_MA_HonsitenBunkaiKekka 
            WHERE KasiKanjouCD IS NOT NULL 
            AND  (ISNULL(KasiHojoKBN,0) = 0 OR ISNULL(KasiUtiwakeKBN,0) = 0)
            AND   OperationComputerName =   @p_UpdateComputerName  
            AND   OperationUserID       =   @p_UpdateUserID  
            AND   OperationJobID        =   @p_UpdateJobID  
        ) SKEK
        WHERE ISNULL(SKEK.UtiwakeCD,'*') <> '*'
        
        -- @@ERROR を取得し、変数に保存する。
        SET @ErrorCode = @@ERROR

        -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
        IF  @ErrorCode <> 0
        BEGIN
            GOTO ERR_SHORI
        END

        IF @Kensuu <> 0
        BEGIN
            SET @po_ErrorKBN = 2
            SET @po_ErrorCD1 = '000'
            SET @po_ErrorCD2 = '00491'
            SET @po_ErrorProcName   = OBJECT_NAME(@@PROCID)
            SET @po_ErrorSummary    = ''
            SET @po_ErrorDetail     = ''
            SET @po_ErrorBtnKBN     = 0
            SET @po_ErrorBtnDefault = 0
            GOTO ERR_SHORI        
        END

        -------------------------------------------------------------------------------------
        -- 部門チェック                                                                 -
        -------------------------------------------------------------------------------------

        -- 部門インバリッドチェック

        SET @SQL = ''
        SET @SQL = @SQL + ' SELECT '
        SET @SQL = @SQL + '       MBU.BumonCD '
        SET @SQL = @SQL + ' FROM ('
        SET @SQL = @SQL + '     SELECT '
        SET @SQL = @SQL + '           KariBumonCD         AS BumonCD '
        SET @SQL = @SQL + '         , KariBumonKaiteiDate AS BumonKaiteiDate '
        SET @SQL = @SQL + '     FROM #M0000WK_MA_HonsitenBunkaiKekka '
        SET @SQL = @SQL + '     WHERE ISNULL(KariBumonKBN,0) <> 0 '
        SET @SQL = @SQL + '     AND   OperationComputerName = ' + dbo.M0000FC_Escape_Varchar(@p_UpdateComputerName)
        SET @SQL = @SQL + '     AND   OperationUserID       = ' + dbo.M0000FC_Escape_Varchar(@p_UpdateUserID)
        SET @SQL = @SQL + '     AND   OperationJobID        = ' + dbo.M0000FC_Escape_Varchar(@p_UpdateJobID)
        
        SET @SQL = @SQL + '     UNION '
        SET @SQL = @SQL + '     SELECT '
        SET @SQL = @SQL + '           KasiBumonCD         AS BumonCD '
        SET @SQL = @SQL + '         , KasiBumonKaiteiDate AS BumonKaiteiDate '
        SET @SQL = @SQL + '     FROM #M0000WK_MA_HonsitenBunkaiKekka '
        SET @SQL = @SQL + '     WHERE ISNULL(KasiBumonKBN,0) <> 0 '
        SET @SQL = @SQL + '     AND   OperationComputerName = ' + dbo.M0000FC_Escape_Varchar(@p_UpdateComputerName)
        SET @SQL = @SQL + '     AND   OperationUserID       = ' + dbo.M0000FC_Escape_Varchar(@p_UpdateUserID)
        SET @SQL = @SQL + '     AND   OperationJobID        = ' + dbo.M0000FC_Escape_Varchar(@p_UpdateJobID)
        SET @SQL = @SQL + ' ) SKEK '
        SET @SQL = @SQL + ' LEFT JOIN M0000MS_MA_Bumon MBU '
        SET @SQL = @SQL + ' ON  MBU.BumonCD    = SKEK.BumonCD '
        SET @SQL = @SQL + ' AND MBU.KaiteiDate = SKEK.BumonKaiteiDate '
        

--2008/02/06 変更 N.Sasaki 開始 振替伝票仕訳チェック処理に振替伝票仕訳チェック区分を追加し、チェックを行うか判断する。
--    -- 引数.部門セキュリティパターンコード<>""の場合
--    IF ISNULL(@p_BumonSecurityPatternCD,'') <> ''
        -- 引数.部門セキュリティパターンコード<>""かつ 振替伝票仕訳チェック区分 = 0の場合
        IF ISNULL(@p_BumonSecurityPatternCD,'') <> '' AND @p_HurikaeDenpyouCheckKBN = 0
--2008/02/06 変更 N.Sasaki 終了
        BEGIN
            SET @SQL = @SQL + ' LEFT JOIN M0000MS_MA_BumonSecurityYouso YOS '
            SET @SQL = @SQL + ' ON  YOS.SecurityPatternCD = ' + dbo.M0000FC_Escape_Varchar(@p_BumonSecurityPatternCD)
            SET @SQL = @SQL + ' AND YOS.BumonCDYouso      = SKEK.BumonCD '
            SET @SQL = @SQL + ' WHERE MBU.BumonCD IS NULL OR YOS.BumonCDYouso IS NULL '        
        END
        ELSE
        BEGIN
            SET @SQL = @SQL + ' WHERE MBU.BumonCD IS NULL '
        END

        EXECUTE (@SQL)

        -- @@ERROR を取得し、変数に保存する。
        SELECT @ErrorCode = @@ERROR
               , @Kensuu = @@ROWCOUNT
               
        -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
        IF  @ErrorCode <> 0
        BEGIN
            GOTO ERR_SHORI
        END


        IF @Kensuu <> 0
        BEGIN
            SET @po_ErrorKBN = 2
            SET @po_ErrorCD1 = '000'
            SET @po_ErrorCD2 = '00468'
            SET @po_ErrorProcName   = OBJECT_NAME(@@PROCID)
            SET @po_ErrorSummary    = ''
            SET @po_ErrorDetail     = ''
            SET @po_ErrorBtnKBN     = 0
            SET @po_ErrorBtnDefault = 0
            GOTO ERR_SHORI        
        END

        -- 部門名未入力チェック
        SELECT 
            @Kensuu = COUNT(*)
        FROM ( 
            SELECT  
                  KariBumonCD         AS BumonCD 
                , KariBumonKaiteiDate AS BumonKaiteiDate
            FROM #M0000WK_MA_HonsitenBunkaiKekka 
            WHERE ISNULL(KariBumonKBN,0) <> 0
            AND   OperationComputerName =   @p_UpdateComputerName  
            AND   OperationUserID       =   @p_UpdateUserID  
            AND   OperationJobID        =   @p_UpdateJobID  
            UNION  
            SELECT  
                  KasiBumonCD         AS BumonCD 
                , KasiBumonKaiteiDate AS BumonKaiteiDate 
            FROM #M0000WK_MA_HonsitenBunkaiKekka 
            WHERE ISNULL(KasiBumonKBN,0) <> 0
            AND   OperationComputerName =   @p_UpdateComputerName  
            AND   OperationUserID       =   @p_UpdateUserID  
            AND   OperationJobID        =   @p_UpdateJobID  
        ) SKEK    
        INNER JOIN M0000MS_MA_Bumon MBU 
        ON  MBU.BumonCD   = SKEK.BumonCD 
        AND MBU.KaiteiDate = SKEK.BumonKaiteiDate
        WHERE MBU.BumonName IS NULL

        -- @@ERROR を取得し、変数に保存する。
        SET @ErrorCode = @@ERROR

        -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
        IF  @ErrorCode <> 0
        BEGIN
            GOTO ERR_SHORI
        END

        IF @Kensuu <> 0
        BEGIN
            SET @po_ErrorKBN = 2
            SET @po_ErrorCD1 = '000'
            SET @po_ErrorCD2 = '00469'
            SET @po_ErrorProcName   = OBJECT_NAME(@@PROCID)
            SET @po_ErrorSummary    = ''
            SET @po_ErrorDetail     = ''
            SET @po_ErrorBtnKBN     = 0
            SET @po_ErrorBtnDefault = 0
            GOTO ERR_SHORI        
        END

        --事業所部門セキュリティ区分取得
        SELECT @ZigyoushoBumonCheckKBN = ZigyoushoBumonCheckKBN
        FROM M0000MS_MA_KaikeiControl
        WHERE ControlKey = 1
        
        -- @@ERROR を取得し、変数に保存する。
        SET @ErrorCode = @@ERROR

        -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
        IF  @ErrorCode <> 0
        BEGIN
            GOTO ERR_SHORI
        END

        IF @ZigyoushoBumonCheckKBN = 2
        BEGIN
            -- 事業所部門ﾊﾟﾀｰﾝチェック
            SELECT 
                @Kensuu = COUNT(*)
            FROM ( 
                SELECT  
                      KariBumonCD         AS BumonCD 
                    , HasseiDate
                    , ZigyoushoCD
                FROM #M0000WK_MA_HonsitenBunkaiKekka 
                WHERE ISNULL(KariBumonKBN,0) <> 0
                AND   OperationComputerName =   @p_UpdateComputerName  
                AND   OperationUserID       =   @p_UpdateUserID  
                AND   OperationJobID        =   @p_UpdateJobID  
                UNION  
                SELECT  
                      KasiBumonCD         AS BumonCD 
                    , HasseiDate
                    , ZigyoushoCD
                FROM #M0000WK_MA_HonsitenBunkaiKekka 
                WHERE ISNULL(KasiBumonKBN,0) <> 0
                AND   OperationComputerName =   @p_UpdateComputerName  
                AND   OperationUserID       =   @p_UpdateUserID  
                AND   OperationJobID        =   @p_UpdateJobID  
            ) SKEK    
            LEFT JOIN (
                SELECT
                      HBK.ZigyoushoCD
                    , HBK.HasseiDate
                    , MAX(MBP.KaiteiDate) AS KaiteiDate
                FROM       #M0000WK_MA_HonsitenBunkaiKekka  HBK
                INNER JOIN M0000MS_MA_ZigyoushoBumonPattern MBP 
                ON    MBP.ZigyoushoCD           =   HBK.ZigyoushoCD
                AND   MBP.KaiteiDate           <=   HBK.HasseiDate
                WHERE HBK.OperationComputerName =   @p_UpdateComputerName  
                AND   HBK.OperationUserID       =   @p_UpdateUserID  
                AND   HBK.OperationJobID        =   @p_UpdateJobID  
                GROUP BY HBK.ZigyoushoCD, HBK.HasseiDate
            ) WBP
            ON  WBP.ZigyoushoCD = SKEK.ZigyoushoCD
            AND WBP.HasseiDate  = SKEK.HasseiDate
            LEFT JOIN M0000MS_MA_ZigyoushoBumonPattern MBP 
            ON  MBP.ZigyoushoCD = SKEK.ZigyoushoCD
            AND MBP.BumonCD     = SKEK.BumonCD
            AND MBP.KaiteiDate  = WBP.KaiteiDate
            WHERE MBP.ZigyoushoCD IS NULL

            -- @@ERROR を取得し、変数に保存する。
            SET @ErrorCode = @@ERROR

            -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
            IF  @ErrorCode <> 0
            BEGIN
                GOTO ERR_SHORI
            END

            IF @Kensuu <> 0
            BEGIN
                SET @po_ErrorKBN = 2
                SET @po_ErrorCD1 = '000'
                SET @po_ErrorCD2 = '00470'
                SET @po_ErrorProcName   = OBJECT_NAME(@@PROCID)
                SET @po_ErrorSummary    = ''
                SET @po_ErrorDetail     = ''
                SET @po_ErrorBtnKBN     = 0
                SET @po_ErrorBtnDefault = 0
                GOTO ERR_SHORI        
            END
        END

        -- 部門区分チェック
        SELECT 
            @Kensuu = COUNT(*)
        FROM ( 
            SELECT  
                  KariKanjouCD         AS KanjouCD 
                , KariKanjouKaiteiDate AS KanjouKaiteiDate
                , KariBumonCD          AS BumonCD 
            FROM #M0000WK_MA_HonsitenBunkaiKekka 
            WHERE KariKanjouCD IS NOT NULL 
            AND   ISNULL(KariBumonKBN,0) = 0
            AND   OperationComputerName =   @p_UpdateComputerName  
            AND   OperationUserID       =   @p_UpdateUserID  
            AND   OperationJobID        =   @p_UpdateJobID  
            UNION  
            SELECT  
                  KasiKanjouCD         AS KanjouCD 
                , KasiKanjouKaiteiDate AS KanjouKaiteiDate 
                , KasiBumonCD          AS BumonCD 
            FROM #M0000WK_MA_HonsitenBunkaiKekka 
            WHERE KasiKanjouCD IS NOT NULL 
            AND   ISNULL(KasiBumonKBN,0) = 0
            AND   OperationComputerName =   @p_UpdateComputerName  
            AND   OperationUserID       =   @p_UpdateUserID  
            AND   OperationJobID        =   @p_UpdateJobID  
        ) SKEK
        WHERE ISNULL(SKEK.BumonCD,'*') <> '*'
        
        -- @@ERROR を取得し、変数に保存する。
        SET @ErrorCode = @@ERROR

        -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
        IF  @ErrorCode <> 0
        BEGIN
            GOTO ERR_SHORI
        END

        IF @Kensuu <> 0
        BEGIN
            SET @po_ErrorKBN = 2
            SET @po_ErrorCD1 = '000'
            SET @po_ErrorCD2 = '00492'
            SET @po_ErrorProcName   = OBJECT_NAME(@@PROCID)
            SET @po_ErrorSummary    = ''
            SET @po_ErrorDetail     = ''
            SET @po_ErrorBtnKBN     = 0
            SET @po_ErrorBtnDefault = 0
            GOTO ERR_SHORI        
        END
--2008/02/06 追加 N.Sasaki 開始 振替伝票仕訳チェック処理に振替伝票仕訳チェック区分を追加し、チェックを行うか判断する。
    END
--2008/02/06 追加 N.Sasaki 終了

    -- 変数.エラーコードをリターンする。    
    RETURN @ErrorCode

    /* ラベル：ERR_SHORI*/
ERR_SHORI:

    --カーソルの後始末
    IF  CURSOR_STATUS('LOCAL','CURSIWAKE') >= 0   --カーソルがオープン中ならCLOSE
    BEGIN 
            CLOSE      CURSIWAKE
    END
    IF  CURSOR_STATUS('LOCAL','CURSIWAKE') = -1   --カーソルがクローズ済ならDEALLOCATE
    BEGIN  
            DEALLOCATE CURSIWAKE
    END
    
    IF @po_ErrorKBN = 0
    BEGIN
        -- 引数に戻り値をセット
        SET @po_ErrorKBN        = 1                      --1：システムエラー
        SET @po_ErrorCD1        = '000'                  --会計は"000"で統一
        SET @po_ErrorCD2        = '00119'                --基幹会計は"00119"で統一
        SET @po_ErrorProcName   = OBJECT_NAME(@@PROCID)
        SET @po_ErrorSummary    = ''
        SET @po_ErrorDetail     = ''
        SET @po_ErrorBtnKBN     = 0
        SET @po_ErrorBtnDefault = 0
    END
END



GO

