using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovement : MonoBehaviour
{
    float speed = 45;
    private Vector3 mOffset;
    private float mZCoord;
    public Transform PalmPosition;
    Vector3 mousePoint;
    Vector3 mousePointDown;
    public float speedRotation=100;

  public  Vector3 HandInitialPos;

    public static HandMovement Instance;
    private void Awake()
    {
        Instance = this;
    }

    void OnMouseDown()
    {
        mousePointDown= mousePoint;
        mZCoord = Camera.main.WorldToScreenPoint(
            gameObject.transform.position).z;
        // Store offset = gameobject world pos - mouse world pos
        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();

        HandInitialPos = this.transform.position;

    }
    private Vector3 GetMouseAsWorldPoint()
    {
        // Pixel coordinates of mouse (x,y)
        mousePoint = Input.mousePosition;
        // z coordinate of game object on screen
        mousePoint.z = mZCoord;
        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        if (DoorController.Instance.isSlide)
        {
            if (DoorController.Instance.grabbaleItem.handleObject != null)
            {           
                transform.position = GetMouseAsWorldPoint() + mOffset;               

                Vector3 posChange = this.transform.position - HandInitialPos;
                    float rotSpeed = Mathf.Abs(posChange.y);
                if(posChange.y>0)
                {
                    DoorController.Instance.grabbaleItem.handleObject.transform.Rotate(new Vector3(0, 0, rotSpeed* speedRotation * Time.deltaTime), Space.World);
                }
                else 
                {
                    DoorController.Instance.grabbaleItem.handleObject.transform.Rotate(new Vector3(0, 0, rotSpeed * -speedRotation * Time.deltaTime), Space.World);
                }
                Vector3 rotClamp = DoorController.Instance.grabbaleItem.handleObject.transform.localRotation.eulerAngles;

                float angle = rotClamp.z;

                angle = angle % 360;

                // force it to be the positive remainder, so that 0 <= angle < 360  
                angle = (angle + 360) % 360;

                // force into the minimum absolute value residue class, so that -180 < angle <= 180  
                if (angle > 180)
                    angle -= 360;

                rotClamp.z = Mathf.Clamp(angle, 0,45);

                DoorController.Instance.grabbaleItem.handleObject.transform.localRotation = Quaternion.Euler(rotClamp);
                // transform.position = HandInitialPos;
                transform.position = DoorController.Instance.grabbaleItem.SwipeObject.HandTransorm.transform.position;
                transform.rotation = DoorController.Instance.grabbaleItem.SwipeObject.HandTransorm.transform.rotation;
                HandInitialPos =transform.position;
                DoorController.Instance.isSwitchable =true;
            }
        }
        else
        {       
            transform.position = GetMouseAsWorldPoint() + mOffset;
            if (Input.GetKey(KeyCode.DownArrow))
            {
                Camera.main.transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Camera.main.transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
            }
        }
    }
    private void OnMouseUp()
    {
        if (DoorController.Instance.isSlide)
        {
            DoorController.Instance.grabbaleItem.handleObject.transform.localRotation = Quaternion.identity;
            transform.DOLocalRotate(new Vector3(0, -90, 0), .5f);
            //   this.transform.localPosition = mousePointDown;
        }
    }
}
