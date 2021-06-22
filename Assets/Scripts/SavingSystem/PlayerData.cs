using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Saving
{
    public class PlayerData
    {
        private int level;

        public float[] rotation;
        public float[] position;

        private Stats strength;
        private Stats dexterity;
        private Stats constitution;
        private Stats wisdom;
        private Stats intelligence;
        private Stats charisma;
        private Stats empathy;

        public PlayerData(Transform playerTransform, PlayerStats playerStats)
        {
            level = playerStats.level;
            
            strength        = playerStats.strength;
            dexterity       = playerStats.dexterity;
            constitution    = playerStats.constitution;
            wisdom          = playerStats.wisdom;
            intelligence    = playerStats.intelligence;
            charisma        = playerStats.charisma;
            empathy         = playerStats.empathy;
        }

        public void LoadPlayerData(Transform playerTransform, PlayerStats playerStats)
        {
            playerStats.level = level;

            playerStats.strength = strength;
            playerStats.dexterity = dexterity;
            playerStats.constitution = constitution;
            playerStats.wisdom = wisdom;
            playerStats.intelligence = intelligence;
            playerStats.charisma = charisma;
            playerStats.empathy = empathy;
        }

        void Start()
        {
            myClass aThing = new myClass();

            aThing.x = 4;
            cLass(aThing);
        }

        void cLass(myClass exampleClass)
        {
            exampleClass.x++;
        }
    }
    
    public class myClass
    {
        public int x;
    }
}