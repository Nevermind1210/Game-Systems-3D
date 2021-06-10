using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Player;

namespace Saving
{
    public static class PlayerBinary
    {
        public static void SavePlayerData(Transform playerTransform, PlayerStats playerStats)
        {
            //converting our game classes into something we can write to a file
            PlayerData data = new PlayerData(playerTransform, playerStats);


            //create a new binary formatter
            BinaryFormatter formatter = new BinaryFormatter();
            //location we are saving the file
            string path = Application.dataPath + "/BloodySave.this";
            //create a file at file path
            FileStream stream = new FileStream(path, FileMode.Create);

            //convert a class into a string that can be written to the file
            formatter.Serialize(stream, data);

            stream.Close();
        }
        
        public static PlayerData LoadPlayerData(Transform playerTransform, PlayerStats playerStats)
            {
                //location to read from (save location)
                string path = Application.dataPath + "/BloodySave.this";
        
                if(File.Exists(path))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    FileStream stream = new FileStream(path, FileMode.Open);
                    //this time we are deserializing the data
                    PlayerData data = (PlayerData) formatter.Deserialize(stream);
                    //PlayerData data = formatter.Deserialize(stream) as PlayerData;
                    stream.Close();
        
                    //load the data back into unity
                    data.LoadPlayerData(playerTransform, playerStats);
                    return data;
                }
                else
                {
                    return null;
                }
            }
    }
}