using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
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
        WriteTextOnCanvas("test again", _pos);

    }
    public void WriteTextOnCanvas(string text, Vector3 pos)
    {
        TextMeshProUGUI newText = Instantiate(_textOnCanvas);
        newText.text = text;
        newText.color = Color.blue;
        newText.faceColor = Color.blue;
        newText.transform.SetParent(transform);
        RectTransform rect = this.GetComponent<RectTransform>();
        newText.rectTransform.position = rect.position;
        Vector2 centerposition = new Vector2(newText.rectTransform.anchoredPosition.x, newText.rectTransform.anchoredPosition.y);
        newText.rectTransform.anchoredPosition = newText.rectTransform.anchoredPosition - centerposition;
        newText.rectTransform.anchoredPosition = newText.rectTransform.anchoredPosition + new Vector2 (pos.x, pos.y);
    }
}
