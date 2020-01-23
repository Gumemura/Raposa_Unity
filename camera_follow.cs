using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_follow : MonoBehaviour
{
	public Transform 	jogador;
	public bool 		segue_y;

	Transform transform_camera;

    float y;

    // Start is called before the first frame update
    void Start()
    {
        transform_camera = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(segue_y){
        	y = jogador.position.y;
        }else{
        	y = 0;
        }

        transform_camera.position = new Vector3(jogador.position.x, y, -10);
    }
}
