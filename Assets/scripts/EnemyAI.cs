using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform[] pontosPatrulha; // aqui e uma array vai colocar os pontos de patrulha que esta no game
    public float velocidadePatrulha = 2f; // velocidade dele so patrulhando na manha kk
    public float velocidadePerseguicao = 4f;//aqui quando ele te ve

    public Transform player;

    Animator animator;

    [Header("Detecção por Visão")]//cria um cone legal, é a visao do bixo kk
    public float distanciaVisao = 10f;
    public float anguloVisao = 60f;

    [Header("Detecção por Som")]
    public float raioAudicao = 6f;

    [Header("Perder o Rastro")]
    public float tempoParaDesistir = 4f;

    float tempoSemDetectar = 4f;

    Player scriptPlayer;


    NavMeshAgent agent;
    int indicePontoAtual = 0;

    enum Estado { Patrulha, Perseguicao }
    Estado estadoAtual = Estado.Patrulha;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = velocidadePatrulha;

        scriptPlayer = player.GetComponent<Player>();

        animator = GetComponentInChildren<Animator>();

        IrParaProximoPonto();
    }

    // Update is called once per frame
    void Update()
    {
        bool viuPlayer = VerificarVisao();
        bool ouviuPlayer = VerificarSom();

        if (viuPlayer || ouviuPlayer)
        {
            estadoAtual = Estado.Perseguicao;
            tempoSemDetectar = 0f; // reseta o timer, ainda está "no rastro"
        }
        else if (estadoAtual == Estado.Perseguicao)
        {
            tempoSemDetectar += Time.deltaTime;

            if (tempoSemDetectar >= tempoParaDesistir)
            {
                estadoAtual = Estado.Patrulha;
                IrParaProximoPonto(); // já manda ele pro próximo ponto ao voltar
            }
        }

        if (estadoAtual == Estado.Patrulha)
        {
            AtualizarPatrulha();
        }
        else if (estadoAtual == Estado.Perseguicao)
        {
            AtualizarPerseguicao();
        }

        animator.SetBool("estaPerseguindo", estadoAtual == Estado.Perseguicao);
    }

    bool VerificarVisao()
    {
        Vector3 direcaoParaPlayer = (player.position - transform.position).normalized;
        float distancia = Vector3.Distance(transform.position, player.position);

        if (distancia > distanciaVisao) return false;

        float angulo = Vector3.Angle(transform.forward, direcaoParaPlayer);

        if (angulo > anguloVisao / 2f) return false;

        // Confirma que não tem parede no meio
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direcaoParaPlayer, out hit, distanciaVisao))
        {
            if (hit.collider.CompareTag("Player"))
            {
                return true; // viu o player de verdade, sem obstáculo no meio
            }
        }

        return false;
    }
    bool VerificarSom()
    {
        if (scriptPlayer == null) return false;
        if (!scriptPlayer.estaCorrendo) return false;

        float distancia = Vector3.Distance(transform.position, player.position);

        return distancia <= raioAudicao;
    }

    void AtualizarPatrulha()
    {
        agent.speed = velocidadePatrulha;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            IrParaProximoPonto();
        }
    }
    void AtualizarPerseguicao()
    {
        agent.speed = velocidadePerseguicao;
        agent.destination = player.position;
    }

    void IrParaProximoPonto()// esse e sinistro mn tive que pesquisar :(
    {
        if (pontosPatrulha.Length == 0) return;

        agent.destination = pontosPatrulha[indicePontoAtual].position;

        indicePontoAtual = (indicePontoAtual + 1) % pontosPatrulha.Length;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaVisao);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, raioAudicao);


        Vector3 anguloEsquerdo = DirecaoPorAngulo(-anguloVisao / 2f);
        Vector3 anguloDireito = DirecaoPorAngulo(anguloVisao / 2f);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + anguloEsquerdo * distanciaVisao);
        Gizmos.DrawLine(transform.position, transform.position + anguloDireito * distanciaVisao);
    }

    Vector3 DirecaoPorAngulo(float anguloGraus)
    {
        float anguloFinal = transform.eulerAngles.y + anguloGraus;
        return new Vector3(Mathf.Sin(anguloFinal * Mathf.Deg2Rad), 0, Mathf.Cos(anguloFinal * Mathf.Deg2Rad));
    }
}
