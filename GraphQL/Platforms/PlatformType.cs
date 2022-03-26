using System.Linq;
using CommanderGQL.Data;
using CommanderGQL.Models;
using HotChocolate;
using HotChocolate.Types;

namespace CommanderGQL.GraphQL.Platforms
{
    public class PlatformType : ObjectType<Platform>
    {
        protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
        {
            descriptor
                .Description("Платформа и коммандные строки.");

            descriptor
                .Field(p => p.Id)
                .Description("Id платформы");

            descriptor
                .Field(p => p.Name)
                .Description("Имя платформы");
            
            descriptor
                .Field(p => p.LicenseKey)
                .Ignore();

            descriptor
                .Field(p => p.Commands)
                .ResolveWith<Resolvers>(p => p.GetCommands(default!, default!))
                .UseDbContext<AppDbContext>()
                .Description("Список доступных команд для платформы");
        }

        private class Resolvers
        {
            public IQueryable<Command> GetCommands([Parent]Platform platform, [ScopedService] AppDbContext context)
            {
                return context.Commands
                    .Where(p => p.PlatformId == platform.Id);
            }
        }
    }
}