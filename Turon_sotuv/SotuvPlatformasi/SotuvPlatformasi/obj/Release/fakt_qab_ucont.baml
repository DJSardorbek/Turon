   M S B A M L   `   `   `  ���� K  GSotuvPlatformasi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:SotuvPlatformasiSotuvPlatformasi  %    SotuvPlatformasi.fakt_qab_ucont   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     0localclr-namespace:SotuvPlatformasi     -   .��� ���#17223B.+�� $��0 30 0 0q�  � RowDefinitions  �� $��* ��� $
��Auto �.+�� .+�� ���#17223B.+�� $��0 0 0 10q�$��0��$��Stretch=�$
��Auto��.+��� -    ��Name$ scrollViewer��$��Disabled=�����  �� 
TargetType" �� �� Triggers ��  �� Binding �� *
HasItems+ �� ElementName$ dataGridFaktura�� �� Value	 False.a���  �� Property � �� �� Value$
 Auto=�.��&   System.Windows.Controls.DataGrid  -   	  Name$	 dataGridFaktura��$
   HorizontalScrollBarVisibility$

 Auto=�$
��Auto��   HorizontalGridLinesBrush �#263859   AutoGenerateColumns .     GridLinesVisibility$ 
Horizontal=�   CanUserAddRows .     CanUserDeleteRows .     
IsReadOnly .    CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .  ���	#FF17223B$��1q�   SelectionMode$ Single=�   SelectionUnit$ FullRow=� � 	Resources    MaterialDesignDataGrid �� BasedOn*  $System.Windows.Controls.DataGridCell  MaterialDesignDataGridCell P� Property P� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader & "MaterialDesignDataGridColumnHeader��%!  '        7   '  D     7  '  N    7  '�� �    �� "  8   .���  �@ GridLinesVisibility$ 
Horizontal=��� "  8   .���  �@�FocusVisualStyle Z� ��  � �� �	#FF17223B��  � ��
 �������  � ��$ 0q���  � �� �#263859��  �@��TextAlignment$ Center=� P�  �@ 
IsSelected . . ���  � �� �#263859�� "  8   .���  � �� �#263859��  � ��
 �������  � ��$ 44����  � ��$ Center=��� " ��.���  � �� �#263859   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 
QABUL NOMI��   Width5  /System.Windows.Controls.DataGridLengthConverter$
 100* 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *name+  $ TAN SO'M��$	 110  �� *som+  $ SOTISH SO'M��$	 120  �� *
sotish_som+  $ 
TAN DOLLAR��$	 110  �� *dollar+  $ SOTISH DOLLAR��$	 130  �� *sotish_dollar+  $
 SANA��$
 100*  �� *date+4  .System.Windows.Controls.DataGridTemplateColumn   �� $��KO'RISH�� �� TextAlignment$ Center=�$	��100��   CellTemplate �� .R���   ��Name$  kurish��-   .���� $��Resources/kurish.png��$
��24px��$
��14px��$��Center=�$��Center=����� " ��.���  � ��$ 90����  � ��$ 40����  � �� �#7EBC59��  � �� �� ! �� 
TargetType"! ��.R��� " �� CornerRadius$" 3��#��z��#��z��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ���  � �� �#7EF059   �� $��QABUL��$ Center=�$	��100�� �� .R��� $  btnQabul_qilish��-   .���� $��Resources/qabul.png��$
��24px��$
��14px��$��Center=�$��Center=����� " ��.���  � ��$ 90����  � ��$ 40����  � �� �#005792��  � �� �� "! ��.R��� $" 3��#��z��#��z��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ���  � �� �#0057F4   �� $��BEKOR��$ Center=�$	��100�� �� .R��� $  btnBekor��-   .���� $��Resources/bekor.png��$
��24px��$
��14px��$��Center=�$��Center=����� " ��.���  � ��$ 90����  � ��$ 40����  � �� �#F75940��  � �� �� "! ��.R��� $" 3��#��z��#��z��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ���  � �� �#FF7140�� $��1��.+��� -   $  	BtnAsosiy��.���� # �� Orientation$# 
Horizontal=�$	��150��.+��� $��Resources/Asosiy.png��$��24��$��24��$��13 0 0 0q��� $��Asosiy��$��25 0 0 0q����� " ��.���  � ��$	 155����  � ��$ 60����  � ��$ Segoe UI)���  � ��$ 18(���  � ��
 �������  � �� �#263859��  � �� �#263859��  � ��$
 Left=���  � ��$ 1q���  � ��$ 0 0 0 30q���  � �� �� "! ��.R��� $" 3��#��z��#��z��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ���  � ��$ 2q���  � �� �#455A64��  � �� �#455A64