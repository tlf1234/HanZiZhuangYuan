//点击鼠标产生波纹
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRipper : MonoBehaviour
{
    public Camera mainCamera;
    public Shader shader;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //主摄像机添加屏幕后处理脚本
            mainCamera.gameObject.AddComponent<RipperPostEffect>();
            RipperPostEffect[] components = mainCamera.gameObject.GetComponents<RipperPostEffect>();
            //指定shader
            components[components.Length - 1].shader = shader;
            //设置鼠标位置在（0，1）区间
            components[components.Length - 1].startPos = new Vector4(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height, 0, 0);
        }
    }
}