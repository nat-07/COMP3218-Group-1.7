using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor.SearchService;

public class HP : MonoBehaviour
{
    public GameObject background;
    public TextMeshProUGUI scoreText;

    public GameObject[] HPHearts;
    private int hp;
    // Start is called before the first frame update
    void Start()
    {
        hp = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp == -1)
        {
            background.SetActive(true);
            SceneManager.LoadScene("GameOverDraft");
        }
    }

    public void ReduceHP()
    {
        hp--;
        for (int i = hp+1; i < HPHearts.Length; i++)
        {
            HPHearts[i].SetActive(false);
        }
    }

}
