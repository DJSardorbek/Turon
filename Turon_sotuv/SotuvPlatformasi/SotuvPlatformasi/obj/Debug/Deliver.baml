   M S B A M L   `   `   `  ����K  GSotuvPlatformasi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:SotuvPlatformasiSotuvPlatformasi  5   	       SotuvPlatformasi.Deliver   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   *localclr-namespace:SotuvPlatformasi  5   	   -   5          � Title$  Deliver��5	   	   $	��450��6   $	��600��6&    � WindowStyle$
 None=�5
   	    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	   ���#263859$��2q�5   	    � 	Resources 5       �� 
TargetType   	txtNasiya �� Property �� Value-  'System.Windows.Input.KeyboardNavigation �� 
TargetType	 �� CornerRadius
 �� Padding  ControlLightColor �� Color ��Name��%�  &	        �� 5   
   " ��6   .�5      ��  �@J�SnapsToDevicePixels . 5   )   �� 5       �@�OverridesDefaultStyle . 5   *   �� 5       �@ TabNavigation$
 None=�5   )   �� 5       �@�FocusVisualStyle 5   )   Z� �� 5       � ��$	 120��5   *   �� 5       � ��$ 50��5   )   �� 5        �@J�	AllowDrop . 5!   )   �� 5"       � ��
 �����6+   �� 5#       � ��$ Segoe UI)�6+   �� 5$       � ��$
 16px(�6)   �� 5%       �@��
CaretBrush
 �����6+   �� 5&       � �� 5'      �� 5(      " ��6&   .R�5*      �� $��Border��6!   $	 10��5+      $
 30 10 10 10q�5,      $��0q�5-      ��5.      �� 5/   "   # �  62   ��51      �� 52   "   $ #6B778C��62   ._�54      �� $ PART_ContentHost��66   $��0q�6+   66   55      57      59      5:      5<      .��5=      � ���#17223B � RowDefinitions 5@   
   �� 5A      $
��Auto �6   �� 5B      $��* �6   .+�5E   
   � $��0��6    � ColumnDefinitions 5G      �� 5H      $��* �6#   �� 5I      $
��Auto �6#   .+�5K      � $��0��6   .+�5L      �� -   5         ��Name$ 	txtSearch��5L        	TextBoxes#��[ 5N      $��40��5O      $��
20 20 20 0q�6"   � 5P      ���#17223B$��20��5R      $	��140��6   $��
50 -25 0 0q�5S      $
��Left=�5T      .+�5U      �� $��Yetkazib beruvchi��5V      ���#6B778C$��Segoe UI)�5X      $
��16px(�5Y      $��Center=�5Z      $��Center=�5[      5\      '  !MaterialDesignThemes.Wpf.PackIcon  5]      ���#6B778C   Kind$ Search=�5_      $��	0 20 30 0q�5`      $��25��5a      $��25��6   $��Right=�5b      $��Center=�5c      5d      � 5f      $��1��6   .+�5g      �� -   5         ��Name$ btnExit��5h      $��25��5j      $��25��6    $	��Top=�5k      $��Right=�5l      $��0 5 5 5q�5m      ���#17223B.��5o      �� $��Resources/cancel.png��5p      $��20��68   $��20��6C   $	��Top=�6O   $��Right=�6g   ��5q      �� 5r      " ��6    .�5s      ��  � ��$ 0q�6@    �� Triggers 5t      P� 5u   "    P� Property �@J�IsMouseOver P� Value . . �5v   &   ��  � �� �#2638596C   5w   #   5y      5{      5}      5~      � 5�   
   $��1��6   .+�5�      � $��0��6   $��	0 30 0 10q�6    $��Stretch=�63   $
��Auto��6Q   .+�5�      �� -   5        $ scrollViewer��5�      $��Disabled=�65   ��5�      �� 5�      " ��6     5�      �� 5�   "    �� Binding 6.   �� *
HasItems+ �� ElementName$ dataGridClient�� �� Value	 False6g   .a�5�   &   ��  � ��$
 Auto=�6T   5�   #   5�      .��5�      &   System.Windows.Controls.DataGrid  -   5          Name$ dataGridClient��5�      $   HorizontalScrollBarVisibility$
 Auto=�5�      $
��Auto��5�         HorizontalGridLinesBrush �#263859   AutoGenerateColumns .     GridLinesVisibility$ 
Horizontal=�5�         CanUserAddRows .     CanUserDeleteRows .     
IsReadOnly .     CanUserReorderColumns  .  !   CanUserResizeColumns! .  "   CanUserResizeRows" .  ���	#FF17223B$��1q�5�      #   SelectionMode$# Single=�6)   $   SelectionUnit$$ FullRow=�6@   $��
20 0 20 10q�5�       5�        MaterialDesignDataGrid% �� BasedOn*  $System.Windows.Controls.DataGridCell  MaterialDesignDataGridCell=  7System.Windows.Controls.Primitives.DataGridColumnHeader & "MaterialDesignDataGridColumnHeader��%%  '        7  '  i     7  '  �    7  '�� �    �� 5�      "  6$   8%   .�5�   "   ��  �@ GridLinesVisibility$ 
Horizontal=�6H   5�      �� 5�      "  6$   8%   .�5�   "   ��  �@�FocusVisualStyle 6E   Z� �� 5�   "    � �� �	#FF17223B6?   �� 5�   "    � ��
 �����6?   �� 5�   "    � ��$ 0q�6D   �� 5�   "    � �� �#2638596@    5�   "   P� 5�   &    �@ 
IsSelected . . �5�   *   ��  � �� �#2638596G   5�   '   5�      �� 5�      "  6$   8%   .�5�   "   ��  � �� �#2638596?   �� 5�   "    � ��
 �����6?   �� 5�   "    � ��$ 44��6;   �� 5�   "    � ��$ Center=�6M    5�   "   5�      �� 5�      " ��6$   .�5�   "   ��  � �� �#2638596?   5�      5�      &   Columns& 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn'   Header$	' KOD��5�   !   (   Width5  /System.Windows.Controls.DataGridLengthConverter$	( 100 5�   !   1	  +System.Windows.Controls.DataGridBoundColumn) 	  Binding) 5�   !   �� *id+5�   !     5�      $' YETKAZIB BERUVCHI��5�   !   $
( 200* 5�   !   ) 5�   !   �� *name+5�   !   4
  .System.Windows.Controls.DataGridTemplateColumn
  5�      ' 5�   "   �� 5�   &   $��TANLASH��60   $	��150��6?   * �� TextAlignment$* Center=�6K   + 
  CellTemplate+ 5�   "   �� 5�   &   .R�5�   *   �� $ 	btnChoose��5�   -   -   5        $��+��5�   -   ��5�   .   �� 5�   2   " ��68   .�5�   6   ��  � ��$	 120��6N   �� 5�   6    � ��$ 30��6O   �� 5�   6    � �� �#0057926S   �� 5�   6    � ��
 �����6S   �� 5�   6    � ��$ Segoe UI)�6S   �� 5�   6    � ��$
 18px(�6Q   �� 5�   6    � �� 5�   :   �� 5�   >   " ��6N   .R�5�   B   �� $	 3��5�   E   #��z��5�   E   #��z��5�   E   ._�5�   F   �� $��Center=�6W   $��Center=�6r   5�   G   5�   C   5�   ?   5�   7    5�   6   P� 5�   :    �@J�IsMouseOver . . �5�   >   ��  � �� �#0057F46[   5�   ;   5�   3   5�   +   5�   '   5�      5�      5�      5�      5�      5�      5�      