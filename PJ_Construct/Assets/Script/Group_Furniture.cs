using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group_Furniture : MonoBehaviour {
    /// <summary>
    /// 마스터클래스 1
    /// "모든" 가구 목록을 가지고 있으며, 호출 시 선택된 가구를 리턴해주는 역할.
    /// </summary>
	// Use this for initialization

    [Header("사용중인 번호")]
    public int selected = 0;

    [Header("가구 오브젝트")]
    public GameObject[] furniture;
    //주르륵 넣어야됨...
    GameObject selected_obj;

    [Header("키 설정")]
    public string back="[";
    public string next="]";

	void Start () {
        // if(furniture.Length<selected||selected<0)
        selected_obj = furniture[0];
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(back) || Input.GetKeyDown(KeyCode.Joystick1Button4))
        {
            changer(-1);
        }
        if (Input.GetKeyDown(next) || Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            changer(1);
        }
	}
    void changer(int arrow)
    {
        selected += arrow;
        if (selected < 0)
        {
            selected = furniture.Length - 1;
        }
        if (selected > furniture.Length - 1)
        {
            selected = 0;
        }
        selected_obj = furniture[selected];
    }
    public GameObject get_Selected()
    {
        Debug.Log("number:" + selected);
        return selected_obj;
    }


}
