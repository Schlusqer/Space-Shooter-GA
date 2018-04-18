using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour {

    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;
    public float dodge;
    public float smoothing;
    public float tilt;
    public Boundary boundary;

    private float currentSpeed;
    private float targetManeuver;
    private Rigidbody rb;
	
	void Start () {

        rb = GetComponent<Rigidbody>();
        StartCoroutine(Evade());
        currentSpeed = rb.velocity.z;
        
    }
	
	IEnumerator Evade ()
    {
        yield return new WaitForSeconds(Random.Range (startWait.x, startWait.y));

        while (true)
        {
            /* -Mathf.Sign reverses negative and positive. Since we use this on 'transform.position.x'
             * it makes the enemy always dodge towards the middle of the screen.
             * This is to make sure that the enemy doesn't try to dodge into a wall */
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign (transform.position.x);
            yield return new WaitForSeconds(Random.Range (maneuverTime.x, maneuverTime.y));
            targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }

    // Makes the enemies stay within the game area
	void FixedUpdate () {
        float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        rb.velocity = new Vector3(newManeuver, 0.0f, currentSpeed);
        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );
        // Rotates the enemy ship like the player when it travels along the x-axis
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}
