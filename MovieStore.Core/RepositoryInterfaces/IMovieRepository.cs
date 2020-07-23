using MovieStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Core.RepositoryInterfaces
{
    public interface IMovieRepository:IAsyncRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetHighestRevenueMovies();
        Task<IEnumerable<Movie>> GetTop25RatedMovies();
    }
}
//IAsyncrepo has 8 methods
//Publica class MovieRepo:  EfRepository,IMovieRepository
//{
//1+8 vs 1
//   Just 1 method because we inherite 8 method from EfRepository
//}

///
