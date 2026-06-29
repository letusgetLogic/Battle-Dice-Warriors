using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFirstTurn : MonoBehaviour
{
    public static SetFirstTurn Instance { get; private set; }

    [SerializeField] private GameObject _turnDiceLeft;
    [SerializeField] private GameObject _turnDiceRight;
    [SerializeField] private float _animTimer = 0.2f;

    private GameObject[] _panels;
    private Vector3 _originScale;

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

        _originScale = _turnDiceLeft.GetComponent<RectTransform>().localScale;
    }

    /// <summary>
    /// Initializes the panels.
    /// </summary>
    public void InitializePanels()
    {
        _panels = new GameObject[]
        {
            PanelManager.Instance.PlayerPanelLeft,
            PanelManager.Instance.PlayerPanelRight,
            PanelManager.Instance.RollPanelLeft,
            PanelManager.Instance.RollPanelRight
        };
    }

    /// <summary>
    /// Sets the turn dice and the player panel at the start state.
    /// </summary>
    public void SetTurnDiceAndPanel()
    {
        //_turnDiceLeft.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
        //_turnDiceRight.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
        _turnDiceLeft.SetActive(true);
        _turnDiceRight.SetActive(true);

        //foreach (var panel in _panels)
        //{
        //    PanelManager.Instance.SetScale(panel, new Vector3(0, 0, 0));
        //}

        foreach (var panel in _panels)
        {
            PanelManager.Instance.SetActive(panel, true);
        }
    }

    /// <summary>
    /// Rolls turn dice.
    /// </summary>
    public void RollTurnDice()
    {
        if (LevelManager.Instance.CurrentPhase != Phase.SetFirstTurn)
        {
            return;
        }

        var turnDice = new List<Dice>
        {
                _turnDiceLeft.GetComponent<Dice>(),
                _turnDiceRight.GetComponent<Dice>(),
        };

        RollDice.Instance.Roll(
            turnDice,
            RollDice.Instance.RollFrequency,
            _animTimer,
            CheckDiceNumber);
    }

    /// <summary>
    /// Checks dice numbers.
    /// </summary>
    public void CheckDiceNumber()
    {
        int numberLeft = _turnDiceLeft.GetComponent<Dice>().CurrentNumber;
        int numberRight = _turnDiceRight.GetComponent<Dice>().CurrentNumber;

        if (numberLeft == numberRight)
            StartCoroutine(Reroll());
        else
        {
            var firstTurn = numberLeft > numberRight ?
                            PlayerType.PlayerLeft :
                            PlayerType.PlayerRight;

            StartCoroutine(SetTurn(firstTurn));
        }
    }

    /// <summary>
    /// Checks the turn dice.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Reroll()
    {
        yield return new WaitForSeconds(1f);

        RollTurnDice();
    }

    /// <summary>
    /// Sets the first turn.
    /// </summary>
    /// <param name="firstTurn"></param>
    /// <returns></returns>
    private IEnumerator SetTurn(PlayerType firstTurn)
    {
        yield return new WaitForSeconds(1f);

        if (LevelManager.Instance.CurrentPhase == Phase.MatchOver)
        {
            yield break;
        }

        //MatchIntroController.Instance.SetIntroInactive();

        TurnManager.Instance.SetFirstTurn(firstTurn);

        StartCoroutine(EndMatchIntro());
    }

    /// <summary>
    /// Ends the match intro.
    /// </summary>
    /// <returns></returns>
    private IEnumerator EndMatchIntro()
    {
        yield return new WaitForSeconds(1f);

        if (LevelManager.Instance.CurrentPhase == Phase.MatchOver)
        {
            yield break;
        }

        _turnDiceLeft.SetActive(false);
        _turnDiceRight.SetActive(false);

        LevelManager.Instance.SetPhase(Phase.Battle);
    }

    /// <summary>
    /// Scales up the alpha values. 
    /// </summary>
    public void ScaleUpPanelsAndTurnDice(float ratio)
    {
        ScaleUpEach(
            _turnDiceLeft, 
            PanelManager.Instance.RollPanelLeft.GetComponent<RollPanel>(), 
            ratio);

        ScaleUpEach(
            _turnDiceRight, 
            PanelManager.Instance.RollPanelRight.GetComponent<RollPanel>(), 
            ratio);
    }

    /// <summary>
    /// Scales up each turn dice, panels and each play dice.
    /// </summary>
    /// <param name="turnDice"></param>
    /// <param name="rollPanel"></param>
    /// <param name="ratio"></param>
    private void ScaleUpEach(GameObject turnDice, RollPanel rollPanel, float ratio)
    {
        if (ratio >= 1)
        {
            turnDice.GetComponent<RectTransform>().localScale = _originScale;

            foreach (var panel in _panels)
            {
                PanelManager.Instance.SetScale(panel, Vector3.one);
            }

            return;
        }

        turnDice.GetComponent<RectTransform>().localScale = _originScale * ratio;

        foreach (var panel in _panels)
        {
            PanelManager.Instance.SetScale(panel, new Vector3(ratio, ratio, ratio));
        }
    }

    /// <summary>
    /// Scales up the alpha values. 
    /// </summary>
    public void ScaleUpPlayDice(float ratio)
    {
        ScaleUpEach(PanelManager.Instance.RollPanelLeft.GetComponent<RollPanel>(), 
            ratio);

        ScaleUpEach(PanelManager.Instance.RollPanelRight.GetComponent<RollPanel>(), 
            ratio);
    }

    /// <summary>
    /// Scales up each turn dice, panels and each play dice.
    /// </summary>
    /// <param name="turnDice"></param>
    /// <param name="rollPanel"></param>
    /// <param name="ratio"></param>
    private void ScaleUpEach(RollPanel rollPanel, float ratio)
    {
        if (ratio >= 1)
        {
            foreach (var playDice in rollPanel.PlayDice)
            {
                playDice.GetComponent<RectTransform>().localScale = Vector3.one;
            }

            return;
        }

        foreach (var playDice in rollPanel.PlayDice)
        {
            playDice.GetComponent<RectTransform>().localScale = Vector3.one * ratio;
        }
    }

    public void HideTurnDice()
    {
               _turnDiceLeft.SetActive(false);
        _turnDiceRight.SetActive(false);
    }
}
