using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public float alertRadius = 2.5f;
    public float sightRadius = 10.0f;
    public float sightAngle = 50.0f;
    public float scanAngle = 180.0f;
    public float spotDuration = 0.5f;
    public float alertDuration = 5.0f;
    public float patrolSpeed = 1.5f;
    public float huntingSpeed = 6.0f;

    public PatrolPath path;

    public GameObject alertSign = null;
    public Material alertMat = null;
    public Material spotMat = null;

    public Transform headTransform = null;

    Character ch;
    NavMeshAgent agent;

    Renderer alertRenderer;
    int playerLayer;
    int doorLayer;

    int pathNode = 0;

    float angleCos = 0.0f;
    float angleView = 0.0f;
    Vector3 viewDir;

    GameObject player = null;

    SphereCollider sightCollider;

    void Awake()
    {
        pathNode = 0;
        transform.position = path.Path[pathNode].location.position;
    }

    // Use this for initialization
    void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        doorLayer = LayerMask.NameToLayer("Door");

        ch = GetComponent<Character>();
        agent = GetComponent<NavMeshAgent>();

        sightCollider = GetComponent<SphereCollider>();
        sightCollider.radius = sightRadius;

        angleCos = Mathf.Cos(Mathf.Deg2Rad * sightAngle * 0.5f);
        alertRenderer = alertSign.GetComponent<Renderer>();
    }

    float waitTimer = 0.0f;
    float alertTimer = 0.0f;
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        if (null != player && (player.transform.position - pos).magnitude < 1.2f)
        {
            UIMessage.Instance.ShowMessage("Busted!");
            Time.timeScale = 0.0f;
            return;
        }

        if (alertSign.activeSelf)
        {
            alertTimer -= Time.deltaTime;

            if (alertTimer <= 0)
            {
                alertSign.SetActive(false);
                FindNearestNode();
                agent.SetDestination(path.Path[pathNode].location.position);
                agent.speed = patrolSpeed;
            }
            else if (alertTimer < alertDuration - spotDuration)
            {
                alertRenderer.sharedMaterial = alertMat;
            }
            else
            {
                agent.SetDestination(player.transform.position);
                agent.speed = huntingSpeed;
            }
            angleView = 0.0f;
        }
        else
        {
            if (waitTimer < 0.0f)
            {
                if (agent.remainingDistance <= 0.01f)
                {
                    waitTimer = path.Path[pathNode].waitTime;
                    pathNode = (pathNode + 1) % path.Path.Length;
                }
            }
            else
            {
                waitTimer -= Time.deltaTime;
                if (waitTimer < 0.0f)
                    agent.SetDestination(path.Path[pathNode].location.position);
            }

            angleView = (scanAngle - sightAngle) * 0.5f * Mathf.Sin(Time.realtimeSinceStartup);
        }
        viewDir = Quaternion.AngleAxis(/*Mathf.Deg2Rad **/ angleView, Vector3.up) * transform.forward;
        ch.LookAt(headTransform.position + viewDir);

        {
            Ray r = new Ray(transform.position, transform.forward);
            RaycastHit hitInfo;
            if (Physics.Raycast(r, out hitInfo, 1.0f, 1 << doorLayer, QueryTriggerInteraction.Ignore))
            {
                hitInfo.collider.GetComponent<Door>().Interact(gameObject);
            }
        }
    }

    void FindNearestNode()
    {
        Vector3 pos = transform.position;
        float minDist = float.MaxValue;
        for (int i = 0; i < path.Path.Length; i++)
        {
            float dist = (path.Path[i].location.position - pos).magnitude;
            if (dist < minDist)
            {
                minDist = dist;
                pathNode = i;
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {

    }

    void OnTriggerStay(Collider collider)
    {
        bool foundPlayer = false;
        if (collider.gameObject.layer == playerLayer)
        {
            Vector3 pos = collider.transform.position;
            Vector3 delta = pos - transform.position;
            Vector3 dir = delta.normalized;
            float dist = delta.magnitude;

            if (dist < alertRadius)
            {
                foundPlayer = true;
            }
            else if (Vector3.Dot(dir, viewDir) >= angleCos)
            {
                Ray r = new Ray(transform.position, dir);
                RaycastHit hitInfo;
                bool raycast = Physics.Raycast(r, out hitInfo, dist, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore);

                if (!raycast || hitInfo.collider.gameObject.layer == playerLayer)
                {
                    foundPlayer = true;
                }
            }
        }

        if (foundPlayer)
        {
            player = collider.gameObject;
            alertSign.SetActive(true);
            alertTimer = alertDuration;
            alertRenderer.sharedMaterial = spotMat;
        }
    }
    
}
