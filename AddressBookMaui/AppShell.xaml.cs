﻿using AddressBookMaui.Pages;

namespace AddressBookMaui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ContactListPage), typeof(ContactListPage));
            Routing.RegisterRoute(nameof(ContactAddPage), typeof(ContactAddPage));
            Routing.RegisterRoute(nameof(ContactEditPage), typeof(ContactEditPage));
        }
    }
}
