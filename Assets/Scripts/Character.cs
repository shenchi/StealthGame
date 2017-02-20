using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    public float speed = 0;

    //[SerializeField]
    private Animator animator;
    private int speedId;

    // Use this for initialization
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        speedId = Animator.StringToHash("Speed");
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat(speedId, speed);
    }
}
