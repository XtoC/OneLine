using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelScriptt : MonoBehaviour
{
    // Start is called before the first frame update
    int a;
    private void Setlv(string s, int addlv)
    {
        //Debug.Log(s + " " + addlv);
        if (addlv >= PlayerPrefs.GetInt(s))
            PlayerPrefs.SetInt(s, addlv);
    }
    public void Pass()
    {
        a = SceneManager.GetActiveScene().buildIndex;
        //Debug.Log(a);
        if (a > 4 && a < 14)
        {
            a = a - 4;
            Setlv("levelsUnlocked", a+1);
        }
        else if (a >= 14 && a <= 22)
        {
            a = a - 13;
            Setlv("MedlevelsUnlocked", a + 1);
        }
        else
        {
            a = a - 22;
            Setlv("HardlevelsUnlocked", a + 1);
        }
        //Debug.Log("level "+PlayerPrefs.GetInt("levelsUnlocked")+ " Unlocked"+" "+a);
    }

}
