using UnityEngine;

public class Rotationer : MonoBehaviour
{
    [SerializeField]
    float mouseSpeed = 5f;

    Vector3 currentRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        currentRotation = transform.localEulerAngles;
    }

    void Update()
    {
        Vector2 move = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentRotation += new Vector3(move.y, move.x, 0f) * mouseSpeed * Time.deltaTime;
        currentRotation = new Vector3(Mathf.Clamp(currentRotation.x, -90, 90), currentRotation.y, 0f);
        transform.localEulerAngles = currentRotation;

    }
}
