   M S B A M L   `   `   `  ����  FileExplorer#    FileExplorer.Views.HeaderView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      U QMaterialDesignThemes.Wpf, Version=2.3.1.953, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      ^ ZCaliburn.Micro.Platform, Version=3.2.0.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f(calhttp://www.caliburnproject.org  5         PrimaryHueDarkBrush#��[  5	      $��60��5      .��5      � $��0 0 0 0q�6     � ColumnDefinitions  5   
   �� 5      $��* �6   �� 5      $
��Auto �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   (  "MaterialDesignThemes.Wpf.ColorZone     Mode$ Standard=�6#   $��4 2 2 2q�63      CornerRadius$ 2��6E    K� ZIndex$ 1��6V   $��7 7 7 0q�5   $   $��40��65   +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth1=�5   $   $��2��5   $   .��5      �   5      �� 5      $
��Auto �6'   �� 5      $
��Auto �6'   �� 5      $��* �6'   �� 5      $
��Auto �6'   �� 5      $
��Auto �6'   .+�5"      '  !MaterialDesignThemes.Wpf.PopupBox     PlacementMode$ BottomAndAlignRightEdges=�6*      	StaysOpen .     PopupContent. 5$      �� .+�5%      ��   Caliburn.Micro.Message	   Attach$+	 %[Event Click] = [Action ProjectMgr()]��6!   ��5&      �� 5'   "   
 �� Orientation$
 
Horizontal=�6-   $
��Left=�6F   $��Center=�6a   .+�5(   &   '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.3.1.953, Culture=neutral, PublicKeyToken=null]]   Kind$ LanguageCsharp=�6>   $��Center=�6T   �� 5)   &   $��Project��60   $��8 0 0 0q�6?   $��Center=�6P   $
��Left=�6k   5*   #   5,      �� 56      $0	 *[Event Click] = [Action ReferenceAssMgr()]��6!   ��57      �� 58   "   $
 
Horizontal=�6-   $
��Left=�6F   $��Center=�59   -   .+�5:   &     $
 Link=�6>   $��Center=�5;   >   �� 5<   &   $��Assembly��60   $��8 0 0 0q�6@   $��Center=�6Q   $
��Left=�6l   5=   #   5?      �� 5@      �� 5A      $%	 [Event Click] = [Action Exit()]��6!   ��5B      �� 5C   "   $
 
Horizontal=�6-   $
��Left=�6F   $��Center=�5D   -   .+�5E   &     $ Close=�6>   $��Center=�6K   �� 5F   &   $
��Exit��60   $��8 0 0 0q�6<   $��Center=�6M   $
��Left=�5G   0   5H   #   5J      5K      5L        5O      $ Magnify=�6*    J� Opacity$	 .56Q�69   $��24��5P   *   $��24��66   $��1��6A   $��Center=�5Q   *   $��Center=�6G   �� 5R      $��2��6   $��8 0 0 0q�6*   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Search here��6;   .  (MaterialDesignThemes.Wpf.TextFieldAssist   DecorationVisibility$ Hidden=�5S   &   $��0q�6c   $	��200��5T   &   $��Center=�65   ��5U      �� *
FilterText+ �� UpdateSourceTrigger$ PropertyChanged=��� 5V        MaterialDesignToolButton#���  6   $��3��6L   $*	 $[Event Click] = [Action ClearText()]��5X      y�5W      �� *	HasText+$ PropertyChanged=�.��5Y        $ Close=�6.   $	 .56Q�6;   $��16��6I   $��16��6U   5Z      5[      5\      5_      5`      