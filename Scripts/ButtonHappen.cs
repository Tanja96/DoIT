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
    public static int skipCount;
    public GameObject tehty;
    //Index kertoo mistä kohtaa päivän taulua teksti pitää ottaa.
    
    void Start()
    {
        skipCount = 2;
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
        GameObject.Find("Otsikko").GetComponent<Text>().text = texts[0];
        GameObject.Find("Tiedot").GetComponent<Text>().text = texts[1];
    }

    public void InfoOpen2()
    {
        List<string> texts = GameObject.Find("Jumala").GetComponent<DatabaseConnector>().DataGetText("SELECT Title,Leipä FROM Tehtävät WHERE ID= (SELECT TehtID FROM Päivän WHERE ID= " + index + ")");
        tehty.SetActive(false);
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
        if(skipCount > 0)
        {
            skipCount -= 1;
            GameObject.Find("Jumala").GetComponent<Challenges>().SkipChallenge(index);
            GameObject.Find("Jumala").GetComponent<Challenges>().ShowNew(index);
            skips.GetComponent<Text>().text = skipCount.ToString();
        }
    }

    public void NewClose()
    {
        GameObject.Find("Jumala").GetComponent<Challenges>().ShowChallenges();
        tehty.SetActive(true);
        uudet.SetActive(false);
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
            NotDone();
        }
    }

    public void NotDone()
    {
        GameObject.Find("Jumala").GetComponent<DatabaseConnector>().UpdateDatabase("UPDATE Päivän SET Tehty=0 WHERE ID=" + nyt);
        string nimi = "Tehty (" + nyt + ")";
        GameObject.Find(nimi).GetComponent<Toggle>().isOn = false;
    }
}
