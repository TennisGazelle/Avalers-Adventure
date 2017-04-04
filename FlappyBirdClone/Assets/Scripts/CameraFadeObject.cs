using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFadeObject : MonoBehaviour {

    GameObject player;
    List<Transform> hiddenObjects = new List<Transform>();
    float timer = 0.0f;

	// Use this for initialization
	void Awake () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
       HideObjects();

        timer += Time.deltaTime;

        // reshow objects 
        if (timer > 2.0f)
        {
            ShowObjects();
            timer = 0.0f;
        }
        
	}

    void HideObjects(){
        RaycastHit[] hits;
        float distance = Vector3.Distance(transform.position, player.transform.position);

        hits = Physics.RaycastAll(transform.position, transform.forward, distance);
        int i;

        for (i = 0; i < hits.Length; i++)
        {
            // if not player, fade object
            if (hits[i].transform.gameObject.tag != "Player" || hits[i].transform.gameObject.tag != "Floor")
            {
                RaycastHit hit = hits[i];

                Renderer rend = hit.transform.GetComponent<Renderer>();
                if (rend != null)
                {
                    rend.material.shader = Shader.Find("Transparent/Diffuse");
                    Color color = rend.material.color;
                    color.a = .5f;
                    rend.material.color = color;
                    hiddenObjects.Add(hits[i].transform);
                }

                // turn off all children
                for (int j = 0; j < hits[i].transform.childCount; j++)
                {
                    rend = hits[i].transform.GetChild(j).GetComponent<Renderer>();
                    if (rend != null)
                    {
                        rend.material.shader = Shader.Find("Transparent/Diffuse");
                        Color color = rend.material.color;
                        color.a = .5f;
                        rend.material.color = color;
                        hiddenObjects.Add(hits[i].transform.GetChild(j));
                    }
                }
            }
        }
    }

    void ShowObjects()
    {
        Debug.Log("trying to show something");
        int i;
        for (i = 0; i < hiddenObjects.Count; i++)
        {
            Debug.Log("Showing something");
            Renderer rend = hiddenObjects[i].GetComponent<Renderer>();
            if (rend != null)
            {
                rend.material.shader = Shader.Find("Transparent/Diffuse");
                Color color = rend.material.color;
                color.a = 100;
                rend.material.color = color;
            }
        }

        hiddenObjects.Clear();

    }
}
