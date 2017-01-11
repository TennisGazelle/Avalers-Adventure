using UnityEngine;
using System.Collections;

public class AbstractGenerator : MonoBehavior {
	// elements that will be generated
	protected ArrayList AllElements = new ArrayList;

	// element against which to check for destruction
	public Transform Destroyer;

	// erase element
	void RemoveUnnecessaryElements() {
		foreach (GameObject go in AllElements) {
			if (go.transform.position.x < Destroyer.position.x) {
				Destroy(go);
				AllElements.Remove(go);
			}
		}
	}
}