using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhiteBall : MonoBehaviour
{
    // rigid body of ball
    public Rigidbody2D rb;

    // sprite renderer of ball
    public SpriteRenderer spriteRender;

    // past velocity of ball
    public Vector3 pastVelocity;

    // basic hit power
    public float hitPowerScale;

    // the whiteball Gameobject
    public Aim aim;

    // The GameObject to revolve around
    public Transform aimTransform;

    /// Vector from ball to the aim
    private UnityEngine.Vector2 OffsetToAim => aimTransform.position - gameObject.transform.position;

    /// Unit vector in the direction of the aim, relative to us
    private UnityEngine.Vector2 HeadingToAim => OffsetToAim.normalized;

    // friction constant
    private float friction;

    // Flag to track whether the ball is currently moving
    private bool isMoving = false;

    // The power slider
    public Slider powerSlider;

    // score gameobject
    private Score score;

    // did white ball go into a pocket
    public bool pocketed;

    // audio manager for whiteball
    public AudioManager audiomanager;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
        hitPowerScale = 30.0f;
        friction = 0.999f;
        powerSlider = FindObjectOfType<Slider>();
        score = FindObjectOfType<Score>();
        pocketed = false;
        audiomanager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        play();


        pastVelocity = rb.velocity;
    }

    // basic functionality of game
    void play()
    {
        // Check if the ball is still moving
        isMoving = rb.velocity.magnitude > 0.01f;

        if (!isMoving)
        {
            Hit();
        }
        addFriction();
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

    // when ball is hit
    void Hit()
    {
        float hitPower = powerSlider.value;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = HeadingToAim * hitPowerScale * hitPower;
            if(hitPower != 0)
            {
                audiomanager.SoundCollideBalls();
                score.current_score = score.current_score - 1; // score decreases by one every hit
            }
        }
    }


    // detects collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided GameObject has the "Pocket" tag
        if (collision.gameObject.CompareTag("Pocket"))
        {
            DestroyWhiteBall();
            pocketed = true;
            audiomanager.SoundGameOver();
            audiomanager.SoundPocket();
        }
        // Check if the collided GameObject has the "Pocket" tag
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Reverse the velocity of the ball upon collision to simulate bouncing.
            Vector3 reflection = Vector3.Reflect(pastVelocity, collision.contacts[0].normal);
            rb.velocity = reflection * 0.7f; // make the ball slow down once it hits wall
            audiomanager.SoundCushion();
        }
        if(collision.gameObject.CompareTag("NumberedBall"))
        {
            audiomanager.SoundCollideBalls();
        }


        pastVelocity = rb.velocity;
    }

    // makes ball invisible when put into pocket
    void DestroyWhiteBall()
    {
        Destroy(gameObject);
        aim.DestroyAim();
    }

}
