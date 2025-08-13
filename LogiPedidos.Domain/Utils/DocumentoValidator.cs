namespace LogiPedidosBackend.LogiPedidos.Domain.Utils;

public static class DocumentoValidator
{
    public static bool IsValidCpfCnpj(string documento)
    {
        if (string.IsNullOrWhiteSpace(documento))
            return false;

        // Remove caracteres não numéricos
        documento = new string(documento.Where(char.IsDigit).ToArray());

        return documento.Length switch
        {
            11 => IsValidCpf(documento),
            14 => IsValidCnpj(documento),
            _ => false
        };
    }

    private static bool IsValidCpf(string cpf)
    {
        if (cpf.Length != 11 || cpf.All(c => c == cpf[0]))
            return false;

        for (int j = 9; j < 11; j++)
        {
            int soma = 0;
            for (int i = 0; i < j; i++)
                soma += (cpf[i] - '0') * (j + 1 - i);

            int resto = soma % 11;
            int digito = resto < 2 ? 0 : 11 - resto;

            if (cpf[j] - '0' != digito)
                return false;
        }

        return true;
    }

    private static bool IsValidCnpj(string cnpj)
    {
        if (cnpj.Length != 14 || cnpj.All(c => c == cnpj[0]))
            return false;

        int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCnpj = cnpj[..12];
        int soma = 0;

        for (int i = 0; i < 12; i++)
            soma += (tempCnpj[i] - '0') * multiplicador1[i];

        int resto = (soma % 11);
        resto = resto < 2 ? 0 : 11 - resto;

        string digito = resto.ToString();
        tempCnpj += digito;

        soma = 0;
        for (int i = 0; i < 13; i++)
            soma += (tempCnpj[i] - '0') * multiplicador2[i];

        resto = (soma % 11);
        resto = resto < 2 ? 0 : 11 - resto;

        digito += resto.ToString();

        return cnpj.EndsWith(digito);
    }
}
