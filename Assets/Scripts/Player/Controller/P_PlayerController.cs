using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class P_PlayerController : MonoBehaviour
{
    private Vector3 startTouchPosition, endTouchPosition;
    private bool coroutineAllowed = true;

    [Header("Lane Settings")]
    [SerializeField] private float laneOffset = 3f;  // Distance between lanes (e.g. -3, 0, +3)
    [SerializeField] private int currentLaneIndex = 1; // 0 = left, 1 = center, 2 = right
    private readonly int maxLaneIndex = 2; // Lane indices: 0, 1, 2

    #region UNITY METHODS

    private void Update()
    {
        HandleInput();
    }

    #endregion

    #region INPUT MANAGEMENT

    private void HandleInput()
    {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
        HandleMouseInput();
#else
        HandleTouchInput();
#endif
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0) && coroutineAllowed)
        {
            endTouchPosition = Input.mousePosition;
            ProcessSwipe();
        }
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
                startTouchPosition = touch.position;

            if (touch.phase == TouchPhase.Ended && coroutineAllowed)
            {
                endTouchPosition = touch.position;
                ProcessSwipe();
            }
        }
    }

    private void ProcessSwipe()
    {
        Vector2 swipeDelta = endTouchPosition - startTouchPosition;

        if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
        {
            if (swipeDelta.x > 0 && currentLaneIndex < maxLaneIndex)
            {
                currentLaneIndex++;
                MoveToLane(currentLaneIndex);
            }
            else if (swipeDelta.x < 0 && currentLaneIndex > 0)
            {
                currentLaneIndex--;
                MoveToLane(currentLaneIndex);
            }
        }
    }

    #endregion

    #region MOVEMENT MANAGEMENT

    private void MoveToLane(int laneIndex)
    {
        float targetX = (laneIndex - 1) * laneOffset; // Converts 0,1,2 => -3,0,3
        Vector3 newPosition = new Vector3(targetX, transform.position.y, transform.position.z);
        StartCoroutine(SmoothTransition(newPosition));
    }

    private IEnumerator SmoothTransition(Vector3 targetPosition)
    {
        coroutineAllowed = false;

        float elapsedTime = 0f;
        float duration = 0.1f; // Quick but smooth snap

        Vector3 startingPos = transform.position;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startingPos, targetPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        coroutineAllowed = true;
    }

    #endregion

    #region PLAYER HEALTH MANAGEMENT

    public void KillPlayer()
    {
        Destroy(gameObject);
        UI_UIManager.Instance.ShowEndScreen();
    }

    #endregion
}
