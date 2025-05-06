using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class IntroManager : MonoBehaviour
{
    public TextMeshProUGUI introTitleText;
    public TextMeshProUGUI introText;
    public TextMeshProUGUI continuePromptText;
    public CanvasGroup introTitleCanvasGroup;
    public CanvasGroup backgroundFade;
    public GameObject environmentLayer;
    public SpriteRenderer doorSpriteRenderer;
    public Sprite closedDoorSprite;
    public Sprite openDoorSprite;
    public Camera mainCamera;
    public GameObject uiPanelToHide;
    public AudioSource audioSource;      
    public AudioClip typeSound;          

    public string titleLine = "Bakalım katili bulabilecek misin?";
    public string[] introLines;
    public float textFadeTime = 1.5f;
    public float doorFadeDuration = 1.5f;

    private int currentLine = 0;
    private bool canProceed = false;
    private bool isTyping = false;

    void Start()
    {
        backgroundFade.alpha = 1;
        introText.text = "";
        introTitleCanvasGroup.alpha = 0;
        introTitleText.text = "";

        if (continuePromptText != null)
            continuePromptText.gameObject.SetActive(false);

        if (doorSpriteRenderer != null)
        {
            doorSpriteRenderer.sprite = closedDoorSprite;
            doorSpriteRenderer.color = new Color(1, 1, 1, 0);
            doorSpriteRenderer.transform.localScale = Vector3.one * 2.5f;
            doorSpriteRenderer.gameObject.SetActive(false);
        }

        StartCoroutine(FadeIn());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && canProceed && !isTyping)
        {
            if (currentLine < introLines.Length)
            {
                StartCoroutine(ShowLine(introLines[currentLine]));
                currentLine++;
            }
            else
            {
                StartCoroutine(PlayDoorFadeTransition());
            }
        }
    }

    IEnumerator FadeIn()
    {
        while (backgroundFade.alpha > 0)
        {
            backgroundFade.alpha -= Time.deltaTime / textFadeTime;
            yield return null;
        }
        backgroundFade.alpha = 0;

        yield return StartCoroutine(ShowTitleLine());
    }

    IEnumerator ShowTitleLine()
    {
        introTitleCanvasGroup.alpha = 0;
        introTitleText.text = "";

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / 1.5f;
            introTitleCanvasGroup.alpha = Mathf.Clamp01(t);
            yield return null;
        }

        foreach (char c in titleLine)
        {
            introTitleText.text += c;

            if (typeSound != null && audioSource != null)
                audioSource.PlayOneShot(typeSound, 0.2f);

            yield return new WaitForSeconds(0.025f);
        }

        yield return new WaitForSeconds(2f);

        float fadeT = 0;
        while (fadeT < 1f)
        {
            fadeT += Time.deltaTime / 1f;
            introTitleCanvasGroup.alpha = 1 - fadeT;
            yield return null;
        }

        introTitleText.text = "";
        if (continuePromptText != null)
            continuePromptText.gameObject.SetActive(true);

        canProceed = true;
    }

    IEnumerator ShowLine(string line)
    {
        isTyping = true;
        introText.text = "";

        if (continuePromptText != null)
            continuePromptText.gameObject.SetActive(false);

        string[] words = line.Split(' '); // kelimelere böl

        foreach (string word in words)
        {
            // her kelime için sesi bir kez çal
            if (typeSound != null && audioSource != null)
                audioSource.PlayOneShot(typeSound, 0.3f);

            foreach (char c in word)
            {
                introText.text += c;
                yield return new WaitForSeconds(0.025f);
            }

            introText.text += " "; // boşluk ekle
            yield return new WaitForSeconds(0.025f);
        }

        isTyping = false;

        if (continuePromptText != null)
            continuePromptText.gameObject.SetActive(true);
    }

    IEnumerator PlayDoorFadeTransition()
    {
        canProceed = false;

        if (continuePromptText != null)
            continuePromptText.gameObject.SetActive(false);

        if (introText != null)
        {
            introText.text = "";
            introText.gameObject.SetActive(false);
        }

        if (environmentLayer != null)
            environmentLayer.SetActive(false);

        doorSpriteRenderer.sprite = closedDoorSprite;
        doorSpriteRenderer.color = new Color(1, 1, 1, 0);
        doorSpriteRenderer.transform.localScale = Vector3.one * 2.5f;
        doorSpriteRenderer.gameObject.SetActive(true);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / doorFadeDuration;
            doorSpriteRenderer.color = new Color(1, 1, 1, Mathf.Clamp01(t));
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / doorFadeDuration;
            float blend = Mathf.SmoothStep(0f, 1f, t);
            doorSpriteRenderer.color = new Color(1, 1, 1, 1);
            doorSpriteRenderer.sprite = blend < 0.5f ? closedDoorSprite : openDoorSprite;
            yield return null;
        }

        if (uiPanelToHide != null)
            uiPanelToHide.SetActive(false);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("CrimeScene", LoadSceneMode.Single);
    }

}
