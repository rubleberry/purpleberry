using System.Collections;
using TMPro;
using UnityEngine;

public class WorldTextController : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private Color textColor;

    private float moveSpeed = 0.2f;
    private float fadeSpeed = 3f;
    private float duration = 0.5f;

    void Awake()
    {
        textMesh = this.GetComponent<TextMeshProUGUI>();
        if (textMesh == null)
        {
            Debug.LogError("TextMesh is Null.");
        }
    }

    void Start()
    {
        StartCoroutine(FlyAndFade());
    }
    IEnumerator FlyAndFade()
    {
        textColor = textMesh.color;
        Debug.Log(textColor);
        float timer = 0;
        while (timer < duration)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }

        while (textColor.a > 0)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

            textColor.a -= fadeSpeed * Time.deltaTime;
            textMesh.color = textColor;

            yield return null;
        }

//        textColor.a = 1;
//        textMesh.color = textColor;
        Destroy(gameObject);
    }

}
