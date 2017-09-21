using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Switch : MonoBehaviour {
    //bool toggle = true; //1 : 보임, 0 : 안보임
    int toggle = 0;
    public GameObject UI;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       UI_toggle(toggle);        
	}
    public void set_toggle()
    {
        toggle ^= 1;
    }
    void UI_toggle(int toggle)
    {        
        if (toggle == 1)
        {
            UI.SetActive(true);            
        }
        if (toggle == 0)
        {
            //toggle = true;
            UI.SetActive(false);
        }
      //  Debug.Log(toggle);
    }
}
