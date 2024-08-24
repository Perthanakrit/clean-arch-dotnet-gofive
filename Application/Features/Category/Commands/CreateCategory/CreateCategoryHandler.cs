using Application.Contracts.Persistence;
using Application.Models;
using AutoMapper;
using MediatR;

namespace Application.Features.Category.Commands.CreateCategory;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    private readonly ICategoryRepository categoryRepository;
    private readonly IMapper mapper;

    public CreateCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        this.mapper = mapper;
        this.categoryRepository = categoryRepository;
    }

    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Category categoryModel = mapper.Map<Domain.Entities.Category>(request.Request);
        Domain.Entities.Category result = await categoryRepository.CreateAsync(categoryModel);

        return mapper.Map<CategoryDto>(result);
    }
}
