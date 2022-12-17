using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//[CreateAssetMenu(fileName = "New Option",menuName = "Out Hand Option")]
public class OutHandScriptible : MonoBehaviour
{
    public Sprite icon;
    public string triggerAnim = "";
    public float distanceToUse = 2f;
    public UnityEvent eventToTrigger;
}
