using Player;
using UnityEngine;
[System.Serializable]
public class PlayerDataLoadGame
{
    private PlayerDataLoadGame ThePlayerDataLoadGame;
    
    public string name;
    public int level;
    public int classIndex;
    public int raceIndex;
    public string raceName;

    public int health;
    public int healthRegen;
    public int manaMax;
    public int manaRegen;
    public int stamina;
    public int speed;

    public int[] visual;
    
    public PlayerDataLoadGame(PlayerStats player, Movement movement,CostominsationGet data)
    {

        name = player.characterName;
        level = player.levelInt;
        classIndex = player.classIndex;
        raceIndex = player.raceIndex;
        raceName = player.raceName;

        health = player.healthMax;
        healthRegen = player.healthRegen;
        manaMax = player.manaMax;
        manaRegen = player.manaRegen;

        stamina = movement.staminaMax;
        speed = movement.baseSpeed * 2;

        
        visual = new int[6];
        visual[0] = data.visual[0];
        visual[1] = data.visual[1];
        visual[2] = data.visual[2];
        visual[3] = data.visual[3];
        visual[4] = data.visual[4];
        visual[5] = data.visual[5];

        ThePlayerDataLoadGame = this;
    }
    
}

