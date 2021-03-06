using System.Collections;
using System.Collections.Generic;
using Player;
using Saving;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class CustomisationGet : MonoBehaviour
{
   [Header("Character Properties")]
    [SerializeField] private Renderer characterRenderer;
    [SerializeField] public GameObject player;

    public Movement movement;
    public PlayerStats playerStats;

    [Header("Names")]
    public static string characterName;
    public static int classIndex;
    public static int raceIndex;
    public static string raceName;

    [Header("Stats")]
    public static int level;
    public static int healthMax;
    public static int healthRegen;
    public static int speed;
    public static int stamina;
    public static int manaMax;
    public static int manaRegen;

    [Header("Visual Textures")]
    public int[] visual = new int[6];

    // Start is called before the first frame update
    void Start()
    {
        // Testing Which load function to run based on bool from main menu
        if (!OptionMenu.loadData)
        {
            Load();
        }
        else if (OptionMenu.loadData)
        {
            LoadInGame();
        }
    }

    /// <summary>
    /// Loads all the saved player data from the customisation scene
    /// </summary>
    public void Load()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        visual[0] = data.visual[0];
        visual[1] = data.visual[1];
        visual[2] = data.visual[2];
        visual[3] = data.visual[3];
        visual[4] = data.visual[4];
        visual[5] = data.visual[5];
        
        characterName = data.name;
        classIndex = data.classIndex;
        raceIndex = data.raceIndex;
        raceName = data.raceName;

        healthMax = data.stats[0];
        healthRegen = data.stats[1];
        speed = data.stats[2];
        stamina = data.stats[3];
        manaMax = data.stats[4];
        manaRegen = data.stats[5];

        SetTexture("skin", visual[0]);
        SetTexture("eyes", visual[1]);
        SetTexture("mouth", visual[2]);
        SetTexture("hair", visual[3]);
        SetTexture("armour", visual[4]);
        SetTexture("clothes", visual[5]);

    }

    /// <summary>
    /// This saves all the updated information from the game
    /// </summary>
    public void SaveInGame()
    {
        SaveSystem.SavePlayerInGame(playerStats, Movement._movement , this);
        Debug.Log("Saved In Game");
    }

    /// <summary>
    /// This loads all the In game saved information and then updates the relevent scripts
    /// </summary>
    public void LoadInGame()
    {
        PlayerDataLoadGame data = SaveSystem.LoadInGame();

        SetTexture("skin", data.visual[0]);
        SetTexture("eyes", data.visual[1]);
        SetTexture("mouth", data.visual[2]);
        SetTexture("hair", data.visual[3]);
        SetTexture("armour", data.visual[4]);
        SetTexture("clothes", data.visual[5]);
        characterName = data.name;
        classIndex = data.classIndex;
        raceIndex = data.raceIndex;
        raceName = data.raceName;
        level = data.level;

        healthMax = data.health;
        healthRegen = data.healthRegen;
        speed = data.speed;
        stamina = data.stamina;
        manaMax = data.manaMax;
        manaRegen = data.manaRegen;

        //Debug.Log(speed); // I was debugging why the speed variable was not loading correctly. See Movement Journal.

        playerStats.SetValues();
        Movement._movement.SetStamValues();
    }
    
    void SetTexture(string type, int index)
    {
        Texture2D texture = null;
        int matIndex = 0;
        switch (type)
        {
            case "skin":
                texture = Resources.Load("Character/Skin_" + index) as Texture2D;
                matIndex = 1;
                break;
            case "eyes":
                texture = Resources.Load("Character/Eyes_" + index) as Texture2D;
                matIndex = 2;
                break;
            case "mouth":
                texture = Resources.Load("Character/Mouth_" + index) as Texture2D;
                matIndex = 3;
                break;
            case "hair":
                texture = Resources.Load("Character/Hair_" + index) as Texture2D;
                matIndex = 4;
                break;
            case "armour":
                texture = Resources.Load("Character/Armour_" + index) as Texture2D;
                matIndex = 5;
                break;
            case "clothes":
                texture = Resources.Load("Character/Clothes_" + index) as Texture2D;
                matIndex = 6;
                break;
        }
                
        Material[] mats = characterRenderer.materials;
        mats[matIndex].mainTexture = texture;
        characterRenderer.materials = mats;
    }
}

