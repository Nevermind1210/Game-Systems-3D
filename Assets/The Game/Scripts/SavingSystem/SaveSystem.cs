using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Player;
using UnityEngine;

namespace Saving
{
    public static class SaveSystem 
    {
        // This functions serves to be called and to save whatever data inside PlayerData. Used after customisation and New Game
        public static void SavePlayer(CustominsationSet player)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/PlayerData.save";
            FileStream stream = new FileStream(path, FileMode.Create);

            PlayerData data = new PlayerData(player);
        
            // Then freeze it and serialize it.
            formatter.Serialize(stream, data);
            stream.Close();
        }
        // Nothing crazy open it and then deserialize it
        public static PlayerData LoadPlayer()
        {
            string path = Application.persistentDataPath + "/PlayerData.save";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                PlayerData data =  formatter.Deserialize(stream) as PlayerData;
                stream.Close();
            
                return data;

            }
            else
            {
                Debug.LogError("Save file haven't been discovered in ->" + path);
                return null;
            }
        }
        
        public static void SavePlayerInGame(PlayerStats playerStats, Movement movement, CustominsationGet)
    }
    
   
}