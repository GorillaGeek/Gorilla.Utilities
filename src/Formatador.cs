using System;

namespace Gorilla.Utilities
{
    /// <summary>
    /// Utilitário para formatação de documentos
    /// </summary>
    public class Formatador
    {
        private const string FORMATO_CPF = @"000\.000\.000-00";
        private const string FORMATO_CNPJ = @"00\.000\.000/0000-00";

        public static string FormatarCNPJ(string cnpj)
        {
            return string.IsNullOrWhiteSpace(cnpj) ? "" : Convert.ToInt64(cnpj).ToString(FORMATO_CNPJ);
        }

        public static string FormatarCPF(string valor)
        {
            return string.IsNullOrWhiteSpace(valor) ? "" : Convert.ToInt64(valor).ToString(FORMATO_CPF);
        }

        public static string FormatarDocumento(string valor)
        {
            return valor.Length <= 12
                ? FormatarCPF(valor)
                : FormatarCNPJ(valor);
        }

        public static string FormatarTelefone(string telefone)
        {
            throw new NotImplementedException();
        }


        public static string FormatarTelefone(string telefone, string ddd)
        {
            return string.IsNullOrWhiteSpace(telefone) ? null : string.Format("({0}) {1}", ddd, telefone);
        }

        public static string FormatarTelefone(string telefone, string ddd, string ddi)
        {
            return string.IsNullOrWhiteSpace(telefone) ? null : string.Format("{0} ({1}) {2}", (string.IsNullOrWhiteSpace(ddi) ? "" : "+" + ddi), ddd, telefone);
        }
    }
}
