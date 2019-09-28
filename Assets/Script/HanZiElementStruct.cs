using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using UnityEngine;
using System.Collections;
using System.Xml;


/// <summary>
/// 汉字元素结构体
/// </summary>
public struct Han_Zi_Ele_Struct
{	
	public int FirstNum;         //这几个编号数据应该放到一个数组中
	public int SecondNum;
    public int ThirdNum;
    public int ForthNum;
    public int FiveNum;
	public int CompNum;         //汉字合成编号
    public Texture2D image;     //对应文字的图片
	public int CompCount;       //合成次数
	//public string name;       //暂时不用
}



/// <summary>
/// 汉字元素索引结构体
/// </summary>

/*public struct Element_Index_Struct {
public int FirstNum;
public int SecondNum;
public int CompNum;
	/*public Element_Index_Struct(int a,int b,int c)
	{
		this.FirstNum=a;
		this.SecondNum=b;
		this.CompNum=c;

	}
}*/





namespace Assets.Script
{
    class HanZiElementStruct
    {
    }
}
