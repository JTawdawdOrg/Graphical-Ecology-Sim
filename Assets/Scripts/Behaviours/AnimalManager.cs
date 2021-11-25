using UnityEngine;

namespace Behaviours
{
    public class AnimalManager : MonoBehaviour
    {
     
        public static void registerDeath(GameObject other , CausesOfDeath cause)
        {   
            Debug.Log("Animal died of:" + cause);
            Destroy(other);
        }
        
        public enum CausesOfDeath
        {
            HUNGER,
            THIRST,
            PREDATION,
        }

        public static void seek(GameObject from, GameObject to)
        {
            Prey prey1 = from.GetComponent<Prey>();
            Prey prey2 = to.GetComponent<Prey>();
            Predator pred1 = from.GetComponent<Predator>();
            Predator pred2 = to.GetComponent<Predator>();

            if (prey1 && prey2)
            {
                Debug.Log("prey met prey");
                Vector3 step = to.transform.position - from.transform.position;
                from.transform.position += from.gameObject.transform.position * prey1.Speed1;
            }

            if (pred1 && prey2)
            {
                Debug.Log("predator met prey");
                Vector3 step = to.transform.position - from.transform.position;
                from.transform.position += from.gameObject.transform.position * pred1.Speed1;
            }

        }
    }
}