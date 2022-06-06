using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVEditor : Singleton<CSVEditor>
{
    string filepath;
    StreamWriter editor;
    StreamReader reader;
    public List<Response> dataFromFile;
    public List<Response> dataFromServer;
    string path;
        // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Initialized");
    }

    private void OnEnable()
    {
        path = Application.dataPath;
        if (Application.platform == RuntimePlatform.OSXPlayer)
        {
            path += "/../../";
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            path += "/../";
        }
        filepath = path + "\\csv.csv";
        //filepath = "D:\\RayQubeProjects\\JigsawPuzzle\\Builds\\V1.0\\csv.csv";
        Debug.Log(filepath);
        readFromFile();
    }

    public void checkOnServerEntries()
    {
        Debug.Log("Checking server entries: " + dataFromFile.Count);
        if (APIHandler.Instance.checkInternet())
        {
            Debug.Log("Checking");
            foreach (Response r in dataFromFile)
            {
                if (!r.isOnServer)
                {
                    Debug.Log("Found unregistered player!");
                    Credentials c = new Credentials();
                    c.name = r.name;
                    c.email = r.email;
                    c.phone = r.phone;
                    c.score = r.score;
                    APIHandler.Instance.sendUserStats(c);
                }
            }
        }
       // Debug.Log("Reading Data from Server again");
        //dataFromServer = APIHandler.Instance.getAllUsers();
        Debug.Log("Recieved data: " + dataFromServer.Count);
        //writeOnFile(APIHandler.Instance.root);
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void writeOnFile(Response[] allUsers)
    {
        string data = "";
        Debug.Log("Writing");
        editor = new StreamWriter(filepath, false);
        data = "Name, Phone, Email, Score, Rank, isOnServer \n";
        //editor.WriteLine("Name, Phone, Email, Score, Rank, isOnServer");
        //editor.Close();

        //editor = new StreamWriter(filepath, true);
        for(int i = 0; i < allUsers.Length; i++)
        {
            data += allUsers[i].name + ", " + allUsers[i].phone + ", " + allUsers[i].email + ", " + allUsers[i].score + ", " + allUsers[i].rank + ", " + allUsers[i].isOnServer + "\n";
            //editor.WriteLine(allUsers[i].name + ", " + allUsers[i].phone + ", " + allUsers[i].email + ", " + allUsers[i].score + ", "+allUsers[i].rank+ ", " + allUsers[i].isOnServer);
        }
        editor.WriteLine(data);
        editor.Flush();
        editor.Close();
        dataFromFile = readFromFile();
    }

    public List<Response> readFromFile()
    {
        Debug.Log("Reading from file");
        dataFromFile = new List<Response>();
        reader = new StreamReader(filepath);
        bool endOfFile = false;
        string line = reader.ReadLine();
        if (line == null)
        {
            reader.Close();
            return null;
        }
        while (!endOfFile)
        {
            string Data = reader.ReadLine();
            if (Data == null)
            {
                endOfFile = true;
                break;
            }
            if (Data != "")
            {
                var data_Values = Data.Split(',');
                Response temp = new Response();
                temp.name = data_Values[0].ToString();
                temp.phone = data_Values[1].ToString();
                temp.email = data_Values[2].ToString();
                temp.score = int.Parse(data_Values[3]);
                temp.rank = int.Parse(data_Values[4]);
                temp.isOnServer = bool.Parse(data_Values[5]);
                dataFromFile.Add(temp);
            }
        }
        reader.Close();
        //checkOnServerEntries();
        Debug.Log("Reading from file completed");
        return dataFromFile;
    }


}
