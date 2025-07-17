using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Test1 : MonoBehaviour
{
    private UIDocument _hud;
    private int _counter = 0;

    private void Awake()
    {
        Debug.Log("Test1 Awake");
    }

    private void Start()
    {
        Debug.Log("Test1 Start");
        _hud = GetComponent<UIDocument>();
        Debug.Log(_hud);
        StartCoroutine(Test());
    }

    private IEnumerator Test()
    {
        while (true)
        {
            Debug.Log(_counter++ + "" + (_hud.rootVisualElement));
            yield return new WaitForSeconds(1f);
        }
    }
}
