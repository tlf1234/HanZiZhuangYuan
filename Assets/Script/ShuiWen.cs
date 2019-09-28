using UnityEngine;
using System.Collections;

public class ShuiWen : MonoBehaviour {

	// Use this for initialization
    private float speed = 0.5f;
    private Material m_Material;

    void Start()
    {
        m_Material = this.gameObject.GetComponent<LineRenderer>().material;
    }

    void Update()
    {
        float x = Time.time * speed;
        m_Material.SetTextureOffset("_MainTex", new Vector2(x, 0));
    }
}
