using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class h005_openDoor_S : MonoBehaviour {
    

    public GameObject[] allDoor;
    public GameObject[] keyNumbers;
    public GameObject keyNumberP;

    public bool on = true;
    public bool colliderOn = true;

    int keyCount = 0;
    int allCount = 0;

    Vector3 triggerCollider;
    Vector3 half;  // triggerCollider half scale
    Vector3 parentNowPosition;  
    Vector3 parentLastPosition;

    float dis;
    

    // Use this for initialization
    void Start() {
        triggerCollider = transform.localScale;
        half = new Vector3(triggerCollider.x / 2.0f, triggerCollider.y / 2.0f, triggerCollider.z / 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        keyEnter();
        if (colliderOn == true)
        {
            colliderOn = false;                
            StartCoroutine(waitMoveCheck(0.2f));
        }
        dis = Vector3.Distance(parentNowPosition, parentLastPosition);
        exitDoor();
    }

    void keyEnter()
    {
        if (Input.inputString != "" && Input.GetButton("Fire2") == false)
        {
            char inputKey = Input.inputString[0];

            for (int i = 0; i < allDoor.Length; i++)
            {
                if (inputKey == (i + 1).ToString()[0] && i < 9)
                {
                    allDoor[i].SendMessage("sandAnimOpen");
                    allDoor[i].SendMessage("sandAnimOn", true);
                }
            }
        }
        else if (Input.inputString != "" && Input.GetButton("Fire2") == true)
        {
            char inputKey = Input.inputString[0];

            for (int i = 0; i < allDoor.Length; i++)
            {
                if (inputKey == i.ToString()[0])
                {
                    if (i + 9 >= allDoor.Length)
                    {
                        Debug.Log("Out of range");
                    }
                    else
                    {
                        allDoor[i + 9].SendMessage("sandAnimOpen");
                        allDoor[i + 9].SendMessage("sandAnimOn", true);
                    }
                }
            }
        }
    }

    IEnumerator waitMoveCheck(float waitTime)
    {
        parentNowPosition = transform.parent.position;
        yield return new WaitForSeconds(waitTime);
        parentLastPosition = parentNowPosition;
        colliderOn = true;
    }   

    void OnTriggerStay(Collider other)
    {
        if (on == true && dis > 0.1f)
        {
            findDoor();
        }        
        if(other.gameObject.tag == "DOOR" || other.gameObject.tag == "DRAWER")
        {
            if (dis > 0.1f)
            {
                addFindDoor();
            }
        }
    }     
          
    void findDoor()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, half);
        keyCount = 0;
        allCount = 0;

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "DOOR" || colliders[i].gameObject.tag == "DRAWER")
            {
                on = false;
                keyCount++;
            }
        }        
        
        keyNumbers = new GameObject[keyCount];
        allDoor = new GameObject[keyCount];        

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "DOOR" || colliders[i].gameObject.tag == "DRAWER")
            {
                allDoor[allCount] = colliders[i].gameObject;

                keyNumbers[allCount] = (GameObject)Instantiate(keyNumberP, colliders[i].transform.position, colliders[i].transform.rotation);
                keyNumbers[allCount].transform.SetParent(colliders[i].transform);

                Vector3 center = colliders[i].GetComponent<BoxCollider>().center;
                float localX = (colliders[i].GetComponent<BoxCollider>().size.x) / 2.0f;

                keyNumbers[allCount].transform.localPosition = new Vector3(center.x + localX, center.y, center.z);
                keyNumbers[allCount].GetComponent<TextMesh>().text = (allCount + 1).ToString();

                allCount++;
            }
        }    
    }
    
    void addFindDoor()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, half);
        GameObject[] sameDoor = new GameObject[colliders.Length];

        int same = 0;
        int add = 0;
        bool pass = false;

        for(int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "DOOR" || colliders[i].gameObject.tag == "DRAWER")
            {
                for (int j = 0; j < allDoor.Length; j++)
                {
                    if (colliders[i] == allDoor[j])
                    {
                        sameDoor[i] = colliders[i].gameObject;
                        same++;
                        pass = true;
                    }
                }

                if (pass == false)
                {
                    add++;
                }
            }
        }

        if(add > 0)
        {
            destroy();
            on = true;
            findDoor();
        }
    }


    void exitDoor()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, half);
        int dNum = 0;        

        if (dis > 0.1f)
        {
            for (int i = 1; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.tag == "DOOR" || colliders[i].gameObject.tag == "DRAWER")
                {
                    dNum++;
                }
            }
            if (dNum == 0)
            {
                destroy();
            }
        }
    }  
    void destroy()
    {
        for (int i = 0; i < keyNumbers.Length; i++)
        {
            Destroy(keyNumbers[i]);
            keyNumbers[i] = null;
            allDoor[i] = null;
            on = true;
        }        
    }

}
