   M S B A M L   `   `   `  ���� K  GSotuvPlatformasi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:SotuvPlatformasiSotuvPlatformasi  !    SotuvPlatformasi.Staff_list   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     0localclr-namespace:SotuvPlatformasi     -     N� Title$  
Staff_list��.��� ���#17223B.+�� $��0 20 0 0q� � RowDefinitions �� $��* ��� $
��Auto �.+�� $��0 0 0 10q�$��0��$��Stretch=�$
��Auto��$
��Auto��.+��� -    ��Name$ scrollViewer��$��Disabled=�����  �� 
TargetType" �� �� Triggers ��  �� Binding �� *
HasItems+ �� ElementName$ dataGridStaffs�� �� Value	 False.a���  �� Property � ��	 �� Value$
	 Auto=�.��&   System.Windows.Controls.DataGrid  -   
  Name$
 dataGridStaffs��$   HorizontalScrollBarVisibility$
 Auto=�$
��Auto��   HorizontalGridLinesBrush �#263859   AutoGenerateColumns .     GridLinesVisibility$ 
Horizontal=�   CanUserAddRows .     CanUserDeleteRows .     
IsReadOnly .    CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .  ���	#FF17223B$��1q�   SelectionMode$ Single=�   SelectionUnit$ FullRow=�$��
20 0 20 10q� � 	Resources    MaterialDesignDataGrid �� BasedOn*  $System.Windows.Controls.DataGridCell  MaterialDesignDataGridCell P� Property P� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader & "MaterialDesignDataGridColumnHeader��%%  '        7   '  D     7  '  N    7  '�� �    �� "  8   .���  �@ GridLinesVisibility$	 
Horizontal=��� "  8   .���  �@�FocusVisualStyle	 Z� ��  � ��	 �	#FF17223B��  � ��
	 �������  � ��$	 0q���  � ��	 �#263859��  �@��TextAlignment$	 Center=� P�  �@ 
IsSelected . . ���  � ��	 �#263859�� "  8   .���  � ��	 �#263859��  � ��
	 �������  � ��$	 Center=���  � ��$	 44�� �� " ��.���  � ��	 �#263859   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$	 ISM��   Width5  /System.Windows.Controls.DataGridLengthConverter$
 200* 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *
first_name+  $ FAMILIYA��$
 200*  �� *	last_name+  $ LAVOZIM��$
 200*  �� *staff+  $ PAROL��$
 200*  �� *
password+4  .System.Windows.Controls.DataGridTemplateColumn   �� $��OLIB TASHLASH��$	��150�� �� TextAlignment$ Center=�    CellTemplate  �� .R���  
 st_del#��[ .���� $��Resources/st_del.png��$��25��$��25���� $��1��.+��� -   ! ��Name$! 	BtnAsosiy��.���� " �� Orientation$" 
Horizontal=�$	��150��.+��� $��Resources/Asosiy.png��$��24��$��24��$��13 0 0 0q��� $��Asosiy��$��25 0 0 0q����� " ��.���  � ��$		 155����  � ��$	 60����  � ��$	 Segoe UI)���  � ��$	 18(���  � ��
	 �������  � ��	 �#263859��  � ��	 �#263859��  � ��$
	 Left=���  � ��$	 1q���  � ��$	 	20 0 0 30q���  � ��	 �� # �� 
TargetType"# ��.R��� $ �� CornerRadius$$ 3��#��z��#��z��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ���  � ��$	 2q���  � ��	 �#455A64��  � ��	 �#455A64