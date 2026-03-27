using System;
using UnityEngine;

[Serializable]
public class CaseTypeData
{
    public char Char;
    public GameObject Prefab;
    public TypeOfCases CaseType;
    public ContentOfCases CaseContent;

    public enum TypeOfCases
    {
        Walkable,
        Spawn,
        //SpawnEnemies,
        //Lolipop,
        Door
    }

    public enum ContentOfCases
    {
        None,
        Enemy,
        Lolipop,
    }
}
