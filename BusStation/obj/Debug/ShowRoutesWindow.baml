   M S B A M L   `   `   `  ����E  ABusStation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:BusStation
BusStation  5   	   !    BusStation.ShowRoutesWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:BusStation     5   	     � Title$'  !Показать маршруты��5   	   $��320.757��6#   $��649.608��64   .��5	      � .+�5
   
   &   System.Windows.Controls.DataGrid  -   5          Name$ RoutesDataGrid��5
         
IsReadOnly .    AutoGenerateColumns .     HorizontalGridLinesBrush �	#FFCDCDCD   VerticalGridLinesBrush �	#FFCDCDCD   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ № маршрута��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6Y   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 69   �� 
 �� Path$
 Number�6e     5      $ № автобусов��6%   $ * 6_   	 6;   �� $
 
busNumbers�6k     5      $' !Время отправления��6%   $ * 6h   	 6A   �� $
 departureTime�6t     5      $' !Место отправления��6%   $ * 6i   	 6A   �� $
 departurePlace�6u     5      $! Место прибытия��6%   $ * 6g   	 6=   �� $
 destinationPlace�6s   5      5      5      