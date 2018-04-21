using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KiraNet.Camellia.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.ApiService.Models.ViewModels;
using Server.ApiService.Repositories.Abstracts;
using Server.ApiService.Services;

namespace Server.ApiService.Controllers
{
    [Authorize/*(AuthenticationSchemes = "Bearer")*/]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase<UserController, IUserInfoRepository>
    {
        public UserController(IInjectService<UserController, IUserInfoRepository> service) : base(service)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetUserSimple()
        {
            var isCompleted = false;
            var userName = User.Claims.FirstOrDefault(x => x.Type == "name").Value;
            var userInfo = await _repository.GetByNameAsync(User.Claims.FirstOrDefault(x => x.Type == "name").Value);
            if (userInfo == null)
            {
                userInfo = await _repository.CreateUserInfoAsync(userName, User.Claims.FirstOrDefault(x => x.Type == "role").Value);
                await _uf.SaveChangesAsync();
            }
            if (await _repository.IsCompletedAsync(userInfo.Id, User.Claims.FirstOrDefault(x => x.Type == "email").Value))
            {
                isCompleted = true;
            }

            var userSimpleModel = _mapper.Map<UserSimpleViewModel>(userInfo);
            userSimpleModel.IsCompleted = isCompleted;
            return Ok(userSimpleModel);
        }

        [HttpPost]
        public async Task<IActionResult> UploadAvatar(IFormFile file)
        {
            var data = new MessageDataViewModel();
            if (file == null ||
              file.Length == 0 ||
              file.Length > 4 * 1024 * 1024 ||
              new[] { ".jpg", ".png", ".gif", ".jpeg" }.All(x => x != Path.GetExtension(file.FileName).ToLower()))
            {
                data.IsSuccess = false;
                data.Message = "文件不合法";
                return Json(data);
            }

            var userInfo = await _repository.GetByNameAsync(User.Claims.First(x => x.Type == "name").Value);
            if (userInfo == null)
            {
                data.IsSuccess = false;
                data.Message = "无法得到该用户信息";
                return Json(data);
            }

            var imgRootPath = $@"{AssetsConfiguration.Configs.Assets}\imgs\avatars";
            if (!Directory.Exists(imgRootPath))
            {
                Directory.CreateDirectory(imgRootPath);
            }

            var oldFile = Path.Combine(imgRootPath, userInfo.Avatar);
            if (System.IO.File.Exists(oldFile))
            {
                System.IO.File.Delete(oldFile);
            }

            var fileName = DateTime.Now.ToString("yyyyMMddHHssfff") + new Random().Next() % 100 + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(imgRootPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            userInfo.Avatar = fileName;
            _repository.Update(userInfo);
            await _uf.SaveChangesAsync();

            data.IsSuccess = true;
            data.Message = fileName;
            return Ok(data);
        }
    }
}
    