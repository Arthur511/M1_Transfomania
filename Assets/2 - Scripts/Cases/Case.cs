using UnityEngine;

public class Case : MonoBehaviour
{
    public Vector2Int CasePosition { get { return _casePosition; } set { _casePosition = value; }}
    public CaseTypeData CaseTypeData { get { return _caseTypeData; } set { _caseTypeData = value; } }

    private Vector2Int _casePosition;
    private CaseTypeData _caseTypeData;

}
