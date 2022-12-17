using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StatusController : MonoBehaviour
{
    [SerializeField] Transform parent;
    Animator animator;
    NavMeshAgent agent;




    [Header("Stats")]
    
    public float speed;
    
    [SerializeField] float food;

    public Faction myFaction;

    public List<CharTag> myTags  = new List<CharTag>();


    #region Setup

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        Setupp();
       transform.Translate(Vector3.forward);
       
    }
     void Setupp()
     
    {
        foreach (Transform item in parent)
        {
            item.GetComponent<Status>().anim = animator;
            item.GetComponent<Status>().statusController = this;
            item.GetComponent<Status>().agent = agent;
            item.gameObject.SetActive(false);
        }
        parent.transform.GetChild(0).gameObject.SetActive(true);
    }
    #endregion
   
   
    #region Animation Events
    void AnimHit()
    {
     // Debug.Log(gameObject.name + " vurdu" );
    }
    #endregion

}
