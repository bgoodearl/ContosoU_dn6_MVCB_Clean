using CU.Definitions.Lookups;

namespace ContosoUniversity.Models.Lookups
{
    public class RandomLookupType : LookupBaseWith2cKey
    {
        public RandomLookupType()
        {
            LookupTypeId = (short)CULookupTypes.RandomLookupType;
        }
    }
}
