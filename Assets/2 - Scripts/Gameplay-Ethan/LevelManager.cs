using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class LevelManager : MonoBehaviour
{
    /*public LevelManager(TextAsset maping)
    {
        _maping = maping;
    }*/

    public Level_StateManager StateMachine { get; private set; }
    public enum BaseStateChoices { Pause, PlayerTurn, AITurn };
    public BaseStateChoices NPC_BaseState;
    public int[,] DistanceFromPlayer { get; set; }
    public Case[,] Map => _map;
    public Vector2Int[] NeighborDirection => _neighborDirection;
    public List<ChildNPC> Children => _children;


    [SerializeField] private TextAsset _maping;
    [SerializeField] private Vector3 _caseSize = new Vector3(1, 0.5f, 1);
    [SerializeField] private CaseTypeData[] _casesTypeDatas;
    [Header("Case's Materials")]
    [SerializeField] private Material _caseMat;
    [SerializeField] private Material _accessCaseMat;
    [SerializeField] List<ChildNPC> _children;


    private Case[,] _map;
    Vector2Int[] _neighborDirection = new Vector2Int[]
    {
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0),
        new Vector2Int(0, 1),
        new Vector2Int(0, -1)
    };
    List<Case> _currentAccessibleCases = new List<Case>();

    private List<GameObject> _debugTexts = new List<GameObject>();

    private Dictionary<BaseStateChoices, Action> _stateAction;
    private int _currentTurn = 0;


    private void Awake()
    {
        StateMachine = new Level_StateManager();
        GenerateLevel();
        InitStateAction();
        SetBaseState();
        StateMachine.SwitchState(new Level_State_PlayerTurn(this));
    }


    void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.FixedUpdateState();
    }

    private void GenerateLevel()
    {
        Dictionary<char, CaseTypeData> casesEntitesDict = new Dictionary<char, CaseTypeData>();

        foreach (var caseData in _casesTypeDatas)
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
                    GameObject gObj = Instantiate(casesEntitesDict[character].Prefab, new Vector3(x * _caseSize.x, 0, y * -_caseSize.z), Quaternion.identity);
                    gObj.transform.localScale = _caseSize;
                    _map[x, y] = gObj.GetComponent<Case>();
                    _map[x, y].CasePosition = new Vector2Int(x, y);

                    if (casesEntitesDict[character].CaseType == CaseTypeData.TypeOfCases.Spawn)
                    {
                        MainGame.Instance.PlayerController.transform.position = gObj.transform.position + new Vector3(0, 1, 0);
                        MainGame.Instance.PlayerController.PlayerPosition = new Vector2Int(x, y);
                    }
                }
            }
        }
    }



    private void InitStateAction()
    {
        _stateAction = new Dictionary<BaseStateChoices, Action>
        {
            {BaseStateChoices.Pause, () => StateMachine.Initialize(new Level_StatePause(this))},
            {BaseStateChoices.PlayerTurn,() => StateMachine.Initialize(new Level_State_PlayerTurn(this))},
            {BaseStateChoices.AITurn,() => StateMachine.Initialize(new Level_State_AITurn (this))},
        };
    }
    private void SetBaseState()
    {
        /*
        if (NPC_BaseState == BaseStateChoices.Pause)
            StateMachine.Initialize(new Level_StatePause(this));
        else if (NPC_BaseState == BaseStateChoices.PlayerTurn)
            StateMachine.Initialize(new Level_State_PlayerTurn(this));
        else if (NPC_BaseState == BaseStateChoices.AITurn)
            StateMachine.Initialize(new Level_State_AITurn (this));
        */

        if (_stateAction.TryGetValue(NPC_BaseState, out Action action))
            action();
    }

    public void CanPlayerMoveTo()
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

    public int[,] CalculateDistanceFromCase(PlayerController player)
    {
        int[,] values = new int[_map.GetLength(0), _map.GetLength(1)];
        Queue<Vector2Int> posCases = new Queue<Vector2Int>();
        bool[,] visited = new bool[_map.GetLength(0), _map.GetLength(1)];

        posCases.Enqueue(player.PlayerPosition);
        visited[player.PlayerPosition.x, player.PlayerPosition.y] = true;

        while (posCases.Count > 0)
        {
            var currentPos = posCases.Dequeue();
            foreach (var dir in _neighborDirection)
            {
                var neighbor = currentPos + dir;

                if (neighbor.x < 0 || neighbor.y < 0 || neighbor.x >= _map.GetLength(0) || neighbor.y >= _map.GetLength(1))
                { continue; }

                if (visited[neighbor.x, neighbor.y])
                { continue; }

                if (_map[neighbor.x, neighbor.y] != null)
                {
                    values[neighbor.x, neighbor.y] = values[currentPos.x, currentPos.y] + 1;
                    visited[neighbor.x, neighbor.y] = true;
                    posCases.Enqueue(neighbor);
                }
            }
        }
        return values;
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

    #region DEBUG
    public void DebugDistanceMap(int[,] values)
    {
        // Nettoyer les anciens textes
        foreach (var txt in _debugTexts)
            Destroy(txt);
        _debugTexts.Clear();

        for (int x = 0; x < values.GetLength(0); x++)
        {
            for (int y = 0; y < values.GetLength(1); y++)
            {
                if (_map[x, y] == null) continue;

                // Créer un GameObject avec TextMeshPro
                var go = new GameObject($"Debug_{x}_{y}");
                var tmp = go.AddComponent<TextMeshPro>();

                tmp.text = values[x, y].ToString();
                tmp.fontSize = 20;
                tmp.alignment = TextAlignmentOptions.Center;
                tmp.color = Color.white;

                // Positionner au-dessus de la case
                go.transform.position = _map[x, y].transform.position + Vector3.up * 0.5f;
                go.transform.rotation = Quaternion.Euler(90, 0, 0); // À plat sur le plateau

                _debugTexts.Add(go);
            }
        }
    }
    #endregion

    public void NextTurn()
    {
        if (StateMachine.CurrentState is Level_State_PlayerTurn)
            StateMachine.SwitchState(new Level_State_AITurn(this));
        else if (StateMachine.CurrentState is Level_State_AITurn)
            StateMachine.SwitchState(new Level_State_PlayerTurn(this));

        _currentTurn++;
    }

}