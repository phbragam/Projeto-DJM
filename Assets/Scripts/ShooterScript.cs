﻿using UnityEngine;
using System.Collections;

public class ShooterScript : MonoBehaviour {

    public GameObject missilePrefab;

    private float speed = 12f;
	private float damage = 25f;
    private Vector3 spawningPos;
    private bool active = true;
    private AudioSource shootingAudio;

    // Use this for initialization
    void Start () {
        shootingAudio = GetComponent<AudioSource>();
        spawningPos = new Vector3(transform.position.x+transform.localScale.x, -4.5f, transform.position.z);
        Spawn ();
    }

    void Spawn() {
        if (active) {
            GameObject missile = GameObject.Instantiate (missilePrefab) as GameObject;

            missile.transform.position = spawningPos;
            missile.transform.parent = transform;
			damage = Random.Range (0f, 100f);
			speed = Random.Range (20f, 100f);

            missile.gameObject.GetComponent<MissileController> ().speed = speed;
			missile.gameObject.GetComponent<MissileController> ().damage = damage;
            shootingAudio.Play();

            Invoke ("Spawn", Random.Range (1f, 1.5f));
        }
    }

    public void Stop() {
        BroadcastMessage("StopMissile");
        active = false;
    }
}