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

    int isUse = 1;

	void Start () {
        // if(furniture.Length<selected||selected<0)
        selected_obj = furniture[0];
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(back) || Input.GetKeyDown(KeyCode.Joystick1Button4))
        {
            changer(-1);
            Seeing();
        }
        if (Input.GetKeyDown(next) || Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            changer(1);
            Seeing();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button6))
        {
            isUse ^= 1;
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
    void Seeing()
    {
        if (isUse == 1)
        {
            StartCoroutine("Seeker");
        }
    //    

    }
    IEnumerator Seeker()        // 물체 임시로 띄워서 보게 하는 코루틴. 3초 대기.
    {
        GameObject temp = selected_obj;
        GameObject point = null;


        GameObject pin = GameObject.FindGameObjectWithTag("Pin");
        point = Instantiate(temp, new Vector3(pin.transform.position.x, pin.transform.position.y, pin.transform.position.z), pin.transform.rotation) as GameObject;
        point.GetComponent<Rigidbody>().isKinematic = true;
        Destroy(point.GetComponent<Rigidbody>());
        point.GetComponent<Collider>().enabled = false;
        Debug.Log(point);
        yield return new WaitForSeconds(3);
        {
            Destroy(point.gameObject);
        }
    }


}
