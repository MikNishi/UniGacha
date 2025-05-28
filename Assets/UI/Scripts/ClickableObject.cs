using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ClickableObject : MonoBehaviour, IPointerClickHandler
{
    public enum ActionType
    {
        OpenMenu,
        StartRoguelike,
        OpenGacha,
        Custom,
        OpenCharacter,
        OpenDepartment,
    }

    public ActionType action;

  

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (action)
        {
            case ActionType.OpenMenu:
                UIManager.Instance.ToggleMenuPanel();
                break;
            case ActionType.StartRoguelike:
                SceneManager.LoadScene("Vampires"); 
                break;
            case ActionType.OpenGacha:
                Debug.Log("Открыть гачу");
                break;
            case ActionType.OpenCharacter:
                UIManager.Instance.ToggleCharacterPanel();
                break;
            case ActionType.OpenDepartment:
                UIManager.Instance.ToggleDepartmentPanel();
                break;
            case ActionType.Custom:
                Debug.Log("Пользовательская логика");
                break;
        }
    }
}
