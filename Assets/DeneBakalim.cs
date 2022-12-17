using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeneBakalim : MonoBehaviour
{
   [SerializeField] UnityEvent eventTrigger;
   public UnityAction ac;
   
   
    private void OnDisable() {
       eventTrigger?.Invoke();
    }
    
}
