using DG.Tweening;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class DoorController : MonoBehaviour
{
    public List<GrabItem> grabItemsList;
    public List<DropZone> dropZonesList;
    public List<Texture> doorTextures;
    public Material doorMaterial;
    public GrabItem grabbaleItem;
    public DropZone dropItem;
    public static DoorController Instance;
    public Animation GrabAnim;
    public AnimationClip grabAnim;
    public AnimationClip unGrabAnim;
    public AnimationClip handlegrabAnim;
    public AnimationClip handleunGrabAnim;
    public int sequenceNumber = 1;
    Vector3 grabbalbeItemInitialScale;
    public bool isTriggered;
    public bool isSlide;
    public bool isSwitchable=false;
    public Vector3 cameraPosition;
    int tabCount = 0;
    void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        GrabAnim.Stop();
        Events.OnGrab += GrabbableItemInHand;
        Events.OnDrop += DropZoneItem;
        ResetObjectsCollider(sequenceNumber);
    }
    private void Update()
    {
        if (isSwitchable)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Camera.main.transform.DOLocalMove(cameraPosition,1);
                doorMaterial.mainTexture = doorTextures[tabCount];
                tabCount++;
                if (tabCount > doorTextures.Count - 1)
                    tabCount = 0;
            }
        }
    }
    void GrabbableItemInHand(GrabItem grabObj)
    {
        grabbaleItem = grabObj;
        grabObj.transform.SetParent(HandMovement.Instance.transform);
        SetGrabbableItemTransform();
        dropItem = grabbaleItem.droppableObject;
        if (grabbaleItem.gameObject.tag.Equals(Tags.Handle.ToString()))
        {
            for (int i = 0; i < dropItem.grabbableItems.Count; i++)
            {
                if (grabbaleItem != dropItem.grabbableItems[i])
                {
                    dropItem.grabbableItems[i].GetComponent<BoxCollider>().enabled = false;
                }
            }
        }
       
    }
    void DropZoneItem()
    {    
        grabbaleItem.transform.SetParent(dropItem.transform);
        if (grabbaleItem.collider.CompareTag(Tags.Handle.ToString()))
        {
            GrabAnim.clip = handleunGrabAnim;
        }
        else
        {
            GrabAnim.clip = unGrabAnim;
        }
        GrabAnim.Play();
        SetDroppableItemTransform();
        dropItem.GetComponent<BoxCollider>().enabled = false;
        grabbaleItem.GetComponent<BoxCollider>().enabled = false;
        grabbaleItem.enabled = false;
        dropItem.enabled = false;
        if(grabbaleItem.SwipeObject!=null && grabbaleItem.SwipeObjectCollider != null)
        {
            grabbaleItem.SwipeObject.enabled = true;
            grabbaleItem.SwipeObjectCollider.enabled = true;
        }
        isTriggered= false;   
        sequenceNumber++;
        ResetObjectsCollider(sequenceNumber);
    }
    void SetGrabbableItemTransform()
    {

        grabbaleItem.transform.localPosition = new Vector3(HandMovement.Instance.PalmPosition.localPosition.x, HandMovement.Instance.PalmPosition.localPosition.y, HandMovement.Instance.PalmPosition.localPosition.z);

        grabbalbeItemInitialScale = grabbaleItem.transform.localScale;
        grabbaleItem.transform.localScale = new Vector3(.3f, .3f, .3f);
    }

    void SetDroppableItemTransform()
    {
        grabbaleItem.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        SnapTransform(grabbaleItem.transform, dropItem.SnapPos.transform);
    }
    void ResetObjectsCollider(int sequence)
    {
        foreach(GrabItem grabbedItem in grabItemsList)
        {
            grabbedItem.collider.enabled=false;
        }
        if((sequence-1)==0)
        {
            for (int i = 0; i < grabItemsList.Count; i++)
            {
                grabItemsList[i].GetComponent<BoxCollider>().enabled=true;
                break;
            }
        }
        else
        {
            for (int i = sequence-1; i < grabItemsList.Count; i++)
            {
                grabItemsList[i].GetComponent<BoxCollider>().enabled = true;
            }
        }
    }

    void SnapTransform  (Transform transformPos,Transform targetPos)
    {
        transformPos.DOLocalMove(targetPos.localPosition,1f);
        transformPos.DOLocalRotate(targetPos.localRotation.eulerAngles, 1f);
    }
}
public enum Tags
{
    Handle,
    Grab
}