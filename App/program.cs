using App.Controllers;
using App.Managers;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            ItemManager manager = new ItemManager();

            MenuController menu = new MenuController(manager);

            menu.Run();
        }
    }
}