using UnityEngine;
using System.Collections;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderAdjust : MonoBehaviour {

    public bool AdjustBoxCollider = false;
    private BoxCollider boxCollider;
    private RectTransform gameObject;
    // Use this for initialization
     void Start () {
        gameObject = this.GetComponent<RectTransform>();
        boxCollider = this.GetComponent<BoxCollider>();
     }
    
    // Update is called once per frame
   void Update () {
        if (boxCollider == null)
         {
             Debug.Log("can't find collider");
             return;
         }
         else
         {
 
             if (AdjustBoxCollider == true)
             {
                 boxCollider.center = gameObject.rect.center;      //把box collider设置到物体的中心
                 boxCollider.size = new Vector3(gameObject.rect.width, gameObject.rect.height,1);    //改变collider大小
             }
         }
     }
 }
