using AutoMapper;
using Domain.Interfaces;
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

        public MessageController(IMapper imapper, IMessage imessage)
        {
            _imapper = imapper;
            _imessage = imessage;
        }


        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/Add")]
        public async Task<List<Notifies>> Add(Message message)
        {
            message.UserId = await RetornarIdUsuarioLogado();
            var messageMap = _imapper.Map<Message>(message);
            await _imessage.Add(messageMap);

            return messageMap.Notifications;
        }


        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/Update")]
        public async Task<List<Notifies>> Update(Message message)
        {
            var messageMap = _imapper.Map<Message>(message);
            await _imessage.Update(messageMap);

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
        public async Task<MessageViewModel> GetEntityById(Message message)
        {
            message = await _imessage.GetEntityById(message.Id);
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
