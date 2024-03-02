using System;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script is attached to your Dead Wall game object. 
//Anytime a cube slips past you and encounters the dead wall it will be destroyed immediately and the Hits Missed UI will be incremented. 
//The Box Colliders on both your Red and Blue Cube prefabs, must have their 'isTrigger' property checked.

public class DeadWall : MonoBehaviour
{
    [SerializeField] private MenuController menuController;
    int missed = 0;
    //Events
    public event Action<int> OnCubeMissed = (count) => { }; //This event  is being listened for in the 'BoxesMissed' script. Whenever a punch against the cube/box is either incorrectly performed or you miss punching the cube, this event is triggered.

    private void OnTriggerEnter(Collider other)
    {
       // Debug.Log($"Cube : {other.gameObject.name} collided with Dead Wall");

        OnCubeMissed(1); //Raise event letting the script 'BoxesMissed' know that the player either missed punching the cube or cube was punched incorrectly, hence it slipped away.

        if (other.gameObject.GetComponentInParent<Rigidbody>().gameObject)
        {
            missed++;
        }

        if(missed >= 35)
        {
            LoseGame();
        }

        Destroy(other.gameObject.GetComponentInParent<Rigidbody>().gameObject); //Destroy this cube block that slipped away from your punch.

        /*****
        In the above Destroy() method call, you are searching for a Rigid body component, on the first available game object, that is a parent,
        of the object (red/blue cube) that collided with the dead wall.
        Your red/blue cube object, nested within the mesh container, is the one that sports the box collider setup as a trigger,
        so collision can only be detected against this nested red/blue cube that has a box collider on it.  
        However, you need to destroy the topmost parent object, within which your red/blue cube is nested. 
        That is you need to destroy the instantiated,Interactions.Interactable_Blue or Red cube game objects actually,which in turn destroys its nested child objects. 
        You will find that the Rigidbody only exists at this topmost,Interactions.Interactable_Blue/Red cube game object,so you need to locate this Rigidbody first.
        Then once this Rigidbody is locted and returned to you, you access the game object it was attached to, 
        which would be the Interactions.Interactable_Blue/Red cube game object, the topmost parent object, of your red/blue nested cubes.
        Once you have this game object returned, your Destroy() method above destroys it, in essence destroying the entire cube block. 
        *****/
    }

    private void LoseGame()
    {
        menuController.GameLose();
    }
}
