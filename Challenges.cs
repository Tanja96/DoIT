using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Challenges : MonoBehaviour {
    public GameObject[] challenges = new GameObject[5];
    public GameObject info;
   

	void Start () {

        challenges = GameObject.FindGameObjectsWithTag("Challenge");
        ChangeChallenges();
	}
	
	void Update () {
		
	}

    public void SetPoints()
    {
        foreach(GameObject chal in challenges)
        {
            if (chal.GetComponent<Toggle>().isOn)
            {
                List<int> list = GameObject.Find("Jumala").GetComponent<DatabaseConnector>().GetPoints("SELECT Sos,Äly,Fyy,Hen,Tun FROM Tehtävät WHERE Title= " + chal.gameObject.GetComponentInChildren<Text>().text);
                GameObject.Find("Jumala").GetComponent<DatabaseConnector>().UpdateDatabase("UPDATE Käyttäjä SET SosPisteet= SosPisteet +" + list[0] + ", ÄlyPisteet= ÄlyPisteet " + list[1] + ", FyyPisteet= FyyPisteet +" + list[2] + ", HenPisteet= HenPisteet +" + list[3] + ", TuPisteetn= TunPisteet +" + list[4] + " WHERE ID =1 ");
            }
        }
    }

    public void ChangeChallenges()
    {
        List<int> valitut = new List<int>();
        //Tähän alkuun tulee kutsu Statseista, jonka avulla katsotaan pitääkö jotain osa-aluetta suosia.

        //SOSIAALISUUS
        List<int> IDt = GameObject.Find("Jumala").GetComponent<DatabaseConnector>().FindList("SELECT ID FROM Tehtävät WHERE Daily = 1 AND Sos > 0 AND Sos >= Fyy AND Sos >= Äly AND Sos >=Hen AND Sos >=Tun");
        int rand = (int)Random.Range(0, IDt.Count);
        int num = IDt[rand];
        valitut.Add(num);
        List<string> texts = GameObject.Find("Jumala").GetComponent<DatabaseConnector>().DataGetText("SELECT Title,Leipä FROM Tehtävät WHERE ID= " + num);
        challenges[0].gameObject.GetComponentInChildren<Text>().text = texts[0];

        //FYYSINEN
        IDt = GameObject.Find("Jumala").GetComponent<DatabaseConnector>().FindList("SELECT ID FROM Tehtävät WHERE Daily = 1 AND Fyy > 0 AND Fyy >= Sos AND Fyy >= Äly AND Fyy >=Hen AND Fyy >=Tun");
        for (int i = 0; i < IDt.Count; i++)
        {
            foreach (int x in valitut)
            {
                if (IDt[i] == x)
                {
                    IDt.Remove(x);
                }
            }
        }
        rand = (int)Random.Range(0, IDt.Count);
        num = IDt[rand];
        valitut.Add(num);
        texts = GameObject.Find("Jumala").GetComponent<DatabaseConnector>().DataGetText("SELECT Title,Leipä FROM Tehtävät WHERE ID= " + num);
        challenges[1].gameObject.GetComponentInChildren<Text>().text = texts[0];

        //ÄLYLLINEN
        IDt = GameObject.Find("Jumala").GetComponent<DatabaseConnector>().FindList("SELECT ID FROM Tehtävät WHERE Daily = 1 AND Äly > 0 AND Äly >= Sos AND Äly >= Fyy AND Äly >=Hen AND Äly >=Tun");
        for (int i = 0; i < IDt.Count; i++)
        {
            foreach (int x in valitut)
            {
                if (IDt[i] == x)
                {
                    IDt.Remove(x);
                }
            }
        }
        rand = (int)Random.Range(0, IDt.Count);
        num = IDt[rand];
        valitut.Add(num);
        texts = GameObject.Find("Jumala").GetComponent<DatabaseConnector>().DataGetText("SELECT Title,Leipä FROM Tehtävät WHERE ID= " + num);
        challenges[2].gameObject.GetComponentInChildren<Text>().text = texts[0];

        //HENKINEN
        IDt = GameObject.Find("Jumala").GetComponent<DatabaseConnector>().FindList("SELECT ID FROM Tehtävät WHERE Daily = 1 AND Hen > 0 AND Hen >= Sos AND Hen >= Äly AND Hen >=Fyy AND Hen >=Tun");
        for (int i = 0; i < IDt.Count; i++)
        {
            foreach (int x in valitut)
            {
                if (IDt[i] == x)
                {
                    IDt.Remove(x);
                }
            }
        }
        num = IDt[rand];
        valitut.Add(num);
        texts = GameObject.Find("Jumala").GetComponent<DatabaseConnector>().DataGetText("SELECT Title,Leipä FROM Tehtävät WHERE ID= " + num);
        challenges[3].gameObject.GetComponentInChildren<Text>().text = texts[0];

        //TUNTEELLINEN
        IDt = GameObject.Find("Jumala").GetComponent<DatabaseConnector>().FindList("SELECT ID FROM Tehtävät WHERE Daily = 1 AND Tun > 0 AND Tun >= Sos AND Tun >= Äly AND Tun >=Hen AND Tun >=Fyy");
        for (int i= 0; i < IDt.Count; i++)
        {
            foreach (int x in valitut)
            {
                if (IDt[i] == x)
                {
                    IDt.Remove(x);
                }
            }
        }
        rand = (int)Random.Range(0, IDt.Count);
        num = IDt[rand];
        valitut.Add(num);
        texts = GameObject.Find("Jumala").GetComponent<DatabaseConnector>().DataGetText("SELECT Title,Leipä FROM Tehtävät WHERE ID= " + num);
        challenges[4].gameObject.GetComponentInChildren<Text>().text = texts[0];
        info.GetComponent<Text>().text = texts[1];
    }

}
