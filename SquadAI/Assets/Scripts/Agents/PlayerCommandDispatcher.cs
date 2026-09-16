using UnityEngine;

public class PlayerCommandDispatcher : MonoBehaviour
{
    [SerializeField]
    GameObject TargetCursorPrefab = null;
    [SerializeField]
    GameObject NPCTargetCursorPrefab = null;

    GameObject TargetCursor = null;
    GameObject NPCTargetCursor = null;

    [HideInInspector] public PlayerAgent Player;

    [HideInInspector] public Vector3 shotTarget;
    [HideInInspector] public bool IsShootOrderActive = false;
    [SerializeField] float shotOrderDuration = 0.1f;
    float shotStartTime;

    [HideInInspector] public Vector3 coverShotTarget;
    [HideInInspector] public bool IsCoverShootOrderActive = false;
    [SerializeField] float coverShotDuration = 3f;
    float coverShotStartTime;

    private GameObject GetTargetCursor()
    {
        if (TargetCursor == null)
            TargetCursor = Instantiate(TargetCursorPrefab);
        return TargetCursor;
    }

    private GameObject GetNPCTargetCursor()
    {
        if (NPCTargetCursor == null)
        {
            NPCTargetCursor = Instantiate(NPCTargetCursorPrefab);
        }
        return NPCTargetCursor;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Player = GetComponent<PlayerAgent>();
        Player?.OnAim.AddListener(Aim);
        Player?.OnShoot.AddListener(SetShotTarget);
        Player?.OnCoverShoot.AddListener(SetCoverShotTarget);
        Player?.OnCancel.AddListener(CancelCommands);
    }

    private void Update()
    {
        if (IsShootOrderActive && Time.time > shotStartTime + shotOrderDuration)
        {
            IsShootOrderActive = false;
        }
        if (IsCoverShootOrderActive && Time.time > coverShotStartTime + coverShotDuration)
        {
            CancelCommands();
        }
    }

    void SetShotTarget(Vector3 target)
    {
        shotTarget = target;
        IsShootOrderActive = true;
        shotStartTime = Time.time;
    }

    void SetCoverShotTarget(Vector3 target)
    {
        if (!IsCoverShootOrderActive)
        {
            GetNPCTargetCursor().gameObject.SetActive(true);
            GetNPCTargetCursor().transform.position = target;
            coverShotStartTime = Time.time;
            coverShotTarget = target;
            IsCoverShootOrderActive = true;
        }
        else
        {
            CancelCommands();
        }
    }

    void CancelCommands()
    {
        if (IsCoverShootOrderActive)
        {
            IsCoverShootOrderActive = false;
            GetNPCTargetCursor().gameObject.SetActive(false);
        }
    }

    void Aim(Vector3 pos)
    {
        GetTargetCursor().transform.position = pos;
    }
}
