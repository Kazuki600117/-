IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M0000PR_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Entry_Siwake]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[M0000PR_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Entry_Siwake]

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE  M0000PR_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Entry_Siwake(

      @po_ErrorKBN                      int              OUTPUT     /* エラー区分 */
    , @po_ErrorCD1                      varchar(128)     OUTPUT     /* エラーCD1 */
    , @po_ErrorCD2                      varchar(128)     OUTPUT     /* エラーCD2 */
    , @po_ErrorProcName                 varchar(128)     OUTPUT     /* エラーストアド名 */
    , @po_ErrorSummary                  varchar(256)     OUTPUT     /* エラーメッセージ概要 */
    , @po_ErrorDetail                   varchar(2560)    OUTPUT     /* エラーメッセージ詳細 */
    , @po_ErrorBtnKBN                   tinyint          OUTPUT     /* ボタン区分(0：OK、1：YES・NO、2：YES・NO・CANCEL) */
    , @po_ErrorBtnDefault               tinyint          OUTPUT     /* デフォルトボタン区分(0：ボタン１、1：ボタン２、2：ボタン３) */
    , @po_SystemDenpyouNO               decimal(18, 0)   OUTPUT     /* システム伝票番号 */
    , @po_DenpyouNO                     varchar(12)      OUTPUT     /* 伝票番号 */
    , @p_ShoriMode                      tinyint                     /* 処理モード */
    , @p_DenpyouSaibanHouhou            tinyint                     /* 伝票採番方法 */
    , @p_DenpyouCountKeta               tinyint                     /* 伝票番号カウンタ桁数 */
    , @p_ZigyoushoCD                    varchar(6)                  /* 事業所コード */
    , @p_HasseiDate                     datetime                    /* 発生日 */
    -- 2010/08/17 修正 C.Okuno@OBS  (M04451)↓
    --, @p_DenpyouKBN                     varchar(2)                  /* 伝票区分 */
    --, @p_ShuuseimaeDenpyouKBN           varchar(2)                  /* 修正前伝票区分 */
    , @p_DenpyouKBN                     varchar(3)                  /* 伝票区分 */
    , @p_ShuuseimaeDenpyouKBN           varchar(3)                  /* 修正前伝票区分 */
    -- 2010/08/17 修正 C.Okuno@OBS  (M04451)↑
    , @p_BumonSecurityPatternCD         varchar(4)                  /* 部門セキュリティパターンコード */
    , @p_KanjouSecurityPatternCD        varchar(4)                  /* 総勘定科目セキュリティパターンコード */
    , @p_ZigyoushoSecurityPatternCD     varchar(4)                  /* 事業所セキュリティパターンコード */
    , @p_HonsitenSecurityFlag           tinyint                     /* 本支店セキュリティフラグ */
    -- ↓ 2016/10/17 M.Tamura INS [47349]分析コード機能強化対応(残高更新)
    , @p_BunsekibetuZandakaSiyouKBN     tinyint = 0                 /* 分析別残高使用区分 */
    -- ↑ 2016/10/17 M.Tamura INS [47349]分析コード機能強化対応(残高更新)
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
**    名称：本支店仕訳明細登録処理
**    機能：本支店仕訳入力画面の内容を仕訳明細データに登録する。
**    パラメータ
**    @po_ErrorKBN                      int              OUTPUT     /* エラー区分 */
**  , @po_ErrorCD1                      varchar(128)     OUTPUT     /* エラーCD1 */
**  , @po_ErrorCD2                      varchar(128)     OUTPUT     /* エラーCD2 */
**  , @po_ErrorProcName                 varchar(128)     OUTPUT     /* エラーストアド名 */
**  , @po_ErrorSummary                  varchar(256)     OUTPUT     /* エラーメッセージ概要 */
**  , @po_ErrorDetail                   varchar(2560)    OUTPUT     /* エラーメッセージ詳細 */
**  , @po_ErrorBtnKBN                   tinyint          OUTPUT     /* ボタン区分(0：OK、1：YES・NO、2：YES・NO・CANCEL) */
**  , @po_ErrorBtnDefault               tinyint          OUTPUT     /* デフォルトボタン区分(0：ボタン１、1：ボタン２、2：ボタン３) */
**  , @p_BumonSecurityPatternCD         varchar(4)                  /* 部門セキュリティパターンコード */
**  , @p_KanjouSecurityPatternCD        varchar(4)                  /* 総勘定科目セキュリティパターンコード */
**  , @p_ZigyoushoSecurityPatternCD     varchar(4)                  /* 事業所セキュリティパターンコード */
**  , @po_SystemDenpyouNO               decimal(18, 0)   OUTPUT     /* システム伝票番号 */
**  , @po_DenpyouNO                     varchar(12)      OUTPUT     /* 伝票番号 */
**  , @p_ShoriMode                      tinyint                     /* 処理モード */
**  , @p_DenpyouSaibanHouhou            tinyint                     /* 伝票採番方法 */
**  , @p_DenpyouCountKeta               tinyint                     /* 伝票番号カウンタ桁数 */
**  , @p_ZigyoushoCD                    varchar(6)                  /* 事業所コード */
**  , @p_HasseiDate                     datetime                    /* 発生日 */
-- 2010/08/17 修正 C.Okuno@OBS  (M04451)↓
--**  , @p_DenpyouKBN                     varchar(2)                  /* 伝票区分 */
--**  , @p_ShuuseimaeDenpyouKBN           varchar(2)                  /* 修正前伝票区分 */    
**  , @p_DenpyouKBN                     varchar(3)                  /* 伝票区分 */
**  , @p_ShuuseimaeDenpyouKBN           varchar(3)                  /* 修正前伝票区分 */    
-- 2010/08/17 修正 C.Okuno@OBS  (M04451)↑
**  -- ↓ 2016/10/17 M.Tamura INS [47349]分析コード機能強化対応(残高更新)
**  , @p_BunsekibetuZandakaSiyouKBN     tinyint                     /* 分析別残高使用区分 */
**  -- ↑ 2016/10/17 M.Tamura INS [47349]分析コード機能強化対応(残高更新)
**  , @p_UpdateDatetime                 datetime                    /* 更新日時 */
**  , @p_UpdateCertificatedGuid         uniqueidentifier            /* 更新認証済Guid */
**  , @p_UpdateJobID                    varchar(128)                /* 更新JobID */
**  , @p_UpdateUserID                   varchar(128)                /* 更新ユーザID */
**  , @p_UpdateComputerName             varchar(128)                /* 更新PC名 */
**  , @p_UpdateComputerUserName         varchar(128)                /* 更新PCユーザ名 */
**  , @p_UpdateComputerDomainName       varchar(256)                /* 更新PCドメイン名 */
***************************************************************************************************
**      履歴
**              2007/11/27 N.Sasaki   新規作成
**              2008/05/16 H.Suzuki   仕訳単位項目型変更
**              2009/08/06 OBIC H.Sudo (M04265)貸借不一致チェックを追加
**              2009/08/17 OBIC H.Sudo (M04265)の修正
**              2010/08/17 C.Okuno    (M04451)ダブルスタンダード対応
**              2013/04/22 周俊        変更 桁数拡張
**              2013/09/04 M.Shinbo    変更 GX対応(M00020:メモ機能の実装)
**              2016/10/17 M.Tamura    変更 (47349)分析コード機能強化対応
**              2016/10/11 H.Fukuda    変更 (47267)【仮勘定消込】消込済伝票の修正・削除を制御する
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
  --2013/04/22 周俊　修正 開始
  --DECLARE @ShokutiCD                   Varchar(5)     /* 諸口コードの待避領域 */
    DECLARE @ShokutiCD                   Varchar(10)     /* 諸口コードの待避領域 */
  --2013/04/22 周俊　修正 完了 

    DECLARE @ShokutiCDKaiteiDate         datetime       /* 諸口コード改定日の待避領域 */
    DECLARE @ZigyoushoSecurityPatternCD  varchar(4)     /* 事業所セキュリティﾊﾟﾀｰﾝコード */

--2009/08/17 OBIC H.Sudo 削除 開始 (M04265)の修正
----2009/08/06 OBIC H.Sudo 変更 開始 (M04265)貸借不一致チェックを追加するための変数を準備
--    DECLARE @KariGoukei                  decimal(15,0)  /* 借方合計金額を算出 */
--    DECLARE @KasiGoukei                  decimal(15,0)  /* 貸方合計金額を算出 */
----2009/08/06 OBIC H.Sudo 変更 終了
--2009/08/17 OBIC H.Sudo 削除 終了


    -- 引数初期化
    SET @po_ErrorKBN = 0
    SET @po_ErrorCD1 = ''
    SET @po_ErrorCD2 = ''
    SET @po_ErrorProcName = ''
    SET @po_ErrorSummary = ''
    SET @po_ErrorDetail = ''
    SET @po_ErrorBtnKBN = 0
    SET @po_ErrorBtnDefault = 0

    -- 本支店分解用ワークをCREATE
    CREATE TABLE dbo.#M0000WK_MA_HonsitenBunkai
    (
             OperationComputerName              varchar(128)  COLLATE Database_Default      NOT NULL
        ,    OperationUserID                    varchar(128)  COLLATE Database_Default      NOT NULL
        ,    OperationDatetime                  datetime                                    NOT NULL
        ,    OperationJobID                     varchar(128)  COLLATE Database_Default      NOT NULL
        ,    DenpyouTani                        int                                         NOT NULL
        ,    HasseiDate                         datetime                                    NOT NULL
-- 2008/05/16 変更 NOSE H.Suzuki 開始 仕訳単位項目型変更
--         ,    SiwakeTani                         tinyint                                     NULL
        ,    SiwakeTani                         int                                         NULL
-- 2008/05/16 変更 NOSE H.Suzuki 終了
        ,    SiwakeKBN                          int                                         NOT NULL
        ,    ZigyoushoCD                        varchar(6)    COLLATE Database_Default      NOT NULL
        ,    ZigyoushoKaiteiDate                datetime                                    NOT NULL
        ,    KariZigyoushoCD                    varchar(6)    COLLATE Database_Default      NOT NULL
        ,    KariZigyoushoKaiteiDate            datetime                                    NOT NULL
      --2013/04/22 周俊　修正 開始
      --,    KariBumonCD                        varchar(6)    COLLATE Database_Default      NULL
        ,    KariBumonCD                        varchar(10)    COLLATE Database_Default      NULL
      --2013/04/22 周俊　修正 完了 

        ,    KariBumonKaiteiDate                datetime                                    NULL
      --2013/04/22 周俊　修正 開始
      --,    KariKanjouCD                       varchar(5)    COLLATE Database_Default      NULL
        ,    KariKanjouCD                       varchar(10)    COLLATE Database_Default      NULL
      --2013/04/22 周俊　修正 完了 

        ,    KariKanjouKaiteiDate               datetime                                    NULL
        ,    KariHojoCD                         varchar(10)   COLLATE Database_Default      NULL
        ,    KariHojoKaiteiDate                 datetime                                    NULL
        ,    KariUtiwakeCD                      varchar(10)   COLLATE Database_Default      NULL
        ,    KariUtiwakeKaiteiDate              datetime                                    NULL
        ,    KariTorihikisakiCD                 varchar(10)   COLLATE Database_Default      NULL
        ,    KariTorihikisakiKaiteiDate         datetime                                    NULL
        ,    KariTaishakuKBN                    tinyint                                     NULL
        ,    KariHasseiKBN                      tinyint                                     NULL
        ,    KariKingaku                        decimal(15,0)                               NULL
        ,    KariZeigaku                        decimal(15,0)                               NULL
      --2013/04/22 周俊　修正 開始
      --,    KariZeiKamokuCD                    varchar(5)    COLLATE Database_Default      NULL
        ,    KariZeiKamokuCD                    varchar(10)    COLLATE Database_Default      NULL
      --2013/04/22 周俊　修正 完了 

        ,    KariZeiKaiteiDate                  datetime                                    NULL
        ,    KariZeiTaishakuKBN                 tinyint                                     NULL
      --2013/04/22 周俊　修正 開始
      --,    KariZeiHontaiCD                    varchar(5)    COLLATE Database_Default      NULL
        ,    KariZeiHontaiCD                    varchar(10)    COLLATE Database_Default      NULL
      --2013/04/22 周俊　修正 完了 

        ,    KariZeiHontaiKaiteiDate            datetime                                    NULL
      --2013/04/22 周俊　修正 開始
      --,    KariZeiKBN                         varchar(2)    COLLATE Database_Default      NULL
        ,    KariZeiKBN                         varchar(3)    COLLATE Database_Default      NULL
      --2013/04/22 周俊　修正 完了 

        ,    KariZeiRate                        decimal(7,3)                                NULL
        ,    KariHasuuKBN                       tinyint                                     NULL
        ,    KariZeiKeisanKBN                   tinyint                                     NULL
        ,    KariZeiKamokuKBN                   tinyint                                     NULL
        ,    KariZeikomiKBN                     tinyint                                     NULL
        ,    KariTorihikisakiKBN                tinyint                                     NULL
        ,    KariHojoKBN                        tinyint                                     NULL
        ,    KariUtiwakeKBN                     tinyint                                     NULL
        ,    KariBumonKBN                       tinyint                                     NULL
        ,    KariBunsekiCD1                     varchar(30)   COLLATE Database_Default      NULL
        ,    KariBunsekiCD2                     varchar(30)   COLLATE Database_Default      NULL
        ,    KariBunsekiCD3                     varchar(30)   COLLATE Database_Default      NULL
        ,    KariBunsekiCD4                     varchar(30)   COLLATE Database_Default      NULL
        ,    KariBunsekiCD5                     varchar(30)   COLLATE Database_Default      NULL
        ,    KariSikinCD                        varchar(30)   COLLATE Database_Default      NULL
        ,    KariProjectCD                      varchar(30)   COLLATE Database_Default      NULL
        ,    KariBunsekiCustomizeCD1            varchar(30)   COLLATE Database_Default      NULL
        ,    KariBunsekiCustomizeCD2            varchar(30)   COLLATE Database_Default      NULL
        ,    KariBunsekiCustomizeCD3            varchar(30)   COLLATE Database_Default      NULL
        ,    KariBunsekiCustomizeCD4            varchar(30)   COLLATE Database_Default      NULL
        ,    KariBunsekiCustomizeCD5            varchar(30)   COLLATE Database_Default      NULL
        ,    KasiZigyoushoCD                    varchar(6)    COLLATE Database_Default      NOT NULL
        ,    KasiZigyoushoKaiteiDate            datetime                                    NOT NULL
      --2013/04/22 周俊　修正 開始
      --,    KasiBumonCD                        varchar(6)    COLLATE Database_Default      NULL
        ,    KasiBumonCD                        varchar(10)    COLLATE Database_Default      NULL
      --2013/04/22 周俊　修正 完了 

        ,    KasiBumonKaiteiDate                datetime                                    NULL
      --2013/04/22 周俊　修正 開始
      --,    KasiKanjouCD                       varchar(5)    COLLATE Database_Default      NULL
        ,    KasiKanjouCD                       varchar(10)    COLLATE Database_Default      NULL
      --2013/04/22 周俊　修正 完了 

        ,    KasiKanjouKaiteiDate               datetime                                    NULL
        ,    KasiHojoCD                         varchar(10)   COLLATE Database_Default      NULL
        ,    KasiHojoKaiteiDate                 datetime                                    NULL
        ,    KasiUtiwakeCD                      varchar(10)   COLLATE Database_Default      NULL
        ,    KasiUtiwakeKaiteiDate              datetime                                    NULL
        ,    KasiTorihikisakiCD                 varchar(10)   COLLATE Database_Default      NULL
        ,    KasiTorihikisakiKaiteiDate         datetime                                    NULL
        ,    KasiTaishakuKBN                    tinyint                                     NULL
        ,    KasiHasseiKBN                      tinyint                                     NULL
        ,    KasiKingaku                        decimal(15,0)                               NULL
        ,    KasiZeigaku                        decimal(15,0)                               NULL
      --2013/04/22 周俊　修正 開始
      --,    KasiZeiKamokuCD                    varchar(5)    COLLATE Database_Default      NULL
        ,    KasiZeiKamokuCD                    varchar(10)    COLLATE Database_Default      NULL
      --2013/04/22 周俊　修正 完了 

        ,    KasiZeiKaiteiDate                  datetime                                    NULL
        ,    KasiZeiTaishakuKBN                 tinyint                                     NULL
      --2013/04/22 周俊　修正 開始
      --,    KasiZeiHontaiCD                    varchar(5)    COLLATE Database_Default      NULL
        ,    KasiZeiHontaiCD                    varchar(10)    COLLATE Database_Default      NULL
      --2013/04/22 周俊　修正 完了 

        ,    KasiZeiHontaiKaiteiDate            datetime                                    NULL
      --2013/04/22 周俊　修正 開始
      --,    KasiZeiKBN                         varchar(2)    COLLATE Database_Default      NULL
        ,    KasiZeiKBN                         varchar(3)    COLLATE Database_Default      NULL
      --2013/04/22 周俊　修正 完了 

        ,    KasiZeiRate                        decimal(7,3)                                NULL
        ,    KasiHasuuKBN                       tinyint                                     NULL
        ,    KasiZeiKeisanKBN                   tinyint                                     NULL
        ,    KasiZeiKamokuKBN                   tinyint                                     NULL
        ,    KasiZeikomiKBN                     tinyint                                     NULL
        ,    KasiTorihikisakiKBN                tinyint                                     NULL
        ,    KasiHojoKBN                        tinyint                                     NULL
        ,    KasiUtiwakeKBN                     tinyint                                     NULL
        ,    KasiBumonKBN                       tinyint                                     NULL
        ,    KasiBunsekiCD1                     varchar(30)   COLLATE Database_Default      NULL
        ,    KasiBunsekiCD2                     varchar(30)   COLLATE Database_Default      NULL
        ,    KasiBunsekiCD3                     varchar(30)   COLLATE Database_Default      NULL
        ,    KasiBunsekiCD4                     varchar(30)   COLLATE Database_Default      NULL
        ,    KasiBunsekiCD5                     varchar(30)   COLLATE Database_Default      NULL
        ,    KasiSikinCD                        varchar(30)   COLLATE Database_Default      NULL
        ,    KasiProjectCD                      varchar(30)   COLLATE Database_Default      NULL
        ,    KasiBunsekiCustomizeCD1            varchar(30)   COLLATE Database_Default      NULL
        ,    KasiBunsekiCustomizeCD2            varchar(30)   COLLATE Database_Default      NULL
        ,    KasiBunsekiCustomizeCD3            varchar(30)   COLLATE Database_Default      NULL
        ,    KasiBunsekiCustomizeCD4            varchar(30)   COLLATE Database_Default      NULL
        ,    KasiBunsekiCustomizeCD5            varchar(30)   COLLATE Database_Default      NULL
        ,    MeisaiTekiyou                      varchar(60)   COLLATE Database_Default      NULL
        ,    GyouNO                             int                                         NOT NULL
        ,    CompanyNO                          int                                         NOT NULL
        ,    UpdateDatetime                     datetime                                    NOT NULL
        ,    UpdateCertificatedGuid             uniqueidentifier                            NOT NULL
        ,    UpdateJobID                        varchar(128)  COLLATE Database_Default      NOT NULL
        ,    UpdateUserID                       varchar(128)  COLLATE Database_Default      NOT NULL
        ,    UpdateComputerName                 varchar(128)  COLLATE Database_Default      NOT NULL
        ,    UpdateComputerUserName             varchar(128)  COLLATE Database_Default      NOT NULL
        ,    UpdateComputerDomainName           varchar(256)  COLLATE Database_Default      NOT NULL
    )
    CREATE CLUSTERED INDEX IX_#M0000WK_MA_HonsitenBunkai ON #M0000WK_MA_HonsitenBunkai
    (
             OperationComputerName
        ,    OperationUserID
        ,    OperationDatetime
        ,    OperationJobID
        ,    GyouNO
    )

    --本支店分解結果ワークを作成
    CREATE TABLE dbo.#M0000WK_MA_HonsitenBunkaiKekka
    (
             OperationComputerName              varchar(128)  COLLATE Database_Default      NOT NULL
        ,    OperationUserID                    varchar(128)  COLLATE Database_Default      NOT NULL
        ,    OperationDatetime                  datetime                                    NOT NULL
        ,    OperationJobID                     varchar(128)  COLLATE Database_Default      NOT NULL
        ,    DenpyouTani                        int                                         NOT NULL
        ,    DenpyouTaniNew                     int                                         NULL
        ,    HasseiDate                         datetime                                    NOT NULL
        ,    ZigyoushoCD                        varchar(6)    COLLATE Database_Default      NOT NULL
        ,    ZigyoushoKaiteiDate                datetime                                    NULL
-- 2008/05/16 変更 NOSE H.Suzuki 開始 仕訳単位項目型変更
--         ,    SiwakeTani                         tinyint                                     NULL
        ,    SiwakeTani                         int                                         NULL
-- 2008/05/16 変更 NOSE H.Suzuki 終了
        ,    SiwakeKBN                          int                                         NULL
      --2013/04/22 周俊　修正 開始
      --,    KariBumonCD                        varchar(6)    COLLATE Database_Default      NULL
        ,    KariBumonCD                        varchar(10)    COLLATE Database_Default      NULL
      --2013/04/22 周俊　修正 完了 

        ,    KariBumonKaiteiDate                datetime                                    NULL
      --2013/04/22 周俊　修正 開始
      --,    KariKanjouCD                       varchar(5)    COLLATE Database_Default      NULL
        ,    KariKanjouCD                       varchar(10)    COLLATE Database_Default      NULL
      --2013/04/22 周俊　修正 完了 

        ,    KariKanjouKaiteiDate               datetime                                    NULL
        ,    KariHojoCD                         varchar(10)   COLLATE Database_Default      NULL
        ,    KariHojoKaiteiDate                 datetime                                    NULL
        ,    KariUtiwakeCD                      varchar(10)   COLLATE Database_Default      NULL
        ,    KariUtiwakeKaiteiDate              datetime                                    NULL
        ,    KariTorihikisakiCD                 varchar(10)   COLLATE Database_Default      NULL
        ,    KariTorihikisakiKaiteiDate         datetime                                    NULL
        ,    KariTaishakuKBN                    tinyint                                     NULL
        ,    KariHasseiKBN                      tinyint                                     NULL
        ,    KariKingaku                        decimal(15,0)                               NULL
        ,    KariZeigaku                        decimal(15,0)                               NULL
      --2013/04/22 周俊　修正 開始
      --,    KariZeiKamokuCD                    varchar(5)    COLLATE Database_Default      NULL
        ,    KariZeiKamokuCD                    varchar(10)    COLLATE Database_Default      NULL
      --2013/04/22 周俊　修正 完了 

        ,    KariZeiKaiteiDate                  datetime                                    NULL
        ,    KariZeiTaishakuKBN                 tinyint                                     NULL
      --2013/04/22 周俊　修正 開始
      --,    KariZeiHontaiCD                    varchar(5)    COLLATE Database_Default      NULL
        ,    KariZeiHontaiCD                    varchar(10)    COLLATE Database_Default      NULL
      --2013/04/22 周俊　修正 完了 

        ,    KariZeiHontaiKaiteiDate            datetime                                    NULL
      --2013/04/22 周俊　修正 開始
      --,    KariZeiKBN                         varchar(2)    COLLATE Database_Default      NULL
        ,    KariZeiKBN                         varchar(3)    COLLATE Database_Default      NULL
      --2013/04/22 周俊　修正 完了 

        ,    KariZeiRate                        decimal(7,3)                                NULL
        ,    KariHasuuKBN                       tinyint                                     NULL
        ,    KariZeiKeisanKBN                   tinyint                                     NULL
        ,    KariZeiKamokuKBN                   tinyint                                     NULL
        ,    KariZeikomiKBN                     tinyint                                     NULL
        ,    KariTorihikisakiKBN                tinyint                                     NULL
        ,    KariHojoKBN                        tinyint                                     NULL
        ,    KariUtiwakeKBN                     tinyint                                     NULL
        ,    KariBumonKBN                       tinyint                                     NULL
        ,    KariBunsekiCD1                     varchar(30)   COLLATE Database_Default      NULL
        ,    KariBunsekiCD2                     varchar(30)   COLLATE Database_Default      NULL
        ,    KariBunsekiCD3                     varchar(30)   COLLATE Database_Default      NULL
        ,    KariBunsekiCD4                     varchar(30)   COLLATE Database_Default      NULL
        ,    KariBunsekiCD5                     varchar(30)   COLLATE Database_Default      NULL
        ,    KariSikinCD                        varchar(30)   COLLATE Database_Default      NULL
        ,    KariProjectCD                      varchar(30)   COLLATE Database_Default      NULL
        ,    KariBunsekiCustomizeCD1            varchar(30)   COLLATE Database_Default      NULL
        ,    KariBunsekiCustomizeCD2            varchar(30)   COLLATE Database_Default      NULL
        ,    KariBunsekiCustomizeCD3            varchar(30)   COLLATE Database_Default      NULL
        ,    KariBunsekiCustomizeCD4            varchar(30)   COLLATE Database_Default      NULL
        ,    KariBunsekiCustomizeCD5            varchar(30)   COLLATE Database_Default      NULL
      --2013/04/22 周俊　修正 開始
      --,    KasiBumonCD                        varchar(6)    COLLATE Database_Default      NULL
        ,    KasiBumonCD                        varchar(10)    COLLATE Database_Default      NULL
      --2013/04/22 周俊　修正 完了 

        ,    KasiBumonKaiteiDate                datetime                                    NULL
      --2013/04/22 周俊　修正 開始
      --,    KasiKanjouCD                       varchar(5)    COLLATE Database_Default      NULL
        ,    KasiKanjouCD                       varchar(10)    COLLATE Database_Default      NULL
      --2013/04/22 周俊　修正 完了 

        ,    KasiKanjouKaiteiDate               datetime                                    NULL
        ,    KasiHojoCD                         varchar(10)   COLLATE Database_Default      NULL
        ,    KasiHojoKaiteiDate                 datetime                                    NULL
        ,    KasiUtiwakeCD                      varchar(10)   COLLATE Database_Default      NULL
        ,    KasiUtiwakeKaiteiDate              datetime                                    NULL
        ,    KasiTorihikisakiCD                 varchar(10)   COLLATE Database_Default      NULL
        ,    KasiTorihikisakiKaiteiDate         datetime                                    NULL
        ,    KasiTaishakuKBN                    tinyint                                     NULL
        ,    KasiHasseiKBN                      tinyint                                     NULL
        ,    KasiKingaku                        decimal(15,0)                               NULL
        ,    KasiZeigaku                        decimal(15,0)                               NULL
      --2013/04/22 周俊　修正 開始
      --,    KasiZeiKamokuCD                    varchar(5)    COLLATE Database_Default      NULL
        ,    KasiZeiKamokuCD                    varchar(10)    COLLATE Database_Default      NULL
      --2013/04/22 周俊　修正 完了 

        ,    KasiZeiKaiteiDate                  datetime                                    NULL
        ,    KasiZeiTaishakuKBN                 tinyint                                     NULL
      --2013/04/22 周俊　修正 開始
      --,    KasiZeiHontaiCD                    varchar(5)    COLLATE Database_Default      NULL
        ,    KasiZeiHontaiCD                    varchar(10)    COLLATE Database_Default      NULL
      --2013/04/22 周俊　修正 完了 

        ,    KasiZeiHontaiKaiteiDate            datetime                                    NULL
      --2013/04/22 周俊　修正 開始
      --,    KasiZeiKBN                         varchar(2)    COLLATE Database_Default      NULL
        ,    KasiZeiKBN                         varchar(3)    COLLATE Database_Default      NULL
      --2013/04/22 周俊　修正 完了 

        ,    KasiZeiRate                        decimal(7,3)                                NULL
        ,    KasiHasuuKBN                       tinyint                                     NULL
        ,    KasiZeiKeisanKBN                   tinyint                                     NULL
        ,    KasiZeiKamokuKBN                   tinyint                                     NULL
        ,    KasiZeikomiKBN                     tinyint                                     NULL
        ,    KasiTorihikisakiKBN                tinyint                                     NULL
        ,    KasiHojoKBN                        tinyint                                     NULL
        ,    KasiUtiwakeKBN                     tinyint                                     NULL
        ,    KasiBumonKBN                       tinyint                                     NULL
        ,    KasiBunsekiCD1                     varchar(30)   COLLATE Database_Default      NULL
        ,    KasiBunsekiCD2                     varchar(30)   COLLATE Database_Default      NULL
        ,    KasiBunsekiCD3                     varchar(30)   COLLATE Database_Default      NULL
        ,    KasiBunsekiCD4                     varchar(30)   COLLATE Database_Default      NULL
        ,    KasiBunsekiCD5                     varchar(30)   COLLATE Database_Default      NULL
        ,    KasiSikinCD                        varchar(30)   COLLATE Database_Default      NULL
        ,    KasiProjectCD                      varchar(30)   COLLATE Database_Default      NULL
        ,    KasiBunsekiCustomizeCD1            varchar(30)   COLLATE Database_Default      NULL
        ,    KasiBunsekiCustomizeCD2            varchar(30)   COLLATE Database_Default      NULL
        ,    KasiBunsekiCustomizeCD3            varchar(30)   COLLATE Database_Default      NULL
        ,    KasiBunsekiCustomizeCD4            varchar(30)   COLLATE Database_Default      NULL
        ,    KasiBunsekiCustomizeCD5            varchar(30)   COLLATE Database_Default      NULL
        ,    MeisaiTekiyou                      varchar(60)   COLLATE Database_Default      NULL
        ,    GyouNO                             int                                         NOT NULL
        ,    GyouNOEdaban                       int                                         NULL
        ,    GyouNONew                          int                                         NULL
        ,    CompanyNO                          int                                         NOT NULL
        ,    UpdateDatetime                     datetime                                    NOT NULL
        ,    UpdateCertificatedGuid             uniqueidentifier                            NOT NULL
        ,    UpdateJobID                        varchar(128)  COLLATE Database_Default      NOT NULL
        ,    UpdateUserID                       varchar(128)  COLLATE Database_Default      NOT NULL
        ,    UpdateComputerName                 varchar(128)  COLLATE Database_Default      NOT NULL
        ,    UpdateComputerUserName             varchar(128)  COLLATE Database_Default      NOT NULL
        ,    UpdateComputerDomainName           varchar(256)  COLLATE Database_Default      NOT NULL
    )
    CREATE CLUSTERED INDEX IX_#M0000WK_MA_HonsitenBunkaiKekka ON dbo.#M0000WK_MA_HonsitenBunkaiKekka
    (
             OperationComputerName
        ,    OperationUserID
        ,    OperationDatetime
        ,    OperationJobID    
        ,    DenpyouTani
        ,    GyouNO
        ,    GyouNOEdaban
        ,    GyouNONew
    )
    
    --↓2013/09/04 追加 M.Shinbo GX対応(M00020:メモ機能の実装) 開始
    -- 事業所ワークを作成  
    --   連携メモ情報データへ更新する連携受入順番号の取得用
    CREATE TABLE #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Zigyousho  
    (  
             ZigyoushoCD            varchar(6)      COLLATE Database_Default    NOT NULL  
           , RenkeiUkeireJunNO      decimal(18,0)                               NULL  
    )
    CREATE CLUSTERED INDEX IX_#M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Zigyousho ON #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Zigyousho  
    (  
          ZigyoushoCD  
    )
    --↑2013/09/04 追加 M.Shinbo GX対応(M00020:メモ機能の実装) 開始
    

    /* ワークテーブルの前回データ削除 */
    -- 仕訳11編集用ワークの前回データ削除
    DELETE FROM M0000WK_MA_Siwake11Hensyuu
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

    -- 仕訳明細共通ワークの前回データ削除
    DELETE FROM M0000WK_MA_SiwakeKyoutuu
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

    -- 本支店分解用ワークに本支店仕訳ワークの内容更新
    INSERT INTO #M0000WK_MA_HonsitenBunkai
    (
          OperationComputerName
        , OperationUserID
        , OperationDatetime
        , OperationJobID
        , DenpyouTani
        , HasseiDate
        , SiwakeTani
        , SiwakeKBN
        , ZigyoushoCD
        , ZigyoushoKaiteiDate
        , KariZigyoushoCD
        , KariZigyoushoKaiteiDate
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
        , KasiZigyoushoCD
        , KasiZigyoushoKaiteiDate
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
        , 1
        , HED.HasseiDate
        , NULL
        , MEI.SiwakeKBN
        , HED.ZigyoushoCD
        , HED.ZigyoushoKaiteiDate
        , CASE WHEN MEI.KariZigyoushoCD IS NULL THEN HED.ZigyoushoCD ELSE MEI.KariZigyoushoCD END
        , CASE WHEN MEI.KariZigyoushoCD IS NULL THEN HED.ZigyoushoKaiteiDate ELSE MEI.KariZigyoushoKaiteiDate END
        , MEI.KariBumonCD
        , MEI.KariBumonKaiteiDate
        , MEI.KariKanjouCD
        , MEI.KariKanjouKaiteiDate
        , MEI.KariHojoCD
        , MEI.KariHojoKaiteiDate
        , MEI.KariUtiwakeCD
        , MEI.KariUtiwakeKaiteiDate
        , MEI.KariTorihikisakiCD
        , MEI.KariTorihikisakiKaiteiDate
        , MEI.KariTaishakuKBN
        , 0
        , MEI.KariKingaku
        , MEI.KariZeigaku
        , MEI.KariZeiKamokuCD
        , MEI.KariZeiKaiteiDate
        , MEI.KariZeiTaishakuKBN
        , MEI.KariZeiHontaiCD
        , MEI.KariZeiHontaiKaiteiDate
        , ISNULL(MEI.KariZeiKBN,'0')
        , MEI.KariZeiRate
        , MEI.KariHasuuKBN
        , MEI.KariZeiKeisanKBN
        , MEI.KariZeiKamokuKBN
        , ISNULL(MEI.KariZeikomiKBN,0)
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
        , CASE WHEN MEI.KasiZigyoushoCD IS NULL THEN HED.ZigyoushoCD ELSE MEI.KasiZigyoushoCD END
        , CASE WHEN MEI.KasiZigyoushoCD IS NULL THEN HED.ZigyoushoKaiteiDate ELSE MEI.KasiZigyoushoKaiteiDate END
        , MEI.KasiBumonCD
        , MEI.KasiBumonKaiteiDate
        , MEI.KasiKanjouCD
        , MEI.KasiKanjouKaiteiDate
        , MEI.KasiHojoCD
        , MEI.KasiHojoKaiteiDate
        , MEI.KasiUtiwakeCD
        , MEI.KasiUtiwakeKaiteiDate
        , MEI.KasiTorihikisakiCD
        , MEI.KasiTorihikisakiKaiteiDate
        , MEI.KasiTaishakuKBN
        , 1
        , MEI.KasiKingaku
        , MEI.KasiZeigaku
        , MEI.KasiZeiKamokuCD
        , MEI.KasiZeiKaiteiDate
        , MEI.KasiZeiTaishakuKBN
        , MEI.KasiZeiHontaiCD
        , MEI.KasiZeiHontaiKaiteiDate
        , ISNULL(MEI.KasiZeiKBN,'0')
        , MEI.KasiZeiRate
        , MEI.KasiHasuuKBN
        , MEI.KasiZeiKeisanKBN
        , MEI.KasiZeiKamokuKBN
        , ISNULL(MEI.KasiZeikomiKBN,0)
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
        , MEI.MeisaiTekiyou
        , MEI.GyouNO
        , HED.CompanyNO
        , @p_UpdateDatetime
        , @p_UpdateCertificatedGuid
        , @p_UpdateJobID
        , @p_UpdateUserID
        , @p_UpdateComputerName
        , @p_UpdateComputerUserName
        , @p_UpdateComputerDomainName
    FROM M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Head HED
    INNER JOIN M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Meisai MEI
    ON  MEI.OperationComputerName  = HED.OperationComputerName
    AND MEI.OperationUserID        = HED.OperationUserID
    AND MEI.OperationJobID         = HED.OperationJobID
    WHERE HED.OperationComputerName = @p_UpdateComputerName
    AND   HED.OperationUserID       = @p_UpdateUserID
    AND   HED.OperationJobID        = @p_UpdateJobID


    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR

    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END

--2009/08/06 OBIC H.Sudo 変更 開始 (M04265)貸借不一致チェックを追加
--2009/08/17 OBIC H.Sudo (M04265)の修正   
--    --借方合計金額、貸方合計金額を計算
--    SET @KariGoukei = (SELECT SUM(ISNULL(KariKingaku,0)) + SUM(ISNULL(KariZeigaku,0)) FROM #M0000WK_MA_HonsitenBunkai)
--    SET @KasiGoukei = (SELECT SUM(ISNULL(KasiKingaku,0)) + SUM(ISNULL(KasiZeigaku,0)) FROM #M0000WK_MA_HonsitenBunkai)
--
--    --借方合計金額 <> 借方合計金額の場合、エラーを返す
--    IF  @KariGoukei <> @KasiGoukei
--    BEGIN
--            SET @po_ErrorKBN = 2
--            SET @po_ErrorCD1 = '000'
--            SET @po_ErrorCD2 = '00248'
--            SET @po_ErrorProcName   = OBJECT_NAME(@@PROCID)
--            SET @po_ErrorSummary    = ''
--            SET @po_ErrorDetail     = ''
--            SET @po_ErrorBtnKBN     = 0
--            SET @po_ErrorBtnDefault = 0
--
--            GOTO ERR_SHORI
--    END


    --借方合計金額 <> 借方合計金額の場合、エラーを返す
    IF EXISTS (
        SELECT 1
        FROM #M0000WK_MA_HonsitenBunkai
        WHERE OperationComputerName = @p_UpdateComputerName
        AND   OperationUserID       = @p_UpdateUserID
        AND   OperationJobID        = @p_UpdateJobID
        HAVING SUM(ISNULL(KariKingaku,0)) + SUM(ISNULL(KariZeigaku,0)) <> SUM(ISNULL(KasiKingaku,0)) + SUM(ISNULL(KasiZeigaku,0))
    )
    BEGIN
            SET @po_ErrorKBN = 2
            SET @po_ErrorCD1 = '000'
            SET @po_ErrorCD2 = '00248'
            SET @po_ErrorProcName   = OBJECT_NAME(@@PROCID)
            SET @po_ErrorSummary    = ''
            SET @po_ErrorDetail     = ''
            SET @po_ErrorBtnKBN     = 0
            SET @po_ErrorBtnDefault = 0

            GOTO ERR_SHORI
    END
    
    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR

    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END
--2009/08/17 OBIC H.Sudo 変更 終了
--2009/08/06 OBIC H.Sudo 変更 終了


    -- 本支店分解処理用のセキュリティﾊﾟﾀｰﾝコードセット
    SET @ZigyoushoSecurityPatternCD = CASE WHEN @p_HonsitenSecurityFlag = 1 THEN '' ELSE @p_ZigyoushoSecurityPatternCD END

    -- 共通ストアド：本支店分解処理を実行
    EXECUTE @SUB_ErrorCode = M0000PR_MA_HonsitenBunkai
          @po_ErrorKBN                  = @SUB_ErrorKBN        OUTPUT
        , @po_ErrorCD1                  = @SUB_ErrorCD1        OUTPUT
        , @po_ErrorCD2                  = @SUB_ErrorCD2        OUTPUT
        , @po_ErrorProcName             = @SUB_ErrorProcName   OUTPUT
        , @po_ErrorSummary              = @SUB_ErrorSummary    OUTPUT
        , @po_ErrorDetail               = @SUB_ErrorDetail     OUTPUT
        , @po_ErrorBtnKBN               = @SUB_ErrorBtnKBN     OUTPUT
        , @po_ErrorBtnDefault           = @SUB_ErrorBtnDefault OUTPUT
        , @p_BumonSecurityPatternCD     = @p_BumonSecurityPatternCD
        , @p_KanjouSecurityPatternCD    = @p_KanjouSecurityPatternCD
        , @p_HurikaeDenpyouCheckKBN     = 0
        , @p_ZigyoushoSecurityPatternCD = @ZigyoushoSecurityPatternCD
        , @p_UpdateDatetime             = @p_UpdateDatetime
        , @p_UpdateCertificatedGuid     = @p_UpdateCertificatedGuid
        , @p_UpdateJobID                = @p_UpdateJobID
        , @p_UpdateUserID               = @p_UpdateUserID
        , @p_UpdateComputerName         = @p_UpdateComputerName
        , @p_UpdateComputerUserName     = @p_UpdateComputerUserName
        , @p_UpdateComputerDomainName   = @p_UpdateComputerDomainName

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

    -- 変数.サブエラー区分 <>  の時
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

    -- 仕訳11編集用ワークに、本支店分解結果ワークの内容を追加
    INSERT INTO M0000WK_MA_Siwake11Hensyuu
    (
          OperationComputerName
        , OperationUserID
        , OperationDatetime
        , OperationJobID
        , ControlKey
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
        , KariNyuuryokuKingaku
        , KariNyuuryokuZeigaku
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
        , KasiNyuuryokuKingaku
        , KasiNyuuryokuZeigaku
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
        , CompanyNO
        , UpdateDatetime
        , UpdateCertificatedGuid
        , UpdateJobID
        , UpdateUserID
        , UpdateComputerName
        , UpdateComputerUserName
        , UpdateComputerDomainName
        --↓2013/09/04 追加 M.Shinbo GX対応(M00020:メモ機能の実装) 開始
        , KariRValue
        , KariGValue
        , KariBValue
        , KariMemo
        , KasiRValue
        , KasiGValue
        , KasiBValue
        , KasiMemo
        --↑2013/09/04 追加 M.Shinbo GX対応(M00020:メモ機能の実装) 終了
    )
    SELECT
          @p_UpdateComputerName
        , @p_UpdateUserID
        , @p_UpdateDatetime
        , @p_UpdateJobID    
        , BUN.DenpyouTaniNew
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
        , 0
        , BUN.KariKingaku
        , ISNULL(BUN.KariZeigaku,0)
        , BUN.KariKingaku
        , BUN.KariZeigaku
        , BUN.KariZeiKamokuCD
        , BUN.KariZeiKaiteiDate
        , BUN.KariZeiTaishakuKBN
        , BUN.KariZeiHontaiCD
        , BUN.KariZeiHontaiKaiteiDate
        , ISNULL(BUN.KariZeiKBN,'0')
        , BUN.KariZeiRate
        , BUN.KariHasuuKBN
        , BUN.KariZeiKeisanKBN
        , BUN.KariZeiKamokuKBN
        , ISNULL(BUN.KariZeikomiKBN,0)
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
        , 1
        , BUN.KasiKingaku
        , ISNULL(BUN.KasiZeigaku,0)
        , BUN.KasiKingaku
        , BUN.KasiZeigaku
        , BUN.KasiZeiKamokuCD
        , BUN.KasiZeiKaiteiDate
        , BUN.KasiZeiTaishakuKBN
        , BUN.KasiZeiHontaiCD
        , BUN.KasiZeiHontaiKaiteiDate
        , ISNULL(BUN.KasiZeiKBN,'0')
        , BUN.KasiZeiRate
        , BUN.KasiHasuuKBN
        , BUN.KasiZeiKeisanKBN
        , BUN.KasiZeiKamokuKBN
        , ISNULL(BUN.KasiZeikomiKBN,0)
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
        , BUN.GyouNONew
        , HED.CompanyNO
        , @p_UpdateDatetime
        , @p_UpdateCertificatedGuid
        , @p_UpdateJobID
        , @p_UpdateUserID
        , @p_UpdateComputerName
        , @p_UpdateComputerUserName
        , @p_UpdateComputerDomainName
        --↓2013/09/04 追加 M.Shinbo GX対応(M00020:メモ機能の実装) 開始
        , CASE WHEN BUN.KariKanjouCD = MEI.KariKanjouCD
               THEN MEM.KariRValue
               ELSE NULL END
        , CASE WHEN BUN.KariKanjouCD = MEI.KariKanjouCD
               THEN MEM.KariGValue
               ELSE NULL END
        , CASE WHEN BUN.KariKanjouCD = MEI.KariKanjouCD
               THEN MEM.KariBValue
               ELSE NULL END
        , CASE WHEN BUN.KariKanjouCD = MEI.KariKanjouCD
               THEN CASE WHEN MEM.KariMemo = '' THEN NULL ELSE MEM.KariMemo END
               ELSE NULL END
        , CASE WHEN BUN.KasiKanjouCD = MEI.KasiKanjouCD
               THEN MEM.KasiRValue
               ELSE NULL END
        , CASE WHEN BUN.KasiKanjouCD = MEI.KasiKanjouCD
               THEN MEM.KasiGValue
               ELSE NULL END
        , CASE WHEN BUN.KasiKanjouCD = MEI.KasiKanjouCD
               THEN MEM.KasiBValue
               ELSE NULL END
        , CASE WHEN BUN.KasiKanjouCD = MEI.KasiKanjouCD
               THEN CASE WHEN MEM.KasiMemo = '' THEN NULL ELSE MEM.KasiMemo END
               ELSE NULL END
        --↑2013/09/04 追加 M.Shinbo GX対応(M00020:メモ機能の実装) 終了
    FROM #M0000WK_MA_HonsitenBunkaiKekka BUN
    --↓2013/09/04 修正 M.Shinbo GX対応(M00020:メモ機能の実装) 開始
    --INNER JOIN M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Head HED
    --ON    HED.OperationComputerName = @p_UpdateComputerName
    --AND   HED.OperationUserID       = @p_UpdateUserID
    --AND   HED.OperationJobID        = @p_UpdateJobID
    INNER JOIN M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Head HED
    ON    HED.OperationComputerName     = BUN.OperationComputerName
    AND   HED.OperationUserID           = BUN.OperationUserID
    AND   HED.OperationJobID            = BUN.OperationJobID
    INNER JOIN M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Meisai MEI
    ON    MEI.OperationComputerName     = BUN.OperationComputerName
    AND   MEI.OperationUserID           = BUN.OperationUserID
    AND   MEI.OperationJobID            = BUN.OperationJobID
    AND   MEI.GyouNO                    = BUN.GyouNO
    LEFT JOIN M0000WK_MA_HonsitenMemo MEM
    ON    MEM.OperationComputerName     = BUN.OperationComputerName
    AND   MEM.OperationUserID           = BUN.OperationUserID
    AND   MEM.OperationJobID            = BUN.OperationJobID
    AND   MEM.GyouNO                    = BUN.GyouNO
    WHERE BUN.OperationComputerName     = @p_UpdateComputerName
    AND   BUN.OperationUserID           = @p_UpdateUserID
    AND   BUN.OperationJobID            = @p_UpdateJobID
    --↑2013/09/04 修正 M.Shinbo GX対応(M00020:メモ機能の実装) 終了
    
    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR

    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END
    

    -- 共通ストアド：仕訳11編集処理を実行
    EXECUTE @SUB_ErrorCode = M0000PR_MA_SiwakeHenshuu
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

    -- 変数.サブエラー区分 <>  の時
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

    -- 仕訳明細共通ワークに、仕訳11編集後の内容を追加
    -- 借方の内容追加
    INSERT INTO M0000WK_MA_SiwakeKyoutuu
    (
          OperationComputerName
        , OperationUserID
        , OperationDatetime
        , OperationJobID
        , ControlKey
        , SystemDenpyouNO
        , DenpyouNO
        , ShoriDate
        , HasseiDate
        , DenpyouTypeKBN
        , SystemCategory 
        , SubSystemDenpyouNO
        , SubShoriKBN
        , SubDenpyouNO
        , SiwakeKBN
        , DenpyouKBN
        , ZigyoushoCD
        , ZigyoushoKaiteiDate
        , BumonCD
        , BumonKaiteiDate
        , KanjouCD
        , KanjouKaiteiDate
        , HojoCD
        , HojoKaiteiDate
        , UtiwakeCD
        , UtiwakeKaiteiDate
        , TorihikisakiCD
        , TorihikisakiKaiteiDate
        , TaishakuKBN
        , HasseiKBN
        , Kingaku
        , Zeigaku
        , ZeiKamokuCD
        , ZeiKaiteiDate
        , ZeiHontaiCD
        , ZeiHontaiKaiteiDate
        , ShoumiKingaku
        , ShoumiZeigaku
        , MeisaiTekiyou
        , ZeiKBN
        , ZeikomiKBN
        , TorihikisakiKBN
        , HojoKBN
        , UtiwakeKBN
        , BumonKBN
        , AiteBumonCD
        , AiteBumonKaiteiDate
        , AiteKanjouCD
        , AiteKanjouKaiteiDate
        , AiteHojoCD
        , AiteHojoKaiteiDate
        , AiteUtiwakeCD
        , AiteUtiwakeKaiteiDate
        , AiteTorihikisakiCD
        , AiteTorihikisakiKaiteiDate
        , AiteKingaku
        , AiteZeigaku
        , AiteZeiKamokuCD
        , AiteZeiKaiteiDate
        , AiteZeiKBN
        , AiteZeikomiKBN
        , GyouNO
        , KariKingaku
        , KariZeigaku
        , KasiKingaku
        , KasiZeigaku
        , BunsekiCD1
        , BunsekiCD2
        , BunsekiCD3
        , BunsekiCD4
        , BunsekiCD5
        , SikinCD
        , ProjectCD
        , BunsekiCustomizeCD1
        , BunsekiCustomizeCD2
        , BunsekiCustomizeCD3
        , BunsekiCustomizeCD4
        , BunsekiCustomizeCD5
        , AiteBunsekiCD1
        , AiteBunsekiCD2
        , AiteBunsekiCD3
        , AiteBunsekiCD4
        , AiteBunsekiCD5
        , AiteSikinCD
        , AiteProjectCD
        , AiteBunsekiCustomizeCD1
        , AiteBunsekiCustomizeCD2
        , AiteBunsekiCustomizeCD3
        , AiteBunsekiCustomizeCD4
        , AiteBunsekiCustomizeCD5
        , DenpyouTekiyou
        , DenpyouListInziZumiKBN
        , CheckListInziZumiKBN
        , GuidAnken
        , ShouninStateKBN
        , KihyouUserID
        , CurrentUserID
        , ShouninUserID
        , ShouninDate
        , DenpyouSanshouRank
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
          HD.OperationComputerName
        , HD.OperationUserID
        , HD.OperationDatetime
        , HD.OperationJobID
        , HEN.ControlKey
        , ISNULL(HD.SystemDenpyouNO,0)
        , ISNULL(HD.DenpyouNO,'0')
        , HD.ShoriDate
        , HD.HasseiDate
        , HD.DenpyouTypeKBN
        , HD.SystemCategory
        , HD.SubSystemDenpyouNO
        , HD.SubShoriKBN
        , HD.SubDenpyouNO
        , HEN.SiwakeKBN 
        , HD.DenpyouKBN 
        , KEK.ZigyoushoCD
        , KEK.ZigyoushoKaiteiDate
        , HEN.KariBumonCD
        , HEN.KariBumonKaiteiDate
        , HEN.KariKanjouCD
        , HEN.KariKanjouKaiteiDate
        , HEN.KariHojoCD
        , HEN.KariHojoKaiteiDate
        , HEN.KariUtiwakeCD
        , HEN.KariUtiwakeKaiteiDate
        , HEN.KariTorihikisakiCD
        , HEN.KariTorihikisakiKaiteiDate
        , HEN.KariTaishakuKBN
        , HEN.KariHasseiKBN
        , HEN.KariKingaku
        , HEN.KariZeigaku
        , HEN.KariZeiKamokuCD
        , HEN.KariZeiKaiteiDate
        , HEN.KariZeiHontaiCD
        , HEN.KariZeiHontaiKaiteiDate
        , CASE WHEN HEN.KariTaishakuKBN    = HEN.KariHasseiKBN THEN HEN.KariKingaku ELSE HEN.KariKingaku * (-1) END
        , CASE WHEN HEN.KariZeiTaishakuKBN = HEN.KariHasseiKBN THEN HEN.KariZeigaku ELSE HEN.KariZeigaku * (-1) END
        , HEN.MeisaiTekiyou
        , HEN.KariZeiKBN
        , HEN.KariZeikomiKBN
        , HEN.KariTorihikisakiKBN
        , HEN.KariHojoKBN
        , HEN.KariUtiwakeKBN
        , HEN.KariBumonKBN
        , HEN.KasiBumonCD
        , HEN.KasiBumonKaiteiDate
        , HEN.KasiKanjouCD
        , HEN.KasiKanjouKaiteiDate
        , HEN.KasiHojoCD
        , HEN.KasiHojoKaiteiDate
        , HEN.KasiUtiwakeCD
        , HEN.KasiUtiwakeKaiteiDate
        , HEN.KasiTorihikisakiCD
        , HEN.KasiTorihikisakiKaiteiDate
        , HEN.KasiKingaku
        , HEN.KasiZeigaku
        , HEN.KasiZeiKamokuCD
        , HEN.KasiZeiKaiteiDate
        , HEN.KasiZeiKBN
        , HEN.KasiZeikomiKBN
        , HEN.GyouNO
        , HEN.KariNyuuryokuKingaku
        , HEN.KariNyuuryokuZeigaku
        , HEN.KasiNyuuryokuKingaku
        , HEN.KasiNyuuryokuZeigaku
        , HEN.KariBunsekiCD1
        , HEN.KariBunsekiCD2
        , HEN.KariBunsekiCD3
        , HEN.KariBunsekiCD4
        , HEN.KariBunsekiCD5
        , HEN.KariSikinCD
        , HEN.KariProjectCD
        , HEN.KariBunsekiCustomizeCD1
        , HEN.KariBunsekiCustomizeCD2
        , HEN.KariBunsekiCustomizeCD3
        , HEN.KariBunsekiCustomizeCD4
        , HEN.KariBunsekiCustomizeCD5
        , HEN.KasiBunsekiCD1
        , HEN.KasiBunsekiCD2
        , HEN.KasiBunsekiCD3
        , HEN.KasiBunsekiCD4
        , HEN.KasiBunsekiCD5
        , HEN.KasiSikinCD
        , HEN.KasiProjectCD
        , HEN.KasiBunsekiCustomizeCD1
        , HEN.KasiBunsekiCustomizeCD2
        , HEN.KasiBunsekiCustomizeCD3
        , HEN.KasiBunsekiCustomizeCD4
        , HEN.KasiBunsekiCustomizeCD5
        , HD.DenpyouTekiyou
        , HD.DenpyouListInziZumiKBN
        , HD.CheckListInziZumiKBN
        , HD.GuidAnken
        , HD.ShouninStateKBN
        , HD.KihyouUserID
        , HD.CurrentUserID
        , HD.ShouninUserID
        , HD.ShouninDate
        , HD.DenpyouSanshouRank
        , HD.CompanyNO
        , @p_UpdateDatetime
        , @p_UpdateCertificatedGuid
        , @p_UpdateJobID
        , @p_UpdateUserID
        , @p_UpdateComputerName
        , @p_UpdateComputerUserName
        , @p_UpdateComputerDomainName
    FROM M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Head HD
    INNER JOIN M0000WK_MA_Siwake11Hensyuu HEN
    ON  HEN.OperationComputerName  = HD.OperationComputerName
    AND HEN.OperationUserID        = HD.OperationUserID
    AND HEN.OperationJobID         = HD.OperationJobID
    INNER JOIN #M0000WK_MA_HonsitenBunkaiKekka KEK
    ON    KEK.OperationComputerName = @p_UpdateComputerName
    AND   KEK.OperationUserID       = @p_UpdateUserID
    AND   KEK.OperationJobID        = @p_UpdateJobID
    AND   KEK.DenpyouTaniNew        = HEN.ControlKey
    AND   KEK.GyouNONew             = HEN.GyouNO
    WHERE HD.OperationComputerName  = @p_UpdateComputerName
    AND   HD.OperationUserID        = @p_UpdateUserID
    AND   HD.OperationJobID         = @p_UpdateJobID
    AND   HEN.KariKanjouCD IS NOT NULL
    
    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR

    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END


    -- 貸方の内容追加
    INSERT INTO M0000WK_MA_SiwakeKyoutuu
    (
          OperationComputerName
        , OperationUserID
        , OperationDatetime
        , OperationJobID
        , ControlKey
        , SystemDenpyouNO
        , DenpyouNO
        , ShoriDate
        , HasseiDate
        , DenpyouTypeKBN
        , SystemCategory
        , SubSystemDenpyouNO
        , SubShoriKBN
        , SubDenpyouNO
        , SiwakeKBN
        , DenpyouKBN
        , ZigyoushoCD
        , ZigyoushoKaiteiDate
        , BumonCD
        , BumonKaiteiDate
        , KanjouCD
        , KanjouKaiteiDate
        , HojoCD
        , HojoKaiteiDate
        , UtiwakeCD
        , UtiwakeKaiteiDate
        , TorihikisakiCD
        , TorihikisakiKaiteiDate
        , TaishakuKBN
        , HasseiKBN
        , Kingaku
        , Zeigaku
        , ZeiKamokuCD
        , ZeiKaiteiDate
        , ZeiHontaiCD
        , ZeiHontaiKaiteiDate
        , ShoumiKingaku
        , ShoumiZeigaku
        , MeisaiTekiyou
        , ZeiKBN
        , ZeikomiKBN
        , TorihikisakiKBN
        , HojoKBN
        , UtiwakeKBN
        , BumonKBN
        , AiteBumonCD
        , AiteBumonKaiteiDate
        , AiteKanjouCD
        , AiteKanjouKaiteiDate
        , AiteHojoCD
        , AiteHojoKaiteiDate
        , AiteUtiwakeCD
        , AiteUtiwakeKaiteiDate
        , AiteTorihikisakiCD
        , AiteTorihikisakiKaiteiDate
        , AiteKingaku
        , AiteZeigaku
        , AiteZeiKamokuCD
        , AiteZeiKaiteiDate
        , AiteZeiKBN
        , AiteZeikomiKBN
        , GyouNO
        , KariKingaku
        , KariZeigaku
        , KasiKingaku
        , KasiZeigaku
        , BunsekiCD1
        , BunsekiCD2
        , BunsekiCD3
        , BunsekiCD4
        , BunsekiCD5
        , SikinCD
        , ProjectCD
        , BunsekiCustomizeCD1
        , BunsekiCustomizeCD2
        , BunsekiCustomizeCD3
        , BunsekiCustomizeCD4
        , BunsekiCustomizeCD5
        , AiteBunsekiCD1
        , AiteBunsekiCD2
        , AiteBunsekiCD3
        , AiteBunsekiCD4
        , AiteBunsekiCD5
        , AiteSikinCD
        , AiteProjectCD
        , AiteBunsekiCustomizeCD1
        , AiteBunsekiCustomizeCD2
        , AiteBunsekiCustomizeCD3
        , AiteBunsekiCustomizeCD4
        , AiteBunsekiCustomizeCD5
        , DenpyouTekiyou
        , DenpyouListInziZumiKBN
        , CheckListInziZumiKBN
        , GuidAnken
        , ShouninStateKBN
        , KihyouUserID
        , CurrentUserID
        , ShouninUserID
        , ShouninDate
        , DenpyouSanshouRank
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
          HD.OperationComputerName
        , HD.OperationUserID
        , HD.OperationDatetime
        , HD.OperationJobID
        , HEN.ControlKey
        , ISNULL(HD.SystemDenpyouNO,0)
        , ISNULL(HD.DenpyouNO,'0')
        , HD.ShoriDate
        , HD.HasseiDate
        , HD.DenpyouTypeKBN
        , HD.SystemCategory
        , HD.SubSystemDenpyouNO
        , HD.SubShoriKBN
        , HD.SubDenpyouNO
        , HEN.SiwakeKBN 
        , HD.DenpyouKBN 
        , KEK.ZigyoushoCD
        , KEK.ZigyoushoKaiteiDate
        , HEN.KasiBumonCD
        , HEN.KasiBumonKaiteiDate
        , HEN.KasiKanjouCD
        , HEN.KasiKanjouKaiteiDate
        , HEN.KasiHojoCD
        , HEN.KasiHojoKaiteiDate
        , HEN.KasiUtiwakeCD
        , HEN.KasiUtiwakeKaiteiDate
        , HEN.KasiTorihikisakiCD
        , HEN.KasiTorihikisakiKaiteiDate
        , HEN.KasiTaishakuKBN
        , HEN.KasiHasseiKBN
        , HEN.KasiKingaku
        , HEN.KasiZeigaku
        , HEN.KasiZeiKamokuCD
        , HEN.KasiZeiKaiteiDate
        , HEN.KasiZeiHontaiCD
        , HEN.KasiZeiHontaiKaiteiDate
        , CASE WHEN HEN.KasiTaishakuKBN    = HEN.KasiHasseiKBN THEN HEN.KasiKingaku ELSE HEN.KasiKingaku * (-1) END
        , CASE WHEN HEN.KasiZeiTaishakuKBN = HEN.KasiHasseiKBN THEN HEN.KasiZeigaku ELSE HEN.KasiZeigaku * (-1) END
        , HEN.MeisaiTekiyou
        , HEN.KasiZeiKBN
        , HEN.KasiZeikomiKBN
        , HEN.KasiTorihikisakiKBN
        , HEN.KasiHojoKBN
        , HEN.KasiUtiwakeKBN
        , HEN.KasiBumonKBN
        , HEN.KariBumonCD
        , HEN.KariBumonKaiteiDate
        , HEN.KariKanjouCD
        , HEN.KariKanjouKaiteiDate
        , HEN.KariHojoCD
        , HEN.KariHojoKaiteiDate
        , HEN.KariUtiwakeCD
        , HEN.KariUtiwakeKaiteiDate
        , HEN.KariTorihikisakiCD
        , HEN.KariTorihikisakiKaiteiDate
        , HEN.KariKingaku
        , HEN.KariZeigaku
        , HEN.KariZeiKamokuCD
        , HEN.KariZeiKaiteiDate
        , HEN.KariZeiKBN
        , HEN.KariZeikomiKBN
        , HEN.GyouNO
        , HEN.KariNyuuryokuKingaku
        , HEN.KariNyuuryokuZeigaku
        , HEN.KasiNyuuryokuKingaku
        , HEN.KasiNyuuryokuZeigaku
        , HEN.KasiBunsekiCD1
        , HEN.KasiBunsekiCD2
        , HEN.KasiBunsekiCD3
        , HEN.KasiBunsekiCD4
        , HEN.KasiBunsekiCD5
        , HEN.KasiSikinCD
        , HEN.KasiProjectCD
        , HEN.KasiBunsekiCustomizeCD1
        , HEN.KasiBunsekiCustomizeCD2
        , HEN.KasiBunsekiCustomizeCD3
        , HEN.KasiBunsekiCustomizeCD4
        , HEN.KasiBunsekiCustomizeCD5
        , HEN.KariBunsekiCD1
        , HEN.KariBunsekiCD2
        , HEN.KariBunsekiCD3
        , HEN.KariBunsekiCD4
        , HEN.KariBunsekiCD5
        , HEN.KariSikinCD
        , HEN.KariProjectCD
        , HEN.KariBunsekiCustomizeCD1
        , HEN.KariBunsekiCustomizeCD2
        , HEN.KariBunsekiCustomizeCD3
        , HEN.KariBunsekiCustomizeCD4
        , HEN.KariBunsekiCustomizeCD5
        , HD.DenpyouTekiyou
        , HD.DenpyouListInziZumiKBN
        , HD.CheckListInziZumiKBN
        , HD.GuidAnken
        , HD.ShouninStateKBN
        , HD.KihyouUserID
        , HD.CurrentUserID
        , HD.ShouninUserID
        , HD.ShouninDate
        , HD.DenpyouSanshouRank
        , HD.CompanyNO
        , @p_UpdateDatetime
        , @p_UpdateCertificatedGuid
        , @p_UpdateJobID
        , @p_UpdateUserID
        , @p_UpdateComputerName
        , @p_UpdateComputerUserName
        , @p_UpdateComputerDomainName
    FROM M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Head HD
    INNER JOIN M0000WK_MA_Siwake11Hensyuu HEN
    ON  HEN.OperationComputerName  = HD.OperationComputerName
    AND HEN.OperationUserID        = HD.OperationUserID
    AND HEN.OperationJobID         = HD.OperationJobID
    INNER JOIN #M0000WK_MA_HonsitenBunkaiKekka KEK
    ON    KEK.OperationComputerName = @p_UpdateComputerName
    AND   KEK.OperationUserID       = @p_UpdateUserID
    AND   KEK.OperationJobID        = @p_UpdateJobID
    AND   KEK.DenpyouTaniNew        = HEN.ControlKey
    AND   KEK.GyouNONew             = HEN.GyouNO
    WHERE HD.OperationComputerName = @p_UpdateComputerName
    AND   HD.OperationUserID       = @p_UpdateUserID
    AND   HD.OperationJobID        = @p_UpdateJobID
    AND   HEN.KasiKanjouCD IS NOT NULL
    
    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR

    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END
    
    
    -- 総勘定科目コントロールマスタより、諸口科目コードを取得し、変数に保存する。
    SELECT TOP 1
        @ShokutiCD = ShokutiCD
    FROM M0000MS_MA_KanjouControl
    WHERE KaiteiDate <= @p_HasseiDate
    ORDER BY KaiteiDate DESC
    
    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR

    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END
    
    -- 総勘定科目マスタより、諸口科目コードの改定日を取得し、変数に保存する。
    SELECT TOP 1
        @ShokutiCDKaiteiDate = KaiteiDate
    FROM M0000MS_MA_Kanjou
    WHERE KanjouCD = @ShokutiCD
    AND   KaiteiDate <= @p_HasseiDate
    ORDER BY KaiteiDate DESC

    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR

    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END

    -- 諸口情報を仕訳明細共通ワークに更新
    UPDATE KYO SET
          AiteBumonCD                 = '*'
        , AiteBumonKaiteiDate         = NULL
        , AiteKanjouCD                = @ShokutiCD
        , AiteKanjouKaiteiDate        = @ShokutiCDKaiteiDate
        , AiteHojoCD                  = '*'
        , AiteHojoKaiteiDate          = NULL
        , AiteUtiwakeCD               = '*'
        , AiteUtiwakeKaiteiDate       = NULL
        , AiteTorihikisakiCD          = '*'
        , AiteTorihikisakiKaiteiDate  = NULL
        , AiteKingaku                 = Kingaku + Zeigaku
        , AiteZeigaku                 = 0
        , AiteZeiKamokuCD             = NULL
        , AiteZeiKaiteiDate           = NULL
        , AiteZeiKBN                  = '0'
        , AiteZeikomiKBN              = 0
        , AiteBunsekiCD1              = NULL
        , AiteBunsekiCD2              = NULL
        , AiteBunsekiCD3              = NULL
        , AiteBunsekiCD4              = NULL
        , AiteBunsekiCD5              = NULL
        , AiteSikinCD                 = NULL
        , AiteProjectCD               = NULL
        , AiteBunsekiCustomizeCD1     = NULL
        , AiteBunsekiCustomizeCD2     = NULL
        , AiteBunsekiCustomizeCD3     = NULL
        , AiteBunsekiCustomizeCD4     = NULL
        , AiteBunsekiCustomizeCD5     = NULL
    FROM M0000WK_MA_SiwakeKyoutuu KYO
    WHERE OperationComputerName = @p_UpdateComputerName
    AND   OperationUserID       = @p_UpdateUserID
    AND   OperationJobID        = @p_UpdateJobID
    AND   SiwakeKBN = 1
    
    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR

    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END
    
    -- 共通ストアド：仕訳登録処理を実行
    EXECUTE @SUB_ErrorCode = M0000PR_MA_SiwakeEntry
          @po_ErrorKBN                = @SUB_ErrorKBN        OUTPUT
        , @po_ErrorCD1                = @SUB_ErrorCD1        OUTPUT
        , @po_ErrorCD2                = @SUB_ErrorCD2        OUTPUT
        , @po_ErrorProcName           = @SUB_ErrorProcName   OUTPUT
        , @po_ErrorSummary            = @SUB_ErrorSummary    OUTPUT
        , @po_ErrorDetail             = @SUB_ErrorDetail     OUTPUT
        , @po_ErrorBtnKBN             = @SUB_ErrorBtnKBN     OUTPUT
        , @po_ErrorBtnDefault         = @SUB_ErrorBtnDefault OUTPUT
        , @po_SystemDenpyouNO         = @po_SystemDenpyouNO  OUTPUT
        , @po_DenpyouNO               = @po_DenpyouNO        OUTPUT
        , @p_ProgramKubun             = 3
        , @p_ShoriMode                = @p_ShoriMode
        , @p_DenpyouSaibanHouhou      = @p_DenpyouSaibanHouhou
        , @p_DenpyouCountKeta         = @p_DenpyouCountKeta
        , @p_ZigyoushoCD              = @p_ZigyoushoCD
        , @p_HasseiDate               = @p_HasseiDate
        , @p_DenpyouKBN               = @p_DenpyouKBN
        , @p_ShuuseimaeDenpyouKBN     = @p_ShuuseimaeDenpyouKBN
        -- ↓ 2016/10/17 M.Tamura INS [47349]分析コード機能強化対応(残高更新)
        , @p_BunsekibetuZandakaSiyouKBN = @p_BunsekibetuZandakaSiyouKBN
        -- ↑ 2016/10/17 M.Tamura INS [47349]分析コード機能強化対応(残高更新)
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

    -- 変数.サブエラー区分 <>  の時
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




    -- 修正時、呼出したシステム伝票番号で、修正前伝票を削除
    IF @p_ShoriMode = 2
    BEGIN
        -- 本支店明細データの修正前伝票削除
        DELETE FROM M0000RR_MA_HonsitenSiwakeMeisai
        WHERE SystemDenpyouNO       = @po_SystemDenpyouNO

        -- @@ERROR を取得し、変数に保存する。
        SET @ErrorCode = @@ERROR

        -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
        IF  @ErrorCode <> 0
        BEGIN
            GOTO ERR_SHORI
        END
        
        --↓2013/09/04 追加 M.Shinbo GX対応(M00020:メモ機能の実装) 開始
        -- メモ情報データ削除
        DELETE FROM M0000RR_MA_Memo
        WHERE SystemDenpyouNO       = @po_SystemDenpyouNO

        -- @@ERROR を取得し、変数に保存する。
        SET @ErrorCode = @@ERROR

        -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
        IF  @ErrorCode <> 0
        BEGIN
            GOTO ERR_SHORI
        END
        
        -- 連携メモ情報データ削除
        DELETE FROM M0000RR_MA_RenkeiMemo
        WHERE SystemDenpyouNO       = @po_SystemDenpyouNO

        -- @@ERROR を取得し、変数に保存する。
        SET @ErrorCode = @@ERROR

        -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
        IF  @ErrorCode <> 0
        BEGIN
            GOTO ERR_SHORI
        END
        
        -- 本支店メモ情報データ削除
        DELETE FROM M0000RR_MA_HonsitenMemo
        WHERE SystemDenpyouNO       = @po_SystemDenpyouNO

        -- @@ERROR を取得し、変数に保存する。
        SET @ErrorCode = @@ERROR

        -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
        IF  @ErrorCode <> 0
        BEGIN
            GOTO ERR_SHORI
        END
        --↑2013/09/04 追加 M.Shinbo GX対応(M00020:メモ機能の実装) 終了
    END
    
    -- 本支店入力画面ヘッダ/明細ワークから本支店仕訳明細データへINSERT
    INSERT INTO M0000RR_MA_HonsitenSiwakeMeisai (
          SystemDenpyouNO
        , DenpyouNO
        , ShoriDate
        , HasseiDate
        , DenpyouTypeKBN
        , SystemCategory
        , SubSystemDenpyouNO
        , SubShoriKBN
        , SubDenpyouNO
        , SiwakeKBN
        , DenpyouKBN
        , ZigyoushoCD
        , ZigyoushoKaiteiDate
        , KariZigyoushoCD
        , KariZigyoushoKaiteiDate
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
        , KariKingaku
        , KariZeigaku
        , KariZeiKamokuCD
        , KariZeiKaiteiDate
        , KariZeiHontaiCD
        , KariZeiHontaiKaiteiDate
        , KariZeiKBN
        , KariZeikomiKBN
        , KariTorihikisakiKBN
        , KariHojoKBN
        , KariUtiwakeKBN
        , KariBumonKBN
        , MeisaiTekiyou
        , KasiZigyoushoCD
        , KasiZigyoushoKaiteiDate
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
        , KasiKingaku
        , KasiZeigaku
        , KasiZeiKamokuCD
        , KasiZeiKaiteiDate
        , KasiZeiHontaiCD
        , KasiZeiHontaiKaiteiDate
        , KasiZeiKBN
        , KasiZeikomiKBN
        , KasiTorihikisakiKBN
        , KasiHojoKBN
        , KasiUtiwakeKBN
        , KasiBumonKBN
        , GyouNO
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
        , DenpyouTekiyou
        , DenpyouListInziZumiKBN
        , CheckListInziZumiKBN
        , GuidAnken
        , ShouninStateKBN
        , KihyouUserID
        , CurrentUserID
        , ShouninUserID
        , ShouninDate
        , DenpyouSanshouRank
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
          @po_SystemDenpyouNO
        , @po_DenpyouNO
        , HED.ShoriDate
        , HED.HasseiDate
        , HED.DenpyouTypeKBN
        , HED.SystemCategory
        , HED.SubSystemDenpyouNO
        , HED.SubShoriKBN
        , HED.SubDenpyouNO
        , MEI.SiwakeKBN
        , HED.DenpyouKBN 
        , HED.ZigyoushoCD
        , HED.ZigyoushoKaiteiDate
        , MEI.KariZigyoushoCD
        , MEI.KariZigyoushoKaiteiDate
        , MEI.KariBumonCD
        , MEI.KariBumonKaiteiDate
        , MEI.KariKanjouCD
        , MEI.KariKanjouKaiteiDate
        , MEI.KariHojoCD
        , MEI.KariHojoKaiteiDate
        , MEI.KariUtiwakeCD
        , MEI.KariUtiwakeKaiteiDate
        , MEI.KariTorihikisakiCD
        , MEI.KariTorihikisakiKaiteiDate
        , MEI.KariTaishakuKBN
        , MEI.KariKingaku
        , MEI.KariZeigaku
        , MEI.KariZeiKamokuCD
        , MEI.KariZeiKaiteiDate
        , MEI.KariZeiHontaiCD
        , MEI.KariZeiHontaiKaiteiDate
        , ISNULL(MEI.KariZeiKBN,'0')
        , ISNULL(MEI.KariZeikomiKBN,0)
        , MEI.KariTorihikisakiKBN
        , MEI.KariHojoKBN
        , MEI.KariUtiwakeKBN
        , MEI.KariBumonKBN
        , MEI.MeisaiTekiyou
        , MEI.KasiZigyoushoCD
        , MEI.KasiZigyoushoKaiteiDate
        , MEI.KasiBumonCD
        , MEI.KasiBumonKaiteiDate
        , MEI.KasiKanjouCD
        , MEI.KasiKanjouKaiteiDate
        , MEI.KasiHojoCD
        , MEI.KasiHojoKaiteiDate
        , MEI.KasiUtiwakeCD
        , MEI.KasiUtiwakeKaiteiDate
        , MEI.KasiTorihikisakiCD
        , MEI.KasiTorihikisakiKaiteiDate
        , MEI.KasiTaishakuKBN
        , MEI.KasiKingaku
        , MEI.KasiZeigaku
        , MEI.KasiZeiKamokuCD
        , MEI.KasiZeiKaiteiDate
        , MEI.KasiZeiHontaiCD
        , MEI.KasiZeiHontaiKaiteiDate
        , ISNULL(MEI.KasiZeiKBN,'0')
        , ISNULL(MEI.KasiZeikomiKBN,0)
        , MEI.KasiTorihikisakiKBN
        , MEI.KasiHojoKBN
        , MEI.KasiUtiwakeKBN
        , MEI.KasiBumonKBN
        , MEI.GyouNO        
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
        , HED.DenpyouTekiyou
        , HED.DenpyouListInziZumiKBN
        , HED.CheckListInziZumiKBN
        , HED.GuidAnken
        , HED.ShouninStateKBN
        , HED.KihyouUserID
        , HED.CurrentUserID
        , HED.ShouninUserID
        , HED.ShouninDate
        , HED.DenpyouSanshouRank
        , HED.CompanyNO
        , @p_UpdateDatetime
        , @p_UpdateCertificatedGuid
        , @p_UpdateJobID
        , @p_UpdateUserID
        , @p_UpdateComputerName
        , @p_UpdateComputerUserName
        , @p_UpdateComputerDomainName    
    FROM M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Head HED
    INNER JOIN M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Meisai MEI
    ON  MEI.OperationComputerName  = HED.OperationComputerName
    AND MEI.OperationUserID        = HED.OperationUserID
    AND MEI.OperationJobID         = HED.OperationJobID
    WHERE HED.OperationComputerName = @p_UpdateComputerName
    AND   HED.OperationUserID       = @p_UpdateUserID
    AND   HED.OperationJobID        = @p_UpdateJobID
    
    
    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR

    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END

    --↓2013/09/04 追加 M.Shinbo GX対応(M00020:メモ機能の実装) 開始
    
    --連携メモ情報データへ更新する連携受入順番号を取得する
    INSERT INTO #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Zigyousho
    (
          ZigyoushoCD
        , RenkeiUkeireJunNO
    )
    SELECT
          ZigyoushoCD
        , RenkeiUkeireJunNO
    FROM  M0000RR_MA_RenkeiSiwakeMeisai REN
    WHERE REN.SystemDenpyouNO   =  @po_SystemDenpyouNO
    AND   REN.DenpyouNO         =  @po_DenpyouNO
    AND   REN.ZigyoushoCD       <> @p_ZigyoushoCD
    GROUP BY REN.ZigyoushoCD, REN.RenkeiUkeireJunNO
    ORDER BY REN.ZigyoushoCD, REN.RenkeiUkeireJunNO
    
    -- @@ERROR を取得し、変数に保存する。
    SET @ErrorCode = @@ERROR

    -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
    IF  @ErrorCode <> 0
    BEGIN
        GOTO ERR_SHORI
    END
    
    
    --貸借どちらにもメモが登録されていない場合は、メモデータへ更新しない。
    
    DECLARE @MemoCount int      --メモデータ登録数
    
    SELECT @MemoCount = COUNT(HEN.ControlKey)
    FROM  M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Head HD
    INNER JOIN M0000WK_MA_Siwake11Hensyuu HEN
    ON    HEN.OperationComputerName = HD.OperationComputerName
    AND   HEN.OperationUserID       = HD.OperationUserID
    AND   HEN.OperationJobID        = HD.OperationJobID
    WHERE HD.OperationComputerName  = @p_UpdateComputerName
    AND   HD.OperationUserID        = @p_UpdateUserID
    AND   HD.OperationJobID         = @p_UpdateJobID
    AND   (HEN.KariRValue IS NOT NULL  OR  HEN.KasiRValue IS NOT NULL)
    
    -- メモデータ登録数 が 0件でない場合
    IF @MemoCount <> 0
    BEGIN
    
        --メモ情報データへINSERT
        
        -- 借方の内容追加
        INSERT INTO M0000RR_MA_Memo
        (
              SystemDenpyouNO
            , DenpyouNO
            , HasseiKBN
            , GyouNO
            , RValue
            , GValue
            , BValue
            , Memo
            , CompanyNO
            , UpdateDatetime
            , UpdateCertificatedGuid
            , UpdateJobID
            , UpdateUserID
            , UpdateComputerName
            , UpdateComputerUserName
            , UpdateComputerDomainName
            , AiteRValue
            , AiteGValue
            , AiteBValue
            , AiteMemo
        )
        SELECT
             @po_SystemDenpyouNO
            ,@po_DenpyouNO
            ,HEN.KariHasseiKBN
            ,HEN.GyouNO
            ,HEN.KariRValue
            ,HEN.KariGValue
            ,HEN.KariBValue
            ,HEN.KariMemo
            ,HEN.CompanyNO
            ,@p_UpdateDatetime
            ,@p_UpdateCertificatedGuid
            ,@p_UpdateJobID
            ,@p_UpdateUserID
            ,@p_UpdateComputerName
            ,@p_UpdateComputerUserName
            ,@p_UpdateComputerDomainName
            ,HEN.KasiRValue
            ,HEN.KasiGValue
            ,HEN.KasiBValue
            ,HEN.KasiMemo
        FROM M0000WK_MA_Siwake11Hensyuu HEN
        INNER JOIN #M0000WK_MA_HonsitenBunkaiKekka KEK
        ON    KEK.OperationComputerName = HEN.OperationComputerName
        AND   KEK.OperationUserID       = HEN.OperationUserID      
        AND   KEK.OperationJobID        = HEN.OperationJobID       
        AND   KEK.DenpyouTaniNew        = HEN.ControlKey
        AND   KEK.GyouNONew             = HEN.GyouNO
        WHERE HEN.OperationComputerName = @p_UpdateComputerName
        AND   HEN.OperationUserID       = @p_UpdateUserID
        AND   HEN.OperationJobID        = @p_UpdateJobID
        AND   HEN.KariKanjouCD IS NOT NULL
        AND   KEK.ZigyoushoCD = @p_ZigyoushoCD      --入力事業所分のみ

        -- @@ERROR を取得し、変数に保存する。
        SET @ErrorCode = @@ERROR

        -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
        IF  @ErrorCode <> 0
        BEGIN
            GOTO ERR_SHORI
        END

        -- 貸方の内容追加
        INSERT INTO M0000RR_MA_Memo
        (
              SystemDenpyouNO
            , DenpyouNO
            , HasseiKBN
            , GyouNO
            , RValue
            , GValue
            , BValue
            , Memo
            , CompanyNO
            , UpdateDatetime
            , UpdateCertificatedGuid
            , UpdateJobID
            , UpdateUserID
            , UpdateComputerName
            , UpdateComputerUserName
            , UpdateComputerDomainName
            , AiteRValue
            , AiteGValue
            , AiteBValue
            , AiteMemo
        )
        SELECT
             @po_SystemDenpyouNO
            ,@po_DenpyouNO
            ,HEN.KasiHasseiKBN
            ,HEN.GyouNO
            ,HEN.KasiRValue
            ,HEN.KasiGValue
            ,HEN.KasiBValue
            ,HEN.KasiMemo
            ,HEN.CompanyNO
            ,@p_UpdateDatetime
            ,@p_UpdateCertificatedGuid
            ,@p_UpdateJobID
            ,@p_UpdateUserID
            ,@p_UpdateComputerName
            ,@p_UpdateComputerUserName
            ,@p_UpdateComputerDomainName
            ,HEN.KariRValue
            ,HEN.KariGValue
            ,HEN.KariBValue
            ,HEN.KariMemo
        FROM M0000WK_MA_Siwake11Hensyuu HEN
        INNER JOIN #M0000WK_MA_HonsitenBunkaiKekka KEK
        ON    KEK.OperationComputerName = HEN.OperationComputerName
        AND   KEK.OperationUserID       = HEN.OperationUserID      
        AND   KEK.OperationJobID        = HEN.OperationJobID       
        AND   KEK.DenpyouTaniNew        = HEN.ControlKey
        AND   KEK.GyouNONew             = HEN.GyouNO
        WHERE HEN.OperationComputerName = @p_UpdateComputerName
        AND   HEN.OperationUserID       = @p_UpdateUserID
        AND   HEN.OperationJobID        = @p_UpdateJobID
        AND   HEN.KasiKanjouCD IS NOT NULL
        AND   KEK.ZigyoushoCD = @p_ZigyoushoCD      --入力事業所分のみ

        -- @@ERROR を取得し、変数に保存する。
        SET @ErrorCode = @@ERROR

        -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
        IF  @ErrorCode <> 0
        BEGIN
            GOTO ERR_SHORI
        END
        
        
        --連携メモ情報データへINSERT
        
        -- 借方の内容追加
        INSERT INTO M0000RR_MA_RenkeiMemo
        (
              SystemDenpyouNO
            , DenpyouNO
            , ZigyoushoCD
            , HasseiKBN
            , GyouNO
            , RValue
            , GValue
            , BValue
            , Memo
            , AiteRValue
            , AiteGValue
            , AiteBValue
            , AiteMemo
            , RenkeiUkeireJunNO
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
             @po_SystemDenpyouNO
            ,@po_DenpyouNO
            ,KEK.ZigyoushoCD
            ,HEN.KariHasseiKBN
            ,HEN.GyouNO
            ,HEN.KariRValue
            ,HEN.KariGValue
            ,HEN.KariBValue
            ,HEN.KariMemo
            ,HEN.KasiRValue
            ,HEN.KasiGValue
            ,HEN.KasiBValue
            ,HEN.KasiMemo
            ,ZIG.RenkeiUkeireJunNO
            ,HEN.CompanyNO
            ,@p_UpdateDatetime
            ,@p_UpdateCertificatedGuid
            ,@p_UpdateJobID
            ,@p_UpdateUserID
            ,@p_UpdateComputerName
            ,@p_UpdateComputerUserName
            ,@p_UpdateComputerDomainName
        FROM M0000WK_MA_Siwake11Hensyuu HEN
        INNER JOIN #M0000WK_MA_HonsitenBunkaiKekka KEK
        ON    KEK.OperationComputerName     = HEN.OperationComputerName
        AND   KEK.OperationUserID           = HEN.OperationUserID
        AND   KEK.OperationJobID            = HEN.OperationJobID
        AND   KEK.DenpyouTaniNew            = HEN.ControlKey
        AND   KEK.GyouNONew                 = HEN.GyouNO
        INNER JOIN #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Zigyousho ZIG
        ON    ZIG.ZigyoushoCD               = KEK.ZigyoushoCD
        WHERE HEN.OperationComputerName     = @p_UpdateComputerName
        AND   HEN.OperationUserID           = @p_UpdateUserID
        AND   HEN.OperationJobID            = @p_UpdateJobID
        AND   HEN.KariKanjouCD IS NOT NULL
        
        -- @@ERROR を取得し、変数に保存する。
        SET @ErrorCode = @@ERROR

        -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
        IF  @ErrorCode <> 0
        BEGIN
            GOTO ERR_SHORI
        END
        
        -- 貸方の内容追加
        INSERT INTO M0000RR_MA_RenkeiMemo
        (
              SystemDenpyouNO
            , DenpyouNO
            , ZigyoushoCD
            , HasseiKBN
            , GyouNO
            , RValue
            , GValue
            , BValue
            , Memo
            , AiteRValue
            , AiteGValue
            , AiteBValue
            , AiteMemo
            , RenkeiUkeireJunNO
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
             @po_SystemDenpyouNO
            ,@po_DenpyouNO
            ,KEK.ZigyoushoCD
            ,HEN.KasiHasseiKBN
            ,HEN.GyouNO
            ,HEN.KasiRValue
            ,HEN.KasiGValue
            ,HEN.KasiBValue
            ,HEN.KasiMemo
            ,HEN.KariRValue
            ,HEN.KariGValue
            ,HEN.KariBValue
            ,HEN.KariMemo
            ,ZIG.RenkeiUkeireJunNO
            ,HEN.CompanyNO
            ,@p_UpdateDatetime
            ,@p_UpdateCertificatedGuid
            ,@p_UpdateJobID
            ,@p_UpdateUserID
            ,@p_UpdateComputerName
            ,@p_UpdateComputerUserName
            ,@p_UpdateComputerDomainName
        FROM M0000WK_MA_Siwake11Hensyuu HEN
        INNER JOIN #M0000WK_MA_HonsitenBunkaiKekka KEK
        ON    KEK.OperationComputerName     = HEN.OperationComputerName
        AND   KEK.OperationUserID           = HEN.OperationUserID
        AND   KEK.OperationJobID            = HEN.OperationJobID
        AND   KEK.DenpyouTaniNew            = HEN.ControlKey
        AND   KEK.GyouNONew                 = HEN.GyouNO
        INNER JOIN #M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Zigyousho ZIG
        ON    ZIG.ZigyoushoCD               = KEK.ZigyoushoCD
        WHERE HEN.OperationComputerName      = @p_UpdateComputerName
        AND   HEN.OperationUserID            = @p_UpdateUserID
        AND   HEN.OperationJobID             = @p_UpdateJobID
        AND   HEN.KasiKanjouCD IS NOT NULL
        
        
        -- @@ERROR を取得し、変数に保存する。
        SET @ErrorCode = @@ERROR

        -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
        IF  @ErrorCode <> 0
        BEGIN
            GOTO ERR_SHORI
        END
        
    
        --本支店メモ情報データへINSERT
        
        INSERT INTO M0000RR_MA_HonsitenMemo
        (
              SystemDenpyouNO
            , DenpyouNO
            , GyouNO
            , KariRValue
            , KariGValue
            , KariBValue
            , KariMemo
            , KasiRValue
            , KasiGValue
            , KasiBValue
            , KasiMemo
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
             @po_SystemDenpyouNO
            ,@po_DenpyouNO
            ,MEI.GyouNO
            ,MEM.KariRValue
            ,MEM.KariGValue
            ,MEM.KariBValue
            ,MEM.KariMemo
            ,MEM.KasiRValue
            ,MEM.KasiGValue
            ,MEM.KasiBValue
            ,MEM.KasiMemo
            ,HED.CompanyNO
            ,@p_UpdateDatetime
            ,@p_UpdateCertificatedGuid
            ,@p_UpdateJobID
            ,@p_UpdateUserID
            ,@p_UpdateComputerName
            ,@p_UpdateComputerUserName
            ,@p_UpdateComputerDomainName
        FROM M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Meisai MEI
        INNER JOIN M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Head HED
        ON    HED.OperationComputerName = MEI.OperationComputerName
        AND   HED.OperationUserID       = MEI.OperationUserID
        AND   HED.OperationJobID        = MEI.OperationJobID
        LEFT JOIN M0000WK_MA_HonsitenMemo MEM
        ON    MEM.OperationComputerName = HED.OperationComputerName
        AND   MEM.OperationUserID       = HED.OperationUserID
        AND   MEM.OperationJobID        = HED.OperationJobID
        AND   MEM.GyouNO                = MEI.GyouNO
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
    
    END
    --↑2013/09/04 追加 M.Shinbo GX対応(M00020:メモ機能の実装) 終了

    --2016/10/11 追加 H.Fukuda (47267)【仮勘定消込】消込済伝票の修正・削除を制御する  ↓
    -- 引数.処理モード = 2：修正時
    IF @p_ShoriMode = 2
    BEGIN
        DELETE FROM M0000RR_MA_Kesikomi
        WHERE SystemDenpyouNO = @po_SystemDenpyouNO
        
        -- @@ERROR を取得し、変数に保存する。
        SET @ErrorCode = @@ERROR
        
        -- 変数.エラーコード <> 0 の時、ラベル：ERR_SHORIに分岐する。
        IF  @ErrorCode <> 0
        BEGIN
            GOTO ERR_SHORI
        END
    END
    --2016/10/11 追加 H.Fukuda (47267)【仮勘定消込】消込済伝票の修正・削除を制御する  ↑

    -- 仕訳11編集用ワークの前回データ削除
    DELETE FROM M0000WK_MA_Siwake11Hensyuu
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
    
    -- 本支店仕訳入力画面ヘッダワークの前回データ削除
    DELETE FROM M0000WK_Obic7fx_Win_M0000_Input_MA_HonsitenSiwake_Head
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

    -- 本支店仕訳入力画面明細ワークの前回データ削除
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
    
    --↓2013/09/04 追加 M.Shinbo GX対応(M00020:メモ機能の実装) 開始
    -- 本支店メモ情報ワークの前回データ削除
    DELETE FROM M0000WK_MA_HonsitenMemo
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
    --↑2013/09/04 追加 M.Shinbo GX対応(M00020:メモ機能の実装) 終了
    
    
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

    
