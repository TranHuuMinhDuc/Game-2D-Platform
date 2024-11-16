using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour
{
    private TextMeshPro textMesh;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public static DamagePopUp create()
    //{

    //}


    public void setUp(int damageAmount)
    {
        textMesh.text = damageAmount.ToString();
    }

}
