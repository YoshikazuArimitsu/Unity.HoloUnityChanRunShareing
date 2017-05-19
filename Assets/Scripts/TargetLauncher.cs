using Academy.HoloToolkit.Sharing;
using Academy.HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLauncher : MonoBehaviour {
    public GameObject TargetPrefab_;
    private GameObject Target_;

	// Use this for initialization
	void Start () {
        CustomMessages.Instance.MessageHandlers[CustomMessages.TestMessageID.InstantiateTarget] = ProcessRemoteInstantiateTarget;
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnSelect() {
        if (CursorManager.Instance.CursorOnHolograms.activeSelf == false) {
            Debug.Log("Cursor is not active.");
        }

        var pos = CursorManager.Instance.CursorOnHolograms.transform.position;
        Debug.Log("SpawnTarget at " + pos);
        instantiateTarget(pos);

        // 
        Transform anchor = ImportExportAnchorManager.Instance.gameObject.transform;
        Vector3 v = anchor.InverseTransformPoint(pos);
        Debug.Log(string.Format("PostInstantiateTarget : Pos(AnchorLocal)={0}, Pos(MyWorld)={1}",
               pos, v));
        CustomMessages.Instance.SendInstantiateTarget(pos);
    }

    /// <summary>
    /// Process user hit.
    /// </summary>
    /// <param name="msg"></param>
    void ProcessRemoteInstantiateTarget(NetworkInMessage msg) {
        // Parse the message
        long userID = msg.ReadInt64();
        /*
        Vector3 remoteProjectilePosition = CustomMessages.Instance.ReadVector3(msg);
        Vector3 remoteProjectileDirection = CustomMessages.Instance.ReadVector3(msg);

        Transform anchor = ImportExportAnchorManager.Instance.gameObject.transform;
        ShootProjectile(anchor.TransformPoint(remoteProjectilePosition), anchor.TransformDirection(remoteProjectileDirection), userID);
        */

        Vector3 remoteInstantiatePosition = CustomMessages.Instance.ReadVector3(msg);
        Transform anchor = ImportExportAnchorManager.Instance.gameObject.transform;
        Vector3 v = anchor.TransformPoint(remoteInstantiatePosition);

        Debug.Log(string.Format("ProcessRemoteInstantiateTarget : Pos(AnchorLocal)={0}, Pos(MyWorld)={1}",
               remoteInstantiatePosition, v));

        instantiateTarget(v);
    }

    void destroyTarget() {
        if (Target_ != null) {
            Destroy(Target_);
        }
        Target_ = null;

    }

    void instantiateTarget(Vector3 vec) {
        destroyTarget();

        this.Target_ = GameObject.Instantiate(TargetPrefab_);
        this.Target_.transform.position = vec;
    }

}
