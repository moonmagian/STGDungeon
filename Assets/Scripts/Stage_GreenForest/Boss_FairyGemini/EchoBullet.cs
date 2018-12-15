using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts.Stage_GreenForest.Boss_FairyGemini
{
    public class EchoBullet : MonoBehaviour
    {
        private Rigidbody rb;
        public int maxReflectNumber = 5;
        private int reflectNum = 0;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "playAreaBorder" && reflectNum <= maxReflectNumber)
            {
                switch (other.name)
                {
                    case "BorderDown":
                    case "BorderUp":
                        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -rb.velocity.z);
                        break;
                    case "BorderLeft":
                    case "BorderRight":
                        rb.velocity = new Vector3(-rb.velocity.x, rb.velocity.y, rb.velocity.z);
                        break;
                }

                ++reflectNum;
            }
            if (other.tag == "BulletReflecter")
            {
                rb.velocity = -rb.velocity;
            }
        }
    }
}