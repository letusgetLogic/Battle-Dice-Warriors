using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiceDragEvent : MonoBehaviour,
    IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float _delayEndDrag = 0.1f;

    private Dice _dice;
    private DiceDisplay _display;
    private DiceMovement _move;
    private Vector3 defaultScale;

    private void Start()
    {
        _dice = GetComponent<Dice>();
        _display = GetComponent<DiceDisplay>();
        _move = GetComponent<DiceMovement>();
        defaultScale = transform.localScale;
    }

    /// <summary>
    /// Triggers event at the beginning of drag.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            _display.SetAlphaDown();
            _display.SetBlocksRaycasts(false);
            _display.SetScale();
        }

    }

    /// <summary>
    /// Triggers event while drag.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            _display.UpdatePosition(eventData);
        }

    }

    /// <summary>
    /// Triggers event at the end of drag.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");

        _display.SetDefault();
        _display.SetBlocksRaycasts(true);

        StartCoroutine(WaitForEndDrag());

    }

    /// <summary>
    /// Waits for a delay before sending the dice back to base.
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitForEndDrag()
    {
        yield return new WaitForSeconds(_delayEndDrag);

        if (_dice.IsDropped)
        {
            yield break;
        }

        _move.SendBackToBase();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale *= 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = defaultScale;
    }
}

