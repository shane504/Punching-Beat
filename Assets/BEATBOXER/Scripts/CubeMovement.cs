using UnityEngine;

//This component will be attached to the cube prefabs. 
//The Box Colliders on both your Red and Blue Cube prefabs, must have their 'isTrigger' property checked.


public class CubeMovement : MonoBehaviour
{
    [SerializeField] float cubeSpeed = 2f; //how fast should your cube move

    private Vector3 movement; //how fast should the cube move.


    void Start()
    {
        movement = new Vector3(0f, 0f, cubeSpeed); //set cube speed

    }

    void Update()
    {
        transform.Translate(movement * Time.deltaTime); //Moves the cube along the Z axis

    }


}
