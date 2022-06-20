using System;
using JetBrains.Annotations;
using UnityEngine;

public class AINode : MonoBehaviour
{
    private static GameObject[] _nodes;
    private bool _isInitialized;
    [Range(0, 1)] public float targetSpeed = 1; 

    [Header("Node")] [SerializeField] [CanBeNull]
    private GameObject rightNode;

    [SerializeField] [CanBeNull] private GameObject leftNode;

    private void Start()
    {
        if (!_isInitialized)
        {
            _nodes = GameObject.FindGameObjectsWithTag("Node");
            _isInitialized = true;
        }

        var index = Array.FindIndex(_nodes, o => o == gameObject);
        if (_nodes.Length-1 > index)
        {
            rightNode = _nodes[index + 1];
        }
    }

    public GameObject GetRightChild()
    {
        if (rightNode)
        {
            return rightNode;
        }

        return null;
    }

    public GameObject GetLeftChild()
    {
        if (leftNode)
        {
            return leftNode;
        }

        return null;
    }
}