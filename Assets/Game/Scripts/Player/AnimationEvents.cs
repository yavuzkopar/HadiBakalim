using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    void InmeEvent()
    {
        TPoint.singleton.activeObject.transform.parent = PlayerInfo.singleton.transform;
        Debug.Log("Evented");
    }
}
