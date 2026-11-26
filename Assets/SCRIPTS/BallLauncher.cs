using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //Make sure you have this.

public class BallLauncher : MonoBehaviour
{

    //I will leave comments explaining what the script does and how to set it up in your Unity project or want to mess around with it.
    //lmk me know if you have any questions, it breaks, or need help setting it up. :)
    //-Sjofn

    [Header("Launcher Objects")]

    [Tooltip("Drag and drop your camera object from the scene list here")]
    [SerializeField] Camera mainCamera; //!!!MAKE SURE YOUR CAMERA IS SET TO ORTHOGRAPHIC AND NOT PERSPECTIVE!!!
    [Tooltip("Drag and drop your ball prefab from your prefabs folder here")]
    [SerializeField] GameObject ballPrefab; 
    [Tooltip("Drag and drop your spawn location Transform from the scene list here")]
    [SerializeField] Transform spawnLocation; 

    [Header("Launcher Objects")]

    [Tooltip("Dictates how fast the ball will come out of the launcher")]
    [SerializeField] float launcherForce; 

    [Header("Launcher Rotation Limit")]
    [SerializeField] float centerAngle;
    [SerializeField] float maximumRotation;
    
    [Header("State Tracking")]
    [SerializeField] public int maxBalls = 10;
    [SerializeField] public int remainingBalls = 0;
    [SerializeField] bool hasActiveBalls;
    [SerializeField] List<GameObject> activeBalls;
    public Action<GameObject> _TryReEnableFiring;

    void Awake()
    {
        _TryReEnableFiring = TryReEnableFiring;
    }

    void OnEnable()
    {
        remainingBalls = maxBalls;
    }

    void Update()
    {
        if(!hasActiveBalls)
            PositionCannon();
    
        if(Input.GetMouseButtonDown(0)) //"If you click left mouse button:
        {
            SpawnBall(); //calls the SpawnBall method below! VVV
        }
    }

    public void PositionCannon()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Debug.Log(mousePosition);
        //This finds your mouse position every frame.

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mainCamera.nearClipPlane));
        //sets your mouse position relative to the camera position.

        Vector3 rotateDirection = (worldPosition - transform.position).normalized;
        rotateDirection.z = 0;
        //sets the rotate direction to the mouse position relative to the camera position.

        float angle = Mathf.Atan2(rotateDirection.y, rotateDirection.x) * Mathf.Rad2Deg;
        //turns this rotation direction into an angle

        float difference = Mathf.DeltaAngle(centerAngle, angle);
        //calculates the difference between current angle and the center

        float clampedDifference = Mathf.Clamp(difference, -maximumRotation, maximumRotation);
        //clamps the difference to the maximum rotation angle (so you can't aim upwards)

        float clampedAngle = centerAngle + clampedDifference;
        //adds the clamped difference to move the launcher

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, clampedAngle));
        //rotates the game object to said angle!
    }

    public void TryReEnableFiring(GameObject removedBall)
    {
        activeBalls.Remove(removedBall);

        if (remainingBalls <= 0)
        {
            GameManagement.Instance.Loose();
            return;
        }
        
        if (activeBalls.Count <= 0)
            hasActiveBalls = false;
    }


    public void SpawnBall()
    {
        if (remainingBalls <= 0) return;
        if (hasActiveBalls) return;
        
        hasActiveBalls = true;
        remainingBalls--;
        
        Debug.Log("Fired Ball");

        GameObject ballPrefabClone = Instantiate(ballPrefab, spawnLocation.position, spawnLocation.rotation);
        activeBalls.Add(ballPrefabClone);
        var ball = ballPrefabClone.GetComponent<Killable>();
        ball.OnKill = _TryReEnableFiring;
        
        //spawns in a ball at the position and rotation of the spawnLocation Transform.
        //Basically, it spawns it ontop of itself.

        Rigidbody2D ballCloneRB = ballPrefabClone.GetComponent<Rigidbody2D>();
        //grabs instantiated objects rigidbody

        ballCloneRB.AddForce(-spawnLocation.up * launcherForce, ForceMode2D.Impulse);
        //adds force down and multiplied by the launcherForce variable.
    }

}
