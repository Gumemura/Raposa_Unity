using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public Transform jogador;

    Transform posicao_Camera;

    private void Start() {
        posicao_Camera = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        posicao_Camera.position = new Vector3(jogador.position.x, 0, -10); 
    }
}
