using Microsoft.EntityFrameworkCore.Design;

namespace CountersLibrary;

class SampleContextFactory : IDesignTimeDbContextFactory<ApplContext>
{
    public ApplContext CreateDbContext(string[] args)
    {
        return new ApplContext();
    }
}