using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_object : MonoBehaviour {
    // 캐릭터 컨트롤에서 '오브젝트가 선택되었을 시 움직이지 않게' 설정할 것.
    // 선택시에는 레이캐스트로 '화면에 보이게', 릴리즈 (다시 플레이어가 움직이게 할때)는 보이지 않더라도 작동하게 할것.

    int locked = 0;           //1:lock / 0:non_lock
    static int world_locked = 0; //1:lock / 0:non_lock <-  락 걸리는건 단 하나!
    public float speed = 10f;
    private float inputH; // 수평 입력
    private float inputV; // 수직 입력  

    public GameObject marker;
    bool maked = false;

 //   Renderer renderer;

    // Use this for initialization
    void Start () {
       
//        Instantiate(marker, new Vector3(transform.position.x, transform.position.y+1, transform.position.z), transform.rotation).transform.SetParent(transform);
        //       renderer = marker.GetComponent<Renderer>();
        //       renderer.material.shader = Shader.Find("Standard");
        //       renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0.0f);
 //       marker.SetActive(false);
    }
	
	// Update is called once per frame
    void FixedUpdate()
    {
        is_control();
    }

    public int Locker()
    {
        if (locked == 1)
        {
            world_locked = 0;
            return locked = 0;
        }
        if (world_locked == 1)
        {
            return locked = 0;
        }
        world_locked = 1;
        return locked = 1;
    }
    void is_control()
    {
        if (locked == 1)
        {
            Debug.Log("locking");
            movement();
            shadering();
        }
        else
        {
            maked = false;
            // 이하, '모든' 차일드 삭제.
            Transform[] childList = GetComponentsInChildren<Transform>(true);
            if (childList != null)      
            {
                for (int i = 0; i < childList.Length; i++)      
                {
                    if (childList[i] != transform)
                        Destroy(childList[i].gameObject);
                }
            }
            //https://goo.gl/hJ5GhC 참조.

            //            renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0.0f);
        }
    }
    void movement()     //일반적인 움직임 관련.
    {
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");


        // 이동 부분
        float moveX = inputH * speed * Time.deltaTime;
        float moveZ = inputV * speed * Time.deltaTime;
        //if (moveX != 0.0) print(moveX);
        //if (moveZ != 0.0) print(moveZ);


        //rbody.velocity = new Vector3(moveX, 0f, moveZ);
        transform.position += transform.forward * moveZ;
        transform.position += transform.right * moveX;
        // 이동 부분 관련 끝
    }
    void shadering()   //'벽 뒤로 가기' 쉐이더 입히기.
    {
        if (!maked)     // 이건 선택된 오브젝트 위에 블럭 얹기
        {
            Instantiate(marker, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation).transform.SetParent(transform);
            maked = true;
        }

        //        renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0.7f);

    }
   public int world_locker()
    {
        return world_locked;
    }

}
