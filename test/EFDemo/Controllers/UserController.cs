using Microsoft.AspNetCore.Mvc;
using EFDemo.Data;
using DotNetCore.Data.EntityFrameworkCore.Repositories;
using DotNetCore.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EFDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        // ���Ͳִ�
        // ���DbContextʱ�����������ָ��DbContext��Ĭ��ʹ�õ�һ��ע���DbContext
        private readonly IEfCoreRepository<UserDbContext, User,string> _repository;

        //�Զ���ִ�
        private readonly IUserRepository _userRepository;



        public UserController(IEfCoreRepository<UserDbContext, User,string> repository,
            IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        [HttpGet("FirstOrDefaultAsync")]
        public async Task<User> FirstOrDefaultAsync()
        {
            return await _repository.Table.FirstOrDefaultAsync();
        }

        [HttpGet("GetAllAsync")]
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var context = _repository.CurrentDbContext;
            return await _repository.Table.ToListAsync();
        }

        [HttpPost]
        public async Task<User> InsertAsync(string name)
        {
            var entity = await _userRepository.InsertAsync(new User { Name = name }, HttpContext.RequestAborted);
            return entity;
        }

    }
}