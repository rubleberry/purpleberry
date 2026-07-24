using System;
using UnityEngine;

public class OfficerData : MonoBehaviour
{
    public float battle_coef;

    [Serializable]
    public class officerClass
    {
        public int affiliation = 0;
        public string name = "testname";
        public int troops = 3000;
        public int maxtroops = 5000;
        public int morale = 100;
        public float speed = 1;
        public float atk = 1;
        public float def = 1;
        public float arm = 1;
        public bool isLeadingUnit = false;
        public bool isWorking = false;
    }

    [Serializable]
    public class officersClass
    {
        public officerClass[] officers;
    }

    public officerClass[] offdatainarray = new officerClass[5];


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TestInit();
    }

    public void TestInit()
    {
        ChangeName(0, "Arctic");
        ChangeAffiliation(0, 1);
        ChangeName(1, "Purple");
        ChangeAffiliation(1, 1);
        ChangeName(2, "Fobos");
        ChangeName(3, "DeRob");
    }

    public void ChangeName(int index, string name)
    {
        offdatainarray[index].name = name;
    }
    
    public void ChangeTroops(int index, int troops)
    {
        offdatainarray[index].troops = troops;
    }

    public void ChangeAffiliation(int index, int affiliation)
    {
        offdatainarray[index].affiliation = affiliation;
    }

}
