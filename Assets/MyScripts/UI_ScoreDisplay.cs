using TMPro;
using UnityEngine;

// Esse script atualiza na tela a UI que cuida do score.
// Ele se inscreve no evento OnScoreChanged de ScoreManager e atualiza o texto na tela sempre que o score mudar.

public class UI_ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;      // arrastar no Inspector o objeto de texto (TMP) que mostra o score

    private void Start()
    {
        // Importante: inscrever em Start (não em Awake/OnEnable), pois o Unity garante que
        // todos os Awake() da cena rodam antes de qualquer Start(). Isso evita que este script
        // tente acessar ScoreManager.Instance antes dele existir.
        
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged += UpdateScore;
            UpdateScore(0);                                   // garante que a tela comece mostrando "0"
        }
        else
        {
            Debug.LogWarning("UI_ScoreDisplay: ScoreManager.Instance ainda não existe na cena.");
        }
    }

    private void UpdateScore(int newScore)
    {
        scoreText.text = newScore.ToString();
    }

    private void OnDestroy()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged -= UpdateScore;
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