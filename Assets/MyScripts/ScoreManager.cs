using MyScripts;
using TMPro;
using UnityEngine;

// fazer o ScoreManager se comunicar com o SpaceBullet
// fazer o SpaceBullet se comunicar com o PrefabDestroyer
// usar inscrição em eventos, singleton, ...  

// repensar sobre a dinâmica do Score UI na tela...

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private SpaceBullet spaceBullet;
    [SerializeField] private TextMeshProUGUI scoreText;

    private int _acumulador = 0;
    
    private void Start()
    {
        spaceBullet.OnCountChanged += HandleBulletScore;
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void HandleBulletScore(int score)
    {
        _acumulador += score;
        scoreText.text = _acumulador.ToString();
        
        if (_acumulador >= 3)
        {
            // UI_Manager.Instance.ShowVictory();
            Debug.Log("Pontuação >= 3: Tela de Vitória aqui!");
        }
    }

    private void OnDestroy()
    {
        spaceBullet.OnCountChanged -= HandleBulletScore;
    }
    
   
}
