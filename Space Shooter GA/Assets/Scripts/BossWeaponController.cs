using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeaponController : MonoBehaviour
{

    public GameObject shot;
    public Vector3 shotSpawnPosition;
    public Vector3 bossSpawn;
    public Transform shotSpawnBoss;
    public float fireRate;
    public float delay;
    

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Fire", delay, fireRate);
        // Vector3 spawn = new Vector3(bossSpawn.x, bossSpawn.y, bossSpawn.z);
        // Makes the shots spawn at random x-values
        // Vector3 shotSpawnBoss = new Vector3(Random.Range(-shotSpawnPosition.x, shotSpawnPosition.x), shotSpawnPosition.y, shotSpawnPosition.z);
    }

    // Makes the boss fire
    void Fire()
    {
        Instantiate(shot, shotSpawnBoss.position, shotSpawnBoss.rotation);
        audioSource.Play();
    }

}
