using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

public class DialogueEditor : EditorWindow
{
    private static Dialogue selectedDialogue = null;

    
    [NonSerialized]private GUIStyle newStyle;
     [NonSerialized]private GUIStyle speakerStyle1,speakerStyle2,speakerStyle3,none;


    [NonSerialized]DialogueNode creatingNode = null;
    [NonSerialized]DialogueNode deletingNode = null;
    [NonSerialized]DialogueNode draggingNode = null;
    [NonSerialized]DialogueNode linkingParentNode = null;
    [NonSerialized]Vector2 draggingOffset;

    [NonSerialized]
    bool dragCanvas = false;
    [NonSerialized] Vector2 draggingCanvasOffset;
     EditorZoomer zoomer = new EditorZoomer();


    [MenuItem("Custom Editors/Dialogue Editor")]
    public static void OpenEditorWindow()
    {
        GetWindow(typeof(DialogueEditor),false,"Dialogue Editor");
        
    }

    [OnOpenAsset(1)]
    public static bool OnOpenAsset(int instanceID ,int line)
    {
        Dialogue currentDialogue = EditorUtility.InstanceIDToObject(instanceID) as Dialogue;
        if (currentDialogue != null)
        {
            
            OpenEditorWindow();
            return true;
        }

        return false;

    }

    private void OnEnable()
    {
        Selection.selectionChanged += OnSelectionChanged;
        newStyle = new GUIStyle();
        newStyle.normal.background = EditorGUIUtility.Load("node0") as Texture2D;
        newStyle.padding = new RectOffset(20, 20, 20, 20);
        newStyle.border = new RectOffset(12, 12, 12, 12);

        speakerStyle1 = new GUIStyle();
        speakerStyle1.normal.background = EditorGUIUtility.Load("node1") as Texture2D;
        speakerStyle1.padding = new RectOffset(20, 20, 20, 20);
        speakerStyle1.border = new RectOffset(12, 12, 12, 12);

        speakerStyle2 = new GUIStyle();
        speakerStyle2.normal.background = EditorGUIUtility.Load("node2") as Texture2D;
        speakerStyle2.padding = new RectOffset(20, 20, 20, 20);
        speakerStyle2.border = new RectOffset(12, 12, 12, 12);

       speakerStyle3 = new GUIStyle();
       speakerStyle3.normal.background = EditorGUIUtility.Load("node3") as Texture2D;
       speakerStyle3.padding = new RectOffset(20, 20, 20, 20);
       speakerStyle3.border = new RectOffset(12, 12, 12, 12);
    }

    private void OnSelectionChanged()
    {
        Dialogue newDialogue = Selection.activeObject as Dialogue;
        if (newDialogue != null)
        {
            selectedDialogue = newDialogue;
            Repaint();
        }
    }
Vector2 scrollPos;
    private void OnGUI()
    {
         
      
        
        if (selectedDialogue != null)
        {
            
       
            ProcessEvent();
            
          scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
           GUILayoutUtility.GetRect(4000,1000);
            
          //  zoomer.Begin();
            
       
        
           

            

            

            foreach (DialogueNode node in selectedDialogue.GetAllNodes())
            {
               DrawConnections(node);               
            }
            foreach (DialogueNode node in selectedDialogue.GetAllNodes())
            {
               DrawNode(node);
            }
             
           



          
        //   zoomer.End();
             EditorGUILayout.EndScrollView();
             
            
            if(creatingNode != null)
            {
                selectedDialogue.CreateNode(creatingNode);
                creatingNode = null;
            }
             if(deletingNode != null)
            {
                selectedDialogue.DeleteNode(deletingNode);
                deletingNode = null;
            }
        }
     
         

    }

    


    void ProcessEvent()
    {

      
     
        
        if(Event.current.type == EventType.MouseDown && draggingNode == null)
        {
            draggingNode = GetNodeAtPoint(Event.current.mousePosition + scrollPos);
        
            if(draggingNode != null)
            {
                draggingOffset = draggingNode.GetRect().position - Event.current.mousePosition;
                Selection.activeObject = draggingNode;
           
            }
            else
            {
                dragCanvas = true;
                draggingCanvasOffset = Event.current.mousePosition + scrollPos;
                Selection.activeObject = selectedDialogue;
            }
        }
        else if(Event.current.type == EventType.MouseDrag && draggingNode != null)
        {
            draggingNode.SetPosition(Event.current.mousePosition + draggingOffset);
            GUI.changed = true;
        }
        else if (Event.current.type == EventType.MouseDrag && dragCanvas)
        {
            scrollPos = draggingCanvasOffset - Event.current.mousePosition;
            GUI.changed = true;
        }
        else if(Event.current.type == EventType.MouseUp && draggingNode != null)
        {
            draggingNode = null;
        }
         else if(Event.current.type == EventType.MouseUp && dragCanvas)
        {
            dragCanvas = false;
        }

    }

   
    [Multiline] private string newText;
    private void DrawNode(DialogueNode node)
    {
        
        GUIStyle style = newStyle;

    //    switch (node.GetSpeaker())
    //    {
    //        case DialogueNode.Speaker.Player:
    //        style = newStyle;
    //        break; 
    //        case DialogueNode.Speaker.Speaker1:
    //        style = speakerStyle1;
    //        break; 
    //        case DialogueNode.Speaker.Speaker2:
    //        style = speakerStyle2;
    //        break; 
    //        case DialogueNode.Speaker.Speaker3:
    //        style = speakerStyle3;
    //        break; 
//
    //        default:
    //        style = none;
    //        break;
    //        
    //    }
    if(node.IsPlayerSpeaking())
    {
        style = speakerStyle1;
    }
    

        GUILayout.BeginArea(node.GetRect(),style);

             
        node.SetText(EditorGUILayout.TextField(node.GetText(),GUILayout.Height(50)));
        
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("x"))
        {
            deletingNode = node;
        }
        if(linkingParentNode == null)
        {
            if (GUILayout.Button("link"))
            {
                linkingParentNode = node;
            }
        }
        else
        {
            if(linkingParentNode != node)
            {
                if(linkingParentNode.GetChildren().Contains(node.name))
                {
                    if (GUILayout.Button("Unlink"))
                    {
                        
                        linkingParentNode.RemoveChild(node.name);
                        linkingParentNode = null;
                    }
                }
                else
                {
                    if (GUILayout.Button("child"))
                {
                    linkingParentNode.AddChild(node.name);
                    linkingParentNode = null;
                }
                }
                
            }
            else
            {
                if (GUILayout.Button("cancel"))
                {
                    linkingParentNode = null;
                }
            }
            
        }
        if(GUILayout.Button("+"))
        {
            creatingNode = node;
        }
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
    private void DrawConnections(DialogueNode node)
    {
        Vector3 startpos = new Vector2(node.GetRect().xMax,node.GetRect().center.y);
        foreach (DialogueNode childNode in selectedDialogue.GetAllChildren(node))
        {
            Vector3 endPos = new Vector2(childNode.GetRect().xMin,childNode.GetRect().center.y);
            Vector3 controlOffset = endPos - startpos;
            controlOffset.y = 0;
            controlOffset.x *= 0.5f;

            Handles.DrawBezier(startpos,endPos,
            startpos + controlOffset,endPos - controlOffset,
            Color.white,null,4f);
        }
    }
     private DialogueNode GetNodeAtPoint(Vector2 point)
    {
        DialogueNode foundNode = null;

        foreach (DialogueNode node in selectedDialogue.GetAllNodes())
        {
            if(node.GetRect().Contains(point))
            {
                foundNode = node;
            }
            
        }
        return foundNode;
    }


#region Zooming

 

#endregion
 

}
