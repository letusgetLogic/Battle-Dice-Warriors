using UnityEngine;
using UnityEngine.UI;

public class DiceMovement : MonoBehaviour
{
    [SerializeField] private float _animSpeed = 1f;
    [SerializeField] private AnimationCurve _animCurve;

    private RectTransform _rectTransform => GetComponent<RectTransform>();

    private Vector2 _basePosition;
    private Vector2 _currentPosition;

    private bool _isRunning = false;
    private float _currentValue = 0f;

    /// <summary>
    /// Awake method.
    /// </summary>
    private void Awake()
    {
        _basePosition = _rectTransform.anchoredPosition;
    }

    /// <summary>
    /// Update method.
    /// </summary>
    private void Update()
    {
        LerpMovement();
    }


    /// <summary>
    /// Lerps the movement of the dice.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void LerpMovement()
    {
        if (_isRunning)
        {
            if (_rectTransform.anchoredPosition == _basePosition)
            {
                _currentValue = 0f;
                _isRunning = false;

                if (BattleController.Instance.State == BattleController.BattleState.PhaseRoll)
                    return;

                var dice = GetComponent<Dice>();
                var diceDragEvent = GetComponent<DiceDragEvent>();
                dice.SetComponentEnabled(diceDragEvent, true);

                return;
            }

            _currentValue = Mathf.MoveTowards(_currentValue, 1, _animSpeed * 0.0001f / Time.deltaTime);

            if (_currentValue > 0.9f)
            {
                _rectTransform.anchoredPosition = _basePosition;
                return;
            }

            var lerpPos = Vector2.Lerp(_currentPosition, _basePosition, _animCurve.Evaluate(_currentValue));
            _rectTransform.anchoredPosition = lerpPos;
        }
    }

    /// <summary>
    /// Sends the dice back to the roll panel.
    /// </summary>
    public void SendBackToBase()
    {
        _rectTransform.anchoredPosition = _basePosition;

        //_currentPosition = _rectTransform.anchoredPosition;

        //var dice = GetComponent<Dice>();
        //var diceDragEvent = GetComponent<DiceDragEvent>();
        //dice.SetComponentEnabled(diceDragEvent, false);

        //_isRunning = true;
    }

    /// <summary>
    /// </summary>
    public void PositionsTo(Vector3 pos)
    {
        _rectTransform.position = pos;
    }
}
