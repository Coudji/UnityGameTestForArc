using UnityEngine;
using UnityEngine.UIElements;

public class HUDManager : Singleton<HUDManager>
{
    [SerializeField]
    private UIDocument _hudDocument;
    private VisualElement _root;
    private VisualElement _hpBar;

    protected override void Awake()
    {
        base.Awake();

        if (_hudDocument != null)
        {
            _root = _hudDocument.rootVisualElement;
            _hpBar = _root.Q<VisualElement>(UIElementNames.HealthBar);
            Hide();
        }
        else
        {
            Debug.LogError("HUD Document is not assigned in the HUDManager.");
        }
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

    public void UpdateBar(VisualElement bar, float ratio)
    {
        if (bar != null)
        {
            bar.style.width = new StyleLength(new Length(ratio * 100, LengthUnit.Percent));
        }
        else
        {
            Debug.LogError("Bar element not found in the HUD.");
        }
    }
}
