using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MyScripts
{
    public class Tyrian3000 : MonoBehaviour                   // trata a interação dos botões das cenas "Intro" e "Tyrian3000" (ideia comum: dar "Play/Retry" no jogo).
    {
        [SerializeField] private Button playButton;           // arrastar botão Play
        [SerializeField] private Button quitButton;           // arrastar botão Quit (do menu da cena "Intro") para sair do jogo
    
        private void Start()
        {
            playButton.onClick.AddListener(PlayGame);         // ocorre o binding do botão de Play do jogo ao método PlayGame
                                                              // na cena "Intro", botão PlayButton arrastado ao campo playButton age como "botão Play" de fato.
            quitButton.onClick.AddListener(QuitGame);
            
            /*if (playOrRetryButton != null)
                playOrRetryButton.onClick.AddListener(PlayRetryGame);

            if (returnButton != null)
                returnButton.onClick.AddListener(ReturnGame);

            if (quitButton != null)
                quitButton.onClick.AddListener(QuitGame);*/
        }
    
        private void PlayGame()                                     
        {
            UI_Manager.Instance.HideAllScreens();                  // método PlayGame acessa o método público HideAllScreens, limpando todas as telas de UI do jogo. 
            SceneManager.LoadScene("Tyrian 3000");                 // faz o loading da cena Tyrian3000 (a nave "nasce" aqui).
        }
        
        private void QuitGame()                                    // sai do jogo (via editor ou via aplicação/build final) 
        {
            Debug.Log("Jogo encerrado agora...");

            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;   // simula "fechar o jogo" dentro do Editor
            #else
                Application.Quit();                                // funciona de verdade só no build
            #endif
        }
    }
}
