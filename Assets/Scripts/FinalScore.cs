using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalScore : MonoBehaviour
{
    // Accesses white ball 
    private WhiteBall whiteBall;

    // Reference to the Game Over canvas
    private TMP_Text gameOverCanvas;

    // score gameobject
    private Score score;

    // Start is called before the first frame update
    void Start()
    {
        // white ball accessed
        whiteBall = FindObjectOfType<WhiteBall>();

        // Find the Text component and assign it to gameOverCanvas.
        gameOverCanvas = GetComponent<TMP_Text>();

        // Ensure the Game Over canvas is initially disabled
        gameOverCanvas.text = "";

        score = FindObjectOfType<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if white ball is pocketed or if all numbered balls are pocketed
        if (whiteBall.pocketed || GameObject.FindObjectsOfType<NumberedBall>().Length == 0)
        {
            // If game is over, show the final score
            gameOverCanvas.text = "Final Score: " + score.current_score;
        }
        else
        {
            // If white ball is not pocketed, hide the Game Over canvas
            gameOverCanvas.text = "";
        }
    }
}
