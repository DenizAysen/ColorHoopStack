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
            case "Secim"://Cember secilebilmisse bulundu�u stand�n �st�ne do�ru ��kar
                HareketPozisyonu = GidilecekObje;
                secildi = true;
                break;
            case "PozisyonDegistir":// Cember pozisyon de�i�tirebiliyorsa g�nderilen stand�n
                //Bo�ta olan en �stteki sokete gider.
                GidecegiStand = Stand;
                _AitOlduguCemberSoketi = Soket;
                HareketPozisyonu = GidilecekObje;
                PosDegistir = true;
                break;
            case "SoketeGeriGit"://E�er �ember se�ilip ba�ka bir sokete gidemezse, bulundu�u sokete geri d�ner
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
