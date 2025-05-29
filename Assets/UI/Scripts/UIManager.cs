using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject targetPanel;
    public GameObject characterPanel;
    public GameObject departmentPanel;
    public GameObject darkOverlay;
    public GameObject inventoryPanel;
    public GameObject GachaPanel;

    private void Awake()
    {
        Instance = this;

        if (targetPanel != null) targetPanel.SetActive(false);
        if (characterPanel != null) characterPanel.SetActive(false);
        if (departmentPanel != null) departmentPanel.SetActive(false);
        if (darkOverlay != null) darkOverlay.SetActive(false);
        if (inventoryPanel != null) inventoryPanel.SetActive(false);
        if (GachaPanel != null) GachaPanel.SetActive(false);
    }

    private void CloseAllPanelsExcept(GameObject panelToKeep)
    {
        if (targetPanel != panelToKeep && targetPanel != null)
            targetPanel.SetActive(false);
        if (characterPanel != panelToKeep && characterPanel != null)
            characterPanel.SetActive(false);
        if (departmentPanel != panelToKeep && departmentPanel != null)
            departmentPanel.SetActive(false);
        if (inventoryPanel != panelToKeep && inventoryPanel != null)
            inventoryPanel.SetActive(false);
        if (GachaPanel != panelToKeep && GachaPanel != null)
            GachaPanel.SetActive(false);
    }

    private void UpdateDarkOverlay()
    {
        bool anyPanelOpen = (targetPanel != null && targetPanel.activeSelf) ||
                            (characterPanel != null && characterPanel.activeSelf) ||
                            (departmentPanel != null && departmentPanel.activeSelf) ||
                            (inventoryPanel != null && inventoryPanel.activeSelf) ||
                            (GachaPanel != null && GachaPanel.activeSelf);

        if (darkOverlay != null)
            darkOverlay.SetActive(anyPanelOpen);
    }

    public void ToggleCharacterPanel()
    {
        bool isActive = characterPanel.activeSelf;
        if (!isActive)
        {
            CloseAllPanelsExcept(characterPanel);
            characterPanel.SetActive(true);
        }
        else
        {
            characterPanel.SetActive(false);
        }
        UpdateDarkOverlay();
    }

    public void ToggleDepartmentPanel()
    {
        bool isActive = departmentPanel.activeSelf;
        if (!isActive)
        {
            CloseAllPanelsExcept(departmentPanel);
            departmentPanel.SetActive(true);
        }
        else
        {
            departmentPanel.SetActive(false);
        }
        UpdateDarkOverlay();
    }

    public void ToggleMenuPanel()
    {
        bool isActive = targetPanel.activeSelf;
        if (!isActive)
        {
            CloseAllPanelsExcept(targetPanel);
            targetPanel.SetActive(true);
        }
        else
        {
            targetPanel.SetActive(false);
        }
        UpdateDarkOverlay();
    }

    public void ToggleInventoryPanel()
    {
        bool isActive = inventoryPanel.activeSelf;
        if (!isActive)
        {
            CloseAllPanelsExcept(inventoryPanel);
            inventoryPanel.SetActive(true);
        }
        else
        {
            inventoryPanel.SetActive(false);
        }
        UpdateDarkOverlay();
    }
    public void ToggleGachaPanel()
    {
        bool isActive = GachaPanel.activeSelf;
        if (!isActive)
        {
            CloseAllPanelsExcept(GachaPanel);
            GachaPanel.SetActive(true);
        }
        else
        {
            GachaPanel.SetActive(false);
        }
        UpdateDarkOverlay();
    }
}
