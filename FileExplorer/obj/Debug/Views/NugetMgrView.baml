   M S B A M L   `   `   `  ����G  CFileExplorer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:FileExplorer.ViewsFileExplorer.Views  5      ;"clr-namespace:FileExplorer.ControlFileExplorer.Control  5      %    FileExplorer.Views.NugetMgrView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:FileExplorer.Views     5      6control"clr-namespace:FileExplorer.Control     5      .��5
      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   '   !FileExplorer.Control.CommonFilter  &   System.Windows.Controls.DataGrid  5   
   $��2��6   $��1 3 1 1q�5         CanUserSortColumns .    CanUserAddRows .     
IsReadOnly .     AutoGenerateColumns .  ��5      �� *Items+   Columns 5      4  .System.Windows.Controls.DataGridCheckBoxColumn  5       -  )MaterialDesignDataGridCheckBoxColumnStyle1  +System.Windows.Controls.DataGridBoundColumn   ElementStyle# [  5   )    4 0MaterialDesignDataGridCheckBoxColumnEditingStyle   EditingElementStyle# [ 5   )      Binding 5   )   �� *	IsChecked+	 �� Mode$	 TwoWay=�,  &System.Windows.Controls.DataGridColumn
   Header
 5      �� 5      
������  �� Padding$ 10 0 6 0q�6:   $��Center=�6M   ._�5      �� $��Center=�6'    � DataContext 5    +   ��  �� RelativeSource ��  �� AncestorType"   �� Path$ DataContext� d� 	IsChecked 5!   +   �� *IsAllItemsSelected+5"      5$      0  *System.Windows.Controls.DataGridTextColumn  5%      $

 Name��5&   )      
IsReadOnly .  0 ,MaterialDesignDataGridTextColumnEditingStyle# [ 5'   )    5%   %   �� *Name+5'   )     5(      $
 	Nuget Ver��5)   )    . # [ 5*   )    5(   %   �� *NugetVersion+5*   )   5,      5-      5.      