using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZone : MonoBehaviour
{
    public bool IsLocked;
    public List<GrabItem> grabbableItems;
    public Transform SnapPos;
    private void OnTriggerEnter(Collider other)
    {
        if (grabbableItems.Exists(x=>x.gameObject== other.gameObject))
        {
            Events.OnDroppable();         
        }        
        if (other.CompareTag(Tags.Grab.ToString()))
        {
            DoorController.Instance.isTriggered = true;
        }
        else
        {
            DoorController.Instance.isTriggered = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        DoorController.Instance.isTriggered = false;
    }
}