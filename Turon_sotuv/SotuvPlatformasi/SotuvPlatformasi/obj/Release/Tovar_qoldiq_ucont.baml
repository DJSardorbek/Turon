   M S B A M L   `   `   `  ���� K  GSotuvPlatformasi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:SotuvPlatformasiSotuvPlatformasi  )    #SotuvPlatformasi.Tovar_qoldiq_ucont   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  :d2http://schemas.microsoft.com/expression/blend/2008  *localclr-namespace:SotuvPlatformasi  -     � 	Resources  )   #SotuvPlatformasi.MyBkColorConverter   bkColorCvrt��%   &	          .��� ���#17223B.+�� $��0 40 0 0q� � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+�� $��0�� � ColumnDefinitions �� $��* ��� $
��Auto �.+�� $��1��$��
20 0 25 15q�.+��� -    ��Name$ comboFilial��
�������  ComboBoxTest2#��[ $	��350��$��45��$��Right=�$��Segoe UI)�$
��23px(�� -    �Name$ StackKam��$��0 0 0 10q�$��1��$��Stretch=�$
��Auto�� �� $��* ��� $
��Auto �.+��� -    ��Name$ scrollViewer��$��Disabled=�����  �� 
TargetType" �� �� Triggers ��  �� Binding �� *
HasItems+	 �� ElementName$	 dataGridProductKam��
 �� Value	
 False.a���  �� Property � �� �� Value$
 Auto=�.��&   System.Windows.Controls.DataGrid  -     Name$ dataGridProductKam��$   HorizontalScrollBarVisibility$
 Auto=�$
��Auto��   HorizontalGridLinesBrush �#263859   AutoGenerateColumns .     GridLinesVisibility$ 
Horizontal=�   CanUserAddRows .     CanUserDeleteRows .     
IsReadOnly .    CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .  ���	#FF17223B$��1q�   SelectionMode$ Single=�   SelectionUnit$ FullRow=�$��
25 0 20 10q�    MaterialDesignDataGrid �� BasedOn*  $System.Windows.Controls.DataGridCell  MaterialDesignDataGridCell P� Property P� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader & "MaterialDesignDataGridColumnHeader��%�  '        7  '  D     7  '      7  '�� �    �� "  8   .���  �@ GridLinesVisibility$ 
Horizontal=��� "  8   .���  �@�FocusVisualStyle Z� ��  � �� �#F75940��  � ��$ 0q���  �@��TextAlignment$ Center=� P�  �@ 
IsSelected . . ���  � �� �#263859�� "  8   .���  � �� �#263859��  � ��
 �������  � ��$ 44����  � ��$ Center=� �� " ��.���  � �� �#263859   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$	 KOD��   Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 1  +System.Windows.Controls.DataGridBoundColumn    Binding  �� *
product_id+  $ MAXSULOT��$
 300*   �� *name+  $ O'LCHOV��$	 100   �� *measurement+  $ ZAVOD��$
 200*   �� *
preparer+  $ MIQDOR��$	 150   �� *
quantity+  $ NARXI��$
 150*   �� *price+  $ VAL. O'LCHOV��$	 130   �� *val_ul+  $ 
SHTRIX KOD��$
 200*   �� *	barcode+� -   $ GridPage��$��1��$	��200��$y�	Collapsed=� �� �� .+��� -   ! ��Name$! btnPreviusKam��$��0��{�.  " J� Opacity$	" 0.5Q�.���� .+�'	  !MaterialDesignThemes.Wpf.PackIcon	  # 	  Kind$# 	ArrowLeft=�$��25��$��25������ " ��.���  � ��$ 50����  � ��$ 50����  � �� �#263859��  � �� �#263859��  � ��
 �������  � �� �� $ �� 
TargetType"$ ��.R��� % �� CornerRadius$% 50��#��z��#��z��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ���  � ��$ 2q���  � �� �#455A64��  � �� �#455A64�� -   $! 
btnNextKam��$��1��.���� .+�	  $# 
ArrowRight=�$��25��$��25������ " ��.���  � ��$ 50����  � ��$ 50����  � �� �#263859��  � �� �#263859��  � ��
 �������  � �� �� "$ ��.R��� $% 50��#��z��#��z��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ���  � ��$ 2q���  � �� �#455A64��  � �� �#455A64� -	   $ 	StackUrta��$��0 0 0 10q�$��1��$��Stretch=�$
��Auto��$y�Visible=� �� $��* ��� $
��Auto �.+��� -
   $ 
scrollView��$��Disabled=����� " �� ��  �� *
HasItems+$	 dataGridProductUrta��	
 False.a���  � ��$
 Auto=�.��  -   $ dataGridProductUrta��$
 Auto=�$
��Auto�� �#263859 .  $ 
Horizontal=� .   .   .  .   .   .  ���	#FF17223B$��1q�$ Single=�$ FullRow=�$��
25 0 20 10q�  ��%�  '        7  '  D     7  '      7  '�� �    �� "  8   .���  �@ GridLinesVisibility$ 
Horizontal=��� "  8   .���  �@�FocusVisualStyle Z� ��  � �� �#FF9900��  � ��$ 0q���  �@��TextAlignment$ Center=� P�  �@ 
IsSelected . . ���  � �� �#263859�� "  8   .���  � �� �#263859��  � ��
 �������  � ��$ 44����  � ��$ Center=� �� " ��.���  � �� �#263859   $	 KOD��$	 100   �� *
product_id+  $ MAXSULOT��$
 300*   �� *name+  $ O'LCHOV��$	 100   �� *measurement+  $ ZAVOD��$
 200*   �� *
preparer+  $ MIQDOR��$	 150   �� *
quantity+  $ NARXI��$
 150*   �� *price+  $ VAL. O'LCHOV��$	 130   �� *val_ul+  $ 
SHTRIX KOD��$
 200*   �� *	barcode+� -   $ GridPageUrta��$��1��$	��200��$y�	Collapsed=� �� �� .+��� -   $! btnPreviusUrta��$��0��{�.  $	" 0.5Q�.���� .+�	  $# 	ArrowLeft=�$��25��$��25������ " ��.���  � ��$ 50����  � ��$ 50����  � �� �#263859��  � �� �#263859��  � ��
 �������  � �� �� "$ ��.R��� $% 50��#��z��#��z��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ���  � ��$ 2q���  � �� �#455A64��  � �� �#455A64�� -   $! btnNextUrta��$��1��.���� .+�	  $# 
ArrowRight=�$��25��$��25������ " ��.���  � ��$ 50����  � ��$ 50����  � �� �#263859��  � �� �#263859��  � ��
 �������  � �� �� "$ ��.R��� $% 50��#��z��#��z��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ���  � ��$ 2q���  � �� �#455A64��  � �� �#455A64� -   $ 
StackMeyor��$��0 0 0 10q�$��1��$��Stretch=�$
��Auto��$y�	Collapsed=� �� $��* ��� $
��Auto �.+��� -   $ scrollVieer��$��Disabled=����� " �� ��  �� *
HasItems+$	 dataGrid��	
 False.a���  � ��$
 Auto=�.��  -   $ dataGridProductMeyor��$
 Auto=�$
��Auto�� �#263859 .  $ 
Horizontal=� .   .   .  .   .   .  ���	#FF17223B$��1q�$ Single=�$ FullRow=�$��
25 0 20 10q�  ��%�  '        7  '  D     7  '      7  '�� �    �� "  8   .���  �@ GridLinesVisibility$ 
Horizontal=��� "  8   .���  �@�FocusVisualStyle Z� ��  � �� �#7EBC59��  � ��$ 0q���  �@��TextAlignment$ Center=� P�  �@ 
IsSelected . . ���  � �� �#263859�� "  8   .���  � �� �#263859��  � ��
 �������  � ��$ 44����  � ��$ Center=� �� " ��.���  � �� �#263859   $	 KOD��$	 100   �� *
product_id+  $ MAXSULOT��$
 300*   �� *name+  $ O'LCHOV��$	 100   �� *measurement+  $ ZAVOD��$
 200*   �� *
preparer+  $ MIQDOR��$	 150   �� *
quantity+  $ NARXI��$
 150*   �� *price+  $ VAL. O'LCHOV��$	 130   �� *val_ul+  $ 
SHTRIX KOD��$
 200*   �� *	barcode+� -   $ GridPageMeyor��$��1��$	��200��$y�	Collapsed=� �� �� .+��� -   $! btnPreviusMeyor��$��0��{�.  $	" 0.5Q�.���� .+�	  $# 	ArrowLeft=�$��25��$��25������ " ��.���  � ��$ 50����  � ��$ 50����  � �� �#263859��  � �� �#263859��  � ��
 �������  � �� �� "$ ��.R��� $% 50��#��z��#��z��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ���  � ��$ 2q���  � �� �#455A64��  � �� �#455A64�� -   $! btnNextMeyor��$��1��.���� .+�	  $# 
ArrowRight=�$��25��$��25������ " ��.���  � ��$ 50����  � ��$ 50����  � �� �#263859��  � �� �#263859��  � ��
 �������  � �� �� "$ ��.R��� $% 50��#��z��#��z��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ���  � ��$ 2q���  � �� �#455A64��  � �� �#455A64�� $��2��.+��� -   $! 	BtnAsosiy��.���� & �� Orientation$& 
Horizontal=�$	��150��.+��� $��Resources/Asosiy.png��$��24��$��24��$��13 0 0 0q��� $��Asosiy��$��25 0 0 0q����� " ��.���  � ��$	 155����  � ��$ 60����  � ��$ Segoe UI)���  � ��$ 18(���  � ��
 �������  � �� �#263859��  � �� �#263859��  � ��$
 Left=���  � ��$ 1q���  � ��$ 	20 0 0 30q���  � �� �� "$ ��.R��� $% 3��#��z��#��z��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ���  � ��$ 2q���  � �� �#455A64��  � �� �#455A64