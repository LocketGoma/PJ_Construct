using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Group_UI : MonoBehaviour {
    public int scenenumber = 0;
    [Header("조명 그룹")]
    public GameObject Light;
    int lighton = 0;        //  0 : off, 1 : on
    GameObject gameobject;
    int toggle = 0;     // 0 : off, 1 : on
                        // Use this for initialization
    bool toggle_axisX=true;                 //축 토글용. bool값 먹여서 축을 한번씩만 작동되게 함.
    bool toggle_axisY = true;
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        UI_Switch UISwitch = GetComponent<UI_Switch>();
		if (Input.GetKeyDown("u")|| Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            UISwitch.set_toggle();
            toggle ^= 1;
        }
        if (toggle == 1)
        {
            UI_Menu();
        }
	}

    void UI_Menu()
    {   //1 : 위, 2, 좌, 3, 아래, 4, 우
        if (Input.GetKeyDown("1")|| Input.GetAxisRaw("DpadY")==1&&toggle_axisY==true)
        {
            toggle_axisY = false;
            this.Menu_light();
            Debug.Log("input 1");
        }
        if (Input.GetKeyDown("2") || Input.GetAxis("DpadX") == -1 && toggle_axisX == true)
        {            
            this.Menu_claer();
            Debug.Log("Input 2");
        }
        if (Input.GetKeyDown("3") || Input.GetAxis("DpadY") == -1 && toggle_axisY == true)
        {
            toggle_axisY = false;
            this.Menu_Scene();
            Debug.Log("input 3");
        }
        if (Input.GetKeyDown("4") || Input.GetAxis("DpadX") == 1 && toggle_axisX == true)
        {
            this.Menu_EXIT();
            Debug.Log("Input 4");
        }

        if ( Input.GetAxis("DpadY") == 0)
        {
            toggle_axisY = true;
        }
        if (Input.GetAxis("DpadX") == 0)
        {
            toggle_axisX = true;
        }

    }
    void Menu_Scene()
    {
        scenenumber ^= 1;
        SceneManager.LoadScene(scenenumber, LoadSceneMode.Single);
    }
    void Menu_light()
    {
        lighton ^= 1;
        if (lighton == 0)
        Light.SetActive(false);
        if (lighton==1)
        Light.SetActive(true);
    }
    void Menu_claer()
    {
        SceneManager.LoadScene(scenenumber, LoadSceneMode.Single);
    }
    void Menu_EXIT()
    {
        Application.Quit();
    }
}
