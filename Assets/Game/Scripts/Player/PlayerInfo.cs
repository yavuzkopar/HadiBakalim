using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo singleton;
    public Transform rightHandTransform;

    private void Awake()
    {
        singleton = this;
    }
}
