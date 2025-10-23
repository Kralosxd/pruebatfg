using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Google.XR.Cardboard;

public class NumberController : MonoBehaviour
{


    public int numberclicked;

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

        //if (gameObject.name == "cube1"){
        //    numberclicked = 1;
        //}
        //else if (gameObject.name == "cube2")
        //{
        //    numberclicked = 2;
        //}
        //else if (gameObject.name == "cube3")
        //{
        //    numberclicked = 3;
        //}
        //else if (gameObject.name == "cube4")
        //{
        //    numberclicked = 4;
        //}
        //else if (gameObject.name == "cube5")
        //{
        //    numberclicked = 5;
        //}
        //else if (gameObject.name == "cube6")
        //{
        //    numberclicked = 6;
        //}
        //else if (gameObject.name == "cube7")
        //{
        //    numberclicked = 7;
        //}
        //else if (gameObject.name == "cube8")
        //{
        //    numberclicked = 8;
        //}
        //else if (gameObject.name == "cube9")
        //{
        //    numberclicked = 9;
        //}
        //else if (gameObject.name == "cube0")
        //{
        //    numberclicked = 0;
        //}

        //switch (numberclicked)
        //{
        //    case 1:
        //        print("cubo1");
        //        break;
        //    default:
        //        print("otro");
        //        break;
        //}

        switch (gameObject.name)
        {
            case "cube1":
                numberclicked = 1;
                break;
            default:
                print("otro");
                break;
        }

        //hacer un metodo en ModifyParams que dependa del numberclicked creo o ver si puedo hacerlo en el metodo modifyILD
        scancube.GetComponent<ModifyParams>().ModILDNumber();

    }
}
