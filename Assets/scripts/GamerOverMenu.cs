using UnityEngine;
using UnityEngine.SceneManagement;

public class GamerOverMenu : MonoBehaviour
{
   
     public void reniciarJogo()
    {
        if (GameManager.instance != null)
        {
            Destroy(GameManager.instance.gameObject);
        }
        SceneManager.LoadScene("jogo");
    }

    public void SairJogo()
    {
        if (GameManager.instance != null)
        {
            Destroy(GameManager.instance.gameObject);
        }
        SceneManager.LoadScene("MenuInicial");
    }



}
