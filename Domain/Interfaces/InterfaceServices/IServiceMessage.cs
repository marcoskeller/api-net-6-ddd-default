using Entities.Entities;

namespace Domain.Interfaces.InterfaceServices
{
    public interface IServiceMessage
    {
        Task Adicionar(Message Objeto);
        
        Task ATualizar(Message Objeto);
        
        Task <List<Message>> ListarMensagenAtiva();
        
        Task <List<Message>> ListarMensagenInativa();

    }
}
