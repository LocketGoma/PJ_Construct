using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameracontrol : MonoBehaviour {

    Vector2 _mouseAbsolute;
    Vector2 _smoothMouse;

    //public float sensitivity = 0.05f;
   
    public Camera cam;
    //   public GameObject character;

 

    public Vector2 targetDirection;
    public Vector2 targetCharacterDirection;
    [Header("Camera option & range")]
    public Vector2 sensitivity = new Vector2(2, 2);
    public Vector2 Smoothing = new Vector2(2, 2);
    public Vector2 clampInDegrees = new Vector2(360, 180);

    private float gab = 0.5f;

    void Start () {
        cam = GetComponent<Camera>();
        // Set target direction to the camera's initial orientation.
        targetDirection = transform.localRotation.eulerAngles;

        // Set target direction for the character body to its inital state.
        //     if (character) targetCharacterDirection = character.transform.localRotation.eulerAngles;


    }
	
	// Update is called once per frame
	void Update () {

        //transform.LookAt(cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane)), Vector3.up);
        var targetOrientation = Quaternion.Euler(targetDirection);

        //   Vector3 vp = cam.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouseDelta = Vector2.Scale(mouseDelta,new Vector2(sensitivity.x*Smoothing.x, sensitivity.y * Smoothing.y));

        //보간 삽입
        _smoothMouse.x = Mathf.Lerp(_smoothMouse.x, mouseDelta.x, 1f / Smoothing.x);
        _smoothMouse.y = Mathf.Lerp(_smoothMouse.y, mouseDelta.y, 1f / Smoothing.y);

        _mouseAbsolute += _smoothMouse;

        //clamp (범위 지정)
        if(clampInDegrees.x < 360)
        {
            _mouseAbsolute.x = Mathf.Clamp(_mouseAbsolute.x, -clampInDegrees.x * gab, clampInDegrees.x * gab);
        }
        transform.localRotation = Quaternion.AngleAxis(-_mouseAbsolute.y, targetOrientation * Vector3.right); //* 여기가 핵심인듯.

        if (clampInDegrees.y < 360)
        {
            _mouseAbsolute.y = Mathf.Clamp(_mouseAbsolute.y, -clampInDegrees.y * gab, clampInDegrees.y * gab);
        }
        transform.localRotation *= targetOrientation;

        transform.localRotation*=(Quaternion.AngleAxis(_mouseAbsolute.x, transform.InverseTransformDirection(Vector3.up))); //* 여기도.



        // Debug.Log("mouse.x : " + vp.x + "mouse.y :" + vp.y);
        /*요기 안에 있는것들 = 구 버전*/

        /*
        vp.x -= gab;
        vp.y -= gab;
        vp.x *= sensitivity;
        vp.y *= sensitivity;
        vp.x += gab;
        vp.y += gab;
        Vector3 sp = cam.ViewportToScreenPoint(vp);

        Vector3 v = cam.ScreenToWorldPoint(sp);
        */
        //transform.localRotation = Quaternion.Euler(new Vector3(vp.x,vp.y,0));
        //transform.Rotate(0, 0, 0, Space.Self);
        /*transform.LookAt(v, Vector3.up);
        Debug.Log(transform.eulerAngles.x); 
        if (transform.eulerAngles.x > 85 && transform.eulerAngles.x<90)
        {
            transform.eulerAngles = new Vector3(85, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
        if (transform.eulerAngles.x < 275 && transform.eulerAngles.x > 270)
        {
            transform.eulerAngles = new Vector3(275, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
*/
        // 89 <----- < 0 < 360 <---- 270
        // 여기서 막혀야되는건 80 < 85, 265 < 270
        // 그럼...

    }
}
