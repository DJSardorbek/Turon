   M S B A M L   `   `   `  ����K  GSotuvPlatformasi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:SotuvPlatformasiSotuvPlatformasi  5   	   #    SotuvPlatformasi.ChooseClient   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:SotuvPlatformasi     5   	   b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5          � Title$  ChooseClient��5	   	   $	��500��5
   	   $	��650��6    � WindowStyle$
 None=�5   	    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	   $��2q�5   	   ���#263859.��5      � ���#17223B � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $
��Auto �6#   .+�5      � $��0��6   .+�5      �� -   5         ��Name$ 	txtSearch��5         	TextBoxes#��[  5       $��40��5!      $��
20 20 20 0q�6"   � 5"      ���#17223B$��20��5$      $	��110��6   $��
40 -25 0 0q�5%      $
��Left=�5&      .+�5'      �� $��Mijoz fish...��5(      ���#6B778C$��Segoe UI)�5*      $
��16px(�5+      $��Center=�5,      $��Center=�5-      5.      '  !MaterialDesignThemes.Wpf.PackIcon  5/      ���#6B778C   Kind$ Search=�51      $��	0 20 30 0q�52      $��25��53      $��25��6   $��Right=�54      $��Center=�55      56      � 58      $��1��6   $��0 20 0 0q�6'   .+�59      �� -   5         ��Name$ btnNew��5:        SotuvHeader#��[ 5<      $��40��5=      $	��100��5>      $��0 0 20 0q�5?      .��5@      �� $��Yangi��6$   ���#6B778C5A   $   5B      5C      5D      � 5F   
   $��1��6   .+�5G      � $��0��6   $��	0 30 0 10q�6    $��Stretch=�63   $
��Auto��6Q   .+�5H      �� -   5        	 ��Name$	 scrollViewer��5H      $��Disabled=�65   ��5I      �� 5J      
 �� 
TargetType"
 ��6     �� Triggers 5K      �� 5L   "    �� Binding 6.   �� *
HasItems+ �� ElementName$ dataGridClient�� �� Value	 False6g   .a�5M   &   ��  �� Property � �� �� Value$
 Auto=�6T   5N   #   5P      .��5R      &   System.Windows.Controls.DataGrid  -   5          Name$ dataGridClient��5R      $   HorizontalScrollBarVisibility$
 Auto=�5S      $
��Auto��5T         HorizontalGridLinesBrush �#263859   AutoGenerateColumns .     GridLinesVisibility$ 
Horizontal=�5W         CanUserAddRows .     CanUserDeleteRows .     
IsReadOnly .    CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .  ���	#FF17223B$��1q�5_         SelectionMode$ Single=�6)      SelectionUnit$ FullRow=�6@   $��
20 0 20 10q�5`       � 	Resources 5a        MaterialDesignDataGrid �� BasedOn*  $System.Windows.Controls.DataGridCell  MaterialDesignDataGridCell  P� Property! P� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader & "MaterialDesignDataGridColumnHeader��%%  '        7  '  i     7  '  �    7  '�� �    �� 5b      "
  6$   8   .�5c   "   ��  �@ GridLinesVisibility$ 
Horizontal=�6H   5d      �� 5e      "
  6$   8   .�5f   "   ��  �@�FocusVisualStyle 6E   Z� �� 5g   "    � �� �	#FF17223B6?   �� 5h   "    � ��
 �����6?   �� 5i   "    � ��$ 0q�6D   �� 5j   "    � �� �#2638596@    5k   "   P� 5l   &     �@ 
IsSelected! . . �5m   *   ��  � �� �#2638596G   5n   '   5q      �� 5r      "
  6$   8   .�5s   "   ��  � �� �#2638596?   �� 5t   "    � ��
 �����6?   �� 5u   "    � ��$ 44��6;   �� 5v   "    � ��$ Center=�6M    5w   "   5z      �� 5|      "
 ��6$   .�5}   "   ��  � �� �#2638596?   5~      5      "   Columns" 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn#   Header$	# KOD��5�   !   $   Width5  /System.Windows.Controls.DataGridLengthConverter$	$ 100 5�   !   1  +System.Windows.Controls.DataGridBoundColumn%   Binding% 5�   !   �� *id+5�   !     5�      $# MIJOZ��5�   !   $
$ 200* 5�   !   % 5�   !   �� *
mijoz_fish+5�   !   4	  .System.Windows.Controls.DataGridTemplateColumn	  5�      # 5�   "   �� 5�   &   $��TANLASH��60   $	��150��6?   & �� TextAlignment$& Center=�6K   ' 	  CellTemplate' 5�   "   �� 5�   &   .R�5�   *   �� $ 	btnChoose��5�   -   -   5        $��+��5�   -   ��5�   .   �� 5�   2   "
 ��68   .�5�   6   ��  � ��$	 120��6N   �� 5�   6    � ��$ 30��6O   �� 5�   6    � �� �#0057926S   �� 5�   6    � ��
 �����6S   �� 5�   6    � ��$ Segoe UI)�6S   �� 5�   6    � ��$
 18px(�6Q   �� 5�   6    � �� 5�   :   �� 5�   >   ( �� 
TargetType"( ��6N   .R�5�   B   �� ) �� CornerRadius$) 3��5�   E   #��z��5�   E   #��z��5�   E   ._�5�   F   �� $��Center=�6W   $��Center=�6r   5�   G   5�   C   5�   ?   5�   7    5�   6   P� 5�   :     �@J�IsMouseOver! . . �5�   >   ��  � �� �#0057F46[   5�   ;   5�   3   5�   +   5�   '   5�      5�      5�      5�      5�      5�      5�      