using UnityEngine;
public abstract class ActionBase
{
    protected ActionPanel ActionPanel { get; private set; }
    protected GameObject CharacterObject { get; private set; }
    protected Character Character => CharacterObject.GetComponent<Character>();

    protected int ActiveSkillIndex { get; set; } = 0;

    /// <summary>
    /// Sets data when the constructor has been created.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="characterObject"></param>
    public ActionBase(ActionPanel actionPanel, GameObject characterObject)
    {
        ActionPanel = actionPanel;
        CharacterObject = characterObject;
    }

    /// <summary>
    /// Checks the dice condition, appended to the dice's valid number.
    /// </summary>
    /// <param name="dice"></param>
    /// <exception cref="NotImplementedException"></exception>
    public virtual bool IsValid(int diceNumber)
    {
        return CheckDiceCondition.IsNumberValid(ActionPanel.ActionData.AllowedDiceNumber, diceNumber);
    }

    /// <summary>
    /// Finds the interactible objects.
    /// </summary>
    /// <param name="diceNumber"></param>
    public abstract bool FindInteractible(int diceNumber);

    /// <summary>
    /// Shows the interactible objects.
    /// </summary>
    public abstract void ShowInteractible();

    /// <summary>
    /// Activates the interactible objects.
    /// </summary>
    public abstract void ActivateInteractible();

    /// <summary>
    /// Activates the skill of the action.
    /// </summary>
    public virtual void ActivateSkill(int diceNumber)
    { }

    /// <summary>
    /// Processes the input of player.
    /// </summary>
    /// <param name="fieldObject"></param>
    public abstract void ProcessInput(GameObject fieldObject);

    /// <summary>
    /// Updates the hit endurance for defend action.
    /// </summary>
    public virtual void UpdateHitEnduranceForDefend()
    { }

    /// <summary>
    /// Counts down the round endurance.
    /// </summary>
    public virtual void CountDownRoundEndurance(PlayerType lastTurn)
    { }

    /// <summary>
    /// Sets the description of the action for popup based on dice number or not.
    /// </summary>
    /// <param name="diceNumber"></param>
    public virtual void SetDataPopUp(int diceNumber)
    {
        PopUpAction.Instance.SetData(ActionPanel.ActionData.Description);
    }

    public virtual void SetDefault()
    {
        ActiveSkillIndex = 0;
    }

    /// <summary>
    /// Finds the first target within a specified range and direction from the given origin field index.
    /// </summary>
    /// <remarks>This method iterates through fields in the specified direction and range, skipping fields
    /// that are out of bounds.  The search stops as soon as a valid target is found. If no target is found within the
    /// range, the method returns <see langword="null"/>.</remarks>
    /// <param name="characterFieldIndexOrigin">The starting field index of the character, represented as a 2D grid coordinate.</param>
    /// <param name="actionDirection">The direction in which to search for the target, represented as a 2D vector.</param>
    /// <param name="range">The maximum number of fields to search in the specified direction. Must be a positive integer.</param>
    /// <param name="objectManager">An object that defines the logic for identifying the target within a field.</param>
    /// <returns>The first <see cref="GameObject"/> found within the specified range and direction that matches the criteria
    /// defined by  <paramref name="objectManager"/>. Returns <see langword="null"/> if no target is found.</returns>
    public GameObject FindEnemy(Vector2Int characterFieldIndexOrigin,
       Vector2Int actionDirection, int range)
    {
        for (int i = 1; i <= range; i++)
        {
            var fieldIndex = characterFieldIndexOrigin;
            fieldIndex += actionDirection * i;

            if (FieldManager.Instance.IsTargetOutOfMap(fieldIndex))
                return null;

            var field = FieldManager.Instance.Fields[fieldIndex.x, fieldIndex.y].
            GetComponent<Field>();

            GameObject target = field.EnemyObject(Character.Player.PlayerType);

            if (target == null)
                continue;

            return target;
        }

        return null;
    }

}

