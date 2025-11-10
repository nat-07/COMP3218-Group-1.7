using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CrossFade : MonoBehaviour
{
    public CanvasGroup[] images;
    public CanvasGroup blackOverlay;    // Reuse the black panel
    public float fadeDuration = 1f;
    public float sceneFadeDuration = 1f;

    private int currentIndex = 0;
    private bool isFading = false;
    private bool finalFadeStarted = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isFading &&  !finalFadeStarted)
        {
            StartCoroutine(FadeToNext());
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            finalFadeStarted = true;
            StartCoroutine(FadeToBlackAndLoad());
            
        }
    }

    IEnumerator FadeToNext()
    {
        isFading = true;
        int nextIndex = currentIndex + 1;
        if (nextIndex >= images.Length && !finalFadeStarted)
        {
            finalFadeStarted = true;
            StartCoroutine(FadeToBlackAndLoad());
            yield break;
        }

        CanvasGroup current = images[currentIndex];
        CanvasGroup next = images[nextIndex];

        next.alpha = 0f;
        next.gameObject.SetActive(true);

        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;

            current.alpha = Mathf.Lerp(1f, 0f, t);
            next.alpha = Mathf.Lerp(0f, 1f, t);

            yield return null;
        }

        current.alpha = 0f;
        next.alpha = 1f;

        currentIndex = nextIndex;
        isFading = false;
    }

    IEnumerator FadeToBlackAndLoad()
    {
        blackOverlay.gameObject.SetActive(true);
        blackOverlay.alpha = 0f;
        float elapsed = 0f;
        while (elapsed < sceneFadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / sceneFadeDuration;

            blackOverlay.alpha = Mathf.Lerp(0f, 1f, t);
            yield return null;
        }
        blackOverlay.alpha = 1f;
        SceneManager.LoadScene("URP2DSceneTemplate");
    }


}
