using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyScripts
{
    public class SceneLoader : MonoBehaviour
    {
        public void LoadMainMenuScene()
        {
            UI_Manager.Instance.ShowIntro();
            SceneManager.LoadScene("Intro");
        }
        
        public void LoadGameScene()                                     
        {
            UI_Manager.Instance.HideAllScreens();                    // método PlayRetryGame acessa o método público HideAllScreens, limpando todas as telas de UI do jogo. 
            SceneManager.LoadScene("Tyrian 3000");                   // faz o loading da cena Tyrian3000 (a nave "nasce" aqui).
        }
        
        public void QuitGame()
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