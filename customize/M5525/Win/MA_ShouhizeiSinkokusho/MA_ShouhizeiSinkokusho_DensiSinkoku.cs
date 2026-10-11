//
//  2019/9/02  新規作成    M.Washizu
//2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↓


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Obic7fx.Win.M5525.Print.MA_ShouhizeiSinkokusho
{
    public class MA_ShouhizeiSinkokusho_DensiSinkoku
    {
        public MA_ShouhizeiSinkokusho_DensiSinkoku()
        {
            DataInfo dataInfo = new DataInfo();
        }

        [System.Xml.Serialization.XmlRoot("REPORT")]
        public class DataInfo
        {
            [System.Xml.Serialization.XmlElement("SYSTEM")]
            public SystemTag SystemTag { get; set; }

            [System.Xml.Serialization.XmlElement("BASIC")]
            public BASIC BASIC { get; set; }

            [System.Xml.Serialization.XmlElement("DATA")]
            public DATA DATA { get; set; }

            public DataInfo()
            {
                SystemTag = new SystemTag();
                BASIC = new BASIC();
                DATA = new DATA();
            }
        }

        public class SystemTag
        {
            [System.Xml.Serialization.XmlElement("GYOUMUPREFIX")]
            public String GYOUMUPREFIX { get; set; }

            [System.Xml.Serialization.XmlElement("VERSION")]
            public String VERSION { get; set; }

            [System.Xml.Serialization.XmlElement("FUNCTIONID")]
            public String FUNCTIONID { get; set; }

            [System.Xml.Serialization.XmlElement("CONNECTEDPROGRAMVER")]
            public String CONNECTEDPROGRAMVER { get; set; }

            [System.Xml.Serialization.XmlElement("PROGRAMNAME")]
            public String PROGRAMNAME { get; set; }

            [System.Xml.Serialization.XmlElement("PROGRAMVER")]
            public String PROGRAMVER { get; set; }
            public SystemTag()
            {
                GYOUMUPREFIX = String.Empty;
                VERSION = String.Empty;
                FUNCTIONID = String.Empty;
                CONNECTEDPROGRAMVER = String.Empty;
                PROGRAMNAME = String.Empty;
                PROGRAMVER = String.Empty;
            }
        }

        public class BASIC
        {
            [System.Xml.Serialization.XmlElement("KOMONSAKINAME")]
            public String KOMONSAKINAME { get; set; }

            [System.Xml.Serialization.XmlElement("JIGYOUNENDOFROM")]
            public String JIGYOUNENDOFROM { get; set; }

            [System.Xml.Serialization.XmlElement("JIGYOUNENDOTO")]
            public String JIGYOUNENDOTO { get; set; }

            [System.Xml.Serialization.XmlElement("HOUJINKOJINKUBUN")]
            public String HOUJINKOJINKUBUN { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIKUBUN")]
            public String KAZEIKUBUN { get; set; }

            [System.Xml.Serialization.XmlElement("KEIKASHOCHIKAZEISHISAN")]
            public String KEIKASHOCHIKAZEISHISAN { get; set; }

            [System.Xml.Serialization.XmlElement("URIAGEZEIGAKUKEISAN")]
            public String URIAGEZEIGAKUKEISAN { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREZEIGAKUKEISAN")]
            public String SHIIREZEIGAKUKEISAN { get; set; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public BASIC()
            {
                KOMONSAKINAME = String.Empty;
                JIGYOUNENDOFROM = String.Empty;
                JIGYOUNENDOTO = String.Empty;
                HOUJINKOJINKUBUN = String.Empty;
                KAZEIKUBUN = String.Empty;
                URIAGEZEIGAKUKEISAN = String.Empty;
                SHIIREZEIGAKUKEISAN = String.Empty;
            }
        }

        public class DATA
        {
            [System.Xml.Serialization.XmlElement("D020114")]
            [System.ComponentModel.DefaultValue("")]
            public D020114 D020114 { get; set; }

            [System.Xml.Serialization.XmlElement("D020113")]
            [System.ComponentModel.DefaultValue("")]
            public D020113 D020113 { get; set; }

            [System.Xml.Serialization.XmlElement("D020110")]
            [System.ComponentModel.DefaultValue("")]
            public D020110 D020110 { get; set; }

            [System.Xml.Serialization.XmlElement("D020111")]
            [System.ComponentModel.DefaultValue("")]
            public D020111 D020111 { get; set; }

            [System.Xml.Serialization.XmlElement("D020112")]
            [System.ComponentModel.DefaultValue("")]
            public D020112 D020112 { get; set; }

            [System.Xml.Serialization.XmlElement("D020124")]
            [System.ComponentModel.DefaultValue("")]
            public D020124 D020124 { get; set; }

            [System.Xml.Serialization.XmlElement("D020123")]
            [System.ComponentModel.DefaultValue("")]
            public D020123 D020123 { get; set; }

            [System.Xml.Serialization.XmlElement("D020120")]
            [System.ComponentModel.DefaultValue("")]
            public D020120 D020120 { get; set; }

            [System.Xml.Serialization.XmlElement("D020121")]
            [System.ComponentModel.DefaultValue("")]
            public D020121 D020121 { get; set; }

            [System.Xml.Serialization.XmlElement("D020122")]
            [System.ComponentModel.DefaultValue("")]
            public D020122 D020122 { get; set; }

            [System.Xml.Serialization.XmlElement("D020134")]
            [System.ComponentModel.DefaultValue("")]
            public D020134 D020134 { get; set; }

            [System.Xml.Serialization.XmlElement("D020133")]
            [System.ComponentModel.DefaultValue("")]
            public D020133 D020133 { get; set; }

            [System.Xml.Serialization.XmlElement("D020130")]
            [System.ComponentModel.DefaultValue("")]
            public D020130 D020130 { get; set; }

            [System.Xml.Serialization.XmlElement("D020131")]
            [System.ComponentModel.DefaultValue("")]
            public D020131 D020131 { get; set; }

            [System.Xml.Serialization.XmlElement("D020132")]
            [System.ComponentModel.DefaultValue("")]
            public D020132 D020132 { get; set; }

            [System.Xml.Serialization.XmlElement("D02204")]
            [System.ComponentModel.DefaultValue("")]
            public D02204 D02204 { get; set; }

            [System.Xml.Serialization.XmlElement("D02203")]
            [System.ComponentModel.DefaultValue("")]
            public D02203 D02203 { get; set; }

            [System.Xml.Serialization.XmlElement("D02200")]
            [System.ComponentModel.DefaultValue("")]
            public D02200 D02200 { get; set; }

            [System.Xml.Serialization.XmlElement("D02201")]
            [System.ComponentModel.DefaultValue("")]
            public D02201 D02201 { get; set; }

            [System.Xml.Serialization.XmlElement("D02202")]
            [System.ComponentModel.DefaultValue("")]
            public D02202 D02202 { get; set; }


            /// <summary>
            /// コンストラクタ
            /// </summary>
            public DATA()
            {
                D020114 = new D020114();
                D020113 = new D020113();
                D020110 = new D020110();
                D020111 = new D020111();
                D020112 = new D020112();
                D020124 = new D020124();
                D020123 = new D020123();
                D020120 = new D020120();
                D020121 = new D020121();
                D020122 = new D020122();
                D020134 = new D020134();
                D020133 = new D020133();
                D020130 = new D020130();
                D020131 = new D020131();
                D020132 = new D020132();
                D02204 = new D02204();
                D02203 = new D02203();
                D02200 = new D02200();
                D02201 = new D02201();
                D02202 = new D02202();
            }
        }

        public class D020114
        {
            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEINUKI10")]
            public String KAZEIURIAGEZEINUKI10 { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEIGAKU10")]
            public String KAZEIURIAGEZEIGAKU10 { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEIKOMI10")]
            public String KAZEIURIAGEZEIKOMI10 { get; set; }

            [System.Xml.Serialization.XmlElement("MENZEIURIAGEZEINUKI10")]
            public String MENZEIURIAGEZEINUKI10 { get; set; }

            [System.Xml.Serialization.XmlElement("HIKAZEIURIAGEYUUKAZEINUKI10")]
            public String HIKAZEIURIAGEYUUKAZEINUKI10 { get; set; }

            [System.Xml.Serialization.XmlElement("HIKAZEIURIAGEYUUKAIGAIZEINUKI10")]
            public String HIKAZEIURIAGEYUUKAIGAIZEINUKI10 { get; set; }

            [System.Xml.Serialization.XmlElement("HIKAZEISHISANYUSHUTSUZEINUKI10")]
            public String HIKAZEISHISANYUSHUTSUZEINUKI10 { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEINUKI10")]
            public String KAZEIURIAGEHENKANZEINUKI10 { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEIGAKU10")]
            public String KAZEIURIAGEHENKANZEIGAKU10 { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEIKOMI10")]
            public String KAZEIURIAGEHENKANZEIKOMI10 { get; set; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public D020114()
            {
                KAZEIURIAGEZEINUKI10 = String.Empty;
                KAZEIURIAGEZEIGAKU10 = String.Empty;
                KAZEIURIAGEZEIKOMI10 = String.Empty;
                MENZEIURIAGEZEINUKI10 = String.Empty;
                HIKAZEIURIAGEYUUKAZEINUKI10 = String.Empty;
                HIKAZEIURIAGEYUUKAIGAIZEINUKI10 = String.Empty;
                HIKAZEISHISANYUSHUTSUZEINUKI10 = String.Empty;
                KAZEIURIAGEHENKANZEINUKI10 = String.Empty;
                KAZEIURIAGEHENKANZEIGAKU10 = String.Empty;
                KAZEIURIAGEHENKANZEIKOMI10 = String.Empty;
            }
        }

        public class D020113
        {
            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEINUKI8K")]
            public String KAZEIURIAGEZEINUKI8K { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEIGAKU8K")]
            public String KAZEIURIAGEZEIGAKU8K { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEIKOMI8K")]
            public String KAZEIURIAGEZEIKOMI8K { get; set; }

            [System.Xml.Serialization.XmlElement("MENZEIURIAGEZEINUKI8K")]
            public String MENZEIURIAGEZEINUKI8K { get; set; }

            [System.Xml.Serialization.XmlElement("HIKAZEIURIAGEYUUKAZEINUKI8K")]
            public String HIKAZEIURIAGEYUUKAZEINUKI8K { get; set; }

            [System.Xml.Serialization.XmlElement("HIKAZEIURIAGEYUUKAIGAIZEINUKI8K")]
            public String HIKAZEIURIAGEYUUKAIGAIZEINUKI8K { get; set; }

            [System.Xml.Serialization.XmlElement("HIKAZEISHISANYUSHUTSUZEINUKI8K")]
            public String HIKAZEISHISANYUSHUTSUZEINUKI8K { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEINUKI8K")]
            public String KAZEIURIAGEHENKANZEINUKI8K { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEIGAKU8K")]
            public String KAZEIURIAGEHENKANZEIGAKU8K { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEIKOMI8K")]
            public String KAZEIURIAGEHENKANZEIKOMI8K { get; set; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public D020113()
            {
                KAZEIURIAGEZEINUKI8K = String.Empty;
                KAZEIURIAGEZEIGAKU8K = String.Empty;
                KAZEIURIAGEZEIKOMI8K = String.Empty;
                MENZEIURIAGEZEINUKI8K = String.Empty;
                HIKAZEIURIAGEYUUKAZEINUKI8K = String.Empty;
                HIKAZEIURIAGEYUUKAIGAIZEINUKI8K = String.Empty;
                HIKAZEISHISANYUSHUTSUZEINUKI8K = String.Empty;
                KAZEIURIAGEHENKANZEINUKI8K = String.Empty;
                KAZEIURIAGEHENKANZEIGAKU8K = String.Empty;
                KAZEIURIAGEHENKANZEIKOMI8K = String.Empty;
            }
        }

        public class D020110
        {
            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEINUKI8")]
            public String KAZEIURIAGEZEINUKI8 { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEIGAKU8")]
            public String KAZEIURIAGEZEIGAKU8 { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEIKOMI8")]
            public String KAZEIURIAGEZEIKOMI8 { get; set; }

            [System.Xml.Serialization.XmlElement("MENZEIURIAGEZEINUKI8")]
            public String MENZEIURIAGEZEINUKI8 { get; set; }

            [System.Xml.Serialization.XmlElement("HIKAZEIURIAGEYUUKAZEINUKI8")]
            public String HIKAZEIURIAGEYUUKAZEINUKI8 { get; set; }

            [System.Xml.Serialization.XmlElement("HIKAZEIURIAGEYUUKAIGAIZEINUKI8")]
            public String HIKAZEIURIAGEYUUKAIGAIZEINUKI8 { get; set; }

            [System.Xml.Serialization.XmlElement("HIKAZEISHISANYUSHUTSUZEINUKI8")]
            public String HIKAZEISHISANYUSHUTSUZEINUKI8 { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEINUKI8")]
            public String KAZEIURIAGEHENKANZEINUKI8 { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEIGAKU8")]
            public String KAZEIURIAGEHENKANZEIGAKU8 { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEIKOMI8")]
            public String KAZEIURIAGEHENKANZEIKOMI8 { get; set; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public D020110()
            {
                KAZEIURIAGEZEINUKI8 = String.Empty;
                KAZEIURIAGEZEIGAKU8 = String.Empty;
                KAZEIURIAGEZEIKOMI8 = String.Empty;
                MENZEIURIAGEZEINUKI8 = String.Empty;
                HIKAZEIURIAGEYUUKAZEINUKI8 = String.Empty;
                HIKAZEIURIAGEYUUKAIGAIZEINUKI8 = String.Empty;
                HIKAZEISHISANYUSHUTSUZEINUKI8 = String.Empty;
                KAZEIURIAGEHENKANZEINUKI8 = String.Empty;
                KAZEIURIAGEHENKANZEIGAKU8 = String.Empty;
                KAZEIURIAGEHENKANZEIKOMI8 = String.Empty;
            }
        }

        public class D020111
        {
            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEINUKI5")]
            public String KAZEIURIAGEZEINUKI5 { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEIGAKU5")]
            public String KAZEIURIAGEZEIGAKU5 { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEIKOMI5")]
            public String KAZEIURIAGEZEIKOMI5 { get; set; }

            [System.Xml.Serialization.XmlElement("MENZEIURIAGEZEINUKI5")]
            public String MENZEIURIAGEZEINUKI5 { get; set; }

            [System.Xml.Serialization.XmlElement("HIKAZEIURIAGEYUUKAZEINUKI5")]
            public String HIKAZEIURIAGEYUUKAZEINUKI5 { get; set; }

            [System.Xml.Serialization.XmlElement("HIKAZEIURIAGEYUUKAIGAIZEINUKI5")]
            public String HIKAZEIURIAGEYUUKAIGAIZEINUKI5 { get; set; }

            [System.Xml.Serialization.XmlElement("HIKAZEISHISANYUSHUTSUZEINUKI5")]
            public String HIKAZEISHISANYUSHUTSUZEINUKI5 { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEINUKI5")]
            public String KAZEIURIAGEHENKANZEINUKI5 { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEIGAKU5")]
            public String KAZEIURIAGEHENKANZEIGAKU5 { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEIKOMI5")]
            public String KAZEIURIAGEHENKANZEIKOMI5 { get; set; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public D020111()
            {
                KAZEIURIAGEZEINUKI5 = String.Empty;
                KAZEIURIAGEZEIGAKU5 = String.Empty;
                KAZEIURIAGEZEIKOMI5 = String.Empty;
                MENZEIURIAGEZEINUKI5 = String.Empty;
                HIKAZEIURIAGEYUUKAZEINUKI5 = String.Empty;
                HIKAZEIURIAGEYUUKAIGAIZEINUKI5 = String.Empty;
                HIKAZEISHISANYUSHUTSUZEINUKI5 = String.Empty;
                KAZEIURIAGEHENKANZEINUKI5 = String.Empty;
                KAZEIURIAGEHENKANZEIGAKU5 = String.Empty;
                KAZEIURIAGEHENKANZEIKOMI5 = String.Empty;
            }
        }

        public class D020112
        {
            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEINUKI3")]
            public String KAZEIURIAGEZEINUKI3 { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEIGAKU3")]
            public String KAZEIURIAGEZEIGAKU3 { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEIKOMI3")]
            public String KAZEIURIAGEZEIKOMI3 { get; set; }

            [System.Xml.Serialization.XmlElement("MENZEIURIAGEZEINUKI3")]
            public String MENZEIURIAGEZEINUKI3 { get; set; }

            [System.Xml.Serialization.XmlElement("HIKAZEIURIAGEYUUKAZEINUKI3")]
            public String HIKAZEIURIAGEYUUKAZEINUKI3 { get; set; }

            [System.Xml.Serialization.XmlElement("HIKAZEIURIAGEYUUKAIGAIZEINUKI3")]
            public String HIKAZEIURIAGEYUUKAIGAIZEINUKI3 { get; set; }

            [System.Xml.Serialization.XmlElement("HIKAZEISHISANYUSHUTSUZEINUKI3")]
            public String HIKAZEISHISANYUSHUTSUZEINUKI3 { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEINUKI3")]
            public String KAZEIURIAGEHENKANZEINUKI3 { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEIGAKU3")]
            public String KAZEIURIAGEHENKANZEIGAKU3 { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEIKOMI3")]
            public String KAZEIURIAGEHENKANZEIKOMI3 { get; set; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public D020112()
            {
                KAZEIURIAGEZEINUKI3 = String.Empty;
                KAZEIURIAGEZEIGAKU3 = String.Empty;
                KAZEIURIAGEZEIKOMI3 = String.Empty;
                MENZEIURIAGEZEINUKI3 = String.Empty;
                HIKAZEIURIAGEYUUKAZEINUKI3 = String.Empty;
                HIKAZEIURIAGEYUUKAIGAIZEINUKI3 = String.Empty;
                HIKAZEISHISANYUSHUTSUZEINUKI3 = String.Empty;
                KAZEIURIAGEHENKANZEINUKI3 = String.Empty;
                KAZEIURIAGEHENKANZEIGAKU3 = String.Empty;
                KAZEIURIAGEHENKANZEIKOMI3 = String.Empty;
            }
        }

        public class D020124
        {
            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEINUKI10")]
            public String SHIIREKAZEIZEINUKI10 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIGAKU10")]
            public String SHIIREKAZEIZEIGAKU10 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIKOMI10")]
            public String SHIIREKAZEIZEIKOMI10 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEINUKI10")]
            public String SHIIREHIKAZEIZEINUKI10 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIGAKU10")]
            public String SHIIREHIKAZEIZEIGAKU10 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIKOMI10")]
            public String SHIIREHIKAZEIZEIKOMI10 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEINUKI10")]
            public String SHIIREKYOTUUZEINUKI10 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIGAKU10")]
            public String SHIIREKYOTUUZEIGAKU10 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIKOMI10")]
            public String SHIIREKYOTUUZEIKOMI10 { get; set; }

            [System.Xml.Serialization.XmlElement("TOKUTEISHIIREKAZEIZEINUKI10")]
            public String TOKUTEISHIIREKAZEIZEINUKI10 { get; set; }

            [System.Xml.Serialization.XmlElement("TOKUTEISHIIREHIKAZEIZEINUKI10")]
            public String TOKUTEISHIIREHIKAZEIZEINUKI10 { get; set; }

            [System.Xml.Serialization.XmlElement("TOKUTEISHIIREKYOTUUZEINUKI10")]
            public String TOKUTEISHIIREKYOTUUZEINUKI10 { get; set; }

            [System.Xml.Serialization.XmlElement("YUNYUSHIIREKAZEIZEIGAKU10")]
            public String YUNYUSHIIREKAZEIZEIGAKU10 { get; set; }

            [System.Xml.Serialization.XmlElement("YUNYUSHIIREHIKAZEIZEIGAKU10")]
            public String YUNYUSHIIREHIKAZEIZEIGAKU10 { get; set; }

            [System.Xml.Serialization.XmlElement("YUNYUSHIIREKYOTUUZEIGAKU10")]
            public String YUNYUSHIIREKYOTUUZEIGAKU10 { get; set; }

            [System.Xml.Serialization.XmlElement("YUNYUSHIIRETIHO10")]
            public String YUNYUSHIIRETIHO10 { get; set; }

            [System.Xml.Serialization.XmlElement("TOKUTEISHIIREHENKANKAZEIZEINUKI10")]
            public String TOKUTEISHIIREHENKANKAZEIZEINUKI10 { get; set; }

            [System.Xml.Serialization.XmlElement("TOKUTEISHIIREHENKANHIKAZEIZEINUKI10")]
            public String TOKUTEISHIIREHENKANHIKAZEIZEINUKI10 { get; set; }

            [System.Xml.Serialization.XmlElement("TOKUTEISHIIREHENKANKYOTUUZEINUKI10")]
            public String TOKUTEISHIIREHENKANKYOTUUZEINUKI10 { get; set; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public D020124()
            {
                SHIIREKAZEIZEINUKI10 = String.Empty;
                SHIIREKAZEIZEIGAKU10 = String.Empty;
                SHIIREKAZEIZEIKOMI10 = String.Empty;
                SHIIREHIKAZEIZEINUKI10 = String.Empty;
                SHIIREHIKAZEIZEIGAKU10 = String.Empty;
                SHIIREHIKAZEIZEIKOMI10 = String.Empty;
                SHIIREKYOTUUZEINUKI10 = String.Empty;
                SHIIREKYOTUUZEIGAKU10 = String.Empty;
                SHIIREKYOTUUZEIKOMI10 = String.Empty;
                TOKUTEISHIIREKAZEIZEINUKI10 = String.Empty;
                TOKUTEISHIIREHIKAZEIZEINUKI10 = String.Empty;
                TOKUTEISHIIREKYOTUUZEINUKI10 = String.Empty;
                YUNYUSHIIREKAZEIZEIGAKU10 = String.Empty;
                YUNYUSHIIREHIKAZEIZEIGAKU10 = String.Empty;
                YUNYUSHIIREKYOTUUZEIGAKU10 = String.Empty;
                YUNYUSHIIRETIHO10 = String.Empty;
                TOKUTEISHIIREHENKANKAZEIZEINUKI10 = String.Empty;
                TOKUTEISHIIREHENKANHIKAZEIZEINUKI10 = String.Empty;
                TOKUTEISHIIREHENKANKYOTUUZEINUKI10 = String.Empty;
            }
        }

        public class D020123
        {
            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEINUKI8K")]
            public String SHIIREKAZEIZEINUKI8K { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIGAKU8K")]
            public String SHIIREKAZEIZEIGAKU8K { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIKOMI8K")]
            public String SHIIREKAZEIZEIKOMI8K { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEINUKI8K")]
            public String SHIIREHIKAZEIZEINUKI8K { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIGAKU8K")]
            public String SHIIREHIKAZEIZEIGAKU8K { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIKOMI8K")]
            public String SHIIREHIKAZEIZEIKOMI8K { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEINUKI8K")]
            public String SHIIREKYOTUUZEINUKI8K { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIGAKU8K")]
            public String SHIIREKYOTUUZEIGAKU8K { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIKOMI8K")]
            public String SHIIREKYOTUUZEIKOMI8K { get; set; }

            [System.Xml.Serialization.XmlElement("YUNYUSHIIREKAZEIZEIGAKU8K")]
            public String YUNYUSHIIREKAZEIZEIGAKU8K { get; set; }

            [System.Xml.Serialization.XmlElement("YUNYUSHIIREHIKAZEIZEIGAKU8K")]
            public String YUNYUSHIIREHIKAZEIZEIGAKU8K { get; set; }

            [System.Xml.Serialization.XmlElement("YUNYUSHIIREKYOTUUZEIGAKU8K")]
            public String YUNYUSHIIREKYOTUUZEIGAKU8K { get; set; }

            [System.Xml.Serialization.XmlElement("YUNYUSHIIRETIHO8K")]
            public String YUNYUSHIIRETIHO8K { get; set; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public D020123()
            {
                SHIIREKAZEIZEINUKI8K = String.Empty;
                SHIIREKAZEIZEIGAKU8K = String.Empty;
                SHIIREKAZEIZEIKOMI8K = String.Empty;
                SHIIREHIKAZEIZEINUKI8K = String.Empty;
                SHIIREHIKAZEIZEIGAKU8K = String.Empty;
                SHIIREHIKAZEIZEIKOMI8K = String.Empty;
                SHIIREKYOTUUZEINUKI8K = String.Empty;
                SHIIREKYOTUUZEIGAKU8K = String.Empty;
                SHIIREKYOTUUZEIKOMI8K = String.Empty;
                YUNYUSHIIREKAZEIZEIGAKU8K = String.Empty;
                YUNYUSHIIREHIKAZEIZEIGAKU8K = String.Empty;
                YUNYUSHIIREKYOTUUZEIGAKU8K = String.Empty;
                YUNYUSHIIRETIHO8K = String.Empty;
            }
        }

        public class D020120
        {
            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEINUKI8")]
            public String SHIIREKAZEIZEINUKI8 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIGAKU8")]
            public String SHIIREKAZEIZEIGAKU8 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIKOMI8")]
            public String SHIIREKAZEIZEIKOMI8 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEINUKI8")]
            public String SHIIREHIKAZEIZEINUKI8 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIGAKU8")]
            public String SHIIREHIKAZEIZEIGAKU8 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIKOMI8")]
            public String SHIIREHIKAZEIZEIKOMI8 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEINUKI8")]
            public String SHIIREKYOTUUZEINUKI8 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIGAKU8")]
            public String SHIIREKYOTUUZEIGAKU8 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIKOMI8")]
            public String SHIIREKYOTUUZEIKOMI8 { get; set; }

            [System.Xml.Serialization.XmlElement("TOKUTEISHIIREKAZEIZEINUKI8")]
            public String TOKUTEISHIIREKAZEIZEINUKI8 { get; set; }

            [System.Xml.Serialization.XmlElement("TOKUTEISHIIREHIKAZEIZEINUKI8")]
            public String TOKUTEISHIIREHIKAZEIZEINUKI8 { get; set; }

            [System.Xml.Serialization.XmlElement("TOKUTEISHIIREKYOTUUZEINUKI8")]
            public String TOKUTEISHIIREKYOTUUZEINUKI8 { get; set; }

            [System.Xml.Serialization.XmlElement("YUNYUSHIIREKAZEIZEIGAKU8")]
            public String YUNYUSHIIREKAZEIZEIGAKU8 { get; set; }

            [System.Xml.Serialization.XmlElement("YUNYUSHIIREHIKAZEIZEIGAKU8")]
            public String YUNYUSHIIREHIKAZEIZEIGAKU8 { get; set; }

            [System.Xml.Serialization.XmlElement("YUNYUSHIIREKYOTUUZEIGAKU8")]
            public String YUNYUSHIIREKYOTUUZEIGAKU8 { get; set; }

            [System.Xml.Serialization.XmlElement("YUNYUSHIIRETIHO8")]
            public String YUNYUSHIIRETIHO8 { get; set; }

            [System.Xml.Serialization.XmlElement("TOKUTEISHIIREHENKANKAZEIZEINUKI8")]
            public String TOKUTEISHIIREHENKANKAZEIZEINUKI8 { get; set; }

            [System.Xml.Serialization.XmlElement("TOKUTEISHIIREHENKANHIKAZEIZEINUKI8")]
            public String TOKUTEISHIIREHENKANHIKAZEIZEINUKI8 { get; set; }

            [System.Xml.Serialization.XmlElement("TOKUTEISHIIREHENKANKYOTUUZEINUKI8")]
            public String TOKUTEISHIIREHENKANKYOTUUZEINUKI8 { get; set; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public D020120()
            {
                SHIIREKAZEIZEINUKI8 = String.Empty;
                SHIIREKAZEIZEIGAKU8 = String.Empty;
                SHIIREKAZEIZEIKOMI8 = String.Empty;
                SHIIREHIKAZEIZEINUKI8 = String.Empty;
                SHIIREHIKAZEIZEIGAKU8 = String.Empty;
                SHIIREHIKAZEIZEIKOMI8 = String.Empty;
                SHIIREKYOTUUZEINUKI8 = String.Empty;
                SHIIREKYOTUUZEIGAKU8 = String.Empty;
                SHIIREKYOTUUZEIKOMI8 = String.Empty;
                TOKUTEISHIIREKAZEIZEINUKI8 = String.Empty;
                TOKUTEISHIIREHIKAZEIZEINUKI8 = String.Empty;
                TOKUTEISHIIREKYOTUUZEINUKI8 = String.Empty;
                YUNYUSHIIREKAZEIZEIGAKU8 = String.Empty;
                YUNYUSHIIREHIKAZEIZEIGAKU8 = String.Empty;
                YUNYUSHIIREKYOTUUZEIGAKU8 = String.Empty;
                YUNYUSHIIRETIHO8 = String.Empty;
                TOKUTEISHIIREHENKANKAZEIZEINUKI8 = String.Empty;
                TOKUTEISHIIREHENKANHIKAZEIZEINUKI8 = String.Empty;
                TOKUTEISHIIREHENKANKYOTUUZEINUKI8 = String.Empty;
            }
        }

        public class D020121
        {
            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEINUKI5")]
            public String SHIIREKAZEIZEINUKI5 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIGAKU5")]
            public String SHIIREKAZEIZEIGAKU5 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIKOMI5")]
            public String SHIIREKAZEIZEIKOMI5 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEINUKI5")]
            public String SHIIREHIKAZEIZEINUKI5 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIGAKU5")]
            public String SHIIREHIKAZEIZEIGAKU5 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIKOMI5")]
            public String SHIIREHIKAZEIZEIKOMI5 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEINUKI5")]
            public String SHIIREKYOTUUZEINUKI5 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIGAKU5")]
            public String SHIIREKYOTUUZEIGAKU5 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIKOMI5")]
            public String SHIIREKYOTUUZEIKOMI5 { get; set; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public D020121()
            {
                SHIIREKAZEIZEINUKI5 = String.Empty;
                SHIIREKAZEIZEIGAKU5 = String.Empty;
                SHIIREKAZEIZEIKOMI5 = String.Empty;
                SHIIREHIKAZEIZEINUKI5 = String.Empty;
                SHIIREHIKAZEIZEIGAKU5 = String.Empty;
                SHIIREHIKAZEIZEIKOMI5 = String.Empty;
                SHIIREKYOTUUZEINUKI5 = String.Empty;
                SHIIREKYOTUUZEIGAKU5 = String.Empty;
                SHIIREKYOTUUZEIKOMI5 = String.Empty;
            }
        }

        public class D020122
        {
            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEINUKI3")]
            public String SHIIREKAZEIZEINUKI3 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIGAKU3")]
            public String SHIIREKAZEIZEIGAKU3 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIKOMI3")]
            public String SHIIREKAZEIZEIKOMI3 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEINUKI3")]
            public String SHIIREHIKAZEIZEINUKI3 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIGAKU3")]
            public String SHIIREHIKAZEIZEIGAKU3 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIKOMI3")]
            public String SHIIREHIKAZEIZEIKOMI3 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEINUKI3")]
            public String SHIIREKYOTUUZEINUKI3 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIGAKU3")]
            public String SHIIREKYOTUUZEIGAKU3 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIKOMI3")]
            public String SHIIREKYOTUUZEIKOMI3 { get; set; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public D020122()
            {
                SHIIREKAZEIZEINUKI3 = String.Empty;
                SHIIREKAZEIZEIGAKU3 = String.Empty;
                SHIIREKAZEIZEIKOMI3 = String.Empty;
                SHIIREHIKAZEIZEINUKI3 = String.Empty;
                SHIIREHIKAZEIZEIGAKU3 = String.Empty;
                SHIIREHIKAZEIZEIKOMI3 = String.Empty;
                SHIIREKYOTUUZEINUKI3 = String.Empty;
                SHIIREKYOTUUZEIGAKU3 = String.Empty;
                SHIIREKYOTUUZEIKOMI3 = String.Empty;
            }
        }

        public class D020134
        {
            [System.Xml.Serialization.XmlElement("KASHIDAOREZEINUKI10")]
            public String KASHIDAOREZEINUKI10 { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREZEIGAKU10")]
            public String KASHIDAOREZEIGAKU10 { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREZEIKOMI10")]
            public String KASHIDAOREZEIKOMI10 { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEINUKI10")]
            public String KASHIDAOREKAISHUZEINUKI10 { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEIGAKU10")]
            public String KASHIDAOREKAISHUZEIGAKU10 { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEIKOMI10")]
            public String KASHIDAOREKAISHUZEIKOMI10 { get; set; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public D020134()
            {
                KASHIDAOREZEINUKI10 = String.Empty;
                KASHIDAOREZEIGAKU10 = String.Empty;
                KASHIDAOREZEIKOMI10 = String.Empty;
                KASHIDAOREKAISHUZEINUKI10 = String.Empty;
                KASHIDAOREKAISHUZEIGAKU10 = String.Empty;
                KASHIDAOREKAISHUZEIKOMI10 = String.Empty;
            }
        }

        public class D020133
        {
            [System.Xml.Serialization.XmlElement("KASHIDAOREZEINUKI8K")]
            public String KASHIDAOREZEINUKI8K { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREZEIGAKU8K")]
            public String KASHIDAOREZEIGAKU8K { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREZEIKOMI8K")]
            public String KASHIDAOREZEIKOMI8K { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEINUKI8K")]
            public String KASHIDAOREKAISHUZEINUKI8K { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEIGAKU8K")]
            public String KASHIDAOREKAISHUZEIGAKU8K { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEIKOMI8K")]
            public String KASHIDAOREKAISHUZEIKOMI8K { get; set; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public D020133()
            {
                KASHIDAOREZEINUKI8K = String.Empty;
                KASHIDAOREZEIGAKU8K = String.Empty;
                KASHIDAOREZEIKOMI8K = String.Empty;
                KASHIDAOREKAISHUZEINUKI8K = String.Empty;
                KASHIDAOREKAISHUZEIGAKU8K = String.Empty;
                KASHIDAOREKAISHUZEIKOMI8K = String.Empty;
            }
        }

        public class D020130
        {
            [System.Xml.Serialization.XmlElement("KASHIDAOREZEINUKI8")]
            public String KASHIDAOREZEINUKI8 { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREZEIGAKU8")]
            public String KASHIDAOREZEIGAKU8 { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREZEIKOMI8")]
            public String KASHIDAOREZEIKOMI8 { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEINUKI8")]
            public String KASHIDAOREKAISHUZEINUKI8 { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEIGAKU8")]
            public String KASHIDAOREKAISHUZEIGAKU8 { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEIKOMI8")]
            public String KASHIDAOREKAISHUZEIKOMI8 { get; set; }


            /// <summary>
            /// コンストラクタ
            /// </summary>
            public D020130()
            {
                KASHIDAOREZEINUKI8 = String.Empty;
                KASHIDAOREZEIGAKU8 = String.Empty;
                KASHIDAOREZEIKOMI8 = String.Empty;
                KASHIDAOREKAISHUZEINUKI8 = String.Empty;
                KASHIDAOREKAISHUZEIGAKU8 = String.Empty;
                KASHIDAOREKAISHUZEIKOMI8 = String.Empty;
            }
        }

        public class D020131
        {
            [System.Xml.Serialization.XmlElement("KASHIDAOREZEINUKI5")]
            public String KASHIDAOREZEINUKI5 { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREZEIGAKU5")]
            public String KASHIDAOREZEIGAKU5 { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREZEIKOMI5")]
            public String KASHIDAOREZEIKOMI5 { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEINUKI5")]
            public String KASHIDAOREKAISHUZEINUKI5 { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEIGAKU5")]
            public String KASHIDAOREKAISHUZEIGAKU5 { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEIKOMI5")]
            public String KASHIDAOREKAISHUZEIKOMI5 { get; set; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public D020131()
            {
                KASHIDAOREZEINUKI5 = String.Empty;
                KASHIDAOREZEIGAKU5 = String.Empty;
                KASHIDAOREZEIKOMI5 = String.Empty;
                KASHIDAOREKAISHUZEINUKI5 = String.Empty;
                KASHIDAOREKAISHUZEIGAKU5 = String.Empty;
                KASHIDAOREKAISHUZEIKOMI5 = String.Empty;
            }
        }

        public class D020132
        {
            [System.Xml.Serialization.XmlElement("KASHIDAOREZEINUKI3")]
            public String KASHIDAOREZEINUKI3 { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREZEIGAKU3")]
            public String KASHIDAOREZEIGAKU3 { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREZEIKOMI3")]
            public String KASHIDAOREZEIKOMI3 { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEINUKI3")]
            public String KASHIDAOREKAISHUZEINUKI3 { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEIGAKU3")]
            public String KASHIDAOREKAISHUZEIGAKU3 { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEIKOMI3")]
            public String KASHIDAOREKAISHUZEIKOMI3 { get; set; }


            /// <summary>
            /// コンストラクタ
            /// </summary>
            public D020132()
            {
                KASHIDAOREZEINUKI3 = String.Empty;
                KASHIDAOREZEIGAKU3 = String.Empty;
                KASHIDAOREZEIKOMI3 = String.Empty;
                KASHIDAOREKAISHUZEINUKI3 = String.Empty;
                KASHIDAOREKAISHUZEIGAKU3 = String.Empty;
                KASHIDAOREKAISHUZEIKOMI3 = String.Empty;
            }
        }

        public class D02204
        {
            [System.Xml.Serialization.XmlElement("GIMUMENJOKAZEI10")]
            public String GIMUMENJOKAZEI10 { get; set; }

            [System.Xml.Serialization.XmlElement("GIMUMENJOHIKAZEI10")]
            public String GIMUMENJOHIKAZEI10 { get; set; }

            [System.Xml.Serialization.XmlElement("GIMUMENJOKYOTUU10")]
            public String GIMUMENJOKYOTUU10 { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEWARIAIHENDO10")]
            public String KAZEIURIAGEWARIAIHENDO10 { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIGYOMUYOTENYO10")]
            public String KAZEIGYOMUYOTENYO10 { get; set; }

            //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↓
            [System.Xml.Serialization.XmlElement("KYOJUYOCHINTAI10")]
            public String KYOJUYOCHINTAI10 { get; set; }
            //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↑

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public D02204()
            {
                GIMUMENJOKAZEI10 = String.Empty;
                GIMUMENJOHIKAZEI10 = String.Empty;
                GIMUMENJOKYOTUU10 = String.Empty;
                KAZEIURIAGEWARIAIHENDO10 = String.Empty;
                KAZEIGYOMUYOTENYO10 = String.Empty;
                //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↓
                KYOJUYOCHINTAI10 = String.Empty;
                //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↑
            }
        }

        public class D02203
        {
            [System.Xml.Serialization.XmlElement("GIMUMENJOKAZEI8K")]
            public String GIMUMENJOKAZEI8K { get; set; }

            [System.Xml.Serialization.XmlElement("GIMUMENJOHIKAZEI8K")]
            public String GIMUMENJOHIKAZEI8K { get; set; }

            [System.Xml.Serialization.XmlElement("GIMUMENJOKYOTUU8K")]
            public String GIMUMENJOKYOTUU8K { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEWARIAIHENDO8K")]
            public String KAZEIURIAGEWARIAIHENDO8K { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIGYOMUYOTENYO8K")]
            public String KAZEIGYOMUYOTENYO8K { get; set; }

            //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↓
            [System.Xml.Serialization.XmlElement("KYOJUYOCHINTAI8K")]
            public String KYOJUYOCHINTAI8K { get; set; }
            //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↑

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public D02203()
            {
                GIMUMENJOKAZEI8K = String.Empty;
                GIMUMENJOHIKAZEI8K = String.Empty;
                GIMUMENJOKYOTUU8K = String.Empty;
                KAZEIURIAGEWARIAIHENDO8K = String.Empty;
                KAZEIGYOMUYOTENYO8K = String.Empty;
                //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↓
                KYOJUYOCHINTAI8K = String.Empty;
                //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↑
            }
        }

        public class D02200
        {
            [System.Xml.Serialization.XmlElement("GIMUMENJOKAZEI8")]
            public String GIMUMENJOKAZEI8 { get; set; }

            [System.Xml.Serialization.XmlElement("GIMUMENJOHIKAZEI8")]
            public String GIMUMENJOHIKAZEI8 { get; set; }

            [System.Xml.Serialization.XmlElement("GIMUMENJOKYOTUU8")]
            public String GIMUMENJOKYOTUU8 { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEWARIAIHENDO8")]
            public String KAZEIURIAGEWARIAIHENDO8 { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIGYOMUYOTENYO8")]
            public String KAZEIGYOMUYOTENYO8 { get; set; }

            //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↓
            [System.Xml.Serialization.XmlElement("KYOJUYOCHINTAI8")]
            public String KYOJUYOCHINTAI8 { get; set; }
            //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↑
            /// <summary>
            /// コンストラクタ
            /// </summary>
            public D02200()
            {
                GIMUMENJOKAZEI8 = String.Empty;
                GIMUMENJOHIKAZEI8 = String.Empty;
                GIMUMENJOKYOTUU8 = String.Empty;
                KAZEIURIAGEWARIAIHENDO8 = String.Empty;
                KAZEIGYOMUYOTENYO8 = String.Empty;
                //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↓
                KYOJUYOCHINTAI8 = String.Empty;
                //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↑
            }
        }

        public class D02201
        {
            [System.Xml.Serialization.XmlElement("GIMUMENJOKAZEI5")]
            public String GIMUMENJOKAZEI5 { get; set; }

            [System.Xml.Serialization.XmlElement("GIMUMENJOHIKAZEI5")]
            public String GIMUMENJOHIKAZEI5 { get; set; }

            [System.Xml.Serialization.XmlElement("GIMUMENJOKYOTUU5")]
            public String GIMUMENJOKYOTUU5 { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEWARIAIHENDO5")]
            public String KAZEIURIAGEWARIAIHENDO5 { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIGYOMUYOTENYO5")]
            public String KAZEIGYOMUYOTENYO5 { get; set; }

            //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↓
            [System.Xml.Serialization.XmlElement("KYOJUYOCHINTAI5")]
            public String KYOJUYOCHINTAI5 { get; set; }
            //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↑
            /// <summary>
            /// コンストラクタ
            /// </summary>
            public D02201()
            {
                GIMUMENJOKAZEI5 = String.Empty;
                GIMUMENJOHIKAZEI5 = String.Empty;
                GIMUMENJOKYOTUU5 = String.Empty;
                KAZEIURIAGEWARIAIHENDO5 = String.Empty;
                KAZEIGYOMUYOTENYO5 = String.Empty;
                //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↓
                KYOJUYOCHINTAI5 = String.Empty;
                //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↑
            }
        }

        public class D02202
        {
            [System.Xml.Serialization.XmlElement("GIMUMENJOKAZEI3")]
            public String GIMUMENJOKAZEI3 { get; set; }

            [System.Xml.Serialization.XmlElement("GIMUMENJOHIKAZEI3")]
            public String GIMUMENJOHIKAZEI3 { get; set; }

            [System.Xml.Serialization.XmlElement("GIMUMENJOKYOTUU3")]
            public String GIMUMENJOKYOTUU3 { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEWARIAIHENDO3")]
            public String KAZEIURIAGEWARIAIHENDO3 { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIGYOMUYOTENYO3")]
            public String KAZEIGYOMUYOTENYO3 { get; set; }

            //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↓
            [System.Xml.Serialization.XmlElement("KYOJUYOCHINTAI3")]
            public String KYOJUYOCHINTAI3 { get; set; }
            //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↑
            /// <summary>
            /// コンストラクタ
            /// </summary>
            public D02202()
            {
                GIMUMENJOKAZEI3 = String.Empty;
                GIMUMENJOHIKAZEI3 = String.Empty;
                GIMUMENJOKYOTUU3 = String.Empty;
                KAZEIURIAGEWARIAIHENDO3 = String.Empty;
                KAZEIGYOMUYOTENYO3 = String.Empty;
                //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↓
                KYOJUYOCHINTAI3 = String.Empty;
                //2021/02/15    K.Matsumoto     (184646, 184647)        R3年度消費税制度改正対応       ↑
            }
        }
    }
}

