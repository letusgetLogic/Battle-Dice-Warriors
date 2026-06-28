using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionPanelMouseEvent : MonoBehaviour,
        IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField][Range(0f, 1f)] private float _delayOnHoverTime = .5f;
    public float DelayOnHoverTime => _delayOnHoverTime;

    private IEnumerator _coroutine;
    private PlayerType _playerType => GetComponent<ActionPanel>().
        CharacterObject.GetComponent<Character>().Player.PlayerType;

    /// <summary>
    /// OnPointerEnter. 
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (LevelManager.Instance.CurrentPhase != Phase.Battle ||
            TurnManager.Instance.Turn != _playerType)
            return;

        Dice dice = null;

        if (eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("Dice"))
        {
            dice = eventData.pointerDrag.GetComponent<Dice>();
        }

        _coroutine = ShowInfo(dice);
        StartCoroutine(_coroutine);
    }

    /// <summary>
    /// OnPointerExit.
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (LevelManager.Instance.CurrentPhase != Phase.Battle ||
            TurnManager.Instance.Turn != _playerType)
            return;

        // Ensure that the coroutine is not null before stopping it.
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        HidePopUp();

        var panel = GetComponent<ActionPanel>();
        if (panel == null)
            return;

        if (BattleController.Instance.ActiveAction == null)
        {
            BattleController.Instance.DeactivateInteractible();
        }

        if (BattleController.Instance.ActiveAction != panel.Action)
        {
            panel.Action.SetDefault();
        }
    }

    /// <summary>
    /// Shows the action popup.
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShowInfo(Dice dice)
    {
        yield return new WaitForSeconds(_delayOnHoverTime);

        var panel = GetComponent<ActionPanel>();
        if (panel == null)
            yield break;

        PanelManager.Instance.SetActive(PanelManager.Instance.PopUpActionObject, true);

        int diceNumber = dice == null ? 0 : dice.CurrentNumber;

        panel.Action.SetDataPopUp(diceNumber);
        PopUpAction.Instance.SetPosition(gameObject);

        if (dice == null)
            yield break;

        if (panel.Action.IsValid(dice.CurrentNumber) == false)
            yield break;

        BattleController.Instance.DeactivateInteractible(); // Reset the list of interactible

        bool isInteractable = panel.Action.FindInteractible(dice.CurrentNumber);
        if (isInteractable == false)
            yield break;

        panel.Action.ShowInteractible();
    }

    /// <summary>
    /// Hides the action popup.
    /// </summary>
    public void HidePopUp()
    {
        PanelManager.Instance.SetActive(PanelManager.Instance.PopUpActionObject, false);
    }

}
