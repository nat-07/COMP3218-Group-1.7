using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{  
    public CanvasGroup blackOverlay;    // Reuse the black panel
    public float fadeDuration = 1f;
    public float sceneFadeDuration = 1f;

    bool isPlay = false; 


    void Update()
    {
        if (!isPlay)
        {
            blackOverlay.gameObject.SetActive(false);
        }

    }

    public void LoadScene()
    {
        isPlay = true;

        StartCoroutine(FadeToBlackAndLoad());
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
        SceneManager.LoadScene("Cutscene 1");
    }
}
