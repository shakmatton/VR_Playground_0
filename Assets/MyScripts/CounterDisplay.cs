using TMPro;
using UnityEngine;

// CounterDisplay must catch Counter event. 

public class CounterDisplay : MonoBehaviour
{
    [SerializeField] private Counter counter;                   // referência ao Counter.cs (precisa ser arrastado via Inspector)
    private TextMeshProUGUI textMeshProUGUI;                    // Display de pontuação
 
    void Start()      // Start, Awake, Update, OnEnable etc, são métodos do MonoBehaviour lifecycle de Unity. Tudo o que for colocado neles, será rastreado e ativado pelo Unity.
                      // Logo, faz sentido a linha de inscrição de eventos (abaixo) ser chamada (ativada) no Start, em vez de chamada diretamente em outro lugar, fora do lifecycle.
    {
        counter.OnCountChanged += HandleScore;                  // inscrição do método HandleScore no evento OnCountChanged (do objeto counter)
        
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();      // "GetComponent e derivados" só funcionam para componentes dentro de seu próprio gameObject ao qual pertencem.
                                                                // Usar [SerializeField] mais acima para arrastar scripts e pegar referências dos objetos estrangeiros.
    }

    private void HandleScore(int count)                         // método que se inscreve em OnCountChanged?.Invoke(count), do script Counter.cs  
    {
        textMeshProUGUI.text = count.ToString();                // o campo Texto do TMP recebe a conversão de count para string.
    }
}
