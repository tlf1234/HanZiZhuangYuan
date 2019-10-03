using UnityEngine;
using System.Collections;

public class ChuiZiElement : MonoBehaviour {


    public int ChuiZiIsSelect=0;
    Vector3 Scal;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (((!Physics.Raycast(ray, out hit))||(hit.collider.gameObject!=this.gameObject)) && (ChuiZiIsSelect==1))
            {
                if (hit.collider.gameObject.tag == "HanZiKuang") {
                    return;
                }
                ChuiZiIsSelect = 0;
                Scal = new Vector3(1.3f, 1.3f, 1.3f);
                this.gameObject.transform.localScale = Scal;
            }
        
        }
	
	}


    void OnMouseUpAsButton() {

        if (ChuiZiIsSelect == 1)
        {
            ChuiZiIsSelect = 0;
            Scal = new Vector3(1.3f, 1.3f, 1f);
            this.gameObject.transform.localScale = Scal;


        }
        else {
            ChuiZiIsSelect = 1;       //选定锤子按钮
            Scal = new Vector3(1.8f, 1.8f, 1f);
            this.gameObject.transform.localScale = Scal;      
        }
    }

    public void ChuiZiCancleSelect() {
        ChuiZiIsSelect = 0;
        Scal = new Vector3(1.3f, 1.3f, 1f);
        this.gameObject.transform.localScale = Scal;
    
    }
}
