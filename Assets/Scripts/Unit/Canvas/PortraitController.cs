using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PortraitController : MonoBehaviour
{
    private GameObject _parent;

    private string _filePath = Application.streamingAssetsPath + "/Portraits/Purple.png";
    private RawImage _rawImage;

    private void Start()
    {
        _parent = this.transform.parent.parent.parent.gameObject;
        if (_parent == null)
        {
            Debug.LogError("Parent is Null.");
        }
        _rawImage = this.transform.GetComponent<RawImage>();
        _filePath = Application.streamingAssetsPath + "/Portraits/" + _parent.name + ".png";
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
        else
        {
            Debug.LogError("RawImage not assigned in Inspector");
        }
    }
}
