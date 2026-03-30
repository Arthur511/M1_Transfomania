using UnityEngine;

public class NPC_Skin : MonoBehaviour
{
    [SerializeField] private NPC_SkinsSO _data;
    [SerializeField] private SkinnedMeshRenderer _mesh;

    public void SetInstancedMaterial()
    {
        int index = Random.Range(0, _data.NPC_SkinTextures.Count);
        _mesh.material.mainTexture = _data.NPC_SkinTextures[index];
    }
}
