using System.Numerics;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private Rigidbody2D ballRb;

    [Header("Ball Variables")]

    [Tooltip("Dictates how much the ball will move when you press A or D")]
    [SerializeField] float moveForce;

    void Start()
    {
        ballRb = GetComponent<Rigidbody2D>();
        //grabs ball's rigidbody
        //Remeber to apply a 2D rigidbody and not a 3D one!
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.D)) //when player presses "D"
        {
            ballRb.AddForce(transform.right * moveForce); //push the ball to the right multiplied by the moveForce float
        }

        if (Input.GetKey(KeyCode.A))
        {
            ballRb.AddForce(transform.right * -moveForce); //negative move force to push the object to the left lol.
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        GetComponent<BallScript>().enabled = false; //this disables the script when it collides with anything.
        //if you want to keep controlling the bugs even after they collide with something (or just indefinitely), just delete or comment this part out!!
    }
}
