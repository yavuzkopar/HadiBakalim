using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerDeneme : MonoBehaviour
{
   [SerializeField]public List<Transform> targetList = new List<Transform>();
    [SerializeField]public LayerMask layerMask;
    
   

    private void OnTriggerEnter(Collider other){
        if ((layerMask.value & (1 << other.gameObject.layer)) > 0)
        {
            
            targetList.Add(other.transform);
            Debug.Log("vuhuuu");
        }
    
    }
    
    void OnTriggerExit(Collider other)
    {
        targetList.Remove(other.transform);
    }
}
