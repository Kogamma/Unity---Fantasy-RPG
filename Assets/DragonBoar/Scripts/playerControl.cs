using UnityEngine;
using System.Collections;

public class playerControl : MonoBehaviour 
{
	public Animator anim;
	int scream;
	int basicAttack;
	int getHit;
	int walk;
	int die;
	int run;


	void Awake () 
	{
		anim = GetComponent<Animator>();
		scream = Animator.StringToHash("Scream");
		basicAttack = Animator.StringToHash("Basic Attack");
		getHit = Animator.StringToHash("Get Hit");
		walk = Animator.StringToHash("Walk");
		die = Animator.StringToHash("Die");
		run = Animator.StringToHash ("Run");
	}


	public void Scream ()
	{
		anim.SetTrigger(scream);
	}

	public void BasicAttack ()
	{
		anim.SetTrigger(basicAttack);
	}

	public void GetHit ()
	{
		anim.SetTrigger(getHit);
	}

	public void Walk ()
	{
		anim.SetTrigger(walk);
	}

	public void Die ()
	{
		anim.SetTrigger(die);
	}

	public void Run ()
	{
		anim.SetTrigger(run);
	}
		
}
