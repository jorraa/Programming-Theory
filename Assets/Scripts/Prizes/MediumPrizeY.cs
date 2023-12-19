using UnityEngine;

namespace Prizes {
    public class MediumPrizeY : Prize{ // INHERITANCE
        public override void Move(Transform transform) { // POLYMORPHISM
            MoveYZ(transform);
        }
    }
}