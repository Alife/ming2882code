using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Models.Enums
{
    public enum Nation
    {
        /// <summary> 
        ///汉族 
        /// </summary> 
        [Description("汉族")]
        s1 = 1,
        /// <summary> 
        ///藏族 
        /// </summary> 
        [Description("藏族")]
        s2 = 2,
        /// <summary> 
        ///朝鲜族 
        /// </summary> 
        [Description("朝鲜族")]
        s3 = 3,
        /// <summary> 
        ///蒙古族 
        /// </summary> 
        [Description("蒙古族")]
        s4 = 4,
        /// <summary> 
        ///满族 
        /// </summary> 
        [Description("满族")]
        s5 = 5,
        /// <summary> 
        ///维吾尔族 
        /// </summary> 
        [Description("维吾尔族")]
        s6 = 6,
        /// <summary> 
        ///壮族 
        /// </summary> 
        [Description("壮族")]
        s7 = 7,
        /// <summary> 
        ///彝族 
        /// </summary> 
        [Description("彝族")]
        s8 = 8,
        /// <summary> 
        ///苗族 
        /// </summary> 
        [Description("苗族")]
        s9 = 9,
        /// <summary> 
        ///侗族 
        /// </summary> 
        [Description("侗族")]
        s10 = 10,
        /// <summary> 
        ///瑶族 
        /// </summary> 
        [Description("瑶族")]
        s11 = 11,
        /// <summary> 
        ///白族 
        /// </summary> 
        [Description("白族")]
        s12 = 12,
        /// <summary> 
        ///布依族 
        /// </summary> 
        [Description("布依族")]
        s13 = 13,
        /// <summary> 
        ///傣族 
        /// </summary> 
        [Description("傣族")]
        s14 = 14,
        /// <summary> 
        ///京族 
        /// </summary> 
        [Description("京族")]
        s15 = 15,
        /// <summary> 
        ///黎族 
        /// </summary> 
        [Description("黎族")]
        s16 = 16,
        /// <summary> 
        ///羌族 
        /// </summary> 
        [Description("羌族")]
        s17 = 17,
        /// <summary> 
        ///佤族 
        /// </summary> 
        [Description("佤族")]
        s18 = 18,
        /// <summary> 
        ///水族 
        /// </summary> 
        [Description("水族")]
        s19 = 19,
        /// <summary> 
        ///畲族 
        /// </summary> 
        [Description("畲族")]
        s20 = 20,
        /// <summary> 
        ///土族 
        /// </summary> 
        [Description("土族")]
        s21 = 21,
        /// <summary> 
        ///阿昌族 
        /// </summary> 
        [Description("阿昌族")]
        s22 = 22,
        /// <summary> 
        ///哈尼族 
        /// </summary> 
        [Description("哈尼族")]
        s23 = 23,
        /// <summary> 
        ///高山族 
        /// </summary> 
        [Description("高山族")]
        s24 = 24,
        /// <summary> 
        ///景颇族 
        /// </summary> 
        [Description("景颇族")]
        s25 = 25,
        /// <summary> 
        ///珞巴族 
        /// </summary> 
        [Description("珞巴族")]
        s26 = 26,
        /// <summary> 
        ///锡伯族 
        /// </summary> 
        [Description("锡伯族")]
        s27 = 27,
        /// <summary> 
        ///德昂(崩龙)族 
        /// </summary> 
        [Description("崩龙族")]
        s28 = 28,
        /// <summary> 
        ///保安族 
        /// </summary> 
        [Description("保安族")]
        s29 = 29,
        /// <summary> 
        ///基诺族 
        /// </summary> 
        [Description("基诺族")]
        s30 = 30,
        /// <summary> 
        ///门巴族 
        /// </summary> 
        [Description("门巴族")]
        s31 = 31,
        /// <summary> 
        ///毛南族 
        /// </summary> 
        [Description("毛南族")]
        s32 = 32,
        /// <summary> 
        ///赫哲族 
        /// </summary> 
        [Description("赫哲族")]
        s33 = 33,
        /// <summary> 
        ///裕固族 
        /// </summary> 
        [Description("裕固族")]
        s34 = 34,
        /// <summary> 
        ///撒拉族 
        /// </summary> 
        [Description("撒拉族")]
        s35 = 35,
        /// <summary> 
        ///独龙族 
        /// </summary> 
        [Description("独龙族")]
        s36 = 36,
        /// <summary> 
        ///普米族 
        /// </summary> 
        [Description("普米族")]
        s37 = 37,
        /// <summary> 
        ///仫佬族 
        /// </summary> 
        [Description("仫佬族")]
        s38 = 38,
        /// <summary> 
        ///仡佬族 
        /// </summary> 
        [Description("仡佬族")]
        s39 = 39,
        /// <summary> 
        ///东乡族 
        /// </summary> 
        [Description("东乡族")]
        s40 = 40,
        /// <summary> 
        ///拉祜族 
        /// </summary> 
        [Description("拉祜族")]
        s41 = 41,
        /// <summary> 
        ///土家族 
        /// </summary> 
        [Description("土家族")]
        s42 = 42,
        /// <summary> 
        ///纳西族 
        /// </summary> 
        [Description("纳西族")]
        s43 = 43,
        /// <summary> 
        ///土家族 
        /// </summary> 
        [Description("土家族")]
        s44 = 44,
        /// <summary> 
        ///纳西族 
        /// </summary> 
        [Description("纳西族")]
        s45 = 45,
        /// <summary> 
        ///傈僳族 
        /// </summary> 
        [Description("傈僳族")]
        s46 = 46,
        /// <summary> 
        ///布朗族 
        /// </summary> 
        [Description("布朗族")]
        s47 = 47,
        /// <summary> 
        ///哈萨克族 
        /// </summary> 
        [Description("哈萨克族")]
        s48 = 48,
        /// <summary> 
        ///达斡尔族 
        /// </summary> 
        [Description("达斡尔族")]
        s49 = 49,
        /// <summary> 
        ///鄂伦春族 
        /// </summary> 
        [Description("鄂伦春族")]
        s50 = 50,
        /// <summary> 
        ///鄂温克族 
        /// </summary> 
        [Description("鄂温克族")]
        s51 = 51,
        /// <summary> 
        ///俄罗斯族 
        /// </summary> 
        [Description("俄罗斯族")]
        s52 = 52,
        /// <summary> 
        ///塔塔尔族 
        /// </summary> 
        [Description("塔塔尔族")]
        s53 = 53,
        /// <summary> 
        ///塔吉克族 
        /// </summary> 
        [Description("塔吉克族")]
        s54 = 54,
        /// <summary> 
        ///柯尔克孜族 
        /// </summary> 
        [Description("柯尔克孜族")]
        s55 = 55,
        /// <summary> 
        ///乌兹别克族 
        /// </summary> 
        [Description("乌兹别克族")]
        s56 = 56,
        /// <summary> 
        ///国外 
        /// </summary> 
        [Description("国外")]
        s57 = 57
    }
}
