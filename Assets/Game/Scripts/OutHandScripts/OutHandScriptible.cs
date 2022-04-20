using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Option",menuName = "Out Hand Option")]
public class OutHandScriptible : ScriptableObject
{
    public Sprite icon;
    public OutHandAction action;
    public float animationDelay;
}
