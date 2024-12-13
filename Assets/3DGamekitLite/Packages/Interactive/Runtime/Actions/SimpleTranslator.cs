using UnityEngine;

namespace Gamekit3D.GameCommands
{
    public class SimpleTranslator : SimpleTransformer
    {
        #region Variables

        public Vector3 end = Vector3.forward;
        public new Rigidbody rigidbody;
        public Vector3 start = -Vector3.forward;

        #endregion

        #region Public methods

        public override void PerformTransform(float position)
        {
            float curvePosition = accelCurve.Evaluate(position);
            Vector3 pos = transform.TransformPoint(Vector3.Lerp(start, end, curvePosition));
            Vector3 deltaPosition = pos - rigidbody.position;
            if (Application.isEditor && !Application.isPlaying)
            {
                rigidbody.transform.position = pos;
            }

            rigidbody.MovePosition(pos);

            if (m_Platform != null)
            {
                m_Platform.MoveCharacterController(deltaPosition);
            }
        }

        #endregion
    }
}