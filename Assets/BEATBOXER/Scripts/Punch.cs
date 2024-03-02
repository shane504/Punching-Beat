using System;
using UnityEngine;
using Tilia.Interactions.Interactables.Interactors;


//This script must be attached to each glove. 

public class Punch : MonoBehaviour
{
    //Events
    public event Action<int> OnCubeHit = (count) => { }; //This event  is being listened for in the 'BoxesHit' script. Whenever a cube/box is hit, this event is raised in this script. 
    public event Action<string> OnCubeHitHaptics = (hand) => { }; //This event is being listend for in the 'GenerateHapticFeedback' script.   


    [SerializeField] LayerMask layer; //maps to the cubes layer, ensuring that a Red cub is hit with a Red Glove only and a Blue Cube is hit with a blue glove only.
    
    private Vector3 previousPosn = Vector3.zero; //keeps track of the gloves previous position. Used only for the purpose of creating a directional Vector, that is required by the Vector3.Angle() method used below.

    private string interactor; //Useful in determining whether the Punch was thrown by the Left or Right controller/Interactor/Hand.


    private void Start()
    {
        interactor = gameObject.GetComponentInParent<InteractorFacade>().tag; //Get the tag of the interactor against which, this script is attached.
    }

    void Update()
    {

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 1f, layer))
        {
            Vector3 dirVector = transform.position - previousPosn;

            if (Vector3.Angle(dirVector, hit.transform.up) > 110f) //Vector3.Angle() requires 2 directional vectors as input to yield an angle value, which will always be limited to 180 degrees
            {
                GetComponent<AudioSource>().Play(); // play  the Punch sound, when you punch the cube correctly.
                                
                OnCubeHit(1); //Raise event letting the script 'BoxesHit' know that 1 cube has been hit correctly.
                
                Destroy(hit.transform.gameObject); //cube was punched from the correct side so destroy cube.

                //Debug.Log($"Punch thrown using your : {interactor} Hand");

                OnCubeHitHaptics(interactor); //Raise event passing in the Tag of the controller that hit the cube.
            }
           
        }
      
        previousPosn = transform.position;
    }

}
