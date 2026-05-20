using System;
using UnityEngine;

/* Counter dispara evento a ser ouvido por CounterDisplay (que, por sua vez, irá nele se inscrever).
   Counter usa a lógica de incrementar aqui, e repassar o valor incrementado via delegate.  */

public class Counter : MonoBehaviour
{
    private int count = 0;                                  // inicializa count

    // Uma das formas de criar eventos do tipo delegate:
    public delegate void MyEvent(int i);                    // método delegate instaura o tipo MyEvent (com parâmetro i)
    public event MyEvent OnCountChanged;                    // tipo evento a partir do delegate criado acima
    
    /*     
        Outra forma de criar delegate:
            public event Action<int> OnCountChanged;   */
    
    /*  Evitar buscar soluções "hardcoded" como as abaixo (usar [SerializeField], como mostrado em BigButton.cs e CounterDisplay.cs).
    
        BigButton = GameObject.Find("Button");   // hardcoded
        GetComponentInParent() -> gets parent of Counter (BigPushButton)
        GetComponentInChildren() -> gets one of BigPushButton's children
        GetSibling() ???
        BigButton = GetComponentInParent<GameObject>().GetComponentInChildren<GameObject>();  // ???            */
    

    public void Increment()                         // método comum do Counter, chamado por BigButton
    {
        count++;                                    // incrementa count aqui
        
        OnCountChanged?.Invoke(count);              // se OnCountChanged != null, então o método "delegate event" é invocado
                                                    // (OnCountChanged é null quando ninguém se inscreveu nele)
    }
    
    
    
    
    
    
    
    
    /*
    private void OnEnable()     // quando Counter estiver ativo, método RefreshCounter se inscreve no OnCountChanged. 
    { 
        /* método "delegate event" do BigButton.cs --->  public event Action<int> OnCountChanged;
           método RefreshCounter deve se inscrever com a mesma assinatura do método do evento acima;     */                    /*
        
        bigButton.OnCountChanged += RefreshCounter;      // RefreshCounter se inscreveu no OnCountChanged
    }

    private void OnDisable()
    {
        bigButton.OnCountChanged -= RefreshCounter;     
    }

    
    private void RefreshCounter(int count)      // método com mesma "assinatura" do delegate event 
    {
        Debug.Log(count);                       // 
    }
    
    */

}
