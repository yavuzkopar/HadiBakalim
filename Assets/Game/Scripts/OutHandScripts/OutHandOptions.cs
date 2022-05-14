using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutHandOptions : MonoBehaviour
{
    public List<OutHandScriptible> options = new List<OutHandScriptible>();

    public void AddOptionToList(OutHandScriptible option)
    {
        options.Add(option);
    }
}
