using UnityEngine;
using UnityEngine.SceneManagement;

//This script needs to be placed on the 'GameManager' game object in the hierarchy.
//Allows you to set a pre-defined number of cubes for your level.
//Also is informed when the Level has Completed. 


public class GameManager : MonoBehaviour
{
    
    [SerializeField] private int boxesInLevel = 100; //This value needs to be setup on the Game Manager object prior to game start.
    [SerializeField] private CubeSpawner cubeSpawner; ////access to the CubeSpawner class so you may access its OnSpawningComplete event
    [SerializeField] private float timer = 245f;
    [SerializeField] private MenuController winGame;

    public int BoxesInLevel => boxesInLevel; //get property

    private int cubeCount; //keeps a count of the number of cubes in the level. If cubeCount is zero, all cube blocks have been spawned. 
    private bool allCubesSpawned = false; //maintains the state of whether all cubes have been spawned. Only if all cubes have finished spawning, will you begin executing the code within Update() below.This ensures you don't run the code in update every frame while cubes are still being spawned.


    private void OnEnable()
    {
        cubeSpawner.OnSpawningComplete += AllCubesInLevelSpawned;
    }

    private void OnDisable()
    {
        cubeSpawner.OnSpawningComplete -= AllCubesInLevelSpawned;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            OnLevelComplete();
        }
        if (!allCubesSpawned) //You dont need to unnecessarily count the number of cubes in the level each Update if all Cubes have not been spawned yet.
            return;

        cubeCount = GameObject.FindGameObjectsWithTag("Cube").Length; //Find out if there are any active cubes in the level. 

        if (cubeCount <= 0) //if no active Cube in the level, raise  the OnLevelComplete event.
        {
            OnLevelComplete();
        }

    }


    void AllCubesInLevelSpawned()
    {
        allCubesSpawned = true;
    }

    void OnLevelComplete()
    {

        winGame.WinGame();

    }

}
