using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class restart : CoinCounter
{
    // Start is called before the first frame update
    public void Res()
    {
        PlayerPrefs.SetInt("score", 0);
        for (int i = 1; i <= 9; i++) d[i] = 0;
        for (int i = 1; i <= 9; i++) dm[i] = 0;
        for (int i = 1; i <= 9; i++) dh[i] = 0;
        PlayerPrefs.SetInt("levelsUnlocked", 1);
        PlayerPrefs.SetInt("MedlevelsUnlocked", 1);
        PlayerPrefs.SetInt("HardlevelsUnlocked", 1);
    }
}
