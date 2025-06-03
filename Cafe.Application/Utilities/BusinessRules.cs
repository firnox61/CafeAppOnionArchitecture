using Cafe.Core.Utilities.Results;

namespace Core.Utilities.Business
{
    public static class BusinessRules
    {
        public static async Task<IResult?> RunAsync(params Func<Task<IResult>>[] logics)
        {
            foreach (var logic in logics)
            {
                IResult result = await logic();
                if (!result.Success)
                {
                    return result;
                }
            }
            return null;
        }
    }

}