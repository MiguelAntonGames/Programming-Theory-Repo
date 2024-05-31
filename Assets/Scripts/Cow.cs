using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Cow : Animal {
    [SerializeField] private AudioClip talkSound;
    private const float JUMP_FORCE = 70f;
    private const string TALKING_MESSAGE = "MOO!";

    // POLYMORPHISM
    public override void Jump() {
        if (IsOnGround) {
            RigidBody.AddForce(Vector3.up * JUMP_FORCE, ForceMode.Impulse);
            IsOnGround = false;
        }
    }

    // POLYMORPHISM
    public override void Talk() {
        OnTalk?.Invoke(TALKING_MESSAGE);
        AudioManager.Instance.PlayClip(talkSound);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag(GameManager.GROUND_TAG)) {
            IsOnGround = true;
        }
    }

}
