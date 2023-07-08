using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ActionObject", order = 1)]
public class ActionObject : ScriptableObject
{
    public enum Action
    {
        Jump,
        Move
    }

    public Action action;

    
    public int value;
}
