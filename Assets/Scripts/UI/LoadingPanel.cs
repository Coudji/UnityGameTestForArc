namespace Arc.UI
{
    public class UILoadingPanel : StaticInstance<UILoadingPanel>
    {
        public void Start()
        {
            Hide();
        }
    
        public void Show()
        {
            gameObject.SetActive(true);
        }
    
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
