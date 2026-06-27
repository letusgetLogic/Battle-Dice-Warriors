using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class FieldMouseEvent : MonoBehaviour,
        IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _foggyPanel;
    [SerializeField] private GameObject _animationHint;
    [SerializeField] private GameObject _animationClick;
    public GameObject AnimationHint => _animationHint;

    [SerializeField] private Color _onPointerEnterColor;
    [SerializeField] private float _animClickTime = .01f;

    private bool _isClicking = false;

    /// <summary>
    /// OnEnable method.
    /// </summary>
    private void OnEnable()
    {
        _foggyPanel.SetActive(true);
        _animationHint.SetActive(true);
        _animationClick.SetActive(false);
    }

    /// <summary>
    /// On mouse click event.
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            _isClicking = true;
            _animationHint.SetActive(false);
            _animationClick.SetActive(true);

            StartCoroutine(DisableAnimClick(eventData.pointerClick));
        }
    }

    /// <summary>
    /// Disable animation click.
    /// </summary>
    /// <returns></returns>
    private IEnumerator DisableAnimClick(GameObject fieldObject)
    {
        yield return new WaitForSeconds(_animClickTime);

        _isClicking = false;
        BattleController.Instance.HandleInput(fieldObject);
    }

    /// <summary>
    /// On mouse hover event.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_isClicking)
        {
            _animationHint.GetComponent<SpriteRenderer>().color = _onPointerEnterColor;
        }
    }

    /// <summary>
    /// On mouse exit event.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_isClicking)
        {
            _animationHint.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    /// <summary>
    /// Hides the components for the mouse event.
    /// </summary>
    private void OnDisable()
    {
        _foggyPanel.SetActive(false);
        _animationHint.SetActive(false);
        _animationClick.SetActive(false);
        _animationHint.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
