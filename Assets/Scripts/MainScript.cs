using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MainScript : MonoBehaviour
{
    public Text rollCountText;
    public Text scoreText;      
    //public Image[] diceImages;
    public Sprite[] diceSprites;
    public Button[] dices;
    public Text gameOverText;

    private int rollCount = 3; 
    private int score = 0; 
    private int[] diceValues = new int[5];
    private bool[] isFrozen;

    public GameObject gameOverPanel;
    public GameObject gamePlayPanel;
    public GameObject mainMenuPanel;
    
    public event Action<int, bool> OnFreezeToggle;

    void Start()
    {
        isFrozen = new bool[dices.Length];
        UpdateUI();
    }
    
    /*public void RollDice()
    {  
        if (rollCount > 0)
        {
            for (int i = 0; i < diceImages.Length; i++)
            {
                int diceResult = Random.Range(1, 7);
                diceValues[i] = diceResult;
                diceImages[i].sprite = diceSprites[diceResult - 1];
            }

            rollCount--;
            CalculateScore();
            UpdateUI();
            CheckGameOver();

        }
    }*/
    
    
    
    public void RollDice()
    {  
        if (rollCount > 0)
        {
            for (int i = 0; i < dices.Length; i++)
            {
                if (!isFrozen[i])
                {
                    int diceResult = UnityEngine.Random.Range(1, 7);
                    diceValues[i] = diceResult;
                    Image diceImage = dices[i].GetComponent<Image>();
                    diceImage.sprite = diceSprites[diceResult - 1];
                    Debug.Log($"Dice {i} rolled: {diceResult}");
                }
                else
                {
                    Debug.Log($"Dice {i} is frozen, no roll.");
                }
            }

            rollCount--;
            CalculateScore();
            UpdateUI();
            CheckGameOver();

        }
        else
        {
            Debug.Log("No rolls left.");
        }
    }

    public void ToggleFreeze(int diceIndex)
    {
        isFrozen[diceIndex] = !isFrozen[diceIndex];
        Debug.Log($"Dice {diceIndex} freeze state toggled to: {isFrozen[diceIndex]}");
        UpdateDiceVisual(diceIndex);
        
        string frozenStates = string.Join(", ", isFrozen);
        Debug.Log($"Current frozen states: [{frozenStates}]");
        
    }
    
    void UpdateDiceVisual(int diceIndex)
    {
        Image diceImage = dices[diceIndex].GetComponent<Image>();
        if (isFrozen[diceIndex])
        {
            diceImage.color = new Color(0.5f, 0.5f, 0.5f); 
        }
        else
        {
            diceImage.color = Color.white; 
        }
    }
    
    public void StartGame()
    {
        gamePlayPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    void UpdateUI()
    {
        rollCountText.text = rollCount + "/3"; 
        scoreText.text = score + "/300";
    }

    void CheckGameOver()
    {
        if (rollCount == 0 && score < 300)
        {
            gameOverPanel.SetActive(true);
        }
        else if (score >= 300)
        {
            gameOverText.text = "ПОБЕДА!!!";
            gameOverPanel.SetActive(true);
        }
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    void CalculateScore()
    {
        int[] counts = new int[7];
        
        foreach (int value in diceValues)
        {
            counts[value]++;
        }
        
        if (counts[1] == 3) score += 100;
        if (counts[1] == 4) score += 200;
        if (counts[1] == 5) score += 1000;
        
        if (counts[2] == 3) score += 20;
        if (counts[2] == 4) score += 40;
        if (counts[2] == 5) score += 200;

        if (counts[3] == 3) score += 30;
        if (counts[3] == 4) score += 60;
        if (counts[3] == 5) score += 300;

        if (counts[4] == 3) score += 40;
        if (counts[4] == 4) score += 80;
        if (counts[4] == 5) score += 400;

        if (counts[5] == 3) score += 50;
        if (counts[5] == 4) score += 100;
        if (counts[5] == 5) score += 500;

        if (counts[6] == 3) score += 60;
        if (counts[6] == 4) score += 120;
        if (counts[6] == 5) score += 600;
    }
    
}
