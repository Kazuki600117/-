if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[N0000PR_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Print]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[N0000PR_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Print]
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE  N0000PR_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Print(
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
    ,@p_Sinkokushurui                   tinyint                     -- 申告種類(電算申告:0 増減申告:1)
    ,@p_HyoukagakuPrintKBN              tinyint                     -- 評価額印字
    ,@p_ZennenPrintKBN                  tinyint                     -- 前年中印字
    ,@p_YoutoKBN                        tinyint                     -- 用途区分(0:提出用、1:控え用)
    ,@p_ShuturyokuKBN                   tinyint                     -- 出力区分(0:全社、1:事業所別)
    ,@p_TeishutuDate                    datetime                    -- 提出日
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
** 名前：償却資産申告書
** 機能：償却資産申告書に出力するデータを作成する
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
**      ,@p_Sinkokushurui                   tinyint                     -- 申告種類(電算申告:0 増減申告:1)
**      ,@p_HyoukagakuPrintKBN              tinyint                     -- 評価額印字
**      ,@p_ZennenPrintKBN                  tinyint                     -- 前年中印字
**      ,@p_YoutoKBN                        tinyint                     -- 用途区分(0:提出用、1:控え用)
**      ,@p_ShuturyokuKBN                   tinyint                     -- 出力区分(0:全社、1:事業所別)
**      ,@p_TeishutuDate                    datetime                    -- 提出日
**      ,@p_SinkokuNendo                    datetime                    -- 申告年度
**      ,@p_ZititaiCDBegin                  varchar(6)                  -- 自治体コード開始
**      ,@p_ZititaiCDEnd                    varchar(6)                  -- 自治体コード終了
**      ,@p_ZigyoushoCDBegin                varchar(6)                  -- 事業所コード開始
**      ,@p_ZigyoushoCDEnd                  varchar(6)                  -- 事業所コード終了
**      ,@p_ZigSecPtnCD                     varchar(4)                  -- 事業所セキュリティパターンコード
**      ,@p_CompanyName                     varchar(50)  = NULL         -- 会社名
--2012/11/05 E.Sato ST (N00970)eLTax対応
**      ,@p_ShuturyokuSiteiKBN              tinyint = 0                 -- 出力指定区分(0:印刷、1:ファイル出力
--2012/11/05 E.Sato ED
***************************************************************************************************
**      履歴
**              2009/02/06 Y.Ishizawa   新規作成
**              2009/05/25 S.Mino       変更 フル桁印字の表示改修
**              2010/12/09 M.Shinbo     変更  (N00697)IFRS・ダブルスタンダード対応
**              2011/01/06 S.Harada     修正 （N00727）自治体移動が複数回あった場合に、出力内容が不正になる不具合修正
**              2011/01/18 M.Shinbo     修正  (N00697)IFRS・ダブルスタンダード対応  戻り対応
**              2011/01/20 S.Harada     修正 （N00748）複数回移動があり、最終の移動が自治体に絡まない移動の時、出力が不正になる不具合修正
**              2012/11/05 E.Sato       変更  (N00970)eLTax対応
**              2013/01/15 S.Murakami   変更  (N00995) 分割合併対応
**              2013/02/19 Y.Fujita     修正 （N01023）出力区分=事業所別で事業所の範囲指定を入力した後、出力区分=全社で出力すると、事業所の範囲指定がかかってしまう不具合修正
**              2015/01/05 K.Morita     変更  (19288-19289-19291)eLTax仕様変更対応
**              2015/11/09 T.Sato       変更  (32764)電子申告連携の達人への出力ファイルで所有区分の内容が不正になる件を改修
**              2015/11/10 T.Sato       修正  (32423-32424-32426)償却資産申告書法人番号追加対応
**              2015/12/08 T.Shiraishi  修正  (33520)地方税の申告対象となる日付を取得日ではなく償却開始日にする
**              2015/12/17 T.Sato       修正  (34037-34032-34034)償却資産申告書法人番号追加対応(eLTax)
**              2017/04/19 Y.Kunihiro   修正  (59019)固定資産コントロールマスタの設立年月日が明治時代の場合、ファイル出力すると和暦の年が1年ずれる不具合修正
**              2018/04/26 T.Maeda      修正  (97532-97643)改元対応
**              2019/03/08 M.Hamachika  修正  (123875)出力のたびに自治体の並び順が変わってしまう不具合修正
**              2020/04/16 H.Torii      修正  (164042)分割合併により取得した資産を申告期間終了以降に除却すると、取得価額が不正になる件を修正
**              2020/11/16 N.Maeda      修正  (149911)特例率ゼロが登録できません。
**              2020/12/24 Y.Matuzawa   修正  (182011)半角を全角に置き換え
**              2020/12/24 Y.Matuzawa   修正  (182011)提出先市町村コードの取得元を地方公共団体コードに変更
**
***************************************************************************************************
*/
BEGIN

        SET NOCOUNT ON
    
        -- 変数定義
        DECLARE @ErrorCode                      int                 -- @@ERRORの待避領域
        DECLARE @SinkokuKikanBegin              datetime
        DECLARE @SinkokuKikanEnd                datetime
--2015/12/08 T.Shiraishi  修正  (33520)地方税の申告対象となる日付を取得日ではなく償却開始日にする↓
        DECLARE @ShutokuDateHanteiKBN           tinyint
--2015/12/08 T.Shiraishi  修正  (33520)地方税の申告対象となる日付を取得日ではなく償却開始日にする↑
    
        -- ストアド変数
        DECLARE @strSQL    AS varchar(MAX)

        -- 引数初期化
        SET @po_ErrorKBN = 0
        SET @po_ErrorCD1 = ''
        SET @po_ErrorCD2 = ''
        SET @po_ErrorProcName = ''
        SET @po_ErrorSummary = ''
        SET @po_ErrorDetail = ''
        SET @po_ErrorBtnKBN = 0
        SET @po_ErrorBtnDefault = 0

--2015/12/08 T.Shiraishi  修正  (33520)地方税の申告対象となる日付を取得日ではなく償却開始日にする↓
        SELECT @ShutokuDateHanteiKBN = ShutokuDateHanteiKBN
        FROM N0000MS_NA_TihouzeiControl
        WHERE NendoID = 0
                
        -- エラー処理
        SET @ErrorCode = @@ERROR
        IF  @ErrorCode <> 0
        BEGIN
            GOTO ERR_SHORI
        END  
--2015/12/08 T.Shiraishi  修正  (33520)地方税の申告対象となる日付を取得日ではなく償却開始日にする↑

        -- 先行移動テーブル
        CREATE TABLE #N0000WK_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_SenkouIdou (
                 SisanNO            varchar(12)    COLLATE database_default NULL 
                ,Edaban             int            NULL    
                ,ZigyoushoCD        varchar(6)     COLLATE database_default NULL
                ,ZititaiCD          varchar(6)     COLLATE database_default NULL
        )

        -- 全資産申告テーブル
        CREATE TABLE #N0000WK_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Zensisan (
                  SisanNO                 varchar(12)    COLLATE database_default NULL   -- 資産ナンバー
                , Edaban                  int            NULL                            -- 枝番
                , SisanShubetuNO          tinyint        NULL                            -- 資産種別NO
                , ZigyoushoCD             varchar(6)     COLLATE database_default NULL   -- 事業所コード
                , ZititaiCD               varchar(6)     COLLATE database_default NULL   -- 申告自治体コード
                , TihouzeiShutokuKagaku   decimal(15,0)  NULL             -- 地方税取得価額
                , HonnendoHyoukagaku      decimal(15,0)  NULL             -- 本年度評価額
                , TokureirituNumerator    tinyint        NULL             -- 特例率分子
                , TokureirituDenominator  tinyint        NULL             -- 特例率分母
                , TokureiKikan            datetime       NULL                            -- 特例率適用期間
        )

        -- 減少資産テーブル
        CREATE TABLE #N0000WK_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Genshou (
                  SisanNO                 varchar(12)    COLLATE database_default NULL   -- 資産ナンバー
                , Edaban                  int            NULL                            -- 枝番
                , ZiyuuKBN                tinyint        NULL                            -- 事由区分
                , SisanShubetuNO          tinyint        NULL                            -- 資産種別コード
                , ZigyoushoCD             varchar(6)     COLLATE database_default NULL   -- 事業所コード
                , ZititaiCD               varchar(6)     COLLATE database_default NULL   -- 申告自治体コード
                , TihouzeiShutokuKagaku   decimal(15,0)  NULL                            -- 地方税取得価額
        )

        -- 増加資産テーブル
        CREATE TABLE #N0000WK_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Zouka (
                  SisanNO                 varchar(12)    COLLATE database_default NULL   -- 資産ナンバー
                , Edaban                  int            NULL                            -- 枝番
                , ZiyuuKBN                tinyint        NULL                            -- 事由区分
                , SisanShubetuNO          tinyint        NULL                            -- 資産種別コード
                , ZigyoushoCD             varchar(6)     COLLATE database_default NULL   -- 事業所コード
                , ZititaiCD               varchar(6)     COLLATE database_default NULL   -- 申告自治体コード
                , TihouzeiShutokuKagaku   decimal(15,0)  NULL                            -- 地方税取得価額
        )

        -- 資産種類別集計後テーブル
        CREATE TABLE #N0000WK_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Shukei (
                  SisanShubetuNO      tinyint        NULL                            -- 資産種別コード
                , ZigyoushoCD         varchar(6)     COLLATE database_default NULL   -- 事業所コード
                , ZititaiCD           varchar(6)     COLLATE database_default NULL   -- 申告自治体コード
                , ShutokuKagaku1      decimal(15,0)  NULL                            -- 地方税取得価額
                , ShutokuKagaku2      decimal(15,0)  NULL                            -- 地方税取得価額
                , ShutokuKagaku3      decimal(15,0)  NULL                            -- 地方税取得価額
                , ShutokuKagaku4      decimal(15,0)  NULL                            -- 地方税取得価額
                , HonnendoHyoukagaku  decimal(15,0)  NULL                            -- 本年度評価額
                , Ketteikagaku        decimal(15,0)  NULL                            -- 決定価額
                , KazeiHyoujungaku    decimal(15,0)  NULL                            -- 課税標準額
                , Kensuu              int            NULL                            -- 件数
                , KeiKBN              tinyint        NULL                            -- 集計区分(0:各ワークを集計して追加した状態, 1:「0」データを集計した状態)
        )

        -- 帳票出力前ワーク
        CREATE TABLE #N0000WK_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Print (
                  SisanShubetuNO        tinyint        NULL                            -- 資産種別コード
                , ZigyoushoCD           varchar(6)     COLLATE database_default NULL   -- 事業所コード
                , ZititaiCD             varchar(6)     COLLATE database_default NULL   -- 申告自治体コード
                , ShutokuKagaku1_1      decimal(15,0)  NULL                            -- 地方税取得価額
                , ShutokuKagaku1_2      decimal(15,0)  NULL                            -- 地方税取得価額
                , ShutokuKagaku1_3      decimal(15,0)  NULL                            -- 地方税取得価額
                , ShutokuKagaku1_4      decimal(15,0)  NULL                            -- 地方税取得価額
                , ShutokuKagaku1_5      decimal(15,0)  NULL                            -- 地方税取得価額
                , ShutokuKagaku1_6      decimal(15,0)  NULL                            -- 地方税取得価額
                , ShutokuKagaku1_7      decimal(15,0)  NULL                            -- 地方税取得価額
                , ShutokuKagaku2_1      decimal(15,0)  NULL                            -- 地方税取得価額
                , ShutokuKagaku2_2      decimal(15,0)  NULL                            -- 地方税取得価額
                , ShutokuKagaku2_3      decimal(15,0)  NULL                            -- 地方税取得価額
                , ShutokuKagaku2_4      decimal(15,0)  NULL                            -- 地方税取得価額
                , ShutokuKagaku2_5      decimal(15,0)  NULL                            -- 地方税取得価額
                , ShutokuKagaku2_6      decimal(15,0)  NULL                            -- 地方税取得価額
                , ShutokuKagaku2_7      decimal(15,0)  NULL                            -- 地方税取得価額
                , ShutokuKagaku3_1      decimal(15,0)  NULL                            -- 地方税取得価額
                , ShutokuKagaku3_2      decimal(15,0)  NULL                            -- 地方税取得価額
                , ShutokuKagaku3_3      decimal(15,0)  NULL                            -- 地方税取得価額
                , ShutokuKagaku3_4      decimal(15,0)  NULL                            -- 地方税取得価額
                , ShutokuKagaku3_5      decimal(15,0)  NULL                            -- 地方税取得価額
                , ShutokuKagaku3_6      decimal(15,0)  NULL                            -- 地方税取得価額
                , ShutokuKagaku3_7      decimal(15,0)  NULL                            -- 地方税取得価額
                , ShutokuKagaku4_1      decimal(15,0)  NULL                            -- 地方税取得価額
                , ShutokuKagaku4_2      decimal(15,0)  NULL                            -- 地方税取得価額
                , ShutokuKagaku4_3      decimal(15,0)  NULL                            -- 地方税取得価額
                , ShutokuKagaku4_4      decimal(15,0)  NULL                            -- 地方税取得価額
                , ShutokuKagaku4_5      decimal(15,0)  NULL                            -- 地方税取得価額
                , ShutokuKagaku4_6      decimal(15,0)  NULL                            -- 地方税取得価額
                , ShutokuKagaku4_7      decimal(15,0)  NULL                            -- 地方税取得価額
                , HonnendoHyoukagaku_1  decimal(15,0)  NULL                            -- 本年度評価額
                , HonnendoHyoukagaku_2  decimal(15,0)  NULL                            -- 本年度評価額
                , HonnendoHyoukagaku_3  decimal(15,0)  NULL                            -- 本年度評価額
                , HonnendoHyoukagaku_4  decimal(15,0)  NULL                            -- 本年度評価額
                , HonnendoHyoukagaku_5  decimal(15,0)  NULL                            -- 本年度評価額
                , HonnendoHyoukagaku_6  decimal(15,0)  NULL                            -- 本年度評価額
                , HonnendoHyoukagaku_7  decimal(15,0)  NULL                            -- 本年度評価額
                , Ketteikagaku_1        decimal(15,0)  NULL                            -- 決定価額
                , Ketteikagaku_2        decimal(15,0)  NULL                            -- 決定価額
                , Ketteikagaku_3        decimal(15,0)  NULL                            -- 決定価額
   , Ketteikagaku_4        decimal(15,0)  NULL                            -- 決定価額
                , Ketteikagaku_5        decimal(15,0)  NULL                            -- 決定価額
                , Ketteikagaku_6        decimal(15,0)  NULL                            -- 決定価額
                , Ketteikagaku_7        decimal(15,0)  NULL                            -- 決定価額
                , KazeiHyoujungaku_1    decimal(15,0)  NULL                            -- 課税標準額
                , KazeiHyoujungaku_2    decimal(15,0)  NULL                            -- 課税標準額
                , KazeiHyoujungaku_3    decimal(15,0)  NULL                            -- 課税標準額
                , KazeiHyoujungaku_4    decimal(15,0)  NULL                            -- 課税標準額
                , KazeiHyoujungaku_5    decimal(15,0)  NULL                            -- 課税標準額
                , KazeiHyoujungaku_6    decimal(15,0)  NULL                            -- 課税標準額
                , KazeiHyoujungaku_7    decimal(15,0)  NULL                            -- 課税標準額
                , Kensuu_1              int            NULL                            -- 件数
                , Kensuu_2              int            NULL                            -- 件数
                , Kensuu_3              int            NULL                            -- 件数
                , Kensuu_4              int            NULL                            -- 件数
                , Kensuu_5              int            NULL                            -- 件数
                , Kensuu_6              int            NULL                            -- 件数
                , Kensuu_7              int            NULL                            -- 件数
        )




        SET @SinkokuKikanBegin = DATEADD(day,1,DATEADD(year,-1,@p_SinkokuNendo))
        SET @SinkokuKikanEnd   = @p_SinkokuNendo


        -- 1.先行移動データの取得(※指定申告期間終了年月日以降の移動資産)
        INSERT INTO #N0000WK_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_SenkouIdou(
                 SisanNO
                ,Edaban
                ,ZigyoushoCD
                ,ZititaiCD
                ) 
        SELECT
                 SIS.SisanNO
                ,SIS.Edaban
                ,CASE WHEN IDO.SisanNO IS NULL
                      THEN SIS.ZigyoushoCD
                      ELSE IDO.IdoumotoZigyoushoCD
                 END
                ,CASE WHEN IDO.SisanNO IS NULL
                      THEN SIS.ZititaiCD
                      ELSE IDO.IdoumotoZititaiCD
                 END
        FROM    N0000RR_NA_SisanJouhou SIS
        LEFT JOIN (SELECT IDO.SisanNO
                         ,IDO.Edaban
                         ,IDO.IdoumotoZigyoushoCD
                         ,IDO.IdoumotoZititaiCD
                   FROM N0000RR_NA_IdouJouhou IDO
                   INNER JOIN (
                               SELECT
                                        IDO.SisanNO
                                       ,IDO.Edaban
                                       ,Min(IDO.IdouDate) AS MinIdouDate
                               FROM    N0000RR_NA_IdouJouhou IDO
                               WHERE IDO.IdouDate  > @SinkokuKikanEnd
                               --2011/01/18 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 戻り対応 BEGIN↓
                               AND   IDO.ShoukyakuKBN  = 0
                               --2011/01/18 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 戻り対応 END↑
                               GROUP BY IDO.SisanNO, IDO.Edaban) MINIDO
                   ON         IDO.SisanNO  = MINIDO.SisanNO
                   AND        IDO.Edaban   = MINIDO.Edaban
                   AND        IDO.IdouDate = MINIDO.MinIdouDate
                   --2010/12/09 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 BEGIN↓
                   AND        IDO.ShoukyakuKBN = 0
                   --2010/12/09 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 END↑
                ) IDO
        ON         SIS.SisanNO  = IDO.SisanNO
        AND        SIS.Edaban   = IDO.Edaban
        --  2013/01/15 S.Murakami  (N00995)分割合併対応↓
        LEFT JOIN N0000RR_NA_BunkatuGappeiJouhou BGJ
        ON        BGJ.SisanNO      = SIS.SisanNO
        AND       BGJ.Edaban       = SIS.Edaban
        AND       BGJ.ShoukyakuKBN = 0
        AND       BGJ.ZouGenKBN    = 0
        --  2013/01/15 S.Murakami  (N00995)分割合併対応↑
        
        --2010/12/09 M.Shinbo 変更 (N00697)IFRS・ダブルスタンダード対応 BEGIN↓
        --WHERE (@p_SisanKBN = 2 OR (SIS.KarisisanFlag = @p_SisanKBN))
        WHERE ((@p_SisanKBN <> 2 AND SIS.KarisisanFlag = @p_SisanKBN)
        OR     (@p_SisanKBN = 2 AND (SIS.KarisisanFlag = 0 OR SIS.KarisisanFlag = 1)))
        --2010/12/09 M.Shinbo 変更 (N00697)IFRS・ダブルスタンダード対応 END↑
        AND   SIS.SinkokuKBN IN ( 0, 2 )
        AND   SIS.SisanShubetuNO BETWEEN 1 AND 6
        AND NOT EXISTS (
                SELECT * FROM N0000RR_NA_JokyakuJouhou JOK
                WHERE JOK.SisanNO      = SIS.SisanNO
                AND   JOK.Edaban       = SIS.Edaban
                --2010/12/09 M.Shinbo 変更 (N00697)IFRS・ダブルスタンダード対応 BEGIN↓
                --AND   JOK.JokyakuDate <= @SinkokuKikanEnd )
                --  2013/01/15 S.Murakami  (N00995)分割合併対応↓
                --AND   JOK.JokyakuDate <= @SinkokuKikanEnd
                --2020/04/16 H.Torii 修正 (164042)分割合併により取得した資産を申告期間終了以降に除却すると、取得価額が不正になる件を修正 開始
                --AND   ISNULL(BGJ.BunkatuGappeiDate,JOK.JokyakuDate) <= @SinkokuKikanEnd
                AND   JOK.JokyakuDate <= @SinkokuKikanEnd
                --2020/04/16 H.Torii 修正 (164042)分割合併により取得した資産を申告期間終了以降に除却すると、取得価額が不正になる件を修正 終了
                --  2013/01/15 S.Murakami  (N00995)分割合併対応↑
                AND   JOK.ShoukyakuKBN = 0 )
                --2010/12/09 M.Shinbo 変更 (N00697)IFRS・ダブルスタンダード対応 END↑
        --2020/04/16 H.Torii 修正 (164042)分割合併により取得した資産を申告期間終了以降に除却すると、取得価額が不正になる件を修正 開始
        AND  ISNULL(BGJ.BunkatuGappeiDate,CASE @ShutokuDateHanteiKBN WHEN 0 THEN SIS.ShutokuDate ELSE SIS.ShoukyakuKaisiDate END) <= @SinkokuKikanEnd
        --2020/04/16 H.Torii 修正 (164042)分割合併により取得した資産を申告期間終了以降に除却すると、取得価額が不正になる件を修正 終了

        SET @ErrorCode = @@ERROR
        IF @ErrorCode <> 0 GOTO ERR_SHORI

        -- 2.全資産申告データ抽出
        INSERT INTO #N0000WK_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Zensisan (
                  SisanNO
                , Edaban
                , SisanShubetuNO
                , ZigyoushoCD
                , ZititaiCD
                , TihouzeiShutokuKagaku
                , HonnendoHyoukagaku
                , TokureirituNumerator
                , TokureirituDenominator
                , TokureiKikan
                )
        SELECT
                  SIS.SisanNO
                , SIS.Edaban
                , SIS.SisanShubetuNO
                , WK.ZigyoushoCD
                , WK.ZititaiCD
                , SIS.TihouzeiShutokuKagaku
                , TIH.HonnendoHyoukagaku
                , TIH.TokureirituNumerator
                , TIH.TokureirituDenominator
                , TIH.TokureiKikan
        FROM    N0000RR_NA_SisanJouhou SIS
        INNER JOIN N0000RR_NA_TihouzeiJouhou TIH
        ON         TIH.SisanNO      = SIS.SisanNO
        AND        TIH.Edaban       = SIS.Edaban
        AND        TIH.SinkokuNendo = @SinkokuKikanEnd
        LEFT  JOIN #N0000WK_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_SenkouIdou WK
        ON         SIS.SisanNO  = WK.SisanNO
        AND        SIS.Edaban   = WK.Edaban
        --  2013/01/15 S.Murakami  (N00995)分割合併対応↓
        LEFT JOIN N0000RR_NA_BunkatuGappeiJouhou BGJ
        ON        BGJ.SisanNO      = SIS.SisanNO
        AND       BGJ.Edaban       = SIS.Edaban
        AND       BGJ.ShoukyakuKBN = 0
        AND       BGJ.ZouGenKBN    = 0
        --WHERE SIS.ShutokuDate <= @SinkokuKikanEnd
--2015/12/08 T.Shiraishi  修正  (33520)地方税の申告対象となる日付を取得日ではなく償却開始日にする↓
--      WHERE ISNULL(BGJ.BunkatuGappeiDate,SIS.ShutokuDate) <= @SinkokuKikanEnd
        WHERE ISNULL(BGJ.BunkatuGappeiDate,CASE @ShutokuDateHanteiKBN WHEN 0 THEN SIS.ShutokuDate ELSE SIS.ShoukyakuKaisiDate END) <= @SinkokuKikanEnd
--2015/12/08 T.Shiraishi  修正  (33520)地方税の申告対象となる日付を取得日ではなく償却開始日にする↑
        --  2013/01/15 S.Murakami  (N00995)分割合併対応↑
        --2010/12/09 M.Shinbo 変更 (N00697)IFRS・ダブルスタンダード対応 BEGIN↓
        --AND  (@p_SisanKBN = 2 OR (SIS.KarisisanFlag = @p_SisanKBN))
        AND ((@p_SisanKBN <> 2 AND SIS.KarisisanFlag = @p_SisanKBN)
        OR   (@p_SisanKBN = 2 AND (SIS.KarisisanFlag = 0 OR SIS.KarisisanFlag = 1)))
        --2010/12/09 M.Shinbo 変更 (N00697)IFRS・ダブルスタンダード対応 END↑
        AND   SIS.SinkokuKBN IN ( 0, 2 )
        AND   SIS.SisanShubetuNO BETWEEN 1 AND 6
        AND NOT EXISTS (
                SELECT * FROM N0000RR_NA_JokyakuJouhou JOK
                WHERE JOK.SisanNO      = SIS.SisanNO
                AND   JOK.Edaban       = SIS.Edaban
                --2010/12/09 M.Shinbo 変更 (N00697)IFRS・ダブルスタンダード対応 BEGIN↓
                --AND   JOK.JokyakuDate <= @SinkokuKikanEnd )
                AND   JOK.JokyakuDate <= @SinkokuKikanEnd
                AND   JOK.ShoukyakuKBN = 0 )
                --2010/12/09 M.Shinbo 変更 (N00697)IFRS・ダブルスタンダード対応 END↑
        --  2013/01/15 S.Murakami  (N00995)分割合併対応↓
        --AND  (@p_ZennenPrintKBN = 0 OR (SIS.ShutokuDate BETWEEN @SinkokuKikanBegin AND @SinkokuKikanEnd))
--2015/12/08 T.Shiraishi  修正  (33520)地方税の申告対象となる日付を取得日ではなく償却開始日にする↓
--        AND  (@p_ZennenPrintKBN = 0 OR (ISNULL(BGJ.BunkatuGappeiDate,SIS.ShutokuDate) BETWEEN @SinkokuKikanBegin AND @SinkokuKikanEnd))
        AND  (@p_ZennenPrintKBN = 0 OR (ISNULL(BGJ.BunkatuGappeiDate,CASE @ShutokuDateHanteiKBN WHEN 0 THEN SIS.ShutokuDate ELSE SIS.ShoukyakuKaisiDate END) BETWEEN @SinkokuKikanBegin AND @SinkokuKikanEnd))
--2015/12/08 T.Shiraishi  修正  (33520)地方税の申告対象となる日付を取得日ではなく償却開始日にする↑
        --  2013/01/15 S.Murakami  (N00995)分割合併対応↑

        SET @ErrorCode = @@ERROR
        IF @ErrorCode <> 0 GOTO ERR_SHORI

        -- 3.減少資産データ抽出
        -- 3.1.(除却)
        INSERT INTO #N0000WK_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Genshou (
                  SisanNO
                , Edaban
                , ZiyuuKBN
                , SisanShubetuNO
                , ZigyoushoCD
                , ZititaiCD
                , TihouzeiShutokuKagaku
                )
        SELECT
                  SIS.SisanNO
                , SIS.Edaban
                , 2
                , SIS.SisanShubetuNO
                , CASE WHEN IDO.SisanNO IS NULL
                       THEN SIS.ZigyoushoCD
                       ELSE IDO.IdoumotoZigyoushoCD
                  END
                , CASE WHEN IDO.SisanNO IS NULL
                       THEN SIS.ZititaiCD
                       ELSE IDO.IdoumotoZititaiCD
                  END
                , SIS.TihouzeiShutokuKagaku
        FROM    N0000RR_NA_SisanJouhou SIS
        INNER JOIN N0000RR_NA_JokyakuJouhou JOK
        ON    SIS.SisanNO  = JOK.SisanNO
        AND   SIS.Edaban   = JOK.Edaban
        --2010/12/09 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 BEGIN↓
        AND   JOK.ShoukyakuKBN = 0
        --2010/12/09 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 END↑
        LEFT JOIN ( SELECT IDO.SisanNO
                          ,IDO.Edaban
                          ,IDO.IdoumotoZigyoushoCD
                          ,IDO.IdoumotoZititaiCD
                    FROM N0000RR_NA_IdouJouhou IDO
                    INNER JOIN (
                                SELECT
                                         IDO.SisanNO
                                        ,IDO.Edaban
                                        ,Min(IDO.IdouDate) AS MinIdouDate
                                FROM    N0000RR_NA_IdouJouhou IDO
                                WHERE IDO.IdouDate  >= @SinkokuKikanBegin
                                AND   IDO.IdouDate  <= @SinkokuKikanEnd
                                --2011/01/18 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 戻り対応 BEGIN↓
                                AND   IDO.ShoukyakuKBN  = 0
                                --2011/01/18 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 戻り対応 END↑
                                GROUP BY IDO.SisanNO, IDO.Edaban) MINIDO
                    ON         IDO.SisanNO  = MINIDO.SisanNO
                    AND        IDO.Edaban   = MINIDO.Edaban
                    AND        IDO.IdouDate = MINIDO.MinIdouDate
                    --2010/12/09 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 BEGIN↓
                    AND        IDO.ShoukyakuKBN = 0
                    --2010/12/09 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 END↑
                   ) IDO
        ON         SIS.SisanNO  = IDO.SisanNO
        AND        SIS.Edaban   = IDO.Edaban
        --  2013/01/15 S.Murakami  (N00995)分割合併対応↓
        LEFT JOIN N0000RR_NA_BunkatuGappeiJouhou BGJ
        ON        BGJ.SisanNO      = SIS.SisanNO
        AND       BGJ.Edaban       = SIS.Edaban
        AND       BGJ.ShoukyakuKBN = 0
        AND       BGJ.ZouGenKBN    = 0
        --WHERE SIS.ShutokuDate < @SinkokuKikanBegin -- ※申告期間内で(取得 → 除却)のデータは対象外
--2015/12/08 T.Shiraishi  修正  (33520)地方税の申告対象となる日付を取得日ではなく償却開始日にする↓
--        WHERE ISNULL(BGJ.BunkatuGappeiDate,SIS.ShutokuDate) < @SinkokuKikanBegin -- ※申告期間内で(取得 → 除却)のデータは対象外
        WHERE ISNULL(BGJ.BunkatuGappeiDate,CASE @ShutokuDateHanteiKBN WHEN 0 THEN SIS.ShutokuDate ELSE SIS.ShoukyakuKaisiDate END) < @SinkokuKikanBegin -- ※申告期間内で(取得 → 除却)のデータは対象外
--2015/12/08 T.Shiraishi  修正  (33520)地方税の申告対象となる日付を取得日ではなく償却開始日にする↑
        --  2013/01/15 S.Murakami  (N00995)分割合併対応↑
        AND   JOK.JokyakuDate BETWEEN @SinkokuKikanBegin AND @SinkokuKikanEnd
        --2010/12/09 M.Shinbo 変更 (N00697)IFRS・ダブルスタンダード対応 BEGIN↓
        --AND  (@p_SisanKBN = 2 OR (SIS.KarisisanFlag = @p_SisanKBN))
        AND ((@p_SisanKBN <> 2 AND SIS.KarisisanFlag = @p_SisanKBN)
        OR   (@p_SisanKBN = 2 AND (SIS.KarisisanFlag = 0 OR SIS.KarisisanFlag = 1)))
        --2010/12/09 M.Shinbo 変更 (N00697)IFRS・ダブルスタンダード対応 END↑
        AND   SIS.SinkokuKBN IN ( 0, 2 )
        AND   SIS.SisanShubetuNO BETWEEN 1 AND 6

        SET @ErrorCode = @@ERROR
        IF @ErrorCode <> 0 GOTO ERR_SHORI

        -- 3.2.(移動)
        INSERT INTO #N0000WK_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Genshou (
                  SisanNO
                , Edaban
                , ZiyuuKBN
                , SisanShubetuNO
                , ZigyoushoCD
                , ZititaiCD
                , TihouzeiShutokuKagaku
                )
        SELECT
                  SIS.SisanNO
                , SIS.Edaban
                , 3
                , SIS.SisanShubetuNO
--↓2011/01/06 S.Harada 修正 （N00727）自治体移動が複数回あった場合に、出力内容が不正になる不具合修正
--                , MAXIDO.IdoumotoZigyoushoCD
--                , MAXIDO.IdoumotoZititaiCD
                , MINIDO.IdoumotoZigyoushoCD
                , MINIDO.IdoumotoZititaiCD
--↑2011/01/06 S.Harada 修正 （N00727）自治体移動が複数回あった場合に、出力内容が不正になる不具合修正
                , SIS.TihouzeiShutokuKagaku
        FROM    N0000RR_NA_SisanJouhou SIS
        INNER JOIN (
                    SELECT
                             IDO.SisanNO
                            ,IDO.Edaban
                            ,MAX(IDO.IdouDate) AS MaxIdouDate
                            ,MIN(IDO.IdouDate) AS MinIdouDate
                    FROM    N0000RR_NA_IdouJouhou IDO
                    WHERE IDO.IdouDate  >= @SinkokuKikanBegin
                    AND   IDO.IdouDate  <= @SinkokuKikanEnd
                    --2010/12/09 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 BEGIN↓
                    AND   IDO.ShoukyakuKBN = 0
                    --2010/12/09 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 END↑
                    GROUP BY IDO.SisanNO, IDO.Edaban) IDOSUB
        ON         SIS.SisanNO  = IDOSUB.SisanNO
        AND        SIS.Edaban   = IDOSUB.Edaban
        INNER JOIN N0000RR_NA_IdouJouhou MAXIDO
        ON    MAXIDO.SisanNO  = IDOSUB.SisanNO
        AND   MAXIDO.Edaban   = IDOSUB.Edaban
        AND   MAXIDO.IdouDate = IDOSUB.MaxIdouDate
        --2011/01/18 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 戻り対応 BEGIN↓
        AND   MAXIDO.ShoukyakuKBN = 0
        --2011/01/18 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 戻り対応 END↑
        INNER JOIN N0000RR_NA_IdouJouhou MINIDO
        ON    MINIDO.SisanNO  = IDOSUB.SisanNO
        AND   MINIDO.Edaban   = IDOSUB.Edaban
        AND   MINIDO.IdouDate = IDOSUB.MinIdouDate
        --2011/01/18 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 戻り対応 BEGIN↓
        AND   MINIDO.ShoukyakuKBN = 0
        --2011/01/18 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 戻り対応 END↑
        --  2013/01/15 S.Murakami  (N00995)分割合併対応↓
        LEFT JOIN N0000RR_NA_BunkatuGappeiJouhou BGJ
        ON        BGJ.SisanNO      = SIS.SisanNO
        AND       BGJ.Edaban       = SIS.Edaban
        AND       BGJ.ShoukyakuKBN = 0
        AND       BGJ.ZouGenKBN    = 0
        --  2013/01/15 S.Murakami  (N00995)分割合併対応↑
        WHERE MAXIDO.IdouDate BETWEEN @SinkokuKikanBegin AND @SinkokuKikanEnd
        --2010/12/09 M.Shinbo 変更 (N00697)IFRS・ダブルスタンダード対応 BEGIN↓
        --AND  (@p_SisanKBN = 2 OR (SIS.KarisisanFlag = @p_SisanKBN))
        AND ((@p_SisanKBN <> 2 AND SIS.KarisisanFlag = @p_SisanKBN)
        OR   (@p_SisanKBN = 2 AND (SIS.KarisisanFlag = 0 OR SIS.KarisisanFlag = 1)))
        --2010/12/09 M.Shinbo 変更 (N00697)IFRS・ダブルスタンダード対応 END↑
        AND   SIS.SinkokuKBN IN ( 0, 2 )
        AND   SIS.SisanShubetuNO BETWEEN 1 AND 6
--↓2011/01/20 S.Harada 修正（N00748）複数回移動があり、最終の移動が自治体に絡まない移動の時、出力が不正になる不具合修正
--        AND ((@p_ShuturyokuKBN = 0 AND MAXIDO.IdoumotoZititaiCD <> MAXIDO.IdousakiZititaiCD) OR @p_ShuturyokuKBN = 1)
        AND ((@p_ShuturyokuKBN = 0 AND MINIDO.IdoumotoZititaiCD <> MAXIDO.IdousakiZititaiCD) OR @p_ShuturyokuKBN = 1)
--↑2011/01/20 S.Harada 修正（N00748）複数回移動があり、最終の移動が自治体に絡まない移動の時、出力が不正になる不具合修正
        AND ((MINIDO.IdoumotoZititaiCD   <> MAXIDO.IdousakiZititaiCD)
        OR   (MINIDO.IdoumotoZigyoushoCD <> MAXIDO.IdousakiZigyoushoCD))
        --  2013/01/15 S.Murakami  (N00995)分割合併対応↓
        --AND SIS.ShutokuDate < @SinkokuKikanBegin
--2015/12/08 T.Shiraishi  修正  (33520)地方税の申告対象となる日付を取得日ではなく償却開始日にする↓
--        AND ISNULL(BGJ.BunkatuGappeiDate,SIS.ShutokuDate) < @SinkokuKikanBegin
        AND ISNULL(BGJ.BunkatuGappeiDate,CASE @ShutokuDateHanteiKBN WHEN 0 THEN SIS.ShutokuDate ELSE SIS.ShoukyakuKaisiDate END) < @SinkokuKikanBegin
--2015/12/08 T.Shiraishi  修正  (33520)地方税の申告対象となる日付を取得日ではなく償却開始日にする↑
        --  2013/01/15 S.Murakami  (N00995)分割合併対応↑
        AND NOT EXISTS (
                SELECT RIR.SisanNO FROM N0000RR_NA_RirekiJouhou RIR
                WHERE RIR.SisanNO     = SIS.SisanNO
                AND   RIR.Edaban      = SIS.Edaban
                AND   RIR.RirekiDate <= @SinkokuKikanEnd
                --2010/12/09 M.Shinbo 変更 (N00697)IFRS・ダブルスタンダード対応 BEGIN↓
                --AND   RIR.ZiyuuKBN    = 2 )
                --  2013/01/15 S.Murakami  (N00995)分割合併対応↓
                --AND   RIR.ZiyuuKBN    = 2
                AND   RIR.ZiyuuKBN    IN(2,20)
                --  2013/01/15 S.Murakami  (N00995)分割合併対応↑
                AND   RIR.ShoukyakuKBN = 0 )
                --2010/12/09 M.Shinbo 変更 (N00697)IFRS・ダブルスタンダード対応 END↑

        SET @ErrorCode = @@ERROR
        IF @ErrorCode <> 0 GOTO ERR_SHORI

        -- 4.増加資産データ抽出
        -- 4.1.(取得)
        INSERT INTO #N0000WK_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Zouka (
                  SisanNO
                , Edaban
                , ZiyuuKBN
                , SisanShubetuNO
                , ZigyoushoCD
                , ZititaiCD
                , TihouzeiShutokuKagaku
                )
        SELECT
                  SIS.SisanNO
                , SIS.Edaban
                , 1
                , SIS.SisanShubetuNO
                , CASE WHEN IDO.SisanNO IS NULL
                       THEN SHU.ShutokuZigyoushoCD
                       ELSE IDO.IdousakiZigyoushoCD
                  END
                , CASE WHEN IDO.SisanNO IS NULL
                       THEN SHU.ShutokuZititaiCD
                       ELSE IDO.IdousakiZititaiCD
                  END
                , SIS.TihouzeiShutokuKagaku
        FROM    N0000RR_NA_SisanJouhou SIS
        INNER JOIN N0000RR_NA_ShutokuJouhou SHU
        ON    SIS.SisanNO  = SHU.SisanNO
        AND   SIS.Edaban   = SHU.Edaban
        --2010/12/09 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 BEGIN↓
        AND   SHU.ShoukyakuKBN = 0
 --2010/12/09 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 END↑
        LEFT JOIN ( SELECT IDO.SisanNO
                          ,IDO.Edaban
                          ,IDO.IdousakiZigyoushoCD
                          ,IDO.IdousakiZititaiCD
                    FROM N0000RR_NA_IdouJouhou IDO
                    INNER JOIN (
                                SELECT
                                         IDO.SisanNO
                                        ,IDO.Edaban
                                        ,MAX(IDO.IdouDate) AS MaxIdouDate
                                FROM    N0000RR_NA_IdouJouhou IDO
                                WHERE IDO.IdouDate  >= @SinkokuKikanBegin
                                AND   IDO.IdouDate  <= @SinkokuKikanEnd
                                --2011/01/18 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 戻り対応 BEGIN↓
                                AND   IDO.ShoukyakuKBN = 0
                                --2011/01/18 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 戻り対応 END↑
                                GROUP BY IDO.SisanNO, IDO.Edaban) MAXIDO
                    ON         IDO.SisanNO  = MAXIDO.SisanNO
                    AND        IDO.Edaban   = MAXIDO.Edaban
                    AND        IDO.IdouDate = MAXIDO.MaxIdouDate
                    --2010/12/09 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 BEGIN↓
                    AND        IDO.ShoukyakuKBN = 0
                    --2010/12/09 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 END↑
                   ) IDO
        ON         SIS.SisanNO  = IDO.SisanNO
        AND        SIS.Edaban   = IDO.Edaban
        --  2013/01/15 S.Murakami  (N00995)分割合併対応↓
        LEFT JOIN N0000RR_NA_BunkatuGappeiJouhou BGJ
        ON        BGJ.SisanNO      = SIS.SisanNO
        AND       BGJ.Edaban       = SIS.Edaban
        AND       BGJ.ShoukyakuKBN = 0
        AND       BGJ.ZouGenKBN    = 0
        --WHERE SHU.ShutokuDate BETWEEN @SinkokuKikanBegin AND @SinkokuKikanEnd
--2015/12/08 T.Shiraishi  修正  (33520)地方税の申告対象となる日付を取得日ではなく償却開始日にする↓
--        WHERE ISNULL(BGJ.BunkatuGappeiDate,SHU.ShutokuDate) BETWEEN @SinkokuKikanBegin AND @SinkokuKikanEnd
        WHERE ISNULL(BGJ.BunkatuGappeiDate,CASE @ShutokuDateHanteiKBN WHEN 0 THEN SIS.ShutokuDate ELSE SIS.ShoukyakuKaisiDate END) BETWEEN @SinkokuKikanBegin AND @SinkokuKikanEnd
--2015/12/08 T.Shiraishi  修正  (33520)地方税の申告対象となる日付を取得日ではなく償却開始日にする↑
        --  2013/01/15 S.Murakami  (N00995)分割合併対応↑
        --2010/12/09 M.Shinbo 変更 (N00697)IFRS・ダブルスタンダード対応 BEGIN↓
        --AND  (@p_SisanKBN = 2 OR (SIS.KarisisanFlag = @p_SisanKBN))
        AND ((@p_SisanKBN <> 2 AND SIS.KarisisanFlag = @p_SisanKBN)
        OR   (@p_SisanKBN = 2 AND (SIS.KarisisanFlag = 0 OR SIS.KarisisanFlag = 1)))
        --2010/12/09 M.Shinbo 変更 (N00697)IFRS・ダブルスタンダード対応 END↑
        AND   SIS.SinkokuKBN IN ( 0, 2 )
        AND   SIS.SisanShubetuNO BETWEEN 1 AND 6

        AND NOT EXISTS (
                SELECT RIR.SisanNO FROM N0000RR_NA_RirekiJouhou RIR
                WHERE RIR.SisanNO     = SIS.SisanNO
                AND   RIR.Edaban      = SIS.Edaban
                AND   RIR.RirekiDate <= @SinkokuKikanEnd
                --2010/12/09 M.Shinbo 変更 (N00697)IFRS・ダブルスタンダード対応 BEGIN↓
                --AND   RIR.ZiyuuKBN    = 2 )
                --  2013/01/15 S.Murakami  (N00995)分割合併対応↓
                --AND   RIR.ZiyuuKBN    = 2
                AND   RIR.ZiyuuKBN    IN(2,20)
                --  2013/01/15 S.Murakami  (N00995)分割合併対応↑
                AND   RIR.ShoukyakuKBN = 0 )
                --2010/12/09 M.Shinbo 変更 (N00697)IFRS・ダブルスタンダード対応 END↑

        SET @ErrorCode = @@ERROR
        IF @ErrorCode <> 0 GOTO ERR_SHORI

        -- 4.2.(移動)
        INSERT INTO #N0000WK_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Zouka (
                  SisanNO
                , Edaban
                , ZiyuuKBN
                , SisanShubetuNO
                , ZigyoushoCD
                , ZititaiCD
                , TihouzeiShutokuKagaku
                )
        SELECT
                  SIS.SisanNO
                , SIS.Edaban
                , 3
                , SIS.SisanShubetuNO
                , MAXIDO.IdousakiZigyoushoCD
                , MAXIDO.IdousakiZititaiCD
                , SIS.TihouzeiShutokuKagaku
        FROM    N0000RR_NA_SisanJouhou SIS
        INNER JOIN (
                    SELECT
                             IDO.SisanNO
                            ,IDO.Edaban
                            ,MAX(IDO.IdouDate) AS MaxIdouDate
                            ,MIN(IDO.IdouDate) AS MinIdouDate
                    FROM    N0000RR_NA_IdouJouhou IDO
                    WHERE IDO.IdouDate  >= @SinkokuKikanBegin
                    AND   IDO.IdouDate  <= @SinkokuKikanEnd
                    --2010/12/09 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 BEGIN↓
                    AND   IDO.ShoukyakuKBN = 0
                    --2010/12/09 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 END↑
                    GROUP BY IDO.SisanNO, IDO.Edaban) IDOSUB
        ON         SIS.SisanNO  = IDOSUB.SisanNO
        AND        SIS.Edaban   = IDOSUB.Edaban
        INNER JOIN N0000RR_NA_IdouJouhou MAXIDO
        ON    MAXIDO.SisanNO  = IDOSUB.SisanNO
        AND   MAXIDO.Edaban   = IDOSUB.Edaban
        AND   MAXIDO.IdouDate = IDOSUB.MaxIdouDate
        --2011/01/18 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 戻り対応 BEGIN↓
        AND   MAXIDO.ShoukyakuKBN = 0
        --2011/01/18 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 戻り対応 END↑
        INNER JOIN N0000RR_NA_IdouJouhou MINIDO
        ON    MINIDO.SisanNO  = IDOSUB.SisanNO
        AND   MINIDO.Edaban   = IDOSUB.Edaban
        AND   MINIDO.IdouDate = IDOSUB.MinIdouDate
        --2011/01/18 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 戻り対応 BEGIN↓
        AND   MINIDO.ShoukyakuKBN = 0
        --2011/01/18 M.Shinbo 追加 (N00697)IFRS・ダブルスタンダード対応 戻り対応 END↑
        LEFT JOIN #N0000WK_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Zouka ZOU
        ON    ZOU.SisanNO     = SIS.SisanNO
        AND   ZOU.Edaban      = SIS.Edaban
        --  2013/01/15 S.Murakami  (N00995)分割合併対応↓
        LEFT JOIN N0000RR_NA_BunkatuGappeiJouhou BGJ
        ON        BGJ.SisanNO      = SIS.SisanNO
        AND       BGJ.Edaban       = SIS.Edaban
        AND       BGJ.ShoukyakuKBN = 0
        AND       BGJ.ZouGenKBN    = 0
        --  2013/01/15 S.Murakami  (N00995)分割合併対応↑
        WHERE MAXIDO.IdouDate BETWEEN @SinkokuKikanBegin AND @SinkokuKikanEnd
        --2010/12/09 M.Shinbo 変更 (N00697)IFRS・ダブルスタンダード対応 BEGIN↓
        --AND  (@p_SisanKBN = 2 OR (SIS.KarisisanFlag = @p_SisanKBN))
        AND ((@p_SisanKBN <> 2 AND SIS.KarisisanFlag = @p_SisanKBN)
        OR   (@p_SisanKBN = 2 AND (SIS.KarisisanFlag = 0 OR SIS.KarisisanFlag = 1)))
        --2010/12/09 M.Shinbo 変更 (N00697)IFRS・ダブルスタンダード対応 END↑
        AND   SIS.SinkokuKBN IN ( 0, 2 )
        AND   SIS.SisanShubetuNO BETWEEN 1 AND 6
--↓2011/01/20 S.Harada 修正（N00748）複数回移動があり、最終の移動が自治体に絡まない移動の時、出力が不正になる不具合修正
--        AND ((@p_ShuturyokuKBN = 0 AND MAXIDO.IdoumotoZititaiCD <> MAXIDO.IdousakiZititaiCD) OR @p_ShuturyokuKBN = 1)
        AND ((@p_ShuturyokuKBN = 0 AND MINIDO.IdoumotoZititaiCD <> MAXIDO.IdousakiZititaiCD) OR @p_ShuturyokuKBN = 1)
--↑2011/01/20 S.Harada 修正（N00748）複数回移動があり、最終の移動が自治体に絡まない移動の時、出力が不正になる不具合修正
        AND ((MINIDO.IdoumotoZititaiCD   <> MAXIDO.IdousakiZititaiCD)
        OR   (MINIDO.IdoumotoZigyoushoCD <> MAXIDO.IdousakiZigyoushoCD))
        --  2013/01/15 S.Murakami  (N00995)分割合併対応↓
        AND ISNULL(BGJ.BunkatuGappeiDate, SIS.ShutokuDate) < @SinkokuKikanBegin
        --  2013/01/15 S.Murakami  (N00995)分割合併対応↑
        AND NOT EXISTS (
                SELECT RIR.SisanNO FROM N0000RR_NA_RirekiJouhou RIR
                WHERE RIR.SisanNO     = SIS.SisanNO
                AND   RIR.Edaban      = SIS.Edaban
                AND   RIR.RirekiDate <= @SinkokuKikanEnd 
                --2010/12/09 M.Shinbo 変更 (N00697)IFRS・ダブルスタンダード対応 BEGIN↓
            --AND   RIR.ZiyuuKBN    = 2 )
                --  2013/01/15 S.Murakami  (N00995)分割合併対応↓
                --AND   RIR.ZiyuuKBN    = 2
                AND   RIR.ZiyuuKBN    IN(2,20)
                --  2013/01/15 S.Murakami  (N00995)分割合併対応↑
                AND   RIR.ShoukyakuKBN = 0 )
                --2010/12/09 M.Shinbo 変更 (N00697)IFRS・ダブルスタンダード対応 END↑
        AND  ZOU.SisanNO IS NULL

        SET @ErrorCode = @@ERROR
        IF @ErrorCode <> 0 GOTO ERR_SHORI

        -- 5.1.「集計ワーク」へ更新(全資産申告ワークの内容)

        SET @strSQL = ''
        SET @strSQL = @strSQL + ' INSERT INTO #N0000WK_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Shukei (' +CHAR(13)
        SET @strSQL = @strSQL + '           SisanShubetuNO' +CHAR(13)
        SET @strSQL = @strSQL + '         , ZigyoushoCD' +CHAR(13)
        SET @strSQL = @strSQL + '         , ZititaiCD' +CHAR(13)
        SET @strSQL = @strSQL + '         , ShutokuKagaku1' +CHAR(13)
        SET @strSQL = @strSQL + '         , ShutokuKagaku2' +CHAR(13)
        SET @strSQL = @strSQL + '         , ShutokuKagaku3' +CHAR(13)
        SET @strSQL = @strSQL + '         , ShutokuKagaku4' +CHAR(13)
        SET @strSQL = @strSQL + '         , HonnendoHyoukagaku' +CHAR(13)
        SET @strSQL = @strSQL + '         , Ketteikagaku' +CHAR(13)
        SET @strSQL = @strSQL + '         , KazeiHyoujungaku' +CHAR(13)
        SET @strSQL = @strSQL + '         , Kensuu' +CHAR(13)
        SET @strSQL = @strSQL + '         , KeiKBN' +CHAR(13)
        SET @strSQL = @strSQL + '         )' +CHAR(13)
        SET @strSQL = @strSQL + ' SELECT' +CHAR(13)
        SET @strSQL = @strSQL + '           WK.SisanShubetuNO' +CHAR(13)
        SET @strSQL = @strSQL + '         , CASE WHEN ' + CONVERT(varchar,@p_ShuturyokuKBN) + ' = 1 THEN WK.ZigyoushoCD ELSE ''*'' END' +CHAR(13)
        SET @strSQL = @strSQL + '         , WK.ZititaiCD' +CHAR(13)
        SET @strSQL = @strSQL + '         , 0' +CHAR(13)
        SET @strSQL = @strSQL + '         , 0' +CHAR(13)
        SET @strSQL = @strSQL + '         , 0' +CHAR(13)
        SET @strSQL = @strSQL + '         , Sum(ISNULL(WK.TihouzeiShutokuKagaku,0))' +CHAR(13)
        SET @strSQL = @strSQL + '         , Sum(ISNULL(WK.HonnendoHyoukagaku,0))' +CHAR(13)
        SET @strSQL = @strSQL + '         , 0' +CHAR(13)
        SET @strSQL = @strSQL + '         , Sum(CASE WHEN ISNULL(WK.TokureirituNumerator,0) <> 0 AND ISNULL(WK.TokureirituDenominator,0) <> 0 AND (WK.TokureiKikan IS NULL Or YEAR(WK.TokureiKikan) >= ''' + CONVERT(varchar,YEAR(@p_SinkokuNendo)) + ''')' +CHAR(13)
        SET @strSQL = @strSQL + '                    THEN FLOOR(ISNULL(WK.HonnendoHyoukagaku,0) * WK.TokureirituNumerator / WK.TokureirituDenominator + 0.500000)' +CHAR(13)
-- ↓ 2020/11/16 N.Maeda      修正  (149911)特例率ゼロが登録できません。
        SET @strSQL = @strSQL + '                    WHEN (WK.TokureirituNumerator = 0 OR WK.TokureirituDenominator = 0) AND (WK.TokureiKikan IS NULL Or YEAR(WK.TokureiKikan) >= ''' + CONVERT(varchar,YEAR(@p_SinkokuNendo)) + ''')' +CHAR(13)
        SET @strSQL = @strSQL + '                    THEN 0' +CHAR(13)
-- ↑ 2020/11/16 N.Maeda      修正  (149911)特例率ゼロが登録できません。
        SET @strSQL = @strSQL + '                    ELSE ISNULL(WK.HonnendoHyoukagaku,0)' +CHAR(13)
        SET @strSQL = @strSQL + '               END)' +CHAR(13)
        SET @strSQL = @strSQL + '         , COUNT(WK.SisanNO)' +CHAR(13)
        SET @strSQL = @strSQL + '         , 0' +CHAR(13)
        SET @strSQL = @strSQL + ' FROM    #N0000WK_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Zensisan WK' +CHAR(13)
        -- 引数.事業所セキュリティパターンコードが""でないとき、以下のJOIN句を追加
        IF (@p_ZigSecPtnCD IS NOT NULL)
        BEGIN
            SET @strSQL = @strSQL + ' INNER JOIN ' +CHAR(13)
            SET @strSQL = @strSQL + '     M0000MS_MA_ZigyoushoSecurityYouso MZSY ' +CHAR(13) -- 事業所セキュリティ要素マスタ
            SET @strSQL = @strSQL + ' ON  MZSY.ZigyoushoCDYouso   = WK.ZigyoushoCD ' +CHAR(13)
            SET @strSQL = @strSQL + ' AND MZSY.SecurityPatternCD  = ' + dbo.N0000FC_Escape_Varchar(@p_ZigSecPtnCD) +CHAR(13)
        END

        SET @strSQL = @strSQL + ' WHERE 1 = 1' +CHAR(13)
        --事業所コード範囲指定
        --2013/02/19 Y.Fujita 修正 (N01023) 開始 -->
        --IF @p_ZigyoushoCDBegin IS NOT NULL
        --BEGIN
        --    SET @strSQL = @strSQL + ' AND WK.ZigyoushoCD >= ''' + CONVERT(varchar(6),@p_ZigyoushoCDBegin) + '''' +CHAR(13)
        --END
        --IF @p_ZigyoushoCDEnd IS NOT NULL
        --BEGIN
        --    SET @strSQL = @strSQL + ' AND WK.ZigyoushoCD <= ''' + CONVERT(varchar(6),@p_ZigyoushoCDEnd) + '''' +CHAR(13)
        --END
        IF @p_ShuturyokuKBN = 1
        BEGIN
            IF @p_ZigyoushoCDBegin IS NOT NULL
            BEGIN
                SET @strSQL = @strSQL + ' AND WK.ZigyoushoCD >= ''' + CONVERT(varchar(6),@p_ZigyoushoCDBegin) + '''' +CHAR(13)
            END
            IF @p_ZigyoushoCDEnd IS NOT NULL
            BEGIN
                SET @strSQL = @strSQL + ' AND WK.ZigyoushoCD <= ''' + CONVERT(varchar(6),@p_ZigyoushoCDEnd) + '''' +CHAR(13)
            END
        END
        --2013/02/19 Y.Fujita 修正 (N01023) 終了 <--
        --自治体コード範囲指定
        IF @p_ZititaiCDBegin IS NOT NULL
        BEGIN
            SET @strSQL = @strSQL + ' AND WK.ZititaiCD >= ''' + CONVERT(varchar(6),@p_ZititaiCDBegin) + '''' +CHAR(13)
        END
        IF @p_ZititaiCDEnd IS NOT NULL
        BEGIN
            SET @strSQL = @strSQL + ' AND WK.ZititaiCD <= ''' + CONVERT(varchar(6),@p_ZititaiCDEnd) + '''' +CHAR(13)
        END
        IF @p_ShuturyokuKBN = 1
        BEGIN
            SET @strSQL = @strSQL + ' GROUP BY WK.ZigyoushoCD ,WK.ZititaiCD ,WK.SisanShubetuNO' +CHAR(13)
        END
        ELSE
        BEGIN
            SET @strSQL = @strSQL + ' GROUP BY WK.ZititaiCD ,WK.SisanShubetuNO' +CHAR(13)
        END

        EXEC (@strSQL)

        SET @ErrorCode = @@ERROR
        IF @ErrorCode <> 0 GOTO ERR_SHORI

        -- 5.2.「集計ワーク」へ更新(減少資産ワークの内容)
        SET @strSQL = ''
        SET @strSQL = @strSQL + ' INSERT INTO #N0000WK_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Shukei (' +CHAR(13)
        SET @strSQL = @strSQL + '           SisanShubetuNO' +CHAR(13)
        SET @strSQL = @strSQL + '         , ZigyoushoCD' +CHAR(13)
        SET @strSQL = @strSQL + '         , ZititaiCD' +CHAR(13)
        SET @strSQL = @strSQL + '         , ShutokuKagaku1' +CHAR(13)
        SET @strSQL = @strSQL + '         , ShutokuKagaku2' +CHAR(13)
        SET @strSQL = @strSQL + '         , ShutokuKagaku3' +CHAR(13)
        SET @strSQL = @strSQL + '         , ShutokuKagaku4' +CHAR(13)
        SET @strSQL = @strSQL + '         , HonnendoHyoukagaku' +CHAR(13)
        SET @strSQL = @strSQL + '         , Ketteikagaku' +CHAR(13)
        SET @strSQL = @strSQL + '         , KazeiHyoujungaku' +CHAR(13)
        SET @strSQL = @strSQL + '         , Kensuu' +CHAR(13)
        SET @strSQL = @strSQL + '         , KeiKBN' +CHAR(13)
        SET @strSQL = @strSQL + '         )' +CHAR(13)
        SET @strSQL = @strSQL + ' SELECT' +CHAR(13)
        SET @strSQL = @strSQL + '           WK.SisanShubetuNO' +CHAR(13)
        SET @strSQL = @strSQL + '         , CASE WHEN ' + CONVERT(varchar,@p_ShuturyokuKBN) + ' = 1 THEN WK.ZigyoushoCD ELSE ''*'' END' +CHAR(13)
        SET @strSQL = @strSQL + '         , WK.ZititaiCD' +CHAR(13)
        SET @strSQL = @strSQL + '         , 0' +CHAR(13)
        SET @strSQL = @strSQL + '         , Sum(ISNULL(WK.TihouzeiShutokuKagaku,0))' +CHAR(13)
        SET @strSQL = @strSQL + '         , 0' +CHAR(13)
        SET @strSQL = @strSQL + '         , 0' +CHAR(13)
        SET @strSQL = @strSQL + '         , 0' +CHAR(13)
        SET @strSQL = @strSQL + '         , 0' +CHAR(13)
        SET @strSQL = @strSQL + '         , 0' +CHAR(13)
        SET @strSQL = @strSQL + '         , 0' +CHAR(13)
        SET @strSQL = @strSQL + '         , 0' +CHAR(13)
        SET @strSQL = @strSQL + ' FROM    #N0000WK_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Genshou WK' +CHAR(13)
        -- 引数.事業所セキュリティパターンコードが""でないとき、以下のJOIN句を追加
        IF (@p_ZigSecPtnCD IS NOT NULL)
        BEGIN
            SET @strSQL = @strSQL + ' INNER JOIN ' +CHAR(13)
            SET @strSQL = @strSQL + '     M0000MS_MA_ZigyoushoSecurityYouso MZSY ' +CHAR(13) -- 事業所セキュリティ要素マスタ
            SET @strSQL = @strSQL + ' ON  MZSY.ZigyoushoCDYouso   = WK.ZigyoushoCD ' +CHAR(13)
            SET @strSQL = @strSQL + ' AND MZSY.SecurityPatternCD  = ' + dbo.N0000FC_Escape_Varchar(@p_ZigSecPtnCD) +CHAR(13)
        END

        SET @strSQL = @strSQL + ' WHERE 1 = 1' +CHAR(13)
        --事業所コード範囲指定
        --2013/02/19 Y.Fujita 修正 (N01023) 開始 -->
        --IF @p_ZigyoushoCDBegin IS NOT NULL
        --BEGIN
        --    SET @strSQL = @strSQL + ' AND WK.ZigyoushoCD >= ''' + CONVERT(varchar(6),@p_ZigyoushoCDBegin) + '''' +CHAR(13)
        --END
        --IF @p_ZigyoushoCDEnd IS NOT NULL
        --BEGIN
        --    SET @strSQL = @strSQL + ' AND WK.ZigyoushoCD <= ''' + CONVERT(varchar(6),@p_ZigyoushoCDEnd) + '''' +CHAR(13)
        --END
        IF @p_ShuturyokuKBN = 1
        BEGIN
            IF @p_ZigyoushoCDBegin IS NOT NULL
            BEGIN
                SET @strSQL = @strSQL + ' AND WK.ZigyoushoCD >= ''' + CONVERT(varchar(6),@p_ZigyoushoCDBegin) + '''' +CHAR(13)
            END
            IF @p_ZigyoushoCDEnd IS NOT NULL
            BEGIN
                SET @strSQL = @strSQL + ' AND WK.ZigyoushoCD <= ''' + CONVERT(varchar(6),@p_ZigyoushoCDEnd) + '''' +CHAR(13)
            END
        END
        --2013/02/19 Y.Fujita 修正 (N01023) 終了 <--
        --自治体コード範囲指定
        IF @p_ZititaiCDBegin IS NOT NULL
        BEGIN
            SET @strSQL = @strSQL + ' AND WK.ZititaiCD >= ''' + CONVERT(varchar(6),@p_ZititaiCDBegin) + '''' +CHAR(13)
        END
        IF @p_ZititaiCDEnd IS NOT NULL
        BEGIN
            SET @strSQL = @strSQL + ' AND WK.ZititaiCD <= ''' + CONVERT(varchar(6),@p_ZititaiCDEnd) + '''' +CHAR(13)
        END
        IF @p_ShuturyokuKBN = 1
        BEGIN
            SET @strSQL = @strSQL + ' GROUP BY WK.ZigyoushoCD ,WK.ZititaiCD ,WK.SisanShubetuNO' +CHAR(13)
        END
        ELSE
        BEGIN
            SET @strSQL = @strSQL + ' GROUP BY WK.ZititaiCD ,WK.SisanShubetuNO' +CHAR(13)
        END

        EXEC (@strSQL)

        SET @ErrorCode = @@ERROR
        IF @ErrorCode <> 0 GOTO ERR_SHORI

        -- 5.3.「集計ワーク」へ更新(増加資産ワークの内容)
        SET @strSQL = ''
        SET @strSQL = @strSQL + ' INSERT INTO #N0000WK_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Shukei (' +CHAR(13)
        SET @strSQL = @strSQL + '           SisanShubetuNO' +CHAR(13)
        SET @strSQL = @strSQL + '         , ZigyoushoCD' +CHAR(13)
        SET @strSQL = @strSQL + '         , ZititaiCD' +CHAR(13)
        SET @strSQL = @strSQL + '         , ShutokuKagaku1' +CHAR(13)
        SET @strSQL = @strSQL + '         , ShutokuKagaku2' +CHAR(13)
        SET @strSQL = @strSQL + '         , ShutokuKagaku3' +CHAR(13)
        SET @strSQL = @strSQL + '         , ShutokuKagaku4' +CHAR(13)
        SET @strSQL = @strSQL + '         , HonnendoHyoukagaku' +CHAR(13)
        SET @strSQL = @strSQL + '         , Ketteikagaku' +CHAR(13)
        SET @strSQL = @strSQL + '         , KazeiHyoujungaku' +CHAR(13)
        SET @strSQL = @strSQL + '         , Kensuu' +CHAR(13)
        SET @strSQL = @strSQL + '         , KeiKBN' +CHAR(13)
        SET @strSQL = @strSQL + '         )' +CHAR(13)
        SET @strSQL = @strSQL + ' SELECT' +CHAR(13)
        SET @strSQL = @strSQL + '           WK.SisanShubetuNO' +CHAR(13)
        SET @strSQL = @strSQL + '         , CASE WHEN ' + CONVERT(varchar,@p_ShuturyokuKBN) + ' = 1 THEN WK.ZigyoushoCD ELSE ''*'' END' +CHAR(13)
        SET @strSQL = @strSQL + '         , WK.ZititaiCD' +CHAR(13)
        SET @strSQL = @strSQL + '         , 0' +CHAR(13)
        SET @strSQL = @strSQL + '         , 0' +CHAR(13)
        SET @strSQL = @strSQL + '         , Sum(ISNULL(WK.TihouzeiShutokuKagaku,0))' +CHAR(13)
        SET @strSQL = @strSQL + '         , 0' +CHAR(13)
        SET @strSQL = @strSQL + '         , 0' +CHAR(13)
        SET @strSQL = @strSQL + '         , 0' +CHAR(13)
        SET @strSQL = @strSQL + '         , 0' +CHAR(13)
        SET @strSQL = @strSQL + '         , 0' +CHAR(13)
        SET @strSQL = @strSQL + '         , 0' +CHAR(13)
        SET @strSQL = @strSQL + ' FROM    #N0000WK_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Zouka WK' +CHAR(13)
        -- 引数.事業所セキュリティパターンコードが""でないとき、以下のJOIN句を追加
        IF (@p_ZigSecPtnCD IS NOT NULL)
        BEGIN
            SET @strSQL = @strSQL + ' INNER JOIN ' +CHAR(13)
            SET @strSQL = @strSQL + '     M0000MS_MA_ZigyoushoSecurityYouso MZSY ' +CHAR(13) -- 事業所セキュリティ要素マスタ
            SET @strSQL = @strSQL + ' ON  MZSY.ZigyoushoCDYouso   = WK.ZigyoushoCD ' +CHAR(13)
            SET @strSQL = @strSQL + ' AND MZSY.SecurityPatternCD  = ' + dbo.N0000FC_Escape_Varchar(@p_ZigSecPtnCD) +CHAR(13)
        END

        SET @strSQL = @strSQL + ' WHERE 1 = 1' +CHAR(13)
        --事業所コード範囲指定
        --2013/02/19 Y.Fujita 修正 (N01023) 開始 -->
        --IF @p_ZigyoushoCDBegin IS NOT NULL
        --BEGIN
        --    SET @strSQL = @strSQL + ' AND WK.ZigyoushoCD >= ''' + CONVERT(varchar(6),@p_ZigyoushoCDBegin) + '''' +CHAR(13)
        --END
        --IF @p_ZigyoushoCDEnd IS NOT NULL
        --BEGIN
        --    SET @strSQL = @strSQL + ' AND WK.ZigyoushoCD <= ''' + CONVERT(varchar(6),@p_ZigyoushoCDEnd) + '''' +CHAR(13)
        --END
        IF @p_ShuturyokuKBN = 1
        BEGIN
            IF @p_ZigyoushoCDBegin IS NOT NULL
            BEGIN
                SET @strSQL = @strSQL + ' AND WK.ZigyoushoCD >= ''' + CONVERT(varchar(6),@p_ZigyoushoCDBegin) + '''' +CHAR(13)
            END
            IF @p_ZigyoushoCDEnd IS NOT NULL
            BEGIN
                SET @strSQL = @strSQL + ' AND WK.ZigyoushoCD <= ''' + CONVERT(varchar(6),@p_ZigyoushoCDEnd) + '''' +CHAR(13)
            END
        END
        --2013/02/19 Y.Fujita 修正 (N01023) 終了 <--
        --自治体コード範囲指定
        IF @p_ZititaiCDBegin IS NOT NULL
        BEGIN
            SET @strSQL = @strSQL + ' AND WK.ZititaiCD >= ''' + CONVERT(varchar(6),@p_ZititaiCDBegin) + '''' +CHAR(13)
        END
        IF @p_ZititaiCDEnd IS NOT NULL
        BEGIN
            SET @strSQL = @strSQL + ' AND WK.ZititaiCD <= ''' + CONVERT(varchar(6),@p_ZititaiCDEnd) + '''' +CHAR(13)
        END
        IF @p_ShuturyokuKBN = 1
        BEGIN
            SET @strSQL = @strSQL + ' GROUP BY WK.ZigyoushoCD ,WK.ZititaiCD ,WK.SisanShubetuNO' +CHAR(13)
        END
        ELSE
        BEGIN
            SET @strSQL = @strSQL + ' GROUP BY WK.ZititaiCD ,WK.SisanShubetuNO' +CHAR(13)
        END

        EXEC (@strSQL)

        SET @ErrorCode = @@ERROR
        IF @ErrorCode <> 0 GOTO ERR_SHORI

        -- 5.4.「集計ワーク」へ更新(集計ワーク内容を集計して計区分=1として更新)
        INSERT INTO #N0000WK_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Shukei (
                  SisanShubetuNO
                , ZigyoushoCD
                , ZititaiCD
                , ShutokuKagaku1
                , ShutokuKagaku2
                , ShutokuKagaku3
                , ShutokuKagaku4
                , HonnendoHyoukagaku
                , Ketteikagaku
                , KazeiHyoujungaku
                , Kensuu
                , KeiKBN
                )
        SELECT
                  WK.SisanShubetuNO
                , WK.ZigyoushoCD
                , WK.ZititaiCD
                , Sum(ISNULL(WK.ShutokuKagaku4,0)) - Sum(ISNULL(WK.ShutokuKagaku3,0))  + Sum(ISNULL(WK.ShutokuKagaku2,0))
                , Sum(ISNULL(WK.ShutokuKagaku2,0))
                , Sum(ISNULL(WK.ShutokuKagaku3,0))
                , Sum(ISNULL(WK.ShutokuKagaku4,0))
                , Sum(ISNULL(WK.HonnendoHyoukagaku,0))
                , Sum(ISNULL(WK.HonnendoHyoukagaku,0))
                , Sum(ISNULL(WK.KazeiHyoujungaku,0))
                , Sum(ISNULL(WK.Kensuu,0))
                , 1
        FROM    #N0000WK_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Shukei WK
        WHERE WK.KeiKBN = 0
        GROUP BY WK.ZigyoushoCD, WK.ZititaiCD, WK.SisanShubetuNO

        SET @ErrorCode = @@ERROR
        IF @ErrorCode <> 0 GOTO ERR_SHORI

        -- 5.5.「集計ワーク」の計区分=0データの削除(集計前データの削除)
        DELETE FROM #N0000WK_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Shukei 
        WHERE KeiKBN = 0

        SET @ErrorCode = @@ERROR
        IF @ErrorCode <> 0 GOTO ERR_SHORI

        -- 5.6.「集計ワーク」へ空データ作成(事業所・自治体ごとにデータの存在しない資産種別のデータを作成する)
        INSERT INTO #N0000WK_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Shukei (
                  SisanShubetuNO
                , ZigyoushoCD
                , ZititaiCD
                , ShutokuKagaku1
                , ShutokuKagaku2
                , ShutokuKagaku3
                , ShutokuKagaku4
                , HonnendoHyoukagaku
                , Ketteikagaku
                , KazeiHyoujungaku
                , Kensuu
                , KeiKBN
                )
        SELECT
                  MAIN.SisanShubetuNO
                , MAIN.ZigyoushoCD
                , MAIN.SisanShubetuNO
                , 0
                , 0
                , 0
                , 0
                , 0
                , 0
                , 0
                , 0
                , 1
        FROM    (SELECT WK.ZigyoushoCD ,WK.ZititaiCD ,CD1.CodeValue AS SisanShubetuNO
                 FROM #N0000WK_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Shukei WK
                 LEFT JOIN M0000MS_CommonCode CD1
                 ON  CD1.Shubetu = 2950
                 AND CD1.CodeValue BETWEEN 1 AND 6
                 GROUP BY WK.ZigyoushoCD ,WK.ZititaiCD ,CD1.CodeValue
                ) MAIN
        LEFT JOIN (SELECT ZigyoushoCD ,ZititaiCD
                   FROM #N0000WK_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Shukei
                   GROUP BY ZigyoushoCD ,ZititaiCD
                  ) SUB
        ON  SUB.ZigyoushoCD  = MAIN.ZigyoushoCD
        AND SUB.ZititaiCD    = MAIN.ZititaiCD
        WHERE SUB.ZigyoushoCD IS NULL

        SET @ErrorCode = @@ERROR
        IF @ErrorCode <> 0 GOTO ERR_SHORI

        -- 5.7.「集計ワーク」へ合計データを作成する 5.8.課税標準額の合計金額を1000円未満切捨
        INSERT INTO #N0000WK_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Shukei (
                  SisanShubetuNO
                , ZigyoushoCD
                , ZititaiCD
                , ShutokuKagaku1
                , ShutokuKagaku2
                , ShutokuKagaku3
                , ShutokuKagaku4
                , HonnendoHyoukagaku
                , Ketteikagaku
                , KazeiHyoujungaku
                , Kensuu
                , KeiKBN
                )
        SELECT
                  7
                , ZigyoushoCD
                , ZititaiCD
                , SUM(ISNULL(ShutokuKagaku1,0))
                , SUM(ISNULL(ShutokuKagaku2,0))
                , SUM(ISNULL(ShutokuKagaku3,0))
                , SUM(ISNULL(ShutokuKagaku4,0))
                , SUM(ISNULL(HonnendoHyoukagaku,0))
                , SUM(ISNULL(HonnendoHyoukagaku,0))
                , FLOOR(SUM(ISNULL(KazeiHyoujungaku,0)) / 1000.000) * 1000.000
                , SUM(ISNULL(Kensuu,0))
                , 1
        FROM #N0000WK_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Shukei
        GROUP BY ZigyoushoCD ,ZititaiCD

        SET @ErrorCode = @@ERROR
        IF @ErrorCode <> 0 GOTO ERR_SHORI

        -- 6.集計ワークを横展開
       INSERT INTO #N0000WK_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Print (
                  ZigyoushoCD
                , ZititaiCD
                , ShutokuKagaku1_1
                , ShutokuKagaku1_2
                , ShutokuKagaku1_3
                , ShutokuKagaku1_4
                , ShutokuKagaku1_5
                , ShutokuKagaku1_6
                , ShutokuKagaku1_7
                , ShutokuKagaku2_1
                , ShutokuKagaku2_2
                , ShutokuKagaku2_3
                , ShutokuKagaku2_4
                , ShutokuKagaku2_5
                , ShutokuKagaku2_6
                , ShutokuKagaku2_7
        , ShutokuKagaku3_1
                , ShutokuKagaku3_2
                , ShutokuKagaku3_3
                , ShutokuKagaku3_4
                , ShutokuKagaku3_5
                , ShutokuKagaku3_6
                , ShutokuKagaku3_7
                , ShutokuKagaku4_1
                , ShutokuKagaku4_2
                , ShutokuKagaku4_3
                , ShutokuKagaku4_4
                , ShutokuKagaku4_5
                , ShutokuKagaku4_6
                , ShutokuKagaku4_7
                , HonnendoHyoukagaku_1
                , HonnendoHyoukagaku_2
                , HonnendoHyoukagaku_3
                , HonnendoHyoukagaku_4
                , HonnendoHyoukagaku_5
                , HonnendoHyoukagaku_6
                , HonnendoHyoukagaku_7
                , Ketteikagaku_1
                , Ketteikagaku_2
                , Ketteikagaku_3
                , Ketteikagaku_4
                , Ketteikagaku_5
                , Ketteikagaku_6
                , Ketteikagaku_7
                , KazeiHyoujungaku_1
                , KazeiHyoujungaku_2
                , KazeiHyoujungaku_3
                , KazeiHyoujungaku_4
                , KazeiHyoujungaku_5
                , KazeiHyoujungaku_6
                , KazeiHyoujungaku_7
                , Kensuu_1
                , Kensuu_2
                , Kensuu_3
                , Kensuu_4
                , Kensuu_5
                , Kensuu_6
                , Kensuu_7
                )
        SELECT
                  ZigyoushoCD
                , ZititaiCD
                , SUM(CASE WHEN SisanShubetuNO = 1
                           THEN ISNULL(ShutokuKagaku1,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 2
                           THEN ISNULL(ShutokuKagaku1,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 3
                           THEN ISNULL(ShutokuKagaku1,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 4
                           THEN ISNULL(ShutokuKagaku1,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 5
                           THEN ISNULL(ShutokuKagaku1,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 6
                           THEN ISNULL(ShutokuKagaku1,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 7
                           THEN ISNULL(ShutokuKagaku1,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 1
                           THEN ISNULL(ShutokuKagaku2,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 2
                           THEN ISNULL(ShutokuKagaku2,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 3
                           THEN ISNULL(ShutokuKagaku2,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 4
                           THEN ISNULL(ShutokuKagaku2,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 5
                           THEN ISNULL(ShutokuKagaku2,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 6
                           THEN ISNULL(ShutokuKagaku2,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 7
                           THEN ISNULL(ShutokuKagaku2,0)
                           ELSE 0
                      END)
         , SUM(CASE WHEN SisanShubetuNO = 1
                           THEN ISNULL(ShutokuKagaku3,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 2
                           THEN ISNULL(ShutokuKagaku3,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 3
                           THEN ISNULL(ShutokuKagaku3,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 4
                           THEN ISNULL(ShutokuKagaku3,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 5
                           THEN ISNULL(ShutokuKagaku3,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 6
                           THEN ISNULL(ShutokuKagaku3,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 7
                           THEN ISNULL(ShutokuKagaku3,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 1
                           THEN ISNULL(ShutokuKagaku4,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 2
                           THEN ISNULL(ShutokuKagaku4,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 3
                           THEN ISNULL(ShutokuKagaku4,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 4
                           THEN ISNULL(ShutokuKagaku4,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 5
                           THEN ISNULL(ShutokuKagaku4,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 6
                           THEN ISNULL(ShutokuKagaku4,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 7
                           THEN ISNULL(ShutokuKagaku4,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 1
                           THEN ISNULL(HonnendoHyoukagaku,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 2
                           THEN ISNULL(HonnendoHyoukagaku,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 3
                           THEN ISNULL(HonnendoHyoukagaku,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 4
                           THEN ISNULL(HonnendoHyoukagaku,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 5
                           THEN ISNULL(HonnendoHyoukagaku,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 6
                           THEN ISNULL(HonnendoHyoukagaku,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 7
                           THEN ISNULL(HonnendoHyoukagaku,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 1
                           THEN ISNULL(Ketteikagaku,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 2
                           THEN ISNULL(Ketteikagaku,0)
                           ELSE 0
                      END)
            , SUM(CASE WHEN SisanShubetuNO = 3
                           THEN ISNULL(Ketteikagaku,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 4
                           THEN ISNULL(Ketteikagaku,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 5
                           THEN ISNULL(Ketteikagaku,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 6
                           THEN ISNULL(Ketteikagaku,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 7
                           THEN ISNULL(Ketteikagaku,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 1
                           THEN ISNULL(KazeiHyoujungaku,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 2
                           THEN ISNULL(KazeiHyoujungaku,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 3
                           THEN ISNULL(KazeiHyoujungaku,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 4
                           THEN ISNULL(KazeiHyoujungaku,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 5
                           THEN ISNULL(KazeiHyoujungaku,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 6
                           THEN ISNULL(KazeiHyoujungaku,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 7
                           THEN ISNULL(KazeiHyoujungaku,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 1
                           THEN ISNULL(Kensuu,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 2
                           THEN ISNULL(Kensuu,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 3
                           THEN ISNULL(Kensuu,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 4
                           THEN ISNULL(Kensuu,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 5
                           THEN ISNULL(Kensuu,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 6
                           THEN ISNULL(Kensuu,0)
                           ELSE 0
                      END)
                , SUM(CASE WHEN SisanShubetuNO = 7
                           THEN ISNULL(Kensuu,0)
                           ELSE 0
                      END)
        FROM #N0000WK_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Shukei
        GROUP BY ZigyoushoCD ,ZititaiCD

        SET @ErrorCode = @@ERROR
        IF @ErrorCode <> 0 GOTO ERR_SHORI

        -- 6.各種印字制御処理
        -- 6.1.画面.申告種類＝増減申告の場合
        IF @p_Sinkokushurui = 1
        BEGIN
            UPDATE WK
            SET    WK.Ketteikagaku_1     = NULL
                  ,WK.Ketteikagaku_2     = NULL
                  ,WK.Ketteikagaku_3     = NULL
                  ,WK.Ketteikagaku_4     = NULL
                  ,WK.Ketteikagaku_5     = NULL
                  ,WK.Ketteikagaku_6     = NULL
                  ,WK.Ketteikagaku_7     = NULL
                  ,WK.KazeiHyoujungaku_1 = NULL
                  ,WK.KazeiHyoujungaku_2 = NULL
                  ,WK.KazeiHyoujungaku_3 = NULL
                  ,WK.KazeiHyoujungaku_4 = NULL
                  ,WK.KazeiHyoujungaku_5 = NULL
                  ,WK.KazeiHyoujungaku_6 = NULL
                  ,WK.KazeiHyoujungaku_7 = NULL
                  ,WK.Kensuu_1           = NULL
                  ,WK.Kensuu_2           = NULL
                  ,WK.Kensuu_3           = NULL
                  ,WK.Kensuu_4           = NULL
                  ,WK.Kensuu_5           = NULL
                  ,WK.Kensuu_6           = NULL
                  ,WK.Kensuu_7           = NULL
            FROM #N0000WK_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Print WK

            SET @ErrorCode = @@ERROR
            IF @ErrorCode <> 0 GOTO ERR_SHORI

            -- 6.1.1.画面.評価額を印字＝OFFの場合
            IF @p_HyoukagakuPrintKBN = 0
            BEGIN
                UPDATE WK
                SET    WK.HonnendoHyoukagaku_1 = NULL
                      ,WK.HonnendoHyoukagaku_2 = NULL
                      ,WK.HonnendoHyoukagaku_3 = NULL
                      ,WK.HonnendoHyoukagaku_4 = NULL
                      ,WK.HonnendoHyoukagaku_5 = NULL
                      ,WK.HonnendoHyoukagaku_6 = NULL
                      ,WK.HonnendoHyoukagaku_7 = NULL
                FROM #N0000WK_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Print WK

                SET @ErrorCode = @@ERROR
                IF @ErrorCode <> 0 GOTO ERR_SHORI
            END

            -- 6.1.2.画面.前年中に減少、取得したもののみを印字＝ONの場合
            IF @p_ZennenPrintKBN = 1
            BEGIN
                UPDATE WK
                SET    WK.ShutokuKagaku1_1 = NULL
                      ,WK.ShutokuKagaku1_2 = NULL
                      ,WK.ShutokuKagaku1_3 = NULL
                      ,WK.ShutokuKagaku1_4 = NULL
                      ,WK.ShutokuKagaku1_5 = NULL
                      ,WK.ShutokuKagaku1_6 = NULL
                      ,WK.ShutokuKagaku1_7 = NULL
                      ,WK.ShutokuKagaku4_1 = NULL
                      ,WK.ShutokuKagaku4_2 = NULL
                      ,WK.ShutokuKagaku4_3 = NULL
                      ,WK.ShutokuKagaku4_4 = NULL
                      ,WK.ShutokuKagaku4_5 = NULL
                      ,WK.ShutokuKagaku4_6 = NULL
                      ,WK.ShutokuKagaku4_7 = NULL
                FROM #N0000WK_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Print WK

                SET @ErrorCode = @@ERROR
                IF @ErrorCode <> 0 GOTO ERR_SHORI
            END
        END

        -- 7.結果出力
--2012/11/05 E.Sato ST (N00970)eLTax対応    
		--印刷の場合
        IF (@p_ShuturyokuSiteiKBN = 0)
        BEGIN
--2012/11/05 E.Sato ED    
        -- 7.1.申告書データ出力
        SELECT         
       --変更 2009/05/25 S.Mino フル桁印字の表示改修 開始
       --      ZIT.ZititaiName
             dbo.A0000FC_InsertReturn(ZIT.ZititaiName, 30) AS ZititaiName 
       --変更 2009/05/25 S.Mino フル桁印字の表示改修 終了
            ,ZIT.ShoyuushaCD                
            ,CASE WHEN @p_ShuturyokuKBN = 1
                  THEN CONVERT(varchar(3),CONVERT(varchar,SZG.YuubinNO1) + '   ') + '-' + CONVERT(varchar(4),CONVERT(varchar,SZG.YuubinNO2) + '    ')
                  ELSE CONVERT(varchar(3),CONVERT(varchar,KAC.YuubinNO1) + '   ') + '-' + CONVERT(varchar(4),CONVERT(varchar,KAC.YuubinNO2) + '    ')
             END AS Yubin
            ,CASE WHEN @p_ShuturyokuKBN = 1
                  THEN SZG.Juusho1Kana
                  ELSE KAC.Juusho1KanaName
             END AS Juusho1Kana
            ,CASE WHEN @p_ShuturyokuKBN = 1
                  THEN SZG.Juusho2Kana
                  ELSE KAC.Juusho2KanaName
             END AS Juusho2Kana
            ,CASE WHEN @p_ShuturyokuKBN = 1
                  THEN SZG.Juusho1
                  ELSE KAC.Juusho1
             END AS Juusho1
            ,CASE WHEN @p_ShuturyokuKBN = 1
                  THEN SZG.Juusho2
                  ELSE KAC.Juusho2
             END AS Juusho2
     ,CASE WHEN @p_ShuturyokuKBN = 1
                  THEN SZG.TELNO1
                  ELSE KAC.TelNO1
             END AS TELNO1
            ,CASE WHEN @p_ShuturyokuKBN = 1
                  THEN SZG.TELNO2
                  ELSE KAC.TelNO2
             END AS TELNO2
            ,CASE WHEN @p_ShuturyokuKBN = 1
                  THEN SZG.TELNO3
                  ELSE KAC.TelNO3
             END AS TELNO3
            ,CASE WHEN @p_ShuturyokuKBN = 1
                  THEN SZG.CompanyKanaName
                  ELSE KTC.CompanyKanaName
             END AS CompanyKanaName
            ,CASE WHEN @p_ShuturyokuKBN = 1
                  THEN SZG.CompanyName
                  ELSE @p_CompanyName
             END AS CompanyName
            ,CASE WHEN @p_ShuturyokuKBN = 1
                  THEN SZG.DaihyoushaKanaName
                  ELSE KAC.DaihyoushaKanaName
             END AS DaihyoushaKanaName
            ,CASE WHEN @p_ShuturyokuKBN = 1
                  THEN SZG.DaihyoushaName
                  ELSE KAC.Daihyousha
             END AS Daihyousha
            ,CASE WHEN @p_ShuturyokuKBN = 1
                  THEN SZG.Yagou
                  ELSE KTC.Yagou
             END AS Yagou
            ,CASE WHEN @p_ShuturyokuKBN = 1
                  THEN SZG.ZigyouShumoku
                  ELSE KTC.ZigyouShumoku
             END AS ZigyouShumoku
            ,CASE WHEN @p_ShuturyokuKBN = 1
                  THEN FLOOR(SZG.Sihonkin/1000000)
                  ELSE FLOOR(KTC.Sihonkin/1000000)
             END AS Sihonkin
            ,CASE WHEN @p_ShuturyokuKBN = 1
                  THEN SZG.SeturituDate
                  ELSE KTC.SeturituDate
             END AS SeturituDate
            ,CASE WHEN @p_ShuturyokuKBN = 1
                  THEN SZG.SinkokuTantoushaName
                  ELSE KTC.SinkokuTantoushaName
             END AS SinkokuTantoushaName
            ,CASE WHEN @p_ShuturyokuKBN = 1
                  THEN SZG.TantoushaTELNO1
                  ELSE KTC.TantoushaTELNO1
             END AS TantoushaTELNO1
            ,CASE WHEN @p_ShuturyokuKBN = 1
                  THEN SZG.TantoushaTELNO2
                  ELSE KTC.TantoushaTELNO2
             END AS TantoushaTELNO2
            ,CASE WHEN @p_ShuturyokuKBN = 1
                  THEN SZG.TantoushaTELNO3
                  ELSE KTC.TantoushaTELNO3
             END AS TantoushaTELNO3
            ,CASE WHEN @p_ShuturyokuKBN = 1
                  THEN SZG.KaikeiZimushoName
                  ELSE KTC.KaikeiZimushoName
             END AS KaikeiZimushoName
            ,CASE WHEN @p_ShuturyokuKBN = 1
                  THEN SZG.KaikeiZimushoTELNO1
                  ELSE KTC.KaikeiZimushoTELNO1
             END AS KaikeiZimushoTELNO1
            ,CASE WHEN @p_ShuturyokuKBN = 1
                  THEN SZG.KaikeiZimushoTELNO2
                  ELSE KTC.KaikeiZimushoTELNO2
             END AS KaikeiZimushoTELNO2
            ,CASE WHEN @p_ShuturyokuKBN = 1
                  THEN SZG.KaikeiZimushoTELNO3
                  ELSE KTC.KaikeiZimushoTELNO3
             END AS KaikeiZimushoTELNO3
            --2015/11/10  追加  T.Sato      (32423-32424-32426)償却資産申告書法人番号追加対応　開始
            ,CASE WHEN KAC.KaishaUnyouKeitaiKBN = 1 THEN CASE WHEN ZIG.HouzinNO IS NULL THEN NULL ELSE dbo.N0000FC_SplitCharacter(ZIG.HouzinNO,1,1,0) END
                                                    ELSE CASE WHEN KAC.HouzinNO IS NULL THEN NULL ELSE dbo.N0000FC_SplitCharacter(KAC.HouzinNO,1,1,0) END
                                                    END AS HouzinNO_1
            ,CASE WHEN KAC.KaishaUnyouKeitaiKBN = 1 THEN CASE WHEN ZIG.HouzinNO IS NULL THEN NULL ELSE dbo.N0000FC_SplitCharacter(ZIG.HouzinNO,1,2,0) END
                                                    ELSE CASE WHEN KAC.HouzinNO IS NULL THEN NULL ELSE dbo.N0000FC_SplitCharacter(KAC.HouzinNO,1,2,0) END
                                                    END AS HouzinNO_2
   ,CASE WHEN KAC.KaishaUnyouKeitaiKBN = 1 THEN CASE WHEN ZIG.HouzinNO IS NULL THEN NULL ELSE dbo.N0000FC_SplitCharacter(ZIG.HouzinNO,1,3,0) END
                                                    ELSE CASE WHEN KAC.HouzinNO IS NULL THEN NULL ELSE dbo.N0000FC_SplitCharacter(KAC.HouzinNO,1,3,0) END
                                                    END AS HouzinNO_3
            ,CASE WHEN KAC.KaishaUnyouKeitaiKBN = 1 THEN CASE WHEN ZIG.HouzinNO IS NULL THEN NULL ELSE dbo.N0000FC_SplitCharacter(ZIG.HouzinNO,1,4,0) END
                                                    ELSE CASE WHEN KAC.HouzinNO IS NULL THEN NULL ELSE dbo.N0000FC_SplitCharacter(KAC.HouzinNO,1,4,0) END
                                                    END AS HouzinNO_4
            ,CASE WHEN KAC.KaishaUnyouKeitaiKBN = 1 THEN CASE WHEN ZIG.HouzinNO IS NULL THEN NULL ELSE dbo.N0000FC_SplitCharacter(ZIG.HouzinNO,1,5,0) END
                                                    ELSE CASE WHEN KAC.HouzinNO IS NULL THEN NULL ELSE dbo.N0000FC_SplitCharacter(KAC.HouzinNO,1,5,0) END
                                                    END AS HouzinNO_5
            ,CASE WHEN KAC.KaishaUnyouKeitaiKBN = 1 THEN CASE WHEN ZIG.HouzinNO IS NULL THEN NULL ELSE dbo.N0000FC_SplitCharacter(ZIG.HouzinNO,1,6,0) END
                                                    ELSE CASE WHEN KAC.HouzinNO IS NULL THEN NULL ELSE dbo.N0000FC_SplitCharacter(KAC.HouzinNO,1,6,0) END
                                                    END AS HouzinNO_6
            ,CASE WHEN KAC.KaishaUnyouKeitaiKBN = 1 THEN CASE WHEN ZIG.HouzinNO IS NULL THEN NULL ELSE dbo.N0000FC_SplitCharacter(ZIG.HouzinNO,1,7,0) END
                                                    ELSE CASE WHEN KAC.HouzinNO IS NULL THEN NULL ELSE dbo.N0000FC_SplitCharacter(KAC.HouzinNO,1,7,0) END
                                                    END AS HouzinNO_7
            ,CASE WHEN KAC.KaishaUnyouKeitaiKBN = 1 THEN CASE WHEN ZIG.HouzinNO IS NULL THEN NULL ELSE dbo.N0000FC_SplitCharacter(ZIG.HouzinNO,1,8,0) END
                                                    ELSE CASE WHEN KAC.HouzinNO IS NULL THEN NULL ELSE dbo.N0000FC_SplitCharacter(KAC.HouzinNO,1,8,0) END
                                                    END AS HouzinNO_8
            ,CASE WHEN KAC.KaishaUnyouKeitaiKBN = 1 THEN CASE WHEN ZIG.HouzinNO IS NULL THEN NULL ELSE dbo.N0000FC_SplitCharacter(ZIG.HouzinNO,1,9,0) END
                                                    ELSE CASE WHEN KAC.HouzinNO IS NULL THEN NULL ELSE dbo.N0000FC_SplitCharacter(KAC.HouzinNO,1,9,0) END
                                                    END AS HouzinNO_9
            ,CASE WHEN KAC.KaishaUnyouKeitaiKBN = 1 THEN CASE WHEN ZIG.HouzinNO IS NULL THEN NULL ELSE dbo.N0000FC_SplitCharacter(ZIG.HouzinNO,1,10,0) END
                                                    ELSE CASE WHEN KAC.HouzinNO IS NULL THEN NULL ELSE dbo.N0000FC_SplitCharacter(KAC.HouzinNO,1,10,0) END
                                                    END AS HouzinNO_10
            ,CASE WHEN KAC.KaishaUnyouKeitaiKBN = 1 THEN CASE WHEN ZIG.HouzinNO IS NULL THEN NULL ELSE dbo.N0000FC_SplitCharacter(ZIG.HouzinNO,1,11,0) END
                                                    ELSE CASE WHEN KAC.HouzinNO IS NULL THEN NULL ELSE dbo.N0000FC_SplitCharacter(KAC.HouzinNO,1,11,0) END
                                                    END AS HouzinNO_11
            ,CASE WHEN KAC.KaishaUnyouKeitaiKBN = 1 THEN CASE WHEN ZIG.HouzinNO IS NULL THEN NULL ELSE dbo.N0000FC_SplitCharacter(ZIG.HouzinNO,1,12,0) END
                                                    ELSE CASE WHEN KAC.HouzinNO IS NULL THEN NULL ELSE dbo.N0000FC_SplitCharacter(KAC.HouzinNO,1,12,0) END
                                                    END AS HouzinNO_12
            ,CASE WHEN KAC.KaishaUnyouKeitaiKBN = 1 THEN CASE WHEN ZIG.HouzinNO IS NULL THEN NULL ELSE dbo.N0000FC_SplitCharacter(ZIG.HouzinNO,1,13,0) END
                  ELSE CASE WHEN KAC.HouzinNO IS NULL THEN NULL ELSE dbo.N0000FC_SplitCharacter(KAC.HouzinNO,1,13,0) END
                                                    END AS HouzinNO_13
            --2015/11/10  追加  T.Sato      (32423-32424-32426)償却資産申告書法人番号追加対応　終了
            ,ZIT.TanshukuTaiNenShouninKBN
            ,ZIT.ZoukaShoukyakuTodokeKBN
            ,ZIT.HikazeiGaitouSisanKBN
            ,ZIT.KazeiHyoujunTokureiKBN
            ,ZIT.TokuShoukyakuAsshukuKichouKBN
            ,ZIT.ZeiKaiShoukyakuTeirituFlag
            ,ZIT.ZeiKaiShoukyakuTeigakuFlag
            ,ZIT.AoiroSinkokuKBN
            ,ZIT.ZigyoushoShozaitiKBN
            ,ZIT.ZigyoushoShozaitiName1
            ,ZIT.ZigyoushoShozaitiName2
            ,ZIT.ZigyoushoShozaitiName3
            ,ZIT.ShakuyouSisanKBN
            ,ZIT.KasinusiName1
            ,ZIT.KasinusiName2
            ,ZIT.ShoyuuKBN1
            ,ZIT.ShoyuuKBN2
            ,ZIT.ShoyuuKBN3
            ,ZIT.Bikou1
            ,ZIT.Bikou2
            ,ZIT.Bikou3
            ,ZIT.Bikou4
            ,ZIT.Bikou5
            ,ZIT.Bikou6
            ,ZIT.Bikou7
            ,CASE WHEN ISNULL(WK.ShutokuKagaku1_1,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku1_1,3,5,1) END AS I_1_1
            ,CASE WHEN ISNULL(WK.ShutokuKagaku1_1,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku1_1,3,4,1) END AS I_1_2
            ,CASE WHEN ISNULL(WK.ShutokuKagaku1_1,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku1_1,3,3,1) END AS I_1_3
            ,CASE WHEN ISNULL(WK.ShutokuKagaku1_1,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku1_1,3,2,1) END AS I_1_4
            ,CASE WHEN ISNULL(WK.ShutokuKagaku1_1,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku1_1,3,1,1) END AS I_1_5
            ,CASE WHEN ISNULL(WK.ShutokuKagaku1_2,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku1_2,3,5,1) END AS I_2_1
            ,CASE WHEN ISNULL(WK.ShutokuKagaku1_2,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku1_2,3,4,1) END AS I_2_2
            ,CASE WHEN ISNULL(WK.ShutokuKagaku1_2,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku1_2,3,3,1) END AS I_2_3
            ,CASE WHEN ISNULL(WK.ShutokuKagaku1_2,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku1_2,3,2,1) END AS I_2_4
            ,CASE WHEN ISNULL(WK.ShutokuKagaku1_2,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku1_2,3,1,1) END AS I_2_5
            ,CASE WHEN ISNULL(WK.ShutokuKagaku1_3,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku1_3,3,5,1) END AS I_3_1
            ,CASE WHEN ISNULL(WK.ShutokuKagaku1_3,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku1_3,3,4,1) END AS I_3_2
            ,CASE WHEN ISNULL(WK.ShutokuKagaku1_3,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku1_3,3,3,1) END AS I_3_3
            ,CASE WHEN ISNULL(WK.ShutokuKagaku1_3,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku1_3,3,2,1) END AS I_3_4
            ,CASE WHEN ISNULL(WK.ShutokuKagaku1_3,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku1_3,3,1,1) END AS I_3_5
            ,CASE WHEN ISNULL(WK.ShutokuKagaku1_4,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku1_4,3,5,1) END AS I_4_1
            ,CASE WHEN ISNULL(WK.ShutokuKagaku1_4,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku1_4,3,4,1) END AS I_4_2
            ,CASE WHEN ISNULL(WK.ShutokuKagaku1_4,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku1_4,3,3,1) END AS I_4_3
            ,CASE WHEN ISNULL(WK.ShutokuKagaku1_4,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku1_4,3,2,1) END AS I_4_4
            ,CASE WHEN ISNULL(WK.ShutokuKagaku1_4,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku1_4,3,1,1) END AS I_4_5
            ,CASE WHEN ISNULL(WK.ShutokuKagaku1_5,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku1_5,3,5,1) END AS I_5_1
            ,CASE WHEN ISNULL(WK.ShutokuKagaku1_5,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku1_5,3,4,1) END AS I_5_2
            ,CASE WHEN ISNULL(WK.ShutokuKagaku1_5,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku1_5,3,3,1) END AS I_5_3
            ,CASE WHEN ISNULL(WK.ShutokuKagaku1_5,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku1_5,3,2,1) END AS I_5_4
            ,CASE WHEN ISNULL(WK.ShutokuKagaku1_5,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku1_5,3,1,1) END AS I_5_5
            ,CASE WHEN ISNULL(WK.ShutokuKagaku1_6,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku1_6,3,5,1) END AS I_6_1
            ,CASE WHEN ISNULL(WK.ShutokuKagaku1_6,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku1_6,3,4,1) END AS I_6_2
            ,CASE WHEN ISNULL(WK.ShutokuKagaku1_6,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku1_6,3,3,1) END AS I_6_3
            ,CASE WHEN ISNULL(WK.ShutokuKagaku1_6,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku1_6,3,2,1) END AS I_6_4
            ,CASE WHEN ISNULL(WK.ShutokuKagaku1_6,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku1_6,3,1,1) END AS I_6_5
            ,CASE WHEN ISNULL(WK.ShutokuKagaku1_7,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku1_7,3,5,1) END AS I_7_1
            ,CASE WHEN ISNULL(WK.ShutokuKagaku1_7,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku1_7,3,4,1) END AS I_7_2
            ,CASE WHEN ISNULL(WK.ShutokuKagaku1_7,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku1_7,3,3,1) END AS I_7_3
            ,CASE WHEN ISNULL(WK.ShutokuKagaku1_7,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku1_7,3,2,1) END AS I_7_4
            ,CASE WHEN ISNULL(WK.ShutokuKagaku1_7,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku1_7,3,1,1) END AS I_7_5

            ,CASE WHEN ISNULL(WK.ShutokuKagaku2_1,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku2_1,3,5,1) END AS Ro_1_1
            ,CASE WHEN ISNULL(WK.ShutokuKagaku2_1,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku2_1,3,4,1) END AS Ro_1_2
            ,CASE WHEN ISNULL(WK.ShutokuKagaku2_1,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku2_1,3,3,1) END AS Ro_1_3
            ,CASE WHEN ISNULL(WK.ShutokuKagaku2_1,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku2_1,3,2,1) END AS Ro_1_4
            ,CASE WHEN ISNULL(WK.ShutokuKagaku2_1,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku2_1,3,1,1) END AS Ro_1_5
            ,CASE WHEN ISNULL(WK.ShutokuKagaku2_2,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku2_2,3,5,1) END AS Ro_2_1
            ,CASE WHEN ISNULL(WK.ShutokuKagaku2_2,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku2_2,3,4,1) END AS Ro_2_2
            ,CASE WHEN ISNULL(WK.ShutokuKagaku2_2,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku2_2,3,3,1) END AS Ro_2_3
            ,CASE WHEN ISNULL(WK.ShutokuKagaku2_2,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku2_2,3,2,1) END AS Ro_2_4
            ,CASE WHEN ISNULL(WK.ShutokuKagaku2_2,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku2_2,3,1,1) END AS Ro_2_5
            ,CASE WHEN ISNULL(WK.ShutokuKagaku2_3,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku2_3,3,5,1) END AS Ro_3_1
            ,CASE WHEN ISNULL(WK.ShutokuKagaku2_3,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku2_3,3,4,1) END AS Ro_3_2
            ,CASE WHEN ISNULL(WK.ShutokuKagaku2_3,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku2_3,3,3,1) END AS Ro_3_3
            ,CASE WHEN ISNULL(WK.ShutokuKagaku2_3,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku2_3,3,2,1) END AS Ro_3_4
            ,CASE WHEN ISNULL(WK.ShutokuKagaku2_3,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku2_3,3,1,1) END AS Ro_3_5
            ,CASE WHEN ISNULL(WK.ShutokuKagaku2_4,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku2_4,3,5,1) END AS Ro_4_1
            ,CASE WHEN ISNULL(WK.ShutokuKagaku2_4,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku2_4,3,4,1) END AS Ro_4_2
            ,CASE WHEN ISNULL(WK.ShutokuKagaku2_4,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku2_4,3,3,1) END AS Ro_4_3
            ,CASE WHEN ISNULL(WK.ShutokuKagaku2_4,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku2_4,3,2,1) END AS Ro_4_4
            ,CASE WHEN ISNULL(WK.ShutokuKagaku2_4,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku2_4,3,1,1) END AS Ro_4_5
            ,CASE WHEN ISNULL(WK.ShutokuKagaku2_5,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku2_5,3,5,1) END AS Ro_5_1
            ,CASE WHEN ISNULL(WK.ShutokuKagaku2_5,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku2_5,3,4,1) END AS Ro_5_2
            ,CASE WHEN ISNULL(WK.ShutokuKagaku2_5,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku2_5,3,3,1) END AS Ro_5_3
            ,CASE WHEN ISNULL(WK.ShutokuKagaku2_5,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku2_5,3,2,1) END AS Ro_5_4
            ,CASE WHEN ISNULL(WK.ShutokuKagaku2_5,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku2_5,3,1,1) END AS Ro_5_5
            ,CASE WHEN ISNULL(WK.ShutokuKagaku2_6,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku2_6,3,5,1) END AS Ro_6_1
            ,CASE WHEN ISNULL(WK.ShutokuKagaku2_6,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku2_6,3,4,1) END AS Ro_6_2
            ,CASE WHEN ISNULL(WK.ShutokuKagaku2_6,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku2_6,3,3,1) END AS Ro_6_3
            ,CASE WHEN ISNULL(WK.ShutokuKagaku2_6,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku2_6,3,2,1) END AS Ro_6_4
            ,CASE WHEN ISNULL(WK.ShutokuKagaku2_6,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku2_6,3,1,1) END AS Ro_6_5
            ,CASE WHEN ISNULL(WK.ShutokuKagaku2_7,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku2_7,3,5,1) END AS Ro_7_1
            ,CASE WHEN ISNULL(WK.ShutokuKagaku2_7,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku2_7,3,4,1) END AS Ro_7_2
            ,CASE WHEN ISNULL(WK.ShutokuKagaku2_7,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku2_7,3,3,1) END AS Ro_7_3
            ,CASE WHEN ISNULL(WK.ShutokuKagaku2_7,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku2_7,3,2,1) END AS Ro_7_4
            ,CASE WHEN ISNULL(WK.ShutokuKagaku2_7,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku2_7,3,1,1) END AS Ro_7_5

            ,CASE WHEN ISNULL(WK.ShutokuKagaku3_1,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku3_1,3,5,1) END AS Ha_1_1
            ,CASE WHEN ISNULL(WK.ShutokuKagaku3_1,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku3_1,3,4,1) END AS Ha_1_2
            ,CASE WHEN ISNULL(WK.ShutokuKagaku3_1,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku3_1,3,3,1) END AS Ha_1_3
            ,CASE WHEN ISNULL(WK.ShutokuKagaku3_1,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku3_1,3,2,1) END AS Ha_1_4
            ,CASE WHEN ISNULL(WK.ShutokuKagaku3_1,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku3_1,3,1,1) END AS Ha_1_5
            ,CASE WHEN ISNULL(WK.ShutokuKagaku3_2,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku3_2,3,5,1) END AS Ha_2_1
            ,CASE WHEN ISNULL(WK.ShutokuKagaku3_2,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku3_2,3,4,1) END AS Ha_2_2
            ,CASE WHEN ISNULL(WK.ShutokuKagaku3_2,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku3_2,3,3,1) END AS Ha_2_3
            ,CASE WHEN ISNULL(WK.ShutokuKagaku3_2,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku3_2,3,2,1) END AS Ha_2_4
            ,CASE WHEN ISNULL(WK.ShutokuKagaku3_2,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku3_2,3,1,1) END AS Ha_2_5
            ,CASE WHEN ISNULL(WK.ShutokuKagaku3_3,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku3_3,3,5,1) END AS Ha_3_1
            ,CASE WHEN ISNULL(WK.ShutokuKagaku3_3,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku3_3,3,4,1) END AS Ha_3_2
            ,CASE WHEN ISNULL(WK.ShutokuKagaku3_3,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku3_3,3,3,1) END AS Ha_3_3
            ,CASE WHEN ISNULL(WK.ShutokuKagaku3_3,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku3_3,3,2,1) END AS Ha_3_4
            ,CASE WHEN ISNULL(WK.ShutokuKagaku3_3,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku3_3,3,1,1) END AS Ha_3_5
            ,CASE WHEN ISNULL(WK.ShutokuKagaku3_4,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku3_4,3,5,1) END AS Ha_4_1
            ,CASE WHEN ISNULL(WK.ShutokuKagaku3_4,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku3_4,3,4,1) END AS Ha_4_2
            ,CASE WHEN ISNULL(WK.ShutokuKagaku3_4,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku3_4,3,3,1) END AS Ha_4_3
            ,CASE WHEN ISNULL(WK.ShutokuKagaku3_4,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku3_4,3,2,1) END AS Ha_4_4
            ,CASE WHEN ISNULL(WK.ShutokuKagaku3_4,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku3_4,3,1,1) END AS Ha_4_5
            ,CASE WHEN ISNULL(WK.ShutokuKagaku3_5,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku3_5,3,5,1) END AS Ha_5_1
            ,CASE WHEN ISNULL(WK.ShutokuKagaku3_5,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku3_5,3,4,1) END AS Ha_5_2
            ,CASE WHEN ISNULL(WK.ShutokuKagaku3_5,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku3_5,3,3,1) END AS Ha_5_3
            ,CASE WHEN ISNULL(WK.ShutokuKagaku3_5,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku3_5,3,2,1) END AS Ha_5_4
            ,CASE WHEN ISNULL(WK.ShutokuKagaku3_5,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku3_5,3,1,1) END AS Ha_5_5
            ,CASE WHEN ISNULL(WK.ShutokuKagaku3_6,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku3_6,3,5,1) END AS Ha_6_1
            ,CASE WHEN ISNULL(WK.ShutokuKagaku3_6,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku3_6,3,4,1) END AS Ha_6_2
            ,CASE WHEN ISNULL(WK.ShutokuKagaku3_6,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku3_6,3,3,1) END AS Ha_6_3
            ,CASE WHEN ISNULL(WK.ShutokuKagaku3_6,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku3_6,3,2,1) END AS Ha_6_4
            ,CASE WHEN ISNULL(WK.ShutokuKagaku3_6,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku3_6,3,1,1) END AS Ha_6_5
            ,CASE WHEN ISNULL(WK.ShutokuKagaku3_7,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku3_7,3,5,1) END AS Ha_7_1
            ,CASE WHEN ISNULL(WK.ShutokuKagaku3_7,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku3_7,3,4,1) END AS Ha_7_2
            ,CASE WHEN ISNULL(WK.ShutokuKagaku3_7,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku3_7,3,3,1) END AS Ha_7_3
            ,CASE WHEN ISNULL(WK.ShutokuKagaku3_7,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku3_7,3,2,1) END AS Ha_7_4
            ,CASE WHEN ISNULL(WK.ShutokuKagaku3_7,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku3_7,3,1,1) END AS Ha_7_5

            ,CASE WHEN ISNULL(WK.ShutokuKagaku4_1,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku4_1,3,5,1) END AS Ni_1_1
            ,CASE WHEN ISNULL(WK.ShutokuKagaku4_1,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku4_1,3,4,1) END AS Ni_1_2
            ,CASE WHEN ISNULL(WK.ShutokuKagaku4_1,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku4_1,3,3,1) END AS Ni_1_3
            ,CASE WHEN ISNULL(WK.ShutokuKagaku4_1,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku4_1,3,2,1) END AS Ni_1_4
            ,CASE WHEN ISNULL(WK.ShutokuKagaku4_1,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku4_1,3,1,1) END AS Ni_1_5
            ,CASE WHEN ISNULL(WK.ShutokuKagaku4_2,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku4_2,3,5,1) END AS Ni_2_1
            ,CASE WHEN ISNULL(WK.ShutokuKagaku4_2,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku4_2,3,4,1) END AS Ni_2_2
            ,CASE WHEN ISNULL(WK.ShutokuKagaku4_2,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku4_2,3,3,1) END AS Ni_2_3
            ,CASE WHEN ISNULL(WK.ShutokuKagaku4_2,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku4_2,3,2,1) END AS Ni_2_4
            ,CASE WHEN ISNULL(WK.ShutokuKagaku4_2,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku4_2,3,1,1) END AS Ni_2_5
            ,CASE WHEN ISNULL(WK.ShutokuKagaku4_3,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku4_3,3,5,1) END AS Ni_3_1
            ,CASE WHEN ISNULL(WK.ShutokuKagaku4_3,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku4_3,3,4,1) END AS Ni_3_2
            ,CASE WHEN ISNULL(WK.ShutokuKagaku4_3,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku4_3,3,3,1) END AS Ni_3_3
            ,CASE WHEN ISNULL(WK.ShutokuKagaku4_3,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku4_3,3,2,1) END AS Ni_3_4
            ,CASE WHEN ISNULL(WK.ShutokuKagaku4_3,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku4_3,3,1,1) END AS Ni_3_5
            ,CASE WHEN ISNULL(WK.ShutokuKagaku4_4,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku4_4,3,5,1) END AS Ni_4_1
            ,CASE WHEN ISNULL(WK.ShutokuKagaku4_4,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku4_4,3,4,1) END AS Ni_4_2
            ,CASE WHEN ISNULL(WK.ShutokuKagaku4_4,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku4_4,3,3,1) END AS Ni_4_3
            ,CASE WHEN ISNULL(WK.ShutokuKagaku4_4,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku4_4,3,2,1) END AS Ni_4_4
            ,CASE WHEN ISNULL(WK.ShutokuKagaku4_4,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku4_4,3,1,1) END AS Ni_4_5
            ,CASE WHEN ISNULL(WK.ShutokuKagaku4_5,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku4_5,3,5,1) END AS Ni_5_1
            ,CASE WHEN ISNULL(WK.ShutokuKagaku4_5,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku4_5,3,4,1) END AS Ni_5_2
            ,CASE WHEN ISNULL(WK.ShutokuKagaku4_5,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku4_5,3,3,1) END AS Ni_5_3
            ,CASE WHEN ISNULL(WK.ShutokuKagaku4_5,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku4_5,3,2,1) END AS Ni_5_4
            ,CASE WHEN ISNULL(WK.ShutokuKagaku4_5,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku4_5,3,1,1) END AS Ni_5_5
            ,CASE WHEN ISNULL(WK.ShutokuKagaku4_6,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku4_6,3,5,1) END AS Ni_6_1
            ,CASE WHEN ISNULL(WK.ShutokuKagaku4_6,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku4_6,3,4,1) END AS Ni_6_2
            ,CASE WHEN ISNULL(WK.ShutokuKagaku4_6,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku4_6,3,3,1) END AS Ni_6_3
            ,CASE WHEN ISNULL(WK.ShutokuKagaku4_6,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku4_6,3,2,1) END AS Ni_6_4
            ,CASE WHEN ISNULL(WK.ShutokuKagaku4_6,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku4_6,3,1,1) END AS Ni_6_5
            ,CASE WHEN ISNULL(WK.ShutokuKagaku4_7,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku4_7,3,5,1) END AS Ni_7_1
            ,CASE WHEN ISNULL(WK.ShutokuKagaku4_7,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku4_7,3,4,1) END AS Ni_7_2
            ,CASE WHEN ISNULL(WK.ShutokuKagaku4_7,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku4_7,3,3,1) END AS Ni_7_3
            ,CASE WHEN ISNULL(WK.ShutokuKagaku4_7,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku4_7,3,2,1) END AS Ni_7_4
            ,CASE WHEN ISNULL(WK.ShutokuKagaku4_7,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.ShutokuKagaku4_7,3,1,1) END AS Ni_7_5

            ,CASE WHEN ISNULL(WK.HonnendoHyoukagaku_1,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.HonnendoHyoukagaku_1,3,5,1) END AS Ho_1_1
            ,CASE WHEN ISNULL(WK.HonnendoHyoukagaku_1,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.HonnendoHyoukagaku_1,3,4,1) END AS Ho_1_2
            ,CASE WHEN ISNULL(WK.HonnendoHyoukagaku_1,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.HonnendoHyoukagaku_1,3,3,1) END AS Ho_1_3
            ,CASE WHEN ISNULL(WK.HonnendoHyoukagaku_1,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.HonnendoHyoukagaku_1,3,2,1) END AS Ho_1_4
            ,CASE WHEN ISNULL(WK.HonnendoHyoukagaku_1,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.HonnendoHyoukagaku_1,3,1,1) END AS Ho_1_5
            ,CASE WHEN ISNULL(WK.HonnendoHyoukagaku_2,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.HonnendoHyoukagaku_2,3,5,1) END AS Ho_2_1
            ,CASE WHEN ISNULL(WK.HonnendoHyoukagaku_2,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.HonnendoHyoukagaku_2,3,4,1) END AS Ho_2_2
            ,CASE WHEN ISNULL(WK.HonnendoHyoukagaku_2,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.HonnendoHyoukagaku_2,3,3,1) END AS Ho_2_3
            ,CASE WHEN ISNULL(WK.HonnendoHyoukagaku_2,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.HonnendoHyoukagaku_2,3,2,1) END AS Ho_2_4
            ,CASE WHEN ISNULL(WK.HonnendoHyoukagaku_2,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.HonnendoHyoukagaku_2,3,1,1) END AS Ho_2_5
            ,CASE WHEN ISNULL(WK.HonnendoHyoukagaku_3,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.HonnendoHyoukagaku_3,3,5,1) END AS Ho_3_1
            ,CASE WHEN ISNULL(WK.HonnendoHyoukagaku_3,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.HonnendoHyoukagaku_3,3,4,1) END AS Ho_3_2
            ,CASE WHEN ISNULL(WK.HonnendoHyoukagaku_3,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.HonnendoHyoukagaku_3,3,3,1) END AS Ho_3_3
            ,CASE WHEN ISNULL(WK.HonnendoHyoukagaku_3,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.HonnendoHyoukagaku_3,3,2,1) END AS Ho_3_4
            ,CASE WHEN ISNULL(WK.HonnendoHyoukagaku_3,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.HonnendoHyoukagaku_3,3,1,1) END AS Ho_3_5
            ,CASE WHEN ISNULL(WK.HonnendoHyoukagaku_4,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.HonnendoHyoukagaku_4,3,5,1) END AS Ho_4_1
            ,CASE WHEN ISNULL(WK.HonnendoHyoukagaku_4,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.HonnendoHyoukagaku_4,3,4,1) END AS Ho_4_2
            ,CASE WHEN ISNULL(WK.HonnendoHyoukagaku_4,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.HonnendoHyoukagaku_4,3,3,1) END AS Ho_4_3
            ,CASE WHEN ISNULL(WK.HonnendoHyoukagaku_4,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.HonnendoHyoukagaku_4,3,2,1) END AS Ho_4_4
            ,CASE WHEN ISNULL(WK.HonnendoHyoukagaku_4,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.HonnendoHyoukagaku_4,3,1,1) END AS Ho_4_5
            ,CASE WHEN ISNULL(WK.HonnendoHyoukagaku_5,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.HonnendoHyoukagaku_5,3,5,1) END AS Ho_5_1
            ,CASE WHEN ISNULL(WK.HonnendoHyoukagaku_5,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.HonnendoHyoukagaku_5,3,4,1) END AS Ho_5_2
            ,CASE WHEN ISNULL(WK.HonnendoHyoukagaku_5,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.HonnendoHyoukagaku_5,3,3,1) END AS Ho_5_3
            ,CASE WHEN ISNULL(WK.HonnendoHyoukagaku_5,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.HonnendoHyoukagaku_5,3,2,1) END AS Ho_5_4
            ,CASE WHEN ISNULL(WK.HonnendoHyoukagaku_5,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.HonnendoHyoukagaku_5,3,1,1) END AS Ho_5_5
            ,CASE WHEN ISNULL(WK.HonnendoHyoukagaku_6,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.HonnendoHyoukagaku_6,3,5,1) END AS Ho_6_1
            ,CASE WHEN ISNULL(WK.HonnendoHyoukagaku_6,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.HonnendoHyoukagaku_6,3,4,1) END AS Ho_6_2
            ,CASE WHEN ISNULL(WK.HonnendoHyoukagaku_6,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.HonnendoHyoukagaku_6,3,3,1) END AS Ho_6_3
            ,CASE WHEN ISNULL(WK.HonnendoHyoukagaku_6,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.HonnendoHyoukagaku_6,3,2,1) END AS Ho_6_4
            ,CASE WHEN ISNULL(WK.HonnendoHyoukagaku_6,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.HonnendoHyoukagaku_6,3,1,1) END AS Ho_6_5
            ,CASE WHEN ISNULL(WK.HonnendoHyoukagaku_7,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.HonnendoHyoukagaku_7,3,5,1) END AS Ho_7_1
            ,CASE WHEN ISNULL(WK.HonnendoHyoukagaku_7,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.HonnendoHyoukagaku_7,3,4,1) END AS Ho_7_2
            ,CASE WHEN ISNULL(WK.HonnendoHyoukagaku_7,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.HonnendoHyoukagaku_7,3,3,1) END AS Ho_7_3
            ,CASE WHEN ISNULL(WK.HonnendoHyoukagaku_7,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.HonnendoHyoukagaku_7,3,2,1) END AS Ho_7_4
            ,CASE WHEN ISNULL(WK.HonnendoHyoukagaku_7,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.HonnendoHyoukagaku_7,3,1,1) END AS Ho_7_5

            ,CASE WHEN ISNULL(WK.Ketteikagaku_1,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Ketteikagaku_1,3,5,1) END AS He_1_1
            ,CASE WHEN ISNULL(WK.Ketteikagaku_1,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Ketteikagaku_1,3,4,1) END AS He_1_2
            ,CASE WHEN ISNULL(WK.Ketteikagaku_1,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Ketteikagaku_1,3,3,1) END AS He_1_3
            ,CASE WHEN ISNULL(WK.Ketteikagaku_1,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Ketteikagaku_1,3,2,1) END AS He_1_4
            ,CASE WHEN ISNULL(WK.Ketteikagaku_1,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Ketteikagaku_1,3,1,1) END AS He_1_5
            ,CASE WHEN ISNULL(WK.Ketteikagaku_2,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Ketteikagaku_2,3,5,1) END AS He_2_1
            ,CASE WHEN ISNULL(WK.Ketteikagaku_2,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Ketteikagaku_2,3,4,1) END AS He_2_2
            ,CASE WHEN ISNULL(WK.Ketteikagaku_2,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Ketteikagaku_2,3,3,1) END AS He_2_3
            ,CASE WHEN ISNULL(WK.Ketteikagaku_2,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Ketteikagaku_2,3,2,1) END AS He_2_4
            ,CASE WHEN ISNULL(WK.Ketteikagaku_2,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Ketteikagaku_2,3,1,1) END AS He_2_5
            ,CASE WHEN ISNULL(WK.Ketteikagaku_3,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Ketteikagaku_3,3,5,1) END AS He_3_1
            ,CASE WHEN ISNULL(WK.Ketteikagaku_3,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Ketteikagaku_3,3,4,1) END AS He_3_2
            ,CASE WHEN ISNULL(WK.Ketteikagaku_3,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Ketteikagaku_3,3,3,1) END AS He_3_3
            ,CASE WHEN ISNULL(WK.Ketteikagaku_3,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Ketteikagaku_3,3,2,1) END AS He_3_4
            ,CASE WHEN ISNULL(WK.Ketteikagaku_3,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Ketteikagaku_3,3,1,1) END AS He_3_5
            ,CASE WHEN ISNULL(WK.Ketteikagaku_4,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Ketteikagaku_4,3,5,1) END AS He_4_1
            ,CASE WHEN ISNULL(WK.Ketteikagaku_4,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Ketteikagaku_4,3,4,1) END AS He_4_2
            ,CASE WHEN ISNULL(WK.Ketteikagaku_4,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Ketteikagaku_4,3,3,1) END AS He_4_3
            ,CASE WHEN ISNULL(WK.Ketteikagaku_4,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Ketteikagaku_4,3,2,1) END AS He_4_4
            ,CASE WHEN ISNULL(WK.Ketteikagaku_4,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Ketteikagaku_4,3,1,1) END AS He_4_5
            ,CASE WHEN ISNULL(WK.Ketteikagaku_5,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Ketteikagaku_5,3,5,1) END AS He_5_1
            ,CASE WHEN ISNULL(WK.Ketteikagaku_5,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Ketteikagaku_5,3,4,1) END AS He_5_2
            ,CASE WHEN ISNULL(WK.Ketteikagaku_5,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Ketteikagaku_5,3,3,1) END AS He_5_3
            ,CASE WHEN ISNULL(WK.Ketteikagaku_5,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Ketteikagaku_5,3,2,1) END AS He_5_4
            ,CASE WHEN ISNULL(WK.Ketteikagaku_5,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Ketteikagaku_5,3,1,1) END AS He_5_5
            ,CASE WHEN ISNULL(WK.Ketteikagaku_6,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Ketteikagaku_6,3,5,1) END AS He_6_1
            ,CASE WHEN ISNULL(WK.Ketteikagaku_6,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Ketteikagaku_6,3,4,1) END AS He_6_2
            ,CASE WHEN ISNULL(WK.Ketteikagaku_6,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Ketteikagaku_6,3,3,1) END AS He_6_3
            ,CASE WHEN ISNULL(WK.Ketteikagaku_6,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Ketteikagaku_6,3,2,1) END AS He_6_4
            ,CASE WHEN ISNULL(WK.Ketteikagaku_6,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Ketteikagaku_6,3,1,1) END AS He_6_5
            ,CASE WHEN ISNULL(WK.Ketteikagaku_7,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Ketteikagaku_7,3,5,1) END AS He_7_1
            ,CASE WHEN ISNULL(WK.Ketteikagaku_7,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Ketteikagaku_7,3,4,1) END AS He_7_2
            ,CASE WHEN ISNULL(WK.Ketteikagaku_7,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Ketteikagaku_7,3,3,1) END AS He_7_3
            ,CASE WHEN ISNULL(WK.Ketteikagaku_7,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Ketteikagaku_7,3,2,1) END AS He_7_4
            ,CASE WHEN ISNULL(WK.Ketteikagaku_7,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.Ketteikagaku_7,3,1,1) END AS He_7_5

            ,CASE WHEN ISNULL(WK.KazeiHyoujungaku_1,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiHyoujungaku_1,3,5,1) END AS To_1_1
            ,CASE WHEN ISNULL(WK.KazeiHyoujungaku_1,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiHyoujungaku_1,3,4,1) END AS To_1_2
            ,CASE WHEN ISNULL(WK.KazeiHyoujungaku_1,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiHyoujungaku_1,3,3,1) END AS To_1_3
            ,CASE WHEN ISNULL(WK.KazeiHyoujungaku_1,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiHyoujungaku_1,3,2,1) END AS To_1_4
            ,CASE WHEN ISNULL(WK.KazeiHyoujungaku_1,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiHyoujungaku_1,3,1,1) END AS To_1_5
            ,CASE WHEN ISNULL(WK.KazeiHyoujungaku_2,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiHyoujungaku_2,3,5,1) END AS To_2_1
            ,CASE WHEN ISNULL(WK.KazeiHyoujungaku_2,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiHyoujungaku_2,3,4,1) END AS To_2_2
            ,CASE WHEN ISNULL(WK.KazeiHyoujungaku_2,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiHyoujungaku_2,3,3,1) END AS To_2_3
            ,CASE WHEN ISNULL(WK.KazeiHyoujungaku_2,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiHyoujungaku_2,3,2,1) END AS To_2_4
            ,CASE WHEN ISNULL(WK.KazeiHyoujungaku_2,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiHyoujungaku_2,3,1,1) END AS To_2_5
            ,CASE WHEN ISNULL(WK.KazeiHyoujungaku_3,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiHyoujungaku_3,3,5,1) END AS To_3_1
            ,CASE WHEN ISNULL(WK.KazeiHyoujungaku_3,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiHyoujungaku_3,3,4,1) END AS To_3_2
            ,CASE WHEN ISNULL(WK.KazeiHyoujungaku_3,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiHyoujungaku_3,3,3,1) END AS To_3_3
            ,CASE WHEN ISNULL(WK.KazeiHyoujungaku_3,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiHyoujungaku_3,3,2,1) END AS To_3_4
            ,CASE WHEN ISNULL(WK.KazeiHyoujungaku_3,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiHyoujungaku_3,3,1,1) END AS To_3_5
            ,CASE WHEN ISNULL(WK.KazeiHyoujungaku_4,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiHyoujungaku_4,3,5,1) END AS To_4_1
            ,CASE WHEN ISNULL(WK.KazeiHyoujungaku_4,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiHyoujungaku_4,3,4,1) END AS To_4_2
            ,CASE WHEN ISNULL(WK.KazeiHyoujungaku_4,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiHyoujungaku_4,3,3,1) END AS To_4_3
            ,CASE WHEN ISNULL(WK.KazeiHyoujungaku_4,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiHyoujungaku_4,3,2,1) END AS To_4_4
            ,CASE WHEN ISNULL(WK.KazeiHyoujungaku_4,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiHyoujungaku_4,3,1,1) END AS To_4_5
            ,CASE WHEN ISNULL(WK.KazeiHyoujungaku_5,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiHyoujungaku_5,3,5,1) END AS To_5_1
            ,CASE WHEN ISNULL(WK.KazeiHyoujungaku_5,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiHyoujungaku_5,3,4,1) END AS To_5_2
            ,CASE WHEN ISNULL(WK.KazeiHyoujungaku_5,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiHyoujungaku_5,3,3,1) END AS To_5_3
            ,CASE WHEN ISNULL(WK.KazeiHyoujungaku_5,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiHyoujungaku_5,3,2,1) END AS To_5_4
            ,CASE WHEN ISNULL(WK.KazeiHyoujungaku_5,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiHyoujungaku_5,3,1,1) END AS To_5_5
            ,CASE WHEN ISNULL(WK.KazeiHyoujungaku_6,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiHyoujungaku_6,3,5,1) END AS To_6_1
            ,CASE WHEN ISNULL(WK.KazeiHyoujungaku_6,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiHyoujungaku_6,3,4,1) END AS To_6_2
            ,CASE WHEN ISNULL(WK.KazeiHyoujungaku_6,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiHyoujungaku_6,3,3,1) END AS To_6_3
            ,CASE WHEN ISNULL(WK.KazeiHyoujungaku_6,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiHyoujungaku_6,3,2,1) END AS To_6_4
            ,CASE WHEN ISNULL(WK.KazeiHyoujungaku_6,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiHyoujungaku_6,3,1,1) END AS To_6_5
            ,CASE WHEN ISNULL(WK.KazeiHyoujungaku_7,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiHyoujungaku_7,3,5,1) END AS To_7_1
            ,CASE WHEN ISNULL(WK.KazeiHyoujungaku_7,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiHyoujungaku_7,3,4,1) END AS To_7_2
            ,CASE WHEN ISNULL(WK.KazeiHyoujungaku_7,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiHyoujungaku_7,3,3,1) END AS To_7_3
            ,CASE WHEN ISNULL(WK.KazeiHyoujungaku_7,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiHyoujungaku_7,3,2,1) END AS To_7_4
            ,CASE WHEN ISNULL(WK.KazeiHyoujungaku_7,0) = 0 THEN NULL ELSE dbo.N0000FC_SplitCharacter(WK.KazeiHyoujungaku_7,3,1,1) END AS To_7_5

            ,CASE WHEN ISNULL(WK.Kensuu_1,0) = 0 THEN NULL ELSE WK.Kensuu_1 END AS Kensuu_1
            ,CASE WHEN ISNULL(WK.Kensuu_2,0) = 0 THEN NULL ELSE WK.Kensuu_2 END AS Kensuu_2
            ,CASE WHEN ISNULL(WK.Kensuu_3,0) = 0 THEN NULL ELSE WK.Kensuu_3 END AS Kensuu_3
            ,CASE WHEN ISNULL(WK.Kensuu_4,0) = 0 THEN NULL ELSE WK.Kensuu_4 END AS Kensuu_4
            ,CASE WHEN ISNULL(WK.Kensuu_5,0) = 0 THEN NULL ELSE WK.Kensuu_5 END AS Kensuu_5
            ,CASE WHEN ISNULL(WK.Kensuu_6,0) = 0 THEN NULL ELSE WK.Kensuu_6 END AS Kensuu_6
            ,CASE WHEN ISNULL(WK.Kensuu_7,0) = 0 THEN NULL ELSE WK.Kensuu_7 END AS Kensuu_7
        FROM #N0000WK_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Print WK
        LEFT JOIN N0000MS_NA_Zititai ZIT
        ON  ZIT.ZititaiCD = WK.ZititaiCD        
        LEFT JOIN N0000MS_NA_KoteiSisanControl KTC
        ON  KTC.NendoID = 0
        LEFT JOIN M0000MS_MA_KaikeiControl KAC
        ON  KAC.ControlKey = 1
        LEFT JOIN N0000MS_NA_SisanZigyousho SZG
        ON  SZG.ZigyoushoCD = WK.ZigyoushoCD
        --2015/11/10  追加  T.Sato      (32423-32424-32426)償却資産申告書法人番号追加対応　開始
        LEFT JOIN ( 
            SELECT 
               ZigyoushoCD 
              ,MAX(KaiteiDate) AS MAX_KaiteiDate 
            FROM  M0000MS_MA_Zigyousho 
            WHERE KaiteiDate      <= @p_KijunDate 
            GROUP BY ZigyoushoCD 
        ) ZIGSUB 
        ON  ZIGSUB.ZigyoushoCD = WK.ZigyoushoCD 
        LEFT JOIN M0000MS_MA_Zigyousho ZIG   
        ON  ZIG.ZigyoushoCD = ZIGSUB.ZigyoushoCD 
        AND ZIG.KaiteiDate  = ZIGSUB.MAX_KaiteiDate
        --2015/11/10  追加  T.Sato      (32423-32424-32426)償却資産申告書法人番号追加対応　終了
--  2019/03/08 M.Hamachika  修正  (123875)出力のたびに自治体の並び順が変わってしまう不具合修正 ↓
        ORDER BY WK.ZititaiCD
--  2019/03/08 M.Hamachika  修正  (123875)出力のたびに自治体の並び順が変わってしまう不具合修正 ↑

        SET @ErrorCode = @@ERROR
        IF  @ErrorCode <> 0
        BEGIN
            GOTO ERR_SHORI
        END
--2012/11/05 E.Sato ST (N00970)eLTax対応
        END
        --ファイル出力の場合
        ELSE 
        BEGIN
            -- 7.1.申告書データ出力
            SELECT         
                -- 2015/01/05 K.Morita 変更 (19288-19289-19291)eLTax仕様変更対応 ↓
                -- '0.0' AS [;中間ファイルバージョン]
                --,KTC.NouzeishaCD AS [納税者コード]
                --,WK.ZititaiCD AS [提出先市区町村コード]
                --,NULL AS [税務事務所コード]
				--2015/12/17 T.Sato  修正  (34037-34032-34034)償却資産申告書法人番号追加対応(eLTax)　開始
                 --'2.0' AS [;中間ファイルバージョン]
                 '3.0' AS [;中間ファイルバージョン]
				--2015/12/17 T.Sato  修正  (34037-34032-34034)償却資産申告書法人番号追加対応(eLTax)　終了
                ,'R0513500' AS [手続きID]
                ,KTC.NouzeishaCD AS [納税者コード]
                --2020/12/24 Y.Matuzawa (182011) 地方公共団体コード地方公共団体コード対応 ↓
                --,WK.ZititaiCD AS [提出先市町村コード]
                ,ISNULL(ZIT.TihoukoukyoudantaiCD, WK.ZititaiCD) AS [提出先市町村コード]
                --2020/12/24 Y.Matuzawa (182011) 地方公共団体コード地方公共団体コード対応 ↑
                ,NULL AS [提出先区・事務所コード]
                -- 2015/01/05 K.Morita 変更 (19288-19289-19291)eLTax仕様変更対応 ↑
                ,KTC.RiyoushaID AS [利用者ＩＤ]
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↓
                --,KTC.CompanyKanaName AS [氏名又は名称（フリガナ）]
                ,dbo.N0000FC_Convert_HankakuToZenkaku(KTC.CompanyKanaName) AS [氏名又は名称（フリガナ）]
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↑
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↓
                --,@p_CompanyName AS [氏名又は名称]
                ,dbo.N0000FC_Convert_HankakuToZenkaku(@p_CompanyName) AS [氏名又は名称]
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↑
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↓
                --,ISNULL(KAC.Juusho1,'')   + ISNULL(KAC.Juusho2,'')   AS [本店情報所在地]
                ,dbo.N0000FC_Convert_HankakuToZenkaku(ISNULL(KAC.Juusho1,'')   + ISNULL(KAC.Juusho2,''))   AS [本店情報所在地]
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↑
                ,ISNULL(KAC.YuubinNO1,'') + ISNULL(KAC.YuubinNO2,'') AS [本店情報郵便番号]
                ,ISNULL(KAC.TelNO1,'') + '-' + ISNULL(KAC.TelNO2,'') + '-' + ISNULL(KAC.TelNO3,'') AS [本店情報電話番号（１）]
                ,RIGHT('00' + CONVERT(varchar, KTC.DaihyoushaSikaku), 2) AS [代表者代表者資格]
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↓
                --,KAC.DaihyoushaKanaName AS [代表者代表者名（フリガナ）]
                ,dbo.N0000FC_Convert_HankakuToZenkaku(KAC.DaihyoushaKanaName) AS [代表者代表者名（フリガナ）]
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↑
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↓
                --,KAC.Daihyousha AS [代表者代表者名]
                ,dbo.N0000FC_Convert_HankakuToZenkaku(KAC.Daihyousha) AS [代表者代表者名]
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↑
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↓
                --,ISNULL(KTC.DaihyoushaJuusho1,'')   + ISNULL(KTC.DaihyoushaJuusho2,'')   AS [代表者代表者住所]
                ,dbo.N0000FC_Convert_HankakuToZenkaku(ISNULL(KTC.DaihyoushaJuusho1,'')   + ISNULL(KTC.DaihyoushaJuusho2,''))   AS [代表者代表者住所]
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↑
                ,ISNULL(KTC.DaihyoushaYuubinNO1,'') + ISNULL(KTC.DaihyoushaYuubinNO2,'') AS [代表者代表者郵便番号]
                ,ISNULL(KTC.DaihyoushaTELNO1,'') + '-' + ISNULL(KTC.DaihyoushaTELNO2,'') + '-' + ISNULL(KTC.DaihyoushaTELNO3,'') AS [代表者代表者電話番号]
                --2015/12/17 T.Sato  追加  (34037-34032-34034)償却資産申告書法人番号追加対応(eLTax)　開始
                ,KAC.HouzinNO AS [個人番号又は法人番号]
                --2015/12/17 T.Sato  追加  (34037-34032-34034)償却資産申告書法人番号追加対応(eLTax)　終了
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↓
                --,KTC.Yagou AS [事業所情報事業所名]
                ,dbo.N0000FC_Convert_HankakuToZenkaku(KTC.Yagou) AS [事業所情報事業所名]
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↑
                ,11 AS [事業所情報本支店区分]
                ,NULL AS [代理人情報代理人利用者ＩＤ]
                ,NULL AS [代理人情報代理人氏名]
                ,NULL AS [代理人情報代理人電話番号]
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↓
                --,ZIT.ZigyoushoShozaitiName1 AS [団体情報課税地]
                ,dbo.N0000FC_Convert_HankakuToZenkaku(ZIT.ZigyoushoShozaitiName1) AS [団体情報課税地]
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↑
                ,ZIT.ShoyuushaCD AS [様式情報所有者コード]
                ,ISNULL(KAC.YuubinNO1,'') + ISNULL(KAC.YuubinNO2,'') AS [様式情報住所郵便番号]
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↓
                --,ISNULL(KAC.Juusho1KanaName,'') + ISNULL(KAC.Juusho2KanaName,'') AS [様式情報住所（フリガナ）]
                ,LEFT(dbo.N0000FC_Convert_HankakuToZenkaku(ISNULL(KAC.Juusho1KanaName,'') + ISNULL(KAC.Juusho2KanaName,'')),80) AS [様式情報住所（フリガナ）]
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↑
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↓
                --,ISNULL(KAC.Juusho1,'') + ISNULL(KAC.Juusho2,'') AS [様式情報住所]
                ,dbo.N0000FC_Convert_HankakuToZenkaku(ISNULL(KAC.Juusho1,'') + ISNULL(KAC.Juusho2,'')) AS [様式情報住所]
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↑
                ,ISNULL(KAC.TelNO1,'') + '-' + ISNULL(KAC.TelNO2,'') + '-' + ISNULL(KAC.TelNO3,'') AS [様式情報電話]
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↓
                --,KTC.CompanyKanaName AS [様式情報氏名（フリガナ）]
                ,dbo.N0000FC_Convert_HankakuToZenkaku(KTC.CompanyKanaName) AS [様式情報氏名（フリガナ）]
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↑
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↓
                --,@p_CompanyName AS [様式情報氏名]
                ,dbo.N0000FC_Convert_HankakuToZenkaku(@p_CompanyName) AS [様式情報氏名]
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↑
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↓
                --,KTC.ZigyouShumoku AS [様式情報事業種目]
                ,dbo.N0000FC_Convert_HankakuToZenkaku(KTC.ZigyouShumoku) AS [様式情報事業種目]
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↑
                ,KTC.Sihonkin AS [様式情報事業種目（資本金等の額）]

                --2017/04/19 Y.Kunihiro   修正  (59019)固定資産コントロールマスタの設立年月日が明治時代の場合、ファイル出力すると和暦の年が1年ずれる不具合修正 開始
				--,CASE WHEN MONTH(KTC.SeturituDate) = 1 AND DAY(KTC.SeturituDate) = 1     
				--	  THEN CASE WHEN YEAR(DATEADD(day,-1,KTC.SeturituDate)) >= 1989     
				--				THEN 4     
				--				WHEN YEAR(DATEADD(day,-1,KTC.SeturituDate)) >= 1926     
				--				THEN 3     
				--				WHEN YEAR(DATEADD(day,-1,KTC.SeturituDate)) >= 1912     
				--				THEN 2     
				--				WHEN YEAR(DATEADD(day,-1,KTC.SeturituDate)) >= 1867     
				--				THEN 1     
				--		   END     
				--	  ELSE CASE WHEN YEAR(KTC.SeturituDate) >= 1989     
				--				THEN 4     
				--				WHEN YEAR(KTC.SeturituDate) >= 1926     
				--				THEN 3     
				--				WHEN YEAR(KTC.SeturituDate) >= 1912     
				--				THEN 2     
				--				WHEN YEAR(KTC.SeturituDate) >= 1867     
				--				THEN 1     
				--		   END     
				-- END AS [様式情報事業開始年月年号]     
--  2018/05/14 T.Maeda      (97532-97643)改元対応(年号を元号マスタの元号区分に変更)↓↓
                --,CASE WHEN KTC.SeturituDate >= '1989/01/08'
                --      THEN 4
                --      WHEN KTC.SeturituDate >= '1926/12/25'
                --      THEN 3
                --      WHEN KTC.SeturituDate >= '1912/07/30'
                --      THEN 2
                --      WHEN KTC.SeturituDate >= '1868/09/08'
                --      THEN 1
                 --END AS [様式情報事業開始年月年号]
                 ,Gengou.GengouKBN AS [様式情報事業開始年月年号]
--  2018/05/14 T.Maeda      (97532-97643)改元対応(年号を元号マスタの元号区分に変更)↑↑
                --2017/04/19 Y.Kunihiro   修正  (59019)固定資産コントロールマスタの設立年月日が明治時代の場合、ファイル出力すると和暦の年が1年ずれる不具合修正 終了
                --2017/04/19 Y.Kunihiro   修正  (59019)固定資産コントロールマスタの設立年月日が明治時代の場合、ファイル出力すると和暦の年が1年ずれる不具合修正 開始
				--,CASE WHEN MONTH(KTC.SeturituDate) = 1 AND DAY(KTC.SeturituDate) = 1     
				--	  THEN CASE WHEN YEAR(DATEADD(day,-1,KTC.SeturituDate)) >= 1989     
				--				THEN YEAR(DATEADD(day,-1,KTC.SeturituDate)) - 1989 + 1     
				--				WHEN YEAR(DATEADD(day,-1,KTC.SeturituDate)) >= 1926     
				--				THEN YEAR(DATEADD(day,-1,KTC.SeturituDate)) - 1926 + 1     
				--				WHEN YEAR(DATEADD(day,-1,KTC.SeturituDate)) >= 1912     
				--				THEN YEAR(DATEADD(day,-1,KTC.SeturituDate)) - 1912 + 1     
				--				WHEN YEAR(DATEADD(day,-1,KTC.SeturituDate)) >= 1867     
				--				THEN YEAR(DATEADD(day,-1,KTC.SeturituDate)) - 1867 + 1     
				--		   END     
				--	  ELSE CASE WHEN YEAR(KTC.SeturituDate) >= 1989     
				--				THEN YEAR(KTC.SeturituDate) - 1989 + 1     
				--				WHEN YEAR(KTC.SeturituDate) >= 1926     
				--				THEN YEAR(KTC.SeturituDate) - 1926 + 1     
				--				WHEN YEAR(KTC.SeturituDate) >= 1912     
				--				THEN YEAR(KTC.SeturituDate) - 1912 + 1     
				--				WHEN YEAR(KTC.SeturituDate) >= 1867     
				--				THEN YEAR(KTC.SeturituDate) - 1867 + 1     
				--		   END     
				-- END AS [様式情報事業開始年月年]     
--  2018/05/14 T.Maeda      (97532-97643)改元対応(事業開始年を元号マスタから取得)↓↓
                --,CASE WHEN KTC.SeturituDate >= '1989/01/08'
                --      THEN YEAR(KTC.SeturituDate) - 1989 + 1
                --      WHEN KTC.SeturituDate >= '1926/12/25'
                --      THEN YEAR(KTC.SeturituDate) - 1926 + 1
                --      WHEN KTC.SeturituDate >= '1912/07/30'
                --      THEN YEAR(KTC.SeturituDate) - 1912 + 1
                --      WHEN KTC.SeturituDate >= '1868/09/08'
                --      THEN YEAR(KTC.SeturituDate) - 1868 + 1
                -- END AS [様式情報事業開始年月年]
                ,YEAR(KTC.SeturituDate) - YEAR(Gengou.GengouStartDate) + 1 AS [様式情報事業開始年月年]
--  2018/05/14 T.Maeda      (97532-97643)改元対応(事業開始年を元号マスタから取得)↑↑
                --2017/04/19 Y.Kunihiro   修正  (59019)固定資産コントロールマスタの設立年月日が明治時代の場合、ファイル出力すると和暦の年が1年ずれる不具合修正 終了
				--,CASE WHEN MONTH(KTC.SeturituDate) = 1 AND DAY(KTC.SeturituDate) = 1     
				--	  THEN MONTH(DATEADD(day,-1,KTC.SeturituDate))     
				--	  ELSE MONTH(KTC.SeturituDate)     
				-- END AS [様式情報事業開始年月月]
                ,MONTH(KTC.SeturituDate) AS [様式情報事業開始年月月]
                --2017/04/19 Y.Kunihiro   修正  (59019)固定資産コントロールマスタの設立年月日が明治時代の場合、ファイル出力すると和暦の年が1年ずれる不具合修正 終了
                ,NULL AS [様式情報応答する係及び氏名（係）]
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↓
                --,KTC.SinkokuTantoushaName AS [様式情報応答する係及び氏名（氏名）]
                ,dbo.N0000FC_Convert_HankakuToZenkaku(KTC.SinkokuTantoushaName) AS [様式情報応答する係及び氏名（氏名）]
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↑
                ,ISNULL(KTC.TantoushaTELNO1,'') + '-' + ISNULL(KTC.TantoushaTELNO2,'') + '-' + ISNULL(KTC.TantoushaTELNO3,'') AS [様式情報応答する係及び氏名（電話）]
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↓
                --,ZIT.ZigyoushoShozaitiName2 AS [様式情報資産の所在地２]
                ,dbo.N0000FC_Convert_HankakuToZenkaku(ZIT.ZigyoushoShozaitiName2) AS [様式情報資産の所在地２]
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↑
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↓
                --,ZIT.ZigyoushoShozaitiName3 AS [様式情報資産の所在地３]
                ,dbo.N0000FC_Convert_HankakuToZenkaku(ZIT.ZigyoushoShozaitiName3) AS [様式情報資産の所在地３]
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↑
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↓
                --,KTC.KaikeiZimushoName AS [税理士等の氏名氏名]
                ,dbo.N0000FC_Convert_HankakuToZenkaku(KTC.KaikeiZimushoName) AS [税理士等の氏名氏名]
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↑
                ,ISNULL(KTC.KaikeiZimushoTELNO1,'') + '-' + ISNULL(KTC.KaikeiZimushoTELNO2,'') + '-' + ISNULL(KTC.KaikeiZimushoTELNO3,'')  AS [税理士等の氏名電話]
                ,NULLIF(ZIT.TanshukuTaiNenShouninKBN, 0) AS [短縮耐用年数の承認]
                ,NULLIF(ZIT.ZoukaShoukyakuTodokeKBN, 0) AS [増加償却の届出]
                ,NULLIF(ZIT.HikazeiGaitouSisanKBN, 0) AS [非課税該当資産]
                ,NULLIF(ZIT.KazeiHyoujunTokureiKBN, 0) AS [課税標準の特例]
                ,NULLIF(ZIT.TokuShoukyakuAsshukuKichouKBN, 0) AS [特別償却又は圧縮記帳]
                ,CASE WHEN ZIT.ZeiKaiShoukyakuTeirituFlag = 1 AND ZIT.ZeiKaiShoukyakuTeigakuFlag = 0 THEN 1
                      WHEN ZIT.ZeiKaiShoukyakuTeirituFlag = 0 AND ZIT.ZeiKaiShoukyakuTeigakuFlag = 1 THEN 2
                      WHEN ZIT.ZeiKaiShoukyakuTeirituFlag = 1 AND ZIT.ZeiKaiShoukyakuTeigakuFlag = 1 THEN 3
                      ELSE NULL
                 END AS [税務会計上の償却方法]
                ,NULLIF(ZIT.AoiroSinkokuKBN, 0) AS [青色申告]
                ,NULLIF(ZIT.ShakuyouSisanKBN, 0) AS [借用資産有無]
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↓
                --,ISNULL(ZIT.KasinusiName1,'') + ISNULL(ZIT.KasinusiName2,'') AS [借用資産貸主の名称等]
                ,dbo.N0000FC_Convert_HankakuToZenkaku(ISNULL(ZIT.KasinusiName1,'') + ISNULL(ZIT.KasinusiName2,'')) AS [借用資産貸主の名称等]
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↑
                --2015/11/09 T.Sato  修正  (32764)電子申告連携の達人への出力ファイルで所有区分の内容が不正になる件を改修　開始
                --,CASE WHEN ZIT.ShoyuuKBN1 = 1 AND ZIT.ShoyuuKBN2 = 1 AND ZIT.ShoyuuKBN3 = 1 THEN 1
                --      WHEN ZIT.ShoyuuKBN1 = 2 AND ZIT.ShoyuuKBN2 = 2 AND ZIT.ShoyuuKBN3 = 2 THEN 2
                ,CASE WHEN (ZIT.ShoyuuKBN1 = 1 OR ZIT.ShoyuuKBN2 = 1 OR ZIT.ShoyuuKBN3 = 1) AND (ZIT.ShoyuuKBN1 = 2 OR ZIT.ShoyuuKBN2 = 2 OR ZIT.ShoyuuKBN3 = 2) THEN 3
                      WHEN ZIT.ShoyuuKBN1 = 1 OR ZIT.ShoyuuKBN2 = 1 OR ZIT.ShoyuuKBN3 = 1 THEN 1
                      WHEN ZIT.ShoyuuKBN1 = 2 OR ZIT.ShoyuuKBN2 = 2 OR ZIT.ShoyuuKBN3 = 2 THEN 2
                --2015/11/09 T.Sato  修正  (32764)電子申告連携の達人への出力ファイルで所有区分の内容が不正になる件を改修　終了
                      ELSE 3
                 END AS [事業所用家屋の所有区分]
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↓
                 --,ISNULL(ZIT.Bikou1,'') + ISNULL(ZIT.Bikou2,'') + ISNULL(ZIT.Bikou3,'') 
                 --+ ISNULL(ZIT.Bikou4,'') + ISNULL(ZIT.Bikou5,'') + ISNULL(ZIT.Bikou6,'') AS [備考]
                ,dbo.N0000FC_Convert_HankakuToZenkaku(ISNULL(ZIT.Bikou1,'') + ISNULL(ZIT.Bikou2,'') + ISNULL(ZIT.Bikou3,'') 
                 + ISNULL(ZIT.Bikou4,'') + ISNULL(ZIT.Bikou5,'') + ISNULL(ZIT.Bikou6,'')) AS [備考]
                --2020/12/25 Y.Matuzawa (182011) 半角を全角に置き換え ↑
                ,WK.ShutokuKagaku1_1 AS [取得価額前年前に取得したもの（イ）構築物]
                ,WK.ShutokuKagaku1_2 AS [取得価額前年前に取得したもの（イ）機械及び装置]
                ,WK.ShutokuKagaku1_3 AS [取得価額前年前に取得したもの（イ）船舶]
                ,WK.ShutokuKagaku1_4 AS [取得価額前年前に取得したもの（イ）航空機]
                ,WK.ShutokuKagaku1_5 AS [取得価額前年前に取得したもの（イ）車両及び運搬具]
                ,WK.ShutokuKagaku1_6 AS [取得価額前年前に取得したもの（イ）工具、器具及び備品]
                ,WK.ShutokuKagaku1_7 AS [取得価額前年前に取得したもの（イ）合計]
                ,WK.ShutokuKagaku2_1 AS [取得価額前年中に減少したもの（ロ）構築物]
                ,WK.ShutokuKagaku2_2 AS [取得価額前年中に減少したもの（ロ）機械及び装置]
                ,WK.ShutokuKagaku2_3 AS [取得価額前年中に減少したもの（ロ）船舶]
                ,WK.ShutokuKagaku2_4 AS [取得価額前年中に減少したもの（ロ）航空機]
                ,WK.ShutokuKagaku2_5 AS [取得価額前年中に減少したもの（ロ）車両及び運搬具]
                ,WK.ShutokuKagaku2_6 AS [取得価額前年中に減少したもの（ロ）工具、器具及び備品]
                ,WK.ShutokuKagaku2_7 AS [取得価額前年中に減少したもの（ロ）合計]
                ,WK.ShutokuKagaku3_1 AS [取得価額前年中に取得したもの（ハ）構築物]
                ,WK.ShutokuKagaku3_2 AS [取得価額前年中に取得したもの（ハ）機械及び装置]
                ,WK.ShutokuKagaku3_3 AS [取得価額前年中に取得したもの（ハ）船舶]
                ,WK.ShutokuKagaku3_4 AS [取得価額前年中に取得したもの（ハ）航空機]
                ,WK.ShutokuKagaku3_5 AS [取得価額前年中に取得したもの（ハ）車両及び運搬具]
                ,WK.ShutokuKagaku3_6 AS [取得価額前年中に取得したもの（ハ）工具、器具及び備品]
                ,WK.ShutokuKagaku3_7 AS [取得価額前年中に取得したもの（ハ）合計]
                ,WK.ShutokuKagaku4_1 AS [取得価額計（（イ）－（ロ）＋（ハ））（ニ）構築物]
                ,WK.ShutokuKagaku4_2 AS [取得価額計（（イ）－（ロ）＋（ハ））（ニ）機械及び装置]
                ,WK.ShutokuKagaku4_3 AS [取得価額計（（イ）－（ロ）＋（ハ））（ニ）船舶]
                ,WK.ShutokuKagaku4_4 AS [取得価額計（（イ）－（ロ）＋（ハ））（ニ）航空機]
                ,WK.ShutokuKagaku4_5 AS [取得価額計（（イ）－（ロ）＋（ハ））（ニ）車両及び運搬具]
                ,WK.ShutokuKagaku4_6 AS [取得価額計（（イ）－（ロ）＋（ハ））（ニ）工具、器具及び備品]
                ,WK.ShutokuKagaku4_7 AS [取得価額計（（イ）－（ロ）＋（ハ））（ニ）合計]
                ,WK.HonnendoHyoukagaku_1 AS [評価額（ホ）構築物]
                ,WK.HonnendoHyoukagaku_2 AS [評価額（ホ）機械及び装置]
                ,WK.HonnendoHyoukagaku_3 AS [評価額（ホ）船舶]
                ,WK.HonnendoHyoukagaku_4 AS [評価額（ホ）航空機]
                ,WK.HonnendoHyoukagaku_5 AS [評価額（ホ）車両及び運搬具]
                ,WK.HonnendoHyoukagaku_6 AS [評価額（ホ）工具、器具及び備品]
                ,WK.HonnendoHyoukagaku_7 AS [評価額（ホ）合計]
                ,WK.Ketteikagaku_1 AS [決定価格（ヘ）構築物]
                ,WK.Ketteikagaku_2 AS [決定価格（ヘ）機械及び装置]
                ,WK.Ketteikagaku_3 AS [決定価格（ヘ）船舶]
                ,WK.Ketteikagaku_4 AS [決定価格（ヘ）航空機]
                ,WK.Ketteikagaku_5 AS [決定価格（ヘ）車両及び運搬具]
                ,WK.Ketteikagaku_6 AS [決定価格（ヘ）工具、器具及び備品]
                ,WK.Ketteikagaku_7 AS [決定価格（ヘ）合計]
                ,WK.KazeiHyoujungaku_1 AS [課税標準額（ト）構築物]
                ,WK.KazeiHyoujungaku_2 AS [課税標準額（ト）機械及び装置]
                ,WK.KazeiHyoujungaku_3 AS [課税標準額（ト）船舶]
                ,WK.KazeiHyoujungaku_4 AS [課税標準額（ト）航空機]
                ,WK.KazeiHyoujungaku_5 AS [課税標準額（ト）車両及び運搬具]
                ,WK.KazeiHyoujungaku_6 AS [課税標準額（ト）工具、器具及び備品]
                ,WK.KazeiHyoujungaku_7 AS [課税標準額（ト）合計]
                ,WK.Kensuu_1 AS [件数構築物]
                ,WK.Kensuu_2 AS [件数機械及び装置]
                ,WK.Kensuu_3 AS [件数船舶]
                ,WK.Kensuu_4 AS [件数航空機]
                ,WK.Kensuu_5 AS [件数車両及び運搬具]
                ,WK.Kensuu_6 AS [件数工具、器具及び備品]
                ,WK.Kensuu_7 AS [件数合計]
                ,'' AS [種類別明細書ファイル名] -- ファイル名はC#側で作成したものをセットする
                ,NULL AS [その他添付ファイル名]
            FROM #N0000WK_Obic7fx_Win_N0000_Print_NA_ShoukyakuSisanSinkokusho_Print WK
            LEFT JOIN N0000MS_NA_Zititai ZIT
            ON  ZIT.ZititaiCD = WK.ZititaiCD        
            LEFT JOIN N0000MS_NA_KoteiSisanControl KTC
            ON  KTC.NendoID = 0
            LEFT JOIN M0000MS_MA_KaikeiControl KAC
            ON  KAC.ControlKey = 1
--  2018/05/14 T.Maeda      (97532-97643)改元対応(元号マスタを結合)↓↓
            LEFT JOIN A0000MS_Gengou AS Gengou
            ON Gengou.GengouStartDate<= KTC.SeturituDate
            AND DATEADD(DAY,1,Gengou.GengouEndDate) > KTC.SeturituDate
--  2018/05/14 T.Maeda      (97532-97643)改元対応(元号マスタを結合)↑↑
            WHERE ZIT.eLTaxSiyouKBN = 1
--  2019/03/08 M.Hamachika  修正  (123875)出力のたびに自治体の並び順が変わってしまう不具合修正 ↓
            ORDER BY WK.ZititaiCD
--  2019/03/08 M.Hamachika  修正  (123875)出力のたびに自治体の並び順が変わってしまう不具合修正 ↑

            SET @ErrorCode = @@ERROR
            IF  @ErrorCode <> 0
            BEGIN
                GOTO ERR_SHORI
            END
        
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

