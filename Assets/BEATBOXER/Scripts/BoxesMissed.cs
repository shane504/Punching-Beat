using UnityEngine;
using UnityEngine.UI;

//This script needs to reside on some sort of a UI element, updating the number of boxes he/she missed to hit, while game is being played, giving the player an indication of how he/she is fairing.
//This script is placed on the 'BoxesMissed object' within the 'PlayerHUD\Canvas_Player_Stats\Canvas' UI.
//This script assumes that the number of Boxes that can be spawned are finite and decided for each level before the game starts.


public class BoxesMissed : MonoBehaviour
{

    [SerializeField] private Text hitsMissed; //Drag the 'HitsMissed' object into this field.
    [SerializeField] private Image fillBar; //Drag the 'FillingBar' object into this field.
    [SerializeField] private GameManager gameManager; //access to the GameManager class so you can access its 'BoxesInLevel' property.
    [SerializeField] private DeadWall deadWall; //access to the DeadWall class so you may access its OnCubeMissed event.
  
    private float totalBoxCount; //Total number of boxes that exist in the level.
    private float missedhits; //keeps track of the number of incorrect or missed hits 

    private void Start()
    {
        //get and display the number of missed or incorrect hits 
        hitsMissed.text = missedhits.ToString(); //displays total number of boxes missed at the start of the level, which will be zero.
        fillBar.fillAmount = 0f; //set the fillbar to 0% as no box has been missed or incorrectly hit at start of level.
        totalBoxCount = gameManager.BoxesInLevel;
    }

    private void OnEnable()
    {
        deadWall.OnCubeMissed += UpdateUIData; //The moment a cube encounters the Dead Wall, update the UI Text and fill bar for Hits Missed.
    }

    private void OnDisable()
    {
        deadWall.OnCubeMissed -= UpdateUIData;
    }




    void UpdateUIData(int missed)
    {
        missedhits += missed;

        //Show the count of missed or incorrect hits on the Diegetic UI.
        hitsMissed.text = missedhits.ToString();
        fillBar.fillAmount = missedhits / totalBoxCount ;


        //Debug.Log($"Fill Bar Fill Amount : { fillBar.fillAmount}");

    }
}
