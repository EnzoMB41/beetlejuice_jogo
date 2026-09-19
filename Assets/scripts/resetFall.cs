using UnityEngine;

public class resetFall : MonoBehaviour
{
    public float fallLimit = -10f; // altura Y que conta como caiu do mapa
    public Transform respawnPoint; // ponto específico da volta

    Vector3 startPosition;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position; // guarda a posição inicial
    }

   
    void Update()
    {
        if (transform.position.y < fallLimit)
        {
            ResetPosition();
        }
    }
    void ResetPosition()
    {
        Vector3 targetPos;

        if (respawnPoint != null)
        {
            targetPos = respawnPoint.position; // resumindo tudo isso, posso simplesmente fazer ele volta para posicao que comecou o jogo
                                                //mas com o esse codigo todo aqui posso escolher usando um obj vazio ae coloco no script e vai pegar a posicao do obj vazio e vai
        }                                        //fazer o obj ou outra coisa nascer la, como fosse um spanwpoint
        else
        {
            targetPos = startPosition;
        }

        transform.position = targetPos;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
        }

        transform.position = targetPos;

        if (rb != null)//pra nao dar erro :)
        {
            rb.linearVelocity = Vector3.zero; // zera a velocidade pra não continuar caindo e ir pra longe
        }
    }
}
