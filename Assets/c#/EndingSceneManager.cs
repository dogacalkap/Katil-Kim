using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndingSceneManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public Button confirmButton;

    public GameObject zeynepSprite, tolgaSprite, keremSprite, mertSprite;
    public SpriteRenderer zeynepRenderer, tolgaRenderer, keremRenderer, mertRenderer;
    public GameObject zeynepTick, tolgaCross, keremCross, mertCross;
    public GameObject inputPanel;
    public DoorTransition doorTransition; // Yeni eklendi

    void Start()
    {
        ShowAllDim();
        confirmButton.onClick.AddListener(OnConfirmButton);
    }

    void OnConfirmButton()
    {
        string input = inputField.text.Trim().ToLower();

        ShowAllDim();

        switch (input)
        {
            case "zeynep":
                zeynepRenderer.color = new Color(1f, 1f, 1f, 1f);
                zeynepTick.SetActive(true);

                // Paneli gizle ve kapý geçiþini baþlat
                inputPanel.SetActive(false);
                if (doorTransition != null)
                    doorTransition.PlayDoorZoomIn();

                break;

            case "tolga":
                tolgaRenderer.color = new Color(1f, 1f, 1f, 1f);
                tolgaCross.SetActive(true);
                inputPanel.SetActive(false);
                break;

            case "kerem":
                keremRenderer.color = new Color(1f, 1f, 1f, 1f);
                keremCross.SetActive(true);
                inputPanel.SetActive(false);
                break;

            case "mert":
                mertRenderer.color = new Color(1f, 1f, 1f, 1f);
                mertCross.SetActive(true);
                inputPanel.SetActive(false);
                break;

            default:
                Debug.Log("Geçerli bir isim girin.");
                break;
        }
    }

    void ShowAllDim()
    {
        zeynepSprite.SetActive(true);
        tolgaSprite.SetActive(true);
        keremSprite.SetActive(true);
        mertSprite.SetActive(true);

        zeynepTick.SetActive(false);
        tolgaCross.SetActive(false);
        keremCross.SetActive(false);
        mertCross.SetActive(false);

        Color dim = new Color(0.2f, 0.2f, 0.2f, 1f);
        zeynepRenderer.color = dim;
        tolgaRenderer.color = dim;
        keremRenderer.color = dim;
        mertRenderer.color = dim;
    }
}
