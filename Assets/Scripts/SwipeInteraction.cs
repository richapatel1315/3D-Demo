using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SwipeInteraction : MonoBehaviour
{
    public Transform slideObject;

    public float minValue=0,maxValue=5;
    public static SwipeInteraction Instance;
    public Transform HandTransorm;
    private void Awake()
    {
        Instance = this;
    }
    private void OnTriggerEnter(Collider other)
    {
    }
    private void OnTriggerStay(Collider other)
    {        
        if( Input.GetKeyDown(KeyCode.M))
        {

            DoorController.Instance.isSlide = true; 
            HandMovement.Instance.transform.position=HandTransorm.transform.position;
            DoorController.Instance.GrabAnim.clip = DoorController.Instance.handlegrabAnim;
            DoorController.Instance.GrabAnim.Play();

        }
     

        }
    private void OnTriggerExit(Collider other)
    {

        DoorController.Instance.isSlide = false;
        DoorController.Instance.grabbaleItem.handleObject.transform.localRotation = Quaternion.identity;
        DoorController.Instance.GrabAnim.clip = DoorController.Instance.handleunGrabAnim;
        DoorController.Instance.GrabAnim.Play();
        HandMovement.Instance.transform.DOLocalRotate(new Vector3(0, -90, 0),.5f);

    }
}
