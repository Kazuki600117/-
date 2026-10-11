IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M0000PR_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Select_Siwake]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[M0000PR_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Select_Siwake]

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE  [M0000PR_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Select_Siwake](

      @po_ErrorKBN                      int              OUTPUT     /* エラー区分 */
    , @po_ErrorCD1                      varchar(128)     OUTPUT     /* エラーCD1 */
    , @po_ErrorCD2                      varchar(128)     OUTPUT     /* エラーCD2 */
    , @po_ErrorProcName                 varchar(128)     OUTPUT     /* エラーストアド名 */
    , @po_ErrorSummary                  varchar(256)     OUTPUT     /* エラーメッセージ概要 */
    , @po_ErrorDetail                   varchar(2560)    OUTPUT     /* エラーメッセージ詳細 */
    , @po_ErrorBtnKBN                   tinyint          OUTPUT     /* ボタン区分(0：OK、1：YES・NO、2：YES・NO・CANCEL) */
    , @po_ErrorBtnDefault               tinyint          OUTPUT     /* デフォルトボタン区分(0：ボタン１、1：ボタン２、2：ボタン３) */
    , @p_ShoriMode                      tinyint                     /* 処理モード */
    , @p_TensouMode                     tinyint                     /* 転送モード */
    , @p_SystemDenpyouNO                decimal(18,0)               /* システム伝票番号 */
    , @p_DenpyouNO                      varchar(12)                 /* 伝票番号 */
    , @p_CompanyNO                      int                         /* 会社NO */
    , @p_UpdateDatetime                 datetime                    /* 更新日時 */
    , @p_UpdateCertificatedGuid         uniqueidentifier            /* 更新認証済Guid */
    , @p_UpdateJobID                    varchar(128)                /* 更新JobID */
    , @p_UpdateUserID                   varchar(128)                /* 更新ユーザID */
    , @p_UpdateComputerName             varchar(128)                /* 更新PC名 */
    , @p_UpdateComputerUserName         varchar(128)                /* 更新PCユーザ名 */
    , @p_UpdateComputerDomainName       varchar(256)                /* 更新PCドメイン名 */
    --↓2013/09/04 追加 M.Shinbo GX対応(M00020:メモ機能の実装) 開始
    , @p_MemoSiyouFlag                  tinyint = 1                 /* メモ使用フラグ(0:使用しない、1:使用する) */
    --↑2013/09/04 追加 M.Shinbo GX対応(M00020:メモ機能の実装) 終了
    -- 2023/12/20 K.Imoto 修正 (228137-230110-230115)【電子帳簿改善2023】最終承認後添付 開始
    , @p_IsTuikaTenpu                   tinyint                     /* 追加添付モード(1：追加添付モード、0：それ以外) */
    -- 2023/12/20 K.Imoto 修正 (228137-230110-230115)【電子帳簿改善2023】最終承認後添付 終了
)
AS

/*
***************************************************************************************************
**      名称：画面表示データ取得
**      機能：引数をもとにして、本支店仕訳入力画面に表示する本支店仕訳明細データを取得する
**      パラメータ
**      @po_ErrorKBN                      int              OUTPUT          -- エラー区分 
**    , @po_ErrorCD1                      varchar(128)     OUTPUT          -- エラーCD1 
**    , @po_ErrorCD2                      varchar(128)     OUTPUT          -- エラーCD2
**    , @po_ErrorProcName                 varchar(128)     OUTPUT          -- エラーストアド名
**    , @po_ErrorSummary                  varchar(256)     OUTPUT          -- エラーメッセージ概要
**    , @po_ErrorDetail                   varchar(2560)    OUTPUT          -- エラーメッセージ詳細
**    , @po_ErrorBtnKBN                   tinyint          OUTPUT          -- ボタン区分(0：OK、1：YES・NO、2：YES・NO・CANCEL)
**    , @po_ErrorBtnDefault               tinyint          OUTPUT          -- デフォルトボタン区分(0：ボタン１、1：ボタン２、2：ボタン３)
**    , @p_ShoriMode                      tinyint                          -- 処理モード
**    , @p_TensouMode                     tinyint                          -- 転送モード
**    , @p_SystemDenpyouNO                decimal(18,0)                    -- システム伝票番号
**    , @p_DenpyouNO                      varchar(12)                      -- 伝票番号
**    , @p_CompanyNO                      int                              -- 会社NO
**    , @p_UpdateDatetime                 datetime                         -- 更新日時
**    , @p_UpdateCertificatedGuid         uniqueidentifier                 -- 更新認証済Guid
**    , @p_UpdateJobID                    varchar(128)                     -- 更新JobID
**    , @p_UpdateUserID                   varchar(128)                     -- 更新ユーザID
**    , @p_UpdateComputerName             varchar(128)                     -- 更新PC名
**    , @p_UpdateComputerUserName         varchar(128)                     -- 更新PCユーザ名
**    , @p_UpdateComputerDomainName       varchar(256)                     -- 更新PCドメイン名
**    --↓2013/09/04 追加 M.Shinbo GX対応(M00020:メモ機能の実装) 開始
**    , @p_MemoSiyouFlag                  tinyint                          -- メモ使用フラグ(0:使用しない、1:使用する)
**    --↑2013/09/04 追加 M.Shinbo GX対応(M00020:メモ機能の実装) 終了
    -- 2023/12/20 K.Imoto 修正 (228137-230110-230115)【電子帳簿改善2023】最終承認後添付 開始
**    , @p_IsTuikaTenpu                   tinyint                          -- 追加添付モード(1：追加添付モード、0：それ以外)
    -- 2023/12/20 K.Imoto 修正 (228137-230110-230115)【電子帳簿改善2023】最終承認後添付 終了
***************************************************************************************************
**      履歴
**              2007/12/04 N.Sasaki   新規作成
**              2008/06/02 N.Sasaki   テンポラリテーブルの変更
**                                    画面情報抽出元変更
**              2010/08/17 C.Okuno    (M04451)ダブルスタンダード対応
**              2010/12/13 Y.Tamashima(M04451)ダブルスタンダード対応 使用科目制御対応
**              2012/09/14 M.Shinbo   (M04835)過去日付伝票を修正する際に不整合が起こる件
**              2012/10/05 K.Morita   (M04851)平成26年消費税改正対応
**              2013/03/12 M.Shinbo   (M04925-00)連携仕訳明細の更新条件を追加
**              2013/04/22 周俊        変更 桁数拡張
**              2013/09/04 M.Shinbo    変更 GX対応(M00020:メモ機能の実装)
**				2014/11/12 K.Shigematsu(12917)伝票表示時に行順が意図しない順番になる現象を修正
**              2015/08/20 K.Yamashita (28126-28218-28231-28233)会計WG要望49期上期対応
**              2023/12/20 K.Imoto     修正 (228137-230110-230115)【電子帳簿改善2023】最終承認後添付
**
***************************************************************************************************
*/
BEGIN

    SET NOCOUNT ON
    /* 初期処理 */
    -- 変数定義
    DECLARE @ErrorCode                   int            /* @@ERRORの待避領域 */
    DECLARE @SUB_ErrorCode               int            /* サブストアドからの戻り値:エラーコード */
    DECLARE @SUB_ErrorKBN                tinyint        /* サブストアドからの戻り値:エラー区分 */    
    DECLARE @SUB_ErrorCD1                varchar(500)   /* サブストアドからの戻り値:エラーコード1 */
    DECLARE @SUB_ErrorCD2                varchar(6)     /* サブストアドからの戻り値:エラーコード2 */ 
    DECLARE @SUB_ErrorProcName           varchar(128)   /* サブストアドのエラーストアド名 */
    DECLARE @SUB_ErrorSummary            varchar(256)   /* サブストアドのエラーメッセージ概要 */
    DECLARE @SUB_ErrorDetail             varchar(2560)  /* サブストアドのエラーメッセージ詳細 */
    DECLARE @SUB_ErrorBtnKBN             tinyint        /* サブストアドのボタン区分 */
    DECLARE @SUB_ErrorBtnDefault         tinyint        /* サブストアドのデフォルトボタン区分 */
       
    DECLARE @Kensuu                      int            /* 件数退避領域 */

    DECLARE @HasseiDate                  datetime       /* 発生日 */
    DECLARE @ZigyoushoCD                 varchar(6)     /* 事業所コード */

--2012/09/14 削除 M.Shinbo 開始 (M04835)    
----2010/12/13 Y.Tamashima(M04451)ダブルスタンダード対応 使用科目制御対応 Start
--    DECLARE @SQL                         varchar (4000)  /* SQL文 */
--    DECLARE @KariBaraiZeiCD              varchar(5)      /* 総勘定科目コントロールマスタ.仮払消費税科目コードの待避領域 */
--    DECLARE @KariUkeZeiCD                varchar(5)      /* 総勘定科目コントロールマスタ.仮受消費税科目コードの待避領域 */ 
--    DECLARE @DenpyouKBN                  varchar(3)
----2010/12/13 Y.Tamashima(M04451)ダブルスタンダード対応 使用科目制御対応 End
--2012/09/14 削除 M.Shinbo 終了 (M04835)

    -- 引数初期化
    SET @po_ErrorKBN = 0
    SET @po_ErrorCD1 = ''
    SET @po_ErrorCD2 = ''
    SET @po_ErrorProcName = ''
    SET @po_ErrorSummary = ''
    SET @po_ErrorDetail = ''
    SET @po_ErrorBtnKBN = 0
    SET @po_ErrorBtnDefault = 0
    
    -- 事業所ワークをCREATE
    CREATE TABLE #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Zigyousho
    (
             ZigyoushoCD            varchar(6)      COLLATE Database_Default        NOT NULL
        ,    KaiteiDate             datetime                                        NOT NULL
        ,    ZigyoushoName          varchar(30)     COLLATE Database_Default        NULL     -- 事業所名
        ,    ZigyoushoNameRyaku     varchar(20)     COLLATE Database_Default        NULL     -- 事業所略名
        ,    SetZandakaDate         datetime                                        NOT NULL -- セット残高日付
        ,    DenpyouSimeDate        datetime                                        NULL     -- 伝票締日
    )
--2008/06/02 修正 N.Sasaki 開始
--    CREATE INDEX IDX_#M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Zigyousho ON #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Zigyousho
    CREATE CLUSTERED INDEX IDX_#M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Zigyousho ON #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Zigyousho
--2008/06/02 修正 N.Sasaki 終了
    
    (
             ZigyoushoCD
    )
    
    -- 本支店勘定科目ワークをCREATE
    CREATE TABLE #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_HonsitenKanjou
    (
             ZigyoushoCD            varchar(6)      COLLATE database_default        NULL
        ,    KaiteiDate             datetime                                        NOT NULL
        ,    HonshaZigyoushoCD      varchar(6)      COLLATE database_default        NULL
    )
--2008/06/02 修正 N.Sasaki 開始
--    CREATE INDEX IDX_#M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_HonsitenKanjou ON #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_HonsitenKanjou
    CREATE CLUSTERED INDEX IDX_#M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_HonsitenKanjou ON #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_HonsitenKanjou
--2008/06/02 修正 N.Sasaki 終了
    (
             ZigyoushoCD
    )

    -- 事業所部門ﾊﾟﾀｰﾝワークをCREATE
--2008/06/02 修正 N.Sasaki 開始
--    CREATE TABLE #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_ZigyoushoBumonPattern
--    (
--             ZigyoushoCD            varchar(6)      COLLATE database_default        NULL
--        ,    KaiteiDate             datetime                                        NOT NULL
--        ,    BumonCD                varchar(6)      COLLATE database_default        NULL
--    )
--    CREATE INDEX IDX_#M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_ZigyoushoBumonPattern ON #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_ZigyoushoBumonPattern
--    (
--             ZigyoushoCD
--        ,    BumonCD
--    )
    CREATE TABLE #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_ZigyoushoBumonPattern
    (
          ZigyoushoCD                        varchar(6)      COLLATE Database_Default NOT NULL -- 事業所コード
      --2013/04/22 周俊　修正 開始
      --, BumonCD                            varchar(6)      COLLATE Database_Default NOT NULL -- 部門コード
        , BumonCD                            varchar(10)      COLLATE Database_Default NOT NULL -- 部門コード
      --2013/04/22 周俊　修正 完了 

        , KaiteiDate                         datetime                                 NOT NULL -- 改定日
    )
    CREATE CLUSTERED INDEX IX_#M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_ZigyoushoBumonPattern ON #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_ZigyoushoBumonPattern
    (
          ZigyoushoCD
        , BumonCD
        , KaiteiDate
    )
--2008/06/02 修正 N.Sasaki 終了
    
    -- 排他対象伝票ワークをCREATE
    CREATE TABLE #M0000WK_MA_JobHaita
    (
          SystemDenpyouNO                    decimal(18,0)                             NOT NULL -- システム伝票番号
        , DenpyouNO                          varchar(12)      COLLATE Database_Default NOT NULL -- 伝票番号
        , YosanKBN                           varchar(8)       COLLATE Database_Default NULL     -- 予算区分
        , Kessanki                           smallint                                  NULL     -- 決算期
        , MonthNO                            tinyint                                   NULL     -- 月度番号
        , ZigyoushoCD                        varchar(6)       COLLATE Database_Default NULL     -- 事業所コード
      --2013/04/22 周俊　修正 開始
      --, BumonCD                            varchar(6)       COLLATE Database_Default NULL     -- 部門コード
        , BumonCD                            varchar(10)       COLLATE Database_Default NULL     -- 部門コード
      --2013/04/22 周俊　修正 完了 

        , CompanyNO                          int                                       NOT NULL -- 会社NO
    )
    CREATE INDEX IDX_#M0000WK_MA_JobHaita ON #M0000WK_MA_JobHaita
    (
         SystemDenpyouNO
    )

--2012/09/14 削除 M.Shinbo 開始 (M04835)
----2010/12/13 Y.Tamashima(M04451)ダブルスタンダード対応 使用科目制御対応 Start
--    -- 総勘定科目ワークをCREATE
--    CREATE TABLE #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Kanjou
--    (
--          KanjouCD                           varchar(5)      COLLATE Database_Default  NOT NULL -- 総勘定科目コード
--        , KaiteiDate                         datetime                                  NOT NULL -- 改定日
--        , KanjouName                         varchar(30)     COLLATE Database_Default  NULL     -- 総勘定科目名
--        , KanjouNameRyaku                    varchar(20)     COLLATE Database_Default  NULL     -- 総勘定科目略名
--        , TaishakuKBN                        tinyint                                   NOT NULL -- 貸借区分
--        , KamokuCategory                     tinyint                                   NOT NULL -- 科目分類
--        , HyouKBN                            tinyint                                   NOT NULL -- 表区分
--        , KamokuKBN                          tinyint                                   NOT NULL -- 科目区分
--        , GenyokinKBN                        tinyint                                   NOT NULL -- 現預金区分
--        , TorihikisakiKBN                    tinyint                                   NOT NULL -- 取引先区分
--        , HojoKBN                            tinyint                                   NOT NULL -- 補助区分
--        , BumonKBN                           tinyint                                   NOT NULL -- 部門区分
--        , KariZeiKBN                         varchar(2)      COLLATE Database_Default  NOT NULL -- 借方税区分
--        , KasiZeiKBN                         varchar(2)      COLLATE Database_Default  NOT NULL -- 貸方税区分
--        , KariZeikomiKBN                     tinyint                                   NOT NULL -- 借方税込区分
--        , KasiZeikomiKBN                     tinyint                                   NOT NULL -- 貸方税込区分
--        , KensakuKBN                         varchar(2)      COLLATE Database_Default  NULL     -- 検索区分
--        , KichouKBN                          tinyint                                   NOT NULL -- 記帳区分
--        , BunsekiCheckKBN1                   tinyint                                   NOT NULL -- 分析チェック区分1
--        , BunsekiCheckKBN2                   tinyint                                   NOT NULL -- 分析チェック区分2
--        , BunsekiCheckKBN3                   tinyint                                   NOT NULL -- 分析チェック区分3
--        , BunsekiCheckKBN4                   tinyint                                   NOT NULL -- 分析チェック区分4
--        , BunsekiCheckKBN5                   tinyint                                   NOT NULL -- 分析チェック区分5
--        , SikinCheckKBN                      tinyint                                   NOT NULL -- 資金チェック区分
--        , ProjectCheckKBN                    tinyint                                   NOT NULL -- プロジェクトチェック区分
--        , BunsekiCustomCheckKBN1             tinyint                                   NOT NULL -- 分析カスタマイズチェック区分1
--        , BunsekiCustomCheckKBN2             tinyint                                   NOT NULL -- 分析カスタマイズチェック区分2
--        , BunsekiCustomCheckKBN3             tinyint                                   NOT NULL -- 分析カスタマイズチェック区分3
--        , BunsekiCustomCheckKBN4             tinyint                                   NOT NULL -- 分析カスタマイズチェック区分4
--        , BunsekiCustomCheckKBN5             tinyint                                   NOT NULL -- 分析カスタマイズチェック区分5
--        , HasseiLeaseKaishaKBN               tinyint                                   NOT NULL -- リース会社区分
--        , HasseiZeimushoKBN                  tinyint                                   NOT NULL -- 税務署区分
--        , HasseiSiiresakiKBN                 tinyint                                   NOT NULL -- 仕入先区分
--        , HasseiSiharaisakiKBN               tinyint                                   NOT NULL -- 支払先区分
--        , HasseiTokuisakiKBN                 tinyint                                   NOT NULL -- 得意先区分
--        , HasseiKaishuusakiKBN               tinyint                                   NOT NULL -- 回収先区分
--        , HasseiTorihikisakiKBN1             tinyint                                   NOT NULL -- 取引先区分1
--        , HasseiTorihikisakiKBN2             tinyint                                   NOT NULL -- 取引先区分2
--        , HasseiTorihikisakiKBN3             tinyint                                   NOT NULL -- 取引先区分3
--        , HasseiTorihikisakiKBN4             tinyint                                   NOT NULL -- 取引先区分4
--    )
--    CREATE CLUSTERED INDEX IX_#M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Kanjou ON #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Kanjou
--    (
--          KanjouCD
--        , KaiteiDate
--    )
----2010/12/13 Y.Tamashima(M04451)ダブルスタンダード対応 使用科目制御対応 End
--2012/09/14 削除 M.Shinbo 終了 (M04835)
    
    
    --処理モードが修正か削除の場合
    IF @p_ShoriMode = 2 OR @p_ShoriMode = 3
    BEGIN
        INSERT INTO #M0000WK_MA_JobHaita
        (
             SystemDenpyouNO
            ,DenpyouNO
            ,CompanyNO
        ) VALUES
        (
             @p_SystemDenpyouNO
            ,@p_DenpyouNO
            ,@p_CompanyNO
        )
        
        -- @@ERROR を取得し、変数に保存する。
        SET @ErrorCode = @@ERROR

        -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
        IF  @ErrorCode <> 0
        BEGIN
            GOTO ERR_SHORI
        END
        
        -- 排他チェック実行処理
        EXECUTE @SUB_ErrorCode = dbo.M0000PR_MA_Haita
              @po_ErrorKBN                = @SUB_ErrorKBN        OUTPUT
            , @po_ErrorCD1                = @SUB_ErrorCD1        OUTPUT
            , @po_ErrorCD2                = @SUB_ErrorCD2        OUTPUT
            , @po_ErrorProcName           = @SUB_ErrorProcName   OUTPUT
            , @po_ErrorSummary            = @SUB_ErrorSummary    OUTPUT
            , @po_ErrorDetail             = @SUB_ErrorDetail     OUTPUT
            , @po_ErrorBtnKBN             = @SUB_ErrorBtnKBN     OUTPUT
            , @po_ErrorBtnDefault         = @SUB_ErrorBtnDefault OUTPUT
            , @p_ShoriMode                = 1
            , @p_JobShubetuKBN            = 0
            , @p_UpdateDatetime           = @p_UpdateDatetime
            , @p_UpdateCertificatedGuid   = @p_UpdateCertificatedGuid
            , @p_UpdateJobID              = @p_UpdateJobID
            , @p_UpdateUserID             = @p_UpdateUserID
            , @p_UpdateComputerName       = @p_UpdateComputerName
            , @p_UpdateComputerUserName   = @p_UpdateComputerUserName
            , @p_UpdateComputerDomainName = @p_UpdateComputerDomainName

        -- @@ERROR を取得し、変数に保存する。
        SET @ErrorCode = @@ERROR

        -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
        IF  @ErrorCode <> 0
        BEGIN
            GOTO ERR_SHORI
        END

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
    
        -- 2023/12/20 K.Imoto 修正 (228137-230110-230115)【電子帳簿改善2023】最終承認後添付 開始
        -- 添付モード時、受入状態区分の更新を行わないための考慮
        IF @p_IsTuikaTenpu = 0
        BEGIN
        -- 2023/12/20 K.Imoto 修正 (228137-230110-230115)【電子帳簿改善2023】最終承認後添付 終了
            -- 連携仕訳明細.受入状態区分更新
            UPDATE ME SET
                 ME.UkeireStateKBN             = 2
                ,ME.UpdateDatetime             = @p_UpdateDatetime
                ,ME.UpdateCertificatedGuid     = @p_UpdateCertificatedGuid
                ,ME.UpdateJobID                = @p_UpdateJobID
                ,ME.UpdateUserID               = @p_UpdateUserID
                ,ME.UpdateComputerName         = @p_UpdateComputerName
                ,ME.UpdateComputerUserName     = @p_UpdateComputerUserName
                ,ME.UpdateComputerDomainName   = @p_UpdateComputerDomainName
            FROM M0000RR_MA_RenkeiSiwakeMeisai ME
            INNER JOIN M0000MS_MA_Control CM
            ON  CM.ControlKey = 1
            -- 2010/08/17 修正 C.Okuno@OBS  (M04451)↓
            INNER JOIN M0000MS_MA_DenpyouKBN DE
            ON ME.DenpyouKBN = DE.DenpyouKBN
            -- 2010/08/17 修正 C.Okuno@OBS  (M04451)↑
            WHERE ME.SystemDenpyouNO =  @p_SystemDenpyouNO
            AND  (CM.ShouninKBN      = 1
            -- 2010/08/17 修正 C.Okuno@OBS  (M04451)↓
            -- OR   ME.DenpyouKBN     >= '20')
             OR   DE.HonKaridenFlag = 1)
            -- 2010/08/17 修正 C.Okuno@OBS  (M04451)↑
            --↓2013/03/12 追加 M.Shinbo (M04925-00) 開始
            --2015/08/20 K.Yamashita (28126-28218-28231-28233)会計WG要望49期上期対応 開始
            --AND   ME.SystemCategory  =  10      -- 10:財務会計
            AND   ME.SystemCategory  IN  (10, 12)      -- 10:財務会計、12:本支店会計
            --2015/08/20 K.Yamashita (28126-28218-28231-28233)会計WG要望49期上期対応 終了
            --↑2013/03/12 追加 M.Shinbo (M04925-00) 終了
            
            -- @@ERROR を取得し、変数に保存する。
            SET @ErrorCode = @@ERROR

            -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
            IF  @ErrorCode <> 0
            BEGIN
                GOTO ERR_SHORI
            END
        -- 2023/12/20 K.Imoto 修正 (228137-230110-230115)【電子帳簿改善2023】最終承認後添付 開始
        END
        -- 2023/12/20 K.Imoto 修正 (228137-230110-230115)【電子帳簿改善2023】最終承認後添付 終了
    END
    

    -- 変数.発生日、変数.事業所コードの待避
    SELECT
          @HasseiDate  = HasseiDate
        , @ZigyoushoCD = ZigyoushoCD
--2012/09/14 削除 M.Shinbo 開始 (M04835)
----2010/12/13 Y.Tamashima(M04451)ダブルスタンダード対応 使用科目制御対応 Start
--        , @DenpyouKBN = DenpyouKBN
----2010/12/13 Y.Tamashima(M04451)ダブルスタンダード対応 使用科目制御対応 End
--2012/09/14 削除 M.Shinbo 終了 (M04835)
    FROM M0000RR_MA_HonsitenSiwakeMeisai
    WHERE SystemDenpyouNO = @p_SystemDenpyouNO
    
    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR

    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END

--2012/09/14 削除 M.Shinbo 開始 (M04835)
--                                       画面表示時には制御不要(あくまでデータが正)なためカット
----2010/12/13 Y.Tamashima(M04451)ダブルスタンダード対応 使用科目制御対応 Start
--    -- 総勘定科目コントロールマスタを参照し、仮受/仮払消費税科目コードを取得する。
--    SELECT
--          @KariBaraiZeiCD = CON.KaribaraiZeiCD
--        , @KariUkeZeiCD   = CON.KariukeZeiCD
--    FROM M0000MS_MA_KanjouControl CON
--    INNER JOIN (
--        SELECT 
--            MAX(KaiteiDate) AS KaiteiDate
--        FROM M0000MS_MA_KanjouControl
--        WHERE KaiteiDate <= @HasseiDate
--    ) WK
--    ON WK.KaiteiDate = CON.KaiteiDate

--    -- 総勘定科目マスタを待避
    
--    SET @SQL = ''

--    SET @SQL = @SQL + ' INSERT INTO #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Kanjou '
--    SET @SQL = @SQL + ' ( '
--    SET @SQL = @SQL + '       KanjouCD '
--    SET @SQL = @SQL + '     , KaiteiDate '
--    SET @SQL = @SQL + '     , KanjouName '
--    SET @SQL = @SQL + '     , KanjouNameRyaku '
--    SET @SQL = @SQL + '     , TaishakuKBN '
--    SET @SQL = @SQL + '     , KamokuCategory '
--    SET @SQL = @SQL + '     , HyouKBN '
--    SET @SQL = @SQL + '     , KamokuKBN '
--    SET @SQL = @SQL + '     , GenyokinKBN '
--    SET @SQL = @SQL + '     , TorihikisakiKBN '
--    SET @SQL = @SQL + '     , HojoKBN '
--    SET @SQL = @SQL + '     , BumonKBN '
--    SET @SQL = @SQL + '     , KariZeiKBN '
--    SET @SQL = @SQL + '     , KasiZeiKBN '
--    SET @SQL = @SQL + '     , KariZeikomiKBN '
--    SET @SQL = @SQL + '     , KasiZeikomiKBN '
--    SET @SQL = @SQL + '     , KensakuKBN '
--    SET @SQL = @SQL + '     , KichouKBN '
--    SET @SQL = @SQL + '     , BunsekiCheckKBN1 '
--    SET @SQL = @SQL + '     , BunsekiCheckKBN2 '
--    SET @SQL = @SQL + '     , BunsekiCheckKBN3 '
--    SET @SQL = @SQL + '     , BunsekiCheckKBN4 '
--    SET @SQL = @SQL + '     , BunsekiCheckKBN5 '
--    SET @SQL = @SQL + '     , SikinCheckKBN '
--    SET @SQL = @SQL + '     , ProjectCheckKBN '
--    SET @SQL = @SQL + '     , BunsekiCustomCheckKBN1 '
--    SET @SQL = @SQL + '     , BunsekiCustomCheckKBN2 '
--    SET @SQL = @SQL + '     , BunsekiCustomCheckKBN3 '
--    SET @SQL = @SQL + '     , BunsekiCustomCheckKBN4 '
--    SET @SQL = @SQL + '     , BunsekiCustomCheckKBN5 '
--    SET @SQL = @SQL + '     , HasseiLeaseKaishaKBN '
--    SET @SQL = @SQL + '     , HasseiZeimushoKBN '
--    SET @SQL = @SQL + '     , HasseiSiiresakiKBN '
--    SET @SQL = @SQL + '     , HasseiSiharaisakiKBN '
--    SET @SQL = @SQL + '     , HasseiTokuisakiKBN '
--    SET @SQL = @SQL + '     , HasseiKaishuusakiKBN '
--    SET @SQL = @SQL + '     , HasseiTorihikisakiKBN1 '
--    SET @SQL = @SQL + '     , HasseiTorihikisakiKBN2 '
--    SET @SQL = @SQL + '     , HasseiTorihikisakiKBN3 '
--    SET @SQL = @SQL + '     , HasseiTorihikisakiKBN4 '
--    SET @SQL = @SQL + ' ) '
--    SET @SQL = @SQL + ' SELECT '
--    SET @SQL = @SQL + '       KAN.KanjouCD '
--    SET @SQL = @SQL + '     , KAN.KaiteiDate '
--    SET @SQL = @SQL + '     , KAN.KanjouName '
--    SET @SQL = @SQL + '     , KAN.KanjouNameRyaku '
--    SET @SQL = @SQL + '     , KAN.TaishakuKBN '
--    SET @SQL = @SQL + '     , KAN.KamokuCategory '
--    SET @SQL = @SQL + '     , KAN.HyouKBN '
--    SET @SQL = @SQL + '     , KAN.KamokuKBN '
--    SET @SQL = @SQL + '     , KAN.GenyokinKBN '
--    SET @SQL = @SQL + '     , KAN.TorihikisakiKBN '
--    SET @SQL = @SQL + '     , KAN.HojoKBN '
--    SET @SQL = @SQL + '     , KAN.BumonKBN '
--    SET @SQL = @SQL + '     , KAN.KariZeiKBN '
--    SET @SQL = @SQL + '     , KAN.KasiZeiKBN '
--    SET @SQL = @SQL + '     , KAN.KariZeikomiKBN '
--    SET @SQL = @SQL + '     , KAN.KasiZeikomiKBN '
--    SET @SQL = @SQL + '     , KAN.KensakuKBN '
--    SET @SQL = @SQL + '     , KAN.KichouKBN '
--    SET @SQL = @SQL + '     , KAN.BunsekiCheckKBN1 '
--    SET @SQL = @SQL + '     , KAN.BunsekiCheckKBN2 '
--    SET @SQL = @SQL + '     , KAN.BunsekiCheckKBN3 '
--    SET @SQL = @SQL + '     , KAN.BunsekiCheckKBN4 '
--    SET @SQL = @SQL + '     , KAN.BunsekiCheckKBN5 '
--    SET @SQL = @SQL + '     , KAN.SikinCheckKBN '
--    SET @SQL = @SQL + '     , KAN.ProjectCheckKBN '
--    SET @SQL = @SQL + '     , KAN.BunsekiCustomCheckKBN1 '
--    SET @SQL = @SQL + '     , KAN.BunsekiCustomCheckKBN2 '
--    SET @SQL = @SQL + '     , KAN.BunsekiCustomCheckKBN3 '
--    SET @SQL = @SQL + '     , KAN.BunsekiCustomCheckKBN4 '
--    SET @SQL = @SQL + '     , KAN.BunsekiCustomCheckKBN5 '
--    SET @SQL = @SQL + '     , KAN.HasseiLeaseKaishaKBN '
--    SET @SQL = @SQL + '     , KAN.HasseiZeimushoKBN '
--    SET @SQL = @SQL + '     , KAN.HasseiSiiresakiKBN '
--    SET @SQL = @SQL + '     , KAN.HasseiSiharaisakiKBN '
--    SET @SQL = @SQL + '     , KAN.HasseiTokuisakiKBN '
--    SET @SQL = @SQL + '     , KAN.HasseiKaishuusakiKBN '
--    SET @SQL = @SQL + '     , KAN.HasseiTorihikisakiKBN1 '
--    SET @SQL = @SQL + '     , KAN.HasseiTorihikisakiKBN2 '
--    SET @SQL = @SQL + '     , KAN.HasseiTorihikisakiKBN3 '
--    SET @SQL = @SQL + '     , KAN.HasseiTorihikisakiKBN4 '
--    SET @SQL = @SQL + ' FROM M0000MS_MA_Kanjou KAN '
--    SET @SQL = @SQL + ' INNER JOIN ( '
--    SET @SQL = @SQL + '     SELECT '
--    SET @SQL = @SQL + '           SKA.KanjouCD '
--    SET @SQL = @SQL + '         , MAX(SKA.KaiteiDate) AS KaiteiDate '
--    SET @SQL = @SQL + '     FROM M0000MS_MA_Kanjou SKA '
--    SET @SQL = @SQL + '     INNER JOIN ( '
--    SET @SQL = @SQL + '         SELECT '
--    SET @SQL = @SQL + '             PATWK.KanjouCD '
--    SET @SQL = @SQL + '         FROM ( '
--    SET @SQL = @SQL + '             SELECT  '
--    SET @SQL = @SQL + '                 KariKanjouCD AS KanjouCD '
--    SET @SQL = @SQL + '             FROM M0000RR_MA_HonsitenSiwakeMeisai '
--    SET @SQL = @SQL + '             UNION ALL '
--    SET @SQL = @SQL + '             SELECT  '
--    SET @SQL = @SQL + '                 KasiKanjouCD AS KanjouCD '
--    SET @SQL = @SQL + '             FROM M0000RR_MA_HonsitenSiwakeMeisai '
--    SET @SQL = @SQL + '             UNION ALL '
--    SET @SQL = @SQL + '             SELECT  '
--    SET @SQL = @SQL + '                 KariZeiHontaiCD AS KanjouCD '
--    SET @SQL = @SQL + '             FROM M0000RR_MA_HonsitenSiwakeMeisai '
--    SET @SQL = @SQL + '             UNION ALL '
--    SET @SQL = @SQL + '             SELECT  '
--    SET @SQL = @SQL + '                 KasiZeiHontaiCD AS KanjouCD '
--    SET @SQL = @SQL + '             FROM M0000RR_MA_HonsitenSiwakeMeisai '
--    SET @SQL = @SQL + '             UNION ALL '
--    SET @SQL = @SQL + '             SELECT  '
--    SET @SQL = @SQL + '                 ' + dbo.M0000FC_Escape_Varchar(@KariBaraiZeiCD) + ' AS KanjouCD '
--    SET @SQL = @SQL + '             UNION ALL '
--    SET @SQL = @SQL + '             SELECT  '
--    SET @SQL = @SQL + '                 ' + dbo.M0000FC_Escape_Varchar(@KariUkeZeiCD) + ' AS KanjouCD ' 
--    SET @SQL = @SQL + '         ) PATWK '
--    SET @SQL = @SQL + '         GROUP BY PATWK.KanjouCD '
--    SET @SQL = @SQL + '     ) SPA '
--    SET @SQL = @SQL + '     ON SPA.KanjouCD = SKA.KanjouCD '
--    SET @SQL = @SQL + '     WHERE SKA.KaiteiDate <= ''' + CONVERT(VARCHAR,@HasseiDate,111) + ''''
--    SET @SQL = @SQL + '     GROUP BY SKA.KanjouCD '
--    SET @SQL = @SQL + ' ) WK '
--    SET @SQL = @SQL + ' ON  WK.KanjouCD = KAN.KanjouCD '
--    SET @SQL = @SQL + ' AND WK.KaiteiDate  = KAN.KaiteiDate '
--	SET @SQL = @SQL + ' LEFT JOIN M0000MS_MA_DenpyouKBNKamokuTokutei DKT '
--	SET @SQL = @SQL + ' ON  DKT.DenpyouKBN    = ' + dbo.M0000FC_Escape_Varchar(@DenpyouKBN)
--	SET @SQL = @SQL + ' AND DKT.KanjouCD      = KAN.KanjouCD '
--    SET @SQL = @SQL + ' WHERE DKT.KanjouCD IS NULL '

--    EXECUTE (@SQL)

--    -- @@ERROR を取得し、変数に保存する。
--    SET @ErrorCode = @@ERROR

--    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
--    IF  @ErrorCode <> 0
--    BEGIN
--        GOTO ERR_SHORI
--    END

----2010/12/13 Y.Tamashima(M04451)ダブルスタンダード対応 使用科目制御対応 End
--2012/09/14 削除 M.Shinbo 終了 (M04835)

    --本社事業所用に事業所マスタ直近データをワークに更新
    INSERT INTO #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Zigyousho (
          ZigyoushoCD
        , KaiteiDate
        , ZigyoushoName
        , SetZandakaDate
        , DenpyouSimeDate
    )
    SELECT
          ZIG.ZigyoushoCD
        , ZIG.KaiteiDate
        , ZIG.ZigyoushoName
        , ZIG.SetZandakaDate
        , ZIG.DenpyouSimeDate
    FROM M0000MS_MA_Zigyousho ZIG 
    INNER JOIN (
        SELECT 
              ZigyoushoCD
            , MAX(KaiteiDate) AS KaiteiDate
        FROM  M0000MS_MA_Zigyousho
        WHERE KaiteiDate <= @HasseiDate
        GROUP BY ZigyoushoCD
    ) SZI
    ON  SZI.ZigyoushoCD = ZIG.ZigyoushoCD
    AND SZI.KaiteiDate = ZIG.KaiteiDate

    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR

    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END


    --本支店科目マスタ直近データをワークに更新
    INSERT INTO #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_HonsitenKanjou (
          ZigyoushoCD    
        , KaiteiDate     
        , HonshaZigyoushoCD 
    )
    SELECT
          HSK.ZigyoushoCD    
        , HSK.KaiteiDate     
        , HSK.HonshaZigyoushoCD 
    FROM M0000MS_MA_HonsitenKanjou HSK
    INNER JOIN (
        SELECT 
              ZigyoushoCD
            , MAX(KaiteiDate) AS KaiteiDate
        FROM  M0000MS_MA_HonsitenKanjou
        WHERE KaiteiDate <= @HasseiDate
        GROUP BY ZigyoushoCD
    ) SHS
    ON  SHS.ZigyoushoCD = HSK.ZigyoushoCD
    AND SHS.KaiteiDate = HSK.KaiteiDate

    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR

    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END

    --事業所部門パターンマスタ直近データをワークに更新
    INSERT INTO #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_ZigyoushoBumonPattern (
          ZigyoushoCD    
        , KaiteiDate     
        , BumonCD 
    )
    SELECT
          ZBP.ZigyoushoCD    
        , ZBP.KaiteiDate     
        , ZBP.BumonCD 
    FROM M0000MS_MA_ZigyoushoBumonPattern ZBP
    INNER JOIN (
        SELECT 
              ZigyoushoCD
            , MAX(KaiteiDate) AS KaiteiDate
        FROM  M0000MS_MA_ZigyoushoBumonPattern
        WHERE KaiteiDate <= @HasseiDate
        GROUP BY ZigyoushoCD
    ) SZB
    ON  SZB.ZigyoushoCD = ZBP.ZigyoushoCD
    AND SZB.KaiteiDate  = ZBP.KaiteiDate
    
    -- ワーク削除
    DELETE FROM M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Meisai
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

    -- 本支店仕訳入力画面明細ワークに更新
    INSERT INTO M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Meisai
    (
          OperationComputerName
        , OperationUserID
        , OperationDatetime
        , OperationJobID
        , SiwakeKBN
        , KariZigyoushoCD
        , KariZigyoushoKaiteiDate
        , KariZigyoushoName
        , KariZigyoushoNameRyaku
        , KariZigyoushoSetZandakaDate
        , KariZigyoushoDenpyouSimeDate
        , KariHonshaZigyoushoCD
        , KariHonshaZigyoushoName
        , KariHonshaZigyoushoSetZandakaDate
        , KariHonshaZigyoushoDenpyouSimeDate
        , KariBumonCD
        , KariBumonKaiteiDate
        , KariBumonName
        , KariBumonNameRyaku
        , KariKanjouCD
        , KariKanjouKaiteiDate
        , KariKanjouName
        , KariKanjouNameRyaku
        , KariHojoCD
        , KariHojoKaiteiDate
        , KariHojoName
        , KariHojoNameRyaku
        , KariUtiwakeCD
        , KariUtiwakeKaiteiDate
        , KariUtiwakeName
        , KariUtiwakeNameRyaku
        , KariTorihikisakiCD
        , KariTorihikisakiKaiteiDate
        , KariTorihikisakiShanaiName
        , KariTorihikisakiLeaseKaishaKBN
        , KariTorihikisakiZeimushoKBN
        , KariTorihikisakiHasseiSiiresakiKBN
        , KariTorihikisakiHasseiSiharaisakiKBN
        , KariTorihikisakiHasseiTokuisakiKBN
        , KariTorihikisakiHasseiKaishuusakiKBN
        , KariTorihikisakiHasseiTorihikisakiKBN1
        , KariTorihikisakiHasseiTorihikisakiKBN2
        , KariTorihikisakiHasseiTorihikisakiKBN3
        , KariTorihikisakiHasseiTorihikisakiKBN4
        , KariTaishakuKBN
        , KariKingaku
        , KariZeigaku
        , KariZeiKamokuCD
        , KariZeiKaiteiDate
        , KariZeiTaishakuKBN
        , KariZeiHontaiCD
        , KariZeiHontaiKaiteiDate
        , KariZeiHontaiName
        , KariZeiHontaiNameRyaku
        , KariZeiHontaiKariZeiKBN
        , KariZeiHontaiKasiZeiKBN
        , KariZeiHontaiKamokuCategory
        , KariZeiHontaiKamokuKBN
        , KariZeiKBN
        , KariZeiRate
        , KariHasuuKBN
        , KariZeiKeisanKBN
        , KariZeiKamokuKBN
        , KariZeiKBNZeiKaisiDate
        , KariZeiKBNZeiHaisiDate
        , KariKanjouKariZeiKamokuKBN
        , KariKanjouKariZeiKBNZeiKaisiDate
        , KariKanjouKariZeiKBNZeiHaisiDate
        , KariKanjouKasiZeiKamokuKBN
        , KariKanjouKasiZeiKBNZeiKaisiDate
        , KariKanjouKasiZeiKBNZeiHaisiDate
        , KariZeiHontaiKariZeiKamokuKBN
        , KariZeiHontaiKariZeiKBNZeiKaisiDate
        , KariZeiHontaiKariZeiKBNZeiHaisiDate
        , KariZeiHontaiKasiZeiKamokuKBN
        , KariZeiHontaiKasiZeiKBNZeiKaisiDate
        , KariZeiHontaiKasiZeiKBNZeiHaisiDate
        , KariZeikomiKBN
        , KariZeikomiKBNName
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
        , KariBunsekiName1
        , KariBunsekiName2
        , KariBunsekiName3
        , KariBunsekiName4
        , KariBunsekiName5
        , KariSikinName
        , KariProjectName
        , KariKanjouKariZeiKBN
        , KariKanjouKasiZeiKBN
        , KariKanjouKamokuCategory
        , KariKanjouKamokuKBN
        , KariKanjouBunsekiCheckKBN1
        , KariKanjouBunsekiCheckKBN2
        , KariKanjouBunsekiCheckKBN3
        , KariKanjouBunsekiCheckKBN4
        , KariKanjouBunsekiCheckKBN5
        , KariKanjouSikinCheckKBN
        , KariKanjouProjectCheckKBN
        , KariKanjouHasseiLeaseKaishaKBN
        , KariKanjouHasseiZeimushoKBN
        , KariKanjouHasseiSiiresakiKBN
        , KariKanjouHasseiSiharaisakiKBN
        , KariKanjouHasseiTokuisakiKBN
        , KariKanjouHasseiKaishuusakiKBN
        , KariKanjouHasseiTorihikisakiKBN1
        , KariKanjouHasseiTorihikisakiKBN2
        , KariKanjouHasseiTorihikisakiKBN3
        , KariKanjouHasseiTorihikisakiKBN4
        , KariKanjouCDErrorFlag
        , KariHojoCDErrorFlag
        , KariUtiwakeCDErrorFlag
        , KariZigyoushoCDErrorFlag
        , KariBumonCDErrorFlag
        , KariTorihikisakiCDErrorFlag
        , KariZeiKBNErrorFlag
        , KariZeikomiKBNErrorFlag
        , KariZeiHontaiCDErrorFlag
        , KariBunsekiCD1ErrorFlag
        , KariBunsekiCD2ErrorFlag
        , KariBunsekiCD3ErrorFlag
        , KariBunsekiCD4ErrorFlag
        , KariBunsekiCD5ErrorFlag
        , KariSikinCDErrorFlag
        , KariProjectCDErrorFlag
        , KasiZigyoushoCD
        , KasiZigyoushoKaiteiDate
        , KasiZigyoushoName
        , KasiZigyoushoNameRyaku
        , KasiZigyoushoSetZandakaDate
        , KasiZigyoushoDenpyouSimeDate
        , KasiHonshaZigyoushoCD
        , KasiHonshaZigyoushoName
        , KasiHonshaZigyoushoSetZandakaDate
        , KasiHonshaZigyoushoDenpyouSimeDate
        , KasiBumonCD
        , KasiBumonKaiteiDate
        , KasiBumonName
        , KasiBumonNameRyaku
        , KasiKanjouCD
        , KasiKanjouKaiteiDate
        , KasiKanjouName
        , KasiKanjouNameRyaku
        , KasiHojoCD
        , KasiHojoKaiteiDate
        , KasiHojoName
        , KasiHojoNameRyaku
        , KasiUtiwakeCD
        , KasiUtiwakeKaiteiDate
        , KasiUtiwakeName
        , KasiUtiwakeNameRyaku
        , KasiTorihikisakiCD
        , KasiTorihikisakiKaiteiDate
        , KasiTorihikisakiShanaiName
        , KasiTorihikisakiLeaseKaishaKBN
        , KasiTorihikisakiZeimushoKBN
        , KasiTorihikisakiHasseiSiiresakiKBN
        , KasiTorihikisakiHasseiSiharaisakiKBN
        , KasiTorihikisakiHasseiTokuisakiKBN
        , KasiTorihikisakiHasseiKaishuusakiKBN
        , KasiTorihikisakiHasseiTorihikisakiKBN1
        , KasiTorihikisakiHasseiTorihikisakiKBN2
        , KasiTorihikisakiHasseiTorihikisakiKBN3
        , KasiTorihikisakiHasseiTorihikisakiKBN4
        , KasiTaishakuKBN
        , KasiKingaku
        , KasiZeigaku
        , KasiZeiKamokuCD
        , KasiZeiKaiteiDate
        , KasiZeiTaishakuKBN
        , KasiZeiHontaiCD
        , KasiZeiHontaiKaiteiDate
        , KasiZeiHontaiName
        , KasiZeiHontaiNameRyaku
        , KasiZeiHontaiKariZeiKBN
        , KasiZeiHontaiKasiZeiKBN
        , KasiZeiHontaiKamokuCategory
        , KasiZeiHontaiKamokuKBN
        , KasiZeiKBN
        , KasiZeiRate
        , KasiHasuuKBN
        , KasiZeiKeisanKBN
        , KasiZeiKamokuKBN
        , KasiZeiKBNZeiKaisiDate
        , KasiZeiKBNZeiHaisiDate
        , KasiKanjouKariZeiKamokuKBN
        , KasiKanjouKariZeiKBNZeiKaisiDate
        , KasiKanjouKariZeiKBNZeiHaisiDate
        , KasiKanjouKasiZeiKamokuKBN
        , KasiKanjouKasiZeiKBNZeiKaisiDate
        , KasiKanjouKasiZeiKBNZeiHaisiDate
        , KasiZeiHontaiKariZeiKamokuKBN
        , KasiZeiHontaiKariZeiKBNZeiKaisiDate
        , KasiZeiHontaiKariZeiKBNZeiHaisiDate
        , KasiZeiHontaiKasiZeiKamokuKBN
        , KasiZeiHontaiKasiZeiKBNZeiKaisiDate
        , KasiZeiHontaiKasiZeiKBNZeiHaisiDate
        , KasiZeikomiKBN
        , KasiZeikomiKBNName
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
        , KasiBunsekiName1
        , KasiBunsekiName2
        , KasiBunsekiName3
        , KasiBunsekiName4
        , KasiBunsekiName5
        , KasiSikinName
        , KasiProjectName
        , KasiKanjouKariZeiKBN
        , KasiKanjouKasiZeiKBN
        , KasiKanjouKamokuCategory
        , KasiKanjouKamokuKBN
        , KasiKanjouBunsekiCheckKBN1
        , KasiKanjouBunsekiCheckKBN2
        , KasiKanjouBunsekiCheckKBN3
        , KasiKanjouBunsekiCheckKBN4
        , KasiKanjouBunsekiCheckKBN5
        , KasiKanjouSikinCheckKBN
        , KasiKanjouProjectCheckKBN
        , KasiKanjouHasseiLeaseKaishaKBN
        , KasiKanjouHasseiZeimushoKBN
        , KasiKanjouHasseiSiiresakiKBN
        , KasiKanjouHasseiSiharaisakiKBN
        , KasiKanjouHasseiTokuisakiKBN
        , KasiKanjouHasseiKaishuusakiKBN
        , KasiKanjouHasseiTorihikisakiKBN1
        , KasiKanjouHasseiTorihikisakiKBN2
        , KasiKanjouHasseiTorihikisakiKBN3
        , KasiKanjouHasseiTorihikisakiKBN4
        , KasiKanjouCDErrorFlag
        , KasiHojoCDErrorFlag
        , KasiUtiwakeCDErrorFlag
        , KasiZigyoushoCDErrorFlag
        , KasiBumonCDErrorFlag
        , KasiTorihikisakiCDErrorFlag
        , KasiZeiKBNErrorFlag
        , KasiZeikomiKBNErrorFlag
        , KasiZeiHontaiCDErrorFlag
        , KasiBunsekiCD1ErrorFlag
        , KasiBunsekiCD2ErrorFlag
        , KasiBunsekiCD3ErrorFlag
        , KasiBunsekiCD4ErrorFlag
        , KasiBunsekiCD5ErrorFlag
        , KasiSikinCDErrorFlag
        , KasiProjectCDErrorFlag
        , MeisaiTekiyou
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
        , HON.SiwakeKBN
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KariZigyoushoCD             ELSE HON.KasiZigyoushoCD              END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KariZigyoushoKaiteiDate     ELSE HON.KasiZigyoushoKaiteiDate      END
        , CASE WHEN @p_TensouMode <> 2 THEN KRZIG.ZigyoushoName             ELSE KSZIG.ZigyoushoName              END
        , CASE WHEN @p_TensouMode <> 2 THEN KRZIG.ZigyoushoNameRyaku        ELSE KSZIG.ZigyoushoNameRyaku         END
        , CASE WHEN @p_TensouMode <> 2 THEN KRZIG.SetZandakaDate            ELSE KSZIG.SetZandakaDate             END
        , CASE WHEN @p_TensouMode <> 2 THEN KRZIG.DenpyouSimeDate           ELSE KSZIG.DenpyouSimeDate            END
        , CASE WHEN @p_TensouMode <> 2 THEN KRHSK.HonshaZigyoushoCD         ELSE KSHSK.HonshaZigyoushoCD          END
        , CASE WHEN @p_TensouMode <> 2 THEN KRHZI.ZigyoushoName             ELSE KSHZI.ZigyoushoName              END
        , CASE WHEN @p_TensouMode <> 2 THEN KRHZI.SetZandakaDate            ELSE KSHZI.SetZandakaDate             END
        , CASE WHEN @p_TensouMode <> 2 THEN KRHZI.DenpyouSimeDate           ELSE KSHZI.DenpyouSimeDate            END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KariBumonCD                 ELSE HON.KasiBumonCD                  END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KariBumonKaiteiDate         ELSE HON.KasiBumonKaiteiDate          END
        , CASE WHEN @p_TensouMode <> 2 THEN KRBMN.BumonName                 ELSE KSBMN.BumonName                  END
        , CASE WHEN @p_TensouMode <> 2 THEN KRBMN.BumonNameRyaku            ELSE KSBMN.BumonNameRyaku             END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KariKanjouCD                ELSE HON.KasiKanjouCD                 END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KariKanjouKaiteiDate        ELSE HON.KasiKanjouKaiteiDate         END
        , CASE WHEN @p_TensouMode <> 2 THEN KRKAN.KanjouName                ELSE KSKAN.KanjouName                 END
        , CASE WHEN @p_TensouMode <> 2 THEN KRKAN.KanjouNameRyaku           ELSE KSKAN.KanjouNameRyaku            END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KariHojoCD                  ELSE HON.KasiHojoCD                   END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KariHojoKaiteiDate          ELSE HON.KasiHojoKaiteiDate           END
        , CASE WHEN @p_TensouMode <> 2 THEN KRHOJ.HojoName                  ELSE KSHOJ.HojoName                   END
        , CASE WHEN @p_TensouMode <> 2 THEN KRHOJ.HojoNameRyaku             ELSE KSHOJ.HojoNameRyaku              END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KariUtiwakeCD               ELSE HON.KasiUtiwakeCD                END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KariUtiwakeKaiteiDate       ELSE HON.KasiUtiwakeKaiteiDate        END
        , CASE WHEN @p_TensouMode <> 2 THEN KRUTI.UtiwakeName               ELSE KSUTI.UtiwakeName                END
        , CASE WHEN @p_TensouMode <> 2 THEN KRUTI.UtiwakeNameRyaku          ELSE KSUTI.UtiwakeNameRyaku           END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KariTorihikisakiCD          ELSE HON.KasiTorihikisakiCD           END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KariTorihikisakiKaiteiDate  ELSE HON.KasiTorihikisakiKaiteiDate   END
        , CASE WHEN @p_TensouMode <> 2 THEN KRTRS.TorihikisakiShanaiName    ELSE KSTRS.TorihikisakiShanaiName     END
        , CASE WHEN @p_TensouMode <> 2 THEN KRTRS.LeaseKaishaKBN            ELSE KSTRS.LeaseKaishaKBN             END
        , CASE WHEN @p_TensouMode <> 2 THEN KRTRS.ZeimushoKBN               ELSE KSTRS.ZeimushoKBN                END
        , CASE WHEN @p_TensouMode <> 2 THEN KRTRS.HasseiSiiresakiKBN        ELSE KSTRS.HasseiSiiresakiKBN         END
        , CASE WHEN @p_TensouMode <> 2 THEN KRTRS.HasseiSiharaisakiKBN      ELSE KSTRS.HasseiSiharaisakiKBN       END
        , CASE WHEN @p_TensouMode <> 2 THEN KRTRS.HasseiTokuisakiKBN        ELSE KSTRS.HasseiTokuisakiKBN         END
        , CASE WHEN @p_TensouMode <> 2 THEN KRTRS.HasseiKaishuusakiKBN      ELSE KSTRS.HasseiKaishuusakiKBN       END
        , CASE WHEN @p_TensouMode <> 2 THEN KRTRS.HasseiTorihikisakiKBN1    ELSE KSTRS.HasseiTorihikisakiKBN1     END
        , CASE WHEN @p_TensouMode <> 2 THEN KRTRS.HasseiTorihikisakiKBN2    ELSE KSTRS.HasseiTorihikisakiKBN2     END
        , CASE WHEN @p_TensouMode <> 2 THEN KRTRS.HasseiTorihikisakiKBN3    ELSE KSTRS.HasseiTorihikisakiKBN3     END
        , CASE WHEN @p_TensouMode <> 2 THEN KRTRS.HasseiTorihikisakiKBN4    ELSE KSTRS.HasseiTorihikisakiKBN4     END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KariTaishakuKBN             ELSE HON.KasiTaishakuKBN              END
        , CASE WHEN @p_TensouMode =  0 THEN HON.KariKingaku 
               WHEN @p_TensouMode =  1 THEN HON.KariKingaku * (-1)
               ELSE HON.KasiKingaku
          END
        , CASE WHEN @p_TensouMode =  0 THEN HON.KariZeigaku 
               WHEN @p_TensouMode =  1 THEN HON.KariZeigaku * (-1)
               ELSE HON.KasiZeigaku
          END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KariZeiKamokuCD             ELSE HON.KasiZeiKamokuCD              END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KariZeiKaiteiDate           ELSE HON.KasiZeiKaiteiDate            END
        , CASE WHEN @p_TensouMode <> 2 THEN KRSKAN.TaishakuKBN              ELSE KSSKAN.TaishakuKBN               END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KariZeiHontaiCD             ELSE HON.KasiZeiHontaiCD              END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KariZeiHontaiKaiteiDate     ELSE HON.KasiZeiHontaiKaiteiDate      END
        , CASE WHEN @p_TensouMode <> 2 THEN KRHKAN.KanjouName               ELSE KSHKAN.KanjouName                END
        , CASE WHEN @p_TensouMode <> 2 THEN KRHKAN.KanjouNameRyaku          ELSE KSHKAN.KanjouNameRyaku           END
        , CASE WHEN @p_TensouMode <> 2 THEN KRHKAN.KariZeiKBN               ELSE KSHKAN.KariZeiKBN                END
        , CASE WHEN @p_TensouMode <> 2 THEN KRHKAN.KasiZeiKBN               ELSE KSHKAN.KasiZeiKBN                END
        , CASE WHEN @p_TensouMode <> 2 THEN KRHKAN.KamokuCategory           ELSE KSHKAN.KamokuCategory            END
        , CASE WHEN @p_TensouMode <> 2 THEN KRHKAN.KamokuKBN                ELSE KSHKAN.KamokuKBN                 END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KariZeiKBN                  ELSE HON.KasiZeiKBN                   END
        , CASE WHEN @p_TensouMode <> 2 THEN KRZKB.ZeiRate                   ELSE KSZKB.ZeiRate                    END
        , CASE WHEN @p_TensouMode <> 2 THEN KRZKB.HasuuKBN                  ELSE KSZKB.HasuuKBN                   END
        , CASE WHEN @p_TensouMode <> 2 THEN KRZKB.ZeiKeisanKBN              ELSE KSZKB.ZeiKeisanKBN               END
        , CASE WHEN @p_TensouMode <> 2 THEN KRZKB.ZeiKamokuKBN              ELSE KSZKB.ZeiKamokuKBN               END
        , CASE WHEN @p_TensouMode <> 2 THEN KRZKB.ZeiKaisiDate              ELSE KSZKB.ZeiKaisiDate               END
        , CASE WHEN @p_TensouMode <> 2 THEN KRZKB.ZeiHaisiDate              ELSE KSZKB.ZeiHaisiDate               END
--2012/10/05 変更 K.Morita ST (M04851)平成26年消費税改正対応
        --, CASE WHEN @p_TensouMode <> 2 THEN KRKRZKB.ZeiKamokuKBN            ELSE KSKRZKB.ZeiKamokuKBN             END
        --, CASE WHEN @p_TensouMode <> 2 THEN KRKRZKB.ZeiKaisiDate            ELSE KSKRZKB.ZeiKaisiDate             END
        --, CASE WHEN @p_TensouMode <> 2 THEN KRKRZKB.ZeiHaisiDate            ELSE KSKRZKB.ZeiHaisiDate             END
        --, CASE WHEN @p_TensouMode <> 2 THEN KRKSZKB.ZeiKamokuKBN            ELSE KSKSZKB.ZeiKamokuKBN             END
        --, CASE WHEN @p_TensouMode <> 2 THEN KRKSZKB.ZeiKaisiDate            ELSE KSKSZKB.ZeiKaisiDate             END
        --, CASE WHEN @p_TensouMode <> 2 THEN KRKSZKB.ZeiHaisiDate            ELSE KSKSZKB.ZeiHaisiDate             END
        , CASE WHEN @p_TensouMode <> 2 THEN (CASE WHEN KRHOJ.ShokiSetKariZeiKBN IS NULL THEN KRKRZKB.ZeiKamokuKBN
                                                                                        ELSE HKRKRZKB.ZeiKamokuKBN END)
                                       ELSE (CASE WHEN KSHOJ.ShokiSetKariZeiKBN IS NULL THEN KSKRZKB.ZeiKamokuKBN
                                                                                        ELSE HKSKRZKB.ZeiKamokuKBN END) END
        , CASE WHEN @p_TensouMode <> 2 THEN (CASE WHEN KRHOJ.ShokiSetKariZeiKBN IS NULL THEN KRKRZKB.ZeiKaisiDate
                                                                                        ELSE HKRKRZKB.ZeiKaisiDate END)
                                       ELSE (CASE WHEN KSHOJ.ShokiSetKariZeiKBN IS NULL THEN KSKRZKB.ZeiKaisiDate
                                                                                        ELSE HKSKRZKB.ZeiKaisiDate END) END
        , CASE WHEN @p_TensouMode <> 2 THEN (CASE WHEN KRHOJ.ShokiSetKariZeiKBN IS NULL THEN KRKRZKB.ZeiHaisiDate
                                                                                        ELSE HKRKRZKB.ZeiHaisiDate END)
                                       ELSE (CASE WHEN KSHOJ.ShokiSetKariZeiKBN IS NULL THEN KSKRZKB.ZeiHaisiDate
                                                                                        ELSE HKSKRZKB.ZeiHaisiDate END) END
        , CASE WHEN @p_TensouMode <> 2 THEN (CASE WHEN KRHOJ.ShokiSetKasiZeiKBN IS NULL THEN KRKSZKB.ZeiKamokuKBN
                                                                                        ELSE HKRKSZKB.ZeiKamokuKBN END)
                                       ELSE (CASE WHEN KSHOJ.ShokiSetKasiZeiKBN IS NULL THEN KSKSZKB.ZeiKamokuKBN
                                                                                        ELSE HKSKSZKB.ZeiKamokuKBN END) END
        , CASE WHEN @p_TensouMode <> 2 THEN (CASE WHEN KRHOJ.ShokiSetKasiZeiKBN IS NULL THEN KRKSZKB.ZeiKaisiDate
                                                                                        ELSE HKRKSZKB.ZeiKaisiDate END)
                                       ELSE (CASE WHEN KSHOJ.ShokiSetKasiZeiKBN IS NULL THEN KSKSZKB.ZeiKaisiDate
                                                                                        ELSE HKSKSZKB.ZeiKaisiDate END) END
        , CASE WHEN @p_TensouMode <> 2 THEN (CASE WHEN KRHOJ.ShokiSetKasiZeiKBN IS NULL THEN KRKSZKB.ZeiHaisiDate
                                                                                        ELSE HKRKSZKB.ZeiHaisiDate END)
                                       ELSE (CASE WHEN KSHOJ.ShokiSetKasiZeiKBN IS NULL THEN KSKSZKB.ZeiHaisiDate
                                                                                        ELSE HKSKSZKB.ZeiHaisiDate END) END
--2012/10/05 変更 K.Morita ED (M04851)平成26年消費税改正対応
        , CASE WHEN @p_TensouMode <> 2 THEN KRKRHZKB.ZeiKamokuKBN           ELSE KSKRHZKB.ZeiKamokuKBN            END
        , CASE WHEN @p_TensouMode <> 2 THEN KRKRHZKB.ZeiKaisiDate           ELSE KSKRHZKB.ZeiKaisiDate            END
        , CASE WHEN @p_TensouMode <> 2 THEN KRKRHZKB.ZeiHaisiDate           ELSE KSKRHZKB.ZeiHaisiDate            END
        , CASE WHEN @p_TensouMode <> 2 THEN KRKSHZKB.ZeiKamokuKBN           ELSE KSKSHZKB.ZeiKamokuKBN            END
        , CASE WHEN @p_TensouMode <> 2 THEN KRKSHZKB.ZeiKaisiDate           ELSE KSKSHZKB.ZeiKaisiDate            END
        , CASE WHEN @p_TensouMode <> 2 THEN KRKSHZKB.ZeiHaisiDate           ELSE KSKSHZKB.ZeiHaisiDate            END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KariZeikomiKBN              ELSE HON.KasiZeikomiKBN               END
        , CASE WHEN @p_TensouMode <> 2 THEN KRCODE.CodeName                 ELSE KSCODE.CodeName                  END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KariTorihikisakiKBN         ELSE HON.KasiTorihikisakiKBN          END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KariHojoKBN                 ELSE HON.KasiHojoKBN                  END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KariUtiwakeKBN              ELSE HON.KasiUtiwakeKBN               END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KariBumonKBN                ELSE HON.KasiBumonKBN                 END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KariBunsekiCD1              ELSE HON.KasiBunsekiCD1               END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KariBunsekiCD2              ELSE HON.KasiBunsekiCD2               END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KariBunsekiCD3              ELSE HON.KasiBunsekiCD3               END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KariBunsekiCD4              ELSE HON.KasiBunsekiCD4               END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KariBunsekiCD5              ELSE HON.KasiBunsekiCD5               END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KariSikinCD                 ELSE HON.KasiSikinCD                  END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KariProjectCD               ELSE HON.KasiProjectCD                END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KariBunsekiCustomizeCD1     ELSE HON.KasiBunsekiCustomizeCD1      END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KariBunsekiCustomizeCD2     ELSE HON.KasiBunsekiCustomizeCD2      END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KariBunsekiCustomizeCD3     ELSE HON.KasiBunsekiCustomizeCD3      END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KariBunsekiCustomizeCD4     ELSE HON.KasiBunsekiCustomizeCD4      END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KariBunsekiCustomizeCD5     ELSE HON.KasiBunsekiCustomizeCD5      END
        , CASE WHEN @p_TensouMode <> 2 THEN KRBUN1.BunsekiName              ELSE KSBUN1.BunsekiName               END
        , CASE WHEN @p_TensouMode <> 2 THEN KRBUN2.BunsekiName              ELSE KSBUN2.BunsekiName               END
        , CASE WHEN @p_TensouMode <> 2 THEN KRBUN3.BunsekiName              ELSE KSBUN3.BunsekiName               END
        , CASE WHEN @p_TensouMode <> 2 THEN KRBUN4.BunsekiName              ELSE KSBUN4.BunsekiName               END
        , CASE WHEN @p_TensouMode <> 2 THEN KRBUN5.BunsekiName              ELSE KSBUN5.BunsekiName               END
        , CASE WHEN @p_TensouMode <> 2 THEN KRBUN6.BunsekiName              ELSE KSBUN6.BunsekiName               END
        , CASE WHEN @p_TensouMode <> 2 THEN KRBUN7.BunsekiName              ELSE KSBUN7.BunsekiName               END
        , CASE WHEN @p_TensouMode <> 2 THEN KRKAN.KariZeiKBN                ELSE KSKAN.KariZeiKBN                 END
        , CASE WHEN @p_TensouMode <> 2 THEN KRKAN.KasiZeiKBN                ELSE KSKAN.KasiZeiKBN                 END
        , CASE WHEN @p_TensouMode <> 2 THEN KRKAN.KamokuCategory            ELSE KSKAN.KamokuCategory             END
        , CASE WHEN @p_TensouMode <> 2 THEN KRKAN.KamokuKBN                 ELSE KSKAN.KamokuKBN                  END
        , CASE WHEN @p_TensouMode <> 2 THEN KRKAN.BunsekiCheckKBN1          ELSE KSKAN.BunsekiCheckKBN1           END
        , CASE WHEN @p_TensouMode <> 2 THEN KRKAN.BunsekiCheckKBN2          ELSE KSKAN.BunsekiCheckKBN2           END
        , CASE WHEN @p_TensouMode <> 2 THEN KRKAN.BunsekiCheckKBN3          ELSE KSKAN.BunsekiCheckKBN3           END
        , CASE WHEN @p_TensouMode <> 2 THEN KRKAN.BunsekiCheckKBN4          ELSE KSKAN.BunsekiCheckKBN4           END
        , CASE WHEN @p_TensouMode <> 2 THEN KRKAN.BunsekiCheckKBN5          ELSE KSKAN.BunsekiCheckKBN5           END
        , CASE WHEN @p_TensouMode <> 2 THEN KRKAN.SikinCheckKBN             ELSE KSKAN.SikinCheckKBN              END
        , CASE WHEN @p_TensouMode <> 2 THEN KRKAN.ProjectCheckKBN           ELSE KSKAN.ProjectCheckKBN            END
        , CASE WHEN @p_TensouMode <> 2 THEN KRKAN.HasseiLeaseKaishaKBN      ELSE KSKAN.HasseiLeaseKaishaKBN       END
        , CASE WHEN @p_TensouMode <> 2 THEN KRKAN.HasseiZeimushoKBN         ELSE KSKAN.HasseiZeimushoKBN          END
        , CASE WHEN @p_TensouMode <> 2 THEN KRKAN.HasseiSiiresakiKBN        ELSE KSKAN.HasseiSiiresakiKBN         END
        , CASE WHEN @p_TensouMode <> 2 THEN KRKAN.HasseiSiharaisakiKBN      ELSE KSKAN.HasseiSiharaisakiKBN       END
        , CASE WHEN @p_TensouMode <> 2 THEN KRKAN.HasseiTokuisakiKBN        ELSE KSKAN.HasseiTokuisakiKBN         END
        , CASE WHEN @p_TensouMode <> 2 THEN KRKAN.HasseiKaishuusakiKBN      ELSE KSKAN.HasseiKaishuusakiKBN       END
        , CASE WHEN @p_TensouMode <> 2 THEN KRKAN.HasseiTorihikisakiKBN1    ELSE KSKAN.HasseiTorihikisakiKBN1     END
        , CASE WHEN @p_TensouMode <> 2 THEN KRKAN.HasseiTorihikisakiKBN2    ELSE KSKAN.HasseiTorihikisakiKBN2     END
        , CASE WHEN @p_TensouMode <> 2 THEN KRKAN.HasseiTorihikisakiKBN3    ELSE KSKAN.HasseiTorihikisakiKBN3     END
        , CASE WHEN @p_TensouMode <> 2 THEN KRKAN.HasseiTorihikisakiKBN4    ELSE KSKAN.HasseiTorihikisakiKBN4     END
--2012/09/14 修正 M.Shinbo 開始 (M04835)
----2010/12/13 Y.Tamashima(M04451)ダブルスタンダード対応 使用科目制御対応 Start
----        , 0
--        , CASE WHEN @p_TensouMode <> 2 THEN 
--                    CASE WHEN HON.KariKanjouCD IS NOT NULL THEN 
--							 (CASE WHEN KRKAN.KanjouCD IS NULL THEN 1 
--								   ELSE 0 END
--							 ) 
--                    ELSE 0 END 
--               ELSE
--                    CASE WHEN HON.KasiKanjouCD IS NOT NULL THEN 
--							 (CASE WHEN KSKAN.KanjouCD IS NULL THEN 1 
--								   ELSE 0 END
--							 ) 
--                    ELSE 0 END 
--          END
----2010/12/13 Y.Tamashima(M04451)ダブルスタンダード対応 使用科目制御対応 End
        , 0
--2012/09/14 修正 M.Shinbo 終了 (M04835)
        , 0
        , 0
        , 0
        , CASE WHEN @p_TensouMode <> 2 THEN CASE WHEN HON.KariBumonKBN <> 0 AND HON.KariZigyoushoCD IS NULL AND HDKRZBP.BumonCD IS NULL THEN 2 
                                                 WHEN HON.KariBumonKBN <> 0 AND HON.KariZigyoushoCD IS NOT NULL AND KRZBP.BumonCD IS NULL THEN 2 ELSE 0 END     
                                       ELSE CASE WHEN HON.KasiBumonKBN <> 0 AND HON.KasiZigyoushoCD IS NULL AND HDKSZBP.BumonCD IS NULL THEN 2
                                                 WHEN HON.KasiBumonKBN <> 0 AND HON.KasiZigyoushoCD IS NOT NULL AND KSZBP.BumonCD IS NULL THEN 2 ELSE 0 END     
          END
        , 0
        , 0
        , 0
--2012/09/14 修正 M.Shinbo 開始 (M04835)
----2010/12/13 Y.Tamashima(M04451)ダブルスタンダード対応 使用科目制御対応 Start
----        , 0
--        , CASE WHEN @p_TensouMode <> 2 THEN 
--                    CASE WHEN HON.KariZeiHontaiCD IS NOT NULL THEN 
--							 (CASE WHEN KRHKAN.KanjouCD IS NULL THEN 1 
--								   ELSE 0 END
--							 ) 
--                    ELSE 0 END 
--               ELSE
--                    CASE WHEN HON.KasiZeiHontaiCD IS NOT NULL THEN 
--							 (CASE WHEN KSHKAN.KanjouCD IS NULL THEN 1 
--								   ELSE 0 END
--							 ) 
--                    ELSE 0 END 
--          END
----2010/12/13 Y.Tamashima(M04451)ダブルスタンダード対応 使用科目制御対応 End
        , 0
--2012/09/14 修正 M.Shinbo 終了 (M04835)
        , CASE WHEN @p_TensouMode <> 2 THEN CASE WHEN HON.KariBunsekiCD1 IS NOT NULL AND KRBUN1.BunsekiCD IS NULL THEN 1 ELSE 0 END
                                       ELSE CASE WHEN HON.KasiBunsekiCD1 IS NOT NULL AND KSBUN1.BunsekiCD IS NULL THEN 1 ELSE 0 END
          END
        , CASE WHEN @p_TensouMode <> 2 THEN CASE WHEN HON.KariBunsekiCD2 IS NOT NULL AND KRBUN2.BunsekiCD IS NULL THEN 1 ELSE 0 END
                                       ELSE CASE WHEN HON.KasiBunsekiCD2 IS NOT NULL AND KSBUN2.BunsekiCD IS NULL THEN 1 ELSE 0 END
          END
        , CASE WHEN @p_TensouMode <> 2 THEN CASE WHEN HON.KariBunsekiCD3 IS NOT NULL AND KRBUN3.BunsekiCD IS NULL THEN 1 ELSE 0 END
                                       ELSE CASE WHEN HON.KasiBunsekiCD3 IS NOT NULL AND KSBUN3.BunsekiCD IS NULL THEN 1 ELSE 0 END
          END
        , CASE WHEN @p_TensouMode <> 2 THEN CASE WHEN HON.KariBunsekiCD4 IS NOT NULL AND KRBUN4.BunsekiCD IS NULL THEN 1 ELSE 0 END
                                       ELSE CASE WHEN HON.KasiBunsekiCD4 IS NOT NULL AND KSBUN4.BunsekiCD IS NULL THEN 1 ELSE 0 END
          END
        , CASE WHEN @p_TensouMode <> 2 THEN CASE WHEN HON.KariBunsekiCD5 IS NOT NULL AND KRBUN5.BunsekiCD IS NULL THEN 1 ELSE 0 END
                                       ELSE CASE WHEN HON.KasiBunsekiCD5 IS NOT NULL AND KSBUN5.BunsekiCD IS NULL THEN 1 ELSE 0 END
          END
        , CASE WHEN @p_TensouMode <> 2 THEN CASE WHEN HON.KariSikinCD    IS NOT NULL AND KRBUN6.BunsekiCD IS NULL THEN 1 ELSE 0 END
                                       ELSE CASE WHEN HON.KasiSikinCD    IS NOT NULL AND KSBUN6.BunsekiCD IS NULL THEN 1 ELSE 0 END
          END
        , CASE WHEN @p_TensouMode <> 2 THEN CASE WHEN HON.KariProjectCD  IS NOT NULL AND KRBUN7.BunsekiCD IS NULL THEN 1 ELSE 0 END
                                       ELSE CASE WHEN HON.KasiProjectCD  IS NOT NULL AND KSBUN7.BunsekiCD IS NULL THEN 1 ELSE 0 END
          END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KasiZigyoushoCD             ELSE HON.KariZigyoushoCD              END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KasiZigyoushoKaiteiDate     ELSE HON.KariZigyoushoKaiteiDate      END
        , CASE WHEN @p_TensouMode <> 2 THEN KSZIG.ZigyoushoName             ELSE KRZIG.ZigyoushoName              END
        , CASE WHEN @p_TensouMode <> 2 THEN KSZIG.ZigyoushoNameRyaku        ELSE KRZIG.ZigyoushoNameRyaku         END
        , CASE WHEN @p_TensouMode <> 2 THEN KSZIG.SetZandakaDate            ELSE KRZIG.SetZandakaDate             END
        , CASE WHEN @p_TensouMode <> 2 THEN KSZIG.DenpyouSimeDate           ELSE KRZIG.DenpyouSimeDate            END
        , CASE WHEN @p_TensouMode <> 2 THEN KSHSK.HonshaZigyoushoCD         ELSE KRHSK.HonshaZigyoushoCD          END
        , CASE WHEN @p_TensouMode <> 2 THEN KSHZI.ZigyoushoName             ELSE KRHZI.ZigyoushoName              END
        , CASE WHEN @p_TensouMode <> 2 THEN KSHZI.SetZandakaDate            ELSE KRHZI.SetZandakaDate             END
        , CASE WHEN @p_TensouMode <> 2 THEN KSHZI.DenpyouSimeDate           ELSE KRHZI.DenpyouSimeDate            END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KasiBumonCD                 ELSE HON.KariBumonCD                  END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KasiBumonKaiteiDate         ELSE HON.KariBumonKaiteiDate          END
        , CASE WHEN @p_TensouMode <> 2 THEN KSBMN.BumonName                 ELSE KRBMN.BumonName                  END
        , CASE WHEN @p_TensouMode <> 2 THEN KSBMN.BumonNameRyaku            ELSE KRBMN.BumonNameRyaku             END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KasiKanjouCD                ELSE HON.KariKanjouCD                 END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KasiKanjouKaiteiDate        ELSE HON.KariKanjouKaiteiDate         END
        , CASE WHEN @p_TensouMode <> 2 THEN KSKAN.KanjouName                ELSE KRKAN.KanjouName                 END
        , CASE WHEN @p_TensouMode <> 2 THEN KSKAN.KanjouNameRyaku           ELSE KRKAN.KanjouNameRyaku            END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KasiHojoCD                  ELSE HON.KariHojoCD                   END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KasiHojoKaiteiDate          ELSE HON.KariHojoKaiteiDate           END
        , CASE WHEN @p_TensouMode <> 2 THEN KSHOJ.HojoName                  ELSE KRHOJ.HojoName                   END
        , CASE WHEN @p_TensouMode <> 2 THEN KSHOJ.HojoNameRyaku             ELSE KRHOJ.HojoNameRyaku              END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KasiUtiwakeCD               ELSE HON.KariUtiwakeCD                END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KasiUtiwakeKaiteiDate       ELSE HON.KariUtiwakeKaiteiDate        END
        , CASE WHEN @p_TensouMode <> 2 THEN KSUTI.UtiwakeName               ELSE KRUTI.UtiwakeName                END
        , CASE WHEN @p_TensouMode <> 2 THEN KSUTI.UtiwakeNameRyaku          ELSE KRUTI.UtiwakeNameRyaku           END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KasiTorihikisakiCD          ELSE HON.KariTorihikisakiCD           END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KasiTorihikisakiKaiteiDate  ELSE HON.KariTorihikisakiKaiteiDate   END
        , CASE WHEN @p_TensouMode <> 2 THEN KSTRS.TorihikisakiShanaiName    ELSE KRTRS.TorihikisakiShanaiName     END
        , CASE WHEN @p_TensouMode <> 2 THEN KSTRS.LeaseKaishaKBN            ELSE KRTRS.LeaseKaishaKBN             END
        , CASE WHEN @p_TensouMode <> 2 THEN KSTRS.ZeimushoKBN               ELSE KRTRS.ZeimushoKBN                END
        , CASE WHEN @p_TensouMode <> 2 THEN KSTRS.HasseiSiiresakiKBN        ELSE KRTRS.HasseiSiiresakiKBN         END
        , CASE WHEN @p_TensouMode <> 2 THEN KSTRS.HasseiSiharaisakiKBN      ELSE KRTRS.HasseiSiharaisakiKBN       END
        , CASE WHEN @p_TensouMode <> 2 THEN KSTRS.HasseiTokuisakiKBN        ELSE KRTRS.HasseiTokuisakiKBN         END
        , CASE WHEN @p_TensouMode <> 2 THEN KSTRS.HasseiKaishuusakiKBN      ELSE KRTRS.HasseiKaishuusakiKBN       END
        , CASE WHEN @p_TensouMode <> 2 THEN KSTRS.HasseiTorihikisakiKBN1    ELSE KRTRS.HasseiTorihikisakiKBN1     END
        , CASE WHEN @p_TensouMode <> 2 THEN KSTRS.HasseiTorihikisakiKBN2    ELSE KRTRS.HasseiTorihikisakiKBN2     END
        , CASE WHEN @p_TensouMode <> 2 THEN KSTRS.HasseiTorihikisakiKBN3    ELSE KRTRS.HasseiTorihikisakiKBN3     END
        , CASE WHEN @p_TensouMode <> 2 THEN KSTRS.HasseiTorihikisakiKBN4    ELSE KRTRS.HasseiTorihikisakiKBN4     END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KasiTaishakuKBN             ELSE HON.KariTaishakuKBN              END
        , CASE WHEN @p_TensouMode =  0 THEN HON.KasiKingaku 
               WHEN @p_TensouMode =  1 THEN HON.KasiKingaku * (-1)
               ELSE HON.KariKingaku
          END
        , CASE WHEN @p_TensouMode =  0 THEN HON.KasiZeigaku 
               WHEN @p_TensouMode =  1 THEN HON.KasiZeigaku * (-1)
               ELSE HON.KariZeigaku
          END        
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KasiZeiKamokuCD             ELSE HON.KariZeiKamokuCD              END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KasiZeiKaiteiDate           ELSE HON.KariZeiKaiteiDate            END
        , CASE WHEN @p_TensouMode <> 2 THEN KSSKAN.TaishakuKBN              ELSE KRSKAN.TaishakuKBN               END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KasiZeiHontaiCD             ELSE HON.KariZeiHontaiCD              END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KasiZeiHontaiKaiteiDate     ELSE HON.KariZeiHontaiKaiteiDate      END
        , CASE WHEN @p_TensouMode <> 2 THEN KSHKAN.KanjouName               ELSE KRHKAN.KanjouName                END
        , CASE WHEN @p_TensouMode <> 2 THEN KSHKAN.KanjouNameRyaku          ELSE KRHKAN.KanjouNameRyaku           END
        , CASE WHEN @p_TensouMode <> 2 THEN KSHKAN.KariZeiKBN               ELSE KRHKAN.KariZeiKBN                END
        , CASE WHEN @p_TensouMode <> 2 THEN KSHKAN.KasiZeiKBN               ELSE KRHKAN.KasiZeiKBN                END
        , CASE WHEN @p_TensouMode <> 2 THEN KSHKAN.KamokuCategory           ELSE KRHKAN.KamokuCategory            END
        , CASE WHEN @p_TensouMode <> 2 THEN KSHKAN.KamokuKBN                ELSE KRHKAN.KamokuKBN                 END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KasiZeiKBN                  ELSE HON.KariZeiKBN                   END
        , CASE WHEN @p_TensouMode <> 2 THEN KSZKB.ZeiRate                   ELSE KRZKB.ZeiRate                    END
        , CASE WHEN @p_TensouMode <> 2 THEN KSZKB.HasuuKBN                  ELSE KRZKB.HasuuKBN                   END
        , CASE WHEN @p_TensouMode <> 2 THEN KSZKB.ZeiKeisanKBN              ELSE KRZKB.ZeiKeisanKBN               END
        , CASE WHEN @p_TensouMode <> 2 THEN KSZKB.ZeiKamokuKBN              ELSE KRZKB.ZeiKamokuKBN               END
        , CASE WHEN @p_TensouMode <> 2 THEN KSZKB.ZeiKaisiDate              ELSE KRZKB.ZeiKaisiDate               END
        , CASE WHEN @p_TensouMode <> 2 THEN KSZKB.ZeiHaisiDate              ELSE KRZKB.ZeiHaisiDate               END
--2012/10/05 変更 K.Morita ST (M04851)平成26年消費税改正対応       
        --, CASE WHEN @p_TensouMode <> 2 THEN KSKRZKB.ZeiKamokuKBN            ELSE KRKRZKB.ZeiKamokuKBN             END
        --, CASE WHEN @p_TensouMode <> 2 THEN KSKRZKB.ZeiKaisiDate            ELSE KRKRZKB.ZeiKaisiDate             END
        --, CASE WHEN @p_TensouMode <> 2 THEN KSKRZKB.ZeiHaisiDate            ELSE KRKRZKB.ZeiHaisiDate             END
        --, CASE WHEN @p_TensouMode <> 2 THEN KSKSZKB.ZeiKamokuKBN            ELSE KRKSZKB.ZeiKamokuKBN             END
        --, CASE WHEN @p_TensouMode <> 2 THEN KSKSZKB.ZeiKaisiDate            ELSE KRKSZKB.ZeiKaisiDate             END
        --, CASE WHEN @p_TensouMode <> 2 THEN KSKSZKB.ZeiHaisiDate            ELSE KRKSZKB.ZeiHaisiDate             END
        , CASE WHEN @p_TensouMode <> 2 THEN (CASE WHEN KSHOJ.ShokiSetKariZeiKBN IS NULL THEN KSKRZKB.ZeiKamokuKBN
                                                                                        ELSE HKSKRZKB.ZeiKamokuKBN END)
                                       ELSE (CASE WHEN KRHOJ.ShokiSetKariZeiKBN IS NULL THEN KRKRZKB.ZeiKamokuKBN
                                                                                        ELSE HKRKRZKB.ZeiKamokuKBN END) END
        , CASE WHEN @p_TensouMode <> 2 THEN (CASE WHEN KSHOJ.ShokiSetKariZeiKBN IS NULL THEN KSKRZKB.ZeiKaisiDate
                                                                                        ELSE HKSKRZKB.ZeiKaisiDate END)
                                       ELSE (CASE WHEN KRHOJ.ShokiSetKariZeiKBN IS NULL THEN KRKRZKB.ZeiKaisiDate
                                                                                        ELSE HKRKRZKB.ZeiKaisiDate END) END
        , CASE WHEN @p_TensouMode <> 2 THEN (CASE WHEN KSHOJ.ShokiSetKariZeiKBN IS NULL THEN KSKRZKB.ZeiHaisiDate
                                                                                        ELSE HKSKRZKB.ZeiHaisiDate END)
                                       ELSE (CASE WHEN KRHOJ.ShokiSetKariZeiKBN IS NULL THEN KRKRZKB.ZeiHaisiDate
                                                                                        ELSE HKRKRZKB.ZeiHaisiDate END) END
        , CASE WHEN @p_TensouMode <> 2 THEN (CASE WHEN KSHOJ.ShokiSetKasiZeiKBN IS NULL THEN KSKSZKB.ZeiKamokuKBN
                                                                                        ELSE HKSKSZKB.ZeiKamokuKBN END)
                                       ELSE (CASE WHEN KRHOJ.ShokiSetKasiZeiKBN IS NULL THEN KRKSZKB.ZeiKamokuKBN
                                                                                        ELSE HKRKSZKB.ZeiKamokuKBN END) END
        , CASE WHEN @p_TensouMode <> 2 THEN (CASE WHEN KSHOJ.ShokiSetKasiZeiKBN IS NULL THEN KSKSZKB.ZeiKaisiDate
                                                                                        ELSE HKSKSZKB.ZeiKaisiDate END)
                                       ELSE (CASE WHEN KRHOJ.ShokiSetKasiZeiKBN IS NULL THEN KRKSZKB.ZeiKaisiDate
                                                                                        ELSE HKRKSZKB.ZeiKaisiDate END) END
        , CASE WHEN @p_TensouMode <> 2 THEN (CASE WHEN KSHOJ.ShokiSetKasiZeiKBN IS NULL THEN KSKSZKB.ZeiHaisiDate
                                                                                        ELSE HKSKSZKB.ZeiHaisiDate END)
                                       ELSE (CASE WHEN KRHOJ.ShokiSetKasiZeiKBN IS NULL THEN KRKSZKB.ZeiHaisiDate
                                                                                        ELSE HKRKSZKB.ZeiHaisiDate END) END
--2012/10/05 変更 K.Morita ED (M04851)平成26年消費税改正対応 
        , CASE WHEN @p_TensouMode <> 2 THEN KSKRHZKB.ZeiKamokuKBN           ELSE KRKRHZKB.ZeiKamokuKBN            END
        , CASE WHEN @p_TensouMode <> 2 THEN KSKRHZKB.ZeiKaisiDate           ELSE KRKRHZKB.ZeiKaisiDate            END
        , CASE WHEN @p_TensouMode <> 2 THEN KSKRHZKB.ZeiHaisiDate           ELSE KRKRHZKB.ZeiHaisiDate            END
        , CASE WHEN @p_TensouMode <> 2 THEN KSKSHZKB.ZeiKamokuKBN           ELSE KRKSHZKB.ZeiKamokuKBN            END
        , CASE WHEN @p_TensouMode <> 2 THEN KSKSHZKB.ZeiKaisiDate           ELSE KRKSHZKB.ZeiKaisiDate            END
        , CASE WHEN @p_TensouMode <> 2 THEN KSKSHZKB.ZeiHaisiDate           ELSE KRKSHZKB.ZeiHaisiDate            END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KasiZeikomiKBN              ELSE HON.KariZeikomiKBN               END
        , CASE WHEN @p_TensouMode <> 2 THEN KSCODE.CodeName                 ELSE KRCODE.CodeName                  END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KasiTorihikisakiKBN         ELSE HON.KariTorihikisakiKBN          END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KasiHojoKBN                 ELSE HON.KariHojoKBN                  END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KasiUtiwakeKBN              ELSE HON.KariUtiwakeKBN               END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KasiBumonKBN                ELSE HON.KariBumonKBN                 END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KasiBunsekiCD1              ELSE HON.KariBunsekiCD1               END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KasiBunsekiCD2              ELSE HON.KariBunsekiCD2               END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KasiBunsekiCD3              ELSE HON.KariBunsekiCD3               END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KasiBunsekiCD4              ELSE HON.KariBunsekiCD4               END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KasiBunsekiCD5              ELSE HON.KariBunsekiCD5               END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KasiSikinCD                 ELSE HON.KariSikinCD                  END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KasiProjectCD               ELSE HON.KariProjectCD                END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KasiBunsekiCustomizeCD1     ELSE HON.KariBunsekiCustomizeCD1      END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KasiBunsekiCustomizeCD2     ELSE HON.KariBunsekiCustomizeCD2      END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KasiBunsekiCustomizeCD3     ELSE HON.KariBunsekiCustomizeCD3      END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KasiBunsekiCustomizeCD4     ELSE HON.KariBunsekiCustomizeCD4      END
        , CASE WHEN @p_TensouMode <> 2 THEN HON.KasiBunsekiCustomizeCD5     ELSE HON.KariBunsekiCustomizeCD5      END
        , CASE WHEN @p_TensouMode <> 2 THEN KSBUN1.BunsekiName              ELSE KRBUN1.BunsekiName               END
        , CASE WHEN @p_TensouMode <> 2 THEN KSBUN2.BunsekiName              ELSE KRBUN2.BunsekiName               END
        , CASE WHEN @p_TensouMode <> 2 THEN KSBUN3.BunsekiName              ELSE KRBUN3.BunsekiName               END
        , CASE WHEN @p_TensouMode <> 2 THEN KSBUN4.BunsekiName              ELSE KRBUN4.BunsekiName               END
        , CASE WHEN @p_TensouMode <> 2 THEN KSBUN5.BunsekiName              ELSE KRBUN5.BunsekiName               END
        , CASE WHEN @p_TensouMode <> 2 THEN KSBUN6.BunsekiName              ELSE KRBUN6.BunsekiName               END
        , CASE WHEN @p_TensouMode <> 2 THEN KSBUN7.BunsekiName              ELSE KRBUN7.BunsekiName               END
        , CASE WHEN @p_TensouMode <> 2 THEN KSKAN.KariZeiKBN                ELSE KRKAN.KariZeiKBN                 END
        , CASE WHEN @p_TensouMode <> 2 THEN KSKAN.KasiZeiKBN                ELSE KRKAN.KasiZeiKBN                 END
        , CASE WHEN @p_TensouMode <> 2 THEN KSKAN.KamokuCategory            ELSE KRKAN.KamokuCategory             END
        , CASE WHEN @p_TensouMode <> 2 THEN KSKAN.KamokuKBN                 ELSE KRKAN.KamokuKBN                  END
        , CASE WHEN @p_TensouMode <> 2 THEN KSKAN.BunsekiCheckKBN1          ELSE KRKAN.BunsekiCheckKBN1           END
        , CASE WHEN @p_TensouMode <> 2 THEN KSKAN.BunsekiCheckKBN2          ELSE KRKAN.BunsekiCheckKBN2           END
        , CASE WHEN @p_TensouMode <> 2 THEN KSKAN.BunsekiCheckKBN3          ELSE KRKAN.BunsekiCheckKBN3           END
        , CASE WHEN @p_TensouMode <> 2 THEN KSKAN.BunsekiCheckKBN4          ELSE KRKAN.BunsekiCheckKBN4           END
        , CASE WHEN @p_TensouMode <> 2 THEN KSKAN.BunsekiCheckKBN5          ELSE KRKAN.BunsekiCheckKBN5           END
        , CASE WHEN @p_TensouMode <> 2 THEN KSKAN.SikinCheckKBN             ELSE KRKAN.SikinCheckKBN              END
        , CASE WHEN @p_TensouMode <> 2 THEN KSKAN.ProjectCheckKBN           ELSE KRKAN.ProjectCheckKBN            END
        , CASE WHEN @p_TensouMode <> 2 THEN KSKAN.HasseiLeaseKaishaKBN      ELSE KRKAN.HasseiLeaseKaishaKBN       END
        , CASE WHEN @p_TensouMode <> 2 THEN KSKAN.HasseiZeimushoKBN         ELSE KRKAN.HasseiZeimushoKBN          END
        , CASE WHEN @p_TensouMode <> 2 THEN KSKAN.HasseiSiiresakiKBN        ELSE KRKAN.HasseiSiiresakiKBN         END
        , CASE WHEN @p_TensouMode <> 2 THEN KSKAN.HasseiSiharaisakiKBN      ELSE KRKAN.HasseiSiharaisakiKBN       END
        , CASE WHEN @p_TensouMode <> 2 THEN KSKAN.HasseiTokuisakiKBN        ELSE KRKAN.HasseiTokuisakiKBN         END
        , CASE WHEN @p_TensouMode <> 2 THEN KSKAN.HasseiKaishuusakiKBN      ELSE KRKAN.HasseiKaishuusakiKBN       END
        , CASE WHEN @p_TensouMode <> 2 THEN KSKAN.HasseiTorihikisakiKBN1    ELSE KRKAN.HasseiTorihikisakiKBN1     END
        , CASE WHEN @p_TensouMode <> 2 THEN KSKAN.HasseiTorihikisakiKBN2    ELSE KRKAN.HasseiTorihikisakiKBN2     END
        , CASE WHEN @p_TensouMode <> 2 THEN KSKAN.HasseiTorihikisakiKBN3    ELSE KRKAN.HasseiTorihikisakiKBN3     END
        , CASE WHEN @p_TensouMode <> 2 THEN KSKAN.HasseiTorihikisakiKBN4    ELSE KRKAN.HasseiTorihikisakiKBN4     END
--2012/09/14 修正 M.Shinbo 開始 (M04835)
----2010/12/13 Y.Tamashima(M04451)ダブルスタンダード対応 使用科目制御対応 Start
----        , 0
--        , CASE WHEN @p_TensouMode <> 2 THEN 
--                    CASE WHEN HON.KasiKanjouCD IS NOT NULL THEN 
--							 (CASE WHEN KSKAN.KanjouCD IS NULL THEN 1 
--								   ELSE 0 END
--							 ) 
--                    ELSE 0 END 
--               ELSE
--                    CASE WHEN HON.KariKanjouCD IS NOT NULL THEN 
--							 (CASE WHEN KRKAN.KanjouCD IS NULL THEN 1 
--								   ELSE 0 END
--							 ) 
--                    ELSE 0 END 
--          END
----2010/12/13 Y.Tamashima(M04451)ダブルスタンダード対応 使用科目制御対応 End
        , 0
--2012/09/14 修正 M.Shinbo 終了 (M04835)
        , 0
        , 0
        , 0
        , CASE WHEN @p_TensouMode <> 2 THEN CASE WHEN HON.KasiBumonKBN <> 0 AND HON.KasiZigyoushoCD IS NULL AND HDKSZBP.BumonCD IS NULL THEN 2
                                                 WHEN HON.KasiBumonKBN <> 0 AND HON.KasiZigyoushoCD IS NOT NULL AND KSZBP.BumonCD IS NULL THEN 2 ELSE 0 END     
                                       ELSE CASE WHEN HON.KariBumonKBN <> 0 AND HON.KariZigyoushoCD IS NULL AND HDKRZBP.BumonCD IS NULL THEN 2
                                                 WHEN HON.KariBumonKBN <> 0 AND HON.KariZigyoushoCD IS NOT NULL AND KRZBP.BumonCD IS NULL THEN 2 ELSE 0 END     
          END
        , 0
        , 0
        , 0
--2012/09/14 修正 M.Shinbo 開始 (M04835)
----2010/12/13 Y.Tamashima(M04451)ダブルスタンダード対応 使用科目制御対応 Start
----        , 0
--        , CASE WHEN @p_TensouMode <> 2 THEN 
--                    CASE WHEN HON.KasiZeiHontaiCD IS NOT NULL THEN 
--							 (CASE WHEN KSHKAN.KanjouCD IS NULL THEN 1 
--								   ELSE 0 END
--							 ) 
--                    ELSE 0 END 
--               ELSE
--                    CASE WHEN HON.KariZeiHontaiCD IS NOT NULL THEN 
--							 (CASE WHEN KRHKAN.KanjouCD IS NULL THEN 1 
--								   ELSE 0 END
--							 ) 
--                    ELSE 0 END 
--          END
----2010/12/13 Y.Tamashima(M04451)ダブルスタンダード対応 使用科目制御対応 End
        , 0
--2012/09/14 修正 M.Shinbo 終了 (M04835)
        , CASE WHEN @p_TensouMode <> 2 THEN CASE WHEN HON.KasiBunsekiCD1 IS NOT NULL AND KSBUN1.BunsekiCD IS NULL THEN 1 ELSE 0 END
                                       ELSE CASE WHEN HON.KariBunsekiCD1 IS NOT NULL AND KRBUN1.BunsekiCD IS NULL THEN 1 ELSE 0 END
          END
        , CASE WHEN @p_TensouMode <> 2 THEN CASE WHEN HON.KasiBunsekiCD2 IS NOT NULL AND KSBUN2.BunsekiCD IS NULL THEN 1 ELSE 0 END
                                       ELSE CASE WHEN HON.KariBunsekiCD2 IS NOT NULL AND KRBUN2.BunsekiCD IS NULL THEN 1 ELSE 0 END
          END
        , CASE WHEN @p_TensouMode <> 2 THEN CASE WHEN HON.KasiBunsekiCD3 IS NOT NULL AND KSBUN3.BunsekiCD IS NULL THEN 1 ELSE 0 END
                                       ELSE CASE WHEN HON.KariBunsekiCD3 IS NOT NULL AND KRBUN3.BunsekiCD IS NULL THEN 1 ELSE 0 END
          END
        , CASE WHEN @p_TensouMode <> 2 THEN CASE WHEN HON.KasiBunsekiCD4 IS NOT NULL AND KSBUN4.BunsekiCD IS NULL THEN 1 ELSE 0 END
                                       ELSE CASE WHEN HON.KariBunsekiCD4 IS NOT NULL AND KRBUN4.BunsekiCD IS NULL THEN 1 ELSE 0 END
          END
        , CASE WHEN @p_TensouMode <> 2 THEN CASE WHEN HON.KasiBunsekiCD5 IS NOT NULL AND KSBUN5.BunsekiCD IS NULL THEN 1 ELSE 0 END
                                       ELSE CASE WHEN HON.KariBunsekiCD5 IS NOT NULL AND KRBUN5.BunsekiCD IS NULL THEN 1 ELSE 0 END
          END
        , CASE WHEN @p_TensouMode <> 2 THEN CASE WHEN HON.KasiSikinCD    IS NOT NULL AND KSBUN6.BunsekiCD IS NULL THEN 1 ELSE 0 END
                                       ELSE CASE WHEN HON.KariSikinCD    IS NOT NULL AND KRBUN6.BunsekiCD IS NULL THEN 1 ELSE 0 END
          END
        , CASE WHEN @p_TensouMode <> 2 THEN CASE WHEN HON.KasiProjectCD  IS NOT NULL AND KSBUN7.BunsekiCD IS NULL THEN 1 ELSE 0 END
                                       ELSE CASE WHEN HON.KariProjectCD  IS NOT NULL AND KRBUN7.BunsekiCD IS NULL THEN 1 ELSE 0 END
          END
        , HON.MeisaiTekiyou
        , HON.GyouNO
        , @p_CompanyNO
        , @p_UpdateDatetime
        , @p_UpdateCertificatedGuid
        , @p_UpdateJobID
        , @p_UpdateUserID
        , @p_UpdateComputerName
        , @p_UpdateComputerUserName
        , @p_UpdateComputerDomainName
    FROM M0000RR_MA_HonsitenSiwakeMeisai HON
--2012/09/14 修正 M.Shinbo 開始 (M04835)
----2010/12/13 Y.Tamashima(M04451)ダブルスタンダード対応 使用科目制御対応 Start
----    LEFT JOIN M0000MS_MA_Kanjou KRKAN
--    LEFT JOIN #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Kanjou KRKAN
----2010/12/13 Y.Tamashima(M04451)ダブルスタンダード対応 使用科目制御対応 End
    LEFT JOIN M0000MS_MA_Kanjou KRKAN
--2012/09/14 修正 M.Shinbo 終了 (M04835)
    ON  KRKAN.KanjouCD = HON.KariKanjouCD
    AND KRKAN.KaiteiDate = HON.KariKanjouKaiteiDate
    LEFT JOIN M0000MS_MA_Hojo KRHOJ
    ON  KRHOJ.KanjouCD = HON.KariKanjouCD
    AND KRHOJ.HojoCD = HON.KariHojoCD
    AND KRHOJ.KaiteiDate = HON.KariHojoKaiteiDate
    LEFT JOIN M0000MS_MA_Utiwake KRUTI
    ON  KRUTI.KanjouCD = HON.KariKanjouCD
    AND KRUTI.HojoCD = HON.KariHojoCD
    AND KRUTI.UtiwakeCD = HON.KariUtiwakeCD
    AND KRUTI.KaiteiDate = HON.KariUtiwakeKaiteiDate
    LEFT JOIN M0000MS_MA_Bumon KRBMN
    ON  KRBMN.BumonCD = HON.KariBumonCD
    AND KRBMN.KaiteiDate = HON.KariBumonKaiteiDate
    LEFT JOIN P0000MS_Torihikisaki KRTRS
    ON  KRTRS.TorihikisakiCD = HON.KariTorihikisakiCD
    AND KRTRS.TorihikisakiKaiteiDate = HON.KariTorihikisakiKaiteiDate
    LEFT JOIN M0000MS_MA_ZeiKBN KRZKB
    ON  KRZKB.ZeiKBN = HON.KariZeiKBN
--2012/09/14 修正 M.Shinbo 開始 (M04835)
----2010/12/13 Y.Tamashima(M04451)ダブルスタンダード対応 使用科目制御対応 Start
----    LEFT JOIN M0000MS_MA_Kanjou KRSKAN
--    LEFT JOIN #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Kanjou KRSKAN
----2010/12/13 Y.Tamashima(M04451)ダブルスタンダード対応 使用科目制御対応 End
    LEFT JOIN M0000MS_MA_Kanjou KRSKAN
--2012/09/14 修正 M.Shinbo 終了 (M04835)
    ON  KRSKAN.KanjouCD = HON.KariZeiKamokuCD
    AND KRSKAN.KaiteiDate = HON.KariZeiKaiteiDate
--2012/09/14 修正 M.Shinbo 開始 (M04835)
----2010/12/13 Y.Tamashima(M04451)ダブルスタンダード対応 使用科目制御対応 Start
----    LEFT JOIN M0000MS_MA_Kanjou KRHKAN
--    LEFT JOIN #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Kanjou KRHKAN
----2010/12/13 Y.Tamashima(M04451)ダブルスタンダード対応 使用科目制御対応 End
    LEFT JOIN M0000MS_MA_Kanjou KRHKAN
--2012/09/14 修正 M.Shinbo 終了 (M04835)
    ON  KRHKAN.KanjouCD = HON.KariZeiHontaiCD
    AND KRHKAN.KaiteiDate = HON.KariZeiHontaiKaiteiDate
    LEFT JOIN M0000MS_MA_ZeiKBN KRKRZKB
    ON  KRKRZKB.ZeiKBN = KRKAN.KariZeiKBN   
    LEFT JOIN M0000MS_MA_ZeiKBN KRKSZKB
    ON  KRKSZKB.ZeiKBN = KRKAN.KasiZeiKBN
--2012/10/05 追加 K.Morita ST (M04851)平成26年消費税改正対応
    LEFT JOIN M0000MS_MA_ZeiKBN HKRKRZKB
    ON  HKRKRZKB.ZeiKBN = KRHOJ.ShokiSetKariZeiKBN
    LEFT JOIN M0000MS_MA_ZeiKBN HKRKSZKB
    ON  HKRKSZKB.ZeiKBN = KRHOJ.ShokiSetKasiZeiKBN
--2012/10/05 追加 K.Morita ED (M04851)平成26年消費税改正対応
    LEFT JOIN M0000MS_MA_ZeiKBN KRKRHZKB
    ON  KRKRHZKB.ZeiKBN = KRHKAN.KariZeiKBN
    LEFT JOIN M0000MS_MA_ZeiKBN KRKSHZKB
    ON  KRKSHZKB.ZeiKBN = KRHKAN.KasiZeiKBN
    LEFT JOIN M0000MS_MA_BunsekiCD KRBUN1
    ON  KRBUN1.BunsekiCDKBN = 1
    AND KRBUN1.BunsekiCD = HON.KariBunsekiCD1
    LEFT JOIN M0000MS_MA_BunsekiCD KRBUN2
    ON  KRBUN2.BunsekiCDKBN = 2
    AND KRBUN2.BunsekiCD = HON.KariBunsekiCD2
    LEFT JOIN M0000MS_MA_BunsekiCD KRBUN3
    ON  KRBUN3.BunsekiCDKBN = 3
    AND KRBUN3.BunsekiCD = HON.KariBunsekiCD3
    LEFT JOIN M0000MS_MA_BunsekiCD KRBUN4
    ON  KRBUN4.BunsekiCDKBN = 4
    AND KRBUN4.BunsekiCD = HON.KariBunsekiCD4
    LEFT JOIN M0000MS_MA_BunsekiCD KRBUN5
    ON  KRBUN5.BunsekiCDKBN = 5
    AND KRBUN5.BunsekiCD = HON.KariBunsekiCD5
    LEFT JOIN M0000MS_MA_BunsekiCD KRBUN6
    ON  KRBUN6.BunsekiCDKBN = 6
    AND KRBUN6.BunsekiCD = HON.KariSikinCD
    LEFT JOIN M0000MS_MA_BunsekiCD KRBUN7
    ON  KRBUN7.BunsekiCDKBN = 7
    AND KRBUN7.BunsekiCD = HON.KariProjectCD
    LEFT JOIN M0000MS_CommonCode KRCODE
    ON  KRCODE.Shubetu = 400
    AND KRCODE.CodeValue = HON.KariZeikomiKBN
--2012/09/14 修正 M.Shinbo 開始 (M04835)
----2010/12/13 Y.Tamashima(M04451)ダブルスタンダード対応 使用科目制御対応 Start
----    LEFT JOIN M0000MS_MA_Kanjou KSKAN
--    LEFT JOIN #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Kanjou KSKAN
----2010/12/13 Y.Tamashima(M04451)ダブルスタンダード対応 使用科目制御対応 End
    LEFT JOIN M0000MS_MA_Kanjou KSKAN
--2012/09/14 修正 M.Shinbo 終了 (M04835)
    ON  KSKAN.KanjouCD = HON.KasiKanjouCD
    AND KSKAN.KaiteiDate = HON.KasiKanjouKaiteiDate
    LEFT JOIN M0000MS_MA_Hojo KSHOJ
    ON  KSHOJ.KanjouCD = HON.KasiKanjouCD
    AND KSHOJ.HojoCD = HON.KasiHojoCD
    AND KSHOJ.KaiteiDate = HON.KasiHojoKaiteiDate
    LEFT JOIN M0000MS_MA_Utiwake KSUTI
    ON  KSUTI.KanjouCD = HON.KasiKanjouCD
    AND KSUTI.HojoCD = HON.KasiHojoCD
    AND KSUTI.UtiwakeCD = HON.KasiUtiwakeCD
    AND KSUTI.KaiteiDate = HON.KasiUtiwakeKaiteiDate
    LEFT JOIN M0000MS_MA_Bumon KSBMN
    ON  KSBMN.BumonCD = HON.KasiBumonCD
    AND KSBMN.KaiteiDate = HON.KasiBumonKaiteiDate
    LEFT JOIN P0000MS_Torihikisaki KSTRS
    ON  KSTRS.TorihikisakiCD = HON.KasiTorihikisakiCD
    AND KSTRS.TorihikisakiKaiteiDate = HON.KasiTorihikisakiKaiteiDate
    LEFT JOIN M0000MS_MA_ZeiKBN KSZKB
    ON  KSZKB.ZeiKBN = HON.KasiZeiKBN
--2012/09/14 修正 M.Shinbo 開始 (M04835)
----2010/12/13 Y.Tamashima(M04451)ダブルスタンダード対応 使用科目制御対応 Start
----    LEFT JOIN M0000MS_MA_Kanjou KSSKAN
--    LEFT JOIN #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Kanjou KSSKAN
----2010/12/13 Y.Tamashima(M04451)ダブルスタンダード対応 使用科目制御対応 End
    LEFT JOIN M0000MS_MA_Kanjou KSSKAN
--2012/09/14 修正 M.Shinbo 終了 (M04835)
    ON  KSSKAN.KanjouCD = HON.KasiZeiKamokuCD
    AND KSSKAN.KaiteiDate = HON.KasiZeiKaiteiDate
--2012/09/14 修正 M.Shinbo 開始 (M04835)
----2010/12/13 Y.Tamashima(M04451)ダブルスタンダード対応 使用科目制御対応 Start
----    LEFT JOIN M0000MS_MA_Kanjou KSHKAN
--    LEFT JOIN #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Kanjou KSHKAN
----2010/12/13 Y.Tamashima(M04451)ダブルスタンダード対応 使用科目制御対応 End
    LEFT JOIN M0000MS_MA_Kanjou KSHKAN
--2012/09/14 修正 M.Shinbo 終了 (M04835)
    ON  KSHKAN.KanjouCD = HON.KasiZeiHontaiCD
    AND KSHKAN.KaiteiDate = HON.KasiZeiHontaiKaiteiDate
    LEFT JOIN M0000MS_MA_ZeiKBN KSKRZKB
    ON  KSKRZKB.ZeiKBN = KSKAN.KariZeiKBN
    LEFT JOIN M0000MS_MA_ZeiKBN KSKSZKB
    ON  KSKSZKB.ZeiKBN = KSKAN.KasiZeiKBN
--2012/10/05 追加 K.Morita ST (M04851)平成26年消費税改正対応
    LEFT JOIN M0000MS_MA_ZeiKBN HKSKRZKB
    ON  HKSKRZKB.ZeiKBN = KSHOJ.ShokiSetKariZeiKBN
    LEFT JOIN M0000MS_MA_ZeiKBN HKSKSZKB
    ON  HKSKSZKB.ZeiKBN = KSHOJ.ShokiSetKasiZeiKBN
--2012/10/05 追加 K.Morita ED (M04851)平成26年消費税改正対応
    LEFT JOIN M0000MS_MA_ZeiKBN KSKRHZKB
    ON  KSKRHZKB.ZeiKBN = KSHKAN.KariZeiKBN
    LEFT JOIN M0000MS_MA_ZeiKBN KSKSHZKB
    ON  KSKSHZKB.ZeiKBN = KSHKAN.KasiZeiKBN
    LEFT JOIN M0000MS_MA_BunsekiCD KSBUN1
    ON  KSBUN1.BunsekiCDKBN = 1
    AND KSBUN1.BunsekiCD = HON.KasiBunsekiCD1
    LEFT JOIN M0000MS_MA_BunsekiCD KSBUN2
    ON  KSBUN2.BunsekiCDKBN = 2
    AND KSBUN2.BunsekiCD = HON.KasiBunsekiCD2
    LEFT JOIN M0000MS_MA_BunsekiCD KSBUN3
    ON  KSBUN3.BunsekiCDKBN = 3
    AND KSBUN3.BunsekiCD = HON.KasiBunsekiCD3
    LEFT JOIN M0000MS_MA_BunsekiCD KSBUN4
    ON  KSBUN4.BunsekiCDKBN = 4
    AND KSBUN4.BunsekiCD = HON.KasiBunsekiCD4
    LEFT JOIN M0000MS_MA_BunsekiCD KSBUN5
    ON  KSBUN5.BunsekiCDKBN = 5
    AND KSBUN5.BunsekiCD = HON.KasiBunsekiCD5
    LEFT JOIN M0000MS_MA_BunsekiCD KSBUN6
    ON  KSBUN6.BunsekiCDKBN = 6
    AND KSBUN6.BunsekiCD = HON.KasiSikinCD
    LEFT JOIN M0000MS_MA_BunsekiCD KSBUN7
    ON  KSBUN7.BunsekiCDKBN = 7
    AND KSBUN7.BunsekiCD = HON.KasiProjectCD
    LEFT JOIN M0000MS_CommonCode KSCODE
    ON  KSCODE.Shubetu = 400
    AND KSCODE.CodeValue = HON.KasiZeikomiKBN
    --事業所部門直近(ヘッダ事業所)
    LEFT JOIN #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_ZigyoushoBumonPattern HDKRZBP
    ON  HDKRZBP.ZigyoushoCD = HON.ZigyoushoCD
    AND HDKRZBP.BumonCD     = HON.KariBumonCD
    --事業所部門直近(ヘッダ事業所)
    LEFT JOIN #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_ZigyoushoBumonPattern HDKSZBP
    ON  HDKSZBP.ZigyoushoCD = HON.ZigyoushoCD
    AND HDKSZBP.BumonCD     = HON.KasiBumonCD
    --事業所部門直近(借方事業所)
    LEFT JOIN #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_ZigyoushoBumonPattern KRZBP
    ON  KRZBP.ZigyoushoCD = HON.KariZigyoushoCD
    AND KRZBP.BumonCD     = HON.KariBumonCD
    --事業所部門直近(貸方事業所)
    LEFT JOIN #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_ZigyoushoBumonPattern KSZBP
    ON  KSZBP.ZigyoushoCD = HON.KasiZigyoushoCD
    AND KSZBP.BumonCD     = HON.KasiBumonCD
    --事業所マスタ
    LEFT JOIN M0000MS_MA_Zigyousho KRZIG
    ON  KRZIG.ZigyoushoCD = HON.KariZigyoushoCD
    AND KRZIG.KaiteiDate  = HON.KariZigyoushoKaiteiDate
    --本支店勘定マスタ
    LEFT JOIN #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_HonsitenKanjou KRHSK
    ON  KRHSK.ZigyoushoCD = HON.KariZigyoushoCD
    --事業所マスタ(本社事業所)
    LEFT JOIN #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Zigyousho KRHZI
    ON  KRHZI.ZigyoushoCD = KRHSK.HonshaZigyoushoCD
    --事業所マスタ
    LEFT JOIN M0000MS_MA_Zigyousho KSZIG
    ON  KSZIG.ZigyoushoCD = HON.KasiZigyoushoCD
    AND KSZIG.KaiteiDate  = HON.KasiZigyoushoKaiteiDate
    --本支店勘定マスタ
    LEFT JOIN #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_HonsitenKanjou KSHSK
    ON  KSHSK.ZigyoushoCD = HON.KasiZigyoushoCD
    --事業所マスタ(本社事業所)
    LEFT JOIN #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Zigyousho KSHZI
    ON  KSHZI.ZigyoushoCD = KSHSK.HonshaZigyoushoCD
    WHERE HON.SystemDenpyouNO = @p_SystemDenpyouNO

    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR

    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END

    --↓2013/09/04 追加 M.Shinbo GX対応(M00020:メモ機能の実装) 開始
    -- ワーク削除
    DELETE FROM M0000WK_MA_HonsitenMemo
    WHERE OperationComputerName     = @p_UpdateComputerName
    AND   OperationUserID           = @p_UpdateUserID
    AND   OperationJobID            = @p_UpdateJobID

    --引数.メモ使用フラグ が 1(使用する) の場合
    IF @p_MemoSiyouFlag = 1
    BEGIN
        INSERT INTO M0000WK_MA_HonsitenMemo
        (
             OperationComputerName
            ,OperationUserID
            ,OperationDatetime
            ,OperationJobID
            ,GyouNO
            ,KariRValue
            ,KariGValue
            ,KariBValue
            ,KariMemo
            ,KasiRValue
            ,KasiGValue
            ,KasiBValue
            ,KasiMemo
            ,CompanyNO
            ,UpdateDatetime
            ,UpdateCertificatedGuid
            ,UpdateJobID
            ,UpdateUserID
            ,UpdateComputerName
            ,UpdateComputerUserName
            ,UpdateComputerDomainName
        )
        SELECT 
            @p_UpdateComputerName
          , @p_UpdateUserID
          , @p_UpdateDatetime
          , @p_UpdateJobID
          , MEMO.GyouNO
          , CASE WHEN @p_TensouMode <> 2 THEN MEMO.KariRValue ELSE MEMO.KasiRValue END
          , CASE WHEN @p_TensouMode <> 2 THEN MEMO.KariGValue ELSE MEMO.KasiGValue END
          , CASE WHEN @p_TensouMode <> 2 THEN MEMO.KariBValue ELSE MEMO.KasiBValue END
          , CASE WHEN @p_TensouMode <> 2
                 THEN CASE WHEN MEMO.KariMemo = '' THEN NULL ELSE MEMO.KariMemo END
                 ELSE MEMO.KasiMemo   END
          , CASE WHEN @p_TensouMode <> 2 THEN MEMO.KasiRValue ELSE MEMO.KariRValue END
          , CASE WHEN @p_TensouMode <> 2 THEN MEMO.KasiGValue ELSE MEMO.KariGValue END
          , CASE WHEN @p_TensouMode <> 2 THEN MEMO.KasiBValue ELSE MEMO.KariBValue END
          , CASE WHEN @p_TensouMode <> 2
                 THEN CASE WHEN MEMO.KasiMemo = '' THEN NULL ELSE MEMO.KasiMemo END
                 ELSE MEMO.KariMemo   END
          , @p_CompanyNO
          , @p_UpdateDatetime
          , @p_UpdateCertificatedGuid
          , @p_UpdateJobID
          , @p_UpdateUserID
          , @p_UpdateComputerName
          , @p_UpdateComputerUserName
          , @p_UpdateComputerDomainName
        FROM  M0000RR_MA_HonsitenMemo MEMO
        WHERE MEMO.SystemDenpyouNO  = @p_SystemDenpyouNO

       
        -- @@ERROR を取得し、変数に保存する。
        SET @ErrorCode = @@ERROR

        -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
        IF  @ErrorCode <> 0
        BEGIN
            GOTO ERR_SHORI
        END
    END
    --↑2013/09/04 追加 M.Shinbo GX対応(M00020:メモ機能の実装) 終了

    -- 画面表示用のデータを返す
    SELECT
          HON.SystemDenpyouNO
        , HON.DenpyouNO
        , HON.ShoriDate
        , HON.HasseiDate
        , HON.DenpyouTypeKBN
        , HON.SystemCategory
        , HON.SubSystemDenpyouNO
        , HON.SubShoriKBN
        , HON.SubDenpyouNO
        , HON.SiwakeKBN
        , HON.DenpyouKBN
        , DEN.DenpyouKBNName
        , DEN.KessanFlag
        -- 2010/08/17 修正 C.Okuno@OBS  (M04451)↓
        , DEN.HonKaridenFlag
        , DEN.KanendoSokyuuShuuseiFlag
        , DEN.SokyuuShoriDate
        , DEN.KijunKBN
        -- 2010/08/17 修正 C.Okuno@OBS  (M04451)↑
        , GTD.Kessanki
        , GTD.MonthNO
        , HON.ZigyoushoCD
        , HON.ZigyoushoKaiteiDate
        , ZIG.ZigyoushoNameRyaku
        , ZIG.SetZandakaDate
        , ZIG.DenpyouSimeDate
        , HSK.HonshaZigyoushoCD
        , HZI.SetZandakaDate AS HonshaZigyoushoSetZandakaDate
        , HZI.DenpyouSimeDate AS HonshaZigyoushoDenpyouSimeDate
        , HON.DenpyouTekiyou
--2008/06/02 修正 N.Sasaki 開始
--        , HON.DenpyouListInziZumiKBN
--        , HON.CheckListInziZumiKBN
--        , HON.GuidAnken
--        , HON.ShouninStateKBN
--        , HON.KihyouUserID
--        , HON.CurrentUserID
--        , HON.ShouninUserID
--        , HON.ShouninDate
        , SWK.DenpyouListInziZumiKBN
        , SWK.CheckListInziZumiKBN
        , SWK.GuidAnken
        , SWK.ShouninStateKBN
        , SWK.KihyouUserID
        , SWK.CurrentUserID
        , SWK.ShouninUserID
        , SWK.ShouninDate
--2008/06/02 修正 N.Sasaki 終了
        , MEI.SiwakeKBN
        , MEI.KariBumonCD
        , MEI.KariBumonKaiteiDate
        , MEI.KariBumonName
        , MEI.KariBumonNameRyaku
        , MEI.KariZigyoushoCD
        , MEI.KariZigyoushoKaiteiDate
        , MEI.KariZigyoushoName
        , MEI.KariZigyoushoNameRyaku
        , MEI.KariZigyoushoSetZandakaDate
        , MEI.KariZigyoushoDenpyouSimeDate
        , MEI.KariHonshaZigyoushoCD
        , MEI.KariHonshaZigyoushoName
        , MEI.KariHonshaZigyoushoSetZandakaDate
        , MEI.KariHonshaZigyoushoDenpyouSimeDate
        , MEI.KariKanjouCD
        , MEI.KariKanjouKaiteiDate
        , MEI.KariKanjouName
        , MEI.KariKanjouNameRyaku
        , MEI.KariHojoCD
        , MEI.KariHojoKaiteiDate
        , MEI.KariHojoName
        , MEI.KariHojoNameRyaku
        , MEI.KariUtiwakeCD
        , MEI.KariUtiwakeKaiteiDate
        , MEI.KariUtiwakeName
        , MEI.KariUtiwakeNameRyaku
        , MEI.KariTorihikisakiCD
        , MEI.KariTorihikisakiKaiteiDate
        , MEI.KariTorihikisakiShanaiName
        , MEI.KariTorihikisakiLeaseKaishaKBN
        , MEI.KariTorihikisakiZeimushoKBN
        , MEI.KariTorihikisakiHasseiSiiresakiKBN
        , MEI.KariTorihikisakiHasseiSiharaisakiKBN
        , MEI.KariTorihikisakiHasseiTokuisakiKBN
        , MEI.KariTorihikisakiHasseiKaishuusakiKBN
        , MEI.KariTorihikisakiHasseiTorihikisakiKBN1
        , MEI.KariTorihikisakiHasseiTorihikisakiKBN2
        , MEI.KariTorihikisakiHasseiTorihikisakiKBN3
        , MEI.KariTorihikisakiHasseiTorihikisakiKBN4
        , MEI.KariTaishakuKBN
        , MEI.KariKingaku
        , MEI.KariZeigaku
        , MEI.KariZeiKamokuCD
        , MEI.KariZeiKaiteiDate
        , MEI.KariZeiTaishakuKBN
        , MEI.KariZeiHontaiCD
        , MEI.KariZeiHontaiKaiteiDate
        , MEI.KariZeiHontaiName
        , MEI.KariZeiHontaiNameRyaku
        , MEI.KariZeiHontaiKariZeiKBN
        , MEI.KariZeiHontaiKasiZeiKBN
        , MEI.KariZeiHontaiKamokuCategory
        , MEI.KariZeiHontaiKamokuKBN
        , MEI.KariZeiKBN
        , MEI.KariZeiRate
        , MEI.KariHasuuKBN
        , MEI.KariZeiKeisanKBN
        , MEI.KariZeiKamokuKBN
        , MEI.KariZeiKBNZeiKaisiDate
        , MEI.KariZeiKBNZeiHaisiDate
        , MEI.KariKanjouKariZeiKamokuKBN
        , MEI.KariKanjouKariZeiKBNZeiKaisiDate
        , MEI.KariKanjouKariZeiKBNZeiHaisiDate
        , MEI.KariKanjouKasiZeiKamokuKBN
        , MEI.KariKanjouKasiZeiKBNZeiKaisiDate
        , MEI.KariKanjouKasiZeiKBNZeiHaisiDate
        , MEI.KariZeiHontaiKariZeiKamokuKBN
        , MEI.KariZeiHontaiKariZeiKBNZeiKaisiDate
        , MEI.KariZeiHontaiKariZeiKBNZeiHaisiDate
        , MEI.KariZeiHontaiKasiZeiKamokuKBN
        , MEI.KariZeiHontaiKasiZeiKBNZeiKaisiDate
        , MEI.KariZeiHontaiKasiZeiKBNZeiHaisiDate
        , MEI.KariZeikomiKBN
        , MEI.KariZeikomiKBNName
        , MEI.KariTorihikisakiKBN
        , MEI.KariHojoKBN
        , MEI.KariUtiwakeKBN
        , MEI.KariBumonKBN
        , MEI.KariBunsekiCD1
        , MEI.KariBunsekiCD2
        , MEI.KariBunsekiCD3
        , MEI.KariBunsekiCD4
        , MEI.KariBunsekiCD5
        , MEI.KariSikinCD
        , MEI.KariProjectCD
        , MEI.KariBunsekiCustomizeCD1
        , MEI.KariBunsekiCustomizeCD2
        , MEI.KariBunsekiCustomizeCD3
        , MEI.KariBunsekiCustomizeCD4
        , MEI.KariBunsekiCustomizeCD5
        , MEI.KariBunsekiName1
        , MEI.KariBunsekiName2
        , MEI.KariBunsekiName3
        , MEI.KariBunsekiName4
        , MEI.KariBunsekiName5
        , MEI.KariSikinName
        , MEI.KariProjectName
        , MEI.KariKanjouKariZeiKBN
        , MEI.KariKanjouKasiZeiKBN
        , MEI.KariKanjouKamokuCategory
        , MEI.KariKanjouKamokuKBN
        , MEI.KariKanjouBunsekiCheckKBN1
        , MEI.KariKanjouBunsekiCheckKBN2
        , MEI.KariKanjouBunsekiCheckKBN3
        , MEI.KariKanjouBunsekiCheckKBN4
        , MEI.KariKanjouBunsekiCheckKBN5
        , MEI.KariKanjouSikinCheckKBN
        , MEI.KariKanjouProjectCheckKBN
        , MEI.KariKanjouHasseiLeaseKaishaKBN
        , MEI.KariKanjouHasseiZeimushoKBN
        , MEI.KariKanjouHasseiSiiresakiKBN
        , MEI.KariKanjouHasseiSiharaisakiKBN
        , MEI.KariKanjouHasseiTokuisakiKBN
        , MEI.KariKanjouHasseiKaishuusakiKBN
        , MEI.KariKanjouHasseiTorihikisakiKBN1
        , MEI.KariKanjouHasseiTorihikisakiKBN2
        , MEI.KariKanjouHasseiTorihikisakiKBN3
        , MEI.KariKanjouHasseiTorihikisakiKBN4
        , MEI.KariKanjouCDErrorFlag
        , MEI.KariHojoCDErrorFlag
        , MEI.KariUtiwakeCDErrorFlag
        , MEI.KariZigyoushoCDErrorFlag
        , MEI.KariBumonCDErrorFlag
        , MEI.KariTorihikisakiCDErrorFlag
        , MEI.KariZeiKBNErrorFlag
        , MEI.KariZeikomiKBNErrorFlag
        , MEI.KariZeiHontaiCDErrorFlag
        , MEI.KariBunsekiCD1ErrorFlag
        , MEI.KariBunsekiCD2ErrorFlag
        , MEI.KariBunsekiCD3ErrorFlag
        , MEI.KariBunsekiCD4ErrorFlag
        , MEI.KariBunsekiCD5ErrorFlag
        , MEI.KariSikinCDErrorFlag
        , MEI.KariProjectCDErrorFlag
        , MEI.KasiBumonCD
        , MEI.KasiBumonKaiteiDate
        , MEI.KasiBumonName
        , MEI.KasiBumonNameRyaku
        , MEI.KasiZigyoushoCD
        , MEI.KasiZigyoushoKaiteiDate
        , MEI.KasiZigyoushoName
        , MEI.KasiZigyoushoNameRyaku
        , MEI.KasiZigyoushoSetZandakaDate
        , MEI.KasiZigyoushoDenpyouSimeDate
        , MEI.KasiHonshaZigyoushoCD
        , MEI.KasiHonshaZigyoushoName
        , MEI.KasiHonshaZigyoushoSetZandakaDate
        , MEI.KasiHonshaZigyoushoDenpyouSimeDate
        , MEI.KasiKanjouCD
        , MEI.KasiKanjouKaiteiDate
        , MEI.KasiKanjouName
        , MEI.KasiKanjouNameRyaku
        , MEI.KasiHojoCD
        , MEI.KasiHojoKaiteiDate
        , MEI.KasiHojoName
        , MEI.KasiHojoNameRyaku
        , MEI.KasiUtiwakeCD
        , MEI.KasiUtiwakeKaiteiDate
        , MEI.KasiUtiwakeName
        , MEI.KasiUtiwakeNameRyaku
        , MEI.KasiTorihikisakiCD
        , MEI.KasiTorihikisakiKaiteiDate
        , MEI.KasiTorihikisakiShanaiName
        , MEI.KasiTorihikisakiLeaseKaishaKBN
        , MEI.KasiTorihikisakiZeimushoKBN
        , MEI.KasiTorihikisakiHasseiSiiresakiKBN
        , MEI.KasiTorihikisakiHasseiSiharaisakiKBN
        , MEI.KasiTorihikisakiHasseiTokuisakiKBN
        , MEI.KasiTorihikisakiHasseiKaishuusakiKBN
        , MEI.KasiTorihikisakiHasseiTorihikisakiKBN1
        , MEI.KasiTorihikisakiHasseiTorihikisakiKBN2
        , MEI.KasiTorihikisakiHasseiTorihikisakiKBN3
        , MEI.KasiTorihikisakiHasseiTorihikisakiKBN4
        , MEI.KasiTaishakuKBN
        , MEI.KasiKingaku
        , MEI.KasiZeigaku
        , MEI.KasiZeiKamokuCD
        , MEI.KasiZeiKaiteiDate
        , MEI.KasiZeiTaishakuKBN
        , MEI.KasiZeiHontaiCD
        , MEI.KasiZeiHontaiKaiteiDate
        , MEI.KasiZeiHontaiName
        , MEI.KasiZeiHontaiNameRyaku
        , MEI.KasiZeiHontaiKariZeiKBN
        , MEI.KasiZeiHontaiKasiZeiKBN
        , MEI.KasiZeiHontaiKamokuCategory
        , MEI.KasiZeiHontaiKamokuKBN
        , MEI.KasiZeiKBN
        , MEI.KasiZeiRate
        , MEI.KasiHasuuKBN
        , MEI.KasiZeiKeisanKBN
        , MEI.KasiZeiKamokuKBN
        , MEI.KasiZeiKBNZeiKaisiDate
        , MEI.KasiZeiKBNZeiHaisiDate
        , MEI.KasiKanjouKariZeiKamokuKBN
        , MEI.KasiKanjouKariZeiKBNZeiKaisiDate
        , MEI.KasiKanjouKariZeiKBNZeiHaisiDate
        , MEI.KasiKanjouKasiZeiKamokuKBN
        , MEI.KasiKanjouKasiZeiKBNZeiKaisiDate
        , MEI.KasiKanjouKasiZeiKBNZeiHaisiDate
        , MEI.KasiZeiHontaiKariZeiKamokuKBN
        , MEI.KasiZeiHontaiKariZeiKBNZeiKaisiDate
        , MEI.KasiZeiHontaiKariZeiKBNZeiHaisiDate
        , MEI.KasiZeiHontaiKasiZeiKamokuKBN
        , MEI.KasiZeiHontaiKasiZeiKBNZeiKaisiDate
        , MEI.KasiZeiHontaiKasiZeiKBNZeiHaisiDate
        , MEI.KasiZeikomiKBN
        , MEI.KasiZeikomiKBNName
        , MEI.KasiTorihikisakiKBN
        , MEI.KasiHojoKBN
        , MEI.KasiUtiwakeKBN
        , MEI.KasiBumonKBN
        , MEI.KasiBunsekiCD1
        , MEI.KasiBunsekiCD2
        , MEI.KasiBunsekiCD3
        , MEI.KasiBunsekiCD4
        , MEI.KasiBunsekiCD5
        , MEI.KasiSikinCD
        , MEI.KasiProjectCD
        , MEI.KasiBunsekiCustomizeCD1
        , MEI.KasiBunsekiCustomizeCD2
        , MEI.KasiBunsekiCustomizeCD3
        , MEI.KasiBunsekiCustomizeCD4
        , MEI.KasiBunsekiCustomizeCD5
        , MEI.KasiBunsekiName1
        , MEI.KasiBunsekiName2
        , MEI.KasiBunsekiName3
        , MEI.KasiBunsekiName4
        , MEI.KasiBunsekiName5
        , MEI.KasiSikinName
        , MEI.KasiProjectName
        , MEI.KasiKanjouKariZeiKBN
        , MEI.KasiKanjouKasiZeiKBN
        , MEI.KasiKanjouKamokuCategory
        , MEI.KasiKanjouKamokuKBN
        , MEI.KasiKanjouBunsekiCheckKBN1
        , MEI.KasiKanjouBunsekiCheckKBN2
        , MEI.KasiKanjouBunsekiCheckKBN3
        , MEI.KasiKanjouBunsekiCheckKBN4
        , MEI.KasiKanjouBunsekiCheckKBN5
        , MEI.KasiKanjouSikinCheckKBN
        , MEI.KasiKanjouProjectCheckKBN
        , MEI.KasiKanjouHasseiLeaseKaishaKBN
        , MEI.KasiKanjouHasseiZeimushoKBN
        , MEI.KasiKanjouHasseiSiiresakiKBN
        , MEI.KasiKanjouHasseiSiharaisakiKBN
        , MEI.KasiKanjouHasseiTokuisakiKBN
        , MEI.KasiKanjouHasseiKaishuusakiKBN
        , MEI.KasiKanjouHasseiTorihikisakiKBN1
        , MEI.KasiKanjouHasseiTorihikisakiKBN2
        , MEI.KasiKanjouHasseiTorihikisakiKBN3
        , MEI.KasiKanjouHasseiTorihikisakiKBN4
        , MEI.KasiKanjouCDErrorFlag
        , MEI.KasiHojoCDErrorFlag
        , MEI.KasiUtiwakeCDErrorFlag
        , MEI.KasiZigyoushoCDErrorFlag
        , MEI.KasiBumonCDErrorFlag
        , MEI.KasiTorihikisakiCDErrorFlag
        , MEI.KasiZeiKBNErrorFlag
        , MEI.KasiZeikomiKBNErrorFlag
        , MEI.KasiZeiHontaiCDErrorFlag
        , MEI.KasiBunsekiCD1ErrorFlag
        , MEI.KasiBunsekiCD2ErrorFlag
        , MEI.KasiBunsekiCD3ErrorFlag
        , MEI.KasiBunsekiCD4ErrorFlag
        , MEI.KasiBunsekiCD5ErrorFlag
        , MEI.KasiSikinCDErrorFlag
        , MEI.KasiProjectCDErrorFlag
        , MEI.MeisaiTekiyou
        , MEI.GyouNO
        , NULL AS Dummy1
        , NULL AS Dummy2
--2012/10/05 追加 K.Morita ST (M04851)平成26年消費税改正対応
        , CASE WHEN KARIHOJ.ShokiSetKariZeiKBN IS NULL THEN 0 ELSE 1 END AS KariHojoZeiKBNSiyouFLG
        , CASE WHEN KASIHOJ.ShokiSetKariZeiKBN IS NULL THEN 0 ELSE 1 END AS KasiHojoZeiKBNSiyouFLG
--2012/10/05 追加 K.Morita ED (M04851)平成26年消費税改正対応
        --↓2013/09/04 追加 M.Shinbo GX対応(M00020:メモ機能の実装) 開始
        , MEMO.KariRValue
        , MEMO.KariGValue
        , MEMO.KariBValue
        , MEMO.KariMemo
        , MEMO.KasiRValue
        , MEMO.KasiGValue
        , MEMO.KasiBValue
        , MEMO.KasiMemo
        --↑2013/09/04 追加 M.Shinbo GX対応(M00020:メモ機能の実装) 終了
    FROM M0000RR_MA_HonsitenSiwakeMeisai HON
    LEFT JOIN M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Meisai MEI
    ON  MEI.OperationComputerName = @p_UpdateComputerName
    AND MEI.OperationUserID       = @p_UpdateUserID
    AND MEI.OperationJobID        = @p_UpdateJobID
    AND MEI.GyouNO                = HON.GyouNO
    --↓2013/09/04 追加 M.Shinbo GX対応(M00020:メモ機能の実装) 開始
    LEFT JOIN M0000WK_MA_HonsitenMemo MEMO
    ON  MEMO.OperationComputerName  = MEI.OperationComputerName
    AND MEMO.OperationUserID        = MEI.OperationUserID      
    AND MEMO.OperationJobID         = MEI.OperationJobID       
    AND MEMO.GyouNO                 = MEI.GyouNO
    --↑2013/09/04 追加 M.Shinbo GX対応(M00020:メモ機能の実装) 終了
    LEFT JOIN M0000MS_MA_Zigyousho ZIG
    ON  ZIG.ZigyoushoCD = HON.ZigyoushoCD
    AND ZIG.KaiteiDate  = HON.ZigyoushoKaiteiDate
    LEFT JOIN M0000MS_MA_DenpyouKBN DEN
    ON  DEN.DenpyouKBN  = HON.DenpyouKBN
    LEFT JOIN #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_HonsitenKanjou HSK
    ON  HSK.ZigyoushoCD = HON.ZigyoushoCD
    LEFT JOIN #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Zigyousho HZI
    ON  HZI.ZigyoushoCD = HSK.HonshaZigyoushoCD
    LEFT JOIN M0000MS_MA_GetudoKikan GTD
    ON  GTD.GetudoKaisiDate    <= HON.HasseiDate
    AND GTD.GetudoShuuryouDate >= HON.HasseiDate
--2012/10/05 追加 K.Morita ST (M04851)平成26年消費税改正対応
    LEFT JOIN M0000MS_MA_Hojo KARIHOJ
    ON  KARIHOJ.KanjouCD = HON.KariKanjouCD
    AND KARIHOJ.HojoCD = HON.KariHojoCD
    AND KARIHOJ.KaiteiDate  = HON.KariHojoKaiteiDate
    LEFT JOIN M0000MS_MA_Hojo KASIHOJ
    ON  KASIHOJ.KanjouCD = HON.KasiKanjouCD
    AND KASIHOJ.HojoCD = HON.KasiHojoCD
    AND KASIHOJ.KaiteiDate  = HON.KasiHojoKaiteiDate
--2012/10/05 追加 K.Morita ED (M04851)平成26年消費税改正対応
--2008/06/02 追加 N.Sasaki 開始
    CROSS JOIN (
         SELECT TOP 1 
              DenpyouListInziZumiKBN
            , CheckListInziZumiKBN
            , GuidAnken
            , ShouninStateKBN
            , KihyouUserID
            , CurrentUserID
            , ShouninUserID
            , ShouninDate
         FROM  M0000RR_MA_SiwakeMeisai
         WHERE SystemDenpyouNO = @p_SystemDenpyouNO
    ) AS SWK
--2008/06/02 追加 N.Sasaki 終了
    WHERE HON.SystemDenpyouNO = @p_SystemDenpyouNO
    --2014/11/12 K.Shigematsu(12917)伝票表示時に行順が意図しない順番になる現象を修正
    ORDER BY MEI.GyouNO
    --2014/11/12 K.Shigematsu(12917)伝票表示時に行順が意図しない順番になる現象を修正

    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR

    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END
    
    RETURN @ErrorCode

    /* ラベル：ERR_SHORI*/
ERR_SHORI:
    
    IF @po_ErrorKBN = 0
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
    END
    -- 変数.エラーコードをリターンする。
    RETURN @ErrorCode
END



GO

