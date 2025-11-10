using UnityEngine;
using System.Collections;

public class CrossFading : MonoBehaviour
{
    public CanvasGroup[] images;
    public float fadeDuration = 1f;

    private int currentIndex = 0;
    private bool isFading = false;

    void Update()
    {
        // Detect clicks anywhere on the screen
        if (Input.GetMouseButtonDown(0) && !isFading)
        {
            StartCoroutine(FadeToNext());
        }
    }

    IEnumerator FadeToNext()
    {
        isFading = true;

        int nextIndex = (currentIndex + 1) % images.Length;

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
}