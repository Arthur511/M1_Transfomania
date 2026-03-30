using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NPCSkinData", menuName = "NPC/NPC Skin Data", order = 1)]
public class NPC_SkinsSO : ScriptableObject
{
    public Material NPC_SkinMat;
    public List<Texture> NPC_SkinTextures;
}
