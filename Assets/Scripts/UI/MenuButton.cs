using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    private GameObject _button1;
    private GameObject _button2;
    private GameObject _button3;

    private UnitManager _unitManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _button1 = transform.GetChild(1).gameObject;
        if( _button1 == null)
        {
            Debug.LogError("Button1 is Null.");
        }

        _button2 = transform.GetChild(2).gameObject;
        if (_button2 == null)
        {
            Debug.LogError("Button2 is Null.");
        }

        _button3 = transform.GetChild(3).gameObject;
        if (_button3 == null)
        {
            Debug.LogError("Button3 is Null.");
        }

        _unitManager = GameObject.Find("Terrain").GetComponent<UnitManager>();
        if (_unitManager == null)
        {
            Debug.LogError("Unit Manager is Null.");
        }

    }

    public void OnClick()
    {
        _button1.SetActive(true);
        _button2.SetActive(true);
        _button3.SetActive(true);
    }
    public void OnClick1()
    {
    }

    public void OffButtons()
    {
        _button1.SetActive(false);
        _button2.SetActive(false);
        _button3.SetActive(false);

    }

}
