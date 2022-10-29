using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPIs.Models;

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {

        private readonly IMapper _imapper;
        private readonly IMessage _imessage;
        private readonly IServiceMessage _serviceMessage;

        public MessageController(IMapper imapper, IMessage imessage, IServiceMessage serviceMessage)
        {
            _imapper = imapper;
            _imessage = imessage;
            _serviceMessage = serviceMessage;
        }


        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/Add")]
        public async Task<List<Notifies>> Add(MessageViewModel message)
        {
            message.UserId = await RetornarIdUsuarioLogado();
            var messageMap = _imapper.Map<Message>(message);

            //Utilizando sem o servico de validacao
            //await _imessage.Add(messageMap);

            //Utilizando o servico de validacao
            await _serviceMessage.Adicionar(messageMap);

            return messageMap.Notifications;
        }


        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/Update")]
        public async Task<List<Notifies>> Update(MessageViewModel message)
        {
            var messageMap = _imapper.Map<Message>(message);

            //Utilizando sem o servico de validacao
            //await _imessage.Update(messageMap);

            //Utilizando o servico de validacao
            await _serviceMessage.ATualizar(messageMap);

            return messageMap.Notifications;
        }


        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/Delete")]
        public async Task<List<Notifies>> Delete(MessageViewModel message)
        {
            var messageMap = _imapper.Map<Message>(message);
            await _imessage.Delete(messageMap);

            return messageMap.Notifications;
        }


        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/GetEntityById")]
        //public async Task<MessageViewModel> GetEntityById(Message message)
        //{
        //    message = await _imessage.GetEntityById(message.Id);
        //    var messageMap = _imapper.Map<MessageViewModel>(message);

        //    return messageMap;
        //}
        public async Task<MessageViewModel> GetEntityById(int id)
        {
            var message = await _imessage.GetEntityById(id);

            var messageMap = _imapper.Map<MessageViewModel>(message);

            return messageMap;
        }


        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/List")]
        public async Task<List<MessageViewModel>> List()
        {
            var mensagens = await _imessage.List();
            var messageMap = _imapper.Map<List<MessageViewModel>>(mensagens);

            return messageMap;
        }


        //Listando messagens com filtro
        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/ListarMessageAtivas")]
        public async Task<List<MessageViewModel>> ListarMessageAtivas()
        {
            var mensagens = await _serviceMessage.ListarMensagenAtiva();
            var messageMap = _imapper.Map<List<MessageViewModel>>(mensagens);

            return messageMap;
        }


        //Listando messagens com filtro
        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/ListarMessageInativas")]
        public async Task<List<MessageViewModel>> ListarMessageInativas()
        {
            var mensagens = await _serviceMessage.ListarMensagenInativa();
            var messageMap = _imapper.Map<List<MessageViewModel>>(mensagens);

            return messageMap;
        }


        //Pegar usuário logado na aplicação para possíves usos
        private async Task<string> RetornarIdUsuarioLogado()
        {
            if (User != null)
            {
                var idUsuario = User.FindFirst("idUsuario");
                return idUsuario.Value;
            }
            return string.Empty;
        }
    }
}
