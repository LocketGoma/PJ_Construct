using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//만들 스크립트
public class Doors : MonoBehaviour {
    [Header("회전 세팅")]
    [Tooltip("문 회전 초기값 설정")]
    public float InitialAngle = 0f;
    [Tooltip("문 회전값 설정")]
    public float RotationAngle = 90.0F;
    public enum SideOfRotation { Left, Right }
    public SideOfRotation RotationSide;
    [Tooltip("문 열리는 속도 설정")]
    public float Speed = 3F;
    [Tooltip("0 = infinite times")]     //?
    public int TimesMoveable = 0;

    
    public enum TypeOfHinge { Centered, CorrectlyPositioned } //아니 구현을 centered만 해놓고...
    [Header("힌지 세팅")]
    public TypeOfHinge HingeType;

    public enum PositionOfHinge { Left, Right }
    [ConditionalHide("HingeType", true, false)]         //?!
    public PositionOfHinge HingePosition;

    // PRIVATE SETTINGS - NOT VISIBLE FOR THE USER
    int TimesRotated = 0;
    [HideInInspector]
    public bool RotationPending = false; // Needs to be public because Detection.cs has to access it

    // DEBUG SETTINGS
    [Header("Debug Settings")]
    [Tooltip("힌지를 색상 블럭으로 보여줍니다.")]
    public bool VisualizeHinge = false;
    [Tooltip("힌지블럭 색상 선택")]
    public Color HingeColor = Color.yellow;

    // Define an initial and final rotation
    Quaternion FinalRot, InitialRot;
    int State;

    // Create a hinge
    GameObject hinge;

    // An offset to take into account the original rotation of a 3rd party door
    Quaternion RotationOffset;

    // Use this for initialization
    void Start () {
        // Door 태그 이용
        gameObject.tag = "Door";

        RotationOffset = transform.rotation;

        if (HingeType == TypeOfHinge.Centered)      //나머지는 고정인감
        {
            hinge = new GameObject("Hinge");

            // Calculate sine/cosine of initial angle (needed for hinge positioning)
            // 아 이거 수치각 리턴해주는거네 euler.y*pi / 180 = 오일러값이 라디안으로 튀어나오고...        
            float CosDeg = Mathf.Cos((transform.eulerAngles.y * Mathf.PI) / 180);
            float SinDeg = Mathf.Sin((transform.eulerAngles.y * Mathf.PI) / 180);

            // Read transform (position/rotation/scale) of the door
            float PosDoorX = transform.position.x;
            float PosDoorY = transform.position.y;
            float PosDoorZ = transform.position.z;

            float RotDoorX = transform.localEulerAngles.x;
            float RotDoorZ = transform.localEulerAngles.z;

            float ScaleDoorX = transform.localScale.x;
            float ScaleDoorZ = transform.localScale.z;

            // Create a placeholder/temporary object of the hinge's position/rotation
            Vector3 HingePosCopy = hinge.transform.position;
            Vector3 HingeRotCopy = hinge.transform.localEulerAngles;

            if (HingePosition == PositionOfHinge.Left)
            {
                if (transform.localScale.x > transform.localScale.z)
                {
                    HingePosCopy.x = (PosDoorX - (ScaleDoorX / 2 * CosDeg));
                    HingePosCopy.z = (PosDoorZ + (ScaleDoorX / 2 * SinDeg));
                    HingePosCopy.y = PosDoorY;

                    HingeRotCopy.x = RotDoorX;
                    HingeRotCopy.y = -InitialAngle;
                    HingeRotCopy.z = RotDoorZ;
                }

                else
                {
                    HingePosCopy.x = (PosDoorX + (ScaleDoorZ / 2 * SinDeg));
                    HingePosCopy.z = (PosDoorZ + (ScaleDoorZ / 2 * CosDeg));
                    HingePosCopy.y = PosDoorY;

                    HingeRotCopy.x = RotDoorX;
                    HingeRotCopy.y = -InitialAngle;
                    HingeRotCopy.z = RotDoorZ;
                }
            }
            if (HingePosition == PositionOfHinge.Right)
            {
                if (transform.localScale.x > transform.localScale.z)
                {
                    HingePosCopy.x = (PosDoorX + (ScaleDoorX / 2 * CosDeg));
                    HingePosCopy.z = (PosDoorZ - (ScaleDoorX / 2 * SinDeg));
                    HingePosCopy.y = PosDoorY;

                    HingeRotCopy.x = RotDoorX;
                    HingeRotCopy.y = -InitialAngle;
                    HingeRotCopy.z = RotDoorZ;
                }

                else
                {
                    HingePosCopy.x = (PosDoorX - (ScaleDoorZ / 2 * SinDeg));
                    HingePosCopy.z = (PosDoorZ - (ScaleDoorZ / 2 * CosDeg));
                    HingePosCopy.y = PosDoorY;

                    HingeRotCopy.x = RotDoorX;
                    HingeRotCopy.y = -InitialAngle;
                    HingeRotCopy.z = RotDoorZ;
                }
            }


            //힌지 포인팅
            hinge.transform.position = HingePosCopy;
            transform.parent = hinge.transform;
            hinge.transform.localEulerAngles = HingeRotCopy;

            //디버그 (힌지 블럭 보여주기)
            if (VisualizeHinge == true)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = HingePosCopy;
                cube.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                cube.GetComponent<Renderer>().material.color = HingeColor;
            }

        }


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator Move()
    {
        // ANGLES
        if (RotationSide == SideOfRotation.Left)
        {
            InitialRot = Quaternion.Euler(0, -InitialAngle, 0);
            FinalRot = Quaternion.Euler(0, -InitialAngle - RotationAngle, 0);
        }

        if (RotationSide == SideOfRotation.Right)
        {
            InitialRot = Quaternion.Euler(0, -InitialAngle, 0);
            FinalRot = Quaternion.Euler(0, -InitialAngle + RotationAngle, 0);
        }

        if (TimesRotated < TimesMoveable || TimesMoveable == 0)             //용도가 왠지... '움직이고 있냐' / '아니냐' 정하는거 같음.
        {
            if (HingeType == TypeOfHinge.Centered)
            {

                // Change state from 1 to 0 and back ( = alternate between FinalRot and InitialRot)             // a ^= b => a = a^b
                if (hinge.transform.rotation == (State == 0 ? FinalRot : InitialRot)) State ^= 1;

                // Set 'FinalRotation' to 'FinalRot' when moving and to 'InitialRot' when moving back
                Quaternion FinalRotation = ((State == 0) ? FinalRot : InitialRot);

                // Make the door/window rotate until it is fully opened/closed
                while (Mathf.Abs(Quaternion.Angle(FinalRotation, hinge.transform.rotation)) > 0.01f)
                {
                    RotationPending = true;
                    hinge.transform.rotation = Quaternion.Lerp(hinge.transform.rotation, FinalRotation, Time.deltaTime * Speed);
                    yield return new WaitForEndOfFrame();
                }

                RotationPending = false;
                if (TimesMoveable == 0) TimesRotated = 0;
                else TimesRotated++;
            }
            else
            {
                // Change state from 1 to 0 and back (= alternate between FinalRot and InitialRot)
                if (transform.rotation == (State == 0 ? FinalRot * RotationOffset : InitialRot * RotationOffset)) State ^= 1;

                // Set 'FinalRotation' to 'FinalRot' when moving and to 'InitialRot' when moving back
                Quaternion FinalRotation = ((State == 0) ? FinalRot * RotationOffset : InitialRot * RotationOffset);

                // Make the door/window rotate until it is fully opened/closed
                while (Mathf.Abs(Quaternion.Angle(FinalRotation, transform.rotation)) > 0.01f)
                {
                    RotationPending = true;
                    transform.rotation = Quaternion.Lerp(transform.rotation, FinalRotation, Time.deltaTime * Speed);
                    yield return new WaitForEndOfFrame();
                }

                RotationPending = false;
                if (TimesMoveable == 0) TimesRotated = 0;
                else TimesRotated++;
            }
        }



    }
}
