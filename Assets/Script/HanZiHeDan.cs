
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//using HanZiElementStruct;
struct Han_Zi_Position_Struct  //下部分汉字存放的汉字元素结构体
{
    public int HanZiNum;
    public int flag;         //标识这个汉字元素的分组 ,第1或者2个能够合成文字的文字元素

}

struct SELECT_HAN_ZI_KUANG_STRUCT{
    public	int hang;
	public int lie;

}

public  class HanZiHeDan : MonoBehaviour {
     public static List<Han_Zi_Ele_Struct> HanZiElementstrcs = new List<Han_Zi_Ele_Struct>(); //这个后面改为数据更好
     
     public Transform HanZiElement;

     public Transform TrHanZiKaung;
	 public Transform TrHanZiKaung_lan;
     //public Transform Huanxian;
	 public Transform TrShenXian;
	 GameObject Hua_xian_DaoHengUp;
     GameObject Hua_xian_DaoHengDown;
     public Han_Zi_KUANG_STRUCT[,] HanZiKuangs = new Han_Zi_KUANG_STRUCT[9, 6];
     bool hasdown = false;
     int StartHanZiKuangRow = 0;     //汉字框在素组中的行
     int StartHanZiKuangCol = 0;     //汉字在素组中的列
     int EndHanZiKuangRow = 0;
     int EndHanZiKuangCol = 0;
	

     int StartHanZiKuangCloUp = 0;  //用于记录要销毁的上下部分汉字元素。后面上部分要改成结构体数组形式，因为销毁的汉字元素并不只在一行。
     int EndHanZiKuangCloUp = 0;


     
     int ret=0;
     Han_Zi_Ele_Struct HanZiStrc = new Han_Zi_Ele_Struct();

     bool UpIsSelect=false;
     bool DownIsSelect=false;


     Han_Zi_Ele_Struct[] TempSelectHanZiUp = new Han_Zi_Ele_Struct[9];
     Han_Zi_Ele_Struct[] TempSelectHanZiDown = new Han_Zi_Ele_Struct[9];
	 SELECT_HAN_ZI_KUANG_STRUCT[] TempSelectHanZiKuangUp=new SELECT_HAN_ZI_KUANG_STRUCT[9];
	 SELECT_HAN_ZI_KUANG_STRUCT[] TempSelectHanZiKuangDown=new SELECT_HAN_ZI_KUANG_STRUCT[9];

	 Vector3 SelectScal = new Vector3(1.8f, 1.8f, 1f);
	 Vector3 OriginScal=new Vector3(1.5f, 1.5f, 1f);
	

	 protected AudioSource m_audio;
	 public AudioClip Soud_Dan_Ji;
	 public AudioClip Sound_He_Cheng;

	//不是用这些编号，应该用0-223中的文字元素编号
     int[] Coprise2CountNum = new int[] { 1,3,4,5,7 ,10, 15 ,17 ,18, 19, 24, 35, 50, 53,54 ,55, 77, 81 ,
     85, 88, 95 ,109, 172, 186 ,194, 209, 211, 221}; 
	
 
    //后面可以加上需要三次合成的文字元素数组。


	
    public Transform HanZiKuangCount_1;
    public Transform HanZiKuangCount_2;
    public Transform HanZiKuangCount_3;
    public Transform HanZiKuangCount_4;

	Transform[] HanZiKuangCounts=new Transform[5];
	 
	void Awake(){
		m_audio = this.gameObject.GetComponent<AudioSource>();
		HanZiKuangCounts[1] = HanZiKuangCount_1;
      //  HanZiKuangCounts[1].CountValue = 1;
        HanZiKuangCounts[2] = HanZiKuangCount_2;
       // HanZiKuangCounts[2].CountValue = 2;
        HanZiKuangCounts[3] = HanZiKuangCount_3;
      //  HanZiKuangCounts[3].CountValue = 3;
        HanZiKuangCounts[4] = HanZiKuangCount_4;
       // HanZiKuangCounts[4].CountValue = 4;

	}
	 
	void Start () {
       
        //通过编号数组把加载相应图片的汉字机构体元素逐一加载到容器中
        for (int i = 0; i < ElementsNumArr.ElementNumArr.GetLength(0); i++)
        {

                HanZiStrc.FirstNum = ElementsNumArr.ElementNumArr[i, 0];
                HanZiStrc.SecondNum = ElementsNumArr.ElementNumArr[i, 1];
                HanZiStrc.ThirdNum = ElementsNumArr.ElementNumArr[i, 2];
                HanZiStrc.ForthNum = ElementsNumArr.ElementNumArr[i, 3];
                HanZiStrc.FiveNum = ElementsNumArr.ElementNumArr[i, 4];
                HanZiStrc.CompNum = ElementsNumArr.ElementNumArr[i, 5];

                //CompCount后面再补充
                // print("#######" + HanZi.CompNum);
                HanZiStrc.image = (Texture2D)Resources.Load(Convert.ToString(HanZiStrc.CompNum));
                HanZiElementstrcs.Add(HanZiStrc);

        }

        //上部分文字框区域
        //把文字框对象显示出来并逐一加进二维数组中
        for (int i = 0; i < 6; i++)          //行
        {

            if (i == 5)
            {
                for (int j = 0; j < 6; j++)      //列
                {
                    //加载文字框预制件
                    Transform TransHanZiKuang;
                    //if(i==5){
                    TransHanZiKuang = (Transform)Instantiate(TrHanZiKaung_lan, new Vector2(2.42f * j, -2.4f * i - 0.4f), Quaternion.Euler(0f, 0f, 0f));

                    //}else{

                    //	TransHanZiKuang = (Transform)Instantiate(TrHanZiKaung, new Vector2(2.42f*j, -2.4f*i), Quaternion.Euler(0f, 0f, 0f));
                    //}

                    GameObject transtemp = TransHanZiKuang.gameObject;   /*GameObject.Find("WenZiKuang" + i + j);*/
                    // print("#######011" + transtemp.transform.position);
                    HanZiKuangs[i, j].HanZiKuangObject = transtemp;
                    HanZiKuangs[i, j].rowindex = i;
                    HanZiKuangs[i, j].cloindex = j;                               //数组和下面的对象都赋相应的值
                    transtemp.GetComponent<HanZiKuang>().M_HanZiKuang.HanZiKuangObject = transtemp;
                    transtemp.GetComponent<HanZiKuang>().M_HanZiKuang.rowindex = i;
                    transtemp.GetComponent<HanZiKuang>().M_HanZiKuang.cloindex = j;
					transtemp.GetComponent<HanZiKuang>().M_HanZiKuang.IsCount=0;
					transtemp.GetComponent<HanZiKuang>().M_HanZiKuang.IsCountDelete=0;
                    transtemp.name = "HanZiKuang" + Convert.ToString(i) + Convert.ToString(j);  //更新生成的文字框的名字


                }
            }
            else
            {

                for (int j = 0; j < 6; j++)      //列
                {
                    //加载文字框预制件
                    Transform TransHanZiKuang;
                    // if(i==5){
                    //	TransHanZiKuang = (Transform)Instantiate(TrHanZiKaung, new Vector2(2.42f*j, -2.4f*i-0.4f), Quaternion.Euler(0f, 0f, 0f));

                    //}else{

                    TransHanZiKuang = (Transform)Instantiate(TrHanZiKaung, new Vector2(2.42f * j, -2.4f * i), Quaternion.Euler(0f, 0f, 0f));
                    //}

                    GameObject transtemp = TransHanZiKuang.gameObject;   /*GameObject.Find("WenZiKuang" + i + j);*/
                    // print("#######011" + transtemp.transform.position);
                    HanZiKuangs[i, j].HanZiKuangObject = transtemp;
                    HanZiKuangs[i, j].rowindex = i;
                    HanZiKuangs[i, j].cloindex = j;                               //数组和下面的对象都赋相应的值
                    transtemp.GetComponent<HanZiKuang>().M_HanZiKuang.HanZiKuangObject = transtemp;
                    transtemp.GetComponent<HanZiKuang>().M_HanZiKuang.rowindex = i;
                    transtemp.GetComponent<HanZiKuang>().M_HanZiKuang.cloindex = j;
                    transtemp.name = "HanZiKuang" + Convert.ToString(i) + Convert.ToString(j);  //更新生成的文字框的名字

                    //print("#######011" + HanZiKuangs[i, j].transform.position);
                }


            }
            
        }

        //创建绳线
        TrShenXian.gameObject.SetActive(true);
        Vector3 ShenXianPosition = new Vector3(5.76f, -13.27f, 0);
        Vector3 ShenXianScal = new Vector3(0.83f, 0.6f, 1f);
        TrShenXian.position = ShenXianPosition;
        TrShenXian.localScale = ShenXianScal;

        //下部分汉字框
        for (int i = 8; i < 9; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                //加载文字框预制件
                Transform TransHanZiKuang = (Transform)Instantiate(TrHanZiKaung_lan, new Vector2(2.42f * j, -2.4f * i), Quaternion.Euler(0f, 0f, 0f));
                GameObject transtemp = TransHanZiKuang.gameObject;   /*GameObject.Find("WenZiKuang" + i + j);*/
                // print("#######011" + transtemp.transform.position);
                HanZiKuangs[i, j].HanZiKuangObject = transtemp;
                HanZiKuangs[i, j].rowindex = i;
                HanZiKuangs[i, j].cloindex = j;                               //数组和下面的对象都赋相应的值
                transtemp.GetComponent<HanZiKuang>().M_HanZiKuang.HanZiKuangObject = transtemp;
                transtemp.GetComponent<HanZiKuang>().M_HanZiKuang.rowindex = i;
                transtemp.GetComponent<HanZiKuang>().M_HanZiKuang.cloindex = j;
                transtemp.name = "HanZiKuang" + Convert.ToString(i) + Convert.ToString(j);  //更新生成的文字框的名字

                //print("#######011" + HanZiKuangs[i, j].transform.position);
            }
        }


		//上部分刚开始随机生成几个文字元素
        for (int i = 0; i < 6; i++) //文字框的列
		{
            //for (int j = 0; j < 6; j++) {    //实际上不需要按行列进行文字创建，只需for循环直接穿件54个汉字即可

               
                // foreach (Han_Zi_Ele_Struct hanzielementstrc in HanZiElementstrcs) //后面改为数组可以节省一些不必要的查找
                 //{

                    //按要求随机生成文字元素
                  //  if (hanzielementstrc.CompNum == RandNum)
                   // {
                        //按前部分偏旁来添加
                      //  print("#######015 RandNum=" + RandNum);
                        //这里添加能够下落到的位置
                       

                 StartCoroutine(CreatHanZiElemAndFallUp(i));

                    //}
                //}
                
            //}
		}
		
		Invoke("CreateHanZiCount", 1.8f);     //随机生成汉字count

		
        Invoke("CreatCompriseHanZi", 1.8f);    //把这个时延和move中的携程时延研究一下。
        StartCoroutine(CreatCompriseHanZi());  
        for (int i = 0; i < 9; i++)
        {
            TempSelectHanZiUp[i].FirstNum = 0;
            TempSelectHanZiUp[i].SecondNum = 0;
            TempSelectHanZiUp[i].CompNum = 0;
            TempSelectHanZiUp[i].image = null;
            TempSelectHanZiUp[i].CompCount = 0;

			TempSelectHanZiKuangUp[i].hang=0xff;
			TempSelectHanZiKuangUp[i].lie=0xff;
			

        }
        
        for (int i = 0; i < 9; i++)
        {
            TempSelectHanZiDown[i].FirstNum = 0;
            TempSelectHanZiDown[i].SecondNum = 0;
            TempSelectHanZiDown[i].CompNum = 0;
            TempSelectHanZiDown[i].image = null;
            TempSelectHanZiDown[i].CompCount = 0;
			
			TempSelectHanZiKuangDown[i].hang=0xff;
			TempSelectHanZiKuangDown[i].lie=0xff;

        }
        Hua_xian_DaoHengUp = GameObject.FindGameObjectWithTag("huaxian1");
        Hua_xian_DaoHengDown = GameObject.FindGameObjectWithTag("huaxian2");

	}

    public IEnumerator CreatHanZiElemAndFallUp(int lie ) {   //上部分创建文字并下落
        
        int fallrow01 = 0;
		int fallrow02 = 0;
        int RandNumTemp = 0;
       // int RandNum = 0;
        //


        //下落有问题，需要优化一下。
		ElementVerticalFall(lie,0, ref fallrow01);  //这个计算是从0开始计算才可以。 //先判断要生成几个文字元素
        //print("!!!!!!!!!!!!!!!! 62 HanZiKuangs" + "[**" + "]," + "[" + lie + "]=" + fallrow01);
		if(fallrow01==0){
 //           print("the fallow num of lie is " + fallrow01);
            yield return new WaitForSeconds(0);                                 //没有消除的元素就直接return
		}else{
			for(int i=0;i<fallrow01+1;i++){
				ElementVerticalFall(lie,0, ref fallrow02);  //再判断每个元素需要下落到的位置

	               
	        	do{
					RandNumTemp = UnityEngine.Random.Range(0, 222);//注意list通过下标来实现，所以下标从0开始有对应问题

	            } while (HanZiElementstrcs[RandNumTemp].CompNum == 155 || HanZiElementstrcs[RandNumTemp].CompNum == 157 || HanZiElementstrcs[RandNumTemp].CompNum == 163 || HanZiElementstrcs[RandNumTemp].CompNum == 173 || HanZiElementstrcs[RandNumTemp].CompNum == 174
	                || HanZiElementstrcs[RandNumTemp].CompNum == 175 || HanZiElementstrcs[RandNumTemp].CompNum == 176 || HanZiElementstrcs[RandNumTemp].CompNum == 178 || HanZiElementstrcs[RandNumTemp].CompNum == 179 || HanZiElementstrcs[RandNumTemp].CompNum == 180
	                || HanZiElementstrcs[RandNumTemp].CompNum == 183 || HanZiElementstrcs[RandNumTemp].CompNum == 184 || HanZiElementstrcs[RandNumTemp].CompNum == 185 || HanZiElementstrcs[RandNumTemp].CompNum == 187 || HanZiElementstrcs[RandNumTemp].CompNum == 188
	                || HanZiElementstrcs[RandNumTemp].CompNum == 189 || HanZiElementstrcs[RandNumTemp].CompNum == 190 || HanZiElementstrcs[RandNumTemp].CompNum == 191 || HanZiElementstrcs[RandNumTemp].CompNum == 193 || HanZiElementstrcs[RandNumTemp].CompNum == 195
	                || HanZiElementstrcs[RandNumTemp].CompNum == 197 || HanZiElementstrcs[RandNumTemp].CompNum == 199 || HanZiElementstrcs[RandNumTemp].CompNum == 200 || HanZiElementstrcs[RandNumTemp].CompNum == 201 || HanZiElementstrcs[RandNumTemp].CompNum == 202
	                || HanZiElementstrcs[RandNumTemp].CompNum == 204 || HanZiElementstrcs[RandNumTemp].CompNum == 205 || HanZiElementstrcs[RandNumTemp].CompNum == 206 || HanZiElementstrcs[RandNumTemp].CompNum == 207 || HanZiElementstrcs[RandNumTemp].CompNum == 208
	                || HanZiElementstrcs[RandNumTemp].CompNum == 212 || HanZiElementstrcs[RandNumTemp].CompNum == 214 || HanZiElementstrcs[RandNumTemp].CompNum == 216 || HanZiElementstrcs[RandNumTemp].CompNum == 217 || HanZiElementstrcs[RandNumTemp].CompNum == 218
	                || HanZiElementstrcs[RandNumTemp].CompNum == 219 || HanZiElementstrcs[RandNumTemp].CompNum == 220 || HanZiElementstrcs[RandNumTemp].CompNum == 222);	

		        //把下面的创建过程放到element中
	            yield return new WaitForSeconds(0.1f);
		        Vector3 scal = new Vector3(1.3f, 1.3f, 1f);
		        Transform trgo = (Transform)Instantiate(HanZiElement, new Vector2(HanZiKuangs[0, lie].HanZiKuangObject.transform.position.x , HanZiKuangs[0, lie].HanZiKuangObject.transform.position.y ), Quaternion.Euler(0f, 0f, 0f));
		        // HanZiElement.CreateElement(hanzielement,new Vector2(1.0f + i, 1.0f + i));
		        trgo.transform.localScale = scal;
		        GameObject go = trgo.gameObject;

	            if (go != null) {
	                
					//print("@@@@@@@@@@@@@ 101 go is not null!");
	                //go=HanZiElement.gameObject;
	                go.GetComponent<HanZiElement>().m_HanZiElementstrc = HanZiElementstrcs[RandNumTemp];   //注意list通过下标来实现//把找到符合调价的汉字赋值给了汉字元素类的对应变量
	                //print("#######016 HanZiElementstrcs[RandNumTemp].CompNum=" + HanZiElementstrcs[RandNumTemp].CompNum);
	                go.GetComponent<HanZiElement>().CreateElement(new Vector2(0.5f, 0.5f));
	                go.GetComponent<HanZiElement>().PlaySound_HanZi_Create();                   //播放汉字元素生成音效
	                // ElementVerticalFall(lie,0, ref fallrow);  //这个计算是从0开始计算才可以。

	                //关键是如何获得相应的文字框
	                go.GetComponent<HanZiElement>().m_parent = HanZiKuangs[fallrow02, lie].HanZiKuangObject.transform;   //把汉字框作为父类赋值给相应的汉字元素
	                //Vector3 StartPosition = new Vector3(HanZiKuangs[0, lie].HanZiKuangObject.transform.position.x, HanZiKuangs[0, lie].HanZiKuangObject.transform.position.y, -1);  //把文字元素放置到文字框上
	                //Vector3 EndPosition = new Vector3(HanZiKuangs[fallrow02, lie].HanZiKuangObject.transform.position.x, HanZiKuangs[fallrow02, lie].HanZiKuangObject.transform.position.y, -1);
	                //go.GetComponent<HanZiElement>().Move(StartPosition, EndPosition);  //传两个汉字框的函数进去即可,该方法有效，后面可以把动画特效做好点
					Vector3 StartPosition = new Vector3(0, fallrow02*1.6f, -1);
					Vector3 EndPosition = new Vector3(0,0, -1);
	                go.GetComponent<HanZiElement>().Move(StartPosition, EndPosition);
					//StartCoroutine(go.GetComponent<HanZiElement>().Move(StartPosition, EndPosition));
	                HanZiKuangs[fallrow02, lie].IsUsed = 1;
					
			
	         
	            }
		        
			}

		}
    
    }

    public IEnumerator CreatCompriseHanZi() {   //创建下半部分能够生成匹配的汉字
        

      	print("@@@@@@@@@@@@@ 105 CreatCompriseHanZi is start!" );
        Han_Zi_Ele_Struct[] hanzielemstrcs = new Han_Zi_Ele_Struct[5]; //根据上部分找到的汉字获得对应的汉字元素
        //int hanzielemstrcindex = 0;
        int[] CompriseNum01 = { 0, 0, 0 };
        int[] CompriseNum02 = { 0, 0, 0 };
        int CompriseCount01 = 0;
        int CompriseCount02 = 0;
        //随机获取上部分最后一行需要组成文字的位置。****注意后面实现该位置向后查询是否有多个文字元素与下面部分组成文字的功能。
        //下半部分生成包含可以组成文字的文字元素
        //根据上半部分最后一行的文字元素查找到能组成文字的下部分文字元素
		
        Han_Zi_Position_Struct[] PosNumArr = new Han_Zi_Position_Struct[6];
        for (int i = 0; i < 6; i++)
        {
            PosNumArr[i].HanZiNum = 0;
            PosNumArr[i].flag = 0;

        }
		yield return new WaitForSeconds(1.5f);
        do
        {
        	
            int temp = UnityEngine.Random.Range(0, 5);
            //print("******014 temp01=" + temp);
            HanZiElement hanzielem = HanZiKuangs[5, temp].HanZiKuangObject.transform.GetChild(0).GetComponent<HanZiElement>(); //逐个获得父类文字框下的汉字元素类
            hanzielemstrcs[0] = hanzielem.m_HanZiElementstrc;
            //print("******0116 hanzielemstrcs.CompNum_01=" + hanzielemstrcs[0].CompNum);
            GetCompariseHanZiElem(hanzielemstrcs, ref CompriseNum01);   //获得下部分第一个组成文字的相关索引
           
			//print("******0117 CompriseNum01[0]=" + CompriseNum01[0]+"  CompriseNum01[0]="+CompriseNum01[1]);
			
            temp = UnityEngine.Random.Range(0, 5);
           //print("******015 temp02=" + temp);
            hanzielem = HanZiKuangs[5, temp].HanZiKuangObject.transform.GetChild(0).GetComponent<HanZiElement>(); //逐个获得父类文字框下的汉字元素类
            hanzielemstrcs[0] = hanzielem.m_HanZiElementstrc;                                        //目前上部分暂时只传入一个汉字元素进行比较
           // print("******0118 hanzielemstrcs.CompNum_02=" + hanzielemstrcs[0].CompNum);
            GetCompariseHanZiElem(hanzielemstrcs, ref CompriseNum02);   //获得下部分第二个组成文字的相关索引
			print("******0119 CompriseNum02[0]=" + CompriseNum02[0]+"  CompriseNum02[0]="+CompriseNum02[1]);
            //把下面部分要生成的文字放到相关位置
            //下面的实现方法感觉不好，可以建一个有标记的结构体数组，标记表示连续两个元素是否可以拆开。先把所有文字从0到5按顺序填上，然后循环移动随机个数即可。
            for (int i = 0; i < 3; i++)
            {
                if (CompriseNum01[i] != 0)
                {

                    CompriseCount01++;

                }
            }
            for (int i = 0; i < 3; i++)
            {
                if (CompriseNum02[i] != 0)
                {

                    CompriseCount02++;

                }
            }

        } while (CompriseCount01 == 0 && CompriseCount02 == 0);   //如果找的两个元素都不能组成文字，则重新生成。
        
    	//更换计算好的文字元素的位置
        Han_Zi_Position_Struct tempPosStrc = new Han_Zi_Position_Struct();
        tempPosStrc.HanZiNum=0;
        tempPosStrc.flag=0;
        print("******029 CompriseCount01=" + CompriseCount01);
        print("******030 CompriseCount02=" + CompriseCount02);

        int RandPosNum01 = UnityEngine.Random.Range(0, 6 - CompriseCount01);
		if(RandPosNum01 <6) {
			for (int i = 0; i < CompriseCount01; i++)
        	{
	            PosNumArr[RandPosNum01].HanZiNum = CompriseNum01[i];
	            PosNumArr[RandPosNum01].flag=1;
	            RandPosNum01++;
        	} 

		}              //实现第一个汉字元素的排列。
                                      
        
        int RandPosNum02 = 0;                  //用该方法会出现死循环
        if (CompriseCount02 > 1) //实际上是等于2
        {           
            do
            {

                RandPosNum02 = UnityEngine.Random.Range(0, 5);
                 //print("******036 RandPosNum02=" + RandPosNum02);

            } while ((PosNumArr[RandPosNum02].flag == 1) || (PosNumArr[RandPosNum02 + 1].flag == 1) ||(RandPosNum02 + CompriseCount02 >= 6));   //走该分支时会出现死循环
           

        }
        else if (CompriseCount02==1)
        {
            do
            {

                RandPosNum02 = UnityEngine.Random.Range(0, 5);
                //print("******037 RandPosNum02=" + RandPosNum02);

            } while ((PosNumArr[RandPosNum02].flag == 1));
        }
       // print("******038 RandPosNum02=" + RandPosNum02);
        for(int i=0;i<CompriseCount02;i++){
            PosNumArr[RandPosNum02].HanZiNum = CompriseNum02[i];
            PosNumArr[RandPosNum02].flag = 2;
            //print("******39 PosNumArr" + "[" + RandPosNum02 + "]" + "=" + PosNumArr[RandPosNum02].HanZiNum);
            RandPosNum02++;

        
        
        }

        //下面是下部分不组成文字的随机文字生成。
        for (int i = 0; i < 6; i++) {
            if (PosNumArr[i].flag == 0) { 
                int RandNumother; //之前下部分中总是概率出现对象未初始化问题，是因为没有去除下面这些编号
               
                RandNumother = UnityEngine.Random.Range(1, 223);//注意list通过下标来实现，所以下标从0开始有对应问题
                PosNumArr[i].HanZiNum = RandNumother;
                print("******40 RandNumotherTemp=" + RandNumother);
                //print("******41 PosNumArr[i].HanZiNum=" + PosNumArr[i].HanZiNum);

				//??????????? 这里的随机数有问题，还是会出现对象创建为空的情况，主要是因为上面的数字实际是list下标，并不是直接对应汉字元素编号。所以可能出现问题现象，如随机140实际对应的是150.
				

				
            }
        
        
        } 

        for (int i = 0; i < 6; i++)
        {
            //print("******40 PosNumArr" + "[" + i + "]" + "=" + PosNumArr[i].HanZiNum);
            //print("******41 PosNumArr" + "[" + i + "].flag" + "=" + PosNumArr[i].flag);
            //if (PosNumArr[i].flag != 0){   //把不必查询的排除，减少时间               汉字元素有搜寻和没搜寻之间会有时序问题，导致最终位置概率性出错。目前暂时用这种方法，后面再考虑优化。
              // print("******42 PosNumArr" + "[" + i + "]" + "=" + PosNumArr[i].HanZiNum);
            foreach (Han_Zi_Ele_Struct hanzielementstrc in HanZiElementstrcs) //后面改为数组可以节省一些不必要的查找
            {

                //按要求随机生成文字元素
                if (hanzielementstrc.CompNum == PosNumArr[i].HanZiNum)
                {

                    Vector3 scal = new Vector3(1.3f, 1.3f, 1f);
                    Transform trgo = (Transform)Instantiate(HanZiElement, new Vector2(HanZiKuangs[8, i].HanZiKuangObject.transform.position.x, HanZiKuangs[8, i].HanZiKuangObject.transform.position.y -5f), Quaternion.Euler(0f, 0f, 0f));

                    trgo.transform.localScale = scal;
                    GameObject go = trgo.gameObject;
                    if (go != null) {
                        go.GetComponent<HanZiElement>().m_HanZiElementstrc = hanzielementstrc;               //把找到符合调价的汉字赋值给了汉字元素类的对应变量
                        go.GetComponent<HanZiElement>().CreateElement(new Vector2(0.5f, 0.5f));
                        go.GetComponent<HanZiElement>().PlaySound_HanZi_Create();
                        
                        go.GetComponent<HanZiElement>().m_parent = HanZiKuangs[8, i].HanZiKuangObject.transform;   //把汉字框作为父类赋值给相应的汉字元素   //这个设置父类应该有时序问题，低概率会出现子类文字元素相对父类文字框的坐标不为0，,这也可能与子类相对父类坐标有关。
                        Vector3 StartPosition = new Vector3(0, -3, -1);  //把文字元素放置到文字框上
                        Vector3 EndPosition = new Vector3(0,0, -1);
                        go.GetComponent<HanZiElement>().Move(StartPosition, EndPosition);  //传两个汉字框的函数进去即可,该方法有效，后面可以把动画特效做好点
                        
                        // StartCoroutine(go.GetComponent<HanZiElement>().Move(StartPosition, EndPosition));
                        HanZiKuangs[8, i].IsUsed = 1;
                        //break;
                    
                    }
            	}

        	}
            
        }

        //把PosNumArr[]中的文字元素重新排一下顺序。只对算计好的两个文字元素进行重新随机放置
              
    }

    //删除及下落代码可以参考消消乐。
    void DeleteHanZiElem(int row,int clo){
        
        //print("******************* 49 HanZiKuangs[" + row + "," + clo + "].IsUsed=" + HanZiKuangs[row, clo].IsUsed);
        if(HanZiKuangs[row, clo].HanZiKuangObject.transform.childCount!=0){
			for (int i = 0; i <  HanZiKuangs[row, clo].HanZiKuangObject.transform.childCount; i++) {  
				if(HanZiKuangs[row, clo].IsUsed==1){       //这个条件要不要都可以
					GameObject TempGameObject = HanZiKuangs[row, clo].HanZiKuangObject.transform.GetChild(i).gameObject;
	        	 	Destroy(TempGameObject);
				}				 
			}
		}
        if(HanZiKuangs[row, clo].IsCount==0){
			HanZiKuangs[row, clo].IsUsed = 0;
			HanZiKuangs[row, clo].IsCountDelete=0;
		}  
    }
	
	void PlaySound_HanZi_DanJi(){

		 m_audio.PlayOneShot(Soud_Dan_Ji);
	}
	
 	 void PlaySound_HanZi_HeCheng()
    {

        m_audio.PlayOneShot(Sound_He_Cheng);

    }
	
	
	//public float timer=1.5f;
 	//int timerstart=0;
	// Update is called once per frame
	Han_Zi_Ele_Struct HanZiStrcNew=new Han_Zi_Ele_Struct();

	Vector3 DaoHeng_startPosUp=new Vector3(0,0,0);
    Vector3 DaoHeng_startPosDown = new Vector3(0, 0, 0);
    Vector3 DaoHeng_EndPos = new Vector3(0, 0, 0);
	void Update () {
	//查找能否组成新的文字时，通过查找Index结构体中有没有一个数一致，没有就返回，有的话继续查找另一个。
	/*	if(timerstart==1){

			timer -= Time.deltaTime;
			if (timer <= 0) {
				StartCoroutine(CreatCompriseHanZi());
				timer =1.5f;
				timerstart=0;
			}
		}    */
			
/*		 if (Input.GetMouseButtonUp(0)){

			 timerstart=1;
            //消除选定的可以组成新汉字的文字元素
            for (int i = 0; i < 6; i++) {                //消除下部分所有汉字元素。
            DeleteHanZiElem(8,i);
            }
		 }                                                     */
       //实现文字框选定及相关特效
        if (Input.GetMouseButtonDown(0)){
         
           Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
           RaycastHit hit;
           if(!Physics.Raycast(ray,out hit))
           {
                  
                return;
           }
         
           StartHanZiKuangRow = hit.collider.gameObject.GetComponent<HanZiKuang>().M_HanZiKuang.rowindex;
           StartHanZiKuangCol = hit.collider.gameObject.GetComponent<HanZiKuang>().M_HanZiKuang.cloindex;
           //print("******************* 19 StartPosition=" + StartHanZiKuangRow);
           hasdown = true;
        }

        if (Input.GetMouseButtonUp(0)){
            if (hasdown)
            {
                PlaySound_HanZi_DanJi();       //汉字框被单击时的声效。
                hasdown = false;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (!Physics.Raycast(ray, out hit))
                {
                    return;
                }
                //为了按汉字框的个数选定文字框
                EndHanZiKuangRow = hit.collider.gameObject.GetComponent<HanZiKuang>().M_HanZiKuang.rowindex;
                EndHanZiKuangCol = hit.collider.gameObject.GetComponent<HanZiKuang>().M_HanZiKuang.cloindex;
                /*
               // print("******************* 20 EndHanZiKuangRow=" + EndHanZiKuangRow);
                //Vector3 temp = new Vector3((StartHanZiKuang.GetComponent<HanZiKuang>().M_HanZiKuang.x, 0, 0);
                //横向选定
               // HuaXianObject.transform.position = new Vector3((HanZiKuangs[EndHanZiKuangRow, EndHanZiKuangCol].HanZiKuangObject.transform.position.x - HanZiKuangs[StartHanZiKuangRow, StartHanZiKuangCol].HanZiKuangObject.transform.position.x) / 2, 0, 0);
                //HuaXianObject.transform.localScale = new Vector3((HanZiKuangs[EndHanZiKuangRow, EndHanZiKuangCol].HanZiKuangObject.transform.position.x - HanZiKuangs[StartHanZiKuangRow, StartHanZiKuangCol].HanZiKuangObject.transform.position.x) / 2, 4f, 0);
               // HuaXianObject.gameObject.SetActive(true);
                //Invoke("SetActivityDesable", 0.5f);  //定时把选线去显示
               // print("******************* 21 childname=" + hit.collider.gameObject.transform.GetChild(0).name);
                */



                // print("******************* 31 StartHanZiKuangRow=" + StartHanZiKuangRow);
                // print("******************* 32 EndHanZiKuangRow=" + EndHanZiKuangRow);
                print("******************* 33 StartHanZiKuangCol=" + StartHanZiKuangCol);
                print("******************* 34 EndHanZiKuangCol=" + EndHanZiKuangCol);
               
                if (StartHanZiKuangRow == 5 && EndHanZiKuangRow==5){//记录上下部分选定的文字元素
                    StartHanZiKuangCloUp = StartHanZiKuangCol;
                    EndHanZiKuangCloUp = EndHanZiKuangCol;
                }
               /*if (StartHanZiKuangRow == 8 && EndHanZiKuangRow == 8)   下部分不需要记录，直接全部清掉。
                {
                    EndHanZiKuangCloDown = StartHanZiKuangCol;
                    EndHanZiKuangCloDown = EndHanZiKuangCol;
                }*/


                GameObject tempobject;
                Vector3 temppos;
				//删除同一区域选定的元素
                for (int i = StartHanZiKuangRow; i <= EndHanZiKuangRow; i++) {  //行
                    if (i == 5) {   //上部分只要第5行就行了，后面可以把上面的代码优化一下，看是否需要第5行之外的选定。
                        if(UpIsSelect){
						   	
							for (int m = 0; m < 9; m++)
							{	
								TempSelectHanZiUp[m].FirstNum = 0;
								TempSelectHanZiUp[m].SecondNum = 0;
								TempSelectHanZiUp[m].CompNum = 0;
								TempSelectHanZiUp[m].image = null;
								TempSelectHanZiUp[m].CompCount = 0;


								   
								if(TempSelectHanZiKuangUp[m].hang!=0xff){
                                    tempobject = HanZiKuangs[TempSelectHanZiKuangUp[m].hang, TempSelectHanZiKuangUp[m].lie].HanZiKuangObject;
                                    tempobject.transform.localScale = OriginScal;
                                    temppos = tempobject.transform.position;
                                    temppos.z = 0;
                                    tempobject.transform.position = temppos;
									

									TempSelectHanZiKuangUp[m].hang=0xff;
								   	TempSelectHanZiKuangUp[m].lie=0xff;
								}
								   
							}
							UpIsSelect=false;
							 
						}
                    }

					if (i == 8) {   //上部分只要第5行就行了，后面可以把上面的代码优化一下，看是否需要第5行之外的选定。
                        if(DownIsSelect){
						   	
							for (int m = 0; m < 9; m++)
							{
								TempSelectHanZiDown[m].FirstNum = 0;
								TempSelectHanZiDown[m].SecondNum = 0;
								TempSelectHanZiDown[m].CompNum = 0;
								TempSelectHanZiDown[m].image = null;
								TempSelectHanZiDown[m].CompCount = 0;

								if (TempSelectHanZiKuangDown[m].hang !=0xff ){
                                    tempobject = HanZiKuangs[TempSelectHanZiKuangDown[m].hang, TempSelectHanZiKuangDown[m].lie].HanZiKuangObject;
                                    tempobject.transform.localScale = OriginScal;
                                    temppos = tempobject.transform.position;
                                    temppos.z =0;
                                    tempobject.transform.position = temppos;
	
								   	TempSelectHanZiKuangDown[m].hang=0xff;
									TempSelectHanZiKuangDown[m].lie=0xff;
								}
								   
							}
							DownIsSelect=false;

						}
                    }

                }
				
				
				int TempIntexUp=0;
				int TempIntexDown=0;
				//更新选定的文字元素
                for (int i = StartHanZiKuangRow; i <= EndHanZiKuangRow; i++) {  //行
				
                    for (int j = StartHanZiKuangCol; j <= EndHanZiKuangCol; j++) //列
                    {
                         if (i == 5) {   //上部分只要第5行就行了，后面可以把上面的代码优化一下，看是否需要第5行之外的选定 (暂时不要，会增加复杂度不利于玩家玩)
                            if (HanZiKuangs[i, j].HanZiKuangObject.transform.childCount != 0)
                            {
                                TempSelectHanZiUp[TempIntexUp] = HanZiKuangs[i, j].HanZiKuangObject.transform.GetChild(0).GetComponent<HanZiElement>().m_HanZiElementstrc;
								TempSelectHanZiKuangUp[TempIntexUp].hang=i;
								TempSelectHanZiKuangUp[TempIntexUp].lie=j;
                                //print("!!!!!!!!!!!!!!!! 41 select UP HanZiKuangs" + "[" + i + "]," + "[" + j+"]");
                                
                                UpIsSelect = true;

                                tempobject = HanZiKuangs[TempSelectHanZiKuangUp[TempIntexUp].hang, TempSelectHanZiKuangUp[TempIntexUp].lie].HanZiKuangObject;
                                tempobject.transform.localScale = SelectScal;
                                temppos = tempobject.transform.position;
                                temppos.z = -1;
                                tempobject.transform.position = temppos;

								
								DaoHeng_startPosUp=HanZiKuangs[i, j].HanZiKuangObject.transform.position;
								TempIntexUp++;
                        
                            }
                       
                        
	                     }
	                     if (i == 8)
	                     {   //上部分只要第5行就行了，后面可以把上面的代码优化一下，看是否需要第5行之外的选定
	                         if (HanZiKuangs[i, j].HanZiKuangObject.transform.childCount!=0)
	                         {
	                             TempSelectHanZiDown[TempIntexDown] = HanZiKuangs[i, j].HanZiKuangObject.transform.GetChild(0).GetComponent<HanZiElement>().m_HanZiElementstrc;
								 TempSelectHanZiKuangDown[TempIntexDown].hang=i;
								 TempSelectHanZiKuangDown[TempIntexDown].lie=j;
								 //print("!!!!!!!!!!!!!!!! 42 select DOWN HanZiKuangs" + "[" + i + "]," + "[" + j + "]");
	                             
	                             DownIsSelect = true;

                                 tempobject = HanZiKuangs[TempSelectHanZiKuangDown[TempIntexDown].hang, TempSelectHanZiKuangDown[TempIntexDown].lie].HanZiKuangObject;
                                 tempobject.transform.localScale = SelectScal;
                                 temppos = tempobject.transform.position;
                                 temppos.z = -1;
                                 tempobject.transform.position = temppos;

                                 DaoHeng_startPosDown=HanZiKuangs[i, j].HanZiKuangObject.transform.position;
								 TempIntexDown++;
	                         
	                         }
	                        
	                    }

                  }
                }

                if (UpIsSelect && DownIsSelect) {
                    UpIsSelect = false;
                    DownIsSelect = false;
                    TempIntexUp = 0;               
                    TempIntexDown = 0;
                    int ret = 0;
					HanZiStrcNew.CompCount=0;
					HanZiStrcNew.CompNum=0;
					HanZiStrcNew.FirstNum=0;
					HanZiStrcNew.FiveNum=0;
					HanZiStrcNew.ForthNum=0;
					HanZiStrcNew.image=null;
					HanZiStrcNew.SecondNum=0;
					HanZiStrcNew.ThirdNum=0;
					
                    while (TempSelectHanZiUp[TempIntexUp].CompNum != 0) {
                        TempIntexUp++;
                    }
                    while (TempSelectHanZiDown[TempIntexDown].CompNum != 0)
                    {
                        TempIntexDown++;
                    }
                    if(TempIntexUp+TempIntexDown>3){
                        print("the select han zi element other than 3!");
                    
                    }

                    print("******44 TempIntexUp=" + TempIntexUp);
                    print("******45 TempIntexDown=" + TempIntexDown);
					
                  

                    if(TempIntexUp==1&&TempIntexDown==1){
                        
                       ret=IsCompriseHanZi(TempSelectHanZiUp[0].CompNum,TempSelectHanZiDown[0].CompNum,0,0,0,2,ref HanZiStrcNew); //应该用一个数组来存放所有搜到符合的元素
                       if (ret ==0) {
                          print("Get Comprise HanZi error! " );
					   
                       }
                    
                    }
                    if (TempIntexUp == 2 && TempIntexDown == 1) {

                       ret = IsCompriseHanZi(TempSelectHanZiUp[0].CompNum, TempSelectHanZiUp[1].CompNum, TempSelectHanZiDown[0].CompNum, 0, 0, 3,ref HanZiStrcNew);  
                       if (ret ==0)
                       {
                           print("Get Comprise HanZi error! ");
                       }
                    
                    }
                    if (TempIntexUp == 1 && TempIntexDown == 2)
                    {

                       ret = IsCompriseHanZi(TempSelectHanZiUp[0].CompNum, TempSelectHanZiDown[0].CompNum, TempSelectHanZiDown[1].CompNum, 0, 0, 3,ref HanZiStrcNew);
                       if (ret ==0)
                       {
                           print("Get Comprise HanZi error! ");
                       }

                    }
                   // print("******43 HanZiStrcNew=" + HanZiStrcNew);
					
					//HanZiStrcNew=HanZiElementstrcs[100];                    //定位用
                  if (HanZiStrcNew.CompNum != 0) {

					//组成文字后也要清楚选定
				  	for (int i=0;i<9;i++){
							
						if(TempSelectHanZiKuangUp[i].hang!=0xff){

	                        tempobject = HanZiKuangs[TempSelectHanZiKuangUp[i].hang, TempSelectHanZiKuangUp[i].lie].HanZiKuangObject;
							tempobject.transform.localScale=OriginScal;
	                        temppos=tempobject.transform.position;
	                        temppos.z=0;
	                        tempobject.transform.position=temppos;

							

						}
						if(TempSelectHanZiKuangDown[i].hang!=0xff){
							//HanZiKuangs[TempSelectHanZiKuangDown[i].hang,TempSelectHanZiKuangDown[i].lie].HanZiKuangObject.transform.position.z=0;
	                        tempobject = HanZiKuangs[TempSelectHanZiKuangDown[i].hang, TempSelectHanZiKuangDown[i].lie].HanZiKuangObject;
	                        tempobject.transform.localScale = OriginScal;
	                        temppos = tempobject.transform.position;
	                        temppos.z = 0;
	                        tempobject.transform.position = temppos;

						}

					}
					//选定的上下文字元素不能组成文字时也要进行清除。

					for (int m = 0; m < 9; m++)
					{	
					   TempSelectHanZiUp[m].FirstNum = 0;
					   TempSelectHanZiUp[m].SecondNum = 0;
					   TempSelectHanZiUp[m].CompNum = 0;
					   TempSelectHanZiUp[m].image = null;
					   TempSelectHanZiUp[m].CompCount = 0;

					   TempSelectHanZiKuangUp[m].hang=0xff;
					   TempSelectHanZiKuangUp[m].lie=0xff;
					}
					UpIsSelect=false;

					for (int m = 0; m < 9; m++)
					{
					   TempSelectHanZiDown[m].FirstNum = 0;
					   TempSelectHanZiDown[m].SecondNum = 0;
					   TempSelectHanZiDown[m].CompNum = 0;
					   TempSelectHanZiDown[m].image = null;
					   TempSelectHanZiDown[m].CompCount = 0;

					   TempSelectHanZiKuangDown[m].hang=0xff;
					   TempSelectHanZiKuangDown[m].lie=0xff;
						   
					}
					DownIsSelect=false;
					//上述实现对应的清除操作
					
					int IsHanZiDeleteUp=0;
					
					for(int i = StartHanZiKuangCloUp; i <= EndHanZiKuangCloUp; i++){  //汉字框有count的情况
						if(HanZiKuangs[5,i].IsCount !=0){
							DeleteHanZiElem(5, i);
                            HanZiKuangCounts[HanZiKuangs[5, i].IsCount].gameObject.SetActive(false);
						
							Vector3 scal = new Vector3(1.2f, 1.2f, 1f);
							//在对应有count的汉字框中生成新的合成的汉字元素
	                    	Transform trgo = (Transform)Instantiate(HanZiElement, new Vector2(HanZiKuangs[5, i].HanZiKuangObject.transform.position.x , HanZiKuangs[5, i].HanZiKuangObject.transform.position.y), Quaternion.Euler(0f, 0f, 0f));

	                    	trgo.transform.localScale = scal;
	                    	GameObject go = trgo.gameObject;

	                    	go.GetComponent<HanZiElement>().m_HanZiElementstrc = HanZiStrcNew;               //把找到符合调价的汉字赋值给了汉字元素类的对应变量
	                    	go.GetComponent<HanZiElement>().CreateElement(new Vector2(0.5f, 0.5f));
							go.GetComponent<HanZiElement>().m_parent = HanZiKuangs[5,i].HanZiKuangObject.transform;
							go.GetComponent<HanZiElement>().SetParent();

							HanZiKuangs[5,i].IsCount--;

							if(HanZiKuangs[5,i].IsCount!=0){

                                HanZiKuangCounts[HanZiKuangs[5, i].IsCount].gameObject.SetActive(true);
						
							}
							

							 //消除选定的可以组成新汉字的文字元素（各种趣味玩法消除对应的汉字元素）
	                    	for (int m = 0; m < 6; m++) {                //消除下部分所有汉字元素。
	                    
	                        	DeleteHanZiElem(8,m);      
	                    	}
	                   		for (int n = StartHanZiKuangCloUp; n <= EndHanZiKuangCloUp; n++) {//消除上部分汉字元素
	                    	
		                   
		                        DeleteHanZiElem(3, n);                  //添加消除音效
							    DeleteHanZiElem(2, n);
	                    									
	                    	}
						
		                    //下落
		                    HanZiElemFallInUp();
							//上部分重新生成文字
						    for (int j = 0; j < 6; j++) {
								 StartCoroutine(CreatHanZiElemAndFallUp(j));
							}

							Invoke("CreateHanZiCount", 1.8f);     //随机生成汉字框count
							
							//生成下部分汉字元素
						    StartCoroutine(CreatCompriseHanZi());



							
						}else{

							IsHanZiDeleteUp=1;
						}
						

					}
					
					if(IsHanZiDeleteUp==1){       //可以消除时
						Vector3 scal = new Vector3(1.2f, 1.2f, 1f);                               
	                    Transform trgo = (Transform)Instantiate(HanZiElement, new Vector2(/*HanZiKuangs[1, 1].HanZiKuangObject.transform.position.x*/ 6f, -16f/*HanZiKuangs[1, 1].HanZiKuangObject.transform.position.y - 0.16f*/), Quaternion.Euler(0f, 0f, 0f));

	                    trgo.transform.localScale = scal;
	                    GameObject go = trgo.gameObject;

	                    go.GetComponent<HanZiElement>().m_HanZiElementstrc = HanZiStrcNew;               //把找到符合调价的汉字赋值给了汉字元素类的对应变量
	                    go.GetComponent<HanZiElement>().CreateElement(new Vector2(0.5f, 0.5f));
	                    

	                    DaoHeng_EndPos = new Vector3(6f, -16f, 0);
	                    //print("$$$$$$$$$$$$$$$$ DaoHeng_startPosUp " + DaoHeng_startPosUp);
	                    Hua_xian_DaoHengUp.GetComponent<XuanZheXian>().HanZiHeChengDaoHeng(DaoHeng_startPosUp, DaoHeng_EndPos);

	                    DaoHeng_EndPos = new Vector3(8.3f, -15.24f, 0);
	                    DaoHeng_startPosDown = new Vector3(DaoHeng_startPosDown.x - 1.31f, DaoHeng_startPosDown.y -1f,0);
	                    Hua_xian_DaoHengDown.GetComponent<XuanZheXian>().HanZiHeChengDaoHeng(DaoHeng_startPosDown, DaoHeng_EndPos);
						
						PlaySound_HanZi_HeCheng();                   //文字合成音效，后续可以加上一些趣味语音
	                    

	                    //消除选定的可以组成新汉字的文字元素（各种趣味玩法消除对应的汉字元素）
	                    for (int i = 0; i < 6; i++) {                //消除下部分所有汉字元素。
	                    
	                        DeleteHanZiElem(8,i);      
	                    }
	                    for (int i = StartHanZiKuangCloUp; i <= EndHanZiKuangCloUp; i++) {//消除上部分汉字元素
	                    	
	                        DeleteHanZiElem(5, i);
	                        DeleteHanZiElem(3, i);                  //添加消除音效
						    DeleteHanZiElem(2, i);
	                    									
	                    }
						
	                    //下落
	                    HanZiElemFallInUp();
						//上部分重新生成文字
					    for (int j = 0; j < 6; j++) {
							 StartCoroutine(CreatHanZiElemAndFallUp(j));
						}

						Invoke("CreateHanZiCount", 1.8f);     //随机生成汉字count
						
						//生成下部分汉字元素
					    StartCoroutine(CreatCompriseHanZi());

	                   //间隔1秒后新生成的文字消除
	                   Destroy(go, 2f);



					}
                    
                            
               	}else{
					//下述实现清楚操作
					for (int i=0;i<9;i++){
							
						if(TempSelectHanZiKuangUp[i].hang!=0xff){
	                        tempobject = HanZiKuangs[TempSelectHanZiKuangUp[i].hang, TempSelectHanZiKuangUp[i].lie].HanZiKuangObject;
	                        tempobject.transform.localScale = OriginScal;
	                        temppos = tempobject.transform.position;
	                        temppos.z = 0;
	                        tempobject.transform.position=temppos;
						}
						if(TempSelectHanZiKuangDown[i].hang!=0xff){
	                        tempobject = HanZiKuangs[TempSelectHanZiKuangDown[i].hang, TempSelectHanZiKuangDown[i].lie].HanZiKuangObject;
							//HanZiKuangs[TempSelectHanZiKuangDown[i].hang,TempSelectHanZiKuangDown[i].lie].HanZiKuangObject.transform.position.z=0;

	                        tempobject.transform.localScale = OriginScal;
	                        temppos = tempobject.transform.position;
	                        temppos.z = 0;
	                        tempobject.transform.position = temppos;
						}

					}
					
					//选定的上下文字元素不能组成文字时也要进行清除。
					for (int m = 0; m < 9; m++)
				    {	
						TempSelectHanZiUp[m].FirstNum = 0;
						TempSelectHanZiUp[m].SecondNum = 0;
						TempSelectHanZiUp[m].CompNum = 0;
						TempSelectHanZiUp[m].image = null;
						TempSelectHanZiUp[m].CompCount = 0;

						TempSelectHanZiKuangUp[m].hang=0xff;
						TempSelectHanZiKuangUp[m].lie=0xff;
					}
					UpIsSelect=false;
					for (int m = 0; m < 9; m++)
					{
						TempSelectHanZiDown[m].FirstNum = 0;
						TempSelectHanZiDown[m].SecondNum = 0;
						TempSelectHanZiDown[m].CompNum = 0;
						TempSelectHanZiDown[m].image = null;
						TempSelectHanZiDown[m].CompCount = 0;

						TempSelectHanZiKuangDown[m].hang=0xff;
						TempSelectHanZiKuangDown[m].lie=0xff;
						   
					}
					DownIsSelect=false;
					}              
                }       
            }
        }					
	}

    public void HanZiElemFallInUp() {
        int EmptyPos = 0;
        for (int x= 0; x < 6; x++) {   //文字框的列
            int IsFirstEmpty = 0;
			int hang=0;
            for (int y = 0; y <=5; y++) { //文字框的行
            	hang=5-y;
            		
                if (HanZiKuangs[hang, x].IsUsed == 0 && IsFirstEmpty == 0)
                {
                    
                    EmptyPos = hang;               //找到第一个为空的位置
                    IsFirstEmpty = 1;
                    //print("******************* 45 EmptyPos=" + EmptyPos);

                }
                if (HanZiKuangs[hang, x].IsUsed == 1 && IsFirstEmpty==1)
                {


                    GameObject go = HanZiKuangs[hang, x].HanZiKuangObject.transform.GetChild(0).gameObject;
                   // print("******************* 50 go.transform.position=" + go.transform.position);
                    //HanZiKuangs[y, x].HanZiKuangObject.transform.parent=null;
                   // print("******************* 51 go.transform.position=" + go.transform.position);
    //                print("******************* 46 HanZiKuangs[" + hang+","+x + "]" );
                    if (go != null) {
                        int FallowNum=0;
                        HanZiKuangs[hang, x].IsUsed = 0;           //遗漏了这个条件，弄了挺久。
                        //print("******************* 47 HanZiKuangs[" + EmptyPos + "," + x + "]");
                        go.GetComponent<HanZiElement>().m_parent = HanZiKuangs[EmptyPos, x].HanZiKuangObject.transform;   //把汉字框作为父类赋值给相应的汉字元素   //这个设置父类应该有时序问题，低概率会出现子类文字元素相对父类文字框的坐标不为0，,这也可能与子类相对父类坐标有关。
                        
                        //print("******************* 54 go.transform.position=" + go.transform.position);
                        //Vector3 StartPosition = new Vector3(HanZiKuangs[y, x].HanZiKuangObject.transform.position.x, HanZiKuangs[y, x].HanZiKuangObject.transform.position.y - 2.4f, -1);  //把文字元素放置到文字框上
                        ElementVerticalFall(x, hang, ref FallowNum);   //获得当前文字元素需要下落的框数。有问题。
   //                     print("******************* 54 FallowNum=" + FallowNum);
                        Vector3 StartPosition = new Vector3(0, 1.6f * (FallowNum) , -1);         //这里的坐标采用的是父类的相对坐标才能按正确的方式下落。后面优化时把动画修改一下，采用自由落体的方式让运动能够更有动感
                        //print("******************* 52 StartPosition=" + StartPosition);
                        //Vector3 EndPosition = new Vector3(HanZiKuangs[EmptyPos, x].HanZiKuangObject.transform.position.x, HanZiKuangs[EmptyPos, x].HanZiKuangObject.transform.position.y, -1);
                        Vector3 EndPosition = new Vector3(0, 0, -1);
                        //print("******************* 53 EndPosition=" + EndPosition);
                        //go.GetComponent<HanZiElement>().Move(StartPosition, EndPosition);  //传两个汉字框的函数进去即可,该方法有效，后面可以把动画特效做好点

                        go.GetComponent<HanZiElement>().Move(StartPosition, EndPosition);
                        //StartCoroutine(go.GetComponent<HanZiElement>().Move(StartPosition, EndPosition));  
                        HanZiKuangs[EmptyPos, x].IsUsed = 1;           
                  
                        EmptyPos--;
                    
                    }
                    

                }
            }
        }
    }





   //鼠标移动
   //调用Move()方法
   //设置方向
	//void Move

	//通过传入两个元素的compnum查找list中有组成文字的元素，并返回该元素
	public int IsCompriseHanZi(int a,int b,int c,int d,int e,int selectcount,ref Han_Zi_Ele_Struct obj) {  //先按是有三个元素来比较

    	foreach(Han_Zi_Ele_Struct hanzi in HanZiElementstrcs)
		{   
			/*if((a!=0xffff)&&(a==hanzi.FirstNum||a==hanzi.SecondNum||a==hanzi.ThirdNum||a==hanzi.ForthNum||a==hanzi.FiveNum))
			{
                if ((b!= 0xffff) && (b == hanzi.FirstNum || b == hanzi.SecondNum || b == hanzi.ThirdNum || b == hanzi.ForthNum || b == hanzi.FiveNum))
				{
                    if ((c != 0xffff) && (c == hanzi.FirstNum || c == hanzi.SecondNum || c == hanzi.ThirdNum || c == hanzi.ForthNum || c == hanzi.FiveNum))
                    
                    obj= hanzi;
					return 1;
				}

			}*/
            if (selectcount == 2) {
                if (a == hanzi.FirstNum)
                {
                    if (b == hanzi.SecondNum)
                    {
                       obj = hanzi;
                        return hanzi.CompNum;                   //********这个应该不能直接return，应该继续查找，后面优化
                    }
                }
                if (a == hanzi.SecondNum)
                {
                    if (b == hanzi.FirstNum)
                    {
                       obj = hanzi;
                        return 1;
                    }
                }
            
            
            }
            if (selectcount == 3) {
                if (a == hanzi.FirstNum)
                {
                    if (b == hanzi.SecondNum)
                    {
                        if (c == hanzi.ThirdNum) {
                            obj = hanzi;
                            return 1;
                        
                        }

                    }
                    if (b == hanzi.ThirdNum) {
                        if (c == hanzi.SecondNum) {
                            obj = hanzi;
                            //complNum = hanzi.CompNum;
                            return 1;
                        }
                    
                    }

                }

                if (a == hanzi.SecondNum)
                {
                    if (b == hanzi.FirstNum)
                    {
                        if (c == hanzi.ThirdNum)
                        {
                            obj = hanzi;
                           //complNum = hanzi.CompNum;
                            return 1;

                        }

                    }
                    if (b == hanzi.ThirdNum)
                    {
                        if (c == hanzi.FirstNum)
                        {
                            obj = hanzi;
                            //complNum = hanzi.CompNum;
                            return 1;
                        }

                    }

                }

                if (a == hanzi.ThirdNum)
                {
                    if (b == hanzi.FirstNum)
                    {
                        if (c == hanzi.SecondNum)
                        {
                            obj = hanzi;
                           //complNum = hanzi.CompNum;
                            return 1;

                        }

                    }
                    if (b == hanzi.SecondNum)
                    {
                        if (c == hanzi.FirstNum)
                        {
                            obj = hanzi;
                           //complNum = hanzi.CompNum;
                            return 1;
                        }

                    }

                }
            
            }
            
		}
		return 0;
    }
    public static void GetCompariseHanZiElem(Han_Zi_Ele_Struct[] InHanZiStrc, ref int[] CompriseNum){//文字生成函数
        int[] TemNum=new int[5];
        int k=0;
       
        for(int i=0;i<TemNum.Length;i++){
            TemNum[i]=0;
        }
        for(int j=0;j<InHanZiStrc.Length;j++){
            if(InHanZiStrc[j].CompNum!=0){
                TemNum[k]=InHanZiStrc[j].CompNum;
                k++;
            }
        }
 		 print("******************* 59 k=" + k);
        foreach (Han_Zi_Ele_Struct selecthanzielemstrc in HanZiElementstrcs)
        {
            if(k==1){  //上部分传入的文字个数为1个。
                //暂时先按三个来文字元素来实现
                if(TemNum[0]==selecthanzielemstrc.FirstNum){ 
                    //由于文字结构体中的有效文字元素组成是从第一个编号开始排的，所以可以按下面的方法找到一个文字中其它文字元素
                     if(selecthanzielemstrc.SecondNum!=0){ 
                        CompriseNum[0]=selecthanzielemstrc.SecondNum;    
                     
                     }
                    if(selecthanzielemstrc.ThirdNum!=0){
                        CompriseNum[1]=selecthanzielemstrc.ThirdNum;
                    
                    }
                     if(selecthanzielemstrc.ForthNum!=0){
                        CompriseNum[2]=selecthanzielemstrc.ForthNum;
                    
                    }
                     print("******************* 60 selecthanzielemstrc.FirstNum=" + selecthanzielemstrc.FirstNum);
					 print("******************* 60 selecthanzielemstrc.CompNum=" + selecthanzielemstrc.CompNum);
                     break;
                
                }
                if(TemNum[0]==selecthanzielemstrc.SecondNum){ 
                    //由于文字结构体中的有效文字元素组成是从第一个编号开始排的，所以可以按下面的方法找到一个文字中其它文字元素
                     if(selecthanzielemstrc.FirstNum!=0){ 
                        CompriseNum[0]=selecthanzielemstrc.FirstNum;    
                     
                     }
                    if(selecthanzielemstrc.ThirdNum!=0){
                        CompriseNum[1]=selecthanzielemstrc.ThirdNum;
                    
                    }
                     if(selecthanzielemstrc.ForthNum!=0){
                        CompriseNum[2]=selecthanzielemstrc.ForthNum;
                    
                    }
                     print("******************* 60 selecthanzielemstrc.SecondNum=" + selecthanzielemstrc.SecondNum);
					 print("******************* 60 selecthanzielemstrc.CompNum=" + selecthanzielemstrc.CompNum);
                     break;
                
                }

                if(TemNum[0]==selecthanzielemstrc.ThirdNum){ 
                    //由于文字结构体中的有效文字元素组成是从第一个编号开始排的，所以可以按下面的方法找到一个文字中其它文字元素
                     if(selecthanzielemstrc.FirstNum!=0){ 
                        CompriseNum[0]=selecthanzielemstrc.FirstNum;    
                     
                     }
                    if(selecthanzielemstrc.SecondNum!=0){
                        CompriseNum[1]=selecthanzielemstrc.SecondNum;
                    
                    }
                     if(selecthanzielemstrc.ForthNum!=0){
                        CompriseNum[2]=selecthanzielemstrc.ForthNum;
                    
                    }
                     print("******************* 60 selecthanzielemstrc.ThirdNum=" + selecthanzielemstrc.ThirdNum);
					 print("******************* 60 selecthanzielemstrc.CompNum=" + selecthanzielemstrc.CompNum);
                     break;
                
                }
           
            
            }
            if(k==2){//上部分传入的文字是2个。  //下面这部分还没有实现。后面再说。
                if((TemNum[0]==selecthanzielemstrc.FirstNum && TemNum[1]==selecthanzielemstrc.SecondNum)||
                    (TemNum[0]==selecthanzielemstrc.SecondNum && TemNum[1]==selecthanzielemstrc.ForthNum)){
                     if(selecthanzielemstrc.ThirdNum!=0){
                        CompriseNum[0]=selecthanzielemstrc.ThirdNum;
                    
                     }
                    if(selecthanzielemstrc.ForthNum!=0){
                        CompriseNum[1]=selecthanzielemstrc.ForthNum;
                    }
                    print("******************* 61 selecthanzielemstrc.CompNum=" + selecthanzielemstrc.CompNum);
                    break;
                
                }

                if((TemNum[0]==selecthanzielemstrc.FirstNum && TemNum[1]==selecthanzielemstrc.ThirdNum)||
                    (TemNum[0]==selecthanzielemstrc.ThirdNum && TemNum[1]==selecthanzielemstrc.FirstNum)){
                     if(selecthanzielemstrc.SecondNum!=0){
                        CompriseNum[0]=selecthanzielemstrc.SecondNum;
                    
                     }
                    if(selecthanzielemstrc.ForthNum!=0){
                        CompriseNum[1]=selecthanzielemstrc.ForthNum;
                    }
                    print("******************* 62 selecthanzielemstrc.CompNum=" + selecthanzielemstrc.CompNum);
                    break;
                }
                if ((TemNum[0] == selecthanzielemstrc.SecondNum && TemNum[1] == selecthanzielemstrc.ThirdNum) ||
                    (TemNum[0] == selecthanzielemstrc.ThirdNum && TemNum[1] == selecthanzielemstrc.SecondNum))
                {
                    if (selecthanzielemstrc.SecondNum != 0)
                    {
                        CompriseNum[0] = selecthanzielemstrc.FirstNum;

                    }
                    if (selecthanzielemstrc.ForthNum != 0)
                    {
                        CompriseNum[1] = selecthanzielemstrc.ForthNum;
                    }
                    print("******************* 63 selecthanzielemstrc.CompNum=" + selecthanzielemstrc.CompNum);
                    break;
                }
                
            }

           /* if (k == 3)//上部分传入文字是3个
            { 
            
            }*/

        }
           
    }

    //输入涉及到的列
    void ElementVerticalFall(int Inclo,int CurrentRowNum,/*ref GameObject hanzi/*也可以不用实参**/ref int OutRow/*ref GameObject hanzikuang*/)  
    {
        int fallnum = 0;
        int row;
        //for (int clo = colstart; clo<= colend; clo++) {
            for (row = CurrentRowNum+1; row < 6; row++) {     //当前的循环可以用while简单完成
                if (HanZiKuangs[row, Inclo].IsUsed == 0)
                {
                    fallnum++;

                }
                else {
                    break;
                }
                
            }
            OutRow = fallnum;  //数组元素是从0开始的，所以
       //}
    
    }


    void SetActivityDesable()
    {
        //HuaXianObject.gameObject.SetActive(false);
    }

	void CreateHanZiCount(){
		int IsHaveCount=0;
		int CountIsNotDelete=0;
		for(int lie=0;lie<6;lie++){
			if(HanZiKuangs[5,lie].IsCount !=0 ){     //
				IsHaveCount++;
				
			}
			if(HanZiKuangs[5,lie].IsCountDelete!=0){
				CountIsNotDelete++;
			}

		}  
		print("AAAAAAAAAAA 200 CreateHanZiCount" );

		//需要添加有过count的汉字框是否被消除
		if(IsHaveCount==0 && CountIsNotDelete<=2){         //这个可以后面优化看可以存在几个count文字框合适。
			for(int i=0;i<3;i++){   //随机最多指定3次汉字元素，如果是多次合成就赋值，否则不用。
				int RandTempNum=UnityEngine.Random.Range(0, 5);
				print("AAAAAAAAAAA 201 m_HanZiElementstrc.CompNum" +HanZiKuangs[5,RandTempNum].HanZiKuangObject.transform.GetChild(0).GetComponent<HanZiElement>().m_HanZiElementstrc.CompNum);
				for(int j=0;j<Coprise2CountNum.GetLength(0);j++){
					if(Coprise2CountNum[j]==HanZiKuangs[5,RandTempNum].HanZiKuangObject.transform.GetChild(0).GetComponent<HanZiElement>().m_HanZiElementstrc.CompNum)
					{
						
						HanZiKuangs[5,RandTempNum].IsCount=1;           //表示要合成两次才能组成一个文字。
						HanZiKuangs[5,RandTempNum].IsCountDelete=HanZiKuangs[5,RandTempNum].IsCount+1; //添加一个count标志，通过count加1来表示该框是否为count合成过的文字没有删除。主要作用只是判断当前是否可以生成新的count。

                        //在对应汉字框中生成对应的count.
                        GameObject CountObjectTemp = HanZiKuangCounts[HanZiKuangs[5, RandTempNum].IsCount].gameObject;
                        CountObjectTemp.SetActive(true);
                        Vector3 CountObjectPos = HanZiKuangs[5, RandTempNum].HanZiKuangObject.transform.position;
                        CountObjectPos.x = CountObjectPos.x - 0.74f;
                        CountObjectPos.y = CountObjectPos.y - 0.74f;
                        CountObjectPos.z = -1;
                        CountObjectTemp.transform.position = CountObjectPos;
						 print("AAAAAAAAAAA 202 HanZiKuangs[5," +RandTempNum+"]="+ Coprise2CountNum[j]);
						break;
					}

				}
				//后面可以添加count为2、3、4等情况
				
				if(HanZiKuangs[5,RandTempNum].IsCount!=0){
					break;
				}
				
			}			

		}

	}

    //汉字组成表
}
