using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class ClickableObject : MonoBehaviour, IPointerClickHandler
{
    public enum ActionType
    {
        OpenMenu,
        StartRoguelike,
        OpenGacha,
        Custom
    }

    public ActionType action;

    public GameObject targetPanel; // ������������, ���� ����� ����������/�������� ������

    public void Start()
    {
        if (targetPanel != null)
            targetPanel.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (action)
        {
            case ActionType.OpenMenu:
                if (targetPanel != null)
                    targetPanel.SetActive(!targetPanel.activeSelf);
                break;
            case ActionType.StartRoguelike:
                Debug.Log("������ ����-����");
                // ��������� ����� ��� ������������ ������
                break;
            case ActionType.OpenGacha:
                Debug.Log("������� ����");
                break;
            case ActionType.Custom:
                Debug.Log("���������������� ������");
                break;
        }
    }
}
     
