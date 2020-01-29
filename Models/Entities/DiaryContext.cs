using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Fox.Microservices.Diary.Models.Entities
{
    public partial class DiaryContext : DbContext
    {
        public DiaryContext()
        {
        }

        public DiaryContext(DbContextOptions<DiaryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AG_B_APPOINTMENT> AG_B_APPOINTMENT { get; set; }
        public virtual DbSet<AG_B_APPOINTMENT_EXT_AUS> AG_B_APPOINTMENT_EXT_AUS { get; set; }
        public virtual DbSet<AG_B_APPOINTMENT_STATUS_HISTORY> AG_B_APPOINTMENT_STATUS_HISTORY { get; set; }
        public virtual DbSet<AG_B_EMPLOYEE_WORKING_HOURS> AG_B_EMPLOYEE_WORKING_HOURS { get; set; }
        public virtual DbSet<AG_S_APPOINTMENT_PLAN_DETAIL> AG_S_APPOINTMENT_PLAN_DETAIL { get; set; }
        public virtual DbSet<AG_S_APPOINTMENT_PLAN_HEADER> AG_S_APPOINTMENT_PLAN_HEADER { get; set; }
        public virtual DbSet<AG_S_OUTCOME_EXT_AUS> AG_S_OUTCOME_EXT_AUS { get; set; }
        public virtual DbSet<AG_S_OUTCOME_REASON_EXT_AUS> AG_S_OUTCOME_REASON_EXT_AUS { get; set; }
        public virtual DbSet<AG_S_RECURRING> AG_S_RECURRING { get; set; }
        public virtual DbSet<AG_S_RECURRING_TYPE> AG_S_RECURRING_TYPE { get; set; }
        public virtual DbSet<AG_S_RESULT> AG_S_RESULT { get; set; }
        public virtual DbSet<AG_S_ROOM> AG_S_ROOM { get; set; }
        public virtual DbSet<AG_S_SERVICE> AG_S_SERVICE { get; set; }
        public virtual DbSet<AG_S_SERVICE_TYPE> AG_S_SERVICE_TYPE { get; set; }
        public virtual DbSet<CM_B_SHOP> CM_B_SHOP { get; set; }
        public virtual DbSet<CM_S_AREA_BOOK> CM_S_AREA_BOOK { get; set; }
        public virtual DbSet<CM_S_CAMPAIGN> CM_S_CAMPAIGN { get; set; }
        public virtual DbSet<CM_S_CITY_BOOK_SHOP> CM_S_CITY_BOOK_SHOP { get; set; }
        public virtual DbSet<CM_S_DAYCENTER> CM_S_DAYCENTER { get; set; }
        public virtual DbSet<CM_S_DAYCENTER_EXT_AUS> CM_S_DAYCENTER_EXT_AUS { get; set; }
        public virtual DbSet<CM_S_EMPLOYEE> CM_S_EMPLOYEE { get; set; }
        public virtual DbSet<CM_S_MEDIATYPE> CM_S_MEDIATYPE { get; set; }
        public virtual DbSet<CM_S_REFERENCE_SOURCE_EXT_AUS> CM_S_REFERENCE_SOURCE_EXT_AUS { get; set; }
        public virtual DbSet<CM_S_REGION_BOOK> CM_S_REGION_BOOK { get; set; }
        public virtual DbSet<CU_B_ACTIVITY> CU_B_ACTIVITY { get; set; }
        public virtual DbSet<CU_B_ACTIVITY_EXT_AUS> CU_B_ACTIVITY_EXT_AUS { get; set; }
        public virtual DbSet<CU_B_ADDRESS_BOOK> CU_B_ADDRESS_BOOK { get; set; }
        public virtual DbSet<CU_B_ADDRESS_BOOK_EXT_AUS> CU_B_ADDRESS_BOOK_EXT_AUS { get; set; }
        public virtual DbSet<SY_EMPLOYEE_TYPE> SY_EMPLOYEE_TYPE { get; set; }
        public virtual DbSet<SY_GENERAL_STATUS> SY_GENERAL_STATUS { get; set; }
        public virtual DbSet<SY_LOCATION_TYPE> SY_LOCATION_TYPE { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=CAU02DB01FOXSIT.D09.ROOT.SYS;Database=FoxAustralia_SIT2;Trusted_Connection=False;User ID=foxuser;Password=Df0x35ZZ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<AG_B_APPOINTMENT>(entity =>
            {
                entity.HasKey(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.SHOP_CODE, e.LAPTOP_CODE, e.APPOINTMENT_ID });

                entity.HasIndex(e => e.APPOINTMENT_ID)
                    .HasName("IX_APPOINTMENT_ID");

                entity.HasIndex(e => new { e.ROWGUID, e.DT_UPDATE })
                    .HasName("IDX_AG_B_APPOINTMENT_ROWGUID");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.LOCATION_TYPE_CODE })
                    .HasName("IDX_AG_B_APPOINTMENT_SY_LOCATION_TYPE");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.RESULT_CODE })
                    .HasName("IDX_AG_B_APPOINTMENT_AG_S_RESULT");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.SERVICE_CODE })
                    .HasName("IDX_AG_B_APPOINTMENT_AG_S_SERVICE");

                entity.HasIndex(e => new { e.CUSTOMER_CODE, e.APPOINTMENT_ID, e.STATUS_CODE })
                    .HasName("IDX_AG_B_APPOINTMENT_STATUS_CODE");

                entity.HasIndex(e => new { e.DT_APPOINTMENT, e.DURATION, e.RECURRING_CODE })
                    .HasName("missing_index_52707");

                entity.HasIndex(e => new { e.EMPLOYEE_CODE, e.COMPANY_CODE, e.DIVISION_CODE })
                    .HasName("IX_AG_B_APPOINTMENT_EMPLOYEE_CODE");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.EMPLOYEE_CODE, e.APPOINTMENT_SHOP_CODE })
                    .HasName("IDX_AG_B_APPOINTMENT_CM_S_EMPLOYEE");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.SHOP_CODE, e.RECURRING_CODE })
                    .HasName("IDX_AG_B_APPOINTMENT_AG_S_RECURRING");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.SHOP_CODE, e.ROOM_CODE })
                    .HasName("IDX_AG_B_APPOINTMENT_AG_S_ROOM");

                entity.HasIndex(e => new { e.DT_APPOINTMENT, e.DURATION, e.COMPANY_CODE, e.DIVISION_CODE, e.APPOINTMENT_ID })
                    .HasName("IDX_AG_B_APPOINTMENT_Comp_Div_App");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.EMPLOYEE_CODE, e.APPOINTMENT_SHOP_CODE, e.STATUS_CODE, e.DT_APPOINTMENT, e.ROOM_CODE })
                    .HasName("missing_index_47387");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.SHOP_CODE, e.LAPTOP_CODE, e.APPOINTMENT_ID, e.USERUPDATE, e.DT_UPDATE })
                    .HasName("missing_index_47356");

                entity.HasIndex(e => new { e.STATUS_CODE, e.COMPANY_CODE, e.DIVISION_CODE, e.EMPLOYEE_CODE, e.APPOINTMENT_SHOP_CODE, e.DT_APPOINTMENT, e.ROOM_CODE })
                    .HasName("missing_index_47385");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.SHOP_CODE, e.LAPTOP_CODE, e.APPOINTMENT_ID, e.DT_INSERT, e.USERUPDATE, e.DT_UPDATE })
                    .HasName("missing_index_47358");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.SHOP_CODE, e.LAPTOP_CODE, e.APPOINTMENT_ID, e.EMPLOYEE_CODE, e.CUSTOMER_CODE, e.APPOINTMENT_SHOP_CODE, e.STATUS_CODE, e.DT_APPOINTMENT, e.SERVICE_CODE })
                    .HasName("missing_index_52289");

                entity.HasIndex(e => new { e.SHOP_CODE, e.LAPTOP_CODE, e.APPOINTMENT_ID, e.STATUS_CODE, e.EMPLOYEE_CODE, e.CUSTOMER_CODE, e.COMPANY_CODE, e.DIVISION_CODE, e.APPOINTMENT_SHOP_CODE, e.DT_APPOINTMENT, e.SERVICE_CODE })
                    .HasName("missing_index_52287");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.SHOP_CODE, e.LAPTOP_CODE, e.APPOINTMENT_ID, e.DURATION, e.EMPLOYEE_CODE, e.CUSTOMER_CODE, e.APPOINTMENT_SHOP_CODE, e.STATUS_CODE, e.DT_APPOINTMENT, e.SERVICE_CODE })
                    .HasName("missing_index_52285");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.SHOP_CODE, e.LAPTOP_CODE, e.APPOINTMENT_ID, e.STATUS_CODE, e.DURATION, e.EMPLOYEE_CODE, e.CUSTOMER_CODE, e.SERVICE_CODE, e.APPOINTMENT_SHOP_CODE, e.DT_APPOINTMENT })
                    .HasName("missing_index_52283");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.SHOP_CODE, e.LAPTOP_CODE, e.APPOINTMENT_ID, e.STATUS_CODE, e.REASON_CODE, e.DT_APPOINTMENT, e.DURATION, e.CUSTOMER_CODE, e.SERVICE_CODE, e.RECURRING_CODE, e.APPOINTMENT_SHOP_CODE, e.LOCATION_TYPE_CODE, e.LOCATION_CODE, e.RESULT_CODE, e.ROOM_CODE, e.NOTE, e.TIMEBEFORE, e.TIMEAFTER, e.FLG_CONFIRMATION, e.FLG_SMS, e.FLG_EMAIL, e.APP_JOURNEY_NUMBER, e.PLAN_ID, e.PLAN_CODE, e.PLAN_APPOINTMENT_ORDER, e.DT_INSERT, e.USERINSERT, e.DT_UPDATE, e.USERUPDATE, e.ROWGUID, e.EMPLOYEE_CODE })
                    .HasName("missing_index_49279");

                entity.HasIndex(e => new { e.LAPTOP_CODE, e.APPOINTMENT_ID, e.REASON_CODE, e.DURATION, e.CUSTOMER_CODE, e.RECURRING_CODE, e.APPOINTMENT_SHOP_CODE, e.LOCATION_TYPE_CODE, e.LOCATION_CODE, e.RESULT_CODE, e.ROOM_CODE, e.NOTE, e.TIMEBEFORE, e.TIMEAFTER, e.FLG_CONFIRMATION, e.FLG_SMS, e.FLG_EMAIL, e.APP_JOURNEY_NUMBER, e.PLAN_ID, e.PLAN_CODE, e.PLAN_APPOINTMENT_ORDER, e.DT_INSERT, e.USERINSERT, e.DT_UPDATE, e.USERUPDATE, e.ROWGUID, e.COMPANY_CODE, e.DIVISION_CODE, e.SHOP_CODE, e.EMPLOYEE_CODE, e.STATUS_CODE, e.SERVICE_CODE, e.DT_APPOINTMENT })
                    .HasName("IDX_AG_B_APPOINTMENT_DIARY_CHECK");

                entity.HasIndex(e => new { e.SHOP_CODE, e.LAPTOP_CODE, e.APPOINTMENT_ID, e.STATUS_CODE, e.REASON_CODE, e.DT_APPOINTMENT, e.DURATION, e.CUSTOMER_CODE, e.RECURRING_CODE, e.APPOINTMENT_SHOP_CODE, e.LOCATION_TYPE_CODE, e.LOCATION_CODE, e.RESULT_CODE, e.ROOM_CODE, e.NOTE, e.TIMEBEFORE, e.TIMEAFTER, e.FLG_CONFIRMATION, e.FLG_SMS, e.FLG_EMAIL, e.APP_JOURNEY_NUMBER, e.PLAN_ID, e.PLAN_CODE, e.PLAN_APPOINTMENT_ORDER, e.DT_INSERT, e.USERINSERT, e.DT_UPDATE, e.USERUPDATE, e.ROWGUID, e.COMPANY_CODE, e.DIVISION_CODE, e.EMPLOYEE_CODE, e.SERVICE_CODE })
                    .HasName("missing_index_49329");

                entity.HasIndex(e => new { e.SHOP_CODE, e.LAPTOP_CODE, e.APPOINTMENT_ID, e.STATUS_CODE, e.REASON_CODE, e.DT_APPOINTMENT, e.DURATION, e.CUSTOMER_CODE, e.RECURRING_CODE, e.APPOINTMENT_SHOP_CODE, e.LOCATION_TYPE_CODE, e.LOCATION_CODE, e.RESULT_CODE, e.ROOM_CODE, e.NOTE, e.TIMEBEFORE, e.TIMEAFTER, e.FLG_CONFIRMATION, e.FLG_SMS, e.FLG_EMAIL, e.APP_JOURNEY_NUMBER, e.PLAN_ID, e.PLAN_CODE, e.PLAN_APPOINTMENT_ORDER, e.DT_INSERT, e.USERINSERT, e.DT_UPDATE, e.USERUPDATE, e.ROWGUID, e.COMPANY_CODE, e.DIVISION_CODE, e.SERVICE_CODE, e.EMPLOYEE_CODE })
                    .HasName("missing_index_54201");

                entity.HasIndex(e => new { e.SHOP_CODE, e.LAPTOP_CODE, e.APPOINTMENT_ID, e.STATUS_CODE, e.REASON_CODE, e.DT_APPOINTMENT, e.DURATION, e.CUSTOMER_CODE, e.RECURRING_CODE, e.LOCATION_TYPE_CODE, e.LOCATION_CODE, e.RESULT_CODE, e.ROOM_CODE, e.NOTE, e.TIMEBEFORE, e.TIMEAFTER, e.FLG_CONFIRMATION, e.FLG_SMS, e.FLG_EMAIL, e.APP_JOURNEY_NUMBER, e.PLAN_ID, e.PLAN_CODE, e.PLAN_APPOINTMENT_ORDER, e.DT_INSERT, e.USERINSERT, e.DT_UPDATE, e.USERUPDATE, e.ROWGUID, e.COMPANY_CODE, e.DIVISION_CODE, e.EMPLOYEE_CODE, e.SERVICE_CODE, e.APPOINTMENT_SHOP_CODE })
                    .HasName("missing_index_52716");

                entity.HasIndex(e => new { e.SHOP_CODE, e.LAPTOP_CODE, e.APPOINTMENT_ID, e.STATUS_CODE, e.REASON_CODE, e.DT_APPOINTMENT, e.DURATION, e.CUSTOMER_CODE, e.SERVICE_CODE, e.RECURRING_CODE, e.LOCATION_TYPE_CODE, e.LOCATION_CODE, e.RESULT_CODE, e.ROOM_CODE, e.NOTE, e.TIMEBEFORE, e.TIMEAFTER, e.FLG_CONFIRMATION, e.FLG_SMS, e.FLG_EMAIL, e.APP_JOURNEY_NUMBER, e.PLAN_ID, e.PLAN_CODE, e.PLAN_APPOINTMENT_ORDER, e.DT_INSERT, e.USERINSERT, e.DT_UPDATE, e.USERUPDATE, e.ROWGUID, e.COMPANY_CODE, e.DIVISION_CODE, e.EMPLOYEE_CODE, e.APPOINTMENT_SHOP_CODE })
                    .HasName("missing_index_52718");

                entity.HasIndex(e => new { e.SHOP_CODE, e.LAPTOP_CODE, e.APPOINTMENT_ID, e.STATUS_CODE, e.REASON_CODE, e.DT_APPOINTMENT, e.DURATION, e.EMPLOYEE_CODE, e.CUSTOMER_CODE, e.RECURRING_CODE, e.APPOINTMENT_SHOP_CODE, e.LOCATION_TYPE_CODE, e.LOCATION_CODE, e.RESULT_CODE, e.ROOM_CODE, e.NOTE, e.TIMEBEFORE, e.TIMEAFTER, e.FLG_CONFIRMATION, e.FLG_SMS, e.FLG_EMAIL, e.APP_JOURNEY_NUMBER, e.PLAN_ID, e.PLAN_CODE, e.PLAN_APPOINTMENT_ORDER, e.DT_INSERT, e.USERINSERT, e.DT_UPDATE, e.USERUPDATE, e.ROWGUID, e.COMPANY_CODE, e.DIVISION_CODE, e.SERVICE_CODE })
                    .HasName("missing_index_49332");

                entity.Property(e => e.COMPANY_CODE).HasMaxLength(3);

                entity.Property(e => e.DIVISION_CODE).HasMaxLength(3);

                entity.Property(e => e.SHOP_CODE).HasMaxLength(3);

                entity.Property(e => e.LAPTOP_CODE).HasMaxLength(3);

                entity.Property(e => e.APPOINTMENT_ID).HasMaxLength(10);

                entity.Property(e => e.APPOINTMENT_SHOP_CODE).HasMaxLength(3);

                entity.Property(e => e.APP_JOURNEY_NUMBER).HasMaxLength(8);

                entity.Property(e => e.CUSTOMER_CODE).HasMaxLength(8);

                entity.Property(e => e.DT_APPOINTMENT).HasColumnType("datetime");

                entity.Property(e => e.DT_INSERT).HasColumnType("datetime");

                entity.Property(e => e.DT_UPDATE).HasColumnType("datetime");

                entity.Property(e => e.EMPLOYEE_CODE)
                    .IsRequired()
                    .HasMaxLength(8);

                entity.Property(e => e.FLG_CONFIRMATION).HasMaxLength(1);

                entity.Property(e => e.FLG_EMAIL).HasMaxLength(1);

                entity.Property(e => e.FLG_SMS).HasMaxLength(1);

                entity.Property(e => e.LOCATION_CODE).HasMaxLength(8);

                entity.Property(e => e.LOCATION_TYPE_CODE).HasMaxLength(3);

                entity.Property(e => e.NOTE).HasColumnType("nvarchar(max)");

                entity.Property(e => e.PLAN_CODE).HasMaxLength(3);

                entity.Property(e => e.PLAN_ID).HasMaxLength(10);

                entity.Property(e => e.REASON_CODE).HasMaxLength(3);

                entity.Property(e => e.RECURRING_CODE).HasMaxLength(10);

                entity.Property(e => e.RESULT_CODE).HasMaxLength(3);

                entity.Property(e => e.ROOM_CODE).HasMaxLength(10);

                entity.Property(e => e.ROWGUID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.SERVICE_CODE).HasMaxLength(8);

                entity.Property(e => e.STATUS_CODE).HasMaxLength(3);

                entity.Property(e => e.USERINSERT).HasMaxLength(50);

                entity.Property(e => e.USERUPDATE).HasMaxLength(50);

                entity.HasOne(d => d.SY_LOCATION_TYPE)
                    .WithMany(p => p.AG_B_APPOINTMENT)
                    .HasForeignKey(d => new { d.COMPANY_CODE, d.DIVISION_CODE, d.LOCATION_TYPE_CODE })
                    .HasConstraintName("FK_AG_B_APPOINTMENT_SY_LOCATION_TYPE");

                entity.HasOne(d => d.AG_S_RESULT)
                    .WithMany(p => p.AG_B_APPOINTMENT)
                    .HasForeignKey(d => new { d.COMPANY_CODE, d.DIVISION_CODE, d.RESULT_CODE })
                    .HasConstraintName("FK_AG_B_APPOINTMENT_AG_S_RESULT");

                entity.HasOne(d => d.AG_S_SERVICE)
                    .WithMany(p => p.AG_B_APPOINTMENT)
                    .HasForeignKey(d => new { d.COMPANY_CODE, d.DIVISION_CODE, d.SERVICE_CODE })
                    .HasConstraintName("FK_AG_B_APPOINTMENT_AG_S_SERVICE");

                entity.HasOne(d => d.CM_S_EMPLOYEE)
                    .WithMany(p => p.AG_B_APPOINTMENT)
                    .HasForeignKey(d => new { d.COMPANY_CODE, d.DIVISION_CODE, d.APPOINTMENT_SHOP_CODE, d.EMPLOYEE_CODE })
                    .HasConstraintName("FK_AG_B_APPOINTMENT_CM_S_EMPLOYEE");

                entity.HasOne(d => d.AG_S_RECURRING)
                    .WithMany(p => p.AG_B_APPOINTMENT)
                    .HasForeignKey(d => new { d.COMPANY_CODE, d.DIVISION_CODE, d.SHOP_CODE, d.RECURRING_CODE })
                    .HasConstraintName("FK_AG_B_APPOINTMENT_AG_S_RECURRING");

                entity.HasOne(d => d.AG_S_APPOINTMENT_PLAN_DETAIL)
                    .WithMany(p => p.AG_B_APPOINTMENT)
                    .HasForeignKey(d => new { d.COMPANY_CODE, d.DIVISION_CODE, d.SHOP_CODE, d.PLAN_CODE, d.PLAN_APPOINTMENT_ORDER })
                    .HasConstraintName("FK_AG_B_APPOINTMENT_AG_S_APPOINTMENT_PLAN_DETAIL");
            });

            modelBuilder.Entity<AG_B_APPOINTMENT_EXT_AUS>(entity =>
            {
                entity.HasKey(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.SHOP_CODE, e.LAPTOP_CODE, e.APPOINTMENT_ID })
                    .HasName("PK__AG_B_APP__69FA79359CBB13A5");

                entity.HasIndex(e => e.APPOINTMENT_ID)
                    .HasName("IX_APPOINTMENT_ID");

                entity.HasIndex(e => new { e.OUTCOME_CODE, e.APPOINTMENT_ID })
                    .HasName("IDX_AG_B_APPOINTMENT_EXT_AUS_Out");

                entity.HasIndex(e => new { e.ROWGUID, e.DT_UPDATE })
                    .HasName("IDX_AG_B_APPOINTMENT_EXT_AUS_ROWGUID");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.APPOINTMENT_ID })
                    .HasName("IDX_AG_B_APPOINTMENT_EXT_AUS_Comp_Div_AppID");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.CUSTOMER_CODE })
                    .HasName("missing_index_53073");

                entity.HasIndex(e => new { e.SHOP_CODE, e.LAPTOP_CODE, e.APPOINTMENT_ID, e.SERVICE_CODE, e.SUB_REASON_CODE, e.OUTCOME_CODE, e.OUTCOME_REASON_CODE, e.OUTCOME_SUBREASON_CODE, e.RESCHEDULED_NUMBER, e.VOUCHER_NUMBER, e.VOUCHER_TYPE, e.INTERN_CODE, e.FLG_ARRIVAL, e.OUTCOME_SUB_REASON_CODE, e.BOOKEDONBEHALF_FIRSTNAME, e.BOOKEDONBEHALF_LASTNAME, e.BOOKEDONBEHALF_RELATIONSHIP, e.BOOKEDONBEHALF_HOME_PHONE, e.BOOKEDONBEHALF_MOBILE_PHONE, e.DT_INSERT, e.USERINSERT, e.DT_UPDATE, e.USERUPDATE, e.ROWGUID, e.VALIDATION_STATUS_CODE, e.DT_UPDATE_VALIDATION_STATUS, e.COMPANY_CODE, e.DIVISION_CODE, e.CUSTOMER_CODE })
                    .HasName("missing_index_52713");

                entity.Property(e => e.COMPANY_CODE).HasMaxLength(3);

                entity.Property(e => e.DIVISION_CODE).HasMaxLength(3);

                entity.Property(e => e.SHOP_CODE).HasMaxLength(3);

                entity.Property(e => e.LAPTOP_CODE).HasMaxLength(3);

                entity.Property(e => e.APPOINTMENT_ID).HasMaxLength(10);

                entity.Property(e => e.APPOINTMENT_SOURCE).HasMaxLength(3);

                entity.Property(e => e.BOOKEDONBEHALF_FIRSTNAME).HasMaxLength(200);

                entity.Property(e => e.BOOKEDONBEHALF_HOME_PHONE).HasMaxLength(100);

                entity.Property(e => e.BOOKEDONBEHALF_LASTNAME).HasMaxLength(200);

                entity.Property(e => e.BOOKEDONBEHALF_MOBILE_PHONE).HasMaxLength(100);

                entity.Property(e => e.BOOKEDONBEHALF_RELATIONSHIP).HasMaxLength(50);

                entity.Property(e => e.CAMPAIGN_CODE).HasMaxLength(15);

                entity.Property(e => e.CUSTOMER_CODE).HasMaxLength(8);

                entity.Property(e => e.DT_CREATED).HasColumnType("datetime");

                entity.Property(e => e.DT_INSERT).HasColumnType("datetime");

                entity.Property(e => e.DT_UPDATE).HasColumnType("datetime");

                entity.Property(e => e.DT_UPDATE_VALIDATION_STATUS).HasColumnType("datetime");

                entity.Property(e => e.FLG_ARRIVAL).HasMaxLength(1);

                entity.Property(e => e.INTERN_CODE).HasMaxLength(8);

                entity.Property(e => e.IS_MCLNEEDED).HasMaxLength(1);

                entity.Property(e => e.LEADID).HasMaxLength(15);

                entity.Property(e => e.LEAVE_ROOM_CODE).HasMaxLength(10);

                entity.Property(e => e.LEAVE_SHOP_CODE).HasMaxLength(3);

                entity.Property(e => e.MEDIATYPE_CODE).HasMaxLength(3);

                entity.Property(e => e.OUTCOME_CODE).HasMaxLength(3);

                entity.Property(e => e.OUTCOME_REASON_CODE).HasMaxLength(3);

                entity.Property(e => e.OUTCOME_SUBREASON_CODE).HasMaxLength(3);

                entity.Property(e => e.OUTCOME_SUB_REASON_CODE).HasMaxLength(3);

                entity.Property(e => e.ROWGUID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.SERVICE_CODE).HasMaxLength(8);

                entity.Property(e => e.SOURCE_APPOINTMENT_ID).HasMaxLength(50);

                entity.Property(e => e.SOURCE_PROMOTER_CODE).HasMaxLength(50);

                entity.Property(e => e.SOURCE_TRACKING_ID).HasMaxLength(50);

                entity.Property(e => e.SUB_REASON_CODE).HasMaxLength(3);

                entity.Property(e => e.USERINSERT).HasMaxLength(50);

                entity.Property(e => e.USERUPDATE).HasMaxLength(50);

                entity.Property(e => e.VALIDATION_STATUS_CODE).HasMaxLength(3);

                entity.Property(e => e.VOUCHER_NUMBER).HasMaxLength(19);

                entity.Property(e => e.VOUCHER_TYPE).HasMaxLength(50);

                entity.HasOne(d => d.CU_B_ADDRESS_BOOK)
                    .WithMany(p => p.AG_B_APPOINTMENT_EXT_AUS)
                    .HasForeignKey(d => new { d.COMPANY_CODE, d.DIVISION_CODE, d.CUSTOMER_CODE })
                    .HasConstraintName("FK_CU_B_ADDRESS_BOOK_AG_B_APPOINTMENT_EXT_AUS");

                entity.HasOne(d => d.AG_S_OUTCOME_EXT_AUS)
                    .WithMany(p => p.AG_B_APPOINTMENT_EXT_AUS)
                    .HasForeignKey(d => new { d.COMPANY_CODE, d.DIVISION_CODE, d.OUTCOME_CODE })
                    .HasConstraintName("FK_AG_B_APPOINTMENT_EXT_AUS_AG_S_OUTCOME_EXT_AUS");

                entity.HasOne(d => d.AG_S_OUTCOME_REASON_EXT_AUS)
                    .WithMany(p => p.AG_B_APPOINTMENT_EXT_AUS)
                    .HasForeignKey(d => new { d.COMPANY_CODE, d.DIVISION_CODE, d.OUTCOME_REASON_CODE })
                    .HasConstraintName("FK_AG_B_APPOINTMENT_EXT_AUS_AG_S_OUTCOME_REASON_EXT_AUS");

                entity.HasOne(d => d.AG_S_SERVICE)
                    .WithMany(p => p.AG_B_APPOINTMENT_EXT_AUS)
                    .HasForeignKey(d => new { d.COMPANY_CODE, d.DIVISION_CODE, d.SERVICE_CODE })
                    .HasConstraintName("FK_AG_S_SERVICE_AG_B_APPOINTMENT_EXT_AUS");

                entity.HasOne(d => d.CM_S_EMPLOYEE)
                    .WithMany(p => p.AG_B_APPOINTMENT_EXT_AUS)
                    .HasForeignKey(d => new { d.COMPANY_CODE, d.DIVISION_CODE, d.SHOP_CODE, d.INTERN_CODE })
                    .HasConstraintName("FK_CM_S_EMPLOYEE_AG_B_APPOINTMENT_EXT_AUS");

                entity.HasOne(d => d.AG_B_APPOINTMENT)
                    .WithOne(p => p.AG_B_APPOINTMENT_EXT_AUS)
                    .HasForeignKey<AG_B_APPOINTMENT_EXT_AUS>(d => new { d.COMPANY_CODE, d.DIVISION_CODE, d.SHOP_CODE, d.LAPTOP_CODE, d.APPOINTMENT_ID })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AG_B_APPOINTMENT_EXT_AUS_AG_B_APPOINTMENT");
            });

            modelBuilder.Entity<AG_B_APPOINTMENT_STATUS_HISTORY>(entity =>
            {
                entity.HasKey(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.SHOP_CODE, e.LAPTOP_CODE, e.APPOINTMENT_ID });

                entity.HasIndex(e => e.APPOINTMENT_ID)
                    .HasName("IX_APPOINTMENT_ID");

                entity.Property(e => e.COMPANY_CODE).HasMaxLength(3);

                entity.Property(e => e.DIVISION_CODE).HasMaxLength(3);

                entity.Property(e => e.SHOP_CODE).HasMaxLength(3);

                entity.Property(e => e.LAPTOP_CODE).HasMaxLength(3);

                entity.Property(e => e.APPOINTMENT_ID).HasMaxLength(10);

                entity.Property(e => e.CANCELLED_FROM_VC).HasMaxLength(1);

                entity.Property(e => e.CANCEL_DATE).HasColumnType("datetime");

                entity.Property(e => e.CANCEL_USER).HasMaxLength(50);

                entity.Property(e => e.CONFIRMATION_STATUS).HasMaxLength(3);

                entity.Property(e => e.CONFIRMED_DATE).HasColumnType("datetime");

                entity.Property(e => e.CONFIRMED_USER).HasMaxLength(50);

                entity.Property(e => e.DT_INSERT).HasColumnType("datetime");

                entity.Property(e => e.DT_UPDATE).HasColumnType("datetime");

                entity.Property(e => e.REASON_CODE).HasMaxLength(3);

                entity.Property(e => e.USERINSERT).HasMaxLength(50);

                entity.Property(e => e.USERUPDATE).HasMaxLength(50);
            });

            modelBuilder.Entity<AG_B_EMPLOYEE_WORKING_HOURS>(entity =>
            {
                entity.HasKey(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.SHOP_CODE, e.EMPLOYEE_CODE, e.AGENDA_TIME_ID, e.LOCATION_TYPE_CODE, e.LOCATION_CODE, e.DT_VALID });

                entity.HasIndex(e => e.ROWGUID)
                    .HasName("UQ_AG_B_EMPLOYEE_WORKING_HOURS_ROWGUID")
                    .IsUnique();

                entity.HasIndex(e => e.SHOP_CODE)
                    .HasName("missing_index_52899");

                entity.HasIndex(e => new { e.EMPLOYEE_CODE, e.DT_VALID })
                    .HasName("IDX_AG_B_EMPLOYEE_WORKING_HOURS_DT_VALID");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.EMPLOYEE_CODE, e.SHOP_CODE })
                    .HasName("IDX_AG_B_EMPLOYEE_WORKING_HOURS_CM_S_EMPLOYEE");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.SHOP_CODE, e.DT_VALID })
                    .HasName("missing_index_53062");

                entity.HasIndex(e => new { e.EMPLOYEE_CODE, e.COMPANY_CODE, e.DIVISION_CODE, e.DT_VALID })
                    .HasName("IDX_AG_B_EMPLOYEE_WORKING_HOURS_001");

                entity.HasIndex(e => new { e.EMPLOYEE_CODE, e.COMPANY_CODE, e.DIVISION_CODE, e.SHOP_CODE, e.DT_VALID })
                    .HasName("missing_index_52975");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.EMPLOYEE_CODE, e.AGENDA_TIME_ID, e.LOCATION_TYPE_CODE, e.LOCATION_CODE, e.SHOP_CODE })
                    .HasName("missing_index_53125");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.SHOP_CODE, e.EMPLOYEE_CODE, e.AGENDA_TIME_ID, e.LOCATION_TYPE_CODE, e.LOCATION_CODE, e.DT_VALID, e.DT_INSERT, e.USERUPDATE, e.DT_UPDATE })
                    .HasName("missing_index_47369");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.SHOP_CODE, e.AGENDA_TIME_ID, e.LOCATION_TYPE_CODE, e.LOCATION_CODE, e.MASK_DAY, e.MASK_WEEK, e.MASK_MONTH, e.START_HOUR, e.END_HOUR, e.BACKGROUND_COLOR, e.DT_INSERT, e.USERINSERT, e.DT_UPDATE, e.USERUPDATE, e.ROWGUID, e.EMPLOYEE_CODE, e.DT_VALID })
                    .HasName("IDX_AG_B_EMPLOYEE_WORKING_HOURS_002");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.SHOP_CODE, e.EMPLOYEE_CODE, e.AGENDA_TIME_ID, e.LOCATION_TYPE_CODE, e.LOCATION_CODE, e.MASK_DAY, e.MASK_WEEK, e.MASK_MONTH, e.START_HOUR, e.END_HOUR, e.BACKGROUND_COLOR, e.DT_INSERT, e.USERINSERT, e.DT_UPDATE, e.USERUPDATE, e.ROWGUID, e.DT_VALID })
                    .HasName("missing_index_47422");

                entity.Property(e => e.COMPANY_CODE).HasMaxLength(3);

                entity.Property(e => e.DIVISION_CODE).HasMaxLength(3);

                entity.Property(e => e.SHOP_CODE).HasMaxLength(3);

                entity.Property(e => e.EMPLOYEE_CODE).HasMaxLength(8);

                entity.Property(e => e.LOCATION_TYPE_CODE).HasMaxLength(3);

                entity.Property(e => e.LOCATION_CODE).HasMaxLength(8);

                entity.Property(e => e.DT_VALID).HasColumnType("date");

                entity.Property(e => e.DT_INSERT).HasColumnType("datetime");

                entity.Property(e => e.DT_UPDATE).HasColumnType("datetime");

                entity.Property(e => e.ROWGUID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.USERINSERT).HasMaxLength(50);

                entity.Property(e => e.USERUPDATE).HasMaxLength(50);

                entity.HasOne(d => d.CM_S_EMPLOYEE)
                    .WithMany(p => p.AG_B_EMPLOYEE_WORKING_HOURS)
                    .HasForeignKey(d => new { d.COMPANY_CODE, d.DIVISION_CODE, d.SHOP_CODE, d.EMPLOYEE_CODE })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AG_B_EMPLOYEE_WORKING_HOURS_CM_S_EMPLOYEE");
            });

            modelBuilder.Entity<AG_S_APPOINTMENT_PLAN_DETAIL>(entity =>
            {
                entity.HasKey(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.SHOP_CODE, e.PLAN_CODE, e.PLAN_APPOINTMENT_ORDER });

                entity.HasIndex(e => e.ROWGUID)
                    .HasName("UQ_AG_S_APPOINTMENT_PLAN_DETAIL_ROWGUID")
                    .IsUnique();

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.SHOP_CODE, e.PLAN_CODE })
                    .HasName("IDX_AG_S_APPOINTMENT_PLAN_DETAIL_AG_S_APPOINTMENT_PLAN_HEADER");

                entity.Property(e => e.COMPANY_CODE).HasMaxLength(3);

                entity.Property(e => e.DIVISION_CODE).HasMaxLength(3);

                entity.Property(e => e.SHOP_CODE).HasMaxLength(3);

                entity.Property(e => e.PLAN_CODE).HasMaxLength(3);

                entity.Property(e => e.DT_END).HasColumnType("date");

                entity.Property(e => e.DT_INSERT).HasColumnType("datetime");

                entity.Property(e => e.DT_START).HasColumnType("date");

                entity.Property(e => e.DT_UPDATE).HasColumnType("datetime");

                entity.Property(e => e.ROWGUID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.SERVICE_CODE).HasMaxLength(8);

                entity.Property(e => e.USERINSERT).HasMaxLength(50);

                entity.Property(e => e.USERUPDATE).HasMaxLength(50);

                entity.HasOne(d => d.AG_S_APPOINTMENT_PLAN_HEADER)
                    .WithMany(p => p.AG_S_APPOINTMENT_PLAN_DETAIL)
                    .HasForeignKey(d => new { d.COMPANY_CODE, d.DIVISION_CODE, d.SHOP_CODE, d.PLAN_CODE })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AG_S_APPOINTMENT_PLAN_DETAIL_AG_S_APPOINTMENT_PLAN_HEADER");
            });

            modelBuilder.Entity<AG_S_APPOINTMENT_PLAN_HEADER>(entity =>
            {
                entity.HasKey(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.SHOP_CODE, e.PLAN_CODE });

                entity.HasIndex(e => e.ROWGUID)
                    .HasName("UQ_AG_S_APPOINTMENT_PLAN_HEADER_ROWGUID")
                    .IsUnique();

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.SHOP_CODE })
                    .HasName("IDX_AG_S_APPOINTMENT_PLAN_HEADER_CM_B_SHOP");

                entity.Property(e => e.COMPANY_CODE).HasMaxLength(3);

                entity.Property(e => e.DIVISION_CODE).HasMaxLength(3);

                entity.Property(e => e.SHOP_CODE).HasMaxLength(3);

                entity.Property(e => e.PLAN_CODE).HasMaxLength(3);

                entity.Property(e => e.DT_END).HasColumnType("date");

                entity.Property(e => e.DT_INSERT).HasColumnType("datetime");

                entity.Property(e => e.DT_START).HasColumnType("date");

                entity.Property(e => e.DT_UPDATE).HasColumnType("datetime");

                entity.Property(e => e.PLAN_DESCR).HasMaxLength(255);

                entity.Property(e => e.ROWGUID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.USERINSERT).HasMaxLength(50);

                entity.Property(e => e.USERUPDATE).HasMaxLength(50);

                entity.HasOne(d => d.CM_B_SHOP)
                    .WithMany(p => p.AG_S_APPOINTMENT_PLAN_HEADER)
                    .HasForeignKey(d => new { d.COMPANY_CODE, d.DIVISION_CODE, d.SHOP_CODE })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AG_S_APPOINTMENT_PLAN_HEADER_CM_B_SHOP");
            });

            modelBuilder.Entity<AG_S_OUTCOME_EXT_AUS>(entity =>
            {
                entity.HasKey(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.OUTCOME_CODE });

                entity.HasIndex(e => e.ROWGUID)
                    .HasName("UQ_AG_S_OUTCOME_EXT_AUS_ROWGUID")
                    .IsUnique();

                entity.Property(e => e.COMPANY_CODE).HasMaxLength(3);

                entity.Property(e => e.DIVISION_CODE).HasMaxLength(3);

                entity.Property(e => e.OUTCOME_CODE).HasMaxLength(3);

                entity.Property(e => e.DT_INSERT).HasColumnType("datetime");

                entity.Property(e => e.DT_UPDATE).HasColumnType("datetime");

                entity.Property(e => e.ENTITY_TYPE_CODE).HasMaxLength(3);

                entity.Property(e => e.ROWGUID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.USERINSERT).HasMaxLength(50);

                entity.Property(e => e.USERUPDATE).HasMaxLength(50);
            });

            modelBuilder.Entity<AG_S_OUTCOME_REASON_EXT_AUS>(entity =>
            {
                entity.HasKey(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.OUTCOME_REASON_CODE });

                entity.HasIndex(e => e.ROWGUID)
                    .HasName("UQ_AG_S_OUTCOME_REASON_EXT_AUS_ROWGUID")
                    .IsUnique();

                entity.Property(e => e.COMPANY_CODE).HasMaxLength(3);

                entity.Property(e => e.DIVISION_CODE).HasMaxLength(3);

                entity.Property(e => e.OUTCOME_REASON_CODE).HasMaxLength(3);

                entity.Property(e => e.DT_INSERT).HasColumnType("datetime");

                entity.Property(e => e.DT_UPDATE).HasColumnType("datetime");

                entity.Property(e => e.ENTITY_TYPE_CODE).HasMaxLength(3);

                entity.Property(e => e.ROWGUID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.USERINSERT).HasMaxLength(50);

                entity.Property(e => e.USERUPDATE).HasMaxLength(50);
            });

            modelBuilder.Entity<AG_S_RECURRING>(entity =>
            {
                entity.HasKey(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.SHOP_CODE, e.RECURRING_CODE });

                entity.HasIndex(e => e.ROWGUID)
                    .HasName("UQ_AG_S_RECURRING_ROWGUID")
                    .IsUnique();

                entity.Property(e => e.COMPANY_CODE).HasMaxLength(3);

                entity.Property(e => e.DIVISION_CODE).HasMaxLength(3);

                entity.Property(e => e.SHOP_CODE).HasMaxLength(3);

                entity.Property(e => e.RECURRING_CODE).HasMaxLength(10);

                entity.Property(e => e.DT_END).HasColumnType("datetime");

                entity.Property(e => e.DT_INSERT).HasColumnType("datetime");

                entity.Property(e => e.DT_START).HasColumnType("datetime");

                entity.Property(e => e.DT_UPDATE).HasColumnType("datetime");

                entity.Property(e => e.RECURRING_DESCR)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ROWGUID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.TYPE_CODE).HasMaxLength(3);

                entity.Property(e => e.USERINSERT).HasMaxLength(50);

                entity.Property(e => e.USERUPDATE).HasMaxLength(50);

                entity.Property(e => e.WEEKDAYS).HasMaxLength(7);

                entity.HasOne(d => d.AG_S_RECURRING_TYPE)
                    .WithMany(p => p.AG_S_RECURRING)
                    .HasForeignKey(d => new { d.COMPANY_CODE, d.DIVISION_CODE, d.TYPE_CODE })
                    .HasConstraintName("FK_AG_S_RECURRING_AG_S_RECURRING_TYPE");
            });

            modelBuilder.Entity<AG_S_RECURRING_TYPE>(entity =>
            {
                entity.HasKey(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.RECURRING_TYPE_CODE });

                entity.HasIndex(e => e.ROWGUID)
                    .HasName("UQ_AG_S_RECURRING_TYPE_ROWGUID")
                    .IsUnique();

                entity.Property(e => e.COMPANY_CODE).HasMaxLength(3);

                entity.Property(e => e.DIVISION_CODE).HasMaxLength(3);

                entity.Property(e => e.RECURRING_TYPE_CODE).HasMaxLength(3);

                entity.Property(e => e.DT_END).HasColumnType("date");

                entity.Property(e => e.DT_INSERT).HasColumnType("datetime");

                entity.Property(e => e.DT_START).HasColumnType("date");

                entity.Property(e => e.DT_UPDATE).HasColumnType("datetime");

                entity.Property(e => e.RECURRING_TYPE_DESCR).HasMaxLength(255);

                entity.Property(e => e.ROWGUID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.USERINSERT).HasMaxLength(50);

                entity.Property(e => e.USERUPDATE).HasMaxLength(50);
            });

            modelBuilder.Entity<AG_S_RESULT>(entity =>
            {
                entity.HasKey(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.RESULT_CODE });

                entity.HasIndex(e => e.ROWGUID)
                    .HasName("UQ_AG_S_RESULT_ROWGUID")
                    .IsUnique();

                entity.Property(e => e.COMPANY_CODE).HasMaxLength(3);

                entity.Property(e => e.DIVISION_CODE).HasMaxLength(3);

                entity.Property(e => e.RESULT_CODE).HasMaxLength(3);

                entity.Property(e => e.DT_END).HasColumnType("date");

                entity.Property(e => e.DT_INSERT).HasColumnType("datetime");

                entity.Property(e => e.DT_START).HasColumnType("date");

                entity.Property(e => e.DT_UPDATE).HasColumnType("datetime");

                entity.Property(e => e.FLG_NOAHTEST).HasMaxLength(1);

                entity.Property(e => e.RESULT_DESCR).HasMaxLength(255);

                entity.Property(e => e.ROWGUID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.USERINSERT).HasMaxLength(50);

                entity.Property(e => e.USERUPDATE).HasMaxLength(50);
            });

            modelBuilder.Entity<AG_S_ROOM>(entity =>
            {
                entity.HasKey(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.SHOP_CODE, e.ROOM_CODE });

                entity.HasIndex(e => e.ROWGUID)
                    .HasName("UQ_AG_S_ROOM_ROWGUID")
                    .IsUnique();

                entity.HasIndex(e => e.SHOP_CODE)
                    .HasName("missing_index_40404");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.ROOM_CODE })
                    .HasName("missing_index_9335");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.ROOM_TYPE_CODE })
                    .HasName("IDX_AG_S_ROOM_AG_S_ROOM_TYPE");

                entity.Property(e => e.COMPANY_CODE).HasMaxLength(3);

                entity.Property(e => e.DIVISION_CODE).HasMaxLength(3);

                entity.Property(e => e.SHOP_CODE).HasMaxLength(3);

                entity.Property(e => e.ROOM_CODE).HasMaxLength(10);

                entity.Property(e => e.DT_END).HasColumnType("date");

                entity.Property(e => e.DT_INSERT).HasColumnType("datetime");

                entity.Property(e => e.DT_START).HasColumnType("date");

                entity.Property(e => e.DT_UPDATE).HasColumnType("datetime");

                entity.Property(e => e.ROOM_DESCR).HasMaxLength(255);

                entity.Property(e => e.ROOM_TYPE_CODE).HasMaxLength(3);

                entity.Property(e => e.ROWGUID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.USERINSERT).HasMaxLength(50);

                entity.Property(e => e.USERUPDATE).HasMaxLength(50);
            });

            modelBuilder.Entity<AG_S_SERVICE>(entity =>
            {
                entity.HasKey(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.SERVICE_CODE });

                entity.HasIndex(e => e.ROWGUID)
                    .HasName("UQ_AG_S_SERVICE_ROWGUID")
                    .IsUnique();

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.SERVICE_TYPE_CODE })
                    .HasName("IDX_AG_S_SERVICE_AG_S_SERVICE_TYPE");

                entity.Property(e => e.COMPANY_CODE).HasMaxLength(3);

                entity.Property(e => e.DIVISION_CODE).HasMaxLength(3);

                entity.Property(e => e.SERVICE_CODE).HasMaxLength(8);

                entity.Property(e => e.DT_END).HasColumnType("date");

                entity.Property(e => e.DT_INSERT).HasColumnType("datetime");

                entity.Property(e => e.DT_START).HasColumnType("date");

                entity.Property(e => e.DT_UPDATE).HasColumnType("datetime");

                entity.Property(e => e.FLG_NEWJOURNEY).HasMaxLength(1);

                entity.Property(e => e.ROWGUID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.SERVICE_DESCR).HasMaxLength(255);

                entity.Property(e => e.SERVICE_TYPE_CODE).HasMaxLength(3);

                entity.Property(e => e.SHOP_CODE)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.USERINSERT).HasMaxLength(50);

                entity.Property(e => e.USERUPDATE).HasMaxLength(50);

                entity.HasOne(d => d.AG_S_SERVICE_TYPE)
                    .WithMany(p => p.AG_S_SERVICE)
                    .HasForeignKey(d => new { d.COMPANY_CODE, d.DIVISION_CODE, d.SERVICE_TYPE_CODE })
                    .HasConstraintName("FK_AG_S_SERVICE_AG_S_SERVICE_TYPE");
            });

            modelBuilder.Entity<AG_S_SERVICE_TYPE>(entity =>
            {
                entity.HasKey(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.SERVICE_TYPE_CODE });

                entity.HasIndex(e => e.ROWGUID)
                    .HasName("UQ_AG_S_SERVICE_TYPE_ROWGUID")
                    .IsUnique();

                entity.Property(e => e.COMPANY_CODE).HasMaxLength(3);

                entity.Property(e => e.DIVISION_CODE).HasMaxLength(3);

                entity.Property(e => e.SERVICE_TYPE_CODE).HasMaxLength(3);

                entity.Property(e => e.DT_END).HasColumnType("date");

                entity.Property(e => e.DT_INSERT).HasColumnType("datetime");

                entity.Property(e => e.DT_START).HasColumnType("date");

                entity.Property(e => e.DT_UPDATE).HasColumnType("datetime");

                entity.Property(e => e.FLG_ALLOW_APPOINTMENT).HasMaxLength(1);

                entity.Property(e => e.ROWGUID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.SERVICE_TYPE_DESCR).HasMaxLength(255);

                entity.Property(e => e.USERINSERT).HasMaxLength(50);

                entity.Property(e => e.USERUPDATE).HasMaxLength(50);
            });

            modelBuilder.Entity<CM_B_SHOP>(entity =>
            {
                entity.HasKey(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.SHOP_CODE })
                    .HasName("PK_CM_SHOP");

                entity.HasIndex(e => e.ROWGUID)
                    .HasName("UQ_CM_B_SHOP_ROWGUID")
                    .IsUnique();

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.ORGANIZATION_CODE })
                    .HasName("IDX_CM_B_SHOP_CM_S_ORGANIZATION");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.SHOP_TYPE_CODE })
                    .HasName("IDX_CM_B_SHOP_SY_SHOP_TYPE");

                entity.Property(e => e.COMPANY_CODE).HasMaxLength(3);

                entity.Property(e => e.DIVISION_CODE).HasMaxLength(3);

                entity.Property(e => e.SHOP_CODE).HasMaxLength(3);

                entity.Property(e => e.DT_END).HasColumnType("date");

                entity.Property(e => e.DT_INSERT).HasColumnType("datetime");

                entity.Property(e => e.DT_START).HasColumnType("date");

                entity.Property(e => e.DT_UPDATE).HasColumnType("datetime");

                entity.Property(e => e.FLG_ACTIVE).HasMaxLength(1);

                entity.Property(e => e.LEGAL_DESCR).HasMaxLength(255);

                entity.Property(e => e.OBJ_CODE).HasMaxLength(8);

                entity.Property(e => e.ORGANIZATION_CODE).HasMaxLength(3);

                entity.Property(e => e.ROWGUID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.SHOP_DESCR).HasMaxLength(255);

                entity.Property(e => e.SHOP_TYPE_CODE).HasMaxLength(3);

                entity.Property(e => e.USERINSERT).HasMaxLength(50);

                entity.Property(e => e.USERUPDATE).HasMaxLength(50);
            });

            modelBuilder.Entity<CM_S_AREA_BOOK>(entity =>
            {
                entity.HasKey(e => new { e.COUNTRY_CODE, e.AREA_CODE })
                    .HasName("PK_CM_AREA_BOOK");

                entity.HasIndex(e => e.ROWGUID)
                    .HasName("UQ_CM_S_AREA_BOOK_ROWGUID")
                    .IsUnique();

                entity.Property(e => e.COUNTRY_CODE).HasMaxLength(3);

                entity.Property(e => e.AREA_CODE).HasMaxLength(3);

                entity.Property(e => e.AREA_DESCR).HasMaxLength(255);

                entity.Property(e => e.DT_END).HasColumnType("date");

                entity.Property(e => e.DT_INSERT).HasColumnType("datetime");

                entity.Property(e => e.DT_START).HasColumnType("date");

                entity.Property(e => e.DT_UPDATE).HasColumnType("datetime");

                entity.Property(e => e.REGION_CODE).HasMaxLength(2);

                entity.Property(e => e.ROWGUID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.USERINSERT).HasMaxLength(50);

                entity.Property(e => e.USERUPDATE).HasMaxLength(50);

                entity.HasOne(d => d.CM_S_REGION_BOOK)
                    .WithMany(p => p.CM_S_AREA_BOOK)
                    .HasForeignKey(d => new { d.COUNTRY_CODE, d.REGION_CODE })
                    .HasConstraintName("FK_CM_S_AREACODE_BOOK_CM_S_REGION_BOOK");
            });

            modelBuilder.Entity<CM_S_CAMPAIGN>(entity =>
            {
                entity.HasKey(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.CAMPAIGN_CODE });

                entity.HasIndex(e => e.ROWGUID)
                    .HasName("UQ_CM_S_CAMPAIGN_ROWGUID")
                    .IsUnique();

                entity.HasIndex(e => new { e.CAMPAIGN_DESCR, e.CAMPAIGN_CODE })
                    .HasName("missing_index_10331");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.CAMPAIGN_TYPE_CODE })
                    .HasName("IDX_CM_S_CAMPAIGN_CM_S_CAMPAIGN_TYPE");

                entity.Property(e => e.COMPANY_CODE).HasMaxLength(3);

                entity.Property(e => e.DIVISION_CODE).HasMaxLength(3);

                entity.Property(e => e.CAMPAIGN_CODE).HasMaxLength(15);

                entity.Property(e => e.CAMPAIGN_DESCR).HasMaxLength(255);

                entity.Property(e => e.CAMPAIGN_TYPE_CODE).HasMaxLength(3);

                entity.Property(e => e.DT_END).HasColumnType("date");

                entity.Property(e => e.DT_INSERT).HasColumnType("datetime");

                entity.Property(e => e.DT_START).HasColumnType("date");

                entity.Property(e => e.DT_UPDATE).HasColumnType("datetime");

                entity.Property(e => e.DT_UPDATE_STATUS).HasColumnType("datetime");

                entity.Property(e => e.ROWGUID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.STATUS_CODE).HasMaxLength(3);

                entity.Property(e => e.USERINSERT).HasMaxLength(50);

                entity.Property(e => e.USERUPDATE).HasMaxLength(50);
            });

            modelBuilder.Entity<CM_S_CITY_BOOK_SHOP>(entity =>
            {
                entity.HasKey(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.COUNTRY_CODE, e.AREA_CODE, e.CITY_COUNTER, e.ZIP_CODE, e.SHOP_CODE, e.DT_START, e.DT_END });

                entity.HasIndex(e => e.ROWGUID)
                    .HasName("UQ_CM_S_CITY_BOOK_SHOP_ROWGUID")
                    .IsUnique();

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.SHOP_CODE })
                    .HasName("IDX_CM_S_CITY_BOOK_SHOP_CM_B_SHOP");

                entity.HasIndex(e => new { e.COUNTRY_CODE, e.AREA_CODE, e.ZIP_CODE, e.CITY_COUNTER })
                    .HasName("IDX_CM_S_CITY_BOOK_SHOP_CM_S_CITY_BOOK");

                entity.Property(e => e.COMPANY_CODE).HasMaxLength(3);

                entity.Property(e => e.DIVISION_CODE).HasMaxLength(3);

                entity.Property(e => e.COUNTRY_CODE).HasMaxLength(3);

                entity.Property(e => e.AREA_CODE).HasMaxLength(3);

                entity.Property(e => e.ZIP_CODE).HasMaxLength(15);

                entity.Property(e => e.SHOP_CODE).HasMaxLength(3);

                entity.Property(e => e.DT_START).HasColumnType("date");

                entity.Property(e => e.DT_END).HasColumnType("date");

                entity.Property(e => e.DT_INSERT).HasColumnType("datetime");

                entity.Property(e => e.DT_UPDATE).HasColumnType("datetime");

                entity.Property(e => e.FLG_MAIN).HasMaxLength(1);

                entity.Property(e => e.FLG_PREFERRED).HasMaxLength(1);

                entity.Property(e => e.ROWGUID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.USERINSERT).HasMaxLength(50);

                entity.Property(e => e.USERUPDATE).HasMaxLength(50);
            });

            modelBuilder.Entity<CM_S_DAYCENTER>(entity =>
            {
                entity.HasKey(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.DAYCENTER_CODE, e.SHOP_CODE });

                entity.HasIndex(e => e.DAYCENTER_DESCR)
                    .HasName("missing_index_11058");

                entity.HasIndex(e => e.ROWGUID)
                    .HasName("UQ_CM_S_DAYCENTER_ROWGUID")
                    .IsUnique();

                entity.HasIndex(e => new { e.DAYCENTER_DESCR, e.DAYCENTER_CODE })
                    .HasName("missing_index_16109");

                entity.HasIndex(e => new { e.DAYCENTER_CODE, e.COMPANY_CODE, e.DIVISION_CODE, e.SHOP_CODE })
                    .HasName("missing_index_2425");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.DAYCENTER_CODE, e.SHOP_CODE, e.DT_INSERT, e.USERUPDATE, e.DT_UPDATE })
                    .HasName("missing_index_183");

                entity.Property(e => e.COMPANY_CODE).HasMaxLength(3);

                entity.Property(e => e.DIVISION_CODE).HasMaxLength(3);

                entity.Property(e => e.DAYCENTER_CODE).HasMaxLength(8);

                entity.Property(e => e.SHOP_CODE).HasMaxLength(3);

                entity.Property(e => e.ACCOUNTING_ID).HasMaxLength(8);

                entity.Property(e => e.DAYCENTER_DESCR).HasMaxLength(255);

                entity.Property(e => e.DT_END).HasColumnType("date");

                entity.Property(e => e.DT_INSERT).HasColumnType("datetime");

                entity.Property(e => e.DT_START).HasColumnType("date");

                entity.Property(e => e.DT_UPDATE).HasColumnType("datetime");

                entity.Property(e => e.MARKETING_ID).HasMaxLength(8);

                entity.Property(e => e.ROWGUID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.USERINSERT).HasMaxLength(50);

                entity.Property(e => e.USERUPDATE).HasMaxLength(50);
            });

            modelBuilder.Entity<CM_S_DAYCENTER_EXT_AUS>(entity =>
            {
                entity.HasKey(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.DAYCENTER_CODE, e.SHOP_CODE });

                entity.HasIndex(e => new { e.LOCATION_TYPE, e.DAYCENTER_CODE })
                    .HasName("missing_index_16107");

                entity.Property(e => e.COMPANY_CODE).HasMaxLength(3);

                entity.Property(e => e.DIVISION_CODE).HasMaxLength(3);

                entity.Property(e => e.DAYCENTER_CODE).HasMaxLength(8);

                entity.Property(e => e.SHOP_CODE).HasMaxLength(3);

                entity.Property(e => e.DT_INSERT).HasColumnType("datetime");

                entity.Property(e => e.DT_UPDATE).HasColumnType("datetime");

                entity.Property(e => e.LOCATION_TYPE).HasMaxLength(3);

                entity.Property(e => e.OHS_SITE_ID).HasMaxLength(20);

                entity.Property(e => e.USERINSERT)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.USERUPDATE)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CM_S_EMPLOYEE>(entity =>
            {
                entity.HasKey(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.SHOP_CODE, e.EMPLOYEE_CODE });

                entity.HasIndex(e => e.ROWGUID)
                    .HasName("UQ_SY_EMPLOYEE_ROWGUID")
                    .IsUnique();

                entity.HasIndex(e => e.SHOP_CODE)
                    .HasName("missing_index_25008");

                entity.HasIndex(e => new { e.EMPLOYEE_CODE, e.EMPLOYEE_TYPE_CODE })
                    .HasName("missing_index_39947");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.EMPLOYEE_DESCR })
                    .HasName("missing_index_18085");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.EMPLOYEE_TYPE_CODE })
                    .HasName("IDX_CM_S_EMPLOYEE_SY_EMPLOYEE_TYPE");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.SHOP_CODE, e.EMPLOYEE_TYPE_CODE })
                    .HasName("missing_index_25194");

                entity.HasIndex(e => new { e.EMPLOYEE_CODE, e.DT_START, e.DT_END, e.EMPLOYEE_TYPE_CODE })
                    .HasName("missing_index_18113");

                entity.HasIndex(e => new { e.SHOP_CODE, e.FIRSTNAME, e.LASTNAME, e.EMPLOYEE_CODE })
                    .HasName("IDX_CM_S_EMPLOYEE_001");

                entity.HasIndex(e => new { e.EMPLOYEE_CODE, e.COMPANY_CODE, e.DIVISION_CODE, e.EMPLOYEE_TYPE_CODE, e.DT_START, e.DT_END })
                    .HasName("IDX_GET_EMPLOYEE_WH");

                entity.Property(e => e.COMPANY_CODE).HasMaxLength(3);

                entity.Property(e => e.DIVISION_CODE).HasMaxLength(3);

                entity.Property(e => e.SHOP_CODE).HasMaxLength(3);

                entity.Property(e => e.EMPLOYEE_CODE).HasMaxLength(8);

                entity.Property(e => e.DT_END).HasColumnType("date");

                entity.Property(e => e.DT_INSERT).HasColumnType("datetime");

                entity.Property(e => e.DT_START).HasColumnType("date");

                entity.Property(e => e.DT_UPDATE).HasColumnType("datetime");

                entity.Property(e => e.EMPLOYEE_DESCR).HasMaxLength(255);

                entity.Property(e => e.EMPLOYEE_ID).HasMaxLength(10);

                entity.Property(e => e.EMPLOYEE_TYPE_CODE).HasMaxLength(3);

                entity.Property(e => e.FIRSTNAME).HasMaxLength(50);

                entity.Property(e => e.LASTNAME).HasMaxLength(50);

                entity.Property(e => e.ROWGUID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.USERINSERT).HasMaxLength(50);

                entity.Property(e => e.USERUPDATE).HasMaxLength(50);

                entity.HasOne(d => d.SY_EMPLOYEE_TYPE)
                    .WithMany(p => p.CM_S_EMPLOYEE)
                    .HasForeignKey(d => new { d.COMPANY_CODE, d.DIVISION_CODE, d.EMPLOYEE_TYPE_CODE })
                    .HasConstraintName("FK_CM_S_EMPLOYEE_SY_EMPLOYEE_TYPE");
            });

            modelBuilder.Entity<CM_S_MEDIATYPE>(entity =>
            {
                entity.HasKey(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.MEDIATYPE_CODE });

                entity.HasIndex(e => e.ROWGUID)
                    .HasName("UQ_CM_S_MEDIATYPE_ROWGUID")
                    .IsUnique();

                entity.Property(e => e.COMPANY_CODE).HasMaxLength(3);

                entity.Property(e => e.DIVISION_CODE).HasMaxLength(3);

                entity.Property(e => e.MEDIATYPE_CODE).HasMaxLength(3);

                entity.Property(e => e.DT_END).HasColumnType("date");

                entity.Property(e => e.DT_INSERT).HasColumnType("datetime");

                entity.Property(e => e.DT_START).HasColumnType("date");

                entity.Property(e => e.DT_UPDATE).HasColumnType("datetime");

                entity.Property(e => e.MEDIATYPE_DESCR).HasMaxLength(255);

                entity.Property(e => e.ROWGUID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.USERINSERT).HasMaxLength(50);

                entity.Property(e => e.USERUPDATE).HasMaxLength(50);
            });

            modelBuilder.Entity<CM_S_REFERENCE_SOURCE_EXT_AUS>(entity =>
            {
                entity.HasKey(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.CODE, e.TYPE_CATEGORY_CODE });

                entity.Property(e => e.COMPANY_CODE).HasMaxLength(3);

                entity.Property(e => e.DIVISION_CODE).HasMaxLength(3);

                entity.Property(e => e.CODE).HasMaxLength(9);

                entity.Property(e => e.TYPE_CATEGORY_CODE).HasMaxLength(3);

                entity.Property(e => e.CUSTOMER_TYPE_CODE).HasMaxLength(3);

                entity.Property(e => e.DESCRIPTION).HasMaxLength(50);

                entity.Property(e => e.DT_INSERT).HasColumnType("datetime");

                entity.Property(e => e.DT_UPDATE).HasColumnType("datetime");

                entity.Property(e => e.REF_CODE).HasMaxLength(9);

                entity.Property(e => e.REF_TYPE_CATEGORY_CODE).HasMaxLength(3);

                entity.Property(e => e.USERINSERT).HasMaxLength(25);

                entity.Property(e => e.USERUPDATE).HasMaxLength(25);

                entity.HasOne(d => d.CM_S_REFERENCE_SOURCE_EXT_AUSNavigation)
                    .WithMany(p => p.InverseCM_S_REFERENCE_SOURCE_EXT_AUSNavigation)
                    .HasForeignKey(d => new { d.COMPANY_CODE, d.DIVISION_CODE, d.REF_CODE, d.REF_TYPE_CATEGORY_CODE })
                    .HasConstraintName("FK_CM_S_REFERENCE_SOURCE_EXT_AUS_CM_S_REFERENCE_SOURCE_EXT_AUS_REF");
            });

            modelBuilder.Entity<CM_S_REGION_BOOK>(entity =>
            {
                entity.HasKey(e => new { e.COUNTRY_CODE, e.REGION_CODE })
                    .HasName("PK_CM_REGION_BOOK");

                entity.HasIndex(e => e.ROWGUID)
                    .HasName("UQ_CM_S_REGION_BOOK_ROWGUID")
                    .IsUnique();

                entity.Property(e => e.COUNTRY_CODE).HasMaxLength(3);

                entity.Property(e => e.REGION_CODE).HasMaxLength(2);

                entity.Property(e => e.DT_END).HasColumnType("date");

                entity.Property(e => e.DT_INSERT).HasColumnType("datetime");

                entity.Property(e => e.DT_START).HasColumnType("date");

                entity.Property(e => e.DT_UPDATE).HasColumnType("datetime");

                entity.Property(e => e.REGION_DESCR).HasMaxLength(255);

                entity.Property(e => e.ROWGUID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.USERINSERT).HasMaxLength(50);

                entity.Property(e => e.USERUPDATE).HasMaxLength(50);
            });

            modelBuilder.Entity<CU_B_ACTIVITY>(entity =>
            {
                entity.HasKey(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.SHOP_CODE, e.LAPTOP_CODE, e.CUSTOMER_CODE, e.ACTIVITY_ID, e.ACTIVITY_DATE });

                entity.HasIndex(e => e.ACTIVITY_TYPE_CODE)
                    .HasName("IX_ACTIVITY_TYPE_CODE");

                entity.HasIndex(e => e.APPOINTMENT_ID)
                    .HasName("#IDX_APPOINTMENTID_CU_B_ACTIVITY");

                entity.HasIndex(e => e.ROWGUID)
                    .HasName("UQ_CU_B_ACTIVITY_ROWGUID")
                    .IsUnique();

                entity.HasIndex(e => e.activity_rv)
                    .HasName("idx_activity_rv")
                    .IsUnique();

                entity.HasIndex(e => new { e.REFERENCE_DATE, e.REFERENCE_NUMBER })
                    .HasName("IX_REF_NAME_REF_DATE");

                entity.HasIndex(e => new { e.ACTIVITY_TYPE_CODE, e.USERINSERT, e.USERUPDATE })
                    .HasName("missing_index_47679");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.ACTIVITY_TYPE_CODE })
                    .HasName("IDX_CU_B_ACTIVITY_SY_ACTIVITY_TYPE");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.CAMPAIGN_CODE })
                    .HasName("IDX_CU_B_ACTIVITY_CM_S_CAMPAIGN");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.CUSTOMER_CODE })
                    .HasName("IDX_CU_B_ACTIVITY_CU_B_ADDRESS_BOOK");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.RECOMMENDEDBY_CODE })
                    .HasName("IDX_CU_B_ACTIVITY_CU_B_ADDRESS_BOOK_RECOMMENDED");

                entity.HasIndex(e => new { e.ACTIVITY_TYPE_CODE, e.DT_UPDATE, e.USERINSERT, e.USERUPDATE })
                    .HasName("missing_index_47681");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.REFERENCE_NUMBER, e.ACTIVITY_TYPE_CODE })
                    .HasName("missing_index_47537");

                entity.HasIndex(e => new { e.SHOP_CODE, e.LAPTOP_CODE, e.ACTIVITY_ID, e.ACTIVITY_DATE, e.CUSTOMER_CODE, e.ACTIVITY_TYPE_CODE, e.EMPLOYEE_CODE, e.CAMPAIGN_CODE, e.PROMOTER_CODE, e.PHYSICIAN_MG_CODE, e.PHYSICIAN_ORL_CODE, e.LOCATION_TYPE_CODE, e.LOCATION_CODE, e.MEDIATYPE_CODE, e.NOTE, e.REFERENCE_DATE, e.TESTRESULT_LEFT, e.TESTRESULT_RIGHT, e.APPOINTMENT_ID, e.RESULT_CODE, e.REASON_CODE, e.DT_EXAM, e.DT_INSERT, e.USERINSERT, e.DT_UPDATE, e.USERUPDATE, e.ROWGUID, e.COMPANY_CODE, e.DIVISION_CODE, e.REFERENCE_NUMBER })
                    .HasName("IDX_CU_B_ACTIVITY_GetListByAppointmentId");

                entity.Property(e => e.COMPANY_CODE).HasMaxLength(3);

                entity.Property(e => e.DIVISION_CODE).HasMaxLength(3);

                entity.Property(e => e.SHOP_CODE).HasMaxLength(3);

                entity.Property(e => e.LAPTOP_CODE).HasMaxLength(3);

                entity.Property(e => e.CUSTOMER_CODE).HasMaxLength(8);

                entity.Property(e => e.ACTIVITY_DATE).HasColumnType("date");

                entity.Property(e => e.ACTIVITY_TYPE_CODE)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.APPOINTMENT_ID).HasMaxLength(10);

                entity.Property(e => e.CAMPAIGN_CODE).HasMaxLength(15);

                entity.Property(e => e.DAYCENTER_CODE).HasMaxLength(8);

                entity.Property(e => e.DT_EXAM).HasColumnType("date");

                entity.Property(e => e.DT_INSERT).HasColumnType("datetime");

                entity.Property(e => e.DT_UPDATE).HasColumnType("datetime");

                entity.Property(e => e.EMPLOYEE_CODE).HasMaxLength(8);

                entity.Property(e => e.LOCATION_CODE).HasMaxLength(8);

                entity.Property(e => e.LOCATION_TYPE_CODE).HasMaxLength(3);

                entity.Property(e => e.MEDIATYPE_CODE).HasMaxLength(3);

                entity.Property(e => e.NOTE).HasColumnType("nvarchar(max)");

                entity.Property(e => e.PHYSICIAN_MG_CODE).HasMaxLength(8);

                entity.Property(e => e.PHYSICIAN_ORL_CODE).HasMaxLength(8);

                entity.Property(e => e.PROMOTER_CODE).HasMaxLength(6);

                entity.Property(e => e.REASON_CODE).HasMaxLength(3);

                entity.Property(e => e.RECOMMENDEDBY_CODE).HasMaxLength(8);

                entity.Property(e => e.REFERENCE_DATE).HasColumnType("date");

                entity.Property(e => e.REFERENCE_NUMBER).HasMaxLength(15);

                entity.Property(e => e.RESULT_CODE).HasMaxLength(3);

                entity.Property(e => e.TESTRESULT_LEFT).HasMaxLength(2);

                entity.Property(e => e.TESTRESULT_RIGHT).HasMaxLength(2);

                entity.Property(e => e.USERINSERT).HasMaxLength(50);

                entity.Property(e => e.USERUPDATE).HasMaxLength(50);

                entity.Property(e => e.activity_rv)
                    .IsRequired()
                    .IsRowVersion();

                entity.HasOne(d => d.CM_S_CAMPAIGN)
                    .WithMany(p => p.CU_B_ACTIVITY)
                    .HasForeignKey(d => new { d.COMPANY_CODE, d.DIVISION_CODE, d.CAMPAIGN_CODE })
                    .HasConstraintName("FK_CU_B_ACTIVITY_CM_S_CAMPAIGN");

                entity.HasOne(d => d.CU_B_ADDRESS_BOOK)
                    .WithMany(p => p.CU_B_ACTIVITYCU_B_ADDRESS_BOOK)
                    .HasForeignKey(d => new { d.COMPANY_CODE, d.DIVISION_CODE, d.CUSTOMER_CODE })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CU_B_ACTIVITY_CU_B_ADDRESS_BOOK");

                entity.HasOne(d => d.SY_LOCATION_TYPE)
                    .WithMany(p => p.CU_B_ACTIVITY)
                    .HasForeignKey(d => new { d.COMPANY_CODE, d.DIVISION_CODE, d.LOCATION_TYPE_CODE })
                    .HasConstraintName("FK_CU_B_ACTIVITY_SY_LOCATION_TYPE");

                entity.HasOne(d => d.CM_S_MEDIATYPE)
                    .WithMany(p => p.CU_B_ACTIVITY)
                    .HasForeignKey(d => new { d.COMPANY_CODE, d.DIVISION_CODE, d.MEDIATYPE_CODE })
                    .HasConstraintName("FK_CU_B_ACTIVITY_CM_S_MEDIATYPE");

                entity.HasOne(d => d.CU_B_ADDRESS_BOOKNavigation)
                    .WithMany(p => p.CU_B_ACTIVITYCU_B_ADDRESS_BOOKNavigation)
                    .HasForeignKey(d => new { d.COMPANY_CODE, d.DIVISION_CODE, d.RECOMMENDEDBY_CODE })
                    .HasConstraintName("FK_CU_B_ACTIVITY_CU_B_ADDRESS_BOOK_RECOMMENDED");
            });

            modelBuilder.Entity<CU_B_ACTIVITY_EXT_AUS>(entity =>
            {
                entity.HasKey(e => new { e.ACTIVITY_DATE, e.ACTIVITY_ID, e.COMPANY_CODE, e.CUSTOMER_CODE, e.DIVISION_CODE, e.LAPTOP_CODE, e.SHOP_CODE })
                    .HasName("PK__CU_B_ACT__E8C26A5EF3D359E6");

                entity.HasIndex(e => e.ROWGUID)
                    .HasName("missing_index_39328");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.CUSTOMER_CODE })
                    .HasName("missing_index_39331");

                entity.HasIndex(e => new { e.ACTIVITY_ID, e.DT_APPOINTEMENT_FROM, e.DT_APPOINTEMENT_TO, e.COMPANY_CODE, e.DIVISION_CODE })
                    .HasName("IDX_CU_B_ACTIVITY_EXT_AUS_COMPANY_CODE_DIVISION_CODE");

                entity.HasIndex(e => new { e.DT_APPOINTEMENT_FROM, e.DT_APPOINTEMENT_TO, e.COMPANY_CODE, e.DIVISION_CODE, e.ACTIVITY_ID })
                    .HasName("IX_CU_B_ACTIVITY_EXT_AUS_Comp_Div_Act_Id");

                entity.Property(e => e.ACTIVITY_DATE).HasColumnType("date");

                entity.Property(e => e.COMPANY_CODE).HasMaxLength(3);

                entity.Property(e => e.CUSTOMER_CODE).HasMaxLength(8);

                entity.Property(e => e.DIVISION_CODE).HasMaxLength(3);

                entity.Property(e => e.LAPTOP_CODE).HasMaxLength(3);

                entity.Property(e => e.SHOP_CODE).HasMaxLength(3);

                entity.Property(e => e.DT_APPOINTEMENT_FROM).HasColumnType("datetime");

                entity.Property(e => e.DT_APPOINTEMENT_TO).HasColumnType("datetime");

                entity.Property(e => e.DT_INSERT).HasColumnType("datetime");

                entity.Property(e => e.DT_UPDATE).HasColumnType("datetime");

                entity.Property(e => e.SUB_REASON_CODE).HasMaxLength(3);

                entity.Property(e => e.USERINSERT).HasMaxLength(50);

                entity.Property(e => e.USERUPDATE).HasMaxLength(50);

                entity.HasOne(d => d.CU_B_ADDRESS_BOOK)
                    .WithMany(p => p.CU_B_ACTIVITY_EXT_AUS)
                    .HasForeignKey(d => new { d.COMPANY_CODE, d.DIVISION_CODE, d.CUSTOMER_CODE })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CU_B_ACTIVITY_EXT_AUS_CU_B_ADDRESS_BOOK");

                entity.HasOne(d => d.CM_B_SHOP)
                    .WithMany(p => p.CU_B_ACTIVITY_EXT_AUS)
                    .HasForeignKey(d => new { d.COMPANY_CODE, d.DIVISION_CODE, d.SHOP_CODE })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CU_B_ACTIVITY_EXT_AUS_CM_B_SHOP");
            });

            modelBuilder.Entity<CU_B_ADDRESS_BOOK>(entity =>
            {
                entity.HasKey(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.CUSTOMER_CODE });

                entity.HasIndex(e => e.BIRTHDATE)
                    .HasName("IX_CU_B_ADDRESS_BOOK_BIRTDATE");

                entity.HasIndex(e => e.CUSTOMER_CODE)
                    .HasName("IDX_CU_B_ADDRESS_BOOK_CU_S_CUSTOMER_CODE");

                entity.HasIndex(e => e.CUSTOMER_ID)
                    .HasName("IX_CUSTOMER_ID");

                entity.HasIndex(e => e.ROWGUID)
                    .HasName("UQ_CU_B_ADDRESS_BOOK_ROWGUID")
                    .IsUnique();

                entity.HasIndex(e => new { e.LASTNAME, e.FIRSTNAME })
                    .HasName("IX_LASTNAME_FIRSTNAME");

                entity.HasIndex(e => new { e.CATEGORY_CODE, e.LASTNAME, e.FIRSTNAME })
                    .HasName("IX_CU_B_ADDRESS_BOOK_FIRSTNAMELASTNAME");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.CATEGORY_CODE })
                    .HasName("IDX_CU_B_ADDRESS_BOOK_CU_S_CATEGORY");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.CUSTOMER_TYPE_CODE })
                    .HasName("IDX_CU_B_ADDRESS_BOOK_CU_S_TYPE");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.GENDER_CODE })
                    .HasName("IDX_CU_B_ADDRESS_BOOK_SY_GENDER");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.SALUTATION_CODE })
                    .HasName("IDX_CU_B_ADDRESS_BOOK_CU_S_SALUTATION");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.STATUS_CODE })
                    .HasName("IDX_CU_B_ADDRESS_BOOK_CU_S_STATUS");

                entity.HasIndex(e => new { e.CUSTOMER_CODE, e.LASTNAME, e.CUSTOMER_ID, e.COMPANY_CODE, e.DIVISION_CODE, e.CATEGORY_CODE })
                    .HasName("IX_CU_B_ADDRESS_BOOK_COMPANY_CODE_DIVISION_CODE_CATEGORY_CODE");

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.CUSTOMER_CODE, e.CATEGORY_CODE, e.CUSTOMER_TYPE_CODE, e.FLG_NON_HA_PURCHASE, e.DT_INSERT, e.USERUPDATE, e.DT_UPDATE })
                    .HasName("IX_CU_B_ADDRESS_BOOK");

                entity.Property(e => e.COMPANY_CODE).HasMaxLength(3);

                entity.Property(e => e.DIVISION_CODE).HasMaxLength(3);

                entity.Property(e => e.CUSTOMER_CODE).HasMaxLength(8);

                entity.Property(e => e.BIRTHDATE).HasColumnType("date");

                entity.Property(e => e.BIRTHLOCATION).HasMaxLength(50);

                entity.Property(e => e.CATEGORY_CODE).HasMaxLength(3);

                entity.Property(e => e.COUNTRY_CODE).HasMaxLength(3);

                entity.Property(e => e.CUSTOMER_ID).HasMaxLength(100);

                entity.Property(e => e.CUSTOMER_TYPE_CODE).HasMaxLength(3);

                entity.Property(e => e.DEFAULT_PRICELIST_CODE).HasMaxLength(3);

                entity.Property(e => e.DISABILITY_CODE).HasMaxLength(2);

                entity.Property(e => e.DT_INSERT).HasColumnType("datetime");

                entity.Property(e => e.DT_PRIVACY_HQ_VALIDATION).HasColumnType("datetime");

                entity.Property(e => e.DT_UPDATE).HasColumnType("datetime");

                entity.Property(e => e.DT_UPDATE_CUSTOMERTYPE).HasColumnType("datetime");

                entity.Property(e => e.DT_UPDATE_STATUS).HasColumnType("datetime");

                entity.Property(e => e.DUPLICATE_CODE).HasMaxLength(8);

                entity.Property(e => e.FIRSTNAME).HasMaxLength(100);

                entity.Property(e => e.FLG_ADVERTISING).HasMaxLength(1);

                entity.Property(e => e.FLG_NON_HA_PURCHASE).HasMaxLength(1);

                entity.Property(e => e.FLG_PRIVACYPERSDATA).HasMaxLength(1);

                entity.Property(e => e.FLG_PROFILING).HasMaxLength(1);

                entity.Property(e => e.FLG_RETIRED).HasMaxLength(1);

                entity.Property(e => e.FLG_SENSDATA).HasMaxLength(1);

                entity.Property(e => e.FLG_TAX_EXEMPT).HasMaxLength(1);

                entity.Property(e => e.GENDER_CODE).HasMaxLength(1);

                entity.Property(e => e.LANGUAGE_CODE).HasMaxLength(5);

                entity.Property(e => e.LASTNAME).HasMaxLength(100);

                entity.Property(e => e.MIDDLENAME).HasMaxLength(100);

                entity.Property(e => e.OCCUPATION_CODE).HasMaxLength(3);

                entity.Property(e => e.OCCUPATION_OTHER).HasMaxLength(100);

                entity.Property(e => e.OTHERCONTACT_FIRSTNAME).HasMaxLength(100);

                entity.Property(e => e.OTHERCONTACT_LASTNAME).HasMaxLength(100);

                entity.Property(e => e.RELATION).HasMaxLength(50);

                entity.Property(e => e.ROWGUID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.SALUTATION_CODE).HasMaxLength(3);

                entity.Property(e => e.STATUS_CODE).HasMaxLength(3);

                entity.Property(e => e.TAX_ID_NUMBER).HasMaxLength(100);

                entity.Property(e => e.TITLE_CODE).HasMaxLength(3);

                entity.Property(e => e.USERINSERT).HasMaxLength(50);

                entity.Property(e => e.USERUPDATE).HasMaxLength(50);
            });

            modelBuilder.Entity<CU_B_ADDRESS_BOOK_EXT_AUS>(entity =>
            {
                entity.HasKey(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.CUSTOMER_CODE });

                entity.HasIndex(e => e.BPAY_CRN)
                    .HasName("missing_index_8382");

                entity.HasIndex(e => e.ROWGUID)
                    .HasName("missing_index_10492");

                entity.HasIndex(e => e.SHOP_CODE);

                entity.HasIndex(e => new { e.TYPE_CODE, e.SHOP_CODE, e.IS_TOPUP, e.CUSTOMER_CODE })
                    .HasName("IDX_CU_B_ADDRESS_BOOK_CUSTOMER_CODE");

                entity.Property(e => e.COMPANY_CODE).HasMaxLength(3);

                entity.Property(e => e.DIVISION_CODE).HasMaxLength(3);

                entity.Property(e => e.CUSTOMER_CODE).HasMaxLength(8);

                entity.Property(e => e.BPAY_CRN).HasMaxLength(20);

                entity.Property(e => e.CONTACTED_ON).HasColumnType("datetime");

                entity.Property(e => e.CURRENT_FILE_LOCATION)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CURRENT_LOCATION_TYPE_CODE).HasMaxLength(3);

                entity.Property(e => e.DT_ARCHIVE_REQUESTED).HasColumnType("datetime");

                entity.Property(e => e.DT_INSERT).HasColumnType("datetime");

                entity.Property(e => e.DT_REGISTERED).HasColumnType("datetime");

                entity.Property(e => e.DT_UPDATE).HasColumnType("datetime");

                entity.Property(e => e.DT_UPDATE_VALIDATION_STATUS).HasColumnType("datetime");

                entity.Property(e => e.DVA_CARD_TYPE_CODE).HasMaxLength(3);

                entity.Property(e => e.DVA_CLIENT_NUMBER).HasMaxLength(12);

                entity.Property(e => e.ELIGIBILITY_CODE).HasMaxLength(10);

                entity.Property(e => e.EMPLOYEE_CODE).HasMaxLength(8);

                entity.Property(e => e.FLG_ARCHIVE_REQUESTED).HasMaxLength(1);

                entity.Property(e => e.FLG_LOST_OHS_ELIGIBILITY).HasMaxLength(1);

                entity.Property(e => e.FLG_MIGRATED_FROM_VC).HasMaxLength(1);

                entity.Property(e => e.FLG_TO_CALL).HasMaxLength(1);

                entity.Property(e => e.IDCALL).HasMaxLength(100);

                entity.Property(e => e.IS_DVA).HasMaxLength(1);

                entity.Property(e => e.IS_TOPUP)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('N')");

                entity.Property(e => e.Is_MCLRecieved).HasMaxLength(1);

                entity.Property(e => e.JOURNEY_STAGE_CODE).HasMaxLength(3);

                entity.Property(e => e.NOTES).HasColumnType("text");

                entity.Property(e => e.OLD_TYPE_CODE).HasMaxLength(3);

                entity.Property(e => e.OUTCOMEID).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.PRACTICE_CODE).HasMaxLength(10);

                entity.Property(e => e.PRACTITIONER_CODE).HasMaxLength(10);

                entity.Property(e => e.PREFERREDNAME).HasMaxLength(50);

                entity.Property(e => e.PREV_ALD_DVA).HasMaxLength(1);

                entity.Property(e => e.PRIVACY_CONSENT_SIGNED).HasMaxLength(1);

                entity.Property(e => e.REF_SOURCE_CODE).HasMaxLength(9);

                entity.Property(e => e.RELATION).HasMaxLength(20);

                entity.Property(e => e.ROWGUID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.SHOP_CODE).HasMaxLength(3);

                entity.Property(e => e.SOURCE_CODE).HasMaxLength(9);

                entity.Property(e => e.SUB_SOURCE_CODE).HasMaxLength(9);

                entity.Property(e => e.TYPE_CODE)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.USERINSERT).HasMaxLength(50);

                entity.Property(e => e.USERUPDATE).HasMaxLength(50);

                entity.Property(e => e.VALIDATION_STATUS_CODE).HasMaxLength(3);

                entity.HasOne(d => d.CU_B_ADDRESS_BOOK)
                    .WithOne(p => p.CU_B_ADDRESS_BOOK_EXT_AUS)
                    .HasForeignKey<CU_B_ADDRESS_BOOK_EXT_AUS>(d => new { d.COMPANY_CODE, d.DIVISION_CODE, d.CUSTOMER_CODE })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CU_B_ADDRESS_BOOK_EXT_AUS_CU_B_ADDRESS_BOOK");

                entity.HasOne(d => d.CM_B_SHOP)
                    .WithMany(p => p.CU_B_ADDRESS_BOOK_EXT_AUS)
                    .HasForeignKey(d => new { d.COMPANY_CODE, d.DIVISION_CODE, d.SHOP_CODE })
                    .HasConstraintName("FK_CU_B_ADDRESS_BOOK_EXT_AUS_CM_B_SHOP");

                entity.HasOne(d => d.CM_S_EMPLOYEE)
                    .WithMany(p => p.CU_B_ADDRESS_BOOK_EXT_AUS)
                    .HasForeignKey(d => new { d.COMPANY_CODE, d.DIVISION_CODE, d.SHOP_CODE, d.EMPLOYEE_CODE })
                    .HasConstraintName("FK_CU_B_ADDRESS_BOOK_EXT_AUS_CM_S_EMPLOYEE");
            });

            modelBuilder.Entity<SY_EMPLOYEE_TYPE>(entity =>
            {
                entity.HasKey(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.EMPLOYEE_TYPE_CODE });

                entity.HasIndex(e => e.ROWGUID)
                    .HasName("UQ_SY_EMPLOYEE_TYPE_ROWGUID")
                    .IsUnique();

                entity.Property(e => e.COMPANY_CODE).HasMaxLength(3);

                entity.Property(e => e.DIVISION_CODE).HasMaxLength(3);

                entity.Property(e => e.EMPLOYEE_TYPE_CODE).HasMaxLength(3);

                entity.Property(e => e.DT_END).HasColumnType("date");

                entity.Property(e => e.DT_INSERT).HasColumnType("datetime");

                entity.Property(e => e.DT_START).HasColumnType("date");

                entity.Property(e => e.DT_UPDATE).HasColumnType("datetime");

                entity.Property(e => e.EMPLOYEE_TYPE_DESCR).HasMaxLength(255);

                entity.Property(e => e.ROWGUID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.USERINSERT).HasMaxLength(50);

                entity.Property(e => e.USERUPDATE).HasMaxLength(50);
            });

            modelBuilder.Entity<SY_GENERAL_STATUS>(entity =>
            {
                entity.HasKey(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.STATUS_CODE, e.ENTITY_TYPE_CODE });

                entity.HasIndex(e => e.ROWGUID)
                    .HasName("UQ_SY_GENERAL_STATUS_ROWGUID")
                    .IsUnique();

                entity.HasIndex(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.ENTITY_TYPE_CODE })
                    .HasName("IDX_SY_GENERAL_STATUS_SY_ENTITY_TYPE");

                entity.Property(e => e.COMPANY_CODE).HasMaxLength(3);

                entity.Property(e => e.DIVISION_CODE).HasMaxLength(3);

                entity.Property(e => e.STATUS_CODE).HasMaxLength(3);

                entity.Property(e => e.ENTITY_TYPE_CODE).HasMaxLength(3);

                entity.Property(e => e.DT_END).HasColumnType("date");

                entity.Property(e => e.DT_INSERT).HasColumnType("datetime");

                entity.Property(e => e.DT_START).HasColumnType("date");

                entity.Property(e => e.DT_UPDATE).HasColumnType("datetime");

                entity.Property(e => e.ROWGUID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.STATUS_DESCR).HasMaxLength(255);

                entity.Property(e => e.USERINSERT).HasMaxLength(50);

                entity.Property(e => e.USERUPDATE).HasMaxLength(50);
            });

            modelBuilder.Entity<SY_LOCATION_TYPE>(entity =>
            {
                entity.HasKey(e => new { e.COMPANY_CODE, e.DIVISION_CODE, e.LOCATION_TYPE_CODE });

                entity.HasIndex(e => e.ROWGUID)
                    .HasName("UQ_SY_LOCATION_TYPE_ROWGUID")
                    .IsUnique();

                entity.Property(e => e.COMPANY_CODE).HasMaxLength(3);

                entity.Property(e => e.DIVISION_CODE).HasMaxLength(3);

                entity.Property(e => e.LOCATION_TYPE_CODE).HasMaxLength(3);

                entity.Property(e => e.DT_END).HasColumnType("date");

                entity.Property(e => e.DT_INSERT).HasColumnType("datetime");

                entity.Property(e => e.DT_START).HasColumnType("date");

                entity.Property(e => e.DT_UPDATE).HasColumnType("datetime");

                entity.Property(e => e.LOCATION_TYPE_DESCR).HasMaxLength(255);

                entity.Property(e => e.ROWGUID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.USERINSERT).HasMaxLength(50);

                entity.Property(e => e.USERUPDATE).HasMaxLength(50);
            });

            modelBuilder.HasSequence("NextFoxid").StartsAt(0);

            modelBuilder.HasSequence("GETNEXTBATCHNUMBER").StartsAt(200);

            modelBuilder.HasSequence("NextFoxCommonVoucherId");

            modelBuilder.HasSequence("NextFoxCouponid").StartsAt(0);

            modelBuilder.HasSequence("NextFoxid").StartsAt(4000);

            modelBuilder.HasSequence("NextFoxVoucherId");
        }
    }
}
