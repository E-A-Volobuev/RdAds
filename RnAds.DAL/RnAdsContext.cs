using Microsoft.EntityFrameworkCore;
using RnAds.Model.Model;


namespace RnAds.DAL
{
    public class RnAdsContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Ad> Ads { get; set; }

        public DbSet<Dialog> Dialogs { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        /// <summary>
        /// фото для объявлений
        /// </summary>
        public DbSet<Picture> Pictures { get; set; }

        /// <summary>
        /// фото профилей пользователей
        /// </summary>
        public DbSet<AvatarUser> Avatars { get; set; }

        /// <summary>
        /// id пользователей,добавивиших объявление в избранные
        /// </summary>
        public DbSet<FavoriteUserId> FavoriteUserIdes { get; set; }

        public RnAdsContext(DbContextOptions<RnAdsContext> options)
           : base(options)
        {
            //Database.EnsureCreated();
        }
    }
}
