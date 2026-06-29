using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollPanel : MonoBehaviour
{
    [SerializeField]
    private Button _rollButton;
    public Button RollButton => _rollButton;

    [SerializeField] private Dice[] _allDice;
    [SerializeField] private UndoButton[] _undoButtons;

    public List<Dice> PlayDice { get; private set; }

    /// <summary>
    /// Initializes the PlayDice array with the active dice from _allDice
    /// and sets the start state to each.
    /// </summary>
    public void InitializePlayDice()
    {
        PlayDice = new List<Dice>();

        for (int i = 0; i < _allDice.Length; i++)
        {
            var dice = _allDice[i];
            dice.InitializeIndexOf(gameObject, i);

            dice.GetComponent<RectTransform>().localScale = Vector3.zero;
            var diceDragEvent = dice.GetComponent<DiceDragEvent>();
            dice.SetComponentEnabled(diceDragEvent, false);

            _undoButtons[i].Init(dice);
            PlayDice.Add(dice);
        }
    }

    /// <summary>
    /// Sets the dice to their default state.
    /// </summary>
    /// <param name="amount"></param>
    public void SetDiceDefault()
    {
        foreach (var dice in PlayDice)
        {
            dice.SetDefault();

            var diceDisplay = dice.GetComponent<DiceDisplay>();
            diceDisplay.SetDefault();
            diceDisplay.SetIdleRolling();
        }
    }

    /// <summary>
    /// Roll Button triggers.
    /// </summary>
    public void Roll()
    {
        ButtonManager.Instance.SetButtonInteractible(RollButton, false);
        ButtonManager.Instance.SetGameObjectActive(ButtonManager.Instance.EndTurnButtonObject, true);

        foreach (var dice in PlayDice)
        {
            var diceDisplay = dice.GetComponent<DiceDisplay>();
            diceDisplay.IsDiceIdleRolling = false;
        }

        RollDice.Instance.Roll(
            PlayDice,
            RollDice.Instance.RollFrequency,
            RollDice.Instance.AnimTimer,
            SetInteraction);
    }

    /// <summary>
    /// Sets interaction for the dice.
    /// </summary>
    private void SetInteraction()
    {
        BattleController.Instance.State = BattleController.BattleState.PhaseAction;
        SetDragEnabled(PlayDice, true);
    }

    /// <summary>
    /// Sets the component DiceDragEvent enabled true/false.
    /// </summary>
    /// <param name="diceObjects"></param>
    /// <param name="value"></param>
    public void SetDragEnabled(List<Dice> diceObjects, bool value)
    {
        foreach (Dice dice in diceObjects)
        {
            var diceDragEvent = dice.GetComponent<DiceDragEvent>();
            dice.SetComponentEnabled(diceDragEvent, value);
        }
    }

    /// <summary>
    /// Sets the alpha of the dice down.
    /// </summary>
    /// <param name="diceObjects"></param>
    public void SetAlphaDown(List<Dice> diceObjects)
    {
        foreach (Dice dice in diceObjects)
        {
            var diceDisplay = dice.GetComponent<DiceDisplay>();
            diceDisplay.SetAlphaDown();
        }
    }

    /// <summary>
    /// Sends the dice back to base.
    /// </summary>
    /// <param name="diceObjects"></param>
    /// <param name="value"></param>
    public void SendBackToBase(List<Dice> diceObjects)
    {
        foreach (Dice dice in diceObjects)
        {
            dice.GetComponent<DiceMovement>().SendBackToBase();
        }
    }

    /// <summary>
    /// Sets the PlayDice inactive.
    /// </summary>
    public void SetPlayDiceInactive()
            {
        foreach (Dice dice in PlayDice)
        {
            dice.gameObject.SetActive(false);
        }
    }
}
