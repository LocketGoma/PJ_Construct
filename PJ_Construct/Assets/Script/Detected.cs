using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detected : MonoBehaviour {
    // GENERAL SETTINGS
    [Header("General Settings")]
    [Tooltip("How close the player has to be in order to be able to open/close the door.")]
    public float Reach = 4.0F;
    [HideInInspector]
    public bool InReach;            //<- 어따 씀?
    [Header("Key binding")]
    [Tooltip("open door")]
    public string action_door = "e";
    [Tooltip("control to change color")]
    public string action_color = "x";
    [Tooltip("select furniture")]
    public string action_selete = "c";
    [Tooltip("item drop")]
    public string drop = "q";
    bool select_lock = false;

    [Tooltip("The image or text that is shown in the middle of the the screen.")]
    public GameObject CrosshairPrefab;
    [HideInInspector]
    public GameObject CrosshairPrefabInstance; // (자동 카피됨. '원본의 손실 대처')

    // 디버그
    [Header("Debug Settings")]
    public bool isOn = true;
    [Tooltip("The color of the debugray that will be shown in the scene view window when playing the game.")]
    public Color DebugRayColor;
    [Tooltip("The opacity of the debugray.")]
    [Range(0.0F, 1.0F)]
    public float Opacity = 1.0F;

    private GameObject master; //최상단 컨트롤 오브젝트 불러오는 용도.

    void Start()
    {
        gameObject.name = "Player";
        gameObject.tag = "Player";
        DebugRayColor.a = Opacity; // DebugRayColor 알파값 설정. 0 : 안보임 / 1:진하게 보임. 값은 0~1
        master = GameObject.FindGameObjectWithTag("GameController");    //'gamecontroller' 로 설정된 오브젝트 찾기. 해당 오브젝트 = 마스터 컨트롤 오브젝트.
    }

    void Update()
    {
        // Set origin of ray to 'center of screen'
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0F));                  //메인 카메라...

        RaycastHit hit; // Variable reading information about the collider hit
        // Cast ray from center of the screen towards where the player is looking
        if (Physics.Raycast(ray, out hit, Reach))
        {
            if (hit.collider.tag == "Door")
            {
                InReach = true; 
                GameObject Door = hit.transform.gameObject;
                Doors dooropening = Door.GetComponent<Doors>();
                if (Input.GetKey(action_door))                {
                    // 문짝 스크립트 작동
                    if (dooropening.RotationPending == false)
                    {
                        StartCoroutine(hit.collider.GetComponent<Doors>().Move());
                    }
                }
            }
            else if (hit.collider.tag == "Wall")
            {
                InReach = true;
                GameObject Wall = hit.transform.gameObject;
                Wall_color Wall_color = Wall.GetComponent<Wall_color>();
                if (Input.GetKey(action_color))
                {
                    hit.collider.GetComponent<Wall_color>().change();
                }

            }
            else if (hit.collider.tag== "Furniture")
            {
                InReach = true;
                GameObject furniture = hit.transform.gameObject;
                Control_object control = furniture.GetComponent<Control_object>();
                if (Input.GetKeyDown(action_selete))
                {
                    control.Locker();
                }
            }
            else if (Input.GetKeyDown(drop))
            {                
                Vector3 targetpos = hit.point;
                targetpos.y += 1;       //pos.y 그대로 둘 시 바닥에 처박힘.
                Instantiate(master.GetComponent<Group_Furniture>().get_Selected(), targetpos, transform.rotation);

            }
            else
            {
                InReach = false;
            }
        }
        else
            InReach = false;

        //Draw the ray as a colored line for debugging purposes.
        if (isOn)
        Debug.DrawRay(ray.origin, ray.direction * Reach, DebugRayColor);
    }
}
