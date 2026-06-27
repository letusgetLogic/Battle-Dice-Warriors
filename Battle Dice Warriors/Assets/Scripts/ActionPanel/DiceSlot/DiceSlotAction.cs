using UnityEngine;
using UnityEngine.EventSystems;

public class DiceSlotAction : MonoBehaviour, IDropHandler
{
    //[SerializeField][Range(0f, 1f)] private float _delayShowingInteractible = .5f;

    private ActionPanel _actionPanel => GetComponent<ActionPanel>();
    public ActionBase Action => _actionPanel.Action;
    private PlayerType _playerType =>
        _actionPanel.CharacterObject.GetComponent<Character>().Player.PlayerType;

    private RectTransform _diceTf;

    public bool HasDice()
    {
        if (_diceTf != null)
        {
            return _diceTf.anchoredPosition == GetComponent<RectTransform>().anchoredPosition;
        }

        return false;
    }

    /// <summary>
    /// UI Element is being dropped.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");

        if (LevelManager.Instance.CurrentPhase != Phase.Battle ||
            TurnManager.Instance.Turn != _playerType)
            return;

        if (eventData.pointerDrag == null ||
            eventData.pointerDrag.CompareTag("Dice") == false)
            return;

        var diceObject = eventData.pointerDrag;
        var dice = diceObject.GetComponent<Dice>();

        if (Action.IsValid(dice.CurrentNumber) == false)
            return;

        bool isInteractable = Action.SetInteractible(dice.CurrentNumber);
        if (isInteractable == false)
            return;
        Debug.Log("OnDrop, isInteractable " + isInteractable);
        //if (IsThereActiveObject() == false)
        //    return;

        BattleController.Instance.ActiveAction = Action;
        Action.ShowInteractible();
        Action.ActivateInteractible();

        dice.SetOnActionSlot(_actionPanel.DiceSlotAction.position);

        Action.ActivateSkill(dice.CurrentNumber);
    }

}

