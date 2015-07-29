using UnityEngine;
using UnityEditor;

using System;
using System.Collections;

public class EditorWindowCode : EditorWindow {

	Box box0;
	Box box1;

	[MenuItem("Sample/Open..")]
	public static void Open() {
		GetWindow<EditorWindowCode>();
	}

	public void OnEnable () {
		Initialize();
	}

	public void Initialize () {
		box0 = new Box(0, "A", new Rect(10,10,100,100));
		box1 = new Box(1, "B", new Rect(120,10,100,100));
	}

	public void OnGUI () {
		BeginWindows();

		box0.Draw();
		box1.Draw();

		EndWindows();
	}

}

public class Box {
	private readonly int index;
	public readonly string id;
	private Rect baseRect;

	private BypassScrObj bypass;

	public Box (int index, string id, Rect rect) {
		this.index = index;
		this.id = id;
		this.baseRect = rect;

		this.bypass = ScriptableObject.CreateInstance<BypassScrObj>();
	}

	public void Draw () {
		baseRect = GUI.Window(index, baseRect, WindowCallback, "id:" + id, "flow node 0");
	}

	public void WindowCallback (int id) {
		if (Event.current.type == EventType.MouseDown) {
			bypass.UpadteData(this);
			Selection.activeObject = bypass;
		}

		GUI.DragWindow();
	}
}

[CustomEditor(typeof(BypassScrObj))]
public class BoxInspector : Editor {
	public override void OnInspectorGUI () {
		var box = ((BypassScrObj)target).box;
		if (box == null) return;
		EditorGUILayout.LabelField("Id", box.id);	
	}
}
