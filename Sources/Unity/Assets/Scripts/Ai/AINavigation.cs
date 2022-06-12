using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AINavigation : MonoBehaviour
{
    [SerializeField] private GameObject currentNode;

    private GameObject _nextNode;

    private void Start()
    {
        _nextNode = currentNode.GetComponent<AINode>().GetrChild();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.Equals(_nextNode))
        {
            SetCurrentNavigation(collider.gameObject);
        }
    }

    public void SetCurrentNavigation(GameObject currentNode)
    {
        this.currentNode = currentNode;
        _nextNode = this.currentNode.GetComponent<AINode>().GetrChild();
    }

    public GameObject GetCurrentNode()
    {
        return currentNode;
    }

    public GameObject GetNextNode()
    {
        return _nextNode;
    }
    
}
