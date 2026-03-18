using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace MyScripts
{
    public class CustomCube : XRBaseInteractable    // exemplo do objeto do cubo customizado do Lyncon, que herda de XRBaseInteractable (criamos um Interactable customizado)
                                                    // ideia: praticar restrição de movimento do cubo
    {
        private float initialZ;                 // salva a posição inicial no eixo Z
        public float range = 1f;                // deslocamento
        protected override void Awake()         // ver explicação sobre modo de uso abaixo
        {
            base.Awake();
            
            /* Base Class
             
            public class BaseClass : MonoBehaviour {
                protected virtual void Awake() {  ...  } }

            // Derived Class
              
            public class DerivedClass : BaseClass {
                protected override void Awake() {
                    base.Awake(); // Calls the code in BaseClass.Awake()     
                    ...; } }                                                               */
            
            initialZ = transform.position.z;    // salva a posição do gameObject aqui
        }

        // futuro método de Grab aqui...
        protected override void OnSelectEntered(SelectEnterEventArgs args)                   
        {
            base.OnSelectEntered(args);  
            // continuar depois...
        }

        private void LateUpdate()                       // LateUpdate usado para interação sem física (para física, melhor usar Update)
        {
            if (interactorsSelecting.Count > 0)         // Se houver um array não nulo de interactors (grab interactor, near-far interactor, etc...)
            {
                Transform attachTransform = firstInteractorSelecting.transform;     // attachTransform recebe o Transform (Position / Rotation / Scale) do primeiro elemento do array
                // Transform attachTransform = firstInteractorSelecting.GetAttachTransform(this);    // attachTransform do gameObject cubo busca Transform do 1º Interactor selecionado

                // Clamp automático:
                // Mathf.Clamp();
                
                // Clamp manual:
                
                /*float newZ =  attachTransform.position.z;       // newZ guarda posição atual do eixo Z do attachTransform
                if (newZ < initialZ)
                {
                    newZ = initialZ;
                } else if (newZ > initialZ + range)
                {
                    newZ = initialZ + range;
                }*/
                
                // Mudamos o valor da posição do objeto do cubo customizado...  Mathf.Clamp(Valor passado, limite mín, limite máx)  
                transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(attachTransform.position.z, initialZ, initialZ+range));
            }
        }
    }
}