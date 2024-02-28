using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[RequireComponent(typeof(TMP_Text))]

public class Score : MonoBehaviour
{
    /// Text component for displaying the score
    private TMP_Text scoreDisplay;

    public int current_score;

    // Start is called before the first frame update
    void Start()
    {
        current_score = 0;
        scoreDisplay = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // Updates the text field of score display
        scoreDisplay.text = "Score: " + current_score;
    }
}
