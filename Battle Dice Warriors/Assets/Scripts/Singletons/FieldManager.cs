using System;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public static FieldManager Instance { get; private set; }

    public GameObject[,] Fields { get; private set; }
    public List<GameObject> InteractibleFields { get; private set; }

    /// <summary>
    /// Awake method.
    /// </summary>
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
    }

    /// <summary>
    /// Initializes the size of the array Fields.
    /// </summary>
    public void InitializeFields()
    {
        int mapHeight = LevelManager.Instance.Data.MapHeight;
        int mapLength = LevelManager.Instance.Data.MapLength;

        Fields = new GameObject[mapHeight, mapLength];
    }

    /// <summary>
    /// Initializes the index of the array Fields and sets the index to the field.
    /// </summary>
    /// <param name="characterPrefab"></param>
    public void SetField(GameObject fieldObject, int j, int i)
    {
        Fields[j, i] = fieldObject;

        Vector2Int index = new Vector2Int(j, i);

        var field = fieldObject.GetComponent<Field>();
        field.SetIndex(index);
        field.SetComponentEnabled(field.GetComponent<FieldMouseEvent>(), false);

    }

    /// <summary>
    /// Adds a field object to the collection of interactable fields.
    /// </summary>
    /// <param name="fieldObject">The field object to add. This parameter cannot be <see langword="null"/>.</param>
    public void AddInteractibleField(GameObject fieldObject)
    {
        if (InteractibleFields == null)
            InteractibleFields = new();

        InteractibleFields.Add(fieldObject);
    }

    /// <summary>
    /// Shows the interactible fields.
    /// </summary>
    /// <param name="characterFieldIndexOrigin"></param>
    /// <param name="actionDirections"></param>
    /// <param name="directionRange"></param>
    public void ShowInteractibleFields()
    {
        foreach (var fieldObject in InteractibleFields)
        {
            var field = fieldObject.GetComponent<FieldMouseEvent>();
            field.AnimationHint.SetActive(true);
        }
    }

    public void ActivateInactibleFields()
    {
        foreach (var fieldObject in InteractibleFields)
        {
            var field = fieldObject.GetComponent<Field>();
            field.SetComponentEnabled(field.GetComponent<FieldMouseEvent>(), true);
        }
    }

    /// <summary>
    /// Deactivates the interactable fields and sets the InteractableFields to null.
    /// </summary>
    /// <param name="clickedField"></param>
    public void DeactivateInteractibleFields()
    {
        BattleController.Instance.ActiveAction = null;

        if (InteractibleFields == null)
            return;

        if (InteractibleFields.Count == 0)
        {
            InteractibleFields = null;
            return;
        }

        // Deactivates the interaction of each field in InteractibleFields
        foreach (var fieldObject in InteractibleFields)
        {
            var field = fieldObject.GetComponent<Field>();
            var mouseEvent = fieldObject.GetComponent<FieldMouseEvent>();
            mouseEvent.AnimationHint.SetActive(false);
            field.SetComponentEnabled(mouseEvent, false);
        }

        InteractibleFields = null;
    }

    /// <summary>
    /// Check if the field index is within bounds
    /// </summary>
    /// <param name="fieldIndex"></param>
    /// <returns></returns>
    public bool IsTargetOutOfMap(Vector2Int fieldIndex)
    {
        if (fieldIndex.x < 0 || fieldIndex.x >= LevelManager.Instance.Data.MapHeight ||
            fieldIndex.y < 0 || fieldIndex.y >= LevelManager.Instance.Data.MapLength)
            return true;

        return false;
    }

}
