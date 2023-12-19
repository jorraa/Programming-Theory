using UnityEngine;

namespace Prizes {
    public class HardPrize : Prize { // INHERITANCE
        public override void Move(Transform transform) { // POLYMORPHISM
            MoveXYZ(transform);
        }
    }
}