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

    private void Start() {
        colisor_2D = GetComponent<Collider2D>();
        boxColider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        rb_2d = GetComponent<Rigidbody2D>();
    }

    //Verifica de o objeto esta em contato com o LayerMask chao
    //Serve para permitir que o objeto apenas pule quando em contato com o chao
    private bool contato_chao(){
        RaycastHit2D raycast  = Physics2D.BoxCast(colisor_2D.bounds.center, colisor_2D.bounds.size, .0f, Vector2.down, .1f, chao);
        return raycast.collider != null;
    }

    // Update is called once per frame
    void Update(){
        //get the Input from Horizontal axis

        //Ativa/desativa animação de agachamento, sendo que essa não concomita com a de movimento
        animator.SetBool("agacha", Input.GetKey(KeyCode.DownArrow));

        //Desativa o colisor de cima para reduzir o tamanho do objeto quando agachado
        boxColider.enabled = !Input.GetKey(KeyCode.DownArrow);

        if(animator.GetBool("agacha") == true){
            animator.SetBool("corre", false);
        }

        if(Input.GetKey(KeyCode.DownArrow) == false){
            float horizontalInput = Input.GetAxisRaw("Horizontal");

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

                //rb_2d.MovePosition(new Vector2(transform_jogador.position.x + horizontalInput * Time.deltaTime, transform_jogador.position.y));

                //Move o objeto
                //transform.position += new Vector3(horizontalInput * Time.deltaTime, 0, 0);

                transform.Translate(horizontalInput * Time.deltaTime, 0, 0);
            }else{
                //desliga a animação de correr
                animator.SetBool("corre", false);
            }
        }

        //Aumentando a velocidade de subida
        if(Input.GetKeyDown(KeyCode.UpArrow) && contato_chao()){
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
