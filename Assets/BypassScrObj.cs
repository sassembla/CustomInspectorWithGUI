using UnityEngine;
using UnityEditor;

public class BypassScrObj : ScriptableObject {
	public Box box;
	public void UpadteData (Box box) {
		this.box = box;
	}
}
