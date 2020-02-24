using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //ConfigParams
    [SerializeField] Paddle paddle1;
    [SerializeField] float xV = 2f;
    [SerializeField] float yV = 15f;
    [SerializeField] float randomFactor = 0.2f;
    bool hasStarted = false;
    [SerializeField] AudioClip[] ballSounds;

    AudioSource myAuduiSource;
    Rigidbody2D myRigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        myAuduiSource = GetComponent<AudioSource>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasStarted)
            LockBallToPaddle();
        LaunchBallOnClick();
    }

    private void LaunchBallOnClick()
    {
        if (Input.GetMouseButtonDown(0) && !hasStarted)
        {
            myRigidbody2D.velocity = new Vector2(xV, yV);
            hasStarted = true;
        }
    }

    private void LockBallToPaddle()
    {
        Vector3 paddlePosition = new Vector3(paddle1.transform.position.x,
                    paddle1.transform.position.y + 0.25f, 1);
        transform.position = paddlePosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 vTweak = new Vector2(UnityEngine.Random.Range(0f, randomFactor), 
                                     UnityEngine.Random.Range(0f, randomFactor));

        if (hasStarted)
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAuduiSource.PlayOneShot(clip);
            myRigidbody2D.velocity += vTweak;
        }
    }
}
