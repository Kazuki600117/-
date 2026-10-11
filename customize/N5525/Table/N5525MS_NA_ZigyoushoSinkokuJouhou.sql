/*
******************************************************************************
** 名前：事業所別地方税申告情報マスタ（カスタマイズSEQ 5525）
** 概要：固定資産コントロールマスタ（N0000MS_NA_KoteiSisanControl）の
**       「地方税申告（連携）」項目を事業所ごとに保持するアドオンテーブル。
**       償却資産申告書（N5525）の eLTax ファイル出力で、出力区分＝事業所別の場合に参照する。
**       事業所のレコードが存在しない場合は、資産事業所マスタと同じく空欄で出力する（全社の値は使用しない）。
**       資産事業所マスタ（N0000MS_NA_SisanZigyousho）と同じく事業所コードをキーとする。
** 対象：
**       N5525MS_NA_ZigyoushoSinkokuJouhou         事業所別地方税申告情報マスタ
**       N5525MS_NA_ZigyoushoSinkokuJouhouHistory  事業所別地方税申告情報マスタ変更履歴
**       TR_N5525MS_NA_ZigyoushoSinkokuJouhou_IUD  変更履歴トリガ
***************************************************************************************************
**      履歴
**              2026/10/11 新規作成  (5525)事業所別償却資産申告対応
**
***************************************************************************************************
*/

-------------------------------------------------------------------------------
-- 事業所別地方税申告情報マスタ
-------------------------------------------------------------------------------
if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[N5525MS_NA_ZigyoushoSinkokuJouhou]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[N5525MS_NA_ZigyoushoSinkokuJouhou] (
      ZigyoushoCD               varchar(6)          NOT NULL                -- 事業所コード
    , NouzeishaCD               varchar(10)         NULL                    -- 納税者コード
    , RiyoushaID                varchar(11)         NULL                    -- 利用者ID
    , DaihyoushaSikaku          tinyint             NOT NULL                -- 代表者資格(1:代表者、2:破産管財人、3:清算人、99:その他)
        CONSTRAINT DF_N5525MS_NA_ZigyoushoSinkokuJouhou_DaihyoushaSikaku DEFAULT 1
    , DaihyoushaYuubinNO1       varchar(3)          NULL                    -- 代表者郵便番号1
    , DaihyoushaYuubinNO2       varchar(4)          NULL                    -- 代表者郵便番号2
    , DaihyoushaJuusho1         varchar(50)         NULL                    -- 代表者住所1
    , DaihyoushaJuusho2         varchar(50)         NULL                    -- 代表者住所2
    , DaihyoushaTELNO1          varchar(6)          NULL                    -- 代表者電話番号1
    , DaihyoushaTELNO2          varchar(5)          NULL                    -- 代表者電話番号2
    , DaihyoushaTELNO3          varchar(5)          NULL                    -- 代表者電話番号3
    , CompanyNO                 int                 NOT NULL                -- 会社NO
    , UpdateTimestamp           timestamp           NOT NULL                -- 更新タイムスタンプ
    , UpdateDatetime            datetime            NOT NULL                -- 更新日時
    , UpdateCertificatedGuid    uniqueidentifier    NOT NULL                -- 更新認証済Guid
    , UpdateJobID               varchar(128)        NOT NULL                -- 更新JobID
    , UpdateUserID              varchar(128)        NOT NULL                -- 更新ユーザID
    , UpdateComputerName        varchar(128)        NOT NULL                -- 更新PC名
    , UpdateComputerUserName    varchar(128)        NOT NULL                -- 更新PCユーザ名
    , UpdateComputerDomainName  varchar(256)        NOT NULL                -- 更新PCドメイン名
    , CONSTRAINT PK_N5525MS_NA_ZigyoushoSinkokuJouhou PRIMARY KEY CLUSTERED (ZigyoushoCD)
    , CONSTRAINT CK_N5525MS_NA_ZigyoushoSinkokuJouhou_NouzeishaCD          CHECK (NouzeishaCD <> '')
    , CONSTRAINT CK_N5525MS_NA_ZigyoushoSinkokuJouhou_RiyoushaID           CHECK (RiyoushaID <> '')
    , CONSTRAINT CK_N5525MS_NA_ZigyoushoSinkokuJouhou_DaihyoushaSikaku     CHECK (DaihyoushaSikaku IN (1, 2, 3, 99))
    , CONSTRAINT CK_N5525MS_NA_ZigyoushoSinkokuJouhou_DaihyoushaYuubinNO1  CHECK (DaihyoushaYuubinNO1 <> '')
    , CONSTRAINT CK_N5525MS_NA_ZigyoushoSinkokuJouhou_DaihyoushaYuubinNO2  CHECK (DaihyoushaYuubinNO2 <> '')
    , CONSTRAINT CK_N5525MS_NA_ZigyoushoSinkokuJouhou_DaihyoushaJuusho1    CHECK (DaihyoushaJuusho1 <> '')
    , CONSTRAINT CK_N5525MS_NA_ZigyoushoSinkokuJouhou_DaihyoushaJuusho2    CHECK (DaihyoushaJuusho2 <> '')
    , CONSTRAINT CK_N5525MS_NA_ZigyoushoSinkokuJouhou_DaihyoushaTELNO1     CHECK (DaihyoushaTELNO1 <> '')
    , CONSTRAINT CK_N5525MS_NA_ZigyoushoSinkokuJouhou_DaihyoushaTELNO2     CHECK (DaihyoushaTELNO2 <> '')
    , CONSTRAINT CK_N5525MS_NA_ZigyoushoSinkokuJouhou_DaihyoushaTELNO3     CHECK (DaihyoushaTELNO3 <> '')
)
END
GO

-------------------------------------------------------------------------------
-- 事業所別地方税申告情報マスタ変更履歴
-------------------------------------------------------------------------------
if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[N5525MS_NA_ZigyoushoSinkokuJouhouHistory]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[N5525MS_NA_ZigyoushoSinkokuJouhouHistory] (
      ZigyoushoCD               varchar(6)          NOT NULL                -- 事業所コード
    , NouzeishaCD               varchar(10)         NULL                    -- 納税者コード
    , RiyoushaID                varchar(11)         NULL                    -- 利用者ID
    , DaihyoushaSikaku          tinyint             NOT NULL                -- 代表者資格
    , DaihyoushaYuubinNO1       varchar(3)          NULL                    -- 代表者郵便番号1
    , DaihyoushaYuubinNO2       varchar(4)          NULL                    -- 代表者郵便番号2
    , DaihyoushaJuusho1         varchar(50)         NULL                    -- 代表者住所1
    , DaihyoushaJuusho2         varchar(50)         NULL                    -- 代表者住所2
    , DaihyoushaTELNO1          varchar(6)          NULL                    -- 代表者電話番号1
    , DaihyoushaTELNO2          varchar(5)          NULL                    -- 代表者電話番号2
    , DaihyoushaTELNO3          varchar(5)          NULL                    -- 代表者電話番号3
    , CompanyNO                 int                 NOT NULL                -- 会社NO
    , UpdateTimestamp           binary(8)           NOT NULL                -- 更新タイムスタンプ
    , UpdateDatetime            datetime            NOT NULL                -- 更新日時
    , UpdateCertificatedGuid    uniqueidentifier    NOT NULL                -- 更新認証済Guid
    , UpdateJobID               varchar(128)        NOT NULL                -- 更新JobID
    , UpdateUserID              varchar(128)        NOT NULL                -- 更新ユーザID
    , UpdateComputerName        varchar(128)        NOT NULL                -- 更新PC名
    , UpdateComputerUserName    varchar(128)        NOT NULL                -- 更新PCユーザ名
    , UpdateComputerDomainName  varchar(256)        NOT NULL                -- 更新PCドメイン名
    , HistoryOperationName      varchar(10)         NOT NULL                -- 履歴操作名
    , HistoryTimestamp          timestamp           NOT NULL                -- 履歴タイムスタンプ
    , HistoryDatetime           datetime            NOT NULL                -- 履歴日時
    , HistoryCertificatedGuid   uniqueidentifier    NOT NULL                -- 履歴認証済Guid
    , HistoryJobID              varchar(128)        NOT NULL                -- 履歴JobID
    , HistoryUserID             varchar(128)        NOT NULL                -- 履歴ユーザID
    , HistoryComputerName       varchar(128)        NOT NULL                -- 履歴PC名
    , HistoryComputerUserName   varchar(128)        NOT NULL                -- 履歴PCユーザ名
    , HistoryComputerDomainName varchar(256)        NOT NULL                -- 履歴PCドメイン名
)

CREATE CLUSTERED INDEX IX_N5525MS_NA_ZigyoushoSinkokuJouhouHistory_01
    ON [dbo].[N5525MS_NA_ZigyoushoSinkokuJouhouHistory] (HistoryDatetime)
END
GO

-------------------------------------------------------------------------------
-- 変更履歴トリガ（標準 TR_N0000MS_NA_SisanZigyousho_IUD と同じ作りとする）
-------------------------------------------------------------------------------
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[TR_N5525MS_NA_ZigyoushoSinkokuJouhou_IUD]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[TR_N5525MS_NA_ZigyoushoSinkokuJouhou_IUD]
GO

CREATE TRIGGER [dbo].[TR_N5525MS_NA_ZigyoushoSinkokuJouhou_IUD]
ON [dbo].[N5525MS_NA_ZigyoushoSinkokuJouhou]
AFTER INSERT,UPDATE,DELETE
AS
/*
*********************************************************************
** 名前　:事業所別地方税申告情報マスタの履歴作成トリガー
** 機能　:[N5525MS_NA_ZigyoushoSinkokuJouhou]の更新履歴を[N5525MS_NA_ZigyoushoSinkokuJouhouHistory]に格納する
** 作成日:2026/10/11
** 作成者:(5525)事業所別償却資産申告対応
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
            INSERT INTO [N5525MS_NA_ZigyoushoSinkokuJouhouHistory] --履歴レコード格納テーブル名をセット
            (
                  [ZigyoushoCD]
                , [NouzeishaCD]
                , [RiyoushaID]
                , [DaihyoushaSikaku]
                , [DaihyoushaYuubinNO1]
                , [DaihyoushaYuubinNO2]
                , [DaihyoushaJuusho1]
                , [DaihyoushaJuusho2]
                , [DaihyoushaTELNO1]
                , [DaihyoushaTELNO2]
                , [DaihyoushaTELNO3]
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
                , [NouzeishaCD]
                , [RiyoushaID]
                , [DaihyoushaSikaku]
                , [DaihyoushaYuubinNO1]
                , [DaihyoushaYuubinNO2]
                , [DaihyoushaJuusho1]
                , [DaihyoushaJuusho2]
                , [DaihyoushaTELNO1]
                , [DaihyoushaTELNO2]
                , [DaihyoushaTELNO3]
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
            INSERT INTO [N5525MS_NA_ZigyoushoSinkokuJouhouHistory] --履歴レコード格納テーブル名をセット
            (
                  [ZigyoushoCD]
                , [NouzeishaCD]
                , [RiyoushaID]
                , [DaihyoushaSikaku]
                , [DaihyoushaYuubinNO1]
                , [DaihyoushaYuubinNO2]
                , [DaihyoushaJuusho1]
                , [DaihyoushaJuusho2]
                , [DaihyoushaTELNO1]
                , [DaihyoushaTELNO2]
                , [DaihyoushaTELNO3]
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
                , [NouzeishaCD]
                , [RiyoushaID]
                , [DaihyoushaSikaku]
                , [DaihyoushaYuubinNO1]
                , [DaihyoushaYuubinNO2]
                , [DaihyoushaJuusho1]
                , [DaihyoushaJuusho2]
                , [DaihyoushaTELNO1]
                , [DaihyoushaTELNO2]
                , [DaihyoushaTELNO3]
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
