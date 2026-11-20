using UnityEngine;

public class BreakableBallScript : MonoBehaviour
{
    [Header("Ball Durability")]

    [Tooltip("Times hit")]
    [SerializeField] float timesHitCounter = 0f;

    [Tooltip("How many hits it takes to break the ball")]
    [SerializeField] float hitsNeededToBreak = 5f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ball")) //"if the other game object is hit with an object with the ball tag
        {
            timesHitCounter++; //add one to the times hit counter

            if(timesHitCounter == hitsNeededToBreak) //if the times hit counter is the same as the hits needed to break number
            {
                Destroy(gameObject); //destroy this game object
            }
        }
    }

}
