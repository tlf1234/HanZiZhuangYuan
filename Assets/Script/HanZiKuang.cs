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
	public GameObject HanZiCountObjtct;
	//public int KuangHaveElemType;  //每个汉字框中含有的元素类别：0表示没有；1表示汉字；2表示金笔
	
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
