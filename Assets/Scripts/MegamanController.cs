using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegamanController : MonoBehaviour {

	[Header("Scene References")]
	public Transform groundCheck;

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

		if (RaycastAgainstLayer ("Ground", groundCheck)) {
			// Acertou o chao
		}

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

	// Metodo para tracar um raio na direcao do objeto de referencia
	RaycastHit2D RaycastAgainstLayer(string layerName, Transform endPoint)
	{
		// Unity representa layer como bits - 00000000000000000000000000001001
		int layer = LayerMask.NameToLayer (layerName); // camada 1, camada 2, 3..
		int layerMask = 1 << layer; // camada 2 -> 100, camada 4 -> 10000

		// camadas 2, 4 // (1 << 2) + (1 << 4) // 100 + 10000 = 10100

		Vector2 originPosition = new Vector2 (transform.position.x, 
											  transform.position.y);

		Vector2 direction = endPoint.localPosition.normalized;

		float rayLength = Mathf.Abs(endPoint.localPosition.y);

		RaycastHit2D hit2d = Physics2D.Raycast (originPosition, 
												direction,
												rayLength,
												layerMask);

		#if UNITY_EDITOR
		Color color;

		if (hit2d != null && hit2d.collider != null) {
			color = Color.green; // Acerta o chao
		} else {
			color = Color.red; // Nao acerta o chao
		}

		Debug.DrawLine(originPosition, //Inicio
			originPosition + direction*rayLength, // Fim
			color, 0f, false);

		#endif

		return hit2d;
	}














}
