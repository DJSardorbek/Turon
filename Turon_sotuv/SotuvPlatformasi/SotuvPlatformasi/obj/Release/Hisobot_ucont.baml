   M S B A M L   `   `   `  ���� K  GSotuvPlatformasi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:SotuvPlatformasiSotuvPlatformasi  $    SotuvPlatformasi.Hisobot_ucont   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     0localclr-namespace:SotuvPlatformasi     -   .��� ���#17223B.+�� $��0 30 0 0q�  � RowDefinitions  �� $
��Auto ��� $��* ��� $
��Auto �.+�� $��0��$��0 0 0 20q� � ColumnDefinitions �� $
��1.5* ��� $��* �.+��� $��0�� �� Orientation$ 
Horizontal=�.+��� $��Jami Savdo uz:��
�������$��Segoe UI)�$
��20px(�$��Center=�$��	30 0 20 0q��� -    ��Name$ txtJamiSavdo_uz��$	��0.0�����#7BC9EB$��Segoe UI)�$
��30px(�$��Center=��� $��Jami Savdo $:��
�������$��Segoe UI)�$
��20px(�$��Center=�$��
200 0 20 0q��� -   $ txtJamiSavdo_dol��$	��0.0�����#7BC9EB$��Segoe UI)�$
��30px(�$��Center=��� $��Right=�$��1��$ 
Horizontal=�.+�(  "System.Windows.Controls.DatePicker  -     Name$ DatePickerDan�� � OverridesDefaultStyle . 
������ $��0 0 10 0q�
��������� $��Dan...��
�������$��Center=�$��Center=�$��Segoe UI)�$
��12px(�$��	0 0 123 0q�  -   $ DatePickerGacha�� . 
������ $��0 0 10 0q�
��������� $��Gacha...��
�������$��Center=�$��Center=�$��Segoe UI)�$
��12px(�$��0 0 32 0q�� $��0 0 0 10q�$��1��$��Stretch=�$
��Auto��.+��� -    ��Name$ scrollViewer��$��Disabled=�����  �� 
TargetType" �� �� Triggers �� 	 �� Binding	 �� *
HasItems+
 �� ElementName$
 dataGridHisobot�� �� Value	 False.a���  �� Property � �� �� Value$
 Auto=�.��&   System.Windows.Controls.DataGrid  -     Name$ dataGridHisobot��$   HorizontalScrollBarVisibility$
 Auto=�$
��Auto��   HorizontalGridLinesBrush �#263859   AutoGenerateColumns .     GridLinesVisibility$ 
Horizontal=�   CanUserAddRows .     CanUserDeleteRows .     
IsReadOnly .    CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .  ���	#FF17223B$��1q�   SelectionMode$ Single=�   SelectionUnit$ FullRow=�$��
25 0 20 10q� � 	Resources    MaterialDesignDataGrid �� BasedOn*  $System.Windows.Controls.DataGridCell  MaterialDesignDataGridCell P� Property P� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader & "MaterialDesignDataGridColumnHeader��%%  '        7   '  D     7  '  N    7  '�� �    �� "  8   .���  �@ GridLinesVisibility$ 
Horizontal=��� "  8   .���  �@�FocusVisualStyle Z� ��  � �� �	#FF17223B��  � ��
 �������  � ��$ 0q���  � �� �#263859��  �@��TextAlignment$ Center=� P�  �@ 
IsSelected . . ���  � �� �#263859�� "  8   .���  � �� �#263859��  � ��
 �������  � ��$ 44����  � ��$ Center=� �� " ��.���  � �� �#263859   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn    Header$  NAQD PUL��!   Width5  /System.Windows.Controls.DataGridLengthConverter$	! 110 1  +System.Windows.Controls.DataGridBoundColumn"   Binding" �� *naqd+  $  PLASTIK��$	! 110 " �� *	plastik+  $  PUL O'TKAZMA��$	! 130 " �� *
transfer+  $  	NASIYA uz��$	! 120 " �� *
nasiya_som+  $  NASIYA $��$	! 120 " �� *nasiya_dollar+  $  	VALYUTA $��$	! 100 " �� *
currency+  $  	SKIDKA uz��$	! 100 " �� *
skidka_som+  $  SKIDKA $��$	! 100 " �� *skidka_dollar+  $  	QAYTUV uz��$	! 100 " �� *
qaytuv_som+  $  QAYTUV $��$	! 100 " �� *qaytuv_dollar+  $  XODIMLAR��$
! 200* " �� *
employee+�� $��2��.+��� -   # ��Name$# 	BtnAsosiy��.���� $ 
Horizontal=�$	��150��.+��� $��Resources/Asosiy.png��$��24��$��24��$��13 0 0 0q��� $��Asosiy��$��25 0 0 0q����� " ��.���  � ��$	 155����  � ��$ 60����  � ��$ Segoe UI)���  � ��$ 18(���  � ��
 �������  � �� �#263859��  � �� �#263859��  � ��$
 Left=���  � ��$ 1q���  � ��$ 	20 0 0 30q���  � �� �� $ �� 
TargetType"$ ��.R��� % �� CornerRadius$% 3��#��z��#��z��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ���  � ��$ 2q���  � �� �#455A64��  � �� �#455A64