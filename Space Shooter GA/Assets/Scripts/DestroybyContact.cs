using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroybyContact : MonoBehaviour {
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Makes sure that objects doesn't explode by the game boundary or if the enemies touches an asteroid
        if (other.tag == "Boundary" || other.tag == "Enemy" || other.tag == "Boss")
        {
            return;
        }
        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }

        // Adds score when something explodes
        gameController.AddScore(scoreValue);

        // Destroys stuff when they explode
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
