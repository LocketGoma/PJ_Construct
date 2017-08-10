using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameracontrol : MonoBehaviour {

    Vector2 _mouseAbsolute;
    Vector2 _smoothMouse;

    public float sensitivity = 0.05f;
   
    public Camera cam;
    public GameObject character;


    public Vector2 targetDirection;
    public Vector2 targetCharacterDirection;
    [Header("Camera range")]
    public Vector2 clampInDegrees = new Vector2(360, 180);

    private float gab = 0.5f;

    void Start () {
        cam = GetComponent<Camera>();
        // Set target direction to the camera's initial orientation.
        targetDirection = transform.localRotation.eulerAngles;

        // Set target direction for the character body to its inital state.
        if (character) targetCharacterDirection = character.transform.localRotation.eulerAngles;
    }
	
	// Update is called once per frame
	void Update () {

        //transform.LookAt(cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane)), Vector3.up);


        Vector3 vp = cam.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));
        

        vp.x -= gab;
        vp.y -= gab;
        vp.x *= sensitivity;
        vp.y *= sensitivity;
        vp.x += gab;
        vp.y += gab;
        Vector3 sp = cam.ViewportToScreenPoint(vp);

        Vector3 v = cam.ScreenToWorldPoint(sp);

        
        transform.LookAt(v, Vector3.up);

        Debug.Log(transform.eulerAngles.x);
        if (transform.eulerAngles.x > 85 && transform.eulerAngles.x<90)
        {
            transform.eulerAngles = new Vector3(85, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
        if (transform.eulerAngles.x < 275 && transform.eulerAngles.x > 270)
        {
            transform.eulerAngles = new Vector3(275, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }

        // 89 <----- < 0 < 360 <---- 270
        // 여기서 막혀야되는건 80 < 85, 265 < 270
        // 그럼...

    }
}
