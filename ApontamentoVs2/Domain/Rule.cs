public class Rule
{
    public string Name { get; set; }
    public string Type { get; set; }
    public string Field { get; set; }
    public string Operator { get; set; }
    public string CompareTo { get; set; }
    public int Duration { get; set; }
    public double MaxHours { get; set; }
    public string Start { get; set; }
    public string End { get; set; }
    public string ErrorMessage { get; set; }
}
