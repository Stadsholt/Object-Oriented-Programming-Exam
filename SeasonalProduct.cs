namespace OOP
{
    public class SeasonalProduct : Product
    {
        public SeasonalProduct(int id, string name, decimal price, bool active, bool canbeboughtoncredit, string seasonstartdate, string seasonenddate)
               : base(id, name, price, active, canbeboughtoncredit)
        {
            SeasonStartDate = seasonstartdate;
            SeasonEndDate = seasonenddate;
        }

        private string _seasonstartdate;
        public string SeasonStartDate
        {
            get { return _seasonstartdate; }
            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 1 || char.IsLower(value[0]))
                {
                    throw new ArgumentException("SeasonStartDate can't be empty!");
                }
                else
                {
                    _seasonstartdate = value;
                }
            }
        }

        private string _seasonenddate;
        public string SeasonEndDate
        {
            get{return _seasonenddate;}
            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 1 || char.IsLower(value[0]))
                {
                    throw new ArgumentException("SeasonStartDate can't be empty!");
                }
                else
                {
                    _seasonenddate = value;
                }
            }
        }
    }
}






