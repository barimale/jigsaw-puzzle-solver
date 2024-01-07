using Solver.Tangram.AlgorithmDefinitions.Generics.SingleAlgorithm;
using Solver.Tangram.Game.Logic;
using System.Reflection;
using Tangram.GameParts.Logic.GameParts;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.UTs.Activator
{
    // WIP
    public class ActivatorNeedsToBeExecuted
    {
        [Fact]
        public GameSet? Example_CreateBigBoard()
        {
            // given
            var controlClassName = "Tangram.GameParts.Elements.GameSetFactory";
            var methodName = "CreateBigBoard";

            // when
            var filtered = MyFunc(controlClassName);

            var assembly = filtered.FirstOrDefault();
            if(assembly == null)
            {
                return null;
            }

            Type t = assembly.GetType(controlClassName);
            var inst = System.Activator.CreateInstance(t);

            var property = t.GetMethod(methodName);
            if (property == null)
            {
                return null;
            }

            GameSet gameParts = (GameSet)property.Invoke(inst, new object[] { true });

            // then
            Assert.NotNull(gameParts);

            return gameParts;
        }

        [Fact]
        public IExecutableAlgorithm? Example_binDepthTS()
        {
            // given
            var controlClassName = "TreeSearch.Algorithm.Tangram.Solver.Templates.TSTemplatesFactory";
            var methodName = "CreateDepthFirstTreeSearchAlgorithm";
            int maxDegreeOfParallelism = 2048 * 6; // -1
            var gameParts = Example_CreateBigBoard();

            // when
            var filtered = MyFunc(controlClassName);

            var assembly = filtered.FirstOrDefault();
            if (assembly == null)
            {
                return null;
            }

            Type t = assembly.GetType(controlClassName);
            var inst = System.Activator.CreateInstance(t);

            var property = t.GetMethod(methodName);
            if (property == null)
            {
                return null;
            }

            IExecutableAlgorithm binDepthTS = (IExecutableAlgorithm)property.Invoke(inst, new object[] {
                gameParts.Board,
                gameParts.Blocks,
                maxDegreeOfParallelism
                });

            // then
            Assert.NotNull(binDepthTS);

            return binDepthTS;
        }

        // Summary: do mappings between input data and more meaningful overal 
        // description of the board and algorithms

        [Fact]
        public void Example_konfiguracjaGry()
        {
            // given
                // 1. required input data
                //var controlClassName = "Tangram.GameParts.Elements.GameSetFactory";
                //var methodName = "CreateBigBoard";
                var gameParts = Example_CreateBigBoard();

                // 2. required input data
                //var controlClassName = "TreeSearch.Algorithm.Tangram.Solver.Templates.TSTemplatesFactory";
                //var methodName = "CreateDepthFirstTreeSearchAlgorithm";
                //int maxDegreeOfParallelism = 2048 * 6; // -1
                var binDepthTS = Example_binDepthTS();

            // when 
            var konfiguracjaGry = new GameBuilder()
                .WithGamePartsConfigurator(gameParts)
                .WithAlgorithm(binDepthTS)
                .Build();

            // then
            Assert.NotNull(konfiguracjaGry);
        }

        // it is usefull for maxDegreeOfParallelism
        [Fact
            (Skip = "As amount of RAM is device-specific, the test is skipped.")
        ]
        public void Check_amount_of_RAM()
        {
            // given
            var gcMemoryInfo = GC.GetGCMemoryInfo();

            // when
            long installedMemory = gcMemoryInfo.TotalAvailableMemoryBytes;
            var physicalMemoryInGigaBytes = (double)installedMemory / 1048576.0 / 1024.0;
            var physicalMemoryInGigaBytesAsInt = Convert.ToInt32(physicalMemoryInGigaBytes);

            // then
            Assert.Equal(16, physicalMemoryInGigaBytesAsInt);
        }

        private static List<Assembly>? MyFunc(string controlClassName)
        {
            string result = "";
            // get a list of all assemblies in this application domain
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            // the trouble is that we don't know which assembly the class is defined in,
            // because we are using the "Web Site" model in Visual Studio that compiles
            // them on the fly into assemblies with random names
            // -> however, we do know that the assembly will be named App_Web_*

            var filtered = assemblies
                .Select(p =>
                {
                    Type t = p.GetType(controlClassName);
                    if (t != null) return p;
                    return null;
                })
                .Where(p => p!= null)
                .ToList();

            var a = assemblies.Count();
            var b = filtered.Count;

            return filtered;
        }
    }
}