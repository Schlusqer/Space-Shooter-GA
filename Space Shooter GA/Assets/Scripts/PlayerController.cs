using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

    // allows me to reference to components in unity
    private Rigidbody rb;
    private AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Basic variables to edit inside unity
    public float speed;
    public float tilt;
    public Boundary boundary;

    // Variables for the player shot
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    private float nextFire;

    // Update function that allows us to fire shots with spacebar
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > nextFire)
        {
            // adjusts the firerate so it isn't just one constant beam if you hold down the fire button
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            
            // plays the projectile sound on fire
            audioSource.Play();
        }
    }

    // Let's us move the player
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;

        // prevents us from going out of bounds
        rb.position = new Vector3(
            Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax), 
            0.0f, 
            Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
        );

        // slightly tilts the ship for realistic movement
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}
