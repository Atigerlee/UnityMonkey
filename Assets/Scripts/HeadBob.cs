using UnityEngine;
using UnityEngine.InputSystem;

public class HeadBob : MonoBehaviour
{
    [Header("相機與晃動設定")]
    public Transform cameraTransform;
    public float walkBobSpeed = 14f;
    public float walkBobAmount = 0.05f;
    public float runBobSpeed = 18f;
    public float runBobAmount = 0.08f;

    [Header("移動 Input System")]
    public InputActionReference moveAction;  // Vector2

    private float timer = 0f;
    private float defaultYPos;
    private Vector3 initialCamLocalPos;

    void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        initialCamLocalPos = cameraTransform.localPosition;
        defaultYPos = initialCamLocalPos.y;

        moveAction.action.Enable();
    }

    void Update()
    {
        Vector2 moveInput = moveAction.action.ReadValue<Vector2>();
        bool isMoving = moveInput.magnitude > 0.1f;

        // 用 Input.GetKey 偵測是否在跑步（舊 Input Manager 寫法）
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        float bobSpeed = isRunning ? runBobSpeed : walkBobSpeed;
        float bobAmount = isRunning ? runBobAmount : walkBobAmount;

        if (isMoving)
        {
            timer += Time.deltaTime * bobSpeed;
            float bobY = Mathf.Sin(timer) * bobAmount;
            float bobX = Mathf.Cos(timer * 0.5f) * bobAmount * 0.5f;

            cameraTransform.localPosition = new Vector3(
                initialCamLocalPos.x + bobX,
                defaultYPos + bobY,
                initialCamLocalPos.z
            );
        }
        else
        {
            timer = 0f;
            Vector3 targetPos = new Vector3(initialCamLocalPos.x, defaultYPos, initialCamLocalPos.z);
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, targetPos, Time.deltaTime * 5f);
        }
    }
}
