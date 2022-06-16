using KT.TodoAppNTier.Common.ResponseObjects;
using KT.TodoAppNTier.Dtos.Interfaces;
using KT.TodoAppNTier.Dtos.WorkDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.TodoAppNTier.Business.Interfaces
{
    public interface IWorkServices
    {
        Task<IResponse<List<WorkListDto>>> GetAll();

        Task<IResponse<WorkCreateDto>> Create(WorkCreateDto workCreateDto);

        Task<IResponse<IDto>> GetById<IDto>(int id);

        Task<IResponse> Remove(int id);

        Task<IResponse<WorkUpdateDto>> Update(WorkUpdateDto workUpdateDto);

    }
}
