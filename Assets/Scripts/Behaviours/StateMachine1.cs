namespace Behaviours
{
    public class StateMachine1
    {
        private States state;

        public StateMachine1()
        {
            state = States.Idle;
        }
        public void setToDrink()
        {
            state = States.Drink;
        }
        public void SetToIdle()
        {
            state = States.Idle;
        }
        public void SetToFeed()
        {
            state = States.Feed;
        }
        public  void SetToReproduce()
        {
            state = States.Reproduce;
        }
        public  void SetToFlee()
        {
            state = States.Flee;
        }
        public States getState()
        {
            return state;
        }
    }
    public enum States   
    {
        Drink,
        Feed,
        Flee,
        Hunt,
        Reproduce,
        Idle,
    }
}