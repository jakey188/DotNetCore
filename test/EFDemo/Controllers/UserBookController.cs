using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using EFDemo.Data;
using EFDemo.Entities;
using DotNetCore.Data;
using DotNetCore.Data.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;
using DotNetCore.Data.EntityFrameworkCore.UnitOfWorks;
using DotNetCore.Data.EntityFrameworkCore.Uow;

namespace EFDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]  
    public class UserBookController : ControllerBase
    {
        // 泛型仓储
        // 多个DbContext时，如果不传入指定DbContext，默认使用第一个注入的DbContext
        private readonly IEfCoreRepository<UserBookDbContext, UserBook,int> _repository;

        // 工作单元,
        // 如果不传入指定DbContext，默认使用第一个注入的DbContext
        private readonly IUnitOfWork<UserBookDbContext> _unitOfWork;


        public UserBookController(IEfCoreRepository<UserBookDbContext, UserBook,int> repository,
            IUnitOfWork<UserBookDbContext> unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("FirstOrDefaultAsync")]
        public async Task<UserBook> FirstOrDefaultAsync()
        {
            var rep = _unitOfWork.GetGenericRepository<UserBookDbContext, UserBook, int>();

            return await _repository.Table.FirstOrDefaultAsync();
        }

        [HttpGet("GetAllAsync")]
        public async Task<IEnumerable<UserBook>> GetAllAsync()
        {
            return await _repository.Table.ToListAsync();
        }

        [HttpGet("InsertAsync")]
        public async Task<UserBook> InsertAsync([Required] string userId, [Required] int bookId, [Required] string name)
        {
            var entity = await _repository.InsertAsync(new UserBook { BookId = bookId, UserId = userId, Name = name }, HttpContext.RequestAborted);
            return entity;
        }

    }
}