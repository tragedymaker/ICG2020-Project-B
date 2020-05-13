﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Primitives : MonoBehaviour
{
    [SerializeField] GameObject m_CubePrefab;
    [SerializeField] GameObject m_SpherePrefab;
    [SerializeField] Vector2 m_Dimension=new Vector2(5,5);
    // Start is called before the first frame update
    void Start()
    {
        GeneratePrimitives(m_CubePrefab, Random.Range(5,10));
        GeneratePrimitives(m_SpherePrefab, Random.Range(5, 10));
    }

    // Update is called once per frame
 void GeneratePrimitives(GameObject primitive, int count)
    {
        for(int i=0; i<count; i++)
        {
            var primitiveIns = GameObject.Instantiate(primitive);
            primitiveIns.transform.localPosition = new Vector3(
                Random.Range(-m_Dimension.x, m_Dimension.x),
                3f,
                Random.Range(-m_Dimension.y, m_Dimension.y));
        }
    }
        
}
