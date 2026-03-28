using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
[Serializable]
public class AnimationEntry
{
    [Tooltip("Nom exact du paramètre dans l'Animator Controller")]
    public string parameterName;

    [Tooltip("Type du paramètre dans l'Animator")]
    public AnimatorControllerParameterType parameterType;

    [Tooltip("Valeur float si le type est Float")]
    public float floatValue;

    [Tooltip("Valeur int si le type est Int")]
    public int intValue;

    [Tooltip("Valeur bool si le type est Bool")]
    public bool boolValue;

    [Tooltip("Durée de transition CrossFade en secondes. 0 = SetTrigger classique.")]
    [Range(0f, 1f)]
    public float crossFadeDuration = 0.1f;

    [Tooltip("Layer de l'Animator ciblé par ce clip (-1 = tous)")]
    public int layer = 0;

    [NonSerialized] public int hash;

    public void CacheHash() => hash = Animator.StringToHash(parameterName);
}

/// <summary>
/// Parent ScriptableObject who contains animations (indexed by key, enum or string)
/// </summary>
public abstract class AnimationDataSO : ScriptableObject
{
    [Header("Global Configuration")]
    [Tooltip("Durée de transition par défaut si l'AnimationEntry ne la spécifie pas")]
    [Range(0f, 0.5f)] public float defaultCrossFadeDuration = 0.1f;

    [Tooltip("Vitesse globale de l'Animator")]
    [Range(0f, 3f)] public float globalSpeed = 1f;

    [Header("Catalogue d'animations")]
    [SerializeField] protected List<AnimationEntry> entries = new();

    private Dictionary<string, AnimationEntry> _lookup;

    public void Initialize()
    {
        _lookup = new Dictionary<string, AnimationEntry>(entries.Count);
        foreach (var entry in entries)
        {
            entry.CacheHash();
            if (!_lookup.ContainsKey(entry.parameterName))
                _lookup[entry.parameterName] = entry;
            else
                Debug.LogWarning($"[{name}] Detected animation double : '{entry.parameterName}'");
        }
    }

    /// <summary>Get entry by parameter name</summary>
    public bool TryGet(string paramName, out AnimationEntry entry)
    {
        if (_lookup == null) Initialize();
        return _lookup.TryGetValue(paramName, out entry);
    }

    /// <summary>Get entry by index</summary>
    public AnimationEntry GetAt(int index)
    {
        if (index < 0 || index >= entries.Count)
        {
            Debug.LogError($"[{name}] Animation index outside of bounds : {index}");
            return null;
        }
        return entries[index];
    }

    public int Count => entries.Count;

    private void OnEnable() => Initialize();
}
