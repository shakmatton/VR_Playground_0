using System;
using MyScripts;
using UnityEngine;

// Colisão do PrefabDestroyer ativa método OnBulletCollide do SpaceBullet.cs. 
// Esse método, por sua vez, chama diretamente o método HandleBulletScore do Singleton ScoreManager.cs.

// As funções do SpaceBullet são: fazer a movimentação do tiro da nave na tela; e avisar o ScoreManager.cs sobre a colisão ocorrida no PrefabDestroyer.cs.
// Dentro do prefab do SpaceBullet, há 2 scripts nele "empacotados" (PrefabDestroyer.cs e SpaceBullet.cs). Por isso, eles podem conversar entre si diretamente.

public class SpaceBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 1.5f;                          // velocidade do tiro
    [SerializeField] private float timeLimit = 3f;                              // tempo de existência do tiro
    [SerializeField] private PrefabDestroyer prefabDestroyer;
    
    private void Awake()
    {
        // Fallback de segurança: se o campo não foi arrastado no Inspector,
        // tenta pegar o componente automaticamente (funciona pois os dois scripts
        // ficam no mesmo GameObject dentro do prefab).
        if (prefabDestroyer == null)
        {
            prefabDestroyer = GetComponent<PrefabDestroyer>();
        }

        if (prefabDestroyer == null)
        {
            Debug.LogError($"SpaceBullet em '{gameObject.name}': PrefabDestroyer não encontrado nem arrastado no Inspector nem presente no mesmo GameObject.");
        }
    }

    private void Start()
    {
        prefabDestroyer.OnCollide += OnBulletCollide;                           // inscrição de método do prefab SpaceBullet no evento OnCollide de PrefabDestroyer.cs
        
        Destroy(gameObject, timeLimit);                                         // certo tempo após o início da vida do tiro, ele é destruído 
                                                                                // não confundir com o método do ciclo de vida do Unity chamado "OnDestroy()" 
                                                                                // OnDestroy() ocorre só quando o objeto deixa de existir (diferente do uso explícito "Destroy" aqui)
    }

    public void OnBulletCollide()                                               // método que se inscreve no OnCollide do prefabDestroyer
    {
        ScoreManager.Instance.HandleBulletScore();                              // chama instância do Singleton ScoreManger.cs aqui
    }
    
    
    private void Update()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;          // tiro se move pra frente
    }

    private void OnDestroy()
    {
        prefabDestroyer.OnCollide -= OnBulletCollide;
    }
}



/*
using System;
using MyScripts;
using UnityEngine;

// Colisão do PrefabDestroyer ativa método OnBulletCollide do SpaceBullet.cs. 
// Esse método, por sua vez, chama diretamente o método HandleBulletScore do Singleton ScoreManager.cs.

// As funções do SpaceBullet são: fazer a movimentação do tiro da nave na tela; e avisar o ScoreManager.cs sobre a colisão ocorrida no PrefabDestroyer.cs.
// Dentro do prefab do SpaceBullet, há 2 scripts nele "empacotados" (PrefabDestroyer.cs e SpaceBullet.cs). Por isso, eles podem conversar entre si diretamente.

public class SpaceBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 1.5f;                          // velocidade do tiro
    [SerializeField] private float timeLimit = 3f;                              // tempo de existência do tiro
    [SerializeField] private PrefabDestroyer prefabDestroyer;
    
    private void Start()
    {
        prefabDestroyer.OnCollide += OnBulletCollide;                           // inscrição de método do prefab SpaceBullet no evento OnCollide de PrefabDestroyer.cs
        
        Destroy(gameObject, timeLimit);                                         // certo tempo após o início da vida do tiro, ele é destruído 
                                                                                // não confundir com o método do ciclo de vida do Unity chamado "OnDestroy()" 
                                                                                // OnDestroy() ocorre só quando o objeto deixa de existir (diferente do uso explícito "Destroy" aqui)
    }

    public void OnBulletCollide()                                               // método que se inscreve no OnCollide do prefabDestroyer
    {
        ScoreManager.Instance.HandleBulletScore();                              // chama instância do Singleton ScoreManger.cs aqui
    }
    
    
    private void Update()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;          // tiro se move pra frente
    }

    // private void OnDestroy()
    // {
    //     prefabDestroyer.OnCollide -= OnBulletCollide;
    // }
}

*/


