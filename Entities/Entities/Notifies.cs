using System.ComponentModel.DataAnnotations.Schema;


namespace Entities.Entities
{
    //Objeto que retorna todas as mensagens de validaçoes e mensagens de todo o projeto

    public class Notifies
    {
        public Notifies()
        {
            Notifications = new List<Notifies>();
        }

        //Propiedade que apresentou erro
        [NotMapped]
        public string NomePropriedade { get; set; }


        //Mensagem de erro que ocorreu
        [NotMapped]
        public string mensagem { get; set; }


        //Lista de erros que podem ocorrer na aplicação
        [NotMapped]
        public List<Notifies> Notifications { get; set; }


        /*Criando Métodos de Validações*/

        //1º - Validando Propriedade strings
        public bool ValidaPropriedadeString(string valor, string nomePropriedade)
        {
            if(string.IsNullOrWhiteSpace(valor) || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Notifications.Add(new Notifies
                {
                    //Mensagem exibida para o usuário
                    mensagem = "Campo Obrigatório",

                    //Propriedade que apresentou o erro
                    NomePropriedade = nomePropriedade
                });
                return false;
            }
            return true;
        }

        //1º - Validando Propriedade int
        public bool ValidaPropriedadeInt(int valor, string nomePropriedade)
        {
            if(valor < 1 || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Notifications.Add(new Notifies
                {
                    mensagem = "Campo Obrigatório",
                    NomePropriedade = nomePropriedade

                });
                return false;
            }
            return true;
        }

    }
}
