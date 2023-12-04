using libermanyankt_42_20.Database;
using libermanyankt_42_20.Filters;
using libermanyankt_42_20.Filters.PrepodKafedraFilters;
using libermanyankt_42_20.Interfaces;
using libermanyankt_42_20.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace liberman_yan.Test2
{
    public class Test2
    {
        public readonly DbContextOptions<PrepodDbContext> _dbContextOptions;

        public Test2()
        {
            _dbContextOptions = new DbContextOptionsBuilder<PrepodDbContext>()
            .UseInMemoryDatabase(databaseName: "prepod_db3")
            .Options;
        }

        [Fact]
        public async Task GetPrepodsByKafedraAsync_KT4220_OneObjects()
        {
            // Arrange
            var ctx = new PrepodDbContext(_dbContextOptions);
            var prepodService = new PrepodService(ctx);
            var kafedra = new List<Kafedra>
            {
                new Kafedra
                {
                    KafedraId =1,
                    KafedraName = "кафедра компьютерных технологий"
                },
                new Kafedra
                {
                    KafedraId =2,
                    KafedraName = "кафедра инженерной техники"
                }
            };
            await ctx.Set<Kafedra>().AddRangeAsync(kafedra);

            await ctx.SaveChangesAsync();

            var degree = new List<Degree>
            {
                new Degree
                {
                    DegreeId =1,
                    Name_degree = "доктор наук"
                },
                new Degree
                {
                    DegreeId =2,
                    Name_degree = "кандидат наук"
                }
            };
            await ctx.Set<Degree>().AddRangeAsync(degree);

            await ctx.SaveChangesAsync();

            var prepods = new List<Prepod>
            {
                new Prepod
                {
                    FirstName = "123",
                    LastName = "123",
                    MiddleName = "123",
                    Telephone = "555",
                    Mail = "123@mail.ru",
                    KafedraId = 1,
                    DegreeId = 2
                },
                new Prepod
                {
                    FirstName = "mem",
                    LastName = "mem",
                    MiddleName = "mem",
                    Telephone = "9999",
                    Mail = "mem@gmail.com",
                    KafedraId = 1,
                    DegreeId = 1
                },
                new Prepod
                {
                    FirstName = "mem1",
                    LastName = "mem1",
                    MiddleName = "mem1",
                    Telephone = "99991",
                    Mail = "mem1@gmail.com",
                    KafedraId = 2,
                    DegreeId = 1
                }
            };
            await ctx.Set<Prepod>().AddRangeAsync(prepods);

            await ctx.SaveChangesAsync();

            // Act
            var filter = new PrepodNameFilter
            {
                KafedraId = 1
            };
            var prepodsResult = await prepodService.GetPrepodsByNameAsync(filter, CancellationToken.None);

            Assert.Equal(2, prepodsResult.Length);
        }
    }
}
