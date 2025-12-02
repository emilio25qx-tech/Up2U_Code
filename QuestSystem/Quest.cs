using UnityEngine;

public abstract class Quest : IQuest
{
    public string Objective { get; private set; }
    public string CurrentHint => _currentHint;

    protected string _currentHint;
    protected bool _isCompleted = false;

    public bool IsCompleted => _isCompleted;

    private float hintTimer;
    private int hintIndex = 0;
    private TimedHint[] hints;

    public Quest(string objective, params TimedHint[] hints)
    {
        this.Objective = objective;
        this.hints = hints;
    }

    public virtual void StartQuest()
    {
        hintIndex = 0;
        if (hints.Length > 0)
        {
            _currentHint = hints[0].text;
            hintTimer = hints[0].time;
        }
        else
        {
            _currentHint = "";
            hintTimer = 0f;
        }
    }

    public virtual void UpdateQuest()
    {
        if (_isCompleted) return;

        if (hints.Length > 1)
        {
            hintTimer -= Time.deltaTime;

            if (hintTimer <= 0f && hintIndex < hints.Length - 1)
            {
                hintIndex++;
                _currentHint = hints[hintIndex].text;
                hintTimer = hints[hintIndex].time;
            }
        }
    }

    protected void Complete()
    {
        _isCompleted = true;
    }
}
