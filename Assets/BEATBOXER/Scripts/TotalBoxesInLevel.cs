using UnityEngine;
using UnityEngine.UI;

//This script needs to reside on some sort of a UI element, updating the count of the number of boxes left that have yet to be spawned while game is being played. This lets the player know the total number of boxes avialable in the level
//This script is placed on the 'TotalBoxesWithinLevel object' within the 'PlayerHUD\Canvas_Player_Stats\Canvas' UI.
//This script assumes that the number of Boxes that can be spawned are finite and decided for each level before the game starts.
//This script will  decrement the count of the Total Boxes within the level, each time a new box is spawned.

public class TotalBoxesInLevel : MonoBehaviour
{

    [SerializeField] private Text boxesInLevel; //Drag the 'BoxesInLevel' object into this field. This value gets decremented
    [SerializeField] private Image fillBar; //Drag the 'FillingBar' object into this field. This fill bar decrements.

    [SerializeField] private GameManager gameManager ; //access to the GameManager class so you can access its 'BoxesInLevel' property.
    [SerializeField] private CubeSpawner cubeSpawner; //access to the Cube Spawner class as you need to listen for its OnCubeSpawned event.

    private float totalBoxCount; //Total number of boxes that can  be spawned for this level.
    private float boxesSpawned; //keeps track of the number of boxes being spawned

    private void Start()
    {
        //get and display the count of total boxes available for this level
        totalBoxCount = gameManager.BoxesInLevel;
        boxesInLevel.text = totalBoxCount.ToString(); //displays total number of boxes allowed to be spawned for this level
        fillBar.fillAmount = 1.0f; //set the fillbar to 100% completely filled
    }

    private void OnEnable()
    {
        cubeSpawner.OnCubeSpawned += UpdateUIData;
    }

    private void OnDisable()
    {
        cubeSpawner.OnCubeSpawned -= UpdateUIData;
    }

    void UpdateUIData(int boxCount)
    {
        boxesSpawned += boxCount;

        //Show the remaining Boxes left to spawn on the Diegetic UI.
        boxesInLevel.text = (totalBoxCount - boxesSpawned).ToString();
        fillBar.fillAmount = 1.0f -  (boxesSpawned / totalBoxCount);


        //Debug.Log($"Fill Bar Fill Amount : { fillBar.fillAmount}");

    }

}
