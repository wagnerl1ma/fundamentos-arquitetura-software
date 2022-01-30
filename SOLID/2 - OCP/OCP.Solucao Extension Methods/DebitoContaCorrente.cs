namespace SOLID.OCP.Solucao_Extension_Methods
{
    // Para estender um método de outra classe, a nova classe criada precisar "static" e o método também.
    public static class DebitoContaCorrente 
    {
        public static string DebitarContaCorrente(this DebitoConta debitoConta) // estendendo o método FormatarTransacao() da classe DebitoConta
        {
            // Logica de negócio para debito em conta corrente.
            return debitoConta.FormatarTransacao();
        }
    }
}