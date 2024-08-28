using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void Pass ()
    {
        /*int a = SceneManager.GetActiveScene().buildIndex;
        if( a >= PlayerPrefs.GetInt("levelsUnlocked") )
        {
            PlayerPrefs.GetInt("levelsUnlocked", a+1);  
        }
        Debug.Log(PlayerPrefs.GetInt("levelsUnlocked")  + "haha" + " " + a);*/
        Debug.Log("Button Clicked");
    }    
}
