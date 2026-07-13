using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MyScripts
{
    public class IntroTyrian3000 : MonoBehaviour            // esse script existe tanto na cena de Intro como na cena padrão do jogo em si (ideia em comum: dar "Play" no jogo).
    {
        [SerializeField] private Button playButton;         // nosso botão de Play do jogo
    
        void Start()
        {
            playButton.onClick.AddListener(PlayGame);       // ocorre o binding do botão de Play do jogo ao método PlayGame
                                                            // na cena "Intro", botão PlayButton arrastado ao campo playButton age como "botão Play" de fato.
                                                            // já na cena principal do jogo, botão TryAgainButton arrastado ao campo playButton age como "botão Replay".
        }
    
        void PlayGame()                                     
        {
            UI_Manager.Instance.HideAllScreens();        // método PlayGame acessa o método público HideAllScreens, limpando todas as telas de UI do jogo. 
            SceneManager.LoadScene("Tyrian 3000");          // faz o loading da cena principal do jogo
        }
    }
}
