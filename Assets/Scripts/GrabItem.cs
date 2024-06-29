using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GrabItem : MonoBehaviour
{
    // Start is called before the first frame update
    bool isInsideHand;
    public BoxCollider collider;
    public DropZone droppableObject;
    public SwipeInteraction SwipeObject;
    public BoxCollider SwipeObjectCollider;
    public GameObject handleObject;

    void Update()
    {
        if(isInsideHand)
        {
            if (Input.GetKey(KeyCode.G))
            {
                Events.OnGrabable(this);
                DoorController.Instance.dropItem = droppableObject;
                if (collider.CompareTag(Tags.Handle.ToString()))
                {
                    DoorController.Instance.GrabAnim.clip = DoorController.Instance.handlegrabAnim;
                }
                else
                {
                    DoorController.Instance.GrabAnim.clip = DoorController.Instance.grabAnim;
                }
                DoorController.Instance.GrabAnim.Play();
                DoorController.Instance.isTriggered = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Tags.Grab.ToString()))
        {
            isInsideHand = true;
            DoorController.Instance.isTriggered= true;
        }
        else
        {
            DoorController.Instance.isTriggered = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        DoorController.Instance.isTriggered = false;

        if (other.CompareTag(Tags.Grab.ToString()))
        {
            isInsideHand = false;
        }
    }
}
