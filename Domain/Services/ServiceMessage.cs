using Domain.Interfaces;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;

namespace Domain.Services
{
    public class ServiceMessage : IServiceMessage
    {
        private readonly IMessage _message;

        public ServiceMessage(IMessage message)
        {
            _message = message;
        }

        public async Task Adicionar(Message Objeto)
        {
            var vaidaTitulo = Objeto.ValidaPropriedadeString(Objeto.Titulo, "Titulo");

            if(vaidaTitulo)
            {
                Objeto.DataCadastro = DateTime.Now;
                Objeto.DataAlteracao = DateTime.Now;
                Objeto.Ativo = true;
                await _message.Add(Objeto); 
            }
        }

        public async Task ATualizar(Message Objeto)
        {
            var vaidaTitulo = Objeto.ValidaPropriedadeString(Objeto.Titulo, "Titulo");

            //Buscar data de cadastro do banco
            var data = await _message.GetEntityById(Objeto.Id);

            if (vaidaTitulo)
            {
                Objeto.DataCadastro = data.DataCadastro;
                Objeto.DataAlteracao = DateTime.Now;
                await _message.Update(Objeto);
            }
        }

        public async Task<List<Message>> ListarMensagenAtiva()
        {
            return await _message.ListarMessage(n => n.Ativo);
        }

        public async Task<List<Message>> ListarMensagenInativa()
        {
            return await _message.ListarMessage(n => n.Ativo == false);
        }
    }
}
