using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    GameObject SeciliObje;
    GameObject SeciliStand;
    Cember _Cember;
    [Header("LEVEL AYARLARI")]
    public bool HareketVar;
    Stand _SecilmisStand;
    [SerializeField] private AudioSource[] sesler;
    //Daha Sonsra kullanýlacak
    public int HedefStandSayisi;
    int TamamlananStandSayisi;
    private int levelIndex;
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI LevelText;
    [SerializeField] private GameObject KazandinPanel;
    private void Start()
    {
        levelIndex = SceneManager.GetActiveScene().buildIndex;
        LevelText.text = "LEVEL : " + levelIndex;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out RaycastHit hit, 100))
            {
                if(hit.collider != null && hit.collider.CompareTag("Stand"))
                {//1 stand ve 1 çember seçilmiþ mi diye kontrol edilir
                    if (SeciliObje !=null && SeciliStand != hit.collider.gameObject)//Önceden seçilmiþ olan stand tekrardan seçilmiþmi diye kontrol edilir
                    {//bir çemberi gönderme
                        Stand _Stand = hit.collider.gameObject.GetComponent<Stand>();//Týkladýðým standýn scriptini alýr
                        if(_Stand._Cemberler.Count != 4 && _Stand._Cemberler.Count != 0)
                        {//Stand dolu deðilse ve seçilen standýn çember sayýsý 0 deðilse
                            if (_Cember.Renk == _Stand._Cemberler[_Stand._Cemberler.Count - 1].GetComponent<Cember>().Renk)
                            {//Ýlk seçilen çemberin rengi ile gönderilen standýn en üstedeki çemberin rengi ayný ise çember yer deðiþtirir.
                                SeciliStand.GetComponent<Stand>().SoketDegistirmeIslemleri(SeciliObje);
                                _Cember.HareketEt("PozisyonDegistir", hit.collider.gameObject, _Stand.MusaitSoketiVer(), _Stand.HareketPozisyonu);
                                _Stand.BosOlanSoket++;
                                _Stand._Cemberler.Add(SeciliObje);
                                _Stand.CemberleriKontrolEt();
                                SeciliObje = null;
                                SeciliStand = null;
                                sesler[0].Play();
                            }
                            else
                            {//Çemberlerin renkleri aynýysa çember baþlangýç standýnda kalýr.
                                _Cember.HareketEt("SoketeGeriGit");
                                SeciliObje = null;
                                SeciliStand = null;
                                sesler[1].Play();
                            }                            
                        }
                        else if (_Stand._Cemberler.Count == 0)
                        {
                            SeciliStand.GetComponent<Stand>().SoketDegistirmeIslemleri(SeciliObje);
                            _Cember.HareketEt("PozisyonDegistir", hit.collider.gameObject, _Stand.MusaitSoketiVer(), _Stand.HareketPozisyonu);

                            _Stand.BosOlanSoket++;
                            _Stand._Cemberler.Add(SeciliObje);
                            _Stand.CemberleriKontrolEt();
                            SeciliObje = null;
                            SeciliStand = null;
                            sesler[0].Play();
                        }
                        else
                        {
                            _Cember.HareketEt("SoketeGeriGit");
                            SeciliObje = null;
                            SeciliStand = null;
                            sesler[1].Play();
                        }
                    }
                    else if (SeciliStand == hit.collider.gameObject)
                    {
                        _Cember.HareketEt("SoketeGeriGit");
                        SeciliObje = null;
                        SeciliStand = null;
                        sesler[1].Play();
                    }
                    else
                    {//Ýlk seçilen stand durumu
                        _SecilmisStand = hit.collider.GetComponent<Stand>();
                        if(_SecilmisStand._Cemberler.Count != 0)
                        {
                            SeciliObje = _SecilmisStand.EnUsttekiCemberiVer();
                            _Cember = SeciliObje.GetComponent<Cember>();
                            HareketVar = true;
                            if (_Cember.HareketEdebilirMi)
                            {
                                _Cember.HareketEt("Secim", null, null, _Cember._AitOlduguStand.GetComponent<Stand>().HareketPozisyonu);

                                SeciliStand = _Cember._AitOlduguStand;
                            }
                        }
                        else
                        {
                            SeciliObje = null;
                            SeciliStand = null;
                        }
                    }
                }
            }
        }
    }
    public void StandTamamlandi()
    {
        TamamlananStandSayisi++;
        if(TamamlananStandSayisi == HedefStandSayisi)
        {
            Debug.Log("Kazandin");//Kazandin paneli çýkacak
            KazandinPanel.SetActive(true);
        }
    }
    public void ButonlarinIslevi(string islem)
    {
        switch (islem)
        {
            case "gec":
                if(levelIndex + 1 > 4)
                {
                    PlayerPrefs.SetInt("Level", 4);
                    SceneManager.LoadScene(levelIndex);
                }
                else
                {
                    PlayerPrefs.SetInt("Level", levelIndex + 1);
                    SceneManager.LoadScene(levelIndex + 1);
                }                
                break;
            case "cik":
                Application.Quit();
                break;
            case "tekrar":
                SceneManager.LoadScene(levelIndex);
                break;
        }
    }
}
