using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance { get; private set; }

    public List<GameObject> InteractibleCharacters { get; private set; }

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
    /// Adds a character to the list of interactable characters.
    /// </summary>
    /// <param name="characterObject"></param>
    public void AddCharacter(GameObject characterObject)
    {
        if (InteractibleCharacters == null)
            InteractibleCharacters = new();

        InteractibleCharacters.Add(characterObject);
    }

    /// <summary>
    /// Shows the interactible characters.
    /// </summary>
    public void ShowInteractibleCharacters()
    {
        foreach (var characterObject in InteractibleCharacters)
        {
            var borderColor = characterObject.GetComponent<CharacterBorderColor>();
            var character = characterObject.GetComponent<Character>();
            character.SetComponentEnabled(borderColor, true);
        }
    }

    /// <summary>
    /// Activates the interactible characters.
    /// </summary>
    public void ActivateInteractibleCharacters()
    {
        foreach (var characterObject in InteractibleCharacters)
        {
            var borderColor = characterObject.GetComponent<CharacterBorderColor>();
            var character = characterObject.GetComponent<Character>();
            character.SetComponentEnabled(borderColor, true);

            var beingAttacked =
                characterObject.GetComponent<Character>().CharacterBeingAttacked;
            character.SetComponentEnabled(beingAttacked, true);
        }
    }

    /// <summary>
    /// Deactivates the interactable characters and sets the InteractableCharacters to null.
    /// </summary>
    public void DeactivateInteractibleCharacters()
    {
        if (InteractibleCharacters == null)
            return;

        if (InteractibleCharacters.Count == 0)
        {
            InteractibleCharacters = null;
            return;
        }

        foreach (var characterObject in InteractibleCharacters)
        {
            var character = characterObject.GetComponent<Character>();
            var borderColor = characterObject.GetComponent<CharacterBorderColor>();
            character.SetComponentEnabled(borderColor, false);

            var beingAttacked =
                characterObject.GetComponent<Character>().CharacterBeingAttacked;
            character.SetComponentEnabled(beingAttacked, false);
        }

        InteractibleCharacters = null;
    }

}
