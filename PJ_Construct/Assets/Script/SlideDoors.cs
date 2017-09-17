using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideDoors : MonoBehaviour {
    [Header("작동 여부")]
    public bool can_open = true;    //false:트리거 작동 안함.
    public float speed = 20f;        //문짝 열리는 속도
    public float length = 1f;       //문짝 열리는 크기

    public enum SideOfRotation { X, Y, Z }
    [Header("축 설정")]
    public SideOfRotation RotationSide;

    public enum SideOfPosition { left, right}
    [Header("열리는 방향 설정")]
    public SideOfPosition PositionSide;


    public Transform door;

    int swt = 1;            //오픈&클로즈 스위치

    Vector3 start;

    // Use this for initialization
    void Start () {
        door = GetComponent<Transform>();
        start = transform.position;
    }

    // Update is called once per frame
    void Update() { }
    public void Switching()
    {
        if (swt == 1)
        {
            open_door();
        }
        if (swt == 0)
        {
            close_door();
        }
        swt ^= 1;

    }
	void open_door()
    {
        if (can_open)
        {
            StopCoroutine("move_close_door");
            StartCoroutine("move_open_door");
        }      	
	}
    void close_door()
    {
        if (can_open)
        {
            StopCoroutine("move_open_door");
            StartCoroutine("move_close_door");
        }
    }

    IEnumerator move_open_door()
    {

        if (PositionSide == SideOfPosition.left)
        {
            if (RotationSide == SideOfRotation.X)
                while (true)
                {
                    transform.position += new Vector3(Time.deltaTime, 0, 0);
                    yield return new WaitForSeconds(1 / speed);
                    if (start.x + length < transform.position.x)
                    {
                        yield break;
                    }
                }
            if (RotationSide == SideOfRotation.Y)
                while (true)
                {
                    transform.position += new Vector3(0, Time.deltaTime, 0);
                    yield return new WaitForSeconds(1 / speed);
                    if (start.y + length < transform.position.y)
                    {
                        yield break;
                    }
                }
            if (RotationSide == SideOfRotation.Z)
                while (true)
                {
                    transform.position += new Vector3(0, 0, Time.deltaTime);
                    yield return new WaitForSeconds(1 / speed);
                    if (start.z + length < transform.position.z)
                    {
                        yield break;
                    }
                }
        }
        //------------------
        if (PositionSide == SideOfPosition.right)
        {
            if (RotationSide == SideOfRotation.X)
                while (true)
                {
                    transform.position += new Vector3(-Time.deltaTime, 0, 0);
                    yield return new WaitForSeconds(1 / speed);
                    if (start.x - length > transform.position.x )
                    {
                        yield break;
                    }
                }
            if (RotationSide == SideOfRotation.Y)
                while (true)
                {
                    transform.position += new Vector3(0, -Time.deltaTime, 0);
                    yield return new WaitForSeconds(1 / speed);
                    if (start.y - length > transform.position.y)
                    {
                        yield break;
                    }
                }
            if (RotationSide == SideOfRotation.Z)
                while (true)
                {
                    transform.position += new Vector3(0, 0, -Time.deltaTime);
                    yield return new WaitForSeconds(1 / speed);
                    if (start.z - length > transform.position.z)
                    {
                        yield break;
                    }
                }
        }

    }
    IEnumerator move_close_door()
    {
        Debug.Log("X");
        if (PositionSide == SideOfPosition.left)
        {
            if (RotationSide == SideOfRotation.X)
                while (true)
                {
                    transform.position += new Vector3(-Time.deltaTime, 0, 0);
                    yield return new WaitForSeconds(1 / speed);
                    if (start.x > transform.position.x)
                    {
                        //StopCoroutine("move_close_door");
                        yield break;
                    }
                }
            if (RotationSide == SideOfRotation.Y)
                while (true)
                {
                     transform.position += new Vector3(0, -Time.deltaTime, 0);
                    yield return new WaitForSeconds(1 / speed);
                    if (start.y > transform.position.y)
                    {
                        //StopCoroutine("move_close_door");
                        yield break;
                    }
                }
            if (RotationSide == SideOfRotation.Z)
                while (true)
                {
                    transform.position += new Vector3(0, 0, -Time.deltaTime);
                    yield return new WaitForSeconds(1 / speed);
                    if (start.z > transform.position.z)
                    {
                        //StopCoroutine("move_close_door");
                        yield break;
                    }
                }
        }
        //------------
        if (PositionSide == SideOfPosition.right)
        {
            if (RotationSide == SideOfRotation.X)
                while (true)
                {
                    transform.position += new Vector3(Time.deltaTime, 0, 0);
                    yield return new WaitForSeconds(1 / speed);
                    if (start.x < transform.position.x)
                    {
                        //StopCoroutine("move_close_door");
                        yield break;
                    }
                }
            if (RotationSide == SideOfRotation.Y)
                while (true)
                {
                    transform.position += new Vector3(0, Time.deltaTime, 0);
                    yield return new WaitForSeconds(1 / speed);
                    if (start.y < transform.position.y)
                    {
                        //StopCoroutine("move_close_door");
                        yield break;
                    }
                }
            if (RotationSide == SideOfRotation.Z)
                while (true)
                {
                    transform.position += new Vector3(0, 0, Time.deltaTime);
                    yield return new WaitForSeconds(1 / speed);
                    if (start.z < transform.position.z)
                    {
                        //StopCoroutine("move_close_door");
                        yield break;
                    }
                }
        }

    }

}
