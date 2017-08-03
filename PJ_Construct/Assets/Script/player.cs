using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {
    public Rigidbody rbody;
    public float speed = 10f;

    private float inputH; // 수평 입력
    private float inputV; // 수직 입력

    // Use this for initialization
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // 파라미터 (움직이는 각도, 애니메이션 이용)
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


        // 회전 관련
        int rot = 100;                 //로테이션
        int ind = 0;
        float rotP = 0f;            //로테이션 파워
        if (Input.GetKey("z"))
            ind--;
        else if (Input.GetKey("c"))
            ind++;
        else
            ind = 0;

        
        rotP = rot * ind * Time.deltaTime;
 //       print("rot:"+rotP);

        transform.Rotate(0, rotP, 0);
        //rbody.transform.rotation = Quaternion.Euler(0, rotP, 0);

        // 회전 부분 끝


    }
}
