using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TextAsset _maping;
    [SerializeField] private Vector3 _caseSize = new Vector3(1, 0.5f, 1);

    [SerializeField] private CaseData[] _casesDatas;
    private Case[,] _map;

    public LevelManager (TextAsset maping)
    {
        _maping = maping;
    }

    void Start()
    {
        GenerateLevel();
    }


    void Update()
    {
        
    }


    private void GenerateLevel()
    {
        Dictionary<char, CaseData> casesEntitesDict = new Dictionary<char, CaseData>();

        foreach (var caseData in _casesDatas)
        {
            casesEntitesDict.Add(caseData.Char, caseData);
        }

        string[] lines = _maping.text.Split("\r\n");
        _map = new Case[lines[0].Length, lines.Length];


        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            for (int x = 0; x < line.Length; x++)
            {
                char character = line[x];

                if (casesEntitesDict.ContainsKey(character))
                {
                    GameObject gObj = Instantiate(casesEntitesDict[character].Prefab, new Vector3(x * _caseSize.x, 0 , y * _caseSize.z), Quaternion.identity);
                    gObj.transform.localScale = _caseSize;
                    _map[x, y] = gObj.GetComponent<Case>();
                }
            }
        }
    }
}
