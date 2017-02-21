using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    [System.Serializable]
    public class Node
    {
        public Transform location;
        public float waitTime;
    }
    
    public Node[] Path;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
