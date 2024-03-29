﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball2D : MonoBehaviour
{
    /// <summary>
    /// 物理剛体
    /// </summary>
    private Rigidbody2D physics = null;

    /// <summary>
    /// 発射方向
    /// </summary>
    [SerializeField]
    private LineRenderer direction = null;

    /// <summary>
    /// 最大付与力量
    /// </summary>
    private const float MaxMagnitude = 2f;

    /// <summary>
    /// 発射方向の力
    /// </summary>
    private Vector3 currentForce = Vector3.zero;

    /// <summary>
    /// メインカメラ
    /// </summary>
    private Camera mainCamera = null;

    /// <summary>
    /// メインカメラ座標
    /// </summary>
    private Transform mainCameraTransform = null;

    /// <summary>
    /// ドラッグ開始点
    /// </summary>
    private Vector3 dragStart = Vector3.zero;

    public void Awake()
    {
        this.physics                = this.GetComponent<Rigidbody2D>();
        this.mainCamera             = Camera.main;
        this.mainCameraTransform    = this.mainCamera.transform;
    }

    private Vector3 GetMousePosition()
    {
        Vector3 position = Input.mousePosition;
        position.z = this.mainCameraTransform.position.z;
        position = this.mainCamera.ScreenToWorldPoint(position);
        position.z = 0;
        return position;
    }

    public void OnMouseDown()
    {
        this.dragStart = this.GetMousePosition();

        this.direction.enabled = true;
        this.direction.SetPosition(0, this.physics.position);
        this.direction.SetPosition(1, this.physics.position);
    }

    public void OnMouseDrag()
    {
        var position        = this.GetMousePosition();
        this.currentForce   = position - this.dragStart;

        if (this.currentForce.magnitude > MaxMagnitude * MaxMagnitude)
        {
            this.currentForce *= MaxMagnitude / this.currentForce.magnitude;
        }

        this.direction.SetPosition(0, this.physics.position);
        this.direction.SetPosition(1, this.physics.position + (Vector2) this.currentForce);
    }

    public void OnMouseUp()
    {
        this.direction.enabled = false;
        this.Flip(this.currentForce * 6f);
    }

    public void Flip(Vector3 force)
    {
        this.physics.AddForce(force, ForceMode2D.Impulse);
    }
}
