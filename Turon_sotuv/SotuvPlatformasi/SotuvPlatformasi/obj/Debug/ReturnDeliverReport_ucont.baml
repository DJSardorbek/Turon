   M S B A M L   `   `   `  ����K  GSotuvPlatformasi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:SotuvPlatformasiSotuvPlatformasi  5      0    *SotuvPlatformasi.ReturnDeliverReport_ucont   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      :d2http://schemas.microsoft.com/expression/blend/2008  5      *localclr-namespace:SotuvPlatformasi  5      .��5
      � ���#17223B  � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      $
��1.5* �6#   �� 5      $��* �6#   .+�5      � $��0��5      $��0 20 0 0q�5       5      �� 5      $
��1.4* �6'   �� 5      $��* �6'   �� 5      $	��160 �6'   .+�5      ��  �� Orientation$ 
Horizontal=�5      $��0��5      .+�5       �� $��	Summa uz:��6   
�������$��Segoe UI)�6@   $
��20px(�6V   $��Center=�6f   $��	30 0 20 0q�6�   �� 5!      -   5         ��Name$ txtSumma_uz��5"      $	��0.0��5#      ���#7BC9EB$��Segoe UI)�5%      $
��30px(�5&      $��Center=�5'      5(      �� 5)      $ 
Horizontal=�5*      $��1��5+      .+�5-      �� $��Summa $:��5.      
�������$��Segoe UI)�50      $
��20px(�51      $��Center=�52      $��0 0 20 0q�53      �� 54      -   5        $ txtSumma_dol��55      $	��0.0��56      ���#7BC9EB$��Segoe UI)�58      $
��30px(�59      $��Center=�5:      5;      �� 5<      $ 
Horizontal=�6   $��2��66   .+�5=      �� $��Kurs:��5>      
�������$��Segoe UI)�5@      $
��20px(�5A      $��Center=�5B      $��0 0 20 0q�5C      �� 5D      -   5        $ txtKurs��5E      $	��0.0��5F      ���#7BC9EB$��Segoe UI)�5H      $
��30px(�5I      $��Center=�5J      5K      5L      �� 5N      $��1��6   $��0 20 0 0q�6)   .+�5O      �� $��Right=�5P      $��1��5Q      $ 
Horizontal=�5R      $��0 10 0 0q�5S      .+�5T      (  "System.Windows.Controls.DatePicker  -   5          Name$ DatePickerDan��5T   !    � OverridesDefaultStyle . 
������ $��0 0 10 0q�5W      
�������5Y      �� 5Z      $��Dan...��6    
�������$��Center=�6A   $��Center=�6\   $��Segoe UI)�6y   $
��12px(�6�   $��	0 0 123 0q�6�     5[      -   5        $ DatePickerGacha��5[   !    . 
������ $��0 0 10 0q�5^      
�������5`      �� 5a      $��Gacha...��6    
�������$��Center=�6C   $��Center=�6^   $��Segoe UI)�6{   $
��12px(�6�   $��0 0 32 0q�6�   5b      5d      5e      � 5f   
   $��1��6   .+�5g      � -   5         �Name$ Basket��5g   &   $��	0 30 0 10q�6   $��1��66   $��Stretch=�6C   $
��Auto��6a   $y�Visible=�6n   .+�5h      �� -   5         ��Name$ scrollViewer��5h      $��Disabled=�65   ��5i      �� 5j       �� 
TargetType" ��6    	 �� Triggers	 5k      �� 5l   "   
 �� Binding
 6.   �� *
HasItems+ �� ElementName$ dataGrid�� �� Value	 False6a   .a�5m   &   ��  �� Property � �� �� Value$
 Auto=�6T   5n   #   5p      .��5r      &   System.Windows.Controls.DataGrid  -   5          Name$ dataGridReport��5r      $y�Visible=�67   $   HorizontalScrollBarVisibility$
 Auto=�5s   !   $
��Auto��5t   !      HorizontalGridLinesBrush �#263859   AutoGenerateColumns .     GridLinesVisibility$ 
Horizontal=�5w   !      CanUserAddRows .     CanUserDeleteRows .     
IsReadOnly .     CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .  ���	#FF17223B$��	20 0 20 0q�5   !   $��1q�5�         SelectionMode$ Single=�6)      SelectionUnit$ FullRow=�6@    � 	Resources 5�         MaterialDesignDataGrid �� BasedOn*  $System.Windows.Controls.DataGridCell  MaterialDesignDataGridCell P� Property P� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader & "MaterialDesignDataGridColumnHeader��%�  '        7   '  i     7  '      7  '��     �� 5�      "  6$   8   .�5�   "   ��  �@ GridLinesVisibility$ 
Horizontal=�6H   5�      �� 5�      "  6$   8   .�5�   "   ��  �@�FocusVisualStyle 6E   Z� �� 5�   "    � �� �	#FF17223B6?   �� 5�   "    � ��
 �����6?   �� 5�   "    � ��$ 0q�6D   �� 5�   "    � �� �#2638596@   �� 5�   "    �@��TextAlignment$ Center=�6L   	 5�   "   P� 5�   &    �@ 
IsSelected . . �5�   *   ��  � �� �#2638596G   5�   '   5�      �� 5�      "  6$   8   .�5�   "   ��  � �� �#2638596?   �� 5�   "    � ��
 �����6?   �� 5�   "    � ��$ 44��6;   �� 5�   "    � ��$ Center=�6O   �� 5�   "    � ��$ Center=�6M   	 5�   "   5�      �� 5�      " ��6$   .�5�   "   ��  � �� �#2638596?   5�      5�          Columns  5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn!   Header$	! KOD��61   "   
IsReadOnly" . #   Width5  /System.Windows.Controls.DataGridLengthConverter$# 80 6g   1  +System.Windows.Controls.DataGridBoundColumn$   Binding$ 6>   �� *id+6g     5�      $! YET. BERUVCHI��61   " . $
# 150* 6v   $ 6H   �� *	deliver+6v     5�      $! MAHSULOT��61   " . $
# 300* 6q   $ 6C   �� *	pr_name+6q     5�      $! ZAVOD��61   " . $
# 150* 6o   $ 6@   �� *
preparer+6o     5�      $! O'LCHOV��61   " . $# 90 6t   $ 6B   �� *measurement+6t     5�      $! TAN NARX��61   " . $	# 130 6r   $ 6C   �� *
tan_narx+6r     5�      $! SOTISH NARX��61   " . $	# 130 6x   $ 6F   �� *sotish_narx+6x     5�      $! VAL. O'LCHOV��61   " . $	# 120 6t   $ 6G   �� *val_ul+6t     5�      $! QAYT. MIQDOR��61   " . $	# 130 6y   $ 6G   �� *qayt_miqdor+6y   5�      5�      5�      5�      � 5�   
   $��2��6   .+�5�      �� -	   5        % ��Name$% 	BtnAsosiy��5�      .��5�      �� $ 
Horizontal=�6   $	��150��66   .+�5�      �� $��Resources/Asosiy.png��6   $��24��6:   $��24��6E   $��13 0 0 0q�6Q   �� 5�      $��Asosiy��6    $��25 0 0 0q�6.   5�      ��5�      �� 5�      " ��6   .�5�      ��  � ��$	 155��62   �� 5�       � ��$ 60��63   �� 5�       � ��$ Segoe UI)�67   �� 5�       � ��$ 18(�65   �� 5�       � ��
 �����67   �� 5�       � �� �#26385967   �� 5�       � �� �#26385968   �� 5�       � ��$
 Left=�6@   �� 5�       � ��$ 1q�6<   �� 5�       � ��$ 	20 0 0 45q�63   �� 5�       � �� 5�      �� 5�   "   & �� 
TargetType"& ��62   .R�5�   &   �� ' �� CornerRadius$' 3��5�   -   #��z��5�   -   #��z��5�   -   ._�5�   *   �� $��Center=�6;   $��Center=�6V   5�   +   5�   '   5�   #   5�      	 5�      P� 5�       �@J�IsMouseOver . . �5�   "   ��  � ��$ 2q�6D   �� 5�   "    � �� �#455A646@   �� 5�   "    � �� �#455A646?   5�      5�      5�      5�      5�      5�      