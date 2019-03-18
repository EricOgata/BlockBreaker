using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    // config params
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPushVelocity = 2f;
    [SerializeField] float yPushVelocity = 15f;
    [SerializeField] AudioClip[] ballSounds;

    bool hasStarted = false;

    // state
    Vector2 paddleToBallVector;

    // Cached component references
    AudioSource myAudioSource;

	// Use this for initialization
	void Start () {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if(hasStarted == false) {
            LockBallToPaddle();
            LunchOnMouseClick();
        }
    }

    private void LunchOnMouseClick() {
        if (Input.GetMouseButtonDown(0)) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(xPushVelocity, yPushVelocity);
            hasStarted = true;
        }
    }

    private void LockBallToPaddle() {
        transform.position = new Vector2(paddle1.transform.position.x + paddleToBallVector.x,
                                         paddle1.transform.position.y + paddleToBallVector.y);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (hasStarted) {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
        }
    }

}
