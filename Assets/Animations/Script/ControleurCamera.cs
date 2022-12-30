using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleurCamera : MonoBehaviour
{
    // publiques
    public Transform personnage; // cible
    // limites de deplacement de camera
    public Transform pointHG;
    public Transform pointBD;

    // privees
    Transform transfo;
    // taille camera
    float cameraDemiLargeur;
    float cameraDemiHauteur;

    // Start is called before the first frame update
    void Start()
    {
        transfo = gameObject.transform;
        Camera cam = gameObject.GetComponent<Camera>();

        cameraDemiHauteur = cam.orthographicSize; // demi hauteur
        cameraDemiLargeur = cam.aspect * cameraDemiHauteur; // demi-Largeur
    }

    // Update is called once per frame
    void Update()
    {
        // limite le deplacement de la camera
        float posHoriz, posVert;

        // deplacement horizontal
        posHoriz = Mathf.Clamp(
            personnage.position.x,
            pointHG.position.x + cameraDemiLargeur,
            pointBD.position.x - cameraDemiLargeur
            );

        // deplacement vertical
        posVert = Mathf.Clamp(
            personnage.position.y,
            pointBD.position.y + cameraDemiHauteur,
            pointHG.position.y - cameraDemiHauteur
            );

        // la camera suit le personnage
        transfo.position = new Vector3(
            posHoriz,
            posVert,
            transfo.position.z
            );
    }
}
