using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MYFSM
{
	

	public enum PLAYER_STATE
	{
		IDLE,
		WALK,
		ATTACK,
		DIE,

		END,
	}

	public enum ENEMY_01_STATE
	{
		IDLE,
		WALK,
		ATTACK,
		FOLLOW,
		UNFOLLOW,
		DIE,

		END,
	}

}

