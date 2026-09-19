using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void jogar()
    {
       SceneManager.LoadScene("jogo");

    }
    public void SairDoJogo()
    {
        Debug.Log("saindooo");
        Application.Quit();
    }
}
