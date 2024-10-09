using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorKill : MonoBehaviour
{
    public Material skyboxMaterial;

    public void ChangeSkyBox(){
        RenderSettings.skybox = skyboxMaterial;
    }
}
