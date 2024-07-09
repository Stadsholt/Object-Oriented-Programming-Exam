namespace OOP
{
    class StregsystemController
    {
        private IStregsystemUI _stregsystemUI;
        private IStregsystem _stregsystem;
        public StregsystemController(IStregsystemUI ui, IStregsystem stregsystem)
        {
            _stregsystemUI = ui;
            _stregsystem = stregsystem;
            StregsystemCommandParser parser = new StregsystemCommandParser(_stregsystemUI, _stregsystem);
            _stregsystemUI.Start();
        }
    }
}
