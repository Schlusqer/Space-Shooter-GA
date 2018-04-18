using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMover : MonoBehaviour {

    public Rigidbody rb;
    private GameController gameController;
    public float speed;
    public Boundary boundary;

	void Start () {
        rb = GetComponent<Rigidbody>();
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
        rb.velocity = transform.forward * speed;
	}
	
	
	private void Update () {
        
        rb.position = new Vector3(
            0.0f,
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );
    }
}
