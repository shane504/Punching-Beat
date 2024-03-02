using UnityEngine;
using UnityEngine.UI;

//This script needs to reside on some sort of a UI element, updating the number of boxes hit while game is being played, giving the player an indication of how he/she is fairing.
//This script is placed on the 'BoxesHit object' within the 'PlayerHUD\Canvas_Player_Stats\Canvas' UI.
//This script assumes that the number of Boxes that can be spawned are finite and decided for each level before the game starts.


public class BoxesHit : MonoBehaviour
{

    [SerializeField] private Text hitsScored; //Drag the 'HitsScored' object into this field.
    [SerializeField] private Image fillBar; //Drag the 'FillingBar' object into this field.
    [SerializeField] private GameManager gameManager; //access to the GameManager class so you can access its 'BoxesInLevel' property.
    [SerializeField] private Punch leftPunch; // //access to the punch class on the left controller as you need to listen for its OnCubeHit event.
    [SerializeField] private Punch rightPunch; // //access to the punch class on the Right controller as you need to listen for its OnCubeHit event.


    private float totalBoxCount; //Total number of boxes that exist in the level.
    private float hitsCount; //keeps track of the number of valid hits made.

    private void Start()
    {
        //get and display the number of valid hits scored against the boxes.
        hitsScored.text = hitsCount.ToString(); //displays total number of boxes hit at the start of the level, which will be zero.
        fillBar.fillAmount = 0f; //set the fillbar to 0% as no box has been hit at start of level.
        totalBoxCount = gameManager.BoxesInLevel;
    }

    private void OnEnable()
    {
        leftPunch.OnCubeHit += UpdateUIData;
        rightPunch.OnCubeHit += UpdateUIData;

    }

    private void OnDisable()
    {
        leftPunch.OnCubeHit -= UpdateUIData;
        rightPunch.OnCubeHit += UpdateUIData;

    }

    void UpdateUIData(int hit)
    {
        hitsCount += hit;

        //Show the hits scored on the Diegetic UI.
        hitsScored.text = hitsCount.ToString();
        fillBar.fillAmount = hitsCount / totalBoxCount ;


        //Debug.Log($"Fill Bar Fill Amount : { fillBar.fillAmount}");

    }
}
