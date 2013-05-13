using System.Linq;
using Castle.MicroKernel;
using Castle.MicroKernel.Facilities;

namespace Sapphire
{
    public static class KernelExtensions
    {
        public static void EnsureDependentFacilityAdded<T>(this IKernel kernel)
        {
            if (kernel.GetFacilities().All(f => f.GetType() != typeof (T)))
                throw new FacilityException(string.Format("You should add {0}", typeof (T).Name));
        }
    }
}