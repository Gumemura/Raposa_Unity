using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    public float velocidade;
    public float pulo;
    public float limite_pulo;
    public Sprite sprite_pulo_sobe;
    public Sprite sprite_pulo_cai;
    public LayerMask chao;

    Collider2D colisor_2D;
    BoxCollider2D boxColider;
    Rigidbody2D rb_2d;
    SpriteRenderer m_SpriteRenderer;
    Animator animator;
    RaycastHit2D raycast;
    
    float horizontalInput;
    bool pode_pular;

    private void Start() {
        colisor_2D = GetComponent<Collider2D>();
        boxColider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        rb_2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){
        horizontalInput = Input.GetAxisRaw("Horizontal");
        pode_pular = Input.GetKey(KeyCode.UpArrow);

        Agachadinho();
    }

    void FixedUpdate(){
        //Cria um raio que parte do rigidbody e detecta colisoes
        raycast = Physics2D.BoxCast(colisor_2D.bounds.center, colisor_2D.bounds.size, .0f, Vector2.down, .08f, chao);

        Movimento_X();
        Pulo();
    }

    //Verifica de o objeto esta em contato com o LayerMask "chao". Serve para permitir que o objeto apenas pule quando em contato com o chao
    bool contato_chao(){
        return raycast.collider != null;
    }

    bool Agachadinho(){
        //Ativa/desativa animação de agachamento, sendo que essa não concomita com a de movimento
        animator.SetBool("agacha", Input.GetKey(KeyCode.DownArrow));

        if(animator.GetBool("agacha")){
            animator.SetBool("corre", false);
        }

        //Desativa o colisor retangular para reduzir o tamanho do objeto quando agachado
        boxColider.enabled = !animator.GetBool("agacha");

        return animator.GetBool("agacha");
    }

    bool Movimento_X(){
        int alterador = 1;

        if(Agachadinho() == false){
            //Esse if é importante pois faz com que o objeto se mova apenas e somente apenas nos frames em que o botao estiver sendo apertado
            if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)){
                //seta o booleano 'corre' para true -> ativa animação de correr
                animator.SetBool("corre", true);

                //Rotaciona o Sprite
                if(Input.GetAxis("Horizontal") < 0){
                    alterador = -1;
                    m_SpriteRenderer.flipX = true;
                }else{
                    alterador = 1; 
                    m_SpriteRenderer.flipX = false;
                }

                //Move o objeto
                //transform.Translate(horizontalInput * Time.deltaTime, 0, 0, Space.Self);

                rb_2d.velocity = new Vector2(velocidade * alterador, rb_2d.velocity.y);
            }else{
                //desliga bool 'correr'
                animator.SetBool("corre", false);
            }
        }

        return animator.GetBool("corre");
    }

    void Pulo(){
        //Aumentando a velocidade de subida
        if(pode_pular && contato_chao()){
            rb_2d.velocity = Vector2.up * pulo;
        }

        //Ativa os sprites de queda e subida
        if(rb_2d.velocity.y > limite_pulo || rb_2d.velocity.y < limite_pulo * -1){
            //Desativando o animador para os sprites de subida e queda possam ser ativados
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
