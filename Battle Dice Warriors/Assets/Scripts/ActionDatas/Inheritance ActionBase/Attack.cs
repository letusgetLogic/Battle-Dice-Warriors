using UnityEngine;

public abstract class Attack : ActionBase
{
    public static readonly string DefaultInfo =
        "Move the dice over here to get more information";

    public AllowedDiceNumber AllowedDiceNumber { get; protected set; }
    public int HitEndurance { get; protected set; }
    public int RoundEndurance { get; protected set; }

    #region Varied AP
    public float VariedAPmin { get; protected set; } = 0f;
    public float VariedAPmax { get; protected set; } = 0f;
    public string VariedAPinfo { get; protected set; } = "";

    /// <summary>
    /// Returns the varied attack points based on the below and above varied percentages.
    /// </summary>
    /// <param name="ap"></param>
    /// <returns></returns>
    public virtual float VariedAP(float ap)
    {
        if (VariedAPmax == 0f)
            return 0f;

        float variedPercentage = UnityEngine.Random.Range(-VariedAPmin * 0.01f, VariedAPmax * 0.01f);
        Debug.Log($"Varied AP: {ap} * {variedPercentage}");
        return ap * variedPercentage;
    }
    #endregion

    public Attack(ActionPanel actionPanel, GameObject characterObject) :
        base(actionPanel, characterObject)
    { }

    public abstract string Info(int index);
    public abstract AttackSkill Skill(int index);

    public override bool IsValid(int diceNumber)
    {
        return CheckDiceCondition.IsNumberValid(AllowedDiceNumber, diceNumber);
    }

    public override void SetDataPopUp(int index)
    {
        if (index == 0 && ActiveSkillIndex != 0)
        {
            PopUpAction.Instance.SetData(Info(ActiveSkillIndex));
            return;
        }
        PopUpAction.Instance.SetData(Info(index));
    }


    public override bool FindInteractible(int diceNumber)
    {
        var skill = Skill(diceNumber);
        var actionDirections = GetVector2IntFromDirection.Get(
            skill.Direction);

        bool findTarget = false;

        foreach (Vector2Int actionDirection in actionDirections)
        {
            var enemyObject = FindEnemy(
                Character.FieldIndex,
                actionDirection,
                skill.Range);

            if (enemyObject == null)
                continue;

            findTarget = true;
            CharacterManager.Instance.AddCharacter(enemyObject);
        }
        return findTarget;
    }


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

        var attack = Character.GetComponent<CharacterAttack>();
        var defense = defenderObject.GetComponent<CharacterDefense>();
        var defenderHealth = defenderObject.GetComponent<CharacterHealth>();

        // attack action animation still needs to be implemented here...

        DamageCalculator.CalculateDamage(attack, defense, defenderHealth, this);

        CountDownHitEndurance();

        var defenderCharacterPanel = defenderObject.GetComponent<Character>().Panel;
        BattleController.Instance.UpdateHitEnduranceForDefender(defenderCharacterPanel);
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
            CharacterObject.GetComponent<CharacterAttack>().SetDefault();
            RoundEndurance = 0;
            ActiveSkillIndex = 0;
        }
        ActionPanel.UpdateEndurance(HitEndurance, RoundEndurance);
    }

    /// <summary>
    /// Counts down the RoundEndurance and resets if it reaches zero.
    /// </summary>
    public override void CountDownRoundEndurance(PlayerType lastTurn)
    {
        var playerType = Character.Player.PlayerType;

        if (playerType == lastTurn)
        {
            if (RoundEndurance > 0)
            {
                RoundEndurance--;
            }
            if (RoundEndurance == 0)
            {
                CharacterObject.GetComponent<CharacterAttack>().SetDefault();
                HitEndurance = 0;
                ActiveSkillIndex = 0;
            }
            ActionPanel.UpdateEndurance(HitEndurance, RoundEndurance);
        }
    }

}
