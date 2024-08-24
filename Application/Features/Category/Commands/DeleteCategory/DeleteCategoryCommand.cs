using System;
using Application.Models;
using MediatR;

namespace Application.Features.Category.Commands.DeleteCategory;

public class DeleteCategoryCommand : IRequest<CategoryDto>
{
    public Guid Id { get; set; }
}
