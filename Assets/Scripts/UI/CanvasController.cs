using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CanvasController : MonoBehaviour
{
    private Vector3 _pos;

    [SerializeField]
    private TextMeshProUGUI _textOnCanvas;

    void Start()
    {        
        if (_textOnCanvas == null)
        {
            Debug.LogError("TextMesh is Null.");
        }
        StartCoroutine(CountAndShow());
        _pos = Vector3.zero;
    }

    IEnumerator CountAndShow()
    {
        float timer = 0;
        float duration = 2;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        WriteTextOnCanvas("test", _pos);

    }
    public void WriteTextOnCanvas(string text, Vector3 pos)
    {
        TextMeshProUGUI newObj = Instantiate(_textOnCanvas);
        newObj.text = text;
//        newObj.transform.parent = this.transform;
        newObj.transform.SetParent(transform);
        RectTransform rect = this.GetComponent<RectTransform>();
        newObj.rectTransform.position = rect.position;
        newObj.rectTransform.anchoredPosition = newObj.rectTransform.anchoredPosition + new Vector2 (pos.x, pos.y);
//        newObj.rectTransform.anchoredPosition = newObj.rectTransform.anchoredPosition;
//        Debug.Log(newObj.rectTransform.position);
//        Debug.Log(newObj.rectTransform.anchoredPosition);

    }
}
