using UnityEngine;
using UnityEngine.UI;

namespace VRLearning
{
	public class TeleportTarget : MonoBehaviour
	{
		[SerializeField] private CameraPointer _player;
		[SerializeField] private float _teleportWaitingTime;
		[SerializeField] private Image _load;
		[SerializeField] private Image _cursor;
		[SerializeField] private PodiumBehaviour _podium;
		[SerializeField] private Animator _teleportationEfect;
		private Renderer _renderer;
		private bool _isSelected = false;
		private float _startTime;
		private float _deltaTime;
		private bool _isInUse = false;

		private void Awake()
		{
			_renderer = GetComponent<Renderer>();
		}
		public void OnPointerEnter()
		{
			if (!_isInUse)
			{
				SetTeleportProgress();
			}
		}

		public void UnUse()
		{
			if (_podium != null)
				_podium.StopRotate();
			_isInUse = false;
			_renderer.material.color = Color.red;
		}

		public void Use()
		{
			if (_podium != null)
				_podium.StartRotate();
			_isInUse = true;
			_renderer.material.color = Color.blue;
		}

		public void OnPointerExit()
		{
			if (!_isInUse)
			{
				UnSetTeleportProgress();
			}
		}

		private void SetTeleportProgress()
		{
			_cursor.gameObject.SetActive(false);
			_isSelected = true;
			_startTime = Time.time;
		}

		private void UnSetTeleportProgress()
		{
			_cursor.gameObject.SetActive(true);
			_isSelected = false;
			_load.fillAmount = 0;

		}

		private void Update()
		{
			if (_isSelected)
			{
				StartTeleportation();
			}
		}

		private void StartTeleportation()
		{
			_deltaTime = Time.time - _startTime;
			_load.fillAmount = Mathf.MoveTowards(_load.fillAmount, 1, 1 / _teleportWaitingTime * Time.deltaTime);
			if (_deltaTime > _teleportWaitingTime)
			{
				UnSetTeleportProgress();
				StartTeleportationEffect();
				_player.Move(this);
			}
		}

		private void StartTeleportationEffect()
		{
			GameObject effect = _teleportationEfect.gameObject;
			if (!effect.activeSelf)
			{
				effect.SetActive(true);
			}
			_teleportationEfect.Play("Fade Out", -1, 0);
		}

	}
}