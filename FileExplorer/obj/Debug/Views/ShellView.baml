   M S B A M L   `   `   `  ����  FileExplorer"    FileExplorer.Views.ShellView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   U QMaterialDesignThemes.Wpf, Version=2.3.1.953, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	      MaterialDesignBody#���   5   	     MaterialDesignPaper#���  5   	   $��Medium"�5   	   $��14(�5   	   $X��Rpack://application:,,,/MaterialDesignThemes.Wpf;component/Resources/Roboto/#Roboto)�5	   	   $��1q�5
   	     � WindowStyle$
  None=�5   	    � AllowsTransparency .  � WindowState$ Normal=�5   	    � Title$! Assembly Version Management��5   	   $	��750��6-   $	��600��6:    � WindowStartupLocation$ CenterScreen=�5   	    � 	Resources 5        CustomButtonStyle �� 
TargetType  MaterialDesignToolButton �� BasedOn �� Triggers	 P� Property
 P� Value �� Property �� Value��%�   &	       7  �� 5   
   " ��6*   8    5      P� 5      	 �@J�IsMouseOver
 . . �5      ��  �@J�Opacity$ .5Q�60   5      5      5      .��5      �  � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $��* �6   .+�5   
   ��   PrimaryHueDarkBrush#��[ 6   ._�5       �  � ColumnDefinitions 5!      �� 5"      $��* �6'   �� 5#      $
��Auto �6'   .+�5%      �� -   5        $��0��5%   :   #��[ 5&      �� 5'       �� Orientation$ 
Horizontal=�6   $��1��66   $
��Auto��5(      $��0 4 0 0q�5)      .+�5*      �� -   5        #���  5*      $
��Auto��5+   1   $��0q�6?   $��30��5,      .��5-      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.3.1.953, Culture=neutral, PublicKeyToken=null]]   Kind$ WindowMinimize=�62   $��20��5.   2   $��20��6>      PrimaryHueMidForegroundBrush#��[ 5/   2   50      �� 51      -   5        #���  51      $
��Auto��52   2   $��0q�6@   $��30��53      .��54        $ Close=�62   $��24��55   2   $��24��6>   #��[ 56   2   57      58      59      5:      )  #MaterialDesignThemes.Wpf.DialogHost  5<   
   -   5          Name$ 
RootDialog��5=   $      
Identifier$ 
RootDialog��5<   $   $��1��5>   $   .��5?      )  #MaterialDesignThemes.Wpf.DrawerHost  .��5@      �� -   5         ��Name$ 
ActiveItem��5@   !   5A      5B      5C      5D      