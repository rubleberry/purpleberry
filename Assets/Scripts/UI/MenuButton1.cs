using UnityEngine;
using UnityEngine.UI;

public class MenuButton1 : MonoBehaviour
{
    private UnitManager _unitManager;
    private MenuButton _menuButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _unitManager = GameObject.Find("Terrain").GetComponent<UnitManager>();
        if ( _unitManager == null )
        {
            Debug.LogError("Unit Manager is Null.");
        }
        _menuButton = transform.parent.parent.GetComponent<MenuButton>();
        if (_menuButton == null)
        {
            Debug.LogError("Parent Menu Button is Null.");
        }

    }

    // Update is called once per frame
    public void OnClick()
    {
        _unitManager.CreateUnit(new Vector3(6, 2, 6), 1);
        _unitManager.CreateUnit(new Vector3(3, 2, 6), 0);
        _unitManager.CreateUnit(new Vector3(6, 2, 3), 2);
        _menuButton.OffButtons();
    }

}
