using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public static PanelManager Instance { get; private set; }

    [SerializeField] private GameObject _canvas;

    [SerializeField]
    private TextMeshProUGUI _nameTextLeft;
    public TextMeshProUGUI NameTextLeft
    {
        get { return _nameTextLeft; }
        set { _nameTextLeft = value; }
    }

    [SerializeField]
    private TextMeshProUGUI _nameTextRight;
    public TextMeshProUGUI NameTextRight
    {
        get { return _nameTextRight; }
        set { _nameTextRight = value; }
    }

    [SerializeField]
    private GameObject _playerPanelLeft;
    public GameObject PlayerPanelLeft
    {
        get { return _playerPanelLeft; }
        set { _playerPanelLeft = value; }
    }

    [SerializeField]
    private GameObject _playerPanelRight;
    public GameObject PlayerPanelRight
    {
        get { return _playerPanelRight; }
        set { _playerPanelRight = value; }
    }

    [SerializeField]
    private GameObject[] _characterPanelsLeft;

    [SerializeField]
    private GameObject[] _characterPanelsRight;

    [SerializeField]
    private GameObject _rollPanelLeft;
    public GameObject RollPanelLeft
    {
        get { return _rollPanelLeft; }
        set { _rollPanelLeft = value; }
    }

    [SerializeField]
    private GameObject _rollPanelRight;
    public GameObject RollPanelRight
    {
        get { return _rollPanelRight; }
        set { _rollPanelRight = value; }
    }

    [SerializeField]
    private GameObject _popUpCharacterObject;
    public GameObject PopUpCharacterObject
    {
        get { return _popUpCharacterObject; }
        set { _popUpCharacterObject = value; }
    }

    [SerializeField]
    private GameObject _popUpActionObject;
    public GameObject PopUpActionObject
    {
        get { return _popUpActionObject; }
        set { _popUpActionObject = value; }
    }

    //public GameObject RerollPanelLeft; 
    //public GameObject RerollPanelRight;

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
    /// Sets the panel for the game.
    /// </summary>
    public void SetPanels()
    {
        _nameTextLeft.text = GameManager.Instance.PlayerLeftName;
        _nameTextRight.text = GameManager.Instance.PlayerRightName;
        InitializeRollPanel(RollPanelLeft.GetComponent<RollPanel>());
        InitializeRollPanel(RollPanelRight.GetComponent<RollPanel>());
        SetPanelsInactive(false);

        
    }

    /// <summary>
    /// Initializes the roll panel by setting up the dice and disabling the roll button.
    /// </summary>
    private void InitializeRollPanel(RollPanel rollPanel)
    {
        rollPanel.InitializePlayDice();
        //rollPanel.SetScaleDiceZero();
        ButtonManager.Instance.SetButtonInteractible(rollPanel.RollButton, false);
    }

    /// <summary>
    /// Gets the roll panel.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public RollPanel GetRollPanelFor(PlayerType player)
    {
        switch (player)
        {
            case PlayerType.PlayerLeft:
                return RollPanelLeft.GetComponent<RollPanel>();
            case PlayerType.PlayerRight:
                return RollPanelRight.GetComponent<RollPanel>();
        }

        throw new Exception("PanelManager.GetRollPanelFor() -> player case invalid");
    }

    /// <summary>
    /// Sets the panels active false.
    /// </summary>
    public void SetPanelsInactive(bool setDiceInactive)
    {
        foreach (GameObject panel in _characterPanelsLeft)
        {
            SetActive(panel, false);
        }

        foreach (GameObject panel in _characterPanelsRight)
        {
            SetActive(panel, false);
        }

        if (setDiceInactive)
        {
            RollPanelLeft.GetComponent<RollPanel>().SetPlayDiceInactive();
            RollPanelRight.GetComponent<RollPanel>().SetPlayDiceInactive();
        }

        SetActive(RollPanelLeft, false);
        SetActive(RollPanelRight, false);

        // Set the inactive panel in the scene active to create the singleton instance.
        SetActive(_popUpCharacterObject, true);
        SetActive(_popUpCharacterObject, false);
        SetActive(_popUpActionObject, true);
        SetActive(_popUpActionObject, false);

    }

    /// <summary>
    /// Sets active, transfers data, sets action for the character panel.
    /// </summary>
    /// <param name="player"></param>
    /// <param name="index"></param>
    /// <param name="characterObject"></param>
    /// <returns></returns>
    public GameObject GetPanel(PlayerType player, int index, GameObject characterObject)
    {
        var characterPanelObject = CharacterPanels(player)[index];
        SetActive(characterPanelObject, true);

        var characterPanel = characterPanelObject.GetComponent<CharacterPanel>();
        characterPanel.SetCharacter(characterObject, player);
        characterPanel.SetAction();

        return characterPanelObject;
    }

    /// <summary>
    /// Return the serialized panels in PanelManager for the corresponding player.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    private GameObject[] CharacterPanels(PlayerType player)
    {
        if (player == PlayerType.PlayerLeft)
        {
            return _characterPanelsLeft;
        }
        else if (player == PlayerType.PlayerRight)
        {
            return _characterPanelsRight;
        }

        throw new Exception("player case invalid in PanelManager.CharacterPanels(PlayerType player)");
    }

    /// <summary>
    /// Sets the scale of the RectTransform.
    /// </summary>
    /// <param name="button"></param>
    public void SetScale(GameObject gameObject, Vector3 scaleSize)
    {
        gameObject.GetComponent<RectTransform>().localScale = scaleSize;
    }

    /// <summary>
    /// Sets the game object active true/false.
    /// </summary>
    /// <param name="button"></param>
    public void SetActive(GameObject gameObject, bool value)
    {
        gameObject.SetActive(value);
    }

}
