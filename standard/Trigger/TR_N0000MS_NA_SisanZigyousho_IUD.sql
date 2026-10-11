CREATE TRIGGER [dbo].[TR_N0000MS_NA_SisanZigyousho_IUD]
ON [dbo].[N0000MS_NA_SisanZigyousho]
AFTER INSERT,UPDATE,DELETE
AS
/*
*********************************************************************
** 名前　:資産事業所マスタの履歴作成トリガー
** 機能　:[N0000MS_NA_SisanZigyousho]の更新履歴を[N0000MS_NA_SisanZigyoushoHistory]に格納する
** 作成日:2013/11/14 11:07:04
** 作成者:OBIC/F03963
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
        IF ( @OperationName = 'Insert' ) OR  -- 挿入もしくは
           ( @OperationName = 'Update' )     -- 更新
        BEGIN
            INSERT INTO [N0000MS_NA_SisanZigyoushoHistory] --履歴レコード格納テーブル名をセット
            (
                  [ZigyoushoCD]
                , [ZititaiCD]
                , [YuubinNO1]
                , [YuubinNO2]
                , [Juusho1Kana]
                , [Juusho2Kana]
                , [Juusho1]
                , [Juusho2]
                , [TELNO1]
                , [TELNO2]
                , [TELNO3]
                , [CompanyName]
                , [CompanyKanaName]
                , [DaihyoushaName]
                , [DaihyoushaKanaName]
                , [Yagou]
                , [ZigyouShumoku]
                , [Sihonkin]
                , [SeturituDate]
                , [SinkokuTantoushaName]
                , [TantoushaTELNO1]
                , [TantoushaTELNO2]
                , [TantoushaTELNO3]
                , [KaikeiZimushoName]
                , [KaikeiZimushoTELNO1]
                , [KaikeiZimushoTELNO2]
                , [KaikeiZimushoTELNO3]
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
                , [ZititaiCD]
                , [YuubinNO1]
                , [YuubinNO2]
                , [Juusho1Kana]
                , [Juusho2Kana]
                , [Juusho1]
                , [Juusho2]
                , [TELNO1]
                , [TELNO2]
                , [TELNO3]
                , [CompanyName]
                , [CompanyKanaName]
                , [DaihyoushaName]
                , [DaihyoushaKanaName]
                , [Yagou]
                , [ZigyouShumoku]
                , [Sihonkin]
                , [SeturituDate]
                , [SinkokuTantoushaName]
                , [TantoushaTELNO1]
                , [TantoushaTELNO2]
                , [TantoushaTELNO3]
                , [KaikeiZimushoName]
                , [KaikeiZimushoTELNO1]
                , [KaikeiZimushoTELNO2]
                , [KaikeiZimushoTELNO3]
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
            INSERT INTO [N0000MS_NA_SisanZigyoushoHistory] --履歴レコード格納テーブル名をセット
            (
                  [ZigyoushoCD]
                , [ZititaiCD]
                , [YuubinNO1]
                , [YuubinNO2]
                , [Juusho1Kana]
                , [Juusho2Kana]
                , [Juusho1]
                , [Juusho2]
                , [TELNO1]
                , [TELNO2]
                , [TELNO3]
                , [CompanyName]
                , [CompanyKanaName]
                , [DaihyoushaName]
                , [DaihyoushaKanaName]
                , [Yagou]
                , [ZigyouShumoku]
                , [Sihonkin]
                , [SeturituDate]
                , [SinkokuTantoushaName]
                , [TantoushaTELNO1]
                , [TantoushaTELNO2]
                , [TantoushaTELNO3]
                , [KaikeiZimushoName]
                , [KaikeiZimushoTELNO1]
                , [KaikeiZimushoTELNO2]
                , [KaikeiZimushoTELNO3]
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
                , [ZititaiCD]
                , [YuubinNO1]
                , [YuubinNO2]
                , [Juusho1Kana]
                , [Juusho2Kana]
                , [Juusho1]
                , [Juusho2]
                , [TELNO1]
                , [TELNO2]
                , [TELNO3]
                , [CompanyName]
                , [CompanyKanaName]
                , [DaihyoushaName]
                , [DaihyoushaKanaName]
                , [Yagou]
                , [ZigyouShumoku]
                , [Sihonkin]
                , [SeturituDate]
                , [SinkokuTantoushaName]
                , [TantoushaTELNO1]
                , [TantoushaTELNO2]
                , [TantoushaTELNO3]
                , [KaikeiZimushoName]
                , [KaikeiZimushoTELNO1]
                , [KaikeiZimushoTELNO2]
                , [KaikeiZimushoTELNO3]
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

