using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionnaireAccessoires : MonoBehaviour
{
    // Ballons
    public GameObject ballon;
    public int nbBallons;
    // Bombes
    public GameObject bombe;
    public int nbBombe;
    // Coeurs
    public GameObject coeur;
    public int nbCoeurs;
    public float intervalleCoeur;
    // Limites de la scene
    public Transform pointHG;
    public Transform pointBD;
    // écrans
    public GameObject ecranAccueil;
    public GameObject jeu;

    // Start is called before the first frame update
    void Start()
    {
        // Ballons
        for (int i = 0; i < nbBallons; i++) 
        {
            // position aleatoire
            float x = Random.Range(pointHG.position.x, pointBD.position.x);
            float y = Random.Range(pointHG.position.y, pointBD.position.y);
            // creation d'objet
            Instantiate(ballon,
                new Vector2(x, y),
                Quaternion.identity,
                gameObject.transform.parent);
        }

        // Bombes
        for (int i = 0; i < nbBombe; i++)
        {
            // position aleatoire
            float x = Random.Range(pointHG.position.x, pointBD.position.x);
            float y = Random.Range(pointHG.position.y, pointBD.position.y);
            // creation d'objet
            Instantiate(bombe,
                new Vector2(x, y),
                Quaternion.identity,
                gameObject.transform.parent);
        }

        // Coeurs
        InvokeRepeating("CreationCoeurs", intervalleCoeur, intervalleCoeur);

    }

    void CreationCoeurs()
    {
        // Coeurs
        for (int i = 0; i < nbCoeurs; i++)
        {
            // position aleatoire
            float x = Random.Range(pointHG.position.x, pointBD.position.x);
            float y = pointHG.position.y;
            // creation d'objet
            Instantiate(coeur,
                new Vector2(x, y),
                Quaternion.identity,
                gameObject.transform.parent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // gestion d'écrans
        // fin de jeu
        if (nbCoeurs < 0)
        {
            ecranAccueil.SetActive(true); // affiche écran accueil
            jeu.SetActive(false); // masque le jeu
        }
    }
}
