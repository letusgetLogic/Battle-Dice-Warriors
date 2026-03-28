using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class CharacterMouseEvent : MonoBehaviour,
        IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private float _delayOnHoverTime = 0.5f;

    private IEnumerator _coroutine;

    private bool _isShowing = false;

    /// <summary>
    /// Update method.
    /// </summary>
    private void Update()
    {
        if (_isShowing)
        {
            var character = transform.root.GetComponent<Character>();
            PopUpCharacter.Instance.SetData(character);
        }
    }

    /// <summary>
    /// Hovers the mouse over the character. 
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (LevelManager.Instance.CurrentPhase != Phase.Battle)
        {
            return;
        }

        _coroutine = ShowInfo();
        StartCoroutine(_coroutine);
    }

    /// <summary>
    /// Mouse exits the collider.
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (LevelManager.Instance.CurrentPhase != Phase.Battle)
        {
            return;
        }

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        HideInfo();
    }

    /// <summary>
    /// Clicks the character.
    /// </summary>
    /// <param name="eventData"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (LevelManager.Instance.CurrentPhase != Phase.Battle)
        {
            return;
        }

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            var character = transform.root.GetComponent<Character>();
            character.Panel.ChangeColorOnClickingCharacter();
        }

    }

    /// <summary>
    /// Shows the action description label.
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShowInfo()
    {
        yield return new WaitForSeconds(_delayOnHoverTime);

        _isShowing = true;

        var popUpCharacter =
            PanelManager.Instance.PopUpCharacterObject.GetComponent<PopUpCharacter>();
        popUpCharacter.SetPosition(transform.root.gameObject);
        PanelManager.Instance.SetActive(PanelManager.Instance.PopUpCharacterObject, true);
    }

    /// <summary>
    /// Hides the action description label.
    /// </summary>
    private void HideInfo()
    {
        _isShowing = false;
        PanelManager.Instance.SetActive(PanelManager.Instance.PopUpCharacterObject, false);
    }

}

