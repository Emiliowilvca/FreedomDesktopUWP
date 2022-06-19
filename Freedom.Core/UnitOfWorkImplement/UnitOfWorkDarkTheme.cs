using AutoMapper;
using Freedom.Core.SQLiteRepositoryInterface;
using Freedom.Core.UnitOfWorkInterface;
using Freedom.Frontend.Models.SqliteModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Freedom.Core.UnitOfWorkImplement
{
    public class UnitOfWorkDarkTheme : IUnitOfWorkDarkTheme
    {
        private readonly IMapper _mapper;
        private readonly IDarkThemeRepository _darkThemeRepository;

        public UnitOfWorkDarkTheme(IMapper mapper, IDarkThemeRepository darkThemeRepository)
        {
            _mapper = mapper;
            _darkThemeRepository = darkThemeRepository;
        }

        public async Task AddOrUpdateAsync(DarkTheme entity)
        {
            await _darkThemeRepository.AddOrUpdateAsync(entity);
        }

        public async Task<DarkTheme> GetByIdAsync(Guid Id)
        {
            return await _darkThemeRepository.GetAsync(Id);
        }

        public async Task<List<DarkTheme>> GetAllAsync()
        {
            return await _darkThemeRepository.GetAllAsync();
        }

        public async Task<bool> ExistByIdAsync(Guid Id)
        {
            return await _darkThemeRepository.ExistAsync(Id);
        }

        public async Task UpdateAsync(DarkTheme entity)
        {
            await _darkThemeRepository.UpdateAsync(entity);
        }

        public async Task InsertAsync(DarkTheme entity)
        {
            await _darkThemeRepository.InsertAsync(entity);
        }
    }
}