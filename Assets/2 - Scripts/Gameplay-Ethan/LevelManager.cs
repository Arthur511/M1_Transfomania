using System.Collections.Generic;
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
    List<Case> _currentAccessibleCases;

    public LevelManager(TextAsset maping)
    {
        _maping = maping;
    }
    
    private void Awake()
    {

        GenerateLevel();
    }


    void Start()
    {
        _neighborDirection = new Vector2Int[]
        {
            new Vector2Int(1, 0),
            new Vector2Int(-1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(0, -1)
        };
        _currentAccessibleCases = new List<Case>();
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
#if UNITY_EDITOR
            Debug.Log(_map[neighbor.x, neighbor.y]);
#endif
            if (_map[neighbor.x, neighbor.y].TryGetComponent<Case>(out Case _case))
            {
                _case.gameObject.GetComponent<MeshRenderer>().material = _accessCaseMat;
                _currentAccessibleCases.Add(_case);
            }
        }
    }

    public void ClearMatOnCases()
    {
#if UNITY_EDITOR
        Debug.Log(_currentAccessibleCases);
#endif
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