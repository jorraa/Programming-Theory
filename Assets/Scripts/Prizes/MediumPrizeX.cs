using UnityEngine;

namespace Prizes {
    public class MediumPrizeX : Prize { // INHERITANCE
        public override void Move(Transform transform) { // POLYMORPHISM
            MoveXZ(transform);
        }
    }
}