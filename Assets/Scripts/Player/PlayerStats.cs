using System;
using System.Collections;
using System.Collections.Generic;
using Saving;
using UnityEngine;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField] private PlayerProfession[] playerProfessions;

        private PlayerProfession _profession;
        public PlayerProfession Profession
        {
            get { return _profession; }
            set 
            { 
                _profession = value;
                strength.defaultStat        = _profession.strength;
                dexterity.defaultStat       = _profession.dexterity;
                constitution.defaultStat    = _profession.constitution;
                wisdom.defaultStat          = _profession.wisdom;
                intelligence.defaultStat    = _profession.intelligence;
                charisma.defaultStat        = _profession.charisma;
                empathy.defaultStat         = _profession.empathy;
                UpdateStats();
            }
        }


        public enum BaseStats { Strength, Dexterity, Constitution, Wisdom, Intelligence, Charisma,
            Empathy
        };

        public int level;
        public int pointPool = 5;

        [Header("Base Stats")]
        public Stats strength;        //Health + Health regen
        public Stats dexterity;      //Movement Speed
        public Stats constitution;   //Stamina + Stamina regen
        public Stats wisdom;         //Mana pool + Mana regen
        public Stats intelligence;   //Spell casting damage
        public Stats charisma;       //Personality  
        public Stats empathy;        //Sense feelings

        [Header("Health")]
        public float maxHealth; //class Base Health + (strength * 0.5f);
        public float currentHealth;
        public float healthRegen;

        [Header("Mana")]
        public float maxMana;
        public float currentMana;
        public float manaRegen;

        [Header("Movement")]
        public float movementSpeed;
        public float sprintSpeed;
        public float crouchSpeed;
        public float jump;
        public float maxStamina;
        public float currentStamina;
        public float staminaRegen;

        private void UpdateStats()
        {
            maxHealth = strength.totalStat * 6;
            healthRegen = strength.totalStat * 0.1f;

            maxMana = wisdom.totalStat * 4;
            manaRegen = wisdom.totalStat * 0.1f;

            movementSpeed = dexterity.defaultStat * 10;
        }

        public Stats EnumToStats(BaseStats baseStats)
        {
            switch (baseStats)
            {
                case BaseStats.Strength:
                    return strength;
                case BaseStats.Dexterity:
                    return dexterity;
                case BaseStats.Constitution:
                    return constitution;
                case BaseStats.Wisdom:
                    return wisdom;
                case BaseStats.Intelligence:
                    return intelligence;
                case BaseStats.Charisma:
                    return charisma;
                case BaseStats.Empathy:
                    return empathy;
            }
            return new Stats();
        }

        public bool ChangeStats(BaseStats baseStat, int amount)
        {
            Stats stats = EnumToStats(baseStat);

            if (amount > 0 && pointPool - amount < 0)
            {
                return false;
            }
            else if (amount < 0 && stats.pooledStat + amount < 0)
            {
                return false;
            }
            stats.pooledStat += amount;
            pointPool -= amount;
            return true;
        }

        private void OnGUI()
        {
            StatsOnGUI();
            ProfessionOnGUI();
            SaveOnGUI();
        }

        private void SaveOnGUI()
        {
            // if(GUI.Button(new Rect(150,10,100,20),"Save"))
            // {
            //     PlayerBinary.SavePlayerData(transform, this);
            // }
            //
            // if (GUI.Button(new Rect(150, 40, 100, 20), "Load"))
            // {
            //     PlayerData playerData =  PlayerBinary.LoadPlayerData(transform, this);
            // }
        }


        Vector2 scrollPosition;
        private void ProfessionOnGUI()
        {
            float currentY = Screen.height * 0.5f;

            GUI.Box(new Rect(Screen.width - 170, currentY, 155, 80), "Profession");

            currentY += 20;
            scrollPosition = GUI.BeginScrollView(new Rect(Screen.width -170, currentY,155,50)
                , scrollPosition
                , new Rect(0,0,100,30 * playerProfessions.Length));

            currentY = 0;
            foreach (PlayerProfession profession in playerProfessions)
            {
                if(GUI.Button(new Rect(20, currentY,100,20),profession.ProfessionName))
                {
                    Profession = profession;
                }
                currentY += 30;
            }

            GUI.EndScrollView();

            if (_profession != null)
            {
                GUI.Box(new Rect(Screen.width - 170, Screen.height - 110, 155, 100), "Profession");
                GUI.Label(new Rect(Screen.width - 140, Screen.height - 120 + 30, 100, 20), _profession.ProfessionName);
                GUI.Label(new Rect(Screen.width - 140, Screen.height - 120 + 45, 100, 20), _profession.AbilityName);

                GUIStyle newStyle = new GUIStyle();
                newStyle.wordWrap = true;
                newStyle.normal.textColor = Color.white;
                GUI.Label(new Rect(Screen.width - 140, Screen.height - 120 + 65, 100, 60), _profession.AbilityDescription, newStyle);
            }
        }

        private void StatsOnGUI()
        {
            float currentY = 40;

            GUI.Box(new Rect(Screen.width - 170, 10, 155, 210), "Stats Pool : " + pointPool);

            foreach (BaseStats baseStats in Enum.GetValues(typeof(BaseStats)))
            {
                Stats stats = EnumToStats(baseStats);

                if (GUI.Button(new Rect(Screen.width - 165, currentY, 20, 20), "-"))
                {
                    ChangeStats(baseStats, -1);
                }
                GUI.Label(new Rect(Screen.width - 140, currentY, 100, 20), baseStats.ToString() + " :" + stats.totalStat);
                if (GUI.Button(new Rect(Screen.width - 40, currentY, 20, 20), "+"))
                {
                    ChangeStats(baseStats, 1);
                }
                currentY += 30;
            }
        }

        public void StatUp(string name)
        {
            BaseStats stat = (BaseStats) Enum.Parse(typeof(BaseStats), name);

            ChangeStats(stat, 1);

        
        }
    }
}

[System.Serializable]
public class Stats
{
    //string statName;
    public int defaultStat;
    public int pooledStat;

    public int totalStat
    {
        get
        {
            return defaultStat + pooledStat;
        }

    }
}