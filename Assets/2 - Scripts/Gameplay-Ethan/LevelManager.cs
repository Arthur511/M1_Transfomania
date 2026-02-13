using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TextAsset _maping;
    [SerializeField] private Vector3 _caseSize = new Vector3(1, 0.5f, 1);
    [SerializeField] private CaseData[] _casesDatas;
    [Header("Case's Materials")]
    [SerializeField] private Material _caseMat;
    [SerializeField] private Material _accessCaseMat;

    private Case[,] _map;
    Vector2Int[] _neighborDirection = new Vector2Int[]
    {
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0),
        new Vector2Int(0, 1),
        new Vector2Int(0, -1)
    };
    List<Case> _currentAccessibleCases = new List<Case>();

    /*public LevelManager(TextAsset maping)
    {
        _maping = maping;
    }*/
    
    private void Awake()
    {

        GenerateLevel();
    }


    void Start()
    {
        
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
                    GameObject gObj = Instantiate(casesEntitesDict[character].Prefab, new Vector3(x * _caseSize.x, 0, y * _caseSize.z), Quaternion.identity);
                    gObj.transform.localScale = _caseSize;
                    _map[x, y] = gObj.GetComponent<Case>();
                    _map[x, y].CasePosition = new Vector2Int(x, y);
                }
            }
        }
    }

    public void CanMove()
    {
        foreach (var dir in _neighborDirection)
        {
            var neighbor = MainGame.Instance.PlayerController.PlayerPosition + dir;

            if (neighbor.x < 0 || neighbor.y < 0 || neighbor.x >= _map.GetLength(0) || neighbor.y >= _map.GetLength(1))
                { continue; }

            if (_map[neighbor.x, neighbor.y] != null)
            {
                _map[neighbor.x, neighbor.y].gameObject.GetComponent<MeshRenderer>().material = _accessCaseMat;
                _currentAccessibleCases.Add(_map[neighbor.x, neighbor.y]);
            }
        }
    }

    public void ClearMatOnCases()
    {
        if (_currentAccessibleCases.Count > 0)
        {
            foreach (var _case in _currentAccessibleCases)
            {
                _case.gameObject.GetComponent<MeshRenderer>().material = _caseMat;
            }
            _currentAccessibleCases.Clear();
        }
    }
}