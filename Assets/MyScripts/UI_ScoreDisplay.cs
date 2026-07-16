using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    
    private void Start()
    {
        ScoreManager.Instance.OnScoreChanged += UpdateScreenScore;              // atualiza tela de pontuação 
    }
    
    private void UpdateScreenScore(int newScore)                                // tela de pontuação atualizada aqui
    {
        scoreText.text = newScore.ToString();
    }

    private void OnDestroy()                                                    // caso a instância do ScoreManager exista, fazer um checkout do método UpdateScreenScore
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged -= UpdateScreenScore;
        }
    }
}


// using System.Collections;
// using TMPro;
// using UnityEngine;
//
// // verificar o porquê de funcionar ok na 1ª vez, depois não funcionar direito da 2ª em diante...
//
// public class UI_ScoreDisplay : MonoBehaviour
// {
//     [SerializeField] private TextMeshProUGUI scoreText;
//
//     private Coroutine _subscribeRoutine;
//
//     private void OnEnable()
//     {
//         _subscribeRoutine = StartCoroutine(SubscribeWhenReady());
//     }
//
//     private IEnumerator SubscribeWhenReady()
//     {
//         // espera, quadro a quadro, até o Singleton estar pronto
//         while (ScoreManager.Instance == null)
//         {
//             yield return null;
//         }
//
//         ScoreManager.Instance.OnScoreChanged += UpdateScreenScore;
//         UpdateScreenScore(0); // garante que a tela comece mostrando "0"
//     }
//
//     private void UpdateScreenScore(int newScore)
//     {
//         scoreText.text = newScore.ToString();
//     }
//
//     private void OnDisable()
//     {
//         if (_subscribeRoutine != null)
//         {
//             StopCoroutine(_subscribeRoutine);
//             _subscribeRoutine = null;
//         }
//
//         if (ScoreManager.Instance != null)
//         {
//             ScoreManager.Instance.OnScoreChanged -= UpdateScreenScore;
//         }
//     }
// }

/*
using System.Collections;
using TMPro;
using UnityEngine;

// Esse script atualiza na tela a UI que cuida do score.
// Ele se inscreve no evento OnScoreChanged de ScoreManager e atualiza o texto na tela sempre que o score mudar.

public class UI_ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;      // arrastar no Inspector o objeto de texto (TMP) que mostra o score
    
    public void SubscribeToScoreUpdates()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged += UpdateScreenScore;
            UpdateScreenScore(0);                                               // garante que a tela comece mostrando "0"
        }
        else
        {
            Debug.LogWarning("UI_ScoreDisplay: ScoreManager.Instance ainda não existe na cena.");
        }
    }
    
    private void OnEnable()
    {
        SubscribeToScoreUpdates();
    }

    private void UpdateScreenScore(int newScore)
    {
        scoreText.text = newScore.ToString();
    }
    

    private void OnDisable()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged -= UpdateScreenScore;
        }
    }

    private void OnDestroy()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged -= UpdateScreenScore;
        }
    }
}


/*

using UnityEngine;

// Esse script deve atualizar na tela a UI que cuida do score.
// Ela receberá o evento ChangeScore de ScoreManager e irá fazer a atualização an tela.

namespace MyScripts
{
    public class UI_ScoreDisplay : MonoBehaviour
    {
 
        [SerializeField] private int _newScore = 0;
        
        void Start()
        {
            ScoreManager.ChangeScore(_newScore) += ChangeScore;
        }

        public int ChangeScore(int _newScore)
        {
            
        }

        
        void Update()
        {
        
        }
    }
}


*/
