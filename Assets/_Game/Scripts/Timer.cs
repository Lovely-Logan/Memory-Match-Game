using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [Header ("Timer Settings")]
    public Slider timeSlider;
    public Text timeText;
    public float gameTime = 60f;
    public GameObject gameover;

    private bool stopTimer = false;
    private float startTime;

    void Start()
    {
        ResetTimer(); // Start the timer
    }

    void Update()
    {
        if (stopTimer) return; // if the timer is stopped, don't update

        float timePassed = Time.time - startTime; // how much time has passed
        float timeLeft = gameTime - timePassed; // how much time is left

        if (timeLeft <= 0f) // if the time is up
        {
            timeLeft = 0f; // set the time left to 0
            stopTimer = true; // stop the timer
            gameover.SetActive(true); // show the gameover screen
        }

        UpdateUI(timeLeft); // update the UI
    }

    void UpdateUI(float timeLeft) // update the UI
    {
        int minutes = Mathf.FloorToInt(timeLeft / 60); // calculate the minutes
        int seconds = Mathf.FloorToInt(timeLeft % 60); // calculate the seconds

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // display the time
        timeSlider.value = timeLeft; // update the slider
    }

    public void ResetTimer() // reset the timer
    {
        stopTimer = false; // start the timer
        startTime = Time.time; // get the current time
        gameover.SetActive(false); // hide the gameover screen

        timeSlider.maxValue = gameTime; // set the max value of the slider
        timeSlider.value = gameTime; // set the current value of the slider
        UpdateUI(gameTime); // update the UI
    }
}
