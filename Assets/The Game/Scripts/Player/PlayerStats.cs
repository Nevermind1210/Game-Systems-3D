using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {
        public static PlayerStats CoolPlayerStats;
        
        [Header("Name")]
        public string characterName;
        [SerializeField] private TMP_Text nameText;

        [Header("Class")]
        public int classIndex;
        public string className;

        [Header("Race")]
        public int raceIndex;
        public string raceName;

        [SerializeField] private TMP_Text description;
        [SerializeField] private TMP_Text level;    
        public int levelInt;

        [Header ("Health")]
        [SerializeField] private Slider healthSlider;
        [SerializeField] private TMP_Text healthText;
        public int healthMax;
        public float health;
        public int healthRegen;
    
        [Header("Mana")]
        [SerializeField] private Slider manaSlider;
        [SerializeField] private TMP_Text manatext;
        public int manaMax;
        public float mana;
        public int manaRegen;

        [Header("Damage UI")]
        [SerializeField] private Transform popUpLocation;
        [SerializeField] private GameObject damagePrefab;
        [SerializeField] private TMP_Text damageText;
        [SerializeField] private float damage = 5f;
        
        [Header("Death UI")]
        [SerializeField] private GameObject deathPanel;
        [SerializeField] private AudioSource deathSound;
        private bool isDead = false;

        private void Awake()
        {
            if (CoolPlayerStats == null)
            {
                CoolPlayerStats = this;
            }
            else
            {
                Destroy(this);
            }
        }


        private void Start()
        {
            SetValues();
            ClassName(classIndex);

            description.text = raceName + " " + className;        
            nameText.text = characterName;
            deathPanel.SetActive(false);
            isDead = false;
        }

        private void Update()
        {
            UseMana();  
            UpdateSliders();
            GetHurt();
            LevelUp();
        }

        #region PlayerVisuals

        

        #endregion
        private void LevelUp()
        {
            if (Input.GetButtonDown("LevelUp"))
            {
                levelInt++;
                healthMax += Mathf.RoundToInt(healthMax * 0.55f);
                manaMax += Mathf.RoundToInt(manaMax * 0.55f);
                level.text = "Level:" + levelInt;
            }
        }

        private void GetHurt()
        {
            if (Input.GetButtonDown("Damage"))
            {
                health -= damage;
                GameObject popUp = Instantiate(damagePrefab, popUpLocation);
                damageText = popUp.GetComponentInChildren<TMP_Text>();
                damageText.text = damage.ToString("0");            
                Destroy(popUp, .5f);
            }
            if(health < healthMax)
            {
                health += (healthRegen*0.1f) * Time.deltaTime;
            }
        
            if (health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (!isDead)
            {
                deathSound.Play();
                Debug.Log("Oh no I killed myself... why.... aaa....no...");
                deathPanel.SetActive(true);
                isDead = true;
            }        
        }
        
        // Send feedback to the player that things are regenerating.
        private void UpdateSliders()
        {
            healthSlider.maxValue = healthMax;
            healthSlider.value = health;
            healthText.text = "Health: " + Mathf.RoundToInt(health) + "/" + healthMax;

            manaSlider.maxValue = manaMax;
            manaSlider.value = mana;
            manatext.text = "Mana: " + Mathf.RoundToInt(mana) + "/" + manaMax;
        }
        
        //Literally does nothing... But its nifty!
        private void UseMana()
        {
            if (Input.GetButton("Cast") && mana > 0)
            {
                mana -= Time.deltaTime;
            }
            if(mana < manaMax && !Input.GetButton("Cast"))
            {
                mana += (manaRegen * 0.1f) * Time.deltaTime;
            }
        }

        #region ValuesSets

        private void ClassName(int i)
        {
            switch (i)
            {
                case 0:
                    className = "Barbarian";
                    break;
                case 1:
                    className = "Ranger";
                    break;
                case 2:
                    className = "Mage";
                    break;
            }
        }

        public void SetValues()
        {
            characterName = CostominsationGet.characterName;
            classIndex = CostominsationGet.classIndex;
            raceIndex = CostominsationGet.raceIndex;
            raceName = CostominsationGet.raceName;

            levelInt = CostominsationGet.level;
            level.text = "Level: " + levelInt;
            healthMax = CostominsationGet.healthMax;
            health = healthMax;
            healthRegen = CostominsationGet.healthRegen;
            healthSlider.maxValue = healthMax;
            manaMax = CostominsationGet.manaMax;
            mana = manaMax;
            manaRegen = CostominsationGet.manaRegen;
            manaSlider.maxValue = manaMax;
        }

        #endregion
        

   
    }
}