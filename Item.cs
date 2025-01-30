public class Item
{
    public Items itemType;
    private float usageTime; //How long the powerUp can be used
    private float timeLeft = 0;
    private int useCount; //How many times the powerUp may be used
    private bool isCountOnly; //Should item not count time?
    public bool active = false; //Should time be counted for the item?

    //Constructs an Item object
    public Item(Items itemType, float usageTime, int useCount, bool isCountOnly) {
        this.itemType = itemType;
        this.usageTime = usageTime;
        this.useCount = useCount;
        this.isCountOnly = isCountOnly;
        if (!isCountOnly) timeLeft = usageTime;
    }

    public float getTimeLeft()
    {
        return timeLeft;
    }

    public void tickTime(float time) {
        if (timeLeft > 0) //Make sure time can be ticked
        {
            timeLeft -= time;
        }
        else {
            active = false;
        }
    }

    public void addCount() {
        useCount++;
    }

    public void consumeCount() {
        useCount--;
        if(!isCountOnly)timeLeft = usageTime; //Only recharge time if time is a thing
    }

    public int getUseCount() {
        return useCount;
    }
}