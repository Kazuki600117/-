/*
******************************************************************************
** 名前：事業所別制御マスタ（カスタマイズSEQ 5525）
** 概要：財務会計の各種制御項目を事業所ごとに保持するアドオンテーブル。
**       ・消費税計算方式(売上・仕入 × R5.9.30以前・R5.10.1以後)
**           消費税申告管理(M5525)で出力区分＝事業所の場合に使用する。
**           NULL の区分は財務コントロールマスタ(M0000MS_MA_Control)の値を使用する。
**       ・取引先コード
** 対象：
**       M5525MS_MA_ZigyoushoSeigyo         事業所別制御マスタ
**       M5525MS_MA_ZigyoushoSeigyoHistory  事業所別制御マスタ変更履歴
**       TR_M5525MS_MA_ZigyoushoSeigyo_IUD  変更履歴トリガ
***************************************************************************************************
**      履歴
**              2026/10/11 新規作成  (5525)事業所別消費税計算方式対応
**
***************************************************************************************************
*/

-------------------------------------------------------------------------------
-- 事業所別制御マスタ
-------------------------------------------------------------------------------
if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[M5525MS_MA_ZigyoushoSeigyo]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[M5525MS_MA_ZigyoushoSeigyo] (
      ZigyoushoCD                     varchar(6)          NOT NULL -- 事業所コード
    , UriageZeiTumiageKBN             tinyint             NULL     -- R5.9.30以前売上消費税計算方式区分(0:割戻方式、1:積上方式、NULL:財務コントロールマスタに従う)
    , SiireZeiTumiageKBN              tinyint             NULL     -- R5.9.30以前仕入消費税計算方式区分(同上)
    , R5NenOctIgoUriageZeiTumiageKBN  tinyint             NULL     -- R5.10.1以後売上消費税計算方式区分(同上)
    , R5NenOctIgoSiireZeiTumiageKBN   tinyint             NULL     -- R5.10.1以後仕入消費税計算方式区分(同上)
    , TorihikisakiCD                  varchar(10)         NULL     -- 取引先コード
    , CompanyNO                       int                 NOT NULL -- 会社NO
    , UpdateTimestamp                 timestamp           NOT NULL -- 更新タイムスタンプ
    , UpdateDatetime                  datetime            NOT NULL -- 更新日時
    , UpdateCertificatedGuid          uniqueidentifier    NOT NULL -- 更新認証済Guid
    , UpdateJobID                     varchar(128)        NOT NULL -- 更新JobID
    , UpdateUserID                    varchar(128)        NOT NULL -- 更新ユーザID
    , UpdateComputerName              varchar(128)        NOT NULL -- 更新PC名
    , UpdateComputerUserName          varchar(128)        NOT NULL -- 更新PCユーザ名
    , UpdateComputerDomainName        varchar(256)        NOT NULL -- 更新PCドメイン名
    , CONSTRAINT PK_M5525MS_MA_ZigyoushoSeigyo PRIMARY KEY CLUSTERED (ZigyoushoCD)
    , CONSTRAINT CK_M5525MS_MA_ZigyoushoSeigyo_UriageZeiTumiageKBN CHECK (UriageZeiTumiageKBN IN (0, 1))
    , CONSTRAINT CK_M5525MS_MA_ZigyoushoSeigyo_SiireZeiTumiageKBN CHECK (SiireZeiTumiageKBN IN (0, 1))
    , CONSTRAINT CK_M5525MS_MA_ZigyoushoSeigyo_R5NenOctIgoUriageZeiTumiageKBN CHECK (R5NenOctIgoUriageZeiTumiageKBN IN (0, 1))
    , CONSTRAINT CK_M5525MS_MA_ZigyoushoSeigyo_R5NenOctIgoSiireZeiTumiageKBN CHECK (R5NenOctIgoSiireZeiTumiageKBN IN (0, 1))
    , CONSTRAINT CK_M5525MS_MA_ZigyoushoSeigyo_TorihikisakiCD CHECK (TorihikisakiCD <> '')
)
END
GO

-------------------------------------------------------------------------------
-- 事業所別制御マスタ変更履歴
-------------------------------------------------------------------------------
if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[M5525MS_MA_ZigyoushoSeigyoHistory]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[M5525MS_MA_ZigyoushoSeigyoHistory] (
      ZigyoushoCD                     varchar(6)          NOT NULL -- 事業所コード
    , UriageZeiTumiageKBN             tinyint             NULL     -- R5.9.30以前売上消費税計算方式区分(0:割戻方式、1:積上方式、NULL:財務コントロールマスタに従う)
    , SiireZeiTumiageKBN              tinyint             NULL     -- R5.9.30以前仕入消費税計算方式区分(同上)
    , R5NenOctIgoUriageZeiTumiageKBN  tinyint             NULL     -- R5.10.1以後売上消費税計算方式区分(同上)
    , R5NenOctIgoSiireZeiTumiageKBN   tinyint             NULL     -- R5.10.1以後仕入消費税計算方式区分(同上)
    , TorihikisakiCD                  varchar(10)         NULL     -- 取引先コード
    , CompanyNO                       int                 NOT NULL -- 会社NO
    , UpdateTimestamp                 binary(8)           NOT NULL -- 更新タイムスタンプ
    , UpdateDatetime                  datetime            NOT NULL -- 更新日時
    , UpdateCertificatedGuid          uniqueidentifier    NOT NULL -- 更新認証済Guid
    , UpdateJobID                     varchar(128)        NOT NULL -- 更新JobID
    , UpdateUserID                    varchar(128)        NOT NULL -- 更新ユーザID
    , UpdateComputerName              varchar(128)        NOT NULL -- 更新PC名
    , UpdateComputerUserName          varchar(128)        NOT NULL -- 更新PCユーザ名
    , UpdateComputerDomainName        varchar(256)        NOT NULL -- 更新PCドメイン名
    , HistoryOperationName            varchar(10)         NOT NULL -- 履歴操作名
    , HistoryTimestamp                timestamp           NOT NULL -- 履歴タイムスタンプ
    , HistoryDatetime                 datetime            NOT NULL -- 履歴日時
    , HistoryCertificatedGuid         uniqueidentifier    NOT NULL -- 履歴認証済Guid
    , HistoryJobID                    varchar(128)        NOT NULL -- 履歴JobID
    , HistoryUserID                   varchar(128)        NOT NULL -- 履歴ユーザID
    , HistoryComputerName             varchar(128)        NOT NULL -- 履歴PC名
    , HistoryComputerUserName         varchar(128)        NOT NULL -- 履歴PCユーザ名
    , HistoryComputerDomainName       varchar(256)        NOT NULL -- 履歴PCドメイン名
)

CREATE CLUSTERED INDEX IX_M5525MS_MA_ZigyoushoSeigyoHistory_01
    ON [dbo].[M5525MS_MA_ZigyoushoSeigyoHistory] (HistoryDatetime)
END
GO

-------------------------------------------------------------------------------
-- 変更履歴トリガ（標準 TR_N0000MS_NA_SisanZigyousho_IUD と同じ作りとする）
-------------------------------------------------------------------------------
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[TR_M5525MS_MA_ZigyoushoSeigyo_IUD]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[TR_M5525MS_MA_ZigyoushoSeigyo_IUD]
GO

CREATE TRIGGER [dbo].[TR_M5525MS_MA_ZigyoushoSeigyo_IUD]
ON [dbo].[M5525MS_MA_ZigyoushoSeigyo]
AFTER INSERT,UPDATE,DELETE
AS
/*
*********************************************************************
** 名前　:事業所別制御マスタの履歴作成トリガー
** 機能　:[M5525MS_MA_ZigyoushoSeigyo]の更新履歴を[M5525MS_MA_ZigyoushoSeigyoHistory]に格納する
** 作成日:2026/10/11
** 作成者:(5525)事業所別消費税計算方式対応
*********************************************************************
** 修正・変更の履歴
** 日付         作業者    作業ID       作業内容
** ------------------------------------------------------------------
** YYYY/MM/DD   氏名      XXXXXXXXXX   改定内容の概要
*********************************************************************
*/
BEGIN
    IF  @@ROWCOUNT = 0 RETURN   --１件も処理分がない場合は即終了
    SET NOCOUNT ON

    --履歴出力用の変数の定義と初期化
    DECLARE @IsNotUseTrigger    AS bit
    DECLARE @HistoryGuid        AS uniqueidentifier
    DECLARE @HistoryJobID       AS varchar(128)
    DECLARE @HistoryUserName    AS varchar(128)
    DECLARE @ComputerName       AS varchar(128)
    DECLARE @ComputerUserName   AS varchar(128)
    DECLARE @ComputerDomainName AS varchar(256)

    SELECT
            @IsNotUseTrigger    = IsNotUseTrigger
           ,@HistoryGuid        = HistoryGuid
           ,@HistoryJobID       = HistoryJobID
           ,@HistoryUserName    = HistoryUserName
           ,@ComputerName       = ComputerName
           ,@ComputerUserName   = ComputerUserName
           ,@ComputerDomainName = ComputerDomainName
    FROM [dbo].[A0000FC_GetHistoryUserInfo]()
    --トリガ無効フラグが「1」であれば履歴出力処理を行わない --> 実装は以下のように =0 なら履歴出力する
    IF (@IsNotUseTrigger = 0)
    BEGIN
        ----------------------------------------------------------------------------
        -- ユーザー操作(INSERT/UPDATE/DELETE)の判定
        DECLARE @OperationName varchar(10)
        DECLARE @Deleted       int
        DECLARE @Inserted      int

        -- 削除数, 挿入数を調べる
        SELECT @Deleted  = COUNT(*) FROM [Deleted]
        SELECT @Inserted = COUNT(*) FROM [Inserted]

        -- タイプを決定する
        IF @Deleted = 0
            SET @OperationName     = 'Insert' -- 挿入
        ELSE
        BEGIN
            IF @Inserted = 0
                SET @OperationName = 'Delete' -- 削除
            ELSE
                SET @OperationName = 'Update' -- 更新
        END

        --==========================================================================
        -- INSERT OR UPADTE 操作時
        ----------------------------------------------------------------------------
        IF ( @OperationName = 'Insert' ) OR  -- 挿入だったら
           ( @OperationName = 'Update' )     -- 更新
        BEGIN
            INSERT INTO [M5525MS_MA_ZigyoushoSeigyoHistory] --履歴レコード格納テーブル名をセット
            (
                  [ZigyoushoCD]
                , [UriageZeiTumiageKBN]
                , [SiireZeiTumiageKBN]
                , [R5NenOctIgoUriageZeiTumiageKBN]
                , [R5NenOctIgoSiireZeiTumiageKBN]
                , [TorihikisakiCD]
                , [CompanyNO]
                , [UpdateTimestamp]
                , [UpdateDatetime]
                , [UpdateCertificatedGuid]
                , [UpdateJobID]
                , [UpdateUserID]
                , [UpdateComputerName]
                , [UpdateComputerUserName]
                , [UpdateComputerDomainName]
                , [HistoryOperationName]
                , [HistoryDatetime]
                , [HistoryCertificatedGuid]
                , [HistoryJobID]
                , [HistoryUserID]
                , [HistoryComputerName]
                , [HistoryComputerUserName]
                , [HistoryComputerDomainName]
            )
            SELECT
                  [ZigyoushoCD]
                , [UriageZeiTumiageKBN]
                , [SiireZeiTumiageKBN]
                , [R5NenOctIgoUriageZeiTumiageKBN]
                , [R5NenOctIgoSiireZeiTumiageKBN]
                , [TorihikisakiCD]
                , [CompanyNO]
                , [UpdateTimestamp]
                , [UpdateDatetime]
                , [UpdateCertificatedGuid]
                , [UpdateJobID]
                , [UpdateUserID]
                , [UpdateComputerName]
                , [UpdateComputerUserName]
                , [UpdateComputerDomainName]
                , @OperationName
                , getdate()
                , @HistoryGuid
                , @HistoryJobID
                , @HistoryUserName
                , @ComputerName
                , @ComputerUserName
                , @ComputerDomainName
            FROM inserted
        END
        --==========================================================================
        -- DELETE 操作時
        ----------------------------------------------------------------------------
        IF ( @OperationName = 'Delete' )       -- 削除
        BEGIN
            INSERT INTO [M5525MS_MA_ZigyoushoSeigyoHistory] --履歴レコード格納テーブル名をセット
            (
                  [ZigyoushoCD]
                , [UriageZeiTumiageKBN]
                , [SiireZeiTumiageKBN]
                , [R5NenOctIgoUriageZeiTumiageKBN]
                , [R5NenOctIgoSiireZeiTumiageKBN]
                , [TorihikisakiCD]
                , [CompanyNO]
                , [UpdateTimestamp]
                , [UpdateDatetime]
                , [UpdateCertificatedGuid]
                , [UpdateJobID]
                , [UpdateUserID]
                , [UpdateComputerName]
                , [UpdateComputerUserName]
                , [UpdateComputerDomainName]
                , [HistoryOperationName]
                , [HistoryDatetime]
                , [HistoryCertificatedGuid]
                , [HistoryJobID]
                , [HistoryUserID]
                , [HistoryComputerName]
                , [HistoryComputerUserName]
                , [HistoryComputerDomainName]
            )
            SELECT
                  [ZigyoushoCD]
                , [UriageZeiTumiageKBN]
                , [SiireZeiTumiageKBN]
                , [R5NenOctIgoUriageZeiTumiageKBN]
                , [R5NenOctIgoSiireZeiTumiageKBN]
                , [TorihikisakiCD]
                , [CompanyNO]
                , [UpdateTimestamp]
                , [UpdateDatetime]
                , [UpdateCertificatedGuid]
                , [UpdateJobID]
                , [UpdateUserID]
                , [UpdateComputerName]
                , [UpdateComputerUserName]
                , [UpdateComputerDomainName]
                , @OperationName
                , getdate()
                , @HistoryGuid
                , @HistoryJobID
                , @HistoryUserName
                , @ComputerName
                , @ComputerUserName
                , @ComputerDomainName
            FROM deleted
        END
    END
END
GO
