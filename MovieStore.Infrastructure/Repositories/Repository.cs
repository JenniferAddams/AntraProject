//using MovieStore.Infrastructure.Data;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace MovieStore.Infrastructure.Repositories
//{
//    public class Repository<T> where T : class
//        //不加where， 任何type
//        //add where :it means we can only import regular classes
//        //where T:struct
//        //the classes cant break the contract in the interfaces so that the code wont be wroing 
//        //they must impletement all method in the interface
//        //we can use abstract class  to replace some interface.
//        //the difference is they impletement differently
//    {
//        private readonly MovieStoreDbContext _dbContext;
//        public Repository(MovieStoreDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }
//        public List<T> GetAll()
//        {
//            // LINQ => Collection of extenstion methods... on Ienumerable type,
//            //that means any class that implments IEnumerable will have access to all the extension methods in LINQ

//            //purple:Normal methods in the list class
//            //grey with blue arror : extension method
//            //eg: go to definition we could see where method:this IEnumerable<TSource> source, means the extension method is in IEnumberable ,which is an interface.
//            // "this IEnumerable<TSource> source" means the class who implementing IEnumerable could access to this function
//            var test = new List<int>();
//            test.Add(2);
//            test.Add(2);
//            return _dbContext.Set<T>().ToList();
//        }
//    }
//}
