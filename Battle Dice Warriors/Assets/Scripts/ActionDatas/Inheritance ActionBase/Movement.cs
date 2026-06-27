using UnityEngine;

public class Movement : ActionBase
{
    public Movement(ActionPanel actionPanel, GameObject characterObject) :
        base(actionPanel, characterObject)
    {}
    public override void SetDataPopUp(int index)
    {
        // Dragging nothing and dice is on slot
        if (index == 0 && activeSkillIndex != 0)
        {
            PopUpAction.Instance.SetData(Description(activeSkillIndex));
            return;
        }

        // Dragging a dice
        if (index > 0 && CheckDiceCondition.IsNumberValid(actionPanel.ActionData.AllowedDiceNumber, index))
        {
            PopUpAction.Instance.SetData(Description(index));
            return;
        }

        // Dragging nothing or a invalid dice
        PopUpAction.Instance.SetData(actionPanel.ActionData.Description);
    }

    private string Description(int index)
    {
        string s = "Move ";
        
        switch(actionPanel.ActionData.Direction)
        {
            case Direction.None:
                break;
            case Direction.Orthogonal:
                s += "orthogonally ";
                break;
            case Direction.Diagonal:
                s += "diagonally ";
                break;
            case Direction.Any:
                s += "in any direction ";
                break;
        }

        int i = GetIntFromAllowedTile.Get(actionPanel.ActionData.AllowedTile, index);
        s += i + (i == 1 ? " Tile" : " Tiles");

        return s;
    }

    public override bool SetInteractible(int diceNumber)
    {
        Vector2Int[] actionDirections = 
            GetVector2IntFromDirection.Get(base.actionPanel.ActionData.Direction);

        int range = GetIntFromAllowedTile.Get(actionPanel.ActionData.AllowedTile, diceNumber);

        bool isMovePossible = false;
        bool isSettingOnce = false;

        foreach (Vector2Int actionDirection in actionDirections)
        {
            if (IsAnyObstacleInWay(actionDirection, range))
                continue;

            if (!isSettingOnce)
            {
                BattleController.Instance.DeactivateInteractible();
                activeSkillIndex = diceNumber;
                isSettingOnce = true;
            }

            isMovePossible = true;

            Vector2Int fieldIndex = character.FieldIndex;
            fieldIndex += actionDirection * range;

            GameObject fieldObject = 
                FieldManager.Instance.Fields[fieldIndex.x, fieldIndex.y];

            FieldManager.Instance.AddInteractibleField(fieldObject);
        }

        return isMovePossible;
    }

    /// <summary>
    /// Determines whether there is any obstacle in the specified direction within the given range.
    /// </summary>
    /// <remarks>This method checks each field along the specified direction up to the given range. Fields
    /// that are out of the map are skipped. If any field within the range contains an obstacle, the method returns <see
    /// langword="true"/>.</remarks>
    /// <param name="actionDirection">The direction to check for obstacles, represented as a 2D vector.</param>
    /// <param name="range">The maximum distance, in units, to check for obstacles. Must be a positive integer.</param>
    /// <returns><see langword="true"/> if an obstacle is detected within the specified range in the given direction; otherwise,
    /// <see langword="false"/>.</returns>
    private bool IsAnyObstacleInWay(Vector2Int actionDirection, int range)
    {
        for (int i = 1; i <= range; i++)
        {
            var fieldIndex = character.FieldIndex;
            fieldIndex += actionDirection * i;

            if (FieldManager.Instance.IsTargetOutOfMap(fieldIndex))
                return true;

            var field = FieldManager.Instance.Fields[fieldIndex.x, fieldIndex.y].
            GetComponent<Field>();

            if (field.IsAnyObstacleOnField())
                return true;
        }
        return false;
    }

    public override void ShowInteractible()
    {
        FieldManager.Instance.ShowInteractibleFields();
    }

    public override void ActivateInteractible()
    {
        FieldManager.Instance.ActivateInactibleFields();
    }

    public override void ProcessInput(GameObject fieldObject)
    {
        if (fieldObject.CompareTag("Field") == false)
        {
            Debug.LogWarning("The clicked object is not a field.");
            return;
        }

        characterObject.GetComponent<CharacterMovement>().MoveTo(fieldObject);
        activeSkillIndex = 0;
    }

}
