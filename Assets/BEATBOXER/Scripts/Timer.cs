using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text timerTxt;

    private float eT; // Elapsed Time.


    void Update()
    {
        eT = Time.realtimeSinceStartup;
        DisplayTime(eT);
    }

    void DisplayTime(float timeToDisplay)
    {
        int minutes = Mathf.FloorToInt(timeToDisplay / 60f);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60f);

        timerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


}
