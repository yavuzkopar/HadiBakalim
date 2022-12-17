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
        Collider[] colls = eldeki.GetComponents<Collider>();
        foreach (Collider item in colls)
        {
            item.isTrigger = true;
        }
    //    eldeki.GetComponent<Collider>().isTrigger = true;
        InhandOptionController.Singleton.ChangeList();
        TPoint.singleton.canTransform = true;
        
        Debug.Log("Evented");
    }

    void DropItem()
    {
        eldeki.transform.position = TPoint.singleton.transform.position;
        eldeki.transform.rotation = Quaternion.identity;
      //  eldeki.GetComponent<Collider>().isTrigger = false;
      Collider[] colls = eldeki.GetComponents<Collider>();
        foreach (Collider item in colls)
        {
            item.isTrigger = false;
        }
        if (eldeki.GetComponent<Rigidbody>() != null)
        {
            eldeki.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
        
        eldeki.parent = null;
        eldeki = null;
        InhandOptionController.Singleton.EmptyList();
    }
    void HitEvent()
    {
       // TPoint.singleton.canTransform = true;
       Debug.Log("Hittt");
    //   Rigidbody rb = TPoint.singleton.activeObject.GetComponent<Rigidbody>();
    //   Vector3 direction = (TPoint.singleton.activeObject.transform.position - transform.position).normalized;
    //    rb.AddForce(direction * 250);
       Invoke("LetTP",0.5f);
    }
    void PushEvent()
    {
       Rigidbody rb = TPoint.singleton.activeObject.GetComponent<Rigidbody>();
       Vector3 direction = (TPoint.singleton.activeObject.transform.position - transform.position).normalized;
        rb.AddForce(direction * 250);
       Invoke("LetTP",0.5f);
    }

    void LetTP()
    {
        TPoint.singleton.canTransform = true;
    }
}
