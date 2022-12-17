using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TPoint : MonoBehaviour
{
    
    private RaycastHit hit;
    public LayerMask layerMask;
    public float castRadius = 0.3f;
    private Ray ray;
    public bool canTransform;
    public static TPoint singleton;
    public GameObject activeObject;
    private MeshRenderer _meshRenderer;
    private void Awake()
    {
        singleton = this;
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangePos(canTransform);
        if (canTransform)
        {
            _meshRenderer.enabled = true;
        }
        else
        {
            _meshRenderer.enabled = false;
        }
    }

    public void ChangePos(bool can)
    {
        if (can)
        {
            ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        
            if (Physics.SphereCast(ray,castRadius,out hit,500f,layerMask))
            {
                transform.position = hit.point;
            }
        }
        
    }
     private void OnTriggerEnter(Collider other)
            {
                    
                    activeObject = other.gameObject;
            }
            private void OnTriggerStay(Collider other)
            {       if(activeObject== null)
                        activeObject = other.gameObject;
    
              
            }
            private void OnTriggerExit(Collider other)
            {
    
                // gameObjectt = other.gameObject;
                if (other.gameObject == activeObject)
                {
                    activeObject = null;
                }
            }
}
