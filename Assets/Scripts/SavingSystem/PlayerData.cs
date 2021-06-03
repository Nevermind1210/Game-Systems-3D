using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Saving
{
    public class PlayerData
    {
        public int level;

        public float[] rotation;

        public Stats strength;
        public Stats dexterity;
        public Stats constitution;
        public Stats wisdom;
        public Stats intelligence;
        public Stats charisma;
        public Stats empathy;

        public PlayerData(Transform playerTransform, PlayerStats playerStats)
        {
            level = playerStats.level;
            
            strength        = playerStats.strength;
            dexterity       = playerStats.dexterity;
            constitution    = playerStats.constitution;
            wisdom          = playerStats.wisdom;
            intelligence    = playerStats.intelligence;
            charisma        = playerStats.charisma;
            empathy         = playerStats.
        }
        
    }

    public class myClass
    {
        public int x;
    }
}