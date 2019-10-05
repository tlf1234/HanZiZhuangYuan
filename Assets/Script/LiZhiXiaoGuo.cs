using UnityEngine;
using System.Collections;

public class LiZhiXiaoGuo : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Stop();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public IEnumerator Move(Vector3 StartPos, Vector3 EndPos)
    {
        //this.gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(0.5f);
        AnimationClip ret = new AnimationClip();
        ret.SetCurve("", typeof(Transform), "localPosition.x", AnimationCurve.Linear(0, StartPos.x, 1.2f, EndPos.x)); //注意如果这个时间过小会导致汉字元素无法运动到指定位置
        ret.SetCurve("", typeof(Transform), "localPosition.y", AnimationCurve.Linear(0, StartPos.y, 1.2f, EndPos.y));
        ret.SetCurve("", typeof(Transform), "localPosition.z", AnimationCurve.Linear(0, StartPos.z, 1.2f, EndPos.z));  //这里面才是决定移动的时间。


        /* ret.SetCurve("", typeof(Transform), "localPosition.x", animcurve);
         ret.SetCurve("", typeof(Transform), "localPosition.y", animcurve);
         ret.SetCurve("", typeof(Transform), "localPosition.z", animcurve);  //这里面才是决定移动的时间。*/
        this.gameObject.animation.AddClip(ret, "Move");
        if (this.gameObject.animation)
        {
            this.gameObject.animation.Blend("Move", 1.2f);
        }

        yield return new WaitForSeconds(1f);
        this.gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Stop();

    }
    


}
