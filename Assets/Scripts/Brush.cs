using UnityEngine;
using System.Collections;

public class Brush : MonoBehaviour {

	//[SerializeField] GameObject WhiteBoard;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		if(this.transform.position.x < 0 || this.transform.position.x > BoardUpdater.Width || this.transform.position.z < 0 || this.transform.position.z > BoardUpdater.Height) return ;
		if(this.transform.position.y <1 && BoardUpdater.Instance.Board[(int)this.transform.position.x, (int)this.transform.position.z] == false){
			Debug.Log("Draw at " + this.transform.position.x.ToString().PadLeft(4, '0') + "," + this.transform.position.z.ToString().PadLeft(4, '0'));
			BoardUpdater.Instance.WriteBoard(this.transform.position.x, this.transform.position.z);
			/*BoardUpdater.Instance.WriteBoard(this.transform.position.x-2, this.transform.position.z);
			BoardUpdater.Instance.WriteBoard(this.transform.position.x+2, this.transform.position.z);
			BoardUpdater.Instance.WriteBoard(this.transform.position.x, this.transform.position.z-2);
			BoardUpdater.Instance.WriteBoard(this.transform.position.x, this.transform.position.z+2);
			BoardUpdater.Instance.WriteBoard(this.transform.position.x-1, this.transform.position.z);
			BoardUpdater.Instance.WriteBoard(this.transform.position.x+1, this.transform.position.z);
			BoardUpdater.Instance.WriteBoard(this.transform.position.x, this.transform.position.z-1);
			BoardUpdater.Instance.WriteBoard(this.transform.position.x, this.transform.position.z+1);
			BoardUpdater.Instance.WriteBoard(this.transform.position.x+1, this.transform.position.z+1);
			BoardUpdater.Instance.WriteBoard(this.transform.position.x+1, this.transform.position.z-1);
			BoardUpdater.Instance.WriteBoard(this.transform.position.x-1, this.transform.position.z+1);
			BoardUpdater.Instance.WriteBoard(this.transform.position.x-1, this.transform.position.z-1);*/
		}
	}
}
