using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public static class ClassExtensionFadeIn
{
    public static List<GameObject> GetAllChilds(this GameObject Go)
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < Go.transform.childCount; i++)
        {
            list.Add(Go.transform.GetChild(i).gameObject);
        }
        return list;
    }
}

public class FadeInGO : MonoBehaviour
{



    /* 
    enable gameobject to start fade in
    */


    public float fadeInSpeed = 0.001f;

    public bool fadeInFinished;

    Color albedoColor;

    private float alphastate;


    private void OnEnable()
    {
        fadeInFinished = false;
        StartCoroutine(FadeInCoroutine());

    }

    private void OnDisable()
    {
        fadeInFinished = false;
    }

    public IEnumerator FadeInCoroutine()
    {
        Debug.Log("mpika stin fade");

        
       

        //make every child's alpha channel to 0
        foreach (var child in gameObject.GetAllChilds())
        {
            albedoColor = child.GetComponent<MeshRenderer>().material.color;
            albedoColor.a = 0;
            child.GetComponent<MeshRenderer>().material.color = albedoColor;

        }

        /*
        albedoColor = GetComponent<MeshRenderer>().material.color;
        albedoColor.a = 0;
        GetComponent<MeshRenderer>().material.color = albedoColor;
        */

        albedoColor.a = 255;

        while (alphastate < 1)
        {
            foreach (var child in gameObject.GetAllChilds())
            {
                child.GetComponent<MeshRenderer>().material.color = Color.Lerp(child.GetComponent<MeshRenderer>().material.color, albedoColor, fadeInSpeed * Time.deltaTime);
                alphastate = child.GetComponent<MeshRenderer>().material.color.a;
                Debug.Log(alphastate);
            }
            yield return new WaitForEndOfFrame();

            /*
            GetComponent<MeshRenderer>().material.color = Color.Lerp(GetComponent<MeshRenderer>().material.color, albedoColor, fadeInSpeed * Time.deltaTime);
            //Debug.Log("mpika stin while");
            */

        }
        
        
        fadeInFinished = true;
    }


    

}
