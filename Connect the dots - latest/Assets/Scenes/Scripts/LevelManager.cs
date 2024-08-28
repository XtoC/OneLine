using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    int levelsUnlocked, MedlevelsUnlocked, HardlevelsUnlocked;
    public Button[] buttons, buttonsM, buttonsH;
    int a;

    void Start()
    {
        //PlayerPrefs.SetInt("levelsUnlocked", 1);
        a = SceneManager.GetActiveScene().buildIndex;
        if (a == 2)
        {
            int lvunlock = PlayerPrefs.GetInt("levelsUnlocked", 1);
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].interactable = false;
            }
            for (int i = 0; i < lvunlock; i++)
            {
                buttons[i].interactable = true;
            }
        }
        else if (a==3)
        {
            int lvunlock = PlayerPrefs.GetInt("MedlevelsUnlocked", 1);
            for (int i = 0; i < buttonsM.Length; i++)
            {
                buttonsM[i].interactable = false;
            }
            for (int i = 0; i < lvunlock; i++)
            {
                buttonsM[i].interactable = true;
            }
        }
        else
        {
            int lvunlock = PlayerPrefs.GetInt("HardlevelsUnlocked", 1);
            for (int i = 0; i < buttonsH.Length; i++)
            {
                buttonsH[i].interactable = false;
            }
            for (int i = 0; i < lvunlock; i++)
            {
                buttonsH[i].interactable = true;
            }
        }
        
       // Debug.Log(levelsUnlocked)
    }
    
    public void Loadd(string ScreenName)
    {
        SceneManager.LoadScene(ScreenName);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
