using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Google.XR.Cardboard;

public class NumberController : MonoBehaviour
{

    GameObject scancube;
    public int numberclicked;

    float cooldownboton; //para que no aparezcan un monton de numeros seguidos si mantienes el cubo pulsado

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
        if (cooldownboton > 0)
        {
            cooldownboton -= Time.deltaTime;
        }
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

        //switch (gameObject.name)
        //{
        //    case "Cube1":
        //        numberclicked = 1;
        //        break;
        //    case "Cube2":
        //        numberclicked = 2;
        //        break;
        //    case "Cube3":
        //        numberclicked = 3;
        //        break;
        //    case "Cube4":
        //        numberclicked = 4;
        //        break;
        //    case "Cube5":
        //        numberclicked = 5;
        //        break;
        //    case "Cube6":
        //        numberclicked = 6;
        //        break;
        //    case "Cube7":
        //        numberclicked = 7;
        //        break;
        //    case "Cube8":
        //        numberclicked = 8;
        //        break;
        //    case "Cube9":
        //        numberclicked = 9;
        //        break;
        //    case "Cube0":
        //        numberclicked = 0;
        //        break;
        //    case "Cubecoma":
        //        numberclicked = 10;
        //        break;
        //    case "Cubedel":
        //        numberclicked = 11;
        //        break;
        //    default:
        //        print("otro");
        //        break;
        //}

        switch (gameObject.name)
        {
            case "Cube1":
                scancube.GetComponent<ModifyParams>().numberclicked = 1;
                break;
            case "Cube2":
                scancube.GetComponent<ModifyParams>().numberclicked = 2;
                break;
            case "Cube3":
                scancube.GetComponent<ModifyParams>().numberclicked = 3;
                break;
            case "Cube4":
                scancube.GetComponent<ModifyParams>().numberclicked = 4;
                break;
            case "Cube5":
                scancube.GetComponent<ModifyParams>().numberclicked = 5;
                break;
            case "Cube6":
                scancube.GetComponent<ModifyParams>().numberclicked = 6;
                break;
            case "Cube7":
                scancube.GetComponent<ModifyParams>().numberclicked = 7;
                break;
            case "Cube8":
                scancube.GetComponent<ModifyParams>().numberclicked = 8;
                break;
            case "Cube9":
                scancube.GetComponent<ModifyParams>().numberclicked = 9;
                break;
            case "Cube0":
                scancube.GetComponent<ModifyParams>().numberclicked = 0;
                break;
            case "Cubecoma":
                scancube.GetComponent<ModifyParams>().numberclicked = 10;
                break;
            case "Cubedel":
                scancube.GetComponent<ModifyParams>().numberclicked = 11;
                break;
            case "Cubechange":
                scancube.GetComponent<ModifyParams>().numberclicked = 12;
                break;
            default:
                print("otro");
                break;
        }

        //hacer un metodo en ModifyParams que dependa del numberclicked creo o ver si puedo hacerlo en el metodo modifyILD

        if (cooldownboton <= 0)
        {
            cooldownboton = 0.25f;  // this is the interval between firing.
            scancube.GetComponent<ModifyParams>().ModILDNumber();
        }

    }
}
