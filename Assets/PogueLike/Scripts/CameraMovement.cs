using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target; // �����
    public Vector3 offset = new Vector3(0, 0, -10); // ����������� Z = -10 ��� 2D-������

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }
}
