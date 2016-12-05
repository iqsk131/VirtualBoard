using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// Handler for UI buttons on the scene.  Also performs some
// necessary setup (initializing the firebase app, etc) on
// startup.
public class BoardUpdater : MonoBehaviour {

	private const string DatabaseUrl = "https://virtualboard-5b7ad.firebaseio.com/";
	private DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
	public static BoardUpdater Instance;
	public const int Width = 80;
	public const int Height = 60;
	public DatabaseReference Reference;
	public bool[,] Board;

	// When the app starts, check to make sure that we have
	// the required dependencies to use Firebase, and if not,
	// add them if possible.
	void Start() {
		Instance = this;
		Board = new bool[Width,Height];

		Debug.Log("Start!!");

		dependencyStatus = FirebaseApp.CheckDependencies();
		if (dependencyStatus != DependencyStatus.Available) {
			FirebaseApp.FixDependenciesAsync().ContinueWith(task => {
				dependencyStatus = FirebaseApp.CheckDependencies();
				if (dependencyStatus == DependencyStatus.Available) {
					InitializeFirebase();
				} else {
					// This should never happen if we're only using Firebase Analytics.
					// It does not rely on any external dependencies.
					Debug.LogError(
						"Could not resolve all Firebase dependencies: " + dependencyStatus);
				}
			});
		} else {
			InitializeFirebase();
		}
	}

	// Initialize the Firebase database:
	private void InitializeFirebase() {

		Debug.Log("Initial Firebase!!");

		// Set up the Editor before calling into the realtime database.
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(DatabaseUrl);
		//FirebaseApp.DefaultInstance.SetEditorAuthUserId("q3uFGcyaB3OHJ5wuTdAyMvBsaQ02");

		// Get the root reference location of the database.
		Reference = FirebaseDatabase.DefaultInstance.RootReference;

		Debug.Log("Add Event Listener!!");

		Reference.ChildAdded += (object sender, ChildChangedEventArgs args) => {
			if (args.DatabaseError != null) {
				Debug.LogError(args.DatabaseError.Message);
				return;
			}
			int x=int.Parse(args.Snapshot.Key.Substring(1,4));
			int y=int.Parse(args.Snapshot.Key.Substring(6,4));
			Debug.Log("Added: " + x + ", " + y);
			GameObject pixel = GameObject.Instantiate(Resources.Load("Pixel")) as GameObject;
			pixel.transform.position = new Vector3(x,0,y);
			Board[x,y]=true;
			if(GameObject.Find("p" + x + "," + y) == null){
				pixel.name="p" + x + "," + y;
			}
		};
		Reference.ChildRemoved += (object sender, ChildChangedEventArgs args) => {
			if (args.DatabaseError != null) {
				Debug.LogError(args.DatabaseError.Message);
				return;
			}
			int x=int.Parse(args.Snapshot.Key.Substring(1,4));
			int y=int.Parse(args.Snapshot.Key.Substring(6,4));
			Debug.Log("Removed: " + x + ", " + y);
			Board[x,y]=false;
			if(GameObject.Find("p" + x + "," + y) != null){
				GameObject.Destroy(GameObject.Find("p" + x + "," + y));
			}
		};
	}

	public void WriteBoard(double x, double y){
		WriteBoard((int)x, (int)y);
	}

	public void WriteBoard(int x, int y){
		if(x < 0 || x > Width || y < 0 || y > Height) return ;
		if(Board[x,y] == true) return ;
		if(Reference != null){
			string key = "p" + x.ToString().PadLeft(4,'0') + "," + y.ToString().PadLeft(4,'0');
			Reference.Child(key).SetValueAsync("true");
			GameObject pixel = GameObject.Instantiate(Resources.Load("Pixel")) as GameObject;
			pixel.transform.position = new Vector3(x,0,y);
			Board[x,y]=true;
			if(GameObject.Find("p" + x + "," + y) == null){
				pixel.name="p" + x + "," + y;
			}
		}
	}
}