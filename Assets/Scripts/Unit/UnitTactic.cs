using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitTactic : MonoBehaviour
{
    public GameObject unitToTarget;
    public GameObject targetlocation;
    public bool delegation = false;
    public bool holdposition = false;

    private GameObject _unit;

    Collider[] hitColliders;
/*
    Transform nearest = null;
    float nearestDistanceSqr = Mathf.Infinity;
    List<float> DistanceUnitsAround = new List<float>() { };
    List<GameObject> UnitsAround = new List<GameObject>() { };
*/

    [System.Serializable]
    public class DistanceAndObject
    {
        public float distance;
        public GameObject gameObject;
    }
    List<DistanceAndObject> surroundings = new List<DistanceAndObject> { };
    
    void Start()
    {
        _unit = this.transform.gameObject;
        if (_unit == null)
        {
            Debug.LogError("Unit tactic is Null.");
        }
    }

    public void SetTarget(GameObject target)
    {
        unitToTarget = target;
    }

    void CheckSurroundings()
    {

        hitColliders = Physics.OverlapSphere(transform.position, 10f);
        surroundings.Clear();
        foreach (Collider col in hitColliders)
        {
            float distanceSqr = (col.transform.position - transform.position).sqrMagnitude;
            DistanceAndObject distanceAndObject = new DistanceAndObject();
            distanceAndObject.distance = distanceSqr;
            distanceAndObject.gameObject = col.gameObject;
            if (col.tag == "Unit")
            {
                surroundings.Add(distanceAndObject);
            }
        }
        for (int i = 0; i < surroundings.Count; i++)
        {
            Debug.Log("Index: " + i + " Distance: " + surroundings[i].distance + " Unit: " + surroundings[i].gameObject.name);
        }
        surroundings.Sort((a, b) => a.distance.CompareTo(b.distance));
        for (int i = 0; i < surroundings.Count; i++)
        {
            Debug.Log("Index: " + i + " Distance: " + surroundings[i].distance + " Unit: " + surroundings[i].gameObject.name);
        }
    }
}