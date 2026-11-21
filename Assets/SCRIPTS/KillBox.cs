using UnityEngine;

public class KillBox : MonoBehaviour
{
    private void Start()
    {
        GameObject.FindGameObjectWithTag("Kill");
        Debug.Log("hhhh");
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Kill"))
        {
            Destroy(gameObject);
            Debug.Log("KILLKILLKILLKILLKILL");
        }
        //Destroy(gameObject);
        //Debug.Log("HELLLOOOOO");
    }
    // Update is called once per frame


}

