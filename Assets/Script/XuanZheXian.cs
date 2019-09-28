using UnityEngine;
using System.Collections;

public class XuanZheXian : MonoBehaviour
{


    Vector3 pos1;     //定义起点坐标
    Vector3 pos2;     //定义终点坐标
    //public int wucha;
    LineRenderer myLinRenderer;     //定义一个LineRenderer对象
    //int count = 0;    //定义一个变量，用来修改泛型的长度

    float time = 0.2f;  //定义一个时间间隔
    //Object HuaXianObject;
    // Use this for initialization
    void Start()
    {
        myLinRenderer = GetComponent<LineRenderer>();     //给LineRenderer对象初始化

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))      //当鼠标左键按下的时候
        {
            pos1 = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));     //将起点坐标记录下来
            //myLinRenderer.SetVertexCount(1);      //第一次按下鼠标左键时，应该有了一个点，所以把节点的数目设置为1
            //myLinRenderer.SetPosition(0, pos1);   //有了一个节点就设置一次坐标，而不是单单记录坐标
        }

        //这些注释部分暂时不需要

        /* if (Input.GetMouseButton(0))    //当鼠标一直按下的时候
        {
            time -= Time.deltaTime;     //时间进行递减
            if (time <= 0)      //当时间减小到0或0以下时，
            {
                time = 0.2f;        //把时间重置
                count++;        //节点数自增

                Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));     //纪录此时的坐标值
                myLinRenderer.SetVertexCount(count);        //每隔0.2秒，就增加一个节点，增加一个点的方法就是SetVertexCount()方法
                count--;        //count还要自减，因为在设置位置的时候，索引是比节点数小1的
                myLinRenderer.SetPosition(count,pos);       //设置这个增加的节点的位置
                count++;        //最后，因为增加了一个节点，所以count是要增加1的

            }
        }*/


         if (Input.GetMouseButtonUp(0))    //当鼠标左键抬起的时候，依旧要纪录最后一个点的坐标，最终把count重置为0
        {
            //count++;
            pos2 = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));     //将终点坐标记录下来
           // myLinRenderer.SetVertexCount(count);
            //count--;
           // myLinRenderer.SetPosition(count, pos2);
           // count = 0;
             
            //下面实现划线不出现歪斜的情况.因该要在
           
            

            float a = pos2.x - pos1.x;
            float b = pos2.y - pos1.y;
            if (Mathf.Abs(a) >= Mathf.Abs(b)){      //横向划线
               // print("******************* 27 pos2.y =" + pos2.y);
                pos2.y = (int)((pos2.y-1.2) / 2.4) * 2.4f;
               // print("******************* 28 pos2.y =" + pos2.y);
                pos1.y = (int)((pos1.y-1.2) / 2.4) * 2.4f;
                //print("******************* 29 pos1.y =" + pos1.y);
                pos2.y = pos1.y;
               // print("******************* 30 pos2.y = pos1.y=" + pos2.y);
            }
            else {                                  //竖直方向划线
                pos2.x = (int)((pos2.x+1.21) / 2.42) * 2.42f;

                pos1.x = (int)((pos1.x+1.21) / 2.42) * 2.42f;
                pos2.x = pos1.x;
                //print("******************* 31 pos2.x = pos1.x=" + pos2.x);
            
            }
            myLinRenderer.SetPosition(0, pos1);
            myLinRenderer.SetPosition(1, pos2);    //分别将起始坐标和终点坐标付给相应的变量
            Invoke("SetHuaXianDesable", 0.5f);  //定时把选线去显示
        }
    }


    void SetHuaXianDesable() {
        myLinRenderer.SetPosition(0, new Vector3(0, 0, 0));
        myLinRenderer.SetPosition(1, new Vector3(0,0,0));
    
    }

	public void HanZiHeChengDaoHeng(Vector3 startpos,Vector3 endpos){

		myLinRenderer.SetPosition(0, startpos);
        myLinRenderer.SetPosition(1, endpos);
		Invoke("SetHuaXianDesable", 0.5f);

	}


	/*下面是另外一种实现的方法
    //定义销毁对象委托
    public delegate void DestroyGameObjectHandler(GameObject game);
    public event DestroyGameObjectHandler DestroyHandler;
    // Use this for initialization
    //添加预设变量
    public GameObject knifeRay;

//  public Texture backGroudImage;

    private LineRenderer lineRender;




    //这个声音组件不用，如果使用的话游戏里面只有一个声音同时存在，我们改用给预设里面添加声音实现多个声音组件同时存在
    private AudioSource knifeAudio;
    //纪录鼠标的世界坐标
    Vector3 firstPosition;
    Vector3 secondPosition;
    bool isMouseDown = false;
    void Start () {
        //添加线条组件
        lineRender = gameObject.AddComponent<LineRenderer>();
        //设置线条的颜色为白色
        lineRender.material.color = Color.white;
        //设置线条的宽度为0.1
        lineRender.SetWidth((float)0.05,(float)0.05);

        //隐藏线条
        lineRender.enabled = false;


    }
    //设置背景图片
//  void OnGUI(){
//      GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backGroudImage, ScaleMode.StretchToFill);
//
//  }
    // Update is called once per frame
    void Update () {
        if (!isMouseDown) {
            if (Input.GetMouseButton (0)) {
                isMouseDown = true;
                firstPosition = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 1));
                lineRender.SetVertexCount (1);
                lineRender.SetPosition (0, firstPosition);
            }
        } else {
            if (Input.GetMouseButtonUp (0)) {



                isMouseDown = false;
                //这里需要把屏幕里或的鼠标坐标转成世界坐标这样才能正常的显示线条
                secondPosition = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 1));
                lineRender.SetVertexCount (2);
                lineRender.SetPosition (1, secondPosition);

                Vector3 middlePosition = new Vector3 ((secondPosition.x + firstPosition.x)/2, (secondPosition.y + firstPosition.y)/2, -5);
                float angle;
                if (secondPosition.x != firstPosition.x) {
                    angle = Mathf.Atan ((secondPosition.y - firstPosition.y) / (secondPosition.x - firstPosition.x));
                } else {
                    angle = 0;
                }

                angle = (float)(angle * 180 / 3.14);

//              Debug.Log (angle);

                //以middlePosition为中心，angle为角度，绕着Z轴旋转（Vector3.forward表示绕着Z轴旋转）
                GameObject gameObj =  (GameObject)Instantiate(knifeRay,middlePosition,Quaternion.AngleAxis(angle,Vector3.forward));

                //播放该预设里面的刀的声音，从而实现多声音存在
                gameObj.GetComponent<AudioSource> ().Play ();
//              this.DestroyHandler = this.DestroyGameObject (gameObj);
                Destroy (gameObj,(float)0.5);
            }
        }




    }
    public void DestroyGameObject(GameObject game)
    {
        game.SetActive (false);
        Destroy (game,(float)1.0);
    }
   */


}


