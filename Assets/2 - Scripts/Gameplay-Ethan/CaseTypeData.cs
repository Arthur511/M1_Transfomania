using System;
using UnityEngine;

[Serializable]
public class CaseTypeData
{
    public char Char;
    public GameObject Prefab;
    public TypeOfCases CaseType;
    public Content CaseContent;

    public enum TypeOfCases
    {
        Walkable,
        Spawn,
        SpawnEnemies,
        Lolipop,
        Door
    }

    public enum Content
    {
        None,
        Enemy,
        Lolipop,
    }
}
