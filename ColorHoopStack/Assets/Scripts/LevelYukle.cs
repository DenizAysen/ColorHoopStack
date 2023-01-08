using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelYukle : MonoBehaviour
{
    void Start()
    {/*Eðer oyuncu oyun ilk defa oyunu açýyorsa ilk leveli yükler
     Öbür türlü oyuncunun en son oynadýðý leveli yükler*/
        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 1);
        }
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
    }
}
