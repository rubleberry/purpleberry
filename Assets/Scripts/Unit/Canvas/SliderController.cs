using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public float curHP;
    public float maxHP;
    public Slider HpBarSlider;
    private string _nameOfOfficer;
    private UnitController _unitController;
    private Image _image;
    private ColorBlock _colorBlock;
    private float _basicColorMultiplier = 2f;
    private float _minColorMultiplier = 1f;
    private Color _defaultColor = Color.blue;


    void Start()
    {
        _unitController = this.transform.parent.parent.parent.GetComponent<UnitController>();
        if (_unitController == null)
        {
            Debug.LogError("Unit Controller is Null.");
        }
        maxHP = _unitController.maxtroops;  
        curHP = _unitController.troops;
        _nameOfOfficer = _unitController.name;
        _image = HpBarSlider.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>();
        _image.color = _defaultColor;
        _colorBlock = HpBarSlider.colors;
        _colorBlock.colorMultiplier = _basicColorMultiplier;
        HpBarSlider.colors = _colorBlock;
    }
    private void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
        curHP = _unitController.troops;
        HpBarSlider.value = curHP / maxHP;
        _colorBlock = HpBarSlider.colors;
        _colorBlock.colorMultiplier = _minColorMultiplier + HpBarSlider.value;
        HpBarSlider.colors = _colorBlock;
    }
}