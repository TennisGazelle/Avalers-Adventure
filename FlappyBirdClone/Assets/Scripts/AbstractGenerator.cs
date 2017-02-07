using UnityEngine;
using System.Collections;

public class AbstractGenerator : MonoBehaviour {

	// elements that will be generated
	protected ArrayList AllElements = new ArrayList();

	// element against which to check for destruction
	public Transform Destroyer;

    private Vector3 changeInX = new Vector3(11.0f,0,0); 

	// erase element
	protected void RemoveUnnecessaryElements() {
		foreach (GameObject go in AllElements) {
			if (go.transform.position.x < Destroyer.position.x) {
				Destroy(go);
				 AllElements.Remove(go);
			}
		}
	}

    protected void SpawnElementAt(GameObject[] arrayToDisplay, ref Vector3 newElementPosition, float changeInX, Transform trans) {
        if (newElementPosition.x + changeInX < System.Math.Abs(transform.position.x)+20) {
            GameObject obj = arrayToDisplay[Random.Range(0, arrayToDisplay.Length)];
            GameObject go = Instantiate(obj, newElementPosition, Quaternion.identity, trans) as GameObject;
            AllElements.Add(go);
            newElementPosition.x += changeInX; 
        }
    }
        
    protected void SpawnBushesAt(GameObject[] arrayToDisplay){

        // instantiate the object
        for (int objNdx = 0; objNdx < arrayToDisplay.Length; objNdx++)
        {
            // add 5 to current obj 
            arrayToDisplay[objNdx].transform.position += changeInX;
            // instantiate with new position and add to all elements
            GameObject go = Instantiate(arrayToDisplay[objNdx], arrayToDisplay[objNdx].transform.position, Quaternion.identity, arrayToDisplay[objNdx].transform);
            AllElements.Add(go);    
        }
    }
}