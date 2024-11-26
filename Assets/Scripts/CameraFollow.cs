using UnityEngine;

namespace Game.CameraSystem
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target; // Nhân vật mà camera sẽ theo dõi
        public float smoothSpeed = 0.125f; // Tốc độ mượt khi camera di chuyển
        private float offsetY; // Khoảng cách giữa camera và nhân vật ban đầu

        void Start()
        {
            // Tính toán khoảng cách ban đầu theo trục Y
            offsetY = transform.position.y - target.position.y;
        }

       
        void LateUpdate()
        {
            if (target.position.y > transform.position.y - offsetY)
            {
                Vector3 targetPosition = new Vector3(transform.position.x, target.position.y + offsetY, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
            }

            // Kiểm tra nếu nhân vật rơi quá xa
            if (target.position.y < transform.position.y - 8f)
            {
                Debug.Log("Game Over!");
                // Gọi hàm Game Over hoặc reset game
            }
        }

    }
}
