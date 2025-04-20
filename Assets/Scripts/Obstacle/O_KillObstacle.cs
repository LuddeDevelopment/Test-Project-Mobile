using UnityEngine;

public class O_KillObstacle : O_Obstacle
{
    #region OVERRIDE OBSTACLE BEHAVIOR

    public override void onPlayerTouchObstacle(GameObject player)
    {
        if (player.TryGetComponent<P_PlayerController>(out var controller))
        {
            controller.KillPlayer();
        }
    }

    #endregion
}
