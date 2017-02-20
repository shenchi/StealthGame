using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private int speedId;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        speedId = Animator.StringToHash("Speed");
    }
    
    // Update is called once per frame
    void Update()
    {
        float speed = (agent.remainingDistance > 0.0f) ? agent.speed : 0.0f;
        animator.SetFloat(speedId, speed);
    }

    public void MoveTo(Vector3 pos)
    {
        agent.SetDestination(pos);
    }
}
