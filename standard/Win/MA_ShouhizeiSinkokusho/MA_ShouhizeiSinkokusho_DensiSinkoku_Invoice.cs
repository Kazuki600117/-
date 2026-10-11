//
//  2023/07/24  新規作成    (221366) Y.Nakajima
//

using System;

namespace Obic7fx.Win.M0000.Print.MA_ShouhizeiSinkokusho
{
    public class MA_ShouhizeiSinkokusho_DensiSinkoku_Invoice
    {
        public MA_ShouhizeiSinkokusho_DensiSinkoku_Invoice()
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

            [System.Xml.Serialization.XmlElement("NIWARITOKUREI")]
            public String NIWARITOKUREI { get; set; }

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
                NIWARITOKUREI = String.Empty;
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
            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEINUKI10_TSUMIAGE")]
            public String KAZEIURIAGEZEINUKI10_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEIGAKU10_TSUMIAGE")]
            public String KAZEIURIAGEZEIGAKU10_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEIKOMI10_TSUMIAGE")]
            public String KAZEIURIAGEZEIKOMI10_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEINUKI10_WARIMODOSHI")]
            public String KAZEIURIAGEZEINUKI10_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEIGAKU10_WARIMODOSHI")]
            public String KAZEIURIAGEZEIGAKU10_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEIKOMI10_WARIMODOSHI")]
            public String KAZEIURIAGEZEIKOMI10_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("MENZEIURIAGEZEINUKI10")]
            public String MENZEIURIAGEZEINUKI10 { get; set; }

            [System.Xml.Serialization.XmlElement("HIKAZEIURIAGEYUUKAZEINUKI10")]
            public String HIKAZEIURIAGEYUUKAZEINUKI10 { get; set; }

            [System.Xml.Serialization.XmlElement("HIKAZEIURIAGEYUUKAIGAIZEINUKI10")]
            public String HIKAZEIURIAGEYUUKAIGAIZEINUKI10 { get; set; }

            [System.Xml.Serialization.XmlElement("HIKAZEISHISANYUSHUTSUZEINUKI10")]
            public String HIKAZEISHISANYUSHUTSUZEINUKI10 { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEINUKI10_TSUMIAGE")]
            public String KAZEIURIAGEHENKANZEINUKI10_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEIGAKU10_TSUMIAGE")]
            public String KAZEIURIAGEHENKANZEIGAKU10_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEIKOMI10_TSUMIAGE")]
            public String KAZEIURIAGEHENKANZEIKOMI10_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEINUKI10_WARIMODOSHI")]
            public String KAZEIURIAGEHENKANZEINUKI10_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEIGAKU10_WARIMODOSHI")]
            public String KAZEIURIAGEHENKANZEIGAKU10_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEIKOMI10_WARIMODOSHI")]
            public String KAZEIURIAGEHENKANZEIKOMI10_WARIMODOSHI { get; set; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public D020114()
            {
                KAZEIURIAGEZEINUKI10_TSUMIAGE = String.Empty;
                KAZEIURIAGEZEIGAKU10_TSUMIAGE = String.Empty;
                KAZEIURIAGEZEIKOMI10_TSUMIAGE = String.Empty;
                KAZEIURIAGEZEINUKI10_WARIMODOSHI = String.Empty;
                KAZEIURIAGEZEIGAKU10_WARIMODOSHI = String.Empty;
                KAZEIURIAGEZEIKOMI10_WARIMODOSHI = String.Empty;
                MENZEIURIAGEZEINUKI10 = String.Empty;
                HIKAZEIURIAGEYUUKAZEINUKI10 = String.Empty;
                HIKAZEIURIAGEYUUKAIGAIZEINUKI10 = String.Empty;
                HIKAZEISHISANYUSHUTSUZEINUKI10 = String.Empty;
                KAZEIURIAGEHENKANZEINUKI10_TSUMIAGE = String.Empty;
                KAZEIURIAGEHENKANZEIGAKU10_TSUMIAGE = String.Empty;
                KAZEIURIAGEHENKANZEIKOMI10_TSUMIAGE = String.Empty;
                KAZEIURIAGEHENKANZEINUKI10_WARIMODOSHI = String.Empty;
                KAZEIURIAGEHENKANZEIGAKU10_WARIMODOSHI = String.Empty;
                KAZEIURIAGEHENKANZEIKOMI10_WARIMODOSHI = String.Empty;
            }
        }

        public class D020113
        {
            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEINUKI8K_TSUMIAGE")]
            public String KAZEIURIAGEZEINUKI8K_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEIGAKU8K_TSUMIAGE")]
            public String KAZEIURIAGEZEIGAKU8K_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEIKOMI8K_TSUMIAGE")]
            public String KAZEIURIAGEZEIKOMI8K_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEINUKI8K_WARIMODOSHI")]
            public String KAZEIURIAGEZEINUKI8K_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEIGAKU8K_WARIMODOSHI")]
            public String KAZEIURIAGEZEIGAKU8K_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEIKOMI8K_WARIMODOSHI")]
            public String KAZEIURIAGEZEIKOMI8K_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("MENZEIURIAGEZEINUKI8K")]
            public String MENZEIURIAGEZEINUKI8K { get; set; }

            [System.Xml.Serialization.XmlElement("HIKAZEIURIAGEYUUKAZEINUKI8K")]
            public String HIKAZEIURIAGEYUUKAZEINUKI8K { get; set; }

            [System.Xml.Serialization.XmlElement("HIKAZEIURIAGEYUUKAIGAIZEINUKI8K")]
            public String HIKAZEIURIAGEYUUKAIGAIZEINUKI8K { get; set; }

            [System.Xml.Serialization.XmlElement("HIKAZEISHISANYUSHUTSUZEINUKI8K")]
            public String HIKAZEISHISANYUSHUTSUZEINUKI8K { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEINUKI8K_TSUMIAGE")]
            public String KAZEIURIAGEHENKANZEINUKI8K_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEIGAKU8K_TSUMIAGE")]
            public String KAZEIURIAGEHENKANZEIGAKU8K_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEIKOMI8K_TSUMIAGE")]
            public String KAZEIURIAGEHENKANZEIKOMI8K_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEINUKI8K_WARIMODOSHI")]
            public String KAZEIURIAGEHENKANZEINUKI8K_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEIGAKU8K_WARIMODOSHI")]
            public String KAZEIURIAGEHENKANZEIGAKU8K_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEIKOMI8K_WARIMODOSHI")]
            public String KAZEIURIAGEHENKANZEIKOMI8K_WARIMODOSHI { get; set; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public D020113()
            {
                KAZEIURIAGEZEINUKI8K_TSUMIAGE = String.Empty;
                KAZEIURIAGEZEIGAKU8K_TSUMIAGE = String.Empty;
                KAZEIURIAGEZEIKOMI8K_TSUMIAGE = String.Empty;
                KAZEIURIAGEZEINUKI8K_WARIMODOSHI = String.Empty;
                KAZEIURIAGEZEIGAKU8K_WARIMODOSHI = String.Empty;
                KAZEIURIAGEZEIKOMI8K_WARIMODOSHI = String.Empty;
                MENZEIURIAGEZEINUKI8K = String.Empty;
                HIKAZEIURIAGEYUUKAZEINUKI8K = String.Empty;
                HIKAZEIURIAGEYUUKAIGAIZEINUKI8K = String.Empty;
                HIKAZEISHISANYUSHUTSUZEINUKI8K = String.Empty;
                KAZEIURIAGEHENKANZEINUKI8K_TSUMIAGE = String.Empty;
                KAZEIURIAGEHENKANZEIGAKU8K_TSUMIAGE = String.Empty;
                KAZEIURIAGEHENKANZEIKOMI8K_TSUMIAGE = String.Empty;
                KAZEIURIAGEHENKANZEINUKI8K_WARIMODOSHI = String.Empty;
                KAZEIURIAGEHENKANZEIGAKU8K_WARIMODOSHI = String.Empty;
                KAZEIURIAGEHENKANZEIKOMI8K_WARIMODOSHI = String.Empty;
            }
        }

        public class D020110
        {
            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEINUKI8_TSUMIAGE")]
            public String KAZEIURIAGEZEINUKI8_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEIGAKU8_TSUMIAGE")]
            public String KAZEIURIAGEZEIGAKU8_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEIKOMI8_TSUMIAGE")]
            public String KAZEIURIAGEZEIKOMI8_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEINUKI8_WARIMODOSHI")]
            public String KAZEIURIAGEZEINUKI8_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEIGAKU8_WARIMODOSHI")]
            public String KAZEIURIAGEZEIGAKU8_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEIKOMI8_WARIMODOSHI")]
            public String KAZEIURIAGEZEIKOMI8_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("MENZEIURIAGEZEINUKI8")]
            public String MENZEIURIAGEZEINUKI8 { get; set; }

            [System.Xml.Serialization.XmlElement("HIKAZEIURIAGEYUUKAZEINUKI8")]
            public String HIKAZEIURIAGEYUUKAZEINUKI8 { get; set; }

            [System.Xml.Serialization.XmlElement("HIKAZEIURIAGEYUUKAIGAIZEINUKI8")]
            public String HIKAZEIURIAGEYUUKAIGAIZEINUKI8 { get; set; }

            [System.Xml.Serialization.XmlElement("HIKAZEISHISANYUSHUTSUZEINUKI8")]
            public String HIKAZEISHISANYUSHUTSUZEINUKI8 { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEINUKI8_TSUMIAGE")]
            public String KAZEIURIAGEHENKANZEINUKI8_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEIGAKU8_TSUMIAGE")]
            public String KAZEIURIAGEHENKANZEIGAKU8_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEIKOMI8_TSUMIAGE")]
            public String KAZEIURIAGEHENKANZEIKOMI8_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEINUKI8_WARIMODOSHI")]
            public String KAZEIURIAGEHENKANZEINUKI8_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEIGAKU8_WARIMODOSHI")]
            public String KAZEIURIAGEHENKANZEIGAKU8_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEIKOMI8_WARIMODOSHI")]
            public String KAZEIURIAGEHENKANZEIKOMI8_WARIMODOSHI { get; set; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public D020110()
            {
                KAZEIURIAGEZEINUKI8_TSUMIAGE = String.Empty;
                KAZEIURIAGEZEIGAKU8_TSUMIAGE = String.Empty;
                KAZEIURIAGEZEIKOMI8_TSUMIAGE = String.Empty;
                KAZEIURIAGEZEINUKI8_WARIMODOSHI = String.Empty;
                KAZEIURIAGEZEIGAKU8_WARIMODOSHI = String.Empty;
                KAZEIURIAGEZEIKOMI8_WARIMODOSHI = String.Empty;
                MENZEIURIAGEZEINUKI8 = String.Empty;
                HIKAZEIURIAGEYUUKAZEINUKI8 = String.Empty;
                HIKAZEIURIAGEYUUKAIGAIZEINUKI8 = String.Empty;
                HIKAZEISHISANYUSHUTSUZEINUKI8 = String.Empty;
                KAZEIURIAGEHENKANZEINUKI8_TSUMIAGE = String.Empty;
                KAZEIURIAGEHENKANZEIGAKU8_TSUMIAGE = String.Empty;
                KAZEIURIAGEHENKANZEIKOMI8_TSUMIAGE = String.Empty;
                KAZEIURIAGEHENKANZEINUKI8_WARIMODOSHI = String.Empty;
                KAZEIURIAGEHENKANZEIGAKU8_WARIMODOSHI = String.Empty;
                KAZEIURIAGEHENKANZEIKOMI8_WARIMODOSHI = String.Empty;
            }
        }

        public class D020111
        {
            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEINUKI5_TSUMIAGE")]
            public String KAZEIURIAGEZEINUKI5_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEIGAKU5_TSUMIAGE")]
            public String KAZEIURIAGEZEIGAKU5_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEIKOMI5_TSUMIAGE")]
            public String KAZEIURIAGEZEIKOMI5_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEINUKI5_WARIMODOSHI")]
            public String KAZEIURIAGEZEINUKI5_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEIGAKU5_WARIMODOSHI")]
            public String KAZEIURIAGEZEIGAKU5_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEIKOMI5_WARIMODOSHI")]
            public String KAZEIURIAGEZEIKOMI5_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("MENZEIURIAGEZEINUKI5")]
            public String MENZEIURIAGEZEINUKI5 { get; set; }

            [System.Xml.Serialization.XmlElement("HIKAZEIURIAGEYUUKAZEINUKI5")]
            public String HIKAZEIURIAGEYUUKAZEINUKI5 { get; set; }

            [System.Xml.Serialization.XmlElement("HIKAZEIURIAGEYUUKAIGAIZEINUKI5")]
            public String HIKAZEIURIAGEYUUKAIGAIZEINUKI5 { get; set; }

            [System.Xml.Serialization.XmlElement("HIKAZEISHISANYUSHUTSUZEINUKI5")]
            public String HIKAZEISHISANYUSHUTSUZEINUKI5 { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEINUKI5_TSUMIAGE")]
            public String KAZEIURIAGEHENKANZEINUKI5_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEIGAKU5_TSUMIAGE")]
            public String KAZEIURIAGEHENKANZEIGAKU5_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEIKOMI5_TSUMIAGE")]
            public String KAZEIURIAGEHENKANZEIKOMI5_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEINUKI5_WARIMODOSHI")]
            public String KAZEIURIAGEHENKANZEINUKI5_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEIGAKU5_WARIMODOSHI")]
            public String KAZEIURIAGEHENKANZEIGAKU5_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEIKOMI5_WARIMODOSHI")]
            public String KAZEIURIAGEHENKANZEIKOMI5_WARIMODOSHI { get; set; }
            /// <summary>
            /// コンストラクタ
            /// </summary>
            public D020111()
            {
                KAZEIURIAGEZEINUKI5_TSUMIAGE = String.Empty;
                KAZEIURIAGEZEIGAKU5_TSUMIAGE = String.Empty;
                KAZEIURIAGEZEIKOMI5_TSUMIAGE = String.Empty;
                KAZEIURIAGEZEINUKI5_WARIMODOSHI = String.Empty;
                KAZEIURIAGEZEIGAKU5_WARIMODOSHI = String.Empty;
                KAZEIURIAGEZEIKOMI5_WARIMODOSHI = String.Empty;
                MENZEIURIAGEZEINUKI5 = String.Empty;
                HIKAZEIURIAGEYUUKAZEINUKI5 = String.Empty;
                HIKAZEIURIAGEYUUKAIGAIZEINUKI5 = String.Empty;
                HIKAZEISHISANYUSHUTSUZEINUKI5 = String.Empty;
                KAZEIURIAGEHENKANZEINUKI5_TSUMIAGE = String.Empty;
                KAZEIURIAGEHENKANZEIGAKU5_TSUMIAGE = String.Empty;
                KAZEIURIAGEHENKANZEIKOMI5_TSUMIAGE = String.Empty;
                KAZEIURIAGEHENKANZEINUKI5_WARIMODOSHI = String.Empty;
                KAZEIURIAGEHENKANZEIGAKU5_WARIMODOSHI = String.Empty;
                KAZEIURIAGEHENKANZEIKOMI5_WARIMODOSHI = String.Empty;
            }
        }

        public class D020112
        {
            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEINUKI3_TSUMIAGE")]
            public String KAZEIURIAGEZEINUKI3_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEIGAKU3_TSUMIAGE")]
            public String KAZEIURIAGEZEIGAKU3_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEIKOMI3_TSUMIAGE")]
            public String KAZEIURIAGEZEIKOMI3_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEINUKI3_WARIMODOSHI")]
            public String KAZEIURIAGEZEINUKI3_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEIGAKU3_WARIMODOSHI")]
            public String KAZEIURIAGEZEIGAKU3_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEZEIKOMI3_WARIMODOSHI")]
            public String KAZEIURIAGEZEIKOMI3_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("MENZEIURIAGEZEINUKI3")]
            public String MENZEIURIAGEZEINUKI3 { get; set; }

            [System.Xml.Serialization.XmlElement("HIKAZEIURIAGEYUUKAZEINUKI3")]
            public String HIKAZEIURIAGEYUUKAZEINUKI3 { get; set; }

            [System.Xml.Serialization.XmlElement("HIKAZEIURIAGEYUUKAIGAIZEINUKI3")]
            public String HIKAZEIURIAGEYUUKAIGAIZEINUKI3 { get; set; }

            [System.Xml.Serialization.XmlElement("HIKAZEISHISANYUSHUTSUZEINUKI3")]
            public String HIKAZEISHISANYUSHUTSUZEINUKI3 { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEINUKI3_TSUMIAGE")]
            public String KAZEIURIAGEHENKANZEINUKI3_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEIGAKU3_TSUMIAGE")]
            public String KAZEIURIAGEHENKANZEIGAKU3_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEIKOMI3_TSUMIAGE")]
            public String KAZEIURIAGEHENKANZEIKOMI3_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEINUKI3_WARIMODOSHI")]
            public String KAZEIURIAGEHENKANZEINUKI3_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEIGAKU3_WARIMODOSHI")]
            public String KAZEIURIAGEHENKANZEIGAKU3_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KAZEIURIAGEHENKANZEIKOMI3_WARIMODOSHI")]
            public String KAZEIURIAGEHENKANZEIKOMI3_WARIMODOSHI { get; set; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public D020112()
            {
                KAZEIURIAGEZEINUKI3_TSUMIAGE = String.Empty;
                KAZEIURIAGEZEIGAKU3_TSUMIAGE = String.Empty;
                KAZEIURIAGEZEIKOMI3_TSUMIAGE = String.Empty;
                KAZEIURIAGEZEINUKI3_WARIMODOSHI = String.Empty;
                KAZEIURIAGEZEIGAKU3_WARIMODOSHI = String.Empty;
                KAZEIURIAGEZEIKOMI3_WARIMODOSHI = String.Empty;
                MENZEIURIAGEZEINUKI3 = String.Empty;
                HIKAZEIURIAGEYUUKAZEINUKI3 = String.Empty;
                HIKAZEIURIAGEYUUKAIGAIZEINUKI3 = String.Empty;
                HIKAZEISHISANYUSHUTSUZEINUKI3 = String.Empty;
                KAZEIURIAGEHENKANZEINUKI3_TSUMIAGE = String.Empty;
                KAZEIURIAGEHENKANZEIGAKU3_TSUMIAGE = String.Empty;
                KAZEIURIAGEHENKANZEIKOMI3_TSUMIAGE = String.Empty;
                KAZEIURIAGEHENKANZEINUKI3_WARIMODOSHI = String.Empty;
                KAZEIURIAGEHENKANZEIGAKU3_WARIMODOSHI = String.Empty;
                KAZEIURIAGEHENKANZEIKOMI3_WARIMODOSHI = String.Empty;
            }
        }

        public class D020124
        {
            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEINUKI10_TSUMIAGE")]
            public String SHIIREKAZEIZEINUKI10_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIGAKU10_TSUMIAGE")]
            public String SHIIREKAZEIZEIGAKU10_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIKOMI10_TSUMIAGE")]
            public String SHIIREKAZEIZEIKOMI10_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEINUKI10_TSUMIAGE")]
            public String SHIIREHIKAZEIZEINUKI10_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIGAKU10_TSUMIAGE")]
            public String SHIIREHIKAZEIZEIGAKU10_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIKOMI10_TSUMIAGE")]
            public String SHIIREHIKAZEIZEIKOMI10_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEINUKI10_TSUMIAGE")]
            public String SHIIREKYOTUUZEINUKI10_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIGAKU10_TSUMIAGE")]
            public String SHIIREKYOTUUZEIGAKU10_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIKOMI10_TSUMIAGE")]
            public String SHIIREKYOTUUZEIKOMI10_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEINUKI10_WARIMODOSHI")]
            public String SHIIREKAZEIZEINUKI10_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIGAKU10_WARIMODOSHI")]
            public String SHIIREKAZEIZEIGAKU10_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIKOMI10_WARIMODOSHI")]
            public String SHIIREKAZEIZEIKOMI10_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEINUKI10_WARIMODOSHI")]
            public String SHIIREHIKAZEIZEINUKI10_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIGAKU10_WARIMODOSHI")]
            public String SHIIREHIKAZEIZEIGAKU10_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIKOMI10_WARIMODOSHI")]
            public String SHIIREHIKAZEIZEIKOMI10_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEINUKI10_WARIMODOSHI")]
            public String SHIIREKYOTUUZEINUKI10_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIGAKU10_WARIMODOSHI")]
            public String SHIIREKYOTUUZEIGAKU10_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIKOMI10_WARIMODOSHI")]
            public String SHIIREKYOTUUZEIKOMI10_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIGAKU10_INVOICEIGAI_TSUMIAGE_KANBEN")]
            public String SHIIREKAZEIZEIGAKU10_INVOICEIGAI_TSUMIAGE_KANBEN { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIKOMI10_INVOICEIGAI_TSUMIAGE_KANBEN")]
            public String SHIIREKAZEIZEIKOMI10_INVOICEIGAI_TSUMIAGE_KANBEN { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIGAKU10_INVOICEIGAI_TSUMIAGE_KANBEN")]
            public String SHIIREHIKAZEIZEIGAKU10_INVOICEIGAI_TSUMIAGE_KANBEN { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIKOMI10_INVOICEIGAI_TSUMIAGE_KANBEN")]
            public String SHIIREHIKAZEIZEIKOMI10_INVOICEIGAI_TSUMIAGE_KANBEN { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIGAKU10_INVOICEIGAI_TSUMIAGE_KANBEN")]
            public String SHIIREKYOTUUZEIGAKU10_INVOICEIGAI_TSUMIAGE_KANBEN { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIKOMI10_INVOICEIGAI_TSUMIAGE_KANBEN")]
            public String SHIIREKYOTUUZEIKOMI10_INVOICEIGAI_TSUMIAGE_KANBEN { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEINUKI10_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREKAZEIZEINUKI10_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIGAKU10_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREKAZEIZEIGAKU10_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIKOMI10_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREKAZEIZEIKOMI10_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEINUKI10_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREHIKAZEIZEINUKI10_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIGAKU10_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREHIKAZEIZEIGAKU10_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIKOMI10_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREHIKAZEIZEIKOMI10_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEINUKI10_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREKYOTUUZEINUKI10_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIGAKU10_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREKYOTUUZEIGAKU10_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIKOMI10_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREKYOTUUZEIKOMI10_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEINUKI10_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREKAZEIZEINUKI10_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIGAKU10_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREKAZEIZEIGAKU10_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIKOMI10_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREKAZEIZEIKOMI10_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEINUKI10_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREHIKAZEIZEINUKI10_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIGAKU10_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREHIKAZEIZEIGAKU10_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIKOMI10_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREHIKAZEIZEIKOMI10_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEINUKI10_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREKYOTUUZEINUKI10_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIGAKU10_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREKYOTUUZEIGAKU10_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIKOMI10_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREKYOTUUZEIKOMI10_INVOICEIGAI_WARIMODOSHI50 { get; set; }

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
                SHIIREKAZEIZEINUKI10_TSUMIAGE = String.Empty;
                SHIIREKAZEIZEIGAKU10_TSUMIAGE = String.Empty;
                SHIIREKAZEIZEIKOMI10_TSUMIAGE = String.Empty;
                SHIIREHIKAZEIZEINUKI10_TSUMIAGE = String.Empty;
                SHIIREHIKAZEIZEIGAKU10_TSUMIAGE = String.Empty;
                SHIIREHIKAZEIZEIKOMI10_TSUMIAGE = String.Empty;
                SHIIREKYOTUUZEINUKI10_TSUMIAGE = String.Empty;
                SHIIREKYOTUUZEIGAKU10_TSUMIAGE = String.Empty;
                SHIIREKYOTUUZEIKOMI10_TSUMIAGE = String.Empty;
                SHIIREKAZEIZEINUKI10_WARIMODOSHI = String.Empty;
                SHIIREKAZEIZEIGAKU10_WARIMODOSHI = String.Empty;
                SHIIREKAZEIZEIKOMI10_WARIMODOSHI = String.Empty;
                SHIIREHIKAZEIZEINUKI10_WARIMODOSHI = String.Empty;
                SHIIREHIKAZEIZEIGAKU10_WARIMODOSHI = String.Empty;
                SHIIREHIKAZEIZEIKOMI10_WARIMODOSHI = String.Empty;
                SHIIREKYOTUUZEINUKI10_WARIMODOSHI = String.Empty;
                SHIIREKYOTUUZEIGAKU10_WARIMODOSHI = String.Empty;
                SHIIREKYOTUUZEIKOMI10_WARIMODOSHI = String.Empty;
                SHIIREKAZEIZEIGAKU10_INVOICEIGAI_TSUMIAGE_KANBEN = String.Empty;
                SHIIREKAZEIZEIKOMI10_INVOICEIGAI_TSUMIAGE_KANBEN = String.Empty;
                SHIIREHIKAZEIZEIGAKU10_INVOICEIGAI_TSUMIAGE_KANBEN = String.Empty;
                SHIIREHIKAZEIZEIKOMI10_INVOICEIGAI_TSUMIAGE_KANBEN = String.Empty;
                SHIIREKYOTUUZEIGAKU10_INVOICEIGAI_TSUMIAGE_KANBEN = String.Empty;
                SHIIREKYOTUUZEIKOMI10_INVOICEIGAI_TSUMIAGE_KANBEN = String.Empty;
                SHIIREKAZEIZEINUKI10_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREKAZEIZEIGAKU10_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREKAZEIZEIKOMI10_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREHIKAZEIZEINUKI10_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREHIKAZEIZEIGAKU10_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREHIKAZEIZEIKOMI10_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREKYOTUUZEINUKI10_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREKYOTUUZEIGAKU10_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREKYOTUUZEIKOMI10_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREKAZEIZEINUKI10_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREKAZEIZEIGAKU10_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREKAZEIZEIKOMI10_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREHIKAZEIZEINUKI10_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREHIKAZEIZEIGAKU10_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREHIKAZEIZEIKOMI10_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREKYOTUUZEINUKI10_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREKYOTUUZEIGAKU10_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREKYOTUUZEIKOMI10_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
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
            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEINUKI8K_TSUMIAGE")]
            public String SHIIREKAZEIZEINUKI8K_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIGAKU8K_TSUMIAGE")]
            public String SHIIREKAZEIZEIGAKU8K_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIKOMI8K_TSUMIAGE")]
            public String SHIIREKAZEIZEIKOMI8K_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEINUKI8K_TSUMIAGE")]
            public String SHIIREHIKAZEIZEINUKI8K_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIGAKU8K_TSUMIAGE")]
            public String SHIIREHIKAZEIZEIGAKU8K_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIKOMI8K_TSUMIAGE")]
            public String SHIIREHIKAZEIZEIKOMI8K_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEINUKI8K_TSUMIAGE")]
            public String SHIIREKYOTUUZEINUKI8K_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIGAKU8K_TSUMIAGE")]
            public String SHIIREKYOTUUZEIGAKU8K_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIKOMI8K_TSUMIAGE")]
            public String SHIIREKYOTUUZEIKOMI8K_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEINUKI8K_WARIMODOSHI")]
            public String SHIIREKAZEIZEINUKI8K_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIGAKU8K_WARIMODOSHI")]
            public String SHIIREKAZEIZEIGAKU8K_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIKOMI8K_WARIMODOSHI")]
            public String SHIIREKAZEIZEIKOMI8K_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEINUKI8K_WARIMODOSHI")]
            public String SHIIREHIKAZEIZEINUKI8K_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIGAKU8K_WARIMODOSHI")]
            public String SHIIREHIKAZEIZEIGAKU8K_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIKOMI8K_WARIMODOSHI")]
            public String SHIIREHIKAZEIZEIKOMI8K_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEINUKI8K_WARIMODOSHI")]
            public String SHIIREKYOTUUZEINUKI8K_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIGAKU8K_WARIMODOSHI")]
            public String SHIIREKYOTUUZEIGAKU8K_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIKOMI8K_WARIMODOSHI")]
            public String SHIIREKYOTUUZEIKOMI8K_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIGAKU8K_INVOICEIGAI_TSUMIAGE_KANBEN")]
            public String SHIIREKAZEIZEIGAKU8K_INVOICEIGAI_TSUMIAGE_KANBEN { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIKOMI8K_INVOICEIGAI_TSUMIAGE_KANBEN")]
            public String SHIIREKAZEIZEIKOMI8K_INVOICEIGAI_TSUMIAGE_KANBEN { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIGAKU8K_INVOICEIGAI_TSUMIAGE_KANBEN")]
            public String SHIIREHIKAZEIZEIGAKU8K_INVOICEIGAI_TSUMIAGE_KANBEN { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIKOMI8K_INVOICEIGAI_TSUMIAGE_KANBEN")]
            public String SHIIREHIKAZEIZEIKOMI8K_INVOICEIGAI_TSUMIAGE_KANBEN { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIGAKU8K_INVOICEIGAI_TSUMIAGE_KANBEN")]
            public String SHIIREKYOTUUZEIGAKU8K_INVOICEIGAI_TSUMIAGE_KANBEN { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIKOMI8K_INVOICEIGAI_TSUMIAGE_KANBEN")]
            public String SHIIREKYOTUUZEIKOMI8K_INVOICEIGAI_TSUMIAGE_KANBEN { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEINUKI8K_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREKAZEIZEINUKI8K_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIGAKU8K_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREKAZEIZEIGAKU8K_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIKOMI8K_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREKAZEIZEIKOMI8K_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEINUKI8K_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREHIKAZEIZEINUKI8K_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIGAKU8K_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREHIKAZEIZEIGAKU8K_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIKOMI8K_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREHIKAZEIZEIKOMI8K_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEINUKI8K_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREKYOTUUZEINUKI8K_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIGAKU8K_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREKYOTUUZEIGAKU8K_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIKOMI8K_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREKYOTUUZEIKOMI8K_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEINUKI8K_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREKAZEIZEINUKI8K_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIGAKU8K_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREKAZEIZEIGAKU8K_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIKOMI8K_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREKAZEIZEIKOMI8K_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEINUKI8K_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREHIKAZEIZEINUKI8K_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIGAKU8K_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREHIKAZEIZEIGAKU8K_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIKOMI8K_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREHIKAZEIZEIKOMI8K_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEINUKI8K_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREKYOTUUZEINUKI8K_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIGAKU8K_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREKYOTUUZEIGAKU8K_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIKOMI8K_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREKYOTUUZEIKOMI8K_INVOICEIGAI_WARIMODOSHI50 { get; set; }

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
                SHIIREKAZEIZEINUKI8K_TSUMIAGE = String.Empty;
                SHIIREKAZEIZEIGAKU8K_TSUMIAGE = String.Empty;
                SHIIREKAZEIZEIKOMI8K_TSUMIAGE = String.Empty;
                SHIIREHIKAZEIZEINUKI8K_TSUMIAGE = String.Empty;
                SHIIREHIKAZEIZEIGAKU8K_TSUMIAGE = String.Empty;
                SHIIREHIKAZEIZEIKOMI8K_TSUMIAGE = String.Empty;
                SHIIREKYOTUUZEINUKI8K_TSUMIAGE = String.Empty;
                SHIIREKYOTUUZEIGAKU8K_TSUMIAGE = String.Empty;
                SHIIREKYOTUUZEIKOMI8K_TSUMIAGE = String.Empty;
                SHIIREKAZEIZEINUKI8K_WARIMODOSHI = String.Empty;
                SHIIREKAZEIZEIGAKU8K_WARIMODOSHI = String.Empty;
                SHIIREKAZEIZEIKOMI8K_WARIMODOSHI = String.Empty;
                SHIIREHIKAZEIZEINUKI8K_WARIMODOSHI = String.Empty;
                SHIIREHIKAZEIZEIGAKU8K_WARIMODOSHI = String.Empty;
                SHIIREHIKAZEIZEIKOMI8K_WARIMODOSHI = String.Empty;
                SHIIREKYOTUUZEINUKI8K_WARIMODOSHI = String.Empty;
                SHIIREKYOTUUZEIGAKU8K_WARIMODOSHI = String.Empty;
                SHIIREKYOTUUZEIKOMI8K_WARIMODOSHI = String.Empty;
                SHIIREKAZEIZEIGAKU8K_INVOICEIGAI_TSUMIAGE_KANBEN = String.Empty;
                SHIIREKAZEIZEIKOMI8K_INVOICEIGAI_TSUMIAGE_KANBEN = String.Empty;
                SHIIREHIKAZEIZEIGAKU8K_INVOICEIGAI_TSUMIAGE_KANBEN = String.Empty;
                SHIIREHIKAZEIZEIKOMI8K_INVOICEIGAI_TSUMIAGE_KANBEN = String.Empty;
                SHIIREKYOTUUZEIGAKU8K_INVOICEIGAI_TSUMIAGE_KANBEN = String.Empty;
                SHIIREKYOTUUZEIKOMI8K_INVOICEIGAI_TSUMIAGE_KANBEN = String.Empty;
                SHIIREKAZEIZEINUKI8K_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREKAZEIZEIGAKU8K_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREKAZEIZEIKOMI8K_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREHIKAZEIZEINUKI8K_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREHIKAZEIZEIGAKU8K_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREHIKAZEIZEIKOMI8K_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREKYOTUUZEINUKI8K_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREKYOTUUZEIGAKU8K_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREKYOTUUZEIKOMI8K_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREKAZEIZEINUKI8K_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREKAZEIZEIGAKU8K_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREKAZEIZEIKOMI8K_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREHIKAZEIZEINUKI8K_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREHIKAZEIZEIGAKU8K_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREHIKAZEIZEIKOMI8K_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREKYOTUUZEINUKI8K_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREKYOTUUZEIGAKU8K_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREKYOTUUZEIKOMI8K_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                YUNYUSHIIREKAZEIZEIGAKU8K = String.Empty;
                YUNYUSHIIREHIKAZEIZEIGAKU8K = String.Empty;
                YUNYUSHIIREKYOTUUZEIGAKU8K = String.Empty;
                YUNYUSHIIRETIHO8K = String.Empty;
            }
        }

        public class D020120
        {
            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEINUKI8_TSUMIAGE")]
            public String SHIIREKAZEIZEINUKI8_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIGAKU8_TSUMIAGE")]
            public String SHIIREKAZEIZEIGAKU8_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIKOMI8_TSUMIAGE")]
            public String SHIIREKAZEIZEIKOMI8_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEINUKI8_TSUMIAGE")]
            public String SHIIREHIKAZEIZEINUKI8_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIGAKU8_TSUMIAGE")]
            public String SHIIREHIKAZEIZEIGAKU8_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIKOMI8_TSUMIAGE")]
            public String SHIIREHIKAZEIZEIKOMI8_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEINUKI8_TSUMIAGE")]
            public String SHIIREKYOTUUZEINUKI8_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIGAKU8_TSUMIAGE")]
            public String SHIIREKYOTUUZEIGAKU8_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIKOMI8_TSUMIAGE")]
            public String SHIIREKYOTUUZEIKOMI8_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEINUKI8_WARIMODOSHI")]
            public String SHIIREKAZEIZEINUKI8_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIGAKU8_WARIMODOSHI")]
            public String SHIIREKAZEIZEIGAKU8_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIKOMI8_WARIMODOSHI")]
            public String SHIIREKAZEIZEIKOMI8_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEINUKI8_WARIMODOSHI")]
            public String SHIIREHIKAZEIZEINUKI8_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIGAKU8_WARIMODOSHI")]
            public String SHIIREHIKAZEIZEIGAKU8_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIKOMI8_WARIMODOSHI")]
            public String SHIIREHIKAZEIZEIKOMI8_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEINUKI8_WARIMODOSHI")]
            public String SHIIREKYOTUUZEINUKI8_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIGAKU8_WARIMODOSHI")]
            public String SHIIREKYOTUUZEIGAKU8_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIKOMI8_WARIMODOSHI")]
            public String SHIIREKYOTUUZEIKOMI8_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIGAKU8_INVOICEIGAI_TSUMIAGE_KANBEN")]
            public String SHIIREKAZEIZEIGAKU8_INVOICEIGAI_TSUMIAGE_KANBEN { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIKOMI8_INVOICEIGAI_TSUMIAGE_KANBEN")]
            public String SHIIREKAZEIZEIKOMI8_INVOICEIGAI_TSUMIAGE_KANBEN { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIGAKU8_INVOICEIGAI_TSUMIAGE_KANBEN")]
            public String SHIIREHIKAZEIZEIGAKU8_INVOICEIGAI_TSUMIAGE_KANBEN { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIKOMI8_INVOICEIGAI_TSUMIAGE_KANBEN")]
            public String SHIIREHIKAZEIZEIKOMI8_INVOICEIGAI_TSUMIAGE_KANBEN { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIGAKU8_INVOICEIGAI_TSUMIAGE_KANBEN")]
            public String SHIIREKYOTUUZEIGAKU8_INVOICEIGAI_TSUMIAGE_KANBEN { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIKOMI8_INVOICEIGAI_TSUMIAGE_KANBEN")]
            public String SHIIREKYOTUUZEIKOMI8_INVOICEIGAI_TSUMIAGE_KANBEN { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEINUKI8_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREKAZEIZEINUKI8_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIGAKU8_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREKAZEIZEIGAKU8_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIKOMI8_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREKAZEIZEIKOMI8_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEINUKI8_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREHIKAZEIZEINUKI8_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIGAKU8_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREHIKAZEIZEIGAKU8_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIKOMI8_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREHIKAZEIZEIKOMI8_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEINUKI8_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREKYOTUUZEINUKI8_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIGAKU8_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREKYOTUUZEIGAKU8_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIKOMI8_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREKYOTUUZEIKOMI8_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEINUKI8_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREKAZEIZEINUKI8_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIGAKU8_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREKAZEIZEIGAKU8_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIKOMI8_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREKAZEIZEIKOMI8_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEINUKI8_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREHIKAZEIZEINUKI8_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIGAKU8_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREHIKAZEIZEIGAKU8_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIKOMI8_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREHIKAZEIZEIKOMI8_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEINUKI8_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREKYOTUUZEINUKI8_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIGAKU8_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREKYOTUUZEIGAKU8_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIKOMI8_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREKYOTUUZEIKOMI8_INVOICEIGAI_WARIMODOSHI50 { get; set; }

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
                SHIIREKAZEIZEINUKI8_TSUMIAGE = String.Empty;
                SHIIREKAZEIZEIGAKU8_TSUMIAGE = String.Empty;
                SHIIREKAZEIZEIKOMI8_TSUMIAGE = String.Empty;
                SHIIREHIKAZEIZEINUKI8_TSUMIAGE = String.Empty;
                SHIIREHIKAZEIZEIGAKU8_TSUMIAGE = String.Empty;
                SHIIREHIKAZEIZEIKOMI8_TSUMIAGE = String.Empty;
                SHIIREKYOTUUZEINUKI8_TSUMIAGE = String.Empty;
                SHIIREKYOTUUZEIGAKU8_TSUMIAGE = String.Empty;
                SHIIREKYOTUUZEIKOMI8_TSUMIAGE = String.Empty;
                SHIIREKAZEIZEINUKI8_WARIMODOSHI = String.Empty;
                SHIIREKAZEIZEIGAKU8_WARIMODOSHI = String.Empty;
                SHIIREKAZEIZEIKOMI8_WARIMODOSHI = String.Empty;
                SHIIREHIKAZEIZEINUKI8_WARIMODOSHI = String.Empty;
                SHIIREHIKAZEIZEIGAKU8_WARIMODOSHI = String.Empty;
                SHIIREHIKAZEIZEIKOMI8_WARIMODOSHI = String.Empty;
                SHIIREKYOTUUZEINUKI8_WARIMODOSHI = String.Empty;
                SHIIREKYOTUUZEIGAKU8_WARIMODOSHI = String.Empty;
                SHIIREKYOTUUZEIKOMI8_WARIMODOSHI = String.Empty;
                SHIIREKAZEIZEIGAKU8_INVOICEIGAI_TSUMIAGE_KANBEN = String.Empty;
                SHIIREKAZEIZEIKOMI8_INVOICEIGAI_TSUMIAGE_KANBEN = String.Empty;
                SHIIREHIKAZEIZEIGAKU8_INVOICEIGAI_TSUMIAGE_KANBEN = String.Empty;
                SHIIREHIKAZEIZEIKOMI8_INVOICEIGAI_TSUMIAGE_KANBEN = String.Empty;
                SHIIREKYOTUUZEIGAKU8_INVOICEIGAI_TSUMIAGE_KANBEN = String.Empty;
                SHIIREKYOTUUZEIKOMI8_INVOICEIGAI_TSUMIAGE_KANBEN = String.Empty;
                SHIIREKAZEIZEINUKI8_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREKAZEIZEIGAKU8_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREKAZEIZEIKOMI8_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREHIKAZEIZEINUKI8_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREHIKAZEIZEIGAKU8_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREHIKAZEIZEIKOMI8_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREKYOTUUZEINUKI8_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREKYOTUUZEIGAKU8_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREKYOTUUZEIKOMI8_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREKAZEIZEINUKI8_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREKAZEIZEIGAKU8_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREKAZEIZEIKOMI8_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREHIKAZEIZEINUKI8_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREHIKAZEIZEIGAKU8_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREHIKAZEIZEIKOMI8_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREKYOTUUZEINUKI8_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREKYOTUUZEIGAKU8_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREKYOTUUZEIKOMI8_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
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
            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEINUKI5_TSUMIAGE")]
            public String SHIIREKAZEIZEINUKI5_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIGAKU5_TSUMIAGE")]
            public String SHIIREKAZEIZEIGAKU5_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIKOMI5_TSUMIAGE")]
            public String SHIIREKAZEIZEIKOMI5_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEINUKI5_TSUMIAGE")]
            public String SHIIREHIKAZEIZEINUKI5_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIGAKU5_TSUMIAGE")]
            public String SHIIREHIKAZEIZEIGAKU5_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIKOMI5_TSUMIAGE")]
            public String SHIIREHIKAZEIZEIKOMI5_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEINUKI5_TSUMIAGE")]
            public String SHIIREKYOTUUZEINUKI5_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIGAKU5_TSUMIAGE")]
            public String SHIIREKYOTUUZEIGAKU5_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIKOMI5_TSUMIAGE")]
            public String SHIIREKYOTUUZEIKOMI5_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEINUKI5_WARIMODOSHI")]
            public String SHIIREKAZEIZEINUKI5_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIGAKU5_WARIMODOSHI")]
            public String SHIIREKAZEIZEIGAKU5_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIKOMI5_WARIMODOSHI")]
            public String SHIIREKAZEIZEIKOMI5_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEINUKI5_WARIMODOSHI")]
            public String SHIIREHIKAZEIZEINUKI5_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIGAKU5_WARIMODOSHI")]
            public String SHIIREHIKAZEIZEIGAKU5_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIKOMI5_WARIMODOSHI")]
            public String SHIIREHIKAZEIZEIKOMI5_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEINUKI5_WARIMODOSHI")]
            public String SHIIREKYOTUUZEINUKI5_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIGAKU5_WARIMODOSHI")]
            public String SHIIREKYOTUUZEIGAKU5_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIKOMI5_WARIMODOSHI")]
            public String SHIIREKYOTUUZEIKOMI5_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIGAKU5_INVOICEIGAI_TSUMIAGE_KANBEN")]
            public String SHIIREKAZEIZEIGAKU5_INVOICEIGAI_TSUMIAGE_KANBEN { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIKOMI5_INVOICEIGAI_TSUMIAGE_KANBEN")]
            public String SHIIREKAZEIZEIKOMI5_INVOICEIGAI_TSUMIAGE_KANBEN { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIGAKU5_INVOICEIGAI_TSUMIAGE_KANBEN")]
            public String SHIIREHIKAZEIZEIGAKU5_INVOICEIGAI_TSUMIAGE_KANBEN { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIKOMI5_INVOICEIGAI_TSUMIAGE_KANBEN")]
            public String SHIIREHIKAZEIZEIKOMI5_INVOICEIGAI_TSUMIAGE_KANBEN { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIGAKU5_INVOICEIGAI_TSUMIAGE_KANBEN")]
            public String SHIIREKYOTUUZEIGAKU5_INVOICEIGAI_TSUMIAGE_KANBEN { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIKOMI5_INVOICEIGAI_TSUMIAGE_KANBEN")]
            public String SHIIREKYOTUUZEIKOMI5_INVOICEIGAI_TSUMIAGE_KANBEN { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEINUKI5_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREKAZEIZEINUKI5_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIGAKU5_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREKAZEIZEIGAKU5_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIKOMI5_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREKAZEIZEIKOMI5_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEINUKI5_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREHIKAZEIZEINUKI5_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIGAKU5_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREHIKAZEIZEIGAKU5_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIKOMI5_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREHIKAZEIZEIKOMI5_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEINUKI5_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREKYOTUUZEINUKI5_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIGAKU5_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREKYOTUUZEIGAKU5_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIKOMI5_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREKYOTUUZEIKOMI5_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEINUKI5_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREKAZEIZEINUKI5_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIGAKU5_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREKAZEIZEIGAKU5_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIKOMI5_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREKAZEIZEIKOMI5_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEINUKI5_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREHIKAZEIZEINUKI5_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIGAKU5_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREHIKAZEIZEIGAKU5_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIKOMI5_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREHIKAZEIZEIKOMI5_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEINUKI5_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREKYOTUUZEINUKI5_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIGAKU5_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREKYOTUUZEIGAKU5_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIKOMI5_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREKYOTUUZEIKOMI5_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public D020121()
            {
                SHIIREKAZEIZEINUKI5_TSUMIAGE = String.Empty;
                SHIIREKAZEIZEIGAKU5_TSUMIAGE = String.Empty;
                SHIIREKAZEIZEIKOMI5_TSUMIAGE = String.Empty;
                SHIIREHIKAZEIZEINUKI5_TSUMIAGE = String.Empty;
                SHIIREHIKAZEIZEIGAKU5_TSUMIAGE = String.Empty;
                SHIIREHIKAZEIZEIKOMI5_TSUMIAGE = String.Empty;
                SHIIREKYOTUUZEINUKI5_TSUMIAGE = String.Empty;
                SHIIREKYOTUUZEIGAKU5_TSUMIAGE = String.Empty;
                SHIIREKYOTUUZEIKOMI5_TSUMIAGE = String.Empty;
                SHIIREKAZEIZEINUKI5_WARIMODOSHI = String.Empty;
                SHIIREKAZEIZEIGAKU5_WARIMODOSHI = String.Empty;
                SHIIREKAZEIZEIKOMI5_WARIMODOSHI = String.Empty;
                SHIIREHIKAZEIZEINUKI5_WARIMODOSHI = String.Empty;
                SHIIREHIKAZEIZEIGAKU5_WARIMODOSHI = String.Empty;
                SHIIREHIKAZEIZEIKOMI5_WARIMODOSHI = String.Empty;
                SHIIREKYOTUUZEINUKI5_WARIMODOSHI = String.Empty;
                SHIIREKYOTUUZEIGAKU5_WARIMODOSHI = String.Empty;
                SHIIREKYOTUUZEIKOMI5_WARIMODOSHI = String.Empty;
                SHIIREKAZEIZEIGAKU5_INVOICEIGAI_TSUMIAGE_KANBEN = String.Empty;
                SHIIREKAZEIZEIKOMI5_INVOICEIGAI_TSUMIAGE_KANBEN = String.Empty;
                SHIIREHIKAZEIZEIGAKU5_INVOICEIGAI_TSUMIAGE_KANBEN = String.Empty;
                SHIIREHIKAZEIZEIKOMI5_INVOICEIGAI_TSUMIAGE_KANBEN = String.Empty;
                SHIIREKYOTUUZEIGAKU5_INVOICEIGAI_TSUMIAGE_KANBEN = String.Empty;
                SHIIREKYOTUUZEIKOMI5_INVOICEIGAI_TSUMIAGE_KANBEN = String.Empty;
                SHIIREKAZEIZEINUKI5_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREKAZEIZEIGAKU5_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREKAZEIZEIKOMI5_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREHIKAZEIZEINUKI5_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREHIKAZEIZEIGAKU5_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREHIKAZEIZEIKOMI5_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREKYOTUUZEINUKI5_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREKYOTUUZEIGAKU5_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREKYOTUUZEIKOMI5_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREKAZEIZEINUKI5_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREKAZEIZEIGAKU5_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREKAZEIZEIKOMI5_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREHIKAZEIZEINUKI5_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREHIKAZEIZEIGAKU5_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREHIKAZEIZEIKOMI5_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREKYOTUUZEINUKI5_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREKYOTUUZEIGAKU5_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREKYOTUUZEIKOMI5_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
            }
        }

        public class D020122
        {
            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEINUKI3_TSUMIAGE")]
            public String SHIIREKAZEIZEINUKI3_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIGAKU3_TSUMIAGE")]
            public String SHIIREKAZEIZEIGAKU3_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIKOMI3_TSUMIAGE")]
            public String SHIIREKAZEIZEIKOMI3_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEINUKI3_TSUMIAGE")]
            public String SHIIREHIKAZEIZEINUKI3_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIGAKU3_TSUMIAGE")]
            public String SHIIREHIKAZEIZEIGAKU3_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIKOMI3_TSUMIAGE")]
            public String SHIIREHIKAZEIZEIKOMI3_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEINUKI3_TSUMIAGE")]
            public String SHIIREKYOTUUZEINUKI3_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIGAKU3_TSUMIAGE")]
            public String SHIIREKYOTUUZEIGAKU3_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIKOMI3_TSUMIAGE")]
            public String SHIIREKYOTUUZEIKOMI3_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEINUKI3_WARIMODOSHI")]
            public String SHIIREKAZEIZEINUKI3_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIGAKU3_WARIMODOSHI")]
            public String SHIIREKAZEIZEIGAKU3_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIKOMI3_WARIMODOSHI")]
            public String SHIIREKAZEIZEIKOMI3_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEINUKI3_WARIMODOSHI")]
            public String SHIIREHIKAZEIZEINUKI3_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIGAKU3_WARIMODOSHI")]
            public String SHIIREHIKAZEIZEIGAKU3_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIKOMI3_WARIMODOSHI")]
            public String SHIIREHIKAZEIZEIKOMI3_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEINUKI3_WARIMODOSHI")]
            public String SHIIREKYOTUUZEINUKI3_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIGAKU3_WARIMODOSHI")]
            public String SHIIREKYOTUUZEIGAKU3_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIKOMI3_WARIMODOSHI")]
            public String SHIIREKYOTUUZEIKOMI3_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIGAKU3_INVOICEIGAI_TSUMIAGE_KANBEN")]
            public String SHIIREKAZEIZEIGAKU3_INVOICEIGAI_TSUMIAGE_KANBEN { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIKOMI3_INVOICEIGAI_TSUMIAGE_KANBEN")]
            public String SHIIREKAZEIZEIKOMI3_INVOICEIGAI_TSUMIAGE_KANBEN { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIGAKU3_INVOICEIGAI_TSUMIAGE_KANBEN")]
            public String SHIIREHIKAZEIZEIGAKU3_INVOICEIGAI_TSUMIAGE_KANBEN { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIKOMI3_INVOICEIGAI_TSUMIAGE_KANBEN")]
            public String SHIIREHIKAZEIZEIKOMI3_INVOICEIGAI_TSUMIAGE_KANBEN { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIGAKU3_INVOICEIGAI_TSUMIAGE_KANBEN")]
            public String SHIIREKYOTUUZEIGAKU3_INVOICEIGAI_TSUMIAGE_KANBEN { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIKOMI3_INVOICEIGAI_TSUMIAGE_KANBEN")]
            public String SHIIREKYOTUUZEIKOMI3_INVOICEIGAI_TSUMIAGE_KANBEN { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEINUKI3_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREKAZEIZEINUKI3_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIGAKU3_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREKAZEIZEIGAKU3_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIKOMI3_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREKAZEIZEIKOMI3_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEINUKI3_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREHIKAZEIZEINUKI3_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIGAKU3_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREHIKAZEIZEIGAKU3_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIKOMI3_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREHIKAZEIZEIKOMI3_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEINUKI3_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREKYOTUUZEINUKI3_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIGAKU3_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREKYOTUUZEIGAKU3_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIKOMI3_INVOICEIGAI_WARIMODOSHI80")]
            public String SHIIREKYOTUUZEIKOMI3_INVOICEIGAI_WARIMODOSHI80 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEINUKI3_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREKAZEIZEINUKI3_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIGAKU3_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREKAZEIZEIGAKU3_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKAZEIZEIKOMI3_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREKAZEIZEIKOMI3_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEINUKI3_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREHIKAZEIZEINUKI3_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIGAKU3_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREHIKAZEIZEIGAKU3_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREHIKAZEIZEIKOMI3_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREHIKAZEIZEIKOMI3_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEINUKI3_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREKYOTUUZEINUKI3_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIGAKU3_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREKYOTUUZEIGAKU3_INVOICEIGAI_WARIMODOSHI50 { get; set; }

            [System.Xml.Serialization.XmlElement("SHIIREKYOTUUZEIKOMI3_INVOICEIGAI_WARIMODOSHI50")]
            public String SHIIREKYOTUUZEIKOMI3_INVOICEIGAI_WARIMODOSHI50 { get; set; }


            /// <summary>
            /// コンストラクタ
            /// </summary>
            public D020122()
            {
                SHIIREKAZEIZEINUKI3_TSUMIAGE = String.Empty;
                SHIIREKAZEIZEIGAKU3_TSUMIAGE = String.Empty;
                SHIIREKAZEIZEIKOMI3_TSUMIAGE = String.Empty;
                SHIIREHIKAZEIZEINUKI3_TSUMIAGE = String.Empty;
                SHIIREHIKAZEIZEIGAKU3_TSUMIAGE = String.Empty;
                SHIIREHIKAZEIZEIKOMI3_TSUMIAGE = String.Empty;
                SHIIREKYOTUUZEINUKI3_TSUMIAGE = String.Empty;
                SHIIREKYOTUUZEIGAKU3_TSUMIAGE = String.Empty;
                SHIIREKYOTUUZEIKOMI3_TSUMIAGE = String.Empty;
                SHIIREKAZEIZEINUKI3_WARIMODOSHI = String.Empty;
                SHIIREKAZEIZEIGAKU3_WARIMODOSHI = String.Empty;
                SHIIREKAZEIZEIKOMI3_WARIMODOSHI = String.Empty;
                SHIIREHIKAZEIZEINUKI3_WARIMODOSHI = String.Empty;
                SHIIREHIKAZEIZEIGAKU3_WARIMODOSHI = String.Empty;
                SHIIREHIKAZEIZEIKOMI3_WARIMODOSHI = String.Empty;
                SHIIREKYOTUUZEINUKI3_WARIMODOSHI = String.Empty;
                SHIIREKYOTUUZEIGAKU3_WARIMODOSHI = String.Empty;
                SHIIREKYOTUUZEIKOMI3_WARIMODOSHI = String.Empty;
                SHIIREKAZEIZEIGAKU3_INVOICEIGAI_TSUMIAGE_KANBEN = String.Empty;
                SHIIREKAZEIZEIKOMI3_INVOICEIGAI_TSUMIAGE_KANBEN = String.Empty;
                SHIIREHIKAZEIZEIGAKU3_INVOICEIGAI_TSUMIAGE_KANBEN = String.Empty;
                SHIIREHIKAZEIZEIKOMI3_INVOICEIGAI_TSUMIAGE_KANBEN = String.Empty;
                SHIIREKYOTUUZEIGAKU3_INVOICEIGAI_TSUMIAGE_KANBEN = String.Empty;
                SHIIREKYOTUUZEIKOMI3_INVOICEIGAI_TSUMIAGE_KANBEN = String.Empty;
                SHIIREKAZEIZEINUKI3_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREKAZEIZEIGAKU3_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREKAZEIZEIKOMI3_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREHIKAZEIZEINUKI3_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREHIKAZEIZEIGAKU3_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREHIKAZEIZEIKOMI3_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREKYOTUUZEINUKI3_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREKYOTUUZEIGAKU3_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREKYOTUUZEIKOMI3_INVOICEIGAI_WARIMODOSHI80 = String.Empty;
                SHIIREKAZEIZEINUKI3_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREKAZEIZEIGAKU3_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREKAZEIZEIKOMI3_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREHIKAZEIZEINUKI3_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREHIKAZEIZEIGAKU3_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREHIKAZEIZEIKOMI3_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREKYOTUUZEINUKI3_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREKYOTUUZEIGAKU3_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
                SHIIREKYOTUUZEIKOMI3_INVOICEIGAI_WARIMODOSHI50 = String.Empty;
            }
        }

        public class D020134
        {
            [System.Xml.Serialization.XmlElement("KASHIDAOREZEINUKI10_TSUMIAGE")]
            public String KASHIDAOREZEINUKI10_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREZEIGAKU10_TSUMIAGE")]
            public String KASHIDAOREZEIGAKU10_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREZEIKOMI10_TSUMIAGE")]
            public String KASHIDAOREZEIKOMI10_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREZEINUKI10_WARIMODOSHI")]
            public String KASHIDAOREZEINUKI10_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREZEIGAKU10_WARIMODOSHI")]
            public String KASHIDAOREZEIGAKU10_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREZEIKOMI10_WARIMODOSHI")]
            public String KASHIDAOREZEIKOMI10_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEINUKI10_TSUMIAGE")]
            public String KASHIDAOREKAISHUZEINUKI10_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEIGAKU10_TSUMIAGE")]
            public String KASHIDAOREKAISHUZEIGAKU10_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEIKOMI10_TSUMIAGE")]
            public String KASHIDAOREKAISHUZEIKOMI10_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEINUKI10_WARIMODOSHI")]
            public String KASHIDAOREKAISHUZEINUKI10_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEIGAKU10_WARIMODOSHI")]
            public String KASHIDAOREKAISHUZEIGAKU10_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEIKOMI10_WARIMODOSHI")]
            public String KASHIDAOREKAISHUZEIKOMI10_WARIMODOSHI { get; set; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public D020134()
            {
                KASHIDAOREZEINUKI10_TSUMIAGE = String.Empty;
                KASHIDAOREZEIGAKU10_TSUMIAGE = String.Empty;
                KASHIDAOREZEIKOMI10_TSUMIAGE = String.Empty;
                KASHIDAOREZEINUKI10_WARIMODOSHI = String.Empty;
                KASHIDAOREZEIGAKU10_WARIMODOSHI = String.Empty;
                KASHIDAOREZEIKOMI10_WARIMODOSHI = String.Empty;
                KASHIDAOREKAISHUZEINUKI10_TSUMIAGE = String.Empty;
                KASHIDAOREKAISHUZEIGAKU10_TSUMIAGE = String.Empty;
                KASHIDAOREKAISHUZEIKOMI10_TSUMIAGE = String.Empty;
                KASHIDAOREKAISHUZEINUKI10_WARIMODOSHI = String.Empty;
                KASHIDAOREKAISHUZEIGAKU10_WARIMODOSHI = String.Empty;
                KASHIDAOREKAISHUZEIKOMI10_WARIMODOSHI = String.Empty;
            }
        }

        public class D020133
        {
            [System.Xml.Serialization.XmlElement("KASHIDAOREZEINUKI8K_TSUMIAGE")]
            public String KASHIDAOREZEINUKI8K_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREZEIGAKU8K_TSUMIAGE")]
            public String KASHIDAOREZEIGAKU8K_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREZEIKOMI8K_TSUMIAGE")]
            public String KASHIDAOREZEIKOMI8K_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREZEINUKI8K_WARIMODOSHI")]
            public String KASHIDAOREZEINUKI8K_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREZEIGAKU8K_WARIMODOSHI")]
            public String KASHIDAOREZEIGAKU8K_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREZEIKOMI8K_WARIMODOSHI")]
            public String KASHIDAOREZEIKOMI8K_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEINUKI8K_TSUMIAGE")]
            public String KASHIDAOREKAISHUZEINUKI8K_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEIGAKU8K_TSUMIAGE")]
            public String KASHIDAOREKAISHUZEIGAKU8K_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEIKOMI8K_TSUMIAGE")]
            public String KASHIDAOREKAISHUZEIKOMI8K_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEINUKI8K_WARIMODOSHI")]
            public String KASHIDAOREKAISHUZEINUKI8K_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEIGAKU8K_WARIMODOSHI")]
            public String KASHIDAOREKAISHUZEIGAKU8K_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEIKOMI8K_WARIMODOSHI")]
            public String KASHIDAOREKAISHUZEIKOMI8K_WARIMODOSHI { get; set; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public D020133()
            {
                KASHIDAOREZEINUKI8K_TSUMIAGE = String.Empty;
                KASHIDAOREZEIGAKU8K_TSUMIAGE = String.Empty;
                KASHIDAOREZEIKOMI8K_TSUMIAGE = String.Empty;
                KASHIDAOREZEINUKI8K_WARIMODOSHI = String.Empty;
                KASHIDAOREZEIGAKU8K_WARIMODOSHI = String.Empty;
                KASHIDAOREZEIKOMI8K_WARIMODOSHI = String.Empty;
                KASHIDAOREKAISHUZEINUKI8K_TSUMIAGE = String.Empty;
                KASHIDAOREKAISHUZEIGAKU8K_TSUMIAGE = String.Empty;
                KASHIDAOREKAISHUZEIKOMI8K_TSUMIAGE = String.Empty;
                KASHIDAOREKAISHUZEINUKI8K_WARIMODOSHI = String.Empty;
                KASHIDAOREKAISHUZEIGAKU8K_WARIMODOSHI = String.Empty;
                KASHIDAOREKAISHUZEIKOMI8K_WARIMODOSHI = String.Empty;
            }
        }

        public class D020130
        {
            [System.Xml.Serialization.XmlElement("KASHIDAOREZEINUKI8_TSUMIAGE")]
            public String KASHIDAOREZEINUKI8_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREZEIGAKU8_TSUMIAGE")]
            public String KASHIDAOREZEIGAKU8_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREZEIKOMI8_TSUMIAGE")]
            public String KASHIDAOREZEIKOMI8_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREZEINUKI8_WARIMODOSHI")]
            public String KASHIDAOREZEINUKI8_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREZEIGAKU8_WARIMODOSHI")]
            public String KASHIDAOREZEIGAKU8_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREZEIKOMI8_WARIMODOSHI")]
            public String KASHIDAOREZEIKOMI8_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEINUKI8_TSUMIAGE")]
            public String KASHIDAOREKAISHUZEINUKI8_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEIGAKU8_TSUMIAGE")]
            public String KASHIDAOREKAISHUZEIGAKU8_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEIKOMI8_TSUMIAGE")]
            public String KASHIDAOREKAISHUZEIKOMI8_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEINUKI8_WARIMODOSHI")]
            public String KASHIDAOREKAISHUZEINUKI8_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEIGAKU8_WARIMODOSHI")]
            public String KASHIDAOREKAISHUZEIGAKU8_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEIKOMI8_WARIMODOSHI")]
            public String KASHIDAOREKAISHUZEIKOMI8_WARIMODOSHI { get; set; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public D020130()
            {
                KASHIDAOREZEINUKI8_TSUMIAGE = String.Empty;
                KASHIDAOREZEIGAKU8_TSUMIAGE = String.Empty;
                KASHIDAOREZEIKOMI8_TSUMIAGE = String.Empty;
                KASHIDAOREZEINUKI8_WARIMODOSHI = String.Empty;
                KASHIDAOREZEIGAKU8_WARIMODOSHI = String.Empty;
                KASHIDAOREZEIKOMI8_WARIMODOSHI = String.Empty;
                KASHIDAOREKAISHUZEINUKI8_TSUMIAGE = String.Empty;
                KASHIDAOREKAISHUZEIGAKU8_TSUMIAGE = String.Empty;
                KASHIDAOREKAISHUZEIKOMI8_TSUMIAGE = String.Empty;
                KASHIDAOREKAISHUZEINUKI8_WARIMODOSHI = String.Empty;
                KASHIDAOREKAISHUZEIGAKU8_WARIMODOSHI = String.Empty;
                KASHIDAOREKAISHUZEIKOMI8_WARIMODOSHI = String.Empty;
            }
        }

        public class D020131
        {
            [System.Xml.Serialization.XmlElement("KASHIDAOREZEINUKI5_TSUMIAGE")]
            public String KASHIDAOREZEINUKI5_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREZEIGAKU5_TSUMIAGE")]
            public String KASHIDAOREZEIGAKU5_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREZEIKOMI5_TSUMIAGE")]
            public String KASHIDAOREZEIKOMI5_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREZEINUKI5_WARIMODOSHI")]
            public String KASHIDAOREZEINUKI5_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREZEIGAKU5_WARIMODOSHI")]
            public String KASHIDAOREZEIGAKU5_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREZEIKOMI5_WARIMODOSHI")]
            public String KASHIDAOREZEIKOMI5_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEINUKI5_TSUMIAGE")]
            public String KASHIDAOREKAISHUZEINUKI5_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEIGAKU5_TSUMIAGE")]
            public String KASHIDAOREKAISHUZEIGAKU5_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEIKOMI5_TSUMIAGE")]
            public String KASHIDAOREKAISHUZEIKOMI5_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEINUKI5_WARIMODOSHI")]
            public String KASHIDAOREKAISHUZEINUKI5_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEIGAKU5_WARIMODOSHI")]
            public String KASHIDAOREKAISHUZEIGAKU5_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEIKOMI5_WARIMODOSHI")]
            public String KASHIDAOREKAISHUZEIKOMI5_WARIMODOSHI { get; set; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public D020131()
            {
                KASHIDAOREZEINUKI5_TSUMIAGE = String.Empty;
                KASHIDAOREZEIGAKU5_TSUMIAGE = String.Empty;
                KASHIDAOREZEIKOMI5_TSUMIAGE = String.Empty;
                KASHIDAOREZEINUKI5_WARIMODOSHI = String.Empty;
                KASHIDAOREZEIGAKU5_WARIMODOSHI = String.Empty;
                KASHIDAOREZEIKOMI5_WARIMODOSHI = String.Empty;
                KASHIDAOREKAISHUZEINUKI5_TSUMIAGE = String.Empty;
                KASHIDAOREKAISHUZEIGAKU5_TSUMIAGE = String.Empty;
                KASHIDAOREKAISHUZEIKOMI5_TSUMIAGE = String.Empty;
                KASHIDAOREKAISHUZEINUKI5_WARIMODOSHI = String.Empty;
                KASHIDAOREKAISHUZEIGAKU5_WARIMODOSHI = String.Empty;
                KASHIDAOREKAISHUZEIKOMI5_WARIMODOSHI = String.Empty;
            }
        }

        public class D020132
        {
            [System.Xml.Serialization.XmlElement("KASHIDAOREZEINUKI3_TSUMIAGE")]
            public String KASHIDAOREZEINUKI3_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREZEIGAKU3_TSUMIAGE")]
            public String KASHIDAOREZEIGAKU3_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREZEIKOMI3_TSUMIAGE")]
            public String KASHIDAOREZEIKOMI3_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREZEINUKI3_WARIMODOSHI")]
            public String KASHIDAOREZEINUKI3_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREZEIGAKU3_WARIMODOSHI")]
            public String KASHIDAOREZEIGAKU3_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREZEIKOMI3_WARIMODOSHI")]
            public String KASHIDAOREZEIKOMI3_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEINUKI3_TSUMIAGE")]
            public String KASHIDAOREKAISHUZEINUKI3_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEIGAKU3_TSUMIAGE")]
            public String KASHIDAOREKAISHUZEIGAKU3_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEIKOMI3_TSUMIAGE")]
            public String KASHIDAOREKAISHUZEIKOMI3_TSUMIAGE { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEINUKI3_WARIMODOSHI")]
            public String KASHIDAOREKAISHUZEINUKI3_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEIGAKU3_WARIMODOSHI")]
            public String KASHIDAOREKAISHUZEIGAKU3_WARIMODOSHI { get; set; }

            [System.Xml.Serialization.XmlElement("KASHIDAOREKAISHUZEIKOMI3_WARIMODOSHI")]
            public String KASHIDAOREKAISHUZEIKOMI3_WARIMODOSHI { get; set; }


            /// <summary>
            /// コンストラクタ
            /// </summary>
            public D020132()
            {
                KASHIDAOREZEINUKI3_TSUMIAGE = String.Empty;
                KASHIDAOREZEIGAKU3_TSUMIAGE = String.Empty;
                KASHIDAOREZEIKOMI3_TSUMIAGE = String.Empty;
                KASHIDAOREZEINUKI3_WARIMODOSHI = String.Empty;
                KASHIDAOREZEIGAKU3_WARIMODOSHI = String.Empty;
                KASHIDAOREZEIKOMI3_WARIMODOSHI = String.Empty;
                KASHIDAOREKAISHUZEINUKI3_TSUMIAGE = String.Empty;
                KASHIDAOREKAISHUZEIGAKU3_TSUMIAGE = String.Empty;
                KASHIDAOREKAISHUZEIKOMI3_TSUMIAGE = String.Empty;
                KASHIDAOREKAISHUZEINUKI3_WARIMODOSHI = String.Empty;
                KASHIDAOREKAISHUZEIGAKU3_WARIMODOSHI = String.Empty;
                KASHIDAOREKAISHUZEIKOMI3_WARIMODOSHI = String.Empty;
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

            [System.Xml.Serialization.XmlElement("KYOJUYOCHINTAI10")]
            public String KYOJUYOCHINTAI10 { get; set; }

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
                KYOJUYOCHINTAI10 = String.Empty;
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

            [System.Xml.Serialization.XmlElement("KYOJUYOCHINTAI8K")]
            public String KYOJUYOCHINTAI8K { get; set; }

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
                KYOJUYOCHINTAI8K = String.Empty;
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

            [System.Xml.Serialization.XmlElement("KYOJUYOCHINTAI8")]
            public String KYOJUYOCHINTAI8 { get; set; }
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
                KYOJUYOCHINTAI8 = String.Empty;
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

            [System.Xml.Serialization.XmlElement("KYOJUYOCHINTAI5")]
            public String KYOJUYOCHINTAI5 { get; set; }
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
                KYOJUYOCHINTAI5 = String.Empty;
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

            [System.Xml.Serialization.XmlElement("KYOJUYOCHINTAI3")]
            public String KYOJUYOCHINTAI3 { get; set; }
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
                KYOJUYOCHINTAI3 = String.Empty;
            }
        }
    }
}

