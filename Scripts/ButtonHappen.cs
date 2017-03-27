using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHappen : MonoBehaviour {
    public GameObject info;
    public int index;
    public static int nyt;
    public GameObject uudet;
    public GameObject skips;
    public static int skipCount = 2;
    public GameObject tehty;
    public GameObject lukko;
    //Index kertoo mistä kohtaa päivän taulua teksti pitää ottaa.
    
    void Start()
    {
        skips.GetComponent<Text>().text = skipCount.ToString();
    }

    public void InfoOpen()
    {
        nyt = index;
        List<string> texts = GameObject.Find("Jumala").GetComponent<DatabaseConnector>().DataGetText("SELECT Title,Leipä FROM Tehtävät WHERE ID= (SELECT TehtID FROM Päivän WHERE ID= " + nyt + ")");
        info.SetActive(true);
        int tehty = GameObject.Find("Jumala").GetComponent<DatabaseConnector>().getID("SELECT Tehty FROM Päivän WHERE ID=" + nyt);
        if (tehty == 1) GameObject.Find("Tehty").GetComponent<Toggle>().isOn = true;
        else GameObject.Find("Tehty").GetComponent<Toggle>().isOn = false;
        //Tarkistaa voiko tehtävää lukita
        int voi = GameObject.Find("Jumala").GetComponent<DatabaseConnector>().getID("SELECT LukonLukko FROM Päivän WHERE ID=" + nyt);
        if(voi == 0)
        {
            lukko.GetComponent<Toggle>().interactable = true;
            int lukittu = GameObject.Find("Jumala").GetComponent<DatabaseConnector>().getID("SELECT Lukittu FROM Päivän WHERE ID=" + nyt);
            if (lukittu == 1) GameObject.Find("Lukko").GetComponent<Toggle>().isOn = true;
            else GameObject.Find("Lukko").GetComponent<Toggle>().isOn = false;
        }
        else
        {
            lukko.GetComponent<Toggle>().interactable = false;
        }
        GameObject.Find("Otsikko").GetComponent<Text>().text = texts[0];
        GameObject.Find("Tiedot").GetComponent<Text>().text = texts[1];
    }

    public void InfoOpen2()
    {
        List<string> texts = GameObject.Find("Jumala").GetComponent<DatabaseConnector>().DataGetText("SELECT Title,Leipä FROM Tehtävät WHERE ID= (SELECT TehtID FROM Päivän WHERE ID= " + index + ")");
        tehty.SetActive(false);
        lukko.SetActive(false);
        info.SetActive(true);
        GameObject.Find("Otsikko").GetComponent<Text>().text = texts[0];
        GameObject.Find("Tiedot").GetComponent<Text>().text = texts[1];
    }

    public void InfoClose()
    {
        info.SetActive(false);
    }

    public void Skip()
    {
        Debug.Log(GameObject.Find("Jumala").GetComponent<DatabaseConnector>().getID("SELECT LukonLukko FROM Päivän WHERE ID=" + index));
        //Tarvitseeko oikeasti katsoa että onko tehtävä aiemmin lukittu? EIkai vaihda jos ei halua vaihtaa?
        if (skipCount > 0 && GameObject.Find("Jumala").GetComponent<DatabaseConnector>().getID("SELECT LukonLukko FROM Päivän WHERE ID=" + index) == 0)
        {
            skipCount -= 1;
            GameObject.Find("Jumala").GetComponent<Challenges>().NewChallenge(index);
            GameObject.Find("Jumala").GetComponent<Challenges>().ShowNew(index);
            skips.GetComponent<Text>().text = skipCount.ToString();
        }
    }

    public void NewClose()
    {
        GameObject.Find("Jumala").GetComponent<Challenges>().ShowChallenges();
        tehty.SetActive(true);
        lukko.SetActive(true);
        uudet.SetActive(false);
        skipCount = 2;
    }

    public void IsDone()
    {
        if (GameObject.Find("Tehty").GetComponent<Toggle>().isOn)
        {
            GameObject.Find("Jumala").GetComponent<DatabaseConnector>().UpdateDatabase("UPDATE Päivän SET Tehty=1 WHERE ID=" + nyt);
            string nimi = "Tehty (" + nyt + ")";
            GameObject.Find(nimi).GetComponent<Toggle>().isOn = true;
        }
       else
        {
            GameObject.Find("Jumala").GetComponent<DatabaseConnector>().UpdateDatabase("UPDATE Päivän SET Tehty=0 WHERE ID=" + nyt);
            string nimi = "Tehty (" + nyt + ")";
            GameObject.Find(nimi).GetComponent<Toggle>().isOn = false;
        }
    }

    public void Lock()
    {
        if (GameObject.Find("Lukko").GetComponent<Toggle>().isOn)
        {
            GameObject.Find("Jumala").GetComponent<DatabaseConnector>().UpdateDatabase("UPDATE Päivän SET Lukittu = 1, Viimeksi= " + System.DateTime.Now.Day + " WHERE ID=" + nyt);
        }
        else
        {
            GameObject.Find("Jumala").GetComponent<DatabaseConnector>().UpdateDatabase("UPDATE Päivän SET Lukittu = 0, Viimeksi = NULL WHERE ID=" + nyt);
        }
    }
}
