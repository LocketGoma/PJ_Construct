using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detected : MonoBehaviour {
    // GENERAL SETTINGS
    [Header("General Settings")]
    [Tooltip("How close the player has to be in order to be able to open/close the door.")]
    public float Reach = 4.0F;
    [Header("target user")]
    public GameObject user;
    public Camera cam;
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
    [Tooltip("furniture delete")]
    public string delete_furniture = "k";
    bool select_lock = false;

    [Tooltip("The image or text that is shown in the middle of the the screen.")]
    public GameObject CrosshairPrefab;
    [Tooltip("if seleted something")]
    public GameObject CrosshairSeleted;
    [HideInInspector]
    public GameObject CrosshairPrefabInstance; // (자동 카피됨. '원본의 손실 대처')
    [HideInInspector]
    public GameObject CrosshairSeletedInstance;
    bool isSeleted = false;

    // 디버그
    [Header("Debug Settings")]
    public bool isOn = true;
    [Tooltip("The color of the debugray that will be shown in the scene view window when playing the game.")]
    public Color DebugRayColor;
    [Tooltip("The opacity of the debugray.")]
    [Range(0.0F, 1.0F)]
    public float Opacity = 1.0F;

    private GameObject master; //최상단 컨트롤 오브젝트 불러오는 용도.
    private player playerscript;    //유저 스크립트
    bool temp_move = false;
    void Start()
    {
        gameObject.name = "Player";
        gameObject.tag = "Player";
        DebugRayColor.a = Opacity; // DebugRayColor 알파값 설정. 0 : 안보임 / 1:진하게 보임. 값은 0~1
        master = GameObject.FindGameObjectWithTag("GameController");    //'gamecontroller' 로 설정된 오브젝트 찾기. 해당 오브젝트 = 마스터 컨트롤 오브젝트.
        playerscript = user.GetComponent<player>();
        if (CrosshairPrefab == null) Debug.Log("<color=yellow><b>No CrosshairPrefab was found.</b></color>"); // Return an error if no crosshair was specified
        else
        {
            CrosshairPrefabInstance = Instantiate(CrosshairPrefab); // 크로스헤어 표기
            CrosshairPrefabInstance.transform.SetParent(transform, true); // 크로스헤어의 부모를 player 인스턴스로 적용.
        }
    }

    void Update()
    {
        // Set origin of ray to 'center of screen'
        
        if (Input.GetKey(KeyCode.Joystick1Button8)&&temp_move==false)
        {            
            playerscript.release();
            temp_move = true;
        }        
        else if (temp_move == true)
        {
            playerscript.locker();
            temp_move = false;
        }
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0F));                  //메인 카메라...
        if (isSeleted == true)
        {
            Destroy(CrosshairPrefabInstance);
            CrosshairPrefabInstance = Instantiate(CrosshairPrefab);
            CrosshairPrefabInstance.transform.SetParent(transform, true);
            isSeleted = false;
        }

 
        
        if (Input.GetKeyDown("r") || Input.GetKeyDown(KeyCode.Joystick1Button9))                    //emergency cancel
        {
            playerscript.release();
        }
        
        RaycastHit hit; // Variable reading information about the collider hit
        // Cast ray from center of the screen towards where the player is looking

        if (Physics.Raycast(ray, out hit, Reach))
        {
            if (isSeleted == false)
            {
                Destroy(CrosshairPrefabInstance);
                CrosshairPrefabInstance = Instantiate(CrosshairSeleted);
                CrosshairPrefabInstance.transform.SetParent(transform, true);
                isSeleted = true;
            }
            if (hit.collider.tag == "Door")
            {
                InReach = true;
                GameObject Door = hit.transform.gameObject;
                Doors dooropening = Door.GetComponent<Doors>();
                if (Input.GetKeyDown(action_door) || Input.GetKeyDown(KeyCode.Joystick1Button0))
                {
                    // 문짝 스크립트 작동
                    if (dooropening.RotationPending == false)
                    {
                        StartCoroutine(hit.collider.GetComponent<Doors>().Move());
                    }
                }
            }
            if (hit.collider.tag == "SlideDoor")
            {
     
                InReach = true;
                GameObject Door = hit.transform.gameObject;
                SlideDoors dooropening = Door.GetComponent<SlideDoors>();
                if (Input.GetKeyDown(action_door) || Input.GetKeyDown(KeyCode.Joystick1Button0))
                {
                    dooropening.Switching();
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
            else if (hit.collider.tag == "Furniture")
            {
                //                Debug.Log("selected");
                InReach = true;

                GameObject furniture = hit.transform.gameObject;
                Control_object control = furniture.GetComponent<Control_object>();
                if (Input.GetKeyDown(action_selete) || Input.GetKeyDown(KeyCode.Joystick1Button3))
                {
                    if (control.Locker() == 1)
                        playerscript.locker();
                    else if (control.world_locker() == 0)
                        playerscript.release();
                }
                if (Input.GetKeyDown(delete_furniture) || Input.GetKeyDown(KeyCode.Joystick1Button1))
                {
                    Destroy(furniture);
                    control.clear();
                    playerscript.release();
                }

            }
            else if (Input.GetButtonDown("Deploy") || Input.GetKeyDown(KeyCode.Joystick1Button2)) //else if (Input.GetKeyDown(drop))
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
        {
            InReach = false;
        }

        //Draw the ray as a colored line for debugging purposes.
        if (isOn)
        Debug.DrawRay(ray.origin, ray.direction * Reach, DebugRayColor);
    }
}
