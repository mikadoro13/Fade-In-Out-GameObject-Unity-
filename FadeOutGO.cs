using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class ClassExtensionFadeOut
{
    static List<GameObject> GetAllChilds(this GameObject Go)
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < Go.transform.childCount; i++)
        {
            list.Add(Go.transform.GetChild(i).gameObject);
        }
        return list;
    }
}

public class FadeOutGO : MonoBehaviour
{
    public bool StartFadeOut;
    public float fadeOutSpeed = 0.8f;
    public bool DisableOnFadeOut = true;
    public bool DestroyOnFadeOut;
    public bool FadeOutPending;

    
    Color albedoColor;
    float alphaState = 1;

    private void Update()
    {
        if (StartFadeOut == true && FadeOutPending == false)
        {
            FadeOutPending = true;
            StartCoroutine(FadeOutCoroutine());
        }
    }


    public IEnumerator FadeOutCoroutine()
    {
        

        Debug.Log("Started fading out...");

       
        albedoColor.a = 0;

        while (alphaState > 0.01f)
        {
            foreach (var child in gameObject.GetAllChilds())
            {
                child.GetComponent<MeshRenderer>().material.color = Color.Lerp(child.GetComponent<MeshRenderer>().material.color, albedoColor, fadeOutSpeed * Time.deltaTime);
                alphaState = child.GetComponent<MeshRenderer>().material.color.a;
                Debug.Log(alphaState);
            }

            yield return new WaitForEndOfFrame();
            //Debug.Log("mpika stin while");
        }

            if (DestroyOnFadeOut == true)
            {
                Destroy(this);
            }
            else if (DisableOnFadeOut == true)
            {
                this.gameObject.SetActive(false);
            }

        StartFadeOut = false;
        FadeOutPending = false;
    }
}