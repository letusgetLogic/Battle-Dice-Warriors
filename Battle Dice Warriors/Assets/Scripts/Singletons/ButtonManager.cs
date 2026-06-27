using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager Instance { get; private set; }

    [SerializeField]  
    private GameObject _endTurnButtonObject;
    public GameObject EndTurnButtonObject => _endTurnButtonObject;

    [SerializeField] 
    private Button _endTurnButton;
    public Button EndTurnButton => _endTurnButton;

    [SerializeField]
    private GameObject _newMatchButtonObject;
    public GameObject NewMatchButtonObject => _newMatchButtonObject;

    [SerializeField]
    private Button _newMatchButton;
    public Button NewMatchButton => _newMatchButton;

    [SerializeField]
    private GameObject _menuButtonObject;
    public GameObject MenuButtonObject => _menuButtonObject;

    [SerializeField]
    private Button _menuButton;
    public Button MenuButton => _menuButton;


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
    /// Start method.
    /// </summary>
    private void Start()
    {
        SetGameObjectActive(_endTurnButtonObject, false);
        SetGameObjectActive(_newMatchButtonObject, false);
    }

    /// <summary>
    /// OnEndTurnButton method is called when the end turn button is clicked.
    /// </summary>
    public void OnEndTurnButton()
    {
        BattleController.Instance.DeactivateInteractible();
        TurnManager.Instance.SwitchTurn();
        SetGameObjectActive(_endTurnButtonObject, false);
    }

    /// <summary>
    /// OnNewMatchButton method is called when the new match button is clicked.
    /// </summary>
    public void OnNewMatchButton()
    {
        GameManager.Instance.LoadScene("BattleArenaScene");
    }

    /// <summary>
    /// OnMenuButton method is called when the menu button is clicked.
    /// </summary>
    public void OnMenuButton()
    {
        GameManager.Instance.SetDefault();
        GameManager.Instance.LoadScene("MainMenuScene");
    }

    /// <summary>
    /// Sets the button active true/false.
    /// </summary>
    /// <param name="go"></param>
    public void SetGameObjectActive(GameObject go, bool value)
    {
        go.gameObject.SetActive(value);
    }

    /// <summary>
    /// Sets the button interactable true/false.
    /// </summary>
    /// <param name="button"></param>
    public void SetButtonInteractible(Button button, bool value)
    {
        button.interactable = value;
    }

}

