using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObsicleController : MonoBehaviour
{
    [SerializeField] List<GameObject> obsticles;
    Dictionary<GameObject,int> obs = new Dictionary<GameObject, int>();
    private void OnTriggerEnter(Collider other) {
        obsticles.Add(other.gameObject);
        obs.Add(other.gameObject,other.gameObject.layer);
        other.GetComponent<MeshRenderer>().material.color = new Color(.5f,.5f,.5f,0f);
        other.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }
    private void OnTriggerExit(Collider other) {
        other.GetComponent<MeshRenderer>().material.color = new Color(.5f,.5f,.5f,1f);
        if (obs.ContainsKey(other.gameObject))
        {
            other.gameObject.layer = obs[other.gameObject];
            obs.Remove(other.gameObject);
        }
        obsticles.Remove(other.gameObject);
    }
}
