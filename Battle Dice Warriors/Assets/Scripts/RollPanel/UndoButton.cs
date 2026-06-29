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
        undoButton.gameObject.SetActive(false);
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
        Dice.OnDropped += EnableUndo;
    }

    public void OnUndoClicked()
    {
        undoButton.interactable = false;
        Dice.GetComponent<DiceMovement>().SendBackToBase();
        Dice.SetDefault();
        Dice.GetComponent<DiceDisplay>().SetDefault();

        var diceDragEvent = Dice.GetComponent<DiceDragEvent>();
        Dice.SetComponentEnabled(diceDragEvent, true);

        EventManager.Instance.OnUndoClicked?.Invoke();
    }

    public void EnableUndo(Dice dice)
    {
        undoButton.interactable = true;
        undoButton.gameObject.SetActive(true);
    }
    public void DisableUndo()
    {
        undoButton.interactable = false;
        undoButton.gameObject.SetActive(false);
    }
}
