using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual bool Interact(GameObject go)
    {
        return false;
    }
}
