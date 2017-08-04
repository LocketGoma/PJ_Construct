using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detected : MonoBehaviour {
    // GENERAL SETTINGS
    [Header("General Settings")]
    [Tooltip("How close the player has to be in order to be able to open/close the door.")]
    public float Reach = 4.0F;
    [HideInInspector]
    public bool InReach;
    public string Character = "e";
    [Tooltip("control to change color")]
    public string changer = "x";



    [Tooltip("The image or text that is shown in the middle of the the screen.")]
    public GameObject CrosshairPrefab;
    [HideInInspector]
    public GameObject CrosshairPrefabInstance; // A copy of the crosshair prefab to prevent data corruption

    // DEBUG SETTINGS
    [Header("Debug Settings")]
    [Tooltip("The color of the debugray that will be shown in the scene view window when playing the game.")]
    public Color DebugRayColor;
    [Tooltip("The opacity of the debugray.")]
    [Range(0.0F, 1.0F)]
    public float Opacity = 1.0F;

    void Start()
    {
        gameObject.name = "Player";
        gameObject.tag = "Player";

    }

    void Update()
    {
        // Set origin of ray to 'center of screen' and direction of ray to 'cameraview'
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0F));                  //메인 카메라...

        RaycastHit hit; // Variable reading information about the collider hit
        Debug.Log("1");
        // Cast ray from center of the screen towards where the player is looking
        if (Physics.Raycast(ray, out hit, Reach))
        {
            Debug.Log("2");
            if (hit.collider.tag == "Door")
            {
                Debug.Log("3");
                InReach = true;

                // Display the UI element when the player is in reach of the door
 

                // Give the object that was hit the name 'Door'
                GameObject Door = hit.transform.gameObject;

                // Get access to the 'Door' script attached to the object that was hit
                Doors dooropening = Door.GetComponent<Doors>();

                if (Input.GetKey(Character))
                {
                    // Open/close the door by running the 'Open' function found in the 'Door' script
                    if (dooropening.RotationPending == false)
                    {
                        StartCoroutine(hit.collider.GetComponent<Doors>().Move());
                        Debug.Log("4");
                    }
                }
            }
            else if (hit.collider.tag == "Wall")
            {
                Debug.Log("3_2");
                InReach = true;

                GameObject Wall = hit.transform.gameObject;

                Wall_color Wall_color = Wall.GetComponent<Wall_color>();
                if (Input.GetKey(changer))
                {
                    Debug.Log("4_2");
                    hit.collider.GetComponent<Wall_color>().change();
                }

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
        Debug.DrawRay(ray.origin, ray.direction * Reach, DebugRayColor);
    }
}
