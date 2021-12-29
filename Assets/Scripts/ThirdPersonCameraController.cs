using UnityEngine;

//This is a camera script made by Haravin (Daniel Valcour).
//This script is public domain, but credit is appreciated!

[RequireComponent(typeof(Camera))]
public class ThirdPersonCameraController : MonoBehaviour
{

    public float moveSpeed;
    public float shiftAdditionalSpeed;
    public float mouseSensitivity;
    public bool invertMouse;
    public bool autoLockCursor;
    public Transform player;

    private Camera cam;

    void Awake()
    {
        cam = this.gameObject.GetComponent<Camera>();
        this.gameObject.name = "ThirdPersonController";
        Cursor.lockState = (autoLockCursor) ? CursorLockMode.Locked : CursorLockMode.None;
    }

    void Update()
    {
        if (Cursor.lockState == CursorLockMode.None && Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (Cursor.lockState == CursorLockMode.Locked && Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if(Cursor.lockState == CursorLockMode.Locked)
        {
            float y = Input.GetAxis("Mouse Y") * mouseSensitivity * ((invertMouse) ? 1 : -1);
            float x = Input.GetAxis("Mouse X") * mouseSensitivity * ((invertMouse) ? -1 : 1);

            this.gameObject.transform.RotateAround(player.position, player.up, x);
            this.gameObject.transform.RotateAround(player.position, this.gameObject.transform.right, y);

        }
    }
}

