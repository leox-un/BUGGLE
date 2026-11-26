using System;
using UnityEngine;

public class Killable : MonoBehaviour
{
    public Action<GameObject> OnKill;

    
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Kill")) return;
        
        OnKill?.Invoke(gameObject);
        Destroy(gameObject);
        Debug.Log("KILLKILLKILLKILLKILL");
    }


}

