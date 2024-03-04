using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBuilding : MonoBehaviour
{
    private UnityEngine.Transform anchor;

    [SerializeField]private int areaWidth = 2;
    [SerializeField]private int areaHeight = 2;

    public int AreaWidth { get => areaWidth; }
    public int AreaHeight { get => areaHeight; }
    public UnityEngine.Transform Anchor { get => anchor; }

    // Start is called before the first frame update
    void Awake()
    {
        anchor = transform.Find("Anchor");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
