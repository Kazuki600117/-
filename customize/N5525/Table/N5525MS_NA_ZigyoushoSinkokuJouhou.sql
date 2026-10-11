/*
******************************************************************************
** 名前：事業所別地方税申告情報マスタ（カスタマイズSEQ 5525）
** 概要：固定資産コントロールマスタ（N0000MS_NA_KoteiSisanControl）の
**       「地方税申告（連携）」項目を事業所ごとに保持するアドオンテーブル。
**       償却資産申告書（N5525）の eLTax ファイル出力で、出力区分＝事業所別の場合に参照する。
**       事業所のレコードが存在しない場合は、固定資産コントロールマスタの値を使用する。
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
-- 変更履歴トリガ
-- ※履歴項目（History*）は、更新時の Update* の値をそのまま転記する。
--   削除時は削除前レコードの Update* を転記し、履歴日時のみ現在日時とする。
--   標準の TR_N0000MS_NA_SisanZigyousho_IUD と履歴項目の取得方法が異なる場合は、標準に合わせること。
-------------------------------------------------------------------------------
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[TR_N5525MS_NA_ZigyoushoSinkokuJouhou_IUD]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[TR_N5525MS_NA_ZigyoushoSinkokuJouhou_IUD]
GO

CREATE TRIGGER TR_N5525MS_NA_ZigyoushoSinkokuJouhou_IUD
ON N5525MS_NA_ZigyoushoSinkokuJouhou
FOR INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON

    DECLARE @OperationName varchar(10)

    IF EXISTS (SELECT * FROM inserted) AND EXISTS (SELECT * FROM deleted)
        SET @OperationName = 'UPDATE'
    ELSE IF EXISTS (SELECT * FROM inserted)
        SET @OperationName = 'INSERT'
    ELSE IF EXISTS (SELECT * FROM deleted)
        SET @OperationName = 'DELETE'
    ELSE
        RETURN

    IF @OperationName IN ('INSERT', 'UPDATE')
    BEGIN
        INSERT INTO N5525MS_NA_ZigyoushoSinkokuJouhouHistory (
              ZigyoushoCD
            , NouzeishaCD
            , RiyoushaID
            , DaihyoushaSikaku
            , DaihyoushaYuubinNO1
            , DaihyoushaYuubinNO2
            , DaihyoushaJuusho1
            , DaihyoushaJuusho2
            , DaihyoushaTELNO1
            , DaihyoushaTELNO2
            , DaihyoushaTELNO3
            , CompanyNO
            , UpdateTimestamp
            , UpdateDatetime
            , UpdateCertificatedGuid
            , UpdateJobID
            , UpdateUserID
            , UpdateComputerName
            , UpdateComputerUserName
            , UpdateComputerDomainName
            , HistoryOperationName
            , HistoryDatetime
            , HistoryCertificatedGuid
            , HistoryJobID
            , HistoryUserID
            , HistoryComputerName
            , HistoryComputerUserName
            , HistoryComputerDomainName
        )
        SELECT
              ZigyoushoCD
            , NouzeishaCD
            , RiyoushaID
            , DaihyoushaSikaku
            , DaihyoushaYuubinNO1
            , DaihyoushaYuubinNO2
            , DaihyoushaJuusho1
            , DaihyoushaJuusho2
            , DaihyoushaTELNO1
            , DaihyoushaTELNO2
            , DaihyoushaTELNO3
            , CompanyNO
            , UpdateTimestamp
            , UpdateDatetime
            , UpdateCertificatedGuid
            , UpdateJobID
            , UpdateUserID
            , UpdateComputerName
            , UpdateComputerUserName
            , UpdateComputerDomainName
            , @OperationName
            , UpdateDatetime
            , UpdateCertificatedGuid
            , UpdateJobID
            , UpdateUserID
            , UpdateComputerName
            , UpdateComputerUserName
            , UpdateComputerDomainName
        FROM inserted
    END
    ELSE
    BEGIN
        INSERT INTO N5525MS_NA_ZigyoushoSinkokuJouhouHistory (
              ZigyoushoCD
            , NouzeishaCD
            , RiyoushaID
            , DaihyoushaSikaku
            , DaihyoushaYuubinNO1
            , DaihyoushaYuubinNO2
            , DaihyoushaJuusho1
            , DaihyoushaJuusho2
            , DaihyoushaTELNO1
            , DaihyoushaTELNO2
            , DaihyoushaTELNO3
            , CompanyNO
            , UpdateTimestamp
            , UpdateDatetime
            , UpdateCertificatedGuid
            , UpdateJobID
            , UpdateUserID
            , UpdateComputerName
            , UpdateComputerUserName
            , UpdateComputerDomainName
            , HistoryOperationName
            , HistoryDatetime
            , HistoryCertificatedGuid
            , HistoryJobID
            , HistoryUserID
            , HistoryComputerName
            , HistoryComputerUserName
            , HistoryComputerDomainName
        )
        SELECT
              ZigyoushoCD
            , NouzeishaCD
            , RiyoushaID
            , DaihyoushaSikaku
            , DaihyoushaYuubinNO1
            , DaihyoushaYuubinNO2
            , DaihyoushaJuusho1
            , DaihyoushaJuusho2
            , DaihyoushaTELNO1
            , DaihyoushaTELNO2
            , DaihyoushaTELNO3
            , CompanyNO
            , UpdateTimestamp
            , UpdateDatetime
            , UpdateCertificatedGuid
            , UpdateJobID
            , UpdateUserID
            , UpdateComputerName
            , UpdateComputerUserName
            , UpdateComputerDomainName
            , @OperationName
            , GETDATE()
            , UpdateCertificatedGuid
            , UpdateJobID
            , UpdateUserID
            , UpdateComputerName
            , UpdateComputerUserName
            , UpdateComputerDomainName
        FROM deleted
    END
END
GO
