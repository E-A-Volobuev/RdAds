using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RnAds.Core.Interfaces;
using RnAds.InterFaces.Interfaces;
using RnAds.Model.Dto;
using System;
using System.Threading.Tasks;

namespace RnAds.WEB.Controllers
{
    public class MessageController : Controller
    {
        private readonly IDialogService _iDialogService;
        private readonly IAuthService _iAuthService;
        private readonly IHubContext<ChatHub> _hubContext;
        public MessageController(IDialogService iDialogService, IAuthService iAuthService, IHubContext<ChatHub> hubContext)
        {
            _iDialogService = iDialogService;
            _iAuthService = iAuthService;
            _hubContext = hubContext;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> WriteMessage([FromBody] DtoSenderMessage message)
        {
            try
            {
                if (message != null)
                {
                    var login = User.Identity.Name;
                    var userId = await _iAuthService.GetCurrentUserIdAsync(login);
                    var sendMessage= await _iDialogService.CreateOrUpdateDialogAsync(message, userId,null);
                    return Ok();

                }
                return BadRequest("форма регистрации пустая");



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// <summary>
        /// получение диалогов текущего пользователя
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetAllDialogs()
        {
            try
            {
                var login = User.Identity.Name;
                var currentUserId = await _iAuthService.GetCurrentUserIdAsync(login);
                var listDialogs = await _iDialogService.GetAllDialogAsync(currentUserId);
                return Ok(listDialogs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SendChatMessage([FromForm] string toUserName, [FromForm] string text, [FromForm] IFormFile[] photo)
        {
            try
            {
                var login = User.Identity.Name;
                var currentUserId = await _iAuthService.GetCurrentUserIdAsync(login);
                //////id пользователя,которому отправляем сообщение
                var toUserId = await _iAuthService.GetCurrentUserIdAsync(toUserName);

                ////сохраняем сообщение в бд
                var message=await _iDialogService.SaveMessageHelperAsync(text, toUserId, currentUserId,photo);

                /////создаём и отправляем получателю дто сообщения
                var incomingMessage = await _iDialogService.SendMessageHelperTwoAsync(message, toUserId, currentUserId);

                if (login != toUserName) // если получатель и текущий пользователь не совпадают
                    await _hubContext.Clients.User(login).SendAsync("Receive", incomingMessage);
                await _hubContext.Clients.User(toUserName).SendAsync("Receive", incomingMessage);

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
