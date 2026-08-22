using UnityEngine;
using UnityEngine.UI;

public class uiBar : MonoBehaviour
{
    public enum barState
    {
        drain,
        gain,
        station
    }
	[SerializeField] public barState state;
	[SerializeField] private RectTransform bar;
	[SerializeField] private RectTransform bgBar;

	
	[SerializeField] private float maxValue;
	private float value;
	[SerializeField] private float width;
	[SerializeField] private float height;
	private float widthratio;

    //public bool drain;
    [SerializeField] private float drainSpeed;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        widthratio = width / maxValue;
        value = maxValue;

	}

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case barState.drain:
				subVal(drainSpeed * Time.deltaTime);
				break;
            case barState.gain:
				addVal(drainSpeed * Time.deltaTime);
				break;
            case barState.station:
                break;
        }
    }

    public void SetMaxVal(float maxVal)
    {
        maxValue = maxVal;

        width = maxValue * widthratio;
		bgBar.sizeDelta = new Vector2(width, height);

        ResizeBar();
	}

	public void SetVal(float val)
	{
        value = val;
        if(value < 0)
        {
            value = 0;
        }
        if (value > maxValue)
        {
            value = maxValue;
        }
        ResizeBar();
	}

	public void addVal(float val)
	{
		value += val;
		if (value > maxValue)
		{
			value = maxValue;
		}
		ResizeBar();
	}

	public bool subVal(float val)
	{
		float temp = value -= val;
        if(temp >= 0)
        {
            SetVal(temp);
            return true;
		}
        return false;
	}

	private void  ResizeBar()
    {
		float newWidth = (value / maxValue) * width;

		bar.sizeDelta = new Vector2(newWidth, height);
	}
    
    public float GetValue()
    {
        return value;
    }
}
