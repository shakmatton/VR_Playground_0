using UnityEngine;
using UnityEngine.SceneManagement;

// Script trata o caso dos botões de "Retry" e "Return" (os outros botões Play e Quit são tratados no script Tyrian3000) 
// Botões Retry e Return foram configurados diretamente no Unity, em vez de via código puro (ver Inspector, na parte das funções "onClick")

namespace MyScripts
{
    public class SceneLoader : MonoBehaviour
    {
        public void LoadIntroScene()                                 // esconde as telas, ativa tela de Intro e muda para a cena de Intro 
        {
            UI_Manager.Instance.ShowIntro();
            SceneManager.LoadScene("Intro");
        }
        
        public void LoadTyrian3000Scene()                            // esconde as telas e muda para a cena do jogo Tyrian 3000
        {
            UI_Manager.Instance.HideAllScreens();                    // método PlayRetryGame acessa o método público HideAllScreens, limpando todas as telas de UI do jogo. 
            SceneManager.LoadScene("Tyrian 3000");                   // faz o loading da cena Tyrian3000 (a nave "nasce" aqui).
        }
        
        public void QuitGame()
        {
            // Debug.Log("Jogo encerrado agora...");

            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;     // simula "fechar o jogo" dentro do Editor
            #else
                Application.Quit();                                  // funciona de verdade só no build
            #endif
        }
    }
}