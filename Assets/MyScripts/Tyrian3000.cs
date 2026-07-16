using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


// Adicionei cena Intro no Profile Build (File > Build Profiles).
// VER PORQUE BOTÃO "QUIT" NÃO FUNCIONA!
// VER PORQUE TRANSIÇÃO ENTRE TELAS PARECE "ENGASGADA"...



namespace MyScripts
{
    public class Tyrian3000 : MonoBehaviour                     // trata a interação dos botões das cenas "Intro" e "Tyrian3000" (ideia comum: dar "Play/Retry" no jogo).
    {
        [SerializeField] private Button playButton;           // arrastar botão Play ou Retry
        [SerializeField] private Button quitButton;                  // arrastar botões Quit (do menu da cena "Intro") para sair do jogo
    
        private void Start()
        {
            playButton.onClick.AddListener(PlayGame);    // ocorre o binding do botão de Play/Retry do jogo ao método PlayRetryGame
                                                                     // na cena "Intro", botão PlayButton arrastado ao campo playOrRetryButton age como "botão Play/Retry" de fato.
                                                                     // já na cena principal do jogo, botão RetryButton arrastado ao campo playOrRetryButton age como "botão Replay".
            quitButton.onClick.AddListener(QuitGame);
            
            
                /*if (playOrRetryButton != null)
                    playOrRetryButton.onClick.AddListener(PlayRetryGame);

                if (returnButton != null)
                    returnButton.onClick.AddListener(ReturnGame);

                if (quitButton != null)
                    quitButton.onClick.AddListener(QuitGame);*/
            
        }
    
        public void PlayGame()                                     
        {
            UI_Manager.Instance.HideAllScreens();                    // método PlayRetryGame acessa o método público HideAllScreens, limpando todas as telas de UI do jogo. 
            SceneManager.LoadScene("Tyrian 3000");                   // faz o loading da cena Tyrian3000 (a nave "nasce" aqui).
        }
        
        
        private void QuitGame()
        {
            Debug.Log("Jogo encerrado agora...");

            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;   // simula "fechar o jogo" dentro do Editor
            #else
                Application.Quit();                                 // funciona de verdade só no build
            #endif
        }
    }
}
