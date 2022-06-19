using AutoMapper;
using Freedom.Core.SQLiteRepositoryInterface;
using Freedom.Core.UnitOfWorkInterface;
using Freedom.Frontend.Models.BindableSqlite;
using Freedom.Frontend.Models.SqliteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freedom.Core.UnitOfWorkImplement
{
    public class UnitOfWorkLoginHistory : IUnitOfWorkLoginHistory
    {
        private readonly IMapper _mapper;
        private readonly ILoginHistoryRepository _loginHistoryRepository;

        public UnitOfWorkLoginHistory(IMapper mapper,
                                      ILoginHistoryRepository loginHistoryRepository)
        {
            _mapper = mapper;
            _loginHistoryRepository = loginHistoryRepository;
        }

        public async Task<List<string>> GetUserNames()
        {
            List<LoginHistory> collection = await _loginHistoryRepository.GetAllAsync();
            List<string> resp = collection.Select(x => x.Email).Distinct().ToList();
            return resp;
        }

        public async Task<List<LoginHistoryBind>> GetAllAsync()
        {
            List<LoginHistory> collection = await _loginHistoryRepository.GetAllAsync();
            return _mapper.Map<List<LoginHistoryBind>>(collection);
        }

        public async Task InsertAsync(LoginHistoryBind entity)
        {
            LoginHistory ent = _mapper.Map<LoginHistory>(entity);
            await _loginHistoryRepository.InsertAsync(ent);
        }
    }
}