using LightInject;
using TaskManager.Presenter.Interfaces;

namespace TaskManager
{
    internal class ApplicationContoller
    {
        private ServiceContainer _ServiceContainer; 
        public ApplicationContoller(ServiceContainer ServiceContainer)
        {
            _ServiceContainer = ServiceContainer;
            _ServiceContainer.RegisterInstance<ApplicationContoller>(this);
        }
        public void Run<T>() where T : class, IPresenter
        {
            var presenter = _ServiceContainer.GetInstance<T>();
            presenter.Run();
        }
    }
}
