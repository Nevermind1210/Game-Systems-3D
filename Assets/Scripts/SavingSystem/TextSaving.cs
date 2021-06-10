using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TextSaving : MonoBehaviour
{
    int scoreA = 5;
    int scoreB = 2;

    private void Start() => StreamingToTextFile("Score = " + (scoreA + scoreB) + "\n");
    
    void CreateText()
    {
        //Path to where the project is
        string path = Application.dataPath + "/Log.txt"; 
        //Path to somewhere inside the OS
        //string path2 = Application.persistentDataPath + "/Log.txt";

        if(!File.Exists(path))
        {
            File.WriteAllText(path, "AYE AYE CAPTIN' \n\n");
        }

        string content = "Captin's log star date: " + System.DateTime.Now + "\n";
        File.AppendAllText(path, content);
    }

    void StreamingToTextFile(string data)
    {
        string path = Application.dataPath + "/stream.txt";

        //true would be writing to the end of the file
        //false is rewriting the file
        StreamWriter writer = new StreamWriter(path,true);
        writer.Write(data);
        writer.Close();


        StreamReader reader = new StreamReader(path);
        string line = reader.ReadLine();

        Debug.Log(line);

        string lineB = reader.ReadToEnd();
        Debug.Log(lineB);
        reader.Close();
    }
}
