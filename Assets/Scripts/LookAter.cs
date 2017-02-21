using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAter : MonoBehaviour
{
    public Vector3 target { get; set; }

    Animator animator;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnAnimatorIK(int layerIndex)
    {
        animator.SetLookAtPosition(target);
        animator.SetLookAtWeight(1);
    }
}
