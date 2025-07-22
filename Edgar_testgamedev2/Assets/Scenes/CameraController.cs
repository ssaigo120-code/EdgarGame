using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 10f, -10f);
    public float followSpeed = 5f;

    [Header("Zoom Settings")]
    public float zoomSpeed = 2f;
    public float minZoomY = 5f;
    public float maxZoomY = 20f;
    public float minZoomZ = -20f;
    public float maxZoomZ = -5f;

    void Update()
    {
        HandleZoom();
    }

    void FixedUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        transform.LookAt(target);
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Abs(scroll) > 0.01f)
        {
            offset.y -= scroll * zoomSpeed;
            offset.z += scroll * zoomSpeed;

            offset.y = Mathf.Clamp(offset.y, minZoomY, maxZoomY);
            offset.z = Mathf.Clamp(offset.z, minZoomZ, maxZoomZ);
        }
    }
}
