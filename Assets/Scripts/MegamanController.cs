using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegamanController : CharacterController2D {

	void Awake()
	{
		// Setup animator parameters
		animatorIsRunning = "isRunning";
		animatorFall = "Fall";
		animatorJump = "Jump";
		animatorLand = "Land";
	}

}
