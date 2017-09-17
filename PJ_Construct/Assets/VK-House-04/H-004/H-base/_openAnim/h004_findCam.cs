using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class h004_findCam : MonoBehaviour {

    public Transform Target;
    
	// Use this for initialization
	void Start () {
        Target = Camera.main.gameObject.transform;		
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.LookRotation(Target.forward, Target.up);		
	}
}
