using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{

    public Camera cam;
    public Image crosshairImage;
    public float interactRange = 5f;

    public Sprite normalCrosshair; // sprite padrão (ponto, cruz, etc)
    public Sprite handCrosshair;   // sprite da mão

    public Vector2 normalSize = new Vector2(12f, 12f);// o tamanho da mira normal
    public Vector2 handSize = new Vector2(30f, 30f);// o tamanho da mira quando ta mirando em um item

    RectTransform crosshairRect;

    void Start()
    {
        crosshairRect = crosshairImage.GetComponent<RectTransform>();
    }


    void Update()
    {
        //vai usar o ray para detectar se e um item e se for fica amarela pra mostar que e um item
        ////se tiver mirando pra algo que nao e um item fica branco mesmo k
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange))
        {
            if (hit.collider.CompareTag("Item"))
            {
                crosshairImage.sprite = handCrosshair;//fica na imagem de mao

                crosshairRect.sizeDelta = handSize;// fica maior pra ver a mao direito

                return;

            }
        }
        if (Physics.Raycast(ray, out hit, interactRange))
        {
            if (hit.collider.CompareTag("PortaFinal"))
            {
                crosshairImage.sprite = handCrosshair;//fica na imagem de mao

                crosshairRect.sizeDelta = handSize;// fica maior pra ver a mao direito

                return;

            }
        }

        crosshairImage.sprite = normalCrosshair; // vai deixar constantemente branca
        crosshairRect.sizeDelta = normalSize; // vai ficar no tamanho normal
    }
}
