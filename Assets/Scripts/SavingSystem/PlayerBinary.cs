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
            PlayerData data = new PlayerData(playerStats, playerStats);
        }
    }
}