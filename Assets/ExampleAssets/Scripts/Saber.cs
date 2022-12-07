using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Saber controls, sobald man richtig sliced verschwindet Block

public class Saber : MonoBehaviour {
    
    public LayerMask layer;
    private Vector3 previousPos;

    void Start () {

    } 

    void Update () {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit,1,layer))
        {
            if(Vector3.Angle(transform.position-previousPos, hit.transform.up)>130)
            {
                Destroy(hit.transform.gameObject);
            }
        }
        previousPos = transform.position;
    }
}