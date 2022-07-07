namespace BoilerplateService.UnitTest.Fixtures
{
    public static class FixtureFactory
    {
        public static IFixture GetInstance()
        {
            IFixture instance = new Fixture();
            instance.Behaviors.Remove(new ThrowingRecursionBehavior());
            instance.Behaviors.Add(new OmitOnRecursionBehavior());

            return instance;
        }
    }
}