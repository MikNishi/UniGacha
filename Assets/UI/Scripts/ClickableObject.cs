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

    public GameObject targetPanel; // Используется, если нужно показывать/скрывать панель

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
                Debug.Log("Запуск мини-игры");
                // Загрузить сцену или активировать объект
                break;
            case ActionType.OpenGacha:
                Debug.Log("Открыть гачу");
                break;
            case ActionType.Custom:
                Debug.Log("Пользовательская логика");
                break;
        }
    }
}
     
