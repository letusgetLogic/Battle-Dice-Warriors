using UnityEngine;
public abstract class Defend : ActionBase
{
    public static readonly string DefaultDescription =
        "Move the dice over here to get more information";

    public AllowedDiceNumber AllowedDiceNumber { get; protected set; }
    public int HitEndurance { get; protected set; }
    public int RoundEndurance { get; protected set; }
    public bool IsHitCrucial { get; protected set; }

    public Defend(ActionPanel actionPanel, GameObject characterObject) :
        base(actionPanel, characterObject)
    { }

    public abstract string Info(int index);
    public abstract DefendSkill Skill(int index);

    public override bool IsValid(int diceNumber)
    {
        return CheckDiceCondition.IsNumberValid(AllowedDiceNumber, diceNumber);
    }

    public override abstract void SetDataPopUp(int diceNumber);

    public override bool FindInteractible(int diceNumber)
    {
        return true;
    }

    public override void ShowInteractible()
    {}

    public override void ActivateInteractible()
    {}

    public override void ProcessInput(GameObject fieldObject)
    {}

    public override void UpdateHitEnduranceForDefend()
    {
        if (IsHitCrucial)
            CountDownHitEndurance();
    }

    // <summary>
    /// Counts down the HitEndurance and resets if it reaches zero.
    /// </summary>
    private void CountDownHitEndurance()
    {
        if (HitEndurance > 0)
        {
            HitEndurance--;
        }
        if (HitEndurance == 0)
        {
            Character.GetComponent<CharacterDefense>().SetDefault();
            RoundEndurance = 0;
            ActiveSkillIndex = 0;
            Character.GetComponent<CharacterWeapon>().IsRelaxing = true;
        }
        ActionPanel.UpdateEndurance(HitEndurance, RoundEndurance);
    }

    /// <summary>
    /// Counts down the RoundEndurance and resets if it reaches zero.
    /// </summary>
    public override void CountDownRoundEndurance(PlayerType lastTurn)
    {
        if (IsHitCrucial)
            return;

        var playerType = Character.Player.PlayerType;

        if (playerType != lastTurn)
        {
            if (RoundEndurance > 0)
            {
                RoundEndurance--;
            }
            if (RoundEndurance == 0)
            {
                CharacterObject.GetComponent<CharacterDefense>().SetDefault();
                HitEndurance = 0;
                ActiveSkillIndex = 0;
                Character.GetComponent<CharacterWeapon>().IsRelaxing = true;
            }
            ActionPanel.UpdateEndurance(HitEndurance, RoundEndurance);
        }
    }

}
