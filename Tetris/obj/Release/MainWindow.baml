   M S B A M L   `   `   `  ���� A  =Tetris, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:TetrisTetris      Tetris.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     &localclr-namespace:Tetris     -     � Title$  Tetris��$	��632��$	��450�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=�
���   � � 
ResizeMode$ NoResize=�.��� ���	#FF729DC2 � RowDefinitions �� $��22 ��� .+�]� .+�o� $
��Left=�$��5 2 2 2q�
����� �.@�m� $
��Game��.@�m� -   $��New Game��
����� �m� -   $��Pause <Pause Key>��
����� �m� -   $
��Exit��
����� ��� -   $��Right=�$��Right=�$��30�����	#FF251212$
��Bold"�
����� �$��2 2 5 2q�.��X� $��1��$��5q� � ColumnDefinitions �� $	��300 ��� .+��� -   $��canvas��}�. $	��300��$	��600��
���   �� $��1��$��5 0 0 0q�
���E�� �� �� �� �� .+��� -   $��previewCanvas��$	��120��$��90���� -   $��gameOverTxtBlk��$��1��$��10q�$��WrapWithOverflow=�
���  ��$��40(�$
��Bold"� �� TextAlignment$ Center=�$��Center=��� -	   $��gameScoreTxtBlk��$��3��$��10q�$��WrapWithOverflow=�
���`���$��20(�$
��Bold"�$ Center=�$��Center=�