using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberedBall : MonoBehaviour
{
    // rigid body of ball
    public Rigidbody2D rb;

    // sprite renderer of ball
    public SpriteRenderer spriteRender;

    // past velocity of ball
    public Vector3 pastVelocity;

    // friction constant
    private float friction;

    // score gameobject
    private Score score;

    // audio manager for whiteball
    public AudioManager audiomanager;

    // count for collisions
    int collisionCount;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        friction = 0.999f;
        score = FindObjectOfType<Score>();
        audiomanager = FindObjectOfType<AudioManager>();
        collisionCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        addFriction(); // adds friction to ball
        pastVelocity = rb.velocity; // keeps up with velocity from past frame
    }

    // adds basic friction to the ball
    void addFriction()
    {
        rb.velocity = rb.velocity * friction;
        if (rb.velocity.magnitude < 0.2f)
        {
            rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided GameObject has the "Pocket" tag
        if (collision.gameObject.CompareTag("Pocket"))
        {
            Destroy(gameObject);
            score.current_score = score.current_score + 10; // increases score by 10 when ball goes into pocket
            audiomanager.SoundPocket();
            audiomanager.SoundScoring();
        }
        // Check if the collided GameObject has the "Pocket" tag
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Reverse the velocity of the ball upon collision to simulate bouncing.
            Vector3 reflection = Vector3.Reflect(pastVelocity, collision.contacts[0].normal);
            rb.velocity = reflection * 0.7f; // make the ball slow down once it hits wall
            audiomanager.SoundCushion();
        }
        // Check if the collided GameObject has the "NumberedBall" tag
        if (collision.gameObject.CompareTag("NumberedBall"))
        {
            if(collisionCount % 2 == 0)
            {
                audiomanager.SoundCollideBalls();
            }
            collisionCount++;
        }
            pastVelocity = rb.velocity;
    }
}
