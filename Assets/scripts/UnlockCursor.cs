using UnityEngine;

public class UnlockCursor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;//vai colocar o cursor no gamer over
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
