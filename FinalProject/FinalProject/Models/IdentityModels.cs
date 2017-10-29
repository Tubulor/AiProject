using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAutheC:\Users\Or\Documents\Visual Studio 2017\Projects\FinalProject\FinalProject\Models\ManageViewModels.csnticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Products> Products { get; set; }
        public DbSet<Buys> Buys { get; set; }

        public DbSet<Branches> Branches { get; set; }
    }

    public class Products
    {
        [Key]
        public int ID { get; set; }
        [Required]
		[DataType(DataType.Text, ErrorMessage = "This field can not be empty.")]
		public string ProductName { get; set; }
        [Required]
		[Range(0, double.MaxValue, ErrorMessage = "Please enter valid Number")]
		public double Price { get; set; }
        [Required]
		[DataType(DataType.Text, ErrorMessage = "This field can not be empty.")]
		public string Description { get; set; }
        [Required]
		[DataType(DataType.Text,ErrorMessage = "This field can not be empty.")]
		public string Brand { get; set; }
		[Required]
		[DataType(DataType.Text, ErrorMessage = "This field can not be empty.")]
		public string Inches { get; set; }
		[Required]
		[DataType(DataType.Text, ErrorMessage = "This field can not be empty.")]
		public string Resolution { get; set; }
		[DataType(DataType.Text, ErrorMessage = "This field can not be empty.")]
		public string RefreshRate { get; set; }
		[Required]
		[Url(ErrorMessage = "Please enter vaild url .")]
		public string Image { get; set; }
        

    }

    public class Buys
    {

        private DateTime? currentTime;
        public int ID { get; set; }
        public int ProductsID { get; set; }
        public string MembersID { get; set; }
		[Required]
		[Range(0, double.MaxValue, ErrorMessage = "Please enter valid Number")]
		public double PriceBought { get; set; }
        public DateTime DateBought
        {
            get { return currentTime ?? DateTime.Now; }
            set { currentTime = value; }
        }
        public virtual ICollection<Products> Product { get; set; }

        public virtual Products Products { get; set; }
		[ForeignKey("MembersID")]
		public virtual ApplicationUser ApplicationUsers { get; set; }
    }

    public class Branches
    {
        [Key]
        public int ID { get; set; }
		[Required]
		[DataType(DataType.Text, ErrorMessage = "This field can not be empty.")]
		public string BranchName { get; set; }
		[Required]
		[DataType(DataType.Text, ErrorMessage = "This field can not be empty.")]
		public string Country { get; set; }
		[Required]
		[DataType(DataType.Text, ErrorMessage = "This field can not be empty.")]
		public string City { get; set; }
		[Required]
		[Range(0, int.MaxValue, ErrorMessage = "Please enter valid Number")]
		public int HouseNumber { get; set; }
		[Required]
		[DataType(DataType.Text, ErrorMessage = "This field can not be empty.")]
		public string Street { get; set; }
		[Required(ErrorMessage = "You must provide a PhoneNumber")]
		[DataType(DataType.PhoneNumber)]
		[RegularExpression(@"^\(?([0-9]{2})\)?[-. ]?([0-9]{7})$", ErrorMessage = "Not a valid Phone number")]
		public string PhoneNumber { get; set; }
		[Required]
		public bool Saturday { get; set; }
		[Required]
		[Range(0, int.MaxValue, ErrorMessage = "Please enter valid Number")]
		public int BranchNumber { get; set; }
		[Required]
		[Url(ErrorMessage = "Please enter vaild url .")]
		public string Image { get; set; }
	}
}