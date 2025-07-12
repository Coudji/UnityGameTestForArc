using FishNet.Managing;
using UnityEngine;
using UnityEngine.UIElements;

public class HUDController : MonoBehaviour
{
    private UIDocument _hudDocument;

    private VisualElement _root;
    private VisualElement _hpBar;
    private VisualElement _staminaBar;

    private void Start()
    {
        _hudDocument = GetComponent<UIDocument>();

        _root = _hudDocument.rootVisualElement;
        _hpBar = _root.Q<VisualElement>(UIElementNames.HealthBar);
        _staminaBar = _root.Q<VisualElement>(UIElementNames.StaminaBar);
    }

    private void OnEnable()
    {
        Debug.Log("HUDController enabled");
        CharacterEvents.OnStaminaUpdated += UpdateStaminaBar;
    }

    private void OnDisable()
    {
        CharacterEvents.OnStaminaUpdated -= UpdateStaminaBar;
    }

    public void Show()
    {
        if (_root != null)
        {
            _root.style.display = DisplayStyle.Flex;
        }
    }

    public void Hide()
    {
        if (_root != null)
        {
            _root.style.display = DisplayStyle.None;
        }
    }

    public void UpdateHealthBar(float healthRatio)
    {
        UpdateBar(_hpBar, healthRatio);
    }

    public void UpdateStaminaBar(float staminaRatio)
    {
        UpdateBar(_staminaBar, staminaRatio);
    }

    public void UpdateBar(VisualElement bar, float ratio)
    {
        if (bar != null)
        {
            bar.style.flexBasis = new StyleLength(
                new Length(Mathf.Max(0, ratio) * 100, LengthUnit.Percent)
            );
        }
    }
}
