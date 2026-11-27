using UnityEngine;

public class UILifeSystem : MonoBehaviour
{

    [SerializeField] BallLauncher launcher; 
    [SerializeField] Sprite emptySprite;

    private GameObject[] HP;
    private SpriteRenderer[] HPSprites;
    private int ballsLeft;
    private int livesLost = 0;

    void Start()
    {
        
        // Find all children of the object and add their sprites to an array 
        int lives = transform.childCount;
        HP = new GameObject[lives];
        HPSprites = new SpriteRenderer[lives];

        for (int i = 0; i < transform.childCount; i++)
        {
            HP[i] = transform.GetChild(i).gameObject;
            HPSprites[i] = HP[i].GetComponent<SpriteRenderer>();
            Debug.Log("hi im life " + i);
        }

        ballsLeft = launcher.maxBalls;
    }

    void Update()
    {
        int lives = launcher.remainingBalls;

        // See if the life count changes by comparing lives with ballsLeft
        // Changes the last life sprite to an empty sprite when amount of balls changes
        if (lives < ballsLeft)
        {
            HPSprites[launcher.maxBalls - livesLost - 1].sprite = emptySprite;
            
            // Increment for the next life sprite update
            livesLost++;
            ballsLeft--;
        }

    }
}   
