using System.Collections;
using TMPro;
using UnityEngine;

public class TextOnUnitCanvasController : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private Color textColor;

    private float moveSpeed = 2f;    // 위로 올라가는 속도
    private float fadeSpeed = 3f;    // 사라지는 속도
    private float duration = 0.5f;   // 사라지기 시작할 때까지 버티는 시간

    void Awake()
    {
        textMesh = this.GetComponent<TextMeshProUGUI>();
        if(textMesh == null)
        {
            Debug.LogError("TextMesh is Null.");
        }
        textColor = textMesh.color;
    }

    void Start()
    {
        StartCoroutine(FlyAndFade());
    }
    IEnumerator FlyAndFade()
    {
        // 1. 설정한 시간(duration) 동안은 위로만 이동
        float timer = 0;
        while (timer < duration)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }

        // 2. 이후 위로 이동하면서 동시에 투명(Fade Out)해짐
        while (textColor.a > 0)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

            // 알파(Alpha) 값 감소
            textColor.a -= fadeSpeed * Time.deltaTime;
            textMesh.color = textColor;

            yield return null;
        }

        // 3. 완전히 투명해지면 오브젝트 파괴
//        Destroy(gameObject);
    }
}