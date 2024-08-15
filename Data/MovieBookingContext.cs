using Microsoft.EntityFrameworkCore;
using Movie_Reservation.Models;

namespace Movie_Reservation.Data;

public partial class MovieBookingContext : DbContext
{
    public MovieBookingContext()
    {
    }

    public MovieBookingContext(DbContextOptions<MovieBookingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<GeneralUser> GeneralUsers { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Screen> Screens { get; set; }

    public virtual DbSet<Seat> Seats { get; set; }

    public virtual DbSet<Show> Shows { get; set; }

    public virtual DbSet<Theater> Theaters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=MovieBooking;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.ToTable("Admin");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.ToTable("Booking");

            entity.Property(e => e.BookedByUser).HasColumnName("Booked_By_User");
            entity.Property(e => e.NoOfTickets).HasColumnName("No_Of_Tickets");
            entity.Property(e => e.ShowId).HasColumnName("Show_Id");

            entity.HasOne(d => d.Show).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ShowId)
                .HasConstraintName("FK_Booking_Show");
        });

        modelBuilder.Entity<GeneralUser>(entity =>
        {
            entity.ToTable("General_User");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.ToTable("Movie");

            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.Genre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ReleaseDate)
                .HasColumnType("date")
                .HasColumnName("Release_Date");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Screen>(entity =>
        {
            entity.ToTable("Screen");

            entity.Property(e => e.TheaterId).HasColumnName("Theater_Id");

            entity.HasOne(d => d.Theater).WithMany(p => p.Screens)
                .HasForeignKey(d => d.TheaterId)
                .HasConstraintName("FK_Screen_Theater");
        });

        modelBuilder.Entity<Seat>(entity =>
        {
            entity.ToTable("Seat");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ScreenId).HasColumnName("Screen_Id");
            entity.Property(e => e.SeatNumber).HasColumnName("Seat_Number");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Seat)
                .HasForeignKey<Seat>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Seat_Screen");
        });

        modelBuilder.Entity<Show>(entity =>
        {
            entity.ToTable("Show");

            entity.Property(e => e.AvailableSeats).HasColumnName("Available_Seats");
            entity.Property(e => e.MovieId).HasColumnName("Movie_Id");
            entity.Property(e => e.ScreenId).HasColumnName("Screen_Id");

            entity.HasOne(d => d.Movie).WithMany(p => p.Shows)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK_Show_Movie");

            entity.HasOne(d => d.Screen).WithMany(p => p.Shows)
                .HasForeignKey(d => d.ScreenId)
                .HasConstraintName("FK_Show_Screen");
        });

        modelBuilder.Entity<Theater>(entity =>
        {
            entity.ToTable("Theater");

            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ContactInfo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Contact_Info");
            entity.Property(e => e.MovieId).HasColumnName("Movie_Id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Movie).WithMany(p => p.Theaters)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK_Theater_Movie");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
