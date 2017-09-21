using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finder : MonoBehaviour {
    private int Xbox_One_Controller = 0;
    private int PS4_Controller = 0;
    public bool on = true;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (on)
        {
            string[] names = Input.GetJoystickNames();
            for (int x = 0; x < names.Length; x++)
            {
                print(names[x].Length);
                if (names[x].Length == 19)
                {
                    print("PS4 CONTROLLER IS CONNECTED");
                    PS4_Controller = 1;
                    Xbox_One_Controller = 0;
                }
                if (names[x].Length == 33)
                {
                    print("XBOX ONE CONTROLLER IS CONNECTED");
                    //set a controller bool to true
                    PS4_Controller = 0;
                    Xbox_One_Controller = 1;

                }
            }


            if (Xbox_One_Controller == 1)
            {
                if (Input.GetKey(KeyCode.Joystick1Button0))
                    Debug.Log("bt0 : A");
                if (Input.GetKey(KeyCode.Joystick1Button1))
                    Debug.Log("bt1 : B");
                if (Input.GetKey(KeyCode.Joystick1Button2))
                    Debug.Log("bt2 : X");
                if (Input.GetKey(KeyCode.Joystick1Button3))
                    Debug.Log("bt3 : Y");
                if (Input.GetKey(KeyCode.Joystick1Button4))
                    Debug.Log("bt4 : Left bumper");
                if (Input.GetKey(KeyCode.Joystick1Button5))
                    Debug.Log("bt5 : Right bumper");
                if (Input.GetKey(KeyCode.Joystick1Button6))
                    Debug.Log("bt6 : left menu");
                if (Input.GetKey(KeyCode.Joystick1Button7))
                    Debug.Log("bt7 : right menu");
                if (Input.GetKey(KeyCode.Joystick1Button8))
                    Debug.Log("bt8 : left axis btn");
                if (Input.GetKey(KeyCode.Joystick1Button9))
                    Debug.Log("bt9 : right axis btn");
                if (Input.GetKey(KeyCode.Joystick1Button10))
                    Debug.Log("bt10");
                if (Input.GetKey(KeyCode.Joystick1Button11))
                    Debug.Log("bt11");
                if (Input.GetKey(KeyCode.Joystick1Button12))
                    Debug.Log("bt12");
                if (Input.GetKey(KeyCode.Joystick1Button13))
                    Debug.Log("bt13");
                if (Input.GetKey(KeyCode.Joystick1Button14))
                    Debug.Log("bt14");
                if (Input.GetKey(KeyCode.Joystick1Button15))
                    Debug.Log("bt15");
                if (Input.GetKey(KeyCode.Joystick1Button16))
                    Debug.Log("bt16");
                if (Input.GetKey(KeyCode.Joystick1Button17))
                    Debug.Log("bt17");
                if (Input.GetKey(KeyCode.Joystick1Button18))
                    Debug.Log("bt18");
                if (Input.GetKey(KeyCode.Joystick1Button19))
                    Debug.Log("bt19");
                if (Input.GetAxisRaw("DpadX") != 0)
                    Debug.Log("DpadX"+Input.GetAxis("DpadX"));
                if (Input.GetAxisRaw("DpadY") != 0)
                    Debug.Log("DpadY" + Input.GetAxis("DpadY"));



            }
            else if (PS4_Controller == 1)
            {
                //do something
            }
            else
            {
                // there is no controllers
            }
        }
    }
}
