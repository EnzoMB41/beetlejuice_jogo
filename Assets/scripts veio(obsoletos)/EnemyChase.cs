using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour
{
    public Transform player;
    public float updateRate = 0.2f; // a cada quanto tempo recalcula o caminho do bixo

    NavMeshAgent agent;
    float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        if (timer >= updateRate)
        {
            timer = 0f;
            agent.SetDestination(player.position);// aqui vai pegar o player como destino e vai seguir >:)
        }
    }
}
