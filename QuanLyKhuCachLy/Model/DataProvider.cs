namespace QuanLyKhuCachLy.Model
{
    public class DataProvider
    {
        private static DataProvider _ins;
        public static DataProvider ins
        {
            get
            {
                if (_ins == null) _ins = new DataProvider();
                return _ins;
            }
            set { _ins = value; }
        }

        public QLKCLEntities db { get; set; }

        private DataProvider()
        {
            db = new QLKCLEntities();
        }
    }
}
