public interface IController
{
    void Handle(Direction direct);
    void Confirm();
    void OnEnter();
    void OnExit();
}