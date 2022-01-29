namespace OOP
{
    public abstract class Eletrodomestico
    {
        private readonly string _nome;
        private readonly int _voltagem;
        protected Eletrodomestico(string nome, int voltagem)
        {
            _nome = nome;
            _voltagem = voltagem;
        }

        public abstract void Ligar();
        public abstract void Desligar();
        

        // O método Testar() não é obrigado ser implementado, porque já existe uma implementação nesse caso abaixo, porém voce pode modificar esse método usando o "virtual".
        public virtual void Testar()
        {
            // teste do equipamento
        }
    }
}