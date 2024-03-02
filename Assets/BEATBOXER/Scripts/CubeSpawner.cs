using System;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    //Events
    public event Action<int> OnCubeSpawned = (count) => { }; //This event  is being listened for in the 'TotalBoxesInLevel' script. Whenever a cube is spawned, this event is raised in this script. 
    public event Action OnSpawningComplete = () => { }; //This event is being listend for in the 'GameManager' script. It gets raised here once all cubes have been spawned. 


    // cube prefabs to be spawned. You have just 2 cubes, the red and blue ones.
    [SerializeField] GameObject[] cubes;

    // used to compute Interval between beats
    [SerializeField] float beatsPerMinute = 130;

    [SerializeField] private GameManager gameManager; //access to the GameManager class so you can access its 'BoxesInLevel' property, which indicates the maximum number of cubes that can be spawned for a level. 


    private float timer = 0f;     // timer counter to spawn another cube.
    private int cubeSpawnThreshold; //Represents the maximum number of cubes you can spawn in a level, based on the value set within the GameManager component. 
    private int cubesSpawned; //keeps a count of the number of cubes spawned

    // Have 5 unique lane starting positions for the cubes.
    private float[,] lanePositions =
    {
        {0f, 1f, 0f },
        {-0.6f,1.25f, 0 },
        {0.6f, 1.25f, 0 },
        {-0.3f, 1f, 0 },
        {0.3f, 1f, 0 }
    };


    private void Start()
    {
        //Obtain the value of the maximum number of cubes that can be spawned for this level.
        cubeSpawnThreshold = gameManager.BoxesInLevel;
    }

    private void Update()
    {
        if (cubesSpawned >= cubeSpawnThreshold) //if you have spawned the alloted number of cubes for the level, don't spawn any more cubes.
        {
            OnSpawningComplete(); //This event is being listened for in the GameManager script
            gameObject.SetActive(false); //Deactivate the Cube Spawner, so no more cubes can be spawned and unnecessary calls to Update() are not made.
            return;
        }

        timer += Time.deltaTime;

        float beatInterval = (60.0f / beatsPerMinute) * 2f; //create a cube  after approx every 1.5 seconds.


        // Cubes appear with beats
        if (timer > beatInterval)
        {
            timer = 0f; //set timer to 0 so it can start counting down again.
            CreateCube();
            OnCubeSpawned(1); //Raise event letting the script 'BoxesInLevel' know that 1 cube has been spawned.
        }
    }

    void CreateCube()
    {
        if (cubes.Length == 0) return; //you have not populated cubes into the array.

        // Instantiate a random cube model.
        int randomCube = UnityEngine.Random.Range(0, cubes.Length);
        GameObject cube = Instantiate(cubes[randomCube]);

        cube.transform.position = transform.position; //position cube at spawner position.

        if(cube.gameObject.tag == "Red Box")
        {
            cube.transform.Rotate(transform.forward, UnityEngine.Random.Range(90, 180));
        }
        if(cube.gameObject.tag == "Blue Box")
        {
            cube.transform.Rotate(transform.forward, UnityEngine.Random.Range(-180, -90));
        }
        //cube.transform.Rotate(transform.forward, 90 * UnityEngine.Random.Range(0, 4)); //Rotate the parent cube randomly by 90 degrees so that the inner cube you need to hit is able to change its rotation, requiring you to either Thump, right/left hook or upper cut the cube, based on how the inner cube is positioned.

        // Select a random lane the cube will start within. You have 5 lanes available for use
        int lane = UnityEngine.Random.Range(0, 5);
        
        cube.transform.position += new Vector3(lanePositions[lane, 0], lanePositions[lane, 1], lanePositions[lane, 2]); //set the cube to start off in its randomly selected lane.

        cubesSpawned++; //increment the count of the number of cubes spawned, as you can spawn only a certain number of cubes for a level.
    }

}

