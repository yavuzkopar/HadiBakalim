using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Dialogue" , menuName = "Create Dialogue")]
public class Dialogue : ScriptableObject, ISerializationCallbackReceiver
{
    public List<DialogueNode> nodes = new List<DialogueNode>();

    Dictionary<string, DialogueNode> nodeLookUp = new Dictionary<string,DialogueNode>();


    private void Awake()
    {
        
        OnValidate();
    }

    

    void OnValidate()
    {
        nodeLookUp.Clear();
        foreach (DialogueNode node in GetAllNodes())
        {
            nodeLookUp[node.name] = node;
        }
    }
    public IEnumerable<DialogueNode> GetAllNodes()
    {
        return nodes;
    }
    public DialogueNode GetRootNode()
    {
        return nodes[0];
    }
    public IEnumerable<DialogueNode> GetAllChildren(DialogueNode parent)
    {
        List<DialogueNode> result = new List<DialogueNode>();
        foreach (string childID in parent.GetChildren())
        {
            if (nodeLookUp.ContainsKey(childID))
            {
                result.Add(nodeLookUp[childID]);

            }
        }
        return result;
    }
    public IEnumerable<DialogueNode> GetPlayerChildren(DialogueNode parent)
    {
        
        foreach (DialogueNode item in GetAllChildren(parent))
        {
            if (item.IsPlayerSpeaking())
            {
                yield return item;
            }
        }
        
    }
    public IEnumerable<DialogueNode> GetAIChildren(DialogueNode parent)
    {
        
        foreach (DialogueNode item in GetAllChildren(parent))
        {
            if (!item.IsPlayerSpeaking())
            {
                yield return item;
            }
        }
        
    }
#if UNITY_EDITOR
    public void CreateNode(DialogueNode parent)
    {
        DialogueNode newNode = MakeNode(parent);
        Undo.RegisterCreatedObjectUndo(newNode, "Created New Node");
        Undo.RecordObject(this, "Node Added");

        AddNode(newNode);
    }

    private void AddNode(DialogueNode newNode)
    {
        nodes.Add(newNode);

        OnValidate();
    }

    private DialogueNode MakeNode(DialogueNode parent)
    {
        DialogueNode newNode = CreateInstance<DialogueNode>();
        newNode.name = Guid.NewGuid().ToString();

        if (parent != null)
        {
            parent.AddChild(newNode.name);
            newNode.SetPlayerSpeaking(!parent.IsPlayerSpeaking());
            newNode.SetPosition(parent.GetRect().position + new Vector2(210,0));
        }

        return newNode;
    }

    public void DeleteNode(DialogueNode deletingNode)
    {
        Undo.RecordObject(this,"Node deleted");

        nodes.Remove(deletingNode);
        
        OnValidate();
        foreach (DialogueNode item in GetAllNodes())
        {
            item.RemoveChild(deletingNode.name);
        }
        Undo.DestroyObjectImmediate(deletingNode);
    }
#endif
    public void OnBeforeSerialize()
    {
#if UNITY_EDITOR

        if (nodes.Count == 0)
        {
            DialogueNode newNode = MakeNode(null);
            AddNode(newNode);
        }
        if(AssetDatabase.GetAssetPath(this) != "")
        {
            foreach (DialogueNode item in GetAllNodes())
            {
                if(AssetDatabase.GetAssetPath(item) == "")
                {
                    AssetDatabase.AddObjectToAsset(item,this);
                }
            }
        }
#endif
    }

    public void OnAfterDeserialize()
    {
        
    }
}
