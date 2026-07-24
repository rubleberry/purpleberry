using UnityEngine;
using UnityEngine.InputSystem;

public class UnitCanvasController : MonoBehaviour
{
    /*    
    private GameObject _button0;
    private GameObject _button1;
    private GameObject _button2;

    void Start()
        {
            _button0 = transform.GetChild(0).transform.GetChild(0).gameObject;
            if (_button0 == null)
            {
                Debug.LogError("Officer Data is Null.");
            }
        }
        public void OnClick0()
        {
            Debug.Log("Button0 from Unit is clicked");
        }

        public void OnClick1()
        {
            Debug.Log("Button1 from Unit is clicked");
        }

        public void OnClick2()
        {
            Debug.Log("Button2 from Unit is clicked");
        }

     */
    private void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward * -1;
    }

}
