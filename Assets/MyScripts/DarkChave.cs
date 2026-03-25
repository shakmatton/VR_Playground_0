using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DarkChave : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f;
    
    // private Transform transform;
    
    /* (sobre a linha comentada acima:)
     *
     * private Transform transform; foi declarada como campo da classe, o que sobrescreve (shadowing) a propriedade transform que o MonoBehaviour já fornece nativamente.
     * Como a linha de atribuição no Start() tinha sido comentada, esse campo tinha ficado como null.
     * 
     * Regra geral no Unity: transform, gameObject, name e tag são propriedades herdadas de MonoBehaviour.
     * Então, nunca é preciso declarar nem fazer GetComponent<Transform>().     
     * Declarar um campo com o mesmo nome cria um conflito silencioso que só aparece em runtime.
     */
    
    private BoxCollider boxCollider;
    private Vector3 worldCenter;
    // private XRGrabInteractable grabInteractable;
    
    void Start()
    {
        // transform = GetComponent<Transform>();
        boxCollider = GetComponent<BoxCollider>();
        // grabInteractable = GetComponent<XRGrabInteractable>();
    }
    
    void Update()
    {
        // Converte o centro local do collider para posição no mundo
        Vector3 worldCenter = transform.TransformPoint(boxCollider.center);
        
        /* O boxCollider.center é um Vector3 que representa o centro do Box Collider em coordenadas locais do objeto.
           "Local" significa: relativo ao próprio objeto, como se ele fosse a origem do mundo.
           
           O problema é que RotateAround precisa de um ponto em coordenadas de mundo — o espaço absoluto da cena. 
           Se a chave está em (10, 5, 0) na cena, o centro local dela precisa ser somado a essa posição para fazer sentido no mundo.
           TransformPoint faz exatamente essa conversão: pega um ponto local e traduz para o espaço de mundo, levando em conta posição, rotação e escala do objeto.
        */
        
        // Gira em torno desse ponto central, no eixo Z do próprio objeto
        transform.RotateAround(worldCenter, transform.forward, rotationSpeed * Time.deltaTime);
        
        /*
         * Por que transform.forward e não Vector3.forward (mais abaixo)?
           Porque transform.forward respeita o eixo Z local do objeto (Space.Self), enquanto Vector3.forward usaria o eixo Z do mundo. 
           Para uma chave "deitada" no ar girando sobre si mesma, o eixo local é o correto.

           Por que Time.deltaTime? Sem ele, a velocidade de rotação depende do framerate — em 120fps ela giraria o dobro do que em 60fps. 
           Com deltaTime, é possível garantir graus por segundo constantes.
         * 
         */
        
        // transform.Rotate(new Vector3(0, 0, 1));
        // transform.Rotate(0, 0, 1, Space.Self);
    }
}