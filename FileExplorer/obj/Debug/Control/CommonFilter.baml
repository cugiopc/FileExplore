   M S B A M L   `   `   `  ����G  CFileExplorer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:FileExplorer.ControlFileExplorer.Control  5      '    !FileExplorer.Control.CommonFilter   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:FileExplorer.Control     5      U QMaterialDesignThemes.Wpf, Version=2.3.1.953, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      ^ ZCaliburn.Micro.Platform, Version=3.2.0.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f(calhttp://www.caliburnproject.org  5      .��5   
   � $��4 4 4 0q�6     � 	Resources  5       �� 
TargetType   MaterialDesignTextBox �� BasedOn)  #MaterialDesignThemes.Wpf.HintAssist �� Property �� Value��%�   '��       7   �� 5      " ��6   8   .�5      ��  �@ 
IsFloating . 6M   �� 5       � ��$ 4q�6/   5      5       � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   � $��0��6   $��2��6    5      �� 5      $��* �6#   �� 5      $
��Auto �6#   .+�5       ��    Hint$ Root dir��6   $��0��5!      $��0 0 4 0q�5#      ��5"      �� *	RootDir+ �� UpdateSourceTrigger$ PropertyChanged=�5#      �� 5$      $��1��6   $��	0 0 0 -10q�6%   $
��Left=�5%      	 � ToolTip$	 Directory browser��5&      $
��Auto��5'        Caliburn.Micro.Message
   Attach$2
 ,[Event Click] = [Action DirectoryBrowser();]��5(      .��5)      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.3.1.953, Culture=neutral, PublicKeyToken=null]]   Kind$ DotsHorizontal=�6*   $��24��5*   *   $��24��5+   *   
�������5,   *   5-      5.      �� 51      $ Filter��6   $��1��52      $��0��6'   $��0 0 4 0q�54      ��53      �� *Filter+$ PropertyChanged=�54      �� 57      $��4 12 4 2q�6   $��1��58      $��1��59       �� Orientation$ 
Horizontal=�5:      $
��Left=�62   .+�5<      �� $��1 0 2 0q�6   $	��110��6*   $%
 [Event Click] = [Action Scan()]��5=      $��Center=�5>      $��Center=�66   .��5?      �� $ 
Horizontal=�6!   .+�5@        $ FileFind=�62   $��20��5A   2   $��18��6>   $��0 0 4 0q�5B   2   
�������$��Center=�5D   2   $��Center=�6O   �� 5E      $
��Scan��6$   $��Center=�60   5F      5G      �� 5I      $��10 0 2 0q�6   $	��110��6+   $1
 +[Event Click] = [Action IncreaseVersion()];��5J      $��Center=�5K      $��Center=�66   .��5L      �� $ 
Horizontal=�6!   .+�5M        $ ArrowUpBold=�62   $��20��5N   2   $��18��6>   $��0 0 4 0q�5O   2   
�������$��Center=�5Q   2   $��Center=�6O   �� 5R      $��Increase��6$   $��Center=�64   5S      5T      5U      5V      5W      