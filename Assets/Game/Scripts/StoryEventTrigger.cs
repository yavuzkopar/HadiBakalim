using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StoryEventTrigger : MonoBehaviour
{

    [SerializeField] string action;
    [SerializeField] UnityEvent onTrigger;



    public void Trigger(string actionToTrigger)
    {
        if (actionToTrigger == action)
        {
            onTrigger?.Invoke();
        }
    }
    
   


}
