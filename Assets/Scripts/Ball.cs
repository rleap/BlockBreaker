using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    // Configuration parameters
    [SerializeField] Paddle paddle1;
    [SerializeField] float pushVelocityX = 2f;
    [SerializeField] float pushVelocityY = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;
   
    // State
    Vector2 paddleToBallVector;
    bool hasStarted = false;

    // Cached component references
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2d;
    GameSession myGameSession;

    // Use this for initialization
    void Start ()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2d = GetComponent<Rigidbody2D>();
        myGameSession = FindObjectOfType<GameSession>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();

            if (myGameSession.IsAutoPlayEnabled())
            {
                LaunchBall();
            }
        }
    }

    private void LaunchOnMouseClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            LaunchBall();
        }
    }

    private void LaunchBall()
    {
        hasStarted = true;
        myRigidBody2d.velocity = new Vector2(pushVelocityX, pushVelocityY);
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2(
            UnityEngine.Random.Range(0f, randomFactor),
            UnityEngine.Random.Range(0f, randomFactor)
        );

        if(hasStarted)
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigidBody2d.velocity += velocityTweak;
        }

    }
}
