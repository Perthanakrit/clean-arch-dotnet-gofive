using Application.Contracts.Persistence;
using Application.Models;
using AutoMapper;
using MediatR;

namespace Application.Features.Category.Commands.UpdateCategory;

public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto>
{
    private readonly ICategoryRepository categoryRepository;
    private readonly IMapper mapper;

    public UpdateCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        this.mapper = mapper;
        this.categoryRepository = categoryRepository;
    }

    public async Task<CategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Category categoryModel = mapper.Map<Domain.Entities.Category>(request.Request);
    
        Domain.Entities.Category result = await categoryRepository.UpdateAsync(categoryModel);

        if (result == null)
        {
            throw new Exception("Update failed");
        }

        return mapper.Map<CategoryDto>(result);
    }
}
