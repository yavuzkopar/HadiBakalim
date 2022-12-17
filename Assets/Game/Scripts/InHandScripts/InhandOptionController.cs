using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InhandOptionController : MonoBehaviour
{
    private PlayerActions _playerActions;
    
    float activeSelection =1;

    public Transform iconParent;
    public List<GameObject> iconObjects = new List<GameObject>();
    public int selection;
    public List<InHandScriptible> inHandOptions = new List<InHandScriptible>();
    public GameObject iconPrefab;
    public static InhandOptionController Singleton;
    private Animator _animator;

    private bool isOpen;
    private void Awake()
    {
        Singleton = this;
        _animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        isOpen = false;
    }

    private void OnEnable()
    {
        if (_playerActions == null)
        {
            _playerActions = new PlayerActions();
        }
        _playerActions.Enable();
    //    if (_playerActions.ActionSelection.ActionSelectorr.enabled)
    //    {
    //        _playerActions.ActionSelection.ActionSelectorr.performed += ctx => activeSelection = ctx.ReadValue<float>();
    //    }
        
        
       // _playerActions.ActionSelection.Disable();
      // _playerActions.PlayerMovement.ActionSelectorr.performed += ctx => ctx.ReadValue<int>();
    }

    
    public void ChangeList()
    {
        
        for (int i = 0; i < PlayerInfo.singleton.rightHandTransform.GetComponentInChildren<InHandOptions>().options.Length; i++)
        {
            inHandOptions.Add(PlayerInfo.singleton.rightHandTransform.GetComponentInChildren<InHandOptions>().options[i]); // outhandScriptibles
        }
        if(inHandOptions.Count <= 0) return;

        for (int i = 0; i < inHandOptions.Count; i++)
        {
            // outHandActions.Add(TPoint.singleton.activeObject.GetComponent<OutHandOptions>().options[i]); // outhandScriptibles
            GameObject obj = Instantiate(iconPrefab, iconParent);
            iconObjects.Add(obj);
            iconObjects[i].GetComponent<Image>().sprite = inHandOptions[i].icon;
            iconObjects[i].GetComponent<Image>().color = Color.gray;

        }

        activeSelection = 1;
        iconObjects[GetNumButton].GetComponent<Image>().color = Color.blue;
    }

    public void EmptyList()
    {
        

        
        if (iconParent.childCount >=1)
        {
            foreach (Transform tr in iconParent)
            {
                Destroy(tr.gameObject);
            }
            inHandOptions.Clear();
            iconObjects.Clear();
            activeSelection = 1;
            isOpen = false;
            
        }
        else
        {
            return;
        }
       
    }

    void ChangeSelection()
    {
        
        
        if (GetNumButton<iconObjects.Count)
        {
            for (int i = 0; i < iconObjects.Count; i++)
            {
                iconObjects[i].GetComponent<Image>().color = Color.gray;
            }
            iconObjects[selection].GetComponent<Image>().color = Color.blue;
            Debug.Log("ChangeSelection");
        }
        else
        {
            return;
        }
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
        Debug.Log(activeSelection);
        if (inHandOptions.Count <= 0)
        {
            return;
        }
        
        if (_playerActions.ActionSelection.ActionSelectorr.triggered)
        {
            
            ChangeSelection(); 
        }
        else if (_playerActions.PlayerMovement.SolClick.triggered)
        {
            // aksiyonu gerceklestir
            if (inHandOptions.Count >=1)
            {
                //  _animator.SetTrigger(inHandOptions[selection].triggerAnim);
                inHandOptions[selection].eventToTrigger?.Invoke();
            }
            
           // EmptyList();// eldeki obje degistiginde
           // ChangeList();// eldeki obje degistiginde
        }
    }
    
    int GetNumButton
    {
        get
        {
            if (_playerActions.ActionSelection.ActionSelectorr.triggered)
            {
                Debug.Log("Anlamlı " + activeSelection);
                activeSelection = _playerActions.ActionSelection.ActionSelectorr.ReadValue<float>();
            }
              selection = (int) activeSelection - 1;
           

            return selection;
        }
        set
        {
            selection = value;
        }
        
    }
    
}
