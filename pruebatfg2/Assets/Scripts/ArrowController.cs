using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Google.XR.Cardboard;

public class ArrowController : MonoBehaviour
{

    GameObject scancube;
    public int arrowclicked = 0;

    /// <summary>
    /// The material to use when this object is inactive (not being gazed at).
    /// </summary>
    public Material InactiveMaterial;

    /// <summary>
    /// The material to use when this object is active (gazed at).
    /// </summary>
    public Material GazedAtMaterial;

    /// <summary>
    /// The material to use when this object is active (gazed at).
    /// </summary>
    public Material ClickedMaterial;

    

    // Start is called before the first frame update
    void Start()
    {
        scancube = GameObject.Find("ScanCube");
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// This method is called by the Main Camera when it starts gazing at this GameObject.
    /// </summary>
    public void OnPointerEnter()
    {
        GetComponent<Renderer>().material = GazedAtMaterial;
    }

    /// <summary>
    /// This method is called by the Main Camera when it stops gazing at this GameObject.
    /// </summary>
    public void OnPointerExit()
    {
        GetComponent<Renderer>().material = InactiveMaterial;
    }

    /// <summary>
    /// This method is called by the Main Camera when it is gazing at this GameObject and the screen
    /// is touched.
    /// </summary>
    public void OnPointerClick()
    {
        GetComponent<Renderer>().material = ClickedMaterial;
        if (gameObject.name == "arrow1") { //para que el if de ModifyILD sume o reste
            arrowclicked = 1; 
        }
        else if (gameObject.name == "arrow2")
        {
            arrowclicked = 2;
        }
        scancube.GetComponent<ModifyParams>().ModifyILD();

    }
}
