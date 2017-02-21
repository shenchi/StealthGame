using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    int playerLayer;

    // Use this for initialization
    void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == playerLayer)
        {
            UIMessage.Instance.ShowMessage("You win!");
            Time.timeScale = 0.0f;
        }
        //print("enter " + other.name);
    }
}
