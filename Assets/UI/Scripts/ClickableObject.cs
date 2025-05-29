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
        OpenInventory,
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
                Debug.Log("������� ����");
                break;
            case ActionType.OpenCharacter:
                UIManager.Instance.ToggleCharacterPanel();
                break;
            case ActionType.OpenDepartment:
                UIManager.Instance.ToggleDepartmentPanel();
                break;
            case ActionType.OpenInventory:
                UIManager.Instance.ToggleInventoryPanel();
                break;

        }
    }
}