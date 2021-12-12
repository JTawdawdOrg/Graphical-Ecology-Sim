using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyStateMachine : StateMachine
{
	  [SerializeField] float radius = 90f;
    [SerializeField] int memorySize = 10;
	  [SerializeField] float detectionAngle = 90;
    [SerializeField] public LayerMask detectionMasks;
    
    [SerializeField] protected GameObject babyDeerPrefab;
    [SerializeField] protected GameObject maleDeerPrefab;
    [SerializeField] protected GameObject femaleDeerPrefab;
	
    protected override void Start()
    {
		  detectionMasks=LayerMask.GetMask("Predator");
        SetState(new Idle(this));
        base.Start();
    }

    protected override void Update()
    {
		predator = CheckForPredators();
		if (!(predator==null)){
			SetState(new Flee(this));
		}
        base.Update();
        _state.OnUpdate();
    }
	
	private GameObject CheckForPredators()
	{
		Collider[] results = new Collider[memorySize];
		Vector3 direction;
        RaycastHit hit;
        float angle;
		float minDistance = float.MaxValue;
        GameObject closestObj = null;
		if (Physics.OverlapSphereNonAlloc(transform.position, radius, results, detectionMasks) > 0)
		{
			foreach (Collider obj in results){
				if (!obj)	
                    continue;
				direction = obj.transform.position - transform.position;
				angle = Mathf.Abs(Vector3.Angle(transform.forward, direction));

				if (angle > detectionAngle && angle < -detectionAngle)
					continue;
				
				if (Physics.Raycast(transform.position, direction, out hit, radius) && hit.transform.gameObject == obj.gameObject)
				{
					float distance = Vector3.Distance(transform.position, obj.transform.position);
					if (distance < minDistance)
					{
						minDistance = distance;
						closestObj = obj.gameObject;
					}
				}
			}
			return closestObj;
		}
		return null;
	}
  
  public override void SpawnBaby()
    {
        GameObject temp = Instantiate(babyDeerPrefab, transform.position, Quaternion.identity);
        temp.GetComponent<StateMachine>().isBaby = true;
    }

    public override void Mature()
    {
        int rndm = Random.Range(1, 3);
        if (rndm == 1)
            Instantiate(maleDeerPrefab, transform.position, Quaternion.identity);
        else
            Instantiate(femaleDeerPrefab, transform.position, Quaternion.identity);

        MyDestroy(this.gameObject);
    }
  
}