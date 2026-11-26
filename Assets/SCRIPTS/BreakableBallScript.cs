using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BreakableBallScript : MonoBehaviour
{

    [Header("Refs")]
    public SpriteRenderer sprRenderer;

    [Header("Ball Durability")]
    [Tooltip("Times hit")]
    [SerializeField] int timesHitCounter = 0;
    [SerializeField] int currentHitState = -1;
    
    // Last HitState is always dead hit state, and doesnt need a Sprite.
    [Tooltip("Last HitState is always dead hit state, and doesnt need a Sprite.")]
    public List<HitState> hitStates = new();

    void Awake()
    {
        // callback for destroy on the final hitstate's onEnterHitState
        if (hitStates.Count <= 0) return;
        hitStates[^1].onEnterHitState ??= () => Destroy(gameObject);
    }

    void Start()
    {
        // set hitstate to -1 to indicate we have not reached a valid hitstate yet
        currentHitState = -1;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Ball")) return; //"if the other game object is hit with an object with the ball tag
        
        timesHitCounter++; //add one to the times hit counter

        TryHitTransition(timesHitCounter);
    }

    public void TryHitTransition(int hitCount)
    {
        int nextHitState = currentHitState + 1;
        
        if (nextHitState >= hitStates.Count) return; // bounds check
        if (hitCount < hitStates[nextHitState].hitGreaterThan) return; // not reached hit state yet
        
        sprRenderer.sprite = hitStates[nextHitState].sprite;
        currentHitState = nextHitState; // increment state
        hitStates[currentHitState].onEnterHitState?.Invoke(); // callback
    }



    [Serializable]
    public class HitState
    {
        public int hitGreaterThan;
        public Sprite sprite;
        public Action onEnterHitState;
    }
    
    // onvalidate is called everytime anything in the editor is changed
    // this is here to ensure hit states are ascending in order from 0 -> end bound
    void OnValidate()
    {
        if (hitStates.Count == 0) return;
        int currentHitGreaterThan = hitStates[0].hitGreaterThan;
        for (int i = 1; i < hitStates.Count; i++)
        {
            if (hitStates[i].hitGreaterThan <= currentHitGreaterThan)
            {
                Debug.LogError("HitStates must be in ascending order!");
                return;
            }
            currentHitGreaterThan = hitStates[i].hitGreaterThan;
        }
    }
}
