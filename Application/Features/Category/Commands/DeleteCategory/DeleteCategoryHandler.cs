using System;
using Application.Contracts.Persistence;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Category.Commands.DeleteCategory;

public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, CategoryDto>
{
    private readonly ICategoryRepository categoryRepository;
    private readonly IMapper mapper;

    public DeleteCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        this.categoryRepository = categoryRepository;
        this.mapper = mapper;
    }

    public async Task<CategoryDto> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Category result = await categoryRepository.DeleteAsync(request.Id);
        
        return mapper.Map<CategoryDto>(result);
    }
}
