using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "Scripts Objectable/MonsterData", order = int.MaxValue)]
public class MonsterDataTest : ScriptableObject
{

    [SerializeField]
    private string monsterName;
    public string MonsterName { get { return monsterName; } }
    [SerializeField]
    private float monsterHp;
    public float MonsterHp { get {  return monsterHp; } }
    [SerializeField]
    private float monsterSpeed;
    public float MonsterSpeed { get {  return monsterSpeed; } }
    [SerializeField]
    private float monsterDamage;
    public float MonsterDamage { get {  return monsterDamage; } }
    [SerializeField]
    private GameObject monsterPrefab;
    public GameObject MonsterPrefab { get { return monsterPrefab; } }

}
