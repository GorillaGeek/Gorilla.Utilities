using System.Text.RegularExpressions;

namespace Gorilla.Utilities
{
    /// <summary>
    /// Classe para validação de padrões e de documentos
    /// </summary>
    public class Validador
    {
        /// <summary>
        /// Faz a validação do email
        /// </summary>
        public static bool IsValidEmail(string email)
        {
            var rx = new Regex(Constantes.REGEX_EMAIL, RegexOptions.IgnoreCase);
            return rx.Match(email).Success;
        }

        /// <summary>
        ///Retorna true se a string tiver apenas numeros
        /// </summary>
        /// <param name="cpfCnpj"></param>
        /// <returns></returns>
        public static bool IsNumber(string cpfCnpj)
        {
            decimal valor;
            return decimal.TryParse(cpfCnpj, out valor);
        }


        /// <summary>
        /// Retorna true se o CPF for válido
        /// </summary>
        public static bool IsValidCpf(string cpf)
        {
            cpf = cpf.OnlyNumbers();

            var multiplicador1 = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma, resto;
            var isNum = IsNumber(cpf);

            if (isNum != true)
                return false;

            if (cpf.Length != 11)
                return false;

            if (cpf == "00000000000")
                return false;

            var tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (var i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            }

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            var digito = resto.ToString();

            tempCpf = tempCpf + digito;

            soma = 0;
            for (var i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto;

            return cpf.EndsWith(digito);
        }


        /// <summary>
        /// Retorna true se o CNPJ for válido
        /// </summary>
        public static bool IsValidCnpj(string cnpj)
        {
            if (cnpj != null)
            {
                cnpj = cnpj.OnlyNumbers();
            }

            var multiplicador1 = new[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma, resto;
            var isNum = IsNumber(cnpj);

            cnpj = (cnpj ?? "").Trim();

            if (isNum != true)
                return false;

            if (cnpj.Length != 14)
                return false;

            var tempCnpj = cnpj.Substring(0, 12);

            soma = 0;
            for (var i = 0; i < 12; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            }

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            var digito = resto.ToString();

            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (var i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto;

            return cnpj.EndsWith(digito);
        }


        /// <summary>
        /// Verfica se o documento é um CPF ou CNPJ e valida de acordo com o tipo
        /// </summary>
        /// <param name="cpfCnpj">Documento</param>
        /// <returns></returns>
        public static bool IsValidCpfnpj(string cpfCnpj)
        {
            cpfCnpj = cpfCnpj.OnlyNumbers();

            if (!string.IsNullOrEmpty(cpfCnpj))
            {
                return cpfCnpj.Length != 14 ? IsValidCpf(cpfCnpj) : IsValidCnpj(cpfCnpj);
            }

            return false;
        }
    }
}