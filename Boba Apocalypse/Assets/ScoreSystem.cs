using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    AudioManager audioManager;
    public CustomerScript CustomerScript;
    [SerializeField] public TextMeshProUGUI ScoreVar;
    [SerializeField] public TextMeshProUGUI Level;
    [SerializeField] public GameObject[] toppings;
    [SerializeField] public GameObject[] Customers;
    [SerializeField] public PressurePointer PressurePointer;
    [SerializeField] public DirectionPointer DirectionPointer;
    // Start is called before the first frame update
    private static int score;
    private int level;
    private bool addingCustomer;
    public static int currentUnlockedToppings;
    private int currentUnlockedCustomers;
    private int[] levelsToUnlockToppings = { 2, 4, 6, 8, 10 };
    private int[] levelsToAddCustomer = { 3, 5, 7, 9 };
    private bool beginningOfLevel;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        currentUnlockedToppings = -1;
        score = 0;
        level = 1;
        ScoreVar.text = score.ToString();
        addingCustomer = false;
        for (int i = 0; i < toppings.Length; i++)
        {
            toppings[i].SetActive(false);
        }
        for (int i = 0; i < Customers.Length; i++)
        {
            Customers[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (score >= (10 * (level * level - level + 2)))
        {
            audioManager.PlaySFX(audioManager.levelUp);
            level += 1;
            Level.text = string.Format("Level {0}", level);
            Debug.Log(Level.text);
            if (levelsToUnlockToppings.Contains(level))
            {
                Debug.Log("Toggle beginning of level to True");
                beginningOfLevel = true;
            }
            StartCoroutine(Flash());
            if (level == 4)
            {
                PressurePointer.changePressureSpeed(15);
            }else if ( level == 8)
            {
                PressurePointer.changePressureSpeed(20);
            }
            if (level == 4)
            {
                DirectionPointer.changerotationSpeed(60);
            }
            else if (level == 6)
            {
                DirectionPointer.changerotationSpeed(70);
            }else if (level == 8)
            {
                DirectionPointer.changerotationSpeed(80);
            }


        }
        IEnumerator Flash()
            {
                Color yellow = Color.yellow;
                Color white = Color.white;
                Level.fontSize = 1f;

                float duration = 2f; 
                float half = duration / 2f;

                Level.color = yellow;
                yield return new WaitForSeconds(half);

                Level.color = white;
                yield return new WaitForSeconds(half);
    
                Level.color = yellow;
                yield return new WaitForSeconds(half);

                Level.color = white;
                yield return new WaitForSeconds(half);


  
            }

        if (levelsToUnlockToppings.Contains(level))
        {
            for (int i = 0; i <= levelsToUnlockToppings.Length; i++)
            {
                if (levelsToUnlockToppings[i] == level)
                {
                    currentUnlockedToppings = i;
                    break;
                }
            }
            toppings[currentUnlockedToppings].SetActive(true);
        }
        if (levelsToAddCustomer.Contains(level) && !addingCustomer)
        {
            for (int i = 0; i <= levelsToAddCustomer.Length; i++)
            {
                if (levelsToAddCustomer[i] == level)
                {
                    currentUnlockedCustomers = i;
                    break;
                }
            }
            StartCoroutine(AddNewCustomer());
        }
    }

    public void AddScore()
    {
        if (level == 1 || level == 2)
        {
            score += 10;
        } else if (level == 3 || level == 4)
        {
            score += 20;
        } else if (level == 5 ||  level == 6 || level == 7)
        {
            score += 30;
        } else
        {
            score += 40;
        }

        ScoreVar.text = score.ToString();
        CustomerScript.gotBoba = false;

    }
    
    IEnumerator AddNewCustomer()
    {
        addingCustomer = true;
        yield return new WaitForSeconds(7);
        Customers[currentUnlockedCustomers].SetActive(true);
        addingCustomer = false;
    }

    public int getScore()
    {
        return score;
    }

    public bool getBeginningLevel()
    {
        return beginningOfLevel;
    }

    public void setBeginningLevelFalse()
    {
        Debug.Log("Toggle beginning of level to false");
        beginningOfLevel = false;
    }

    public int getLevel()
    {
        return level;
    }

}
