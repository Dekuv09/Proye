using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_person1 : MonoBehaviour
{

    public float velocidad;
    public float fuerzaSalto;
    public int saltomulti;
    public LayerMask capaSuelo;
    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private bool mirandoDerecha = true;
    private int saltorestan;
    private Animator animator;

    // Start is called before the first frame update
    private void Start()
    {
        rigidBody = GetComponent <Rigidbody2D>();
        boxCollider = GetComponent <BoxCollider2D>();
        saltorestan = saltomulti;
        animator = GetComponent<Animator>();
    }
 
    // Update is called once per frame
    void Update()
    {
        ProcesarMovimiento();
        ProcesarSalto();   
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
            rigidBody.AddForce(Vector2.up*fuerzaSalto, ForceMode2D.Impulse);

        }

    }

    void ProcesarMovimiento()
    {
        //La logica de movimiento 
        float inputMovimiento = Input.GetAxis("Horizontal");
        if ( inputMovimiento != 0f )
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
         
        rigidBody. velocity = new Vector2(inputMovimiento*velocidad, rigidBody.velocity.y); 
        
        GestionarOrientacion(inputMovimiento);
    }

    void GestionarOrientacion(float inputMovimiento)
    {
        /*Si se cumole*/
        if( (mirandoDerecha = true && inputMovimiento < 0) || (mirandoDerecha = false && inputMovimiento > 0) )
        {
            //Ejecutar volteo
            mirandoDerecha = !mirandoDerecha;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }   
    }


}
