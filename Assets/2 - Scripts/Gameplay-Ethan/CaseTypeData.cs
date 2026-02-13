using System;
using UnityEngine;

[Serializable]
public class CaseTypeData
{
    public char Char;
    public GameObject Prefab;
    public TypeOfCases CaseType;

    public enum TypeOfCases
    {
        Walkable,
        Spawn,
        Broken,
        OnlyPlayer,
        OnlyEnemies,
    }
}
