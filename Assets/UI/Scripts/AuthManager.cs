using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class AuthManager : MonoBehaviour
{
    // Существующие поля
    public TMP_InputField nameInputField;
    public TMP_Text greetingText;
    public GameObject inputPanel;
    public GameObject welcomePanel;
    public GameObject mainMenuPanel;

    // Новые поля для подтверждения
    public GameObject deleteConfirmationPanel; // Панель с кнопками "Удалить"/"Не удалять"
    public Button confirmDeleteButton;
    public Button cancelDeleteButton;

    private void Start()
    {
        // Инициализация подтверждения
        if (deleteConfirmationPanel != null)
        {
            deleteConfirmationPanel.SetActive(false);
            confirmDeleteButton.onClick.AddListener(ConfirmDelete);
            cancelDeleteButton.onClick.AddListener(CancelDelete);
        }

        // Существующая логика загрузки
        PlayerData playerData = SaveManager.LoadPlayerData();

        if (!string.IsNullOrEmpty(playerData.playerName))
        {
            ShowWelcome(playerData.playerName);
        }
        else
        {
            inputPanel.SetActive(true);
            welcomePanel.SetActive(false);
            mainMenuPanel.SetActive(false);
        }
    }

    // Остальные существующие методы без изменений
    public void OnContinueClicked()
    {
        string playerName = nameInputField.text.Trim();

        if (!string.IsNullOrEmpty(playerName))
        {
            PlayerData playerData = SaveManager.LoadPlayerData();
            playerData.playerName = playerName;
            SaveManager.SavePlayerData(playerData);

            ShowWelcome(playerName);
        }
        else
        {
            Debug.Log("Имя не введено!");
        }
    }

    void ShowWelcome(string name)
    {
        inputPanel.SetActive(false);
        welcomePanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        greetingText.text = $"Приветствую, {name}!";
    }

    public void OnWelcomeContinue()
    {
        welcomePanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    // Измененный метод ResetPrefs - теперь только показывает подтверждение
    public void ResetPrefs()
    {
        if (deleteConfirmationPanel != null)
        {
            deleteConfirmationPanel.SetActive(true);
            inputPanel.SetActive(false);
            welcomePanel.SetActive(false);

        }
        else
        {
            // Если панель не назначена, удаляем сразу
            PerformDataDeletion();
        }
    }

    // Новый метод - подтверждение удаления
    private void ConfirmDelete()
    {
        PerformDataDeletion();
        deleteConfirmationPanel.SetActive(false);
        inputPanel.SetActive(true);
    }

    // Новый метод - отмена удаления
    private void CancelDelete()
    {
        deleteConfirmationPanel.SetActive(false);
        welcomePanel.SetActive(true);
    }

    // Вынесенная логика удаления данных
    private void PerformDataDeletion()
    {
        SaveManager.DeleteAllSaves();
        welcomePanel.SetActive(false);
        inputPanel.SetActive(true);
        nameInputField.text = "";
        Debug.Log("Данные успешно удалены");
    }
}