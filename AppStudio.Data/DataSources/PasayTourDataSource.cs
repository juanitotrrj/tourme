using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class PasayTourDataSource : DataSourceBase<MenuSchema>
    {
        private const string _file = "/Assets/Data/PasayTourDataSource.json";

        protected override string CacheKey
        {
            get { return "PasayTourDataSource"; }
        }

        public override bool HasStaticData
        {
            get { return true; }
        }

        public async override Task<IEnumerable<MenuSchema>> LoadDataAsync()
        {
            try
            {
                var serviceDataProvider = new StaticDataProvider(_file);
                return await serviceDataProvider.Load<MenuSchema>();
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("PasayTourDataSource.LoadData", ex.ToString());
                return new MenuSchema[0];
            }
        }

    }
}
