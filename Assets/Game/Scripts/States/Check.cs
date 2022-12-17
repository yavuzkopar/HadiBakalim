using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Check : MonoBehaviour
{
    public List<Transform> npcler = new List<Transform>();
    [SerializeField] LayerMask npcLayer;
    public List<Transform> weaponss = new List<Transform>();
    [SerializeField] LayerMask weaponLayer;
    public List<Transform> furnituress = new List<Transform>();
    [SerializeField] LayerMask furnitureLayer;
    public List<Transform> foodss = new List<Transform>();
    [SerializeField] LayerMask foodLayer;


   void ListeyeEkle(GameObject go)
   {
       if((npcLayer.value & (1 << go.layer)) > 0)
        {
            npcler.Add(go.transform);
            return;
        }
        else if ((weaponLayer.value & (1 << go.layer)) > 0)
        {
            weaponss.Add(go.transform);
            return;
        }
        else if ((furnitureLayer.value & (1 << go.layer)) > 0)
        {
            furnituress.Add(go.transform);
            return;
        }
        else if ((foodLayer.value & (1 << go.layer)) > 0)
        {
            foodss.Add(go.transform);
            return;
        }
    
   }


    private void OnTriggerEnter(Collider other){
        
    ListeyeEkle(other.gameObject);
    }
    void ListedenCikart(Transform other)
    {
        if (weaponss.Contains(other.transform))
        {
            weaponss.Remove(other.transform);
            return;
        }
        else if (npcler.Contains(other.transform))
        {
            npcler.Remove(other.transform);
            return;
        }
         else if (furnituress.Contains(other.transform))
        {
            furnituress.Remove(other.transform);
            return;
        }
         else if (foodss.Contains(other.transform))
        {
            foodss.Remove(other.transform);
            return;
        }
    }
    void OnTriggerExit(Collider other)
    {
        ListedenCikart(other.transform);
    }
}
