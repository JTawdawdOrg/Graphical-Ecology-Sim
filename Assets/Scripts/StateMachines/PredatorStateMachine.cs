/*
Program: PredatorStateMachine.cs
Date Created: ‎18/10/‎2021
Description: Class for predator creatures
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorStateMachine : StateMachine
{
	[SerializeField] private GameObject babyWolfPrefab;
	[SerializeField] private GameObject maleWolfPrefab;
	[SerializeField] private GameObject femaleWolfPrefab;

	protected override void Start()
	{
		SetState(new Idle(this));
		base.Start();
	}
	
	protected override void Update()
	{
		base.Update();
		_state.OnUpdate();
	}

	public override void SpawnBaby()
	{
		GameObject temp = Instantiate(babyWolfPrefab, transform.position, Quaternion.identity);
		temp.GetComponent<StateMachine>().isBaby = true;
	}
	
	//Matures creatures by spawning an adult of random gender over them and deleting themself
	public override void Mature()
	{
		int rndm = Random.Range(1, 3);
		if (rndm == 1)
			Instantiate(maleWolfPrefab, transform.position, Quaternion.identity);
		else
			Instantiate(femaleWolfPrefab, transform.position, Quaternion.identity);

		MyDestroy(this.gameObject);
	}
}
