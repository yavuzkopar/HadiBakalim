using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogueCondition : MonoBehaviour
{

    public static DialogueCondition singleton;
    public List<string> parcacik = new List<string>();


    private void Awake() {
        singleton = this;
       

    }


    public void AddParca(string parca)
    {
        if(!parcacik.Contains(parca))
            parcacik.Add(parca);
    }
    public void RemoveParca(string parca)
    {
        if (parcacik.Contains(parca))
        {
             parcacik.Remove(parca);
        }
       
    }
    
}
