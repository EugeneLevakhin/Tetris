   M S B A M L   `   `   `  ����A  =Tetris, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:TetrisTetris  5   	       Tetris.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Tetris     5   	   -   5          � Title$  Tetris��5   	   $	��632��6   $	��450��6%    � WindowStartupLocation$ CenterScreen=�5	   	    � WindowStyle$
 None=�6.   
���   � � 
ResizeMode$ NoResize=�5
   	   .��5      � ���	#FF729DC2 � RowDefinitions 5   
   �� 5      $��22 �6   �� 5      .+�5   
   ]� .+�5      o� $
��Left=�6   $��5 2 2 2q�6)   
����� �.@�5      m� $
��Game��6   .@�5      m� -   5        $��New Game��5      
����� �6d   m� 5      -   5        $��Pause <Pause Key>��5      
����� �6p   m� 5      -   5        $
��Exit��5      
����� �6\   5      5      �� 5      -   5        $��Right=�5      $��Right=�6,   $��30��6H   ���	#FF251212$
��Bold"�6j   
����� �$��2 2 5 2q�6�   .��6�   X6�   5      � 5   
   $��1��6   $��5q�6    � ColumnDefinitions 5      �� 5       $	��300 �6#   �� 5!      .+�5$      �� -   5        $��canvas��5$   )   }�. $	��300��67   $	��600��6C   
���   �6P   � 5&      $��1��6   $��5 0 0 0q�6#   
���E�� 5'      �� 5(      �� 5)      �� 5*      �� 5+      .+�5.      �� -   5        $��previewCanvas��5.      $	��120��6.   $��90��6:   �� 5/      -   5        $��gameOverTxtBlk��5/      $��1��62   $��10q�6?   $��WrapWithOverflow=�6K   
���  ��$��40(�6�   $
��Bold"�6�    �� TextAlignment$ Center=�6�   $��Center=�6�   �� 50      -	   5        $��gameScoreTxtBlk��50      $��3��63   $��10q�6@   $��WrapWithOverflow=�6L   
���`���$��20(�6�   $
��Bold"�6�   $ Center=�6�   $��Center=�6�   51      52      53      54      