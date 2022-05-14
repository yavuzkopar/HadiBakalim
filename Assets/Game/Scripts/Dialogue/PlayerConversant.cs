using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerConversant : MonoBehaviour
{
    [SerializeField] Dialogue testDialogue;
  [SerializeField]  Dialogue currentDialogue;
    DialogueNode currentNode;
    [SerializeField] GameObject DialoguePanel;
    public event Action onConversationUpdated;

    [SerializeField] GameObject eventHolder;

    public void StartDialogue(Dialogue newDialogue)
    {
        
        currentDialogue = newDialogue;
        currentNode = currentDialogue.GetRootNode();
        onConversationUpdated();
        
    }
    public void Quit()
    {
        currentDialogue = null;
        currentNode = null;
        GetComponent<Animator>().SetTrigger("idle");
        onConversationUpdated();
    }
    public bool IsActive()
    {
        return currentDialogue != null;
    }
    public string GetText()
    {
        if(currentNode == null)
        {
            return "";
        }
        return currentNode.GetText();
    }
    public IEnumerable<DialogueNode> GetChoises()
    {
        if(currentDialogue != null)
            return currentDialogue.GetPlayerChildren(currentNode);
        else return null;
    }
    public void Next()
    {
        int playerResponses = currentDialogue.GetPlayerChildren(currentNode).ToArray().Count();
        if (playerResponses >0)
        {
            onConversationUpdated();
            return;
        }
        DialogueNode[] children = currentDialogue.GetAllChildren(currentNode).ToArray();
        int randomIndex= UnityEngine.Random.Range(0,children.Count());
        currentNode = children[randomIndex]; 
        onConversationUpdated();
    }
    public bool HasNext()
    {
        
        return currentDialogue.GetAllChildren(currentNode).Count() > 0;
    }

    public void SelectChoise(DialogueNode chosen)
    {
        currentNode = chosen;
        TriggerAction();

    }
    void TriggerAction()
    {
        if (currentNode != null)
        {
            Triggerr(currentNode.GetAction());
        }
    }
    void Triggerr(string action)
    {
        if(action == "") return;

        foreach (StoryEventTrigger item in eventHolder.GetComponentsInChildren<StoryEventTrigger>())
        {
            item.Trigger(action);
        }
    }
}
