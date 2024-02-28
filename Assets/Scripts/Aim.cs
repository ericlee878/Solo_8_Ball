using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    // the transform of this gameobject
    Transform aimTransform;

    // the whiteball Gameobject
    public WhiteBall whiteBall;

    // The GameObject transform to revolve around
    public Transform whiteBallTransform;

    // The rigidbody of whiteBall
    public Rigidbody2D whiteBallrb;

    // Adjust the speed of rotation
    public float rotationSpeed;

    // Angle of aim relative to whiteBall
    private float angle;

    // Radius of the circular path
    public float radius;

    // Reference to the renderer component of your GameObject
    private Renderer myRenderer;

    // List of objects to track for movement
    private NumberedBall[] numberedBalls = {};


    // Start is called before the first frame update
    void Start()
    {
        aimTransform = gameObject.transform; // Assign the transform of the current GameObject to the variable t
        aimTransform.position = new Vector3(-5, 0, 0); // the starting position of aim

        whiteBall = FindObjectOfType<WhiteBall>(); // Finds the whiteBall gameobject
        whiteBallTransform = whiteBall.transform; // Accesses the transform element of whiteBall


        angle = 0.0f; // initialize the intial angel of aim relative to ball
        rotationSpeed = 0.15f; // assign the rotation speed of circular path
        radius = 1.0f; // assign the radius of the circular path

        whiteBallrb = whiteBall.GetComponent<Rigidbody2D>(); // rigidbody of whiteball
        myRenderer = GetComponent<Renderer>(); // renderer of aim

        numberedBalls = GameObject.FindObjectsOfType<NumberedBall>(); // array of all NumberedBalls
    }

    // Update is called once per frame
    void Update()
    {
        Revolve(); // makes the aim diamond revolve around the ball

        myRenderer.enabled = !checkIfInPlay(); // aim is invisible when balls are in play
    }

    // Makes the aim diamond revolve around the ball
    void Revolve()
    {
        float rotationInput = 0.0f;
        if(Input.GetKey(KeyCode.RightArrow))
        {
            rotationInput = 1.0f;
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            rotationInput = -1.0f;
        }

        // Calculate the new angle
        angle += rotationInput * rotationSpeed;

        // Calculate the position for the circular path
        float x = whiteBallTransform.position.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = whiteBallTransform.position.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);

        // Apply the new position to the GameObject
        aimTransform.position = new Vector3(x, y, aimTransform.position.z);

        // Make the GameObject look at the target
        Vector2 direction = whiteBallTransform.position - aimTransform.position;
        float angleDegrees = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        aimTransform.rotation = Quaternion.Euler(0, 0, angleDegrees); // Offset by -90 degrees for proper orientation
    }

    // checking if the balls are still in play
    private bool checkIfInPlay()
    {
        bool isWhiteBallMoving = whiteBallrb.velocity.magnitude > 0.02f; // Check if the WhiteBall is still moving

        bool isNumberedBallMoving = false;
        // Loop through all the numberedBalls
        foreach (NumberedBall obj in GameObject.FindObjectsOfType<NumberedBall>())
        {
            Rigidbody2D objRB = obj.GetComponent<Rigidbody2D>(); // rigidbody of whiteball
            if (objRB.velocity.magnitude > 0.02f) // Check if the numbered ball is still moving
            {
                isNumberedBallMoving = true;
                break;
            }

        }

        return (isWhiteBallMoving || isNumberedBallMoving);
    }


    // public accessor method to destroy aim
    public void DestroyAim()
    {
        Destroy(gameObject);
    }

}
