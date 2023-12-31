﻿using Planner.Models.Domain;

namespace Planner.Repositories.Interface
{
    public interface IWeekDayRepository
    {
        Task<IEnumerable<WeekDay>> GetAllAsync();
        Task<WeekDay?> GetAsync(Guid id);
        Task<WeekDay> AddAsync(WeekDay weekDay);
        Task<WeekDay?> UpdateAsync(WeekDay weekDay);
        Task<WeekDay?> DeleteAsync(Guid id);

    }
}
