using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Status : MonoBehaviour
{
 // [HideInInspector] public Transform me;
  [HideInInspector] public Animator anim;
  [HideInInspector] public NavMeshAgent agent;
  [HideInInspector] public StatusController statusController;

  
    //protected float abbas = 13f;
  [SerializeField] string animTrigger = "";

  protected virtual void OnEnable()
  {
    anim.SetTrigger(animTrigger);
  }
    protected void GecisYap(GameObject obj)
    {
        
        obj.SetActive(true);
        gameObject.SetActive(false);
    }

  protected virtual void OnDisable()
  {
    Debug.Log("wadsalşikfaşj");
    anim.ResetTrigger(animTrigger);
  }
    
}
