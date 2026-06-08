using System.Collections.Generic;
using UnityEngine;

/* Script é um exemplo de uso de ScriptableObject.
   Pensar no ScriptableObject como um .txt ou .json, que armazena campos que podem ser modificados.
   Lembra prefabs, mas ScriptableObjects permitem mais flexibilidade, pois se pode modificar campos. 
   
   Casos de uso: objetos que se repetem, e que precisam ser modificados em algum ponto em detalhe.
   Ex.: games de cartas, como Magic: muitas cartas possuem como padrão campos específicos (pontos de ataque, de defesa e de vida), alteráveis a depender da carta. */ 

namespace MyScripts
{
    // menuName indica o nome que aparece no menu Unity (clique direito)
    // order indica a posição da lista em que aparece o menuName
    [CreateAssetMenu(fileName = "Asteroide", menuName = "MyGame/AsteroidDescription", order = 0)]     
    public class AsteroidDescription : ScriptableObject
    {
        public List<Color> colors = new List<Color>();
    }
}