using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    // Accesses white ball 
    private WhiteBall whiteBall;

    // Reference to the Game Over canvas
    private TMP_Text gameOverCanvas;


    // Start is called before the first frame update
    void Start()
    {
        // white ball accessed
        whiteBall = FindObjectOfType<WhiteBall>();

        // Find the Text component and assign it to gameOverCanvas.
        gameOverCanvas = GetComponent<TMP_Text>();

        // Ensure the Game Over canvas is initially disabled
        gameOverCanvas.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        // Check if white ball is pocketed
        if (whiteBall.pocketed)
        {
            // If the white ball is pocketed, show the Game Over canvas
            gameOverCanvas.text = "Game Over";
        }
        else
        {
            // If white ball is not pocketed, hide the Game Over canvas
            gameOverCanvas.text = "";
        }
    }
}
