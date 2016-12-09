using UnityEngine;
using System.Collections;

public class Brush : MonoBehaviour {

	[SerializeField] GameObject WhiteBoard;

	// Update is called once per frame
	void Update () {
		BoardUpdater.Instance.SetPointerPosition((this.transform.position.x - WhiteBoard.transform.position.x)*0.15 + BoardUpdater.Width/2, (this.transform.position.y - WhiteBoard.transform.position.y)*0.15+ BoardUpdater.Height/2);
		//Debug.Log((this.transform.position.x - WhiteBoard.transform.position.x) + ", " + (this.transform.position.y - WhiteBoard.transform.position.y));
		//Debug.Log("Board(" + WhiteBoard.transform.position.x + ", " + WhiteBoard.transform.position.y + ", " + WhiteBoard.transform.position.z + ") Marker(" + this.transform.position.x + ", " + this.transform.position.y + ", " + this.transform.position.z + ")");
		/*if(this.transform.position.x < 0 || this.transform.position.x > BoardUpdater.Width || this.transform.position.z < 0 || this.transform.position.z > BoardUpdater.Height) return ;
		if(this.transform.position.y <1 && BoardUpdater.Instance.Board[(int)this.transform.position.x, (int)this.transform.position.z] == false){
			Debug.Log("Draw at " + this.transform.position.x.ToString().PadLeft(4, '0') + "," + this.transform.position.z.ToString().PadLeft(4, '0'));
			BoardUpdater.Instance.WriteBoard(this.transform.position.x, this.transform.position.z);
		}*/
		if(WhiteBoard.transform.position.z - this.transform.position.z <50){
			BoardUpdater.Instance.SetPointerActive(true);
			BoardUpdater.Instance.WriteBoard((this.transform.position.x - WhiteBoard.transform.position.x)*0.15 + BoardUpdater.Width/2, (this.transform.position.y - WhiteBoard.transform.position.y)*0.15+ BoardUpdater.Height/2);
		}
		else{
			BoardUpdater.Instance.SetPointerActive(false);
		}
	}
}
