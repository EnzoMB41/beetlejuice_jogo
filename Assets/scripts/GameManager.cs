using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // acesso fácil de qualquer script 

    public int lives = 3;

    bool isGameOver = false; 

    void Awake()// o awake ele a prioridade maxima quando carrega o jogo, ele carrega primeiro que o start. resumindo ele e importante k
    {
        
        if (instance == null)
        {
            instance = this;// faz que o GameManager possa ser acessado sem precisar arrastar nada no Inspector dos outros scripts
            DontDestroyOnLoad(gameObject); // sobrevive entre reinícios de cena e manter vidass
         
        }
        else
        {
            Destroy(gameObject); // vai evita duplicar o GameManager
        }
    }
    

    public void PlayerDied()
    {
        if (isGameOver) return; // se já acabou o jogo, ignora qualquer nova morte

        lives--;//perde 1 vidas 
        Debug.Log("Vidas restantes: " + lives);

        if (lives <= 0)// simples mn, se tem vida reseta a fase se nao perdeu k 
        {
            GameOver();
        }
        else
        {
            RestartLevel();
        }
    }

    void RestartLevel()
    {
        SceneManager.LoadScene("jogo");
    }

    void GameOver()
    {
        isGameOver = true;
        Debug.Log("GAME OVER brooooooo");// aqui depois vai mostrar a tela de Game Over, tem que fazer ainda :( 
   

        SceneManager.LoadScene("GameOverScene");


    }
}