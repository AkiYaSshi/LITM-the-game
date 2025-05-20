using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectionHighlight : MonoBehaviour
{
    [SerializeField]
    private const string focusTag = "Focus";

    [SerializeField]
    private Color lighten = new(1, 1, 1, 1);

    private Color normal = new(0, 0, 0, 0);

    [SerializeField]
    private LayerMask unselectLayer; 
    MaterialPropertyBlock lightMat;
    MaterialPropertyBlock normalMat;

    private void Start(){
        if(lightMat == null) lightMat = new();

        if(normalMat == null) normalMat = new();
        lightMat.SetColor("_EmissionColor", lighten);
        normalMat.SetColor("_EmissionColor", normal);
    }
    private void SetHighlight(GameObject target){
        Reset();
        if(target != null)
        {        

            Renderer[] renderer = target.GetComponentsInChildren<Renderer>();
            Debug.Log(renderer.Length);
            foreach(Renderer ren in renderer)
            ren.SetPropertyBlock(lightMat);
        }
    }

    private void Reset()
    {
        List<GameObject> objects = GetAllObjectInLayer();

        foreach(GameObject obj in objects){
            Renderer[] renderer = obj.GetComponentsInChildren<Renderer>();

            foreach(Renderer ren in renderer)
            ren.SetPropertyBlock(normalMat);
        }
    }

    private List<GameObject> GetAllObjectInLayer()
    {
        GameObject[] unfocused = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        List<GameObject> unselectObject = new();

        foreach (GameObject obj in unfocused)
        {
            if (obj.layer == unselectLayer) unselectObject.Add(obj);
        }
        return unselectObject;
    }

    private void OnEnable(){
        SelecctionManager.FocusChange += SetHighlight;
    }
    private void OnDisable(){
        SelecctionManager.FocusChange -= SetHighlight;
    }
}
