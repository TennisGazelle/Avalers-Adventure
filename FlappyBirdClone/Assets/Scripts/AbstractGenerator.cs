using UnityEngine;
using System.Collections;

public class AbstractGenerator : MonoBehaviour {
	// elements that will be generated
	protected ArrayList AllElements = new ArrayList();

	// element against which to check for destruction
	public Transform Destroyer;

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

    protected void SpawnBushesAt(GameObject[] arrayToDisplay, float changeInX){

    }
}