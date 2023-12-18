using UnityEngine;
public class GameLevelController: MonoBehaviour {
    /*
     * Game levels 0, 1, 2, 3, 4
     * Barrier collision
     * 0 -> -1 point, 1 -> -1 point, 2 -> -2 points, 3 -> -2 points, 4 -> -3 points
     * Missed prize
     * 0 -> -1 point, 1 -> -2 points, 2 -> -2 points, 3 -> -3 points, 4 -> -3 points
     * Game over, lost game
     * 0 -> -12 points, 1 -> -11 points, 2 -> -10 points, 3 -> -9 points, 4 -> -8 points
     */
    public int gameLevel = 0;
    private int[] minusBarrierCollisions = { -1, -1, -2, -3, -4 };
    private int[] minusMissedPrizes = { -1, -2, -2, -3, -4 };
    private int[] minusLostGame = { -12, -11, -10, -9, -8 };

    public int GetBarrierCollisionPoints() {
        return minusBarrierCollisions[gameLevel];
    }

    public int GetMissedPrizePoints() {
        return minusMissedPrizes[gameLevel];
    }

    public int GetLostGamePoints() {
        return minusLostGame[gameLevel];
    }
    
}
