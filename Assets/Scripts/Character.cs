using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private LookAter lookAter;
    private int speedId;

    private HashSet<Interactable> interactables = new HashSet<Interactable>();

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        lookAter = GetComponentInChildren<LookAter>();
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

    public void AddInteractable(Interactable interactable)
    {
        interactables.Add(interactable);
    }

    public void RemoveInteractable(Interactable interactable)
    {
        interactables.Remove(interactable);
    }

    public bool IsInteractable(Interactable interactable)
    {
        return interactables.Contains(interactable);
    }
    
    public bool Interact(Interactable interactable)
    {
        if (IsInteractable(interactable))
        {
            interactable.Interact(gameObject);
            return true;
        }
        return false;
    }
    
    public void LookAt(Vector3 position)
    {
        lookAter.target = position;
    }
}
