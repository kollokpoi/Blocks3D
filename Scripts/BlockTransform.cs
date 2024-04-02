using Assets.Scripts.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTransform : MonoBehaviour
{
    [HideInInspector]
    public Vector VectorBlock;

    public Animation animation;
    public Transform target;
    public float Speed = 3;
    public bool EndPosition;

    RaycastHit hit;
    Vector3 direct = Vector3.zero;

    void Start()
    {
        if (VectorBlock == Vector.Top)
            direct = transform.TransformDirection(Vector3.back);
        else if (VectorBlock == Vector.Bottom)
            direct = transform.TransformDirection(Vector3.forward);
        else if (VectorBlock == Vector.Left)
            direct = transform.TransformDirection(Vector3.right);
        else if (VectorBlock == Vector.Right)
            direct = transform.TransformDirection(Vector3.left);
    }

    void Update()
    {
        if (target && !EndPosition) {
            transform.position = Vector3.Lerp(
                    transform.position,
                    target.position,
                    Speed * Time.deltaTime);
            double Distance = Math.Round(Vector3.Distance(transform.position, target.position), 2);
            if (Distance == 0.00f)
                EndPosition = true;

            //Debug.DrawRay(transform.position + Vector3.up * 0.8f, direct, Color.red, 1);
            if (Physics.Raycast(transform.position + Vector3.up * 0.8f,
                   direct,
                   out hit,
                   0.4f,
                   1,
                   QueryTriggerInteraction.Ignore))
            {
                
            }
        }
    }
}
