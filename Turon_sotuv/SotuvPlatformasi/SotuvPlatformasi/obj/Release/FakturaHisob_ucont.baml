   M S B A M L   `   `   `  ���� K  GSotuvPlatformasi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:SotuvPlatformasiSotuvPlatformasi  )    #SotuvPlatformasi.FakturaHisob_ucont   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  *localclr-namespace:SotuvPlatformasi  -   .��� ���#17223B.+�� $��0 30 0 0q�  � RowDefinitions  �� $
��Auto ��� $��* ��� $
��Auto �.+�� $��0�� � ColumnDefinitions �� $��* ��� $��* �.+��� $��0��$
��Left=�$��20 0 0 0q�.+��� $��0 0 20 0q�$	��350��$��80�� �� RadiusX$ 4�� �� RadiusY$ 4�����#6B778C$��1��$��Right=�� $	��340��$��
0 -80 30 0q�$��Right=�.+��� $	��100�����#17223B$��Center=�$��Umumiy farq��
�������$��0 -80 220 0q� �� TextAlignment$ Center=�$��Segoe UI)�$
��15px(��� -    ��Name$ txtUmumiyFarq��$	��0.0��
�������$��Center=�$��Center=�$��Segoe UI)�$
��25px(�� $��1��.+��� -    ��Name$ comboFilial��
�������   ComboBoxTest2#��[  $	��350��$��70��$��Right=�$��	0 0 20 20q�$��Segoe UI)�$
��23px(�� $��	0 20 0 10q�$��1��$��Stretch=�$
��Auto��  �� $��* ��� $
��Auto �.+��� -    ��Name$ scrollViewer��$��Disabled=�����  �� 
TargetType" ��	 �� Triggers	 �� 
 �� Binding
 �� *
HasItems+ �� ElementName$ dataGridFakturaHisob�� �� Value	 False.a���  �� Property � �� �� Value$
 Auto=�.��&   System.Windows.Controls.DataGrid  -     Name$ dataGridFakturaHisob��$   HorizontalScrollBarVisibility$
 Auto=�$
��Auto��   HorizontalGridLinesBrush �#263859   AutoGenerateColumns .     GridLinesVisibility$ 
Horizontal=�   CanUserAddRows .     CanUserDeleteRows .     
IsReadOnly .    CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .  ���	#FF17223B$��1q�   SelectionMode$ Single=�   SelectionUnit$ FullRow=�$��
20 0 20 10q� � 	Resources   MaterialDesignDataGrid �� BasedOn*  $System.Windows.Controls.DataGridCell  MaterialDesignDataGridCell P� Property P� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader & "MaterialDesignDataGridColumnHeader��%%  '        7  '  D     7  '  N    7  '�� �    �� "  8   .���  �@ GridLinesVisibility$ 
Horizontal=��� "  8   .���  �@�FocusVisualStyle Z� ��  � �� �	#FF17223B��  � ��
 �������  � ��$ 0q���  � �� �#263859��  �@��TextAlignment$ Center=�	 P�  �@ 
IsSelected . . ���  � �� �#263859�� "  8   .���  � �� �#263859��  � ��
 �������  � ��$ 44����  � ��$ Center=�	 �� " ��.���  � �� �#263859    Columns  0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn!   Header$	! KOD��"   Width5  /System.Windows.Controls.DataGridLengthConverter$	" 100 1  +System.Windows.Controls.DataGridBoundColumn#   Binding# �� *id+  $! SUMMA��$
" 200* # �� *summa+  $
! SANA��$	" 200 # �� *date+  $! STATUS��$
" 200* # �� *	message+  $! FARQI ( + ; - )��$
" 200* # �� *
difference+� -   $ �Name$$ GridPage��$��1��$	��200��$y�	Collapsed=� �� �� .+��� -   % ��Name$% 
btnPrevius��$��0��{�.  & J� Opacity$	& 0.5Q�.���� .+�'  !MaterialDesignThemes.Wpf.PackIcon  '   Kind$' 	ArrowLeft=�$��25��$��25������ " ��.���  � ��$ 50����  � ��$ 50����  � �� �#263859��  � �� �#263859��  � ��
 �������  � �� �� ( �� 
TargetType"( ��.R��� ) �� CornerRadius$) 50��#��z��#��z��._��� $��Center=�$��Center=�	 P�  �@J�IsMouseOver . . ���  � ��$ 2q���  � �� �#455A64��  � �� �#455A64�� -   $% btnNext��$��1��.���� .+�  $' 
ArrowRight=�$��25��$��25������ " ��.���  � ��$ 50����  � ��$ 50����  � �� �#263859��  � �� �#263859��  � ��
 �������  � �� �� "( ��.R��� $) 50��#��z��#��z��._��� $��Center=�$��Center=�	 P�  �@J�IsMouseOver . . ���  � ��$ 2q���  � �� �#455A64��  � �� �#455A64�� $��2��.+��� -	   $% 	BtnAsosiy��.���� * �� Orientation$* 
Horizontal=�$	��150��.+��� $��Resources/Asosiy.png��$��24��$��24��$��13 0 0 0q��� $��Asosiy��$��25 0 0 0q����� " ��.���  � ��$	 155����  � ��$ 60����  � ��$ Segoe UI)���  � ��$ 18(���  � ��
 �������  � �� �#263859��  � �� �#263859��  � ��$
 Left=���  � ��$ 1q���  � ��$ 20 -10 0 40q���  � �� �� "( ��.R��� $) 3��#��z��#��z��._��� $��Center=�$��Center=�	 P�  �@J�IsMouseOver . . ���  � ��$ 2q���  � �� �#455A64��  � �� �#455A64