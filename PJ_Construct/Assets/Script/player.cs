using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {
    [Header("HEAD Camera")]
    public Transform LookTransform;

    public Rigidbody rbody;
    public float speed = 10f;

    private float inputH; // 수평 입력
    private float inputV; // 수직 입력


    bool locked=true;         //T: 사용중, F:사용불가
    public Vector3 Gravity = Vector3.down * 9.81f;
    public float RotationRate = 0.1f;

    // Use this for initialization
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        GetComponent<Rigidbody>().freezeRotation = true;            //빙글빙글 도는것 방지.
        GetComponent<Rigidbody>().useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*
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

        */

        // 회전 관련
/*
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

        transform.Rotate(0, rotP, 0);

        // 회전 부분 끝
*/

    }

    void FixedUpdate()
    {
        bool grounded = Physics.Raycast(transform.position, Gravity.normalized, 1.1f);

        Vector3 gravityForward = Vector3.Cross(Gravity, transform.right);
        Quaternion targetRotation = Quaternion.LookRotation(gravityForward, -Gravity);
        GetComponent<Rigidbody>().rotation = Quaternion.Lerp(GetComponent<Rigidbody>().rotation, targetRotation, RotationRate);
        if (locked)
        {
            Vector3 forward = Vector3.Cross(transform.up, -LookTransform.right).normalized;         //상하 힘 *            // transform.up = Y축
            Vector3 right = Vector3.Cross(transform.up, LookTransform.forward).normalized;          //좌우 힘 *

            Vector3 BaseVelocity = (forward * Input.GetAxis("Vertical") + right * Input.GetAxis("Horizontal")) * speed;
            Vector3 localVelocity = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);
            Vector3 ChangeVelocity = transform.InverseTransformDirection(BaseVelocity) - localVelocity;

            GetComponent<Rigidbody>().AddForce(ChangeVelocity, ForceMode.VelocityChange);
            GetComponent<Rigidbody>().AddForce(Gravity * GetComponent<Rigidbody>().mass);
        }
        
       

    }
    public void locker()
    {
        Debug.Log("user lock");
        locked = false;
    }
    public void release()
    {
        locked = true;
    }
}
