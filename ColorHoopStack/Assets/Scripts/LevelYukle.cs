using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelYukle : MonoBehaviour
{
    void Start()
    {/*E�er oyuncu oyun ilk defa oyunu a��yorsa ilk leveli y�kler
     �b�r t�rl� oyuncunun en son oynad��� leveli y�kler*/
        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 1);
        }
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
    }
}
