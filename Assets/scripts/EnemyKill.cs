using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyKill : MonoBehaviour
{

    public AudioSource somSusto;
    public float delayAntesDeMorrer = 3f;

    [Header("Jumpscare")]
    public Transform pontoCabeca;
    public float distanciaDoRosto = 1.2f;
    public float velocidadeCamera = 8f;
    public MonoBehaviour[] scriptsParaDesativar;


    bool jaMorreu = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !jaMorreu)
        {
            jaMorreu = true;
            // trava o player o que e vc no jogo
            other.GetComponent<Player>().enabled = false;
            other.GetComponent<ItemPickup>().enabled = false;
            Rigidbody playerRb = other.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                playerRb.linearVelocity = Vector3.zero;
            }

            foreach (var s in scriptsParaDesativar)
            {
                if (s != null) s.enabled = false;
            }

            // trava o inimigo para ele nao sair andando
            EnemyAI ia = GetComponentInParent<EnemyAI>();
            NavMeshAgent agent = GetComponentInParent<NavMeshAgent>();
            Transform inimigoRaiz = ia.transform;

            ia.enabled = false;
            agent.isStopped = true;
            agent.enabled = false;

            Transform cam = Camera.main.transform;

            // coloca o inimigo na frente da câmera e sustus
            Vector3 frente = cam.forward;
            frente.y = 0f;
            frente.Normalize();

            inimigoRaiz.position = new Vector3(
                other.transform.position.x + frente.x * distanciaDoRosto,
                inimigoRaiz.position.y,
                other.transform.position.z + frente.z * distanciaDoRosto);
            inimigoRaiz.rotation = Quaternion.LookRotation(-frente);

            Animator anim = inimigoRaiz.GetComponentInChildren<Animator>();
            anim.SetTrigger("jumpscare");

            if (somSusto != null)
            {
                somSusto.Play();
            }

            StartCoroutine(OlharParaInimigo(cam, inimigoRaiz));
            Invoke(nameof(Morrer), delayAntesDeMorrer);
        }
    }

    IEnumerator OlharParaInimigo(Transform cam, Transform inimigoRaiz)
    {
        float t = 0f;
        while (true)
        {
            Vector3 alvo = pontoCabeca != null
                ? pontoCabeca.position
                : inimigoRaiz.position + Vector3.up * 1.6f;

            Quaternion fim = Quaternion.LookRotation(alvo - cam.position);
            t = Mathf.Min(1f, t + Time.deltaTime * velocidadeCamera);
            cam.rotation = Quaternion.Slerp(cam.rotation, fim, t);
            yield return null;
        }
    }

    void Morrer()
    {
        GameManager.instance.PlayerDied();
    }
}
