using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    private Transform eldeki = null;
    void TakeToHandEvent()
    {
        eldeki = TPoint.singleton.activeObject.transform;
        eldeki.TakeToHand(PlayerInfo.singleton.rightHandTransform);
        eldeki.GetComponent<Collider>().isTrigger = true;
        InhandOptionController.Singleton.ChangeList();
        TPoint.singleton.canTransform = true;
        
        Debug.Log("Evented");
    }

    void DropItem()
    {
        eldeki.parent = null;
        eldeki = null;
        InhandOptionController.Singleton.EmptyList();
    }
    void HitEvent()
    {
       // TPoint.singleton.canTransform = true;
       LetTP();
    }

    void LetTP()
    {
        TPoint.singleton.canTransform = true;
    }
}
