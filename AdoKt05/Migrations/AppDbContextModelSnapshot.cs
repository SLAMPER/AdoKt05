using System;
using AdoKt05.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AdoKt05.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "10.0.2");

            modelBuilder.Entity("AdoKt05.Data.CategoryEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categories", (string)null);
                });

            modelBuilder.Entity("AdoKt05.Data.ProductEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("FkCategoryId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("FkCategoryId");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("AdoKt05.Data.ProductEntity", b =>
                {
                    b.HasOne("AdoKt05.Data.CategoryEntity", "CategoryEntity")
                        .WithMany("ProductEntities")
                        .HasForeignKey("FkCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoryEntity");
                });

            modelBuilder.Entity("AdoKt05.Data.CategoryEntity", b =>
                {
                    b.Navigation("ProductEntities");
                });
#pragma warning restore 612, 618
        }
    }
}
