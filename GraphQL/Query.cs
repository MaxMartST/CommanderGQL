using System.Linq;
using CommanderGQL.Data;
using CommanderGQL.Models;
using HotChocolate;
using HotChocolate.Data;

namespace CommanderGQL.GraphQL
{
    public class Query
    {
        [UseDbContext(typeof(AppDbContext))]
        // эта настройка для метода GetPlatform, позволяет получить
        // контекст, выполнить query и вернуть контекст, когда он
        // будет завершён. Поэтому исп. [ScopedService]
        //
        //[UseProjection] // закоментировал, т. к. явно указал приобразователи в Type
        // показывать дочерние объекты
        [UseFiltering] // фильтрация
        [UseSorting] // сортировка
        public IQueryable<Platform> GetPlatform([ScopedService] AppDbContext context)
        {
            return context.Platforms;
        }

        [UseDbContext(typeof(AppDbContext))]
        //[UseProjection] // закоментировал, т. к. явно указал приобразователи в Type
        [UseFiltering] // фильтрация
        [UseSorting] // сортировка
        public IQueryable<Command> GetCommands([ScopedService] AppDbContext context)
        {
            return context.Commands;
        }
    }
}