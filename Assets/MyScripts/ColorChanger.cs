using System;
using UnityEngine;

namespace MyScripts
{
    public class ColorChanger: MonoBehaviour
    {
        private Renderer _renderer;       // _renderer : convenção da Microsoft para membros privados...
        private Material _material;       // "renderer sem underline": compilador dá warning (confusão com a declaração "private Renderer renderer")...
        private Color _color;             // Ao declarar "private Renderer renderer", você esconde esse membro herdado com o seu (compilador avisa pois isso raramente é intencional).
        
        private void Start()
        {
            /* color = GetComponent<Renderer>().material.color;
             
             A forma de fazer acima é equivalente à forma abaixo. Porém, a vantagem da forma abaixo é a seguinte: 
             
             renderer.material (diferente de renderer.sharedMaterial) cria uma cópia do material na memória. 
             Se você chamar GetComponent<Renderer>().material toda vez, você cria uma nova cópia a cada chamada, vazando memória. 
             Guardando em material uma única vez no Start, você reutiliza a mesma instância.                                            */ 
            
            _renderer = GetComponent<Renderer>();
            _material = _renderer.material;               // cria instância de material exclusiva
            _color = _material.color;

            Debug.Log(_color);
        }
    }
}