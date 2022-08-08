using BLL;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace Project.Models
{
    public class CacheHelper
    {
        public static List<Category> GetCategoriesFromCache()
        {
            var result = WebCache.Get("category-cache");

            if (result == null)
            {
                CategoryManager cm = new CategoryManager();
                result = cm.List();

                WebCache.Set("category-cache", result, 20, true);
            }

            return result;
        }

        public static void RemoveCategoriesFromCache()
        {
            Remove("category-cache");
        }

        public static void Remove(string key)
        {
            WebCache.Remove(key);
        }


        public static List<Brand> GetBrandsFromCache()
        {
            var result = WebCache.Get("brand-cache");

            if (result == null)
            {
                BrandManager bm = new BrandManager();
                result = bm.List();

                WebCache.Set("brand-cache", result, 20, true);
            }

            return result;
        }

        public static void RemoveBrandsFromCache()
        {
            Remove("brand-cache");
        }

    }
}