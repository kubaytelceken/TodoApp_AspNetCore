using AutoMapper;
using FluentValidation;
using KT.TodoAppNTier.Business.Extensions;
using KT.TodoAppNTier.Business.Interfaces;
using KT.TodoAppNTier.Business.ValidationRules;
using KT.TodoAppNTier.Common.ResponseObjects;
using KT.TodoAppNTier.DataAccess.UnitOfWork;
using KT.TodoAppNTier.Dtos.Interfaces;
using KT.TodoAppNTier.Dtos.WorkDtos;
using KT.TodoAppNTier.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.TodoAppNTier.Business.Services
{
    public class WorkServices : IWorkServices
    {
        private readonly IUow _context;
        private readonly IMapper _mapper;
        private readonly IValidator<WorkCreateDto> _createDtoValitor; 
        private readonly IValidator<WorkUpdateDto> _updateDtoValitor;

        public WorkServices(IUow context, IMapper mapper, IValidator<WorkCreateDto> createDtoValitor, IValidator<WorkUpdateDto> updateDtoValitor)
        {
            _context = context;
            _mapper = mapper;
            _createDtoValitor = createDtoValitor;
            _updateDtoValitor = updateDtoValitor;
        }

        public async Task<IResponse<WorkCreateDto>> Create(WorkCreateDto workCreateDto)
        {
            var validationResult = _createDtoValitor.Validate(workCreateDto);
            if (validationResult.IsValid)
            {
                await _context.GetRepository<Work>().Create(_mapper.Map<Work>(workCreateDto));
                await _context.SaveChanges();
                return new Response<WorkCreateDto>(ResponseType.Success, workCreateDto);
            }
            else
            {
                
                return new Response<WorkCreateDto>(ResponseType.ValidationError, workCreateDto, validationResult.ConvertToCustomValidationError());
            }
           
        }

        public async Task<IResponse<List<WorkListDto>>> GetAll()
        {

            var list = await _context.GetRepository<Work>().GetAll();

            var data = _mapper.Map<List<WorkListDto>>(list);

            return new Response<List<WorkListDto>>(ResponseType.Success, data);  
           
            
        }

        public async Task<IResponse<IDto>> GetById<IDto>(int id)
        {
            var work = await _context.GetRepository<Work>().GetByFilter(x=>x.Id == id);
            var data = _mapper.Map<IDto>(work);
            if (data == null)
            {
                return new Response<IDto>(ResponseType.NotFound, $"Id'ye ait data bulunamadı");
            }
            else
            {
                return new Response<IDto>(ResponseType.Success,data);
            }
             

            //return new WorkListDto()
            //{
            //    IsCompleted = work.IsCompleted,
            //    Definition = work.Definition
            //};
        }

        public async Task<IResponse> Remove(int id)
        {
            //var deletedEntity = await _context.GetRepository<Work>().GetById(id);

            var entity = await _context.GetRepository<Work>().GetByFilter(x => x.Id == id);
            if(entity != null)
            {
                _context.GetRepository<Work>().Remove(entity);
                await _context.SaveChanges();
                return new Response(ResponseType.Success);
            }
            else
            {
                return new Response(ResponseType.NotFound , "Id ye ait data bulunamadı.");
            }
            
        }

        public async Task<IResponse<WorkUpdateDto>> Update(WorkUpdateDto workUpdateDto)
        {
            var result=  _updateDtoValitor.Validate(workUpdateDto);
            if (result.IsValid)
            {
                var updatedEntity = await _context.GetRepository<Work>().GetById(workUpdateDto.Id);
                if(updatedEntity != null)
                {
                    _context.GetRepository<Work>().Update(_mapper.Map<Work>(workUpdateDto), updatedEntity);

                    await _context.SaveChanges();

                    return new Response<WorkUpdateDto>(ResponseType.Success, workUpdateDto);
                }
                else
                {
                    return new Response<WorkUpdateDto>(ResponseType.NotFound, "Id ye ait data bulunamadı.");
                }
               
            }
            else
            {
                
                return new Response<WorkUpdateDto>(ResponseType.ValidationError, workUpdateDto, result.ConvertToCustomValidationError());
            }

        }
    }
}
