using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance;

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Text dialogueText;
    [SerializeField] private GameObject choicesPanel;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    private void Awake()
    {
        Instance = this;
        dialoguePanel.SetActive(false);
        choicesPanel.SetActive(false);
    }

    // Diálogo simple, sin opciones (solo mostrar texto)
    public void ShowLine(string text)
    {
        dialoguePanel.SetActive(true);
        choicesPanel.SetActive(false);
        dialogueText.text = text;
    }

    // Diálogo con opciones Sí / No
    public void ShowChoice(string text, UnityAction onYes, UnityAction onNo)
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = text;
        choicesPanel.SetActive(true);

        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();
        yesButton.onClick.AddListener(onYes);
        noButton.onClick.AddListener(onNo);
    }

    public void Hide()
    {
        dialoguePanel.SetActive(false);
        choicesPanel.SetActive(false);
    }
}