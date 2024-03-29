using Microsoft.AspNetCore.Mvc;
using EFDemo.Data;
using EFDemo.Services.Impl;
using Microsoft.EntityFrameworkCore;
using DotNetCore.Data.EntityFrameworkCore.UnitOfWorks;
using DotNetCore.Data.EntityFrameworkCore.Uow;


namespace EFDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        //自定义仓储
        private readonly IBookRepository _bookRepository;

        // 工作单元,
        // 如果不传入指定DbContext，默认使用第一个注入的DbContext
        private readonly IUnitOfWork _unitOfWork;

        private readonly BookDbContext _bookDbContext;

        public BookController(IBookService bookService,
            IBookRepository bookRepository,
            IUnitOfWork unitOfWork,
            BookDbContext bookDbContext)
        {
            _bookService = bookService;
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
            _bookDbContext = bookDbContext;

        }

        [HttpGet("FirstOrDefaultAsync")]
        public async Task<Book> FirstOrDefaultAsync()
        {
            //return await _unitOfWork.DbContext.Set<Book>().AsNoTracking().FirstOrDefaultAsync();
            return await _bookRepository.Table.AsNoTracking().FirstOrDefaultAsync();
        }



        [HttpGet("DbQueryMainBookFirst")]
        public async Task<IActionResult> QueryMainBookFirst()
        {
            var data = await _bookDbContext.Book.AsNoTracking().FirstOrDefaultAsync();

            return Ok(new { data });
        }

        [HttpGet("GetAllAsync")]
        public Task<IEnumerable<Book>> GetAllAsync()
        {
            return _bookService.GetAllAsync(HttpContext.RequestAborted);
        }

        [HttpPost("InsertAsync")]
        public async Task<Book> InsertAsync(string name)
        {
            var entity = await _bookRepository.InsertAsync(new Book { Name = name });

            return entity;
        }

        [HttpPost("InsertTranAsync")]
        public async Task<Book> InsertTranAsync(string name)
        {
            var resp = _unitOfWork.GetGenericRepository<Book, int>();

            using var tran = await _unitOfWork.BeginTransactionAsync();

            var entity = await _bookRepository.InsertAsync(new Book { Name = name });

            await tran.CommitAsync();

            return entity;
        }

        [HttpPost("InsertTranExceptionAsync")]
        public async Task<Book> InsertTranExceptionAsync(string name)
        {
            using var tran = await _bookRepository.UnitOfWork.BeginTransactionAsync();

            try
            {
                var entity = await _bookRepository.InsertAsync(new Book { Name = name }, HttpContext.RequestAborted);

                Convert.ToInt32(name);

                await tran.CommitAsync();

                return entity;
            }
            catch (Exception)
            {
                await tran.RollbackAsync();
                return null;
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<int> Update(int id, string name)
        {
            var entity = await _bookRepository.Table.FirstAsync(c=>c.Id==id);

            entity.Name = name; 

            return await _bookRepository.UpdateAsync(entity);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<int> Delete(int id)
        {
            var entity = await _bookRepository.Table.FirstAsync(c => c.Id == id);

            return await _bookRepository.DeleteAsync(entity);
        }

    }
}