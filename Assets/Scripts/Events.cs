using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    public static event Action<GrabItem> OnGrab = delegate { };
    public static event Action OnDrop = delegate { };

    public static void OnGrabable(GrabItem grabObj)
    {
        OnGrab(grabObj);
    }
    public static void OnDroppable()
    {
        OnDrop();
    }
}
