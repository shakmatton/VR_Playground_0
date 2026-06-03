using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// fazer comentários sobre esse script depois...


namespace MyScripts
{
    public class ColorChanger: MonoBehaviour
    {
        private Renderer _renderer;       // _renderer : convenção da Microsoft para membros privados...
        private Material _material;       // "renderer sem underline": compilador dá warning (confusão com a declaração "private Renderer renderer")...
        [SerializeField] private List<Color> colors;
        private int _currentColorIndex = 0;
        [SerializeField] private float frequency;
        
        private void Start()
        {
            /* color = GetComponent<Renderer>().material.color;
             
             A forma de fazer acima é equivalente à forma abaixo. Porém, a vantagem da forma abaixo é a seguinte: 
             
             renderer.material (diferente de renderer.sharedMaterial) cria uma cópia do material na memória. 
             Se você chamar GetComponent<Renderer>().material toda vez, você cria uma nova cópia a cada chamada, vazando memória. 
             Guardando em material uma única vez no Start, você reutiliza a mesma instância.                                            */ 
            
            _renderer = GetComponent<Renderer>();
            _material = _renderer.material;
            if (colors.Count > 0)
            {
                StartCoroutine(ChangeColorRoutine());
            }
        }

        private IEnumerator ChangeColorRoutine()
        {
            while (true)
            {
                Debug.Log(_currentColorIndex);
                _material.color = colors[_currentColorIndex];
                _currentColorIndex = (++_currentColorIndex) % colors.Count;
                Debug.Log(_currentColorIndex);
                yield return new WaitForSeconds(frequency);
            }
        }
    }
}