using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;

public class scounte : MonoBehaviour {

	int count;
	public Text countText;
	bool isFirebaseAvaible;

	// Use this for initialization
	void Start () {

		Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
			var dependencyStatus = task.Result;
			if (dependencyStatus == Firebase.DependencyStatus.Available) {
				isFirebaseAvaible = true;
			} else {
				UnityEngine.Debug.LogError(System.String.Format(
					"Could not resolve all Firebase dependencies: {0}", dependencyStatus));
				isFirebaseAvaible = false;
			}
		});

		if (isFirebaseAvaible) {
			FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://fir-test-5d377.firebaseio.com");

			DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

			count = 0;
			//SetCountText (); 

			//FirebaseDatabase.DefaultInstance
			//	.GetReference("/counters/NL3vgzJOz7Mufhz1ZenG/count")
			//	.ValueChanged += HandleValueChanged;

			FirebaseDatabase.DefaultInstance
			.GetReference ("counter")
			.GetValueAsync ().ContinueWith (task => {
				if (task.IsFaulted) {
					// Handle the error...
				} else if (task.IsCompleted) {
					DataSnapshot snapshot = task.Result;
					// Do something with snapshot...
					Debug.Log (snapshot);
					countText.text = snapshot.Value.ToString ();

						FirebaseDatabase.DefaultInstance
							.GetReference("counter")
							.ValueChanged += HandleValueChanged;

				}
			});
		} else {
			countText.text = "Error in Firebase dependencies";
		}
	}


	// Update is called once per frame
	void Update () {

	}

	public void ButtonClick(){
		count++;
		SetCountText ();
	}

	void SetCountText(){
		countText.text = count.ToString();
	}

	void HandleValueChanged(object sender, ValueChangedEventArgs args) {
		if (args.DatabaseError != null) {
			Debug.LogError(args.DatabaseError.Message);
			return;
		}

		countText.text = args.Snapshot.Value.ToString();
	}
}
