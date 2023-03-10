using Mapster;
using Microsoft.Extensions.Options;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.BLL.Category;

public record CreateCategoryCommand(
    int StudentStoreId,
    CategoryDTO Category
) : ICommand;

public record CreateCategoryCommandResult(
    CategoryDTO Category
) : ICommandResult;


public class CreateCategoryCommandHandler : Command<UniversityOrderAPIDbContext>,
ICommandHandler<CreateCategoryCommand, CreateCategoryCommandResult>, IConfig
{
    public CreateCategoryCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext) { }

    public CreateCategoryCommandHandler(UniversityOrderAPIDbContext dbContext, IOptions<Config> config) :
        this(dbContext)
    {
        Config = config;
    }

    public Task<CreateCategoryCommandResult> Handle(CreateCategoryCommand request,
        CancellationToken? cancellationToken)
    {
        var maxSlotsPerStudent = Config.Value.MaxSlotsPerStudent;

        var countOfCategoriesPerStudentStore = DbContext.Categories
            .Count(el => el.StudentStoreId == request.StudentStoreId);

        if (countOfCategoriesPerStudentStore >= maxSlotsPerStudent)
            throw new Exception($"Max amount of categories per student store was exceeded, allowed: {maxSlotsPerStudent}");
        
        if (string.IsNullOrEmpty(request.Category.Name))
            throw new Exception("Category name null or empty");

        var newCategory = new DAL.Models.Category
        {
            StudentStoreId = request.StudentStoreId,
            Name = request.Category.Name
        };

        DbContext.Categories.Add(newCategory);

        DbContext.SaveChanges();

        return Task.FromResult(new CreateCategoryCommandResult(
            newCategory.Adapt<CategoryDTO>()));
    }

    public IOptions<Config> Config { get; set; }
}