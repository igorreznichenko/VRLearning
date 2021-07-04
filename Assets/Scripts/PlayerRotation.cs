using System;
using UnityEngine;

namespace VRLearning
{
	public class PlayerRotation : MonoBehaviour
	{
		private Quaternion _startRotation;
		private Quaternion _xRotation;
		private Quaternion _yRotation;
		[SerializeField] private float _angle = 10f;
		void Start()
		{
			SetRotation();
		}

		void Update()
		{
			RotatePlayer();
		}

		public void SetRotation()
		{
			_startRotation = transform.rotation;
			_xRotation = Quaternion.identity;
			_yRotation = Quaternion.identity;
		}

		private void RotatePlayer()
		{
			float horizontal = Input.GetAxis("Horizontal");
			float vertical = Input.GetAxis("Vertical");
			if (Math.Abs(horizontal) > 0)
			{
				_yRotation *= Quaternion.Euler(Vector3.up * _angle * horizontal * Time.deltaTime);
			}
			if (Math.Abs(vertical) > 0)
			{
				_xRotation *= Quaternion.Euler(Vector3.left * _angle * vertical * Time.deltaTime);
			}
			transform.rotation = _startRotation * _yRotation * _xRotation;
		}
	}
}