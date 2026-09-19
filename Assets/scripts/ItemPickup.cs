using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Camera cam;
    public Transform holdPoint;
    public float pickupRange = 5f;
    public float throwForce = 5f;

    GameObject heldItem;
    Rigidbody heldItemRb;


    void Start()
    {

    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))// vai ver se tem algo na mao, se nao tiver pega e se tiver solta :)
        {
            if (heldItem == null)
            {
                TryPickup();
            }
            else
            {
                DropItem();
            }
        }

        if (Input.GetKeyDown(KeyCode.F) && heldItem != null)
        {
            TryEntregarChave();
        }

    }
    void TryPickup()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward); // raycast que vai servir pra pegar o item , vai pegar o centro 
        RaycastHit hit;                                                    //da camera e vai disparar um raio se o rai pegar ele pega o item

        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            if (hit.collider.CompareTag("Item"))
            {
                heldItemRb = hit.collider.GetComponent<Rigidbody>();

                if (heldItemRb == null)
                {
                    Debug.LogWarning(hit.collider.name + " nao tem Rigidbody");
                    return;
                }


                heldItem = hit.collider.gameObject;
                heldItemRb = heldItem.GetComponent<Rigidbody>();

                heldItemRb.isKinematic = true; // vai desliga física enquanto esta na mao

                hit.collider.enabled = false; // desliga o collider enquanto esta na mao


                heldItem.transform.SetParent(holdPoint);
                heldItem.transform.localPosition = Vector3.zero;// aqui pra baixo e pra deixar o item certinho na mao
                heldItem.transform.localRotation = Quaternion.identity;

                Debug.Log("Pegou: " + heldItem.name); // pra ver se ta funcionando
            }
        }
    }

    void DropItem()
    {
        heldItem.transform.SetParent(null);// ele solta o item
        heldItemRb.isKinematic = false; // e liga a física de volta

        heldItem.GetComponent<Collider>().enabled = true;// liga o collider dnv

        heldItem = null; // vai deixar nulo para ser possivel pegar denovo
        heldItemRb = null;
    }
    void TryEntregarChave()
    {
        TipoChave chave = heldItem.GetComponent<TipoChave>();

        if (chave == null) return; // o item na mão não é uma chave, ignora XD

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            if (hit.collider.CompareTag("PortaFinal"))
            {
                PortaFinal porta = hit.collider.GetComponent<PortaFinal>();
                porta.ColocarChave(chave.cor);

                Destroy(heldItem); // remove a chave do jogo, já foi entregue
                heldItem = null;
                heldItemRb = null;

            }
        }
    }
}