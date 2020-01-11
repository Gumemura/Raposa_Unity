using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulo : MonoBehaviour
{
    Rigidbody2D rb_2d;
    public float pulo;

    public LayerMask plataforma_Mascara;
    private BoxCollider2D box_colider_2d;


    // Update is called once per frame
    private bool Ta_na_plataforma(){
        RaycastHit2D rc2d = Physics2D.BoxCast(box_colider_2d.bounds.center, box_colider_2d.bounds.size, 0, Vector2.down, .1f, plataforma_Mascara);
        Debug.Log(rc2d.collider);
        Debug.Log("oi");
        return rc2d.collider != null;
    }

    private void Start() {
        rb_2d = GetComponent<Rigidbody2D>();    
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) && Ta_na_plataforma()){
            rb_2d.velocity = Vector2.up * pulo;
        }
    }
}
