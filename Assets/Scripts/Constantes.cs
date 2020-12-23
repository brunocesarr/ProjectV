using System.Collections.Generic;
public class Constantes
{
    /// Destinos
    public static List<string> TiposDestinos
    {
        get => new List<string>{
            "Laboratório",
            "Sala do Professor",
            "Grupo de Pesquisa",
            "Outro Lugar"
        };
    }
    public static List<string> Professores
    {
        get => new List<string>{
            "Alessandro Vivas de Andrade",
            "André Covre",
            "Áthila Rocha Trindade",
            "Caroline Queiroz Santos",
            "Cinthya Rocha Tameirão",
            "Claudia Beatriz Berti",
            "Eduardo Pelli",
            "Erinaldo Barbosa da Silva",
            "George Henrique M. Rodolfo",
            "Geruza de Fátima Tomé Sabino",
            "Leonardo Lana de Carvalho",
            "Luciana Pereira de Assis",
            "Marcelo Ferreira Rego",
            "Maria Lúcia Bento Villela",
            "Rafael Santin",
        };
    }
    public static List<string> Laboratorios
    {
        get => new List<string>{
            "Laboratório 31",
            "Laboratório 32",
            "Laboratório 33",
            "Laboratório 34",
            "Laboratório 35",
            "Laboratório 36",
        };
    }
    public static List<string> GruposDePesquisa
    {
        get => new List<string>{
            "GesTI",
            "LabLIC",
            "LaPIES",
            "OIA",
            "MTPLNAM",
        };
    }
    public static List<string> OutrosDestinos
    {
        get => new List<string>{
            "Auditório",
            "Next Step",
            "Sala 37",
            "Secretaria",
            "Técnicos",
            "The Bug",
        };
    }

    /// Inícios - Ponto
    public static Dictionary<string, string> DefaultPosicao
    {
        get => new Dictionary<string, string>{
            {"Andar 1 - Ponto 1", "StartPoints/A1/1"},
            {"Andar 2 - Ponto 1", "StartPoints/A2/1"},
        };
    }

    /// Destinos - Ponto
    public static Dictionary<string, string> ProfessoresPosicao
    {
        get => new Dictionary<string, string>{
            {"Alessandro Vivas de Andrade", "DestinyPoints/A2/Corredor-3/14"},
            {"André Covre", "DestinyPoints/A2/Corredor-3/15"},
            {"Áthila Rocha Trindade", "DestinyPoints/A2/Corredor-3/9"},
            {"Caroline Queiroz Santos", "DestinyPoints/A2/Corredor-3/10"},
            {"Cinthya Rocha Tameirão", "DestinyPoints/A2/Corredor-3/11"},
            {"Claudia Beatriz Berti", "DestinyPoints/A2/Corredor-3/8"},
            {"Eduardo Pelli", "DestinyPoints/A2/Corredor-3/6"},
            {"Erinaldo Barbosa da Silva", "DestinyPoints/A2/Corredor-3/6"},
            {"George Henrique M. Rodolfo", "DestinyPoints/A2/Corredor-3/1"},
            {"Geruza de Fátima Tomé Sabino", "DestinyPoints/A2/Corredor-3/4"},
            {"Leonardo Lana de Carvalho", "DestinyPoints/A2/Corredor-3/13"},
            {"Luciana Pereira de Assis", "DestinyPoints/A2/Corredor-3/12"},
            {"Marcelo Ferreira Rego", "DestinyPoints/A2/Corredor-3/7"},
            {"Maria Lúcia Bento Villela", "DestinyPoints/A2/Corredor-3/2"},
            {"Rafael Santin", "DestinyPoints/A2/Corredor-3/5"},

            {"Ana Carolina Rodrigues", ""},
            {"Jésyka Milleny A. Gonçalves", ""},
        };
    }
    public static Dictionary<string, string> LaboratoriosPosicao
    {
        get => new Dictionary<string, string>{
            {"Laboratório 31", "DestinyPoints/A1/Corredor-1/1"},
            {"Laboratório 32", "DestinyPoints/A1/Corredor-1/2"},
            {"Laboratório 33", "DestinyPoints/A1/Corredor-3/1"},
            {"Laboratório 34", "DestinyPoints/A1/Corredor-3/2"},
            {"Laboratório 35", "DestinyPoints/A1/Corredor-3/3"},
            {"Laboratório 36", "DestinyPoints/A1/Corredor-3/4"},
        };
    }
    public static Dictionary<string, string> GruposPesquisaPosicao
    {
        get => new Dictionary<string, string>{
            {"GesTI", "DestinyPoints/A2/Corredor-1/5"},
            {"LabLIC", "DestinyPoints/A1/Corredor-2/3"},
            {"LaPIES", "DestinyPoints/A2/Corredor-1/3"},
            {"OIA", "DestinyPoints/A2/Corredor-1/1"},
            {"MTPLNAM", "DestinyPoints/A2/Corredor-1/2"},
        };
    }
    public static Dictionary<string, string> OutrosDestinosPosicao
    {
        get => new Dictionary<string, string>{
            {"Auditório", "DestinyPoints/A1/Corredor-2/1"},
            {"Next Step", "DestinyPoints/A2/Corredor-2/3"},
            {"Sala 37", "DestinyPoints/A1/Corredor-2/4"},
            {"Secretaria", "DestinyPoints/A2/Corredor-2/1"},
            {"Técnicos", "DestinyPoints/A1/Corredor-2/2"},
            {"The Bug", "DestinyPoints/A2/Corredor-2/7"},
        };
    }
}