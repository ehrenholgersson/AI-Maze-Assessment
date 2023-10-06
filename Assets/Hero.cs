using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    NavMeshAgent _navAgent;
    public Zone CurrentZone;
    int _health = 100;
    int _stamina = 100;
    int _fear = 0;
    public static List<GameObject> Enemies = new List<GameObject>();
    Animator _animator;
    Vector3 _lastposition;
    bool _enemysighted;
    bool _dead;

    [SerializeField] Image _healthBar;
    [SerializeField] Image _staminaBar;
    [SerializeField] Image _fearBar;
    // Start is called before the first frame update
    void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        StartCoroutine("HeroAi");
        _animator = GetComponentInChildren<Animator>();
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;
        if (_health < 0)
            _health = 0;
    }

    bool CheckLOS(GameObject target)
    {
        int layerMask = ~((1 << 3) + (1 << 8));
        RaycastHit hit;
        Vector3 directionToTarget = (target.transform.position - transform.position).normalized;
        float distanceToTarget = (target.transform.position - transform.position).magnitude;

        if (Physics.Raycast(transform.position, directionToTarget, out hit, distanceToTarget, layerMask))
            return false;
        else 
            return true;
    }

    IEnumerator HeroAi()
    {
        
        while (true)
        {
            _enemysighted = false;
            foreach (GameObject g in Enemies)
            {
                if (Mathf.Abs((g.transform.position - transform.position).magnitude) < 50 && CheckLOS(g))
                {
                    _enemysighted = true;
                }
            }

            if (_enemysighted && _fear < 100) 
            {
                _fear += 10;
            }
            else if (_fear > 0)
            {
                _fear -= 5;
            }

            if ((_enemysighted|| _fear > 50) && _stamina > 0)
            {
                _navAgent.speed = 4.5f;
                _stamina -= 5;
            }
            else
            {
                _navAgent.speed = 3;
                if (_stamina < 100)
                    _stamina += 5;
            }

            Debug.Log("Hero Picking Destination");
            if (CurrentZone != null && _navAgent != null)
            {
                if (CurrentZone.RequiredCollectables.Count > 0)
                {
                    _navAgent.destination = CurrentZone.RequiredCollectables[0].transform.position;
                    Debug.Log("set goal as required item");
                }
                else if (CurrentZone.BonusCollectables.Count > 0)
                {
                    if (_fear > 50)
                    {
                        CurrentZone.BonusCollectables.Remove(CurrentZone.BonusCollectables[0]);
                        _navAgent.destination = CurrentZone.Goal.transform.position;
                    }
                    else
                    {
                        _navAgent.destination = CurrentZone.BonusCollectables[0].transform.position;
                    }
                    Debug.Log("set goal as bonus item");
                }
                else
                {
                    _navAgent.destination = CurrentZone.Goal.transform.position;
                    Debug.Log("set goal as detination");
                }

            }
            yield return new WaitForSeconds(3);
        }
    }

    //// Update is called once per frame
    void FixedUpdate()
    {
        _animator.SetFloat("Speed", Mathf.Abs((new Vector2(transform.position.x, transform.position.z) - new Vector2 (_lastposition.x,_lastposition.z)).magnitude)*100);
        _lastposition = transform.position;

        _healthBar.fillAmount = _health / 100f;
        _staminaBar.fillAmount = _stamina / 100f;
        _fearBar.fillAmount= _fear / 100f;

        if (_health <= 0 && !_dead)
        {
            _dead = true;
            _animator.Play("dead");
            _navAgent.isStopped = true;
            UIText.DisplayText("Solo Man is Dead!");
            Enemies.Clear();
            GameControl.RestartLevel();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
