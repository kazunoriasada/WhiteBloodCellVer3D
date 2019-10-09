using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    //物理剛体
    private Rigidbody physics = null;
    //発射方向
    [SerializeField]
    private LineRenderer direction = null;
    //最大付与力量
    private const float MaxMagnitude = 2f;
    //発射方向の力
    private Vector3 currentForce = Vector3.zero;
    //メインカメラ
    private Camera mainCamera = null;
    //メインカメラ座標
    private Transform mainCameraTransform = null;
    //ドラッグ開始点
    private Vector3 dragStart = Vector3.zero;
    

    public void Awake()
    {
        this.physics = this.GetComponent<Rigidbody>();
        this.mainCamera = Camera.main;
        this.mainCameraTransform = this.mainCamera.transform;
    }
    //マウス座標をワールド座標に変換して取得
    private Vector3 GetMousePosition()
    {
        // マウスから取得できないZ座標を補完する
        //なぜ、varをVector3へ変更したのか？をきく
        Vector3 position = Input.mousePosition;
        position.z = this.mainCameraTransform.position.z;
        position = this.mainCamera.ScreenToWorldPoint(position);
        position.z = 0;
        return position;
    }
    
    //ドラック開始イベントハンドラ
    public void OnMouseDown()
    {
        this.dragStart = this.GetMousePosition();

        this.direction.enabled = true;
        this.direction.SetPosition(0, this.physics.position);
        this.direction.SetPosition(1, this.physics.position);
    }

    //ドラッグ中イベントハンドラ
    public void OnMouseDrag()
    {
        var position      = this.GetMousePosition();
        this.currentForce = position - this.dragStart;

        if (this.currentForce.magnitude > MaxMagnitude * MaxMagnitude)
        {
            this.currentForce *= MaxMagnitude / this.currentForce.magnitude;
        }

        this.direction.SetPosition(0, this.physics.position);
        //Vector3かVector2かオペランドがあいまいというエラーの解消のためコメントアウト
        this.direction.SetPosition(1, this.physics.position + (Vector3) this.currentForce /* + this.currentForce*/);
    }

    //ドラッグ終了イベントハンドラ
    public void OnMouseUp()
    {
        this.direction.enabled = false;
        this.Flip(this.currentForce * 6f);
    }
    
    public void Flip(Vector3 force)
    {
        // 瞬間的に力を加えてはじく
        //AddForce = 第二引数にForceMode.Impulseを入れることで瞬間的に力を加えたかのような挙動を作ることができる
        //https://www.sejuku.net/blog/54896
        this.physics.AddForce(force, ForceMode.Impulse);
    }

}
