using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DoorTransition : MonoBehaviour
{
    public SpriteRenderer doorSpriteRenderer;
    public Sprite openDoor;
    public Sprite closedDoor;
    public CanvasGroup environmentLayer;
    public GameObject uiPanelToHide; // 🔥 intro'daki input panel

    public float zoomTime = 0.8f;
    public float finalScale = 3f;

    public void PlayDoorZoomIn()
    {
        StartCoroutine(ZoomIntoClosedDoor());
    }

    IEnumerator ZoomIntoClosedDoor()
    {
        // Ortam efekti gizle
        if (environmentLayer != null)
        {
            environmentLayer.alpha = 0;
            environmentLayer.gameObject.SetActive(false);
        }

        if (uiPanelToHide != null)
        {
            Debug.Log("Input panel gizleniyor.");
            uiPanelToHide.SetActive(false);
        }

        // Kapı animasyonu
        doorSpriteRenderer.gameObject.SetActive(true);
        doorSpriteRenderer.sprite = openDoor;
        doorSpriteRenderer.color = Color.white;
        doorSpriteRenderer.transform.localScale = Vector3.one * 1.5f;

        yield return new WaitForSeconds(0.05f);

        doorSpriteRenderer.sprite = closedDoor;

        float t = 0f;
        Vector3 startScale = doorSpriteRenderer.transform.localScale;
        Vector3 endScale = Vector3.one * finalScale;

        while (t < 1f)
        {
            t += Time.deltaTime / zoomTime;
            doorSpriteRenderer.transform.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }

        yield return new WaitForSeconds(0.3f);

        SceneManager.LoadScene("CrimeScene");
    }
}
