using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class HanZiElement : MonoBehaviour {

	// Use this for initialization

    //对于图片对象先用最简单的Resources.Load方法，后面如果效果不好就改为map动态分配
    //private int IdNumber;
    //private int Position;  //不用位子，只需编号相同，就把所有能组成的文字都显示出来。
    //图片元素

   
	//int FirstNum;
	//int SecondNum;
	private Texture2D texture;
	public Han_Zi_Ele_Struct m_HanZiElementstrc;
	bool m_IsSelect;
    HanZiElement OtherObject=null;
    Han_Zi_Ele_Struct New_HanZiElementstrc;
    SpriteRenderer m_SpriteRenderer;
    Sprite m_Sprite;
    public Transform m_parent;

	protected AudioSource m_audio;
    public AudioClip Han_Zi_Create;

	public int KuangHaveElemType;  //每个汉字框中含有的元素类别：0表示没有；1表示汉字；2表示金笔        		。 暂时可以不用，直接用对象的名字即可
	
   // public AnimationCurve animcurve;
    //int timerstart = 0;
    //public float timer = 1f;
    /*Vector3 m_startpos;
    Vector3 m_endpos;
    public float speed = 10;*/
    void Awake() {

        m_audio = this.gameObject.GetComponent<AudioSource>();
    
    }
	void Start () {
      //  CreateElement();
        m_IsSelect = false;
       
	
	}
	
	// Update is called once per frame
	void Update () {

        /*if (this.gameObject.transform.localPosition.y < m_endpos.y) {

            this.gameObject.transform.position.y = m_startpos.y+;
        
        }else*/
        


		//if(Input.GetMouseButtonDown(0))
		//{
            //下面通过碰撞获取对象的方式可以不用了
            /* if (m_IsSelect)
             {
                 Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
                 RaycastHit hit;
                 if(!Physics.Raycast(ray,out hit))
                 {
                     m_IsSelect = false;
                     //print("*******************02 IsSelect=" + m_IsSelect);
                     return;
                 }
                 //Physics.Raycast(ray, out hit);
                //Physics.Raycast(this.gameObject.transform.position, Vector3.forward, out hit);
                // print("*******************04" + hit.collider.gameObject);
                 //Physics.Raycast(this.gameObject.transform.position, Vector3.back, out hit);
                // print("*******************05" + hit.collider.gameObject);
                 if ((hit.collider.gameObject != this.gameObject) && m_IsSelect)
                 {
                     //获得鼠标的位置。
                     Vector3 DestPos = hit.collider.gameObject.transform.position;
                     //print("*******************03 DestPos=" + DestPos);
                    // print("*******************04 startPos=" + this.gameObject.transform.position);
                     //Move(this.gameObject.transform.position ,DestPos);
                     StartCoroutine(Move(this.gameObject.transform.position, DestPos));
                     //获得碰撞汉字对象下的类的汉字结构体
                     Han_Zi_Ele_Struct OtherElement=hit.collider.gameObject.GetComponent<HanZiElement>().m_HanZiElement;
					
                     //调用IsCompriseHanZi函数

                     m_IsSelect = false;

                 }
				
             }*/
           
        //}
	}

	//随机创建一个对象
    public void CreateElement(Vector2 position)
	{

       // m_HanZiElement = HanZiHeDan.HanZiElements[Random.Range(0, 500)];
        m_Sprite = Sprite.Create(m_HanZiElementstrc.image, new Rect(0, 0, m_HanZiElementstrc.image.width,
        m_HanZiElementstrc.image.height), position);
      //  print("******************* 13 m_HanZiElement.image.width=" + m_HanZiElement.image.width);

        m_SpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        m_SpriteRenderer.sprite = m_Sprite;
        m_SpriteRenderer.sprite.name = Convert.ToString(m_HanZiElementstrc.CompNum);
        this.gameObject.name = Convert.ToString(m_HanZiElementstrc.CompNum);




	}

    public void PlaySound_HanZi_Create() {

        m_audio.PlayOneShot(Han_Zi_Create);
    
    }
    /*public void PlaySound_HanZi_DanJi()
    {

        m_audio.PlayOneShot(Dan_Ji);

    }*/
   /* public void PlaySound_HanZi_HeCheng()
    {

        m_audio.PlayOneShot(He_Cheng);

    }*/

   /* public void HanZiElementMove(Vector3 StartPos, Vector3 EndPos){
        m_startpos = StartPos;
        m_endpos = EndPos;
        
    
    }*/



    //后面动画要重新设计,可以根据直接每一帧的变换来实现
    public void Move(Vector3 StartPos, Vector3 EndPos)  //  选择的对象进行移动
    {
     	SetParent();
		
        AnimationClip ret = new AnimationClip();
        ret.SetCurve("", typeof(Transform), "localPosition.x", AnimationCurve.Linear(0, StartPos.x, 1.2f, EndPos.x)); //注意如果这个时间过小会导致汉字元素无法运动到指定位置
        ret.SetCurve("", typeof(Transform), "localPosition.y", AnimationCurve.Linear(0, StartPos.y, 1.2f, EndPos.y));
		ret.SetCurve("", typeof(Transform), "localPosition.z", AnimationCurve.Linear(0, StartPos.z, 1.2f, EndPos.z));  //这里面才是决定移动的时间。

       
       /* ret.SetCurve("", typeof(Transform), "localPosition.x", animcurve);
        ret.SetCurve("", typeof(Transform), "localPosition.y", animcurve);
        ret.SetCurve("", typeof(Transform), "localPosition.z", animcurve);  //这里面才是决定移动的时间。*/
        this.gameObject.animation.AddClip(ret,"Move");
        if (this.gameObject.animation)
        {
            this.gameObject.animation.Blend("Move", 1.2f);
        }

        //yield return new WaitForSeconds(1.2f);
        //SetParent(m_parent);
        //this.gameObject.transform.localPosition = new Vector3(0,0,-1);  //这里添加一行是为了防止概率性子类相对父类位置偏散，能否解决有待验证 //****注意这里是有localPosition，不然就不是相对父类的坐标了

        //实现两个文字元素的碰撞
        //Vector3 RayPosition =this.
           /* RaycastHit hitInfo_forward;
            Physics.Raycast(this.gameObject.transform.position, Vector3.forward, out hitInfo_forward);
        if (OtherObject != null) {
            result=HanZiHeDan.IsCompriseHanZi(this.gameObject.GetComponent<HanZiElement>().m_HanZiElement.CompNum, OtherObject.GetComponent<HanZiElement>().m_HanZiElement.CompNum,
                ref New_HanZiElement);//获得组合后的新文字
             print("this.CompNum="+this.gameObject.GetComponent<HanZiElement>().m_HanZiElement.CompNum);
			print("OtherObject.CompNum="+OtherObject.GetComponent<HanZiElement>().m_HanZiElement.CompNum);
            //把新合成的文字生成新对象
             if (result == 1) {
                 print("###########09");
                 m_Sprite = Sprite.Create(New_HanZiElement.image, new Rect(0, 0, New_HanZiElement.image.width,
                 New_HanZiElement.image.height), new Vector2(0.5f, 0.5f));
                 print("New_HanZiElement.CompNum=" + New_HanZiElement.CompNum);
                 m_SpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
                 m_SpriteRenderer.sprite = m_Sprite;

                 OtherObject = null;
             
             
             }
          
        
        }*/
    
    }



    void SetPosition(int position)
    {
		
       // Position = position;
    
    }

     /*void OnTriggerEnter(Collider other) {      //这应该是系统实时调用的函数,估计没有什么用。
         print("#######07");
         if (!m_IsSelect) {
             HanZiElement hanzi = other.GetComponent<HanZiElement>();
             if (hanzi == null)
             {
                 return;
             }
             else
             {
                 print("#######08");
                 OtherObject = hanzi;
             }
         
         }
        
    }*/

	//鼠标点击通过当前位置找到对应的对象，然后获得该对象的compnum
	void OnMouseUpAsButton()
	{
        print("############01");
        m_IsSelect = true;

	}


    public void SetParent() {
        this.gameObject.transform.parent = m_parent;
    
    }
}
