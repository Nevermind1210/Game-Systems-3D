using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Saving
{
    [System.Serializable]
    public class PlayerData
    {
        public static PlayerData PlayerDataToSave;

        public string name;
        public int classIndex;
        public int raceIndex;
        public string raceName;

        public int[] stats;   
        public int[] visual;
    
        // We do be saving this
        public PlayerData (CustomizationSet player)
        {
            name = player.characterName;
            classIndex = player.selectedClassIndex;
            raceIndex = player.raceIndex;
            raceName = player.raceName;

            stats = new int[6];
            stats[0] = (player.characterStats[0].baseStats + player.characterStats[0].tempStats);
            stats[1] = (player.characterStats[1].baseStats + player.characterStats[1].tempStats);
            stats[2] = (player.characterStats[2].baseStats + player.characterStats[2].tempStats);
            stats[3] = (player.characterStats[3].baseStats + player.characterStats[3].tempStats);
            stats[4] = (player.characterStats[4].baseStats + player.characterStats[4].tempStats);
            stats[5] = (player.characterStats[5].baseStats + player.characterStats[5].tempStats);

            visual = new int[6];
            visual[0] = player.skinIndex;
            visual[1] = player.eyesIndex;
            visual[2] = player.mouthIndex;
            visual[3] = player.hairIndex;
            visual[4] = player.armourIndex;
            visual[5] = player.clothesIndex;
            
            PlayerDataToSave = this;
        }


    }
}