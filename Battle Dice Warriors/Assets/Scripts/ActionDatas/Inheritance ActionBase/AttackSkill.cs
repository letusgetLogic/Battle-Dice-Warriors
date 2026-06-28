public struct AttackSkill
{
    public Direction Direction;
    public int Range;   
    public int Percentage;
    public int HitEndurance;
    public int RoundEndurance;
    public string BuffAPText;

    public AttackSkill(Direction direction, int range, int percentage, int hitEndurance, int roundEndurance, string buffAPText)
    {
        Direction = direction;
        Range = range;
        Percentage = percentage;
        HitEndurance = hitEndurance;
        RoundEndurance = roundEndurance;
        BuffAPText = buffAPText;
    }
}
