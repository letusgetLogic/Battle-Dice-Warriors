using UnityEngine;

public abstract class Attack : ActionBase
{
    public static readonly string DefaultDescription =
        "Move the dice over here to get more information";

    public AllowedDiceNumber AllowedDiceNumber { get; protected set; }
    public int HitEndurance { get; protected set; }
    public int RoundEndurance { get; protected set; }
    
    public Attack(ActionPanel actionPanel, GameObject characterObject) :
        base(actionPanel, characterObject)
    { }

    public override bool IsValid(int diceNumber)
    {
        return CheckDiceCondition.IsNumberValid(AllowedDiceNumber, diceNumber);
    }

    public override abstract void SetDataPopUp(int diceNumber);

    public override abstract bool SetInteractible(int diceNumber); // child class will implement

    public override void ShowInteractible()
    {
        CharacterManager.Instance.ShowInteractibleCharacters();
    }

    public override void ActivateInteractible()
    {
        CharacterManager.Instance.ActivateInteractibleCharacters();
    }

    public override void ProcessInput(GameObject clickedCharacterBody)
    {
        if (clickedCharacterBody.CompareTag("Character") == false)
        {
            Debug.LogWarning("The clicked object is not a character body.");
            return;
        }
        GameObject defenderObject = clickedCharacterBody.transform.root.gameObject;

        var attack = character.GetComponent<CharacterAttack>();
        var defense = defenderObject.GetComponent<CharacterDefense>();
        var defenderHealth = defenderObject.GetComponent<CharacterHealth>();

        // attack action animation still needs to be implemented here...

        DamageCalculator.CalculateDamage(attack, defense, defenderHealth, this);

        CountDownHitEndurance();

        var defenderCharacterPanel = defenderObject.GetComponent<Character>().Panel;
        BattleController.Instance.UpdateHitEnduranceForDefender(defenderCharacterPanel);
    }

    /// <summary>
    /// Returns the varied attack points.
    /// </summary>
    /// <param name="ap"></param>
    /// <returns></returns>
    public virtual float VariedAP(float ap)
    {
        return 0f;
    }

    /// <summary>
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
            characterObject.GetComponent<CharacterAttack>().SetDefault();
            RoundEndurance = 0;
            activeSkillIndex = 0;
        }
        actionPanel.UpdateEndurance(HitEndurance, RoundEndurance);
    }

    /// <summary>
    /// Counts down the RoundEndurance and resets if it reaches zero.
    /// </summary>
    public override void CountDownRoundEndurance(PlayerType lastTurn)
    {
        var playerType = character.Player.PlayerType;

        if (playerType == lastTurn)
        {
            if (RoundEndurance > 0)
            {
                RoundEndurance--;
            }
            if (RoundEndurance == 0)
            {
                characterObject.GetComponent<CharacterAttack>().SetDefault();
                HitEndurance = 0;
                activeSkillIndex = 0;
            }
            actionPanel.UpdateEndurance(HitEndurance, RoundEndurance);
        }
    }
}
