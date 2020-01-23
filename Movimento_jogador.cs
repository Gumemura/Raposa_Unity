using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento_jogador : MonoBehaviour
{
	public float 		velocidade;
	public float		aceleracao_pulo;
    public LayerMask	chao_mask;

    public Sprite 		sprite_pulo_sobe;
    public Sprite 		sprite_pulo_cai;

    Animator 			animador_jogador;
    BoxCollider2D		box_collider_jogador;
    CircleCollider2D	circle_colider_2d;
    RaycastHit2D 		capsule_cast;
    Rigidbody2D 		rb_2d;
    SpriteRenderer 		sr_jogador;

    //Exemplo de declaração de uma classe
    /*public class Teste{
    	public int num;

    	public Teste(int num){
    		this.num = num;
    	}   

    	public int ret_metade(){
    		return num/2; 
    	}
    }

    public Teste a = new Teste(50);*/

    // Start is called before the first frame update
    void Start()
    {
    	animador_jogador		= GetComponent<Animator>();
    	box_collider_jogador	= GetComponent<BoxCollider2D>();
    	circle_colider_2d		= GetComponent<CircleCollider2D>();
    	rb_2d 					= GetComponent<Rigidbody2D>();
    	sr_jogador 				= GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    	pulo();
    }

    void FixedUpdate()
    {
        capsule_cast = Physics2D.CapsuleCast(circle_colider_2d.bounds.center, new Vector2(circle_colider_2d.radius * 2, circle_colider_2d.radius * 2), CapsuleDirection2D.Vertical, 0, Vector2.down, .03f, chao_mask);

    	agacha();

    	if(agacha() == false)
    		Movimento_Horizontal();


    }

    void Movimento_Horizontal(){
    	float input_horizontal = Input.GetAxis("Horizontal");

    	rb_2d.velocity = new Vector2(input_horizontal * velocidade, rb_2d.velocity.y);
    	
    	if(input_horizontal != 0)
    		sr_jogador.flipX = (input_horizontal < 0);

    	animador_jogador.SetBool("pode_correr", (input_horizontal != 0));
    }

    bool agacha(){
    	animador_jogador.SetBool("Agachado", (Input.GetKey(KeyCode.S)));

    	box_collider_jogador.enabled = !animador_jogador.GetBool("Agachado");

    	return animador_jogador.GetBool("Agachado");
    }

    //Não executado
    void freeze_position(){
    	float angulo_jogador_rampa = Vector2.Angle(capsule_cast.normal, Vector2.up);

    	if(angulo_jogador_rampa > 0 && capsule_cast && Input.GetAxis("Horizontal") != 0){
    		rb_2d.constraints  = RigidbodyConstraints2D.FreezePosition;
    	}else{
    		rb_2d.constraints  = RigidbodyConstraints2D.FreezeRotation;
    	}
    }

    void pulo(){
    	animador_jogador.enabled = capsule_cast;

    	if(capsule_cast){
    		if(Input.GetKeyDown(KeyCode.W)){
	    		rb_2d.AddForce(Vector2.up * aceleracao_pulo);
    		}
		}else{
			if(rb_2d.velocity.y > 0){
				sr_jogador.sprite = sprite_pulo_sobe;
			}else if(rb_2d.velocity.y < 0){
				sr_jogador.sprite = sprite_pulo_cai;
			}
		}
    }
}
