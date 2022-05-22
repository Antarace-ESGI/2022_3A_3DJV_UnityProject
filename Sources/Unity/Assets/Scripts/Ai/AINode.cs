using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class AINode : MonoBehaviour
{

    [Header("Node")] 
    [SerializeField] [CanBeNull] private GameObject rChild;
    [SerializeField] [CanBeNull] private GameObject lChild;
    
    public GameObject GetrChild()
    {
        if(rChild)
            return rChild;
        return null;
    }
    
    public GameObject GetlChild()
    {
        if(lChild)
            return lChild;
        return null;
    }
    
}
