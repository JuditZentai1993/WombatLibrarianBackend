﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WombatLibrarianApi.Models;

namespace WombatLibrarianApi.Migrations
{
    [DbContext(typeof(WombatBooksContext))]
    partial class WombatBooksContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("BookBookshelf", b =>
                {
                    b.Property<string>("BooksId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("BookshelvesId")
                        .HasColumnType("int");

                    b.HasKey("BooksId", "BookshelvesId");

                    b.HasIndex("BookshelvesId");

                    b.ToTable("BookBookshelf");
                });

            modelBuilder.Entity("BookWishlist", b =>
                {
                    b.Property<string>("BooksId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("WishlistsId")
                        .HasColumnType("int");

                    b.HasKey("BooksId", "WishlistsId");

                    b.HasIndex("WishlistsId");

                    b.ToTable("BookWishlist");
                });

            modelBuilder.Entity("WombatLibrarianApi.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("BookId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("WombatLibrarianApi.Models.Book", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasMaxLength(100000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Language")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaturityRating")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PageCount")
                        .HasColumnType("int");

                    b.Property<string>("Published")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Publisher")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.Property<double>("RatingCount")
                        .HasColumnType("float");

                    b.Property<string>("Subtitle")
                        .HasMaxLength(10000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Thumbnail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("WombatLibrarianApi.Models.Bookshelf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.HasKey("Id");

                    b.ToTable("Bookshelves");
                });

            modelBuilder.Entity("WombatLibrarianApi.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("BookId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("WombatLibrarianApi.Models.Wishlist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.HasKey("Id");

                    b.ToTable("Wishlists");
                });

            modelBuilder.Entity("BookBookshelf", b =>
                {
                    b.HasOne("WombatLibrarianApi.Models.Book", null)
                        .WithMany()
                        .HasForeignKey("BooksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WombatLibrarianApi.Models.Bookshelf", null)
                        .WithMany()
                        .HasForeignKey("BookshelvesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BookWishlist", b =>
                {
                    b.HasOne("WombatLibrarianApi.Models.Book", null)
                        .WithMany()
                        .HasForeignKey("BooksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WombatLibrarianApi.Models.Wishlist", null)
                        .WithMany()
                        .HasForeignKey("WishlistsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WombatLibrarianApi.Models.Author", b =>
                {
                    b.HasOne("WombatLibrarianApi.Models.Book", null)
                        .WithMany("Authors")
                        .HasForeignKey("BookId");
                });

            modelBuilder.Entity("WombatLibrarianApi.Models.Category", b =>
                {
                    b.HasOne("WombatLibrarianApi.Models.Book", null)
                        .WithMany("Categories")
                        .HasForeignKey("BookId");
                });

            modelBuilder.Entity("WombatLibrarianApi.Models.Book", b =>
                {
                    b.Navigation("Authors");

                    b.Navigation("Categories");
                });
#pragma warning restore 612, 618
        }
    }
}
