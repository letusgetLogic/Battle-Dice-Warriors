using UnityEngine;
using UnityEngine.UI;

public class UndoButton : MonoBehaviour
{
    [SerializeField] private Button undoButton;
    public Dice Dice { get; private set; }

    private void Awake()
    {
        undoButton.onClick.AddListener(OnUndoClicked);
        undoButton.interactable = false;
    }

    private void OnEnable()
    {
        EventManager.Instance.OnResetAction += DisableUndo;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnResetAction -= DisableUndo;
    }

    public void Init(Dice dice)
    {
        Dice = dice;
        Dice.OnDropped += (myDice) => undoButton.interactable = true;
    }

    public void OnUndoClicked()
    {
        undoButton.interactable = false;
        Dice.GetComponent<DiceMovement>().SendBackToBase();
    }

    public void DisableUndo()
    {
        undoButton.interactable = false;
    }
}
