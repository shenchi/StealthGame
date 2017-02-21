using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class PlayerInput : MonoBehaviour
{
    private Character ch;

    private int floorId;
    private int doorId;

    // Use this for initialization
    void Start()
    {
        floorId = LayerMask.NameToLayer("Floor");
        doorId = LayerMask.NameToLayer("Door");

        ch = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        GameObject target = null;
        RaycastHit hitInfo;
        if (Physics.Raycast(mouseRay, out hitInfo, float.MaxValue, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
        {
            target = hitInfo.collider.gameObject;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            if (null != target)
            {
                if (target.layer == floorId)
                    ch.MoveTo(hitInfo.point);
                else if (target.layer == doorId)
                {
                    var door = target.GetComponent<Door>();
                    if (!ch.Interact(door))
                    {
                        RaycastHit floorHitInfo;
                        if (Physics.Raycast(mouseRay, out floorHitInfo, float.MaxValue, 1 << floorId))
                        {
                            ch.MoveTo(floorHitInfo.point);
                        }
                    }
                }
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
