using System.Threading.Tasks;

namespace Algorithm.ConstraintsPairing
{
    public interface ICreateStep
    {
        Task Initialize(InputData input);
    }
}