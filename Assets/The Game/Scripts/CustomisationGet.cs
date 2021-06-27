using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class CostominsationGet : MonoBehaviour
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

  
}
