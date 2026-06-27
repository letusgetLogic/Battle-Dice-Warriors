using System.Collections;
using TMPro;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    public static BattleUI Inst { get; private set; }

    [SerializeField] private TextMeshProUGUI _warningTextShader;
    [SerializeField] private TextMeshProUGUI _warningText;
    [SerializeField] private float _warningDuration = 0.2f;
    [SerializeField] private GameObject _index;

    public bool IsShowingIndex;

    private void Awake()
    {
        if (Inst != null)
        {
            Destroy(Inst.gameObject);
        }
        Inst = this;

        _index.SetActive(IsShowingIndex);
    }

    public void ShowWarning(string message)
    {
        _warningTextShader.gameObject.SetActive(true);
        _warningTextShader.text = message;
        _warningText.text = message;

        StartCoroutine(HideWarning());
    }


    private IEnumerator HideWarning()
    {
        yield return new WaitForSeconds(_warningDuration);

        _warningTextShader.gameObject.SetActive(false);
    }

}