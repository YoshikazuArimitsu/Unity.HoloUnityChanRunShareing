using Academy.HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanController : Singleton<UnityChanController> {
    //private AudioSource audioSource = null;
    private Animator Animator_;
    private Locomotion Locomotion_;
    private GameObject TargetCube_;

    // Use this for initialization
    void Start () {
        Animator_ = GetComponent<Animator>();
        Locomotion_ = new Locomotion(Animator_);

        //audioSource = gameObject.AddComponent<AudioSource>();
        //audioSource.playOnAwake = false;
        //audioSource.spatialize = true;
        //audioSource.spatialBlend = 1.0f;
        //audioSource.dopplerLevel = 0.0f;
        //audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
        //audioSource.maxDistance = 20f;
        //audioSource.Stop();
    }

    // Update is called once per frame
    void Update () {
        if (TargetCube_ != null) {
            // UnityChan.position -> TargetCube の角度
            float trans_to_target_angle = GetAim(
                new Vector2(transform.position.x, transform.position.z),
                                        new Vector2(TargetCube_.transform.position.x, TargetCube_.transform.position.z));

            // UnityChan.forward から TargetCube の角度
            float forward_angle = GetAim(new Vector2(0, 0),
                new Vector2(transform.forward.x, transform.forward.z));
            var delta = Mathf.DeltaAngle(trans_to_target_angle, forward_angle);

            //Debug.Log(string.Format("Unity to Target Rad: {0}", delta));

            // Locomotionに適用
            //JoystickToEvents.Do(transform, Camera.main.transform, 1, raddeg);
            Locomotion_.Do(10.0f, delta);
        }

        if(this.transform.position.y < -20.0f) {
            // 奈落に落ちた？
            /*
            AudioClip impactClip = Resources.Load<AudioClip>("Impact");
            audioSource.clip = impactClip;
            audioSource.Play();
            */

            // 停止
            Destroy(TargetCube_);
            TargetCube_ = null;

            // 目の前に落ちてくる
            var pos = Camera.main.transform.position + (Camera.main.transform.forward.normalized * 2);
            var pos_y = new Vector3(pos.x, pos.y + 30, pos.z);
            this.transform.position = pos_y;

            Locomotion_.Do(0.0f, -180);
        }
    }

    public void SetTargetCube(GameObject o)
    {
        // 既に存在していれば置き換える
        if (TargetCube_ != null) {
            Destroy(TargetCube_);
            TargetCube_ = null;
        }

        TargetCube_ = o;
    }

    public float GetAim(Vector2 p1, Vector2 p2)
    {
        float dx = p2.x - p1.x;
        float dy = p2.y - p1.y;
        float rad = Mathf.Atan2(dy, dx);
        return rad * Mathf.Rad2Deg;
    }

    void OnTriggerEnter(Collider c)
    {
        //Debug.Log("UnityChan TriggerEnter!");

        // TargetCubeに当たったら止める
        if (c.tag.Contains("TargetCube")) {
            Destroy(TargetCube_);
            TargetCube_ = null;

            //AudioClip impactClip = Resources.Load<AudioClip>("Impact");
            //audioSource.clip = impactClip;
            //audioSource.Play();
        }
    }
}
