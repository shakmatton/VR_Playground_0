using UnityEngine;

namespace MyScripts
{
    public class UI_Manager : MonoBehaviour
    { 
        public static UI_Manager Instance { get; private set; }
        
        // get (puramente escrito assim, desse jeito) significa que qualquer classe pode ler valores de qualquer lugar
        // private set impede que scripts externos a sobrescrevam. Somente a classe UI_Manager pode escrever algo nesse script aqui
        // (note que só ela, e mais ninguém, pode atribuir valores no Awake).
        
        /*
        Instance: variável estática que pertence à classe, não a um objeto. Ela fica "pendurada" na própria classe (não em nenhum objeto específico).                
        Ela guarda uma referência para o único objeto UI_Manager que existe na cena.
        
           UI_Manager.Instance.ShowGameOver();  
                  seria equivalente a 
           minhaReferenciaParaUI_Manager.ShowGameOver();  (se se tem a referência direta)
           
        A diferença é que, com o Singleton, não é preciso arrastar nada no Inspector. A classe guarda o caminho para si mesma.
        O prefab apenas pergunta para a classe UI_Manager onde está a sua instância, e depois chama o método nessa instância.   */
        

        [Header("Telas de UI")]                                                             // deve aparecer no objeto UI_Manager (as telas devem ser arrastadas para cada campo)
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private GameObject victoryScreen;
        [SerializeField] private GameObject introScreen;
        [SerializeField] private GameObject scoreScreen;

        
        private void Awake()                                                                                    
        {
            // Awake() é o lugar certo para inicializar o Singleton porque garante que ele existe antes de qualquer outro script tentar usá-lo.
            // Awake está protegido para escrita por scripts externos (vide private set acima).
         
            /* Padrão Singleton clássico:
             se ainda não existe nenhum UI_Manager registrado, este objeto se registra como a instância oficial.
             DontDestroyOnLoad faz o objeto sobreviver caso haja troca de cena — útil se o UI_Manager precisar persistir entre cenas. */
            
            if (Instance == null)                       // se não há nenhum UI Instance instanciado ainda...
            {
                Instance = this;                           // ... Instance referencia a instância de UI corrente   
                DontDestroyOnLoad(gameObject);             // Protege a instância de UI referenciada, fazendo ela persistir entre cenas, se necessário (útil para múltiplas cenas)
            }
            else
            {
                Destroy(gameObject);                       // NOTE: Destroy(gameObject), e não Destroy(this)!
                /*
                 Se já existe um UI_Manager registrado (por exemplo, foi carregada uma nova cena que também tem um UI_Manager no prefab),
                 este segundo é destruído imediatamente. Isso garante que só existe um.
                   
                 Diferença importante: o Destroy(this) destrói apenas o componente (o script), mas o GameObject continua existindo na cena. 
                 O correto é Destroy(gameObject), que destrói o objeto inteiro.  */
            }
        }
        
        // ── Método interno auxiliar ───────────────────────────────────────────

        public void HideAllScreens()                      // garante que todas as telas estejam desativadas
        {
            gameOverScreen.SetActive(false);
            victoryScreen.SetActive(false);
            introScreen.SetActive(false);
            scoreScreen.SetActive(false);
        }
        
        // ── Métodos públicos que qualquer script pode chamar
        
        public void ShowIntro()                           // public aqui poderia ser private, pois esse método público é chamado dentro do método private Start. 
        {
            HideAllScreens();
            introScreen.SetActive(true);
        }

        public void ShowGameOver()
        {
            HideAllScreens();
            gameOverScreen.SetActive(true);               // SetActive(true/false) ativa/desativa o GameObject na cena, tornando-o visível/invisível. 
        }

        public void ShowVictory()
        {
            HideAllScreens();
            victoryScreen.SetActive(true);
        }

        public void ShowScore()
        {
            scoreScreen.SetActive(true);
        }

    }
}