using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwordTakeEvent : MonoBehaviour
{
    public UnityEvent @event;

    private void OnTransformParentChanged()
    {
        if(transform.parent == PlayerInfo.singleton.rightHandTransform)
            @event?.Invoke();
    }
}
