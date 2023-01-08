using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cember : MonoBehaviour
{
    public GameObject _AitOlduguStand;
    public GameObject _AitOlduguCemberSoketi;
    public bool HareketEdebilirMi;
    public string Renk;
    public GameManager _GameManager;
    int CemberSayisi;
    GameObject HareketPozisyonu, GidecegiStand;

    bool secildi, PosDegistir, SoketeOtur, SoketeGeriDon;
    public void HareketEt(string islem, GameObject Stand= null,GameObject Soket = null,GameObject GidilecekObje = null)
    {
        switch (islem)
        {
            case "Secim"://Cember secilebilmisse bulunduðu standýn üstüne doðru çýkar
                HareketPozisyonu = GidilecekObje;
                secildi = true;
                break;
            case "PozisyonDegistir":// Cember pozisyon deðiþtirebiliyorsa gönderilen standýn
                //Boþta olan en üstteki sokete gider.
                GidecegiStand = Stand;
                _AitOlduguCemberSoketi = Soket;
                HareketPozisyonu = GidilecekObje;
                PosDegistir = true;
                break;
            case "SoketeGeriGit"://Eðer çember seçilip baþka bir sokete gidemezse, bulunduðu sokete geri döner
                SoketeGeriDon = true;
                break;
        }
    }
    void Update()
    {
        if (secildi)
        {
            transform.position = Vector3.Lerp(transform.position, HareketPozisyonu.transform.position, .2f);
            if (Vector3.Distance(transform.position, HareketPozisyonu.transform.position) < .10f)
            {
                transform.position = HareketPozisyonu.transform.position;
                secildi = false;
            }
        }
        if (PosDegistir)
        {
            transform.position = Vector3.Lerp(transform.position, HareketPozisyonu.transform.position, .2f);
            if (Vector3.Distance(transform.position, HareketPozisyonu.transform.position) < .10f)
            {
                transform.position = HareketPozisyonu.transform.position;
                PosDegistir = false;
                SoketeOtur = true;
            }
        }
        if (SoketeOtur)
        {
            transform.position = Vector3.Lerp(transform.position, _AitOlduguCemberSoketi.transform.position, .2f);
            if (Vector3.Distance(transform.position, _AitOlduguCemberSoketi.transform.position) < .10f)
            {
                transform.position = _AitOlduguCemberSoketi.transform.position;
                SoketeOtur = false;
                _AitOlduguStand = GidecegiStand;
                CemberSayisi = _AitOlduguStand.GetComponent<Stand>()._Cemberler.Count;
                if (CemberSayisi > 1)
                {
                    _AitOlduguStand.GetComponent<Stand>()._Cemberler[CemberSayisi - 2].GetComponent<Cember>().HareketEdebilirMi = false;
                }
                _GameManager.HareketVar = false;
            }
        }
        if (SoketeGeriDon)
        {
            transform.position = Vector3.Lerp(transform.position, _AitOlduguCemberSoketi.transform.position, .2f);
            if (Vector3.Distance(transform.position, _AitOlduguCemberSoketi.transform.position) < .10f)
            {
                transform.position = _AitOlduguCemberSoketi.transform.position;
                SoketeGeriDon = false;
                
                _GameManager.HareketVar = false;
            }
        }
    }
}
