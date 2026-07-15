using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;
using UserRoleManagement.Authors;
using UserRoleManagement.Books;
namespace UserRoleManagement.Blazor;
[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class UserRoleManagementBlazorMappers : MapperBase<BookDto, CreateUpdateBookDto>
{
    public override partial CreateUpdateBookDto Map(BookDto source);
    public override partial void Map(BookDto source, CreateUpdateBookDto destination);
}
[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class UserRoleManagementAuthorDtoToCreateUpdateAuthorDtoMapper : MapperBase<AuthorDto, CreateUpdateAuthorDto>
{
    public override partial CreateUpdateAuthorDto Map(AuthorDto source);
    public override partial void Map(AuthorDto source, CreateUpdateAuthorDto destination);
}
