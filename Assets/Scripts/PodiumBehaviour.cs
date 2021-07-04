using UnityEngine;

namespace VRLearning
{
	public class PodiumBehaviour : MonoBehaviour
	{
		[SerializeField] private float _rotationSpeed;
		private bool _isRotate = false;

		private void Update()
		{
			if (_isRotate)
			{
				Rotate();
			}
		}

		public void StartRotate()
		{
			_isRotate = true;
		}

		public void StopRotate()
		{
			_isRotate = false;
		}

		private void Rotate()
		{
			transform.rotation *= Quaternion.Euler(0, _rotationSpeed * Time.deltaTime, 0);
		}
	}
}