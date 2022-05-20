using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AINode : MonoBehaviour
{

    [Header("Node")] 
    [SerializeField] private GameObject rChild;
    [SerializeField] private GameObject lChild;
    
    public GameObject? GetrChild()
    {
        if(rChild)
            return rChild;
        return null;
    }

    public GameObject? GetlChild()
    {
        if(lChild)
            return lChild;
        return null;
    }
    
}
