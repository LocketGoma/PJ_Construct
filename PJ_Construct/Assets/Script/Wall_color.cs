using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_color : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void change()
    {
        Debug.Log("5");
        Renderer rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Specular");
        rend.material.SetColor("_SpecColor", Color.white);
    }
}
