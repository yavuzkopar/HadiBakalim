using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class DialogueNode : ScriptableObject
{
    public enum Speaker
    {
        Player,
        Speaker1,
        Speaker2,
        Speaker3,
    }
    [SerializeField] Speaker whoisSpeaking;
    [SerializeField] bool isPlayerSpeaking;
    [TextArea(3,4)]  [SerializeField] string text;
    [SerializeField] List<string> children = new List<string>();
    [SerializeField] Rect rect = new Rect(0,0,200,100);

    [SerializeField] string actionToTrigger;
    [SerializeField] string[] kondisyonlar;
  
    

    public string GetAction()
    {
        return actionToTrigger;
    }

    public Rect GetRect()
    {
        return rect;
    }
    public string GetText()
    {
        return text;
    }
    public List<string> GetChildren()
    {
        return children;
    }
    public Speaker GetSpeaker()
    {
        return whoisSpeaking;
    }
    public bool IsPlayerSpeaking()
    {
        return isPlayerSpeaking;
    }
    public bool CheckCondition()
    {
        foreach (string item in kondisyonlar)
        {
            if(!DialogueCondition.singleton.parcacik.Contains(item))
            {
                return false;
            }
            
        }
        return true;
            
        
        
    }
#if UNITY_EDITOR
    public void SetPosition(Vector2 value)
    {
        Undo.RecordObject(this,"Node Moved");

        rect.position = value;
        EditorUtility.SetDirty(this);
    }
    public void SetText(string value)
    {
        if (value != text)
        {
            Undo.RecordObject(this,"Nodes Changed");

            text = value;
            EditorUtility.SetDirty(this);

        }
    }
    public void AddChild(string childID)
    {
        Undo.RecordObject(this,"Add Link");

        children.Add(childID);
        EditorUtility.SetDirty(this);

    }
    public void RemoveChild(string childID)
    {
        Undo.RecordObject(this,"UnLinked");
        children.Remove(childID);
        EditorUtility.SetDirty(this);

    }

    public void SetPlayerSpeaking(bool v)
    {
        Undo.RecordObject(this,"Change speaker");
       isPlayerSpeaking = v;
       EditorUtility.SetDirty(this);
    }

    
#endif
}
