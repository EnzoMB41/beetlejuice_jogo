using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryTrigger : MonoBehaviour
{
    public Image fadeScreen;
    public AudioSource passosAudio;
    public float tempoDeEspera = 4.5f;

    public void IniciarVitoria()
    {
        StartCoroutine(SequenciaVitoria());
    }
    System.Collections.IEnumerator SequenciaVitoria()
    {
        Color c = fadeScreen.color;
        c.a = 1f;
        fadeScreen.color = c;

        if (passosAudio != null)
        {
            passosAudio.Play();
        }

        yield return new WaitForSeconds(tempoDeEspera);

        SceneManager.LoadScene("VictoryScene");
    }








}
