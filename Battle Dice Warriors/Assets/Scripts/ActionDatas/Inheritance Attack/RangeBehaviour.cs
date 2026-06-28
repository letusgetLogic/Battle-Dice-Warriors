using UnityEngine;

public class RangeBehaviour : Attack
{
    private static readonly string[] infos = new string[]
    {
            DefaultInfo,
            "Dice 1: Hit orthogonally an opponent",
            "Dice 2: Hit orthogonally an opponent with range of 2",
            "Dice 3: Hit orthogonally an opponent with range of 3",
            "Dice 4: Hit orthogonally an opponent with range of 4",
            "Dice 5: Hit orthogonally an opponent with range of 5",
            "Dice 6: Hit orthogonally an opponent with range of 6",
    };

    private static readonly AttackSkill[] bowSkills = new AttackSkill[]
    {
        // Direction,           Range, %, Hit, Round, BuffAPText
        new(Direction.None,       0,   0,   0, 0, ""),
        new(Direction.Orthogonal, 1,   0,   1, 0, ""),
        new(Direction.Orthogonal, 2,   0,   1, 0, ""),
        new(Direction.Orthogonal, 3,   0,   1, 0, ""),
        new(Direction.Orthogonal, 4,   0,   1, 0, ""),
        new(Direction.Orthogonal, 5,   0,   1, 0, ""),
        new(Direction.Orthogonal, 6,   0,   1, 0, ""),
    };

    public override AttackSkill Skill(int index) => bowSkills[index];
    public override string Info(int index) => infos[index];


    public RangeBehaviour(ActionPanel actionPanel, GameObject characterObject) :
        base(actionPanel, characterObject)
    {
        AllowedDiceNumber = AllowedDiceNumber.D1_6;
    }
    
   
    public override void ActivateSkill(int diceNumber)
    {
        ActiveSkillIndex = diceNumber;
        var skill = bowSkills[diceNumber];
        var characterAttack = Character.GetComponent<CharacterAttack>();

       

        ActionPanel.UpdateEndurance(skill.HitEndurance, skill.RoundEndurance);
    }

}


