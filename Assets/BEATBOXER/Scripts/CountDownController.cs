using System.Collections;
using UnityEngine;

public class CountDownController : MonoBehaviour
{
    // count down values to display. First ensure that all these count down objects are deactivated in the hierarchy. Also ensure that the CubeSpawner and PlayerHUD game objects are deactivated in the hierarchy. 
    [SerializeField] GameObject[] countDownObject;
    [SerializeField] GameObject cubeSpawner; //Access to the cube spawner game object in the hierarhcy, so that you may activate it once the count down has completed. 
    [SerializeField] GameObject playerHUD; //Access to the player HUD game object in the hierarchy, so that you may activate it once the count down has completed. 

    private int countDown = 3; //and index into the countDownObject Array as well as a counter.

    void Start()
    {
        StartCoroutine(CountDown());
    }

  
    IEnumerator CountDown()
    {
        while (countDown >= 0)
        {
            countDownObject[countDown].SetActive(true);

            yield return new WaitForSeconds(1f);

            countDownObject[countDown].SetActive(false);

            countDown--;
        }

        playerHUD.SetActive(true); //Activate  the Player HUD game object in the hierarchy.

        cubeSpawner.SetActive(true); //Activate  the Cube Spawner game object in the hierarchy.

    }

}
