   M S B A M L   `   `   `  ���� K  GSotuvPlatformasi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:SotuvPlatformasiSotuvPlatformasi  0    *SotuvPlatformasi.ReturnDeliverReport_ucont   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  :d2http://schemas.microsoft.com/expression/blend/2008  *localclr-namespace:SotuvPlatformasi  .��� ���#17223B  � RowDefinitions  �� $
��Auto ��� $��* ��� $
��Auto �.+�� $��0�� � ColumnDefinitions �� $
��1.5* ��� $��* �.+�� $��0��$��0 20 0 0q� �� $
��1.4* ��� $��* ��� $	��160 �.+���  �� Orientation$ 
Horizontal=�$��0��.+��� $��	Summa uz:��
�������$��Segoe UI)�$
��20px(�$��Center=�$��	30 0 20 0q��� -    ��Name$ txtSumma_uz��$	��0.0�����#7BC9EB$��Segoe UI)�$
��30px(�$��Center=��� $ 
Horizontal=�$��1��.+��� $��Summa $:��
�������$��Segoe UI)�$
��20px(�$��Center=�$��0 0 20 0q��� -   $ txtSumma_dol��$	��0.0�����#7BC9EB$��Segoe UI)�$
��30px(�$��Center=��� $ 
Horizontal=�$��2��.+��� $��Kurs:��
�������$��Segoe UI)�$
��20px(�$��Center=�$��0 0 20 0q��� -   $ txtKurs��$	��0.0�����#7BC9EB$��Segoe UI)�$
��30px(�$��Center=��� $��1��$��0 20 0 0q�.+��� $��Right=�$��1��$ 
Horizontal=�$��0 10 0 0q�.+�(  "System.Windows.Controls.DatePicker  -     Name$ DatePickerDan�� � OverridesDefaultStyle . 
������ $��0 0 10 0q�
��������� $��Dan...��
�������$��Center=�$��Center=�$��Segoe UI)�$
��12px(�$��	0 0 123 0q�  -   $ DatePickerGacha�� . 
������ $��0 0 10 0q�
��������� $��Gacha...��
�������$��Center=�$��Center=�$��Segoe UI)�$
��12px(�$��0 0 32 0q�� $��1��.+�� -    �Name$ Basket��$��	0 30 0 10q�$��1��$��Stretch=�$
��Auto��$y�Visible=�.+��� -    ��Name$ scrollViewer��$��Disabled=�����  �� 
TargetType" ��	 �� Triggers	 �� 
 �� Binding
 �� *
HasItems+ �� ElementName$ dataGrid�� �� Value	 False.a���  �� Property � �� �� Value$
 Auto=�.��&   System.Windows.Controls.DataGrid  -     Name$ dataGridReport��$y�Visible=�$   HorizontalScrollBarVisibility$
 Auto=�$
��Auto��   HorizontalGridLinesBrush �#263859   AutoGenerateColumns .     GridLinesVisibility$ 
Horizontal=�   CanUserAddRows .     CanUserDeleteRows .     
IsReadOnly .     CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .  ���	#FF17223B$��	20 0 20 0q�$��1q�   SelectionMode$ Single=�   SelectionUnit$ FullRow=� � 	Resources    MaterialDesignDataGrid �� BasedOn*  $System.Windows.Controls.DataGridCell  MaterialDesignDataGridCell P� Property P� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader & "MaterialDesignDataGridColumnHeader��%?  '        7   '  D     7  '  N    7  '�� �    �� "  8   .���  �@ GridLinesVisibility$ 
Horizontal=��� "  8   .���  �@�FocusVisualStyle Z� ��  � �� �	#FF17223B��  � ��
 �������  � ��$ 0q���  � �� �#263859��  �@��TextAlignment$ Center=�	 P�  �@ 
IsSelected . . ���  � �� �#263859�� "  8   .���  � �� �#263859��  � ��
 �������  � ��$ 44����  � ��$ Center=���  � ��$ Center=�	 �� " ��.���  � �� �#263859    Columns  0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn!   Header$	! KOD��"   
IsReadOnly" . #   Width5  /System.Windows.Controls.DataGridLengthConverter$# 80 1  +System.Windows.Controls.DataGridBoundColumn$   Binding$ �� *id+  $! YET. BERUVCHI��" . $
# 150* $ �� *	deliver+  $! MAHSULOT��" . $
# 300* $ �� *	pr_name+  $! ZAVOD��" . $
# 150* $ �� *
preparer+  $! O'LCHOV��" . $# 90 $ �� *measurement+  $! TAN NARX��" . $	# 130 $ �� *
tan_narx+  $! SOTISH NARX��" . $	# 130 $ �� *sotish_narx+  $! VAL. O'LCHOV��" . $	# 120 $ �� *val_ul+  $! QAYT. MIQDOR��" . $	# 130 $ �� *qayt_miqdor+� $��2��.+��� -	   % ��Name$% 	BtnAsosiy��.���� $ 
Horizontal=�$	��150��.+��� $��Resources/Asosiy.png��$��24��$��24��$��13 0 0 0q��� $��Asosiy��$��25 0 0 0q����� " ��.���  � ��$	 155����  � ��$ 60����  � ��$ Segoe UI)���  � ��$ 18(���  � ��
 �������  � �� �#263859��  � �� �#263859��  � ��$
 Left=���  � ��$ 1q���  � ��$ 	20 0 0 45q���  � �� �� & �� 
TargetType"& ��.R��� ' �� CornerRadius$' 3��#��z��#��z��._��� $��Center=�$��Center=�	 P�  �@J�IsMouseOver . . ���  � ��$ 2q���  � �� �#455A64��  � �� �#455A64