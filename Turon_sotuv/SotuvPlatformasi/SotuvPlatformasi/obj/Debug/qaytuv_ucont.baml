   M S B A M L   `   `   `  ����K  GSotuvPlatformasi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:SotuvPlatformasiSotuvPlatformasi  5      #    SotuvPlatformasi.qaytuv_ucont   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:SotuvPlatformasi     5      b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      -   5        .��5
      � ���#17223B  � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      $
��1.5* �6#   �� 5      $��* �6#   .+�5      � $��0��6   .+�5      �� -   5         ��Name$ 	txtSearch��5         	TextBoxes#��[  6i   $��50��6�   $��	20 20 0 0q�6�   � 5      ���#17223B$��20��6,   $	��180��68   $��
50 -30 0 0q�6D   $
��Left=�6X   .+�5      �� $��Maxsulot nomi ... Chek...��6    ���#6B778C$��Segoe UI)�6V   $
��16px(�6l   $��Center=�6|   $��Center=�6�   5      '  !MaterialDesignThemes.Wpf.PackIcon  5      ���#6B778C   Kind$ Search=�6?   $��0 20 0 0q�6M   $��25��6_   $��25��6j   $��Right=�6v   $��Center=�6�   5      � 5!      $��1��6   $��50��6#   $��
40 20 20 0q�6/   .+�5#      ��  �� CornerRadius$ 20��6   ���#263859$��1q�6A   ._�5$      (  "System.Windows.Controls.DatePicker  -   5          Name$ dateTimePicker1��5$   !   $��40��6u   $��	30 0 30 0q�6�   $��Center=�6�   $��Center=�6�   
�������$��Segoe UI)�6�   $
��24px(�6�   5%      5&      � 5'      ���#17223B$��20��6,   $��50��68   $
��Left=�6C   $��30 -50 0 0 q�6^   .+�5(      �� $
��Sana��6    ���#6B778C$��Segoe UI)�6A   $
��15px(�6W   $��Center=�6g   $��Center=�6�   5)      5*      5+      � 5.   
   $��1��6   $��	0 30 0 10q�6   $��Stretch=�6/   $
��Auto��6M   .+�5/      �� -   5         ��Name$ scrollViewer��5/      $��Disabled=�61   ��50      �� 51       �� 
TargetType" ��6    �� Triggers 52      �� 53      	 �� Binding	 6*   �� *
HasItems+
 �� ElementName$
 dataGridQaytuv�� �� Value	 False6c   .a�54   "   ��  �� Property � �� �� Value$
 Auto=�6P   55      57      .��59      &   System.Windows.Controls.DataGrid  -   5          Name$ dataGridQaytuv��59      $   HorizontalScrollBarVisibility$
 Auto=�5:      $
��Auto��5;         HorizontalGridLinesBrush �#263859   AutoGenerateColumns .     GridLinesVisibility$ 
Horizontal=�5>         CanUserAddRows .     CanUserDeleteRows .     
IsReadOnly .    CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .  ���	#FF17223B$��1q�5F         SelectionMode$ Single=�6)      SelectionUnit$ FullRow=�6@   $��
20 0 20 10q�5G       � 	Resources 5H        MaterialDesignDataGrid �� BasedOn*  $System.Windows.Controls.DataGridCell  MaterialDesignDataGridCell P� Property P� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader & "MaterialDesignDataGridColumnHeader��%�  '        7  '  i     7  '      7  '��     �� 5I      "  6    8   .�5J      ��  �@ GridLinesVisibility$ 
Horizontal=�6D   5K      �� 5L      "  6    8   .�5M      ��  �@�FocusVisualStyle 6A   Z� �� 5N       � �� �	#FF17223B6;   �� 5O       � ��
 �����6;   �� 5P       � ��$ 0q�6@   �� 5Q       � �� �#2638596<   �� 5R       �@��TextAlignment$ Center=�6H    5S      P� 5T   "    �@ 
IsSelected . . �5U   &   ��  � �� �#2638596C   5V   #   5Y      �� 5Z      "  6    8   .�5[      ��  � �� �#2638596;   �� 5\       � ��
 �����6;   �� 5]       � ��$ 44��67   �� 5^       � ��$ Center=�6K   �� 5_       � ��$ Center=�6I    5`      5c      �� 5e      " ��6    .�5f      ��  � �� �#2638596;   5g      5h         Columns 5j      0  *System.Windows.Controls.DataGridTextColumn  5k      ,  &System.Windows.Controls.DataGridColumn    Header$
  CHEK��6-   !   Width5  /System.Windows.Controls.DataGridLengthConverter$! 80 6T   1	  +System.Windows.Controls.DataGridBoundColumn" 	  Binding" 6;   �� *chek+6T     5l      $  MAXSULOT��6-   $
! 300* 6X   " 6?   �� *name+6X     5m      $  NARXI��6-   $	! 100 6V   " 6<   �� *price+6V     5n      $  VAL. O'LCHOV��6-   $	! 120 6^   " 6C   �� *val_ul+6^     5o      $  SOTILGAN MIQDOR��6-   $	! 150 6c   " 6F   �� *
quantity+6c     5p      $  SUMMA��6-   $	! 120 6V   " 6<   �� *total+6V     5q      $  XODIM��6-   $
! 200* 6[   " 6<   �� *
first_name+6[     5r      $
  SANA��6-   $	! 200 6T   " 6;   �� *date+6T   4
  .System.Windows.Controls.DataGridTemplateColumn
  5s        5t      �� 5u   "   $��QAYTUV��6,   # �� TextAlignment$# Center=�6:   $	��120��6Q   $ 
  CellTemplate$ 5w      �� 5x   "   .R�5y   &   �� % ��Name$% 	btnQaytuv��6>   -   5        $��Qaytuv��5y   -   ��5z   *   �� 5{   .   " ��64   .�5|   2   ��  � ��$	 100��6J   �� 5}   2    � ��$ 24��6K   �� 5~   2    � ��$ Segoe UI)�6O   �� 5   2    � ��$
 10px(�6M   �� 5�   2    � ��
 �����6O   �� 5�   2    � �� �#F759406O   �� 5�   2    � �� 5�   6   �� 5�   :   & �� 
TargetType"& ��6J   .R�5�   >   �� $ 3��5�   E   #��z��5�   E   #��z��5�   E   ._�5�   B   �� $��Center=�6S   $��Center=�6n   5�   ?   5�   ;   5�   3    5�   2   P� 5�   6    �@J�IsMouseOver . . �5�   :   ��  � �� �#F599606W   5�   7   5�   /   5�   '   5�   #   5�      5�      5�      5�      �� 5�   
   $��2��6   .+�5�      �� -   5        $% 	BtnAsosiy��5�      .��5�      �� ' �� Orientation$' 
Horizontal=�6   $	��150��66   .+�5�      �� $��Resources/Asosiy.png��6   $��24��6:   $��24��6E   $��13 0 0 0q�6Q   �� 5�      $��Asosiy��6    $��25 0 0 0q�6.   5�      ��5�      �� 5�      " ��6   .�5�      ��  � ��$	 155��62   �� 5�       � ��$ 60��63   �� 5�       � ��$ Segoe UI)�67   �� 5�       � ��$ 18(�65   �� 5�       � ��
 �����67   �� 5�       � �� �#26385967   �� 5�       � �� �#26385968   �� 5�       � ��$
 Left=�6@   �� 5�       � ��$ 1q�6<   �� 5�       � ��$ 	20 0 0 30q�63   �� 5�       � �� 5�      �� 5�   "   "& ��62   .R�5�   &   �� $ 3��5�   -   #��z��5�   -   #��z��5�   -   ._�5�   *   �� $��Center=�6;   $��Center=�6V   5�   +   5�   '   5�   #   5�       5�      P� 5�       �@J�IsMouseOver . . �5�   "   ��  � ��$ 2q�6D   �� 5�   "    � �� �#455A646@   �� 5�   "    � �� �#455A646?   5�      5�      5�      5�      5�      5�      