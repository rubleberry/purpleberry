using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UnitController : MonoBehaviour
{
    public int officerIndex = 0;
    public int affiliation = 0;
    public string leadername = "testname";
    public int troops = 3000;
    public int maxtroops = 5000;
    public int morale = 100;
    public float speed = 1f;
    public float atk = 1;
    public float def = 1;
    public float arm = 1;
    public GameObject target;

    private OfficerData _officerData;
    private Vector3 _destination;
    private GameObject _pointLightTop;
    private GameObject _pointLightBottom;
    private UnityEngine.UI.Image _unitCanvasImage;
    private float _unitCanvasImageAlpha = .5f;
    private Light _pointLightTopComp;
    private Light _pointLightBottomComp;
    private float _lightIntensity = 4;
    private float _lightRange = 2;

    [SerializeField]
    private TextMeshProUGUI _textOnUnitCanvas;
    [SerializeField]
    private Transform _unitCanvas;


    void Start()
    {
        leadername = this.name;
        _officerData = GameObject.Find("Terrain").GetComponent<OfficerData>();
        if (_officerData == null)
        {
            Debug.LogError("Officer Data is Null.");
        }
        OfficerDataLoad();

        _pointLightTop = transform.GetChild(0).gameObject;
        if (_pointLightTop == null)
        {
            Debug.LogError("Point Light Top is Null.");
        }
        _pointLightTopComp = _pointLightTop.GetComponent<Light>();
        if (_pointLightTopComp == null)
        {
            Debug.LogError("Point Light Top is Null.");
        }

        _pointLightBottom = transform.GetChild(1).gameObject;
        if (_pointLightBottom == null)
        {
            Debug.LogError("Point Light Bottom is Null.");
        }
        _pointLightBottomComp = _pointLightBottom.GetComponent<Light>();
        if (_pointLightBottomComp == null)
        {
            Debug.LogError("Point Light Bottom is Null.");
        }
        _unitCanvasImage = transform.GetChild(2).GetComponent<UnityEngine.UI.Image>();
        if (_pointLightBottomComp == null)
        {
            Debug.LogError("Unit Canvas Image is Null.");
        }

        UnitColorSetUp();

        _destination = transform.position;

        Instantiate(_textOnUnitCanvas, _unitCanvas).name = "testText";
        Debug.Log(_textOnUnitCanvas);
    }

    private void UnitColorSetUp()
    {
        if (affiliation == 0)
        {
            _pointLightTopComp.color = Color.blue;
            _pointLightBottomComp.color = Color.blue;
            Color _color = Color.blue;
            _color.a = _unitCanvasImageAlpha;
            _unitCanvasImage.color = _color;
        }
        else if (affiliation == 1)
        {
            _pointLightTopComp.color = Color.red;
            _pointLightBottomComp.color = Color.red;
            Color _color = Color.red;
            _color.a = _unitCanvasImageAlpha;
            _unitCanvasImage.color = _color;
        }
        _pointLightTopComp.intensity = _lightIntensity;
        _pointLightTopComp.range = _lightRange;
        _pointLightBottomComp.intensity = _lightIntensity;
        _pointLightBottomComp.range = _lightRange;

    }

    private void Update()
    {
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            _destination = target.transform.position;
        }
    }

    public void OnClick()
    {
    }

    public void SetDestination(Vector3 destination)
    {
        _destination = destination;
        StartCoroutine(UnitMoveRoutine());
    }

    IEnumerator UnitMoveRoutine()
    {
        int counter = 0;
        WaitForSeconds wfs = new WaitForSeconds(0.01f);
        while (transform.position != _destination)
        {
            yield return wfs;
            transform.position = Vector3.MoveTowards(transform.position, _destination, 2 * speed * Time.deltaTime);
//            Debug.Log(counter);
            counter++;
        }
    }
        
    public void OnCollisionEnter(Collision collision)
    {
        
        if (collision != null)
        {
            if (collision.gameObject.tag == "Unit")
            {
                UnitController enemy = collision.gameObject.GetComponent<UnitController>();
                if (enemy != null)
                {
                    if (enemy.affiliation != this.affiliation)
                    {
                        DamageControl(enemy.troops, enemy.atk, enemy.morale);
                    }
                }
            }
        }

    }

    private void DamageControl(int troops, float atk, float morale)
    {   
        int damage = (int) (troops * atk * morale / 500 - 
            this.troops * this.def * this.morale / 1000);
        //        Debug.Log(this.transform + "Enemy Atk: " + troops * atk * morale / 1000);
        //        Debug.Log(this.transform + "Own Def: " + this.troops * this.def * this.morale / 500);
        //        Debug.Log(damage);
        if (damage < 1)
        {
            damage = 1;
        }
        Debug.Log("DamageTaken:" + damage);
        if (this.troops > damage)
        {
            this.troops -= damage;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    private void OfficerDataLoad()
    {
        int _arrayLength = _officerData.offdatainarray.Length;
        for (int i = 0; i < _arrayLength; i++)
        {
            if (_officerData.offdatainarray[i].name == leadername)
            {
                officerIndex = i;
            }
        }
        _officerData.offdatainarray[officerIndex].name = leadername;
        affiliation = _officerData.offdatainarray[officerIndex].affiliation;
        troops = _officerData.offdatainarray[officerIndex].troops;
        maxtroops = _officerData.offdatainarray[officerIndex].maxtroops;
        morale = _officerData.offdatainarray[officerIndex].morale;
        speed = _officerData.offdatainarray[officerIndex].speed;
        atk = _officerData.offdatainarray[officerIndex].atk;
        def = _officerData.offdatainarray[officerIndex].def;
        arm = _officerData.offdatainarray[officerIndex].arm;
    }
}

