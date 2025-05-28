using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject targetPanel;
    public GameObject characterPanel;
    public GameObject departmentPanel;
    public GameObject darkOverlay;

    private void Awake()
    {
        Instance = this;

        if (targetPanel != null) targetPanel.SetActive(false);
        if (characterPanel != null) characterPanel.SetActive(false);
        if (departmentPanel != null) departmentPanel.SetActive(false);
        if (darkOverlay != null) darkOverlay.SetActive(false);
    }

    public void ToggleCharacterPanel()
    {
        bool isActive = characterPanel.activeSelf;
        characterPanel.SetActive(!isActive);
        darkOverlay.SetActive(!isActive);
    }

    public void ToggleDepartmentPanel()
    {
        bool isActive = departmentPanel.activeSelf;
        departmentPanel.SetActive(!isActive);
        darkOverlay.SetActive(!isActive);
    }

    public void ToggleMenuPanel()
    {
        bool isActive = targetPanel.activeSelf;
        targetPanel.SetActive(!isActive);
    }
}

