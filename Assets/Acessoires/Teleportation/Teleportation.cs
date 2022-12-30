using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public Transform destination;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Robot"))
        {
            // jouer son
            gameObject.GetComponent<AudioSource>().Play();

            // Teleportation
            collision.transform.position =
                destination.transform.position + Vector3.up;
        }
    }
}
