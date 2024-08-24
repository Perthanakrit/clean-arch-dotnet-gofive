using System;
using Application.Models;
using MediatR;

namespace Application.Features.Category.Commands.UpdateCategory;

public class UpdateCategoryCommand : IRequest<CategoryDto>
{
    public Guid Id { get; set; }
    public UpdateCategoryRequestDto Request { get; set; }    
}
