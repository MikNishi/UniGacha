using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target; // игрок
    public Vector3 offset = new Vector3(0, 0, -10); // обязательно Z = -10 для 2D-камеры

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }
}
