using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class h005_openAnim_S : MonoBehaviour {

    public enum enumType { Drawer, Door }
    public enum enumDir { Left, Right, Middle }

    public enumType Type;
    public enumDir Direction;
    public bool open = false;
    public bool animOn = false;

    public float drawerRange = 1.2f;
    public float doorRange = 90.0f;

    Vector3 firstPosition;
    Quaternion firstRotation;
    

    // Use this for initialization
    void Start () {
        firstPosition = transform.localPosition;
        firstRotation = transform.localRotation;
    }
	
	// Update is called once per frame
	void Update () {

        if (animOn == true)
        {            
            if (Type == enumType.Drawer)
            {
                if (open == true)
                {
                    if (Direction == enumDir.Left)
                    {
                        float localZ = (transform.GetComponent<BoxCollider>().size.z) / drawerRange;
                        Vector3 moveDrawer = new Vector3(transform.localPosition.x, transform.localPosition.y, firstPosition.z + localZ);
                        float speed = 2.0f * Time.deltaTime;
                        transform.localPosition = Vector3.Lerp(transform.localPosition, moveDrawer, speed);
                        return;
                    }
                    else if (Direction == enumDir.Right)
                    {
                        float localZ = (transform.GetComponent<BoxCollider>().size.z) / drawerRange;
                        Vector3 moveDrawer = new Vector3(transform.localPosition.x, transform.localPosition.y, firstPosition.z - localZ);
                        float speed = 2.0f * Time.deltaTime;
                        transform.localPosition = Vector3.Lerp(transform.localPosition, moveDrawer, speed);
                        return;
                    }
                    else if (Direction == enumDir.Middle)
                    {
                        float localX = (transform.GetComponent<BoxCollider>().size.x) / drawerRange;
                        Vector3 moveDrawer = new Vector3(firstPosition.x + localX, transform.localPosition.y, transform.localPosition.z);
                        float speed = 2.0f * Time.deltaTime;
                        transform.localPosition = Vector3.Lerp(transform.localPosition, moveDrawer, speed);
                        return;
                    }
                    return;
                }
                else if (open == false)
                {
                    if (Direction == enumDir.Left)
                    {                        
                        float speed = 2.0f * Time.deltaTime;
                        transform.localPosition = Vector3.Lerp(transform.localPosition, firstPosition, speed);
                        return;
                    }
                    else if (Direction == enumDir.Right)
                    {                        
                        float speed = 2.0f * Time.deltaTime;
                        transform.localPosition = Vector3.Lerp(transform.localPosition, firstPosition, speed);
                        return;
                    }
                    else if (Direction == enumDir.Middle)
                    {                        
                        float speed = 2.0f * Time.deltaTime;
                        transform.localPosition = Vector3.Lerp(transform.localPosition, firstPosition, speed);
                        return;
                    }                 
                    return;
                }
            }
            else if (Type == enumType.Door)
            {
                if (open == true)
                {                    
                    if (Direction == enumDir.Left)
                    {
                        Quaternion secondRotation = Quaternion.Euler(firstRotation.x, doorRange, firstRotation.z);
                        transform.localRotation = Quaternion.Lerp(transform.localRotation, secondRotation, 2.0f * Time.deltaTime);
                        return;
                    }
                    else if (Direction == enumDir.Right)
                    {
                        Quaternion secondRotation = Quaternion.Euler(firstRotation.x, -doorRange, firstRotation.z);
                        transform.localRotation = Quaternion.Lerp(transform.localRotation, secondRotation, 2.0f * Time.deltaTime);
                        return;

                    }else if (Direction == enumDir.Middle)
                    {
                        Quaternion secondRotation = Quaternion.Euler(firstRotation.x, firstRotation.y, doorRange);
                        transform.localRotation = Quaternion.Lerp(transform.localRotation, secondRotation, 2.0f * Time.deltaTime);
                        return;
                    }
                    return;
                }
                else if (open == false)
                {                    
                    if (Direction == enumDir.Left)
                    {                        
                        transform.localRotation = Quaternion.Lerp(transform.localRotation, firstRotation, 2.0f * Time.deltaTime);
                        return;
                    }
                    else if (Direction == enumDir.Right)
                    {                        
                        transform.localRotation = Quaternion.Lerp(transform.localRotation, firstRotation, 2.0f * Time.deltaTime);
                        return;
                    }
                    else if(Direction == enumDir.Middle)
                    {                        
                        transform.localRotation = Quaternion.Lerp(transform.localRotation, firstRotation, 2.0f * Time.deltaTime);
                        return;
                    }
                    return;
                }
            }          
            animOn = false;
        }
    }

    void sandAnimOpen()
    {
        switch(open)
        {
            case true:
                open = false;
                break;
            case false:
                open = true;
                break;
            default:
                break;
        }
    }

    void sandAnimOn(bool aOn)
    {
        switch (aOn)
        {
            case true:
                animOn = true;
                break;
            case false:
                animOn = false;
                break;
            default:
                break;
        }
    }

}
