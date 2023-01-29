﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OurBudgets.Services
{
    internal sealed class MessageService : IMessageService
    {
        public MessageBoxResult Question(string message)
        {
            return MessageBox.Show(message, "Check", MessageBoxButton.OKCancel, MessageBoxImage.Question);
        }

        public void ShowDialog(string message)
        {
            MessageBox.Show(message);
        }
    }
}
