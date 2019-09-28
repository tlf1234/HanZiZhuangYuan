using UnityEngine;
using System.Collections;

public struct Han_Zi_KUANG_STRUCT
{

    public GameObject HanZiKuangObject;
    public int IsUsed;
    public int rowindex;
    public int cloindex;
	public int IsCount;
	public int IsCountDelete;     //只用来判定当前是否能生成count。
}

public struct HAN_ZI_KUANG_COUNT_STRUCT {
    public Transform HanZiKuangCount;
    public int CountValue;

}

public class HanZiKuang : MonoBehaviour {

	// Use this for initialization
    public Han_Zi_KUANG_STRUCT M_HanZiKuang;


	
	void Start () {
        
       
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
