using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public enum monsterType
{
    fire,
    grass,
    water,
    normal
}

[CreateAssetMenu(fileName = "ngMonsterTypeName", menuName = "Monster_ScriptableObject/ngMonsterType")]
public class ngMonsterType : ScriptableObject
{
    public List<monsterType> strength;
    public List<monsterType> weakness;
    public List<monsterType> neutral;
}