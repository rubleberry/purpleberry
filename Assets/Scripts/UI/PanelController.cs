using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    private GameObject _portrait;
    private GameObject _button0;
    private GameObject _button1;
    private GameObject _button2;
    private string _filePath = Application.streamingAssetsPath + "/Portraits/unit.png";
    private RawImage _rawImage;

    private CameraController _cameraController;
    public GameObject unitToControl;
    public GameObject unitToTarget;
    public Vector3 coordToMove;
    public Vector3 coordOfOrigin;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();
        if (_cameraController == null)
        {
            Debug.LogError("CameraController is Null.");
        }

        _portrait = transform.GetChild(0).gameObject;
        if (_portrait == null)
        {
            Debug.LogError("Panel Portrait is Null.");
        }

        _button0 = transform.GetChild(1).gameObject;
        if (_button0 == null)
        {
            Debug.LogError("Panel button0 is Null.");
        }
        _button1 = transform.GetChild(2).gameObject;
        if (_button1 == null)
        {
            Debug.LogError("Panel button1 is Null.");
        }
        _button2 = transform.GetChild(3).gameObject;
        if (_button2 == null)
        {
            Debug.LogError("Panel button2 is Null.");
        }

        TMP_Text txt0 = _button0.transform.GetChild(0).GetComponent<TMP_Text>();
        txt0.text = "Move";

        TMP_Text txt1 = _button1.transform.GetChild(0).GetComponent<TMP_Text>();
        txt1.text = "Target";

        TMP_Text txt2 = _button2.transform.GetChild(0).GetComponent<TMP_Text>();
        txt2.text = ":";

        _rawImage = _portrait.transform.GetComponent<RawImage>();
        LoadImage();

    }

    void LoadImage()
    {
        byte[] imageBytes = File.ReadAllBytes(_filePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageBytes);
        if (_rawImage != null)
        {
            _rawImage.texture = texture;
        }
    }

    public void InitControlPanel(GameObject unitToControl) 
    {
        _filePath = Application.streamingAssetsPath + "/Portraits/" + unitToControl.name + ".png";
        LoadImage();
    }

    public void OnClickButton0()
    {
        Debug.Log("Button0 on Control Panel.");
        this.gameObject.SetActive(false);
    }
    public void OnClickButton1()
    {
        Debug.Log("Button1 on Control Panel.");
        this.gameObject.SetActive(false);
    }
    public void OnClickButton2()
    {
        Debug.Log("Button2 on Control Panel.");
    }
}
