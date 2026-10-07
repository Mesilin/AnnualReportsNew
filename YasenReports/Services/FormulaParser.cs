using System.Text.Json;
using YasenReports.Api.Models;

namespace YasenReports.Api.Services;

public static class FormConfigSerializer
{
    public static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() },
    };

    public static FormDefinition Deserialize(JsonElement config) =>
        config.Deserialize<FormDefinition>(Options) ?? throw new InvalidOperationException("Пустая конфигурация формы");

    public static JsonElement Serialize(FormDefinition def) =>
        JsonSerializer.SerializeToElement(def, Options);
}

/// <summary>
/// Разбор простых формул вида "col3+col4-col5", "2-1-avia.col10", "$rownum".
/// Поддерживаются только слагаемые (идентификатор колонки с опциональным знаком).
/// </summary>
public static class FormulaParser
{
    public record Term(string? FormCode, string ColumnCode, int Sign);

    public static List<Term> Parse(string expression)
    {
        var terms = new List<Term>();
        if (string.IsNullOrWhiteSpace(expression)) return terms;

        var s = expression.Replace(" ", "");
        int i = 0;
        while (i < s.Length)
        {
            int sign = 1;
            if (s[i] == '+') { i++; }
            else if (s[i] == '-') { sign = -1; i++; }

            int start = i;
            while (i < s.Length && s[i] != '+' && s[i] != '-') i++;
            var token = s[start..i];
            if (token.Length == 0) continue;

            string? formCode = null;
            var dot = token.IndexOf('.');
            if (dot > 0)
            {
                formCode = token[..dot];
                token = token[(dot + 1)..];
            }
            terms.Add(new Term(formCode, token, sign));
        }
        return terms;
    }
}
