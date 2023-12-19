using UnityEngine;

namespace Prizes {
    public class Prize {
        private float xSpeed = 2.5f;
        private float ySpeed = 2.5f;
        private float zSpeed = 0.7f;
        private bool xLeft = true;
        private bool yUp = true;
        /*
         * Default movement Z to minus direction
         */
        public virtual void Move(Transform transform) {
            MoveZ(transform);
        }

        protected void MoveZ(Transform transform) { 
            transform.Translate(Vector3.back * zSpeed * Time.deltaTime);
        }

        protected void MoveXZ(Transform transform) {
            MoveZ(transform);
            MoveX(transform);
        }

        protected void MoveYZ(Transform transform) {
            MoveZ(transform);
            MoveY(transform);
        }

        protected void MoveXYZ(Transform transform) {
            MoveZ(transform);
            MoveX(transform);
            MoveY(transform);
        }

        /*
         * X direction toggles between left and right
         */
        private void MoveX(Transform transform) {
            if (GameManager.Instance.rightLimitX < transform.position.x) {
                xLeft = true;
            } else if(GameManager.Instance.leftLimitX > transform.position.x) {
                xLeft = false;
            } else {
                xLeft = Random.Range(0,100)<10 ? Random.Range(0, 100) < 50 ? !xLeft : xLeft : xLeft;
            }
            Vector3 moveDirectionX = xLeft ? Vector3.left : Vector3.right;
            transform.Translate(moveDirectionX * xSpeed * Time.deltaTime);
        }
        /*
         * Y direction toggles between up and down
         */
        private void MoveY(Transform transform) {
            if (GameManager.Instance.upLimitY < transform.position.y) {
                yUp = false;
            } else if(GameManager.Instance.downLimitY > transform.position.y) {
                yUp = true;
            }
            else {
                yUp = Random.Range(0, 100) < 10 ? Random.Range(0, 100) < 20 ? !yUp : yUp : yUp;
            }

            Vector3 moveDirectionY = yUp ? Vector3.up : Vector3.down;
            //yUp = !yUp;
            transform.Translate(moveDirectionY * ySpeed * Time.deltaTime);
        }

    }
}