using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : Interactable
{
    enum State
    {
        Closed,
        Opened,
        Closing,
        Opening,
    }

    State state = State.Closed;

    public float duration = 0.5f;
    public float threshold = 0.02f;

    int playerLayer;
    int enemyLayer;
    float initialHeight;
    float doorHeight;

    // Use this for initialization
    void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        enemyLayer = LayerMask.NameToLayer("Enemy");
        initialHeight = transform.position.y;
        Bounds bounds = GetComponent<Renderer>().bounds;
        doorHeight = (bounds.max - bounds.min).y;
    }

    float timer = 0;
    Tweener tweener = null;

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Opened:
                timer -= Time.deltaTime;
                if (timer <= 0.0f)
                {
                    Close();
                }
                break;
            case State.Opening:
            case State.Closing:
            case State.Closed:
            default:
                break;
        }
    }

    void Open()
    {
        float targetHeight = (initialHeight - doorHeight);

        if (null != tweener)
        {
            if (state == State.Closing)
            {
                float newDur = Mathf.Abs(targetHeight - transform.position.y) / doorHeight * duration;
                tweener.Kill();
                tweener = null;
            }
            else
            {
                return;
            }
        }

        tweener = transform.DOMoveY(targetHeight, duration).OnComplete(delegate ()
        {
            tweener = null;
            var pos = transform.position;
            pos.y = targetHeight;
            transform.position = pos;
            state = State.Opened;
            timer = 3.0f;
        });
        state = State.Opening;
    }

    void Close()
    {
        float targetHeight = initialHeight;

        tweener = transform.DOMoveY(targetHeight, duration).OnComplete(delegate ()
        {
            tweener = null;
            var pos = transform.position;
            pos.y = targetHeight;
            transform.position = pos;
            state = State.Closed;
            timer = 3.0f;
        });
        state = State.Closing;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == playerLayer)
        {
            other.GetComponent<Character>().AddInteractable(this);
        }
        //print("enter " + other.name);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == playerLayer)
        {
            other.GetComponent<Character>().RemoveInteractable(this);
        }
        //print("leave " + other.name);
    }

    public override bool Interact(GameObject go)
    {
        if (state == State.Closed || state == State.Closing)
        {
            Open();
            //return true;
        }
        return true;
    }
}
