using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Challenges : MonoBehaviour {
    public GameObject[] challenges = new GameObject[5];
    public GameObject[] newchallenges = new GameObject[5];
    public GameObject info;
    public GameObject uudet;
    private static System.DateTime Date;
    private List<int> valitut = new List<int>();
    private string fyy = "SELECT ID FROM Tehtävät WHERE Daily = 1 AND Fyy > 0 AND Fyy >= Sos AND Fyy >= Äly AND Fyy >=Hen AND Fyy >=Tun";
    private string sos = "SELECT ID FROM Tehtävät WHERE Daily = 1 AND Sos > 0 AND Sos >= Fyy AND Sos >= Äly AND Sos >=Hen AND Sos >=Tun";
    private string äly = "SELECT ID FROM Tehtävät WHERE Daily = 1 AND Äly > 0 AND Äly >= Sos AND Äly >= Fyy AND Äly >=Hen AND Äly >=Tun";
    private string hen = "SELECT ID FROM Tehtävät WHERE Daily = 1 AND Hen > 0 AND Hen >= Sos AND Hen >= Äly AND Hen >=Fyy AND Hen >=Tun";
    private string tun = "SELECT ID FROM Tehtävät WHERE Daily = 1 AND Tun > 0 AND Tun >= Sos AND Tun >= Äly AND Tun >=Hen AND Tun >=Fyy";

    void Start () {
        //if(Date == null)
        //{
        //    Date = System.DateTime.UtcNow;
        //}
        //if(Date.Date != System.DateTime.UtcNow.Date)
        //{
        //    NewMissions();
        //}
        //Debug.Log(System.DateTime.Now.ToString());
        int pv = GameObject.Find("Jumala").GetComponent<DatabaseConnector>().getID("SELECT ViimeksiPaikalla FROM Käyttäjä WHERE ID=1");
        if (pv == 0)
        {
            GameObject.Find("Jumala").GetComponent<DatabaseConnector>().UpdateDatabase("UPDATE Käyttäjä SET ViimeksiPaikalla = " + System.DateTime.UtcNow.Day + " WHERE ID=1");
            NewMissions();
        }
        if(System.DateTime.UtcNow.Day == pv)
        {
            NewMissions();
            GameObject.Find("Jumala").GetComponent<DatabaseConnector>().UpdateDatabase("UPDATE Käyttäjä SET ViimeksiPaikalla = " + System.DateTime.UtcNow.Day + " WHERE ID=1");
        }
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

    public void ShowChallenges()
    {
        int x = 1;
        foreach( GameObject chal in challenges)
        {
            List<string> texts = GameObject.Find("Jumala").GetComponent<DatabaseConnector>().DataGetText("SELECT Title,Leipä FROM Tehtävät WHERE ID=(SELECT TehtID FROM Päivän WHERE ID = " + x + ")");
            if (texts[0].Length > 25)
            {
                string sub = texts[0].Substring(0, 22);
                sub = sub + "...";
                chal.GetComponentInChildren<Text>().text = sub;
            }
            else
            {
                chal.GetComponentInChildren<Text>().text = texts[0];
            }
            x++;
        }
    }

    public void ShowNew(int ind)
    {
        List<string> texts = GameObject.Find("Jumala").GetComponent<DatabaseConnector>().DataGetText("SELECT Title,Leipä FROM Tehtävät WHERE ID=(SELECT TehtID FROM Päivän WHERE ID = " + ind + ")");
        if (texts[0].Length > 25)
        {
            string sub = texts[0].Substring(0, 21);
            sub = sub + "...";
            newchallenges[ind - 1].GetComponentInChildren<Text>().text = sub;
        }
        else newchallenges[ind-1].GetComponentInChildren<Text>().text = texts[0];
    }

    public void NewMissions()
    {
        valitut.Clear();
        int x = 1;
        foreach (GameObject chal in newchallenges)
        {
            SkipChallenge(x);
            List<string> texts = GameObject.Find("Jumala").GetComponent<DatabaseConnector>().DataGetText("SELECT Title,Leipä FROM Tehtävät WHERE ID=(SELECT TehtID FROM Päivän WHERE ID = " + x + ")");
            if(texts[0].Length > 25)
            {
                string sub = texts[0].Substring(0, 22);
                sub = sub + "...";
                chal.GetComponentInChildren<Text>().text = sub;
            }
            else
            {
                chal.GetComponentInChildren<Text>().text = texts[0];
            }
            x++;
        }
        uudet.SetActive(true);
    }

    public void SkipChallenge(int index)
    {
        string str;
        if (index == 1) str = sos;
        else if (index == 2) str = fyy;
        else if (index == 3) str = äly;
        else if (index == 4) str = hen;
        else str = tun;
        List<int> IDt = GameObject.Find("Jumala").GetComponent<DatabaseConnector>().FindList(str);
        if (IDt == null || IDt.Count == 0)
        {
            str = "SELECT ID FROM Tehtävät WHERE Daily = 1";
            IDt = GameObject.Find("Jumala").GetComponent<DatabaseConnector>().FindList(str);
        }
        List<int> uudet = new List<int>();
        for (int i = 0; i < IDt.Count; i++)
        {
            for (int x = 0; x < valitut.Count; x++)
            {
                if (IDt[i] == valitut[x])
                {
                    uudet.Add(IDt[i]);
                }
            }
        }
        if (uudet != null || uudet.Count > 0)
        {
            foreach (int x in uudet)
            {
                IDt.Remove(x);
            }
        }
        if (IDt == null || IDt.Count == 0)
        {
            uudet.Clear();
            str = "SELECT ID FROM Tehtävät WHERE Daily = 1";
            IDt = GameObject.Find("Jumala").GetComponent<DatabaseConnector>().FindList(str);
            for (int i = 0; i < IDt.Count; i++)
            {
                for (int x = 0; x < valitut.Count; x++)
                {
                    if (IDt[i] == valitut[x])
                    {
                        uudet.Add(IDt[i]);
                    }
                }
            }
            if (uudet != null || uudet.Count != 0)
            {
                foreach (int x in uudet)
                {
                    IDt.Remove(x);
                }
            }
        }
        int rand = (int)Random.Range(0, IDt.Count);
        int num = IDt[rand];
        valitut.Add(num);
        GameObject.Find("Jumala").GetComponent<DatabaseConnector>().UpdateDatabase("UPDATE Päivän SET TehtID = " + num + " WHERE ID = " + index);
    }

    public void ChangeChallenges()
    {
        //Tähän alkuun tulee kutsu Statseista, jonka avulla katsotaan pitääkö jotain osa-aluetta suosia.

        //SOSIAALISUUS
        List<int> IDt = GameObject.Find("Jumala").GetComponent<DatabaseConnector>().FindList("SELECT ID FROM Tehtävät WHERE Daily = 1 AND Sos > 0 AND Sos >= Fyy AND Sos >= Äly AND Sos >=Hen AND Sos >=Tun");
        int rand = (int)Random.Range(0, IDt.Count);
        int num = IDt[rand];
        valitut.Add(num);
        GameObject.Find("Jumala").GetComponent<DatabaseConnector>().UpdateDatabase("UPDATE Päivän SET TehtID = " + num + " WHERE ID = 1");
        Debug.Log("Onnistui");

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
        GameObject.Find("Jumala").GetComponent<DatabaseConnector>().UpdateDatabase("UPDATE Päivän SET TehtID = " + num + " WHERE ID = 2");

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
        GameObject.Find("Jumala").GetComponent<DatabaseConnector>().UpdateDatabase("UPDATE Päivän SET TehtID = " + num + " WHERE ID = 3");

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
        GameObject.Find("Jumala").GetComponent<DatabaseConnector>().UpdateDatabase("UPDATE Päivän SET TehtID = " + num + " WHERE ID = 4");

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
        GameObject.Find("Jumala").GetComponent<DatabaseConnector>().UpdateDatabase("UPDATE Päivän SET TehtID = " + num + " WHERE ID = 5");
    }

}
