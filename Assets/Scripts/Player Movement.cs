using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpHeight = 2f;
    public float verticalGravity = -19.62f; // Yerçekimi

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        // 1. CharacterController bileþenini alýyoruz
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
   
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Yerdeyken hýzý sýfýrlamaya yakýn tut (yüzeye yapýþmayý saðlar)
        }

        // 3. WASD Movement (Yatay Hareket)
        float x = Input.GetAxis("Horizontal"); // A ve D
        float z = Input.GetAxis("Vertical");   // W ve S

        // Oyuncunun baktýðý yöne göre X ve Z ekseninde hareket verktörü oluþturuluyor
        Vector3 move = transform.right * x + transform.forward * z;

        // Karakteri Walk (Yürüme) hýzýnda hareket ettir
        controller.Move(move * speed * Time.deltaTime);

        // 4. Jump Mechanic (Zýplama Mekaniði)
        // Space tuþuna basýldýðýnda ve karakter yerde olduðunda zýpla
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Fizik formülü: Kök(yükseklik * -2 * yerçekimi)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * verticalGravity);
        }

        // 5. Apply Gravity (Yerçekimi Uygulamasý)
        velocity.y += verticalGravity * Time.deltaTime;

        // Dikey hýzý (zýplama veya yerçekimi ile düþüþü) karaktere uygula
        controller.Move(velocity * Time.deltaTime);
    }
}

