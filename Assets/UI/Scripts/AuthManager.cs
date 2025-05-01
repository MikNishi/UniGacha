using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AuthManager : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public TMP_Text greetingText;
    public GameObject inputPanel;        // Ввод имени
    public GameObject welcomePanel;      // "С возвращением" + кнопка
    public GameObject mainMenuPanel;     // Главное меню

    private const string PlayerNameKey = "PlayerName";
    private string currentName = "";

    void Start()
    {
        if (PlayerPrefs.HasKey(PlayerNameKey))
        {
            currentName = PlayerPrefs.GetString(PlayerNameKey);
            ShowWelcome(currentName);
        }
        else
        {
            inputPanel.SetActive(true);
            welcomePanel.SetActive(false);
            mainMenuPanel.SetActive(false);
        }
    }

    public void OnContinueClicked()
    {
        string playerName = nameInputField.text.Trim();

        if (!string.IsNullOrEmpty(playerName))
        {
            PlayerPrefs.SetString(PlayerNameKey, playerName);
            PlayerPrefs.Save();
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

    // Вызывается при нажатии кнопки "Нажмите, чтобы продолжить"
    public void OnWelcomeContinue()
    {
        welcomePanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void ResetPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        welcomePanel.SetActive(false);
        inputPanel.SetActive(true);
    }
}
