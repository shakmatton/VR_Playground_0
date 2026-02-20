using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace MyScripts
{
    public class Star : MonoBehaviour
    {
        private XRGrabInteractable grabInteractable;
        private Renderer starRenderer;
        private Material starMaterial;
    
        private Color originalColor;
        private Coroutine blinkCoroutine;  // não confundir com threads...!
        
        [SerializeField] private bool superStar = false;
        [SerializeField] private float colorChangeSpeed = 0.25f; // tempo em segundos entre cada mudança de cor
        
        /* =========  AÚDIO ========= */
        
        private AudioSource starSoundSource;                    // o dispositivo "alto-falante"
        [SerializeField] private AudioClip starSoundClip;       // "a música que toca no alto-falante" 
        [SerializeField] private bool starLoop;
        [SerializeField] private float starVolume = 0.8f;           // Varia de 0 a 1.
    
        // Array de cores para piscar (estilo Super Mario)
        private Color[] starColorsArray = new Color[]
        {
            Color.red,
            Color.yellow,
            Color.green,
            Color.cyan,
        };
    
        void Start()
        {
            grabInteractable = GetComponent<XRGrabInteractable>();
            starRenderer = GetComponent<Renderer>();
        
            // Cria uma instância do material para não afetar outros objetos
            starMaterial = starRenderer.material;
        
            // Guarda a cor original (amarelo)
            originalColor = starMaterial.color;
            
            
            /* ========= AÚDIO ========= */

            
            // starSoundSource tenta obter o componente do tipo AudioSource...
            starSoundSource = GetComponent<AudioSource>();
            
            // ... mas, se não conseguir, o componente é adicionado automaticamente
            if (starSoundSource == null)     // caso não exista 
            {
                starSoundSource = gameObject.AddComponent<AudioSource>();   // componente adicionado no objeto Star
            }
            
            // Configurações extras de áudio
            starSoundSource.clip = starSoundClip;
            starSoundSource.loop = starLoop;
            starSoundSource.volume = starVolume;
            starSoundSource.playOnAwake = false;   // não toca automaticamente
            starSoundSource.spatialBlend = 1f;     // 3D sound (1 = totalmente espacial, 0 = 2D) 
        
            // Adiciona listeners para botão de Grab (listeners ficam sempre ativos ao longo do Play, pois sempre precisam detectar ações de Grab/Release)
            grabInteractable.selectEntered.AddListener(OnGrabbed);     // se fosse Trigger, seria algo como [...].activated.AddListener(...)  (checar no Inspector Unity)
            grabInteractable.selectExited.AddListener(OnReleased);
        }

        void OnGrabbed(SelectEnterEventArgs args)   // lembrar de usar esses tipos de parâmetros 
        {
            if (!superStar)    // equivale a "if (superStar == false)"
            {
                superStar = true;
                
                // Toca o som
                if (starSoundSource != null && starSoundClip != null)
                {
                    starSoundSource.Play();
                }
            
                // Inicia o efeito de piscar
                if (blinkCoroutine != null)
                    StopCoroutine(blinkCoroutine);      // pára a Coroutine aqui, por segurança
                
                blinkCoroutine = StartCoroutine(BlinkColors());    // inicia a Coroutine chamada BlinkColors (que é um IEnumerator)
            }
        }
        IEnumerator BlinkColors() 
        // ativado no método OnGrabbed(), IEnumarator serve para iterar sobre coleções (como foreach) e pausar/retomar execução (corrotinas)
        {
            int colorIndex = 0;
        
            while (superStar)        // while (superStar == true)
            {
                // Muda para a próxima cor
                starMaterial.color = starColorsArray[colorIndex];
            
                // Avança para a próxima cor
                colorIndex = (colorIndex + 1) % starColorsArray.Length;
            
                // Aguarda antes da próxima mudança   (yield significa algo como "pause aqui e retorne depois")
                yield return new WaitForSeconds(colorChangeSpeed);   // colorChangeSpeed = 0.25f   ("pausa a cor" por esse tempo, e depois retoma com outra cor)
            }
        }
    
        void OnReleased(SelectExitEventArgs args)
        {
            // parar de piscar quando soltar
            StopBlinking();    // nesse método o som deve parar e a estrela deve voltar ao normal
        }
    
    
        public void StopBlinking()    // ativado no método OnReleased()
        {
            superStar = false;
        
            if (blinkCoroutine != null)
            {
                StopCoroutine(blinkCoroutine);   // finaliza a Coroutine BlinkColors
                blinkCoroutine = null;
            }
            
            // pára o áudio
            if (starSoundSource != null && starSoundSource.isPlaying)
            {
                starSoundSource.Stop();
            }
        
            // Volta para a cor original
            starMaterial.color = originalColor;
        }
    
        void OnDestroy()                            // método que serve como um "dispose" geral
        {
            // Limpa material
            if (starMaterial != null)
                Destroy(starMaterial);
    
            // Finaliza áudio
            if (starSoundSource != null && starSoundSource.isPlaying)
            {
                starSoundSource.Stop();
            }
    
            // Boa prática: Remove listeners (mesmo não sendo crítico)
            if (grabInteractable != null)
            {
                grabInteractable.selectEntered.RemoveListener(OnGrabbed);
                grabInteractable.selectExited.RemoveListener(OnReleased);
            }
        }
    }
}