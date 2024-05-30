using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal : MonoBehaviour {
    public Action<string> OnTalk;
    private Rigidbody rb;
    protected Rigidbody RigidBody { get => rb; }
    protected bool IsOnGround = true;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    public abstract void Jump();
    public abstract void Talk();

}
