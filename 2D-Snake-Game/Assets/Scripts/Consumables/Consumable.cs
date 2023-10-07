using UnityEngine;

public abstract class Consumable : MonoBehaviour
{
        [SerializeField] private ConsumableManager manager;
        [SerializeField] private float timeOnScreen;
        private float timer;

        private void Start()
        {
            timer = 0;
        }
        public void SetManager(ConsumableManager manager)
        {
            this.manager = manager;
        }

        public void SetPosition()
        {
            transform.position = manager.RandomizePosition();
        }

        protected virtual void Update()
        {
            UpdateOnScreenTimer();
        }
        public abstract void Consume(SnakeController snake);
        public void UpdateOnScreenTimer()
        {
            timer += Time.deltaTime;
            if (timer > timeOnScreen)
            {
                ResetOnScreenTimer();
            }
        }
        protected virtual void ResetOnScreenTimer()
        {
            timer = 0;
        }
}