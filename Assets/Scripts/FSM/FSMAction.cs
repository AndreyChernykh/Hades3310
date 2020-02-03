namespace FSM {
    public delegate void ActionCallback();

    public class Action : IAction {
        public string name;
        public ActionCallback callback;

        public Action(ActionCallback callback, string name = null) {
            this.callback = callback;
            this.name = name;
        }

        public void Act() {
            callback();
        }
    }
}