using UnityEngine;

public class PortaFinal : MonoBehaviour
{

    public bool TemAzul = false;
    public bool TemVermelha = false;
    public bool TemVerde = false;

    public VictoryTrigger victoryTrigger;


    public void ColocarChave(TipoChave.Cor corRecebida)
    {
        switch (corRecebida)
        {
            case TipoChave.Cor.vermelha:
            TemVermelha = true;
            Debug.Log("Chave vermelha colocada");
            break;

        case TipoChave.Cor.Azul:
            TemAzul = true;
            Debug.Log("Chave azul colocada");
            break;

        case TipoChave.Cor.Verde:
            TemVerde = true;
            Debug.Log("Chave verde colocada");
            break;
        }

        VerificarVitoria();
    }

    void VerificarVitoria()
    {
        if (TemVermelha && TemAzul && TemVerde)
        {
            Debug.Log("TODAS AS CHAVES COLOCADAS! Porta liberada! eba");
            victoryTrigger.IniciarVitoria();
            // aqui vai liberar a porta e vc ganha!
        }
    }
}
