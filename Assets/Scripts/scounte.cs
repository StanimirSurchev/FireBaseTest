using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;

public class scounte : MonoBehaviour {

	public int count;
	public Text countText;

	// Use this for initialization
	void Start () {



		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://fir-test-5d377.firebaseio.com");

		DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

		count = 0;
		//SetCountText (); 

		//FirebaseDatabase.DefaultInstance
		//	.GetReference("/counters/NL3vgzJOz7Mufhz1ZenG/count")
		//	.ValueChanged += HandleValueChanged;

		FirebaseDatabase.DefaultInstance
			.GetReference("counter")
			.GetValueAsync().ContinueWith(task => {
				if (task.IsFaulted) {
					// Handle the error...
				}
				else if (task.IsCompleted) {
					DataSnapshot snapshot = task.Result;
					// Do something with snapshot...
					Debug.Log(snapshot);
					countText.text = snapshot.Value.ToString();
					//count = task.Result;
					//SetCountText();
				}
			});
	}


	// Update is called once per frame
	void Update () {

	}

	public void ButtonClick(){
		count++;
		SetCountText ();
	}

	void SetCountText(){
		countText.text = count.ToString ();
	}
}
