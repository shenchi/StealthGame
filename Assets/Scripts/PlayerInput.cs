using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class PlayerInput : MonoBehaviour
{
    private Character ch;

    private int floorId;

    // Use this for initialization
    void Start()
    {
        floorId = LayerMask.NameToLayer("Floor");

        ch = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        GameObject target = null;
        RaycastHit hitInfo;
        if (Physics.Raycast(mouseRay, out hitInfo))
        {
            target = hitInfo.collider.gameObject;
        }


        if (Input.GetMouseButtonDown(0))
        {
            if (null != target && target.layer == floorId)
            {
                ch.MoveTo(hitInfo.point);
            }
        }

        if (null != target)
        {
            if (target.layer == floorId)
            {

            }
        }
    }
}
