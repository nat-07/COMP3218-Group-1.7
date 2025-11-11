using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour
{
    public GameObject rocketScene;
    //public GameObject introScene;!
    public GameObject allienceScene;
    public GameObject slaveryScene;
    public GameObject bobaScene;

    private GameObject[] backgrounds;
    private int currentBackgroundIndex = 0;

    //FADE
    public float fadeDuration = 1f;
    private bool isFading = false;


    // Start is called before the first frame update
    void Start()
    {
        backgrounds = new GameObject[]
        {
            //introScene,
            rocketScene,
            allienceScene,
            slaveryScene,
            bobaScene
        };

        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].SetActive(i == 0);
        }
    }

    public void ChangeScene() {
        if (!isFading)
        {
            StartCoroutine(FadeToNextBackground());
        }
    }

    private IEnumerator FadeToNextBackground()
    {
        isFading = true;

        GameObject currentBg = backgrounds[currentBackgroundIndex];
        int nextIndex = (currentBackgroundIndex + 1) % backgrounds.Length;
        GameObject nextBg = backgrounds[nextIndex];

        
        CanvasGroup currentGroup = GetOrAddCanvasGroup(currentBg);
        CanvasGroup nextGroup = GetOrAddCanvasGroup(nextBg);

        
        currentGroup.alpha = 1f;
        nextGroup.alpha = 0f;
        nextBg.SetActive(true);

        
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;

            currentGroup.alpha = 1f - t;
            nextGroup.alpha = t;

            yield return null;
        }

       
        currentGroup.alpha = 0f;
        nextGroup.alpha = 1f;
        currentBg.SetActive(false);

    
        currentBackgroundIndex = nextIndex;
        isFading = false;
    }

    private CanvasGroup GetOrAddCanvasGroup(GameObject obj)
    {
        CanvasGroup group = obj.GetComponent<CanvasGroup>();
        if (group == null)
        {
            group = obj.AddComponent<CanvasGroup>();
        }
        return group;
    }


    public void SkipCutscene()
    {
        SceneManager.LoadScene("URP2DSceneTemplate");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
