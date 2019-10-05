using UnityEngine;
using System.Collections;

public class ChuiZiElement : MonoBehaviour {


    public int ChuiZiIsSelect=0;
    Vector3 Scal;
    Vector3 Pos;

    public AudioSource m_audio;
    public AudioClip Sound_Dian_Ji;

    void Awake() {
        m_audio = this.gameObject.GetComponent<AudioSource>();
    
    }
	// Use this for initialization
	void Start () {
        

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if ((!Physics.Raycast(ray, out hit)) && (ChuiZiIsSelect == 1))   //未选中任何对象时，锤子选定状态还原
            {
                ChuiZiIsSelect = 0;
                Scal = new Vector3(1.3f, 1.3f, 1.3f);
                this.gameObject.transform.localScale = Scal;
                Pos = this.gameObject.transform.position;
                Pos.z = 0;
                this.gameObject.transform.position = Pos;
            }
            else if ((Physics.Raycast(ray, out hit))&&(hit.collider.gameObject != this.gameObject) && (ChuiZiIsSelect == 1))
            {    //选中对象并且选中对象不是锤子本身
                if (hit.collider.gameObject.tag == "HanZiKuang") {
                    return;
                }
                ChuiZiIsSelect = 0;
                Scal = new Vector3(1.3f, 1.3f, 1.3f);
                this.gameObject.transform.localScale = Scal;
                Pos = this.gameObject.transform.position;
                Pos.z =0;
                this.gameObject.transform.position = Pos;
            }
        
        }
	
	}


    void OnMouseUpAsButton() {

        if (ChuiZiIsSelect == 1)
        {
            m_audio.PlayOneShot(Sound_Dian_Ji);     //点击声效播放
          
            ChuiZiIsSelect = 0;
            Scal = new Vector3(1.3f, 1.3f, 1f);
            this.gameObject.transform.localScale = Scal;
            Pos = this.gameObject.transform.position;
            Pos.z =0;
            this.gameObject.transform.position = Pos;


        }
        else {
            m_audio.PlayOneShot(Sound_Dian_Ji);    

            ChuiZiIsSelect = 1;       //选定锤子按钮
            Scal = new Vector3(1.8f, 1.8f, 1f);
            this.gameObject.transform.localScale = Scal;
            Pos = this.gameObject.transform.position;
            Pos.z = -1;
            this.gameObject.transform.position = Pos;
      
        }
    }

    public void ChuiZiCancleSelect() {
        ChuiZiIsSelect = 0;
        Scal = new Vector3(1.3f, 1.3f, 1f);
        this.gameObject.transform.localScale = Scal;
        Pos = this.gameObject.transform.position;
        Pos.z = 0;
        this.gameObject.transform.position = Pos;
    
    }
}
