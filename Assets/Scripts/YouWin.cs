using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class YouWin : MonoBehaviour
{
    // Reference to the Game Over canvas
    private TMP_Text gameOverCanvas;

    // Start is called before the first frame update
    void Start()
    {
        // Find the Text component and assign it to gameOverCanvas.
        gameOverCanvas = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindObjectsOfType<NumberedBall>().Length == 0)
        {
            // If all the balls are pocketed, show the Game Over canvas
            gameOverCanvas.text = "You Win!";
        }
        else
        {
            // If there are still balls left
            gameOverCanvas.text = "";
        }
    }
}
