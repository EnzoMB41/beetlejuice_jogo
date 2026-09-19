using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public float jump = 10f;
    public float speedCorrendo = 6f;
    public float speed = 3f;
    public Transform cam;

    public bool ground = true;

    public float mouseSensitivity = 400f;

    public bool estaCorrendo = false;


    Rigidbody rb;
    float xRotation = 0f;


    void Start()
    {           
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80, 80);
        cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);// rotacao da camera

        if (Input.GetKeyDown(KeyCode.Space) && ground )
        {
            rb.AddForce(Vector3.up * jump , ForceMode.Impulse);
             ground = false;

            Debug.Log("pulo");

        }
        estaCorrendo = Input.GetKey(KeyCode.LeftShift); // checa se ta segurando Shift

    }

    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");//e o comando da unity para pegar as teclas de movimentaçao padrao ( A e D)
        float z = Input.GetAxis("Vertical");//( W e S)

        Vector3 move = (transform.right * x + transform.forward * z).normalized;

        float velocidadeAtual = estaCorrendo ? speedCorrendo : speed; // escolhe a velocidade certa

        Vector3 newVelocity = move * velocidadeAtual;
        newVelocity.y = rb.linearVelocity.y;

        rb.linearVelocity = newVelocity;


    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            ground = true;
        }
    }

}
