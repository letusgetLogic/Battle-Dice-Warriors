using System;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    public float CurrentAP { get; set; }
    public float CurrentBuffAP { get; set; }
    public string CurrentBuffAPText { get; set; }
    public string InfoText { get; set; } = string.Empty;
    public float OriginAP => GetComponent<Character>().Data.AP;

    /// <summary>
    /// Sets the value default.
    /// </summary>
    public void SetDefault()
    {
        CurrentAP = OriginAP;
        CurrentBuffAP = 0f;
        InfoText = string.Empty;
    }

}

