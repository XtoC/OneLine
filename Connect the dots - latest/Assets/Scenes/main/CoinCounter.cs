using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CoinCounter : MonoBehaviour
{
    // Start is called before the first frame update
    public Text scoreText;
    public static int score;
    public static  int [] d = new int [10];
    public static int[] dm = new int[10];
    public static int[] dh = new int[10];
    int levelsUnlocked, MedlevelsUnlocked, HardlevelsUnlocked;
    void Start()
    {
        //PlayerPrefs.SetInt("score", 0);
        //for (int i = 1; i <= 9; i++) d[i] = 0;
        Debug.Log(PlayerPrefs.GetInt("score"));
        score = PlayerPrefs.GetInt("score");
        scoreText.text = PlayerPrefs.GetInt("score").ToString();
        levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);
        for (int i = 1; i < levelsUnlocked; i++) d[i] = 1;
        MedlevelsUnlocked = PlayerPrefs.GetInt("MedlevelsUnlocked", 1);
        for (int i = 1; i < MedlevelsUnlocked; i++) dm[i] = 1;
        HardlevelsUnlocked = PlayerPrefs.GetInt("HardlevelsUnlocked", 1);
        for (int i = 1; i < HardlevelsUnlocked; i++) dh[i] = 1;
        
    }

    // Update is called once per frame
    public void scoreUpdate()
    {
        int a = SceneManager.GetActiveScene().buildIndex;
        if (a > 4 && a < 14)
        {
            a = a - 4;
            if (d[a] == 0)
            {
                score++;
                scoreText.text = score.ToString();
                PlayerPrefs.SetInt("score", score);
                Debug.Log(a + " " + d[a]);
                d[a] = 1;

            }
            //else Debug.Log(a + " " + d[a] + " " + PlayerPrefs.GetInt("score"));
        }
        else if (a >= 14 && a <= 22)
        {
            a = a - 13;
            if (dm[a] == 0)
            {
                score = score + 2;
                scoreText.text = score.ToString();
                PlayerPrefs.SetInt("score", score);
                Debug.Log(a + " " + dm[a]);
                dm[a] = 1;

            }
            //else Debug.Log(a + " " + d[a] + " " + PlayerPrefs.GetInt("score"));
        }
        else
        {
            a = a - 22;
            if (dh[a] == 0)
            {
                score = score + 3;
                scoreText.text = score.ToString();
                PlayerPrefs.SetInt("score", score);
                Debug.Log(a + " " + dh[a]);
                dh[a] = 1;

            }
            //else Debug.Log(a + " " + d[a] + " " + PlayerPrefs.GetInt("score"));
        }
    }
}
