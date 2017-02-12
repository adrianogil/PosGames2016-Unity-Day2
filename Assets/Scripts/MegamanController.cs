using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegamanController : MonoBehaviour {

	[Header("Movement")]
	private float pixelToUnit = 40f;
	public float maxVelocity = 10f; // pixels/seconds
	public Vector3 moveSpeed = Vector3.zero; // (0,0,0)

	[Header("Animation")]
	public bool isFacingLeft = false;
	public bool isRunning = false;

	[Header("Components")]
	public Rigidbody2D rigidbody2D;
	public SpriteRenderer spriterenderer;
	public Animator animator;

	// Update is called once per frame
	void Update () {
		UpdateAnimatorParameters ();
		HandleHorizontalMovement ();
		HandleVerticalMovement ();
		MoveCharacterController ();
	}

	// Use this for initialization
	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D> ();
		spriterenderer = GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator> ();
	}

	void UpdateAnimatorParameters() {
		animator.SetBool ("isRunning", isRunning);
	}

	void HandleHorizontalMovement() {
		moveSpeed.x = Input.GetAxis ("Horizontal") * (maxVelocity / pixelToUnit);

		if (moveSpeed.x != 0f) {
			isRunning = true;
		} else {
			isRunning = false;
		}

		if (Input.GetAxis ("Horizontal") < 0 && !isFacingLeft) {
			// Muda o megaman para esquerda
			isFacingLeft = true;
		} else if (Input.GetAxis ("Horizontal") > 0 && isFacingLeft) {
			// Muda o megaman para direita
			isFacingLeft = false;
		}

		spriterenderer.flipX = isFacingLeft;
	}

	void HandleVerticalMovement() {
		
	}

	void MoveCharacterController() {
		rigidbody2D.velocity = moveSpeed;
	}
}
