using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 60.0f;
    public float panBorderThickness = 10.0f;

    public float scrollSpeed = 5.0f;
    public float minY = 20.0f;
    public float maxY = 80.0f;

    private void Update()
    {
        if (GameManager.gameIsOver)
        {
            this.enabled = false;   // 게임오버되면 스크립트 비활성화.
            return;
        }

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.x = Mathf.Clamp(pos.x, -10.0f, 80.0f);
        pos.z = Mathf.Clamp(pos.z, -40.0f, 40.0f);
        transform.position = pos;
    }
}
