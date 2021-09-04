using UnityEngine;

namespace Robot
{
    public class RobotMotor : MonoBehaviour
    {

        [Header("Settings")] public float speed = 5f;
        
        
        public void MoveRight()
        {
            transform.position = new Vector3(transform.position.x + (speed * Time.deltaTime), 
                transform.position.y,
                transform.position.z);
        }
        
        public void MoveLeft()
        {
            transform.position = new Vector3(transform.position.x - (speed * Time.deltaTime), 
                transform.position.y,
                transform.position.z);
        }
    }
}
