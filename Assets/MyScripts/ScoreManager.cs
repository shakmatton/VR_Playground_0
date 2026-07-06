using System;
using MyScripts;
using UnityEngine;

// Colisão do PrefabDestroyer ativa método OnBulletCollide do SpaceBullet.cs, que chama diretamente o método HandleBulletScore do Singleton ScoreManager.cs.
// HandleBulletScore processa a lógica do incremento e dispara o evento OnScoreChanged, que o UI_ScoreDisplay.cs escuta para se atualizar.

// ScoreManager: Singleton que gerencia a lógica de incremento do score, e também, a sinalização de que a UI responsável pelo score deve ser atualizada.
// SpaceBullet invoca a instância do Singleton diretamente (chamada direta, sem inscrição em evento).

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    // Evento que avisa qualquer UI inscrita (ex: UI_ScoreDisplay) sobre a mudança no score.
    // É esse "Action<int> OnScoreChanged" que faz o papel do "evento ChangeScore" mencionado no comentário original do UI_ScoreDisplay.
    public event Action<int> OnScoreChanged;

    private int _acumulador = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void HandleBulletScore()
    {
        // TODO: se GameOver, resetar jogo e resetar score antes de somar ponto novamente.

        _acumulador++;
        OnScoreChanged?.Invoke(_acumulador);          // avisa a UI (UI_ScoreDisplay) que o score mudou

        // if (_acumulador >= 3)
        // {
        //     UI_Manager.Instance.ShowVictory();
        // }
    }

    public void ResetScore()
    {
        _acumulador = 0;
        OnScoreChanged?.Invoke(_acumulador);
    }
}



/*

using System;
using System.Dynamic;
using MyScripts;
using TMPro;
using UnityEngine;

// Colisão do PrefabDestroyer ativa método OnBulletCollide do SpaceBullet.cs, que chama diretamente o método HandleBulletScore do Singleton ScoreManager.cs.
// HandleBulletScore deve processar a lógica do incremento e avisar sobre a modificação do score para o UI_ScoreDisplay.cs.

// ScoreManager: Singleton que gerencia a lógica de incremento do score, e também, a sinalização de que a UI responsável pelo score deve ser atualizada.
// SpaceBullet deve invocar a instância do Singleton diretamente.

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private SpaceBullet spaceBullet;
    [SerializeField] private UI_ScoreDisplay _uiScoreDisplay;           // trocar isso por referência ao script UI_ScoreDisplay.cs
    
    public static ScoreManager Instance { get; private set; }
    private int _acumulador = 0;
    
    
    // public event Action<int> AddScore; 
    // private int points = 0;
    

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;    
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //UI_Manager.Instance.ShowScore();                                    // score não precisa aparecer na tela de intro (remover o score de lá e manter apenas na cena de jogo).
        //spaceBullet.OnBulletCollide += HandleBulletScore;
        // scorePoints = GetComponent<TextMeshProUGUI>();
    }

    public void HandleBulletScore()
    {
                // escrever lógica para a situação abaixo:
                // if GameOver, reset game e reset score...
        
        
        
        // Debug.Log($"HandleBulletScore recebido! Score: {score}");

        _acumulador++;
        // AddScore?.Invoke(_acumulador);
        
        // scorePoints.text = _acumulador.ToString();
        // ChangeScore?.Invoke(_acumulador);
        
        if (_acumulador >= 3)
        {
            // UI_Manager.Instance.ShowVictory();
            Debug.Log("Pontuação >= 3: Tela de Vitória aqui!");
        }
    }

    public void ChangeScore(int score)
    {
        
    }


    private void OnDestroy()
    {
        // spaceBullet.OnBulletCollide -= HandleBulletScore;
    }
    
   
}

*/