using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public CustomerScript CustomerScript;
    [SerializeField] public TextMeshProUGUI ScoreVar;
    [SerializeField] public TextMeshProUGUI Level;
    [SerializeField] public GameObject[] toppings;
    [SerializeField] public GameObject[] Customers;
    // Start is called before the first frame update
    private static int score;
    private int level;
    public static int currentUnlockedToppings;
    private int currentUnlockedCustomers;
    private int[] levelsToUnlockToppings = { 2, 4, 6, 8, 10 };
    private int[] levelsToAddCustomer = { 3, 5, 7, 9 };

    void Start()
    {
        currentUnlockedToppings = -1;
        score = 0;
        level = 1;
        ScoreVar.text = score.ToString();
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
            level += 1;
            Level.text = string.Format("Level {0}", level);
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
        if (levelsToAddCustomer.Contains(level))
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
            Customers[currentUnlockedCustomers].SetActive(true);
        }
    }

    public void AddScore()
    {
            score += 10;
            ScoreVar.text = score.ToString();
            CustomerScript.gotBoba = false;

    }
    
    IEnumerator AddNewCustomer()
    {
        yield return new WaitForSeconds(7);
        Customers[currentUnlockedCustomers].SetActive(true);
    }

    public int getScore()
    {
        return score;
    }

    public int getLevel()
    {
        return level;
    }

}
