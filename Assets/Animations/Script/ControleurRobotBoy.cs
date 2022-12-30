using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControleurRobotBoy : MonoBehaviour
{   // Publique
    public float vitesse; // vitesse deplacement
    public float impulsion; // saut
    public float vieMax; // nombre vies limite
    public GameObject imageVies; // affichage des vies
    public AudioClip musiqueMort; // marche mortuaire

    // privees
    int nbVies = 1; // nombre de vies
    float deplacement; //input
    bool saute; // etat de saut
    bool accroupi; // etat accroupissement
    int nbSauts; // gestion double sauts
    Rigidbody2D rb; // physique
    SpriteRenderer sr; // apparence
    Animator anim; // animateur

    // Initialisation
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
    }
    private void OnEnable()
    {
        deplacement = 0;
        saute = false;
        accroupi = false;
        nbSauts = 0;
        // Vie initial
        imageVies.GetComponent<RectTransform>().localScale = new Vector3(nbVies, 1f, 1f);
        imageVies.GetComponent<RawImage>().uvRect = new Rect(0, 0, nbVies, 1f);
    }

    // controle + logique
    void Update()
    {
        deplacement = Input.GetAxis("Horizontal"); // input du clavier -1, 1
        anim.SetFloat("Etat", Mathf.Abs(deplacement));
        // direction du Robot
        if(deplacement < 0) 
        {
            sr.flipX = true;
        }
        if (deplacement > 0)
        {
            sr.flipX = false;
        }

        // gestion de saut
        if (Input.GetKeyDown("w") && nbSauts < 2) 
        {
            anim.SetTrigger("Saut");
            saute = true;
            nbSauts++;
        }

        // gestion roulade
        if (Input.GetKeyDown("x") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Roulade"))
        {
            anim.SetTrigger("Roulade");
        }

        // gestion accroupissement
        if (Input.GetKeyDown("s") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Accroupi"))
        {
            anim.SetTrigger("Accroupi");
            accroupi = true;
        }
        if (Input.GetKeyUp("s"))
        {
            anim.SetTrigger("Accroupi");
            accroupi = false;
        }
    }

    private void FixedUpdate()
    {
        // marche et accroupissement
        if (!accroupi)
        {
            rb.AddRelativeForce(Vector2.right * vitesse * deplacement);
        }
        else
        {
            rb.AddRelativeForce(Vector2.right * vitesse/2f * deplacement);
        }

        // saut
        if(saute) 
        {
            rb.AddRelativeForce(Vector2.up * impulsion, ForceMode2D.Impulse);
            saute = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        nbSauts = 0;
        GestionCollisions(collision.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GestionCollisions(collision.gameObject);
    }

    // Objets Bonus, malus ou mortem
    void GestionCollisions(GameObject objet)
    {
        if (objet.CompareTag("Bonus") && nbVies < vieMax)
        {
            nbVies++;
            AffichageVies();
        }

        // Malus
        if (objet.CompareTag("Malus"))
        {
            nbVies--;
            AffichageVies();
        }

        // Mortem
        if (objet.CompareTag("Mortem") || nbVies == 0)
        {
            nbVies = 0;
            AffichageVies();
            // arrete de bouger
            impulsion = 0;
            vitesse = 0;
            rb.simulated = false;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            anim.SetTrigger("Mort");

            // Audio
            gameObject.GetComponent<AudioSource>().Stop();
            gameObject.GetComponent<AudioSource>().PlayOneShot(musiqueMort);

            // recommence jeu
            Invoke("Recommencer", 7f);
        }
    }

    // Redemarre jeu
    void Recommencer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // Realise l'affichage de coeurs
    void AffichageVies()
    {
        imageVies.GetComponent<RectTransform>().localScale = new Vector3(nbVies, 1f, 1f);
        imageVies.GetComponent<RawImage>().uvRect = new Rect(0, 0, nbVies, 1f);
    }
}
