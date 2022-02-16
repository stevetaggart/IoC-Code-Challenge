using System;
using Xunit;

namespace IoC_Code_Challenge
{
    public class IoCContainerTests
    {
        [Fact]
        public void Transient_lifecycle_creates_new_objects_per_request()
        {
            // Arrange
            var sut = new IocContainer();
            sut.Register<ICalculator, Calculator>(LifeCycleType.Transient);

            // Act
            var calc1 = sut.Resolve<ICalculator>();
            var calc2 = sut.Resolve<ICalculator>();

            // Assert
            Assert.NotSame(calc1, calc2);
        }

        [Fact]
        public void Singleton_lifecycle_uses_same_object_per_request()
        {
            // Arrange
            var sut = new IocContainer();
            sut.Register<ICalculator, Calculator>(LifeCycleType.Singleton);

            // Act
            var calc1 = sut.Resolve<ICalculator>();
            var calc2 = sut.Resolve<ICalculator>();

            // Assert
            Assert.Same(calc1, calc2);
        }

        [Fact]
        public void Resolving_unregistered_type_throws_an_exception()
        {
            // Arrange
            var sut = new IocContainer();

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => sut.Resolve<ICalculator>());
        }

        [Fact]
        public void Container_injects_constructor_dependencies_for_known_types()
        {
            // Arrange
            var sut = new IocContainer();
            sut.Register<ICalculator, Calculator>();

            // Act
            var controller = sut.Resolve<CalculatorController>();

            // Assert
            Assert.Equal(42, controller.PerformCalculations(40, 2));
        }
    }
}