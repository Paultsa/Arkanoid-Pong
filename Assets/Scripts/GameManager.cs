using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    public int lives = 3;
    public int livesP2;
    public int score = 0;
    public int scoreP2 = 0;
    public Text livesText1;
    public Text scoreText1;
    public Text livesText2;
    public Text scoreText2;
    public Text endText;
    public Text winText;
    public int maxScore;
    public bool multiplayer;
    public static bool gameOver;
    SceneManager sceneManager;
    SoundManager sm;
    
    // Start is called before the first frame update
    void Awake()
    {
        sm = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
        gameOver = false;
        winText.text = "";
        endText.text = "";
        livesText1.text = "Lives: " + lives;
        scoreText1.text = "Score: " + score;
        maxScore = 0;
        
        if(SceneManager.GetActiveScene().buildIndex > 2)
        {
            multiplayer = true;
        }
        else
        {
            multiplayer = false;
        }
        if (multiplayer)
        {
            livesText2.text = "Lives: " + livesP2;
            scoreText2.text = "Score: " + scoreP2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Checks if lives are  updated, also checks if no lives left 
    public void UpdateLives(int changeInLives)
    {
        
        
        lives += changeInLives;
        livesText1.text = "Lives: " + lives;
        if (lives <= 0 && multiplayer)
        {
            sm.GameOver();
            sm.StopMusic();
            winText.text = "P2 WON!";
            endText.text = "(press ESC for menu)";
            gameOver = true;
            Time.timeScale = 0f;
            //End game -> open restart menu
        } else if (lives <= 0)
        {
            sm.GameOver();
            sm.StopMusic();
            winText.text = "Game over!";
            endText.text = "(press ESC for menu)";
            gameOver = true;
            Time.timeScale = 0f;
        }

        if (changeInLives < 0 && lives > 0)
        {
            sm.LoseLife();
        }
        else if(lives > 0)
        {
            sm.Heal();
        }
    }
    public void UpdateLivesP2(int changeInLives)
    {
        
        livesP2 += changeInLives;
        livesText2.text = "Lives: " + livesP2;
        if (livesP2 <= 0 && multiplayer)
        {
            sm.GameOver();
            sm.StopMusic();
            winText.text = "P1 WON!";
            endText.text = "(press ESC for menu)";
            gameOver = true;
            Time.timeScale = 0f;
            //End game -> open restart menu
        }

        if (changeInLives < 0 && livesP2 > 0)
        {
            sm.LoseLife();
        }
        else if(lives > 0)
        {
            sm.Heal();
        }
    }

    public void UpdateScore(int changeInScore)
    {
        score += changeInScore;
        scoreText1.text = "Score: " + score;
        if (score+scoreP2 >= maxScore && multiplayer)
        {
            if(score > scoreP2)
            {
                sm.Win();
                sm.StopMusic();
                winText.text = "P1 WON!";
            }
            else if (scoreP2 > score)
            {
                sm.Win();
                sm.StopMusic();
                winText.text = "P2 WON!";
            }
            else
            {
                sm.Win();
                sm.StopMusic();
                winText.text = "A TIE!";
            }
            endText.text = "(press ESC for menu)";
            gameOver = true;
            Time.timeScale = 0f;
            //End game -> open restart menu
        }
        else if (score >= maxScore && !multiplayer)
        {
            sm.Win();
            sm.StopMusic();
            winText.text = "You won!";
            endText.text = "(press ESC for menu)";
            gameOver = true;
            Time.timeScale = 0f;
        }
    }
    public void UpdateScoreP2(int changeInScore)
    {
        scoreP2 += changeInScore;
        scoreText2.text = "Score: " + scoreP2;
        if (scoreP2+score >= maxScore && multiplayer)
        {
            if (scoreP2 > score)
            {
                sm.Win();
                sm.StopMusic();
                winText.text = "P2 WON!";
            }
            else if (score > scoreP2)
            {
                sm.Win();
                sm.StopMusic();
                winText.text = "P1 WON!";
            }
            else
            {
                sm.Win();
                sm.StopMusic();
                winText.text = "A TIE!";
            }
            endText.text = "(press ESC for menu)";
            Time.timeScale = 0f;
            //End game -> open restart menu
        }
    }
    public bool isGameOver()
    {
        return gameOver;
    }
}
