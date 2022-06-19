using AutoMapper;
using Freedom.Core.SQLiteRepositoryInterface;
using Freedom.Core.UnitOfWorkInterface;
using Freedom.Frontend.Models.SqliteModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Freedom.Core.UnitOfWorkImplement
{
    public class UnitOfWorkLoginStore : IUnitOfWorkLoginStore
    {
        private readonly IMapper _mapper;
        private readonly ILoginStoreRepository _loginStoreRepository;

        public UnitOfWorkLoginStore(IMapper mapper,
                                    ILoginStoreRepository loginStoreRepository)
        {
            _mapper = mapper;
            _loginStoreRepository = loginStoreRepository;
        }

        public async Task AddOrUpdateAsync(LoginStore entity)
        {
            await _loginStoreRepository.AddOrUpdateAsync(entity);
        }

        public async Task<List<LoginStore>> GetAllAsync()
        {
            return await _loginStoreRepository.GetAllAsync();
        }

        public async Task InsertAsync(LoginStore entity)
        {
            await _loginStoreRepository.InsertAsync(entity);
        }

        public async Task UpdateAsync(LoginStore entity)
        {
            await _loginStoreRepository.UpdateAsync(entity);
        }
    }
}