using UnityEngine;

public class RangeBehaviour : Attack
{
    private string[] infos;

    private AttackSkill[] bowSkills;

    public override AttackSkill Skill(int index) => bowSkills[index];
    public override string Info(int index) => infos[index];


    public RangeBehaviour(ActionPanel actionPanel, GameObject characterObject) :
        base(actionPanel, characterObject)
    {
        bowSkills = new AttackSkill[]
    {
        // Direction,                       Range, %, Hit, Round, BuffAPText
        new(ActionPanel.ActionData.Direction, 0,   0,   0, 0, ""),
        new(ActionPanel.ActionData.Direction, 1,   0,   1, 0, ""),
        new(ActionPanel.ActionData.Direction, 2,   0,   1, 0, ""),
        new(ActionPanel.ActionData.Direction, 3,   0,   1, 0, ""),
        new(ActionPanel.ActionData.Direction, 4,   0,   1, 0, ""),
        new(ActionPanel.ActionData.Direction, 5,   0,   1, 0, ""),
        new(ActionPanel.ActionData.Direction, 6,   0,   1, 0, ""),
    };
    }
    public override void SetDataPopUp(int index)
    {
        // Dragging nothing and dice is on slot
        if (index == 0 && ActiveSkillIndex != 0)
        {
            PopUpAction.Instance.SetData(Description(ActiveSkillIndex));
            return;
        }

        // Dragging a dice
        if (index > 0 && CheckDiceCondition.IsNumberValid(ActionPanel.ActionData.AllowedDiceNumber, index))
        {
            PopUpAction.Instance.SetData(Description(index));
            return;
        }

        // Dragging nothing or a invalid dice
        PopUpAction.Instance.SetData(ActionPanel.ActionData.Description);
    }
    private string Description(int index)
    {
        string s = "Hit ";

        switch (ActionPanel.ActionData.Direction)
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

        s += "an opponent with a range of ";

        int i = GetIntFromAllowedTile.Get(ActionPanel.ActionData.AllowedTile, index);
        s += i + (i == 1 ? " Tile" : " Tiles");

        return s;
    }

    public override void ActivateSkill(int diceNumber)
    {
        ActiveSkillIndex = diceNumber;
        var skill = bowSkills[diceNumber];
        var characterAttack = Character.GetComponent<CharacterAttack>();



        ActionPanel.UpdateEndurance(skill.HitEndurance, skill.RoundEndurance);
    }

}


