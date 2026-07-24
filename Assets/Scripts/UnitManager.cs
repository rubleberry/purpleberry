using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField]
    GameObject Unit;
        
    OfficerData _officerData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _officerData = GameObject.Find("Terrain").GetComponent<OfficerData>();
        if ( _officerData == null)
        {
            Debug.LogError("Officer Data is Null.");
        }

    }
    public void CreateUnit(Vector3 position, int index)
    {
        Instantiate(Unit, position, Quaternion.identity).name = _officerData.offdatainarray[index].name;
    }
}
