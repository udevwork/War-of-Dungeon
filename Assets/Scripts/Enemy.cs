using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Enemy/New enemy")]

public class Enemy : ScriptableObject {
	public Sprite enemyIcon;
	public string enemyName;
	public float health;
	public float damage;
	public float defense;

    public GameObject ObjectToSpawn;

	public float RadiusForFindPlayer;

	  [Header("DEATH REWARD")]
    public int rewardMoney;
    public int rewardPlatinum;
    public int rewardGem;
    public int rewardExperiense;
	
}
