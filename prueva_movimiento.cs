using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prueva_movimiento : MonoBehaviour 
{
    //Declaración de variables
    public float fuerzaSalto;
    public int saltomulti;
    public LayerMask capaSuelo;
    private BoxCollider2D boxCollider;
    private bool mirandoDerecha;
    private int saltorestan;
    private Animator animator;

    [Range(1, 10)] public float velocidad; //Velocidad del jugador
    Rigidbody2D rb2d;
    SpriteRenderer spRd;

    private void Start () 
    {

        //Capturo y asocio los componentes Rigidbody2D y Sprite Renderer del Jugador
        rb2d = GetComponent<Rigidbody2D>();
        spRd = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent <BoxCollider2D>();
        saltorestan = saltomulti;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        ProcesarSalto();   
    }

    void FixedUpdate () 
    {
        
        // Movimiento horizontal
        float movimientoH = Input.GetAxisRaw("Horizontal");
        rb2d.velocity = new Vector2(movimientoH * velocidad, rb2d.velocity.y);

        // Sentido horizontal (para girar el render del jugador)
        if (movimientoH > 0)
        {
            spRd.flipX = false;
        }
        else if (movimientoH < 0)
        {
            spRd.flipX = true;
        }

        if ( movimientoH != 0f )
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
    }

    bool EstaEnSuelo()

    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.down, 0.2f, capaSuelo);  
        return raycastHit.collider != null;
    }

    void ProcesarSalto()
    
    {
        if (EstaEnSuelo())
        {
            saltorestan = saltomulti;
        }

        if (Input.GetKeyDown(KeyCode.Space) && saltorestan > 0)
        {
            saltorestan = saltorestan - 1;
            rb2d.AddForce(Vector2.up*fuerzaSalto, ForceMode2D.Impulse);

        }
        
        if ( Input.GetKeyDown(KeyCode.Space) || EstaEnSuelo()== false)
        {
            animator.SetBool("Skipping", true);
        }
        else
        {
            animator.SetBool("Skipping", false);
        }

    }

}