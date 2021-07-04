using UnityEngine;

namespace VRLearning
{
	public class CameraPointer : MonoBehaviour
	{
		private Transform _player;
		[SerializeField] private TeleportTarget _currentTeleport;
		[SerializeField] private PlayerRotation _playerRotation;
		[SerializeField] private float _distance = 10f;
		[SerializeField] private float _minObjectDistance = 5f;
		private TeleportTarget _teleportTarget;

		private void Awake()
		{
			_player = GetComponent<Transform>();
		}

		private void Start()
		{
			UseTeleport(_currentTeleport);
		}

		private void Update()
		{
			RayObject();
		}

		public void Move(TeleportTarget teleport)
		{
			_currentTeleport.UnUse();
			_currentTeleport = teleport;
			UseTeleport(_currentTeleport);
		}

		private void UseTeleport(TeleportTarget teleport)
		{
			teleport.Use();
			SetupPlayerTeleportPosition(teleport);
		}

		private void SetupPlayerTeleportPosition(TeleportTarget teleport)
		{
			_player.position = teleport.transform.position;
			Camera.main.transform.rotation = Quaternion.Euler(0, 90, 0);
			_playerRotation.SetRotation();
		}

		private void RayObject()
		{
			Vector3 origin = Camera.main.transform.position;
			Vector3 direction = Camera.main.transform.forward;
			RaycastHit raycastHit;
			if (Physics.Raycast(origin, direction, out raycastHit, _distance))
			{
				if (raycastHit.collider.CompareTag("Teleport"))
				{
					TeleportTarget teleportTarget = raycastHit.collider.GetComponent<TeleportTarget>();
					SelectTeleportTarget(teleportTarget);
				}
				else
				{
					UnselectTeleportTarget();
					if (raycastHit.collider.CompareTag("DisappearObject"))
					{
						Transform other = raycastHit.transform;
						DestroyOnDistance(other);

					}

				}
			}
		}

		private void DestroyOnDistance(Transform other)
		{
			if (CheckDistance(other))
			{
				other.gameObject.SetActive(false);
			}
		}

		private void SelectTeleportTarget(TeleportTarget teleportTarget)
		{
			if (_teleportTarget == null)
			{
				_teleportTarget = teleportTarget;
				_teleportTarget.OnPointerEnter();
			}
		}

		private void UnselectTeleportTarget()
		{
			if (_teleportTarget != null)
			{
				_teleportTarget.OnPointerExit();
				_teleportTarget = null;
			}
		}

		private bool CheckDistance(Transform other)
		{
			if (Vector3.Distance(transform.position, other.transform.position) < _minObjectDistance)
			{
				return true;
			}
			return false;
		}
	}
}