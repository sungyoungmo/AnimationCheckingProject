using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementalType
{
    Water,
    Fire,
    Grass
}

public class ngPlayerStatus : MonoBehaviour
{
    public int rootATK;

    public int rootSPD;

    public int rootDEF;

    public int maxHP;
    public int currentHP;

    public ElementalType playerType;
}
