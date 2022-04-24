using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RingMenuController : MonoBehaviour
{
    public static RingMenuController Singleton;
    private PlayerActions _playerActions;




    public List<OutHandScriptible> outHandActions;
    public List<GameObject> iconObjects = new List<GameObject>();
    public GameObject iconPrefab;
    private Vector2 normalizedMousePos;
    public float currentAngle;
    public int selection;
    private int previousSelection;

    private Animator _animator;
    public bool isSelectionActive;
    private void Awake()
    {
        Singleton = this;
        _animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    public GameObject ringMenuParent;

    public void OpenRingMenu()
    {
        ringMenuParent.SetActive(true);
        ringMenuParent.transform.position = 
            new Vector2(Mathf.Clamp(Mouse.current.position.x.ReadValue(),300,1620),
            Mathf.Clamp(Mouse.current.position.y.ReadValue(),300,780));
        
        TryToGetActions();
        
    }
    void CanTurn()
    {
        TPoint.singleton.canTransform = true;
    }

    void CloseRingMenu()
    {
        ringMenuParent.SetActive(false);

        
       // TPoint.singleton.canTransform = true;
       if (outHandActions.Count >= 1 )
       {
           if (isSelectionActive)
           {
             //  Invoke("CanTurn", outHandActions[selection].animationDelay);
             //  outHandActions[selection].action.Doit();
             _animator.SetTrigger(outHandActions[selection].triggerAnim);
           }
           else
                TPoint.singleton.canTransform = true;
           outHandActions.Clear();

           Debug.Log("bıraktı");

           iconObjects.Clear();
           for (int i = 0; i < ringMenuParent.transform.childCount; i++)
           {
               Destroy(ringMenuParent.transform.GetChild(i).gameObject);
           }
       }
       else
       {
           TPoint.singleton.canTransform = true;
           
       }

       previousSelection = 0;
    }

    void TryToGetActions()
    {
        if (TPoint.singleton.activeObject.GetComponent<OutHandOptions>() == null)
        {
            return;
        }
        else
        {
            for (int i = 0; i < TPoint.singleton.activeObject.GetComponent<OutHandOptions>().options.Length; i++)
            {
                outHandActions.Add(TPoint.singleton.activeObject.GetComponent<OutHandOptions>().options[i]); // outhandScriptibles
            }

            for (int i = 0; i < outHandActions.Count; i++)
            {
               // outHandActions.Add(TPoint.singleton.activeObject.GetComponent<OutHandOptions>().options[i]); // outhandScriptibles
                GameObject obj = Instantiate(iconPrefab,ringMenuParent.transform);
                iconObjects.Add(obj);
                iconObjects[i].GetComponent<Image>().sprite = outHandActions[i].icon;
                obj.transform.RotateAround(ringMenuParent.transform.position,Vector3.forward,360 / (outHandActions.Count * 2) );// = Quaternion.Euler(0, 0, 360 / (outHandActions.Count * 2));
                obj.transform.RotateAround(ringMenuParent.transform.position,Vector3.forward, i * (360 / outHandActions.Count));
                obj.transform.localEulerAngles = Vector3.zero;
                
                
            }
            
           
            
        }
    }

    void Start()
    {
        
    }

    private void OnEnable()
    {
        if (_playerActions == null)
        {
            _playerActions = new PlayerActions();
        }
        _playerActions.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerActions.PlayerMovement.RingAcma.triggered)
        {
            OpenRingMenu();
            TPoint.singleton.canTransform = false;
            // denemelik.GetComponent<OutHandOptions>().options[0].action.Doit();
        }

        if (_playerActions.PlayerMovement.RingAcma.WasReleasedThisFrame())
        {            
            CloseRingMenu();
            
        }
        if(iconObjects.Count == 0) return;
        
        normalizedMousePos = new Vector2(Mouse.current.position.x.ReadValue() - ringMenuParent.transform.position.x,
            Mouse.current.position.y.ReadValue() - ringMenuParent.transform.position.y);
        currentAngle = Mathf.Atan2(normalizedMousePos.y, normalizedMousePos.x) * Mathf.Rad2Deg;
        currentAngle = (currentAngle + 360) % 360;
        selection = (int)currentAngle / (360 / iconObjects.Count);

        Vector2 mousePos = new Vector2(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue());
       // if (selection != previousSelection)
       // {
            selection = Mathf.Clamp(selection, 0, ringMenuParent.transform.childCount - 1);
            iconObjects[previousSelection].GetComponent<Image>().color = Color.red;


            if (Vector2.Distance(mousePos,iconObjects[selection].transform.position)<150f)
            {
                isSelectionActive = true;
                previousSelection = selection;

                iconObjects[selection].GetComponent<Image>().color = Color.green;
            }
            else
            {
               
                isSelectionActive = false;
            }
                
            //  }
    }
}
