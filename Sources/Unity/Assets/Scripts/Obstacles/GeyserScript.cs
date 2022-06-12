using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserScript : MonoBehaviour
{

    [SerializeField] [Range(0,1)] private float startingFrame = 0f;

    private Animator _animator;

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _animator.PlayInFixedTime("Spray",0,startingFrame);
    }
    
}
