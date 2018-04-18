using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {
    private Rigidbody rb;
    public float speedMin;
    public float speedMax;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // The enemies and hazards move down the screen with a random speed value that I've set in Unity
        rb.velocity = transform.forward * Random.Range(speedMin, speedMax);
    }
}
