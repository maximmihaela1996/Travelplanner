using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Travelpal.Interfaces;
using Travelpal.Managers;
using Travelpal.Models;

namespace Travelpal
{
    public partial class TravelsWindow : Window
    {
        private UserManager userManager;
        private TravelManager travelManager;
        private IUser user;
        public TravelsWindow(UserManager userManager, TravelManager travelManager, bool isUser)
        {
            InitializeComponent();
            this.travelManager = travelManager;
            this.userManager = userManager;
            this.user = user;

            if (isUser)
            {
                btnVerify.Content = "IsUser";
            }
            else if (!isUser)
                btnVerify.Content = "IsAdmin";
        }
    }
}
