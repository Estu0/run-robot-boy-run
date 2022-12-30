using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Bombe : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // collision avec le Robot
        if (collision.gameObject.CompareTag("Robot"))
        {
            // animation du Ballon
            gameObject.GetComponent<PlayableDirector>().Play();
            // Destruction de l'objet
            Destroy(gameObject, 1f);
        }
    }
}
