using UnityEngine;
using UnityEngine.Events;

public class BattleController : MonoBehaviour
{
    public static BattleController Instance { get; private set; }

    public enum BattleState
    {
        None,
        PhaseRoll,
        PhaseAction,
    }
    public BattleState State { get; set; } = BattleState.None;
    public ActionPanel CurrentPanelOfDefend { get; set; }
    //public IEnumerator Coroutine { get; set; }

    public ActionBase ActiveAction { get; set; }


    /// <summary>
    /// Awake method.
    /// </summary>
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
    }

    /// <summary>
    /// Starts the match by enabling the End Turn button.
    /// </summary>
    public void StartMatch()
    {
    }

    /// <summary>
    /// Deactivates the interactable objects and Sets the coroutine null.
    /// </summary>
    public void DeactivateInteractible()
    {
        FieldManager.Instance.DeactivateInteractibleFields();
        CharacterManager.Instance.DeactivateInteractibleCharacters();
        
        ActiveAction = null;
        
        EventManager.Instance.OnResetAction?.Invoke();
        //SetCoroutineNull();
    }

    ///// <summary>
    ///// Stops coroutine if necessary, setting it to null.
    ///// </summary>
    //private void SetCoroutineNull()
    //{
    //    // Ensure that the coroutine is not null before stopping it.
    //    if (Coroutine != null)
    //    {
    //        StopCoroutine(Coroutine);
    //        Coroutine = null;
    //    }
    //}

    /// <summary>
    /// Handles the input of player on the clicked field or enemy character.
    /// </summary>
    /// <param name="clickedObject"></param>
    public void HandleInput(GameObject clickedObject)
    {
        ActiveAction.ProcessInput(clickedObject);
        ActiveAction = null;
        DeactivateInteractible();
    }

    /// <summary>
    /// Updates the hit endurance for the defender character panel.
    /// </summary>
    /// <param name="characterPanel"></param>
    public void UpdateHitEnduranceForDefender(CharacterPanel characterPanel)
    {
        foreach (ActionPanel actionPanel in characterPanel.ActiveActionPanels)
        {
            actionPanel.Action.UpdateHitEnduranceForDefend();
        }
    }

    /// <summary>
    /// Ends the match.
    /// </summary>
    /// <param name="loser"></param>
    public void EndMatch(PlayerType loser)
    {
        LevelManager.Instance.SubmitWinnerFrom(loser);

        LevelManager.Instance.SetPhase(Phase.MatchOver);
    }
}

