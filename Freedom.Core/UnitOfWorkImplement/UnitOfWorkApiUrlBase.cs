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
    public class UnitOfWorkApiUrlBase : IUnitOfWorkApiUrlBase
    {
        private readonly IMapper _mapper;
        private readonly IApiUrlBaseRepository _apiUrlBaseRepository;

        public UnitOfWorkApiUrlBase(IMapper mapper, IApiUrlBaseRepository apiUrlBaseRepository)
        {
            _mapper = mapper;
            _apiUrlBaseRepository = apiUrlBaseRepository;
        }

        public async Task AddOrUpdateAsync(ApiUrlBaseBind apiUrlBaseBind)
        {
            var entity = _mapper.Map<ApiUrlBase>(apiUrlBaseBind);

            var exist = await _apiUrlBaseRepository.ExistAsync(apiUrlBaseBind.Id);
            if (exist)
            {
               await _apiUrlBaseRepository.UpdateAsync(entity);
                return;
            }
            
            await _apiUrlBaseRepository.InsertAsync(entity);
        }

        public async Task<ApiUrlBaseBind> GetByIdAsync(Guid Id)
        {
            var ent = await _apiUrlBaseRepository.GetAsync(Id);
            return _mapper.Map<ApiUrlBaseBind>(ent);
        }

        public async Task<List<ApiUrlBaseBind>> GetAllAsync()
        {
            var collection = await _apiUrlBaseRepository.GetAllAsync();
            return _mapper.Map<List<ApiUrlBaseBind>>(collection);
        }

        public async Task<bool> ExistByIdAsync(Guid Id)
        {
            var collection = await _apiUrlBaseRepository.GetAllAsync();
            return collection.Any(x => x.Id == Id);
        }

        public async Task UpdateAsync(ApiUrlBaseBind entity)
        {
            ApiUrlBase apiUrlBase = _mapper.Map<ApiUrlBase>(entity);
            await _apiUrlBaseRepository.UpdateAsync(apiUrlBase);
        }

        public async Task InsertAsync(ApiUrlBaseBind entity)
        {
            ApiUrlBase apiUrlBase = _mapper.Map<ApiUrlBase>(entity);
            await _apiUrlBaseRepository.InsertAsync(apiUrlBase);
        }
    }
}