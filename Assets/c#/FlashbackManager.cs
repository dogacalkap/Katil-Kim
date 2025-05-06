using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class FlashbackManager : MonoBehaviour
{
    [System.Serializable]
    public class FlashEntry
    {
        public string text1;  // mutsuz
        public string text2;  // kavga
        public string text3;  // final
        public Sprite leftIdle;
        public Sprite rightIdle;
        public Sprite leftArgue;
        public Sprite rightArgue;
        public Sprite leftFinal;
        public Sprite rightFinal;
    }

    public CanvasGroup flashbackPanel;
    public TextMeshProUGUI flashbackText;
    public RectTransform speechBubble;
    public Image leftCharacterImage;
    public Image rightCharacterImage;

    private FlashEntry currentEntry;
    private int flashStage = 0;
    private bool isActive = false;
    private bool flashbackFinished = false;
    void Start()
    {
        flashbackPanel.alpha = 0;
        flashbackPanel.interactable = false;
        flashbackPanel.blocksRaycasts = false;

        ResetCharacterImage(leftCharacterImage);
        ResetCharacterImage(rightCharacterImage);

        if (speechBubble != null)
            speechBubble.gameObject.SetActive(false);
    }

    private void ResetCharacterImage(Image img)
    {
        img.sprite = null;
        img.color = new Color(1, 1, 1, 0);
        img.enabled = false;
        img.gameObject.SetActive(false);
    }

    private void ShowCharacterImage(Image img, Sprite sprite)
    {
        if (sprite != null)
        {
            img.sprite = sprite;
            img.color = Color.white;
            img.enabled = true;
            img.gameObject.SetActive(true);
        }
    }

    public void StartFlashback(List<FlashEntry> flashEntries)
    {
        if (flashEntries.Count > 0)
        {
            currentEntry = flashEntries[0];
            flashStage = 0;
            isActive = true;

            flashbackPanel.alpha = 1;
            flashbackPanel.interactable = true;
            flashbackPanel.blocksRaycasts = true;

            if (speechBubble != null)
                speechBubble.gameObject.SetActive(true);

            ShowNext();
        }
    }

    void Update()
    {
        if (isActive && Input.GetKeyDown(KeyCode.Return))
        {
            ShowNext();
        }
        if (flashbackFinished && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("EndingScene", LoadSceneMode.Single);
        }
    }

    void ShowNext()
    {
        switch (flashStage)
        {
            case 0: // Idle (mutsuz)
                flashbackText.text = currentEntry.text1;
                ShowCharacterImage(leftCharacterImage, currentEntry.leftIdle);
                ShowCharacterImage(rightCharacterImage, currentEntry.rightIdle);
                break;

            case 1: // Argue
                flashbackText.text = currentEntry.text2;
                ShowCharacterImage(leftCharacterImage, currentEntry.leftArgue);
                ShowCharacterImage(rightCharacterImage, currentEntry.rightArgue);
                break;

            case 2: // Final
                flashbackText.text = currentEntry.text3;
                ShowCharacterImage(leftCharacterImage, currentEntry.leftFinal);
                ShowCharacterImage(rightCharacterImage, currentEntry.rightFinal);
                break;

            case 3: // Bitir
                flashbackPanel.alpha = 0;
                flashbackPanel.interactable = false;
                flashbackPanel.blocksRaycasts = false;

                ResetCharacterImage(leftCharacterImage);
                ResetCharacterImage(rightCharacterImage);

                if (speechBubble != null)
                    speechBubble.gameObject.SetActive(false);

                isActive = false;
                flashbackFinished = true;
                break;
        }

        flashStage++;
    }
}
