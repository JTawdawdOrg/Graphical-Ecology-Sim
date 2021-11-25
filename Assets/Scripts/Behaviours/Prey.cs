using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

namespace Behaviours
{
    public class Prey : MonoBehaviour
    {
        //private SphereCollider radius;
        private Boolean isMale = false;
        [Range(10, 100)]
        [SerializeField]
        private float FP = 20;
        [Range(10, 100)]
        [SerializeField]
        private float WP = 20;
        [Range(0.001f, 1)]
        [SerializeField]
        private float Speed = 0.1f;


        //private float Threshold = 10.0f;
        private StateMachine1 stateMachine;
        private float desireability;
        private float gestationPeriod = 1.0f;
        private float hunger, thirst = 0.0f;
        [Range(0.01f, 1.0f)]
        [SerializeField]
        private float thirstThreshold, hungerThreshold = 0.5f;
        [Range(0.01f, 1.0f)]
        [SerializeField]
        private float reproductivrRefactoryPeriod = 0.0f;
        [Range(0.01f, 1.0f)]
        [SerializeField]
        private float reproductivrRefactoryPeriodThreshold = 0.7f;
        private bool hungry, thirsty, shouldFlee, shouldBreed = false;
        
        
        public float Fp
        {
            get => FP;
            set => FP = value;
        }

        public float Wp
        {
            get => WP;
            set => WP = value;
        }

        public float Speed1
        {
            get => Speed;
            set => Speed = value;
        }

        public float ThirstThreshold
        {
            get => thirstThreshold;
            set => thirstThreshold = value;
        }

        public float HungerThreshold
        {
            get => hungerThreshold;
            set => hungerThreshold = value;
        }

        

    
        /*
        public Prey(PreyStateMachine SpicesType, Vector3 pos)
        {
            PreyStateMachine = SpicesType;
            Position = pos;
        }
        */
        public void Start()
        {
            stateMachine = new StateMachine1();
            if (gameObject.tag == "stag")
            {
                isMale = true;
            }
        }
        

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("enviroment"))
            {
                Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), other);
            }
            
            if (other.gameObject.CompareTag("mWolf"))
            {
                stateMachine.SetToFlee();
                FleeFrom(other);
            }
            
            if (other.gameObject.CompareTag("fWolf"))
            {
                stateMachine.SetToFlee();
                FleeFrom(other);
            }
            
            if (other.gameObject.CompareTag("doe"))
            {
                if (isMale)
                {
                    if (canBreed())
                    {
                        AnimalManager.seek(this.gameObject, other.gameObject);
                        stateMachine.SetToReproduce();
                        Reproduce(other);
                    }
                }
                //in FP and WP high enough change to feed state
            }
            if (other.gameObject.CompareTag("stag"))
            {
                if (!isMale)
                {
                    if (canBreed())
                    {
                        stateMachine.SetToReproduce();
                        Reproduce(other);
                        Debug.Log("doe and stag found eachother");
                    }
                }
                //in FP and WP high enough change to feed state
            }

            if (other.gameObject.CompareTag("water"))
            {
                if (isThirsty())
                {
                    stateMachine.setToDrink();
                    Drink(other);
                }
            }

            if (other.gameObject.CompareTag("plant"))
            {
                if (isHungry())
                {
                    stateMachine.SetToFeed();
                    Eat(other);
                }
            }
        }

        public void Reproduce(Collider other)
        {
            Debug.Log("reproduce called");
        }

        private void moveRandomly()
        {
            Vector3 step = new Vector3(UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(0, 0), UnityEngine.Random.Range(-10, 10));
            transform.position =  transform.position + step * Speed;
            //agent.SetDestination(transform.position + step);

        }

        private void Eat(Collider other)
        {
            transform.position = other.transform.position;
            Destroy(other.gameObject);
            hunger = 0;
        }

        private void FleeFrom(Collider other)
        {
            Debug.Log("flee called");
        }

        private void Drink(Collider other)
        {
            transform.position = other.transform.position;
            Destroy(other.gameObject);
            thirst = 0;
        }

        public bool isHungry()
        {
            return hunger > hungerThreshold;
        }

        public bool isThirsty()
        {
            return thirst > thirstThreshold;
        }

        public bool canBreed()
        {
            bool a = reproductivrRefactoryPeriod < reproductivrRefactoryPeriodThreshold;
            bool b = !isThirsty();
            bool c = !isHungry();
            return a & b & c;
        }

        private void seek(Collider other)
        {
            //agent.SetDestination(other.transform.position);
            //agent.Move(other.transform.position);
        }

        private void evade(Collider other)
        {
        }

        private void HandleState()
        {
            if (shouldFlee)
            {
                stateMachine.SetToFlee();
                return;
            }

            if (thirst < thirstThreshold)
            {
                stateMachine.setToDrink();
                return;
            }


            if (hunger < hungerThreshold)
            {
                stateMachine.SetToFeed();
                return;
            }

            if (reproductivrRefactoryPeriod < reproductivrRefactoryPeriodThreshold)
            {
                stateMachine.SetToReproduce();
                return;
            }
            ActOnState();
        }

        public void ActOnState()
        {
            switch (stateMachine.getState())
            {
             case States.Flee:
                 break;
             case States.Idle:
                 break;
             case States.Feed:
                 break;
             case States.Drink:
                 break;
             case States.Reproduce:
                 break;
            }
        }
        

        public void die(AnimalManager.CausesOfDeath causeOfDeath)
        {
            AnimalManager.registerDeath(this.gameObject, causeOfDeath);
        }

        private void ExhaustFoodWater()
        {
            hunger += Time.deltaTime * 1 / FP;
            thirst += Time.deltaTime * 1 / WP;
        }
        

        private void CheckState()
        {
            if ( hunger > 1)
            {
                die(AnimalManager.CausesOfDeath.HUNGER);
            }
            if ( thirst > 1)
            {  
                die(AnimalManager.CausesOfDeath.THIRST);
            }
        }

        protected virtual void Update()
        {
            moveRandomly();
            ExhaustFoodWater();
            CheckState();
            HandleState();
        }
        
    }
}