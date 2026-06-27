using UnityEngine;
public abstract class ActionBase
{
    protected ActionPanel actionPanel { get; private set; }
    protected GameObject characterObject { get; private set; }
    protected Character character => characterObject.GetComponent<Character>();

    protected int activeSkillIndex { get; set; } = 0;


    /// <summary>
    /// Sets data when the constructor has been created.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="characterObject"></param>
    public ActionBase(ActionPanel actionPanel, GameObject characterObject)
    {
        this.actionPanel = actionPanel;
        this.characterObject = characterObject;
    }

    /// <summary>
    /// Checks the dice condition, appended to the dice's valid number.
    /// </summary>
    /// <param name="dice"></param>
    /// <exception cref="NotImplementedException"></exception>
    public virtual bool IsValid(int diceNumber)
    {
        return CheckDiceCondition.IsNumberValid(actionPanel.ActionData.AllowedDiceNumber, diceNumber);
    }

    /// <summary>
    /// Sets the interactible objects.
    /// </summary>
    /// <param name="diceNumber"></param>
    public abstract bool SetInteractible(int diceNumber);

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
        PopUpAction.Instance.SetData(actionPanel.ActionData.Description);
    }

    public virtual void SetDefault()
    {
        activeSkillIndex = 0;
    }
}

