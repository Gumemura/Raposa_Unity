using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    public float velocidade;
    public float pulo;
    public Sprite sprite_pulo_sobe;
    public Sprite sprite_pulo_cai;

    Rigidbody2D rb_2d;
    SpriteRenderer m_SpriteRenderer;
    Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        rb_2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){
        //get the Input from Horizontal axis
        float horizontalInput = Input.GetAxis("Horizontal");
        
        //Seta a velocidade de alteração do vector 2d; tranforma a velocidade de float para int (movimentação constante sem aceleração)
        if(horizontalInput < 0){
            horizontalInput = -1 * velocidade;
        }else if(horizontalInput > 0){
            horizontalInput = 1 * velocidade;
        }

        //Ativa/desativa animação de agachamento, sendo que essa não concomita com a de movimento
        animator.SetBool("agacha", Input.GetKey(KeyCode.DownArrow));
        if(animator.GetBool("agacha") == true){
            animator.SetBool("corre", false);
        }

        if(Input.GetKey(KeyCode.DownArrow) == false){
            //Esse if é importante pois faz com que o objeto se mova apenas e somente apenas nos frames em que o botao estiver sendo apertado
            if(Input.GetKey(KeyCode.RightArrow) == true || Input.GetKey(KeyCode.LeftArrow) == true){
                //seta o booleano 'corre' para true, q por sua vez ativa a animacao de correr
                animator.SetBool("corre", true);

                //Rotaciona o Sprite
                if(Input.GetAxis("Horizontal") < 0){
                    m_SpriteRenderer.flipX = true;
                }else{
                    m_SpriteRenderer.flipX = false;
                }

                //Move o objeto
                transform.position += new Vector3(horizontalInput * Time.deltaTime, 0, 0);
            }else{
                //desliga a animação de correr
                animator.SetBool("corre", false);
            }
        }

        animator.SetBool("pula", Input.GetKey(KeyCode.UpArrow));

        if(Input.GetKeyDown(KeyCode.UpArrow)){
            rb_2d.velocity = Vector2.up * pulo;
        }

        if(rb_2d.velocity.y != 0){
            animator.enabled = false;
            if(rb_2d.velocity.y > 0){
                m_SpriteRenderer.sprite = sprite_pulo_sobe;
            }else if(rb_2d.velocity.y < 0){
                m_SpriteRenderer.sprite = sprite_pulo_cai;
            }
        }else{
            animator.enabled = true;
        }
    }
}
