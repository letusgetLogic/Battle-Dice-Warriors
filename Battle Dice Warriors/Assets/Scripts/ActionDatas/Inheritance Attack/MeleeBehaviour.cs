using UnityEngine;

public class MeleeBehaviour : Attack
{
    private static readonly string[] infos = new string[]
    {
            DefaultInfo,
            "Dice 1: Hit orthogonally an opponent",
            "Dice 2: Hit orthogonally an opponent with 20% AP Buff",
            "Dice 3: Hit orthogonally an opponent with 50% AP Buff",
            "Dice 4: Hit orthogonally an opponent with 100% AP Buff",
            "Dice 5: Hit orthogonally an opponent with 150% AP Buff",
            "Dice 6: Hit orthogonally an opponent with 200% AP Buff",
    };

    private static readonly AttackSkill[] swordSkills = new AttackSkill[]
    {
        // Direction,           Range, %, Hit, Round, BuffAPText
        new(Direction.None,       0,   0,   0, 0, ""),
        new(Direction.Orthogonal, 1,   0,   1, 0, ""),
        new(Direction.Orthogonal, 1,   20,  1, 0, "(+20% AP)"),
        new(Direction.Orthogonal, 1,   50,  1, 0, "(+50% AP)"),
        new(Direction.Orthogonal, 1,   100, 1, 0, "(+100% AP)"),
        new(Direction.Orthogonal, 1,   150, 1, 0, "(+150% AP)"),
        new(Direction.Orthogonal, 1,   200, 1, 0, "(+200% AP)"),
    };

    public override AttackSkill Skill(int index) => swordSkills[index];
    public override string Info(int index) => infos[index];


    public MeleeBehaviour(ActionPanel actionPanel, GameObject characterObject, WeaponType weaponType) :
        base(actionPanel, characterObject)
    {
        switch (weaponType)
        {
            case WeaponType.Sword:
                VariedAPmin = 15f;
                VariedAPmax = 25f;
                VariedAPinfo = $"* AP varies from -{VariedAPmin}% to +{VariedAPmax}%";
                break;

            case WeaponType.Sword2:
                break;

        }
    }


    public override void ActivateSkill(int diceNumber)
    {
        ActiveSkillIndex = diceNumber;
        var skill = swordSkills[diceNumber];
        var characterAttack = Character.GetComponent<CharacterAttack>();

        float buffAP = characterAttack.CurrentAP * skill.Percentage * 0.01f;
        characterAttack.CurrentBuffAP = buffAP;
        characterAttack.CurrentAP = characterAttack.CurrentAP + buffAP;
        characterAttack.CurrentBuffAPText = skill.BuffAPText;
        characterAttack.InfoText = VariedAPinfo;

        ActionPanel.UpdateEndurance(skill.HitEndurance, skill.RoundEndurance);
    }



    //private static Dictionary<string, string> _attackDescription = new Dictionary<string, string>
    //{
    //    {"Roll 1", "Solid Thrust - Hit an opponent within 1 orthogonal tile range" },
    //    {"Roll 2", "Long Thrust - Hit an opponent within 1 diagonal tile range" },
    //    {"Roll 3", "Silver Swing - Hit all opponents on 3 nearby tiles, which is orthogonal to character, with 75% AP" },
    //    {"Roll 4", "The 4 Stitches - Hit all opponents on all orthogonal or diagonal Tiles with 75% AP" },
    //    {"Roll 5", "Stunning Strike - Hit and stun an opponent within 1 tile range" },
    //    {"Roll 6", "The Giant Sword - Hit all opponents within 2 tile range in a direction with 150% AP" },
    //};
}


