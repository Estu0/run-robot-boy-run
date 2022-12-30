using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Coeur : MonoBehaviour
{
    public float dureeVie = 5f;
    public PlayableAsset timeline2;
    // Start is called before the first frame update
    void Start()
    {
        // duree de vie
        Destroy(gameObject, dureeVie);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Robot")) 
        {
            // Arreter la descente
            gameObject.GetComponent<Rigidbody2D>().simulated = false;
            // Jouer l'animation 2
            gameObject.GetComponent<PlayableDirector>().Play(timeline2, DirectorWrapMode.Hold);
            // Destruction de l'objet
            Destroy(gameObject, 2f);
        }
    }
}
