using System.Collections.Generic;

public class Dialogue
{
    public string Titulo { get; set; }
    public Queue<string> Sentences { get; set; }

    public Dialogue()
    {
        Sentences = new Queue<string>();
    }
}
